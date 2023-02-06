using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Context;

namespace RecordPoint.Connectors.SDK.Databases.LocalDb
{
    public class LocalDbConnectorDatabaseProvider : LocalDbDatabaseProvider<ConnectorDbContext>, IConnectorDatabaseProvider
    {
        private readonly IOptions<LocalDbConnectorDatabaseOptions> _databaseOptions;
        public LocalDbConnectorDatabaseProvider(ISystemContext systemContext,
            IOptions<LocalDbConnectorDatabaseOptions> databaseOptions)
            : base(systemContext)
        {
            _databaseOptions = databaseOptions;
        }

        public override string GetDatabaseName() => _databaseOptions.Value.DatabaseName;

        public override ConnectorDbContext CreateDbContext()
        {
            return new LocalDbConnectorDbContext(GetContextOptionsBuilder().Options);
        }
    }
}