using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.Notifications
{
    /// <summary>
    /// 
    /// </summary>
    public interface INotificationManager
    {

        /// <summary>
        /// Handle notification 
        /// </summary>
        /// <param name="notification">Notification to handle</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task</returns>
        public Task HandleNotificationAsync(ConnectorNotificationModel notification, CancellationToken cancellationToken);

    }
}
