using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.Notifications
{

    /// <summary>
    /// Records 365 notification
    /// </summary>
    public class Notification
    {
        public string? NotificationType { get; set; }
        public ConnectorNotificationModel Value { get; private set; }

        public Notification(ConnectorNotificationModel value)
        {
            NotificationType = value.NotificationType;
            Value = value;
        }

    }
}
