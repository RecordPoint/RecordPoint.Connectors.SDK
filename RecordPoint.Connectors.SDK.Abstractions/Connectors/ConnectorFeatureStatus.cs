namespace RecordPoint.Connectors.SDK.Connectors
{
    /// <summary>
    /// Feature status
    /// </summary>
    public class ConnectorFeatureStatus
    {
        /// <summary>
        /// Connector Id
        /// </summary>
        public string ConnectorId { get; set; } = string.Empty;

        /// <summary>
        /// Feature Name
        /// </summary>
        public string FeatureName { get; set; } = string.Empty;

        /// <summary>
        /// Is the feature enabled
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Reason why for the enabled state
        /// </summary>
        public string EnabledReason { get; set; } = string.Empty;
    }
}