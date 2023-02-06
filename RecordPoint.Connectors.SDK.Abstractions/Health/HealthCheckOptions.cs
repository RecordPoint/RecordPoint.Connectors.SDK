namespace RecordPoint.Connectors.SDK.Health
{

    /// <summary>
    /// App-level, health check options
    /// </summary>
    public class HealthCheckOptions
    {
        public const string SECTION_NAME = "HealthCheck";

        /// <summary>
        /// Number of seconds to trigger health check.
        /// 
        /// Defaults to 10 minutes.
        /// </summary>
        public int HealthCheckFrequencySeconds { get; set; } = 600;

        /// <summary>
        /// Number of seconds to delay for the first health check to let the
        /// initialize itself.
        /// 
        /// Defaults to 1 minute.
        /// </summary>
        public int HealthCheckStartDelaySeconds { get; set; } = 60;

        /// <summary>
        /// Number of Gb which is minimum requires for disk space
        /// </summary>
        public int MinimumDiskSpaceInGB { get; set; } = 30;
    }
}
