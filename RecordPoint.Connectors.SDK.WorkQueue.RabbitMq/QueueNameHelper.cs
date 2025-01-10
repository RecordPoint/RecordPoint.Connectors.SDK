namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq
{
    /// <summary>
    /// The queue name helper.
    /// </summary>
    public static class QueueNameHelper
    {
        /// <summary>
        /// Suffix used on dead-letter queue names.
        /// </summary>
        public const string DeadLetterSuffix = "DL";

        /// <summary>
        /// Get queue name.
        /// </summary>
        /// <param name="workType">The work type.</param>
        /// <param name="queuePrefix">The queue prefix.</param>
        /// <returns>A string</returns>
        public static string GetQueueName(string workType, string queuePrefix) => string.IsNullOrEmpty(queuePrefix)
            ? $"{workType.Replace(" ", "-").ToLower()}"
            : $"{queuePrefix}-{workType.Replace(" ", "-").ToLower()}";

        /// <summary>
        /// Get DL queue name.
        /// </summary>
        /// <param name="workType">The work type.</param>
        /// <param name="queuePrefix">The queue prefix.</param>
        /// <returns>A string</returns>
        public static string GetDLQueueName(string workType, string queuePrefix) => string.IsNullOrEmpty(queuePrefix)
            ? $"{workType.Replace(" ", "-").ToLower()}-{DeadLetterSuffix}"
            : $"{queuePrefix}-{workType.Replace(" ", "-").ToLower()}-{DeadLetterSuffix}";
    }
}
