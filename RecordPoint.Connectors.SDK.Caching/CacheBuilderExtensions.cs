using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RecordPoint.Connectors.SDK.Caching
{
    public static class CacheBuilderExtensions
    {
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
