using RecordPoint.Connectors.SDK.Work.Models;

namespace RecordPoint.Connectors.SDK.Work
{
    /// <summary>
    /// Deadletter queue interface
    /// </summary>
    public interface IDeadLetterQueueService
    {
        /// <summary>
        /// Get messages based on the queue
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="maxCount">Max number of items to return. If 0, all items are returned</param>
        /// <returns></returns>
        Task<List<DeadLetterModel>> GetMessagesAsync(string queueName, int maxCount = 0);

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
        /// Drain up to <paramref name="maxCount"/> messages from the dead-letter
        /// queue and resubmit them to the main queue, without performing a
        /// peek-then-receive round-trip. Implementations should stream messages
        /// directly from the DLQ in PeekLock mode, resubmit each one, complete
        /// successfully resubmitted messages, and abandon messages that fail to
        /// resubmit so they remain available in the DLQ for a later attempt.
        /// </summary>
        /// <param name="queueName">The main queue whose dead-letter sub-queue should be drained.</param>
        /// <param name="maxCount">The maximum number of messages to resubmit in this call.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>
        /// A <see cref="DeadLetterResubmitResult"/> carrying the number of messages resubmitted
        /// and, when that count is 0, whether the dead-letter queue was confirmed empty (as
        /// opposed to the pass merely making no progress). A caller looping to drain the DLQ
        /// should stop only once <see cref="DeadLetterResubmitResult.QueueConfirmedEmpty"/> is
        /// <c>true</c>, so a transient no-progress pass does not end the drain prematurely.
        /// </returns>
        Task<DeadLetterResubmitResult> ResubmitTopMessagesAsync(string queueName, int maxCount, CancellationToken cancellationToken = default);

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