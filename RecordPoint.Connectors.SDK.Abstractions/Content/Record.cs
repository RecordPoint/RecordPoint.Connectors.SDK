namespace RecordPoint.Connectors.SDK.Content
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Record : ContentItem, IEquatable<Record>
    {
        /// <summary>
        /// 
        /// </summary>
        public string? ParentExternalId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? MimeType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? MediaType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? FileSize { get; set; }

        /// <summary>
        /// Associated binaries
        /// </summary>
        public List<BinaryMetaInfo> Binaries { get; set; } = new List<BinaryMetaInfo>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Record? other)
        {
            if (other == null) return false;

            return base.Equals(other)
                && ParentExternalId == other.ParentExternalId
                && Location == other.Location
                && MimeType == other.MimeType
                && MediaType == other.MediaType
                && FileSize == other.FileSize
                && Binaries.IsEqual(other.Binaries);
        }

        /// <summary>
        /// 
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (obj is not Record other)
                return false;
            else
                return Equals(other);
        }

        /// <summary>
        /// 
        /// </summary>
        public override int GetHashCode()
        {
            if (ExternalId == null) return 0;
            return ExternalId.GetHashCode();
        }
    }
}