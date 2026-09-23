using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.Observability;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos
{
    /// <summary>
    /// Cosmos-native implementation of <see cref="IDirectChannelAccess"/> that performs true point reads
    /// using <c>ReadItemStreamAsync</c> instead of EF Core queries.
    /// 
    /// EF Core 8 cannot perform point reads for ChannelModel because the partition key (ConnectorId)
    /// is not part of the primary key (ExternalId only). Every EF Core query generates a SQL SELECT
    /// costing ~3-5 RU. A direct point read costs ~1 RU — saving 24-48M RU/day at scale.
    /// </summary>
    public class CosmosDirectChannelAccess : IDirectChannelAccess
    {
        private const string ChannelContainerName = "channels";

        private readonly IConnectorDatabaseClient _databaseClient;
        private readonly ITelemetryTracker _telemetryTracker;

        /// <summary>
        /// Initializes a new instance of the <see cref="CosmosDirectChannelAccess"/> class.
        /// </summary>
        /// <param name="databaseClient">Database client for obtaining the Cosmos client via DbContext</param>
        /// <param name="telemetryTracker">Telemetry tracker for emitting RU metrics</param>
        public CosmosDirectChannelAccess(
            IConnectorDatabaseClient databaseClient,
            ITelemetryTracker telemetryTracker)
        {
            _databaseClient = databaseClient;
            _telemetryTracker = telemetryTracker;
        }

        /// <inheritdoc/>
        public bool IsEnabled => true;

        /// <inheritdoc/>
        // Requires a live Cosmos-configured EF Core context: it resolves the underlying CosmosClient via
        // dbContext.Database.GetCosmosClient() and performs a network point read (ReadItemStreamAsync).
        // This cannot be exercised without a live Cosmos account. The document-parsing logic it delegates to
        // (ParseChannelDocument) is covered by dedicated unit tests.
        [ExcludeFromCodeCoverage]
        public async Task<ChannelModel?> ReadChannelAsync(string connectorId, string externalId, CancellationToken cancellationToken)
        {
            using var dbContext = _databaseClient.CreateDbContext();
            var cosmosClient = dbContext.Database.GetCosmosClient();
            var databaseName = dbContext.Database.GetCosmosDatabaseId();
            var container = cosmosClient.GetContainer(databaseName, ChannelContainerName);

            try
            {
                using var response = await container.ReadItemStreamAsync(
                    externalId,
                    new PartitionKey(connectorId),
                    cancellationToken: cancellationToken);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    _telemetryTracker.TrackMetric(CosmosMetricConstants.RequestCharge, response.Headers.RequestCharge, CosmosMetricConstants.ContainerDimension, ChannelContainerName);
                    return null;
                }

                response.EnsureSuccessStatusCode();

                _telemetryTracker.TrackMetric(CosmosMetricConstants.RequestCharge, response.Headers.RequestCharge, CosmosMetricConstants.ContainerDimension, ChannelContainerName);

                return ParseChannelDocument(response.Content);
            }
            catch (CosmosException ex)
            {
                _telemetryTracker.TrackMetric(CosmosMetricConstants.RequestCharge, ex.RequestCharge, CosmosMetricConstants.ContainerDimension, ChannelContainerName);

                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                throw;
            }
        }

        /// <inheritdoc/>
        // Requires a live Cosmos-configured EF Core context and a network query (GetItemQueryIterator /
        // ReadNextAsync); cannot be exercised without a live Cosmos account. The projection-parsing logic it
        // delegates to (ParseChannelClassificationDocument) is covered by dedicated unit tests.
        [ExcludeFromCodeCoverage]
        public async IAsyncEnumerable<ChannelClassificationModel> ReadChannelClassificationsAsync(
            string connectorId,
            int pageSize,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            using var dbContext = _databaseClient.CreateDbContext();
            var cosmosClient = dbContext.Database.GetCosmosClient();
            var databaseName = dbContext.Database.GetCosmosDatabaseId();
            var container = cosmosClient.GetContainer(databaseName, ChannelContainerName);

            var query = new QueryDefinition("""
                SELECT VALUE {
                    "ExternalId": c.id,
                    "MetaData": IIF(IS_DEFINED(c.MetaData), c.MetaData, c.metaData)
                }
                FROM c
                """);

            using var iterator = container.GetItemQueryIterator<JsonElement>(
                queryDefinition: query,
                requestOptions: new QueryRequestOptions
                {
                    PartitionKey = new PartitionKey(connectorId),
                    MaxItemCount = pageSize
                });

            while (iterator.HasMoreResults)
            {
                FeedResponse<JsonElement> response;

                try
                {
                    response = await iterator.ReadNextAsync(cancellationToken).ConfigureAwait(false);
                    _telemetryTracker.TrackMetric(CosmosMetricConstants.RequestCharge, response.RequestCharge, CosmosMetricConstants.ContainerDimension, ChannelContainerName);
                }
                catch (CosmosException ex)
                {
                    _telemetryTracker.TrackMetric(CosmosMetricConstants.RequestCharge, ex.RequestCharge, CosmosMetricConstants.ContainerDimension, ChannelContainerName);
                    throw;
                }

                foreach (var item in response)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    yield return ParseChannelClassificationDocument(item);
                }
            }
        }

        /// <summary>
        /// Parses a Cosmos document stream into a <see cref="ChannelModel"/>.
        /// Casing-resilient: tries PascalCase property names first, then falls back to camelCase.
        /// This eliminates silent null returns when <c>UseCamelCaseNamingPolicy</c> doesn't match the stored data.
        /// </summary>
        internal static ChannelModel ParseChannelDocument(Stream content)
        {
            using var document = JsonDocument.Parse(content);
            var root = document.RootElement;

            // "id" is always lowercase — it's the Cosmos document ID field.
            // EF Core maps ChannelModel.ExternalId to this field.
            return new ChannelModel
            {
                ExternalId = GetStringProperty(root, "id"),
                ConnectorId = GetStringProperty(root, "ConnectorId", "connectorId"),
                Title = GetStringProperty(root, "Title", "title"),
                MetaData = GetStringProperty(root, "MetaData", "metaData"),
                CreatedDate = GetDateTimeOffsetProperty(root, "CreatedDate", "createdDate"),
                ContentSynchronisationWorkId = GetStringProperty(root, "ContentSynchronisationWorkId", "contentSynchronisationWorkId")
            };
        }

        internal static ChannelClassificationModel ParseChannelClassificationDocument(JsonElement element)
        {
            return new ChannelClassificationModel
            {
                ExternalId = GetRequiredStringProperty(element, "ExternalId"),
                MetaData = GetStringProperty(element, "MetaData")
            };
        }

        private static string? GetStringProperty(JsonElement element, string primaryName, string? fallbackName = null)
        {
            if (element.TryGetProperty(primaryName, out var prop) && prop.ValueKind != JsonValueKind.Null)
                return prop.GetString();
            if (fallbackName != null && element.TryGetProperty(fallbackName, out prop) && prop.ValueKind != JsonValueKind.Null)
                return prop.GetString();
            return null;
        }

        private static string GetRequiredStringProperty(JsonElement element, string propertyName)
        {
            if (!element.TryGetProperty(propertyName, out var prop))
            {
                throw new InvalidOperationException($"Expected property '{propertyName}' was not present in the Cosmos projection.");
            }

            if (prop.ValueKind == JsonValueKind.Null)
            {
                throw new InvalidOperationException($"Expected property '{propertyName}' was null in the Cosmos projection.");
            }

            return prop.GetString()
                ?? throw new InvalidOperationException($"Expected property '{propertyName}' was not a string in the Cosmos projection.");
        }

        private static DateTimeOffset GetDateTimeOffsetProperty(JsonElement element, string primaryName, string? fallbackName = null)
        {
            if (element.TryGetProperty(primaryName, out var prop) && prop.ValueKind != JsonValueKind.Null)
                return prop.GetDateTimeOffset();
            if (fallbackName != null && element.TryGetProperty(fallbackName, out prop) && prop.ValueKind != JsonValueKind.Null)
                return prop.GetDateTimeOffset();
            return DateTimeOffset.MinValue;
        }
    }
}
