namespace RecordPoint.Connectors.SDK.Content
{
    /// <summary>
    /// Aggreation
    /// </summary>
    public sealed class AggregationModel : IEquatable<AggregationModel>
    {
        /// <summary>
        /// Connector Configuration that this Channel belongs to
        /// </summary>
        public string? ConnectorId { get; set; }

        /// <summary>
        /// External ID that uniquely identifies the Channel for a connector instance
        /// </summary>
        public string? ExternalId { get; set; }

        /// <summary>
        /// Channel title
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Serialized Channel Meta Data
        /// </summary>
        public string? MetaData { get; set; }

        /// <summary>
        /// The date/time the Channel was created
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// Aggregation location
        /// </summary>
        public string Location { get; set; } = string.Empty;

        public bool Equals(AggregationModel? other)
        {
            if (other == null)
                return false;

            return Title == other.Title
                 && ExternalId == other.ExternalId
                 && ConnectorId == other.ConnectorId
                 && MetaData == other.MetaData;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (obj is not AggregationModel other)
                return false;
            else
                return Equals(other);
        }

        public override int GetHashCode()
        {
            if (ExternalId == null) return 0;
            return ExternalId.GetHashCode();
        }
    }
}