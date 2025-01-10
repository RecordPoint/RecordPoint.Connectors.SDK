using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            return hostBuilder.ConfigureServices((hostContext, services) =>
            {
                services
                    .AddSingleton<IChannelManager, DatabaseChannelManager>();
            });
        }
    }
}
