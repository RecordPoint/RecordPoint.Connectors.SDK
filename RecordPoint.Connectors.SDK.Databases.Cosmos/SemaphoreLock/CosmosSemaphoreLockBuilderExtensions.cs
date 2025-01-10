using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Caching;
using RecordPoint.Connectors.SDK.Caching.Semaphore;
using RecordPoint.Connectors.SDK.Databases.Cosmos.Manager;
using RecordPoint.Connectors.SDK.Databases.Cosmos.SemephoreLock;
using RecordPoint.Connectors.SDK.Databases.SemephoreLock;
using AzureAuthenticationOptions = RecordPoint.Connectors.SDK.Configuration.AzureAuthenticationOptions;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos.SemaphoreLock;

/// <summary>
/// The cosmos semaphore lock builder extensions.
/// </summary>
public static class CosmosSemaphoreLockBuilderExtensions
{
    /// <summary>
    /// Use cosmos semaphore lock.
    /// </summary>
    /// <param name="hostBuilder">The host builder.</param>
    /// <returns>An IHostBuilder</returns>
    public static IHostBuilder UseCosmosSemaphoreLock(this IHostBuilder hostBuilder)
    {
        return hostBuilder
            .ConfigureServices((context, services) =>
            {
                var cosmosDbConnectorDatabaseOptions =
                    context.Configuration.GetSection(CosmosDbConnectorDatabaseOptions.SECTION_NAME).Get<CosmosDbConnectorDatabaseOptions>();
                var azureAuthenticationOptions = context.Configuration.GetSection(AzureAuthenticationOptions.SECTION_NAME).Get<AzureAuthenticationOptions>();
                services
                .AddSingleton<ICosmosDbManager<SemaphoreLockCosmosDbItem>>(x =>
                    x.InitialiseCosmosStorage<SemaphoreLockCosmosDbItem>(cosmosDbConnectorDatabaseOptions!, azureAuthenticationOptions!, cosmosDbConnectorDatabaseOptions!.DatabaseName, SemaphoreLockCosmosDbItem.COSMOS_DB_CONTAINER_NAME))
                .AddSingleton<ISemaphoreLockManager, CosmosSemaphoreLockManager>();
            });

    }

    /// <summary>
    /// Use cosmos semaphore lock.
    /// </summary>
    /// <typeparam name="TSemaphoreLockScopedKeyAction"/>
    /// <param name="hostBuilder">The host builder.</param>
    /// <returns>An IHostBuilder</returns>
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