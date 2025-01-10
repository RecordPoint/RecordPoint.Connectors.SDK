namespace RecordPoint.Connectors.SDK.Connectors
{
    /// <summary>
    /// App-level, connector options
    /// </summary>
    public class ConnectorOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public const string SECTION_NAME = "Connector";

        private const double DEFAULT_BINARY_SKIP_THRESHOLD = 500;

        /// <summary>
        /// Is binary submission enabled? Only has an effect if set to false in which case it
        /// overrides any other normal binary submission settings.
        /// </summary>
        public bool BinariesEnabled { get; set; } = true;

        /// <summary>
        /// The size in MegaBytes that any binary should be skipped for submission to R365.
        /// A negative value will disable the feature and all binaryies are submitted provided BinariesEnabled and SubmissionEnabled are true.
        /// </summary>
        public double BinarySkipThreshold { get; set; } = DEFAULT_BINARY_SKIP_THRESHOLD;

        /// <summary>
        /// Is any sort of submission enabled? Only have an effect if set to false in which case it
        /// overrides any other normal submission settings. Note that the connector still operates and
        /// thus pulls changes from the content source.
        /// </summary>
        public bool SubmissionEnabled { get; set; } = true;

        /// <summary>
        /// This flag is for connector to support retrying operations when an exception occurs up to the maximum retry count
        /// </summary>
        public bool RetryOnFailure { get; set; } = true;

        /// <summary>
        /// The maximum number of times a failed work operation should be retried before being sent to the dead letter queue. This can be set to -1 for never being dead lettered (default: 5 times)
        /// </summary>
        public int MaxRetries { get; set; } = 5;

        /// <summary>
        /// The maximum time a retry delay can be in seconds. This is to stop the exponential delay from getting too long. (default: 1 hour or 3600)
        /// </summary>
        public int MaxRetryDelay { get; set; } = 3600;

        /// <summary>
        /// The amount of time on seconds between retries of a failed work operation (default: 30 seconds)
        /// </summary>
        public int RetryDelay { get; set; } = 30;

        /// <summary>
        /// Enables an exponential backoff to retry attempts (default: true)
        /// </summary>
        public bool ExponentialRetryDelay { get; set; } = true;

    }
}