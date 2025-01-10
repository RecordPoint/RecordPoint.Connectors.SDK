namespace RecordPoint.Connectors.SDK.Content
{
    /// <summary>
    /// Represents meta information about a binary
    /// </summary>
    public sealed class BinaryMetaInfo : ContentItem, IEquatable<BinaryMetaInfo>
    {
        /// <summary>
        /// External ID of the record this binary is associated with
        /// </summary>
        public string ItemExternalId { get; set; } = string.Empty;

        /// <summary>
        /// String that uniquely identifies how the content token is formatted.
        /// Provided and used by content modules and typcially needed to support backwards
        /// compatibility.
        /// </summary>
        public string ContentTokenType { get; set; } = string.Empty;

        /// <summary>
        /// Token that can be used by the content module to locate the content
        /// </summary>
        public string ContentToken { get; set; } = string.Empty;

        /// <summary>
        /// Mime Type of the content
        /// </summary>
        public string MimeType { get; set; } = string.Empty;

        /// <summary>
        /// Optional MD5 file hash is available
        /// </summary>
        public string FileHash { get; set; } = string.Empty;

        /// <summary>
        /// "File name" for the content.
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// File Size in bytes of the content
        /// </summary>
        public long? FileSize { get; set; }

        /// <summary>
        /// Flag to skip the enrichment pipeline
        /// </summary>
        public bool? SkipEnrichment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(BinaryMetaInfo? other)
        {
            if (other == null) return false;

            return base.Equals(other)
                && ItemExternalId == other.ItemExternalId
                && ContentToken == other.ContentToken
                && ContentTokenType == other.ContentTokenType
                && MimeType == other.MimeType
                && FileHash == other.FileHash
                && FileName == other.FileName
                && FileSize == other.FileSize;
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

            if (obj is not BinaryMetaInfo other)
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