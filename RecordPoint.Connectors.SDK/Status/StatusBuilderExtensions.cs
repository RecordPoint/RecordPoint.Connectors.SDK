using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Health;

namespace RecordPoint.Connectors.SDK.Status
{
    public static class StatusBuilderExtensions
    {
        /// <summary>
        /// Configure the host to use polled notifications
        /// </summary>
        /// <param name="hostBuilder">Host builder to configure</param>
        /// <returns>Configured host builder</returns>
        public static IHostBuilder UseStatusManager(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(svcs => svcs
                .AddSingleton<IStatusManager, StatusManager>()
                .AddSingleton<IHealthCheckStrategy, StatusHealthChecker>()
                .AddSingleton<IStatusStrategy, ConnectorStatusStrategy>()
            );
        }
    }
}
