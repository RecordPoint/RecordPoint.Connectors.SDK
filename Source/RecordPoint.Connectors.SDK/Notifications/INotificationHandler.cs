using RecordPoint.Connectors.SDK.Client.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Notifications
{
    /// <summary>
    /// Provide an implementation of this interface to NotificationTask to implement
    /// connector-specific details of notification handling.
    /// </summary>
    public interface INotificationHandler
    {
        /// <summary>
        /// Provides the implementation for handling of a notification.
        /// If this method runs to completion, NotificationTask will acknowledge the notification with 
        /// ProcessingResult.OK.
        /// If any exception (other than TaskCanceledException) is thrown from this method,
        /// NotificationTask will acknowledge the notification with ProcessingResult.NotificationError.
        /// </summary>
        /// <param name="connectorConfigModel"></param>
        /// <param name="notification"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task HandleNotification(ConnectorConfigModel connectorConfigModel, ConnectorNotificationModel notification, CancellationToken ct);
    }
}
