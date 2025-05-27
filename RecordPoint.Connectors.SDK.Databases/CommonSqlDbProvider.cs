using Microsoft.EntityFrameworkCore;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;

namespace RecordPoint.Connectors.SDK.Databases
{
    /// <summary>
    /// The common sql db provider.
    /// </summary>
    /// <typeparam name="TDbContext"/>
    public abstract class CommonSqlDbProvider<TDbContext>
        where TDbContext : DbContext
    {
        /// <summary>
        /// The system context.
        /// </summary>
        protected readonly ISystemContext _systemContext;
        /// <summary>
        /// The telemetry tracker.
        /// </summary>
        protected readonly ITelemetryTracker _telemetryTracker;
        /// <summary>
        /// The ready source.
        /// </summary>
        protected readonly TaskCompletionSource<Exception> _readySource = new();

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="systemContext">The system context.</param>
        /// <param name="telemetryTracker">The telemetry tracker.</param>
        protected CommonSqlDbProvider(
            ISystemContext systemContext,
            ITelemetryTracker telemetryTracker)
        {
            _systemContext = systemContext;
            _telemetryTracker = telemetryTracker;
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

        /// <summary>
        /// Check database exists.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        protected abstract void CheckDatabaseExists(CancellationToken cancellationToken);

        /// <summary>
        /// Database cleanup
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        public Task CleanupAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task RemoveAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract DbContextOptionsBuilder<TDbContext> GetAdminContextOptionsBuilder();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scriptName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected abstract string GetSqlDatabaseScript(string scriptName, Dictionary<string, string> parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetExternalSystemName()
        {
            return _systemContext.GetConnectorName();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Exists()
        {
            return CreateDbAdminContext().Database.CanConnect();
        }
    }

}
