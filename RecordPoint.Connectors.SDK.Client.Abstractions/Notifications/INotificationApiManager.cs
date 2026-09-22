using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.Notifications
{
    /// <summary>
    /// Calls the RecordPoint Notification API.
    /// </summary>
    public interface INotificationApiManager
    {
        /// <summary>
        /// Queries for all connector notifications for a given connector instance that are pending acknowledgement.
        /// </summary>
        /// <param name="factorySettings"></param>
        /// <param name="authenticationSettings"></param>
        /// <param name="connectorConfigId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IList<ConnectorNotificationModel>> GetAllPendingConnectorNotifications(ApiClientFactorySettings factorySettings,
            AuthenticationHelperSettings authenticationSettings,
            string connectorConfigId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Queries for all connector notifications for a given connector type instance that are pending acknowledgement.
        /// </summary>
        /// <param name="factorySettings"></param>
        /// <param name="authenticationSettings"></param>
        /// <param name="connectorTypeId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IList<ConnectorNotificationModel>> GetAllPendingConnectorTypeNotifications(ApiClientFactorySettings factorySettings,
            AuthenticationHelperSettings authenticationSettings,
            Guid connectorTypeId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Acknowledges a notification as having been processed.
        /// </summary>
        /// <param name="factorySettings"></param>
        /// <param name="authenticationSettings"></param>
        /// <param name="acknowledgement"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AcknowledgeNotification(ApiClientFactorySettings factorySettings,
            AuthenticationHelperSettings authenticationSettings,
            ConnectorNotificationAcknowledgeModel acknowledgement,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Notifies RecordPoint of the result of a disposal attempt.
        /// </summary>
        /// <param name="factorySettings"></param>
        /// <param name="authenticationSettings"></param>
        /// <param name="callbackNotification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DisposalCallback(ApiClientFactorySettings factorySettings,
            AuthenticationHelperSettings authenticationSettings,
            ItemNotificationDisposalCallbackModel callbackNotification,
            CancellationToken cancellationToken = default);
    }
}
