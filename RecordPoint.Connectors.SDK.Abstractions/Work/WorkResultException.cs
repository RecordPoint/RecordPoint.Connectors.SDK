﻿namespace RecordPoint.Connectors.SDK.Abstractions.Work
{
    /// <summary>
    /// Represents errors that occur during work result processing.
    /// </summary>
    public class WorkResultException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkResultException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public WorkResultException(string? message) : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkResultException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public WorkResultException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
