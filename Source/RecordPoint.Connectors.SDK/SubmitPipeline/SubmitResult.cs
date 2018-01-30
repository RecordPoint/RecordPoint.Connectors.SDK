
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
            Skipped
        }

        public Status SubmitStatus { get; set; }

        public SubmitResult()
        {
            SubmitStatus = Status.OK;
        }
    }
}
