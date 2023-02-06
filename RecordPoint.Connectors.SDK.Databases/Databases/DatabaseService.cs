using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.Databases
{

    /// <summary>
    /// Implementation of database management services.
    /// This service can be used by some database providers that require preperation prior to use.
    /// e.g. The service can be used to attach a localDb database to the hosting service, or performing EF migrations.
    /// </summary>
    public class DatabaseService<TDbContext, TDbProvider> : BackgroundService
        where TDbContext : DbContext
        where TDbProvider : IDatabaseProvider<TDbContext>
    {

        private readonly ISystemContext _systemContext;
        private readonly TDbProvider _databaseProvider;
        private readonly IScopeManager _scopeManager;
        private readonly ITelemetryTracker _telemetryTracker;
        private readonly ILogger<DatabaseService<TDbContext, TDbProvider>> _logger;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IHostApplicationLifetime _applicationLifetime;

        private readonly string _serviceId;

        /// <summary>
        /// Instantiates a new Database Service
        /// </summary>
        /// <param name="systemContext"></param>
        /// <param name="databaseProvider"></param>
        /// <param name="scopeManager"></param>
        /// <param name="telemetryTracker"></param>
        /// <param name="logger"></param>
        /// <param name="dateTimeProvider"></param>
        /// <param name="applicationLifetime"></param>
        public DatabaseService(
            ISystemContext systemContext,
            TDbProvider databaseProvider,
            IScopeManager scopeManager,
            ITelemetryTracker telemetryTracker,
            ILogger<DatabaseService<TDbContext, TDbProvider>> logger,
            IDateTimeProvider dateTimeProvider,
            IHostApplicationLifetime applicationLifetime)
        {
            _systemContext = systemContext;
            _databaseProvider = databaseProvider;
            _scopeManager = scopeManager;
            _telemetryTracker = telemetryTracker;
            _logger = logger;
            _dateTimeProvider = dateTimeProvider;

            _applicationLifetime = applicationLifetime;

            _serviceId = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Get the name of the service
        /// </summary>
        /// <returns></returns>
        protected string GetServiceName() => "Database Service";

        /// <summary>
        /// Starts the service
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _applicationLifetime.ApplicationStopped.Register(OnShutdown);

            using var systemScope = _scopeManager.BeginSystemScope(_systemContext);
            using var serviceScope = _scopeManager.BeginServiceScope(GetServiceName(), _serviceId);

            var prepareOutcome = await PrepareDatabaseAsync(cancellationToken);
            if (prepareOutcome != WorkResultType.Complete)
                return;

            _databaseProvider.SetReady(null);
        }

        /// <summary>
        /// Executes the Database Preparation Operation
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<WorkResultType> PrepareDatabaseAsync(CancellationToken cancellationToken)
        {
            // Work items already log so no need to repeat it
            var prepareDatabaseOperation = new PrepareDatabaseOperation<TDbContext, TDbProvider>(_databaseProvider, _scopeManager, _logger, _telemetryTracker, _dateTimeProvider);
            await prepareDatabaseOperation.RunAsync(null, cancellationToken);
            if (prepareDatabaseOperation.Exception != null)
                _databaseProvider.SetReady(prepareDatabaseOperation.Exception);
            return prepareDatabaseOperation.ResultType;
        }

        /// <summary>
        /// Executes the Services
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override Task ExecuteAsync(CancellationToken stoppingToken) => Task.CompletedTask;

        private void OnShutdown()
        {
            using var systemScope = _scopeManager.BeginSystemScope(_systemContext);
            using var serviceScope = _scopeManager.BeginServiceScope(GetServiceName(), _serviceId);

            var startupDimensions = new Dimensions()
            {
                [StandardDimensions.EVENT_TYPE] = EventType.Shutdown.ToString()
            };
            using var cleanupScope = _scopeManager.BeginScope(startupDimensions);
            _logger.LogEvent($"Cleaning up {GetServiceName()}", startupDimensions);

            try
            {
                var dimensions = new Dimensions();
                _scopeManager.InvokeAsync(dimensions, () =>
                    _databaseProvider.CleanupAsync(CancellationToken.None)
                ).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                _logger.LogEventException($"Cleaning up {GetServiceName()}", ex);
            }
        }

    }
}
