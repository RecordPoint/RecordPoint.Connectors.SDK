namespace RecordPoint.Connectors.SDK.Health
{
    /// <summary>
    /// Health check strategy
    /// </summary>
    public interface IHealthCheckStrategy
    {
        /// <summary>
        /// Health check type
        /// </summary>
        string HealthCheckType { get; }

        /// <summary>
        /// Perform a health check
        /// </summary>
        /// <returns>Health check</returns>
        Task<HealthCheckResult> HealthCheckAsync(CancellationToken stoppingToken);
    }
}
