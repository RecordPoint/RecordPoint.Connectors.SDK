using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;

namespace RecordPoint.Connectors.SDK.Notifications.Handlers
{
    /// <summary>
    /// The connector config created handler.
    /// </summary>
    public class ConnectorConfigCreatedHandler : NotificationHandler
    {
        /// <summary>
        /// The CONNECTOR CONFIG CREATED NOTIFICATION TYPE.
        /// </summary>
        public const string CONNECTOR_CONFIG_CREATED_NOTIFICATION_TYPE = "ConnectorConfigCreated";

        /// <summary>
        /// The connector manager.
        /// </summary>
        private readonly IConnectorConfigurationManager _connectorManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectorConfigCreatedHandler"/> class.
        /// </summary>
        /// <param name="connectorManager">The connector manager.</param>
        public ConnectorConfigCreatedHandler(IConnectorConfigurationManager connectorManager)
        {
            _connectorManager = connectorManager;
        }

        /// <summary>
        /// Gets the notification type.
        /// </summary>
        public override string NotificationType => CONNECTOR_CONFIG_CREATED_NOTIFICATION_TYPE;

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