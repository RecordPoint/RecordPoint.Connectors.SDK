using Microsoft.EntityFrameworkCore;

namespace RecordPoint.Connectors.SDK.Databases.PostgreSql
{

    /// <summary>
    /// Defines extensions to the database provider specifically for PostgreSql
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IPostgreSqlDbProvider<out TDbContext> : IDatabaseProvider<TDbContext>
        where TDbContext : DbContext
    {

    }

}
