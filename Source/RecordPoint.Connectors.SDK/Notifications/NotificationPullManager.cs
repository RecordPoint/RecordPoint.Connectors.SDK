using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Notifications
{
    public class NotificationPullManager : INotificationPullManager
    {
        public IApiClientFactory ApiClientFactory { get; set; }

        public AuthenticationHelperSettings AuthenticationHelperSettings { get; set; }

        public NotificationPullManager()
        {
        }

        /// <summary>
        /// Queries for all connector notifications for a given connector instance that are pending acknowledgement.
        /// </summary>
        /// <param name="factorySettings"></param>
        /// <param name="authenticationSettings"></param>
        /// <param name="connectorConfigId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IList<ConnectorNotificationModel>> GetAllPendingConnectorNotifications(ApiClientFactorySettings factorySettings, 
            AuthenticationHelperSettings authenticationSettings,
            string connectorConfigId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            // Get Notifications
            var client = ApiClientFactory.CreateApiClient(factorySettings);

            var policy = ApiClientRetryPolicy.GetPolicy(4, 2000, cancellationToken);

            var notificationQueryResponse = await policy.ExecuteAsync(
                async () =>
                {
                    var authHelper = ApiClientFactory.CreateAuthenticationHelper();
                    var headers = await authHelper.GetHttpRequestHeaders(AuthenticationHelperSettings).ConfigureAwait(false);
                    var response = await client.ApiNotificationsByConnectorIdGetWithHttpMessagesAsync(connectorConfigId, customHeaders: headers, cancellationToken: cancellationToken).ConfigureAwait(false);
                    return response;
                }
            ).ConfigureAwait(false);

            // TODO: add more comprehensive handling of the response codes

            var notifications = notificationQueryResponse.Body;

            return notifications;
        }

        /// <summary>
        /// Acknowledges a notification as having been processed.
        /// </summary>
        /// <param name="factorySettings"></param>
        /// <param name="authenticationSettings"></param>
        /// <param name="acknowledgement"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task AcknowledgeNotification(ApiClientFactorySettings factorySettings,
            AuthenticationHelperSettings authenticationSettings,
            ConnectorNotificationAcknowledgeModel acknowledgement,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var client = ApiClientFactory.CreateApiClient(factorySettings);

            var policy = ApiClientRetryPolicy.GetPolicy(4, 2000, cancellationToken);

            var acknowledgementResponse = await policy.ExecuteAsync(
                async () =>
                {
                    var authHelper = ApiClientFactory.CreateAuthenticationHelper();
                    var headers = await authHelper.GetHttpRequestHeaders(AuthenticationHelperSettings).ConfigureAwait(false);
                    var response = await client.ApiNotificationsPostWithHttpMessagesAsync(acknowledgement, customHeaders: headers, cancellationToken: cancellationToken).ConfigureAwait(false);
                    return response;
                }
            ).ConfigureAwait(false);

            // TODO: add more comprehensive handling of the response codes
        }
    }
}
