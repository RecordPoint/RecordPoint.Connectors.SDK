using Microsoft.EntityFrameworkCore;

namespace RecordPoint.Connectors.SDK.Databases.LocalDb
{

    /// <summary>
    /// Defines extensions to the database provider specifically for LocalDb
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public interface ILocalDbDatabaseProvider<out TDbContext> : IDatabaseProvider<TDbContext>
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
