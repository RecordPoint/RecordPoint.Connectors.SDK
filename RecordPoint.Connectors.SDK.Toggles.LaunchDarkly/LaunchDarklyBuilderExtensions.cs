using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RecordPoint.Connectors.SDK.Toggles.LaunchDarkly;

/// <summary>
/// Launch darkly host builder extensions
/// </summary>
public static class LaunchDarklyBuilderExtensions
{
    /// <summary>
    /// Configure the host to use the launch darkly toggle provider
    /// </summary>
    /// <param name="hostBuilder">Host builder to update</param>
    /// <returns>Updated host builder</returns>
    public static IHostBuilder UseLaunchDarklyToggles(this IHostBuilder hostBuilder)
    {
        return hostBuilder
            .ConfigureServices((hostContext, serviceCollection) =>
            {
                var launchDarklyConfigSection = hostContext.Configuration.GetSection(LaunchDarklyOptions.SECTION_NAME);

                if (launchDarklyConfigSection == null)
                {
                    throw new RequiredValueNullException(nameof(launchDarklyConfigSection));
                }
                var launchDarklyOptions = launchDarklyConfigSection.Get<LaunchDarklyOptions>();
                if (launchDarklyOptions == null)
                    return;

                serviceCollection
                    .Configure<LaunchDarklyOptions>(launchDarklyConfigSection)
                    .AddSingleton<IToggleProvider, LaunchDarklyToggleProvider>();
            });
    }
}
