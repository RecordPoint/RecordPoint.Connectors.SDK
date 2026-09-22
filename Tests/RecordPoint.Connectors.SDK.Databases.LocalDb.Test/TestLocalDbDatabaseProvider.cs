#nullable enable
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Databases.LocalDb;

namespace RecordPoint.Connectors.SDK.Databases.LocalDb.Test
{
    /// <summary>
    /// Concrete implementation of the abstract <see cref="LocalDbDatabaseProvider{TDbContext}"/>
    /// used to exercise the shared (non database-bound) logic in unit tests.
    /// </summary>
    internal sealed class TestLocalDbDatabaseProvider : LocalDbDatabaseProvider<ConnectorDbContext>
    {
        private readonly string _databaseName;

        public TestLocalDbDatabaseProvider(ISystemContext systemContext, string databaseName)
            : base(systemContext)
        {
            _databaseName = databaseName;
        }

        public override string GetDatabaseName() => _databaseName;

        public override ConnectorDbContext CreateDbContext()
            => new LocalDbConnectorDbContext(GetContextOptionsBuilder().Options);
    }
}
