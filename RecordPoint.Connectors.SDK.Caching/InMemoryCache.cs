using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace RecordPoint.Connectors.SDK.Caching
{
    /// <summary>
    /// The in memory cache.
    /// </summary>
    /// <typeparam name="TCacheItemType"/>
    public class InMemoryCache<TCacheItemType> : ICache<TCacheItemType>
    {
        /// <summary>
        /// The service provider.
        /// </summary>
        private readonly IServiceProvider _serviceProvider;
        /// <summary>
        /// The memory cache.
        /// </summary>
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="memoryCache">The memory cache.</param>
        public InMemoryCache(IServiceProvider serviceProvider, IMemoryCache memoryCache)
        {
            _serviceProvider = serviceProvider;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Get and return a task of type tcacheitemtype? asynchronously.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="context">The context.</param>
        /// <returns><![CDATA[Task<TCacheItemType?>]]></returns>
        public async Task<TCacheItemType?> GetAsync(string key, CacheActionContext context)
        {
            if (!_memoryCache.TryGetValue(key, out TCacheItemType? cacheItem))
            {
                var cacheAction = _serviceProvider.GetRequiredService<ICacheAction<TCacheItemType>>();
                var cacheActionResult = await cacheAction.ExecuteAsync(context);
                if (cacheActionResult.CacheItem != null)
                {
                    if (cacheActionResult.Expires.HasValue)
                    {
                        _memoryCache.Set(key, cacheActionResult.CacheItem, cacheActionResult.Expires.Value);
                    }
                    else
                    {
                        _memoryCache.Set(key, cacheActionResult.CacheItem);
                    }

                    cacheItem = cacheActionResult.CacheItem;
                }
            }

            return cacheItem;
        }
    }
}
