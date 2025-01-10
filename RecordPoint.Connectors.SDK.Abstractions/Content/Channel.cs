namespace RecordPoint.Connectors.SDK.Content
{
    /// <summary>
    /// Channel meta data
    /// </summary>
    public sealed class Channel : IEquatable<Channel>
    {
        /// <summary>
        /// Null Channel ID
        /// </summary>
        public const string NULL_CHANNEL_ID = "NULL";

        /// <summary>
        /// Null Channel Title
        /// </summary>
        public const string NULL_CHANNEL_TITLE = "Null";

        /// <summary>
        /// External ID that uniquely identifies the Channel for a connector instance
        /// </summary>
        public string? ExternalId { get; set; }

        /// <summary>
        /// Channel title
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Channel Metadata
        /// </summary>
        public List<MetaDataItem> MetaDataItems { get; set; } = new();

        /// <summary>
        /// Get the null Channel. 
        /// </summary>
        /// <remarks>
        /// The Channel supplied for content sources that don't
        /// have a "Channel" concept.
        /// </remarks>
        /// <returns></returns>
        public static Channel CreateNullChannel()
        {
            return new Channel()
            {
                ExternalId = NULL_CHANNEL_ID,
                Title = NULL_CHANNEL_TITLE
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Channel? other)
        {
            if (other == null)
                return false;

            return Title == other.Title
                && ExternalId == other.ExternalId
                && MetaDataItems.IsEqual(other.MetaDataItems);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (obj is not Channel other)
                return false;
            else
                return Equals(other);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if (ExternalId == null) return 0;
            return ExternalId.GetHashCode();
        }
    }
}
