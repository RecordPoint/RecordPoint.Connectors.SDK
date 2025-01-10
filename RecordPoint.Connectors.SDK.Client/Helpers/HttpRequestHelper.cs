using RecordPoint.Connectors.SDK.Client;
using System.Net;

namespace RecordPoint.Connectors.SDK.Helpers
{
    /// <summary>
    /// The HTTP request helper.
    /// </summary>
    public static class HTTPRequestHelper
    {
        /// <summary>
        /// The known retriable web exception statuses.
        /// </summary>
        private static readonly WebExceptionStatus[] _knownRetriableWebExceptionStatuses = new[]
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

        /// <summary>
        /// Checks if is retriable web request exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns>A bool</returns>
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

        /// <summary>
        /// Checks if is web exception status retriable.
        /// </summary>
        /// <param name="wex">The wex.</param>
        /// <returns>A bool</returns>
        private static bool IsWebExceptionStatusRetriable(WebException wex)
        {
            if (wex?.Status != null)
            {
                return _knownRetriableWebExceptionStatuses.Any(x => x == wex.Status);
            }

            return false;
        }

        /// <summary>
        /// Get Headers with Bearer Token
        /// </summary>
        /// <param name="authProvider"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static async Task<Dictionary<string, List<string>>> GetHttpRequestHeaders(this IAuthenticationProvider authProvider, AuthenticationHelperSettings settings)
        {
            var headers = new Dictionary<string, List<string>>();
            var authenticationResult = await authProvider
                .AcquireTokenAsync(settings)
                .ConfigureAwait(false);
            headers.AddAuthorizationHeader(authenticationResult.AccessTokenType, authenticationResult.AccessToken);
            return headers;
        }
    }
}

