namespace RecordPoint.Connectors.SDK.Content
{
    public sealed class Record : ContentItem, IEquatable<Record>
    {
        public string? ParentExternalId { get; set; }

        public string? Location { get; set; }

        public string? MimeType { get; set; }

        public string? MediaType { get; set; }

        public long? FileSize { get; set; }

        /// <summary>
        /// Associated binaries
        /// </summary>
        public List<BinaryMetaInfo> Binaries { get; set; } = new List<BinaryMetaInfo>();

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

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (obj is not Record other)
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