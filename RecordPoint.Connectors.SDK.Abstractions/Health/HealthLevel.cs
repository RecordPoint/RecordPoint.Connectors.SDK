namespace RecordPoint.Connectors.SDK.Health
{

    /// <summary>
    /// Possible health check alert levels 
    /// </summary>
    /// <remarks>
    /// Health levels int value represent the priority order. The overall level
    /// for a healthcheck will be at the highest alertlevel
    /// </remarks>
    public enum HealthLevel
    {
        /// <summary>
        /// Normal operation
        /// </summary>
        Normal = 0,
        /// <summary>
        /// Warning of possible health issues
        /// </summary>
        Warning = 1,
        /// <summary>
        /// Connector is not operating correctly
        /// </summary>
        Failure = 2
    }
}
