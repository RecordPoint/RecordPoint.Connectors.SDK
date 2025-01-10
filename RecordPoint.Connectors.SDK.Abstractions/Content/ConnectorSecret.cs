namespace RecordPoint.Connectors.SDK.Abstractions.Content
{
    /// <summary>
    /// Model for a Connector Secret
    /// </summary>
    public class ConnectorSecret
    {
        /// <summary>
        /// Connectors Secret Property Name
        /// </summary>
        public string Field { get; set; } = string.Empty;
        /// <summary>
        /// Connector Secrets Value
        /// </summary>
        public string Value { get; set; } = string.Empty;
    }
}
