using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Caching;
using RecordPoint.Connectors.SDK.Caching.Semaphore;
using RecordPoint.Connectors.SDK.Configuration;
using RecordPoint.Connectors.SDK.Databases.Cosmos.Manager;
using RecordPoint.Connectors.SDK.Databases.Cosmos.SemephoreLock;
using RecordPoint.Connectors.SDK.Databases.SemephoreLock;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos.SemaphoreLock;

public static class CosmosSemaphoreLockBuilderExtensions
{
    public static IHostBuilder UseCosmosSemaphoreLock(this IHostBuilder hostBuilder)
    {
        return hostBuilder
            .ConfigureServices((context, services) =>
            {
                var cosmosDbConnectorDatabaseOptions = context.Configuration.GetSection(CosmosDbConnectorDatabaseOptions.SECTION_NAME)
                    .Get<CosmosDbConnectorDatabaseOptions>();

                CosmosClient cosmosClient;
                if (!string.IsNullOrEmpty(cosmosDbConnectorDatabaseOptions.ConnectionString))
                {
                    cosmosClient = new CosmosClient(cosmosDbConnectorDatabaseOptions.ConnectionString);
                }
                else
                {
                    var azureAuthenticationOptions = context.Configuration.GetSection(AzureAuthenticationOptions.SECTION_NAME).Get<AzureAuthenticationOptions>();
                    var credential = azureAuthenticationOptions.GetTokenCredential();
                    var accountEndpoint = $"https://{cosmosDbConnectorDatabaseOptions.CosmosDbAccountName}.documents.azure.com/";
                    cosmosClient = new CosmosClient(accountEndpoint, credential);
                }

                services
                    .AddSingleton<ICosmosDbManager<SemaphoreLockCosmosDbItem>>(x =>
                        x.InitialiseCosmosStorage<SemaphoreLockCosmosDbItem>(cosmosClient, cosmosDbConnectorDatabaseOptions.DatabaseName, SemaphoreLockCosmosDbItem.COSMOS_DB_CONTAINER_NAME))
                    .AddSingleton<ISemaphoreLockManager, CosmosSemaphoreLockManager>();
            });
    }

    public static IHostBuilder UseCosmosSemaphoreLock<TSemaphoreLockScopedKeyAction>(this IHostBuilder hostBuilder)
        where TSemaphoreLockScopedKeyAction : class, ISemaphoreLockScopedKeyAction
    {
        return hostBuilder.UseCosmosSemaphoreLock()
            .ConfigureServices((hostContext, services) =>
            {
                services
                    .AddScoped<ISemaphoreLockScopedKeyAction, TSemaphoreLockScopedKeyAction>();
            });
    }
}