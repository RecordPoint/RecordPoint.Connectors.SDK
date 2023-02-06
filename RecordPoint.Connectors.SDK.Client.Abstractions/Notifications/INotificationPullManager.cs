using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.Notifications
{
    public interface INotificationPullManager
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
            CancellationToken cancellationToken = default(CancellationToken));

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
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
