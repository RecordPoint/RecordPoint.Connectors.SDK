using System.Runtime.Serialization;

namespace RecordPoint.Connectors.SDK.Observability
{
    [Serializable]
    public class Dimensions : Dictionary<string, string?>
    {
        public Dimensions() { }

        public Dimensions(IEnumerable<KeyValuePair<string, string?>> collection) : base(collection) { }

        protected Dimensions(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// Convert dimensions to log scope state suitable for being passed to a logging BeingScope
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object?> ToLogState()
        {
            return this.AsEnumerable()
                .ToDictionary(kp => kp.Key, kp => (object?)kp.Value);
        }
    }
}
