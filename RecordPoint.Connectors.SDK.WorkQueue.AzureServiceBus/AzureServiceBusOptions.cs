namespace RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus
{
    public class AzureServiceBusOptions
    {
        public const string SECTION_NAME = "AzureServiceBusSettings";
        public string QueuePrefix { get; set; } = string.Empty;
        /// <summary>
        /// The number of milliseconds to delay to allow in-process operations to complete before exiting the host.
        /// </summary>
        public int ServiceShutdownDelay { get; set; } = 25000;
        /// <summary>
        /// The amount of time in milliseconds to delay between checking the killswitch
        /// </summary>
        public int KillswitchCheckInterval { get; set; } = 60000;
        public string ServiceBusConnectionString { get; set; } = string.Empty;
        public string ServiceBusName { get; set; } = string.Empty;
        public int MaxDegreeOfParallelism { get; set; } = 1;
    }
}
