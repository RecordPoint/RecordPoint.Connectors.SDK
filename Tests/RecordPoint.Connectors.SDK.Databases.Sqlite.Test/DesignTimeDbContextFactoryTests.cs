#nullable enable
using Microsoft.EntityFrameworkCore;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Databases.Sqlite;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.Sqlite.Test
{
    public class DesignTimeDbContextFactoryTests
    {
        private static readonly string[] CreateDbContextArgs = new[] { "arg1" };

        [Fact]
        public void GetContextOptionsBuilder_IsConfiguredForSqlite()
        {
            var builder = DesignTimeDbContextFactory.GetContextOptionsBuilder();

            Assert.NotNull(builder);
            using var context = new SqliteConnectorDbContext(builder.Options);
            Assert.Equal("Microsoft.EntityFrameworkCore.Sqlite", context.Database.ProviderName);
        }

        [Fact]
        public void CreateDbContext_ReturnsSqliteConnectorDbContext()
        {
            var factory = new DesignTimeDbContextFactory();

            using ConnectorDbContext context = factory.CreateDbContext(CreateDbContextArgs);

            Assert.NotNull(context);
            Assert.IsType<SqliteConnectorDbContext>(context);
            Assert.Equal("Microsoft.EntityFrameworkCore.Sqlite", context.Database.ProviderName);
        }
    }
}
