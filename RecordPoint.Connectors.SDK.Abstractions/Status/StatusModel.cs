namespace RecordPoint.Connectors.SDK.Status
{
    /// <summary>
    /// The status model.
    /// </summary>
    public class StatusModel
    {
        /// <summary>
        /// Gets or sets the connector id.
        /// </summary>
        public string ConnectorId { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public List<string> Status { get; set; } = new();
    }
}
