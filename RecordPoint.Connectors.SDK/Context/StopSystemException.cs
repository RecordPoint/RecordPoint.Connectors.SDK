using System;
using System.Runtime.Serialization;

namespace RecordPoint.Connectors.SDK.Context
{
    /// <summary>
    /// Exception that indicates that the system was requested to stop
    /// </summary>
    [Serializable]
    public class StopSystemException : Exception
    {
        public StopSystemException()
        {
        }

        public StopSystemException(string message) : base(message)
        {
        }

        public StopSystemException(string message, Exception inner) : base(message, inner)
        {
        }

        protected StopSystemException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}