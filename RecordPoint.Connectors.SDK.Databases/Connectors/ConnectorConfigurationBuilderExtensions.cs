using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Caching;
using RecordPoint.Connectors.SDK.Client.Models;

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
                .UseInMemoryCache<ConnectorConfigurationCacheAction, ConnectorConfigurationModel>()
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