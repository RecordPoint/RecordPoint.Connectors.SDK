namespace RecordPoint.Connectors.SDK.Content
{
    /// <summary>
    /// Lightweight channel projection for classification and discovery lookups.
    /// </summary>
    public sealed class ChannelClassificationModel
    {
        /// <summary>
        /// External ID that uniquely identifies the channel for a connector instance.
        /// </summary>
        public string? ExternalId { get; set; }

        /// <summary>
        /// Serialized channel metadata.
        /// </summary>
        public string? MetaData { get; set; }
    }
}
