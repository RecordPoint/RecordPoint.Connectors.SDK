using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RecordPoint.Connectors.SDK.Databases.Sqlite
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ConnectorDbContext>
    {
        public static DbContextOptionsBuilder<ConnectorDbContext> GetContextOptionsBuilder() => new DbContextOptionsBuilder<ConnectorDbContext>()
                .UseSqlite("Data Source=connector.db");

        public ConnectorDbContext CreateDbContext(string[] args) => new SqliteConnectorDbContext(GetContextOptionsBuilder().Options);
    }
}
