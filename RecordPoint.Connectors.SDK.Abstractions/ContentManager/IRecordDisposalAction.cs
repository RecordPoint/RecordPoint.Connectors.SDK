using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Content;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Defines a record disposal action which is responsible for removing the record from the content source.
    /// </summary>
    public interface IRecordDisposalAction
    {
        /// <summary>
        /// Destroys the record in the content source
        /// </summary>
        /// <param name="connectorConfiguration">The connector configuration</param>
        /// <param name="record">The record to dispose</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Get binary outcome</returns>
        Task<RecordDisposalResult> ExecuteAsync(ConnectorConfigModel connectorConfiguration, Record record, CancellationToken cancellationToken);
    }
}