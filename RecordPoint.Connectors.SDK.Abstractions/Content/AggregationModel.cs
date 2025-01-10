namespace RecordPoint.Connectors.SDK.Content
{
    /// <summary>
    /// Aggreation
    /// </summary>
    public sealed class AggregationModel : IEquatable<AggregationModel>
    {
        /// <summary>
        /// Connector Configuration that this Aggregation belongs to
        /// </summary>
        public string? ConnectorId { get; set; }

        /// <summary>
        /// External ID that uniquely identifies the Aggregation for a connector instance
        /// </summary>
        public string? ExternalId { get; set; }

        /// <summary>
        /// Parent External ID that uniquely identifies the Parent Aggregation
        /// </summary>
        public string? ParentExternalId { get; set; }

        /// <summary>
        /// Aggregation title
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Serialized Aggregation Meta Data
        /// </summary>
        public string? MetaData { get; set; }

        /// <summary>
        /// The date/time the Aggregation was created
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// Aggregation location
        /// </summary>
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(AggregationModel? other)
        {
            if (other == null)
                return false;

            return Title == other.Title
                 && ExternalId == other.ExternalId
                 && ParentExternalId == other.ParentExternalId
                 && ConnectorId == other.ConnectorId
                 && MetaData == other.MetaData;
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

            if (obj is not AggregationModel other)
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