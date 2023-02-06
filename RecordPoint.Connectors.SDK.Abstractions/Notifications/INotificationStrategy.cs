using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.Notifications
{
    public interface INotificationStrategy
    {
        string NotificationType { get; }

        /// <summary>
        /// Handle a notification
        /// </summary>
        /// <param name="notification">Notification</param>
        /// <returns>Task</returns>
        public Task<NotificationOutcome> HandleNotificationAsync(ConnectorNotificationModel notification, CancellationToken cancellationToken);
    }
}
