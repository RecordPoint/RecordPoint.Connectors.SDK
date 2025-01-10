namespace RecordPoint.Connectors.SDK.Content
{
    /// <summary>
    /// The audit event.
    /// </summary>
    public sealed class AuditEvent : IEquatable<AuditEvent>
    {
        /// <summary>
        /// Gets or sets the event external id.
        /// </summary>
        public string EventExternalId { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the external id.
        /// </summary>
        public string ExternalId { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the event type.
        /// </summary>
        public string EventType { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Additional source fields
        /// </summary>
        public List<MetaDataItem> MetaDataItems { get; set; } = new List<MetaDataItem>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (obj is not AuditEvent other)
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
