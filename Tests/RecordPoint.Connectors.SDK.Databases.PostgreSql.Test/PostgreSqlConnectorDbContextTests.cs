#nullable enable
using Microsoft.EntityFrameworkCore;
using RecordPoint.Connectors.SDK.Databases;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.PostgreSql.Test
{
    public class PostgreSqlConnectorDbContextTests
    {
        private sealed class TestablePostgreSqlConnectorDbContext : PostgreSqlConnectorDbContext
        {
            public TestablePostgreSqlConnectorDbContext(DbContextOptions<ConnectorDbContext> options, string schemaName)
                : base(options, schemaName)
            {
            }

            public string PublicGetSchema() => GetSchema();
        }

        private static DbContextOptions<ConnectorDbContext> CreateOptions()
            => new DbContextOptionsBuilder<ConnectorDbContext>()
                .UseNpgsql("Host=localhost;Database=test;Username=u;Password=p")
                .Options;

        [Fact]
        public void DefaultSchemaName_HasExpectedValue()
        {
            Assert.Equal("connector", PostgreSqlConnectorDbContext.DEFAULT_DB_SCHEMA_NAME);
        }

        [Fact]
        public void GetSchema_ReturnsProvidedSchemaName()
        {
            using var context = new TestablePostgreSqlConnectorDbContext(CreateOptions(), "custom_schema");

            Assert.Equal("custom_schema", context.PublicGetSchema());
        }

        [Fact]
        public void Constructor_UsingDefaultSchema_ReturnsDefault()
        {
            using var context = new TestablePostgreSqlConnectorDbContext(
                CreateOptions(),
                PostgreSqlConnectorDbContext.DEFAULT_DB_SCHEMA_NAME);

            Assert.Equal("connector", context.PublicGetSchema());
        }

        [Fact]
        public void Context_IsConnectorDbContext()
        {
            using var context = new PostgreSqlConnectorDbContext(CreateOptions(), "connector");

            Assert.IsAssignableFrom<ConnectorDbContext>(context);
        }
    }
}
