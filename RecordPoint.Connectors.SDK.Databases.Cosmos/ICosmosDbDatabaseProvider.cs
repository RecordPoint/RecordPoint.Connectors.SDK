using Microsoft.EntityFrameworkCore;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos
{
    /// <summary>
    /// Defines extensions to the database provider specifically for LocalDb
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public interface ICosmosDbDatabaseProvider<out TDbContext> : IDatabaseProvider<TDbContext>
        where TDbContext : DbContext
    {

        /// <summary>
        /// Get the path to the database file
        /// </summary>
        /// <returns>File path</returns>
        string GetDatabasePath();

        /// <summary> 
        /// Get the path to the database log file
        /// </summary>
        /// <returns>File path</returns>
        string GetDatabaseLogFilePath();
    }
}
