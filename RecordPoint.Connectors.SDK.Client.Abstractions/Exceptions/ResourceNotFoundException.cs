﻿namespace RecordPoint.Connectors.SDK.Exceptions
{
    /// <summary>
    /// Thrown by the SDK when trying to access an API resource that does not exist.
    /// For example, when trying to acknowledge a connector notification that has already
    /// been acknowledged.
    /// </summary>
    public class ResourceNotFoundException : Exception
    {
        /// <summary>
        /// Constructs a new instance of ResourceNotFoundException.
        /// </summary>
        public ResourceNotFoundException()
        {
        }

        /// <summary>
        /// Constructs a new instance of ResourceNotFoundException with an exception message.
        /// </summary>
        /// <param name="message"></param>
        public ResourceNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructs a new instance of ResourceNotFoundException with an exception message and an inner exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ResourceNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
