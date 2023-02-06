namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq
{
    public class RabbitMqOptions
    {
        public const string SECTION_NAME = "RabbitMqSettings";
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
    }
}
