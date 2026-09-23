using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
    /// <remarks>
    /// Instantiates a new Database Service
    /// </remarks>
    public class DatabaseService<TDbContext, TDbProvider>(
        IServiceProvider serviceProvider,
        ISystemContext systemContext,
        TDbProvider databaseProvider,
        IObservabilityScope observabilityScope,
        ITelemetryTracker telemetryTracker,
        IDateTimeProvider dateTimeProvider,
        IHostApplicationLifetime applicationLifetime) : BackgroundService
        where TDbContext : DbContext
        where TDbProvider : IDatabaseProvider<TDbContext>
    {


        /// <summary>
        /// Starts the service
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            applicationLifetime.ApplicationStopped.Register(OnShutdown);

            using var systemScope = observabilityScope.BeginSystemScope(systemContext);

            var prepareOutcome = await PrepareDatabaseAsync(cancellationToken);
            if (prepareOutcome != WorkResultType.Complete)
                return;

            databaseProvider.SetReady(null);
        }

        /// <summary>
        /// Executes the Database Preparation Operation
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<WorkResultType> PrepareDatabaseAsync(CancellationToken cancellationToken)
        {
            // Work items already log so no need to repeat it
            var prepareDatabaseOperation = new PrepareDatabaseOperation<TDbContext, TDbProvider>(serviceProvider, databaseProvider, observabilityScope, telemetryTracker, dateTimeProvider);
            await prepareDatabaseOperation.RunAsync(null, cancellationToken);
            if (prepareDatabaseOperation.Exception != null)
                databaseProvider.SetReady(prepareDatabaseOperation.Exception);
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
            using var systemScope = observabilityScope.BeginSystemScope(systemContext);

            var startupDimensions = new Dimensions()
            {
                [StandardDimensions.EVENT_TYPE] = EventType.Shutdown.ToString()
            };
            using var cleanupScope = observabilityScope.BeginScope(startupDimensions);
            telemetryTracker.TrackTrace($"Cleaning up Database Service", SeverityLevel.Information, startupDimensions);

            try
            {
                var dimensions = new Dimensions();
                observabilityScope.InvokeAsync(dimensions, () =>
                    databaseProvider.CleanupAsync(CancellationToken.None)
                ).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                telemetryTracker.TrackException(ex);
            }
        }

    }
}
