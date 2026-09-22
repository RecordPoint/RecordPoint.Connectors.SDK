using Newtonsoft.Json;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Exceptions;
using RecordPoint.Connectors.SDK.Helpers;
using System;
using System.Net;

namespace RecordPoint.Connectors.SDK.Notifications
{
    /// <summary>
    /// Manages pulling and acknowledging of connector notification messages.
    /// Note this class mainly applies to connector types that use the "pull" notification method.
    /// </summary>
    public class NotificationApiManager : INotificationApiManager
    {
        private readonly IApiClientFactory _apiClientFactory;
        private const int MaxRetryAttempts = 4;

        /// <summary>
        /// Creates a new NotificationApiManager.
        /// </summary>
        public NotificationApiManager()
        {
            _apiClientFactory = new ApiClientFactory();
        }

        /// <summary>
        /// Creates a new NotificationApiManager with a supplied <see cref="IApiClientFactory"/>.
        /// Intended for testing so that the underlying API client can be substituted.
        /// </summary>
        /// <param name="apiClientFactory"></param>
        internal NotificationApiManager(IApiClientFactory apiClientFactory)
        {
            _apiClientFactory = apiClientFactory ?? throw new ArgumentNullException(nameof(apiClientFactory));
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

        ///<inheritdoc/>
        public async Task<IList<ConnectorNotificationModel>> GetAllPendingConnectorTypeNotifications(ApiClientFactorySettings factorySettings,
            AuthenticationHelperSettings authenticationSettings,
            Guid connectorTypeId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var client = _apiClientFactory.CreateApiClient(factorySettings);
            var authHelper = _apiClientFactory.CreateAuthenticationProvider(authenticationSettings);

            var policy = ApiClientRetryPolicy.GetPolicy(MaxRetryAttempts, cancellationToken);

            var notificationQueryResponse = await policy.ExecuteAsync(
                async (ct) =>
                {
                    var headers = await authHelper.GetHttpRequestHeaders(authenticationSettings).ConfigureAwait(false);
                    var response = await client.GET.ApiNotificationsConnectorTypesconnectorTypeIdNotificationsWithHttpMessagesAsync(
                        connectorTypeId,
                        customHeaders: headers,
                        cancellationToken: ct
                    ).ConfigureAwait(false);

                    return response;
                },
                cancellationToken
            ).ConfigureAwait(false);

            var notifications = FromAutoRestObject<ConnectorNotificationModel>(notificationQueryResponse.Body);

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

        /// <summary>
        /// Acknowledges a notification as having been processed.
        /// </summary>
        /// <param name="factorySettings"></param>
        /// <param name="authenticationSettings"></param>
        /// <param name="callbackNotification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task DisposalCallback(ApiClientFactorySettings factorySettings,
            AuthenticationHelperSettings authenticationSettings,
            ItemNotificationDisposalCallbackModel callbackNotification,
            CancellationToken cancellationToken = default)
        {
            var client = _apiClientFactory.CreateApiClient(factorySettings);
            var authHelper = _apiClientFactory.CreateAuthenticationProvider(authenticationSettings);
            var policy = ApiClientRetryPolicy.GetPolicy(MaxRetryAttempts, cancellationToken);

            await policy.ExecuteAsync(
                async () =>
                {
                    var headers = await authHelper.GetHttpRequestHeaders(authenticationSettings).ConfigureAwait(false);
                    await client.POST.ApiNotificationsDisposalCallbackWithHttpMessagesAsync(body: callbackNotification, customHeaders: headers, cancellationToken: cancellationToken).ConfigureAwait(false);
                }
            ).ConfigureAwait(false);
        }

        /// <summary>
        /// Convert the result from AutoRest into the expected type.
        /// </summary>
        private static IList<T> FromAutoRestObject<T>(object result)
        {
            if (result == null)
                return Array.Empty<T>();

            try
            {
                return JsonConvert.DeserializeObject<IList<T>>(
                    JsonConvert.SerializeObject(result));
            }
            catch (JsonException)
            {
                return Array.Empty<T>();
            }
        }
    }
}
