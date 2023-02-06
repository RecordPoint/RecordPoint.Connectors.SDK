using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecordPoint.Connectors.SDK.Context;

namespace RecordPoint.Connectors.SDK.Databases
{


    public abstract class CommonSqlDbProvider<TDbContext>
        where TDbContext : DbContext
    {
        protected readonly ISystemContext _systemContext;
        protected readonly ILogger _logger;
        protected readonly TaskCompletionSource<Exception> _readySource = new();

        protected CommonSqlDbProvider(
            ISystemContext systemContext,
            ILogger logger)
        {
            _systemContext = systemContext;
            _logger = logger;
        }

        /// <summary>
        /// Required override to get the database name
        /// </summary>
        /// <returns></returns>
        protected abstract string GetDatabaseName();

        /// <summary>
        /// Required override to get the admin level connection string for the database
        /// </summary>
        /// <returns>Admin level Connection string for the database</returns>
        protected abstract string GetAdminConnectionString();

        /// <summary>
        /// Required override to get the service level connection string for the database
        /// </summary>
        /// <returns>Connection string for the database</returns>
        public abstract string GetConnectionString();

        /// <summary>
        /// Performs any initialisation for the database such as attaching the DB file, and/or running EF migrations
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task PrepareAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            CheckDatabaseExists(cancellationToken);

            using var dbContext = CreateDbContext();
            await dbContext.Database.MigrateAsync(cancellationToken);
        }

        protected abstract void CheckDatabaseExists(CancellationToken cancellationToken);

        /// <summary>
        /// Database cleanup
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        public Task CleanupAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }


        public Task RemoveAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected abstract DbContextOptionsBuilder<TDbContext> GetAdminContextOptionsBuilder();

        protected abstract DbContextOptionsBuilder<TDbContext> GetContextOptionsBuilder();

        /// <summary>
        /// Called by dependent services to determine if the database is ready for operation.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Readiness Task</returns>
        public async Task ReadyAsync(CancellationToken cancellationToken)
        {
            // Wait till ready or its been cancelled
            var tcs = new TaskCompletionSource<bool>();
            cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).SetResult(true), tcs);
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
        /// Create a connector db context
        /// </summary>
        /// <returns>Connector db context</returns>
        public abstract TDbContext CreateDbContext();

        /// <summary>
        /// Create a connector db context with admin permissions
        /// </summary>
        /// <returns>Connector db context</returns>
        protected abstract TDbContext CreateDbAdminContext();

        protected abstract string GetSqlDatabaseScript(string scriptName, Dictionary<string, string> parameters);

        public string GetExternalSystemName()
        {
            return _systemContext.GetConnectorName();
        }

        public bool Exists()
        {
            return CreateDbAdminContext().Database.CanConnect();
        }
    }

}
