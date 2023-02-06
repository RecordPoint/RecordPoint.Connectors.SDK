namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// The result of an attempt to retrieve a single piece of content
    /// </summary>
    public class BinaryRetrievalResult : ActionResultBase
    {
        /// <summary>
        /// Result type
        /// </summary>
        public BinaryRetrievalResultType ResultType { get; set; }

        /// <summary>
        /// Result reason
        /// </summary>
        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// Optional exception that caused the retrieval to fail
        /// </summary>
        public Exception? Exception { get; set; }

        /// <summary>
        /// Binary stream
        /// </summary>
        public Stream? Stream { get; set; }
    }
}
