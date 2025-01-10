using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.Notifications
{
    /// <summary>
    /// Base class for notification handlers
    /// </summary>
    /// <remarks>
    /// Provides logging, metrics and error handling for notification handlers
    /// </remarks>
    public abstract class NotificationHandler : INotificationStrategy
    {
        /// <summary>
        /// 
        /// </summary>
        public abstract string NotificationType { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public abstract Task<NotificationOutcome> HandleNotificationAsync(ConnectorNotificationModel notification, CancellationToken cancellationToken);
    }
}
