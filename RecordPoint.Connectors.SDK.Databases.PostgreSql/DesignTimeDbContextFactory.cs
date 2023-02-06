using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RecordPoint.Connectors.SDK.Databases.PostgreSql
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ConnectorDbContext>
    {
        public static DbContextOptionsBuilder<ConnectorDbContext> GetContextOptionsBuilder() => new DbContextOptionsBuilder<ConnectorDbContext>()
                .UseNpgsql("Data Source=connector.db");

        public ConnectorDbContext CreateDbContext(string[] args) => new PostgreSqlConnectorDbContext(GetContextOptionsBuilder().Options, PostgreSqlConnectorDbContext.DEFAULT_DB_SCHEMA_NAME);
    }
}
