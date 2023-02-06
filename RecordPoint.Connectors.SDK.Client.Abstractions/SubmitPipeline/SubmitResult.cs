namespace RecordPoint.Connectors.SDK.SubmitPipeline
{
    public class SubmitResult
    {
        public enum Status
        {
            /// <summary>
            /// Submit succeeded.
            /// </summary>
            OK,
            /// <summary>
            /// Indicates that the Records365 vNext platform rejected the submit
            /// because the connector was disabled.
            /// Submissions that fail with this status should not be dead-lettered.
            /// The connector implementation should stop running if this status is returned.
            /// </summary>
            ConnectorDisabled,
            /// <summary>
            /// Indicates that the Records365 vNext platform rejected the submit
            /// because the connector was not found.
            /// Submissions that fail with this status should not be dead-lettered.
            /// The connector implementation should stop running if this status is returned.
            /// </summary>
            ConnectorNotFound,
            /// <summary>
            /// Indicates that the submission was skipped by the submission pipleline.
            /// </summary>
            Skipped,
            /// <summary>
            /// Indicates that the Records365 vNext platform rejected the submit
            /// because it was not yet valid - for example, a dependent item may not yet
            /// be processed and available in the platform. The connector implementation
            /// should retry the submit at a later time. The connector should abandon the 
            /// submit if Deferred is returned for the same submit for many repeated
            /// attempts over a long period of time.
            /// </summary>
            Deferred,
            /// <summary>
            /// Indicates that the Records365 vNext platform rejected the submit
            /// because a part of the submission chain is experiencing heavy load.
            /// The connector implementation should use a circuit breaker to wait 
            /// on all submissions and retry the submit at a later time. 
            /// </summary>
            TooManyRequests
        }

        public Status SubmitStatus { get; set; }

        public DateTime? WaitUntilTime { get; set; } = null;

        public SubmitResult()
        {
            SubmitStatus = Status.OK;
        }
    }
}
