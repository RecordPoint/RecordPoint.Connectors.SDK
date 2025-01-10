using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RecordPoint.Connectors.SDK.Caching.Semaphore
{
    /// <summary>
    /// The in memory semaphore lock builder extensions.
    /// </summary>
    public static class InMemorySemaphoreLockBuilderExtensions
    {
        /// <summary>
        /// Use in memory semaphore lock.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <returns>An IHostBuilder</returns>
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

        /// <summary>
        /// Use in memory semaphore lock.
        /// </summary>
        /// <typeparam name="TSemaphoreLockScopedKeyAction"/>
        /// <param name="hostBuilder">The host builder.</param>
        /// <returns>An IHostBuilder</returns>
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
