using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RecordPoint.Connectors.SDK.Toggles.LaunchDarkly
{
    /// <summary>
    /// Launch darkly host builder extensions
    /// </summary>
    public static class LaunchDarklyBuilderExtensions
    {

        ///// <summary>
        ///// Configure the host to use the launch darkly toggle provider
        ///// </summary>
        ///// <param name="hostBuilder">Host builder to update</param>
        ///// <returns>Updated host builder</returns>
        public static IHostBuilder UseLaunchDarklyToggles(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((hostContext, serviceCollection) =>
            {
                var configurationLDarklySection = hostContext.Configuration.GetSection(LaunchDarklyOptions.SECTION_NAME);
                serviceCollection.Configure<LaunchDarklyOptions>(configurationLDarklySection);

                serviceCollection.AddSingleton<IToggleProvider, LaunchDarklyToggleProvider>();
            });

            return hostBuilder;
        }

    }
}
