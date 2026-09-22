#nullable enable
using System;
using System.Linq;
using RecordPoint.Connectors.SDK.Databases;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.AzureSql.Test
{
    public class DesignTimeDbContextFactoryTests
    {
        [Fact]
        public void GetContextOptionsBuilder_ConfiguresSqlServer()
        {
            var builder = DesignTimeDbContextFactory.GetContextOptionsBuilder();

            Assert.NotNull(builder);
            Assert.True(builder.Options.Extensions.Any());
        }

        [Fact]
        public void CreateDbContext_ReturnsAzureSqlConnectorDbContext()
        {
            var factory = new DesignTimeDbContextFactory();

            using ConnectorDbContext context = factory.CreateDbContext(System.Array.Empty<string>());

            Assert.NotNull(context);
            Assert.IsType<AzureSqlConnectorDbContext>(context);
        }
    }
}
