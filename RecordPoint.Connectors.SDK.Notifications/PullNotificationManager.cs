using Microsoft.Extensions.Logging;
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
        /// <param name="logger"></param>
        /// <param name="notificationStrategies"></param>
        /// <param name="scopeManager"></param>
        /// <param name="telemetryTracker"></param>
        public PullNotificationManager(
            IR365NotificationClient r365NotificationClient,
            ILogger<PullNotificationManager> logger,
            IEnumerable<INotificationStrategy> notificationStrategies,
            IScopeManager scopeManager,
            ITelemetryTracker telemetryTracker)
            : base(logger, notificationStrategies, scopeManager, telemetryTracker)
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
            var notificationType = notification.NotificationType;
            if (!_notificationStrategies.ContainsKey(notificationType))
            {
                var errorReason = $"Received unknown notification type {notificationType}";
                _logger.LogWarning(errorReason);
                await _r365NotificationClient.AcknowledgeNotificationAsync(notification, SDK.Client.ProcessingResult.NotificationError, errorReason, cancellationToken);
                return;
            }

            var strategy = _notificationStrategies[notificationType];
            using var notificationScope = _scopeManager.BeginScope(GetDimensions(notification));
            try
            {
                var outcome = await strategy.HandleNotificationAsync(notification, cancellationToken);
                switch (outcome.OutcomeType)
                {
                    case NotificationOutcomeType.Ok:
                        await _r365NotificationClient.AcknowledgeNotificationAsync(notification, SDK.Client.ProcessingResult.OK, outcome.Reason, cancellationToken);
                        _logger.LogEvent(notificationType, GetOutcomeDimensions(outcome));
                        break;

                    case NotificationOutcomeType.Failed:
                        await _r365NotificationClient.AcknowledgeNotificationAsync(notification, SDK.Client.ProcessingResult.NotificationError, outcome.Reason, cancellationToken);
                        _logger.LogEvent(notificationType, GetOutcomeDimensions(outcome));
                        break;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                await _r365NotificationClient.AcknowledgeNotificationAsync(notification, SDK.Client.ProcessingResult.NotificationError, message, cancellationToken);
                _telemetryTracker.TrackException(notificationType, ex);
                _logger.LogEventException(notificationType, ex);
            }
        }
    }
}
