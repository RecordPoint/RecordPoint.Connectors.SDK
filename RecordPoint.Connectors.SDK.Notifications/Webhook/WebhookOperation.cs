using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.Notifications.Webhook
{
    /// <summary>
    /// The webhook operation.
    /// </summary>
    public class WebhookOperation : WorkBase<object>
    {
        /// <summary>
        /// The WEBHOOK WORK TYPE.
        /// </summary>
        private const string WEBHOOK_WORK_TYPE = "Webhook Notifications";

        /// <summary>
        /// The notification manager.
        /// </summary>
        private readonly INotificationManager _notificationManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebhookOperation"/> class.
        /// </summary>
        /// <param name="notificationManager">The notification manager.</param>
        /// <param name="observabilityScope">The scope manager.</param>
        /// <param name="telemetryTracker">The telemetry tracker.</param>
        /// <param name="dateTimeProvider">The date time provider.</param>
        public WebhookOperation(
            INotificationManager notificationManager,
            IObservabilityScope observabilityScope,
            ITelemetryTracker telemetryTracker,
            IDateTimeProvider dateTimeProvider)
            : base(observabilityScope, telemetryTracker, dateTimeProvider)
        {
            _notificationManager = notificationManager;
        }

        /// <summary>
        /// Gets the work type.
        /// </summary>
        public override string WorkType => WEBHOOK_WORK_TYPE;

        /// <summary>
        /// Gets the connector notification.
        /// </summary>
        public ConnectorNotificationModel ConnectorNotification => (ConnectorNotificationModel)Parameter;

        /// <summary>
        /// Inner the run asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task</returns>
        protected override async Task InnerRunAsync(CancellationToken cancellationToken)
        {
            await _notificationManager.HandleNotificationAsync(ConnectorNotification, cancellationToken);

            await CompleteAsync("Processing complete", cancellationToken);
        }

        /// <summary>
        /// Get custom key dimensions.
        /// </summary>
        /// <returns>A Dimensions</returns>
        protected override Dimensions GetCustomKeyDimensions()
        {
            var dimensions = base.GetCustomKeyDimensions();
            dimensions.Add("ConnectorNotificationId", ConnectorNotification.Id);
            dimensions.Add("NotificationType", ConnectorNotification.NotificationType);
            dimensions.Add("TenantId", ConnectorNotification.TenantId);
            dimensions.Add("ConnectorId", ConnectorNotification.ConnectorId);
            if (ConnectorNotification.ConnectorConfig != null)
            {
                dimensions.Add("TenantDomain", ConnectorNotification.ConnectorConfig.TenantDomainName);
                dimensions.Add("ConnectorTypeId", ConnectorNotification.ConnectorConfig.ConnectorTypeId);
            }
            return dimensions;
        }
    }
}
