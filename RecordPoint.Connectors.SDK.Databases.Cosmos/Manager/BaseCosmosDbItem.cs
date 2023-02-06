using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos.Manager;

public class BaseCosmosDbItem
{
    [JsonConverter(typeof(UnixDateTimeConverter))]
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "_ts")]
    public DateTime? LastModifiedCosmosTimeStamp { get; set; }

    [JsonProperty("id")] 
    public string Id { get; set; } = default!;

    [JsonProperty("ttl")] 
    public int Ttl { get; set; } = -1;

    [JsonProperty("_etag")]
    public string? ETag { get; set; }
}