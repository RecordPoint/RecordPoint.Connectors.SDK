namespace RecordPoint.Connectors.SDK.Content
{
    /// <summary>
    /// Aggreation
    /// </summary>
    public sealed class Aggregation : ContentItem, IEquatable<Aggregation>
    {
        /// <summary>
        /// Parent External Id of the Aggregation
        /// </summary>
        public string? ParentExternalId { get; set; }

        /// <summary>
        /// Aggregation item type
        /// </summary>
        public const int ItemTypeId = 1;

        /// <summary>
        /// Aggregation location
        /// </summary>
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Aggregation? other)
        {
            if (other == null) return false;

            return base.Equals(other)
                && ExternalId == other.ExternalId
                && Author == other.Author
                && ContentVersion == other.ContentVersion
                && SourceCreatedBy == other.SourceCreatedBy
                && SourceCreatedDate == other.SourceCreatedDate
                && SourceLastModifiedBy == other.SourceLastModifiedBy
                && SourceLastModifiedDate == other.SourceLastModifiedDate
                && Title == other.Title
                && Location == other.Location
                && ParentExternalId == other.ParentExternalId;
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

            if (obj is not Aggregation other)
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