using System;

namespace RecordPoint.Connectors.SDK.Context
{
    /// <summary>
    /// The stop system exception.
    /// </summary>
    public class StopSystemException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StopSystemException"/> class.
        /// </summary>
        public StopSystemException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StopSystemException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public StopSystemException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StopSystemException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public StopSystemException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}