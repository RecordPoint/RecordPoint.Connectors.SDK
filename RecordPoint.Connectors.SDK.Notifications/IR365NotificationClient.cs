using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.Client
{
    /// <summary>
    /// Provides access to R365's Notification APIs.
    /// </summary>
    public interface IR365NotificationClient
    {
        /// <summary>
        /// Is Records 365 Configured?
        /// </summary>
        /// <remarks>
        /// Note that the client can be "ready" (All its dependant services are ready) but the configuration not yet loaded.
        /// </remarks>
        /// <returns>True if records 365 has been configured and we should be able to invoke it, otherwise false</returns>
        bool IsConfigured();

        /// <summary>
        /// Get Pending Notifications. Only supported for On-prem version. Cloud versions should use a webhook.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns all the pull notifications for a configured connector types for a tenant</returns>
        public Task<List<ConnectorNotificationModel>> GetAllPendingNotifications( CancellationToken cancellationToken);

        /// <summary>
        /// Acknowledges a Notification from R365
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="result"></param>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task AcknowledgeNotificationAsync(ConnectorNotificationModel notification, ProcessingResult result, string message, CancellationToken cancellationToken);

    }
}
