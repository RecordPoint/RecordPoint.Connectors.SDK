using RecordPoint.Connectors.SDK.Abstractions.ContentManager;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Content;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Defines a record submission callback which is executed after a successfull record submission
    /// </summary>
    public interface IRecordSubmissionCallbackAction
    {
        /// <summary>
        /// Executes after a successfull record submission operation
        /// </summary>
        /// <param name="connectorConfiguration">The connector configuration</param>
        /// <param name="record">Meta info for the record that was submitted</param>
        /// <param name="submissionActionType">Indicates if the action is being invoked pre-submit or post-submit</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task ExecuteAsync(ConnectorConfigModel connectorConfiguration, Record record, SubmissionActionType submissionActionType, CancellationToken cancellationToken);
    }
}