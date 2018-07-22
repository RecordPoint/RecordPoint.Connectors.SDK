﻿using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.ConnectorConfiguration
{
    /// <summary>
    /// Gets access to the Records365 vNext Connector API, retrieves a ConnectorConfigModel with a ConnectorId
    /// </summary>
    public class ConnectorConfigAccessor
    {
        public IApiClientFactory ApiClientFactory { get; set; }

        /// <summary>
        /// Settings used to call into the Records365 vNext Connector API.
        /// </summary>
        public ApiClientFactorySettings ApiClientFactorySettings { get; set; }
        /// <summary>
        /// The configuration that ConnectorConfigAccessor uses authenticate to the Records365 vNext Connector API
        /// </summary>
        public AuthenticationHelperSettings AuthenticationHelperSettings { get; set; }

        /// <summary>
        /// Gets the ConnectorConfigModel from the Records365 vNext Connector API
        /// </summary>
        /// <param name="connectorConfigId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ConnectorConfigModel> GetConnectorConfig(Guid connectorConfigId, CancellationToken cancellationToken)
        {
            var client = ApiClientFactory.CreateApiClient(ApiClientFactorySettings);
            var policy = ApiClientRetryPolicy.GetPolicy(4, 2000, cancellationToken);

            var connectorConfig = await policy.ExecuteAsync(
                async () =>
                {
                    var authHelper = ApiClientFactory.CreateAuthenticationHelper();
                    var headers = await authHelper.GetHttpRequestHeaders(AuthenticationHelperSettings).ConfigureAwait(false);
                    var response = await client.ApiConnectorConfigurationsByIdGetWithHttpMessagesAsync(
                        connectorConfigId,
                        customHeaders: headers,
                        cancellationToken: cancellationToken
                    ).ConfigureAwait(false);

                    return response.Body;
                }
            ).ConfigureAwait(false);

            return connectorConfig;
        }
    }
}
