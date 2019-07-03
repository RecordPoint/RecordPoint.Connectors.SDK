using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Interfaces
{
    public interface IAzureStorageRetryProvider
    {
        bool IsCircuitClosed(out TimeSpan waitFor);

        Task ExecuteWithRetry(
            CloudBlobClient blobClient,
            Func<Task> codeToExecute,
            Type type,
            string methodName);
    }
}
