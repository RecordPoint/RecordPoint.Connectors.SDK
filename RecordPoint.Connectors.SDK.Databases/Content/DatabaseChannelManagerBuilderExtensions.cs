using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Caching;
using RecordPoint.Connectors.SDK.Databases.Caching;

namespace RecordPoint.Connectors.SDK.Content
{
    /// <summary>
    /// The database channel manager builder extensions.
    /// </summary>
    public static class DatabaseChannelManagerBuilderExtensions
    {
        /// <summary>
        /// Use database channel manager.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <returns>An IHostBuilder</returns>
        public static IHostBuilder UseDatabaseChannelManager(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseInMemoryCache<ChannelExistsCacheAction, bool>();

            return hostBuilder.ConfigureServices((hostContext, services) =>
            {
                // TryAdd so that if the Cosmos layer already registered CosmosDirectChannelAccess,
                // this no-op default doesn't overwrite it regardless of registration order.
                services.TryAddSingleton<IDirectChannelAccess, NullDirectChannelAccess>();
                services.AddSingleton<IChannelManager, DatabaseChannelManager>();
            });
        }
    }
}
