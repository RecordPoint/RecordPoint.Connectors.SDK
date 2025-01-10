using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.Notifications
{
    /// <summary>
    /// 
    /// </summary>
    public interface INotificationStrategy
    {
        /// <summary>
        /// 
        /// </summary>
        string NotificationType { get; }

        /// <summary>
        /// Handle a notification
        /// </summary>
        /// <param name="notification">Notification</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task</returns>
        public Task<NotificationOutcome> HandleNotificationAsync(ConnectorNotificationModel notification, CancellationToken cancellationToken);
    }
}
