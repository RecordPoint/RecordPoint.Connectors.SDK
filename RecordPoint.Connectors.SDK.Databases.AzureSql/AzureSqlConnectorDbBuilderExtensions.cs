using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RecordPoint.Connectors.SDK.Databases.AzureSql
{
    /// <summary>
    /// AzureSql host builder extensions
    /// </summary>
    public static class AzureSqlConnectorDbBuilderExtensions
    {
        /// <summary>
        /// Use an AzureSql Server as the connector database
        /// </summary>
        /// <param name="hostBuilder">App host builder</param>
        /// <returns>Updated host builder</returns>
        public static IHostBuilder UseAzureSqlConnectorDatabase(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;
                services
                   .AddSingleton<IConnectorDatabaseProvider, AzureSqlConnectorDbProvider>()
                   .AddSingleton<IConnectorDatabaseClient, ConnectorDatabaseClient>()
                   .AddSingleton<IAzureSqlConnectionFactory, AzureSqlConnectionFactory>()
                   .Configure<AzureSqlConnectorDbOptions>(configuration.GetSection(AzureSqlConnectorDbOptions.SECTION_NAME));
            })
            .AddDatabaseService();
        }
    }
}
