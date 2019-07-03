using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Interfaces
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
