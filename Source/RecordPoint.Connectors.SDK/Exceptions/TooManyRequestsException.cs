using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class TooManyRequestsException : Exception
    {
        public DateTime WaitUntilTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="time"></param>
        public TooManyRequestsException(string message, DateTime time) : base(message)
        {
            WaitUntilTime = time;
        }

        /// <summary>
        /// Constructs a new instance of TooManyRequestsException from a serialization context.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected TooManyRequestsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
