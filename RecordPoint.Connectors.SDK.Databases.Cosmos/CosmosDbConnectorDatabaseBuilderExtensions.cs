using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RecordPoint.Connectors.SDK.Databases.Cosmos.Manager;
using RecordPoint.Connectors.SDK.Observability;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos
{
    /// <summary>
    /// Cosmos Db host builder extensions
    /// </summary>
    public static class CosmosDbConnectorDatabaseBuilderExtensions
    {
        /// <summary>
        /// Use a Cosmos DB as the connector database
        /// </summary>
        /// <param name="hostBuilder">App host builder</param>
        /// <returns>Updated host builder</returns>
        public static IHostBuilder UseCosmosDbConnectorDatabase(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;
                services
                    .AddSingleton<CosmosDbConnectorDatabaseProvider, CosmosDbConnectorDatabaseProvider>()
                    .AddSingleton(container =>
                    {
                        // Wrap the cosmos db provider with custom telemetry
                        // so that we know when connections are made
                        var realProvider = container.GetService<CosmosDbConnectorDatabaseProvider>();
                        var tracker = container.GetService<ITelemetryTracker>();
                        return realProvider.WithAddedTelemetry(tracker);
                    })
                    .AddSingleton<IConnectorDatabaseClient, ConnectorDatabaseClient>()
                    .Configure<CosmosDbConnectorDatabaseOptions>(configuration.GetSection(CosmosDbConnectorDatabaseOptions.SECTION_NAME));
            });
        }

        /// <summary>
        /// Initialises a Cosmos Database Manager for the provided model type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <param name="cosmosClient"></param>
        /// <param name="databaseName"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        public static CosmosDbManager<T> InitialiseCosmosStorage<T>(
            this IServiceProvider serviceProvider,
            CosmosClient cosmosClient, 
            string databaseName, 
            string containerName) 
            where T : BaseCosmosDbItem
        {
            var logger = serviceProvider.GetRequiredService<ILogger<CosmosDbManager<T>>>();
            return new CosmosDbManager<T>(cosmosClient, databaseName, containerName, logger);
        }

    }
}