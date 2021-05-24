using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.ConnectorConfiguration
{
    /// <summary>
    /// Gets a connector configuration by ID from the Records365 Connector API.
    /// </summary>
    public class ConnectorConfigAccessor
    {
        /// <summary>
        /// The ApiClientFactory to use to call the Records365 Connector API.
        /// </summary>
        public IApiClientFactory ApiClientFactory { get; set; }

        /// <summary>
        /// Settings used to call into the Records365 Connector API.
        /// </summary>
        public ApiClientFactorySettings ApiClientFactorySettings { get; set; }

        /// <summary>
        /// The configuration that ConnectorConfigAccessor uses authenticate to the Records365 Connector API.
        /// </summary>
        public AuthenticationHelperSettings AuthenticationHelperSettings { get; set; }

        /// <summary>
        /// Gets the ConnectorConfigModel from the Records365 Connector API.
        /// </summary>
        /// <param name="connectorConfigId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ConnectorConfigModel> GetConnectorConfig(Guid connectorConfigId, CancellationToken cancellationToken)
        {
            var client = ApiClientFactory.CreateApiClient(ApiClientFactorySettings);
            var policy = ApiClientRetryPolicy.GetPolicy(4, cancellationToken);

            var connectorConfig = await policy.ExecuteAsync(
                async (ct) =>
                {
                    var authHelper = ApiClientFactory.CreateAuthenticationHelper();
                    var headers = await authHelper.GetHttpRequestHeaders(AuthenticationHelperSettings).ConfigureAwait(false);
                    var response = await client.ApiConnectorConfigurationsByIdGetWithHttpMessagesAsync(
                        connectorConfigId,
                        customHeaders: headers,
                        cancellationToken: ct
                    ).ConfigureAwait(false);

                    return response.Body;
                },
                cancellationToken
            ).ConfigureAwait(false);

            return connectorConfig;
        }
    }
}
