using Microsoft.Rest;
using RecordPoint.Connectors.SDK.Client.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Client
{
    public partial interface IApiClient
    {
        Task<HttpOperationResponse<ErrorResponseModel>> ApiBinariesPostWithHttpMessagesAndStreamAsync(string connectorId = default(string),
            string itemExternalId = default(string),
            string binaryExternalId = default(string),
            string fileName = default(string),
            string acceptLanguage = default(string),
            Dictionary<string, List<string>> customHeaders = null,
            Stream inputStream = null,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
