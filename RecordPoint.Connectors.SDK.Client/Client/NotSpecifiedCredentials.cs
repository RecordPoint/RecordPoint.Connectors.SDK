using Microsoft.Rest;

namespace RecordPoint.Connectors.SDK.Client
{
    /// <summary>
    /// It is being used by AutoRest to handle Credentials, So this type of Credentials means there is actually no authentication set
    /// on ApiClient level (it's being set at request level).
    /// <see cref="ApiClientFactory"/>
    /// </summary>
    public class NotSpecifiedCredentials : ServiceClientCredentials
    {
        public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            // No authorization header is set, because it's being done per request
            return InnerProcessHttpRequestAsync(request, cancellationToken);
        }

        private async Task InnerProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // No authorization header is set, because it's being done per request
            await base.ProcessHttpRequestAsync(request, cancellationToken);
        }
    }
}
