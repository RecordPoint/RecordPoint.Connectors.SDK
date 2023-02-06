using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RecordPoint.Connectors.SDK.Databases
{
    /// <summary>
    /// Database host extensions
    /// </summary>
    public static class DatabaseBuilderExtensions
    {
        /// <summary>
        /// Add the connect database management service
        /// </summary>
        /// <param name="hostBuilder">Host builder to configure</param>
        /// <returns>Updated host builder</returns>
        public static IHostBuilder AddDatabaseService(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(services => services.AddHostedService<DatabaseService<ConnectorDbContext, IConnectorDatabaseProvider>>());
        }
    }
}