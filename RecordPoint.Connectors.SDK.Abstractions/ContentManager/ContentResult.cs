using RecordPoint.Connectors.SDK.Content;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Outcome of a content operation
    /// </summary>
    public class ContentResult : ActionResultBase
    {
        /// <summary>
        /// Work outcome type
        /// </summary>
        public ContentResultType ResultType { get; set; }

        /// <summary>
        /// Outcome reason
        /// </summary>
        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// Optional exception that caused the sync to fail
        /// </summary>
        public Exception? Exception { get; set; }

        /// <summary>
        /// Progress cursor. Wil be needed on subsequent operations to get the next section of work
        /// </summary>
        public string Cursor { get; set; } = string.Empty;

        /// <summary>
        /// Audit Events
        /// </summary>
        public List<AuditEvent> AuditEvents { get; set; } = new List<AuditEvent>();

        /// <summary>
        /// Records
        /// </summary>
        public List<Record> Records { get; set; } = new List<Record>();

        /// <summary>
        /// Aggregations
        /// </summary>
        public List<Aggregation> Aggregations { get; set; } = new List<Aggregation>();

    }
}