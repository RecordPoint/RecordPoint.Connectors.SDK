using Microsoft.EntityFrameworkCore;

namespace RecordPoint.Connectors.SDK.Databases.LocalDb
{

    /// <summary>
    /// Sqlite Connector Database Context
    /// </summary>
    public class LocalDbConnectorDbContext : ConnectorDbContext
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public LocalDbConnectorDbContext(DbContextOptions<ConnectorDbContext> options)
            : base(options)
        {

        }


    }
}
