namespace RecordPoint.Connectors.SDK.ContentManager
{
    public class ContentManagerOptions
    {
        public const string SECTION_NAME = "ContentManager";

        /// <summary>
        /// Number of seconds to delay betwwen Content Manager Operation executions
        /// </summary>
        /// <remarks>
        /// The Content Manager Operation ensures Channel Discovery & Content Synchronisation is being performed for all Connector Configurations and discovered Channels.
        /// Defaults to 300 seconds (5 minutes)
        /// </remarks>
        public int DelaySeconds { get; set; } = 300;

        /// <summary>
        /// Automatically remove Channels that belong to Connectors Configurations that have been removed
        /// </summary>
        /// <remarks>
        /// Defaults to true
        /// </remarks>
        public bool CleanUpChannels { get; set; } = true;

        /// <summary>
        /// Automatically remove Completed Managed Work
        /// </summary>
        /// <remarks>
        /// Defaults to true
        /// </remarks>
        public bool RemoveCompletedWork { get; set; } = true;

        /// <summary>
        /// Automatically remove Abandoned Managed Work
        /// </summary>
        /// <remarks>
        /// Defaults to true
        /// </remarks>
        public bool RemoveAbandonedWork { get; set; } = true;

        /// <summary>
        /// The max age for a Completed Managed Work Status before it is removed
        /// </summary>
        /// <remarks>
        /// Defaults to 300 seconds (5 minutes)
        /// </remarks>
        public int MaxCompletedWorkAge { get; set; } = 300;

        /// <summary>
        /// The max age for a Abandoned Managed Work Status before it is removed
        /// </summary>
        /// <remarks>
        /// Defaults to 300 seconds (5 minutes)
        /// </remarks>
        public int MaxAbandonedWorkAge { get; set; } = 300;

        /// <summary>
        /// The maximum age for Channel Synchronisation Work that has been abandoned due to the Channel no longer existing
        /// </summary>
        public int MaxAbandonedChannelSynchronisationAge { get; set; } = 300;
    }


    /// <summary>
    /// Base Configuration settings for Content Manager Operations
    /// </summary>
    public abstract class ContentManagerOperationOptionsBase
    {
        /// <summary>
        /// The typical delay between executions
        /// </summary>
        /// <remarks>
        /// Defaults to 60 seconds.
        /// </remarks>
        public int DelaySeconds { get; set; } = 60;

        /// <summary>
        /// Increases the delay time exponentially
        /// </summary>
        /// <remarks>
        /// Defaults to Enabled
        /// </remarks>
        public bool ExponentialBackOff { get; set; } = true;

        /// <summary>
        /// Re-execute the operation immediately when the previous execution results in new work
        /// </summary>
        /// <remarks>
        /// Defaults to Disabled
        /// </remarks>
        public bool ImmediateReExecution { get; set; } = false;

        /// <summary>
        /// Adds a random time seed to the delay
        /// </summary>
        /// <remarks>
        /// Defaults to Disabled
        /// </remarks>
        public bool RandomDelay { get; set; } = false;

        /// <summary>
        /// The maximum delay between operation executions when Back off is enabled
        /// </summary>
        /// <remarks>
        /// Defaults to 600 seconds (10 minutes)
        /// </remarks>
        public int MaxDelaySeconds { get; set; } = 600;
    }

    /// <summary>
    /// Configuration settings for Channel Discovery
    /// </summary>
    public class ChannelDiscoveryOperationOptions : ContentManagerOperationOptionsBase
    {
        public const string SECTION_NAME = "ContentManager:ChannelDiscovery";
    }

    /// <summary>
    /// Configuration settings for Content Registration
    /// </summary>
    public class ContentRegistrationOperationOptions : ContentManagerOperationOptionsBase
    {
        public const string SECTION_NAME = "ContentManager:ContentRegistration";

        /// <summary>
        /// The maximum number of records to retrieve in a single Content Registration execution
        /// </summary>
        public int MaxFetchBatchSize { get; set; } = 1000;
    }

    /// <summary>
    /// Configuration settings for Content Synchronisation
    /// </summary>
    public class ContentSynchronisationOperationOptions : ContentManagerOperationOptionsBase
    {
        public const string SECTION_NAME = "ContentManager:ContentSynchronisation";

        /// <summary>
        /// The maximum number of records to retrieve in a single Content Synchronisation execution
        /// </summary>
        public int MaxFetchBatchSize { get; set; } = 1000;
    }

}
