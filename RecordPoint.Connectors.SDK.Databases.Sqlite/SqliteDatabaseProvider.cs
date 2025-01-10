using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RecordPoint.Connectors.SDK.Context;

namespace RecordPoint.Connectors.SDK.Databases.Sqlite
{
    /// <summary>
    /// The sqlite database provider.
    /// </summary>
    /// <typeparam name="TDbContext"/>
    public abstract class SqliteDatabaseProvider<TDbContext> : ISqliteDatabaseProvider<TDbContext>
        where TDbContext : DbContext
    {

        /// <summary>
        /// The SQLITE SYSTEM NAME.
        /// </summary>
        public const string SQLITE_SYSTEM_NAME = "Sqlite";
        /// <summary>
        /// The SQLITE EXTENSION.
        /// </summary>
        public const string SQLITE_EXTENSION = "sqlite";

        /// <summary>
        /// The system context.
        /// </summary>
        private readonly ISystemContext _systemContext;

        /// <summary>
        /// The ready source.
        /// </summary>
        private readonly TaskCompletionSource<Exception> _readySource = new();

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="systemContext">The system context.</param>
        protected SqliteDatabaseProvider(ISystemContext systemContext)
        {
            _systemContext = systemContext;
        }

        /// <summary>
        /// Required override to get the database name
        /// </summary>
        /// <returns></returns>
        public abstract string GetDatabaseName();

        /// <summary>
        /// Get the data directory
        /// </summary>
        /// <returns></returns>
        public virtual string GetDataDirectory()
        {
            return _systemContext.GetDataRootPath();
        }

        /// <summary>
        /// Get the full path to an sqllite database
        /// </summary>
        /// <returns>Path to the selected database</returns>
        public virtual string GetDatabasePath()
        {
            return Path.Join(GetDataDirectory(), Path.ChangeExtension(GetDatabaseName(), SQLITE_EXTENSION));
        }

        /// <summary>
        /// Get the connection string for a database
        /// </summary>
        /// <returns>Connection string for the database</returns>
        public virtual string GetConnectionString()
        {
            var builder = new SqliteConnectionStringBuilder
            {
                DataSource = GetDatabasePath()
            };
            return builder.ConnectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task PrepareAsync(CancellationToken cancellationToken)
        {
            // Make sure the data directory exists
            Directory.CreateDirectory(GetDataDirectory());

            using var dbContext = CreateDbContext();
            await dbContext.Database.MigrateAsync(cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task CleanupAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Does the database exist?
        /// </summary>
        /// <returns>True if the database exists, otherwise false</returns>
        public bool Exists()
        {
            var filePath = new FileInfo(GetDatabasePath());
            return filePath.Exists;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task RemoveAsync(CancellationToken cancellationToken)
        {
            File.Delete(GetDatabasePath());
            return Task.CompletedTask;
        }

        /// <summary>
        /// Called by dependant services to determine if the database is ready for operation.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Readiness Task</returns>
        public async Task ReadyAsync(CancellationToken cancellationToken)
        {
            // Wait till ready or its been cancelled
            var tcs = new TaskCompletionSource<bool>();
            cancellationToken.Register(s => tcs.SetResult(true), tcs);
            await Task.WhenAny(_readySource.Task, tcs.Task).ConfigureAwait(false);

            cancellationToken.ThrowIfCancellationRequested();

            var exception = await _readySource.Task.ConfigureAwait(false);
            if (exception != null)
                throw exception;
        }

        /// <summary>
        /// Set the readiness for this service
        /// </summary>
        /// <param name="exception">Exception that indicates that the service has critically failed</param>
        public void SetReady(Exception exception)
        {
            _readySource.SetResult(exception);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual DbContextOptionsBuilder<TDbContext> GetContextOptionsBuilder()
        {
            var builder = new DbContextOptionsBuilder<TDbContext>()
                .UseSqlite(GetConnectionString());
            return builder;
        }

        /// <summary>
        /// Create a connector db context
        /// </summary>
        /// <returns>Connector db context</returns>
        public abstract TDbContext CreateDbContext();

        /// <summary>
        /// Get the external system name
        /// </summary>
        /// <returns></returns>
        public virtual string GetExternalSystemName()
        {
            return SQLITE_SYSTEM_NAME;
        }

    }
}
