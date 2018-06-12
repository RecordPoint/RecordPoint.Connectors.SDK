using RecordPoint.Connectors.SDK.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Helpers
{
    public static class HTTPRequestHelper
    {
        public static readonly List<WebExceptionStatus> KnownRetriableWebExceptionStatuses = new List<WebExceptionStatus>()
        {
            WebExceptionStatus.ConnectFailure,
            WebExceptionStatus.ConnectionClosed,
            WebExceptionStatus.KeepAliveFailure,
            WebExceptionStatus.NameResolutionFailure,
            WebExceptionStatus.Pending,
            WebExceptionStatus.PipelineFailure,
            WebExceptionStatus.ProxyNameResolutionFailure,
            WebExceptionStatus.ReceiveFailure,
            WebExceptionStatus.SendFailure,
            WebExceptionStatus.Timeout,
            WebExceptionStatus.UnknownError
        };

        public static bool IsRetriableWebRequestException(this Exception ex)
        {
            if (ex is WebException wex)
            {
                return IsWebExceptionStatusRetriable(wex);
            }

            if (ex is HttpRequestException hx)
            {
                var iex = hx?.InnerException;
                if (iex != null && iex is WebException wx)
                {
                    return IsWebExceptionStatusRetriable(wx);
                }
            }

            return false;
        }

        private static bool IsWebExceptionStatusRetriable(WebException wex)
        {
            if (wex?.Status != null)
            {
                return KnownRetriableWebExceptionStatuses.Any(x => x == wex.Status);
            }

            return false;
        }

        public static async Task<Dictionary<string, List<string>>> GetHttpRequestHeaders(this IAuthenticationHelper authenticationHelper, AuthenticationHelperSettings settings)
        {
            var headers = new Dictionary<string, List<string>>();
            var authenticationResult = await authenticationHelper
                .AcquireTokenAsync(settings)
                .ConfigureAwait(false);
            headers.AddAuthorizationHeader(authenticationResult.AccessTokenType, authenticationResult.AccessToken);
            return headers;
        }
    }
}

