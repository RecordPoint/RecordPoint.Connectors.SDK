using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos.Manager;

/// <summary>
/// The base cosmos db item.
/// </summary>
public class BaseCosmosDbItem
{
    /// <summary>
    /// Gets or sets the last modified cosmos time stamp.
    /// </summary>
    [JsonConverter(typeof(UnixDateTimeConverter))]
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "_ts")]
    public DateTime? LastModifiedCosmosTimeStamp { get; set; }

    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; } = default!;

    /// <summary>
    /// Gets or sets the ttl.
    /// </summary>
    [JsonProperty("ttl")]
    public int Ttl { get; set; } = -1;

    /// <summary>
    /// Gets or sets the E tag.
    /// </summary>
    [JsonProperty("_etag")]
    public string? ETag { get; set; }
}