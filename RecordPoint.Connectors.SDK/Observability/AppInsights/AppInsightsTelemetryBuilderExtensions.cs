using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RecordPoint.Connectors.SDK.Observability.AppInsights;

/// <summary>
/// Host builder extensions for the log based telemetry
/// </summary>
public static class AppInsightsTelemetryBuilderExtensions
{
    /// <summary>
    /// Registers Application Insights Telemetry tracking services
    /// </summary>
    public static IHostBuilder UseAppInsightsTelemetryTracking(this IHostBuilder hostBuilder)
    {
        return hostBuilder
            .UseObservabilityTracking()
            .ConfigureServices((hostContext, services) =>
            {
                var appInsightsOptionsSection = hostContext.Configuration.GetSection(ApplicationInsightOptions.OPTION_NAME);
                var appInsightsOptions = appInsightsOptionsSection.Get<ApplicationInsightOptions>() ?? new();

                services
                    .Configure<ApplicationInsightOptions>(appInsightsOptionsSection)
                    .AddSingleton<ITelemetryClientFactory, TelemetryClientFactory>()
                    .AddSingleton<ITelemetrySink, ApplicationInsightsTelemetrySink>();

                if (appInsightsOptions.IncludeKubernetesEnricher)
                    services.AddApplicationInsightsKubernetesEnricher();
            });
    }
}
