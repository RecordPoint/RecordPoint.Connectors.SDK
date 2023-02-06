using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Context;

namespace RecordPoint.Connectors.SDK.Databases.AzureSql
{

    public class AzureSqlConnectorDbProvider : AzureSqlDbProvider<ConnectorDbContext>, IConnectorDatabaseProvider
    {
        private readonly IOptions<AzureSqlConnectorDbOptions> _databaseOptions;

        private readonly Lazy<string> _databaseName;
        private readonly Lazy<string> _adminConnectionString;

        public AzureSqlConnectorDbProvider(
            ISystemContext systemContext,
            IOptions<AzureSqlConnectorDbOptions> databaseOptions,
            ILogger<AzureSqlDbProvider<ConnectorDbContext>> logger,
            IAzureSqlConnectionFactory connectionFactory)
            : base(systemContext, logger, connectionFactory, databaseOptions)
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

        protected override string GetDatabaseName() => _databaseName.Value;

        protected override string GetAdminConnectionString()
        {
            return _adminConnectionString.Value;
        }

        public override string GetConnectionString()
        {
            return _databaseOptions.Value.ConnectionString;
        }

        protected override ConnectorDbContext CreateDbAdminContext()
        {
            return new AzureSqlConnectorDbContext(GetAdminContextOptionsBuilder().Options, AzureSqlConnectorDbContext.DEFAULT_DB_SCHEMA_NAME);
        }

        public override ConnectorDbContext CreateDbContext()
        {
            return new AzureSqlConnectorDbContext(GetContextOptionsBuilder().Options, AzureSqlConnectorDbContext.DEFAULT_DB_SCHEMA_NAME);
        }
    }
}
