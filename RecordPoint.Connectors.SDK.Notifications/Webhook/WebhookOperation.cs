using Microsoft.Extensions.Logging;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.Notifications.Webhook
{
    public class WebhookOperation : WorkBase<object>
    {
        private const string WEBHOOK_WORK_TYPE = "Webhook Notifications";

        private readonly INotificationManager _notificationManager;

        public WebhookOperation(
            INotificationManager notificationManager,
            IScopeManager scopeManager,
            ILogger<WebhookOperation> logger,
            ITelemetryTracker telemetryTracker,
            IDateTimeProvider dateTimeProvider)
            : base(scopeManager, logger, telemetryTracker, dateTimeProvider)
        {
            _notificationManager = notificationManager;
        }

        public override string WorkType => WEBHOOK_WORK_TYPE;

        public ConnectorNotificationModel ConnectorNotification => (ConnectorNotificationModel)Parameter;

        protected override async Task InnerRunAsync(CancellationToken cancellationToken)
        {
            await _notificationManager.HandleNotificationAsync(ConnectorNotification, cancellationToken);

            await CompleteAsync("Processing complete", cancellationToken);
        }

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
