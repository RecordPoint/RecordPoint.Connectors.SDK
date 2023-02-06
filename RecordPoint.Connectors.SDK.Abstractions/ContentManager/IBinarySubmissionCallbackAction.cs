using RecordPoint.Connectors.SDK.Abstractions.ContentManager;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Content;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Defines a binary submission callback which is executed after a successfull binary submission
    /// </summary>
    public interface IBinarySubmissionCallbackAction
    {
        /// <summary>
        /// Executes after a successfull binary submission operation
        /// </summary>
        /// <param name="connectorConfiguration">The connector configuration</param>
        /// <param name="binaryMetaInfo">Meta info for the binary that was submitted</param>
        /// <param name="submissionActionType">Indicates if the action is being invoked pre-submit or post-submit</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task ExecuteAsync(ConnectorConfigModel connectorConfiguration, BinaryMetaInfo binaryMetaInfo, SubmissionActionType submissionActionType, CancellationToken cancellationToken);
    }
}