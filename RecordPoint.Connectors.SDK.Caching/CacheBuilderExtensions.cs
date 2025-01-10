using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RecordPoint.Connectors.SDK.Caching
{
    /// <summary>
    /// The cache builder extensions.
    /// </summary>
    public static class CacheBuilderExtensions
    {
        /// <summary>
        /// Use in memory cache.
        /// </summary>
        /// <typeparam name="TCacheAction"/>
        /// <typeparam name="TCacheItemType"/>
        /// <param name="hostBuilder">The host builder.</param>
        /// <returns>An IHostBuilder</returns>
        public static IHostBuilder UseInMemoryCache<TCacheAction, TCacheItemType>(this IHostBuilder hostBuilder)
            where TCacheAction : class, ICacheAction<TCacheItemType>
        {
            return hostBuilder.ConfigureServices((hostContext, services) =>
            {
                services
                    .AddMemoryCache()
                    .AddTransient<ICacheAction<TCacheItemType>, TCacheAction>()
                    .AddSingleton<ICache<TCacheItemType>, InMemoryCache<TCacheItemType>>();
            });
        }
    }
}
