using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace RecordPoint.Connectors.SDK.Caching
{
    public class InMemoryCache<TCacheItemType> : ICache<TCacheItemType>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMemoryCache _memoryCache;

        public InMemoryCache(IServiceProvider serviceProvider, IMemoryCache memoryCache)
        {
            _serviceProvider = serviceProvider;
            _memoryCache = memoryCache;
        }

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
