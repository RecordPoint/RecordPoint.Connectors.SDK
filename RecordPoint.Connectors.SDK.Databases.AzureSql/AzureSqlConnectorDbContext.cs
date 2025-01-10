using Microsoft.EntityFrameworkCore;

namespace RecordPoint.Connectors.SDK.Databases.AzureSql
{
    /// <summary>
    /// The azure sql connector db context.
    /// </summary>
    public class AzureSqlConnectorDbContext : ConnectorDbContext
    {
        /// <summary>
        /// The DEFAULT DB SCHEMA NAME.
        /// </summary>
        public const string DEFAULT_DB_SCHEMA_NAME = "connector";

        /// <summary>
        /// The schema name.
        /// </summary>
        private readonly string _schemaName;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureSqlConnectorDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="schemaName">The schema name.</param>
        public AzureSqlConnectorDbContext(DbContextOptions<ConnectorDbContext> options, string schemaName)
            : base(options)
        {
            _schemaName = schemaName;
        }

        /// <summary>
        /// Get the schema.
        /// </summary>
        /// <returns>A string</returns>
        protected override string GetSchema()
        {
            return _schemaName;
        }

    }
}
