using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;

namespace RecordPoint.Connectors.SDK.Notifications.Handlers
{
    public class ConnectorConfigDeletedHandler : NotificationHandler
    {
        public const string CONNECTOR_CONFIG_DELETED_NOTIFICATION_TYPE = "ConnectorConfigDeleted";

        private readonly IConnectorConfigurationManager _connectorManager;

        public ConnectorConfigDeletedHandler(IConnectorConfigurationManager connectorManager)
        {
            _connectorManager = connectorManager;
        }

        public override string NotificationType => CONNECTOR_CONFIG_DELETED_NOTIFICATION_TYPE;

        public override Task<NotificationOutcome> HandleNotificationAsync(ConnectorNotificationModel notification, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(notification.ConnectorId))
            {
                throw new RequiredValueNullException(nameof(ConnectorNotificationModel.ConnectorId));
            }

            return InnerHandleNotificationAsync(notification.ConnectorId, cancellationToken);
        }

        private async Task<NotificationOutcome> InnerHandleNotificationAsync(string connectorId, CancellationToken cancellationToken)
        {
            await _connectorManager.DeleteConnectorConfigurationAsync(connectorId, cancellationToken);
            return NotificationOutcome.OK();
        }
    }
}