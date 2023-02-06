using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;

namespace RecordPoint.Connectors.SDK.Notifications.Handlers
{
    public class ConnectorConfigCreatedHandler : NotificationHandler
    {
        public const string CONNECTOR_CONFIG_CREATED_NOTIFICATION_TYPE = "ConnectorConfigCreated";

        private readonly IConnectorConfigurationManager _connectorManager;

        public ConnectorConfigCreatedHandler(IConnectorConfigurationManager connectorManager)
        {
            _connectorManager = connectorManager;
        }

        public override string NotificationType => CONNECTOR_CONFIG_CREATED_NOTIFICATION_TYPE;

        public override async Task<NotificationOutcome> HandleNotificationAsync(ConnectorNotificationModel notification, CancellationToken cancellationToken)
        {
            await _connectorManager.SetConnectorAsync(notification.ConnectorConfig, cancellationToken);
            return NotificationOutcome.OK();
        }
    }
}