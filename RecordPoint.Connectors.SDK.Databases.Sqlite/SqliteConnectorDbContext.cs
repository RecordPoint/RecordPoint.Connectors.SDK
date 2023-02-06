using Microsoft.EntityFrameworkCore;

namespace RecordPoint.Connectors.SDK.Databases.Sqlite
{
    /// <summary>
    /// Sqlite Connector Database Context
    /// </summary>
    public class SqliteConnectorDbContext : ConnectorDbContext
    {

        public SqliteConnectorDbContext(DbContextOptions<ConnectorDbContext> options)
            : base(options)
        {
        }
    }
}
