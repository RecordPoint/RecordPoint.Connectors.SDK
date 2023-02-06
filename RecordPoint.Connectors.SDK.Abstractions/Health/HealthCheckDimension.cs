namespace RecordPoint.Connectors.SDK.Health
{

    /// <summary>
    /// Health check dimensions 
    /// </summary>
    public class HealthCheckDimension : HealthCheckItem
    {
        /// <summary>
        /// Connector Id
        /// </summary>
        public string ConnectorId { get; set; } = string.Empty;

        /// <summary>
        /// Dimension Value
        /// </summary>
        public string Value { get; set; } = string.Empty;
    }
}
