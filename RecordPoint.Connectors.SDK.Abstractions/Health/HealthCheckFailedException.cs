namespace RecordPoint.Connectors.SDK.Health
{
    /// <summary>
    /// The health check failed exception.
    /// </summary>
    public class HealthCheckFailedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HealthCheckFailedException"/> class.
        /// </summary>
        public HealthCheckFailedException() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="HealthCheckFailedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public HealthCheckFailedException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="HealthCheckFailedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public HealthCheckFailedException(string message, Exception inner) : base(message, inner) { }
    }
}