using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RecordPoint.Connectors.SDK.Connectors
{
    /// <summary>
    /// Host builder extensions for the connectors service
    /// </summary>
    public static class ConnectorConfigurationBuilderExtensions
    {
        /// <summary>
        /// Use the connector management services
        /// </summary>
        /// <param name="hostBuilder">Host builder to configure</param>
        /// <returns>Updated host builder</returns>
        public static IHostBuilder UseDatabaseConnectorConfigurationManager(this IHostBuilder hostBuilder)
        {
            hostBuilder
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = hostContext.Configuration;
                    services
                        .Configure<ConnectorOptions>(configuration.GetSection(ConnectorOptions.SECTION_NAME))
                        .AddSingleton<IConnectorConfigurationManager, DatabaseConnectorConfigurationManager>();
                });
            return hostBuilder;
        }
    }
}