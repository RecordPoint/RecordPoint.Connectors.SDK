using System.Diagnostics.CodeAnalysis;

namespace RecordPoint.Connectors.SDK.Content
{
    /// <summary>
    /// Base class for Records 365 Content
    /// </summary>
    public class ContentItem : IEqualityComparer<ContentItem>, IEquatable<ContentItem>
    {
        /// <summary>
        /// External ID that uniquely identifies the item in the content source
        /// </summary>
        public string? ExternalId { get; set; }

        /// <summary>
        /// Item title
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Who last modified the content
        /// </summary>
        public string? SourceLastModifiedBy { get; set; }

        /// <summary>
        /// When the content was last modified
        /// </summary>

        public DateTimeOffset SourceLastModifiedDate { get; set; }

        /// <summary>
        /// Who created by the content
        /// </summary>
        public string? SourceCreatedBy { get; set; }

        /// <summary>
        /// When the content was created
        /// </summary>
        public DateTimeOffset SourceCreatedDate { get; set; }

        /// <summary>
        /// Contents author
        /// </summary>
        public string? Author { get; set; }

        /// <summary>
        /// String that describes the contents version
        /// </summary>
        public string? ContentVersion { get; set; }

        /// <summary>
        /// Meta Data for the Content Item
        /// </summary>
        public List<MetaDataItem> MetaDataItems { get; set; } = new List<MetaDataItem>();

        public bool Equals(ContentItem? other)
        {
            return Equals(this, other);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (obj is not ContentItem other)
                return false;
            else
                return Equals(other);
        }

        public bool Equals(ContentItem? x, ContentItem? y)
        {
            if (x == null && y == null) return true;
            if (x == null) return false;
            if (y == null) return false;

            return x.ExternalId == y.ExternalId
                && x.Title == y.Title
                && x.SourceLastModifiedBy == y.SourceLastModifiedBy
                && x.SourceLastModifiedDate == y.SourceLastModifiedDate
                && x.Author == y.Author
                && x.ContentVersion == y.ContentVersion
                && x.MetaDataItems.IsEqual(y.MetaDataItems);
        }

        public override int GetHashCode()
        {
            if (ExternalId == null) return 0;
            return ExternalId.GetHashCode();
        }

        public int GetHashCode([DisallowNull] ContentItem obj)
        {
            if (obj.ExternalId == null) return 0;
            return obj.ExternalId.GetHashCode();
        }
    }
}