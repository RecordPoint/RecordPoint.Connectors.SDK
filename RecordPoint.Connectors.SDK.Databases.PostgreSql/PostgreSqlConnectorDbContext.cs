using Microsoft.EntityFrameworkCore;

namespace RecordPoint.Connectors.SDK.Databases.PostgreSql
{

    /// <summary>
    /// PostgreSql Connector Database Context
    /// </summary>
    public class PostgreSqlConnectorDbContext : ConnectorDbContext
    {
        /// <summary>
        /// 
        /// </summary>
        public const string DEFAULT_DB_SCHEMA_NAME = "connector";

        private readonly string _schemaName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="schemaName"></param>
        public PostgreSqlConnectorDbContext(DbContextOptions<ConnectorDbContext> options, string schemaName)
            : base(options)
        {
            _schemaName = schemaName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string GetSchema()
        {
            return _schemaName;
        }

    }
}
