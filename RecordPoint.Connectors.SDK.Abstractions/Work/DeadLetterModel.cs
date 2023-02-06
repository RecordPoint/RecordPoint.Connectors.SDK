namespace RecordPoint.Connectors.SDK.Work.Models
{
    /// <summary>
    /// DeadLetterModel
    /// </summary>
    public class DeadLetterModel
    {
        /// <summary>
        /// MessageId
        /// </summary>
        public string MessageId { get; set; } = string.Empty;
        /// <summary>
        /// EnqueuedTime
        /// </summary>
        public DateTimeOffset EnqueuedTime { get; set; }
        /// <summary>
        /// DeadLetterReason
        /// </summary>
        public string DeadLetterReason { get; set; } = string.Empty;
        /// <summary>
        /// SequenceNumber
        /// </summary>
        public string SequenceNumber { get; set; } = string.Empty;
    }
}
