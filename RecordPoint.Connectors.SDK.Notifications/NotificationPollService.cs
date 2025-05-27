using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;

namespace RecordPoint.Connectors.SDK.Notifications
{

    /// <summary>
    /// Service that regularily polls records 365 for notifications
    /// </summary>
    public class NotificationPollService : BackgroundService
    {

        private readonly ISystemContext _systemContext;
        private readonly IObservabilityScope _observabilityScope;
        private readonly ITelemetryTracker _telemetryTracker;
        private readonly IOptions<NotificationsPollerOptions> _options;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 
        /// </summary>
        public NotificationPollService(
            ISystemContext systemContext,
            IOptions<NotificationsPollerOptions> options,
            IObservabilityScope observabilityScope,
            ITelemetryTracker telemetryTracker,
            IServiceProvider serviceProvider)
        {
            _systemContext = systemContext;
            _options = options;
            _observabilityScope = observabilityScope;
            _telemetryTracker = telemetryTracker;
            _serviceProvider = serviceProvider;
        }

        private TimeSpan GetPollInternal() => TimeSpan.FromSeconds(_options.Value.PollIntervalSeconds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var systemScope = _observabilityScope.BeginSystemScope(_systemContext);

            var startupDimensions = new Dimensions()
            {
                [StandardDimensions.EVENT_TYPE] = EventType.Startup.ToString()
            };
            _telemetryTracker.TrackTrace("Notification polling started", SeverityLevel.Information, startupDimensions);

            while (!stoppingToken.IsCancellationRequested)
            {
                var pollNotificationsOperation = _serviceProvider.GetRequiredService<PollNotificationsOperation>();
                await pollNotificationsOperation.RunAsync(string.Empty, stoppingToken);
                try
                {
                    await Task.Delay(GetPollInternal(), stoppingToken).ConfigureAwait(false);
                }
                catch (TaskCanceledException)
                {
                    // Ignore on purpose

                }
            }
            var shutdownDimensions = new Dimensions()
            {
                [StandardDimensions.EVENT_TYPE] = EventType.Shutdown.ToString()
            };
            _telemetryTracker.TrackTrace("Notification polling stopped", SeverityLevel.Information, shutdownDimensions);
        }

    }
}
