using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RecordPoint.Connectors.SDK.Content
{
    public static class DatabaseChannelManagerBuilderExtensions
    {
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
