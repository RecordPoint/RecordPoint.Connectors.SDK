namespace RecordPoint.Connectors.SDK.Caching
{
    public class CacheActionResult<TCacheItemType>
    {
        public TCacheItemType? CacheItem { get; set; }
        public DateTimeOffset? Expires { get; set; }
    }
}
