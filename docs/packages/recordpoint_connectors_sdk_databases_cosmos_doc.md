<a name='assembly'></a>
# RecordPoint.Connectors.SDK.Databases.Cosmos

## Contents

- [BaseCosmosDbItem](#T-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-BaseCosmosDbItem 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.BaseCosmosDbItem')
  - [ETag](#P-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-BaseCosmosDbItem-ETag 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.BaseCosmosDbItem.ETag')
  - [Id](#P-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-BaseCosmosDbItem-Id 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.BaseCosmosDbItem.Id')
  - [LastModifiedCosmosTimeStamp](#P-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-BaseCosmosDbItem-LastModifiedCosmosTimeStamp 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.BaseCosmosDbItem.LastModifiedCosmosTimeStamp')
  - [Ttl](#P-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-BaseCosmosDbItem-Ttl 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.BaseCosmosDbItem.Ttl')
- [ConnectorToggleExtensions](#T-RecordPoint-Connectors-SDK-Databases-Cosmos-ConnectorToggleExtensions 'RecordPoint.Connectors.SDK.Databases.Cosmos.ConnectorToggleExtensions')
  - [UseCosmosDedicatedGateway(toggleProvider)](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-ConnectorToggleExtensions-UseCosmosDedicatedGateway-RecordPoint-Connectors-SDK-Toggles-IToggleProvider- 'RecordPoint.Connectors.SDK.Databases.Cosmos.ConnectorToggleExtensions.UseCosmosDedicatedGateway(RecordPoint.Connectors.SDK.Toggles.IToggleProvider)')
- [CosmosDbConnectorDatabaseBuilderExtensions](#T-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseBuilderExtensions 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseBuilderExtensions')
  - [GetCosmosClient(featureToggleProvider,options,cosmosAzureAuthenticationOptions)](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseBuilderExtensions-GetCosmosClient-RecordPoint-Connectors-SDK-Toggles-IToggleProvider,RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseOptions,RecordPoint-Connectors-SDK-Configuration-AzureAuthenticationOptions- 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseBuilderExtensions.GetCosmosClient(RecordPoint.Connectors.SDK.Toggles.IToggleProvider,RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseOptions,RecordPoint.Connectors.SDK.Configuration.AzureAuthenticationOptions)')
  - [InitialiseCosmosStorage\`\`1(serviceProvider,cosmosDbConnectorDatabaseOptions,cosmosAzureAuthenticationOptions,databaseName,containerName)](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseBuilderExtensions-InitialiseCosmosStorage``1-System-IServiceProvider,RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseOptions,RecordPoint-Connectors-SDK-Configuration-AzureAuthenticationOptions,System-String,System-String- 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseBuilderExtensions.InitialiseCosmosStorage``1(System.IServiceProvider,RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseOptions,RecordPoint.Connectors.SDK.Configuration.AzureAuthenticationOptions,System.String,System.String)')
  - [UseCosmosDbConnectorDatabase(hostBuilder)](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseBuilderExtensions-UseCosmosDbConnectorDatabase-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseBuilderExtensions.UseCosmosDbConnectorDatabase(Microsoft.Extensions.Hosting.IHostBuilder)')
- [CosmosDbConnectorDatabaseOptions](#T-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseOptions 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseOptions')
  - [DEFAULT_DATABASE_NAME](#F-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseOptions-DEFAULT_DATABASE_NAME 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseOptions.DEFAULT_DATABASE_NAME')
  - [SECTION_NAME](#F-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseOptions-SECTION_NAME 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseOptions.SECTION_NAME')
  - [ConnectionString](#P-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseOptions-ConnectionString 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseOptions.ConnectionString')
  - [CosmosDbAccountName](#P-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseOptions-CosmosDbAccountName 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseOptions.CosmosDbAccountName')
  - [DatabaseName](#P-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseOptions-DatabaseName 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseOptions.DatabaseName')
  - [UseCamelCaseNamingPolicy](#P-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseOptions-UseCamelCaseNamingPolicy 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseOptions.UseCamelCaseNamingPolicy')
  - [UseGateWayConnectionMode](#P-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseOptions-UseGateWayConnectionMode 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseOptions.UseGateWayConnectionMode')
- [CosmosDbConnectorDatabaseProvider](#T-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseProvider 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseProvider')
  - [#ctor(systemContext,configuration,telemetryTracker,toggleProvider,databaseOptions)](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseProvider-#ctor-RecordPoint-Connectors-SDK-Context-ISystemContext,Microsoft-Extensions-Configuration-IConfiguration,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Toggles-IToggleProvider,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseOptions}- 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseProvider.#ctor(RecordPoint.Connectors.SDK.Context.ISystemContext,Microsoft.Extensions.Configuration.IConfiguration,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,RecordPoint.Connectors.SDK.Toggles.IToggleProvider,Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseOptions})')
  - [_databaseOptions](#F-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseProvider-_databaseOptions 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseProvider._databaseOptions')
  - [CreateDbAdminContext()](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseProvider-CreateDbAdminContext 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseProvider.CreateDbAdminContext')
  - [CreateDbContext()](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseProvider-CreateDbContext 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseProvider.CreateDbContext')
  - [GetAdminConnectionString()](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseProvider-GetAdminConnectionString 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseProvider.GetAdminConnectionString')
  - [GetConnectionString()](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseProvider-GetConnectionString 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseProvider.GetConnectionString')
  - [GetDatabaseName()](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseProvider-GetDatabaseName 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseProvider.GetDatabaseName')
- [CosmosDbConnectorDbContext](#T-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDbContext 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDbContext')
  - [#ctor(options)](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDbContext-#ctor-Microsoft-EntityFrameworkCore-DbContextOptions{RecordPoint-Connectors-SDK-Databases-ConnectorDbContext}- 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDbContext.#ctor(Microsoft.EntityFrameworkCore.DbContextOptions{RecordPoint.Connectors.SDK.Databases.ConnectorDbContext})')
  - [OnModelCreating(modelBuilder)](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDbContext-OnModelCreating-Microsoft-EntityFrameworkCore-ModelBuilder- 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDbContext.OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder)')
- [CosmosDbDatabaseProvider\`1](#T-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbDatabaseProvider`1 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbDatabaseProvider`1')
  - [#ctor(systemContext,configuration,telemetryTracker,toggleProvider,options)](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbDatabaseProvider`1-#ctor-RecordPoint-Connectors-SDK-Context-ISystemContext,Microsoft-Extensions-Configuration-IConfiguration,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Toggles-IToggleProvider,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseOptions}- 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbDatabaseProvider`1.#ctor(RecordPoint.Connectors.SDK.Context.ISystemContext,Microsoft.Extensions.Configuration.IConfiguration,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,RecordPoint.Connectors.SDK.Toggles.IToggleProvider,Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseOptions})')
  - [_configuration](#F-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbDatabaseProvider`1-_configuration 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbDatabaseProvider`1._configuration')
  - [_options](#F-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbDatabaseProvider`1-_options 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbDatabaseProvider`1._options')
  - [_toggleProvider](#F-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbDatabaseProvider`1-_toggleProvider 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbDatabaseProvider`1._toggleProvider')
  - [CheckDatabaseExists(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbDatabaseProvider`1-CheckDatabaseExists-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbDatabaseProvider`1.CheckDatabaseExists(System.Threading.CancellationToken)')
  - [Exists()](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbDatabaseProvider`1-Exists 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbDatabaseProvider`1.Exists')
  - [GetAdminContextOptionsBuilder()](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbDatabaseProvider`1-GetAdminContextOptionsBuilder 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbDatabaseProvider`1.GetAdminContextOptionsBuilder')
  - [GetContextOptionsBuilder()](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbDatabaseProvider`1-GetContextOptionsBuilder 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbDatabaseProvider`1.GetContextOptionsBuilder')
  - [GetSqlDatabaseScript(scriptName,parameters)](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbDatabaseProvider`1-GetSqlDatabaseScript-System-String,System-Collections-Generic-Dictionary{System-String,System-String}- 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbDatabaseProvider`1.GetSqlDatabaseScript(System.String,System.Collections.Generic.Dictionary{System.String,System.String})')
  - [PrepareAsync()](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbDatabaseProvider`1-PrepareAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbDatabaseProvider`1.PrepareAsync(System.Threading.CancellationToken)')
- [CosmosDbManager\`1](#T-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-CosmosDbManager`1 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.CosmosDbManager`1')
  - [#ctor(cosmosClient,databaseId,containerId,telemetryTracker)](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-CosmosDbManager`1-#ctor-Microsoft-Azure-Cosmos-CosmosClient,System-String,System-String,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker- 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.CosmosDbManager`1.#ctor(Microsoft.Azure.Cosmos.CosmosClient,System.String,System.String,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker)')
  - [_container](#F-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-CosmosDbManager`1-_container 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.CosmosDbManager`1._container')
  - [_telemetryTracker](#F-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-CosmosDbManager`1-_telemetryTracker 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.CosmosDbManager`1._telemetryTracker')
  - [DeleteAsync()](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-CosmosDbManager`1-DeleteAsync-System-String,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.CosmosDbManager`1.DeleteAsync(System.String,System.String,System.Threading.CancellationToken)')
  - [GetAsync()](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-CosmosDbManager`1-GetAsync-System-String,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.CosmosDbManager`1.GetAsync(System.String,System.String,System.Threading.CancellationToken)')
  - [GetContainerQuery()](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-CosmosDbManager`1-GetContainerQuery 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.CosmosDbManager`1.GetContainerQuery')
  - [GetFeedIterator()](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-CosmosDbManager`1-GetFeedIterator-System-Linq-IQueryable{`0}- 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.CosmosDbManager`1.GetFeedIterator(System.Linq.IQueryable{`0})')
  - [QueryLinqAsync()](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-CosmosDbManager`1-QueryLinqAsync-System-Linq-IQueryable{`0}- 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.CosmosDbManager`1.QueryLinqAsync(System.Linq.IQueryable{`0})')
  - [QuerySqlAsync()](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-CosmosDbManager`1-QuerySqlAsync-System-String,Microsoft-Azure-Cosmos-QueryDefinition,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.CosmosDbManager`1.QuerySqlAsync(System.String,Microsoft.Azure.Cosmos.QueryDefinition,System.Threading.CancellationToken)')
  - [ReplaceAsync()](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-CosmosDbManager`1-ReplaceAsync-System-String,`0- 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.CosmosDbManager`1.ReplaceAsync(System.String,`0)')
  - [UpsertAsync()](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-CosmosDbManager`1-UpsertAsync-System-String,`0,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.CosmosDbManager`1.UpsertAsync(System.String,`0,System.Threading.CancellationToken)')
  - [UpsertMatchingEtagAsync()](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-CosmosDbManager`1-UpsertMatchingEtagAsync-System-String,`0- 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.CosmosDbManager`1.UpsertMatchingEtagAsync(System.String,`0)')
- [CosmosEndpointHelper](#T-RecordPoint-Connectors-SDK-Databases-Cosmos-Helpers-CosmosEndpointHelper 'RecordPoint.Connectors.SDK.Databases.Cosmos.Helpers.CosmosEndpointHelper')
  - [BuildCosmosAccountEndpoint(featureToggleProvider,cosmosDbAccountName)](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-Helpers-CosmosEndpointHelper-BuildCosmosAccountEndpoint-RecordPoint-Connectors-SDK-Toggles-IToggleProvider,System-String- 'RecordPoint.Connectors.SDK.Databases.Cosmos.Helpers.CosmosEndpointHelper.BuildCosmosAccountEndpoint(RecordPoint.Connectors.SDK.Toggles.IToggleProvider,System.String)')
- [CosmosSemaphoreLockBuilderExtensions](#T-RecordPoint-Connectors-SDK-Databases-Cosmos-SemaphoreLock-CosmosSemaphoreLockBuilderExtensions 'RecordPoint.Connectors.SDK.Databases.Cosmos.SemaphoreLock.CosmosSemaphoreLockBuilderExtensions')
  - [UseCosmosSemaphoreLock(hostBuilder)](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-SemaphoreLock-CosmosSemaphoreLockBuilderExtensions-UseCosmosSemaphoreLock-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Databases.Cosmos.SemaphoreLock.CosmosSemaphoreLockBuilderExtensions.UseCosmosSemaphoreLock(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseCosmosSemaphoreLock\`\`1(hostBuilder)](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-SemaphoreLock-CosmosSemaphoreLockBuilderExtensions-UseCosmosSemaphoreLock``1-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Databases.Cosmos.SemaphoreLock.CosmosSemaphoreLockBuilderExtensions.UseCosmosSemaphoreLock``1(Microsoft.Extensions.Hosting.IHostBuilder)')
- [CosmosSemaphoreLockManager](#T-RecordPoint-Connectors-SDK-Databases-SemephoreLock-CosmosSemaphoreLockManager 'RecordPoint.Connectors.SDK.Databases.SemephoreLock.CosmosSemaphoreLockManager')
  - [#ctor(serviceProvider,semaphoreDbManager)](#M-RecordPoint-Connectors-SDK-Databases-SemephoreLock-CosmosSemaphoreLockManager-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-ICosmosDbManager{RecordPoint-Connectors-SDK-Databases-Cosmos-SemephoreLock-SemaphoreLockCosmosDbItem}- 'RecordPoint.Connectors.SDK.Databases.SemephoreLock.CosmosSemaphoreLockManager.#ctor(System.IServiceProvider,RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.ICosmosDbManager{RecordPoint.Connectors.SDK.Databases.Cosmos.SemephoreLock.SemaphoreLockCosmosDbItem})')
  - [GLOBAL_SEMAPHORE_KEY](#F-RecordPoint-Connectors-SDK-Databases-SemephoreLock-CosmosSemaphoreLockManager-GLOBAL_SEMAPHORE_KEY 'RecordPoint.Connectors.SDK.Databases.SemephoreLock.CosmosSemaphoreLockManager.GLOBAL_SEMAPHORE_KEY')
  - [_semaphoreDbManager](#F-RecordPoint-Connectors-SDK-Databases-SemephoreLock-CosmosSemaphoreLockManager-_semaphoreDbManager 'RecordPoint.Connectors.SDK.Databases.SemephoreLock.CosmosSemaphoreLockManager._semaphoreDbManager')
  - [_serviceProvider](#F-RecordPoint-Connectors-SDK-Databases-SemephoreLock-CosmosSemaphoreLockManager-_serviceProvider 'RecordPoint.Connectors.SDK.Databases.SemephoreLock.CosmosSemaphoreLockManager._serviceProvider')
  - [ConnectorConfiguration](#P-RecordPoint-Connectors-SDK-Databases-SemephoreLock-CosmosSemaphoreLockManager-ConnectorConfiguration 'RecordPoint.Connectors.SDK.Databases.SemephoreLock.CosmosSemaphoreLockManager.ConnectorConfiguration')
  - [CheckWaitSemaphoreAsync(workType,context,cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-SemephoreLock-CosmosSemaphoreLockManager-CheckWaitSemaphoreAsync-System-String,System-Object,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.SemephoreLock.CosmosSemaphoreLockManager.CheckWaitSemaphoreAsync(System.String,System.Object,System.Threading.CancellationToken)')
  - [GetScopedSemaphoreKeyAsync(workType,context,cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-SemephoreLock-CosmosSemaphoreLockManager-GetScopedSemaphoreKeyAsync-System-String,System-Object,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.SemephoreLock.CosmosSemaphoreLockManager.GetScopedSemaphoreKeyAsync(System.String,System.Object,System.Threading.CancellationToken)')
  - [GetSemaphoreAsync(workType,context,cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-SemephoreLock-CosmosSemaphoreLockManager-GetSemaphoreAsync-System-String,System-Object,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.SemephoreLock.CosmosSemaphoreLockManager.GetSemaphoreAsync(System.String,System.Object,System.Threading.CancellationToken)')
  - [GetSemaphoreKeysAsync(workType,context,cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-SemephoreLock-CosmosSemaphoreLockManager-GetSemaphoreKeysAsync-System-String,System-Object,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.SemephoreLock.CosmosSemaphoreLockManager.GetSemaphoreKeysAsync(System.String,System.Object,System.Threading.CancellationToken)')
  - [SetSemaphoreAsync(semaphoreLockType,workType,context,duration,cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-SemephoreLock-CosmosSemaphoreLockManager-SetSemaphoreAsync-RecordPoint-Connectors-SDK-Caching-Semaphore-SemaphoreLockType,System-String,System-Object,System-Int32,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.SemephoreLock.CosmosSemaphoreLockManager.SetSemaphoreAsync(RecordPoint.Connectors.SDK.Caching.Semaphore.SemaphoreLockType,System.String,System.Object,System.Int32,System.Threading.CancellationToken)')
- [ICosmosDbDatabaseProvider\`1](#T-RecordPoint-Connectors-SDK-Databases-Cosmos-ICosmosDbDatabaseProvider`1 'RecordPoint.Connectors.SDK.Databases.Cosmos.ICosmosDbDatabaseProvider`1')
  - [GetDatabaseLogFilePath()](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-ICosmosDbDatabaseProvider`1-GetDatabaseLogFilePath 'RecordPoint.Connectors.SDK.Databases.Cosmos.ICosmosDbDatabaseProvider`1.GetDatabaseLogFilePath')
  - [GetDatabasePath()](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-ICosmosDbDatabaseProvider`1-GetDatabasePath 'RecordPoint.Connectors.SDK.Databases.Cosmos.ICosmosDbDatabaseProvider`1.GetDatabasePath')
- [ICosmosDbManager\`1](#T-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-ICosmosDbManager`1 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.ICosmosDbManager`1')
  - [DeleteAsync(partitionKey,id,cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-ICosmosDbManager`1-DeleteAsync-System-String,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.ICosmosDbManager`1.DeleteAsync(System.String,System.String,System.Threading.CancellationToken)')
  - [GetAsync(partitionKey,id,cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-ICosmosDbManager`1-GetAsync-System-String,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.ICosmosDbManager`1.GetAsync(System.String,System.String,System.Threading.CancellationToken)')
  - [GetContainerQuery()](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-ICosmosDbManager`1-GetContainerQuery 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.ICosmosDbManager`1.GetContainerQuery')
  - [GetFeedIterator(query)](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-ICosmosDbManager`1-GetFeedIterator-System-Linq-IQueryable{`0}- 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.ICosmosDbManager`1.GetFeedIterator(System.Linq.IQueryable{`0})')
  - [QueryLinqAsync(query)](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-ICosmosDbManager`1-QueryLinqAsync-System-Linq-IQueryable{`0}- 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.ICosmosDbManager`1.QueryLinqAsync(System.Linq.IQueryable{`0})')
  - [QuerySqlAsync(partitionKey,query,cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-ICosmosDbManager`1-QuerySqlAsync-System-String,Microsoft-Azure-Cosmos-QueryDefinition,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.ICosmosDbManager`1.QuerySqlAsync(System.String,Microsoft.Azure.Cosmos.QueryDefinition,System.Threading.CancellationToken)')
  - [ReplaceAsync(partitionKey,item)](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-ICosmosDbManager`1-ReplaceAsync-System-String,`0- 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.ICosmosDbManager`1.ReplaceAsync(System.String,`0)')
  - [UpsertAsync(partitionKey,item,cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-ICosmosDbManager`1-UpsertAsync-System-String,`0,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.ICosmosDbManager`1.UpsertAsync(System.String,`0,System.Threading.CancellationToken)')
  - [UpsertMatchingEtagAsync(partitionKey,item)](#M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-ICosmosDbManager`1-UpsertMatchingEtagAsync-System-String,`0- 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.ICosmosDbManager`1.UpsertMatchingEtagAsync(System.String,`0)')
- [SemaphoreLockCosmosDbItem](#T-RecordPoint-Connectors-SDK-Databases-Cosmos-SemephoreLock-SemaphoreLockCosmosDbItem 'RecordPoint.Connectors.SDK.Databases.Cosmos.SemephoreLock.SemaphoreLockCosmosDbItem')
  - [COSMOS_DB_CONTAINER_NAME](#F-RecordPoint-Connectors-SDK-Databases-Cosmos-SemephoreLock-SemaphoreLockCosmosDbItem-COSMOS_DB_CONTAINER_NAME 'RecordPoint.Connectors.SDK.Databases.Cosmos.SemephoreLock.SemaphoreLockCosmosDbItem.COSMOS_DB_CONTAINER_NAME')
  - [LockExpiry](#P-RecordPoint-Connectors-SDK-Databases-Cosmos-SemephoreLock-SemaphoreLockCosmosDbItem-LockExpiry 'RecordPoint.Connectors.SDK.Databases.Cosmos.SemephoreLock.SemaphoreLockCosmosDbItem.LockExpiry')

<a name='T-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-BaseCosmosDbItem'></a>
## BaseCosmosDbItem `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.Cosmos.Manager

##### Summary

The base cosmos db item.

<a name='P-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-BaseCosmosDbItem-ETag'></a>
### ETag `property`

##### Summary

Gets or sets the E tag.

<a name='P-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-BaseCosmosDbItem-Id'></a>
### Id `property`

##### Summary

Gets or sets the id.

<a name='P-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-BaseCosmosDbItem-LastModifiedCosmosTimeStamp'></a>
### LastModifiedCosmosTimeStamp `property`

##### Summary

Gets or sets the last modified cosmos time stamp.

<a name='P-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-BaseCosmosDbItem-Ttl'></a>
### Ttl `property`

##### Summary

Gets or sets the ttl.

<a name='T-RecordPoint-Connectors-SDK-Databases-Cosmos-ConnectorToggleExtensions'></a>
## ConnectorToggleExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.Cosmos

##### Summary

Connector config extensions as part of the framework for cosmos DB.

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-ConnectorToggleExtensions-UseCosmosDedicatedGateway-RecordPoint-Connectors-SDK-Toggles-IToggleProvider-'></a>
### UseCosmosDedicatedGateway(toggleProvider) `method`

##### Summary

Retrieve toggle status for the Tmp-UseCosmosDedicatedGateway toggle

##### Returns

Tmp-UseCosmosDedicatedGateway Toggle value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| toggleProvider | [RecordPoint.Connectors.SDK.Toggles.IToggleProvider](#T-RecordPoint-Connectors-SDK-Toggles-IToggleProvider 'RecordPoint.Connectors.SDK.Toggles.IToggleProvider') | IToggleProvider instance to retrieve toggle status from |

<a name='T-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseBuilderExtensions'></a>
## CosmosDbConnectorDatabaseBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.Cosmos

##### Summary

Cosmos Db host builder extensions

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseBuilderExtensions-GetCosmosClient-RecordPoint-Connectors-SDK-Toggles-IToggleProvider,RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseOptions,RecordPoint-Connectors-SDK-Configuration-AzureAuthenticationOptions-'></a>
### GetCosmosClient(featureToggleProvider,options,cosmosAzureAuthenticationOptions) `method`

##### Summary

Returns a cosmos client for building cosmos db manager

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| featureToggleProvider | [RecordPoint.Connectors.SDK.Toggles.IToggleProvider](#T-RecordPoint-Connectors-SDK-Toggles-IToggleProvider 'RecordPoint.Connectors.SDK.Toggles.IToggleProvider') |  |
| options | [RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseOptions](#T-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseOptions 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseOptions') |  |
| cosmosAzureAuthenticationOptions | [RecordPoint.Connectors.SDK.Configuration.AzureAuthenticationOptions](#T-RecordPoint-Connectors-SDK-Configuration-AzureAuthenticationOptions 'RecordPoint.Connectors.SDK.Configuration.AzureAuthenticationOptions') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseBuilderExtensions-InitialiseCosmosStorage``1-System-IServiceProvider,RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseOptions,RecordPoint-Connectors-SDK-Configuration-AzureAuthenticationOptions,System-String,System-String-'></a>
### InitialiseCosmosStorage\`\`1(serviceProvider,cosmosDbConnectorDatabaseOptions,cosmosAzureAuthenticationOptions,databaseName,containerName) `method`

##### Summary

Initialises a Cosmos Database Manager for the provided model type

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| serviceProvider | [System.IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider') |  |
| cosmosDbConnectorDatabaseOptions | [RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseOptions](#T-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseOptions 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseOptions') |  |
| cosmosAzureAuthenticationOptions | [RecordPoint.Connectors.SDK.Configuration.AzureAuthenticationOptions](#T-RecordPoint-Connectors-SDK-Configuration-AzureAuthenticationOptions 'RecordPoint.Connectors.SDK.Configuration.AzureAuthenticationOptions') |  |
| databaseName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| containerName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseBuilderExtensions-UseCosmosDbConnectorDatabase-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseCosmosDbConnectorDatabase(hostBuilder) `method`

##### Summary

Use a Cosmos DB as the connector database

##### Returns

Updated host builder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | App host builder |

<a name='T-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseOptions'></a>
## CosmosDbConnectorDatabaseOptions `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.Cosmos

##### Summary

The cosmos db connector database options.

<a name='F-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseOptions-DEFAULT_DATABASE_NAME'></a>
### DEFAULT_DATABASE_NAME `constants`

##### Summary

Default database name

<a name='F-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseOptions-SECTION_NAME'></a>
### SECTION_NAME `constants`

##### Summary

Configuration section name

<a name='P-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseOptions-ConnectionString'></a>
### ConnectionString `property`

##### Summary

Connection string for the database to be used

<a name='P-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseOptions-CosmosDbAccountName'></a>
### CosmosDbAccountName `property`

##### Summary

The endpoint to the Cosmos Resource

<a name='P-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseOptions-DatabaseName'></a>
### DatabaseName `property`

##### Summary

Name of the database

<a name='P-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseOptions-UseCamelCaseNamingPolicy'></a>
### UseCamelCaseNamingPolicy `property`

##### Summary

Sets the serializer to use Camel Case naming policy

##### Remarks

Default: True

<a name='P-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseOptions-UseGateWayConnectionMode'></a>
### UseGateWayConnectionMode `property`

##### Summary

Force to use Gateway connection mode

<a name='T-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseProvider'></a>
## CosmosDbConnectorDatabaseProvider `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.Cosmos

##### Summary

The cosmos db connector database provider.

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseProvider-#ctor-RecordPoint-Connectors-SDK-Context-ISystemContext,Microsoft-Extensions-Configuration-IConfiguration,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Toggles-IToggleProvider,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseOptions}-'></a>
### #ctor(systemContext,configuration,telemetryTracker,toggleProvider,databaseOptions) `constructor`

##### Summary

Initializes a new instance of the [CosmosDbConnectorDatabaseProvider](#T-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseProvider 'RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseProvider') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |
| configuration | [Microsoft.Extensions.Configuration.IConfiguration](#T-Microsoft-Extensions-Configuration-IConfiguration 'Microsoft.Extensions.Configuration.IConfiguration') | The configuration. |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The telemetry tracker. |
| toggleProvider | [RecordPoint.Connectors.SDK.Toggles.IToggleProvider](#T-RecordPoint-Connectors-SDK-Toggles-IToggleProvider 'RecordPoint.Connectors.SDK.Toggles.IToggleProvider') | The toggle provider. |
| databaseOptions | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseOptions}') | The database options. |

<a name='F-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseProvider-_databaseOptions'></a>
### _databaseOptions `constants`

##### Summary

The database options.

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseProvider-CreateDbAdminContext'></a>
### CreateDbAdminContext() `method`

##### Summary

Creates db admin context.

##### Returns

A ConnectorDbContext

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseProvider-CreateDbContext'></a>
### CreateDbContext() `method`

##### Summary

Creates db context.

##### Returns

A ConnectorDbContext

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseProvider-GetAdminConnectionString'></a>
### GetAdminConnectionString() `method`

##### Summary

Get admin connection string.

##### Returns

A string

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseProvider-GetConnectionString'></a>
### GetConnectionString() `method`

##### Summary

Get connection string.

##### Returns

A string

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseProvider-GetDatabaseName'></a>
### GetDatabaseName() `method`

##### Summary

Get database name.

##### Returns

A string

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDbContext'></a>
## CosmosDbConnectorDbContext `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.Cosmos

##### Summary

Cosmos Db Connector Database Context

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDbContext-#ctor-Microsoft-EntityFrameworkCore-DbContextOptions{RecordPoint-Connectors-SDK-Databases-ConnectorDbContext}-'></a>
### #ctor(options) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| options | [Microsoft.EntityFrameworkCore.DbContextOptions{RecordPoint.Connectors.SDK.Databases.ConnectorDbContext}](#T-Microsoft-EntityFrameworkCore-DbContextOptions{RecordPoint-Connectors-SDK-Databases-ConnectorDbContext} 'Microsoft.EntityFrameworkCore.DbContextOptions{RecordPoint.Connectors.SDK.Databases.ConnectorDbContext}') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDbContext-OnModelCreating-Microsoft-EntityFrameworkCore-ModelBuilder-'></a>
### OnModelCreating(modelBuilder) `method`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| modelBuilder | [Microsoft.EntityFrameworkCore.ModelBuilder](#T-Microsoft-EntityFrameworkCore-ModelBuilder 'Microsoft.EntityFrameworkCore.ModelBuilder') |  |

<a name='T-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbDatabaseProvider`1'></a>
## CosmosDbDatabaseProvider\`1 `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.Cosmos

##### Summary

The cosmos db database provider.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TDbContext |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbDatabaseProvider`1-#ctor-RecordPoint-Connectors-SDK-Context-ISystemContext,Microsoft-Extensions-Configuration-IConfiguration,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Toggles-IToggleProvider,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseOptions}-'></a>
### #ctor(systemContext,configuration,telemetryTracker,toggleProvider,options) `constructor`

##### Summary

Initializes a new instance of the class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |
| configuration | [Microsoft.Extensions.Configuration.IConfiguration](#T-Microsoft-Extensions-Configuration-IConfiguration 'Microsoft.Extensions.Configuration.IConfiguration') | The configuration. |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The telemetry tracker. |
| toggleProvider | [RecordPoint.Connectors.SDK.Toggles.IToggleProvider](#T-RecordPoint-Connectors-SDK-Toggles-IToggleProvider 'RecordPoint.Connectors.SDK.Toggles.IToggleProvider') | The toggle provider. |
| options | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbConnectorDatabaseOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Databases.Cosmos.CosmosDbConnectorDatabaseOptions}') | The options. |

<a name='F-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbDatabaseProvider`1-_configuration'></a>
### _configuration `constants`

##### Summary

The configuration.

<a name='F-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbDatabaseProvider`1-_options'></a>
### _options `constants`

##### Summary

The options.

<a name='F-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbDatabaseProvider`1-_toggleProvider'></a>
### _toggleProvider `constants`

##### Summary

The toggle provider.

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbDatabaseProvider`1-CheckDatabaseExists-System-Threading-CancellationToken-'></a>
### CheckDatabaseExists(cancellationToken) `method`

##### Summary

Check database exists.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [RecordPoint.Connectors.SDK.Databases.ConnectorDatabaseException](#T-RecordPoint-Connectors-SDK-Databases-ConnectorDatabaseException 'RecordPoint.Connectors.SDK.Databases.ConnectorDatabaseException') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbDatabaseProvider`1-Exists'></a>
### Exists() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbDatabaseProvider`1-GetAdminContextOptionsBuilder'></a>
### GetAdminContextOptionsBuilder() `method`

##### Summary

Get admin context options builder.

##### Returns

DbContextOptionsBuilder<TDbContext>

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbDatabaseProvider`1-GetContextOptionsBuilder'></a>
### GetContextOptionsBuilder() `method`

##### Summary

Get context options builder.

##### Returns

DbContextOptionsBuilder<TDbContext>

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbDatabaseProvider`1-GetSqlDatabaseScript-System-String,System-Collections-Generic-Dictionary{System-String,System-String}-'></a>
### GetSqlDatabaseScript(scriptName,parameters) `method`

##### Summary

Get sql database script.

##### Returns

A string

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| scriptName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The script name. |
| parameters | [System.Collections.Generic.Dictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.String}') | The parameters. |

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-CosmosDbDatabaseProvider`1-PrepareAsync-System-Threading-CancellationToken-'></a>
### PrepareAsync() `method`

##### Summary

The following methods override the base functionality as EFCore Cosmos provider does not support the required implementations

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-CosmosDbManager`1'></a>
## CosmosDbManager\`1 `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.Cosmos.Manager

##### Summary

The cosmos db manager.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-CosmosDbManager`1-#ctor-Microsoft-Azure-Cosmos-CosmosClient,System-String,System-String,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker-'></a>
### #ctor(cosmosClient,databaseId,containerId,telemetryTracker) `constructor`

##### Summary

Initializes a new instance of the class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cosmosClient | [Microsoft.Azure.Cosmos.CosmosClient](#T-Microsoft-Azure-Cosmos-CosmosClient 'Microsoft.Azure.Cosmos.CosmosClient') | The cosmos client. |
| databaseId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The database id. |
| containerId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The container id. |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The telemetry tracker. |

<a name='F-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-CosmosDbManager`1-_container'></a>
### _container `constants`

##### Summary

The container.

<a name='F-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-CosmosDbManager`1-_telemetryTracker'></a>
### _telemetryTracker `constants`

##### Summary

The telemetry tracker.

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-CosmosDbManager`1-DeleteAsync-System-String,System-String,System-Threading-CancellationToken-'></a>
### DeleteAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-CosmosDbManager`1-GetAsync-System-String,System-String,System-Threading-CancellationToken-'></a>
### GetAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-CosmosDbManager`1-GetContainerQuery'></a>
### GetContainerQuery() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-CosmosDbManager`1-GetFeedIterator-System-Linq-IQueryable{`0}-'></a>
### GetFeedIterator() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-CosmosDbManager`1-QueryLinqAsync-System-Linq-IQueryable{`0}-'></a>
### QueryLinqAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-CosmosDbManager`1-QuerySqlAsync-System-String,Microsoft-Azure-Cosmos-QueryDefinition,System-Threading-CancellationToken-'></a>
### QuerySqlAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-CosmosDbManager`1-ReplaceAsync-System-String,`0-'></a>
### ReplaceAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-CosmosDbManager`1-UpsertAsync-System-String,`0,System-Threading-CancellationToken-'></a>
### UpsertAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-CosmosDbManager`1-UpsertMatchingEtagAsync-System-String,`0-'></a>
### UpsertMatchingEtagAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Databases-Cosmos-Helpers-CosmosEndpointHelper'></a>
## CosmosEndpointHelper `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.Cosmos.Helpers

##### Summary

Cosmos endpoint helper methods

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-Helpers-CosmosEndpointHelper-BuildCosmosAccountEndpoint-RecordPoint-Connectors-SDK-Toggles-IToggleProvider,System-String-'></a>
### BuildCosmosAccountEndpoint(featureToggleProvider,cosmosDbAccountName) `method`

##### Summary

Generate the cosmos db connection string

##### Returns

Cosmos DB connection string

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| featureToggleProvider | [RecordPoint.Connectors.SDK.Toggles.IToggleProvider](#T-RecordPoint-Connectors-SDK-Toggles-IToggleProvider 'RecordPoint.Connectors.SDK.Toggles.IToggleProvider') |  |
| cosmosDbAccountName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-RecordPoint-Connectors-SDK-Databases-Cosmos-SemaphoreLock-CosmosSemaphoreLockBuilderExtensions'></a>
## CosmosSemaphoreLockBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.Cosmos.SemaphoreLock

##### Summary

The cosmos semaphore lock builder extensions.

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-SemaphoreLock-CosmosSemaphoreLockBuilderExtensions-UseCosmosSemaphoreLock-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseCosmosSemaphoreLock(hostBuilder) `method`

##### Summary

Use cosmos semaphore lock.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-SemaphoreLock-CosmosSemaphoreLockBuilderExtensions-UseCosmosSemaphoreLock``1-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseCosmosSemaphoreLock\`\`1(hostBuilder) `method`

##### Summary

Use cosmos semaphore lock.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TSemaphoreLockScopedKeyAction |  |

<a name='T-RecordPoint-Connectors-SDK-Databases-SemephoreLock-CosmosSemaphoreLockManager'></a>
## CosmosSemaphoreLockManager `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.SemephoreLock

##### Summary

The cosmos semaphore lock manager.

<a name='M-RecordPoint-Connectors-SDK-Databases-SemephoreLock-CosmosSemaphoreLockManager-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-ICosmosDbManager{RecordPoint-Connectors-SDK-Databases-Cosmos-SemephoreLock-SemaphoreLockCosmosDbItem}-'></a>
### #ctor(serviceProvider,semaphoreDbManager) `constructor`

##### Summary

Initializes a new instance of the [CosmosSemaphoreLockManager](#T-RecordPoint-Connectors-SDK-Databases-SemephoreLock-CosmosSemaphoreLockManager 'RecordPoint.Connectors.SDK.Databases.SemephoreLock.CosmosSemaphoreLockManager') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| serviceProvider | [System.IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider') | The service provider. |
| semaphoreDbManager | [RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.ICosmosDbManager{RecordPoint.Connectors.SDK.Databases.Cosmos.SemephoreLock.SemaphoreLockCosmosDbItem}](#T-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-ICosmosDbManager{RecordPoint-Connectors-SDK-Databases-Cosmos-SemephoreLock-SemaphoreLockCosmosDbItem} 'RecordPoint.Connectors.SDK.Databases.Cosmos.Manager.ICosmosDbManager{RecordPoint.Connectors.SDK.Databases.Cosmos.SemephoreLock.SemaphoreLockCosmosDbItem}') | The semaphore db manager. |

<a name='F-RecordPoint-Connectors-SDK-Databases-SemephoreLock-CosmosSemaphoreLockManager-GLOBAL_SEMAPHORE_KEY'></a>
### GLOBAL_SEMAPHORE_KEY `constants`

##### Summary

The GLOBAL SEMAPHORE KEY.

<a name='F-RecordPoint-Connectors-SDK-Databases-SemephoreLock-CosmosSemaphoreLockManager-_semaphoreDbManager'></a>
### _semaphoreDbManager `constants`

##### Summary

The semaphore db manager.

<a name='F-RecordPoint-Connectors-SDK-Databases-SemephoreLock-CosmosSemaphoreLockManager-_serviceProvider'></a>
### _serviceProvider `constants`

##### Summary

The service provider.

<a name='P-RecordPoint-Connectors-SDK-Databases-SemephoreLock-CosmosSemaphoreLockManager-ConnectorConfiguration'></a>
### ConnectorConfiguration `property`

##### Summary

Gets or sets the connector configuration.

<a name='M-RecordPoint-Connectors-SDK-Databases-SemephoreLock-CosmosSemaphoreLockManager-CheckWaitSemaphoreAsync-System-String,System-Object,System-Threading-CancellationToken-'></a>
### CheckWaitSemaphoreAsync(workType,context,cancellationToken) `method`

##### Summary

Check wait semaphore asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The work type. |
| context | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | Context for lock keys when external apis have different restrictions, ie: by channel |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-Databases-SemephoreLock-CosmosSemaphoreLockManager-GetScopedSemaphoreKeyAsync-System-String,System-Object,System-Threading-CancellationToken-'></a>
### GetScopedSemaphoreKeyAsync(workType,context,cancellationToken) `method`

##### Summary

Get scoped semaphore key asynchronously.

##### Returns

Task<string>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The work type. |
| context | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | Context for lock keys when external apis have different restrictions, ie: by channel |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [RecordPoint.Connectors.SDK.RequiredValueNullException](#T-RecordPoint-Connectors-SDK-RequiredValueNullException 'RecordPoint.Connectors.SDK.RequiredValueNullException') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-SemephoreLock-CosmosSemaphoreLockManager-GetSemaphoreAsync-System-String,System-Object,System-Threading-CancellationToken-'></a>
### GetSemaphoreAsync(workType,context,cancellationToken) `method`

##### Summary

Get the semaphore asynchronously.

##### Returns

Task<DateTimeOffset?>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The work type. |
| context | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | Context for lock keys when external apis have different restrictions, ie: by channel |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-Databases-SemephoreLock-CosmosSemaphoreLockManager-GetSemaphoreKeysAsync-System-String,System-Object,System-Threading-CancellationToken-'></a>
### GetSemaphoreKeysAsync(workType,context,cancellationToken) `method`

##### Summary

Get semaphore keys asynchronously.

##### Returns

Task<string[]>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The work type. |
| context | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | Context for lock keys when external apis have different restrictions, ie: by channel |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-Databases-SemephoreLock-CosmosSemaphoreLockManager-SetSemaphoreAsync-RecordPoint-Connectors-SDK-Caching-Semaphore-SemaphoreLockType,System-String,System-Object,System-Int32,System-Threading-CancellationToken-'></a>
### SetSemaphoreAsync(semaphoreLockType,workType,context,duration,cancellationToken) `method`

##### Summary

Set the semaphore asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| semaphoreLockType | [RecordPoint.Connectors.SDK.Caching.Semaphore.SemaphoreLockType](#T-RecordPoint-Connectors-SDK-Caching-Semaphore-SemaphoreLockType 'RecordPoint.Connectors.SDK.Caching.Semaphore.SemaphoreLockType') | The semaphore lock type. |
| workType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The work type. |
| context | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | Context for lock keys when external apis have different restrictions, ie: by channel |
| duration | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The duration. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [RecordPoint.Connectors.SDK.RequiredValueNullException](#T-RecordPoint-Connectors-SDK-RequiredValueNullException 'RecordPoint.Connectors.SDK.RequiredValueNullException') |  |

<a name='T-RecordPoint-Connectors-SDK-Databases-Cosmos-ICosmosDbDatabaseProvider`1'></a>
## ICosmosDbDatabaseProvider\`1 `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.Cosmos

##### Summary

Defines extensions to the database provider specifically for LocalDb

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TDbContext |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-ICosmosDbDatabaseProvider`1-GetDatabaseLogFilePath'></a>
### GetDatabaseLogFilePath() `method`

##### Summary

Get the path to the database log file

##### Returns

File path

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-ICosmosDbDatabaseProvider`1-GetDatabasePath'></a>
### GetDatabasePath() `method`

##### Summary

Get the path to the database file

##### Returns

File path

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-ICosmosDbManager`1'></a>
## ICosmosDbManager\`1 `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.Cosmos.Manager

##### Summary



##### Generic Types

| Name | Description |
| ---- | ----------- |
| T |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-ICosmosDbManager`1-DeleteAsync-System-String,System-String,System-Threading-CancellationToken-'></a>
### DeleteAsync(partitionKey,id,cancellationToken) `method`

##### Summary

Deletes Cosmos DB Item

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| partitionKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| id | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-ICosmosDbManager`1-GetAsync-System-String,System-String,System-Threading-CancellationToken-'></a>
### GetAsync(partitionKey,id,cancellationToken) `method`

##### Summary

Get Item Async from cosmos

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| partitionKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| id | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-ICosmosDbManager`1-GetContainerQuery'></a>
### GetContainerQuery() `method`

##### Summary

Get Container query for DIY queries.

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-ICosmosDbManager`1-GetFeedIterator-System-Linq-IQueryable{`0}-'></a>
### GetFeedIterator(query) `method`

##### Summary

Passing a query and returns a Feed Iterator

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| query | [System.Linq.IQueryable{\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.IQueryable 'System.Linq.IQueryable{`0}') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-ICosmosDbManager`1-QueryLinqAsync-System-Linq-IQueryable{`0}-'></a>
### QueryLinqAsync(query) `method`

##### Summary

Passing a Linq query to this will execute it and return results.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| query | [System.Linq.IQueryable{\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.IQueryable 'System.Linq.IQueryable{`0}') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-ICosmosDbManager`1-QuerySqlAsync-System-String,Microsoft-Azure-Cosmos-QueryDefinition,System-Threading-CancellationToken-'></a>
### QuerySqlAsync(partitionKey,query,cancellationToken) `method`

##### Summary

Passing an SQL query to this will execute it and will yield return it

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| partitionKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| query | [Microsoft.Azure.Cosmos.QueryDefinition](#T-Microsoft-Azure-Cosmos-QueryDefinition 'Microsoft.Azure.Cosmos.QueryDefinition') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-ICosmosDbManager`1-ReplaceAsync-System-String,`0-'></a>
### ReplaceAsync(partitionKey,item) `method`

##### Summary

Replaces an existing cosmos item

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| partitionKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| item | [\`0](#T-`0 '`0') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-ICosmosDbManager`1-UpsertAsync-System-String,`0,System-Threading-CancellationToken-'></a>
### UpsertAsync(partitionKey,item,cancellationToken) `method`

##### Summary

Update or create item Async

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| partitionKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| item | [\`0](#T-`0 '`0') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-Cosmos-Manager-ICosmosDbManager`1-UpsertMatchingEtagAsync-System-String,`0-'></a>
### UpsertMatchingEtagAsync(partitionKey,item) `method`

##### Summary

Upserts based on Etag

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| partitionKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| item | [\`0](#T-`0 '`0') |  |

<a name='T-RecordPoint-Connectors-SDK-Databases-Cosmos-SemephoreLock-SemaphoreLockCosmosDbItem'></a>
## SemaphoreLockCosmosDbItem `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.Cosmos.SemephoreLock

##### Summary

Represents a semaphore lock entry in a Cosmos Container

<a name='F-RecordPoint-Connectors-SDK-Databases-Cosmos-SemephoreLock-SemaphoreLockCosmosDbItem-COSMOS_DB_CONTAINER_NAME'></a>
### COSMOS_DB_CONTAINER_NAME `constants`

##### Summary

The container name used for Semaphore Locks within the Cosmos Database

<a name='P-RecordPoint-Connectors-SDK-Databases-Cosmos-SemephoreLock-SemaphoreLockCosmosDbItem-LockExpiry'></a>
### LockExpiry `property`

##### Summary

The expiry time of the semaphore lock
