using Microsoft.Rest;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Client
{
    /// <summary>
    /// It is being used by AutoRest to handle Credentials, So this type of Credentials means there is actually no authentication set
    /// on ApiClient level (it's being set at request level).
    /// <see cref="ApiClientFactory"/>
    /// </summary>
    public class NotSpecifiedCredentials : ServiceClientCredentials
    {
        public override async Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }
            // No authorization header is set, because it's being done per request
            await base.ProcessHttpRequestAsync(request, cancellationToken);
        }
    }
}
