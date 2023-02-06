using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RecordPoint.Connectors.SDK.Databases.Sqlite
{
    /// <summary>
    /// Sqllite host builder extensions
    /// </summary>
    public static class SqliteConnectorDatabaseBuilderExtensions
    {
        /// <summary>
        /// Use Sqlite as the connector database
        /// </summary>
        /// <param name="hostBuilder">App host builder</param>
        /// <returns>Updated host builder</returns>
        public static IHostBuilder UseSqliteConnectorDatabase(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(services => 
            {
                services
                    .AddSingleton<IConnectorDatabaseProvider, SqliteConnectorDatabaseProvider>()
                    .AddSingleton<IConnectorDatabaseClient, ConnectorDatabaseClient>();
            })
            .AddDatabaseService();
        }
    }
}
