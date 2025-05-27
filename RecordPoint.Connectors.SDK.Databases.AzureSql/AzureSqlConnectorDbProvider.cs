using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;

namespace RecordPoint.Connectors.SDK.Databases.AzureSql
{

    /// <summary>
    /// The azure sql connector db provider.
    /// </summary>
    public class AzureSqlConnectorDbProvider : AzureSqlDbProvider<ConnectorDbContext>, IConnectorDatabaseProvider
    {
        /// <summary>
        /// The database options.
        /// </summary>
        private readonly IOptions<AzureSqlConnectorDbOptions> _databaseOptions;

        /// <summary>
        /// The database name.
        /// </summary>
        private readonly Lazy<string> _databaseName;
        /// <summary>
        /// The admin connection string.
        /// </summary>
        private readonly Lazy<string> _adminConnectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureSqlConnectorDbProvider"/> class.
        /// </summary>
        /// <param name="systemContext">The system context.</param>
        /// <param name="databaseOptions">The database options.</param>
        /// <param name="telemetryTracker">The telemetry tracker.</param>
        /// <param name="connectionFactory">The connection factory.</param>
        public AzureSqlConnectorDbProvider(
            ISystemContext systemContext,
            IOptions<AzureSqlConnectorDbOptions> databaseOptions,
            ITelemetryTracker telemetryTracker,
            IAzureSqlConnectionFactory connectionFactory)
            : base(systemContext, telemetryTracker, connectionFactory, databaseOptions)
        {
            _databaseOptions = databaseOptions;
            _databaseName = new Lazy<string>(() =>
            {
                var connString = GetConnectionString();
                var databaseName = string.Empty;
                if (connString != null)
                {
                    // Extract database name from connection string if it exists. If it doesn't, nothing we can do.
                    var connectionStringBuilder = new SqlConnectionStringBuilder(connString);
                    databaseName = connectionStringBuilder.InitialCatalog;
                }
                return databaseName;
            });

            _adminConnectionString = new Lazy<string>(() =>
            {
                // for MI we expect password to be empty and username to be optionally the name of the MI to use. If left blank it will be inferred from the environment.
                // for non-MI both should be provided
                var connectionStringBuilder = new SqlConnectionStringBuilder(_databaseOptions.Value.ConnectionString);
                if (!string.IsNullOrEmpty(_databaseOptions.Value.AdminUsername))
                {
                    connectionStringBuilder.UserID = _databaseOptions.Value.AdminUsername;
                }
                if (!string.IsNullOrEmpty(_databaseOptions.Value.AdminPassword))
                {
                    connectionStringBuilder.Password = _databaseOptions.Value.AdminPassword;
                }
                return connectionStringBuilder.ConnectionString;
            });
        }

        /// <summary>
        /// Get database name.
        /// </summary>
        /// <returns>A string</returns>
        protected override string GetDatabaseName() => _databaseName.Value;

        /// <summary>
        /// Get admin connection string.
        /// </summary>
        /// <returns>A string</returns>
        protected override string GetAdminConnectionString()
        {
            return _adminConnectionString.Value;
        }

        /// <summary>
        /// Get connection string.
        /// </summary>
        /// <returns>A string</returns>
        public override string GetConnectionString()
        {
            return _databaseOptions.Value.ConnectionString;
        }

        /// <summary>
        /// Creates db admin context.
        /// </summary>
        /// <returns>A ConnectorDbContext</returns>
        protected override ConnectorDbContext CreateDbAdminContext()
        {
            return new AzureSqlConnectorDbContext(GetAdminContextOptionsBuilder().Options, AzureSqlConnectorDbContext.DEFAULT_DB_SCHEMA_NAME);
        }

        /// <summary>
        /// Creates db context.
        /// </summary>
        /// <returns>A ConnectorDbContext</returns>
        public override ConnectorDbContext CreateDbContext()
        {
            return new AzureSqlConnectorDbContext(GetContextOptionsBuilder().Options, AzureSqlConnectorDbContext.DEFAULT_DB_SCHEMA_NAME);
        }
    }
}
