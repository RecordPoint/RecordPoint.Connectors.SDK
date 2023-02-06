using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RecordPoint.Connectors.SDK.Observability.Null
{

    /// <summary>
    /// Builder extensions used to setup the null telemetry tracker
    /// </summary>
    public static class NullTelemetryBuilderExtensions
    {

        /// <summary>
        /// Configure the host to use the null telemetry tracking
        /// </summary>
        /// <param name="hostBuilder">Host builder to configure</param>
        /// <returns>Updated host builder</returns>
        public static IHostBuilder UseNullTelemetryTracking(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<IScopeManager, ScopeManager>();
                services.AddSingleton<ITelemetryTracker, NullTelemetryTracker>();
            });
            return hostBuilder;
        }

    }
}
