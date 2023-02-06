using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RecordPoint.Connectors.SDK.Caching.Semaphore
{
    public static class InMemorySemaphoreLockBuilderExtensions
    {
        public static IHostBuilder UseInMemorySemaphoreLock(this IHostBuilder hostBuilder)
        {
            return hostBuilder
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .AddMemoryCache()
                        .AddSingleton<ISemaphoreLockManager, InMemorySemaphoreLockManager>();
                });
        }

        public static IHostBuilder UseInMemorySemaphoreLock<TSemaphoreLockScopedKeyAction>(this IHostBuilder hostBuilder)
            where TSemaphoreLockScopedKeyAction : class, ISemaphoreLockScopedKeyAction
        {
            return hostBuilder
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .AddMemoryCache()
                        .AddScoped<ISemaphoreLockScopedKeyAction, TSemaphoreLockScopedKeyAction>()
                        .AddSingleton<ISemaphoreLockManager, InMemorySemaphoreLockManager>();
                });
        }
    }
}
