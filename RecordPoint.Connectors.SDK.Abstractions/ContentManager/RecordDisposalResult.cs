namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// The result of an attempt to dispose a record
    /// </summary>
    public class RecordDisposalResult : ActionResultBase
    {
        /// <summary>
        /// Result type
        /// </summary>
        public RecordDisposalResultType ResultType { get; set; }

        /// <summary>
        /// Result reason
        /// </summary>
        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// Optional exception that caused the disposal to fail
        /// </summary>
        public Exception? Exception { get; set; }

    }
}
