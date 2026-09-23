#nullable enable
using Microsoft.EntityFrameworkCore;
using RecordPoint.Connectors.SDK.Databases;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.PostgreSql.Test
{
    public class DesignTimeDbContextFactoryTests
    {
        [Fact]
        public void GetContextOptionsBuilder_IsConfiguredForNpgsql()
        {
            var builder = DesignTimeDbContextFactory.GetContextOptionsBuilder();

            Assert.NotNull(builder);
            Assert.True(builder.IsConfigured);

            using var context = new PostgreSqlConnectorDbContext(builder.Options, "connector");
            Assert.Contains("Npgsql", context.Database.ProviderName);
        }

        [Fact]
        public void CreateDbContext_ReturnsPostgreSqlConnectorDbContext()
        {
            var factory = new DesignTimeDbContextFactory();

            using var context = factory.CreateDbContext(System.Array.Empty<string>());

            Assert.NotNull(context);
            Assert.IsType<PostgreSqlConnectorDbContext>(context);
            Assert.IsAssignableFrom<ConnectorDbContext>(context);
        }

        [Fact]
        public void CreateDbContext_IsDesignTimeFactory()
        {
            var factory = new DesignTimeDbContextFactory();

            Assert.IsAssignableFrom<Microsoft.EntityFrameworkCore.Design.IDesignTimeDbContextFactory<ConnectorDbContext>>(factory);
        }
    }
}
