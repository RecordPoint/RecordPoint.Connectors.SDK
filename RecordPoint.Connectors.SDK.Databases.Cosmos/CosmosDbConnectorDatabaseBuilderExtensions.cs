using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RecordPoint.Connectors.SDK.Configuration;
using RecordPoint.Connectors.SDK.Databases.Cosmos.Helpers;
using RecordPoint.Connectors.SDK.Databases.Cosmos.Manager;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Toggles;

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
        /// <param name="cosmosDbConnectorDatabaseOptions"></param>
        /// <param name="cosmosAzureAuthenticationOptions"></param>
        /// <param name="databaseName"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        public static CosmosDbManager<T> InitialiseCosmosStorage<T>(
            this IServiceProvider serviceProvider,
            CosmosDbConnectorDatabaseOptions cosmosDbConnectorDatabaseOptions,
            AzureAuthenticationOptions cosmosAzureAuthenticationOptions,
            string databaseName,
            string containerName)
            where T : BaseCosmosDbItem
        {
            var featureToggleProvider = serviceProvider.GetRequiredService<IToggleProvider>();
            var cosmosClient = GetCosmosClient(featureToggleProvider, cosmosDbConnectorDatabaseOptions, cosmosAzureAuthenticationOptions);
            var logger = serviceProvider.GetRequiredService<ILogger<CosmosDbManager<T>>>();
            return new CosmosDbManager<T>(cosmosClient, databaseName, containerName, logger);
        }

        /// <summary>
        /// Returns a cosmos client for building cosmos db manager
        /// </summary>
        /// <param name="featureToggleProvider"></param>
        /// <param name="options"></param>
        /// <param name="cosmosAzureAuthenticationOptions"></param>
        /// <returns></returns>
        private static CosmosClient GetCosmosClient(IToggleProvider featureToggleProvider, CosmosDbConnectorDatabaseOptions options, AzureAuthenticationOptions cosmosAzureAuthenticationOptions)
        {
            CosmosClient cosmosClient;

            var cosmosSerializationOptions = new CosmosClientOptions
            {
                SerializerOptions = new CosmosSerializationOptions
                {
                    PropertyNamingPolicy = options.UseCamelCaseNamingPolicy
                        ? CosmosPropertyNamingPolicy.CamelCase
                        : CosmosPropertyNamingPolicy.Default
                }
            };

            if (!string.IsNullOrEmpty(options.ConnectionString))
            {
                SetConnectionMode(cosmosSerializationOptions, options.ConnectionString, options.UseGateWayConnectionMode);
                cosmosClient = new CosmosClient(options.ConnectionString, cosmosSerializationOptions);
            }
            else
            {
                var credential = cosmosAzureAuthenticationOptions.GetTokenCredential();
                var accountEndpoint = CosmosEndpointHelper.BuildCosmosAccountEndpoint(featureToggleProvider, options.CosmosDbAccountName);
                SetConnectionMode(cosmosSerializationOptions, accountEndpoint, options.UseGateWayConnectionMode);
                cosmosClient = new CosmosClient(accountEndpoint, credential, cosmosSerializationOptions);
            }
            return cosmosClient;
        }

        private static void SetConnectionMode(CosmosClientOptions cosmosSerializationOptions, string cosmosConnectionStringOrEndpoint, bool useGateway)
        {
            const string cosmosDedicatedGatewaySignifier = "sqlx.cosmos.azure.com";
            cosmosSerializationOptions.ConnectionMode =
                cosmosConnectionStringOrEndpoint.Contains(cosmosDedicatedGatewaySignifier) || useGateway
                    ? ConnectionMode.Gateway
                    : ConnectionMode.Direct;
        }

    }
}