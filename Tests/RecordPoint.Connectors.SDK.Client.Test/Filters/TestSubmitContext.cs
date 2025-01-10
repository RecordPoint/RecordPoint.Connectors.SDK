using Newtonsoft.Json;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using Xunit.Abstractions;

namespace RecordPoint.Connectors.SDK.Client.Test.Filters
{
    public class TestSubmitContext : SubmitContext, IXunitSerializable
    {
        public string Name { get; set; }

        public void Deserialize(IXunitSerializationInfo info)
        {
            JsonConvert.PopulateObject(info.GetValue<string>("Object"), this);
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue("Object", JsonConvert.SerializeObject(this));
        }

        public override string ToString()
        {
            return Name ?? base.ToString();
        }
    }
}
