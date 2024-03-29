﻿using Microsoft.Rest;
using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.Client
{
    /// <summary>
    /// Partial interface with custom extensions on the autogenerated IApiClient.
    /// </summary>
    public partial interface IApiClient
    {
        /// <summary>
        /// Post a binary to the Records365 Connector API with a stream.
        /// </summary>
        /// <param name="connectorId"></param>
        /// <param name="itemExternalId"></param>
        /// <param name="binaryExternalId"></param>
        /// <param name="fileName"></param>
        /// <param name="correlationId"></param>
        /// <param name="acceptLanguage"></param>
        /// <param name="customHeaders"></param>
        /// <param name="inputStream"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<HttpOperationResponse<ErrorResponseModel>> ApiBinariesPostWithHttpMessagesAndStreamAsync(
            string connectorId = default,
            string itemExternalId = default,
            string binaryExternalId = default,
            string fileName = default,
            string correlationId = default,
            string acceptLanguage = default,
            Dictionary<string, List<string>> customHeaders = null,
            Stream inputStream = null,
            CancellationToken cancellationToken = default);
    }
}
