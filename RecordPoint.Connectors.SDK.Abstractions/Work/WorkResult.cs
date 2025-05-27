using RecordPoint.Connectors.SDK.Observability;

namespace RecordPoint.Connectors.SDK.Work
{
    /// <summary>
    /// Result of completing work
    /// </summary>
    public class WorkResult
    {
        /// <summary>
        /// Unit of work has completed succcessfully
        /// </summary>
        /// <returns></returns>
        public static WorkResult Complete(string? reason = null) => new()
        {
            ResultType = WorkResultType.Complete,
            Reason = reason
        };

        /// <summary>
        /// Unit of work has critically failed
        /// </summary>
        /// <returns></returns>
        public static WorkResult Failed(string reason) => new()
        {
            ResultType = WorkResultType.Failed,
            Reason = reason
        };

        /// <summary>
        /// Unit of work has critically failed due to an exception
        /// </summary>
        /// <returns></returns>
        public static WorkResult Failed(Exception exception) => new()
        {
            ResultType = WorkResultType.Failed,
            Reason = exception.Message,
            Exception = exception
        };

        /// <summary>
        /// Unit of work has critically failed due to an exception
        /// </summary>
        /// <returns></returns>
        public static WorkResult Failed(string reason, Exception exception) => new()
        {
            ResultType = WorkResultType.Failed,
            Reason = reason,
            Exception = exception
        };

        /// <summary>
        /// Unit of work has been abandoned
        /// </summary>
        /// <returns></returns>
        public static WorkResult Abandoned(string reason) => new()
        {
            ResultType = WorkResultType.Abandoned,
            Reason = reason
        };

        /// <summary>
        /// Unit of work has been abandoned
        /// </summary>
        /// <returns></returns>
        public static WorkResult Abandoned(string reason, Exception exception) => new()
        {
            ResultType = WorkResultType.Abandoned,
            Reason = reason,
            Exception = exception
        };

        /// <summary>
        /// Unit of work will be deadletter
        /// </summary>
        /// <returns></returns>
        public static WorkResult DeadLetter(string reason, Exception exception) => new()
        {
            ResultType = WorkResultType.DeadLetter,
            Reason = reason,
            Exception = exception
        };

        /// <summary>
        /// Defer processing till a later time
        /// </summary>
        /// <returns></returns>
        public static WorkResult Defer(string reason, DateTimeOffset? waitTill) => new()
        {
            ResultType = WorkResultType.Deferred,
            Reason = reason,
            WaitTill = waitTill
        };

        /// <summary>
        /// Work result type
        /// </summary>
        public WorkResultType ResultType { get; set; }

        /// <summary>
        /// Duration of the unit of work
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Result reason
        /// </summary>
        public string? Reason { get; set; } = string.Empty;

        /// <summary>
        /// Wait till
        /// </summary>
        public DateTimeOffset? WaitTill { get; set; }

        /// <summary>
        /// Exception that caused the work to fail
        /// </summary>
        public Exception? Exception { get; set; }

        /// <summary>
        /// Additional observability measures
        /// </summary>
        public Measures Measures { get; set; } = new Measures();
    }
}
