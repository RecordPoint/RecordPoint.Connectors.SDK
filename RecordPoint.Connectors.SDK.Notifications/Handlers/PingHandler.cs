using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;

namespace RecordPoint.Connectors.SDK.Notifications.Handlers
{
    /// <summary>
    /// The ping handler.
    /// </summary>
    public class PingHandler : NotificationHandler
    {
        /// <summary>
        /// The PING NOTIFICATION TYPE.
        /// </summary>
        public const string PING_NOTIFICATION_TYPE = "Ping";

        /// <summary>
        /// Initializes a new instance of the <see cref="PingHandler"/> class.
        /// </summary>
        /// <param name="connectorClient">The connector client.</param>
        public PingHandler(IConnectorConfigurationManager connectorClient)
        {
        }

        /// <summary>
        /// Gets the notification type.
        /// </summary>
        public override string NotificationType => PING_NOTIFICATION_TYPE;

        /// <summary>
        /// Handle the notification asynchronously.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><![CDATA[Task<NotificationOutcome>]]></returns>
        public override Task<NotificationOutcome> HandleNotificationAsync(
            ConnectorNotificationModel notification, CancellationToken cancellationToken)
        {
            return Task.FromResult(NotificationOutcome.OK());
        }
    }
}