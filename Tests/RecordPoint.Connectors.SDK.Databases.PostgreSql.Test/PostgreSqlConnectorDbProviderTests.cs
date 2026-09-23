#nullable enable
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Moq;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Observability.Null;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.PostgreSql.Test
{
    public class PostgreSqlConnectorDbProviderTests
    {
        private const string BaseConnectionString = "Server=localhost;Database=mydb;User Id=svc;Password=svcpw";

        private static ITelemetryTracker Telemetry => new NullTelemetryTracker();

        private static Mock<ISystemContext> SystemContext(string connectorName = "TestConnector")
        {
            var mock = new Mock<ISystemContext>();
            mock.Setup(x => x.GetConnectorName()).Returns(connectorName);
            return mock;
        }

        private static TestablePostgreSqlConnectorDbProvider CreateProvider(PostgreSqlConnectorDbOptions options, Mock<ISystemContext>? systemContext = null)
        {
            return new TestablePostgreSqlConnectorDbProvider(
                (systemContext ?? SystemContext()).Object,
                Options.Create(options),
                Telemetry,
                new PostgreSqlConnectionFactory());
        }

        [Fact]
        public void GetConnectionString_ReturnsOptionsConnectionString()
        {
            var provider = CreateProvider(new PostgreSqlConnectorDbOptions { ConnectionString = BaseConnectionString });

            Assert.Equal(BaseConnectionString, provider.GetConnectionString());
        }

        [Fact]
        public void GetDatabaseName_ExtractsInitialCatalog()
        {
            var provider = CreateProvider(new PostgreSqlConnectorDbOptions { ConnectionString = BaseConnectionString });

            Assert.Equal("mydb", provider.PublicGetDatabaseName());
        }

        [Fact]
        public void GetDatabaseName_WhenNoDatabaseInConnectionString_ReturnsEmpty()
        {
            var provider = CreateProvider(new PostgreSqlConnectorDbOptions { ConnectionString = "Server=localhost;User Id=svc;Password=pw" });

            Assert.Equal(string.Empty, provider.PublicGetDatabaseName());
        }

        [Fact]
        public void GetDatabaseName_IsCached_AcrossCalls()
        {
            var provider = CreateProvider(new PostgreSqlConnectorDbOptions { ConnectionString = BaseConnectionString });

            var first = provider.PublicGetDatabaseName();
            var second = provider.PublicGetDatabaseName();

            Assert.Equal("mydb", first);
            Assert.Same(first, second);
        }

        [Fact]
        public void GetAdminConnectionString_WithAdminCredentials_AppliesThem()
        {
            var provider = CreateProvider(new PostgreSqlConnectorDbOptions
            {
                ConnectionString = BaseConnectionString,
                AdminUsername = "admin",
                AdminPassword = "adminpw"
            });

            var adminConnectionString = provider.PublicGetAdminConnectionString();

            var parsed = new SqlConnectionStringBuilder(adminConnectionString);
            Assert.Equal("admin", parsed.UserID);
            Assert.Equal("adminpw", parsed.Password);
            Assert.Equal("mydb", parsed.InitialCatalog);
        }

        [Fact]
        public void GetAdminConnectionString_WithoutAdminCredentials_KeepsOriginalCredentials()
        {
            var provider = CreateProvider(new PostgreSqlConnectorDbOptions
            {
                ConnectionString = BaseConnectionString,
                AdminUsername = string.Empty,
                AdminPassword = string.Empty
            });

            var adminConnectionString = provider.PublicGetAdminConnectionString();

            var parsed = new SqlConnectionStringBuilder(adminConnectionString);
            Assert.Equal("svc", parsed.UserID);
            Assert.Equal("svcpw", parsed.Password);
        }

        [Fact]
        public void GetAdminConnectionString_WithOnlyUsername_AppliesUsernameOnly()
        {
            var provider = CreateProvider(new PostgreSqlConnectorDbOptions
            {
                ConnectionString = BaseConnectionString,
                AdminUsername = "miIdentity",
                AdminPassword = string.Empty
            });

            var parsed = new SqlConnectionStringBuilder(provider.PublicGetAdminConnectionString());
            Assert.Equal("miIdentity", parsed.UserID);
            Assert.Equal("svcpw", parsed.Password);
        }

        [Fact]
        public void CreateDbContext_ReturnsPostgreSqlConnectorDbContext()
        {
            var provider = CreateProvider(new PostgreSqlConnectorDbOptions { ConnectionString = BaseConnectionString });

            using var context = provider.CreateDbContext();

            Assert.IsType<PostgreSqlConnectorDbContext>(context);
        }

        [Fact]
        public void CreateDbAdminContext_ReturnsPostgreSqlConnectorDbContext()
        {
            var provider = CreateProvider(new PostgreSqlConnectorDbOptions
            {
                ConnectionString = BaseConnectionString,
                AdminUsername = "admin",
                AdminPassword = "adminpw"
            });

            using var context = provider.PublicCreateDbAdminContext();

            Assert.IsType<PostgreSqlConnectorDbContext>(context);
        }

        [Fact]
        public void GetExternalSystemName_ReturnsConnectorName()
        {
            var systemContext = SystemContext("MyConnector");
            var provider = CreateProvider(new PostgreSqlConnectorDbOptions { ConnectionString = BaseConnectionString }, systemContext);

            Assert.Equal("MyConnector", provider.GetExternalSystemName());
            systemContext.Verify(x => x.GetConnectorName(), Times.Once);
        }

        [Fact]
        public void Provider_ImplementsConnectorDatabaseProvider()
        {
            var provider = CreateProvider(new PostgreSqlConnectorDbOptions { ConnectionString = BaseConnectionString });

            Assert.IsType<IConnectorDatabaseProvider>(provider, false);
        }
    }
}
