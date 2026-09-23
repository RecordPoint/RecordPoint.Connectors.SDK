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
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TCacheItemType?> GetAsync(string key, CacheActionContext context, CancellationToken cancellationToken = (default));

        /// <summary>
        /// Removes the cache entry for the specified key.
        /// </summary>
        /// <param name="key">The cache key to invalidate.</param>
        void Invalidate(string key);
    }
}
