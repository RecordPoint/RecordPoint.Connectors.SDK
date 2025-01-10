namespace RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus
{
    /// <summary>
    /// The azure service bus options.
    /// </summary>
    public class AzureServiceBusOptions
    {
        /// <summary>
        /// The SECTION NAME.
        /// </summary>
        public const string SECTION_NAME = "AzureServiceBusSettings";
        /// <summary>
        /// Gets or sets the queue prefix.
        /// </summary>
        public string QueuePrefix { get; set; } = string.Empty;
        /// <summary>
        /// The number of milliseconds to delay to allow in-process operations to complete before exiting the host.
        /// </summary>
        public int ServiceShutdownDelay { get; set; } = 25000;
        /// <summary>
        /// The amount of time in milliseconds to delay between checking the killswitch
        /// </summary>
        public int KillswitchCheckInterval { get; set; } = 60000;
        /// <summary>
        /// Gets or sets the service bus connection string.
        /// </summary>
        public string ServiceBusConnectionString { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the service bus name.
        /// </summary>
        public string ServiceBusName { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the max degree of parallelism.
        /// </summary>
        public int MaxDegreeOfParallelism { get; set; } = 1;
        /// <summary>
        /// Gets or sets the max auto lock renewal duration seconds.
        /// </summary>
        public int MaxAutoLockRenewalDurationSeconds { get; set; } = 300;
    }
}
