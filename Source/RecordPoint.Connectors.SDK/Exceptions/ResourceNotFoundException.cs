using System;
using System.Runtime.Serialization;

namespace RecordPoint.Connectors.SDK.Exceptions
{
    /// <summary>
    /// Thrown by the SDK when trying to access an API resource that does not exist.
    /// For example, when trying to acknowledge a connector notification that has already
    /// been acknowledged.
    /// </summary>
    [Serializable]
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException()
        {
        }

        public ResourceNotFoundException(string message)
            : base(message)
        {
        }

        public ResourceNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        // Without this constructor, deserialization will fail
        protected ResourceNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
