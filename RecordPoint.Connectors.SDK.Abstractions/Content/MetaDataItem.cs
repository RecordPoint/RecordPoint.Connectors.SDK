namespace RecordPoint.Connectors.SDK.Content
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class MetaDataItem : IEquatable<MetaDataItem>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public MetaDataItemType MetaDataItemType { get; set; } = MetaDataItemType.R365MetaData;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(MetaDataItem? other)
        {
            if (other == null) return false;

            return Name == other.Name
                && Type == other.Type
                && Value == other.Value;
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

            if (obj is not MetaDataItem other)
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
            if (Name == null) return 0;
            return Name.GetHashCode();
        }
    }
}