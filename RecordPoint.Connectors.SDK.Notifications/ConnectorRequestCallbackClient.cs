using System.Net;
using System.Text.Json;
using Microsoft.Rest;
using Newtonsoft.Json.Linq;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Configuration;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Requests;
using RecordPoint.Connectors.SDK.Helpers;
using RecordPoint.Connectors.SDK.Secrets;

namespace RecordPoint.Connectors.SDK.Notifications
{
    /// <summary>
    /// Returns a connector's answer by posting it to the Connector API, authenticated as the connector.
    /// </summary>
    /// <remarks>Shaped like NotificationApiManager.DisposalCallback: the caller supplies auth and retry.</remarks>
    public class ConnectorRequestCallbackClient : IConnectorRequestCallbackClient
    {
        /// <summary>Attempts before an answer is given up on, matching the other API clients.</summary>
        private const int MaxRetryAttempts = 4;

        private readonly IApiClientFactory _apiClientFactory;
        private readonly IR365ConfigurationClient _configurationClient;
        private readonly IObservabilityScope _observabilityScope;

        /// <param name="apiClientFactory">Supplies the API client and the authentication provider.</param>
        /// <param name="configurationClient">Supplies the Connector API address and credentials.</param>
        /// <param name="observabilityScope">Wraps the call for telemetry, as the other clients do.</param>
        public ConnectorRequestCallbackClient(
            IApiClientFactory apiClientFactory,
            IR365ConfigurationClient configurationClient,
            IObservabilityScope observabilityScope)
        {
            _apiClientFactory = apiClientFactory ?? throw new ArgumentNullException(nameof(apiClientFactory));
            _configurationClient = configurationClient ?? throw new ArgumentNullException(nameof(configurationClient));
            _observabilityScope = observabilityScope;
        }

        /// <inheritdoc/>
        public async Task SendAsync(ConnectorConfigModel connectorConfig, ConnectorRequestResponse response, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(connectorConfig);
            ArgumentNullException.ThrowIfNull(response);

            var configuration = _configurationClient.GetR365Configuration(connectorConfig.ConnectorTypeConfigurationId)
                ?? throw new RequiredValueNullException(nameof(R365ConfigurationModel));

            // Mapped as R365NotificationClient does, so this authenticates like every other call.
            var authenticationSettings = new AuthenticationHelperSettings
            {
                ClientId = configuration.ClientId.ToString(),
                ClientSecret = EncryptionExtensions.GetSecureSecret(configuration.ClientSecret),
                AuthenticationResource = configuration.Audience,
                TenantDomainName = connectorConfig.TenantDomainName
            };

            // Set here, not by the handler: it addresses the answer and authorises the callback.
            if (!Guid.TryParse(connectorConfig.Id, out var connectorId))
            {
                throw new ArgumentException(
                    $"The connector configuration id '{connectorConfig.Id}' is not a GUID.", nameof(connectorConfig));
            }

            var body = new ConnectorRequestResponseCallbackModel
            {
                ConnectorId = connectorId,
                RequestType = response.RequestType,
                CorrelationId = response.CorrelationId,
                ResponseScope = response.ResponseScope,

                // A string, because the platform publishes the enum by name rather than by ordinal.
                Outcome = response.Outcome.ToString(),
                Messages = response.Messages,
                Data = ToNewtonsoftReadable(response.Data)
            };

            await Post(configuration, authenticationSettings, body, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Puts an answer's data into a form the generated client can serialise.</summary>
        /// <remarks>Newtonsoft renders a JsonElement as an empty object, so a relayed payload is re-read.</remarks>
        private static object? ToNewtonsoftReadable(object? data)
        {
            return data is JsonElement element ? JToken.Parse(element.GetRawText()) : data;
        }

        private async Task Post(
            R365ConfigurationModel configuration,
            AuthenticationHelperSettings authenticationSettings,
            ConnectorRequestResponseCallbackModel body,
            CancellationToken cancellationToken)
        {
            var client = _apiClientFactory.CreateApiClient(new ApiClientFactorySettings
            {
                ConnectorApiUrl = configuration.ConnectorApiUrl,
                ServerCertificateValidation = configuration.ServerCertificateValidation
            });

            var authenticationProvider = _apiClientFactory.CreateAuthenticationProvider(authenticationSettings);

            var send = async () =>
            {
                // Inside the retry, so a second attempt cannot use a token that expired during backoff.
                var headers = await authenticationProvider.GetHttpRequestHeaders(authenticationSettings).ConfigureAwait(false);

                using var response = await client.POST.ApiNotificationsConnectorRequestCallbackWithHttpMessagesAsync(
                    body: body, customHeaders: headers, cancellationToken: cancellationToken).ConfigureAwait(false);

                // The client throws only for undeclared statuses, and this route declares 400 and 413.
                var statusCode = response.Response.StatusCode;
                if (statusCode != HttpStatusCode.Accepted)
                {
                    var detail = response.Response.Content == null
                        ? string.Empty
                        : await response.Response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

                    // The type ApiClientRetryPolicy inspects. Neither 400 nor 413 is worth retrying.
                    throw new HttpOperationException(
                        $"Returning the answer to connector request '{body.RequestType}' failed with {(int)statusCode}: {detail}")
                    {
                        Response = new HttpResponseMessageWrapper(response.Response, detail)
                    };
                }
            };

            // The same backoff as every other Connector API call. Without it a restart loses the answer.
            var policy = ApiClientRetryPolicy.GetPolicy(MaxRetryAttempts, cancellationToken);
            var sendWithRetry = () => policy.ExecuteAsync(send);

            if (_observabilityScope == null)
            {
                await sendWithRetry().ConfigureAwait(false);
                return;
            }

            await _observabilityScope.InvokeAsync(
                new Dimensions
                {
                    [StandardDimensions.DEPENDANCY_TYPE] = DependancyType.Records365.ToString(),
                    ["RequestType"] = body.RequestType,
                    ["ConnectorConfigId"] = body.ConnectorId.ToString()
                },
                sendWithRetry).ConfigureAwait(false);
        }
    }
}
