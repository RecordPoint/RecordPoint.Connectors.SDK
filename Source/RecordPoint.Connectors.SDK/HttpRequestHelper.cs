using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace RecordPoint.Connectors
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
    }
}

