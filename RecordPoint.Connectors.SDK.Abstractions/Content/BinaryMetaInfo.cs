namespace RecordPoint.Connectors.SDK.Content
{
    /// <summary>
    /// Represents meta information about a binary
    /// </summary>
    public sealed class BinaryMetaInfo : ContentItem, IEquatable<BinaryMetaInfo>
    {
        /// <summary>
        /// The maximum length of a Item External Id + Binary External Id is limited to 974 characters due to Azure Blob Storage filename limitations.
        /// The Full binary storage path is based on {ConnectorId}.{ItemExternalId}\{ExternalId}{.cracked.txt}
        /// ConnectorId is a GUID (36 characters) plus the separater (.) and ItemExternalId, followed by the path separator (\)
        /// We also need to account for the cracked document suffix (.cracked.txt) which is 12 characters long, resulting in a total of 50 characters that are not usable.
        /// Resulting in a maximum available length of 974 characters for both the ItemExternalId and the Binary External Id.
        /// </summary>
        private const int MaxBinaryExternalIdLength = 974;

        private string? externalId = string.Empty;
        private string? itemExternalId = string.Empty;

        /// <summary>
        /// Gets or sets the external identifier associated with this entity.
        /// </summary>
        /// <remarks>
        /// This overrides the property from the ContentItem base to provide maximum length validation</remarks>
        public new string? ExternalId { 
            get => externalId; 
            set {
                ValidateBinaryStorageFilenameLength(ItemExternalId, value);
                externalId = value;
            }
        }

        /// <summary>
        /// External ID of the record this binary is associated with
        /// </summary>
        public string? ItemExternalId { 
            get => itemExternalId;
            set
            {
                ValidateBinaryStorageFilenameLength(value, ExternalId);
                itemExternalId = value;
            }
        }

        private static void ValidateBinaryStorageFilenameLength(string? itemExternalId, string? externalId)
        {
            var combinedLength = (itemExternalId?.Length ?? 0) + (externalId?.Length ?? 0);
            if (combinedLength > MaxBinaryExternalIdLength)
                throw new ArgumentOutOfRangeException($"ItemExternalId + ExternalId cannot exceed {MaxBinaryExternalIdLength} characters in length.");
        }

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
        /// Indicates if the binary has been succesfully submitted to the platform or should be skipped
        /// </summary>
        public BinarySubmissionStatus BinarySubmissionStatus { get; set; } = BinarySubmissionStatus.NotSubmitted;

        /// <summary>
        /// The number of times the binary submission has been retried
        /// </summary>
        public int SubmissionAttempts { get; set; } = 0;

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