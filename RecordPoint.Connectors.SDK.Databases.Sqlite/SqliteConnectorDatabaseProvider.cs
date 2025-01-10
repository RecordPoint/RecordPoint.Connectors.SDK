using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Context;

namespace RecordPoint.Connectors.SDK.Databases.Sqlite
{
    /// <summary>
    /// The sqlite connector database provider.
    /// </summary>
    public class SqliteConnectorDatabaseProvider : SqliteDatabaseProvider<ConnectorDbContext>, IConnectorDatabaseProvider
    {
        /// <summary>
        /// The database options.
        /// </summary>
        private readonly IOptions<SqliteConnectorDatabaseOptions> _databaseOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqliteConnectorDatabaseProvider"/> class.
        /// </summary>
        /// <param name="systemContext">The system context.</param>
        /// <param name="databaseOptions">The database options.</param>
        public SqliteConnectorDatabaseProvider(
            ISystemContext systemContext,
            IOptions<SqliteConnectorDatabaseOptions> databaseOptions)
            : base(systemContext)
        {
            _databaseOptions = databaseOptions;
        }

        /// <summary>
        /// Get database name.
        /// </summary>
        /// <returns>A string</returns>
        public override string GetDatabaseName() => _databaseOptions.Value.DatabaseName;

        /// <summary>
        /// Creates db context.
        /// </summary>
        /// <returns>A ConnectorDbContext</returns>
        public override ConnectorDbContext CreateDbContext()
        {
            return new SqliteConnectorDbContext(GetContextOptionsBuilder().Options);
        }
    }
}
