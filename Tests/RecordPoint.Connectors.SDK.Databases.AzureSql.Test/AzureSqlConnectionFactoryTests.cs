#nullable enable
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.AzureSql.Test
{
    public class AzureSqlConnectionFactoryTests
    {
        [Fact]
        public void GetConnection_ReturnsConnectionWithSuppliedConnectionString()
        {
            var factory = new AzureSqlConnectionFactory();
            const string connectionString =
                "Server=example.database.windows.net;Database=TestDb;Encrypt=False;TrustServerCertificate=True";

            using var connection = factory.GetConnection(connectionString);

            Assert.NotNull(connection);
            Assert.Equal(connectionString, connection.ConnectionString);
            Assert.Equal("TestDb", connection.Database);
        }

        [Fact]
        public void GetConnection_ReturnsNewInstanceEachCall()
        {
            var factory = new AzureSqlConnectionFactory();
            const string connectionString = "Server=localhost;Database=Db;Encrypt=False";

            using var first = factory.GetConnection(connectionString);
            using var second = factory.GetConnection(connectionString);

            Assert.NotSame(first, second);
        }
    }
}
