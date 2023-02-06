using Microsoft.EntityFrameworkCore;
using RecordPoint.Connectors.SDK.Databases;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Test.Mock.Databases
{

    /// <summary>
    /// Mock connector database provider
    /// </summary>
    public class MockConnectorDatabaseProvider : IConnectorDatabaseProvider
    {
        public const string SYSTEM_NAME = "Mock Database";
        private bool _exists = false;
        private readonly string _dbName = Guid.NewGuid().ToString();

        /// <summary>
        /// Get a unique database name for every instance to ensure data is not conflicting across tests
        /// </summary>
        /// <returns></returns>
        public virtual string GetDatabaseName() => _dbName;

        public ConnectorDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ConnectorDbContext>()
                .UseInMemoryDatabase(GetDatabaseName())
                .Options;
            return new MockConnectorDBContext(options);
        }

        public bool Exists()
        {
            // Mock database exists unless it was specifically deleted
            return _exists;
        }

        public string GetConnectionString()
        {
            // Cannot be used with the mock database
            throw new NotImplementedException();
        }

        public string GetExternalSystemName()
        {
            return SYSTEM_NAME;
        }

        public Task PrepareAsync(CancellationToken cancellationToken)
        {
            // Make sure that any existing test databases get deleted prior to running tests
            CreateDbContext().Database.EnsureDeleted();
            _exists = true;
            return Task.CompletedTask;
        }

        public Task RemoveAsync(CancellationToken cancellationToken)
        {
            // Delete the database
            _exists = false;
            return CreateDbContext().Database.EnsureDeletedAsync(cancellationToken);
        }

        public Task CleanupAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task ReadyAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void SetReady(Exception exception)
        {
            Task.Delay(0);
        }

    }
}

