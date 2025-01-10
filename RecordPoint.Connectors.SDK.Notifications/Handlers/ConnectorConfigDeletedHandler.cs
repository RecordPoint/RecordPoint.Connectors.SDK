using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;

namespace RecordPoint.Connectors.SDK.Notifications.Handlers
{
    /// <summary>
    /// The connector config deleted handler.
    /// </summary>
    public class ConnectorConfigDeletedHandler : NotificationHandler
    {
        /// <summary>
        /// The CONNECTOR CONFIG DELETED NOTIFICATION TYPE.
        /// </summary>
        public const string CONNECTOR_CONFIG_DELETED_NOTIFICATION_TYPE = "ConnectorConfigDeleted";

        /// <summary>
        /// The connector manager.
        /// </summary>
        private readonly IConnectorConfigurationManager _connectorManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectorConfigDeletedHandler"/> class.
        /// </summary>
        /// <param name="connectorManager">The connector manager.</param>
        public ConnectorConfigDeletedHandler(IConnectorConfigurationManager connectorManager)
        {
            _connectorManager = connectorManager;
        }

        /// <summary>
        /// Gets the notification type.
        /// </summary>
        public override string NotificationType => CONNECTOR_CONFIG_DELETED_NOTIFICATION_TYPE;

        /// <summary>
        /// Handle the notification asynchronously.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="RequiredValueNullException"></exception>
        /// <returns><![CDATA[Task<NotificationOutcome>]]></returns>
        public override Task<NotificationOutcome> HandleNotificationAsync(ConnectorNotificationModel notification, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(notification.ConnectorId))
            {
                throw new RequiredValueNullException(nameof(ConnectorNotificationModel.ConnectorId));
            }

            return InnerHandleNotificationAsync(notification.ConnectorId, cancellationToken);
        }

        /// <summary>
        /// Inner handle notification asynchronously.
        /// </summary>
        /// <param name="connectorId">The connector id.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><![CDATA[Task<NotificationOutcome>]]></returns>
        private async Task<NotificationOutcome> InnerHandleNotificationAsync(string connectorId, CancellationToken cancellationToken)
        {
            await _connectorManager.DeleteConnectorConfigurationAsync(connectorId, cancellationToken);
            return NotificationOutcome.OK();
        }
    }
}