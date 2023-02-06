using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RecordPoint.Connectors.SDK.Content
{
    public static class DatabaseAggregationManagerBuilderExtensions
    {
        public static IHostBuilder UseDatabaseAggregationManager(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((hostContext, services) =>
            {
                services
                    .AddSingleton<IAggregationManager, DatabaseAggregationManager>();
            });
        }
    }
}
