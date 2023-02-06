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
        /// </remarks>
        Task<List<HealthCheckItem>> HealthCheckAsync(CancellationToken stoppingToken);
    }
}
