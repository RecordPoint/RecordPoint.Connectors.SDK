using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Polly;
using RecordPoint.Connectors.SDK.Diagnostics;
using RecordPoint.Connectors.SDK.Helpers;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Providers
{
    /// <summary>
    /// Retry provider for Azure blob storage 
    /// </summary>
    public class AzureBlobRetryProvider
    {
        private const int MaxAttempts = 3;
        private const double PowBase = 2;

        /// <summary>
        /// A log.
        /// </summary>
        public ILog Log { get; set; }

        private static readonly Func<int, TimeSpan> _sleepTimeFunction = (retryCount) =>
        {
            return TimeSpan.FromSeconds(Math.Pow(PowBase, retryCount));
        };

        /// <summary>
        /// Execute the code with a retry policy
        /// </summary>
        public async Task ExecuteWithRetry(
            CloudBlobClient blobClient,
            Func<Task> codeToExecute,
            Type type,
            string methodName)
        {
            //Cancel the existing default retry policy provided by Azure Blob before executing using our own retry policy.
            blobClient.DefaultRequestOptions.RetryPolicy = new Microsoft.WindowsAzure.Storage.RetryPolicies.NoRetry();

            await GetRetryPolicy(type, methodName).ExecuteAsync(async () =>
            {
                await codeToExecute().ConfigureAwait(false);
            }).ConfigureAwait(false);

        }

        /// <summary>
        /// Retry policy
        /// </summary>
        /// <param name="type"></param>
        /// <param name="methodName"></param>
        protected virtual Policy GetRetryPolicy(Type type, string methodName)
        {
            return Policy.Handle<Exception>(ex => IsTransientAzureBlobException(ex)).WaitAndRetryAsync(
                MaxAttempts,
                _sleepTimeFunction,
                (exception, waitDuration, retryAttempt) =>
                {
                    Log?.LogVerbose(type, methodName, $"Retrying transient Azure Blob Storage exception. " +
                        $"WaitTime [{waitDuration.TotalSeconds}s] " +
                        $"Attempt [{retryAttempt}] " +
                        $"Exception [{exception}]"
                    );
                }
            );
        }

        private bool IsTransientAzureBlobException(Exception e)
        {
            // All AzureBlob exceptions should be of type StorageException or AggregateException containing StorageException
            // Documentation indicates this will handle all transient/retriable exceptions (That is, anything that isn't: >=400 && <500, 501, 505).
            // See for more detail: https://blogs.msdn.microsoft.com/windowsazurestorage/2011/02/02/overview-of-retry-policies-in-the-windows-azure-storage-client-library/
            if (e.IsAssignableFrom<StorageException>(ex =>
                (ex.RequestInformation.HttpStatusCode >= 400
                && ex.RequestInformation.HttpStatusCode < 500)
                || ex.RequestInformation.HttpStatusCode == 501
                || ex.RequestInformation.HttpStatusCode == 505))
            {
                return false;
            }
            else if (e.IsAssignableFrom<StorageException>(ex =>
                (ex != null)))
            {
                // Known Retriables:
                // (statusCode == 500 && se.RequestInformation.ErrorCode == "InternalError") // Internal Server Exception
                // (statusCode == 500 && se.RequestInformation.ErrorCode == "OperationTimedOut") // Operation per second is over account limit
                return true;
            }

            //Any non-Storage exception is not an azure blob exception, therefore will be returned as false
            return false;
        }
    }
}
