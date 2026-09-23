namespace RecordPoint.Connectors.SDK.Work
{
    /// <summary>
    /// The outcome of a single <see cref="IDeadLetterQueueService.ResubmitTopMessagesAsync"/> pass.
    /// </summary>
    /// <param name="Resubmitted">
    /// The number of messages that were successfully resubmitted (both sent to the main
    /// queue and completed from the dead-letter queue) during this pass.
    /// </param>
    /// <param name="QueueConfirmedEmpty">
    /// Only meaningful when <paramref name="Resubmitted"/> is 0. <c>true</c> when the
    /// dead-letter queue was confirmed empty by a lock-independent check (a peek that
    /// returned nothing); <c>false</c> when the pass merely made no progress (for example
    /// the head messages were momentarily locked, or every send failed) and the queue is
    /// not known to be empty. Callers looping to drain a DLQ should keep going while this
    /// is <c>false</c> and stop only once it is <c>true</c>.
    /// </param>
    public readonly record struct DeadLetterResubmitResult(int Resubmitted, bool QueueConfirmedEmpty);
}
