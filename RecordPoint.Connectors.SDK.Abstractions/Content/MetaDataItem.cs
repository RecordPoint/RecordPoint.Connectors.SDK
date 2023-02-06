namespace RecordPoint.Connectors.SDK.Content
{
    public sealed class MetaDataItem : IEquatable<MetaDataItem>
    {
        public string Name { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        public MetaDataItemType MetaDataItemType { get; set; } = MetaDataItemType.R365MetaData;

        public bool Equals(MetaDataItem? other)
        {
            if (other == null) return false;

            return Name == other.Name
                && Type == other.Type
                && Value == other.Value;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (obj is not MetaDataItem other)
                return false;
            else
                return Equals(other);
        }

        public override int GetHashCode()
        {
            if (Name == null) return 0;
            return Name.GetHashCode();
        }
    }
}