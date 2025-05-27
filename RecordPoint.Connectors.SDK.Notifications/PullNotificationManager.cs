using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Observability;

namespace RecordPoint.Connectors.SDK.Notifications
{
    /// <summary>
    /// Notifications manager that switches on the WorkType and kicks off the correct work item
    /// </summary>
    public class PullNotificationManager : NotificationManager
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly IR365NotificationClient _r365NotificationClient;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r365NotificationClient"></param>
        /// <param name="notificationStrategies"></param>
        /// <param name="observabilityScope"></param>
        /// <param name="telemetryTracker"></param>
        public PullNotificationManager(
            IR365NotificationClient r365NotificationClient,
            IEnumerable<INotificationStrategy> notificationStrategies,
            IObservabilityScope observabilityScope,
            ITelemetryTracker telemetryTracker)
            : base(notificationStrategies, observabilityScope, telemetryTracker)
        {
            _r365NotificationClient = r365NotificationClient;
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
                await _r365NotificationClient.AcknowledgeNotificationAsync(notification, SDK.Client.ProcessingResult.NotificationError, errorReason, cancellationToken);
                return;
            }

            var strategy = _notificationStrategies[notificationType];
            try
            {
                var outcome = await strategy.HandleNotificationAsync(notification, cancellationToken);
                switch (outcome.OutcomeType)
                {
                    case NotificationOutcomeType.Ok:
                        await _r365NotificationClient.AcknowledgeNotificationAsync(notification, SDK.Client.ProcessingResult.OK, outcome.Reason, cancellationToken);
                        _telemetryTracker.TrackTrace("Notification Processed OK", SeverityLevel.Information, GetOutcomeDimensions(outcome));
                        break;

                    case NotificationOutcomeType.Failed:
                        await _r365NotificationClient.AcknowledgeNotificationAsync(notification, SDK.Client.ProcessingResult.NotificationError, outcome.Reason, cancellationToken);
                        _telemetryTracker.TrackTrace("Notification Processing Failed", SeverityLevel.Error, GetOutcomeDimensions(outcome));
                        break;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                await _r365NotificationClient.AcknowledgeNotificationAsync(notification, SDK.Client.ProcessingResult.NotificationError, message, cancellationToken);
                _telemetryTracker.TrackException(ex);
            }
        }
    }
}
