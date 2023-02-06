using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RecordPoint.Connectors.SDK.Databases.PostgreSql
{
    /// <summary>
    /// PostgreSql host builder extensions
    /// </summary>
    public static class PostgreSqlConnectorDbBuilderExtensions
    {
        /// <summary>
        /// Use an PostgreSql Server as the connector database
        /// </summary>
        /// <param name="hostBuilder">App host builder</param>
        /// <returns>Updated host builder</returns>
        public static IHostBuilder UsePostgreSqlConnectorDatabase(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;
                services
                   .AddSingleton<IConnectorDatabaseProvider, PostgreSqlConnectorDbProvider>()
                   .AddSingleton<IConnectorDatabaseClient, ConnectorDatabaseClient>()
                   .AddSingleton<IPostgreSqlConnectionFactory, PostgreSqlConnectionFactory>()
                   .Configure<PostgreSqlConnectorDbOptions>(configuration.GetSection(PostgreSqlConnectorDbOptions.SECTION_NAME));
            })
            .AddDatabaseService();
        }
    }
}
