using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Context;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos
{
    public class CosmosDbConnectorDatabaseProvider : CosmosDbDatabaseProvider<ConnectorDbContext>, IConnectorDatabaseProvider
    {
        private readonly IOptions<CosmosDbConnectorDatabaseOptions> _databaseOptions;

        public CosmosDbConnectorDatabaseProvider(
            ISystemContext systemContext,
            IConfiguration configuration,
            ILogger<CosmosDbDatabaseProvider<ConnectorDbContext>> logger,
            IOptions<CosmosDbConnectorDatabaseOptions> databaseOptions)
            : base(systemContext, configuration, logger, databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }

        protected override string GetDatabaseName() => _databaseOptions.Value.DatabaseName;

        protected override string GetAdminConnectionString() => GetConnectionString();

        public override string GetConnectionString() => _databaseOptions.Value.ConnectionString;

        protected override ConnectorDbContext CreateDbAdminContext() => CreateDbContext();

        public override ConnectorDbContext CreateDbContext()
        {
            return new CosmosDbConnectorDbContext(GetContextOptionsBuilder().Options);
        }

    }
}