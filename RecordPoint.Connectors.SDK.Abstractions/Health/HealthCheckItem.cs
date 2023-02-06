namespace RecordPoint.Connectors.SDK.Health
{
    /// <summary>
    /// Result of a health check
    /// </summary>
    public class HealthCheckItem
    {
        /// <summary>
        /// Health check type
        /// </summary>
        public string HealthCheckType { get; set; } = string.Empty;

        /// <summary>
        /// Alert level
        /// </summary>
        public HealthLevel HealthLevel { get; set; } = HealthLevel.Normal;

        /// <summary>
        /// Should an alert
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
