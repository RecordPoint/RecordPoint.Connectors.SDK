using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Context;

namespace RecordPoint.Connectors.SDK.Databases.Sqlite
{
    public class SqliteConnectorDatabaseProvider : SqliteDatabaseProvider<ConnectorDbContext>, IConnectorDatabaseProvider
    {
        private readonly IOptions<SqliteConnectorDatabaseOptions> _databaseOptions;

        public SqliteConnectorDatabaseProvider(
            ISystemContext systemContext,
            IOptions<SqliteConnectorDatabaseOptions> databaseOptions)
            : base(systemContext)
        {
            _databaseOptions = databaseOptions;
        }

        public override string GetDatabaseName() => _databaseOptions.Value.DatabaseName;

        public override ConnectorDbContext CreateDbContext()
        {
            return new SqliteConnectorDbContext(GetContextOptionsBuilder().Options);
        }
    }
}
