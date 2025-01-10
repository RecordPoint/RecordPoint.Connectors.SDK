using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.Notifications
{

    /// <summary>
    /// Records 365 notification
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// Gets or sets the notification type.
        /// </summary>
        public string? NotificationType { get; set; }
        /// <summary>
        /// Gets the value.
        /// </summary>
        public ConnectorNotificationModel Value { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Notification"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Notification(ConnectorNotificationModel value)
        {
            NotificationType = value.NotificationType;
            Value = value;
        }

    }
}
