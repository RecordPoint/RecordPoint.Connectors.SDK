using Microsoft.EntityFrameworkCore;

namespace RecordPoint.Connectors.SDK.Databases.PostgreSql
{

    /// <summary>
    /// PostgreSql Connector Database Context
    /// </summary>
    public class PostgreSqlConnectorDbContext : ConnectorDbContext
    {
        public const string DEFAULT_DB_SCHEMA_NAME = "connector";

        private readonly string _schemaName;

        public PostgreSqlConnectorDbContext(DbContextOptions<ConnectorDbContext> options, string schemaName)
            : base(options)
        {
            _schemaName = schemaName;
        }

        protected override string GetSchema()
        {
            return _schemaName;
        }

    }
}
