using RecordPoint.Connectors.SDK.Content;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Result of a Channel Discovery operation
    /// </summary>
    public class ChannelDiscoveryResult : ActionResultBase
    {
        /// <summary>
        /// Work result type
        /// </summary>
        public ChannelDiscoveryResultType ResultType { get; set; }

        /// <summary>
        /// Result reason
        /// </summary>
        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// Optional exception that caused the operation to fail
        /// </summary>
        public Exception? Exception { get; set; }

        /// <summary>
        /// Channels that were discovered or updated.
        /// The Channel Discovery operation will upsert any Channels returned in this list.
        /// </summary>
        public List<Channel> Channels { get; set; } = new();

        /// <summary>
        /// New Channel Registrations
        /// The Channel Discovery operation will invoke new Content Registrations for Channels returned in this list.
        /// </summary>
        public List<Channel> NewChannelRegistrations { get; set; } = new();

        /// <summary>
        /// Audit Events
        /// </summary>
        public List<AuditEvent> AuditEvents { get; set; } = new List<AuditEvent>();
        
        /// <summary>
        /// Progress cursor. Will be needed on subsequent operations to get the next section of work.
        /// </summary>
        public string? Cursor { get; set; }
    }
}
