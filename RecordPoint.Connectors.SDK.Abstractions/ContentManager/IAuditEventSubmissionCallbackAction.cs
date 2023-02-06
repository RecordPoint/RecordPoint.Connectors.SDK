using RecordPoint.Connectors.SDK.Abstractions.ContentManager;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Content;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Defines an audit event submission callback which is executed after a successfull audit event submission
    /// </summary>
    public interface IAuditEventSubmissionCallbackAction
    {
        /// <summary>
        /// Executes after a successfull audit event submission operation
        /// </summary>
        /// <param name="connectorConfiguration">The connector configuration</param>
        /// <param name="auditEvent">Meta info for the audit event that was submitted</param>
        /// <param name="submissionActionType">Indicates if the action is being invoked pre-submit or post-submit</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task ExecuteAsync(ConnectorConfigModel connectorConfiguration, AuditEvent auditEvent, SubmissionActionType submissionActionType, CancellationToken cancellationToken);
    }
}