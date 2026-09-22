#nullable enable
using Microsoft.EntityFrameworkCore;
using RecordPoint.Connectors.SDK.Databases;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.AzureSql.Test
{
    public class AzureSqlConnectorDbContextTests
    {
        private static DbContextOptions<ConnectorDbContext> BuildOptions()
            => new DbContextOptionsBuilder<ConnectorDbContext>()
                .UseSqlServer(TestFactory.UnreachableConnectionString)
                .Options;

        [Fact]
        public void DefaultSchemaName_HasExpectedValue()
        {
            Assert.Equal("connector", AzureSqlConnectorDbContext.DEFAULT_DB_SCHEMA_NAME);
        }

        [Fact]
        public void Constructor_StoresSchemaName_ReturnedByGetSchema()
        {
            using var context = new TestableAzureSqlConnectorDbContext(
                BuildOptions(),
                AzureSqlConnectorDbContext.DEFAULT_DB_SCHEMA_NAME);

            Assert.Equal(AzureSqlConnectorDbContext.DEFAULT_DB_SCHEMA_NAME, context.CallGetSchema());
        }

        [Fact]
        public void GetSchema_ReturnsCustomSchema()
        {
            using var context = new TestableAzureSqlConnectorDbContext(BuildOptions(), "custom_schema");

            Assert.Equal("custom_schema", context.CallGetSchema());
        }

        [Fact]
        public void Context_ExposesExpectedDbSets()
        {
            using var context = new AzureSqlConnectorDbContext(
                BuildOptions(),
                AzureSqlConnectorDbContext.DEFAULT_DB_SCHEMA_NAME);

            Assert.NotNull(context.Connectors);
            Assert.NotNull(context.Channels);
            Assert.NotNull(context.Aggregations);
            Assert.NotNull(context.ManagedWorkStatuses);
        }
    }
}
