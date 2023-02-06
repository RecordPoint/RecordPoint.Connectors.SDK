using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;

namespace RecordPoint.Connectors.SDK.Notifications.Handlers
{
    public class ConnectorConfigUpdatedHandler : NotificationHandler
    {
        public const string CONNECTOR_CONFIG_UPDATED_NOTIFICATION_TYPE = "ConnectorConfigUpdated";

        private readonly IConnectorConfigurationManager _connectorManager;

        public ConnectorConfigUpdatedHandler(IConnectorConfigurationManager connectorManager)
        {
            _connectorManager = connectorManager;
        }

        public override string NotificationType => CONNECTOR_CONFIG_UPDATED_NOTIFICATION_TYPE;

        public override async Task<NotificationOutcome> HandleNotificationAsync(ConnectorNotificationModel notification, CancellationToken cancellationToken)
        {
            await _connectorManager.SetConnectorAsync(notification.ConnectorConfig, cancellationToken);
            return NotificationOutcome.OK();
        }
    }
}