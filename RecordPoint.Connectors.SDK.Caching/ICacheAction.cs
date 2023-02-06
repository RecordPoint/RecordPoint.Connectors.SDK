namespace RecordPoint.Connectors.SDK.Caching
{
    public interface ICacheAction<TCacheItemType>
    {
        Task<CacheActionResult<TCacheItemType>> ExecuteAsync(CacheActionContext context);
    }
}
