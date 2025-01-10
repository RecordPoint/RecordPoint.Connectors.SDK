namespace RecordPoint.Connectors.SDK.Caching
{
    /// <summary>
    /// The cache action result.
    /// </summary>
    /// <typeparam name="TCacheItemType"/>
    public class CacheActionResult<TCacheItemType>
    {
        /// <summary>
        /// Gets or sets the cache item.
        /// </summary>
        public TCacheItemType? CacheItem { get; set; }
        /// <summary>
        /// Gets or sets the expires.
        /// </summary>
        public DateTimeOffset? Expires { get; set; }
    }
}
