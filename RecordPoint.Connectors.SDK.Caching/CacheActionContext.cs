namespace RecordPoint.Connectors.SDK.Caching
{
    /// <summary>
    /// The cache action context.
    /// </summary>
    public class CacheActionContext
    {
        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        public Dictionary<string, object> Properties { get; set; } = new();
    }
}
