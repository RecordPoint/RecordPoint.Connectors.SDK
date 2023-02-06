using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<NotificationPollService> _logger;
        private readonly IScopeManager _scopeManager;
        private readonly IOptions<NotificationsPollerOptions> _options;
        private readonly string _serviceId;
        private readonly IServiceProvider _serviceProvider;

        private const string SERVICE_TYPE = "Notifications Poller";

        public NotificationPollService(
            ISystemContext systemContext,
            IOptions<NotificationsPollerOptions> options,
            ILogger<NotificationPollService> logger,
            IScopeManager scopeManager,
            IServiceProvider serviceProvider)
        {
            _systemContext = systemContext;
            _logger = logger;
            _options = options;
            _scopeManager = scopeManager;
            _serviceProvider = serviceProvider;

            _serviceId = Guid.NewGuid().ToString();
        }

        private static string GetServiceName() => SERVICE_TYPE;

        private TimeSpan GetPollInternal() => TimeSpan.FromSeconds(_options.Value.PollIntervalSeconds);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var systemScope = _scopeManager.BeginSystemScope(_systemContext);
            using var serviceScope = _scopeManager.BeginServiceScope(GetServiceName(), _serviceId);

            var startupDimensions = new Dimensions()
            {
                [StandardDimensions.EVENT_TYPE] = EventType.Startup.ToString()
            };
            _logger.LogEvent("Notification polling started", startupDimensions);

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
            _logger.LogEvent("Notification polling stopped", shutdownDimensions);
        }

    }
}
