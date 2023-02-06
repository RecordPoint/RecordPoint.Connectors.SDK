using Microsoft.EntityFrameworkCore;

namespace RecordPoint.Connectors.SDK.Databases.AzureSql
{

    /// <summary>
    /// AzureSql Connector Database Context
    /// </summary>
    public class AzureSqlConnectorDbContext : ConnectorDbContext
    {
        public const string DEFAULT_DB_SCHEMA_NAME = "connector";

        private readonly string _schemaName;

        public AzureSqlConnectorDbContext(DbContextOptions<ConnectorDbContext> options, string schemaName)
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
