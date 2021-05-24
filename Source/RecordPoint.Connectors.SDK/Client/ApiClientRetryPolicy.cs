using Microsoft.Rest;
using Polly;
using RecordPoint.Connectors.SDK.Diagnostics;
using RecordPoint.Connectors.SDK.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Client
{
    /// <summary>
    /// Helpers for handling retries against the Records365 vNext Connector API.
    /// </summary>
    public static class ApiClientRetryPolicy 
    {
        private const int RetryPolicyBaseSeconds = 2;
        /// <summary>
        /// A list of HTTP response codes that are retriable.
        /// </summary>
        public static readonly List<HttpStatusCode> KnownRetriableWebResponseStatusCodes = new List<HttpStatusCode>()
        {
            (HttpStatusCode)429,
            HttpStatusCode.RequestTimeout,
            HttpStatusCode.ServiceUnavailable,
            HttpStatusCode.GatewayTimeout
        };

        /// <summary>
        /// Gets a retry policy for use when calling the Records365 vNext Connector API.
        /// </summary>
        /// <param name="maxTryCount"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public static Policy GetPolicy(int maxTryCount, CancellationToken ct)
        {
            // TODO: Pass in a CancellationToken
            return Policy.Handle<Exception>(ex => ex.IsRecords365ApiRetriableException(ct))
                .WaitAndRetryAsync(maxTryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(RetryPolicyBaseSeconds, retryAttempt)),
                (ex, ts) =>
                {
                   // Logger.WriteWarning($"Connector API client is retrying a transient failure, DelayMilliseconds = [{ts.TotalMilliseconds}], LastException = [{ex?.ToString() ?? "<null>"}");
                });
        }

        /// <summary>
        /// Gets a retry policy for use when calling the Records365 vNext Connector API.
        /// </summary>
        /// <param name="log"></param>
        /// <param name="callerType"></param>
        /// <param name="methodName"></param>
        /// <param name="maxTryCount"></param>
        /// <param name="ct"></param>
        /// <param name="logPrefix"></param>
        /// <returns></returns>
        public static Policy GetPolicy(ILog log, Type callerType, string methodName, int maxTryCount, CancellationToken ct, string logPrefix)
        {
            return Policy
                .Handle<Exception>(ex => ex.IsRecords365ApiRetriableException(ct))
                .WaitAndRetryAsync(
                    maxTryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(RetryPolicyBaseSeconds, retryAttempt)),
                    (ex, waitTime, retryCount, context) =>
                    {
                        log?.LogMessage(
                            callerType, 
                            $"{methodName}_Retry", 
                            $"Retrying a transient failure: {logPrefix}" +
                            $"RetryCount [{retryCount}] " +
                            $"WaitTime [{waitTime.TotalMilliseconds}ms] " +
                            $"Exception [{ex?.ToString() ?? "<null>"}]"
                        );
                    }
                );
        }

        /// <summary>
        /// Determines if the exception is considered retriable in the context of accessing the Records365 vNext
        /// Connector API.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
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

