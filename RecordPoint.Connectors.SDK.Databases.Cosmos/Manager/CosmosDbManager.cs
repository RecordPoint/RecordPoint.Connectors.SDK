using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using RecordPoint.Connectors.SDK.Observability;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos.Manager
{
    /// <summary>
    /// The cosmos db manager.
    /// </summary>
    /// <typeparam name="T"/>
    public class CosmosDbManager<T> : ICosmosDbManager<T> where T : BaseCosmosDbItem
    {
        private readonly ITelemetryTracker _telemetryTracker;
        private readonly Container? _container;
        private readonly string _containerId;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="cosmosClient">The cosmos client.</param>
        /// <param name="databaseId">The database id.</param>
        /// <param name="containerId">The container id.</param>
        /// <param name="telemetryTracker">The telemetry tracker.</param>
        public CosmosDbManager(CosmosClient cosmosClient, string databaseId, string containerId, ITelemetryTracker telemetryTracker)
        {
            _container = cosmosClient.GetContainer(databaseId, containerId);
            _containerId = containerId;
            _telemetryTracker = telemetryTracker;
        }

        /// <inheritdoc/>
        public async Task UpsertAsync(string partitionKey, T item, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _container!.UpsertItemAsync(item, new PartitionKey(partitionKey), cancellationToken: cancellationToken);
                _telemetryTracker.TrackMetric(CosmosMetricConstants.RequestCharge, response.RequestCharge, CosmosMetricConstants.ContainerDimension, _containerId);
            }
            catch (CosmosException ex)
            {
                _telemetryTracker.TrackMetric(CosmosMetricConstants.RequestCharge, ex.RequestCharge, CosmosMetricConstants.ContainerDimension, _containerId);
                _telemetryTracker.TrackException(ex);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<T?> GetAsync(string partitionKey, string id, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _container!.ReadItemAsync<T>(id, new PartitionKey(partitionKey), null, cancellationToken: cancellationToken);
                _telemetryTracker.TrackMetric(CosmosMetricConstants.RequestCharge, response.RequestCharge, CosmosMetricConstants.ContainerDimension, _containerId);
                return (T)response;
            }
            catch (CosmosException ex)
            {
                _telemetryTracker.TrackMetric(CosmosMetricConstants.RequestCharge, ex.RequestCharge, CosmosMetricConstants.ContainerDimension, _containerId);
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(string partitionKey, string id, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _container!.DeleteItemAsync<T>(id, new PartitionKey(partitionKey), cancellationToken: cancellationToken);
                _telemetryTracker.TrackMetric(CosmosMetricConstants.RequestCharge, response.RequestCharge, CosmosMetricConstants.ContainerDimension, _containerId);
            }
            catch (CosmosException ex)
            {
                _telemetryTracker.TrackMetric(CosmosMetricConstants.RequestCharge, ex.RequestCharge, CosmosMetricConstants.ContainerDimension, _containerId);
                throw;
            }
        }

        /// <inheritdoc/>
        public async IAsyncEnumerable<T> QuerySqlAsync(string partitionKey, QueryDefinition query, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            double totalRequestCharge = 0;

            using var feedIterator = _container!.GetItemQueryIterator<T>(query, null,
                requestOptions: string.IsNullOrWhiteSpace(partitionKey) ? default : new QueryRequestOptions { PartitionKey = new PartitionKey(partitionKey) });

            try
            {
                while (feedIterator.HasMoreResults)
                {
                    FeedResponse<T> items;
                    try
                    {
                        items = await feedIterator.ReadNextAsync(cancellationToken);
                        totalRequestCharge += items.RequestCharge;
                    }
                    catch (CosmosException ex)
                    {
                        totalRequestCharge += ex.RequestCharge;
                        throw;
                    }

                    foreach (var item in items)
                    {
                        yield return item;
                    }
                }
            }
            finally
            {
                try
                {
                    if (totalRequestCharge > 0)
                    {
                        _telemetryTracker.TrackMetric(CosmosMetricConstants.RequestCharge, totalRequestCharge, CosmosMetricConstants.ContainerDimension, _containerId);
                    }
                }
                catch
                {
                    // Telemetry must never mask the original exception during stack unwinding
                }
            }
        }

        /// <inheritdoc/>
        public async IAsyncEnumerable<T> QueryLinqAsync(IQueryable<T> query)
        {
            double totalRequestCharge = 0;

            using var feedIterator = GetFeedIterator(query);
            try
            {
                while (feedIterator.HasMoreResults)
                {
                    FeedResponse<T> items;
                    try
                    {
                        items = await feedIterator.ReadNextAsync();
                        totalRequestCharge += items.RequestCharge;
                    }
                    catch (CosmosException ex)
                    {
                        totalRequestCharge += ex.RequestCharge;
                        throw;
                    }

                    foreach (var item in items)
                    {
                        yield return item;
                    }
                }
            }
            finally
            {
                try
                {
                    if (totalRequestCharge > 0)
                    {
                        _telemetryTracker.TrackMetric(CosmosMetricConstants.RequestCharge, totalRequestCharge, CosmosMetricConstants.ContainerDimension, _containerId);
                    }
                }
                catch
                {
                    // Telemetry must never mask the original exception during stack unwinding
                }
            }
        }

        /// <inheritdoc/>
        public IOrderedQueryable<T> GetContainerQuery()
        {
            return _container!.GetItemLinqQueryable<T>();
        }

        /// <inheritdoc/>
        // Thin wrapper over the Cosmos LINQ ToFeedIterator() extension, which only functions against a
        // live Cosmos LINQ provider and therefore cannot be exercised by unit tests. Made virtual so
        // QueryLinqAsync can be covered via a test double that supplies a mocked FeedIterator.
        [ExcludeFromCodeCoverage]
        public virtual FeedIterator<T> GetFeedIterator(IQueryable<T> query)
        {
            return query.ToFeedIterator();
        }

        /// <inheritdoc/>
        public async Task<T> ReplaceAsync(string partitionKey, T item)
        {
            try
            {
                var response = await _container!.ReplaceItemAsync(item, item.Id, new PartitionKey(partitionKey));
                _telemetryTracker.TrackMetric(CosmosMetricConstants.RequestCharge, response.RequestCharge, CosmosMetricConstants.ContainerDimension, _containerId);
                return response.Resource;
            }
            catch (CosmosException ex)
            {
                _telemetryTracker.TrackMetric(CosmosMetricConstants.RequestCharge, ex.RequestCharge, CosmosMetricConstants.ContainerDimension, _containerId);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<T> UpsertMatchingEtagAsync(string partitionKey, T item)
        {
            try
            {
                var response = await _container!.UpsertItemAsync(item, new PartitionKey(partitionKey),
                    new ItemRequestOptions { IfMatchEtag = item.ETag });
                _telemetryTracker.TrackMetric(CosmosMetricConstants.RequestCharge, response.RequestCharge, CosmosMetricConstants.ContainerDimension, _containerId);
                return response.Resource;
            }
            catch (CosmosException ex)
            {
                _telemetryTracker.TrackMetric(CosmosMetricConstants.RequestCharge, ex.RequestCharge, CosmosMetricConstants.ContainerDimension, _containerId);
                throw;
            }
        }
    }
}
