using RecordPoint.Connectors.SDK.Abstractions.ContentManager;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Content;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Defines an aggregation submission callback which is executed after a successfull aggregation submission
    /// </summary>
    public interface IAggregationSubmissionCallbackAction
    {
        /// <summary>
        /// Executes after a successfull aggregation submission operation
        /// </summary>
        /// <param name="connectorConfiguration">The connector configuration</param>
        /// <param name="aggregation">Meta info for the aggregation that was submitted</param>
        /// <param name="submissionActionType">Indicates if the action is being invoked pre-submit or post-submit</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task ExecuteAsync(ConnectorConfigModel connectorConfiguration, Aggregation aggregation, SubmissionActionType submissionActionType, CancellationToken cancellationToken);
    }
}