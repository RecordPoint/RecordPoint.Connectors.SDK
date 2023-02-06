using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RecordPoint.Connectors.SDK.Databases.AzureSql
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ConnectorDbContext>
    {
        public static DbContextOptionsBuilder<ConnectorDbContext> GetContextOptionsBuilder() => new DbContextOptionsBuilder<ConnectorDbContext>()
                .UseSqlServer("Data Source=connector.db");

        public ConnectorDbContext CreateDbContext(string[] args) => new AzureSqlConnectorDbContext(GetContextOptionsBuilder().Options, AzureSqlConnectorDbContext.DEFAULT_DB_SCHEMA_NAME);
    }
}
