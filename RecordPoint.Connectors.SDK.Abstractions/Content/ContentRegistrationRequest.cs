namespace RecordPoint.Connectors.SDK.Abstractions.Content
{
    /// <summary>
    /// Model for a Content Registration Request
    /// </summary>
    public class ContentRegistrationRequest
    {
        /// <summary>
        /// The context of a Content Registration Request
        /// e.g. A high level aggregation within a Content Source
        /// </summary>
        public string Context { get; set; } = string.Empty;
        /// <summary>
        /// The earliest point in time to register content
        /// </summary>
        public DateTimeOffset? StartDate { get; set; }
        /// <summary>
        /// The point in time for which content should be registered up to
        /// </summary>
        public DateTimeOffset? EndDate { get; set; }
    }
}
