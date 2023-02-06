using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.Test.Mock.Work
{

    /// <summary>
    /// Current state of a work request.
    /// </summary>
    /// <remarks>
    /// Exposed by the mock work queue in order to gain "access" to the internal work queue state.
    /// </remarks>
    public class WorkRequestState
    {

        /// <summary>
        /// Original work request
        /// </summary>
        public WorkRequest WorkRequest { get; set; }

        /// <summary>
        /// Execute task
        /// </summary>
        public Task ExecuteTask { get; set; }

        /// <summary>
        /// Earliest time we can execute the request
        /// </summary>
        public DateTime EarliestTime { get; set; }

        /// <summary>
        /// Last work outcome
        /// </summary>
        public WorkResult LastWorkResult { get; set; }

        /// <summary>
        /// LaSt Exception
        /// </summary>
        public Exception LastException { get; set; }

        /// <summary>
        /// The retry this request is for
        /// </summary>
        public int Retry { get; set; }

    }
}
