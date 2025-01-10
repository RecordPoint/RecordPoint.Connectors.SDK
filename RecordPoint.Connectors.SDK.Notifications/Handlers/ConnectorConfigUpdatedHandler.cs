using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;

namespace RecordPoint.Connectors.SDK.Notifications.Handlers
{
    /// <summary>
    /// The connector config updated handler.
    /// </summary>
    public class ConnectorConfigUpdatedHandler : NotificationHandler
    {
        /// <summary>
        /// The CONNECTOR CONFIG UPDATED NOTIFICATION TYPE.
        /// </summary>
        public const string CONNECTOR_CONFIG_UPDATED_NOTIFICATION_TYPE = "ConnectorConfigUpdated";

        /// <summary>
        /// The connector manager.
        /// </summary>
        private readonly IConnectorConfigurationManager _connectorManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectorConfigUpdatedHandler"/> class.
        /// </summary>
        /// <param name="connectorManager">The connector manager.</param>
        public ConnectorConfigUpdatedHandler(IConnectorConfigurationManager connectorManager)
        {
            _connectorManager = connectorManager;
        }

        /// <summary>
        /// Gets the notification type.
        /// </summary>
        public override string NotificationType => CONNECTOR_CONFIG_UPDATED_NOTIFICATION_TYPE;

        /// <summary>
        /// Handle the notification asynchronously.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><![CDATA[Task<NotificationOutcome>]]></returns>
        public override async Task<NotificationOutcome> HandleNotificationAsync(ConnectorNotificationModel notification, CancellationToken cancellationToken)
        {
            await _connectorManager.SetConnectorAsync(notification.ConnectorConfig, cancellationToken);
            return NotificationOutcome.OK();
        }
    }
}