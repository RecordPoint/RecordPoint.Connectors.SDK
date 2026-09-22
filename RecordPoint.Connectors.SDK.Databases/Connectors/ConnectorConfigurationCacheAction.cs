using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Caching;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Observability;

namespace RecordPoint.Connectors.SDK.Connectors
{
    /// <summary>
    /// Cache action for retrieving a single connector configuration by ConnectorId.
    /// The cache TTL is driven by <see cref="ConnectorOptions.ConnectorConfigurationCacheTtl"/>.
    /// </summary>
    public class ConnectorConfigurationCacheAction : ICacheAction<ConnectorConfigurationModel>
    {
        private readonly IConnectorDatabaseClient _databaseClient;
        private readonly IObservabilityScope _observabilityScope;
        private readonly IOptions<ConnectorOptions> _connectorOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectorConfigurationCacheAction"/> class.
        /// </summary>
        /// <param name="databaseClient">The database client.</param>
        /// <param name="observabilityScope">The observability scope.</param>
        /// <param name="connectorOptions">The connector options.</param>
        public ConnectorConfigurationCacheAction(
            IConnectorDatabaseClient databaseClient,
            IObservabilityScope observabilityScope,
            IOptions<ConnectorOptions> connectorOptions)
        {
            _databaseClient = databaseClient;
            _observabilityScope = observabilityScope;
            _connectorOptions = connectorOptions;
        }

        /// <summary>
        /// Fetches the connector configuration from the database and returns it with a TTL-based expiry.
        /// </summary>
        /// <param name="context">Cache action context containing the ConnectorId property.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="CacheActionResult{ConnectorConfigurationModel}"/> with the configuration and expiry.</returns>
        public async Task<CacheActionResult<ConnectorConfigurationModel>> ExecuteAsync(CacheActionContext context, CancellationToken cancellationToken = default)
        {
            var connectorId = context?.Properties.TryGetValue(DatabaseConnectorConfigurationManager.CONNECTOR_ID_DIMENSION, out var cid) == true ? cid as string : null;

            if (string.IsNullOrEmpty(connectorId))
            {
                return new CacheActionResult<ConnectorConfigurationModel>
                {
                    CacheItem = null,
                    Expires = null
                };
            }

            var connectorConfiguration = await _observabilityScope.Invoke(
                new Dimensions { [DatabaseConnectorConfigurationManager.CONNECTOR_ID_DIMENSION] = connectorId },
                async () =>
                {
                    using var dbContext = _databaseClient.CreateDbContext();
                    var config = await dbContext.Connectors
                        .FirstOrDefaultAsync(a => a.ConnectorId == connectorId, cancellationToken);

                    if (config == null) return null;

                    return config.ConnectorId.Equals(connectorId, StringComparison.InvariantCultureIgnoreCase)
                        ? config
                        : null;
                });

            var ttlSeconds = _connectorOptions.Value.ConnectorConfigurationCacheTtl;
            if (ttlSeconds <= 0)
            {
                throw new InvalidOperationException(
                    $"{nameof(ConnectorConfigurationCacheAction)} was invoked but {nameof(ConnectorOptions.ConnectorConfigurationCacheTtl)} is 0. " +
                    "The cache should not be used when caching is disabled.");
            }

            return new CacheActionResult<ConnectorConfigurationModel>
            {
                CacheItem = connectorConfiguration,
                Expires = DateTimeOffset.UtcNow.AddSeconds(ttlSeconds)
            };
        }
    }
}
