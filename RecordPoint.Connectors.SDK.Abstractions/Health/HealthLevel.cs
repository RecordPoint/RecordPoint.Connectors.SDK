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
        Normal = 0,  // Normal operation
        Warning = 1,  // Warning of possible health issues
        Failure = 2  // Connector is not operating correctly
    }
}
