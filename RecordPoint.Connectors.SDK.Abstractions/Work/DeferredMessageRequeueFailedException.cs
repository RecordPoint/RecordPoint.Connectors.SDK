namespace RecordPoint.Connectors.SDK.Work
{
    /// <summary>
    /// Exception thrown when a deferred work message fails to be re-queued
    /// </summary>
    public class DeferredMessageRequeueFailedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeferredMessageRequeueFailedException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public DeferredMessageRequeueFailedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
