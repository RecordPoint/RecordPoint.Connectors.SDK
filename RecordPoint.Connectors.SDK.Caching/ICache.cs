namespace RecordPoint.Connectors.SDK.Caching
{
    public interface ICache<TCacheItemType>
    {
        Task<TCacheItemType?> GetAsync(string key, CacheActionContext context);
    }
}
