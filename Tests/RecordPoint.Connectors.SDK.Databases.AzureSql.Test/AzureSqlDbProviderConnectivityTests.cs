#nullable enable
using System.Threading;
using RecordPoint.Connectors.SDK.Databases;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.AzureSql.Test
{
    /// <summary>
    /// Tests that exercise the "database not reachable" code paths. These do not require a
    /// live SQL Server: an unreachable host makes EF Core's CanConnect() return false, which
    /// drives the failure branch of CheckDatabaseExists. The success branch (schema creation)
    /// genuinely requires a live Azure SQL instance and is not exercised here.
    /// </summary>
    public class AzureSqlDbProviderConnectivityTests
    {
        [Fact]
        public void Exists_UnreachableDatabase_ReturnsFalse()
        {
            var provider = TestFactory.CreateProvider();

            Assert.False(provider.Exists());
        }

        [Fact]
        public void CheckDatabaseExists_UnreachableDatabase_ThrowsConnectorDatabaseException()
        {
            var provider = TestFactory.CreateProvider();

            var ex = Assert.Throws<ConnectorDatabaseException>(() =>
                provider.CallCheckDatabaseExists(CancellationToken.None));

            Assert.Contains("ConnectorTestDb", ex.Message);
        }

        [Fact]
        public async System.Threading.Tasks.Task PrepareAsync_CancelledToken_ReturnsWithoutConnecting()
        {
            var provider = TestFactory.CreateProvider();
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            // Should return immediately without throwing (no connectivity attempt).
            Assert.Null(await Record.ExceptionAsync(() => provider.PrepareAsync(cts.Token)));
        }

        [Fact]
        public async System.Threading.Tasks.Task PrepareAsync_UnreachableDatabase_ThrowsConnectorDatabaseException()
        {
            var provider = TestFactory.CreateProvider();

            await Assert.ThrowsAsync<ConnectorDatabaseException>(() =>
                provider.PrepareAsync(CancellationToken.None));
        }

        [Fact]
        public void GetExternalSystemName_ReturnsConnectorName()
        {
            var provider = TestFactory.CreateProvider(
                systemContext: TestFactory.SystemContext("MyConnector").Object);

            Assert.Equal("MyConnector", provider.GetExternalSystemName());
        }
    }
}
