using System.Runtime.Serialization;

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

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("WaitUntilTime", WaitUntilTime, typeof(DateTime));
        }
    }
}
