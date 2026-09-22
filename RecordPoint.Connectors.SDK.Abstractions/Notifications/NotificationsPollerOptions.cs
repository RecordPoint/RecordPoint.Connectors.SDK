namespace RecordPoint.Connectors.SDK.Notifications
{

    /// <summary>
    /// Notifications poller options
    /// </summary>
    public class NotificationsPollerOptions
    {
        /// <summary>
        /// Section name for the option
        /// </summary>
        public const string SECTION_NAME = "Notifications:NotificationsPoller";
       
        /// <summary>
        /// Poll interval in seconds
        /// </summary>
        public int PollIntervalSeconds { get; set; } = 10;

        /// <summary>
        /// Tenant Domain to use when connecting to the notification service.         
        /// </summary>
        public string TenantDomainName { get; set; } = string.Empty;

        /// <summary>
        /// Polling Connector types to register for notifications.
        /// </summary>
        public Guid[] ConnectorTypes { get; set; } = Array.Empty<Guid>();
    }
}
