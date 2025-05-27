using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Exceptions;
using RecordPoint.Connectors.SDK.Helpers;
using System.Net;

namespace RecordPoint.Connectors.SDK.Notifications
{
    /// <summary>
    /// Manages pulling and acknowledging of connector notification messages.
    /// Note this class only applies to connector types that use the "pull" notification method.
    /// </summary>
    public class NotificationPullManager : INotificationPullManager
    {
        private readonly IApiClientFactory _apiClientFactory;
        private const int MaxRetryAttempts = 4;

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
            CancellationToken cancellationToken = default)
        {
            // Get Notifications
            var client = _apiClientFactory.CreateApiClient(factorySettings);
            var authHelper = _apiClientFactory.CreateAuthenticationProvider(authenticationSettings);

            var policy = ApiClientRetryPolicy.GetPolicy(MaxRetryAttempts, cancellationToken);

            var notificationQueryResponse = await policy.ExecuteAsync(
                async (ct) =>
                {
                    var headers = await authHelper.GetHttpRequestHeaders(authenticationSettings).ConfigureAwait(false);
                    var response = await client.GET.ApiNotificationsWithHttpMessagesAsync(
                        connectorConfigId,
                        customHeaders: headers,
                        cancellationToken: ct
                    ).ConfigureAwait(false);

                    return response;
                },
                cancellationToken
            ).ConfigureAwait(false);

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
            CancellationToken cancellationToken = default)
        {
            var client = _apiClientFactory.CreateApiClient(factorySettings);
            var authHelper = _apiClientFactory.CreateAuthenticationProvider(authenticationSettings);

            var policy = ApiClientRetryPolicy.GetPolicy(MaxRetryAttempts, cancellationToken);

            var acknowledgementResponse = await policy.ExecuteAsync(
                async () =>
                {
                    var headers = await authHelper.GetHttpRequestHeaders(authenticationSettings).ConfigureAwait(false);
                    var response = await client.POST.ApiNotificationsWithHttpMessagesAsync(body: acknowledgement, customHeaders: headers, cancellationToken: cancellationToken).ConfigureAwait(false);
                    return response;
                }
            ).ConfigureAwait(false);

            if (acknowledgementResponse.Response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ResourceNotFoundException($"Notification with ID [{acknowledgement.NotificationId}] was not found. It may have already been acknowledged.");
            }
        }
    }
}
