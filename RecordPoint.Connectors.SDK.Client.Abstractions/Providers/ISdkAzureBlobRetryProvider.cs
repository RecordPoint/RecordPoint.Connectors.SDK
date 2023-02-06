using Microsoft.Azure.Storage.Blob;

namespace RecordPoint.Connectors.SDK.Providers
{
    public interface ISdkAzureBlobRetryProvider
    {
        Task ExecuteWithRetry(
            CloudBlobClient blobClient,
            Func<Task> codeToExecute,
            Type type,
            string methodName);
    }
}
