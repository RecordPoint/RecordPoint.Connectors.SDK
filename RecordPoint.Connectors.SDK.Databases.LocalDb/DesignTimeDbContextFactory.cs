using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RecordPoint.Connectors.SDK.Databases.LocalDb
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ConnectorDbContext>
    {
        public static DbContextOptionsBuilder<ConnectorDbContext> GetContextOptionsBuilder() => new DbContextOptionsBuilder<ConnectorDbContext>()
                .UseSqlServer("Data Source=connector.db");

        public ConnectorDbContext CreateDbContext(string[] args) => new LocalDbConnectorDbContext(GetContextOptionsBuilder().Options);
    }
}
