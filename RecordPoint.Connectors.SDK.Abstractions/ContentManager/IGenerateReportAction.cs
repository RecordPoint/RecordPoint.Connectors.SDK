using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Definition for a Content Report Generation action
    /// </summary>
    public interface IGenerateReportAction
    {
        /// <summary>
        /// Begin a new content report generation operation
        /// </summary>
        /// <param name="connectorConfiguration">The onnector configuration</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Result of the discovery operation</returns>
        Task<ContentResult> BeginAsync(ConnectorConfigModel ConnectorConfiguration, CancellationToken cancellationToken);

        /// <summary>
        /// Continue a content report generation
        /// </summary>
        /// <param name="connectorConfiguration">The onnector configuration</param>
        /// <param name="cursor">Scan cursor provided by the prior sync operation</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Result of the discovery operation</returns>
        Task<ContentResult> ContinueAsync(ConnectorConfigModel ConnectorConfiguration, string cursor, CancellationToken cancellationToken);
    }
}