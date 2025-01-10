namespace RecordPoint.Connectors.SDK.Filters
{
    /// <summary>
    /// The match result.
    /// </summary>
    public class MatchResult
    {
        /// <summary>
        /// Gets or sets  a value indicating whether to result.
        /// </summary>
        public bool Result { get; set; } = false;
        /// <summary>
        /// Gets or sets the match reason.
        /// </summary>
        public string MatchReason { get; set; } = string.Empty;
    }
}
