namespace RecordPoint.Connectors.SDK.Test.Mock.Work
{
    /// <summary>
    /// Mock work queue options
    /// </summary>
    public class MockWorkQueueOptions
    {

        public const int DEFAULT_MAX_RETRIES = 10;

        public const int DEFAULT_POLL_MILLSECONDS = 100;

        public const int DEFAULT_RETRY_MILLSECONDS = 1000;

        /// <summary>
        /// Max permitted retries
        /// </summary>
        public int MaxRetries { get; set; } = DEFAULT_MAX_RETRIES;

        /// <summary>
        /// Time between queue polls
        /// </summary>
        public TimeSpan PollTimespan { get; set; } = TimeSpan.FromMilliseconds(DEFAULT_POLL_MILLSECONDS);

        /// <summary>
        /// Time between retry attempts
        /// </summary>
        public TimeSpan RetryTimespan { get; set; } = TimeSpan.FromMilliseconds(DEFAULT_RETRY_MILLSECONDS);


    }
}
