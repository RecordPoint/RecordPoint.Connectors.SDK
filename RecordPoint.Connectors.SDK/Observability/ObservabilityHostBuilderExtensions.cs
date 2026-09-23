using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RecordPoint.Connectors.SDK.Observability;

/// <summary>
/// Host builder extensions for Observability
/// </summary>
public static class ObservabilityHostBuilderExtensions
{
    /// <summary>
    /// Configure the host for observability tracking
    /// </summary>
    /// <param name="hostBuilder">Host builder to configure</param>
    /// <returns>Updated host builder</returns>
    public static IHostBuilder UseObservabilityTracking(this IHostBuilder hostBuilder)
    {
        return hostBuilder.ConfigureServices((hostContext, services) =>
        {
            var loggingOptionsSection = hostContext.Configuration.GetSection(LoggingOptions.SECTION_NAME);

            services
                .Configure<LoggingOptions>(loggingOptionsSection)
                .AddSingleton<IObservabilityScope, ObservabilityScope>()
                .AddSingleton<ITelemetryTracker, TelemetryTracker>();
        });
    }
}
