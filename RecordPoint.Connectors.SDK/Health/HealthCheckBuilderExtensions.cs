using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RecordPoint.Connectors.SDK.Health
{
    /// <summary>
    /// Health check builder extensions
    /// </summary>
    public static class HealthCheckBuilderExtensions
    {
        /// <summary>
        /// Use the health checker
        /// </summary>
        /// <param name="hostBuilder">Host builder to target</param>
        public static IHostBuilder UseHealthChecker(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;
                services
                    .AddHostedService<HealthCheckService>()
                    .Configure<HealthCheckOptions>(configuration.GetSection(HealthCheckOptions.SECTION_NAME))
                    .AddSingleton<IHealthCheckManager, HealthCheckManager>()
                    .AddTransient<HealthCheckOperation>()
                    .AddSingleton<IHealthCheckStrategy, RunHealthChecker>();
            });
        }
    }
}
