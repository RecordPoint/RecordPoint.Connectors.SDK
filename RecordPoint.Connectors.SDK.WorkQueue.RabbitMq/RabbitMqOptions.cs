namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq
{
    /// <summary>
    /// The rabbit mq options.
    /// </summary>
    public class RabbitMqOptions
    {
        /// <summary>
        /// The SECTION NAME.
        /// </summary>
        public const string SECTION_NAME = "RabbitMqSettings";
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
        /// HostName for RabbitMq server
        /// </summary>
        public string HostName { get; set; } = string.Empty;
        /// <summary>
        /// HostUserName username for RabbitMq server
        /// </summary>
        public string HostUserName { get; set; } = string.Empty;
        /// <summary>
        /// HostPassword password for RabbitMq server
        /// </summary>
        public string HostPassword { get; set; } = string.Empty;

        /// <summary>
        /// PrefetchCount (degree of parallelism) to be used with RabbitMq queue.
        /// </summary>
        /// <remarks>
        /// If not supplied, RabbitMq will feed out as many messages
        /// as possible according to the current available system resources.
        /// </remarks>
        public ushort? MaxDegreeOfParallelism { get; set; } = null;
    }
}
