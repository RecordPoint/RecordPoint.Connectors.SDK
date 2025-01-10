using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RecordPoint.Connectors.SDK.Content
{
    /// <summary>
    /// The database aggregation manager builder extensions.
    /// </summary>
    public static class DatabaseAggregationManagerBuilderExtensions
    {
        /// <summary>
        /// Use database aggregation manager.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <returns>An IHostBuilder</returns>
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
