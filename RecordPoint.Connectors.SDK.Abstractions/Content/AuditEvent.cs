namespace RecordPoint.Connectors.SDK.Content
{
    public sealed class AuditEvent : IEquatable<AuditEvent>
    {
        public string EventExternalId { get; set; } = string.Empty;
        public string ExternalId { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public string EventType { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Additional source fields
        /// </summary>
        public List<MetaDataItem> MetaDataItems { get; set; } = new List<MetaDataItem>();

        public bool Equals(AuditEvent? other)
        {
            if (other == null) return false;

            return EventExternalId == other.EventExternalId
                && ExternalId == other.ExternalId
                && CreatedDate == other.CreatedDate
                && Description == other.Description
                && EventType == other.EventType
                && UserName == other.UserName
                && UserId == other.UserId
                && MetaDataItems.IsEqual(other.MetaDataItems);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (obj is not AuditEvent other)
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
