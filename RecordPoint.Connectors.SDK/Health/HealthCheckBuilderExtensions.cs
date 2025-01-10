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
        /// Use the health checker with default health check actions.
        /// </summary>
        /// <param name="hostBuilder">Host builder to target</param>
        public static IHostBuilder UseHealthChecker(this IHostBuilder hostBuilder)
        {
            return hostBuilder.UseHealthChecker<DefaultHealthCheckLiveAction, DefaultHealthCheckReadyAction>();
        }

        /// <summary>
        /// Use the health checker with provided health check actions.
        /// </summary>
        /// <param name="hostBuilder">Host builder to target</param>
        public static IHostBuilder UseHealthChecker<THealthCheckLiveAction, THealthCheckReadyAction>(this IHostBuilder hostBuilder)
            where THealthCheckLiveAction : class, IHealthCheckLiveAction
            where THealthCheckReadyAction : class, IHealthCheckReadyAction
        {
            return hostBuilder.ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;
                services
                    .AddHostedService<HealthCheckService>()
                    .Configure<HealthCheckOptions>(configuration.GetSection(HealthCheckOptions.SECTION_NAME))
                    .AddSingleton<IHealthCheckManager, HealthCheckManager>()
                    .AddSingleton<IHealthCheckStrategy, UptimeStrategy>()
                    .AddSingleton<IHealthCheckLiveAction, THealthCheckLiveAction>()
                    .AddSingleton<IHealthCheckReadyAction, THealthCheckReadyAction>();
            });
        }
    }
}
