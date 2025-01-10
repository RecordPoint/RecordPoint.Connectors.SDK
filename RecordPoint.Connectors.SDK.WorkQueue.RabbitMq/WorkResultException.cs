namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq
{
    /// <summary>
    /// The work result exception.
    /// </summary>
    public class WorkResultException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkResultException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public WorkResultException(string? message) : base(message)
        {

        }
    }
}
