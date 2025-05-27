using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Observability;

namespace RecordPoint.Connectors.SDK.Notifications
{

    /// <summary>
    /// Notifications manager that switches on the WorkType and kicks off the correct work item
    /// </summary>
    public abstract class NotificationManager : INotificationManager
    {
      /// <summary>
        /// 
        /// </summary>
        protected readonly Dictionary<string, INotificationStrategy> _notificationStrategies;
        /// <summary>
        /// 
        /// </summary>
        protected readonly IObservabilityScope _observabilityScope;
        /// <summary>
        /// 
        /// </summary>
        protected readonly ITelemetryTracker _telemetryTracker;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notificationStrategies"></param>
        /// <param name="observabilityScope"></param>
        /// <param name="telemetryTracker"></param>
        protected NotificationManager(
            IEnumerable<INotificationStrategy> notificationStrategies,
            IObservabilityScope observabilityScope,
            ITelemetryTracker telemetryTracker)
        {
            _notificationStrategies = notificationStrategies.ToDictionary(s => s.NotificationType);
            _observabilityScope = observabilityScope;
            _telemetryTracker = telemetryTracker;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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
        /// <param name="outcome">Notification to get key properties for</param>
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
