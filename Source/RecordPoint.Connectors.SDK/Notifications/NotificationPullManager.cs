using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Exceptions;
using RecordPoint.Connectors.SDK.Helpers;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Notifications
{
    /// <summary>
    /// Manages pulling and acknowledging of connector notification messages.
    /// Note this class only applies to connector types that use the "pull" notification method.
    /// </summary>
    public class NotificationPullManager : INotificationPullManager
    {
        private IApiClientFactory _apiClientFactory;
        
        /// <summary>
        /// Creates a new NotificationPullManager.
        /// </summary>
        public NotificationPullManager()
        {
            _apiClientFactory = new ApiClientFactory();
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
            var client = _apiClientFactory.CreateApiClient(factorySettings);

            var policy = ApiClientRetryPolicy.GetPolicy(4, 2000, cancellationToken);

            var notificationQueryResponse = await policy.ExecuteAsync(
                async () =>
                {
                    var authHelper = _apiClientFactory.CreateAuthenticationHelper();
                    var headers = await authHelper.GetHttpRequestHeaders(authenticationSettings).ConfigureAwait(false);
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
            var client = _apiClientFactory.CreateApiClient(factorySettings);

            var policy = ApiClientRetryPolicy.GetPolicy(4, 2000, cancellationToken);

            var acknowledgementResponse = await policy.ExecuteAsync(
                async () =>
                {
                    var authHelper = _apiClientFactory.CreateAuthenticationHelper();
                    var headers = await authHelper.GetHttpRequestHeaders(authenticationSettings).ConfigureAwait(false);
                    var response = await client.ApiNotificationsPostWithHttpMessagesAsync(acknowledgement, customHeaders: headers, cancellationToken: cancellationToken).ConfigureAwait(false);
                    return response;
                }
            ).ConfigureAwait(false);

            if (acknowledgementResponse.Response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ResourceNotFoundException($"Notification with ID [{acknowledgement.NotificationId}] was not found. It may have already been acknowledged.");
            }

            // TODO: add more comprehensive handling of the response codes
        }
    }
}
