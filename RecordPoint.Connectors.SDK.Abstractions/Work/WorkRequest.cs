namespace RecordPoint.Connectors.SDK.Work
{
    /// <summary>
    /// Request to execute a unit of work
    /// </summary>
    public class WorkRequest
    {
        /// <summary>
        /// Work Type
        /// </summary>
        public string WorkType { get; set; } = string.Empty;

        /// <summary>
        /// Unique Work ID that identifies the unit of work
        /// </summary>
        public string WorkId { get; set; } = string.Empty;

        /// <summary>
        /// Connector the work relates to.
        /// </summary>
        public string ConnectorConfigId { get; set; } = string.Empty;

        /// <summary>
        /// Tenant Id
        /// </summary>
        public string TenantId { get; set; } = string.Empty;

        /// <summary>
        /// Tenant domain name
        /// </summary>
        public string TenantDomainName { get; set; } = string.Empty;

        /// <summary>
        /// Work Request Body
        /// </summary>
        public string Body { get; set; } = string.Empty;

        /// <summary>
        /// Time the request was submitted
        /// </summary>
        public DateTimeOffset SubmitDateTime { get; set; }

        /// <summary>
        /// Time we must finish work by
        /// </summary>
        public DateTimeOffset MustFinishDateTime { get; set; }

        /// <summary>
        /// Optional time to wait to submit the work
        /// </summary>
        public DateTimeOffset? WaitTill { get; set; }

        /// <summary>
        /// Number of faulted count for a work request.
        /// </summary>
        public int? FaultedCount { get; set; }
    }
}
