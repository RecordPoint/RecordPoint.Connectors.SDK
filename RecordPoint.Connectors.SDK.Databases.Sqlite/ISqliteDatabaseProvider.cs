using Microsoft.EntityFrameworkCore;

namespace RecordPoint.Connectors.SDK.Databases.Sqlite
{
    /// <summary>
    /// Defines extensions to the database provider specifically for sqllite
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public interface ISqliteDatabaseProvider<out TDbContext> : IDatabaseProvider<TDbContext>
        where TDbContext : DbContext
    {
        /// <summary> 
        /// Get the path to the sqlite database
        /// </summary>
        /// <returns>File path</returns>
        string GetDatabasePath();
    }
}
