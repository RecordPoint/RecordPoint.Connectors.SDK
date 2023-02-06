using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.Caching
{
    public interface ISemaphoreLockScopedKeyAction
    {
        /// <summary>
        /// Execution point for generating a scoped semaphore key
        /// </summary>
        /// <param name="connectorConfigModel"></param>
        /// <param name="workType"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> ExecuteAsync(ConnectorConfigModel connectorConfigModel, string workType, CancellationToken cancellationToken);
    }
}
