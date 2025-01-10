using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Context;

namespace RecordPoint.Connectors.SDK.Databases.LocalDb
{
    /// <summary>
    /// The local db connector database provider.
    /// </summary>
    public class LocalDbConnectorDatabaseProvider : LocalDbDatabaseProvider<ConnectorDbContext>, IConnectorDatabaseProvider
    {
        /// <summary>
        /// The database options.
        /// </summary>
        private readonly IOptions<LocalDbConnectorDatabaseOptions> _databaseOptions;
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalDbConnectorDatabaseProvider"/> class.
        /// </summary>
        /// <param name="systemContext">The system context.</param>
        /// <param name="databaseOptions">The database options.</param>
        public LocalDbConnectorDatabaseProvider(ISystemContext systemContext,
            IOptions<LocalDbConnectorDatabaseOptions> databaseOptions)
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
            return new LocalDbConnectorDbContext(GetContextOptionsBuilder().Options);
        }
    }
}