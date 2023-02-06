namespace RecordPoint.Connectors.SDK.Health
{
    [Serializable]
    public class HealthCheckFailedException : Exception
    {
        public HealthCheckFailedException() { }
        public HealthCheckFailedException(string message) : base(message) { }
        public HealthCheckFailedException(string message, Exception inner) : base(message, inner) { }
        protected HealthCheckFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
