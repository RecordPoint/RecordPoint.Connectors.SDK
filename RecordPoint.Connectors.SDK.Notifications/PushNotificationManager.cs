using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Observability;

namespace RecordPoint.Connectors.SDK.Notifications
{
    /// <summary>
    /// Notifications manager that switches on the WorkType and kicks off the correct work item
    /// </summary>
    public class PushNotificationManager : NotificationManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notificationStrategies"></param>
        /// <param name="observabilityScope"></param>
        /// <param name="telemetryTracker"></param>
        public PushNotificationManager(
            IEnumerable<INotificationStrategy> notificationStrategies,
            IObservabilityScope observabilityScope,
            ITelemetryTracker telemetryTracker)
            : base(notificationStrategies, observabilityScope, telemetryTracker)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task HandleNotificationAsync(ConnectorNotificationModel notification, CancellationToken cancellationToken)
        {
            using var notificationScope = _observabilityScope.BeginScope(GetDimensions(notification));

            var notificationType = notification.NotificationType;
            if (!_notificationStrategies.ContainsKey(notificationType))
            {
                var errorReason = $"Received unknown notification type {notificationType}";
                _telemetryTracker.TrackTrace(errorReason, SeverityLevel.Warning);
                return;
            }
            var strategy = _notificationStrategies[notificationType];
            try
            {
                var outcome = await strategy.HandleNotificationAsync(notification, cancellationToken);
                switch (outcome.OutcomeType)
                {
                    case NotificationOutcomeType.Ok:
                        _telemetryTracker.TrackTrace("Notification Processed OK", SeverityLevel.Information, GetOutcomeDimensions(outcome));
                        break;

                    case NotificationOutcomeType.Failed:
                        _telemetryTracker.TrackTrace("Notification Processing Failed", SeverityLevel.Error, GetOutcomeDimensions(outcome));
                        break;
                }
            }
            catch (Exception ex)
            {
                _telemetryTracker.TrackException(ex);
            }
        }
    }
}
