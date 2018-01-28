using Microsoft.Rest;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.Client
{
    public static class ApiClientRetryPolicy 
    {
        public static readonly List<HttpStatusCode> KnownRetriableWebResponseStatusCodes = new List<HttpStatusCode>()
        {
            (HttpStatusCode)429,
            HttpStatusCode.InternalServerError
        };

        public static Policy GetPolicy(int maxTryCount, int retryDelayMilliseconds, CancellationToken ct)
        {
            // TODO: Pass in a CancellationToken
            return Policy.Handle<Exception>(ex => ex.IsRecords365ApiRetriableException(ct))
                .WaitAndRetryAsync(maxTryCount, x => TimeSpan.FromMilliseconds(retryDelayMilliseconds),
                (ex, ts) =>
                {
                   // Logger.WriteWarning($"Connector API client is retrying a transient failure, DelayMilliseconds = [{ts.TotalMilliseconds}], LastException = [{ex?.ToString() ?? "<null>"}");
                });
        }

        public static bool IsRecords365ApiRetriableException(this Exception ex, CancellationToken ct)
        {
            if(ex is TaskCanceledException && !ct.IsCancellationRequested)
            {
                // Thrown when HttpClient.SendAsync times out
                return true;
            }

            if(ex.IsRetriableWebRequestException())
            {
                return true;
            }

            if(ex is HttpOperationException hox)
            {
                return hox?.Response?.IsRetriableStatusCode() ?? false;
            }

            return false;
        }

        private static bool IsRetriableStatusCode(this HttpResponseMessageWrapper response)
        {
            if(response == null)
            {
                return false;
            }

            return KnownRetriableWebResponseStatusCodes.Any(x => x == response.StatusCode);
        }
    }
}

