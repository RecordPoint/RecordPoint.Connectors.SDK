#nullable enable
using Microsoft.EntityFrameworkCore;
using Moq;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Test.Mock.Databases;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.Test.Databases
{
    /// <summary>
    /// Concrete test double for the abstract <see cref="CommonSqlDbProvider{TDbContext}"/>.
    /// Uses the EF Core in-memory provider so the non-migration behaviour can be exercised.
    /// </summary>
    public sealed class TestSqlDbProvider : CommonSqlDbProvider<ConnectorDbContext>
    {
        private readonly string _dbName = Guid.NewGuid().ToString();

        public TestSqlDbProvider(ISystemContext systemContext, ITelemetryTracker telemetryTracker)
            : base(systemContext, telemetryTracker)
        {
        }

        public bool CheckDatabaseExistsCalled { get; private set; }

        protected override string GetDatabaseName() => _dbName;

        protected override string GetAdminConnectionString() => "admin-connection-string";

        public override string GetConnectionString() => "connection-string";

        protected override void CheckDatabaseExists(CancellationToken cancellationToken)
        {
            CheckDatabaseExistsCalled = true;
        }

        protected override DbContextOptionsBuilder<ConnectorDbContext> GetAdminContextOptionsBuilder()
            => new DbContextOptionsBuilder<ConnectorDbContext>().UseInMemoryDatabase(_dbName);

        protected override DbContextOptionsBuilder<ConnectorDbContext> GetContextOptionsBuilder()
            => new DbContextOptionsBuilder<ConnectorDbContext>().UseInMemoryDatabase(_dbName);

        public override ConnectorDbContext CreateDbContext()
            => new MockConnectorDBContext(GetContextOptionsBuilder().Options);

        protected override ConnectorDbContext CreateDbAdminContext()
            => new MockConnectorDBContext(GetAdminContextOptionsBuilder().Options);

        protected override string GetSqlDatabaseScript(string scriptName, Dictionary<string, string> parameters)
            => $"{scriptName}:{parameters.Count}";
    }

    public class CommonSqlDbProviderTests
    {
        private static TestSqlDbProvider CreateProvider(string connectorName = "TestConnector")
        {
            var systemContext = new Mock<ISystemContext>();
            systemContext.Setup(x => x.GetConnectorName()).Returns(connectorName);
            return new TestSqlDbProvider(systemContext.Object, Mock.Of<ITelemetryTracker>());
        }

        [Fact]
        public void GetExternalSystemName_ReturnsConnectorName()
        {
            var provider = CreateProvider("MyConnector");
            Assert.Equal("MyConnector", provider.GetExternalSystemName());
        }

        [Fact]
        public void GetConnectionString_ReturnsValue()
        {
            var provider = CreateProvider();
            Assert.Equal("connection-string", provider.GetConnectionString());
        }

        [Fact]
        public void Exists_ReturnsTrue_ForInMemoryProvider()
        {
            var provider = CreateProvider();
            Assert.True(provider.Exists());
        }

        [Fact]
        public async Task CleanupAsync_And_RemoveAsync_Complete()
        {
            var provider = CreateProvider();
            Assert.Null(await Record.ExceptionAsync(async () =>
            {
                await provider.CleanupAsync(CancellationToken.None);
                await provider.RemoveAsync(CancellationToken.None);
            }));
        }

        [Fact]
        public async Task ReadyAsync_CompletesWhenSetReadyWithNull()
        {
            var provider = CreateProvider();
            provider.SetReady(null);

            Assert.Null(await Record.ExceptionAsync(
                () => provider.ReadyAsync(CancellationToken.None)));
        }

        [Fact]
        public async Task ReadyAsync_ThrowsExceptionWhenSetReadyWithException()
        {
            var provider = CreateProvider();
            var expected = new InvalidOperationException("boom");
            provider.SetReady(expected);

            var actual = await Assert.ThrowsAsync<InvalidOperationException>(
                () => provider.ReadyAsync(CancellationToken.None));
            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task ReadyAsync_ThrowsWhenCancelled()
        {
            var provider = CreateProvider();
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            await Assert.ThrowsAnyAsync<OperationCanceledException>(
                () => provider.ReadyAsync(cts.Token));
        }

        [Fact]
        public async Task PrepareAsync_ReturnsEarly_WhenCancelled()
        {
            var provider = CreateProvider();
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            await provider.PrepareAsync(cts.Token);

            Assert.False(provider.CheckDatabaseExistsCalled);
        }

        [Fact]
        public async Task PrepareAsync_ChecksDatabaseAndAttemptsMigration()
        {
            var provider = CreateProvider();

            // The in-memory provider does not support relational migrations, so
            // MigrateAsync throws; this confirms the check + migrate path was reached.
            await Assert.ThrowsAnyAsync<Exception>(() => provider.PrepareAsync(CancellationToken.None));

            Assert.True(provider.CheckDatabaseExistsCalled);
        }
    }
}
