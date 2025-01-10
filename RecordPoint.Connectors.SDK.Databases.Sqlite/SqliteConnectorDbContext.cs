using Microsoft.EntityFrameworkCore;

namespace RecordPoint.Connectors.SDK.Databases.Sqlite
{
    /// <summary>
    /// Sqlite Connector Database Context
    /// </summary>
    public class SqliteConnectorDbContext : ConnectorDbContext
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public SqliteConnectorDbContext(DbContextOptions<ConnectorDbContext> options)
            : base(options)
        {
        }
    }
}
