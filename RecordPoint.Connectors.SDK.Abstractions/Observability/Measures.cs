using System.Runtime.Serialization;

namespace RecordPoint.Connectors.SDK.Observability
{
    [Serializable]
    public class Measures : Dictionary<string, double>
    {
        public Measures() { }

        public Measures(IEnumerable<KeyValuePair<string, double>> collection) : base(collection) { }

        protected Measures(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
