using RecordPoint.Connectors.SDK.Work.Models;

namespace RecordPoint.Connectors.SDK.Work
{
    /// <summary>
    /// Deadletter queue interface
    /// </summary>
    public interface IDeadLetterQueueService
    {
        /// <summary>
        /// Get all messages based on the queue
        /// </summary>
        /// <param name="queueName"></param>       
        /// <returns></returns>
        Task<List<DeadLetterModel>> GetAllMessagesAsync(string queueName);

        /// <summary>
        /// Get message based on the queue and sequenceNumber
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="sequenceNumber"></param>
        /// <returns></returns>
        Task<DeadLetterModel> GetMessageAsync(string queueName, long sequenceNumber);

        /// <summary>
        /// Resubmit to queue based on the queueName and sequenceNumbers
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="sequenceNumbers"></param>
        /// <returns></returns>
        Task ResubmitMessagesAsync(string queueName, long[] sequenceNumbers);

        /// <summary>
        /// Delete the message from the deadletter queue
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="sequenceNumber"></param>
        /// <returns></returns>
        Task DeleteMessageAsync(string queueName, long sequenceNumber);

        /// <summary>
        /// Delete the all messages from the deadletter queue
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        Task DeleteAllMessagesAsync(string queueName);
    }
}