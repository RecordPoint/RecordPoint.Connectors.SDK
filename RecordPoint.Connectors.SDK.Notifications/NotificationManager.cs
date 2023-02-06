using Microsoft.Extensions.Logging;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Observability;

namespace RecordPoint.Connectors.SDK.Notifications
{

    /// <summary>
    /// Notifications manager that switches on the WorkType and kicks off the correct work item
    /// </summary>
    public abstract class NotificationManager : INotificationManager
    {
        protected readonly ILogger<PullNotificationManager> _logger;
        protected readonly Dictionary<string, INotificationStrategy> _notificationStrategies;
        protected readonly IScopeManager _scopeManager;
        protected readonly ITelemetryTracker _telemetryTracker;

        protected NotificationManager(
            ILogger<PullNotificationManager> logger,
            IEnumerable<INotificationStrategy> notificationStrategies,
            IScopeManager scopeManager,
            ITelemetryTracker telemetryTracker)
        {
            _logger = logger;
            _notificationStrategies = notificationStrategies.ToDictionary(s => s.NotificationType);
            _scopeManager = scopeManager;
            _telemetryTracker = telemetryTracker;
        }

        public abstract Task HandleNotificationAsync(ConnectorNotificationModel notification, CancellationToken cancellationToken);


        /// <summary>
        /// Get observability dimensions for the notification
        /// </summary>
        /// <param name="notification">Notification to get key properties for</param>
        /// <returns>Key properties</returns>
        protected virtual Dimensions GetDimensions(ConnectorNotificationModel notification)
        {
            var dimensions = new Dimensions()
            {
                ["NotificationId"] = notification.Id,
                ["NotificationType"] = notification.NotificationType,
                ["TenantId"] = notification.TenantId,
                ["ConnectorConfigId"] = notification.ConnectorConfig?.Id,
            };
            return dimensions;
        }

        /// <summary>
        /// Get observability outcome dimensions
        /// </summary>
        /// <param name="notification">Notification to get key properties for</param>
        /// <returns>Key properties</returns>
        protected virtual Dimensions GetOutcomeDimensions(NotificationOutcome outcome)
        {
            var dimensions = new Dimensions()
            {
                [StandardDimensions.EVENT_TYPE] = EventType.Finish.ToString(),
                [StandardDimensions.OUTCOME] = outcome.OutcomeType.ToString(),
                [StandardDimensions.OUTCOME_REASON] = outcome.Reason
            };
            return dimensions;
        }

    }
}
