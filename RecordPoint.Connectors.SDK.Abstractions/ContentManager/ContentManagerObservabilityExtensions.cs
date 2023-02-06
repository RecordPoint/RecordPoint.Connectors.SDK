namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Extension methods and constants for dealing with content manager observability
    /// </summary>
    public static class ContentManagerObservabilityExtensions
    {
        /// <summary>
        /// Global service name for the component
        /// </summary>
        public const string SERVICE_NAME = "Content Source Discovery";

        /// <summary>
        /// How many connectors were processed?
        /// </summary>
        public const string CONNECTOR_COUNT = "ConnectorCount";

        /// <summary>
        /// How many Channel Discovery Operations were started?
        /// </summary>
        public const string CHANNEL_DISCOVERY_OPERATIONS_STARTED_COUNT = "ChannelDiscoveryOperationsStartedCount";

        /// <summary>
        /// How many Content Registration Operations were started?
        /// </summary>
        public const string CONTENT_REGISTRATION_OPERATIONS_STARTED_COUNT = "ContentRegistrationOperationsStartedCount";

        /// <summary>
        /// How many Content Synchronisation Operations were started?
        /// </summary>
        public const string CONTENT_SYNCHRONISATION_OPERATIONS_STARTED_COUNT = "ContentSynchronisationOperationsStartedCount";

        /// <summary>
        /// How many Channels were processed?
        /// </summary>
        public const string CHANNEL_COUNT = "ChannelCount";
    }
}
