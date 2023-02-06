using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;

namespace RecordPoint.Connectors.SDK.Notifications.Handlers
{
    public class PingHandler : NotificationHandler
    {
        public const string PING_NOTIFICATION_TYPE = "Ping";

        public PingHandler(IConnectorConfigurationManager connectorClient)
        {
        }

        public override string NotificationType => PING_NOTIFICATION_TYPE;

        public override Task<NotificationOutcome> HandleNotificationAsync(
            ConnectorNotificationModel notification, CancellationToken cancellationToken)
        {
            return Task.FromResult(NotificationOutcome.OK());
        }
    }
}