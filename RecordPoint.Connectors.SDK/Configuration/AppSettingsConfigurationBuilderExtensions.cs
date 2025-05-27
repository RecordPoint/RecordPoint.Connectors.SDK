using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RecordPoint.Connectors.SDK.Configuration
{
    /// <summary>
    /// Extension to configure R365 Settings from AppSettings container
    /// </summary>
    public static class AppSettingsConfigurationBuilderExtensions
    {

        /// <summary>
        /// Configure the host to use the app settings configurator
        /// </summary>
        /// <param name="hostBuilder">Host builder to configure</param>
        /// <returns>Updated host builder</returns>
        public static IHostBuilder UseR365AppSettingsConfiguration(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((hostContext, services) =>
            {
                services
                    .Configure<R365ConfigurationModel>(hostContext.Configuration.GetSection("Configuration"))
                    .Configure<R365MultiConfigurationModel>(hostContext.Configuration.GetSection("MultiConfiguration"))
                    .AddSingleton<IR365ConfigurationClient, AppSettingsR365ConfigurationClient>();
            });
            return hostBuilder;
        }

    }
}
