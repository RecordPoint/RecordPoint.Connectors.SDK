using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RecordPoint.Connectors.SDK.Observability.AppInsights
{
    /// <summary>
    /// Host builder extensions for the log based telemetry
    /// </summary>
    public static class AppInsightsTelemetryBuilderExtensions
    {
        /// <summary>
        /// Configure the host to write telemetry to the log
        /// </summary>
        /// <param name="hostBuilder">Host builder to configure</param>
        /// <param name="includeKubernetesEnricher">Whether to include the Kubernetes encricher for Application Insights</param>
        /// <returns>Updated host builder</returns>
        public static IHostBuilder UseAppInsightsTelemetryTracking(this IHostBuilder hostBuilder, bool includeKubernetesEnricher = true)
        {
            hostBuilder.ConfigureServices((hostContext, services) =>
            {
                var appInsightsOptions = hostContext.Configuration.GetSection(ApplicationInsightOptions.OPTION_NAME);

                services
                    .Configure<ApplicationInsightOptions>(appInsightsOptions)
                    .AddSingleton<IScopeManager, ScopeManager>()
                    .AddSingleton<ITelemetryTracker, AppInsightsTelemetryTracker>()
                    .AddLogging(builder =>
                    {
                        builder.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
                        builder.AddApplicationInsights(appInsightsOptions.GetValue<string>("InstrumentationKey"));
                    });

                if (includeKubernetesEnricher)
                {
                    services.AddApplicationInsightsKubernetesEnricher();
                }
            });
            return hostBuilder;
        }
    }
}
