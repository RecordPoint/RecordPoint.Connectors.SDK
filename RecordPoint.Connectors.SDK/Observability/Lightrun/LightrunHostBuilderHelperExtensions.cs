using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RecordPoint.Connectors.SDK.Observability.Lightrun;

/// <summary>
/// Host Builder Extensions for registering the Lightrun Agent
/// </summary>
public static class LightrunHostBuilderHelperExtensions
{
    /// <summary>
    /// Registers the Lightrun Agent
    /// </summary>
    public static IHostBuilder UseLightrunAgent(this IHostBuilder hostBuilder)
    {
        return hostBuilder.ConfigureServices((context, services) => {
            var configSection = context.Configuration.GetSection(LightrunOptions.SECTION_NAME);

            services
                .AddHostedService<LightrunAgentService>()
                .AddOptions<LightrunOptions>()
                .Bind(configSection);
        });
    }
}
