namespace RecordPoint.Connectors.SDK.Caching
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCacheItemType"></typeparam>
    public interface ICache<TCacheItemType>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<TCacheItemType?> GetAsync(string key, CacheActionContext context);
    }
}
