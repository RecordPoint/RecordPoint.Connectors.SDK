using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RecordPoint.Connectors.SDK.Toggles.Null
{

    /// <summary>
    /// Null toggle host builder extensions
    /// </summary>
    public static class NullToggleBuilderExtensions
    {

        /// <summary>
        /// Configure the host to use the default toggle provider
        /// </summary>
        /// <param name="hostBuilder">Host builder to update</param>
        /// <returns>Updated host builder</returns>
        public static IHostBuilder UseNullToggleProvider(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((hostContext, serviceCollection) =>
            {
                serviceCollection.AddSingleton<IToggleProvider, NullToggleProvider>();
            });

            return hostBuilder;
        }

    }
}
