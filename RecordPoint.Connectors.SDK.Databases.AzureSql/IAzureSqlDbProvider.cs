using Microsoft.EntityFrameworkCore;

namespace RecordPoint.Connectors.SDK.Databases.AzureSql
{

    /// <summary>
    /// Defines extensions to the database provider specifically for AzureSql
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IAzureSqlDbProvider<TDbContext> : IDatabaseProvider<TDbContext>
        where TDbContext : DbContext
    {

    }

}
