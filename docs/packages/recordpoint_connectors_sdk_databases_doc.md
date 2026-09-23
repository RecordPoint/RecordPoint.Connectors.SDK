<a name='assembly'></a>
# RecordPoint.Connectors.SDK.Databases

## Contents

- [ChannelExistsCacheAction](#T-RecordPoint-Connectors-SDK-Databases-Caching-ChannelExistsCacheAction 'RecordPoint.Connectors.SDK.Databases.Caching.ChannelExistsCacheAction')
  - [#ctor(databaseClient,observabilityScope)](#M-RecordPoint-Connectors-SDK-Databases-Caching-ChannelExistsCacheAction-#ctor-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseClient,RecordPoint-Connectors-SDK-Observability-IObservabilityScope- 'RecordPoint.Connectors.SDK.Databases.Caching.ChannelExistsCacheAction.#ctor(RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseClient,RecordPoint.Connectors.SDK.Observability.IObservabilityScope)')
  - [ExecuteAsync(context,cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-Caching-ChannelExistsCacheAction-ExecuteAsync-RecordPoint-Connectors-SDK-Caching-CacheActionContext,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.Caching.ChannelExistsCacheAction.ExecuteAsync(RecordPoint.Connectors.SDK.Caching.CacheActionContext,System.Threading.CancellationToken)')
- [CommonSqlDbProvider\`1](#T-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1 'RecordPoint.Connectors.SDK.Databases.CommonSqlDbProvider`1')
  - [#ctor(systemContext,telemetryTracker)](#M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-#ctor-RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker- 'RecordPoint.Connectors.SDK.Databases.CommonSqlDbProvider`1.#ctor(RecordPoint.Connectors.SDK.Context.ISystemContext,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker)')
  - [_readySource](#F-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-_readySource 'RecordPoint.Connectors.SDK.Databases.CommonSqlDbProvider`1._readySource')
  - [_systemContext](#F-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-_systemContext 'RecordPoint.Connectors.SDK.Databases.CommonSqlDbProvider`1._systemContext')
  - [_telemetryTracker](#F-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-_telemetryTracker 'RecordPoint.Connectors.SDK.Databases.CommonSqlDbProvider`1._telemetryTracker')
  - [CheckDatabaseExists(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-CheckDatabaseExists-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.CommonSqlDbProvider`1.CheckDatabaseExists(System.Threading.CancellationToken)')
  - [CleanupAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-CleanupAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.CommonSqlDbProvider`1.CleanupAsync(System.Threading.CancellationToken)')
  - [CreateDbAdminContext()](#M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-CreateDbAdminContext 'RecordPoint.Connectors.SDK.Databases.CommonSqlDbProvider`1.CreateDbAdminContext')
  - [CreateDbContext()](#M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-CreateDbContext 'RecordPoint.Connectors.SDK.Databases.CommonSqlDbProvider`1.CreateDbContext')
  - [Exists()](#M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-Exists 'RecordPoint.Connectors.SDK.Databases.CommonSqlDbProvider`1.Exists')
  - [GetAdminConnectionString()](#M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-GetAdminConnectionString 'RecordPoint.Connectors.SDK.Databases.CommonSqlDbProvider`1.GetAdminConnectionString')
  - [GetAdminContextOptionsBuilder()](#M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-GetAdminContextOptionsBuilder 'RecordPoint.Connectors.SDK.Databases.CommonSqlDbProvider`1.GetAdminContextOptionsBuilder')
  - [GetConnectionString()](#M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-GetConnectionString 'RecordPoint.Connectors.SDK.Databases.CommonSqlDbProvider`1.GetConnectionString')
  - [GetContextOptionsBuilder()](#M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-GetContextOptionsBuilder 'RecordPoint.Connectors.SDK.Databases.CommonSqlDbProvider`1.GetContextOptionsBuilder')
  - [GetDatabaseName()](#M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-GetDatabaseName 'RecordPoint.Connectors.SDK.Databases.CommonSqlDbProvider`1.GetDatabaseName')
  - [GetExternalSystemName()](#M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-GetExternalSystemName 'RecordPoint.Connectors.SDK.Databases.CommonSqlDbProvider`1.GetExternalSystemName')
  - [GetSqlDatabaseScript(scriptName,parameters)](#M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-GetSqlDatabaseScript-System-String,System-Collections-Generic-Dictionary{System-String,System-String}- 'RecordPoint.Connectors.SDK.Databases.CommonSqlDbProvider`1.GetSqlDatabaseScript(System.String,System.Collections.Generic.Dictionary{System.String,System.String})')
  - [PrepareAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-PrepareAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.CommonSqlDbProvider`1.PrepareAsync(System.Threading.CancellationToken)')
  - [ReadyAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-ReadyAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.CommonSqlDbProvider`1.ReadyAsync(System.Threading.CancellationToken)')
  - [RemoveAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-RemoveAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.CommonSqlDbProvider`1.RemoveAsync(System.Threading.CancellationToken)')
  - [SetReady(exception)](#M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-SetReady-System-Exception- 'RecordPoint.Connectors.SDK.Databases.CommonSqlDbProvider`1.SetReady(System.Exception)')
- [ConnectorConfigurationBuilderExtensions](#T-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationBuilderExtensions 'RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationBuilderExtensions')
  - [UseDatabaseConnectorConfigurationManager(hostBuilder)](#M-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationBuilderExtensions-UseDatabaseConnectorConfigurationManager-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationBuilderExtensions.UseDatabaseConnectorConfigurationManager(Microsoft.Extensions.Hosting.IHostBuilder)')
- [ConnectorConfigurationCacheAction](#T-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationCacheAction 'RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationCacheAction')
  - [#ctor(databaseClient,observabilityScope,connectorOptions)](#M-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationCacheAction-#ctor-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseClient,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Connectors-ConnectorOptions}- 'RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationCacheAction.#ctor(RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseClient,RecordPoint.Connectors.SDK.Observability.IObservabilityScope,Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Connectors.ConnectorOptions})')
  - [ExecuteAsync(context,cancellationToken)](#M-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationCacheAction-ExecuteAsync-RecordPoint-Connectors-SDK-Caching-CacheActionContext,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationCacheAction.ExecuteAsync(RecordPoint.Connectors.SDK.Caching.CacheActionContext,System.Threading.CancellationToken)')
- [ConnectorDatabaseClient](#T-RecordPoint-Connectors-SDK-Databases-ConnectorDatabaseClient 'RecordPoint.Connectors.SDK.Databases.ConnectorDatabaseClient')
  - [#ctor(databaseProvider)](#M-RecordPoint-Connectors-SDK-Databases-ConnectorDatabaseClient-#ctor-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseProvider- 'RecordPoint.Connectors.SDK.Databases.ConnectorDatabaseClient.#ctor(RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseProvider)')
- [ConnectorDatabaseException](#T-RecordPoint-Connectors-SDK-Databases-ConnectorDatabaseException 'RecordPoint.Connectors.SDK.Databases.ConnectorDatabaseException')
  - [#ctor(message)](#M-RecordPoint-Connectors-SDK-Databases-ConnectorDatabaseException-#ctor-System-String- 'RecordPoint.Connectors.SDK.Databases.ConnectorDatabaseException.#ctor(System.String)')
- [ConnectorDatabaseOptions](#T-RecordPoint-Connectors-SDK-Databases-ConnectorDatabaseOptions 'RecordPoint.Connectors.SDK.Databases.ConnectorDatabaseOptions')
  - [DEFAULT_DATABASE_NAME](#F-RecordPoint-Connectors-SDK-Databases-ConnectorDatabaseOptions-DEFAULT_DATABASE_NAME 'RecordPoint.Connectors.SDK.Databases.ConnectorDatabaseOptions.DEFAULT_DATABASE_NAME')
  - [DatabaseName](#P-RecordPoint-Connectors-SDK-Databases-ConnectorDatabaseOptions-DatabaseName 'RecordPoint.Connectors.SDK.Databases.ConnectorDatabaseOptions.DatabaseName')
- [ConnectorDbContext](#T-RecordPoint-Connectors-SDK-Databases-ConnectorDbContext 'RecordPoint.Connectors.SDK.Databases.ConnectorDbContext')
  - [#ctor(options)](#M-RecordPoint-Connectors-SDK-Databases-ConnectorDbContext-#ctor-Microsoft-EntityFrameworkCore-DbContextOptions{RecordPoint-Connectors-SDK-Databases-ConnectorDbContext}- 'RecordPoint.Connectors.SDK.Databases.ConnectorDbContext.#ctor(Microsoft.EntityFrameworkCore.DbContextOptions{RecordPoint.Connectors.SDK.Databases.ConnectorDbContext})')
  - [Aggregations](#P-RecordPoint-Connectors-SDK-Databases-ConnectorDbContext-Aggregations 'RecordPoint.Connectors.SDK.Databases.ConnectorDbContext.Aggregations')
  - [Channels](#P-RecordPoint-Connectors-SDK-Databases-ConnectorDbContext-Channels 'RecordPoint.Connectors.SDK.Databases.ConnectorDbContext.Channels')
  - [Connectors](#P-RecordPoint-Connectors-SDK-Databases-ConnectorDbContext-Connectors 'RecordPoint.Connectors.SDK.Databases.ConnectorDbContext.Connectors')
  - [ManagedWorkStatuses](#P-RecordPoint-Connectors-SDK-Databases-ConnectorDbContext-ManagedWorkStatuses 'RecordPoint.Connectors.SDK.Databases.ConnectorDbContext.ManagedWorkStatuses')
  - [GetSchema()](#M-RecordPoint-Connectors-SDK-Databases-ConnectorDbContext-GetSchema 'RecordPoint.Connectors.SDK.Databases.ConnectorDbContext.GetSchema')
  - [MapAggregations(modelBuilder)](#M-RecordPoint-Connectors-SDK-Databases-ConnectorDbContext-MapAggregations-Microsoft-EntityFrameworkCore-ModelBuilder- 'RecordPoint.Connectors.SDK.Databases.ConnectorDbContext.MapAggregations(Microsoft.EntityFrameworkCore.ModelBuilder)')
  - [MapChannels(modelBuilder)](#M-RecordPoint-Connectors-SDK-Databases-ConnectorDbContext-MapChannels-Microsoft-EntityFrameworkCore-ModelBuilder- 'RecordPoint.Connectors.SDK.Databases.ConnectorDbContext.MapChannels(Microsoft.EntityFrameworkCore.ModelBuilder)')
  - [MapManagedWorkStatuses(modelBuilder)](#M-RecordPoint-Connectors-SDK-Databases-ConnectorDbContext-MapManagedWorkStatuses-Microsoft-EntityFrameworkCore-ModelBuilder- 'RecordPoint.Connectors.SDK.Databases.ConnectorDbContext.MapManagedWorkStatuses(Microsoft.EntityFrameworkCore.ModelBuilder)')
  - [OnModelCreating(modelBuilder)](#M-RecordPoint-Connectors-SDK-Databases-ConnectorDbContext-OnModelCreating-Microsoft-EntityFrameworkCore-ModelBuilder- 'RecordPoint.Connectors.SDK.Databases.ConnectorDbContext.OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder)')
- [DatabaseAggregationManager](#T-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManager 'RecordPoint.Connectors.SDK.Content.DatabaseAggregationManager')
  - [#ctor(databaseClient,observabilityScope)](#M-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManager-#ctor-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseClient,RecordPoint-Connectors-SDK-Observability-IObservabilityScope- 'RecordPoint.Connectors.SDK.Content.DatabaseAggregationManager.#ctor(RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseClient,RecordPoint.Connectors.SDK.Observability.IObservabilityScope)')
  - [CONNECTOR_ID_DIMENSION](#F-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManager-CONNECTOR_ID_DIMENSION 'RecordPoint.Connectors.SDK.Content.DatabaseAggregationManager.CONNECTOR_ID_DIMENSION')
  - [AggregationExistsAsync()](#M-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManager-AggregationExistsAsync-System-String,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.DatabaseAggregationManager.AggregationExistsAsync(System.String,System.String,System.Threading.CancellationToken)')
  - [GetAggregationAsync()](#M-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManager-GetAggregationAsync-System-String,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.DatabaseAggregationManager.GetAggregationAsync(System.String,System.String,System.Threading.CancellationToken)')
  - [GetAggregationsAsync()](#M-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManager-GetAggregationsAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.DatabaseAggregationManager.GetAggregationsAsync(System.Threading.CancellationToken)')
  - [GetAggregationsAsync()](#M-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManager-GetAggregationsAsync-System-Linq-Expressions-Expression{System-Func{RecordPoint-Connectors-SDK-Content-AggregationModel,System-Boolean}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.DatabaseAggregationManager.GetAggregationsAsync(System.Linq.Expressions.Expression{System.Func{RecordPoint.Connectors.SDK.Content.AggregationModel,System.Boolean}},System.Threading.CancellationToken)')
  - [GetAggregationsAsync()](#M-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManager-GetAggregationsAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.DatabaseAggregationManager.GetAggregationsAsync(System.String,System.Threading.CancellationToken)')
  - [RemoveAggregationAsync()](#M-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManager-RemoveAggregationAsync-System-String,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.DatabaseAggregationManager.RemoveAggregationAsync(System.String,System.String,System.Threading.CancellationToken)')
  - [RemoveAggregationsAsync()](#M-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManager-RemoveAggregationsAsync-System-String,System-String[],System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.DatabaseAggregationManager.RemoveAggregationsAsync(System.String,System.String[],System.Threading.CancellationToken)')
  - [RemoveAggregationsAsync()](#M-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManager-RemoveAggregationsAsync-System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Content-AggregationModel},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.DatabaseAggregationManager.RemoveAggregationsAsync(System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Content.AggregationModel},System.Threading.CancellationToken)')
  - [UpsertAggregationAsync()](#M-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManager-UpsertAggregationAsync-RecordPoint-Connectors-SDK-Content-AggregationModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.DatabaseAggregationManager.UpsertAggregationAsync(RecordPoint.Connectors.SDK.Content.AggregationModel,System.Threading.CancellationToken)')
  - [UpsertAggregationsAsync()](#M-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManager-UpsertAggregationsAsync-System-Collections-Generic-List{RecordPoint-Connectors-SDK-Content-AggregationModel},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.DatabaseAggregationManager.UpsertAggregationsAsync(System.Collections.Generic.List{RecordPoint.Connectors.SDK.Content.AggregationModel},System.Threading.CancellationToken)')
- [DatabaseAggregationManagerBuilderExtensions](#T-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManagerBuilderExtensions 'RecordPoint.Connectors.SDK.Content.DatabaseAggregationManagerBuilderExtensions')
  - [UseDatabaseAggregationManager(hostBuilder)](#M-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManagerBuilderExtensions-UseDatabaseAggregationManager-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Content.DatabaseAggregationManagerBuilderExtensions.UseDatabaseAggregationManager(Microsoft.Extensions.Hosting.IHostBuilder)')
- [DatabaseBuilderExtensions](#T-RecordPoint-Connectors-SDK-Databases-DatabaseBuilderExtensions 'RecordPoint.Connectors.SDK.Databases.DatabaseBuilderExtensions')
  - [AddDatabaseService(hostBuilder)](#M-RecordPoint-Connectors-SDK-Databases-DatabaseBuilderExtensions-AddDatabaseService-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Databases.DatabaseBuilderExtensions.AddDatabaseService(Microsoft.Extensions.Hosting.IHostBuilder)')
- [DatabaseChannelManager](#T-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager 'RecordPoint.Connectors.SDK.Content.DatabaseChannelManager')
  - [#ctor(databaseClient,observabilityScope,channelExistsCache,directChannelAccess)](#M-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager-#ctor-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseClient,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Caching-ICache{System-Boolean},RecordPoint-Connectors-SDK-Content-IDirectChannelAccess- 'RecordPoint.Connectors.SDK.Content.DatabaseChannelManager.#ctor(RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseClient,RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Caching.ICache{System.Boolean},RecordPoint.Connectors.SDK.Content.IDirectChannelAccess)')
  - [CONNECTOR_ID_DIMENSION](#F-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager-CONNECTOR_ID_DIMENSION 'RecordPoint.Connectors.SDK.Content.DatabaseChannelManager.CONNECTOR_ID_DIMENSION')
  - [ChannelExistsAsync()](#M-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager-ChannelExistsAsync-System-String,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.DatabaseChannelManager.ChannelExistsAsync(System.String,System.String,System.Threading.CancellationToken)')
  - [GetChannelAsync()](#M-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager-GetChannelAsync-System-String,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.DatabaseChannelManager.GetChannelAsync(System.String,System.String,System.Threading.CancellationToken)')
  - [GetChannelClassificationsAsync()](#M-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager-GetChannelClassificationsAsync-System-String,System-Int32,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.DatabaseChannelManager.GetChannelClassificationsAsync(System.String,System.Int32,System.Threading.CancellationToken)')
  - [GetChannelsAsync()](#M-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager-GetChannelsAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.DatabaseChannelManager.GetChannelsAsync(System.Threading.CancellationToken)')
  - [GetChannelsAsync()](#M-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager-GetChannelsAsync-System-Linq-Expressions-Expression{System-Func{RecordPoint-Connectors-SDK-Content-ChannelModel,System-Boolean}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.DatabaseChannelManager.GetChannelsAsync(System.Linq.Expressions.Expression{System.Func{RecordPoint.Connectors.SDK.Content.ChannelModel,System.Boolean}},System.Threading.CancellationToken)')
  - [GetChannelsAsync()](#M-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager-GetChannelsAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.DatabaseChannelManager.GetChannelsAsync(System.String,System.Threading.CancellationToken)')
  - [PatchChannelAsync()](#M-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager-PatchChannelAsync-System-String,System-String,System-Action{RecordPoint-Connectors-SDK-Content-ChannelModel},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.DatabaseChannelManager.PatchChannelAsync(System.String,System.String,System.Action{RecordPoint.Connectors.SDK.Content.ChannelModel},System.Threading.CancellationToken)')
  - [RemoveChannelAsync()](#M-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager-RemoveChannelAsync-System-String,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.DatabaseChannelManager.RemoveChannelAsync(System.String,System.String,System.Threading.CancellationToken)')
  - [RemoveChannelsAsync()](#M-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager-RemoveChannelsAsync-System-String,System-String[],System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.DatabaseChannelManager.RemoveChannelsAsync(System.String,System.String[],System.Threading.CancellationToken)')
  - [RemoveChannelsAsync()](#M-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager-RemoveChannelsAsync-System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Content-ChannelModel},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.DatabaseChannelManager.RemoveChannelsAsync(System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Content.ChannelModel},System.Threading.CancellationToken)')
  - [UpsertChannelAsync()](#M-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager-UpsertChannelAsync-RecordPoint-Connectors-SDK-Content-ChannelModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.DatabaseChannelManager.UpsertChannelAsync(RecordPoint.Connectors.SDK.Content.ChannelModel,System.Threading.CancellationToken)')
  - [UpsertChannelsAsync()](#M-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager-UpsertChannelsAsync-System-Collections-Generic-List{RecordPoint-Connectors-SDK-Content-ChannelModel},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.DatabaseChannelManager.UpsertChannelsAsync(System.Collections.Generic.List{RecordPoint.Connectors.SDK.Content.ChannelModel},System.Threading.CancellationToken)')
- [DatabaseChannelManagerBuilderExtensions](#T-RecordPoint-Connectors-SDK-Content-DatabaseChannelManagerBuilderExtensions 'RecordPoint.Connectors.SDK.Content.DatabaseChannelManagerBuilderExtensions')
  - [UseDatabaseChannelManager(hostBuilder)](#M-RecordPoint-Connectors-SDK-Content-DatabaseChannelManagerBuilderExtensions-UseDatabaseChannelManager-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Content.DatabaseChannelManagerBuilderExtensions.UseDatabaseChannelManager(Microsoft.Extensions.Hosting.IHostBuilder)')
- [DatabaseConnectorConfigurationManager](#T-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager')
  - [#ctor(databaseClient,observabilityScope,connectorOptions,systemContext,toggleProvider,connectorConfigurationCache)](#M-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-#ctor-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseClient,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Connectors-ConnectorOptions},RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Toggles-IToggleProvider,RecordPoint-Connectors-SDK-Caching-ICache{RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel}- 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager.#ctor(RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseClient,RecordPoint.Connectors.SDK.Observability.IObservabilityScope,Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Connectors.ConnectorOptions},RecordPoint.Connectors.SDK.Context.ISystemContext,RecordPoint.Connectors.SDK.Toggles.IToggleProvider,RecordPoint.Connectors.SDK.Caching.ICache{RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel})')
  - [BINARY_APPSETTING_OFF_REASON](#F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-BINARY_APPSETTING_OFF_REASON 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager.BINARY_APPSETTING_OFF_REASON')
  - [BINARY_SUBMISSION_ENABLED_REASON](#F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-BINARY_SUBMISSION_ENABLED_REASON 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager.BINARY_SUBMISSION_ENABLED_REASON')
  - [BINARY_SUBMISSION_FEATURE](#F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-BINARY_SUBMISSION_FEATURE 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager.BINARY_SUBMISSION_FEATURE')
  - [CONNECTOR_DISABLED_REASON](#F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-CONNECTOR_DISABLED_REASON 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager.CONNECTOR_DISABLED_REASON')
  - [CONNECTOR_ENABLED_REASON](#F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-CONNECTOR_ENABLED_REASON 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager.CONNECTOR_ENABLED_REASON')
  - [CONNECTOR_FEATURE](#F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-CONNECTOR_FEATURE 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager.CONNECTOR_FEATURE')
  - [CONNECTOR_FEATURE_DISABLED_REASON](#F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-CONNECTOR_FEATURE_DISABLED_REASON 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager.CONNECTOR_FEATURE_DISABLED_REASON')
  - [CONNECTOR_ID_DIMENSION](#F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-CONNECTOR_ID_DIMENSION 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager.CONNECTOR_ID_DIMENSION')
  - [CONNECTOR_NOT_FOUND_REASON](#F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-CONNECTOR_NOT_FOUND_REASON 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager.CONNECTOR_NOT_FOUND_REASON')
  - [DISABLED](#F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-DISABLED 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager.DISABLED')
  - [DISABLED_TIME](#F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-DISABLED_TIME 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager.DISABLED_TIME')
  - [ENABLED](#F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-ENABLED 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager.ENABLED')
  - [SUBMISSION_APPSETTING_OFF_REASON](#F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-SUBMISSION_APPSETTING_OFF_REASON 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager.SUBMISSION_APPSETTING_OFF_REASON')
  - [SUBMISSION_ENABLED_REASON](#F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-SUBMISSION_ENABLED_REASON 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager.SUBMISSION_ENABLED_REASON')
  - [SUBMISSION_FEATURE](#F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-SUBMISSION_FEATURE 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager.SUBMISSION_FEATURE')
  - [SUBMISSION_KILLSWITCH_ON_REASON](#F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-SUBMISSION_KILLSWITCH_ON_REASON 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager.SUBMISSION_KILLSWITCH_ON_REASON')
  - [_connectorConfigurationCache](#F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-_connectorConfigurationCache 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager._connectorConfigurationCache')
  - [_connectorOptions](#F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-_connectorOptions 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager._connectorOptions')
  - [_databaseClient](#F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-_databaseClient 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager._databaseClient')
  - [_observabilityScope](#F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-_observabilityScope 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager._observabilityScope')
  - [_systemContext](#F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-_systemContext 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager._systemContext')
  - [_toggleProvider](#F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-_toggleProvider 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager._toggleProvider')
  - [ConnectorConfigurationExistsAsync(connectorId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-ConnectorConfigurationExistsAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager.ConnectorConfigurationExistsAsync(System.String,System.Threading.CancellationToken)')
  - [DeleteConnectorConfigurationAsync(connectorId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-DeleteConnectorConfigurationAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager.DeleteConnectorConfigurationAsync(System.String,System.Threading.CancellationToken)')
  - [GetAllConnectorConfigurationsAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-GetAllConnectorConfigurationsAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager.GetAllConnectorConfigurationsAsync(System.Threading.CancellationToken)')
  - [GetBinarySubmissionStatusAsync(connectorId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-GetBinarySubmissionStatusAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager.GetBinarySubmissionStatusAsync(System.String,System.Threading.CancellationToken)')
  - [GetConnectorConfigurationAsync(connectorId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-GetConnectorConfigurationAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager.GetConnectorConfigurationAsync(System.String,System.Threading.CancellationToken)')
  - [GetConnectorStatusAsync(connectorId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-GetConnectorStatusAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager.GetConnectorStatusAsync(System.String,System.Threading.CancellationToken)')
  - [GetDimensions(connectorId)](#M-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-GetDimensions-System-String- 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager.GetDimensions(System.String)')
  - [GetSubmissionStatusAsync(connectorId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-GetSubmissionStatusAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager.GetSubmissionStatusAsync(System.String,System.Threading.CancellationToken)')
  - [PatchConnectorConfigurationAsync()](#M-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-PatchConnectorConfigurationAsync-System-String,System-Action{RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager.PatchConnectorConfigurationAsync(System.String,System.Action{RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel},System.Threading.CancellationToken)')
  - [SetConnectorConfigurationAsync(connectorData,cancellationToken)](#M-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-SetConnectorConfigurationAsync-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager.SetConnectorConfigurationAsync(RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel,System.Threading.CancellationToken)')
- [DatabaseManagedWorkStatusManager](#T-RecordPoint-Connectors-SDK-Work-DatabaseManagedWorkStatusManager 'RecordPoint.Connectors.SDK.Work.DatabaseManagedWorkStatusManager')
  - [#ctor(databaseClient,observabilityScope)](#M-RecordPoint-Connectors-SDK-Work-DatabaseManagedWorkStatusManager-#ctor-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseClient,RecordPoint-Connectors-SDK-Observability-IObservabilityScope- 'RecordPoint.Connectors.SDK.Work.DatabaseManagedWorkStatusManager.#ctor(RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseClient,RecordPoint.Connectors.SDK.Observability.IObservabilityScope)')
  - [WORK_STATUS_ID_DIMENSION](#F-RecordPoint-Connectors-SDK-Work-DatabaseManagedWorkStatusManager-WORK_STATUS_ID_DIMENSION 'RecordPoint.Connectors.SDK.Work.DatabaseManagedWorkStatusManager.WORK_STATUS_ID_DIMENSION')
  - [GetWorkStatusesAsync()](#M-RecordPoint-Connectors-SDK-Work-DatabaseManagedWorkStatusManager-GetWorkStatusesAsync-System-Linq-Expressions-Expression{System-Func{RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel,System-Boolean}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.DatabaseManagedWorkStatusManager.GetWorkStatusesAsync(System.Linq.Expressions.Expression{System.Func{RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel,System.Boolean}},System.Threading.CancellationToken)')
- [DatabaseProviderTelemetryExtensions](#T-RecordPoint-Connectors-SDK-Databases-DatabaseProviderTelemetryExtensions 'RecordPoint.Connectors.SDK.Databases.DatabaseProviderTelemetryExtensions')
  - [WithAddedTelemetry(provider,telemetryTracker)](#M-RecordPoint-Connectors-SDK-Databases-DatabaseProviderTelemetryExtensions-WithAddedTelemetry-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseProvider,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker- 'RecordPoint.Connectors.SDK.Databases.DatabaseProviderTelemetryExtensions.WithAddedTelemetry(RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseProvider,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker)')
- [DatabaseProviderWithTelemetry](#T-RecordPoint-Connectors-SDK-Databases-DatabaseProviderWithTelemetry 'RecordPoint.Connectors.SDK.Databases.DatabaseProviderWithTelemetry')
  - [#ctor(databaseProviderImplementation,telemetryTracker)](#M-RecordPoint-Connectors-SDK-Databases-DatabaseProviderWithTelemetry-#ctor-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseProvider,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker- 'RecordPoint.Connectors.SDK.Databases.DatabaseProviderWithTelemetry.#ctor(RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseProvider,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker)')
  - [_databaseProviderImplementation](#F-RecordPoint-Connectors-SDK-Databases-DatabaseProviderWithTelemetry-_databaseProviderImplementation 'RecordPoint.Connectors.SDK.Databases.DatabaseProviderWithTelemetry._databaseProviderImplementation')
  - [_telemetryTracker](#F-RecordPoint-Connectors-SDK-Databases-DatabaseProviderWithTelemetry-_telemetryTracker 'RecordPoint.Connectors.SDK.Databases.DatabaseProviderWithTelemetry._telemetryTracker')
  - [CleanupAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-DatabaseProviderWithTelemetry-CleanupAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.DatabaseProviderWithTelemetry.CleanupAsync(System.Threading.CancellationToken)')
  - [CreateDbContext()](#M-RecordPoint-Connectors-SDK-Databases-DatabaseProviderWithTelemetry-CreateDbContext 'RecordPoint.Connectors.SDK.Databases.DatabaseProviderWithTelemetry.CreateDbContext')
  - [Exists()](#M-RecordPoint-Connectors-SDK-Databases-DatabaseProviderWithTelemetry-Exists 'RecordPoint.Connectors.SDK.Databases.DatabaseProviderWithTelemetry.Exists')
  - [GetConnectionString()](#M-RecordPoint-Connectors-SDK-Databases-DatabaseProviderWithTelemetry-GetConnectionString 'RecordPoint.Connectors.SDK.Databases.DatabaseProviderWithTelemetry.GetConnectionString')
  - [GetExternalSystemName()](#M-RecordPoint-Connectors-SDK-Databases-DatabaseProviderWithTelemetry-GetExternalSystemName 'RecordPoint.Connectors.SDK.Databases.DatabaseProviderWithTelemetry.GetExternalSystemName')
  - [PrepareAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-DatabaseProviderWithTelemetry-PrepareAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.DatabaseProviderWithTelemetry.PrepareAsync(System.Threading.CancellationToken)')
  - [ReadyAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-DatabaseProviderWithTelemetry-ReadyAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.DatabaseProviderWithTelemetry.ReadyAsync(System.Threading.CancellationToken)')
  - [RemoveAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-DatabaseProviderWithTelemetry-RemoveAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.DatabaseProviderWithTelemetry.RemoveAsync(System.Threading.CancellationToken)')
  - [SetReady(exception)](#M-RecordPoint-Connectors-SDK-Databases-DatabaseProviderWithTelemetry-SetReady-System-Exception- 'RecordPoint.Connectors.SDK.Databases.DatabaseProviderWithTelemetry.SetReady(System.Exception)')
- [DatabaseService\`2](#T-RecordPoint-Connectors-SDK-Databases-DatabaseService`2 'RecordPoint.Connectors.SDK.Databases.DatabaseService`2')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Databases-DatabaseService`2-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-Context-ISystemContext,`1,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider,Microsoft-Extensions-Hosting-IHostApplicationLifetime- 'RecordPoint.Connectors.SDK.Databases.DatabaseService`2.#ctor(System.IServiceProvider,RecordPoint.Connectors.SDK.Context.ISystemContext,`1,RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,RecordPoint.Connectors.SDK.Providers.IDateTimeProvider,Microsoft.Extensions.Hosting.IHostApplicationLifetime)')
  - [ExecuteAsync(stoppingToken)](#M-RecordPoint-Connectors-SDK-Databases-DatabaseService`2-ExecuteAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.DatabaseService`2.ExecuteAsync(System.Threading.CancellationToken)')
  - [PrepareDatabaseAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-DatabaseService`2-PrepareDatabaseAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.DatabaseService`2.PrepareDatabaseAsync(System.Threading.CancellationToken)')
  - [StartAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-DatabaseService`2-StartAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.DatabaseService`2.StartAsync(System.Threading.CancellationToken)')
- [IConnectorDatabaseClient](#T-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseClient 'RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseClient')
- [IConnectorDatabaseProvider](#T-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseProvider 'RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseProvider')
- [IConnectorDbContext](#T-RecordPoint-Connectors-SDK-Databases-IConnectorDbContext 'RecordPoint.Connectors.SDK.Databases.IConnectorDbContext')
  - [Aggregations](#P-RecordPoint-Connectors-SDK-Databases-IConnectorDbContext-Aggregations 'RecordPoint.Connectors.SDK.Databases.IConnectorDbContext.Aggregations')
  - [Channels](#P-RecordPoint-Connectors-SDK-Databases-IConnectorDbContext-Channels 'RecordPoint.Connectors.SDK.Databases.IConnectorDbContext.Channels')
  - [Connectors](#P-RecordPoint-Connectors-SDK-Databases-IConnectorDbContext-Connectors 'RecordPoint.Connectors.SDK.Databases.IConnectorDbContext.Connectors')
  - [ManagedWorkStatuses](#P-RecordPoint-Connectors-SDK-Databases-IConnectorDbContext-ManagedWorkStatuses 'RecordPoint.Connectors.SDK.Databases.IConnectorDbContext.ManagedWorkStatuses')
- [IDatabaseClient\`1](#T-RecordPoint-Connectors-SDK-Databases-IDatabaseClient`1 'RecordPoint.Connectors.SDK.Databases.IDatabaseClient`1')
  - [CreateDbContext()](#M-RecordPoint-Connectors-SDK-Databases-IDatabaseClient`1-CreateDbContext 'RecordPoint.Connectors.SDK.Databases.IDatabaseClient`1.CreateDbContext')
  - [GetExternalSystemName()](#M-RecordPoint-Connectors-SDK-Databases-IDatabaseClient`1-GetExternalSystemName 'RecordPoint.Connectors.SDK.Databases.IDatabaseClient`1.GetExternalSystemName')
  - [ReadyAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-IDatabaseClient`1-ReadyAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.IDatabaseClient`1.ReadyAsync(System.Threading.CancellationToken)')
- [IDatabaseProvider\`1](#T-RecordPoint-Connectors-SDK-Databases-IDatabaseProvider`1 'RecordPoint.Connectors.SDK.Databases.IDatabaseProvider`1')
  - [CleanupAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-IDatabaseProvider`1-CleanupAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.IDatabaseProvider`1.CleanupAsync(System.Threading.CancellationToken)')
  - [CreateDbContext()](#M-RecordPoint-Connectors-SDK-Databases-IDatabaseProvider`1-CreateDbContext 'RecordPoint.Connectors.SDK.Databases.IDatabaseProvider`1.CreateDbContext')
  - [Exists()](#M-RecordPoint-Connectors-SDK-Databases-IDatabaseProvider`1-Exists 'RecordPoint.Connectors.SDK.Databases.IDatabaseProvider`1.Exists')
  - [GetConnectionString()](#M-RecordPoint-Connectors-SDK-Databases-IDatabaseProvider`1-GetConnectionString 'RecordPoint.Connectors.SDK.Databases.IDatabaseProvider`1.GetConnectionString')
  - [GetExternalSystemName()](#M-RecordPoint-Connectors-SDK-Databases-IDatabaseProvider`1-GetExternalSystemName 'RecordPoint.Connectors.SDK.Databases.IDatabaseProvider`1.GetExternalSystemName')
  - [PrepareAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-IDatabaseProvider`1-PrepareAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.IDatabaseProvider`1.PrepareAsync(System.Threading.CancellationToken)')
  - [ReadyAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-IDatabaseProvider`1-ReadyAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.IDatabaseProvider`1.ReadyAsync(System.Threading.CancellationToken)')
  - [RemoveAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-IDatabaseProvider`1-RemoveAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.IDatabaseProvider`1.RemoveAsync(System.Threading.CancellationToken)')
  - [SetReady(exception)](#M-RecordPoint-Connectors-SDK-Databases-IDatabaseProvider`1-SetReady-System-Exception- 'RecordPoint.Connectors.SDK.Databases.IDatabaseProvider`1.SetReady(System.Exception)')
- [IDirectChannelAccess](#T-RecordPoint-Connectors-SDK-Content-IDirectChannelAccess 'RecordPoint.Connectors.SDK.Content.IDirectChannelAccess')
  - [IsEnabled](#P-RecordPoint-Connectors-SDK-Content-IDirectChannelAccess-IsEnabled 'RecordPoint.Connectors.SDK.Content.IDirectChannelAccess.IsEnabled')
  - [ReadChannelAsync(connectorId,externalId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Content-IDirectChannelAccess-ReadChannelAsync-System-String,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.IDirectChannelAccess.ReadChannelAsync(System.String,System.String,System.Threading.CancellationToken)')
  - [ReadChannelClassificationsAsync(connectorId,pageSize,cancellationToken)](#M-RecordPoint-Connectors-SDK-Content-IDirectChannelAccess-ReadChannelClassificationsAsync-System-String,System-Int32,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.IDirectChannelAccess.ReadChannelClassificationsAsync(System.String,System.Int32,System.Threading.CancellationToken)')
- [NullDirectChannelAccess](#T-RecordPoint-Connectors-SDK-Content-NullDirectChannelAccess 'RecordPoint.Connectors.SDK.Content.NullDirectChannelAccess')
  - [IsEnabled](#P-RecordPoint-Connectors-SDK-Content-NullDirectChannelAccess-IsEnabled 'RecordPoint.Connectors.SDK.Content.NullDirectChannelAccess.IsEnabled')
  - [ReadChannelAsync()](#M-RecordPoint-Connectors-SDK-Content-NullDirectChannelAccess-ReadChannelAsync-System-String,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.NullDirectChannelAccess.ReadChannelAsync(System.String,System.String,System.Threading.CancellationToken)')
  - [ReadChannelClassificationsAsync()](#M-RecordPoint-Connectors-SDK-Content-NullDirectChannelAccess-ReadChannelClassificationsAsync-System-String,System-Int32,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.NullDirectChannelAccess.ReadChannelClassificationsAsync(System.String,System.Int32,System.Threading.CancellationToken)')
- [PrepareDatabaseOperation\`2](#T-RecordPoint-Connectors-SDK-Databases-PrepareDatabaseOperation`2 'RecordPoint.Connectors.SDK.Databases.PrepareDatabaseOperation`2')
  - [#ctor(serviceProvider,databaseProvider,observabilityScope,telemetryTracker,dateTimeProvider)](#M-RecordPoint-Connectors-SDK-Databases-PrepareDatabaseOperation`2-#ctor-System-IServiceProvider,`1,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider- 'RecordPoint.Connectors.SDK.Databases.PrepareDatabaseOperation`2.#ctor(System.IServiceProvider,`1,RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,RecordPoint.Connectors.SDK.Providers.IDateTimeProvider)')
  - [PREPARE_DATABASE_WORK_TYPE](#F-RecordPoint-Connectors-SDK-Databases-PrepareDatabaseOperation`2-PREPARE_DATABASE_WORK_TYPE 'RecordPoint.Connectors.SDK.Databases.PrepareDatabaseOperation`2.PREPARE_DATABASE_WORK_TYPE')
  - [_databaseProvider](#F-RecordPoint-Connectors-SDK-Databases-PrepareDatabaseOperation`2-_databaseProvider 'RecordPoint.Connectors.SDK.Databases.PrepareDatabaseOperation`2._databaseProvider')
  - [WorkType](#P-RecordPoint-Connectors-SDK-Databases-PrepareDatabaseOperation`2-WorkType 'RecordPoint.Connectors.SDK.Databases.PrepareDatabaseOperation`2.WorkType')
  - [InnerRunAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-PrepareDatabaseOperation`2-InnerRunAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.PrepareDatabaseOperation`2.InnerRunAsync(System.Threading.CancellationToken)')
- [ProviderDatabaseClient\`2](#T-RecordPoint-Connectors-SDK-Databases-ProviderDatabaseClient`2 'RecordPoint.Connectors.SDK.Databases.ProviderDatabaseClient`2')
  - [#ctor(databaseProvider)](#M-RecordPoint-Connectors-SDK-Databases-ProviderDatabaseClient`2-#ctor-`1- 'RecordPoint.Connectors.SDK.Databases.ProviderDatabaseClient`2.#ctor(`1)')
  - [Provider](#P-RecordPoint-Connectors-SDK-Databases-ProviderDatabaseClient`2-Provider 'RecordPoint.Connectors.SDK.Databases.ProviderDatabaseClient`2.Provider')
  - [CreateDbContext()](#M-RecordPoint-Connectors-SDK-Databases-ProviderDatabaseClient`2-CreateDbContext 'RecordPoint.Connectors.SDK.Databases.ProviderDatabaseClient`2.CreateDbContext')
  - [GetExternalSystemName()](#M-RecordPoint-Connectors-SDK-Databases-ProviderDatabaseClient`2-GetExternalSystemName 'RecordPoint.Connectors.SDK.Databases.ProviderDatabaseClient`2.GetExternalSystemName')
  - [ReadyAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-ProviderDatabaseClient`2-ReadyAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.ProviderDatabaseClient`2.ReadyAsync(System.Threading.CancellationToken)')

<a name='T-RecordPoint-Connectors-SDK-Databases-Caching-ChannelExistsCacheAction'></a>
## ChannelExistsCacheAction `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.Caching

##### Summary

Cache action for checking if a Channel exists based on ConnectorId and ExternalId. This is used to optimize lookups for Channel existence, which can be a common operation in various parts of the system.

<a name='M-RecordPoint-Connectors-SDK-Databases-Caching-ChannelExistsCacheAction-#ctor-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseClient,RecordPoint-Connectors-SDK-Observability-IObservabilityScope-'></a>
### #ctor(databaseClient,observabilityScope) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| databaseClient | [RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseClient](#T-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseClient 'RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseClient') |  |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-Caching-ChannelExistsCacheAction-ExecuteAsync-RecordPoint-Connectors-SDK-Caching-CacheActionContext,System-Threading-CancellationToken-'></a>
### ExecuteAsync(context,cancellationToken) `method`

##### Summary

Get channels matching the ConnectorId and ExternalId from the context.
If any channels are found, it returns true (indicating that the channel exists), otherwise it returns false (indicating that the channel does not exist). 
The result is cached for a specified duration to optimize performance for subsequent lookups with the same parameters.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [RecordPoint.Connectors.SDK.Caching.CacheActionContext](#T-RecordPoint-Connectors-SDK-Caching-CacheActionContext 'RecordPoint.Connectors.SDK.Caching.CacheActionContext') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='T-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1'></a>
## CommonSqlDbProvider\`1 `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases

##### Summary

The common sql db provider.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TDbContext |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-#ctor-RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker-'></a>
### #ctor(systemContext,telemetryTracker) `constructor`

##### Summary

Initializes a new instance of the class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The telemetry tracker. |

<a name='F-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-_readySource'></a>
### _readySource `constants`

##### Summary

The ready source.

<a name='F-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-_systemContext'></a>
### _systemContext `constants`

##### Summary

The system context.

<a name='F-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-_telemetryTracker'></a>
### _telemetryTracker `constants`

##### Summary

The telemetry tracker.

<a name='M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-CheckDatabaseExists-System-Threading-CancellationToken-'></a>
### CheckDatabaseExists(cancellationToken) `method`

##### Summary

Check database exists.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-CleanupAsync-System-Threading-CancellationToken-'></a>
### CleanupAsync(cancellationToken) `method`

##### Summary

Database cleanup

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-CreateDbAdminContext'></a>
### CreateDbAdminContext() `method`

##### Summary

Create a connector db context with admin permissions

##### Returns

Connector db context

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-CreateDbContext'></a>
### CreateDbContext() `method`

##### Summary

Create a connector db context

##### Returns

Connector db context

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-Exists'></a>
### Exists() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-GetAdminConnectionString'></a>
### GetAdminConnectionString() `method`

##### Summary

Required override to get the admin level connection string for the database

##### Returns

Admin level Connection string for the database

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-GetAdminContextOptionsBuilder'></a>
### GetAdminContextOptionsBuilder() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-GetConnectionString'></a>
### GetConnectionString() `method`

##### Summary

Required override to get the service level connection string for the database

##### Returns

Connection string for the database

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-GetContextOptionsBuilder'></a>
### GetContextOptionsBuilder() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-GetDatabaseName'></a>
### GetDatabaseName() `method`

##### Summary

Required override to get the database name

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-GetExternalSystemName'></a>
### GetExternalSystemName() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-GetSqlDatabaseScript-System-String,System-Collections-Generic-Dictionary{System-String,System-String}-'></a>
### GetSqlDatabaseScript(scriptName,parameters) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| scriptName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| parameters | [System.Collections.Generic.Dictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.String}') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-PrepareAsync-System-Threading-CancellationToken-'></a>
### PrepareAsync(cancellationToken) `method`

##### Summary

Performs any initialisation for the database such as attaching the DB file, and/or running EF migrations

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-ReadyAsync-System-Threading-CancellationToken-'></a>
### ReadyAsync(cancellationToken) `method`

##### Summary

Called by dependent services to determine if the database is ready for operation.

##### Returns

Readiness Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-RemoveAsync-System-Threading-CancellationToken-'></a>
### RemoveAsync(cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-CommonSqlDbProvider`1-SetReady-System-Exception-'></a>
### SetReady(exception) `method`

##### Summary

Set the readiness for this service

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| exception | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Exception that indicates that the service has critically failed |

<a name='T-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationBuilderExtensions'></a>
## ConnectorConfigurationBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Connectors

##### Summary

Host builder extensions for the connectors service

<a name='M-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationBuilderExtensions-UseDatabaseConnectorConfigurationManager-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseDatabaseConnectorConfigurationManager(hostBuilder) `method`

##### Summary

Use the connector management services

##### Returns

Updated host builder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | Host builder to configure |

<a name='T-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationCacheAction'></a>
## ConnectorConfigurationCacheAction `type`

##### Namespace

RecordPoint.Connectors.SDK.Connectors

##### Summary

Cache action for retrieving a single connector configuration by ConnectorId.
The cache TTL is driven by [ConnectorConfigurationCacheTtl](#P-RecordPoint-Connectors-SDK-Connectors-ConnectorOptions-ConnectorConfigurationCacheTtl 'RecordPoint.Connectors.SDK.Connectors.ConnectorOptions.ConnectorConfigurationCacheTtl').

<a name='M-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationCacheAction-#ctor-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseClient,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Connectors-ConnectorOptions}-'></a>
### #ctor(databaseClient,observabilityScope,connectorOptions) `constructor`

##### Summary

Initializes a new instance of the [ConnectorConfigurationCacheAction](#T-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationCacheAction 'RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationCacheAction') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| databaseClient | [RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseClient](#T-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseClient 'RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseClient') | The database client. |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | The observability scope. |
| connectorOptions | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Connectors.ConnectorOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Connectors-ConnectorOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Connectors.ConnectorOptions}') | The connector options. |

<a name='M-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationCacheAction-ExecuteAsync-RecordPoint-Connectors-SDK-Caching-CacheActionContext,System-Threading-CancellationToken-'></a>
### ExecuteAsync(context,cancellationToken) `method`

##### Summary

Fetches the connector configuration from the database and returns it with a TTL-based expiry.

##### Returns

A [CacheActionResult\`1](#T-RecordPoint-Connectors-SDK-Caching-CacheActionResult`1 'RecordPoint.Connectors.SDK.Caching.CacheActionResult`1') with the configuration and expiry.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [RecordPoint.Connectors.SDK.Caching.CacheActionContext](#T-RecordPoint-Connectors-SDK-Caching-CacheActionContext 'RecordPoint.Connectors.SDK.Caching.CacheActionContext') | Cache action context containing the ConnectorId property. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='T-RecordPoint-Connectors-SDK-Databases-ConnectorDatabaseClient'></a>
## ConnectorDatabaseClient `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases

##### Summary

Connector database client

<a name='M-RecordPoint-Connectors-SDK-Databases-ConnectorDatabaseClient-#ctor-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseProvider-'></a>
### #ctor(databaseProvider) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| databaseProvider | [RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseProvider](#T-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseProvider 'RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseProvider') |  |

<a name='T-RecordPoint-Connectors-SDK-Databases-ConnectorDatabaseException'></a>
## ConnectorDatabaseException `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases

##### Summary

The connector database exception.

<a name='M-RecordPoint-Connectors-SDK-Databases-ConnectorDatabaseException-#ctor-System-String-'></a>
### #ctor(message) `constructor`

##### Summary

Initializes a new instance of the [ConnectorDatabaseException](#T-RecordPoint-Connectors-SDK-Databases-ConnectorDatabaseException 'RecordPoint.Connectors.SDK.Databases.ConnectorDatabaseException') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message. |

<a name='T-RecordPoint-Connectors-SDK-Databases-ConnectorDatabaseOptions'></a>
## ConnectorDatabaseOptions `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases

##### Summary

Connector Database Options

<a name='F-RecordPoint-Connectors-SDK-Databases-ConnectorDatabaseOptions-DEFAULT_DATABASE_NAME'></a>
### DEFAULT_DATABASE_NAME `constants`

##### Summary

Default database name

<a name='P-RecordPoint-Connectors-SDK-Databases-ConnectorDatabaseOptions-DatabaseName'></a>
### DatabaseName `property`

##### Summary

Name of the database

<a name='T-RecordPoint-Connectors-SDK-Databases-ConnectorDbContext'></a>
## ConnectorDbContext `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases

##### Summary

Connectors Entity framework DB Context Base class

##### Remarks

Concrete instances of this class are created by the Provider module

<a name='M-RecordPoint-Connectors-SDK-Databases-ConnectorDbContext-#ctor-Microsoft-EntityFrameworkCore-DbContextOptions{RecordPoint-Connectors-SDK-Databases-ConnectorDbContext}-'></a>
### #ctor(options) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| options | [Microsoft.EntityFrameworkCore.DbContextOptions{RecordPoint.Connectors.SDK.Databases.ConnectorDbContext}](#T-Microsoft-EntityFrameworkCore-DbContextOptions{RecordPoint-Connectors-SDK-Databases-ConnectorDbContext} 'Microsoft.EntityFrameworkCore.DbContextOptions{RecordPoint.Connectors.SDK.Databases.ConnectorDbContext}') |  |

<a name='P-RecordPoint-Connectors-SDK-Databases-ConnectorDbContext-Aggregations'></a>
### Aggregations `property`

##### Summary

*Inherit from parent.*

<a name='P-RecordPoint-Connectors-SDK-Databases-ConnectorDbContext-Channels'></a>
### Channels `property`

##### Summary

*Inherit from parent.*

<a name='P-RecordPoint-Connectors-SDK-Databases-ConnectorDbContext-Connectors'></a>
### Connectors `property`

##### Summary

*Inherit from parent.*

<a name='P-RecordPoint-Connectors-SDK-Databases-ConnectorDbContext-ManagedWorkStatuses'></a>
### ManagedWorkStatuses `property`

##### Summary

*Inherit from parent.*

<a name='M-RecordPoint-Connectors-SDK-Databases-ConnectorDbContext-GetSchema'></a>
### GetSchema() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-ConnectorDbContext-MapAggregations-Microsoft-EntityFrameworkCore-ModelBuilder-'></a>
### MapAggregations(modelBuilder) `method`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| modelBuilder | [Microsoft.EntityFrameworkCore.ModelBuilder](#T-Microsoft-EntityFrameworkCore-ModelBuilder 'Microsoft.EntityFrameworkCore.ModelBuilder') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-ConnectorDbContext-MapChannels-Microsoft-EntityFrameworkCore-ModelBuilder-'></a>
### MapChannels(modelBuilder) `method`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| modelBuilder | [Microsoft.EntityFrameworkCore.ModelBuilder](#T-Microsoft-EntityFrameworkCore-ModelBuilder 'Microsoft.EntityFrameworkCore.ModelBuilder') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-ConnectorDbContext-MapManagedWorkStatuses-Microsoft-EntityFrameworkCore-ModelBuilder-'></a>
### MapManagedWorkStatuses(modelBuilder) `method`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| modelBuilder | [Microsoft.EntityFrameworkCore.ModelBuilder](#T-Microsoft-EntityFrameworkCore-ModelBuilder 'Microsoft.EntityFrameworkCore.ModelBuilder') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-ConnectorDbContext-OnModelCreating-Microsoft-EntityFrameworkCore-ModelBuilder-'></a>
### OnModelCreating(modelBuilder) `method`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| modelBuilder | [Microsoft.EntityFrameworkCore.ModelBuilder](#T-Microsoft-EntityFrameworkCore-ModelBuilder 'Microsoft.EntityFrameworkCore.ModelBuilder') |  |

<a name='T-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManager'></a>
## DatabaseAggregationManager `type`

##### Namespace

RecordPoint.Connectors.SDK.Content

##### Summary

Implementation of a Aggregation manager that uses the connector database for persistence

<a name='M-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManager-#ctor-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseClient,RecordPoint-Connectors-SDK-Observability-IObservabilityScope-'></a>
### #ctor(databaseClient,observabilityScope) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| databaseClient | [RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseClient](#T-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseClient 'RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseClient') |  |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') |  |

<a name='F-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManager-CONNECTOR_ID_DIMENSION'></a>
### CONNECTOR_ID_DIMENSION `constants`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManager-AggregationExistsAsync-System-String,System-String,System-Threading-CancellationToken-'></a>
### AggregationExistsAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManager-GetAggregationAsync-System-String,System-String,System-Threading-CancellationToken-'></a>
### GetAggregationAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManager-GetAggregationsAsync-System-Threading-CancellationToken-'></a>
### GetAggregationsAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManager-GetAggregationsAsync-System-Linq-Expressions-Expression{System-Func{RecordPoint-Connectors-SDK-Content-AggregationModel,System-Boolean}},System-Threading-CancellationToken-'></a>
### GetAggregationsAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManager-GetAggregationsAsync-System-String,System-Threading-CancellationToken-'></a>
### GetAggregationsAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManager-RemoveAggregationAsync-System-String,System-String,System-Threading-CancellationToken-'></a>
### RemoveAggregationAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManager-RemoveAggregationsAsync-System-String,System-String[],System-Threading-CancellationToken-'></a>
### RemoveAggregationsAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManager-RemoveAggregationsAsync-System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Content-AggregationModel},System-Threading-CancellationToken-'></a>
### RemoveAggregationsAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManager-UpsertAggregationAsync-RecordPoint-Connectors-SDK-Content-AggregationModel,System-Threading-CancellationToken-'></a>
### UpsertAggregationAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManager-UpsertAggregationsAsync-System-Collections-Generic-List{RecordPoint-Connectors-SDK-Content-AggregationModel},System-Threading-CancellationToken-'></a>
### UpsertAggregationsAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManagerBuilderExtensions'></a>
## DatabaseAggregationManagerBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Content

##### Summary

The database aggregation manager builder extensions.

<a name='M-RecordPoint-Connectors-SDK-Content-DatabaseAggregationManagerBuilderExtensions-UseDatabaseAggregationManager-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseDatabaseAggregationManager(hostBuilder) `method`

##### Summary

Use database aggregation manager.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |

<a name='T-RecordPoint-Connectors-SDK-Databases-DatabaseBuilderExtensions'></a>
## DatabaseBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases

##### Summary

Database host extensions

<a name='M-RecordPoint-Connectors-SDK-Databases-DatabaseBuilderExtensions-AddDatabaseService-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### AddDatabaseService(hostBuilder) `method`

##### Summary

Add the connect database management service

##### Returns

Updated host builder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | Host builder to configure |

<a name='T-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager'></a>
## DatabaseChannelManager `type`

##### Namespace

RecordPoint.Connectors.SDK.Content

##### Summary

Implementation of a Channel manager that uses the connector database for persistence

<a name='M-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager-#ctor-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseClient,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Caching-ICache{System-Boolean},RecordPoint-Connectors-SDK-Content-IDirectChannelAccess-'></a>
### #ctor(databaseClient,observabilityScope,channelExistsCache,directChannelAccess) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| databaseClient | [RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseClient](#T-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseClient 'RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseClient') |  |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') |  |
| channelExistsCache | [RecordPoint.Connectors.SDK.Caching.ICache{System.Boolean}](#T-RecordPoint-Connectors-SDK-Caching-ICache{System-Boolean} 'RecordPoint.Connectors.SDK.Caching.ICache{System.Boolean}') |  |
| directChannelAccess | [RecordPoint.Connectors.SDK.Content.IDirectChannelAccess](#T-RecordPoint-Connectors-SDK-Content-IDirectChannelAccess 'RecordPoint.Connectors.SDK.Content.IDirectChannelAccess') |  |

<a name='F-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager-CONNECTOR_ID_DIMENSION'></a>
### CONNECTOR_ID_DIMENSION `constants`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager-ChannelExistsAsync-System-String,System-String,System-Threading-CancellationToken-'></a>
### ChannelExistsAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager-GetChannelAsync-System-String,System-String,System-Threading-CancellationToken-'></a>
### GetChannelAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager-GetChannelClassificationsAsync-System-String,System-Int32,System-Threading-CancellationToken-'></a>
### GetChannelClassificationsAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager-GetChannelsAsync-System-Threading-CancellationToken-'></a>
### GetChannelsAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager-GetChannelsAsync-System-Linq-Expressions-Expression{System-Func{RecordPoint-Connectors-SDK-Content-ChannelModel,System-Boolean}},System-Threading-CancellationToken-'></a>
### GetChannelsAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager-GetChannelsAsync-System-String,System-Threading-CancellationToken-'></a>
### GetChannelsAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager-PatchChannelAsync-System-String,System-String,System-Action{RecordPoint-Connectors-SDK-Content-ChannelModel},System-Threading-CancellationToken-'></a>
### PatchChannelAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager-RemoveChannelAsync-System-String,System-String,System-Threading-CancellationToken-'></a>
### RemoveChannelAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager-RemoveChannelsAsync-System-String,System-String[],System-Threading-CancellationToken-'></a>
### RemoveChannelsAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager-RemoveChannelsAsync-System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Content-ChannelModel},System-Threading-CancellationToken-'></a>
### RemoveChannelsAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager-UpsertChannelAsync-RecordPoint-Connectors-SDK-Content-ChannelModel,System-Threading-CancellationToken-'></a>
### UpsertChannelAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Content-DatabaseChannelManager-UpsertChannelsAsync-System-Collections-Generic-List{RecordPoint-Connectors-SDK-Content-ChannelModel},System-Threading-CancellationToken-'></a>
### UpsertChannelsAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Content-DatabaseChannelManagerBuilderExtensions'></a>
## DatabaseChannelManagerBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Content

##### Summary

The database channel manager builder extensions.

<a name='M-RecordPoint-Connectors-SDK-Content-DatabaseChannelManagerBuilderExtensions-UseDatabaseChannelManager-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseDatabaseChannelManager(hostBuilder) `method`

##### Summary

Use database channel manager.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |

<a name='T-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager'></a>
## DatabaseConnectorConfigurationManager `type`

##### Namespace

RecordPoint.Connectors.SDK.Connectors

##### Summary

The database connector configuration manager.

<a name='M-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-#ctor-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseClient,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Connectors-ConnectorOptions},RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Toggles-IToggleProvider,RecordPoint-Connectors-SDK-Caching-ICache{RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel}-'></a>
### #ctor(databaseClient,observabilityScope,connectorOptions,systemContext,toggleProvider,connectorConfigurationCache) `constructor`

##### Summary

Initializes a new instance of the [DatabaseConnectorConfigurationManager](#T-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager 'RecordPoint.Connectors.SDK.Connectors.DatabaseConnectorConfigurationManager') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| databaseClient | [RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseClient](#T-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseClient 'RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseClient') | The database client. |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | The scope manager. |
| connectorOptions | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Connectors.ConnectorOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Connectors-ConnectorOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Connectors.ConnectorOptions}') | The connector options. |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |
| toggleProvider | [RecordPoint.Connectors.SDK.Toggles.IToggleProvider](#T-RecordPoint-Connectors-SDK-Toggles-IToggleProvider 'RecordPoint.Connectors.SDK.Toggles.IToggleProvider') | The toggle provider. |
| connectorConfigurationCache | [RecordPoint.Connectors.SDK.Caching.ICache{RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel}](#T-RecordPoint-Connectors-SDK-Caching-ICache{RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel} 'RecordPoint.Connectors.SDK.Caching.ICache{RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel}') | The connector configuration cache. |

<a name='F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-BINARY_APPSETTING_OFF_REASON'></a>
### BINARY_APPSETTING_OFF_REASON `constants`

##### Summary

The BINARY APPSETTING OFF REASON.

<a name='F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-BINARY_SUBMISSION_ENABLED_REASON'></a>
### BINARY_SUBMISSION_ENABLED_REASON `constants`

##### Summary

The BINARY SUBMISSION ENABLED REASON.

<a name='F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-BINARY_SUBMISSION_FEATURE'></a>
### BINARY_SUBMISSION_FEATURE `constants`

##### Summary

The BINARY SUBMISSION FEATURE.

<a name='F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-CONNECTOR_DISABLED_REASON'></a>
### CONNECTOR_DISABLED_REASON `constants`

##### Summary

The CONNECTOR DISABLED REASON.

<a name='F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-CONNECTOR_ENABLED_REASON'></a>
### CONNECTOR_ENABLED_REASON `constants`

##### Summary

The CONNECTOR ENABLED REASON.

<a name='F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-CONNECTOR_FEATURE'></a>
### CONNECTOR_FEATURE `constants`

##### Summary

The CONNECTOR FEATURE.

<a name='F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-CONNECTOR_FEATURE_DISABLED_REASON'></a>
### CONNECTOR_FEATURE_DISABLED_REASON `constants`

##### Summary

The CONNECTOR FEATURE DISABLED REASON.

<a name='F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-CONNECTOR_ID_DIMENSION'></a>
### CONNECTOR_ID_DIMENSION `constants`

##### Summary

The CONNECTOR ID DIMENSION.

<a name='F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-CONNECTOR_NOT_FOUND_REASON'></a>
### CONNECTOR_NOT_FOUND_REASON `constants`

##### Summary

The CONNECTOR NOT FOUND REASON.

<a name='F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-DISABLED'></a>
### DISABLED `constants`

##### Summary

The 'Disabled' state of a Connector Configuration

<a name='F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-DISABLED_TIME'></a>
### DISABLED_TIME `constants`

##### Summary

Timestamp for when a Connector Configuration was disabled

<a name='F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-ENABLED'></a>
### ENABLED `constants`

##### Summary

The 'Enabled' state of a Connector Configuration

<a name='F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-SUBMISSION_APPSETTING_OFF_REASON'></a>
### SUBMISSION_APPSETTING_OFF_REASON `constants`

##### Summary

The SUBMISSION APPSETTING OFF REASON.

<a name='F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-SUBMISSION_ENABLED_REASON'></a>
### SUBMISSION_ENABLED_REASON `constants`

##### Summary

The SUBMISSION ENABLED REASON.

<a name='F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-SUBMISSION_FEATURE'></a>
### SUBMISSION_FEATURE `constants`

##### Summary

The SUBMISSION FEATURE.

<a name='F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-SUBMISSION_KILLSWITCH_ON_REASON'></a>
### SUBMISSION_KILLSWITCH_ON_REASON `constants`

##### Summary

The SUBMISSION KILLSWITCH ON REASON.

<a name='F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-_connectorConfigurationCache'></a>
### _connectorConfigurationCache `constants`

##### Summary

The connector configuration cache.

<a name='F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-_connectorOptions'></a>
### _connectorOptions `constants`

##### Summary

The connector options.

<a name='F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-_databaseClient'></a>
### _databaseClient `constants`

##### Summary

The database client.

<a name='F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-_observabilityScope'></a>
### _observabilityScope `constants`

##### Summary

The scope manager.

<a name='F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-_systemContext'></a>
### _systemContext `constants`

##### Summary

The system context.

<a name='F-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-_toggleProvider'></a>
### _toggleProvider `constants`

##### Summary

The toggle provider.

<a name='M-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-ConnectorConfigurationExistsAsync-System-String,System-Threading-CancellationToken-'></a>
### ConnectorConfigurationExistsAsync(connectorId,cancellationToken) `method`

##### Summary

Connectors configuration exists asynchronously.

##### Returns

Task<bool>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The connector id. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Remarks

Performs a service-side check to ensure the retrieved connector configuration matches the specified connector id
as it has been observed results being incorrectly returned when the requested connector id is empty

<a name='M-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-DeleteConnectorConfigurationAsync-System-String,System-Threading-CancellationToken-'></a>
### DeleteConnectorConfigurationAsync(connectorId,cancellationToken) `method`

##### Summary

Deletes connector configuration asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The connector id. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-GetAllConnectorConfigurationsAsync-System-Threading-CancellationToken-'></a>
### GetAllConnectorConfigurationsAsync(cancellationToken) `method`

##### Summary

Get all connector configurations asynchronously.

##### Returns

Task<List<ConnectorConfigurationModel>>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-GetBinarySubmissionStatusAsync-System-String,System-Threading-CancellationToken-'></a>
### GetBinarySubmissionStatusAsync(connectorId,cancellationToken) `method`

##### Summary

Get the connector binary submission feature status

##### Returns

Feature status

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | ID of the connector to get the status for |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-GetConnectorConfigurationAsync-System-String,System-Threading-CancellationToken-'></a>
### GetConnectorConfigurationAsync(connectorId,cancellationToken) `method`

##### Summary

Get connector configuration asynchronously.

##### Returns

Task<ConnectorConfigurationModel>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The connector id. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Remarks

Performs a service-side check to ensure the retrieved connector configuration matches the specified connector id
as it has been observed results being incorrectly returned when the requested connector id is empty

<a name='M-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-GetConnectorStatusAsync-System-String,System-Threading-CancellationToken-'></a>
### GetConnectorStatusAsync(connectorId,cancellationToken) `method`

##### Summary

Get the connector features status

##### Returns

Feature status

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-GetDimensions-System-String-'></a>
### GetDimensions(connectorId) `method`

##### Summary

Get the dimensions.

##### Returns

A Dimensions

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The connector id. |

<a name='M-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-GetSubmissionStatusAsync-System-String,System-Threading-CancellationToken-'></a>
### GetSubmissionStatusAsync(connectorId,cancellationToken) `method`

##### Summary

Get the connector submission feature status

##### Returns

Feature status

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | ID of the connector to get the status for |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-PatchConnectorConfigurationAsync-System-String,System-Action{RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel},System-Threading-CancellationToken-'></a>
### PatchConnectorConfigurationAsync() `method`

##### Summary

Updates the specified connector configuration by applying the provided patch action

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Connectors-DatabaseConnectorConfigurationManager-SetConnectorConfigurationAsync-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel,System-Threading-CancellationToken-'></a>
### SetConnectorConfigurationAsync(connectorData,cancellationToken) `method`

##### Summary

Set connector configuration asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorData | [RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel](#T-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel 'RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel') | The connector data. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='T-RecordPoint-Connectors-SDK-Work-DatabaseManagedWorkStatusManager'></a>
## DatabaseManagedWorkStatusManager `type`

##### Namespace

RecordPoint.Connectors.SDK.Work

##### Summary

Implementation of a Work status manager that uses the connector database for persistence

<a name='M-RecordPoint-Connectors-SDK-Work-DatabaseManagedWorkStatusManager-#ctor-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseClient,RecordPoint-Connectors-SDK-Observability-IObservabilityScope-'></a>
### #ctor(databaseClient,observabilityScope) `constructor`

##### Summary

Initializes a new instance of the [DatabaseManagedWorkStatusManager](#T-RecordPoint-Connectors-SDK-Work-DatabaseManagedWorkStatusManager 'RecordPoint.Connectors.SDK.Work.DatabaseManagedWorkStatusManager') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| databaseClient | [RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseClient](#T-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseClient 'RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseClient') | Database client used to read managed work statuses. |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | Observability scope used for dependency telemetry. |

<a name='F-RecordPoint-Connectors-SDK-Work-DatabaseManagedWorkStatusManager-WORK_STATUS_ID_DIMENSION'></a>
### WORK_STATUS_ID_DIMENSION `constants`

##### Summary

Telemetry dimension name for the managed work status identifier.

<a name='M-RecordPoint-Connectors-SDK-Work-DatabaseManagedWorkStatusManager-GetWorkStatusesAsync-System-Linq-Expressions-Expression{System-Func{RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel,System-Boolean}},System-Threading-CancellationToken-'></a>
### GetWorkStatusesAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Databases-DatabaseProviderTelemetryExtensions'></a>
## DatabaseProviderTelemetryExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases

##### Summary

The database provider telemetry extensions.

<a name='M-RecordPoint-Connectors-SDK-Databases-DatabaseProviderTelemetryExtensions-WithAddedTelemetry-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseProvider,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker-'></a>
### WithAddedTelemetry(provider,telemetryTracker) `method`

##### Summary

With added telemetry.

##### Returns

An IConnectorDatabaseProvider

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| provider | [RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseProvider](#T-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseProvider 'RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseProvider') | The provider. |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The telemetry tracker. |

<a name='T-RecordPoint-Connectors-SDK-Databases-DatabaseProviderWithTelemetry'></a>
## DatabaseProviderWithTelemetry `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases

##### Summary

The database provider with telemetry.

<a name='M-RecordPoint-Connectors-SDK-Databases-DatabaseProviderWithTelemetry-#ctor-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseProvider,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker-'></a>
### #ctor(databaseProviderImplementation,telemetryTracker) `constructor`

##### Summary

Initializes a new instance of the [DatabaseProviderWithTelemetry](#T-RecordPoint-Connectors-SDK-Databases-DatabaseProviderWithTelemetry 'RecordPoint.Connectors.SDK.Databases.DatabaseProviderWithTelemetry') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| databaseProviderImplementation | [RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseProvider](#T-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseProvider 'RecordPoint.Connectors.SDK.Databases.IConnectorDatabaseProvider') | The database provider implementation. |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The telemetry tracker. |

<a name='F-RecordPoint-Connectors-SDK-Databases-DatabaseProviderWithTelemetry-_databaseProviderImplementation'></a>
### _databaseProviderImplementation `constants`

##### Summary

The database provider implementation.

<a name='F-RecordPoint-Connectors-SDK-Databases-DatabaseProviderWithTelemetry-_telemetryTracker'></a>
### _telemetryTracker `constants`

##### Summary

The telemetry tracker.

<a name='M-RecordPoint-Connectors-SDK-Databases-DatabaseProviderWithTelemetry-CleanupAsync-System-Threading-CancellationToken-'></a>
### CleanupAsync(cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-DatabaseProviderWithTelemetry-CreateDbContext'></a>
### CreateDbContext() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-DatabaseProviderWithTelemetry-Exists'></a>
### Exists() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-DatabaseProviderWithTelemetry-GetConnectionString'></a>
### GetConnectionString() `method`

##### Summary

Get connection string.

##### Returns

A string

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-DatabaseProviderWithTelemetry-GetExternalSystemName'></a>
### GetExternalSystemName() `method`

##### Summary

Get external system name.

##### Returns

A string

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-DatabaseProviderWithTelemetry-PrepareAsync-System-Threading-CancellationToken-'></a>
### PrepareAsync(cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-DatabaseProviderWithTelemetry-ReadyAsync-System-Threading-CancellationToken-'></a>
### ReadyAsync(cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-DatabaseProviderWithTelemetry-RemoveAsync-System-Threading-CancellationToken-'></a>
### RemoveAsync(cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-DatabaseProviderWithTelemetry-SetReady-System-Exception-'></a>
### SetReady(exception) `method`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| exception | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') |  |

<a name='T-RecordPoint-Connectors-SDK-Databases-DatabaseService`2'></a>
## DatabaseService\`2 `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases

##### Summary

Implementation of database management services.
This service can be used by some database providers that require preperation prior to use.
e.g. The service can be used to attach a localDb database to the hosting service, or performing EF migrations.

##### Remarks

Instantiates a new Database Service

<a name='M-RecordPoint-Connectors-SDK-Databases-DatabaseService`2-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-Context-ISystemContext,`1,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider,Microsoft-Extensions-Hosting-IHostApplicationLifetime-'></a>
### #ctor() `constructor`

##### Summary

Implementation of database management services.
This service can be used by some database providers that require preperation prior to use.
e.g. The service can be used to attach a localDb database to the hosting service, or performing EF migrations.

##### Parameters

This constructor has no parameters.

##### Remarks

Instantiates a new Database Service

<a name='M-RecordPoint-Connectors-SDK-Databases-DatabaseService`2-ExecuteAsync-System-Threading-CancellationToken-'></a>
### ExecuteAsync(stoppingToken) `method`

##### Summary

Executes the Services

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| stoppingToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-DatabaseService`2-PrepareDatabaseAsync-System-Threading-CancellationToken-'></a>
### PrepareDatabaseAsync(cancellationToken) `method`

##### Summary

Executes the Database Preparation Operation

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-DatabaseService`2-StartAsync-System-Threading-CancellationToken-'></a>
### StartAsync(cancellationToken) `method`

##### Summary

Starts the service

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='T-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseClient'></a>
## IConnectorDatabaseClient `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases

##### Summary

Defines the connector database client

<a name='T-RecordPoint-Connectors-SDK-Databases-IConnectorDatabaseProvider'></a>
## IConnectorDatabaseProvider `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases

##### Summary

Provider for the connector database

<a name='T-RecordPoint-Connectors-SDK-Databases-IConnectorDbContext'></a>
## IConnectorDbContext `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Databases-IConnectorDbContext-Aggregations'></a>
### Aggregations `property`

##### Summary

Aggregation Data

<a name='P-RecordPoint-Connectors-SDK-Databases-IConnectorDbContext-Channels'></a>
### Channels `property`

##### Summary

Channel Data

<a name='P-RecordPoint-Connectors-SDK-Databases-IConnectorDbContext-Connectors'></a>
### Connectors `property`

##### Summary

Connectors Data

<a name='P-RecordPoint-Connectors-SDK-Databases-IConnectorDbContext-ManagedWorkStatuses'></a>
### ManagedWorkStatuses `property`

##### Summary

Managed Work Status Data

<a name='T-RecordPoint-Connectors-SDK-Databases-IDatabaseClient`1'></a>
## IDatabaseClient\`1 `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases

##### Summary

Defines the database client used by components to access database services

<a name='M-RecordPoint-Connectors-SDK-Databases-IDatabaseClient`1-CreateDbContext'></a>
### CreateDbContext() `method`

##### Summary

Get the db context

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-IDatabaseClient`1-GetExternalSystemName'></a>
### GetExternalSystemName() `method`

##### Summary

Get the external system name that identifies the type of database for standard logging

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-IDatabaseClient`1-ReadyAsync-System-Threading-CancellationToken-'></a>
### ReadyAsync(cancellationToken) `method`

##### Summary

Task that returns when the database is ready

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='T-RecordPoint-Connectors-SDK-Databases-IDatabaseProvider`1'></a>
## IDatabaseProvider\`1 `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases

##### Summary

Defines a database provider

##### Remarks

Database providers are responsible for providing database specific funtionality to the database subsystem

<a name='M-RecordPoint-Connectors-SDK-Databases-IDatabaseProvider`1-CleanupAsync-System-Threading-CancellationToken-'></a>
### CleanupAsync(cancellationToken) `method`

##### Summary

Cleanup the database on shutdown

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Databases-IDatabaseProvider`1-CreateDbContext'></a>
### CreateDbContext() `method`

##### Summary

Create a db context used to make db requests if the database is ready,
otherwise raises the reason why the database is not ready

##### Returns

Connector DB Context if the database is ready

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-IDatabaseProvider`1-Exists'></a>
### Exists() `method`

##### Summary

Does the database exist?

##### Returns

True if the database exists, otherwise false

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-IDatabaseProvider`1-GetConnectionString'></a>
### GetConnectionString() `method`

##### Summary

Get the connection string for this database, useful for providing it to external systems that may need to use the database

##### Returns

Connection string

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-IDatabaseProvider`1-GetExternalSystemName'></a>
### GetExternalSystemName() `method`

##### Summary

Get the name of the external system this provider manages

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-IDatabaseProvider`1-PrepareAsync-System-Threading-CancellationToken-'></a>
### PrepareAsync(cancellationToken) `method`

##### Summary

Prepare the database for operation

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Databases-IDatabaseProvider`1-ReadyAsync-System-Threading-CancellationToken-'></a>
### ReadyAsync(cancellationToken) `method`

##### Summary

Called by dependant services to determine if the database is ready for operation.

##### Returns

Readiness Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Databases-IDatabaseProvider`1-RemoveAsync-System-Threading-CancellationToken-'></a>
### RemoveAsync(cancellationToken) `method`

##### Summary

Remove the database, deleting it from the system

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Databases-IDatabaseProvider`1-SetReady-System-Exception-'></a>
### SetReady(exception) `method`

##### Summary

Set that the database is ready

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| exception | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Exception that indicates that the service has critically failed. Any calls to Ready will fail |

<a name='T-RecordPoint-Connectors-SDK-Content-IDirectChannelAccess'></a>
## IDirectChannelAccess `type`

##### Namespace

RecordPoint.Connectors.SDK.Content

##### Summary

Provides direct (non-EF Core) access to channel data for optimised reads.
When enabled, bypasses EF Core query translation to use native point reads.

<a name='P-RecordPoint-Connectors-SDK-Content-IDirectChannelAccess-IsEnabled'></a>
### IsEnabled `property`

##### Summary

Whether direct channel access is enabled.
When false, callers should fall back to the standard EF Core path.

<a name='M-RecordPoint-Connectors-SDK-Content-IDirectChannelAccess-ReadChannelAsync-System-String,System-String,System-Threading-CancellationToken-'></a>
### ReadChannelAsync(connectorId,externalId,cancellationToken) `method`

##### Summary

Reads a single channel by connector ID and external ID using a direct point read.

##### Returns

The channel model if found; null otherwise

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The connector ID (partition key) |
| externalId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The external ID (document ID) |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Content-IDirectChannelAccess-ReadChannelClassificationsAsync-System-String,System-Int32,System-Threading-CancellationToken-'></a>
### ReadChannelClassificationsAsync(connectorId,pageSize,cancellationToken) `method`

##### Summary

Streams lightweight channel classification data for a connector.

##### Returns

Async stream of lightweight channel projections

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The connector ID (partition key) |
| pageSize | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Preferred page size for providers that support paged reads |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='T-RecordPoint-Connectors-SDK-Content-NullDirectChannelAccess'></a>
## NullDirectChannelAccess `type`

##### Namespace

RecordPoint.Connectors.SDK.Content

##### Summary

No-op implementation of [IDirectChannelAccess](#T-RecordPoint-Connectors-SDK-Content-IDirectChannelAccess 'RecordPoint.Connectors.SDK.Content.IDirectChannelAccess').
Always returns `IsEnabled = false`, causing the caller to use the standard EF Core path.
Registered by default so that consumers don't need to configure direct reads unless they opt in.

<a name='P-RecordPoint-Connectors-SDK-Content-NullDirectChannelAccess-IsEnabled'></a>
### IsEnabled `property`

##### Summary

*Inherit from parent.*

<a name='M-RecordPoint-Connectors-SDK-Content-NullDirectChannelAccess-ReadChannelAsync-System-String,System-String,System-Threading-CancellationToken-'></a>
### ReadChannelAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Content-NullDirectChannelAccess-ReadChannelClassificationsAsync-System-String,System-Int32,System-Threading-CancellationToken-'></a>
### ReadChannelClassificationsAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Databases-PrepareDatabaseOperation`2'></a>
## PrepareDatabaseOperation\`2 `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases

##### Summary

The prepare database operation.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TDbContext |  |
| TDbProvider |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-PrepareDatabaseOperation`2-#ctor-System-IServiceProvider,`1,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider-'></a>
### #ctor(serviceProvider,databaseProvider,observabilityScope,telemetryTracker,dateTimeProvider) `constructor`

##### Summary

Initializes a new instance of the class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| serviceProvider | [System.IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider') | The service provider. |
| databaseProvider | [\`1](#T-`1 '`1') | The database provider. |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | The scope manager. |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The telemetry tracker. |
| dateTimeProvider | [RecordPoint.Connectors.SDK.Providers.IDateTimeProvider](#T-RecordPoint-Connectors-SDK-Providers-IDateTimeProvider 'RecordPoint.Connectors.SDK.Providers.IDateTimeProvider') | The date time provider. |

<a name='F-RecordPoint-Connectors-SDK-Databases-PrepareDatabaseOperation`2-PREPARE_DATABASE_WORK_TYPE'></a>
### PREPARE_DATABASE_WORK_TYPE `constants`

##### Summary

PREPARE DATABASE WORK TYPE.

<a name='F-RecordPoint-Connectors-SDK-Databases-PrepareDatabaseOperation`2-_databaseProvider'></a>
### _databaseProvider `constants`

##### Summary

The database provider.

<a name='P-RecordPoint-Connectors-SDK-Databases-PrepareDatabaseOperation`2-WorkType'></a>
### WorkType `property`

##### Summary

Gets the work type.

<a name='M-RecordPoint-Connectors-SDK-Databases-PrepareDatabaseOperation`2-InnerRunAsync-System-Threading-CancellationToken-'></a>
### InnerRunAsync(cancellationToken) `method`

##### Summary

Inner the run asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='T-RecordPoint-Connectors-SDK-Databases-ProviderDatabaseClient`2'></a>
## ProviderDatabaseClient\`2 `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases

##### Summary

Database client that just redirects requests to a provider

<a name='M-RecordPoint-Connectors-SDK-Databases-ProviderDatabaseClient`2-#ctor-`1-'></a>
### #ctor(databaseProvider) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| databaseProvider | [\`1](#T-`1 '`1') |  |

<a name='P-RecordPoint-Connectors-SDK-Databases-ProviderDatabaseClient`2-Provider'></a>
### Provider `property`

##### Summary

Underlying provider

<a name='M-RecordPoint-Connectors-SDK-Databases-ProviderDatabaseClient`2-CreateDbContext'></a>
### CreateDbContext() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-ProviderDatabaseClient`2-GetExternalSystemName'></a>
### GetExternalSystemName() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-ProviderDatabaseClient`2-ReadyAsync-System-Threading-CancellationToken-'></a>
### ReadyAsync(cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |
