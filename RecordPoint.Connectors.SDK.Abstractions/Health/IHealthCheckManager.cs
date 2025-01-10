namespace RecordPoint.Connectors.SDK.Health
{

    /// <summary>
    /// Health Check Manager definition
    /// </summary>
    public interface IHealthCheckManager
    {
        /// <summary>
        /// Gets the current health checks results
        /// </summary>
        public HealthCheckResult HealthCheckResult { get; set; }

        /// <summary>
        /// Run runtime health check
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task</returns>
        Task RunHealthCheckAsync(CancellationToken cancellationToken);
    }
}
