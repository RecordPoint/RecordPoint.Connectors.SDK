using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles
{
    /// <summary>
    /// This file just contains the extension method to add the LocalFileToggleProvider to the host builder.
    /// </summary>
    public static class LocalFeatureToggleHostBuilderExtensions
    {
        /// <summary>
        /// Use the LocalFileToggleProvider as the IToggleProvider implementation. i.e. Use a local json file to determine the value of toggles.
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <returns></returns>
        public static IHostBuilder UseLocalFileToggleProvider(this IHostBuilder hostBuilder)
        {
            return hostBuilder
                .ConfigureServices((hostContext, serviceCollection) =>
                {

                    serviceCollection
                        .Configure<LocalFeatureToggleOptions>(
                            hostContext.Configuration.GetSection(LocalFeatureToggleOptions.SECTION_NAME))
                        .AddSingleton<IToggleProvider, LocalFileToggleProvider>()
                        .AddSingleton<IFileReader, FileReader>();
                });
        }
    }
}
