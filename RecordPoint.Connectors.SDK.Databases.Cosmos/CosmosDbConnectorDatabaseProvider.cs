using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Toggles;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos
{
    /// <summary>
    /// The cosmos db connector database provider.
    /// </summary>
    public class CosmosDbConnectorDatabaseProvider : CosmosDbDatabaseProvider<ConnectorDbContext>, IConnectorDatabaseProvider
    {
        /// <summary>
        /// The database options.
        /// </summary>
        private readonly IOptions<CosmosDbConnectorDatabaseOptions> _databaseOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="CosmosDbConnectorDatabaseProvider"/> class.
        /// </summary>
        /// <param name="systemContext">The system context.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="telemetryTracker">The telemetry tracker.</param>
        /// <param name="toggleProvider">The toggle provider.</param>
        /// <param name="databaseOptions">The database options.</param>
        public CosmosDbConnectorDatabaseProvider(
            ISystemContext systemContext,
            IConfiguration configuration,
            ITelemetryTracker telemetryTracker,
            IToggleProvider toggleProvider,
            IOptions<CosmosDbConnectorDatabaseOptions> databaseOptions)
            : base(systemContext, configuration, telemetryTracker, toggleProvider, databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }

        /// <summary>
        /// Get database name.
        /// </summary>
        /// <returns>A string</returns>
        protected override string GetDatabaseName() => _databaseOptions.Value.DatabaseName;

        /// <summary>
        /// Get admin connection string.
        /// </summary>
        /// <returns>A string</returns>
        protected override string GetAdminConnectionString() => GetConnectionString();

        /// <summary>
        /// Get connection string.
        /// </summary>
        /// <returns>A string</returns>
        public override string GetConnectionString() => _databaseOptions.Value.ConnectionString;

        /// <summary>
        /// Creates db admin context.
        /// </summary>
        /// <returns>A ConnectorDbContext</returns>
        protected override ConnectorDbContext CreateDbAdminContext() => CreateDbContext();

        /// <summary>
        /// Creates db context.
        /// </summary>
        /// <returns>A ConnectorDbContext</returns>
        public override ConnectorDbContext CreateDbContext()
        {
            return new CosmosDbConnectorDbContext(GetContextOptionsBuilder().Options);
        }
    }
}