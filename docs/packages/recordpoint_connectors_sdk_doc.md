<a name='assembly'></a>
# RecordPoint.Connectors.SDK

## Contents

- [AppInsightsTelemetryBuilderExtensions](#T-RecordPoint-Connectors-SDK-Observability-AppInsights-AppInsightsTelemetryBuilderExtensions 'RecordPoint.Connectors.SDK.Observability.AppInsights.AppInsightsTelemetryBuilderExtensions')
  - [UseAppInsightsTelemetryTracking()](#M-RecordPoint-Connectors-SDK-Observability-AppInsights-AppInsightsTelemetryBuilderExtensions-UseAppInsightsTelemetryTracking-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Observability.AppInsights.AppInsightsTelemetryBuilderExtensions.UseAppInsightsTelemetryTracking(Microsoft.Extensions.Hosting.IHostBuilder)')
- [AppSettingsConfigurationBuilderExtensions](#T-RecordPoint-Connectors-SDK-Configuration-AppSettingsConfigurationBuilderExtensions 'RecordPoint.Connectors.SDK.Configuration.AppSettingsConfigurationBuilderExtensions')
  - [UseR365AppSettingsConfiguration(hostBuilder)](#M-RecordPoint-Connectors-SDK-Configuration-AppSettingsConfigurationBuilderExtensions-UseR365AppSettingsConfiguration-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Configuration.AppSettingsConfigurationBuilderExtensions.UseR365AppSettingsConfiguration(Microsoft.Extensions.Hosting.IHostBuilder)')
- [AppSettingsR365ConfigurationClient](#T-RecordPoint-Connectors-SDK-Configuration-AppSettingsR365ConfigurationClient 'RecordPoint.Connectors.SDK.Configuration.AppSettingsR365ConfigurationClient')
  - [#ctor(r365ConfigurationModel,r365MultiConfigurationModel)](#M-RecordPoint-Connectors-SDK-Configuration-AppSettingsR365ConfigurationClient-#ctor-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Configuration-R365ConfigurationModel},Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Configuration-R365MultiConfigurationModel}- 'RecordPoint.Connectors.SDK.Configuration.AppSettingsR365ConfigurationClient.#ctor(Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Configuration.R365ConfigurationModel},Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Configuration.R365MultiConfigurationModel})')
  - [GetR365Configuration()](#M-RecordPoint-Connectors-SDK-Configuration-AppSettingsR365ConfigurationClient-GetR365Configuration-System-String- 'RecordPoint.Connectors.SDK.Configuration.AppSettingsR365ConfigurationClient.GetR365Configuration(System.String)')
  - [R365ConfigurationExists()](#M-RecordPoint-Connectors-SDK-Configuration-AppSettingsR365ConfigurationClient-R365ConfigurationExists 'RecordPoint.Connectors.SDK.Configuration.AppSettingsR365ConfigurationClient.R365ConfigurationExists')
- [ApplicationInsightsTelemetrySink](#T-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightsTelemetrySink 'RecordPoint.Connectors.SDK.Observability.AppInsights.ApplicationInsightsTelemetrySink')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightsTelemetrySink-#ctor-RecordPoint-Connectors-SDK-Observability-AppInsights-ITelemetryClientFactory,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightOptions},RecordPoint-Connectors-SDK-Toggles-IToggleProvider,RecordPoint-Connectors-SDK-Context-ISystemContext- 'RecordPoint.Connectors.SDK.Observability.AppInsights.ApplicationInsightsTelemetrySink.#ctor(RecordPoint.Connectors.SDK.Observability.AppInsights.ITelemetryClientFactory,Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Observability.AppInsights.ApplicationInsightOptions},RecordPoint.Connectors.SDK.Toggles.IToggleProvider,RecordPoint.Connectors.SDK.Context.ISystemContext)')
  - [IsConfigured()](#M-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightsTelemetrySink-IsConfigured 'RecordPoint.Connectors.SDK.Observability.AppInsights.ApplicationInsightsTelemetrySink.IsConfigured')
  - [IsEnabled()](#M-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightsTelemetrySink-IsEnabled 'RecordPoint.Connectors.SDK.Observability.AppInsights.ApplicationInsightsTelemetrySink.IsEnabled')
  - [TrackEvent()](#M-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightsTelemetrySink-TrackEvent-System-String,RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures- 'RecordPoint.Connectors.SDK.Observability.AppInsights.ApplicationInsightsTelemetrySink.TrackEvent(System.String,RecordPoint.Connectors.SDK.Observability.Dimensions,RecordPoint.Connectors.SDK.Observability.Measures)')
  - [TrackException()](#M-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightsTelemetrySink-TrackException-System-Exception,RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures- 'RecordPoint.Connectors.SDK.Observability.AppInsights.ApplicationInsightsTelemetrySink.TrackException(System.Exception,RecordPoint.Connectors.SDK.Observability.Dimensions,RecordPoint.Connectors.SDK.Observability.Measures)')
  - [TrackTrace()](#M-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightsTelemetrySink-TrackTrace-System-String,RecordPoint-Connectors-SDK-Observability-SeverityLevel,RecordPoint-Connectors-SDK-Observability-Dimensions- 'RecordPoint.Connectors.SDK.Observability.AppInsights.ApplicationInsightsTelemetrySink.TrackTrace(System.String,RecordPoint.Connectors.SDK.Observability.SeverityLevel,RecordPoint.Connectors.SDK.Observability.Dimensions)')
- [AzureBlobRetryProvider](#T-RecordPoint-Connectors-SDK-Providers-AzureBlobRetryProvider 'RecordPoint.Connectors.SDK.Providers.AzureBlobRetryProvider')
  - [Log](#P-RecordPoint-Connectors-SDK-Providers-AzureBlobRetryProvider-Log 'RecordPoint.Connectors.SDK.Providers.AzureBlobRetryProvider.Log')
  - [ExecuteWithRetry()](#M-RecordPoint-Connectors-SDK-Providers-AzureBlobRetryProvider-ExecuteWithRetry-System-Func{System-Threading-Tasks-Task},System-Type,System-String- 'RecordPoint.Connectors.SDK.Providers.AzureBlobRetryProvider.ExecuteWithRetry(System.Func{System.Threading.Tasks.Task},System.Type,System.String)')
  - [GetRetryPolicy(type,methodName)](#M-RecordPoint-Connectors-SDK-Providers-AzureBlobRetryProvider-GetRetryPolicy-System-Type,System-String- 'RecordPoint.Connectors.SDK.Providers.AzureBlobRetryProvider.GetRetryPolicy(System.Type,System.String)')
- [AzureBlobRetryProviderWithCircuitBreaker](#T-RecordPoint-Connectors-SDK-Providers-AzureBlobRetryProviderWithCircuitBreaker 'RecordPoint.Connectors.SDK.Providers.AzureBlobRetryProviderWithCircuitBreaker')
  - [#ctor(options,useCircuit)](#M-RecordPoint-Connectors-SDK-Providers-AzureBlobRetryProviderWithCircuitBreaker-#ctor-RecordPoint-Connectors-SDK-Providers-CircuitBreakerOptions,System-Boolean- 'RecordPoint.Connectors.SDK.Providers.AzureBlobRetryProviderWithCircuitBreaker.#ctor(RecordPoint.Connectors.SDK.Providers.CircuitBreakerOptions,System.Boolean)')
  - [GetRetryPolicy(type,methodName)](#M-RecordPoint-Connectors-SDK-Providers-AzureBlobRetryProviderWithCircuitBreaker-GetRetryPolicy-System-Type,System-String- 'RecordPoint.Connectors.SDK.Providers.AzureBlobRetryProviderWithCircuitBreaker.GetRetryPolicy(System.Type,System.String)')
  - [IsCircuitClosed(waitFor)](#M-RecordPoint-Connectors-SDK-Providers-AzureBlobRetryProviderWithCircuitBreaker-IsCircuitClosed-System-TimeSpan@- 'RecordPoint.Connectors.SDK.Providers.AzureBlobRetryProviderWithCircuitBreaker.IsCircuitClosed(System.TimeSpan@)')
- [ChannelDiscoveryOperation](#T-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation')
  - [#ctor(serviceProvider,contentManagerActionProvider,connectorManager,channelManager,workQueueClient,managedWorkFactory,managedWorkStatusManager,systemContext,observabilityScope,telemetryTracker,dateTimeProvider,options,contentManagerOptions)](#M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider,RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,RecordPoint-Connectors-SDK-Content-IChannelManager,RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-Work-IManagedWorkFactory,RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperationOptions},Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions}- 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation.#ctor(System.IServiceProvider,RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider,RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager,RecordPoint.Connectors.SDK.Content.IChannelManager,RecordPoint.Connectors.SDK.Work.IWorkQueueClient,RecordPoint.Connectors.SDK.Work.IManagedWorkFactory,RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager,RecordPoint.Connectors.SDK.Context.ISystemContext,RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,RecordPoint.Connectors.SDK.Providers.IDateTimeProvider,Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperationOptions},Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.ContentManagerOptions})')
  - [WORK_TYPE](#F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-WORK_TYPE 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation.WORK_TYPE')
  - [_actionExecutionTimespan](#F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-_actionExecutionTimespan 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation._actionExecutionTimespan')
  - [_channelDiscoveryResult](#F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-_channelDiscoveryResult 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation._channelDiscoveryResult')
  - [_channelManager](#F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-_channelManager 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation._channelManager')
  - [_connectorConfiguration](#F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-_connectorConfiguration 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation._connectorConfiguration')
  - [_connectorManager](#F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-_connectorManager 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation._connectorManager')
  - [_contentManagerActionProvider](#F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-_contentManagerActionProvider 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation._contentManagerActionProvider')
  - [_contentManagerOptions](#F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-_contentManagerOptions 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation._contentManagerOptions')
  - [_contentRegistrationOperationsStarted](#F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-_contentRegistrationOperationsStarted 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation._contentRegistrationOperationsStarted')
  - [_contentSynchronisationOperationsStarted](#F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-_contentSynchronisationOperationsStarted 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation._contentSynchronisationOperationsStarted')
  - [_managedWorkFactory](#F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-_managedWorkFactory 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation._managedWorkFactory')
  - [_managedWorkStatusManager](#F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-_managedWorkStatusManager 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation._managedWorkStatusManager')
  - [_options](#F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-_options 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation._options')
  - [_submitTimespan](#F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-_submitTimespan 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation._submitTimespan')
  - [_workQueueClient](#F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-_workQueueClient 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation._workQueueClient')
  - [ServiceName](#P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-ServiceName 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation.ServiceName')
  - [WorkType](#P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-WorkType 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation.WorkType')
  - [CreateChannelDiscoveryAction()](#M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-CreateChannelDiscoveryAction-Microsoft-Extensions-DependencyInjection-IServiceScope- 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation.CreateChannelDiscoveryAction(Microsoft.Extensions.DependencyInjection.IServiceScope)')
  - [DeserializeConfiguration(configurationType,configurationText)](#M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-DeserializeConfiguration-System-String,System-String- 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation.DeserializeConfiguration(System.String,System.String)')
  - [DeserializeState(stateType,stateText)](#M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-DeserializeState-System-String,System-String- 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation.DeserializeState(System.String,System.String)')
  - [FetchAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-FetchAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation.FetchAsync(System.Threading.CancellationToken)')
  - [GetCustomKeyDimensions()](#M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-GetCustomKeyDimensions 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation.GetCustomKeyDimensions')
  - [GetCustomResultDimensions()](#M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-GetCustomResultDimensions 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation.GetCustomResultDimensions')
  - [GetCustomResultMeasures()](#M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-GetCustomResultMeasures 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation.GetCustomResultMeasures')
  - [GetMissingChannelsForWorkTypeAsync()](#M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-GetMissingChannelsForWorkTypeAsync-System-Collections-Generic-List{RecordPoint-Connectors-SDK-Content-Channel},System-String,System-Func{RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel,System-String},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation.GetMissingChannelsForWorkTypeAsync(System.Collections.Generic.List{RecordPoint.Connectors.SDK.Content.Channel},System.String,System.Func{RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel,System.String},System.Threading.CancellationToken)')
  - [HandleAbandonedResultAsync(channelResult,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-HandleAbandonedResultAsync-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation.HandleAbandonedResultAsync(RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryResult,System.Threading.CancellationToken)')
  - [HandleCompleteResultAsync(channelResult,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-HandleCompleteResultAsync-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation.HandleCompleteResultAsync(RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryResult,System.Threading.CancellationToken)')
  - [HandleFailedResultAsync(channelResult,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-HandleFailedResultAsync-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation.HandleFailedResultAsync(RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryResult,System.Threading.CancellationToken)')
  - [HandleIncompleteResultAsync(channelResult,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-HandleIncompleteResultAsync-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation.HandleIncompleteResultAsync(RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryResult,System.Threading.CancellationToken)')
  - [HandleSuccessfulResultAsync(channelResult,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-HandleSuccessfulResultAsync-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation.HandleSuccessfulResultAsync(RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryResult,System.Threading.CancellationToken)')
  - [InnerDispose()](#M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-InnerDispose 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation.InnerDispose')
  - [InnerRunAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-InnerRunAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation.InnerRunAsync(System.Threading.CancellationToken)')
  - [SerializeState(state)](#M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-SerializeState-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryState- 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation.SerializeState(RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryState)')
- [ConnectorClientExtensions](#T-RecordPoint-Connectors-SDK-Connectors-ConnectorClientExtensions 'RecordPoint.Connectors.SDK.Connectors.ConnectorClientExtensions')
  - [ConvertToConnectorConfig(connectorData)](#M-RecordPoint-Connectors-SDK-Connectors-ConnectorClientExtensions-ConvertToConnectorConfig-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel- 'RecordPoint.Connectors.SDK.Connectors.ConnectorClientExtensions.ConvertToConnectorConfig(RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel)')
  - [ConvertToConnectorData(connectorConfig)](#M-RecordPoint-Connectors-SDK-Connectors-ConnectorClientExtensions-ConvertToConnectorData-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel- 'RecordPoint.Connectors.SDK.Connectors.ConnectorClientExtensions.ConvertToConnectorData(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel)')
  - [GetConnectorAsync(connectorClient,connectorId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Connectors-ConnectorClientExtensions-GetConnectorAsync-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Connectors.ConnectorClientExtensions.GetConnectorAsync(RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager,System.String,System.Threading.CancellationToken)')
  - [ListConnectorsAsync(connectorClient,cancellationToken)](#M-RecordPoint-Connectors-SDK-Connectors-ConnectorClientExtensions-ListConnectorsAsync-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Connectors.ConnectorClientExtensions.ListConnectorsAsync(RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager,System.Threading.CancellationToken)')
  - [SetConnectorAsync(connectorClient,connectorConfig,cancellationToken)](#M-RecordPoint-Connectors-SDK-Connectors-ConnectorClientExtensions-SetConnectorAsync-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Connectors.ConnectorClientExtensions.SetConnectorAsync(RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager,RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,System.Threading.CancellationToken)')
- [ConnectorConfigExtensions](#T-RecordPoint-Connectors-SDK-Configuration-ConnectorConfigExtensions 'RecordPoint.Connectors.SDK.Configuration.ConnectorConfigExtensions')
  - [CONSENT_AUTHORIZED_ON_PROPERTY](#F-RecordPoint-Connectors-SDK-Configuration-ConnectorConfigExtensions-CONSENT_AUTHORIZED_ON_PROPERTY 'RecordPoint.Connectors.SDK.Configuration.ConnectorConfigExtensions.CONSENT_AUTHORIZED_ON_PROPERTY')
  - [GetConsentAuthorizedOn(connectorConfiguration)](#M-RecordPoint-Connectors-SDK-Configuration-ConnectorConfigExtensions-GetConsentAuthorizedOn-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel- 'RecordPoint.Connectors.SDK.Configuration.ConnectorConfigExtensions.GetConsentAuthorizedOn(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel)')
  - [SetConsentAuthorizedOn(connectorConfig,value)](#M-RecordPoint-Connectors-SDK-Configuration-ConnectorConfigExtensions-SetConsentAuthorizedOn-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,System-DateTimeOffset- 'RecordPoint.Connectors.SDK.Configuration.ConnectorConfigExtensions.SetConsentAuthorizedOn(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,System.DateTimeOffset)')
- [ConnectorConfigurationBuilder](#T-RecordPoint-Connectors-SDK-ConnectorConfigurationBuilder 'RecordPoint.Connectors.SDK.ConnectorConfigurationBuilder')
  - [CreateConfigurationBuilder(args,connectorAssembly)](#M-RecordPoint-Connectors-SDK-ConnectorConfigurationBuilder-CreateConfigurationBuilder-System-String[],System-Reflection-Assembly- 'RecordPoint.Connectors.SDK.ConnectorConfigurationBuilder.CreateConfigurationBuilder(System.String[],System.Reflection.Assembly)')
  - [UseAppSettings(hostBuilder,args,connectorAssembly)](#M-RecordPoint-Connectors-SDK-ConnectorConfigurationBuilder-UseAppSettings-Microsoft-Extensions-Hosting-IHostBuilder,System-String[],System-Reflection-Assembly- 'RecordPoint.Connectors.SDK.ConnectorConfigurationBuilder.UseAppSettings(Microsoft.Extensions.Hosting.IHostBuilder,System.String[],System.Reflection.Assembly)')
  - [UseConfiguration(hostBuilder,configuration)](#M-RecordPoint-Connectors-SDK-ConnectorConfigurationBuilder-UseConfiguration-Microsoft-Extensions-Hosting-IHostBuilder,Microsoft-Extensions-Configuration-IConfiguration- 'RecordPoint.Connectors.SDK.ConnectorConfigurationBuilder.UseConfiguration(Microsoft.Extensions.Hosting.IHostBuilder,Microsoft.Extensions.Configuration.IConfiguration)')
- [ConnectorStatusStrategy](#T-RecordPoint-Connectors-SDK-Status-ConnectorStatusStrategy 'RecordPoint.Connectors.SDK.Status.ConnectorStatusStrategy')
  - [#ctor(connectorManager)](#M-RecordPoint-Connectors-SDK-Status-ConnectorStatusStrategy-#ctor-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager- 'RecordPoint.Connectors.SDK.Status.ConnectorStatusStrategy.#ctor(RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager)')
  - [_connectorManager](#F-RecordPoint-Connectors-SDK-Status-ConnectorStatusStrategy-_connectorManager 'RecordPoint.Connectors.SDK.Status.ConnectorStatusStrategy._connectorManager')
  - [GetStatusText(connectorModel,cancellationToken)](#M-RecordPoint-Connectors-SDK-Status-ConnectorStatusStrategy-GetStatusText-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Status.ConnectorStatusStrategy.GetStatusText(RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel,System.Threading.CancellationToken)')
- [ConnectorToggleExtensions](#T-RecordPoint-Connectors-SDK-Connectors-ConnectorToggleExtensions 'RecordPoint.Connectors.SDK.Connectors.ConnectorToggleExtensions')
  - [GetConnectorBinarySubmissionKillswitch(toggleProvider,systemContext,tenantId)](#M-RecordPoint-Connectors-SDK-Connectors-ConnectorToggleExtensions-GetConnectorBinarySubmissionKillswitch-RecordPoint-Connectors-SDK-Toggles-IToggleProvider,RecordPoint-Connectors-SDK-Context-ISystemContext,System-String- 'RecordPoint.Connectors.SDK.Connectors.ConnectorToggleExtensions.GetConnectorBinarySubmissionKillswitch(RecordPoint.Connectors.SDK.Toggles.IToggleProvider,RecordPoint.Connectors.SDK.Context.ISystemContext,System.String)')
  - [GetConnectorContentProtection(toggleProvider,systemContext,tenantId)](#M-RecordPoint-Connectors-SDK-Connectors-ConnectorToggleExtensions-GetConnectorContentProtection-RecordPoint-Connectors-SDK-Toggles-IToggleProvider,RecordPoint-Connectors-SDK-Context-ISystemContext,System-String- 'RecordPoint.Connectors.SDK.Connectors.ConnectorToggleExtensions.GetConnectorContentProtection(RecordPoint.Connectors.SDK.Toggles.IToggleProvider,RecordPoint.Connectors.SDK.Context.ISystemContext,System.String)')
  - [GetConnectorEnabled(toggleProvider,systemContext,tenantId)](#M-RecordPoint-Connectors-SDK-Connectors-ConnectorToggleExtensions-GetConnectorEnabled-RecordPoint-Connectors-SDK-Toggles-IToggleProvider,RecordPoint-Connectors-SDK-Context-ISystemContext,System-String- 'RecordPoint.Connectors.SDK.Connectors.ConnectorToggleExtensions.GetConnectorEnabled(RecordPoint.Connectors.SDK.Toggles.IToggleProvider,RecordPoint.Connectors.SDK.Context.ISystemContext,System.String)')
  - [GetConnectorKillswitch(toggleProvider,systemContext)](#M-RecordPoint-Connectors-SDK-Connectors-ConnectorToggleExtensions-GetConnectorKillswitch-RecordPoint-Connectors-SDK-Toggles-IToggleProvider,RecordPoint-Connectors-SDK-Context-ISystemContext- 'RecordPoint.Connectors.SDK.Connectors.ConnectorToggleExtensions.GetConnectorKillswitch(RecordPoint.Connectors.SDK.Toggles.IToggleProvider,RecordPoint.Connectors.SDK.Context.ISystemContext)')
- [ConsoleLoggingHostBuilderExtensions](#T-RecordPoint-Connectors-SDK-Observability-Console-ConsoleLoggingHostBuilderExtensions 'RecordPoint.Connectors.SDK.Observability.Console.ConsoleLoggingHostBuilderExtensions')
  - [UseConsoleLogging()](#M-RecordPoint-Connectors-SDK-Observability-Console-ConsoleLoggingHostBuilderExtensions-UseConsoleLogging-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Observability.Console.ConsoleLoggingHostBuilderExtensions.UseConsoleLogging(Microsoft.Extensions.Hosting.IHostBuilder)')
- [ConsoleLoggingSink](#T-RecordPoint-Connectors-SDK-Observability-Console-ConsoleLoggingSink 'RecordPoint.Connectors.SDK.Observability.Console.ConsoleLoggingSink')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Observability-Console-ConsoleLoggingSink-#ctor-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Observability-Console-ConsoleLoggingOptions}- 'RecordPoint.Connectors.SDK.Observability.Console.ConsoleLoggingSink.#ctor(Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Observability.Console.ConsoleLoggingOptions})')
  - [TrackEvent(name,dimensions,measures)](#M-RecordPoint-Connectors-SDK-Observability-Console-ConsoleLoggingSink-TrackEvent-System-String,RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures- 'RecordPoint.Connectors.SDK.Observability.Console.ConsoleLoggingSink.TrackEvent(System.String,RecordPoint.Connectors.SDK.Observability.Dimensions,RecordPoint.Connectors.SDK.Observability.Measures)')
  - [TrackException(exception,dimensions,measures)](#M-RecordPoint-Connectors-SDK-Observability-Console-ConsoleLoggingSink-TrackException-System-Exception,RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures- 'RecordPoint.Connectors.SDK.Observability.Console.ConsoleLoggingSink.TrackException(System.Exception,RecordPoint.Connectors.SDK.Observability.Dimensions,RecordPoint.Connectors.SDK.Observability.Measures)')
  - [TrackTrace(message,severityLevel,dimensions)](#M-RecordPoint-Connectors-SDK-Observability-Console-ConsoleLoggingSink-TrackTrace-System-String,RecordPoint-Connectors-SDK-Observability-SeverityLevel,RecordPoint-Connectors-SDK-Observability-Dimensions- 'RecordPoint.Connectors.SDK.Observability.Console.ConsoleLoggingSink.TrackTrace(System.String,RecordPoint.Connectors.SDK.Observability.SeverityLevel,RecordPoint.Connectors.SDK.Observability.Dimensions)')
- [ContentManagerActionProvider](#T-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerActionProvider')
  - [#ctor(serviceProvider)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider-#ctor-System-IServiceProvider- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerActionProvider.#ctor(System.IServiceProvider)')
  - [_serviceProvider](#F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider-_serviceProvider 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerActionProvider._serviceProvider')
  - [CreateAggregationSubmissionCallbackAction()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider-CreateAggregationSubmissionCallbackAction-Microsoft-Extensions-DependencyInjection-IServiceScope- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerActionProvider.CreateAggregationSubmissionCallbackAction(Microsoft.Extensions.DependencyInjection.IServiceScope)')
  - [CreateAuditEventSubmissionCallbackAction()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider-CreateAuditEventSubmissionCallbackAction-Microsoft-Extensions-DependencyInjection-IServiceScope- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerActionProvider.CreateAuditEventSubmissionCallbackAction(Microsoft.Extensions.DependencyInjection.IServiceScope)')
  - [CreateBinaryRetrievalAction()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider-CreateBinaryRetrievalAction-Microsoft-Extensions-DependencyInjection-IServiceScope- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerActionProvider.CreateBinaryRetrievalAction(Microsoft.Extensions.DependencyInjection.IServiceScope)')
  - [CreateBinarySubmissionCallbackAction()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider-CreateBinarySubmissionCallbackAction-Microsoft-Extensions-DependencyInjection-IServiceScope- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerActionProvider.CreateBinarySubmissionCallbackAction(Microsoft.Extensions.DependencyInjection.IServiceScope)')
  - [CreateChannelDiscoveryAction()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider-CreateChannelDiscoveryAction-Microsoft-Extensions-DependencyInjection-IServiceScope- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerActionProvider.CreateChannelDiscoveryAction(Microsoft.Extensions.DependencyInjection.IServiceScope)')
  - [CreateContentManagerCallbackAction()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider-CreateContentManagerCallbackAction-Microsoft-Extensions-DependencyInjection-IServiceScope- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerActionProvider.CreateContentManagerCallbackAction(Microsoft.Extensions.DependencyInjection.IServiceScope)')
  - [CreateContentRegistrationAction()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider-CreateContentRegistrationAction-Microsoft-Extensions-DependencyInjection-IServiceScope- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerActionProvider.CreateContentRegistrationAction(Microsoft.Extensions.DependencyInjection.IServiceScope)')
  - [CreateContentSynchronisationAction()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider-CreateContentSynchronisationAction-Microsoft-Extensions-DependencyInjection-IServiceScope- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerActionProvider.CreateContentSynchronisationAction(Microsoft.Extensions.DependencyInjection.IServiceScope)')
  - [CreateGenericAction\`\`2()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider-CreateGenericAction``2-Microsoft-Extensions-DependencyInjection-IServiceScope- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerActionProvider.CreateGenericAction``2(Microsoft.Extensions.DependencyInjection.IServiceScope)')
  - [CreateGenericManagedAction\`\`2()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider-CreateGenericManagedAction``2-Microsoft-Extensions-DependencyInjection-IServiceScope- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerActionProvider.CreateGenericManagedAction``2(Microsoft.Extensions.DependencyInjection.IServiceScope)')
  - [CreateRecordDisposalAction()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider-CreateRecordDisposalAction-Microsoft-Extensions-DependencyInjection-IServiceScope- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerActionProvider.CreateRecordDisposalAction(Microsoft.Extensions.DependencyInjection.IServiceScope)')
  - [CreateRecordSubmissionCallbackAction()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider-CreateRecordSubmissionCallbackAction-Microsoft-Extensions-DependencyInjection-IServiceScope- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerActionProvider.CreateRecordSubmissionCallbackAction(Microsoft.Extensions.DependencyInjection.IServiceScope)')
- [ContentManagerBuilderExtensions](#T-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerBuilderExtensions')
  - [UseAggregationSubmissionOperation(hostBuilder)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseAggregationSubmissionOperation-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerBuilderExtensions.UseAggregationSubmissionOperation(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseAggregationSubmissionOperation\`\`1(hostBuilder)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseAggregationSubmissionOperation``1-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerBuilderExtensions.UseAggregationSubmissionOperation``1(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseAuditEventSubmissionOperation(hostBuilder)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseAuditEventSubmissionOperation-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerBuilderExtensions.UseAuditEventSubmissionOperation(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseAuditEventSubmissionOperation\`\`1(hostBuilder)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseAuditEventSubmissionOperation``1-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerBuilderExtensions.UseAuditEventSubmissionOperation``1(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseBinarySubmissionOperation\`\`1(hostBuilder)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseBinarySubmissionOperation``1-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerBuilderExtensions.UseBinarySubmissionOperation``1(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseBinarySubmissionOperation\`\`2(hostBuilder)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseBinarySubmissionOperation``2-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerBuilderExtensions.UseBinarySubmissionOperation``2(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseChannelDiscoveryOperation\`\`1(hostBuilder)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseChannelDiscoveryOperation``1-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerBuilderExtensions.UseChannelDiscoveryOperation``1(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseContentManagerService(hostBuilder)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseContentManagerService-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerBuilderExtensions.UseContentManagerService(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseContentManagerService\`\`1(hostBuilder)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseContentManagerService``1-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerBuilderExtensions.UseContentManagerService``1(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseContentRegistrationOperation\`\`1(hostBuilder)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseContentRegistrationOperation``1-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerBuilderExtensions.UseContentRegistrationOperation``1(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseContentSynchronisationOperation\`\`1(hostBuilder)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseContentSynchronisationOperation``1-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerBuilderExtensions.UseContentSynchronisationOperation``1(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseGenericManagedWorkOperation\`\`5(hostBuilder,configSectionName)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseGenericManagedWorkOperation``5-Microsoft-Extensions-Hosting-IHostBuilder,System-String- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerBuilderExtensions.UseGenericManagedWorkOperation``5(Microsoft.Extensions.Hosting.IHostBuilder,System.String)')
  - [UseGenericQueueableWorkOperation\`\`4(hostBuilder)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseGenericQueueableWorkOperation``4-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerBuilderExtensions.UseGenericQueueableWorkOperation``4(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseRecordDisposalOperation\`\`1(hostBuilder)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseRecordDisposalOperation``1-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerBuilderExtensions.UseRecordDisposalOperation``1(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseRecordSubmissionOperation(hostBuilder)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseRecordSubmissionOperation-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerBuilderExtensions.UseRecordSubmissionOperation(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseRecordSubmissionOperation\`\`1(hostBuilder)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseRecordSubmissionOperation``1-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerBuilderExtensions.UseRecordSubmissionOperation``1(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseSynchronousRecordSubmissionOperation\`\`1(hostBuilder)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseSynchronousRecordSubmissionOperation``1-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerBuilderExtensions.UseSynchronousRecordSubmissionOperation``1(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseSynchronousRecordSubmissionOperation\`\`3(hostBuilder)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseSynchronousRecordSubmissionOperation``3-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerBuilderExtensions.UseSynchronousRecordSubmissionOperation``3(Microsoft.Extensions.Hosting.IHostBuilder)')
- [ContentManagerOperation](#T-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperation')
  - [#ctor(serviceProvider,contentManagerActionProvider,connectorConfigManager,channelManager,managedWorkStatusManager,managedWorkFactory,systemContext,options,observabilityScope,telemetryTracker,dateTimeProvider)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider,RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,RecordPoint-Connectors-SDK-Content-IChannelManager,RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager,RecordPoint-Connectors-SDK-Work-IManagedWorkFactory,RecordPoint-Connectors-SDK-Context-ISystemContext,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions},RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperation.#ctor(System.IServiceProvider,RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider,RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager,RecordPoint.Connectors.SDK.Content.IChannelManager,RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager,RecordPoint.Connectors.SDK.Work.IManagedWorkFactory,RecordPoint.Connectors.SDK.Context.ISystemContext,Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.ContentManagerOptions},RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,RecordPoint.Connectors.SDK.Providers.IDateTimeProvider)')
  - [CONTENT_SOURCE_INTEGRATION_COMPLETED](#F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-CONTENT_SOURCE_INTEGRATION_COMPLETED 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperation.CONTENT_SOURCE_INTEGRATION_COMPLETED')
  - [WORK_TYPE](#F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-WORK_TYPE 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperation.WORK_TYPE')
  - [_channelManager](#F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-_channelManager 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperation._channelManager')
  - [_connectorConfigurationManager](#F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-_connectorConfigurationManager 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperation._connectorConfigurationManager')
  - [_connectorConfigurations](#F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-_connectorConfigurations 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperation._connectorConfigurations')
  - [_contentManagerActionProvider](#F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-_contentManagerActionProvider 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperation._contentManagerActionProvider')
  - [_managedWorkFactory](#F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-_managedWorkFactory 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperation._managedWorkFactory')
  - [_managedWorkStatusManager](#F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-_managedWorkStatusManager 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperation._managedWorkStatusManager')
  - [_options](#F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-_options 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperation._options')
  - [ServiceName](#P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-ServiceName 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperation.ServiceName')
  - [WorkType](#P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-WorkType 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperation.WorkType')
  - [CleanupAggregationsAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-CleanupAggregationsAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperation.CleanupAggregationsAsync(System.Threading.CancellationToken)')
  - [CleanupChannelsAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-CleanupChannelsAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperation.CleanupChannelsAsync(System.Threading.CancellationToken)')
  - [CleanupWorkAsync(status,maxWorkAge,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-CleanupWorkAsync-RecordPoint-Connectors-SDK-Work-ManagedWorkStatuses,System-Int32,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperation.CleanupWorkAsync(RecordPoint.Connectors.SDK.Work.ManagedWorkStatuses,System.Int32,System.Threading.CancellationToken)')
  - [CreateChannelDiscoveryOperationsAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-CreateChannelDiscoveryOperationsAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperation.CreateChannelDiscoveryOperationsAsync(System.Threading.CancellationToken)')
  - [DeserializeConfiguration(configurationType,configurationText)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-DeserializeConfiguration-System-String,System-String- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperation.DeserializeConfiguration(System.String,System.String)')
  - [DeserializeState(stateType,stateText)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-DeserializeState-System-String,System-String- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperation.DeserializeState(System.String,System.String)')
  - [GetCustomResultMeasures()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-GetCustomResultMeasures 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperation.GetCustomResultMeasures')
  - [GetNewEnabledConnectorConfigurationsAsync()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-GetNewEnabledConnectorConfigurationsAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperation.GetNewEnabledConnectorConfigurationsAsync(System.Threading.CancellationToken)')
  - [InnerRunAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-InnerRunAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperation.InnerRunAsync(System.Threading.CancellationToken)')
  - [SerializeState(state)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-SerializeState-RecordPoint-Connectors-SDK-ContentManager-ContentManagerState- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperation.SerializeState(RecordPoint.Connectors.SDK.ContentManager.ContentManagerState)')
- [ContentManagerService](#T-RecordPoint-Connectors-SDK-ContentManager-ContentManagerService 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerService')
  - [#ctor(options,managedWorkFactory,managedWorkStatusManager)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerService-#ctor-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions},RecordPoint-Connectors-SDK-Work-IManagedWorkFactory,RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerService.#ctor(Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.ContentManagerOptions},RecordPoint.Connectors.SDK.Work.IManagedWorkFactory,RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager)')
  - [ExecuteAsync(stoppingToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerService-ExecuteAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerService.ExecuteAsync(System.Threading.CancellationToken)')
- [ContentRegistrationOperation](#T-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation')
  - [#ctor(serviceProvider,contentManagerActionProvider,connectorManager,channelManager,workQueueClient,managedWorkFactory,systemContext,observabilityScope,toggleProvider,telemetryTracker,dateTimeProvider,options,contentManagerOptions,recordSubmissionOptions)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider,RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,RecordPoint-Connectors-SDK-Content-IChannelManager,RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-Work-IManagedWorkFactory,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Toggles-IToggleProvider,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperationOptions},Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions},Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-RecordSubmissionOptions}- 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation.#ctor(System.IServiceProvider,RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider,RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager,RecordPoint.Connectors.SDK.Content.IChannelManager,RecordPoint.Connectors.SDK.Work.IWorkQueueClient,RecordPoint.Connectors.SDK.Work.IManagedWorkFactory,RecordPoint.Connectors.SDK.Context.ISystemContext,RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Toggles.IToggleProvider,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,RecordPoint.Connectors.SDK.Providers.IDateTimeProvider,Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperationOptions},Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.ContentManagerOptions},Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.RecordSubmissionOptions})')
  - [WORK_TYPE](#F-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-WORK_TYPE 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation.WORK_TYPE')
  - [_actionExecutionTimespan](#F-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-_actionExecutionTimespan 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation._actionExecutionTimespan')
  - [_channelManager](#F-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-_channelManager 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation._channelManager')
  - [_connectorConfiguration](#F-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-_connectorConfiguration 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation._connectorConfiguration')
  - [_connectorManager](#F-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-_connectorManager 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation._connectorManager')
  - [_contentManagerActionProvider](#F-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-_contentManagerActionProvider 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation._contentManagerActionProvider')
  - [_contentManagerOptions](#F-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-_contentManagerOptions 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation._contentManagerOptions')
  - [_contentResult](#F-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-_contentResult 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation._contentResult')
  - [_options](#F-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-_options 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation._options')
  - [_recordSubmissionOptions](#F-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-_recordSubmissionOptions 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation._recordSubmissionOptions')
  - [_submitTimespan](#F-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-_submitTimespan 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation._submitTimespan')
  - [_toggleProvider](#F-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-_toggleProvider 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation._toggleProvider')
  - [_workQueueClient](#F-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-_workQueueClient 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation._workQueueClient')
  - [ServiceName](#P-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-ServiceName 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation.ServiceName')
  - [WorkType](#P-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-WorkType 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation.WorkType')
  - [BeginAsync(channel,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-BeginAsync-RecordPoint-Connectors-SDK-Content-Channel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation.BeginAsync(RecordPoint.Connectors.SDK.Content.Channel,System.Threading.CancellationToken)')
  - [ContinueSync(channel,cursor,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-ContinueSync-RecordPoint-Connectors-SDK-Content-Channel,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation.ContinueSync(RecordPoint.Connectors.SDK.Content.Channel,System.String,System.Threading.CancellationToken)')
  - [DeserializeConfiguration(configurationType,configurationText)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-DeserializeConfiguration-System-String,System-String- 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation.DeserializeConfiguration(System.String,System.String)')
  - [DeserializeState(stateType,stateText)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-DeserializeState-System-String,System-String- 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation.DeserializeState(System.String,System.String)')
  - [FetchAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-FetchAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation.FetchAsync(System.Threading.CancellationToken)')
  - [GetCustomKeyDimensions()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-GetCustomKeyDimensions 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation.GetCustomKeyDimensions')
  - [GetCustomResultDimensions()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-GetCustomResultDimensions 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation.GetCustomResultDimensions')
  - [GetCustomResultMeasures()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-GetCustomResultMeasures 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation.GetCustomResultMeasures')
  - [HandleAbandondedContentAsync(contentResult,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-HandleAbandondedContentAsync-RecordPoint-Connectors-SDK-ContentManager-ContentResult,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation.HandleAbandondedContentAsync(RecordPoint.Connectors.SDK.ContentManager.ContentResult,System.Threading.CancellationToken)')
  - [HandleCompleteContentAsync(contentResult,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-HandleCompleteContentAsync-RecordPoint-Connectors-SDK-ContentManager-ContentResult,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation.HandleCompleteContentAsync(RecordPoint.Connectors.SDK.ContentManager.ContentResult,System.Threading.CancellationToken)')
  - [HandleFailedContentAsync(contentResult,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-HandleFailedContentAsync-RecordPoint-Connectors-SDK-ContentManager-ContentResult,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation.HandleFailedContentAsync(RecordPoint.Connectors.SDK.ContentManager.ContentResult,System.Threading.CancellationToken)')
  - [HandleIncompleteContentAsync(contentResult,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-HandleIncompleteContentAsync-RecordPoint-Connectors-SDK-ContentManager-ContentResult,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation.HandleIncompleteContentAsync(RecordPoint.Connectors.SDK.ContentManager.ContentResult,System.Threading.CancellationToken)')
  - [InnerDispose()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-InnerDispose 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation.InnerDispose')
  - [InnerRunAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-InnerRunAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation.InnerRunAsync(System.Threading.CancellationToken)')
  - [SerializeState(state)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-SerializeState-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationState- 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation.SerializeState(RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationState)')
  - [SubmitContentAsync(contentResult,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-SubmitContentAsync-RecordPoint-Connectors-SDK-ContentManager-ContentResult,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation.SubmitContentAsync(RecordPoint.Connectors.SDK.ContentManager.ContentResult,System.Threading.CancellationToken)')
- [ContentSynchronisationOperation](#T-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation')
  - [#ctor(serviceProvider,contentManagerActionProvider,connectorManager,channelManager,workQueueClient,managedWorkFactory,systemContext,observabilityScope,toggleProvider,telemetryTracker,dateTimeProvider,options,contentManagerOptions,recordSubmissionOptions)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider,RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,RecordPoint-Connectors-SDK-Content-IChannelManager,RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-Work-IManagedWorkFactory,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Toggles-IToggleProvider,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperationOptions},Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions},Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-RecordSubmissionOptions}- 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation.#ctor(System.IServiceProvider,RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider,RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager,RecordPoint.Connectors.SDK.Content.IChannelManager,RecordPoint.Connectors.SDK.Work.IWorkQueueClient,RecordPoint.Connectors.SDK.Work.IManagedWorkFactory,RecordPoint.Connectors.SDK.Context.ISystemContext,RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Toggles.IToggleProvider,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,RecordPoint.Connectors.SDK.Providers.IDateTimeProvider,Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperationOptions},Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.ContentManagerOptions},Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.RecordSubmissionOptions})')
  - [WORK_TYPE](#F-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-WORK_TYPE 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation.WORK_TYPE')
  - [_actionExecutionTimespan](#F-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-_actionExecutionTimespan 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation._actionExecutionTimespan')
  - [_channelManager](#F-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-_channelManager 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation._channelManager')
  - [_connectorConfiguration](#F-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-_connectorConfiguration 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation._connectorConfiguration')
  - [_connectorManager](#F-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-_connectorManager 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation._connectorManager')
  - [_contentManagerActionProvider](#F-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-_contentManagerActionProvider 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation._contentManagerActionProvider')
  - [_contentManagerOptions](#F-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-_contentManagerOptions 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation._contentManagerOptions')
  - [_contentResult](#F-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-_contentResult 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation._contentResult')
  - [_options](#F-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-_options 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation._options')
  - [_recordSubmissionOptions](#F-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-_recordSubmissionOptions 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation._recordSubmissionOptions')
  - [_submitTimespan](#F-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-_submitTimespan 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation._submitTimespan')
  - [_toggleProvider](#F-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-_toggleProvider 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation._toggleProvider')
  - [_workQueueClient](#F-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-_workQueueClient 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation._workQueueClient')
  - [ServiceName](#P-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-ServiceName 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation.ServiceName')
  - [WorkType](#P-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-WorkType 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation.WorkType')
  - [BeginAsync(channel,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-BeginAsync-RecordPoint-Connectors-SDK-Content-Channel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation.BeginAsync(RecordPoint.Connectors.SDK.Content.Channel,System.Threading.CancellationToken)')
  - [ContinueSync(channel,cursor,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-ContinueSync-RecordPoint-Connectors-SDK-Content-Channel,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation.ContinueSync(RecordPoint.Connectors.SDK.Content.Channel,System.String,System.Threading.CancellationToken)')
  - [DeserializeConfiguration(configurationType,configurationText)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-DeserializeConfiguration-System-String,System-String- 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation.DeserializeConfiguration(System.String,System.String)')
  - [DeserializeState(stateType,stateText)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-DeserializeState-System-String,System-String- 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation.DeserializeState(System.String,System.String)')
  - [FetchAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-FetchAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation.FetchAsync(System.Threading.CancellationToken)')
  - [GetCustomKeyDimensions()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-GetCustomKeyDimensions 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation.GetCustomKeyDimensions')
  - [GetCustomResultDimensions()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-GetCustomResultDimensions 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation.GetCustomResultDimensions')
  - [GetCustomResultMeasures()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-GetCustomResultMeasures 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation.GetCustomResultMeasures')
  - [GetInitialStartSyncTimeAsync()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-GetInitialStartSyncTimeAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation.GetInitialStartSyncTimeAsync(System.Threading.CancellationToken)')
  - [HandleAbandondedContentAsync(contentResult,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-HandleAbandondedContentAsync-RecordPoint-Connectors-SDK-ContentManager-ContentResult,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation.HandleAbandondedContentAsync(RecordPoint.Connectors.SDK.ContentManager.ContentResult,System.Threading.CancellationToken)')
  - [HandleCompleteContentAsync(contentResult,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-HandleCompleteContentAsync-RecordPoint-Connectors-SDK-ContentManager-ContentResult,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation.HandleCompleteContentAsync(RecordPoint.Connectors.SDK.ContentManager.ContentResult,System.Threading.CancellationToken)')
  - [HandleFailedContentAsync(contentResult,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-HandleFailedContentAsync-RecordPoint-Connectors-SDK-ContentManager-ContentResult,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation.HandleFailedContentAsync(RecordPoint.Connectors.SDK.ContentManager.ContentResult,System.Threading.CancellationToken)')
  - [HandleIncompleteContentAsync(contentResult,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-HandleIncompleteContentAsync-RecordPoint-Connectors-SDK-ContentManager-ContentResult,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation.HandleIncompleteContentAsync(RecordPoint.Connectors.SDK.ContentManager.ContentResult,System.Threading.CancellationToken)')
  - [HandleSuccessfulContentAsync(contentResult,backOffSeconds,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-HandleSuccessfulContentAsync-RecordPoint-Connectors-SDK-ContentManager-ContentResult,System-Int32,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation.HandleSuccessfulContentAsync(RecordPoint.Connectors.SDK.ContentManager.ContentResult,System.Int32,System.Threading.CancellationToken)')
  - [InnerDispose()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-InnerDispose 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation.InnerDispose')
  - [InnerRunAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-InnerRunAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation.InnerRunAsync(System.Threading.CancellationToken)')
  - [SerializeState(state)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-SerializeState-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationState- 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation.SerializeState(RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationState)')
- [ContextTelemetryExtensions](#T-RecordPoint-Connectors-SDK-Context-ContextTelemetryExtensions 'RecordPoint.Connectors.SDK.Context.ContextTelemetryExtensions')
  - [BeginServiceScope(observabilityScope,serviceId)](#M-RecordPoint-Connectors-SDK-Context-ContextTelemetryExtensions-BeginServiceScope-RecordPoint-Connectors-SDK-Observability-IObservabilityScope,System-String- 'RecordPoint.Connectors.SDK.Context.ContextTelemetryExtensions.BeginServiceScope(RecordPoint.Connectors.SDK.Observability.IObservabilityScope,System.String)')
  - [BeginSystemScope(observabilityScope,systemContext)](#M-RecordPoint-Connectors-SDK-Context-ContextTelemetryExtensions-BeginSystemScope-RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Context-ISystemContext- 'RecordPoint.Connectors.SDK.Context.ContextTelemetryExtensions.BeginSystemScope(RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Context.ISystemContext)')
- [DefaultHealthCheckLiveAction](#T-RecordPoint-Connectors-SDK-Health-DefaultHealthCheckLiveAction 'RecordPoint.Connectors.SDK.Health.DefaultHealthCheckLiveAction')
  - [#ctor(healthCheckManager)](#M-RecordPoint-Connectors-SDK-Health-DefaultHealthCheckLiveAction-#ctor-RecordPoint-Connectors-SDK-Health-IHealthCheckManager- 'RecordPoint.Connectors.SDK.Health.DefaultHealthCheckLiveAction.#ctor(RecordPoint.Connectors.SDK.Health.IHealthCheckManager)')
  - [CheckIsLiveAsync()](#M-RecordPoint-Connectors-SDK-Health-DefaultHealthCheckLiveAction-CheckIsLiveAsync 'RecordPoint.Connectors.SDK.Health.DefaultHealthCheckLiveAction.CheckIsLiveAsync')
- [DefaultHealthCheckReadyAction](#T-RecordPoint-Connectors-SDK-Health-DefaultHealthCheckReadyAction 'RecordPoint.Connectors.SDK.Health.DefaultHealthCheckReadyAction')
  - [#ctor(healthCheckManager)](#M-RecordPoint-Connectors-SDK-Health-DefaultHealthCheckReadyAction-#ctor-RecordPoint-Connectors-SDK-Health-IHealthCheckManager- 'RecordPoint.Connectors.SDK.Health.DefaultHealthCheckReadyAction.#ctor(RecordPoint.Connectors.SDK.Health.IHealthCheckManager)')
  - [CheckIsReadyAsync()](#M-RecordPoint-Connectors-SDK-Health-DefaultHealthCheckReadyAction-CheckIsReadyAsync 'RecordPoint.Connectors.SDK.Health.DefaultHealthCheckReadyAction.CheckIsReadyAsync')
- [DictionaryToggleProvider](#T-RecordPoint-Connectors-SDK-Toggles-Development-DictionaryToggleProvider 'RecordPoint.Connectors.SDK.Toggles.Development.DictionaryToggleProvider')
  - [Toggles](#P-RecordPoint-Connectors-SDK-Toggles-Development-DictionaryToggleProvider-Toggles 'RecordPoint.Connectors.SDK.Toggles.Development.DictionaryToggleProvider.Toggles')
  - [GetToggleBool(toggle,default)](#M-RecordPoint-Connectors-SDK-Toggles-Development-DictionaryToggleProvider-GetToggleBool-System-String,System-Boolean- 'RecordPoint.Connectors.SDK.Toggles.Development.DictionaryToggleProvider.GetToggleBool(System.String,System.Boolean)')
  - [GetToggleBool(toggle,userKey,default)](#M-RecordPoint-Connectors-SDK-Toggles-Development-DictionaryToggleProvider-GetToggleBool-System-String,System-String,System-Boolean- 'RecordPoint.Connectors.SDK.Toggles.Development.DictionaryToggleProvider.GetToggleBool(System.String,System.String,System.Boolean)')
  - [GetToggleNumber()](#M-RecordPoint-Connectors-SDK-Toggles-Development-DictionaryToggleProvider-GetToggleNumber-System-String,System-String,System-Int32- 'RecordPoint.Connectors.SDK.Toggles.Development.DictionaryToggleProvider.GetToggleNumber(System.String,System.String,System.Int32)')
  - [GetToggleNumber()](#M-RecordPoint-Connectors-SDK-Toggles-Development-DictionaryToggleProvider-GetToggleNumber-System-String,System-Int32- 'RecordPoint.Connectors.SDK.Toggles.Development.DictionaryToggleProvider.GetToggleNumber(System.String,System.Int32)')
  - [GetToggleString()](#M-RecordPoint-Connectors-SDK-Toggles-Development-DictionaryToggleProvider-GetToggleString-System-String,System-String,System-String- 'RecordPoint.Connectors.SDK.Toggles.Development.DictionaryToggleProvider.GetToggleString(System.String,System.String,System.String)')
  - [GetToggleString()](#M-RecordPoint-Connectors-SDK-Toggles-Development-DictionaryToggleProvider-GetToggleString-System-String,System-String- 'RecordPoint.Connectors.SDK.Toggles.Development.DictionaryToggleProvider.GetToggleString(System.String,System.String)')
- [EncryptionExtensions](#T-RecordPoint-Connectors-SDK-Secrets-EncryptionExtensions 'RecordPoint.Connectors.SDK.Secrets.EncryptionExtensions')
  - [DecryptSecret(encryptedSecret)](#M-RecordPoint-Connectors-SDK-Secrets-EncryptionExtensions-DecryptSecret-System-Byte[]- 'RecordPoint.Connectors.SDK.Secrets.EncryptionExtensions.DecryptSecret(System.Byte[])')
  - [EncryptSecret(plainText)](#M-RecordPoint-Connectors-SDK-Secrets-EncryptionExtensions-EncryptSecret-System-String- 'RecordPoint.Connectors.SDK.Secrets.EncryptionExtensions.EncryptSecret(System.String)')
  - [GetSecureSecret(plainText)](#M-RecordPoint-Connectors-SDK-Secrets-EncryptionExtensions-GetSecureSecret-System-String- 'RecordPoint.Connectors.SDK.Secrets.EncryptionExtensions.GetSecureSecret(System.String)')
- [EnvironmentExtensions](#T-RecordPoint-Connectors-SDK-Context-EnvironmentExtensions 'RecordPoint.Connectors.SDK.Context.EnvironmentExtensions')
  - [ASPNETCORE_ENVIRONMENT](#F-RecordPoint-Connectors-SDK-Context-EnvironmentExtensions-ASPNETCORE_ENVIRONMENT 'RecordPoint.Connectors.SDK.Context.EnvironmentExtensions.ASPNETCORE_ENVIRONMENT')
  - [DEVELOPMENT](#F-RecordPoint-Connectors-SDK-Context-EnvironmentExtensions-DEVELOPMENT 'RecordPoint.Connectors.SDK.Context.EnvironmentExtensions.DEVELOPMENT')
  - [GetASPNetCoreEnvironmentVariable()](#M-RecordPoint-Connectors-SDK-Context-EnvironmentExtensions-GetASPNetCoreEnvironmentVariable 'RecordPoint.Connectors.SDK.Context.EnvironmentExtensions.GetASPNetCoreEnvironmentVariable')
  - [IsDevelopmentEnvironment(environment)](#M-RecordPoint-Connectors-SDK-Context-EnvironmentExtensions-IsDevelopmentEnvironment-System-String- 'RecordPoint.Connectors.SDK.Context.EnvironmentExtensions.IsDevelopmentEnvironment(System.String)')
  - [IsDevelopmentEnvironment()](#M-RecordPoint-Connectors-SDK-Context-EnvironmentExtensions-IsDevelopmentEnvironment 'RecordPoint.Connectors.SDK.Context.EnvironmentExtensions.IsDevelopmentEnvironment')
- [ExceptionExtensions](#T-RecordPoint-Connectors-SDK-Observability-ExceptionExtensions 'RecordPoint.Connectors.SDK.Observability.ExceptionExtensions')
  - [DIMENSIONS_PROPERTY](#F-RecordPoint-Connectors-SDK-Observability-ExceptionExtensions-DIMENSIONS_PROPERTY 'RecordPoint.Connectors.SDK.Observability.ExceptionExtensions.DIMENSIONS_PROPERTY')
  - [HAS_SCOPE_PROPERTY](#F-RecordPoint-Connectors-SDK-Observability-ExceptionExtensions-HAS_SCOPE_PROPERTY 'RecordPoint.Connectors.SDK.Observability.ExceptionExtensions.HAS_SCOPE_PROPERTY')
  - [LOG_MESSAGE_PROPERTY](#F-RecordPoint-Connectors-SDK-Observability-ExceptionExtensions-LOG_MESSAGE_PROPERTY 'RecordPoint.Connectors.SDK.Observability.ExceptionExtensions.LOG_MESSAGE_PROPERTY')
  - [MEASURES_PROPERTY](#F-RecordPoint-Connectors-SDK-Observability-ExceptionExtensions-MEASURES_PROPERTY 'RecordPoint.Connectors.SDK.Observability.ExceptionExtensions.MEASURES_PROPERTY')
  - [EnsureLogMessage(ex,message)](#M-RecordPoint-Connectors-SDK-Observability-ExceptionExtensions-EnsureLogMessage-System-Exception,System-String- 'RecordPoint.Connectors.SDK.Observability.ExceptionExtensions.EnsureLogMessage(System.Exception,System.String)')
  - [GetDimensions(ex)](#M-RecordPoint-Connectors-SDK-Observability-ExceptionExtensions-GetDimensions-System-Exception- 'RecordPoint.Connectors.SDK.Observability.ExceptionExtensions.GetDimensions(System.Exception)')
  - [GetLogMessage(ex)](#M-RecordPoint-Connectors-SDK-Observability-ExceptionExtensions-GetLogMessage-System-Exception- 'RecordPoint.Connectors.SDK.Observability.ExceptionExtensions.GetLogMessage(System.Exception)')
  - [GetMeasures(ex)](#M-RecordPoint-Connectors-SDK-Observability-ExceptionExtensions-GetMeasures-System-Exception- 'RecordPoint.Connectors.SDK.Observability.ExceptionExtensions.GetMeasures(System.Exception)')
  - [HasScope(ex)](#M-RecordPoint-Connectors-SDK-Observability-ExceptionExtensions-HasScope-System-Exception- 'RecordPoint.Connectors.SDK.Observability.ExceptionExtensions.HasScope(System.Exception)')
  - [ScopeTo(ex,observabilityScope)](#M-RecordPoint-Connectors-SDK-Observability-ExceptionExtensions-ScopeTo-System-Exception,RecordPoint-Connectors-SDK-Observability-IObservabilityScope- 'RecordPoint.Connectors.SDK.Observability.ExceptionExtensions.ScopeTo(System.Exception,RecordPoint.Connectors.SDK.Observability.IObservabilityScope)')
  - [SetLogMessage(ex,message)](#M-RecordPoint-Connectors-SDK-Observability-ExceptionExtensions-SetLogMessage-System-Exception,System-String- 'RecordPoint.Connectors.SDK.Observability.ExceptionExtensions.SetLogMessage(System.Exception,System.String)')
- [FeatureToggleModel](#T-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-FeatureToggleModel 'RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles.FeatureToggleModel')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-FeatureToggleModel-#ctor 'RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles.FeatureToggleModel.#ctor')
  - [UserKeyOverrides](#P-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-FeatureToggleModel-UserKeyOverrides 'RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles.FeatureToggleModel.UserKeyOverrides')
  - [Value](#P-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-FeatureToggleModel-Value 'RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles.FeatureToggleModel.Value')
- [FileReader](#T-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-FileReader 'RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles.FileReader')
  - [ReadAllText(path)](#M-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-FileReader-ReadAllText-System-String- 'RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles.FileReader.ReadAllText(System.String)')
- [HealthCheckBuilderExtensions](#T-RecordPoint-Connectors-SDK-Health-HealthCheckBuilderExtensions 'RecordPoint.Connectors.SDK.Health.HealthCheckBuilderExtensions')
  - [UseHealthChecker(hostBuilder)](#M-RecordPoint-Connectors-SDK-Health-HealthCheckBuilderExtensions-UseHealthChecker-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Health.HealthCheckBuilderExtensions.UseHealthChecker(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseHealthChecker\`\`2(hostBuilder)](#M-RecordPoint-Connectors-SDK-Health-HealthCheckBuilderExtensions-UseHealthChecker``2-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Health.HealthCheckBuilderExtensions.UseHealthChecker``2(Microsoft.Extensions.Hosting.IHostBuilder)')
- [HealthCheckManager](#T-RecordPoint-Connectors-SDK-Health-HealthCheckManager 'RecordPoint.Connectors.SDK.Health.HealthCheckManager')
  - [#ctor(healthCheckStrategies,dateTimeProvider)](#M-RecordPoint-Connectors-SDK-Health-HealthCheckManager-#ctor-System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Health-IHealthCheckStrategy},RecordPoint-Connectors-SDK-Providers-IDateTimeProvider- 'RecordPoint.Connectors.SDK.Health.HealthCheckManager.#ctor(System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Health.IHealthCheckStrategy},RecordPoint.Connectors.SDK.Providers.IDateTimeProvider)')
  - [HEALTH_CHECK_FAULT_NAME](#F-RecordPoint-Connectors-SDK-Health-HealthCheckManager-HEALTH_CHECK_FAULT_NAME 'RecordPoint.Connectors.SDK.Health.HealthCheckManager.HEALTH_CHECK_FAULT_NAME')
  - [HealthCheckResult](#P-RecordPoint-Connectors-SDK-Health-HealthCheckManager-HealthCheckResult 'RecordPoint.Connectors.SDK.Health.HealthCheckManager.HealthCheckResult')
  - [RunHealthCheckAsync()](#M-RecordPoint-Connectors-SDK-Health-HealthCheckManager-RunHealthCheckAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Health.HealthCheckManager.RunHealthCheckAsync(System.Threading.CancellationToken)')
- [HealthCheckService](#T-RecordPoint-Connectors-SDK-Health-HealthCheckService 'RecordPoint.Connectors.SDK.Health.HealthCheckService')
  - [#ctor(healthCheckManager,systemContext,observabilityScope,healthCheckOptions)](#M-RecordPoint-Connectors-SDK-Health-HealthCheckService-#ctor-RecordPoint-Connectors-SDK-Health-IHealthCheckManager,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Health-HealthCheckOptions}- 'RecordPoint.Connectors.SDK.Health.HealthCheckService.#ctor(RecordPoint.Connectors.SDK.Health.IHealthCheckManager,RecordPoint.Connectors.SDK.Context.ISystemContext,RecordPoint.Connectors.SDK.Observability.IObservabilityScope,Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Health.HealthCheckOptions})')
  - [_healthCheckManager](#F-RecordPoint-Connectors-SDK-Health-HealthCheckService-_healthCheckManager 'RecordPoint.Connectors.SDK.Health.HealthCheckService._healthCheckManager')
  - [_healthCheckOptions](#F-RecordPoint-Connectors-SDK-Health-HealthCheckService-_healthCheckOptions 'RecordPoint.Connectors.SDK.Health.HealthCheckService._healthCheckOptions')
  - [_observabilityScope](#F-RecordPoint-Connectors-SDK-Health-HealthCheckService-_observabilityScope 'RecordPoint.Connectors.SDK.Health.HealthCheckService._observabilityScope')
  - [_systemContext](#F-RecordPoint-Connectors-SDK-Health-HealthCheckService-_systemContext 'RecordPoint.Connectors.SDK.Health.HealthCheckService._systemContext')
  - [CheckDelay](#P-RecordPoint-Connectors-SDK-Health-HealthCheckService-CheckDelay 'RecordPoint.Connectors.SDK.Health.HealthCheckService.CheckDelay')
  - [StartDelay](#P-RecordPoint-Connectors-SDK-Health-HealthCheckService-StartDelay 'RecordPoint.Connectors.SDK.Health.HealthCheckService.StartDelay')
  - [ExecuteAsync(stoppingToken)](#M-RecordPoint-Connectors-SDK-Health-HealthCheckService-ExecuteAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Health.HealthCheckService.ExecuteAsync(System.Threading.CancellationToken)')
- [IFileReader](#T-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-IFileReader 'RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles.IFileReader')
  - [ReadAllText(path)](#M-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-IFileReader-ReadAllText-System-String- 'RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles.IFileReader.ReadAllText(System.String)')
- [ISystemContextExtensions](#T-RecordPoint-Connectors-SDK-Observability-ISystemContextExtensions 'RecordPoint.Connectors.SDK.Observability.ISystemContextExtensions')
  - [GetDimensions(systemContext)](#M-RecordPoint-Connectors-SDK-Observability-ISystemContextExtensions-GetDimensions-RecordPoint-Connectors-SDK-Context-ISystemContext- 'RecordPoint.Connectors.SDK.Observability.ISystemContextExtensions.GetDimensions(RecordPoint.Connectors.SDK.Context.ISystemContext)')
- [ITelemetryClientFactory](#T-RecordPoint-Connectors-SDK-Observability-AppInsights-ITelemetryClientFactory 'RecordPoint.Connectors.SDK.Observability.AppInsights.ITelemetryClientFactory')
  - [GetTelemetryClient()](#M-RecordPoint-Connectors-SDK-Observability-AppInsights-ITelemetryClientFactory-GetTelemetryClient 'RecordPoint.Connectors.SDK.Observability.AppInsights.ITelemetryClientFactory.GetTelemetryClient')
- [ITelemetryTrackerExtensions](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTrackerExtensions 'RecordPoint.Connectors.SDK.Observability.ITelemetryTrackerExtensions')
  - [CreateRootServiceScope(telemetryTracker,systemContext,dimensions,measures)](#M-RecordPoint-Connectors-SDK-Observability-ITelemetryTrackerExtensions-CreateRootServiceScope-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures- 'RecordPoint.Connectors.SDK.Observability.ITelemetryTrackerExtensions.CreateRootServiceScope(RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,RecordPoint.Connectors.SDK.Context.ISystemContext,RecordPoint.Connectors.SDK.Observability.Dimensions,RecordPoint.Connectors.SDK.Observability.Measures)')
- [LightrunAgentService](#T-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunAgentService 'RecordPoint.Connectors.SDK.Observability.Lightrun.LightrunAgentService')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunAgentService-#ctor-RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunOptions}- 'RecordPoint.Connectors.SDK.Observability.Lightrun.LightrunAgentService.#ctor(RecordPoint.Connectors.SDK.Context.ISystemContext,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Observability.Lightrun.LightrunOptions})')
  - [Enabled](#P-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunAgentService-Enabled 'RecordPoint.Connectors.SDK.Observability.Lightrun.LightrunAgentService.Enabled')
  - [ExecuteAsync()](#M-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunAgentService-ExecuteAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Observability.Lightrun.LightrunAgentService.ExecuteAsync(System.Threading.CancellationToken)')
  - [GetAgentOptions()](#M-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunAgentService-GetAgentOptions 'RecordPoint.Connectors.SDK.Observability.Lightrun.LightrunAgentService.GetAgentOptions')
- [LightrunDynamicLogger](#T-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunDynamicLogger 'RecordPoint.Connectors.SDK.Observability.Lightrun.LightrunDynamicLogger')
  - [#ctor(telemetryTracker)](#M-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunDynamicLogger-#ctor-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker- 'RecordPoint.Connectors.SDK.Observability.Lightrun.LightrunDynamicLogger.#ctor(RecordPoint.Connectors.SDK.Observability.ITelemetryTracker)')
  - [Log(entry)](#M-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunDynamicLogger-Log-Lightrun-Agent-Logging-LogEntry- 'RecordPoint.Connectors.SDK.Observability.Lightrun.LightrunDynamicLogger.Log(Lightrun.Agent.Logging.LogEntry)')
- [LightrunHostBuilderHelperExtensions](#T-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunHostBuilderHelperExtensions 'RecordPoint.Connectors.SDK.Observability.Lightrun.LightrunHostBuilderHelperExtensions')
  - [UseLightrunAgent()](#M-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunHostBuilderHelperExtensions-UseLightrunAgent-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Observability.Lightrun.LightrunHostBuilderHelperExtensions.UseLightrunAgent(Microsoft.Extensions.Hosting.IHostBuilder)')
- [LightrunOptions](#T-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunOptions 'RecordPoint.Connectors.SDK.Observability.Lightrun.LightrunOptions')
  - [SECTION_NAME](#F-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunOptions-SECTION_NAME 'RecordPoint.Connectors.SDK.Observability.Lightrun.LightrunOptions.SECTION_NAME')
  - [AgentLogTargetDir](#P-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunOptions-AgentLogTargetDir 'RecordPoint.Connectors.SDK.Observability.Lightrun.LightrunOptions.AgentLogTargetDir')
  - [CertificatePinningEnabled](#P-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunOptions-CertificatePinningEnabled 'RecordPoint.Connectors.SDK.Observability.Lightrun.LightrunOptions.CertificatePinningEnabled')
  - [DisplayName](#P-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunOptions-DisplayName 'RecordPoint.Connectors.SDK.Observability.Lightrun.LightrunOptions.DisplayName')
  - [MaxCollectionSize](#P-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunOptions-MaxCollectionSize 'RecordPoint.Connectors.SDK.Observability.Lightrun.LightrunOptions.MaxCollectionSize')
  - [MaxDepthToSerialize](#P-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunOptions-MaxDepthToSerialize 'RecordPoint.Connectors.SDK.Observability.Lightrun.LightrunOptions.MaxDepthToSerialize')
  - [MaxFieldCount](#P-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunOptions-MaxFieldCount 'RecordPoint.Connectors.SDK.Observability.Lightrun.LightrunOptions.MaxFieldCount')
  - [MaxStringLength](#P-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunOptions-MaxStringLength 'RecordPoint.Connectors.SDK.Observability.Lightrun.LightrunOptions.MaxStringLength')
  - [Secret](#P-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunOptions-Secret 'RecordPoint.Connectors.SDK.Observability.Lightrun.LightrunOptions.Secret')
  - [ServerUrl](#P-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunOptions-ServerUrl 'RecordPoint.Connectors.SDK.Observability.Lightrun.LightrunOptions.ServerUrl')
  - [Tags](#P-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunOptions-Tags 'RecordPoint.Connectors.SDK.Observability.Lightrun.LightrunOptions.Tags')
- [LocalFeatureToggleHostBuilderExtensions](#T-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFeatureToggleHostBuilderExtensions 'RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles.LocalFeatureToggleHostBuilderExtensions')
  - [UseLocalFileToggleProvider(hostBuilder)](#M-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFeatureToggleHostBuilderExtensions-UseLocalFileToggleProvider-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles.LocalFeatureToggleHostBuilderExtensions.UseLocalFileToggleProvider(Microsoft.Extensions.Hosting.IHostBuilder)')
- [LocalFeatureToggleOptions](#T-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFeatureToggleOptions 'RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles.LocalFeatureToggleOptions')
  - [SECTION_NAME](#F-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFeatureToggleOptions-SECTION_NAME 'RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles.LocalFeatureToggleOptions.SECTION_NAME')
  - [JsonFilePath](#P-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFeatureToggleOptions-JsonFilePath 'RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles.LocalFeatureToggleOptions.JsonFilePath')
- [LocalFileToggleProvider](#T-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFileToggleProvider 'RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles.LocalFileToggleProvider')
  - [#ctor(options,fileReader)](#M-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFileToggleProvider-#ctor-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFeatureToggleOptions},RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-IFileReader- 'RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles.LocalFileToggleProvider.#ctor(Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles.LocalFeatureToggleOptions},RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles.IFileReader)')
  - [GetToggleBool(toggle,default)](#M-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFileToggleProvider-GetToggleBool-System-String,System-Boolean- 'RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles.LocalFileToggleProvider.GetToggleBool(System.String,System.Boolean)')
  - [GetToggleBool(toggle,userKey,default)](#M-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFileToggleProvider-GetToggleBool-System-String,System-String,System-Boolean- 'RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles.LocalFileToggleProvider.GetToggleBool(System.String,System.String,System.Boolean)')
  - [GetToggleNumber()](#M-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFileToggleProvider-GetToggleNumber-System-String,System-String,System-Int32- 'RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles.LocalFileToggleProvider.GetToggleNumber(System.String,System.String,System.Int32)')
  - [GetToggleNumber()](#M-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFileToggleProvider-GetToggleNumber-System-String,System-Int32- 'RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles.LocalFileToggleProvider.GetToggleNumber(System.String,System.Int32)')
  - [GetToggleString()](#M-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFileToggleProvider-GetToggleString-System-String,System-String,System-String- 'RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles.LocalFileToggleProvider.GetToggleString(System.String,System.String,System.String)')
  - [GetToggleString()](#M-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFileToggleProvider-GetToggleString-System-String,System-String- 'RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles.LocalFileToggleProvider.GetToggleString(System.String,System.String)')
  - [GetTogglesDictionaryFromJson()](#M-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFileToggleProvider-GetTogglesDictionaryFromJson 'RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles.LocalFileToggleProvider.GetTogglesDictionaryFromJson')
- [ManagedQueueableWorkBase\`2](#T-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2 'RecordPoint.Connectors.SDK.Work.ManagedQueueableWorkBase`2')
  - [#ctor(serviceProvider,managedWorkFactory,systemContext,observabilityScope,telemetryTracker,dateTimeProvider)](#M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-Work-IManagedWorkFactory,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider- 'RecordPoint.Connectors.SDK.Work.ManagedQueueableWorkBase`2.#ctor(System.IServiceProvider,RecordPoint.Connectors.SDK.Work.IManagedWorkFactory,RecordPoint.Connectors.SDK.Context.ISystemContext,RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,RecordPoint.Connectors.SDK.Providers.IDateTimeProvider)')
  - [SEMAPHORE_CONNECTOR_CONFIGURATION](#F-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-SEMAPHORE_CONNECTOR_CONFIGURATION 'RecordPoint.Connectors.SDK.Work.ManagedQueueableWorkBase`2.SEMAPHORE_CONNECTOR_CONFIGURATION')
  - [SEMAPHORE_GLOBAL](#F-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-SEMAPHORE_GLOBAL 'RecordPoint.Connectors.SDK.Work.ManagedQueueableWorkBase`2.SEMAPHORE_GLOBAL')
  - [_managedWorkFactory](#F-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-_managedWorkFactory 'RecordPoint.Connectors.SDK.Work.ManagedQueueableWorkBase`2._managedWorkFactory')
  - [Configuration](#P-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-Configuration 'RecordPoint.Connectors.SDK.Work.ManagedQueueableWorkBase`2.Configuration')
  - [State](#P-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-State 'RecordPoint.Connectors.SDK.Work.ManagedQueueableWorkBase`2.State')
  - [WorkManager](#P-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-WorkManager 'RecordPoint.Connectors.SDK.Work.ManagedQueueableWorkBase`2.WorkManager')
  - [AbandonedAsync(reason,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-AbandonedAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.ManagedQueueableWorkBase`2.AbandonedAsync(System.String,System.Threading.CancellationToken)')
  - [CalculateBackOffSeconds(settings,hasResult,lastDelaySeconds)](#M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-CalculateBackOffSeconds-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperationOptionsBase,System-Boolean,System-Int32- 'RecordPoint.Connectors.SDK.Work.ManagedQueueableWorkBase`2.CalculateBackOffSeconds(RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperationOptionsBase,System.Boolean,System.Int32)')
  - [CheckConnectorEnabledStatusAsync(connectorConfiguration,options,maxDisabledConnectorAge,state,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-CheckConnectorEnabledStatusAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperationOptionsBase,RecordPoint-Connectors-SDK-Abstractions-ContentManager-IContentDiscoveryState,System-Int32,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.ManagedQueueableWorkBase`2.CheckConnectorEnabledStatusAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperationOptionsBase,RecordPoint.Connectors.SDK.Abstractions.ContentManager.IContentDiscoveryState,System.Int32,System.Threading.CancellationToken)')
  - [CompleteAsync(reason,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-CompleteAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.ManagedQueueableWorkBase`2.CompleteAsync(System.String,System.Threading.CancellationToken)')
  - [ContinueAsync(reason,state,waitTill,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-ContinueAsync-System-String,`1,System-DateTimeOffset,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.ManagedQueueableWorkBase`2.ContinueAsync(System.String,`1,System.DateTimeOffset,System.Threading.CancellationToken)')
  - [DeserializeConfiguration(configurationType,configurationText)](#M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-DeserializeConfiguration-System-String,System-String- 'RecordPoint.Connectors.SDK.Work.ManagedQueueableWorkBase`2.DeserializeConfiguration(System.String,System.String)')
  - [DeserializeState(stateType,stateText)](#M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-DeserializeState-System-String,System-String- 'RecordPoint.Connectors.SDK.Work.ManagedQueueableWorkBase`2.DeserializeState(System.String,System.String)')
  - [FailAsync(reason,exception,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-FailAsync-System-String,System-Exception,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.ManagedQueueableWorkBase`2.FailAsync(System.String,System.Exception,System.Threading.CancellationToken)')
  - [FaultedAsync(reason,exception,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-FaultedAsync-System-String,System-Exception,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.ManagedQueueableWorkBase`2.FaultedAsync(System.String,System.Exception,System.Threading.CancellationToken)')
  - [GetCoreKeyDimensions()](#M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-GetCoreKeyDimensions 'RecordPoint.Connectors.SDK.Work.ManagedQueueableWorkBase`2.GetCoreKeyDimensions')
  - [InnerDispose()](#M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-InnerDispose 'RecordPoint.Connectors.SDK.Work.ManagedQueueableWorkBase`2.InnerDispose')
  - [RunWorkRequestAsync(workRequest,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-RunWorkRequestAsync-RecordPoint-Connectors-SDK-Work-WorkRequest,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.ManagedQueueableWorkBase`2.RunWorkRequestAsync(RecordPoint.Connectors.SDK.Work.WorkRequest,System.Threading.CancellationToken)')
  - [SerializeState()](#M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-SerializeState-`1- 'RecordPoint.Connectors.SDK.Work.ManagedQueueableWorkBase`2.SerializeState(`1)')
  - [SetOutcome(workOutcome)](#M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-SetOutcome-RecordPoint-Connectors-SDK-Work-WorkResult- 'RecordPoint.Connectors.SDK.Work.ManagedQueueableWorkBase`2.SetOutcome(RecordPoint.Connectors.SDK.Work.WorkResult)')
- [ManagedWorkBuilderExtensions](#T-RecordPoint-Connectors-SDK-Work-ManagedWorkBuilderExtensions 'RecordPoint.Connectors.SDK.Work.ManagedWorkBuilderExtensions')
  - [AddWorkStateManagement\`\`1(services)](#M-RecordPoint-Connectors-SDK-Work-ManagedWorkBuilderExtensions-AddWorkStateManagement``1-Microsoft-Extensions-DependencyInjection-IServiceCollection- 'RecordPoint.Connectors.SDK.Work.ManagedWorkBuilderExtensions.AddWorkStateManagement``1(Microsoft.Extensions.DependencyInjection.IServiceCollection)')
  - [UseWorkStateManager\`\`1(hostBuilder)](#M-RecordPoint-Connectors-SDK-Work-ManagedWorkBuilderExtensions-UseWorkStateManager``1-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Work.ManagedWorkBuilderExtensions.UseWorkStateManager``1(Microsoft.Extensions.Hosting.IHostBuilder)')
- [ManagedWorkFactory](#T-RecordPoint-Connectors-SDK-Work-ManagedWorkFactory 'RecordPoint.Connectors.SDK.Work.ManagedWorkFactory')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Work-ManagedWorkFactory-#ctor-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Connectors-ConnectorOptions},RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager,RecordPoint-Connectors-SDK-Work-IWorkQueueClient- 'RecordPoint.Connectors.SDK.Work.ManagedWorkFactory.#ctor(Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Connectors.ConnectorOptions},RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager,RecordPoint.Connectors.SDK.Work.IWorkQueueClient)')
  - [CreateWork()](#M-RecordPoint-Connectors-SDK-Work-ManagedWorkFactory-CreateWork-System-String,System-String,System-String,System-String,System-String- 'RecordPoint.Connectors.SDK.Work.ManagedWorkFactory.CreateWork(System.String,System.String,System.String,System.String,System.String)')
  - [LoadWork()](#M-RecordPoint-Connectors-SDK-Work-ManagedWorkFactory-LoadWork-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel- 'RecordPoint.Connectors.SDK.Work.ManagedWorkFactory.LoadWork(RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel)')
- [ManagedWorkManager](#T-RecordPoint-Connectors-SDK-Work-ManagedWorkManager 'RecordPoint.Connectors.SDK.Work.ManagedWorkManager')
  - [#ctor(managedWorkStatusManager,workQueueClient)](#M-RecordPoint-Connectors-SDK-Work-ManagedWorkManager-#ctor-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager,RecordPoint-Connectors-SDK-Work-IWorkQueueClient- 'RecordPoint.Connectors.SDK.Work.ManagedWorkManager.#ctor(RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager,RecordPoint.Connectors.SDK.Work.IWorkQueueClient)')
  - [WorkStatus](#P-RecordPoint-Connectors-SDK-Work-ManagedWorkManager-WorkStatus 'RecordPoint.Connectors.SDK.Work.ManagedWorkManager.WorkStatus')
  - [AbandonedAsync(reason,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-ManagedWorkManager-AbandonedAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.ManagedWorkManager.AbandonedAsync(System.String,System.Threading.CancellationToken)')
  - [CheckAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-ManagedWorkManager-CheckAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.ManagedWorkManager.CheckAsync(System.Threading.CancellationToken)')
  - [CompleteAsync(reason,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-ManagedWorkManager-CompleteAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.ManagedWorkManager.CompleteAsync(System.String,System.Threading.CancellationToken)')
  - [ContinueAsync(stateType,state,waitTill,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-ManagedWorkManager-ContinueAsync-System-String,System-String,System-DateTimeOffset,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.ManagedWorkManager.ContinueAsync(System.String,System.String,System.DateTimeOffset,System.Threading.CancellationToken)')
  - [CreateWorkStatus()](#M-RecordPoint-Connectors-SDK-Work-ManagedWorkManager-CreateWorkStatus 'RecordPoint.Connectors.SDK.Work.ManagedWorkManager.CreateWorkStatus')
  - [Dispose(disposing)](#M-RecordPoint-Connectors-SDK-Work-ManagedWorkManager-Dispose-System-Boolean- 'RecordPoint.Connectors.SDK.Work.ManagedWorkManager.Dispose(System.Boolean)')
  - [Dispose()](#M-RecordPoint-Connectors-SDK-Work-ManagedWorkManager-Dispose 'RecordPoint.Connectors.SDK.Work.ManagedWorkManager.Dispose')
  - [FailedAsync(reason,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-ManagedWorkManager-FailedAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.ManagedWorkManager.FailedAsync(System.String,System.Threading.CancellationToken)')
  - [FaultyAsync(reason,exception,cancellationToken,faultedCount)](#M-RecordPoint-Connectors-SDK-Work-ManagedWorkManager-FaultyAsync-System-String,System-Exception,System-Threading-CancellationToken,System-Nullable{System-Int32}- 'RecordPoint.Connectors.SDK.Work.ManagedWorkManager.FaultyAsync(System.String,System.Exception,System.Threading.CancellationToken,System.Nullable{System.Int32})')
  - [RetryAsync(waitTill,cancellationToken,faultedCount)](#M-RecordPoint-Connectors-SDK-Work-ManagedWorkManager-RetryAsync-System-DateTimeOffset,System-Threading-CancellationToken,System-Nullable{System-Int32}- 'RecordPoint.Connectors.SDK.Work.ManagedWorkManager.RetryAsync(System.DateTimeOffset,System.Threading.CancellationToken,System.Nullable{System.Int32})')
  - [StartAsync(cancellationToken,waitTill)](#M-RecordPoint-Connectors-SDK-Work-ManagedWorkManager-StartAsync-System-Threading-CancellationToken,System-Nullable{System-DateTimeOffset}- 'RecordPoint.Connectors.SDK.Work.ManagedWorkManager.StartAsync(System.Threading.CancellationToken,System.Nullable{System.DateTimeOffset})')
- [ManagedWorkManagerExtensions](#T-RecordPoint-Connectors-SDK-ContentManager-ManagedWorkManagerExtensions 'RecordPoint.Connectors.SDK.ContentManager.ManagedWorkManagerExtensions')
  - [CreateChannelDiscoveryOperation(managedWorkFactory,connectorConfigModel)](#M-RecordPoint-Connectors-SDK-ContentManager-ManagedWorkManagerExtensions-CreateChannelDiscoveryOperation-RecordPoint-Connectors-SDK-Work-IManagedWorkFactory,RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel- 'RecordPoint.Connectors.SDK.ContentManager.ManagedWorkManagerExtensions.CreateChannelDiscoveryOperation(RecordPoint.Connectors.SDK.Work.IManagedWorkFactory,RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel)')
  - [CreateContentManagerOperation(managedWorkFactory)](#M-RecordPoint-Connectors-SDK-ContentManager-ManagedWorkManagerExtensions-CreateContentManagerOperation-RecordPoint-Connectors-SDK-Work-IManagedWorkFactory- 'RecordPoint.Connectors.SDK.ContentManager.ManagedWorkManagerExtensions.CreateContentManagerOperation(RecordPoint.Connectors.SDK.Work.IManagedWorkFactory)')
  - [CreateContentRegistrationOperation(managedWorkFactory,connectorConfigModel,channel,context)](#M-RecordPoint-Connectors-SDK-ContentManager-ManagedWorkManagerExtensions-CreateContentRegistrationOperation-RecordPoint-Connectors-SDK-Work-IManagedWorkFactory,RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Channel,System-Collections-Generic-Dictionary{System-String,System-String}- 'RecordPoint.Connectors.SDK.ContentManager.ManagedWorkManagerExtensions.CreateContentRegistrationOperation(RecordPoint.Connectors.SDK.Work.IManagedWorkFactory,RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,RecordPoint.Connectors.SDK.Content.Channel,System.Collections.Generic.Dictionary{System.String,System.String})')
  - [CreateContentSynchronisationOperation(managedWorkFactory,connectorConfigModel,channel)](#M-RecordPoint-Connectors-SDK-ContentManager-ManagedWorkManagerExtensions-CreateContentSynchronisationOperation-RecordPoint-Connectors-SDK-Work-IManagedWorkFactory,RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Channel- 'RecordPoint.Connectors.SDK.ContentManager.ManagedWorkManagerExtensions.CreateContentSynchronisationOperation(RecordPoint.Connectors.SDK.Work.IManagedWorkFactory,RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,RecordPoint.Connectors.SDK.Content.Channel)')
  - [DeserialiseChannelDiscoveryConfiguration(workStatus)](#M-RecordPoint-Connectors-SDK-ContentManager-ManagedWorkManagerExtensions-DeserialiseChannelDiscoveryConfiguration-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel- 'RecordPoint.Connectors.SDK.ContentManager.ManagedWorkManagerExtensions.DeserialiseChannelDiscoveryConfiguration(RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel)')
  - [DeserialiseContentManagerConfiguration(workStatus)](#M-RecordPoint-Connectors-SDK-ContentManager-ManagedWorkManagerExtensions-DeserialiseContentManagerConfiguration-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel- 'RecordPoint.Connectors.SDK.ContentManager.ManagedWorkManagerExtensions.DeserialiseContentManagerConfiguration(RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel)')
  - [DeserialiseContentRegistrationConfiguration(workStatus)](#M-RecordPoint-Connectors-SDK-ContentManager-ManagedWorkManagerExtensions-DeserialiseContentRegistrationConfiguration-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel- 'RecordPoint.Connectors.SDK.ContentManager.ManagedWorkManagerExtensions.DeserialiseContentRegistrationConfiguration(RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel)')
  - [DeserialiseContentSynchronisationConfiguration(workStatus)](#M-RecordPoint-Connectors-SDK-ContentManager-ManagedWorkManagerExtensions-DeserialiseContentSynchronisationConfiguration-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel- 'RecordPoint.Connectors.SDK.ContentManager.ManagedWorkManagerExtensions.DeserialiseContentSynchronisationConfiguration(RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel)')
- [NullChannelDiscoveryAction](#T-RecordPoint-Connectors-SDK-ContentManager-NullChannelDiscoveryAction 'RecordPoint.Connectors.SDK.ContentManager.NullChannelDiscoveryAction')
  - [#ctor(channelManager)](#M-RecordPoint-Connectors-SDK-ContentManager-NullChannelDiscoveryAction-#ctor-RecordPoint-Connectors-SDK-Content-IChannelManager- 'RecordPoint.Connectors.SDK.ContentManager.NullChannelDiscoveryAction.#ctor(RecordPoint.Connectors.SDK.Content.IChannelManager)')
  - [_channelManager](#F-RecordPoint-Connectors-SDK-ContentManager-NullChannelDiscoveryAction-_channelManager 'RecordPoint.Connectors.SDK.ContentManager.NullChannelDiscoveryAction._channelManager')
  - [ExecuteAsync(connectorConfiguration,cancellationToken,cursor)](#M-RecordPoint-Connectors-SDK-ContentManager-NullChannelDiscoveryAction-ExecuteAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,System-Threading-CancellationToken,System-String- 'RecordPoint.Connectors.SDK.ContentManager.NullChannelDiscoveryAction.ExecuteAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,System.Threading.CancellationToken,System.String)')
- [NullObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-Null-NullObservabilityScope 'RecordPoint.Connectors.SDK.Observability.Null.NullObservabilityScope')
  - [Dispose(disposing)](#M-RecordPoint-Connectors-SDK-Observability-Null-NullObservabilityScope-Dispose-System-Boolean- 'RecordPoint.Connectors.SDK.Observability.Null.NullObservabilityScope.Dispose(System.Boolean)')
  - [Dispose()](#M-RecordPoint-Connectors-SDK-Observability-Null-NullObservabilityScope-Dispose 'RecordPoint.Connectors.SDK.Observability.Null.NullObservabilityScope.Dispose')
- [NullTelemetryBuilderExtensions](#T-RecordPoint-Connectors-SDK-Observability-Null-NullTelemetryBuilderExtensions 'RecordPoint.Connectors.SDK.Observability.Null.NullTelemetryBuilderExtensions')
  - [UseNullTelemetryTracking(hostBuilder)](#M-RecordPoint-Connectors-SDK-Observability-Null-NullTelemetryBuilderExtensions-UseNullTelemetryTracking-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Observability.Null.NullTelemetryBuilderExtensions.UseNullTelemetryTracking(Microsoft.Extensions.Hosting.IHostBuilder)')
- [NullTelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-Null-NullTelemetryTracker 'RecordPoint.Connectors.SDK.Observability.Null.NullTelemetryTracker')
  - [BeginScope()](#M-RecordPoint-Connectors-SDK-Observability-Null-NullTelemetryTracker-BeginScope-RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures- 'RecordPoint.Connectors.SDK.Observability.Null.NullTelemetryTracker.BeginScope(RecordPoint.Connectors.SDK.Observability.Dimensions,RecordPoint.Connectors.SDK.Observability.Measures)')
  - [TrackEvent()](#M-RecordPoint-Connectors-SDK-Observability-Null-NullTelemetryTracker-TrackEvent-System-String,RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures- 'RecordPoint.Connectors.SDK.Observability.Null.NullTelemetryTracker.TrackEvent(System.String,RecordPoint.Connectors.SDK.Observability.Dimensions,RecordPoint.Connectors.SDK.Observability.Measures)')
  - [TrackException()](#M-RecordPoint-Connectors-SDK-Observability-Null-NullTelemetryTracker-TrackException-System-Exception,RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures- 'RecordPoint.Connectors.SDK.Observability.Null.NullTelemetryTracker.TrackException(System.Exception,RecordPoint.Connectors.SDK.Observability.Dimensions,RecordPoint.Connectors.SDK.Observability.Measures)')
  - [TrackTrace()](#M-RecordPoint-Connectors-SDK-Observability-Null-NullTelemetryTracker-TrackTrace-System-String,RecordPoint-Connectors-SDK-Observability-SeverityLevel,RecordPoint-Connectors-SDK-Observability-Dimensions- 'RecordPoint.Connectors.SDK.Observability.Null.NullTelemetryTracker.TrackTrace(System.String,RecordPoint.Connectors.SDK.Observability.SeverityLevel,RecordPoint.Connectors.SDK.Observability.Dimensions)')
- [NullToggleBuilderExtensions](#T-RecordPoint-Connectors-SDK-Toggles-Null-NullToggleBuilderExtensions 'RecordPoint.Connectors.SDK.Toggles.Null.NullToggleBuilderExtensions')
  - [UseNullToggleProvider(hostBuilder)](#M-RecordPoint-Connectors-SDK-Toggles-Null-NullToggleBuilderExtensions-UseNullToggleProvider-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Toggles.Null.NullToggleBuilderExtensions.UseNullToggleProvider(Microsoft.Extensions.Hosting.IHostBuilder)')
- [NullToggleProvider](#T-RecordPoint-Connectors-SDK-Toggles-Null-NullToggleProvider 'RecordPoint.Connectors.SDK.Toggles.Null.NullToggleProvider')
  - [GetToggleBool(toggle,default)](#M-RecordPoint-Connectors-SDK-Toggles-Null-NullToggleProvider-GetToggleBool-System-String,System-Boolean- 'RecordPoint.Connectors.SDK.Toggles.Null.NullToggleProvider.GetToggleBool(System.String,System.Boolean)')
  - [GetToggleBool(toggle,userKey,default)](#M-RecordPoint-Connectors-SDK-Toggles-Null-NullToggleProvider-GetToggleBool-System-String,System-String,System-Boolean- 'RecordPoint.Connectors.SDK.Toggles.Null.NullToggleProvider.GetToggleBool(System.String,System.String,System.Boolean)')
  - [GetToggleNumber()](#M-RecordPoint-Connectors-SDK-Toggles-Null-NullToggleProvider-GetToggleNumber-System-String,System-String,System-Int32- 'RecordPoint.Connectors.SDK.Toggles.Null.NullToggleProvider.GetToggleNumber(System.String,System.String,System.Int32)')
  - [GetToggleNumber()](#M-RecordPoint-Connectors-SDK-Toggles-Null-NullToggleProvider-GetToggleNumber-System-String,System-Int32- 'RecordPoint.Connectors.SDK.Toggles.Null.NullToggleProvider.GetToggleNumber(System.String,System.Int32)')
  - [GetToggleString()](#M-RecordPoint-Connectors-SDK-Toggles-Null-NullToggleProvider-GetToggleString-System-String,System-String,System-String- 'RecordPoint.Connectors.SDK.Toggles.Null.NullToggleProvider.GetToggleString(System.String,System.String,System.String)')
  - [GetToggleString()](#M-RecordPoint-Connectors-SDK-Toggles-Null-NullToggleProvider-GetToggleString-System-String,System-String- 'RecordPoint.Connectors.SDK.Toggles.Null.NullToggleProvider.GetToggleString(System.String,System.String)')
- [ObservabilityHostBuilderExtensions](#T-RecordPoint-Connectors-SDK-Observability-ObservabilityHostBuilderExtensions 'RecordPoint.Connectors.SDK.Observability.ObservabilityHostBuilderExtensions')
  - [UseObservabilityTracking(hostBuilder)](#M-RecordPoint-Connectors-SDK-Observability-ObservabilityHostBuilderExtensions-UseObservabilityTracking-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Observability.ObservabilityHostBuilderExtensions.UseObservabilityTracking(Microsoft.Extensions.Hosting.IHostBuilder)')
- [ObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-ObservabilityScope 'RecordPoint.Connectors.SDK.Observability.ObservabilityScope')
  - [Dimensions](#P-RecordPoint-Connectors-SDK-Observability-ObservabilityScope-Dimensions 'RecordPoint.Connectors.SDK.Observability.ObservabilityScope.Dimensions')
  - [Measures](#P-RecordPoint-Connectors-SDK-Observability-ObservabilityScope-Measures 'RecordPoint.Connectors.SDK.Observability.ObservabilityScope.Measures')
  - [BeginScope()](#M-RecordPoint-Connectors-SDK-Observability-ObservabilityScope-BeginScope-RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures- 'RecordPoint.Connectors.SDK.Observability.ObservabilityScope.BeginScope(RecordPoint.Connectors.SDK.Observability.Dimensions,RecordPoint.Connectors.SDK.Observability.Measures)')
- [ObservabilityScopeExtensions](#T-RecordPoint-Connectors-SDK-Observability-ObservabilityScopeExtensions 'RecordPoint.Connectors.SDK.Observability.ObservabilityScopeExtensions')
  - [Invoke(observabilityScope,dimensions,action)](#M-RecordPoint-Connectors-SDK-Observability-ObservabilityScopeExtensions-Invoke-RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-Dimensions,System-Action- 'RecordPoint.Connectors.SDK.Observability.ObservabilityScopeExtensions.Invoke(RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Observability.Dimensions,System.Action)')
  - [InvokeAsync(observabilityScope,dimensions,action)](#M-RecordPoint-Connectors-SDK-Observability-ObservabilityScopeExtensions-InvokeAsync-RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-Dimensions,System-Func{System-Threading-Tasks-Task}- 'RecordPoint.Connectors.SDK.Observability.ObservabilityScopeExtensions.InvokeAsync(RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Observability.Dimensions,System.Func{System.Threading.Tasks.Task})')
  - [InvokeAsync\`\`1(observabilityScope,dimensions,func)](#M-RecordPoint-Connectors-SDK-Observability-ObservabilityScopeExtensions-InvokeAsync``1-RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-Dimensions,System-Func{System-Threading-Tasks-Task{``0}}- 'RecordPoint.Connectors.SDK.Observability.ObservabilityScopeExtensions.InvokeAsync``1(RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Observability.Dimensions,System.Func{System.Threading.Tasks.Task{``0}})')
  - [Invoke\`\`1(observabilityScope,dimensions,func)](#M-RecordPoint-Connectors-SDK-Observability-ObservabilityScopeExtensions-Invoke``1-RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-Dimensions,System-Func{``0}- 'RecordPoint.Connectors.SDK.Observability.ObservabilityScopeExtensions.Invoke``1(RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Observability.Dimensions,System.Func{``0})')
- [QueueableWorkBase\`1](#T-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1 'RecordPoint.Connectors.SDK.Work.QueueableWorkBase`1')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider- 'RecordPoint.Connectors.SDK.Work.QueueableWorkBase`1.#ctor(System.IServiceProvider,RecordPoint.Connectors.SDK.Context.ISystemContext,RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,RecordPoint.Connectors.SDK.Providers.IDateTimeProvider)')
  - [_semaphoreLockManager](#F-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-_semaphoreLockManager 'RecordPoint.Connectors.SDK.Work.QueueableWorkBase`1._semaphoreLockManager')
  - [_serviceProvider](#F-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-_serviceProvider 'RecordPoint.Connectors.SDK.Work.QueueableWorkBase`1._serviceProvider')
  - [HasDisposed](#P-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-HasDisposed 'RecordPoint.Connectors.SDK.Work.QueueableWorkBase`1.HasDisposed')
  - [MustFinishDateTime](#P-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-MustFinishDateTime 'RecordPoint.Connectors.SDK.Work.QueueableWorkBase`1.MustFinishDateTime')
  - [ResultDuration](#P-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-ResultDuration 'RecordPoint.Connectors.SDK.Work.QueueableWorkBase`1.ResultDuration')
  - [ServiceName](#P-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-ServiceName 'RecordPoint.Connectors.SDK.Work.QueueableWorkBase`1.ServiceName')
  - [SubmitDateTime](#P-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-SubmitDateTime 'RecordPoint.Connectors.SDK.Work.QueueableWorkBase`1.SubmitDateTime')
  - [SystemContext](#P-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-SystemContext 'RecordPoint.Connectors.SDK.Work.QueueableWorkBase`1.SystemContext')
  - [WaitTill](#P-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-WaitTill 'RecordPoint.Connectors.SDK.Work.QueueableWorkBase`1.WaitTill')
  - [WorkRequest](#P-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-WorkRequest 'RecordPoint.Connectors.SDK.Work.QueueableWorkBase`1.WorkRequest')
  - [Abandoned()](#M-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-Abandoned-System-String- 'RecordPoint.Connectors.SDK.Work.QueueableWorkBase`1.Abandoned(System.String)')
  - [Abandoned(exception)](#M-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-Abandoned-System-Exception- 'RecordPoint.Connectors.SDK.Work.QueueableWorkBase`1.Abandoned(System.Exception)')
  - [CheckSemaphoreLockAsync(connectorConfigModel,context,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-CheckSemaphoreLockAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,System-Object,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.QueueableWorkBase`1.CheckSemaphoreLockAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,System.Object,System.Threading.CancellationToken)')
  - [Deferred()](#M-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-Deferred-System-String,System-Nullable{System-DateTimeOffset}- 'RecordPoint.Connectors.SDK.Work.QueueableWorkBase`1.Deferred(System.String,System.Nullable{System.DateTimeOffset})')
  - [DeserializeParameter()](#M-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-DeserializeParameter 'RecordPoint.Connectors.SDK.Work.QueueableWorkBase`1.DeserializeParameter')
  - [Dispose()](#M-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-Dispose 'RecordPoint.Connectors.SDK.Work.QueueableWorkBase`1.Dispose')
  - [GetCoreKeyDimensions()](#M-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-GetCoreKeyDimensions 'RecordPoint.Connectors.SDK.Work.QueueableWorkBase`1.GetCoreKeyDimensions')
  - [GetCoreResultMeasures()](#M-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-GetCoreResultMeasures 'RecordPoint.Connectors.SDK.Work.QueueableWorkBase`1.GetCoreResultMeasures')
  - [GetWorkResult()](#M-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-GetWorkResult 'RecordPoint.Connectors.SDK.Work.QueueableWorkBase`1.GetWorkResult')
  - [HandleBackOffResultAsync(connectorConfigModel,context,semaphoreLockType,nextDelay,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-HandleBackOffResultAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,System-Object,System-Nullable{RecordPoint-Connectors-SDK-Caching-Semaphore-SemaphoreLockType},System-Nullable{System-Int32},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.QueueableWorkBase`1.HandleBackOffResultAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,System.Object,System.Nullable{RecordPoint.Connectors.SDK.Caching.Semaphore.SemaphoreLockType},System.Nullable{System.Int32},System.Threading.CancellationToken)')
  - [InnerDispose()](#M-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-InnerDispose 'RecordPoint.Connectors.SDK.Work.QueueableWorkBase`1.InnerDispose')
  - [RunWorkRequestAsync(workRequest,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-RunWorkRequestAsync-RecordPoint-Connectors-SDK-Work-WorkRequest,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.QueueableWorkBase`1.RunWorkRequestAsync(RecordPoint.Connectors.SDK.Work.WorkRequest,System.Threading.CancellationToken)')
- [QueueableWorkManager](#T-RecordPoint-Connectors-SDK-Work-QueueableWorkManager 'RecordPoint.Connectors.SDK.Work.QueueableWorkManager')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Work-QueueableWorkManager-#ctor-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,System-IServiceProvider- 'RecordPoint.Connectors.SDK.Work.QueueableWorkManager.#ctor(RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,System.IServiceProvider)')
  - [HandleWorkRequestAsync(workRequest,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-QueueableWorkManager-HandleWorkRequestAsync-RecordPoint-Connectors-SDK-Work-WorkRequest,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.QueueableWorkManager.HandleWorkRequestAsync(RecordPoint.Connectors.SDK.Work.WorkRequest,System.Threading.CancellationToken)')
- [R365BuilderExtensions](#T-RecordPoint-Connectors-SDK-R365-R365BuilderExtensions 'RecordPoint.Connectors.SDK.R365.R365BuilderExtensions')
  - [AddR365Integration(services,submitRecordAndBinariesSynchronously)](#M-RecordPoint-Connectors-SDK-R365-R365BuilderExtensions-AddR365Integration-Microsoft-Extensions-DependencyInjection-IServiceCollection,System-Boolean- 'RecordPoint.Connectors.SDK.R365.R365BuilderExtensions.AddR365Integration(Microsoft.Extensions.DependencyInjection.IServiceCollection,System.Boolean)')
  - [CreateAuditEventPipeline(provider)](#M-RecordPoint-Connectors-SDK-R365-R365BuilderExtensions-CreateAuditEventPipeline-System-IServiceProvider- 'RecordPoint.Connectors.SDK.R365.R365BuilderExtensions.CreateAuditEventPipeline(System.IServiceProvider)')
  - [CreateRecordPipeline(provider)](#M-RecordPoint-Connectors-SDK-R365-R365BuilderExtensions-CreateRecordPipeline-System-IServiceProvider- 'RecordPoint.Connectors.SDK.R365.R365BuilderExtensions.CreateRecordPipeline(System.IServiceProvider)')
  - [UseR365Integration(hostBuilder)](#M-RecordPoint-Connectors-SDK-R365-R365BuilderExtensions-UseR365Integration-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.R365.R365BuilderExtensions.UseR365Integration(Microsoft.Extensions.Hosting.IHostBuilder)')
- [R365Client](#T-RecordPoint-Connectors-SDK-R365-R365Client 'RecordPoint.Connectors.SDK.R365.R365Client')
  - [#ctor(r365ConfigurationClient,observabilityScope,r365Pipelines)](#M-RecordPoint-Connectors-SDK-R365-R365Client-#ctor-RecordPoint-Connectors-SDK-Configuration-IR365ConfigurationClient,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-R365-IR365Pipelines- 'RecordPoint.Connectors.SDK.R365.R365Client.#ctor(RecordPoint.Connectors.SDK.Configuration.IR365ConfigurationClient,RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.R365.IR365Pipelines)')
  - [_observabilityScope](#F-RecordPoint-Connectors-SDK-R365-R365Client-_observabilityScope 'RecordPoint.Connectors.SDK.R365.R365Client._observabilityScope')
  - [_r365ConfigurationClient](#F-RecordPoint-Connectors-SDK-R365-R365Client-_r365ConfigurationClient 'RecordPoint.Connectors.SDK.R365.R365Client._r365ConfigurationClient')
  - [_r365Pipelines](#F-RecordPoint-Connectors-SDK-R365-R365Client-_r365Pipelines 'RecordPoint.Connectors.SDK.R365.R365Client._r365Pipelines')
  - [GetApiClientFactorySettings(r365Configuration)](#M-RecordPoint-Connectors-SDK-R365-R365Client-GetApiClientFactorySettings-RecordPoint-Connectors-SDK-Configuration-R365ConfigurationModel- 'RecordPoint.Connectors.SDK.R365.R365Client.GetApiClientFactorySettings(RecordPoint.Connectors.SDK.Configuration.R365ConfigurationModel)')
  - [GetAuthenticationHelperSettings(r365Configuration,tenantDomainName)](#M-RecordPoint-Connectors-SDK-R365-R365Client-GetAuthenticationHelperSettings-RecordPoint-Connectors-SDK-Configuration-R365ConfigurationModel,System-String- 'RecordPoint.Connectors.SDK.R365.R365Client.GetAuthenticationHelperSettings(RecordPoint.Connectors.SDK.Configuration.R365ConfigurationModel,System.String)')
  - [GetDimensions()](#M-RecordPoint-Connectors-SDK-R365-R365Client-GetDimensions 'RecordPoint.Connectors.SDK.R365.R365Client.GetDimensions')
  - [IsConfigured()](#M-RecordPoint-Connectors-SDK-R365-R365Client-IsConfigured 'RecordPoint.Connectors.SDK.R365.R365Client.IsConfigured')
  - [LoadConfiguration()](#M-RecordPoint-Connectors-SDK-R365-R365Client-LoadConfiguration-System-String- 'RecordPoint.Connectors.SDK.R365.R365Client.LoadConfiguration(System.String)')
  - [SubmitAggregation(connectorConfig,aggregation,cancellationToken)](#M-RecordPoint-Connectors-SDK-R365-R365Client-SubmitAggregation-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Aggregation,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.R365.R365Client.SubmitAggregation(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,RecordPoint.Connectors.SDK.Content.Aggregation,System.Threading.CancellationToken)')
  - [SubmitAuditEvent(connectorConfig,auditEvent,cancellationToken)](#M-RecordPoint-Connectors-SDK-R365-R365Client-SubmitAuditEvent-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-AuditEvent,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.R365.R365Client.SubmitAuditEvent(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,RecordPoint.Connectors.SDK.Content.AuditEvent,System.Threading.CancellationToken)')
  - [SubmitBinary(connectorConfig,binaryMetaInfo,binaryStream,cancellationToken)](#M-RecordPoint-Connectors-SDK-R365-R365Client-SubmitBinary-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-BinaryMetaInfo,System-IO-Stream,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.R365.R365Client.SubmitBinary(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,RecordPoint.Connectors.SDK.Content.BinaryMetaInfo,System.IO.Stream,System.Threading.CancellationToken)')
  - [SubmitRecord(connectorConfig,record,cancellationToken)](#M-RecordPoint-Connectors-SDK-R365-R365Client-SubmitRecord-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Record,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.R365.R365Client.SubmitRecord(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,RecordPoint.Connectors.SDK.Content.Record,System.Threading.CancellationToken)')
- [R365Pipelines](#T-RecordPoint-Connectors-SDK-R365-R365Pipelines 'RecordPoint.Connectors.SDK.R365.R365Pipelines')
  - [#ctor(recordPipeline,binaryPipeline,aggregationPipeline,auditEventPipeline)](#M-RecordPoint-Connectors-SDK-R365-R365Pipelines-#ctor-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission,RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission,RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission,RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission- 'RecordPoint.Connectors.SDK.R365.R365Pipelines.#ctor(RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission,RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission,RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission,RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission)')
  - [AggregationPipeline](#P-RecordPoint-Connectors-SDK-R365-R365Pipelines-AggregationPipeline 'RecordPoint.Connectors.SDK.R365.R365Pipelines.AggregationPipeline')
  - [AuditEventPipeline](#P-RecordPoint-Connectors-SDK-R365-R365Pipelines-AuditEventPipeline 'RecordPoint.Connectors.SDK.R365.R365Pipelines.AuditEventPipeline')
  - [BinaryPipeline](#P-RecordPoint-Connectors-SDK-R365-R365Pipelines-BinaryPipeline 'RecordPoint.Connectors.SDK.R365.R365Pipelines.BinaryPipeline')
  - [RecordPipeline](#P-RecordPoint-Connectors-SDK-R365-R365Pipelines-RecordPipeline 'RecordPoint.Connectors.SDK.R365.R365Pipelines.RecordPipeline')
- [RecordDisposalOperation](#T-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation 'RecordPoint.Connectors.SDK.ContentManager.RecordDisposalOperation')
  - [#ctor(serviceProvider,contentManagerActionProvider,connectorManager,systemContext,observabilityScope,telemetryTracker,dateTimeProvider)](#M-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider,RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider- 'RecordPoint.Connectors.SDK.ContentManager.RecordDisposalOperation.#ctor(System.IServiceProvider,RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider,RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager,RecordPoint.Connectors.SDK.Context.ISystemContext,RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,RecordPoint.Connectors.SDK.Providers.IDateTimeProvider)')
  - [BINARY_SUBMISSION_DELAY_SECONDS](#F-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-BINARY_SUBMISSION_DELAY_SECONDS 'RecordPoint.Connectors.SDK.ContentManager.RecordDisposalOperation.BINARY_SUBMISSION_DELAY_SECONDS')
  - [DEFAULT_DEFERRAL_SECONDS](#F-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-DEFAULT_DEFERRAL_SECONDS 'RecordPoint.Connectors.SDK.ContentManager.RecordDisposalOperation.DEFAULT_DEFERRAL_SECONDS')
  - [WORK_TYPE](#F-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-WORK_TYPE 'RecordPoint.Connectors.SDK.ContentManager.RecordDisposalOperation.WORK_TYPE')
  - [_connectorConfiguration](#F-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-_connectorConfiguration 'RecordPoint.Connectors.SDK.ContentManager.RecordDisposalOperation._connectorConfiguration')
  - [_connectorManager](#F-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-_connectorManager 'RecordPoint.Connectors.SDK.ContentManager.RecordDisposalOperation._connectorManager')
  - [_contentManagerActionProvider](#F-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-_contentManagerActionProvider 'RecordPoint.Connectors.SDK.ContentManager.RecordDisposalOperation._contentManagerActionProvider')
  - [_recordDisposalResult](#F-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-_recordDisposalResult 'RecordPoint.Connectors.SDK.ContentManager.RecordDisposalOperation._recordDisposalResult')
  - [ConnectorConfigId](#P-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-ConnectorConfigId 'RecordPoint.Connectors.SDK.ContentManager.RecordDisposalOperation.ConnectorConfigId')
  - [Record](#P-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-Record 'RecordPoint.Connectors.SDK.ContentManager.RecordDisposalOperation.Record')
  - [ServiceName](#P-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-ServiceName 'RecordPoint.Connectors.SDK.ContentManager.RecordDisposalOperation.ServiceName')
  - [WorkType](#P-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-WorkType 'RecordPoint.Connectors.SDK.ContentManager.RecordDisposalOperation.WorkType')
  - [GetCustomKeyDimensions()](#M-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-GetCustomKeyDimensions 'RecordPoint.Connectors.SDK.ContentManager.RecordDisposalOperation.GetCustomKeyDimensions')
  - [GetCustomResultDimensions()](#M-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-GetCustomResultDimensions 'RecordPoint.Connectors.SDK.ContentManager.RecordDisposalOperation.GetCustomResultDimensions')
  - [GetCustomResultMeasures()](#M-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-GetCustomResultMeasures 'RecordPoint.Connectors.SDK.ContentManager.RecordDisposalOperation.GetCustomResultMeasures')
  - [InnerDispose()](#M-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-InnerDispose 'RecordPoint.Connectors.SDK.ContentManager.RecordDisposalOperation.InnerDispose')
  - [InnerRunAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-InnerRunAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.RecordDisposalOperation.InnerRunAsync(System.Threading.CancellationToken)')
- [SDKLogAdapter](#T-RecordPoint-Connectors-SDK-R365-SDKLogAdapter 'RecordPoint.Connectors.SDK.R365.SDKLogAdapter')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-R365-SDKLogAdapter-#ctor-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker- 'RecordPoint.Connectors.SDK.R365.SDKLogAdapter.#ctor(RecordPoint.Connectors.SDK.Observability.ITelemetryTracker)')
  - [LogMessage(callerType,methodName,message,elapsedTimeTicks)](#M-RecordPoint-Connectors-SDK-R365-SDKLogAdapter-LogMessage-System-Type,System-String,System-String,System-Nullable{System-Int64}- 'RecordPoint.Connectors.SDK.R365.SDKLogAdapter.LogMessage(System.Type,System.String,System.String,System.Nullable{System.Int64})')
  - [LogVerbose(callerType,methodName,message,elapsedTimeTicks)](#M-RecordPoint-Connectors-SDK-R365-SDKLogAdapter-LogVerbose-System-Type,System-String,System-String,System-Nullable{System-Int64}- 'RecordPoint.Connectors.SDK.R365.SDKLogAdapter.LogVerbose(System.Type,System.String,System.String,System.Nullable{System.Int64})')
  - [LogWarning(callerType,methodName,message,elapsedTimeTicks)](#M-RecordPoint-Connectors-SDK-R365-SDKLogAdapter-LogWarning-System-Type,System-String,System-String,System-Nullable{System-Int64}- 'RecordPoint.Connectors.SDK.R365.SDKLogAdapter.LogWarning(System.Type,System.String,System.String,System.Nullable{System.Int64})')
- [SettableCircuitProvider](#T-RecordPoint-Connectors-SDK-Providers-SettableCircuitProvider 'RecordPoint.Connectors.SDK.Providers.SettableCircuitProvider')
  - [DateTimeProvider](#P-RecordPoint-Connectors-SDK-Providers-SettableCircuitProvider-DateTimeProvider 'RecordPoint.Connectors.SDK.Providers.SettableCircuitProvider.DateTimeProvider')
  - [IsCircuitClosed(waitFor)](#M-RecordPoint-Connectors-SDK-Providers-SettableCircuitProvider-IsCircuitClosed-System-TimeSpan@- 'RecordPoint.Connectors.SDK.Providers.SettableCircuitProvider.IsCircuitClosed(System.TimeSpan@)')
  - [SetOpenUntil()](#M-RecordPoint-Connectors-SDK-Providers-SettableCircuitProvider-SetOpenUntil-System-DateTime- 'RecordPoint.Connectors.SDK.Providers.SettableCircuitProvider.SetOpenUntil(System.DateTime)')
- [StatusBuilderExtensions](#T-RecordPoint-Connectors-SDK-Status-StatusBuilderExtensions 'RecordPoint.Connectors.SDK.Status.StatusBuilderExtensions')
  - [UseStatusManager(hostBuilder)](#M-RecordPoint-Connectors-SDK-Status-StatusBuilderExtensions-UseStatusManager-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Status.StatusBuilderExtensions.UseStatusManager(Microsoft.Extensions.Hosting.IHostBuilder)')
- [StatusManager](#T-RecordPoint-Connectors-SDK-Status-StatusManager 'RecordPoint.Connectors.SDK.Status.StatusManager')
  - [#ctor(strategies,connectorManager)](#M-RecordPoint-Connectors-SDK-Status-StatusManager-#ctor-System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Status-IStatusStrategy},RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager- 'RecordPoint.Connectors.SDK.Status.StatusManager.#ctor(System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Status.IStatusStrategy},RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager)')
  - [_connectorManager](#F-RecordPoint-Connectors-SDK-Status-StatusManager-_connectorManager 'RecordPoint.Connectors.SDK.Status.StatusManager._connectorManager')
  - [_strategies](#F-RecordPoint-Connectors-SDK-Status-StatusManager-_strategies 'RecordPoint.Connectors.SDK.Status.StatusManager._strategies')
  - [GetStatusModelAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Status-StatusManager-GetStatusModelAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Status.StatusManager.GetStatusModelAsync(System.Threading.CancellationToken)')
- [StopSystemException](#T-RecordPoint-Connectors-SDK-Context-StopSystemException 'RecordPoint.Connectors.SDK.Context.StopSystemException')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Context-StopSystemException-#ctor 'RecordPoint.Connectors.SDK.Context.StopSystemException.#ctor')
  - [#ctor(message)](#M-RecordPoint-Connectors-SDK-Context-StopSystemException-#ctor-System-String- 'RecordPoint.Connectors.SDK.Context.StopSystemException.#ctor(System.String)')
  - [#ctor(message,inner)](#M-RecordPoint-Connectors-SDK-Context-StopSystemException-#ctor-System-String,System-Exception- 'RecordPoint.Connectors.SDK.Context.StopSystemException.#ctor(System.String,System.Exception)')
- [SubmitAggregationOperation](#T-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation 'RecordPoint.Connectors.SDK.ContentManager.SubmitAggregationOperation')
  - [#ctor(serviceProvider,contentManagerActionProvider,r365Client,workQueueClient,connectorManager,systemContext,observabilityScope,telemetryTracker,dateTimeProvider)](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider,RecordPoint-Connectors-SDK-R365-IR365Client,RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider- 'RecordPoint.Connectors.SDK.ContentManager.SubmitAggregationOperation.#ctor(System.IServiceProvider,RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider,RecordPoint.Connectors.SDK.R365.IR365Client,RecordPoint.Connectors.SDK.Work.IWorkQueueClient,RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager,RecordPoint.Connectors.SDK.Context.ISystemContext,RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,RecordPoint.Connectors.SDK.Providers.IDateTimeProvider)')
  - [CONTENT_LABEL](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-CONTENT_LABEL 'RecordPoint.Connectors.SDK.ContentManager.SubmitAggregationOperation.CONTENT_LABEL')
  - [WORK_TYPE](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-WORK_TYPE 'RecordPoint.Connectors.SDK.ContentManager.SubmitAggregationOperation.WORK_TYPE')
  - [_connectorManager](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-_connectorManager 'RecordPoint.Connectors.SDK.ContentManager.SubmitAggregationOperation._connectorManager')
  - [_contentManagerActionProvider](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-_contentManagerActionProvider 'RecordPoint.Connectors.SDK.ContentManager.SubmitAggregationOperation._contentManagerActionProvider')
  - [Aggregation](#P-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-Aggregation 'RecordPoint.Connectors.SDK.ContentManager.SubmitAggregationOperation.Aggregation')
  - [ConnectorConfig](#P-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-ConnectorConfig 'RecordPoint.Connectors.SDK.ContentManager.SubmitAggregationOperation.ConnectorConfig')
  - [ContentLabel](#P-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-ContentLabel 'RecordPoint.Connectors.SDK.ContentManager.SubmitAggregationOperation.ContentLabel')
  - [ServiceName](#P-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-ServiceName 'RecordPoint.Connectors.SDK.ContentManager.SubmitAggregationOperation.ServiceName')
  - [WorkType](#P-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-WorkType 'RecordPoint.Connectors.SDK.ContentManager.SubmitAggregationOperation.WorkType')
  - [GetFeatureStatus(cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-GetFeatureStatus-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.SubmitAggregationOperation.GetFeatureStatus(System.Threading.CancellationToken)')
  - [InnerDispose()](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-InnerDispose 'RecordPoint.Connectors.SDK.ContentManager.SubmitAggregationOperation.InnerDispose')
  - [InnerRunAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-InnerRunAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.SubmitAggregationOperation.InnerRunAsync(System.Threading.CancellationToken)')
  - [PreSubmitAsync()](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-PreSubmitAsync-Microsoft-Extensions-DependencyInjection-IServiceScope,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.SubmitAggregationOperation.PreSubmitAsync(Microsoft.Extensions.DependencyInjection.IServiceScope,System.Threading.CancellationToken)')
  - [RequeueAsync(waitTill,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-RequeueAsync-System-DateTimeOffset,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.SubmitAggregationOperation.RequeueAsync(System.DateTimeOffset,System.Threading.CancellationToken)')
  - [SubmitAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-SubmitAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.SubmitAggregationOperation.SubmitAsync(System.Threading.CancellationToken)')
  - [SubmitSuccessfulAsync()](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-SubmitSuccessfulAsync-Microsoft-Extensions-DependencyInjection-IServiceScope,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.SubmitAggregationOperation.SubmitSuccessfulAsync(Microsoft.Extensions.DependencyInjection.IServiceScope,System.Threading.CancellationToken)')
- [SubmitAuditEventOperation](#T-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation 'RecordPoint.Connectors.SDK.ContentManager.SubmitAuditEventOperation')
  - [#ctor(serviceProvider,contentManagerActionProvider,r365Client,connectorManager,systemContext,observabilityScope,telemetryTracker,workQueueClient,dateTimeProvider)](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider,RecordPoint-Connectors-SDK-R365-IR365Client,RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider- 'RecordPoint.Connectors.SDK.ContentManager.SubmitAuditEventOperation.#ctor(System.IServiceProvider,RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider,RecordPoint.Connectors.SDK.R365.IR365Client,RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager,RecordPoint.Connectors.SDK.Context.ISystemContext,RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,RecordPoint.Connectors.SDK.Work.IWorkQueueClient,RecordPoint.Connectors.SDK.Providers.IDateTimeProvider)')
  - [CONTENT_LABEL](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-CONTENT_LABEL 'RecordPoint.Connectors.SDK.ContentManager.SubmitAuditEventOperation.CONTENT_LABEL')
  - [DEFAULT_DEFERRAL_SECONDS](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-DEFAULT_DEFERRAL_SECONDS 'RecordPoint.Connectors.SDK.ContentManager.SubmitAuditEventOperation.DEFAULT_DEFERRAL_SECONDS')
  - [WORK_TYPE](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-WORK_TYPE 'RecordPoint.Connectors.SDK.ContentManager.SubmitAuditEventOperation.WORK_TYPE')
  - [_connectorConfiguration](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-_connectorConfiguration 'RecordPoint.Connectors.SDK.ContentManager.SubmitAuditEventOperation._connectorConfiguration')
  - [_connectorManager](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-_connectorManager 'RecordPoint.Connectors.SDK.ContentManager.SubmitAuditEventOperation._connectorManager')
  - [_contentManagerActionProvider](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-_contentManagerActionProvider 'RecordPoint.Connectors.SDK.ContentManager.SubmitAuditEventOperation._contentManagerActionProvider')
  - [_r365Client](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-_r365Client 'RecordPoint.Connectors.SDK.ContentManager.SubmitAuditEventOperation._r365Client')
  - [_workQueueClient](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-_workQueueClient 'RecordPoint.Connectors.SDK.ContentManager.SubmitAuditEventOperation._workQueueClient')
  - [AuditEvent](#P-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-AuditEvent 'RecordPoint.Connectors.SDK.ContentManager.SubmitAuditEventOperation.AuditEvent')
  - [ConnectorConfigId](#P-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-ConnectorConfigId 'RecordPoint.Connectors.SDK.ContentManager.SubmitAuditEventOperation.ConnectorConfigId')
  - [ServiceName](#P-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-ServiceName 'RecordPoint.Connectors.SDK.ContentManager.SubmitAuditEventOperation.ServiceName')
  - [WorkType](#P-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-WorkType 'RecordPoint.Connectors.SDK.ContentManager.SubmitAuditEventOperation.WorkType')
  - [GetBackoffDateTime(suggestedDateTime)](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-GetBackoffDateTime-System-Nullable{System-DateTimeOffset}- 'RecordPoint.Connectors.SDK.ContentManager.SubmitAuditEventOperation.GetBackoffDateTime(System.Nullable{System.DateTimeOffset})')
  - [GetCustomKeyDimensions()](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-GetCustomKeyDimensions 'RecordPoint.Connectors.SDK.ContentManager.SubmitAuditEventOperation.GetCustomKeyDimensions')
  - [GetDeferralDateTime(suggestedDateTime)](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-GetDeferralDateTime-System-Nullable{System-DateTimeOffset}- 'RecordPoint.Connectors.SDK.ContentManager.SubmitAuditEventOperation.GetDeferralDateTime(System.Nullable{System.DateTimeOffset})')
  - [GetFeatureStatus(cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-GetFeatureStatus-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.SubmitAuditEventOperation.GetFeatureStatus(System.Threading.CancellationToken)')
  - [InnerDispose()](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-InnerDispose 'RecordPoint.Connectors.SDK.ContentManager.SubmitAuditEventOperation.InnerDispose')
  - [InnerRunAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-InnerRunAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.SubmitAuditEventOperation.InnerRunAsync(System.Threading.CancellationToken)')
  - [SubmitAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-SubmitAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.SubmitAuditEventOperation.SubmitAsync(System.Threading.CancellationToken)')
- [SubmitBinaryOperation](#T-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation 'RecordPoint.Connectors.SDK.ContentManager.SubmitBinaryOperation')
  - [#ctor(serviceProvider,contentManagerActionProvider,r365Client,connectorManager,systemContext,observabilityScope,telemetryTracker,workQueueClient,dateTimeProvider)](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider,RecordPoint-Connectors-SDK-R365-IR365Client,RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider- 'RecordPoint.Connectors.SDK.ContentManager.SubmitBinaryOperation.#ctor(System.IServiceProvider,RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider,RecordPoint.Connectors.SDK.R365.IR365Client,RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager,RecordPoint.Connectors.SDK.Context.ISystemContext,RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,RecordPoint.Connectors.SDK.Work.IWorkQueueClient,RecordPoint.Connectors.SDK.Providers.IDateTimeProvider)')
  - [BINARY_SUBMISSION_DELAY_SECONDS](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-BINARY_SUBMISSION_DELAY_SECONDS 'RecordPoint.Connectors.SDK.ContentManager.SubmitBinaryOperation.BINARY_SUBMISSION_DELAY_SECONDS')
  - [DEFAULT_DEFERRAL_SECONDS](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-DEFAULT_DEFERRAL_SECONDS 'RecordPoint.Connectors.SDK.ContentManager.SubmitBinaryOperation.DEFAULT_DEFERRAL_SECONDS')
  - [WORK_TYPE](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-WORK_TYPE 'RecordPoint.Connectors.SDK.ContentManager.SubmitBinaryOperation.WORK_TYPE')
  - [_actionExecutionTimespan](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-_actionExecutionTimespan 'RecordPoint.Connectors.SDK.ContentManager.SubmitBinaryOperation._actionExecutionTimespan')
  - [_binaryRetrievalResult](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-_binaryRetrievalResult 'RecordPoint.Connectors.SDK.ContentManager.SubmitBinaryOperation._binaryRetrievalResult')
  - [_connectorConfiguration](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-_connectorConfiguration 'RecordPoint.Connectors.SDK.ContentManager.SubmitBinaryOperation._connectorConfiguration')
  - [_connectorManager](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-_connectorManager 'RecordPoint.Connectors.SDK.ContentManager.SubmitBinaryOperation._connectorManager')
  - [_contentManagerActionProvider](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-_contentManagerActionProvider 'RecordPoint.Connectors.SDK.ContentManager.SubmitBinaryOperation._contentManagerActionProvider')
  - [_r365Client](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-_r365Client 'RecordPoint.Connectors.SDK.ContentManager.SubmitBinaryOperation._r365Client')
  - [_submitTimespan](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-_submitTimespan 'RecordPoint.Connectors.SDK.ContentManager.SubmitBinaryOperation._submitTimespan')
  - [_workQueueClient](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-_workQueueClient 'RecordPoint.Connectors.SDK.ContentManager.SubmitBinaryOperation._workQueueClient')
  - [BinaryMetaInfo](#P-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-BinaryMetaInfo 'RecordPoint.Connectors.SDK.ContentManager.SubmitBinaryOperation.BinaryMetaInfo')
  - [ConnectorConfigId](#P-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-ConnectorConfigId 'RecordPoint.Connectors.SDK.ContentManager.SubmitBinaryOperation.ConnectorConfigId')
  - [ServiceName](#P-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-ServiceName 'RecordPoint.Connectors.SDK.ContentManager.SubmitBinaryOperation.ServiceName')
  - [WorkType](#P-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-WorkType 'RecordPoint.Connectors.SDK.ContentManager.SubmitBinaryOperation.WorkType')
  - [GetCustomKeyDimensions()](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-GetCustomKeyDimensions 'RecordPoint.Connectors.SDK.ContentManager.SubmitBinaryOperation.GetCustomKeyDimensions')
  - [GetCustomResultMeasures()](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-GetCustomResultMeasures 'RecordPoint.Connectors.SDK.ContentManager.SubmitBinaryOperation.GetCustomResultMeasures')
  - [InnerDispose()](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-InnerDispose 'RecordPoint.Connectors.SDK.ContentManager.SubmitBinaryOperation.InnerDispose')
  - [InnerRunAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-InnerRunAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.SubmitBinaryOperation.InnerRunAsync(System.Threading.CancellationToken)')
  - [InvokeSubmissionCallbackAsync(scope,submissionActionType,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-InvokeSubmissionCallbackAsync-Microsoft-Extensions-DependencyInjection-IServiceScope,RecordPoint-Connectors-SDK-Abstractions-ContentManager-SubmissionActionType,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.SubmitBinaryOperation.InvokeSubmissionCallbackAsync(Microsoft.Extensions.DependencyInjection.IServiceScope,RecordPoint.Connectors.SDK.Abstractions.ContentManager.SubmissionActionType,System.Threading.CancellationToken)')
  - [RecordOutcomeAsync(submitResult,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-RecordOutcomeAsync-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.SubmitBinaryOperation.RecordOutcomeAsync(RecordPoint.Connectors.SDK.SubmitPipeline.SubmitResult,System.Threading.CancellationToken)')
- [SubmitOperationBase\`1](#T-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1 'RecordPoint.Connectors.SDK.ContentManager.SubmitOperationBase`1')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-R365-IR365Client,RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider- 'RecordPoint.Connectors.SDK.ContentManager.SubmitOperationBase`1.#ctor(System.IServiceProvider,RecordPoint.Connectors.SDK.R365.IR365Client,RecordPoint.Connectors.SDK.Work.IWorkQueueClient,RecordPoint.Connectors.SDK.Context.ISystemContext,RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,RecordPoint.Connectors.SDK.Providers.IDateTimeProvider)')
  - [DEFAULT_BACKOFF_SECONDS](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-DEFAULT_BACKOFF_SECONDS 'RecordPoint.Connectors.SDK.ContentManager.SubmitOperationBase`1.DEFAULT_BACKOFF_SECONDS')
  - [DEFAULT_DEFERRAL_SECONDS](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-DEFAULT_DEFERRAL_SECONDS 'RecordPoint.Connectors.SDK.ContentManager.SubmitOperationBase`1.DEFAULT_DEFERRAL_SECONDS')
  - [ConnectorConfigId](#P-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-ConnectorConfigId 'RecordPoint.Connectors.SDK.ContentManager.SubmitOperationBase`1.ConnectorConfigId')
  - [ContentItem](#P-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-ContentItem 'RecordPoint.Connectors.SDK.ContentManager.SubmitOperationBase`1.ContentItem')
  - [ContentLabel](#P-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-ContentLabel 'RecordPoint.Connectors.SDK.ContentManager.SubmitOperationBase`1.ContentLabel')
  - [R365Client](#P-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-R365Client 'RecordPoint.Connectors.SDK.ContentManager.SubmitOperationBase`1.R365Client')
  - [WorkQueueClient](#P-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-WorkQueueClient 'RecordPoint.Connectors.SDK.ContentManager.SubmitOperationBase`1.WorkQueueClient')
  - [GetBackoffDateTime(suggestedDateTime)](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-GetBackoffDateTime-System-Nullable{System-DateTimeOffset}- 'RecordPoint.Connectors.SDK.ContentManager.SubmitOperationBase`1.GetBackoffDateTime(System.Nullable{System.DateTimeOffset})')
  - [GetCoreKeyDimensions()](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-GetCoreKeyDimensions 'RecordPoint.Connectors.SDK.ContentManager.SubmitOperationBase`1.GetCoreKeyDimensions')
  - [GetDeferralDateTime(suggestedDateTime)](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-GetDeferralDateTime-System-Nullable{System-DateTimeOffset}- 'RecordPoint.Connectors.SDK.ContentManager.SubmitOperationBase`1.GetDeferralDateTime(System.Nullable{System.DateTimeOffset})')
  - [GetFeatureStatus(cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-GetFeatureStatus-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.SubmitOperationBase`1.GetFeatureStatus(System.Threading.CancellationToken)')
  - [InnerDispose()](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-InnerDispose 'RecordPoint.Connectors.SDK.ContentManager.SubmitOperationBase`1.InnerDispose')
  - [InnerRunAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-InnerRunAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.SubmitOperationBase`1.InnerRunAsync(System.Threading.CancellationToken)')
  - [PreSubmitAsync(scope,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-PreSubmitAsync-Microsoft-Extensions-DependencyInjection-IServiceScope,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.SubmitOperationBase`1.PreSubmitAsync(Microsoft.Extensions.DependencyInjection.IServiceScope,System.Threading.CancellationToken)')
  - [RequeueAsync(waitTill,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-RequeueAsync-System-DateTimeOffset,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.SubmitOperationBase`1.RequeueAsync(System.DateTimeOffset,System.Threading.CancellationToken)')
  - [SubmitAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-SubmitAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.SubmitOperationBase`1.SubmitAsync(System.Threading.CancellationToken)')
  - [SubmitSuccessfulAsync(scope,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-SubmitSuccessfulAsync-Microsoft-Extensions-DependencyInjection-IServiceScope,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.SubmitOperationBase`1.SubmitSuccessfulAsync(Microsoft.Extensions.DependencyInjection.IServiceScope,System.Threading.CancellationToken)')
- [SubmitRecordOperation](#T-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation 'RecordPoint.Connectors.SDK.ContentManager.SubmitRecordOperation')
  - [#ctor(serviceProvider,contentManagerActionProvider,r365Client,workQueueClient,connectorManager,systemContext,observabilityScope,telemetryTracker,dateTimeProvider)](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider,RecordPoint-Connectors-SDK-R365-IR365Client,RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider- 'RecordPoint.Connectors.SDK.ContentManager.SubmitRecordOperation.#ctor(System.IServiceProvider,RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider,RecordPoint.Connectors.SDK.R365.IR365Client,RecordPoint.Connectors.SDK.Work.IWorkQueueClient,RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager,RecordPoint.Connectors.SDK.Context.ISystemContext,RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,RecordPoint.Connectors.SDK.Providers.IDateTimeProvider)')
  - [CONTENT_LABEL](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-CONTENT_LABEL 'RecordPoint.Connectors.SDK.ContentManager.SubmitRecordOperation.CONTENT_LABEL')
  - [WORK_TYPE](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-WORK_TYPE 'RecordPoint.Connectors.SDK.ContentManager.SubmitRecordOperation.WORK_TYPE')
  - [_connectorManager](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-_connectorManager 'RecordPoint.Connectors.SDK.ContentManager.SubmitRecordOperation._connectorManager')
  - [_contentManagerActionProvider](#F-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-_contentManagerActionProvider 'RecordPoint.Connectors.SDK.ContentManager.SubmitRecordOperation._contentManagerActionProvider')
  - [ConnectorConfig](#P-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-ConnectorConfig 'RecordPoint.Connectors.SDK.ContentManager.SubmitRecordOperation.ConnectorConfig')
  - [ContentLabel](#P-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-ContentLabel 'RecordPoint.Connectors.SDK.ContentManager.SubmitRecordOperation.ContentLabel')
  - [ServiceName](#P-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-ServiceName 'RecordPoint.Connectors.SDK.ContentManager.SubmitRecordOperation.ServiceName')
  - [WorkType](#P-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-WorkType 'RecordPoint.Connectors.SDK.ContentManager.SubmitRecordOperation.WorkType')
  - [GetFeatureStatus(cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-GetFeatureStatus-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.SubmitRecordOperation.GetFeatureStatus(System.Threading.CancellationToken)')
  - [InnerRunAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-InnerRunAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.SubmitRecordOperation.InnerRunAsync(System.Threading.CancellationToken)')
  - [PreSubmitAsync()](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-PreSubmitAsync-Microsoft-Extensions-DependencyInjection-IServiceScope,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.SubmitRecordOperation.PreSubmitAsync(Microsoft.Extensions.DependencyInjection.IServiceScope,System.Threading.CancellationToken)')
  - [RequeueAsync(waitTill,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-RequeueAsync-System-DateTimeOffset,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.SubmitRecordOperation.RequeueAsync(System.DateTimeOffset,System.Threading.CancellationToken)')
  - [SubmitAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-SubmitAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.SubmitRecordOperation.SubmitAsync(System.Threading.CancellationToken)')
  - [SubmitSuccessfulAsync()](#M-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-SubmitSuccessfulAsync-Microsoft-Extensions-DependencyInjection-IServiceScope,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.SubmitRecordOperation.SubmitSuccessfulAsync(Microsoft.Extensions.DependencyInjection.IServiceScope,System.Threading.CancellationToken)')
- [SynchronousSubmitRecordOperation](#T-RecordPoint-Connectors-SDK-ContentManager-SynchronousSubmitRecordOperation 'RecordPoint.Connectors.SDK.ContentManager.SynchronousSubmitRecordOperation')
  - [#ctor(serviceProvider,contentManagerActionProvider,r365Client,connectorManager,systemContext,observabilityScope,telemetryTracker,workQueueClient,dateTimeProvider)](#M-RecordPoint-Connectors-SDK-ContentManager-SynchronousSubmitRecordOperation-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider,RecordPoint-Connectors-SDK-R365-IR365Client,RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider- 'RecordPoint.Connectors.SDK.ContentManager.SynchronousSubmitRecordOperation.#ctor(System.IServiceProvider,RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider,RecordPoint.Connectors.SDK.R365.IR365Client,RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager,RecordPoint.Connectors.SDK.Context.ISystemContext,RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,RecordPoint.Connectors.SDK.Work.IWorkQueueClient,RecordPoint.Connectors.SDK.Providers.IDateTimeProvider)')
  - [WORK_TYPE](#F-RecordPoint-Connectors-SDK-ContentManager-SynchronousSubmitRecordOperation-WORK_TYPE 'RecordPoint.Connectors.SDK.ContentManager.SynchronousSubmitRecordOperation.WORK_TYPE')
  - [_connectorConfiguration](#F-RecordPoint-Connectors-SDK-ContentManager-SynchronousSubmitRecordOperation-_connectorConfiguration 'RecordPoint.Connectors.SDK.ContentManager.SynchronousSubmitRecordOperation._connectorConfiguration')
  - [ConnectorConfigId](#P-RecordPoint-Connectors-SDK-ContentManager-SynchronousSubmitRecordOperation-ConnectorConfigId 'RecordPoint.Connectors.SDK.ContentManager.SynchronousSubmitRecordOperation.ConnectorConfigId')
  - [ServiceName](#P-RecordPoint-Connectors-SDK-ContentManager-SynchronousSubmitRecordOperation-ServiceName 'RecordPoint.Connectors.SDK.ContentManager.SynchronousSubmitRecordOperation.ServiceName')
  - [WorkType](#P-RecordPoint-Connectors-SDK-ContentManager-SynchronousSubmitRecordOperation-WorkType 'RecordPoint.Connectors.SDK.ContentManager.SynchronousSubmitRecordOperation.WorkType')
  - [GetCustomKeyDimensions()](#M-RecordPoint-Connectors-SDK-ContentManager-SynchronousSubmitRecordOperation-GetCustomKeyDimensions 'RecordPoint.Connectors.SDK.ContentManager.SynchronousSubmitRecordOperation.GetCustomKeyDimensions')
  - [InnerDispose()](#M-RecordPoint-Connectors-SDK-ContentManager-SynchronousSubmitRecordOperation-InnerDispose 'RecordPoint.Connectors.SDK.ContentManager.SynchronousSubmitRecordOperation.InnerDispose')
  - [InnerRunAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-SynchronousSubmitRecordOperation-InnerRunAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.SynchronousSubmitRecordOperation.InnerRunAsync(System.Threading.CancellationToken)')
- [SystemContext](#T-RecordPoint-Connectors-SDK-Context-SystemContext 'RecordPoint.Connectors.SDK.Context.SystemContext')
  - [#ctor(hostEnvironment,systemOptions)](#M-RecordPoint-Connectors-SDK-Context-SystemContext-#ctor-Microsoft-Extensions-Hosting-IHostEnvironment,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Context-SystemOptions}- 'RecordPoint.Connectors.SDK.Context.SystemContext.#ctor(Microsoft.Extensions.Hosting.IHostEnvironment,Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Context.SystemOptions})')
  - [_hostEnvironment](#F-RecordPoint-Connectors-SDK-Context-SystemContext-_hostEnvironment 'RecordPoint.Connectors.SDK.Context.SystemContext._hostEnvironment')
  - [_systemOptions](#F-RecordPoint-Connectors-SDK-Context-SystemContext-_systemOptions 'RecordPoint.Connectors.SDK.Context.SystemContext._systemOptions')
  - [GetCompanyName()](#M-RecordPoint-Connectors-SDK-Context-SystemContext-GetCompanyName 'RecordPoint.Connectors.SDK.Context.SystemContext.GetCompanyName')
  - [GetConnectorName()](#M-RecordPoint-Connectors-SDK-Context-SystemContext-GetConnectorName 'RecordPoint.Connectors.SDK.Context.SystemContext.GetConnectorName')
  - [GetContentRootPath()](#M-RecordPoint-Connectors-SDK-Context-SystemContext-GetContentRootPath 'RecordPoint.Connectors.SDK.Context.SystemContext.GetContentRootPath')
  - [GetDataRootPath()](#M-RecordPoint-Connectors-SDK-Context-SystemContext-GetDataRootPath 'RecordPoint.Connectors.SDK.Context.SystemContext.GetDataRootPath')
  - [GetDefaultDataRootPath()](#M-RecordPoint-Connectors-SDK-Context-SystemContext-GetDefaultDataRootPath 'RecordPoint.Connectors.SDK.Context.SystemContext.GetDefaultDataRootPath')
  - [GetServiceName()](#M-RecordPoint-Connectors-SDK-Context-SystemContext-GetServiceName 'RecordPoint.Connectors.SDK.Context.SystemContext.GetServiceName')
  - [GetShortName()](#M-RecordPoint-Connectors-SDK-Context-SystemContext-GetShortName 'RecordPoint.Connectors.SDK.Context.SystemContext.GetShortName')
- [SystemContextBuilderExtensions](#T-RecordPoint-Connectors-SDK-Context-SystemContextBuilderExtensions 'RecordPoint.Connectors.SDK.Context.SystemContextBuilderExtensions')
  - [UseSystemContext(hostBuilder,companyName,connectorName,shortName,serviceName)](#M-RecordPoint-Connectors-SDK-Context-SystemContextBuilderExtensions-UseSystemContext-Microsoft-Extensions-Hosting-IHostBuilder,System-String,System-String,System-String,System-String- 'RecordPoint.Connectors.SDK.Context.SystemContextBuilderExtensions.UseSystemContext(Microsoft.Extensions.Hosting.IHostBuilder,System.String,System.String,System.String,System.String)')
- [TelemetryClientFactory](#T-RecordPoint-Connectors-SDK-Observability-AppInsights-TelemetryClientFactory 'RecordPoint.Connectors.SDK.Observability.AppInsights.TelemetryClientFactory')
  - [#ctor(applicationInsightOptions)](#M-RecordPoint-Connectors-SDK-Observability-AppInsights-TelemetryClientFactory-#ctor-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightOptions}- 'RecordPoint.Connectors.SDK.Observability.AppInsights.TelemetryClientFactory.#ctor(Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Observability.AppInsights.ApplicationInsightOptions})')
  - [GetTelemetryClient()](#M-RecordPoint-Connectors-SDK-Observability-AppInsights-TelemetryClientFactory-GetTelemetryClient 'RecordPoint.Connectors.SDK.Observability.AppInsights.TelemetryClientFactory.GetTelemetryClient')
- [TelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-TelemetryTracker 'RecordPoint.Connectors.SDK.Observability.TelemetryTracker')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Observability-TelemetryTracker-#ctor-RecordPoint-Connectors-SDK-Observability-IObservabilityScope,System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Observability-ITelemetrySink},RecordPoint-Connectors-SDK-Context-ISystemContext- 'RecordPoint.Connectors.SDK.Observability.TelemetryTracker.#ctor(RecordPoint.Connectors.SDK.Observability.IObservabilityScope,System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Observability.ITelemetrySink},RecordPoint.Connectors.SDK.Context.ISystemContext)')
  - [BeginScope()](#M-RecordPoint-Connectors-SDK-Observability-TelemetryTracker-BeginScope-RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures- 'RecordPoint.Connectors.SDK.Observability.TelemetryTracker.BeginScope(RecordPoint.Connectors.SDK.Observability.Dimensions,RecordPoint.Connectors.SDK.Observability.Measures)')
  - [GatherDimensions(dimensions,exception)](#M-RecordPoint-Connectors-SDK-Observability-TelemetryTracker-GatherDimensions-RecordPoint-Connectors-SDK-Observability-Dimensions,System-Exception- 'RecordPoint.Connectors.SDK.Observability.TelemetryTracker.GatherDimensions(RecordPoint.Connectors.SDK.Observability.Dimensions,System.Exception)')
  - [GatherMeasures(measures,exception)](#M-RecordPoint-Connectors-SDK-Observability-TelemetryTracker-GatherMeasures-RecordPoint-Connectors-SDK-Observability-Measures,System-Exception- 'RecordPoint.Connectors.SDK.Observability.TelemetryTracker.GatherMeasures(RecordPoint.Connectors.SDK.Observability.Measures,System.Exception)')
  - [TrackEvent()](#M-RecordPoint-Connectors-SDK-Observability-TelemetryTracker-TrackEvent-System-String,RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures- 'RecordPoint.Connectors.SDK.Observability.TelemetryTracker.TrackEvent(System.String,RecordPoint.Connectors.SDK.Observability.Dimensions,RecordPoint.Connectors.SDK.Observability.Measures)')
  - [TrackException()](#M-RecordPoint-Connectors-SDK-Observability-TelemetryTracker-TrackException-System-Exception,RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures- 'RecordPoint.Connectors.SDK.Observability.TelemetryTracker.TrackException(System.Exception,RecordPoint.Connectors.SDK.Observability.Dimensions,RecordPoint.Connectors.SDK.Observability.Measures)')
  - [TrackTrace()](#M-RecordPoint-Connectors-SDK-Observability-TelemetryTracker-TrackTrace-System-String,RecordPoint-Connectors-SDK-Observability-SeverityLevel,RecordPoint-Connectors-SDK-Observability-Dimensions- 'RecordPoint.Connectors.SDK.Observability.TelemetryTracker.TrackTrace(System.String,RecordPoint.Connectors.SDK.Observability.SeverityLevel,RecordPoint.Connectors.SDK.Observability.Dimensions)')
- [TimeBuilderExtensions](#T-RecordPoint-Connectors-SDK-Time-TimeBuilderExtensions 'RecordPoint.Connectors.SDK.Time.TimeBuilderExtensions')
  - [UseSystemTime(hostBuilder)](#M-RecordPoint-Connectors-SDK-Time-TimeBuilderExtensions-UseSystemTime-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Time.TimeBuilderExtensions.UseSystemTime(Microsoft.Extensions.Hosting.IHostBuilder)')
- [UptimeStrategy](#T-RecordPoint-Connectors-SDK-Health-UptimeStrategy 'RecordPoint.Connectors.SDK.Health.UptimeStrategy')
  - [#ctor(dateTimeProvider)](#M-RecordPoint-Connectors-SDK-Health-UptimeStrategy-#ctor-RecordPoint-Connectors-SDK-Providers-IDateTimeProvider- 'RecordPoint.Connectors.SDK.Health.UptimeStrategy.#ctor(RecordPoint.Connectors.SDK.Providers.IDateTimeProvider)')
  - [HEALTH_CHECK_TYPE](#F-RecordPoint-Connectors-SDK-Health-UptimeStrategy-HEALTH_CHECK_TYPE 'RecordPoint.Connectors.SDK.Health.UptimeStrategy.HEALTH_CHECK_TYPE')
  - [UPTIME_SECONDS_NAME](#F-RecordPoint-Connectors-SDK-Health-UptimeStrategy-UPTIME_SECONDS_NAME 'RecordPoint.Connectors.SDK.Health.UptimeStrategy.UPTIME_SECONDS_NAME')
  - [HealthCheckType](#P-RecordPoint-Connectors-SDK-Health-UptimeStrategy-HealthCheckType 'RecordPoint.Connectors.SDK.Health.UptimeStrategy.HealthCheckType')
  - [HealthCheckAsync(stoppingToken)](#M-RecordPoint-Connectors-SDK-Health-UptimeStrategy-HealthCheckAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Health.UptimeStrategy.HealthCheckAsync(System.Threading.CancellationToken)')
- [WorkBase\`1](#T-RecordPoint-Connectors-SDK-Work-WorkBase`1 'RecordPoint.Connectors.SDK.Work.WorkBase`1')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Work-WorkBase`1-#ctor-RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider- 'RecordPoint.Connectors.SDK.Work.WorkBase`1.#ctor(RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,RecordPoint.Connectors.SDK.Providers.IDateTimeProvider)')
  - [DateTimeProvider](#P-RecordPoint-Connectors-SDK-Work-WorkBase`1-DateTimeProvider 'RecordPoint.Connectors.SDK.Work.WorkBase`1.DateTimeProvider')
  - [Exception](#P-RecordPoint-Connectors-SDK-Work-WorkBase`1-Exception 'RecordPoint.Connectors.SDK.Work.WorkBase`1.Exception')
  - [FinishDateTime](#P-RecordPoint-Connectors-SDK-Work-WorkBase`1-FinishDateTime 'RecordPoint.Connectors.SDK.Work.WorkBase`1.FinishDateTime')
  - [HasResult](#P-RecordPoint-Connectors-SDK-Work-WorkBase`1-HasResult 'RecordPoint.Connectors.SDK.Work.WorkBase`1.HasResult')
  - [Id](#P-RecordPoint-Connectors-SDK-Work-WorkBase`1-Id 'RecordPoint.Connectors.SDK.Work.WorkBase`1.Id')
  - [Parameter](#P-RecordPoint-Connectors-SDK-Work-WorkBase`1-Parameter 'RecordPoint.Connectors.SDK.Work.WorkBase`1.Parameter')
  - [ResultReason](#P-RecordPoint-Connectors-SDK-Work-WorkBase`1-ResultReason 'RecordPoint.Connectors.SDK.Work.WorkBase`1.ResultReason')
  - [ResultReasonDetails](#P-RecordPoint-Connectors-SDK-Work-WorkBase`1-ResultReasonDetails 'RecordPoint.Connectors.SDK.Work.WorkBase`1.ResultReasonDetails')
  - [ResultType](#P-RecordPoint-Connectors-SDK-Work-WorkBase`1-ResultType 'RecordPoint.Connectors.SDK.Work.WorkBase`1.ResultType')
  - [ScopeManager](#P-RecordPoint-Connectors-SDK-Work-WorkBase`1-ScopeManager 'RecordPoint.Connectors.SDK.Work.WorkBase`1.ScopeManager')
  - [StartDateTime](#P-RecordPoint-Connectors-SDK-Work-WorkBase`1-StartDateTime 'RecordPoint.Connectors.SDK.Work.WorkBase`1.StartDateTime')
  - [TelemetryTracker](#P-RecordPoint-Connectors-SDK-Work-WorkBase`1-TelemetryTracker 'RecordPoint.Connectors.SDK.Work.WorkBase`1.TelemetryTracker')
  - [WorkDuration](#P-RecordPoint-Connectors-SDK-Work-WorkBase`1-WorkDuration 'RecordPoint.Connectors.SDK.Work.WorkBase`1.WorkDuration')
  - [WorkType](#P-RecordPoint-Connectors-SDK-Work-WorkBase`1-WorkType 'RecordPoint.Connectors.SDK.Work.WorkBase`1.WorkType')
  - [BeginObservabilityScope()](#M-RecordPoint-Connectors-SDK-Work-WorkBase`1-BeginObservabilityScope 'RecordPoint.Connectors.SDK.Work.WorkBase`1.BeginObservabilityScope')
  - [CompleteAsync()](#M-RecordPoint-Connectors-SDK-Work-WorkBase`1-CompleteAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.WorkBase`1.CompleteAsync(System.String,System.Threading.CancellationToken)')
  - [CompleteAsync()](#M-RecordPoint-Connectors-SDK-Work-WorkBase`1-CompleteAsync-System-String,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.WorkBase`1.CompleteAsync(System.String,System.String,System.Threading.CancellationToken)')
  - [EnsureHasOutcome()](#M-RecordPoint-Connectors-SDK-Work-WorkBase`1-EnsureHasOutcome 'RecordPoint.Connectors.SDK.Work.WorkBase`1.EnsureHasOutcome')
  - [EnsureIncomplete()](#M-RecordPoint-Connectors-SDK-Work-WorkBase`1-EnsureIncomplete 'RecordPoint.Connectors.SDK.Work.WorkBase`1.EnsureIncomplete')
  - [FailAsync(reason,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-WorkBase`1-FailAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.WorkBase`1.FailAsync(System.String,System.Threading.CancellationToken)')
  - [FailAsync(exception,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-WorkBase`1-FailAsync-System-Exception,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.WorkBase`1.FailAsync(System.Exception,System.Threading.CancellationToken)')
  - [GetCoreKeyDimensions()](#M-RecordPoint-Connectors-SDK-Work-WorkBase`1-GetCoreKeyDimensions 'RecordPoint.Connectors.SDK.Work.WorkBase`1.GetCoreKeyDimensions')
  - [GetCoreResultDimensions()](#M-RecordPoint-Connectors-SDK-Work-WorkBase`1-GetCoreResultDimensions 'RecordPoint.Connectors.SDK.Work.WorkBase`1.GetCoreResultDimensions')
  - [GetCoreResultMeasures()](#M-RecordPoint-Connectors-SDK-Work-WorkBase`1-GetCoreResultMeasures 'RecordPoint.Connectors.SDK.Work.WorkBase`1.GetCoreResultMeasures')
  - [GetCoreStartDimensions()](#M-RecordPoint-Connectors-SDK-Work-WorkBase`1-GetCoreStartDimensions 'RecordPoint.Connectors.SDK.Work.WorkBase`1.GetCoreStartDimensions')
  - [GetCoreStartMeasures()](#M-RecordPoint-Connectors-SDK-Work-WorkBase`1-GetCoreStartMeasures 'RecordPoint.Connectors.SDK.Work.WorkBase`1.GetCoreStartMeasures')
  - [GetCustomKeyDimensions()](#M-RecordPoint-Connectors-SDK-Work-WorkBase`1-GetCustomKeyDimensions 'RecordPoint.Connectors.SDK.Work.WorkBase`1.GetCustomKeyDimensions')
  - [GetCustomResultDimensions()](#M-RecordPoint-Connectors-SDK-Work-WorkBase`1-GetCustomResultDimensions 'RecordPoint.Connectors.SDK.Work.WorkBase`1.GetCustomResultDimensions')
  - [GetCustomResultMeasures()](#M-RecordPoint-Connectors-SDK-Work-WorkBase`1-GetCustomResultMeasures 'RecordPoint.Connectors.SDK.Work.WorkBase`1.GetCustomResultMeasures')
  - [GetCustomStartDimensions()](#M-RecordPoint-Connectors-SDK-Work-WorkBase`1-GetCustomStartDimensions 'RecordPoint.Connectors.SDK.Work.WorkBase`1.GetCustomStartDimensions')
  - [GetCustomStartMeasures()](#M-RecordPoint-Connectors-SDK-Work-WorkBase`1-GetCustomStartMeasures 'RecordPoint.Connectors.SDK.Work.WorkBase`1.GetCustomStartMeasures')
  - [GetKeyDimensions()](#M-RecordPoint-Connectors-SDK-Work-WorkBase`1-GetKeyDimensions 'RecordPoint.Connectors.SDK.Work.WorkBase`1.GetKeyDimensions')
  - [HandleAbandonedResultAsync()](#M-RecordPoint-Connectors-SDK-Work-WorkBase`1-HandleAbandonedResultAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.WorkBase`1.HandleAbandonedResultAsync(System.String,System.Threading.CancellationToken)')
  - [InnerRunAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-WorkBase`1-InnerRunAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.WorkBase`1.InnerRunAsync(System.Threading.CancellationToken)')
  - [RunAsync()](#M-RecordPoint-Connectors-SDK-Work-WorkBase`1-RunAsync-`0,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.WorkBase`1.RunAsync(`0,System.Threading.CancellationToken)')
  - [TrackFinish()](#M-RecordPoint-Connectors-SDK-Work-WorkBase`1-TrackFinish 'RecordPoint.Connectors.SDK.Work.WorkBase`1.TrackFinish')
  - [TrackStart()](#M-RecordPoint-Connectors-SDK-Work-WorkBase`1-TrackStart 'RecordPoint.Connectors.SDK.Work.WorkBase`1.TrackStart')
- [WorkBuilderExtensions](#T-RecordPoint-Connectors-SDK-Work-WorkBuilderExtensions 'RecordPoint.Connectors.SDK.Work.WorkBuilderExtensions')
  - [AddQueueableWorkOperation\`\`1(services)](#M-RecordPoint-Connectors-SDK-Work-WorkBuilderExtensions-AddQueueableWorkOperation``1-Microsoft-Extensions-DependencyInjection-IServiceCollection- 'RecordPoint.Connectors.SDK.Work.WorkBuilderExtensions.AddQueueableWorkOperation``1(Microsoft.Extensions.DependencyInjection.IServiceCollection)')
  - [UseWorkManager(hostBuilder)](#M-RecordPoint-Connectors-SDK-Work-WorkBuilderExtensions-UseWorkManager-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Work.WorkBuilderExtensions.UseWorkManager(Microsoft.Extensions.Hosting.IHostBuilder)')
- [WorkQueueClientExtensions](#T-RecordPoint-Connectors-SDK-ContentManager-WorkQueueClientExtensions 'RecordPoint.Connectors.SDK.ContentManager.WorkQueueClientExtensions')
  - [DisposeRecordAsync(workQueueClient,contentSubmissionConfiguration,record,waitTill,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-WorkQueueClientExtensions-DisposeRecordAsync-RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-ContentManager-ContentSubmissionConfiguration,RecordPoint-Connectors-SDK-Content-Record,System-Nullable{System-DateTimeOffset},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.WorkQueueClientExtensions.DisposeRecordAsync(RecordPoint.Connectors.SDK.Work.IWorkQueueClient,RecordPoint.Connectors.SDK.ContentManager.ContentSubmissionConfiguration,RecordPoint.Connectors.SDK.Content.Record,System.Nullable{System.DateTimeOffset},System.Threading.CancellationToken)')
  - [SubmitAggregationAsync(workQueueClient,contentSubmissionConfiguration,aggregation,waitTill,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-WorkQueueClientExtensions-SubmitAggregationAsync-RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-ContentManager-ContentSubmissionConfiguration,RecordPoint-Connectors-SDK-Content-Aggregation,System-Nullable{System-DateTimeOffset},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.WorkQueueClientExtensions.SubmitAggregationAsync(RecordPoint.Connectors.SDK.Work.IWorkQueueClient,RecordPoint.Connectors.SDK.ContentManager.ContentSubmissionConfiguration,RecordPoint.Connectors.SDK.Content.Aggregation,System.Nullable{System.DateTimeOffset},System.Threading.CancellationToken)')
  - [SubmitAuditEventAsync(workQueueClient,contentSubmissionConfiguration,auditEvent,waitTill,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-WorkQueueClientExtensions-SubmitAuditEventAsync-RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-ContentManager-ContentSubmissionConfiguration,RecordPoint-Connectors-SDK-Content-AuditEvent,System-Nullable{System-DateTimeOffset},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.WorkQueueClientExtensions.SubmitAuditEventAsync(RecordPoint.Connectors.SDK.Work.IWorkQueueClient,RecordPoint.Connectors.SDK.ContentManager.ContentSubmissionConfiguration,RecordPoint.Connectors.SDK.Content.AuditEvent,System.Nullable{System.DateTimeOffset},System.Threading.CancellationToken)')
  - [SubmitBinaryAsync(workQueueClient,contentSubmissionConfiguration,binaryMetaInfo,waitTill,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-WorkQueueClientExtensions-SubmitBinaryAsync-RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-ContentManager-ContentSubmissionConfiguration,RecordPoint-Connectors-SDK-Content-BinaryMetaInfo,System-Nullable{System-DateTimeOffset},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.WorkQueueClientExtensions.SubmitBinaryAsync(RecordPoint.Connectors.SDK.Work.IWorkQueueClient,RecordPoint.Connectors.SDK.ContentManager.ContentSubmissionConfiguration,RecordPoint.Connectors.SDK.Content.BinaryMetaInfo,System.Nullable{System.DateTimeOffset},System.Threading.CancellationToken)')
  - [SubmitRecordAsync(workQueueClient,contentSubmissionConfiguration,record,waitTill,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-WorkQueueClientExtensions-SubmitRecordAsync-RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-ContentManager-ContentSubmissionConfiguration,RecordPoint-Connectors-SDK-Content-Record,System-Nullable{System-DateTimeOffset},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.WorkQueueClientExtensions.SubmitRecordAsync(RecordPoint.Connectors.SDK.Work.IWorkQueueClient,RecordPoint.Connectors.SDK.ContentManager.ContentSubmissionConfiguration,RecordPoint.Connectors.SDK.Content.Record,System.Nullable{System.DateTimeOffset},System.Threading.CancellationToken)')

<a name='T-RecordPoint-Connectors-SDK-Observability-AppInsights-AppInsightsTelemetryBuilderExtensions'></a>
## AppInsightsTelemetryBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability.AppInsights

##### Summary

Host builder extensions for the log based telemetry

<a name='M-RecordPoint-Connectors-SDK-Observability-AppInsights-AppInsightsTelemetryBuilderExtensions-UseAppInsightsTelemetryTracking-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseAppInsightsTelemetryTracking() `method`

##### Summary

Registers Application Insights Telemetry tracking services

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Configuration-AppSettingsConfigurationBuilderExtensions'></a>
## AppSettingsConfigurationBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Configuration

##### Summary

Extension to configure R365 Settings from AppSettings container

<a name='M-RecordPoint-Connectors-SDK-Configuration-AppSettingsConfigurationBuilderExtensions-UseR365AppSettingsConfiguration-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseR365AppSettingsConfiguration(hostBuilder) `method`

##### Summary

Configure the host to use the app settings configurator

##### Returns

Updated host builder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | Host builder to configure |

<a name='T-RecordPoint-Connectors-SDK-Configuration-AppSettingsR365ConfigurationClient'></a>
## AppSettingsR365ConfigurationClient `type`

##### Namespace

RecordPoint.Connectors.SDK.Configuration

##### Summary

Appsettings R365 configuration client

<a name='M-RecordPoint-Connectors-SDK-Configuration-AppSettingsR365ConfigurationClient-#ctor-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Configuration-R365ConfigurationModel},Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Configuration-R365MultiConfigurationModel}-'></a>
### #ctor(r365ConfigurationModel,r365MultiConfigurationModel) `constructor`

##### Summary

Constructor

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| r365ConfigurationModel | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Configuration.R365ConfigurationModel}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Configuration-R365ConfigurationModel} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Configuration.R365ConfigurationModel}') |  |
| r365MultiConfigurationModel | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Configuration.R365MultiConfigurationModel}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Configuration-R365MultiConfigurationModel} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Configuration.R365MultiConfigurationModel}') |  |

<a name='M-RecordPoint-Connectors-SDK-Configuration-AppSettingsR365ConfigurationClient-GetR365Configuration-System-String-'></a>
### GetR365Configuration() `method`

##### Summary

Get the Records 365 configuration using options key for multi configuration lookup

##### Returns

Default records 365 configuration, null if it doesn't exist

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Configuration-AppSettingsR365ConfigurationClient-R365ConfigurationExists'></a>
### R365ConfigurationExists() `method`

##### Summary

Does a R365 configuration exist?

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightsTelemetrySink'></a>
## ApplicationInsightsTelemetrySink `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability.AppInsights

##### Summary

Application Insights Telemetry Sink

<a name='M-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightsTelemetrySink-#ctor-RecordPoint-Connectors-SDK-Observability-AppInsights-ITelemetryClientFactory,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightOptions},RecordPoint-Connectors-SDK-Toggles-IToggleProvider,RecordPoint-Connectors-SDK-Context-ISystemContext-'></a>
### #ctor() `constructor`

##### Summary

Application Insights Telemetry Sink

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightsTelemetrySink-IsConfigured'></a>
### IsConfigured() `method`

##### Summary

Checks if is configured.

##### Returns

A bool

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightsTelemetrySink-IsEnabled'></a>
### IsEnabled() `method`

##### Summary

Checks if is enabled.

##### Returns

A bool

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightsTelemetrySink-TrackEvent-System-String,RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures-'></a>
### TrackEvent() `method`

##### Summary

Tracks a custom event

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightsTelemetrySink-TrackException-System-Exception,RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures-'></a>
### TrackException() `method`

##### Summary

Tracks an exception

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightsTelemetrySink-TrackTrace-System-String,RecordPoint-Connectors-SDK-Observability-SeverityLevel,RecordPoint-Connectors-SDK-Observability-Dimensions-'></a>
### TrackTrace() `method`

##### Summary

Tracks a trace message

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Providers-AzureBlobRetryProvider'></a>
## AzureBlobRetryProvider `type`

##### Namespace

RecordPoint.Connectors.SDK.Providers

##### Summary

Retry provider for Azure blob storage

<a name='P-RecordPoint-Connectors-SDK-Providers-AzureBlobRetryProvider-Log'></a>
### Log `property`

##### Summary

A log.

<a name='M-RecordPoint-Connectors-SDK-Providers-AzureBlobRetryProvider-ExecuteWithRetry-System-Func{System-Threading-Tasks-Task},System-Type,System-String-'></a>
### ExecuteWithRetry() `method`

##### Summary

Execute the code with a retry policy

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Providers-AzureBlobRetryProvider-GetRetryPolicy-System-Type,System-String-'></a>
### GetRetryPolicy(type,methodName) `method`

##### Summary

Retry policy

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') |  |
| methodName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-RecordPoint-Connectors-SDK-Providers-AzureBlobRetryProviderWithCircuitBreaker'></a>
## AzureBlobRetryProviderWithCircuitBreaker `type`

##### Namespace

RecordPoint.Connectors.SDK.Providers

##### Summary

Retry provider with circuit breaker for Azure blob storage

<a name='M-RecordPoint-Connectors-SDK-Providers-AzureBlobRetryProviderWithCircuitBreaker-#ctor-RecordPoint-Connectors-SDK-Providers-CircuitBreakerOptions,System-Boolean-'></a>
### #ctor(options,useCircuit) `constructor`

##### Summary

Constructor

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| options | [RecordPoint.Connectors.SDK.Providers.CircuitBreakerOptions](#T-RecordPoint-Connectors-SDK-Providers-CircuitBreakerOptions 'RecordPoint.Connectors.SDK.Providers.CircuitBreakerOptions') |  |
| useCircuit | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |

<a name='M-RecordPoint-Connectors-SDK-Providers-AzureBlobRetryProviderWithCircuitBreaker-GetRetryPolicy-System-Type,System-String-'></a>
### GetRetryPolicy(type,methodName) `method`

##### Summary

Retry Policy

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') |  |
| methodName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-Providers-AzureBlobRetryProviderWithCircuitBreaker-IsCircuitClosed-System-TimeSpan@-'></a>
### IsCircuitClosed(waitFor) `method`

##### Summary

Check if the circuit is closed and set a waiting period if the circuit is open

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| waitFor | [System.TimeSpan@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.TimeSpan@ 'System.TimeSpan@') |  |

<a name='T-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation'></a>
## ChannelDiscoveryOperation `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

The channel discovery operation.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider,RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,RecordPoint-Connectors-SDK-Content-IChannelManager,RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-Work-IManagedWorkFactory,RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperationOptions},Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions}-'></a>
### #ctor(serviceProvider,contentManagerActionProvider,connectorManager,channelManager,workQueueClient,managedWorkFactory,managedWorkStatusManager,systemContext,observabilityScope,telemetryTracker,dateTimeProvider,options,contentManagerOptions) `constructor`

##### Summary

Initializes a new instance of the [ChannelDiscoveryOperation](#T-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperation') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| serviceProvider | [System.IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider') | The service provider. |
| contentManagerActionProvider | [RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider](#T-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider 'RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider') | The content manager action provider. |
| connectorManager | [RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager](#T-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager') | The connector manager. |
| channelManager | [RecordPoint.Connectors.SDK.Content.IChannelManager](#T-RecordPoint-Connectors-SDK-Content-IChannelManager 'RecordPoint.Connectors.SDK.Content.IChannelManager') | The channel manager. |
| workQueueClient | [RecordPoint.Connectors.SDK.Work.IWorkQueueClient](#T-RecordPoint-Connectors-SDK-Work-IWorkQueueClient 'RecordPoint.Connectors.SDK.Work.IWorkQueueClient') | The work queue client. |
| managedWorkFactory | [RecordPoint.Connectors.SDK.Work.IManagedWorkFactory](#T-RecordPoint-Connectors-SDK-Work-IManagedWorkFactory 'RecordPoint.Connectors.SDK.Work.IManagedWorkFactory') | The managed work factory. |
| managedWorkStatusManager | [RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager](#T-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager 'RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager') | The managed work status manager. |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | The scope manager. |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The telemetry tracker. |
| dateTimeProvider | [RecordPoint.Connectors.SDK.Providers.IDateTimeProvider](#T-RecordPoint-Connectors-SDK-Providers-IDateTimeProvider 'RecordPoint.Connectors.SDK.Providers.IDateTimeProvider') | The date time provider. |
| options | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperationOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperationOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperationOptions}') | The options. |
| contentManagerOptions | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.ContentManagerOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.ContentManagerOptions}') | The content manager options. |

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-WORK_TYPE'></a>
### WORK_TYPE `constants`

##### Summary

WORK TYPE.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-_actionExecutionTimespan'></a>
### _actionExecutionTimespan `constants`

##### Summary

How long it took to fetch content

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-_channelDiscoveryResult'></a>
### _channelDiscoveryResult `constants`

##### Summary

Outcome of the Channel operation

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-_channelManager'></a>
### _channelManager `constants`

##### Summary

The channel manager.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-_connectorConfiguration'></a>
### _connectorConfiguration `constants`

##### Summary

The connector configuration.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-_connectorManager'></a>
### _connectorManager `constants`

##### Summary

The connector manager.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-_contentManagerActionProvider'></a>
### _contentManagerActionProvider `constants`

##### Summary

The content manager action provider.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-_contentManagerOptions'></a>
### _contentManagerOptions `constants`

##### Summary

Content Manager Options

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-_contentRegistrationOperationsStarted'></a>
### _contentRegistrationOperationsStarted `constants`

##### Summary

Number of content registration operations started

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-_contentSynchronisationOperationsStarted'></a>
### _contentSynchronisationOperationsStarted `constants`

##### Summary

Number of contents synchronisation operations started

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-_managedWorkFactory'></a>
### _managedWorkFactory `constants`

##### Summary

The managed work factory.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-_managedWorkStatusManager'></a>
### _managedWorkStatusManager `constants`

##### Summary

The managed work status manager.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-_options'></a>
### _options `constants`

##### Summary

The options.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-_submitTimespan'></a>
### _submitTimespan `constants`

##### Summary

How long it took to submit the work to the queue

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-_workQueueClient'></a>
### _workQueueClient `constants`

##### Summary

Work queue client.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-ServiceName'></a>
### ServiceName `property`

##### Summary

Gets the service name.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-WorkType'></a>
### WorkType `property`

##### Summary

Gets the work type.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-CreateChannelDiscoveryAction-Microsoft-Extensions-DependencyInjection-IServiceScope-'></a>
### CreateChannelDiscoveryAction() `method`

##### Summary

Create a Channel Discovery Operation for the current connector configuration

##### Returns

Channel scanner

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-DeserializeConfiguration-System-String,System-String-'></a>
### DeserializeConfiguration(configurationType,configurationText) `method`

##### Summary

Deserialize the configuration.

##### Returns

A ChannelDiscoveryConfiguration

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| configurationType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The configuration type. |
| configurationText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The configuration text. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-DeserializeState-System-String,System-String-'></a>
### DeserializeState(stateType,stateText) `method`

##### Summary

Deserialize the state.

##### Returns

A ChannelDiscoveryState

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| stateType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The state type. |
| stateText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The state text. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-FetchAsync-System-Threading-CancellationToken-'></a>
### FetchAsync(cancellationToken) `method`

##### Summary

Assuming that everything is setup correct, go and fetch new Channels

##### Returns

Channel outcome

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-GetCustomKeyDimensions'></a>
### GetCustomKeyDimensions() `method`

##### Summary

Override custom dimensions key for ConnectorId, TenantId and TenantDomainName

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-GetCustomResultDimensions'></a>
### GetCustomResultDimensions() `method`

##### Summary

Get custom result dimensions.

##### Returns

A Dimensions

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-GetCustomResultMeasures'></a>
### GetCustomResultMeasures() `method`

##### Summary

Get custom result measures.

##### Returns

A Measures

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-GetMissingChannelsForWorkTypeAsync-System-Collections-Generic-List{RecordPoint-Connectors-SDK-Content-Channel},System-String,System-Func{RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel,System-String},System-Threading-CancellationToken-'></a>
### GetMissingChannelsForWorkTypeAsync() `method`

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-HandleAbandonedResultAsync-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult,System-Threading-CancellationToken-'></a>
### HandleAbandonedResultAsync(channelResult,cancellationToken) `method`

##### Summary

Handle abandoned result asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| channelResult | [RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryResult](#T-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryResult') | The channel result. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-HandleCompleteResultAsync-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult,System-Threading-CancellationToken-'></a>
### HandleCompleteResultAsync(channelResult,cancellationToken) `method`

##### Summary

Handle complete result asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| channelResult | [RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryResult](#T-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryResult') | The channel result. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-HandleFailedResultAsync-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult,System-Threading-CancellationToken-'></a>
### HandleFailedResultAsync(channelResult,cancellationToken) `method`

##### Summary

Handle failed result asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| channelResult | [RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryResult](#T-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryResult') | The channel result. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-HandleIncompleteResultAsync-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult,System-Threading-CancellationToken-'></a>
### HandleIncompleteResultAsync(channelResult,cancellationToken) `method`

##### Summary

Handle incomplete result asynchronously. Further Channel Discovery work is expected, tracked via cursor.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| channelResult | [RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryResult](#T-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryResult') | The channel result. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-HandleSuccessfulResultAsync-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult,System-Threading-CancellationToken-'></a>
### HandleSuccessfulResultAsync(channelResult,cancellationToken) `method`

##### Summary

Common subsequent logic for successful Channel Discovery fetch.

##### Returns

Start time of successful Channel Discovery result handling.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| channelResult | [RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryResult](#T-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryResult') | The channel result. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-InnerDispose'></a>
### InnerDispose() `method`

##### Summary

Dispose invocation results

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-InnerRunAsync-System-Threading-CancellationToken-'></a>
### InnerRunAsync(cancellationToken) `method`

##### Summary

Inner the run asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [RecordPoint.Connectors.SDK.RequiredValueOutOfRangeException](#T-RecordPoint-Connectors-SDK-RequiredValueOutOfRangeException 'RecordPoint.Connectors.SDK.RequiredValueOutOfRangeException') |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperation-SerializeState-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryState-'></a>
### SerializeState(state) `method`

##### Summary

Serialize the state.

##### Returns

A (string, string)

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| state | [RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryState](#T-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryState 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryState') | The state. |

<a name='T-RecordPoint-Connectors-SDK-Connectors-ConnectorClientExtensions'></a>
## ConnectorClientExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Connectors

##### Summary

Extension methods shared amongst all connector clients

<a name='M-RecordPoint-Connectors-SDK-Connectors-ConnectorClientExtensions-ConvertToConnectorConfig-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel-'></a>
### ConvertToConnectorConfig(connectorData) `method`

##### Summary

Convert a connector data model back into a connector config

##### Returns

Connector config

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorData | [RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel](#T-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel 'RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel') | Connector data to convert |

<a name='M-RecordPoint-Connectors-SDK-Connectors-ConnectorClientExtensions-ConvertToConnectorData-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-'></a>
### ConvertToConnectorData(connectorConfig) `method`

##### Summary

Convert a connector config model into a connector data model suitable for the database

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfig | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | Notification model containing a connector notification |

<a name='M-RecordPoint-Connectors-SDK-Connectors-ConnectorClientExtensions-GetConnectorAsync-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,System-String,System-Threading-CancellationToken-'></a>
### GetConnectorAsync(connectorClient,connectorId,cancellationToken) `method`

##### Summary

Get the connector asynchronously.

##### Returns

Task<ConnectorConfigModel>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorClient | [RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager](#T-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager') | The connector client. |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The connector id. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-Connectors-ConnectorClientExtensions-ListConnectorsAsync-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,System-Threading-CancellationToken-'></a>
### ListConnectorsAsync(connectorClient,cancellationToken) `method`

##### Summary

List the connectors asynchronously.

##### Returns

Task<List<ConnectorConfigModel>>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorClient | [RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager](#T-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager') | The connector client. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-Connectors-ConnectorClientExtensions-SetConnectorAsync-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,System-Threading-CancellationToken-'></a>
### SetConnectorAsync(connectorClient,connectorConfig,cancellationToken) `method`

##### Summary

Set the connector asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorClient | [RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager](#T-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager') | The connector client. |
| connectorConfig | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | The connector config. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='T-RecordPoint-Connectors-SDK-Configuration-ConnectorConfigExtensions'></a>
## ConnectorConfigExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Configuration

##### Summary

Connector config extensions as part of the framework.

<a name='F-RecordPoint-Connectors-SDK-Configuration-ConnectorConfigExtensions-CONSENT_AUTHORIZED_ON_PROPERTY'></a>
### CONSENT_AUTHORIZED_ON_PROPERTY `constants`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Configuration-ConnectorConfigExtensions-GetConsentAuthorizedOn-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-'></a>
### GetConsentAuthorizedOn(connectorConfiguration) `method`

##### Summary

Get consent authorized on.

##### Returns

A DateTimeOffset?

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfiguration | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | The connector configuration. |

<a name='M-RecordPoint-Connectors-SDK-Configuration-ConnectorConfigExtensions-SetConsentAuthorizedOn-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,System-DateTimeOffset-'></a>
### SetConsentAuthorizedOn(connectorConfig,value) `method`

##### Summary

Set consent authorized on.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfig | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | The connector config. |
| value | [System.DateTimeOffset](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTimeOffset 'System.DateTimeOffset') | The value. |

<a name='T-RecordPoint-Connectors-SDK-ConnectorConfigurationBuilder'></a>
## ConnectorConfigurationBuilder `type`

##### Namespace

RecordPoint.Connectors.SDK

##### Summary

Connector Configuration support

<a name='M-RecordPoint-Connectors-SDK-ConnectorConfigurationBuilder-CreateConfigurationBuilder-System-String[],System-Reflection-Assembly-'></a>
### CreateConfigurationBuilder(args,connectorAssembly) `method`

##### Summary

Creates a Connector configuration builder.

##### Returns

Configuration builder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| args | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Application args |
| connectorAssembly | [System.Reflection.Assembly](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Reflection.Assembly 'System.Reflection.Assembly') | Assembly of the application being configured |

##### Remarks

Create a configuration that loads settings from the following sources:
+ The appsettings.json file.
+ The appsetings.{Environment}.json files.
+ Environment values
+ Command line arguments

<a name='M-RecordPoint-Connectors-SDK-ConnectorConfigurationBuilder-UseAppSettings-Microsoft-Extensions-Hosting-IHostBuilder,System-String[],System-Reflection-Assembly-'></a>
### UseAppSettings(hostBuilder,args,connectorAssembly) `method`

##### Summary

Creates a Connector configuration builder.

##### Returns

Configuration builder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder to be configured |
| args | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | Application args |
| connectorAssembly | [System.Reflection.Assembly](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Reflection.Assembly 'System.Reflection.Assembly') | Assembly of the application being configured |

##### Remarks

Create a configuration that loads settings from the following sources:
+ The appsettings.json file.
+ The appsetings.{Environment}.json files.
+ Environment values
+ Command line arguments

<a name='M-RecordPoint-Connectors-SDK-ConnectorConfigurationBuilder-UseConfiguration-Microsoft-Extensions-Hosting-IHostBuilder,Microsoft-Extensions-Configuration-IConfiguration-'></a>
### UseConfiguration(hostBuilder,configuration) `method`

##### Summary

Adds the built Connector configuration to the Host Builder

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') |  |
| configuration | [Microsoft.Extensions.Configuration.IConfiguration](#T-Microsoft-Extensions-Configuration-IConfiguration 'Microsoft.Extensions.Configuration.IConfiguration') |  |

<a name='T-RecordPoint-Connectors-SDK-Status-ConnectorStatusStrategy'></a>
## ConnectorStatusStrategy `type`

##### Namespace

RecordPoint.Connectors.SDK.Status

##### Summary

The connector status strategy.

<a name='M-RecordPoint-Connectors-SDK-Status-ConnectorStatusStrategy-#ctor-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager-'></a>
### #ctor(connectorManager) `constructor`

##### Summary

Initializes a new instance of the [ConnectorStatusStrategy](#T-RecordPoint-Connectors-SDK-Status-ConnectorStatusStrategy 'RecordPoint.Connectors.SDK.Status.ConnectorStatusStrategy') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorManager | [RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager](#T-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager') | The connector manager. |

<a name='F-RecordPoint-Connectors-SDK-Status-ConnectorStatusStrategy-_connectorManager'></a>
### _connectorManager `constants`

##### Summary

The connector manager.

<a name='M-RecordPoint-Connectors-SDK-Status-ConnectorStatusStrategy-GetStatusText-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel,System-Threading-CancellationToken-'></a>
### GetStatusText(connectorModel,cancellationToken) `method`

##### Summary

Get status text.

##### Returns

Task<List<string>>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorModel | [RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel](#T-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel 'RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel') | The connector model. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='T-RecordPoint-Connectors-SDK-Connectors-ConnectorToggleExtensions'></a>
## ConnectorToggleExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Connectors

##### Summary

The connector toggle extensions.

<a name='M-RecordPoint-Connectors-SDK-Connectors-ConnectorToggleExtensions-GetConnectorBinarySubmissionKillswitch-RecordPoint-Connectors-SDK-Toggles-IToggleProvider,RecordPoint-Connectors-SDK-Context-ISystemContext,System-String-'></a>
### GetConnectorBinarySubmissionKillswitch(toggleProvider,systemContext,tenantId) `method`

##### Summary

Get connector binary submission killswitch.

DEFAULT FALSE: As this is a KillSwitch, toggle will default to false if the underlying Toggle platform cannot be reached.

##### Returns

A bool

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| toggleProvider | [RecordPoint.Connectors.SDK.Toggles.IToggleProvider](#T-RecordPoint-Connectors-SDK-Toggles-IToggleProvider 'RecordPoint.Connectors.SDK.Toggles.IToggleProvider') | The toggle provider. |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |
| tenantId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The tenant id. |

<a name='M-RecordPoint-Connectors-SDK-Connectors-ConnectorToggleExtensions-GetConnectorContentProtection-RecordPoint-Connectors-SDK-Toggles-IToggleProvider,RecordPoint-Connectors-SDK-Context-ISystemContext,System-String-'></a>
### GetConnectorContentProtection(toggleProvider,systemContext,tenantId) `method`

##### Summary

Get connector content protection.

DEFAULT TRUE: The toggle will default to true so binaries are by default submitted to the platform.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| toggleProvider | [RecordPoint.Connectors.SDK.Toggles.IToggleProvider](#T-RecordPoint-Connectors-SDK-Toggles-IToggleProvider 'RecordPoint.Connectors.SDK.Toggles.IToggleProvider') |  |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') |  |
| tenantId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-Connectors-ConnectorToggleExtensions-GetConnectorEnabled-RecordPoint-Connectors-SDK-Toggles-IToggleProvider,RecordPoint-Connectors-SDK-Context-ISystemContext,System-String-'></a>
### GetConnectorEnabled(toggleProvider,systemContext,tenantId) `method`

##### Summary

Get connector enabled.

DEFAULT TRUE: As this is not a KillSwitch, toggle will default to true if the underlying Toggle platform cannot be reached.

##### Returns

A bool

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| toggleProvider | [RecordPoint.Connectors.SDK.Toggles.IToggleProvider](#T-RecordPoint-Connectors-SDK-Toggles-IToggleProvider 'RecordPoint.Connectors.SDK.Toggles.IToggleProvider') | The toggle provider. |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |
| tenantId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The tenant id. |

<a name='M-RecordPoint-Connectors-SDK-Connectors-ConnectorToggleExtensions-GetConnectorKillswitch-RecordPoint-Connectors-SDK-Toggles-IToggleProvider,RecordPoint-Connectors-SDK-Context-ISystemContext-'></a>
### GetConnectorKillswitch(toggleProvider,systemContext) `method`

##### Summary

Get connector killswitch.

DEFAULT FALSE: As this is a KillSwitch, toggle will default to false if the underlying Toggle platform cannot be reached.

##### Returns

A bool

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| toggleProvider | [RecordPoint.Connectors.SDK.Toggles.IToggleProvider](#T-RecordPoint-Connectors-SDK-Toggles-IToggleProvider 'RecordPoint.Connectors.SDK.Toggles.IToggleProvider') | The toggle provider. |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |

<a name='T-RecordPoint-Connectors-SDK-Observability-Console-ConsoleLoggingHostBuilderExtensions'></a>
## ConsoleLoggingHostBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability.Console

##### Summary

Host builder extensions for registering a console logger

<a name='M-RecordPoint-Connectors-SDK-Observability-Console-ConsoleLoggingHostBuilderExtensions-UseConsoleLogging-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseConsoleLogging() `method`

##### Summary

Register a console logger with the host

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Observability-Console-ConsoleLoggingSink'></a>
## ConsoleLoggingSink `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability.Console

##### Summary

Console Logging Sink

<a name='M-RecordPoint-Connectors-SDK-Observability-Console-ConsoleLoggingSink-#ctor-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Observability-Console-ConsoleLoggingOptions}-'></a>
### #ctor() `constructor`

##### Summary

Console Logging Sink

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Observability-Console-ConsoleLoggingSink-TrackEvent-System-String,RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures-'></a>
### TrackEvent(name,dimensions,measures) `method`

##### Summary

Logs an event to the console

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| dimensions | [RecordPoint.Connectors.SDK.Observability.Dimensions](#T-RecordPoint-Connectors-SDK-Observability-Dimensions 'RecordPoint.Connectors.SDK.Observability.Dimensions') |  |
| measures | [RecordPoint.Connectors.SDK.Observability.Measures](#T-RecordPoint-Connectors-SDK-Observability-Measures 'RecordPoint.Connectors.SDK.Observability.Measures') |  |

<a name='M-RecordPoint-Connectors-SDK-Observability-Console-ConsoleLoggingSink-TrackException-System-Exception,RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures-'></a>
### TrackException(exception,dimensions,measures) `method`

##### Summary

Logs an exception to the console

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| exception | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') |  |
| dimensions | [RecordPoint.Connectors.SDK.Observability.Dimensions](#T-RecordPoint-Connectors-SDK-Observability-Dimensions 'RecordPoint.Connectors.SDK.Observability.Dimensions') |  |
| measures | [RecordPoint.Connectors.SDK.Observability.Measures](#T-RecordPoint-Connectors-SDK-Observability-Measures 'RecordPoint.Connectors.SDK.Observability.Measures') |  |

<a name='M-RecordPoint-Connectors-SDK-Observability-Console-ConsoleLoggingSink-TrackTrace-System-String,RecordPoint-Connectors-SDK-Observability-SeverityLevel,RecordPoint-Connectors-SDK-Observability-Dimensions-'></a>
### TrackTrace(message,severityLevel,dimensions) `method`

##### Summary

Logs a trace to the console

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| severityLevel | [RecordPoint.Connectors.SDK.Observability.SeverityLevel](#T-RecordPoint-Connectors-SDK-Observability-SeverityLevel 'RecordPoint.Connectors.SDK.Observability.SeverityLevel') |  |
| dimensions | [RecordPoint.Connectors.SDK.Observability.Dimensions](#T-RecordPoint-Connectors-SDK-Observability-Dimensions 'RecordPoint.Connectors.SDK.Observability.Dimensions') |  |

<a name='T-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider'></a>
## ContentManagerActionProvider `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

The content manager action provider.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider-#ctor-System-IServiceProvider-'></a>
### #ctor(serviceProvider) `constructor`

##### Summary

Initializes a new instance of the [ContentManagerActionProvider](#T-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerActionProvider') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| serviceProvider | [System.IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider') | The service provider. |

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider-_serviceProvider'></a>
### _serviceProvider `constants`

##### Summary

The service provider.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider-CreateAggregationSubmissionCallbackAction-Microsoft-Extensions-DependencyInjection-IServiceScope-'></a>
### CreateAggregationSubmissionCallbackAction() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider-CreateAuditEventSubmissionCallbackAction-Microsoft-Extensions-DependencyInjection-IServiceScope-'></a>
### CreateAuditEventSubmissionCallbackAction() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider-CreateBinaryRetrievalAction-Microsoft-Extensions-DependencyInjection-IServiceScope-'></a>
### CreateBinaryRetrievalAction() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider-CreateBinarySubmissionCallbackAction-Microsoft-Extensions-DependencyInjection-IServiceScope-'></a>
### CreateBinarySubmissionCallbackAction() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider-CreateChannelDiscoveryAction-Microsoft-Extensions-DependencyInjection-IServiceScope-'></a>
### CreateChannelDiscoveryAction() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider-CreateContentManagerCallbackAction-Microsoft-Extensions-DependencyInjection-IServiceScope-'></a>
### CreateContentManagerCallbackAction() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider-CreateContentRegistrationAction-Microsoft-Extensions-DependencyInjection-IServiceScope-'></a>
### CreateContentRegistrationAction() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider-CreateContentSynchronisationAction-Microsoft-Extensions-DependencyInjection-IServiceScope-'></a>
### CreateContentSynchronisationAction() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider-CreateGenericAction``2-Microsoft-Extensions-DependencyInjection-IServiceScope-'></a>
### CreateGenericAction\`\`2() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider-CreateGenericManagedAction``2-Microsoft-Extensions-DependencyInjection-IServiceScope-'></a>
### CreateGenericManagedAction\`\`2() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider-CreateRecordDisposalAction-Microsoft-Extensions-DependencyInjection-IServiceScope-'></a>
### CreateRecordDisposalAction() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerActionProvider-CreateRecordSubmissionCallbackAction-Microsoft-Extensions-DependencyInjection-IServiceScope-'></a>
### CreateRecordSubmissionCallbackAction() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions'></a>
## ContentManagerBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

The content manager builder extensions.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseAggregationSubmissionOperation-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseAggregationSubmissionOperation(hostBuilder) `method`

##### Summary

Use aggregation submission operation.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseAggregationSubmissionOperation``1-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseAggregationSubmissionOperation\`\`1(hostBuilder) `method`

##### Summary

Use aggregation submission operation.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TCallbackAction |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseAuditEventSubmissionOperation-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseAuditEventSubmissionOperation(hostBuilder) `method`

##### Summary

Use audit event submission operation.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseAuditEventSubmissionOperation``1-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseAuditEventSubmissionOperation\`\`1(hostBuilder) `method`

##### Summary

Use audit event submission operation.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TCallbackAction |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseBinarySubmissionOperation``1-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseBinarySubmissionOperation\`\`1(hostBuilder) `method`

##### Summary

Use binary submission operation.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TAction |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseBinarySubmissionOperation``2-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseBinarySubmissionOperation\`\`2(hostBuilder) `method`

##### Summary

Use binary submission operation.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TAction |  |
| TCallbackAction |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseChannelDiscoveryOperation``1-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseChannelDiscoveryOperation\`\`1(hostBuilder) `method`

##### Summary

Use channel discovery operation.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TAction |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseContentManagerService-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseContentManagerService(hostBuilder) `method`

##### Summary

Use content manager service.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseContentManagerService``1-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseContentManagerService\`\`1(hostBuilder) `method`

##### Summary

Use content manager service.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TCallbackAction |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseContentRegistrationOperation``1-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseContentRegistrationOperation\`\`1(hostBuilder) `method`

##### Summary

Use content registration operation.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TAction |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseContentSynchronisationOperation``1-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseContentSynchronisationOperation\`\`1(hostBuilder) `method`

##### Summary

Use content synchronisation operation.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TAction |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseGenericManagedWorkOperation``5-Microsoft-Extensions-Hosting-IHostBuilder,System-String-'></a>
### UseGenericManagedWorkOperation\`\`5(hostBuilder,configSectionName) `method`

##### Summary

Use a generic managed work operation.
Like queueable work, managed work operations still implement IQueueableWork.
The difference here is this will configure options that are usually required by managed work operations.
If you don't need the configured Options, use 'UseGenericQueueableWorkOperation' instead.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |
| configSectionName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Section name for where to find the Options in the configuration root |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TOperation |  |
| TAction |  |
| TInput |  |
| TOutput |  |
| TOptions |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseGenericQueueableWorkOperation``4-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseGenericQueueableWorkOperation\`\`4(hostBuilder) `method`

##### Summary

Use a generic QueueableWork operation.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TOperation |  |
| TAction |  |
| TInput |  |
| TOutput |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseRecordDisposalOperation``1-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseRecordDisposalOperation\`\`1(hostBuilder) `method`

##### Summary

Use record disposal operation.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TAction |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseRecordSubmissionOperation-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseRecordSubmissionOperation(hostBuilder) `method`

##### Summary

Use record submission operation.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseRecordSubmissionOperation``1-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseRecordSubmissionOperation\`\`1(hostBuilder) `method`

##### Summary

Use record submission operation.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TCallbackAction |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseSynchronousRecordSubmissionOperation``1-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseSynchronousRecordSubmissionOperation\`\`1(hostBuilder) `method`

##### Summary

Use synchronous record submission operation.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TBinaryRetrieavalAction |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerBuilderExtensions-UseSynchronousRecordSubmissionOperation``3-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseSynchronousRecordSubmissionOperation\`\`3(hostBuilder) `method`

##### Summary

Use synchronous record submission operation.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TBinaryRetrieavalAction |  |
| TRecordSubmissionCallbackAction |  |
| TBinaryRetrieavalCallbackAction |  |

<a name='T-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation'></a>
## ContentManagerOperation `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

The content manager operation.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider,RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,RecordPoint-Connectors-SDK-Content-IChannelManager,RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager,RecordPoint-Connectors-SDK-Work-IManagedWorkFactory,RecordPoint-Connectors-SDK-Context-ISystemContext,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions},RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider-'></a>
### #ctor(serviceProvider,contentManagerActionProvider,connectorConfigManager,channelManager,managedWorkStatusManager,managedWorkFactory,systemContext,options,observabilityScope,telemetryTracker,dateTimeProvider) `constructor`

##### Summary

Initializes a new instance of the [ContentManagerOperation](#T-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperation') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| serviceProvider | [System.IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider') | The service provider. |
| contentManagerActionProvider | [RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider](#T-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider 'RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider') | The content manager action provider. |
| connectorConfigManager | [RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager](#T-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager') | The connector config manager. |
| channelManager | [RecordPoint.Connectors.SDK.Content.IChannelManager](#T-RecordPoint-Connectors-SDK-Content-IChannelManager 'RecordPoint.Connectors.SDK.Content.IChannelManager') | The channel manager. |
| managedWorkStatusManager | [RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager](#T-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager 'RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager') | The managed work status manager. |
| managedWorkFactory | [RecordPoint.Connectors.SDK.Work.IManagedWorkFactory](#T-RecordPoint-Connectors-SDK-Work-IManagedWorkFactory 'RecordPoint.Connectors.SDK.Work.IManagedWorkFactory') | The managed work factory. |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |
| options | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.ContentManagerOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.ContentManagerOptions}') | The options. |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | The scope manager. |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The telemetry tracker. |
| dateTimeProvider | [RecordPoint.Connectors.SDK.Providers.IDateTimeProvider](#T-RecordPoint-Connectors-SDK-Providers-IDateTimeProvider 'RecordPoint.Connectors.SDK.Providers.IDateTimeProvider') | The date time provider. |

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-CONTENT_SOURCE_INTEGRATION_COMPLETED'></a>
### CONTENT_SOURCE_INTEGRATION_COMPLETED `constants`

##### Summary

The CONTENT SOURCE INTEGRATION COMPLETED.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-WORK_TYPE'></a>
### WORK_TYPE `constants`

##### Summary

WORK TYPE.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-_channelManager'></a>
### _channelManager `constants`

##### Summary

The channel manager.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-_connectorConfigurationManager'></a>
### _connectorConfigurationManager `constants`

##### Summary

The connector configuration manager.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-_connectorConfigurations'></a>
### _connectorConfigurations `constants`

##### Summary

The connector configurations.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-_contentManagerActionProvider'></a>
### _contentManagerActionProvider `constants`

##### Summary

The content manager action provider.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-_managedWorkFactory'></a>
### _managedWorkFactory `constants`

##### Summary

The managed work factory.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-_managedWorkStatusManager'></a>
### _managedWorkStatusManager `constants`

##### Summary

The managed work status manager.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-_options'></a>
### _options `constants`

##### Summary

The options.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-ServiceName'></a>
### ServiceName `property`

##### Summary

Gets the service name.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-WorkType'></a>
### WorkType `property`

##### Summary

Gets the work type.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-CleanupAggregationsAsync-System-Threading-CancellationToken-'></a>
### CleanupAggregationsAsync(cancellationToken) `method`

##### Summary

Removes dangling aggregations asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-CleanupChannelsAsync-System-Threading-CancellationToken-'></a>
### CleanupChannelsAsync(cancellationToken) `method`

##### Summary

Removes dangling channels asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-CleanupWorkAsync-RecordPoint-Connectors-SDK-Work-ManagedWorkStatuses,System-Int32,System-Threading-CancellationToken-'></a>
### CleanupWorkAsync(status,maxWorkAge,cancellationToken) `method`

##### Summary

Cleanup the work asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| status | [RecordPoint.Connectors.SDK.Work.ManagedWorkStatuses](#T-RecordPoint-Connectors-SDK-Work-ManagedWorkStatuses 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatuses') | The status. |
| maxWorkAge | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The max work age. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-CreateChannelDiscoveryOperationsAsync-System-Threading-CancellationToken-'></a>
### CreateChannelDiscoveryOperationsAsync(cancellationToken) `method`

##### Summary

Creates channel discovery operations asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-DeserializeConfiguration-System-String,System-String-'></a>
### DeserializeConfiguration(configurationType,configurationText) `method`

##### Summary

Deserialize the configuration.

##### Returns

A ContentManagerConfiguration

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| configurationType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The configuration type. |
| configurationText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The configuration text. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-DeserializeState-System-String,System-String-'></a>
### DeserializeState(stateType,stateText) `method`

##### Summary

Deserialize the state.

##### Returns

A ContentManagerState

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| stateType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The state type. |
| stateText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The state text. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-GetCustomResultMeasures'></a>
### GetCustomResultMeasures() `method`

##### Summary

Get custom result measures.

##### Returns

A Measures

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-GetNewEnabledConnectorConfigurationsAsync-System-Threading-CancellationToken-'></a>
### GetNewEnabledConnectorConfigurationsAsync() `method`

##### Summary

Determines Connector Configurations that do not have Channel Discovery work running.

##### Returns

List<ConnectorConfigModel>

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-InnerRunAsync-System-Threading-CancellationToken-'></a>
### InnerRunAsync(cancellationToken) `method`

##### Summary

Inner the run asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperation-SerializeState-RecordPoint-Connectors-SDK-ContentManager-ContentManagerState-'></a>
### SerializeState(state) `method`

##### Summary

Serialize the state.

##### Returns

A (string, string)

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| state | [RecordPoint.Connectors.SDK.ContentManager.ContentManagerState](#T-RecordPoint-Connectors-SDK-ContentManager-ContentManagerState 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerState') | The state. |

<a name='T-RecordPoint-Connectors-SDK-ContentManager-ContentManagerService'></a>
## ContentManagerService `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Background service that bootstraps the channel discovery infrastructure for the content source

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerService-#ctor-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions},RecordPoint-Connectors-SDK-Work-IManagedWorkFactory,RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager-'></a>
### #ctor(options,managedWorkFactory,managedWorkStatusManager) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| options | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.ContentManagerOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.ContentManagerOptions}') |  |
| managedWorkFactory | [RecordPoint.Connectors.SDK.Work.IManagedWorkFactory](#T-RecordPoint-Connectors-SDK-Work-IManagedWorkFactory 'RecordPoint.Connectors.SDK.Work.IManagedWorkFactory') |  |
| managedWorkStatusManager | [RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager](#T-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager 'RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager') |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerService-ExecuteAsync-System-Threading-CancellationToken-'></a>
### ExecuteAsync(stoppingToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| stoppingToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='T-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation'></a>
## ContentRegistrationOperation `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

The content registration operation.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider,RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,RecordPoint-Connectors-SDK-Content-IChannelManager,RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-Work-IManagedWorkFactory,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Toggles-IToggleProvider,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperationOptions},Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions},Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-RecordSubmissionOptions}-'></a>
### #ctor(serviceProvider,contentManagerActionProvider,connectorManager,channelManager,workQueueClient,managedWorkFactory,systemContext,observabilityScope,toggleProvider,telemetryTracker,dateTimeProvider,options,contentManagerOptions,recordSubmissionOptions) `constructor`

##### Summary

Initializes a new instance of the [ContentRegistrationOperation](#T-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperation') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| serviceProvider | [System.IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider') | The service provider. |
| contentManagerActionProvider | [RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider](#T-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider 'RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider') | The content manager action provider. |
| connectorManager | [RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager](#T-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager') | The connector manager. |
| channelManager | [RecordPoint.Connectors.SDK.Content.IChannelManager](#T-RecordPoint-Connectors-SDK-Content-IChannelManager 'RecordPoint.Connectors.SDK.Content.IChannelManager') | The channel manager. |
| workQueueClient | [RecordPoint.Connectors.SDK.Work.IWorkQueueClient](#T-RecordPoint-Connectors-SDK-Work-IWorkQueueClient 'RecordPoint.Connectors.SDK.Work.IWorkQueueClient') | The work queue client. |
| managedWorkFactory | [RecordPoint.Connectors.SDK.Work.IManagedWorkFactory](#T-RecordPoint-Connectors-SDK-Work-IManagedWorkFactory 'RecordPoint.Connectors.SDK.Work.IManagedWorkFactory') | The managed work factory. |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | The scope manager. |
| toggleProvider | [RecordPoint.Connectors.SDK.Toggles.IToggleProvider](#T-RecordPoint-Connectors-SDK-Toggles-IToggleProvider 'RecordPoint.Connectors.SDK.Toggles.IToggleProvider') | The toggle provider. |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The telemetry tracker. |
| dateTimeProvider | [RecordPoint.Connectors.SDK.Providers.IDateTimeProvider](#T-RecordPoint-Connectors-SDK-Providers-IDateTimeProvider 'RecordPoint.Connectors.SDK.Providers.IDateTimeProvider') | The date time provider. |
| options | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperationOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperationOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperationOptions}') | The options. |
| contentManagerOptions | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.ContentManagerOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.ContentManagerOptions}') | The content manager options. |
| recordSubmissionOptions | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.RecordSubmissionOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-RecordSubmissionOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.RecordSubmissionOptions}') | The record submission options. |

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-WORK_TYPE'></a>
### WORK_TYPE `constants`

##### Summary

WORK TYPE.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-_actionExecutionTimespan'></a>
### _actionExecutionTimespan `constants`

##### Summary

How long it took to for the operation to execute

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-_channelManager'></a>
### _channelManager `constants`

##### Summary

The channel manager.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-_connectorConfiguration'></a>
### _connectorConfiguration `constants`

##### Summary

The connector configuration.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-_connectorManager'></a>
### _connectorManager `constants`

##### Summary

The connector manager.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-_contentManagerActionProvider'></a>
### _contentManagerActionProvider `constants`

##### Summary

The content manager action provider.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-_contentManagerOptions'></a>
### _contentManagerOptions `constants`

##### Summary

The content manager options.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-_contentResult'></a>
### _contentResult `constants`

##### Summary

Outcome of the operation

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-_options'></a>
### _options `constants`

##### Summary

The options.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-_recordSubmissionOptions'></a>
### _recordSubmissionOptions `constants`

##### Summary

The record submission options.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-_submitTimespan'></a>
### _submitTimespan `constants`

##### Summary

How long it took to submit the work to the queue

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-_toggleProvider'></a>
### _toggleProvider `constants`

##### Summary

Feature Toggle Provider.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-_workQueueClient'></a>
### _workQueueClient `constants`

##### Summary

Work queue client.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-ServiceName'></a>
### ServiceName `property`

##### Summary

Gets the service name.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-WorkType'></a>
### WorkType `property`

##### Summary

Gets the work type.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-BeginAsync-RecordPoint-Connectors-SDK-Content-Channel,System-Threading-CancellationToken-'></a>
### BeginAsync(channel,cancellationToken) `method`

##### Summary

Begins and return a task of type contentresult asynchronously.

##### Returns

Task<ContentResult>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| channel | [RecordPoint.Connectors.SDK.Content.Channel](#T-RecordPoint-Connectors-SDK-Content-Channel 'RecordPoint.Connectors.SDK.Content.Channel') | The channel. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-ContinueSync-RecordPoint-Connectors-SDK-Content-Channel,System-String,System-Threading-CancellationToken-'></a>
### ContinueSync(channel,cursor,cancellationToken) `method`

##### Summary

Continue the sync.

##### Returns

Task<ContentResult>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| channel | [RecordPoint.Connectors.SDK.Content.Channel](#T-RecordPoint-Connectors-SDK-Content-Channel 'RecordPoint.Connectors.SDK.Content.Channel') | The channel. |
| cursor | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The cursor. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-DeserializeConfiguration-System-String,System-String-'></a>
### DeserializeConfiguration(configurationType,configurationText) `method`

##### Summary

Deserialize the configuration.

##### Returns

A ContentRegistrationConfiguration

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| configurationType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The configuration type. |
| configurationText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The configuration text. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-DeserializeState-System-String,System-String-'></a>
### DeserializeState(stateType,stateText) `method`

##### Summary

Deserialize the state.

##### Returns

A ContentRegistrationState

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| stateType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The state type. |
| stateText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The state text. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-FetchAsync-System-Threading-CancellationToken-'></a>
### FetchAsync(cancellationToken) `method`

##### Summary

Assuming that everything is setup correct, go and fetch new Content

##### Returns

Fetch content outcome

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-GetCustomKeyDimensions'></a>
### GetCustomKeyDimensions() `method`

##### Summary

Override custom dimensions key for ConnectorId, TenantId and TenantDomainName

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-GetCustomResultDimensions'></a>
### GetCustomResultDimensions() `method`

##### Summary

Get custom result dimensions.

##### Returns

A Dimensions

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-GetCustomResultMeasures'></a>
### GetCustomResultMeasures() `method`

##### Summary

Get custom result measures.

##### Returns

A Measures

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-HandleAbandondedContentAsync-RecordPoint-Connectors-SDK-ContentManager-ContentResult,System-Threading-CancellationToken-'></a>
### HandleAbandondedContentAsync(contentResult,cancellationToken) `method`

##### Summary

Handle abandonded content asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| contentResult | [RecordPoint.Connectors.SDK.ContentManager.ContentResult](#T-RecordPoint-Connectors-SDK-ContentManager-ContentResult 'RecordPoint.Connectors.SDK.ContentManager.ContentResult') | The content result. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-HandleCompleteContentAsync-RecordPoint-Connectors-SDK-ContentManager-ContentResult,System-Threading-CancellationToken-'></a>
### HandleCompleteContentAsync(contentResult,cancellationToken) `method`

##### Summary

Handle complete content asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| contentResult | [RecordPoint.Connectors.SDK.ContentManager.ContentResult](#T-RecordPoint-Connectors-SDK-ContentManager-ContentResult 'RecordPoint.Connectors.SDK.ContentManager.ContentResult') | The content result. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-HandleFailedContentAsync-RecordPoint-Connectors-SDK-ContentManager-ContentResult,System-Threading-CancellationToken-'></a>
### HandleFailedContentAsync(contentResult,cancellationToken) `method`

##### Summary

Handle failed content asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| contentResult | [RecordPoint.Connectors.SDK.ContentManager.ContentResult](#T-RecordPoint-Connectors-SDK-ContentManager-ContentResult 'RecordPoint.Connectors.SDK.ContentManager.ContentResult') | The content result. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-HandleIncompleteContentAsync-RecordPoint-Connectors-SDK-ContentManager-ContentResult,System-Threading-CancellationToken-'></a>
### HandleIncompleteContentAsync(contentResult,cancellationToken) `method`

##### Summary

Handle incomplete content asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| contentResult | [RecordPoint.Connectors.SDK.ContentManager.ContentResult](#T-RecordPoint-Connectors-SDK-ContentManager-ContentResult 'RecordPoint.Connectors.SDK.ContentManager.ContentResult') | The content result. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-InnerDispose'></a>
### InnerDispose() `method`

##### Summary

Dispose invocation results

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-InnerRunAsync-System-Threading-CancellationToken-'></a>
### InnerRunAsync(cancellationToken) `method`

##### Summary

Inner the run asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [RecordPoint.Connectors.SDK.RequiredValueOutOfRangeException](#T-RecordPoint-Connectors-SDK-RequiredValueOutOfRangeException 'RecordPoint.Connectors.SDK.RequiredValueOutOfRangeException') |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-SerializeState-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationState-'></a>
### SerializeState(state) `method`

##### Summary

Serialize the state.

##### Returns

A (string, string)

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| state | [RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationState](#T-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationState 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationState') | The state. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperation-SubmitContentAsync-RecordPoint-Connectors-SDK-ContentManager-ContentResult,System-Threading-CancellationToken-'></a>
### SubmitContentAsync(contentResult,cancellationToken) `method`

##### Summary

Submits the content asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| contentResult | [RecordPoint.Connectors.SDK.ContentManager.ContentResult](#T-RecordPoint-Connectors-SDK-ContentManager-ContentResult 'RecordPoint.Connectors.SDK.ContentManager.ContentResult') | The content result. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='T-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation'></a>
## ContentSynchronisationOperation `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

The content synchronisation operation.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider,RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,RecordPoint-Connectors-SDK-Content-IChannelManager,RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-Work-IManagedWorkFactory,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Toggles-IToggleProvider,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperationOptions},Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions},Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-RecordSubmissionOptions}-'></a>
### #ctor(serviceProvider,contentManagerActionProvider,connectorManager,channelManager,workQueueClient,managedWorkFactory,systemContext,observabilityScope,toggleProvider,telemetryTracker,dateTimeProvider,options,contentManagerOptions,recordSubmissionOptions) `constructor`

##### Summary

Initializes a new instance of the [ContentSynchronisationOperation](#T-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperation') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| serviceProvider | [System.IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider') | The service provider. |
| contentManagerActionProvider | [RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider](#T-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider 'RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider') | The content manager action provider. |
| connectorManager | [RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager](#T-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager') | The connector manager. |
| channelManager | [RecordPoint.Connectors.SDK.Content.IChannelManager](#T-RecordPoint-Connectors-SDK-Content-IChannelManager 'RecordPoint.Connectors.SDK.Content.IChannelManager') | The channel manager. |
| workQueueClient | [RecordPoint.Connectors.SDK.Work.IWorkQueueClient](#T-RecordPoint-Connectors-SDK-Work-IWorkQueueClient 'RecordPoint.Connectors.SDK.Work.IWorkQueueClient') | The work queue client. |
| managedWorkFactory | [RecordPoint.Connectors.SDK.Work.IManagedWorkFactory](#T-RecordPoint-Connectors-SDK-Work-IManagedWorkFactory 'RecordPoint.Connectors.SDK.Work.IManagedWorkFactory') | The managed work factory. |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | The scope manager. |
| toggleProvider | [RecordPoint.Connectors.SDK.Toggles.IToggleProvider](#T-RecordPoint-Connectors-SDK-Toggles-IToggleProvider 'RecordPoint.Connectors.SDK.Toggles.IToggleProvider') | The toggle provider. |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The telemetry tracker. |
| dateTimeProvider | [RecordPoint.Connectors.SDK.Providers.IDateTimeProvider](#T-RecordPoint-Connectors-SDK-Providers-IDateTimeProvider 'RecordPoint.Connectors.SDK.Providers.IDateTimeProvider') | The date time provider. |
| options | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperationOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperationOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperationOptions}') | The options. |
| contentManagerOptions | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.ContentManagerOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.ContentManagerOptions}') | The content manager options. |
| recordSubmissionOptions | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.RecordSubmissionOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-ContentManager-RecordSubmissionOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.ContentManager.RecordSubmissionOptions}') | The record submission options. |

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-WORK_TYPE'></a>
### WORK_TYPE `constants`

##### Summary

WORK TYPE.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-_actionExecutionTimespan'></a>
### _actionExecutionTimespan `constants`

##### Summary

How long it took to for the operation to execute

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-_channelManager'></a>
### _channelManager `constants`

##### Summary

The channel manager.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-_connectorConfiguration'></a>
### _connectorConfiguration `constants`

##### Summary

The connector configuration.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-_connectorManager'></a>
### _connectorManager `constants`

##### Summary

The connector manager.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-_contentManagerActionProvider'></a>
### _contentManagerActionProvider `constants`

##### Summary

The content manager action provider.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-_contentManagerOptions'></a>
### _contentManagerOptions `constants`

##### Summary

The content manager options.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-_contentResult'></a>
### _contentResult `constants`

##### Summary

Outcome of the operation

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-_options'></a>
### _options `constants`

##### Summary

The options.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-_recordSubmissionOptions'></a>
### _recordSubmissionOptions `constants`

##### Summary

The record submission options.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-_submitTimespan'></a>
### _submitTimespan `constants`

##### Summary

How long it took to submit the work to the queue

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-_toggleProvider'></a>
### _toggleProvider `constants`

##### Summary

Feature Toggle Provider.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-_workQueueClient'></a>
### _workQueueClient `constants`

##### Summary

Work queue client.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-ServiceName'></a>
### ServiceName `property`

##### Summary

Gets the service name.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-WorkType'></a>
### WorkType `property`

##### Summary

Gets the work type.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-BeginAsync-RecordPoint-Connectors-SDK-Content-Channel,System-Threading-CancellationToken-'></a>
### BeginAsync(channel,cancellationToken) `method`

##### Summary

Begins and return a task of type contentresult asynchronously.

##### Returns

Task<ContentResult>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| channel | [RecordPoint.Connectors.SDK.Content.Channel](#T-RecordPoint-Connectors-SDK-Content-Channel 'RecordPoint.Connectors.SDK.Content.Channel') | The channel. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-ContinueSync-RecordPoint-Connectors-SDK-Content-Channel,System-String,System-Threading-CancellationToken-'></a>
### ContinueSync(channel,cursor,cancellationToken) `method`

##### Summary

Continue the sync.

##### Returns

Task<ContentResult>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| channel | [RecordPoint.Connectors.SDK.Content.Channel](#T-RecordPoint-Connectors-SDK-Content-Channel 'RecordPoint.Connectors.SDK.Content.Channel') | The channel. |
| cursor | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The cursor. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-DeserializeConfiguration-System-String,System-String-'></a>
### DeserializeConfiguration(configurationType,configurationText) `method`

##### Summary

Deserialize the configuration.

##### Returns

A ContentSynchronisationConfiguration

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| configurationType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The configuration type. |
| configurationText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The configuration text. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-DeserializeState-System-String,System-String-'></a>
### DeserializeState(stateType,stateText) `method`

##### Summary

Deserialize the state.

##### Returns

A ContentSynchronisationState

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| stateType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The state type. |
| stateText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The state text. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-FetchAsync-System-Threading-CancellationToken-'></a>
### FetchAsync(cancellationToken) `method`

##### Summary

Assuming that everything is setup correct, go and fetch new Content

##### Returns

Fetch content outcome

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-GetCustomKeyDimensions'></a>
### GetCustomKeyDimensions() `method`

##### Summary

Override custom dimensions key for ConnectorId, TenantId and TenantDomainName

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-GetCustomResultDimensions'></a>
### GetCustomResultDimensions() `method`

##### Summary

Get custom result dimensions.

##### Returns

A Dimensions

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-GetCustomResultMeasures'></a>
### GetCustomResultMeasures() `method`

##### Summary

Get custom result measures.

##### Returns

A Measures

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-GetInitialStartSyncTimeAsync-System-Threading-CancellationToken-'></a>
### GetInitialStartSyncTimeAsync() `method`

##### Summary

Get the start time for a new synchronisation operation, uses UtcNow if it cannot find the authorization time from the connector config

##### Returns

Sync start time

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-HandleAbandondedContentAsync-RecordPoint-Connectors-SDK-ContentManager-ContentResult,System-Threading-CancellationToken-'></a>
### HandleAbandondedContentAsync(contentResult,cancellationToken) `method`

##### Summary

Handle abandonded content asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| contentResult | [RecordPoint.Connectors.SDK.ContentManager.ContentResult](#T-RecordPoint-Connectors-SDK-ContentManager-ContentResult 'RecordPoint.Connectors.SDK.ContentManager.ContentResult') | The content result. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-HandleCompleteContentAsync-RecordPoint-Connectors-SDK-ContentManager-ContentResult,System-Threading-CancellationToken-'></a>
### HandleCompleteContentAsync(contentResult,cancellationToken) `method`

##### Summary

Handle complete content asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| contentResult | [RecordPoint.Connectors.SDK.ContentManager.ContentResult](#T-RecordPoint-Connectors-SDK-ContentManager-ContentResult 'RecordPoint.Connectors.SDK.ContentManager.ContentResult') | The content result. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-HandleFailedContentAsync-RecordPoint-Connectors-SDK-ContentManager-ContentResult,System-Threading-CancellationToken-'></a>
### HandleFailedContentAsync(contentResult,cancellationToken) `method`

##### Summary

Handle failed content asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| contentResult | [RecordPoint.Connectors.SDK.ContentManager.ContentResult](#T-RecordPoint-Connectors-SDK-ContentManager-ContentResult 'RecordPoint.Connectors.SDK.ContentManager.ContentResult') | The content result. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-HandleIncompleteContentAsync-RecordPoint-Connectors-SDK-ContentManager-ContentResult,System-Threading-CancellationToken-'></a>
### HandleIncompleteContentAsync(contentResult,cancellationToken) `method`

##### Summary

Handle incomplete content asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| contentResult | [RecordPoint.Connectors.SDK.ContentManager.ContentResult](#T-RecordPoint-Connectors-SDK-ContentManager-ContentResult 'RecordPoint.Connectors.SDK.ContentManager.ContentResult') | The content result. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-HandleSuccessfulContentAsync-RecordPoint-Connectors-SDK-ContentManager-ContentResult,System-Int32,System-Threading-CancellationToken-'></a>
### HandleSuccessfulContentAsync(contentResult,backOffSeconds,cancellationToken) `method`

##### Summary

Handle successful content asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| contentResult | [RecordPoint.Connectors.SDK.ContentManager.ContentResult](#T-RecordPoint-Connectors-SDK-ContentManager-ContentResult 'RecordPoint.Connectors.SDK.ContentManager.ContentResult') | The content result. |
| backOffSeconds | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The back off seconds. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-InnerDispose'></a>
### InnerDispose() `method`

##### Summary

Dispose invocation results

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-InnerRunAsync-System-Threading-CancellationToken-'></a>
### InnerRunAsync(cancellationToken) `method`

##### Summary

Inner the run asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [RecordPoint.Connectors.SDK.RequiredValueOutOfRangeException](#T-RecordPoint-Connectors-SDK-RequiredValueOutOfRangeException 'RecordPoint.Connectors.SDK.RequiredValueOutOfRangeException') |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperation-SerializeState-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationState-'></a>
### SerializeState(state) `method`

##### Summary

Serialize the state.

##### Returns

A (string, string)

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| state | [RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationState](#T-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationState 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationState') | The state. |

<a name='T-RecordPoint-Connectors-SDK-Context-ContextTelemetryExtensions'></a>
## ContextTelemetryExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Context

##### Summary

Telemetry extensions related to the context

<a name='M-RecordPoint-Connectors-SDK-Context-ContextTelemetryExtensions-BeginServiceScope-RecordPoint-Connectors-SDK-Observability-IObservabilityScope,System-String-'></a>
### BeginServiceScope(observabilityScope,serviceId) `method`

##### Summary

Begin service observability scope

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') |  |
| serviceId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Unique correlation ID of the service |

<a name='M-RecordPoint-Connectors-SDK-Context-ContextTelemetryExtensions-BeginSystemScope-RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Context-ISystemContext-'></a>
### BeginSystemScope(observabilityScope,systemContext) `method`

##### Summary

Begin system observability scope

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') |  |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') |  |

<a name='T-RecordPoint-Connectors-SDK-Health-DefaultHealthCheckLiveAction'></a>
## DefaultHealthCheckLiveAction `type`

##### Namespace

RecordPoint.Connectors.SDK.Health

##### Summary

Default Implementation for a HealthCheckLiveAction

<a name='M-RecordPoint-Connectors-SDK-Health-DefaultHealthCheckLiveAction-#ctor-RecordPoint-Connectors-SDK-Health-IHealthCheckManager-'></a>
### #ctor(healthCheckManager) `constructor`

##### Summary

Constructor

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| healthCheckManager | [RecordPoint.Connectors.SDK.Health.IHealthCheckManager](#T-RecordPoint-Connectors-SDK-Health-IHealthCheckManager 'RecordPoint.Connectors.SDK.Health.IHealthCheckManager') |  |

<a name='M-RecordPoint-Connectors-SDK-Health-DefaultHealthCheckLiveAction-CheckIsLiveAsync'></a>
### CheckIsLiveAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Health-DefaultHealthCheckReadyAction'></a>
## DefaultHealthCheckReadyAction `type`

##### Namespace

RecordPoint.Connectors.SDK.Health

##### Summary

Default Implementation for a HealthCheckReadyAction

<a name='M-RecordPoint-Connectors-SDK-Health-DefaultHealthCheckReadyAction-#ctor-RecordPoint-Connectors-SDK-Health-IHealthCheckManager-'></a>
### #ctor(healthCheckManager) `constructor`

##### Summary

Constructor

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| healthCheckManager | [RecordPoint.Connectors.SDK.Health.IHealthCheckManager](#T-RecordPoint-Connectors-SDK-Health-IHealthCheckManager 'RecordPoint.Connectors.SDK.Health.IHealthCheckManager') |  |

<a name='M-RecordPoint-Connectors-SDK-Health-DefaultHealthCheckReadyAction-CheckIsReadyAsync'></a>
### CheckIsReadyAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Toggles-Development-DictionaryToggleProvider'></a>
## DictionaryToggleProvider `type`

##### Namespace

RecordPoint.Connectors.SDK.Toggles.Development

##### Summary

Toggle provider that returns values set in the Toggles dictionary, otherwise just returns the default value.

##### Remarks

Intended for use in development

<a name='P-RecordPoint-Connectors-SDK-Toggles-Development-DictionaryToggleProvider-Toggles'></a>
### Toggles `property`

##### Summary

Gets the toggles.

<a name='M-RecordPoint-Connectors-SDK-Toggles-Development-DictionaryToggleProvider-GetToggleBool-System-String,System-Boolean-'></a>
### GetToggleBool(toggle,default) `method`

##### Summary

Get toggle bool.

##### Returns

A bool

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| toggle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The toggle. |
| default | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | If true, default. |

<a name='M-RecordPoint-Connectors-SDK-Toggles-Development-DictionaryToggleProvider-GetToggleBool-System-String,System-String,System-Boolean-'></a>
### GetToggleBool(toggle,userKey,default) `method`

##### Summary

Get toggle bool.

##### Returns

A bool

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| toggle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The toggle. |
| userKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The user key. |
| default | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | If true, default. |

<a name='M-RecordPoint-Connectors-SDK-Toggles-Development-DictionaryToggleProvider-GetToggleNumber-System-String,System-String,System-Int32-'></a>
### GetToggleNumber() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Toggles-Development-DictionaryToggleProvider-GetToggleNumber-System-String,System-Int32-'></a>
### GetToggleNumber() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Toggles-Development-DictionaryToggleProvider-GetToggleString-System-String,System-String,System-String-'></a>
### GetToggleString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Toggles-Development-DictionaryToggleProvider-GetToggleString-System-String,System-String-'></a>
### GetToggleString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Secrets-EncryptionExtensions'></a>
## EncryptionExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Secrets

##### Summary

Secret extension methods used to encrypt and decrypt secrets

<a name='M-RecordPoint-Connectors-SDK-Secrets-EncryptionExtensions-DecryptSecret-System-Byte[]-'></a>
### DecryptSecret(encryptedSecret) `method`

##### Summary

Decrypt a secret

##### Returns

Decryptes plain text

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| encryptedSecret | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | Previously encrypted secret |

<a name='M-RecordPoint-Connectors-SDK-Secrets-EncryptionExtensions-EncryptSecret-System-String-'></a>
### EncryptSecret(plainText) `method`

##### Summary

Encrypt a secret

##### Returns

Encrypted secrety

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| plainText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Secret in plain text |

<a name='M-RecordPoint-Connectors-SDK-Secrets-EncryptionExtensions-GetSecureSecret-System-String-'></a>
### GetSecureSecret(plainText) `method`

##### Summary

Convert plainText into a secure string

##### Returns

Secure text

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| plainText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Secret in plain text |

<a name='T-RecordPoint-Connectors-SDK-Context-EnvironmentExtensions'></a>
## EnvironmentExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Context

##### Summary

The environment extensions.

<a name='F-RecordPoint-Connectors-SDK-Context-EnvironmentExtensions-ASPNETCORE_ENVIRONMENT'></a>
### ASPNETCORE_ENVIRONMENT `constants`

##### Summary

The ASPNETCORE ENVIRONMENT.

<a name='F-RecordPoint-Connectors-SDK-Context-EnvironmentExtensions-DEVELOPMENT'></a>
### DEVELOPMENT `constants`

##### Summary

The DEVELOPMENT.

<a name='M-RecordPoint-Connectors-SDK-Context-EnvironmentExtensions-GetASPNetCoreEnvironmentVariable'></a>
### GetASPNetCoreEnvironmentVariable() `method`

##### Summary

Get ASP net core environment variable.

##### Returns

A string

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Context-EnvironmentExtensions-IsDevelopmentEnvironment-System-String-'></a>
### IsDevelopmentEnvironment(environment) `method`

##### Summary

Checks if is development environment.

##### Returns

A bool

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| environment | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The environment. |

<a name='M-RecordPoint-Connectors-SDK-Context-EnvironmentExtensions-IsDevelopmentEnvironment'></a>
### IsDevelopmentEnvironment() `method`

##### Summary

Checks if is development environment.

##### Returns

A bool

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Observability-ExceptionExtensions'></a>
## ExceptionExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability

##### Summary

Observability exception extensions

##### Remarks

Adds methods used to decorate exceptions with information used by the observability infrastructure

<a name='F-RecordPoint-Connectors-SDK-Observability-ExceptionExtensions-DIMENSIONS_PROPERTY'></a>
### DIMENSIONS_PROPERTY `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Observability-ExceptionExtensions-HAS_SCOPE_PROPERTY'></a>
### HAS_SCOPE_PROPERTY `constants`

##### Summary

Data properties

<a name='F-RecordPoint-Connectors-SDK-Observability-ExceptionExtensions-LOG_MESSAGE_PROPERTY'></a>
### LOG_MESSAGE_PROPERTY `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Observability-ExceptionExtensions-MEASURES_PROPERTY'></a>
### MEASURES_PROPERTY `constants`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Observability-ExceptionExtensions-EnsureLogMessage-System-Exception,System-String-'></a>
### EnsureLogMessage(ex,message) `method`

##### Summary

Set a log message if none is set, otherwise leave the existing value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| ex | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Exception to update |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Message to set |

<a name='M-RecordPoint-Connectors-SDK-Observability-ExceptionExtensions-GetDimensions-System-Exception-'></a>
### GetDimensions(ex) `method`

##### Summary

Get observability dimensions for an exception

##### Returns

Key properties, empty by default

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| ex | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') |  |

<a name='M-RecordPoint-Connectors-SDK-Observability-ExceptionExtensions-GetLogMessage-System-Exception-'></a>
### GetLogMessage(ex) `method`

##### Summary

Get the log message set for this exception

##### Returns

Log message, null if not set

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| ex | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Owning exception |

<a name='M-RecordPoint-Connectors-SDK-Observability-ExceptionExtensions-GetMeasures-System-Exception-'></a>
### GetMeasures(ex) `method`

##### Summary

Get observability measures for an exception

##### Returns

Key properties, empty by default

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| ex | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') |  |

<a name='M-RecordPoint-Connectors-SDK-Observability-ExceptionExtensions-HasScope-System-Exception-'></a>
### HasScope(ex) `method`

##### Summary

Was this exception associated with an observability scope

##### Returns

True if the  Key properties, empty by default

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| ex | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Exception to check |

<a name='M-RecordPoint-Connectors-SDK-Observability-ExceptionExtensions-ScopeTo-System-Exception,RecordPoint-Connectors-SDK-Observability-IObservabilityScope-'></a>
### ScopeTo(ex,observabilityScope) `method`

##### Summary

Scope this exception to a given scope tracker

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| ex | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Exception to update |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | Key properties to set |

##### Remarks

This method is used to attach information about the observability scope to the exception. This must be called within that
observability scope if the exception is not tracked within that observability scope.

The standard observability patterns ensure that this is done.

<a name='M-RecordPoint-Connectors-SDK-Observability-ExceptionExtensions-SetLogMessage-System-Exception,System-String-'></a>
### SetLogMessage(ex,message) `method`

##### Summary

Set the log message for this exception

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| ex | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Exception to update |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Message to set |

<a name='T-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-FeatureToggleModel'></a>
## FeatureToggleModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles

##### Summary

This is to serve as a model for feature toggles. So this will store boolean values for each feature toggle and user keys for each toggle.
The feature toggles will only have a boolean value as this class has limited scope.

<a name='M-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-FeatureToggleModel-#ctor'></a>
### #ctor() `constructor`

##### Summary

This is to serve as a model for feature toggles. So this will store boolean values for each feature toggle and user keys for each toggle.
The feature toggles will only have a boolean value as this class has limited scope.

##### Parameters

This constructor has no parameters.

<a name='P-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-FeatureToggleModel-UserKeyOverrides'></a>
### UserKeyOverrides `property`

##### Summary

This contains the UserKeys that have an override for this toggle.

<a name='P-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-FeatureToggleModel-Value'></a>
### Value `property`

##### Summary

This is the value of the toggle.

<a name='T-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-FileReader'></a>
## FileReader `type`

##### Namespace

RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles

##### Summary

Default implementation of the IFileReader interface.

<a name='M-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-FileReader-ReadAllText-System-String-'></a>
### ReadAllText(path) `method`

##### Summary

Gets all the text from a file.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-RecordPoint-Connectors-SDK-Health-HealthCheckBuilderExtensions'></a>
## HealthCheckBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Health

##### Summary

Health check builder extensions

<a name='M-RecordPoint-Connectors-SDK-Health-HealthCheckBuilderExtensions-UseHealthChecker-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseHealthChecker(hostBuilder) `method`

##### Summary

Use the health checker with default health check actions.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | Host builder to target |

<a name='M-RecordPoint-Connectors-SDK-Health-HealthCheckBuilderExtensions-UseHealthChecker``2-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseHealthChecker\`\`2(hostBuilder) `method`

##### Summary

Use the health checker with provided health check actions.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | Host builder to target |

<a name='T-RecordPoint-Connectors-SDK-Health-HealthCheckManager'></a>
## HealthCheckManager `type`

##### Namespace

RecordPoint.Connectors.SDK.Health

##### Summary

Main front end for the health check component

<a name='M-RecordPoint-Connectors-SDK-Health-HealthCheckManager-#ctor-System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Health-IHealthCheckStrategy},RecordPoint-Connectors-SDK-Providers-IDateTimeProvider-'></a>
### #ctor(healthCheckStrategies,dateTimeProvider) `constructor`

##### Summary

Constructor

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| healthCheckStrategies | [System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Health.IHealthCheckStrategy}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Health.IHealthCheckStrategy}') |  |
| dateTimeProvider | [RecordPoint.Connectors.SDK.Providers.IDateTimeProvider](#T-RecordPoint-Connectors-SDK-Providers-IDateTimeProvider 'RecordPoint.Connectors.SDK.Providers.IDateTimeProvider') |  |

<a name='F-RecordPoint-Connectors-SDK-Health-HealthCheckManager-HEALTH_CHECK_FAULT_NAME'></a>
### HEALTH_CHECK_FAULT_NAME `constants`

##### Summary

Health Check Fault dimension key

<a name='P-RecordPoint-Connectors-SDK-Health-HealthCheckManager-HealthCheckResult'></a>
### HealthCheckResult `property`

##### Summary

*Inherit from parent.*

<a name='M-RecordPoint-Connectors-SDK-Health-HealthCheckManager-RunHealthCheckAsync-System-Threading-CancellationToken-'></a>
### RunHealthCheckAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Health-HealthCheckService'></a>
## HealthCheckService `type`

##### Namespace

RecordPoint.Connectors.SDK.Health

##### Summary

The health check service.

<a name='M-RecordPoint-Connectors-SDK-Health-HealthCheckService-#ctor-RecordPoint-Connectors-SDK-Health-IHealthCheckManager,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Health-HealthCheckOptions}-'></a>
### #ctor(healthCheckManager,systemContext,observabilityScope,healthCheckOptions) `constructor`

##### Summary

Initializes a new instance of the [HealthCheckService](#T-RecordPoint-Connectors-SDK-Health-HealthCheckService 'RecordPoint.Connectors.SDK.Health.HealthCheckService') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| healthCheckManager | [RecordPoint.Connectors.SDK.Health.IHealthCheckManager](#T-RecordPoint-Connectors-SDK-Health-IHealthCheckManager 'RecordPoint.Connectors.SDK.Health.IHealthCheckManager') | The health check manager. |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | The scope manager. |
| healthCheckOptions | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Health.HealthCheckOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Health-HealthCheckOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Health.HealthCheckOptions}') | The health check options. |

<a name='F-RecordPoint-Connectors-SDK-Health-HealthCheckService-_healthCheckManager'></a>
### _healthCheckManager `constants`

##### Summary

The health check manager.

<a name='F-RecordPoint-Connectors-SDK-Health-HealthCheckService-_healthCheckOptions'></a>
### _healthCheckOptions `constants`

##### Summary

The health check options.

<a name='F-RecordPoint-Connectors-SDK-Health-HealthCheckService-_observabilityScope'></a>
### _observabilityScope `constants`

##### Summary

The scope manager.

<a name='F-RecordPoint-Connectors-SDK-Health-HealthCheckService-_systemContext'></a>
### _systemContext `constants`

##### Summary

The system context.

<a name='P-RecordPoint-Connectors-SDK-Health-HealthCheckService-CheckDelay'></a>
### CheckDelay `property`

##### Summary

Gets the check delay.

<a name='P-RecordPoint-Connectors-SDK-Health-HealthCheckService-StartDelay'></a>
### StartDelay `property`

##### Summary

Gets the start delay.

<a name='M-RecordPoint-Connectors-SDK-Health-HealthCheckService-ExecuteAsync-System-Threading-CancellationToken-'></a>
### ExecuteAsync(stoppingToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| stoppingToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='T-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-IFileReader'></a>
## IFileReader `type`

##### Namespace

RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles

##### Summary

Interface for file reading, useful for dependency injection and testing.

<a name='M-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-IFileReader-ReadAllText-System-String-'></a>
### ReadAllText(path) `method`

##### Summary

Reads all text from a file.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| path | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-RecordPoint-Connectors-SDK-Observability-ISystemContextExtensions'></a>
## ISystemContextExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability

##### Summary

Extension methods for [ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext').

<a name='M-RecordPoint-Connectors-SDK-Observability-ISystemContextExtensions-GetDimensions-RecordPoint-Connectors-SDK-Context-ISystemContext-'></a>
### GetDimensions(systemContext) `method`

##### Summary

Gets standard dimensions from the system context

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') |  |

<a name='T-RecordPoint-Connectors-SDK-Observability-AppInsights-ITelemetryClientFactory'></a>
## ITelemetryClientFactory `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability.AppInsights

##### Summary

Contract for a factory to create an instance of [TelemetryClient](#T-Microsoft-ApplicationInsights-TelemetryClient 'Microsoft.ApplicationInsights.TelemetryClient').

<a name='M-RecordPoint-Connectors-SDK-Observability-AppInsights-ITelemetryClientFactory-GetTelemetryClient'></a>
### GetTelemetryClient() `method`

##### Summary

Creates an instance of [TelemetryClient](#T-Microsoft-ApplicationInsights-TelemetryClient 'Microsoft.ApplicationInsights.TelemetryClient') based on the configuration provided in [ApplicationInsightOptions](#T-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightOptions 'RecordPoint.Connectors.SDK.Observability.AppInsights.ApplicationInsightOptions').

##### Returns

A new instance of an Application Insights [TelemetryClient](#T-Microsoft-ApplicationInsights-TelemetryClient 'Microsoft.ApplicationInsights.TelemetryClient')/

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Observability-ITelemetryTrackerExtensions'></a>
## ITelemetryTrackerExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability

##### Summary

Extension methods for the ITelemetryTracker interface

<a name='M-RecordPoint-Connectors-SDK-Observability-ITelemetryTrackerExtensions-CreateRootServiceScope-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures-'></a>
### CreateRootServiceScope(telemetryTracker,systemContext,dimensions,measures) `method`

##### Summary

Returns an observability scope containing default dimensions from the system context

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The Telemetry Tracker |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The current system context |
| dimensions | [RecordPoint.Connectors.SDK.Observability.Dimensions](#T-RecordPoint-Connectors-SDK-Observability-Dimensions 'RecordPoint.Connectors.SDK.Observability.Dimensions') |  |
| measures | [RecordPoint.Connectors.SDK.Observability.Measures](#T-RecordPoint-Connectors-SDK-Observability-Measures 'RecordPoint.Connectors.SDK.Observability.Measures') |  |

<a name='T-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunAgentService'></a>
## LightrunAgentService `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability.Lightrun

##### Summary

Background Host used to start the Lightrun Agent within a console host

<a name='M-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunAgentService-#ctor-RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunOptions}-'></a>
### #ctor() `constructor`

##### Summary

Background Host used to start the Lightrun Agent within a console host

##### Parameters

This constructor has no parameters.

<a name='P-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunAgentService-Enabled'></a>
### Enabled `property`

##### Summary

Is light run enabled?

##### Remarks

Lightrun is enabled only if the Server URL and Secret is provided

<a name='M-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunAgentService-ExecuteAsync-System-Threading-CancellationToken-'></a>
### ExecuteAsync() `method`

##### Summary

Starts the underlying lightrun agent if enabled

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunAgentService-GetAgentOptions'></a>
### GetAgentOptions() `method`

##### Summary

Get the options needed for the underlying Lightrun Agent

##### Returns

Lightrun agent options

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunDynamicLogger'></a>
## LightrunDynamicLogger `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability.Lightrun

##### Summary

Implementation of a Dynmaic Logger for Lightrun using the internal Telemetry Tracker

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| telemetryTracker | [T:RecordPoint.Connectors.SDK.Observability.Lightrun.LightrunDynamicLogger](#T-T-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunDynamicLogger 'T:RecordPoint.Connectors.SDK.Observability.Lightrun.LightrunDynamicLogger') |  |

<a name='M-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunDynamicLogger-#ctor-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker-'></a>
### #ctor(telemetryTracker) `constructor`

##### Summary

Implementation of a Dynmaic Logger for Lightrun using the internal Telemetry Tracker

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') |  |

<a name='M-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunDynamicLogger-Log-Lightrun-Agent-Logging-LogEntry-'></a>
### Log(entry) `method`

##### Summary

Send Lightrun logs to the Telemetry Tracker

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| entry | [Lightrun.Agent.Logging.LogEntry](#T-Lightrun-Agent-Logging-LogEntry 'Lightrun.Agent.Logging.LogEntry') |  |

<a name='T-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunHostBuilderHelperExtensions'></a>
## LightrunHostBuilderHelperExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability.Lightrun

##### Summary

Host Builder Extensions for registering the Lightrun Agent

<a name='M-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunHostBuilderHelperExtensions-UseLightrunAgent-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseLightrunAgent() `method`

##### Summary

Registers the Lightrun Agent

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunOptions'></a>
## LightrunOptions `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability.Lightrun

##### Summary

Configurable Options for Lightrun

<a name='F-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunOptions-SECTION_NAME'></a>
### SECTION_NAME `constants`

##### Summary

Configuration Section Name

<a name='P-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunOptions-AgentLogTargetDir'></a>
### AgentLogTargetDir `property`

##### Summary

The directory where the agent will store its logs. Default: Use Lightrun Default

<a name='P-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunOptions-CertificatePinningEnabled'></a>
### CertificatePinningEnabled `property`

##### Summary

Whether to enable Certificate Pinning. Default: FALSE
Overidden default setting as we don't require this against internal LR servers

<a name='P-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunOptions-DisplayName'></a>
### DisplayName `property`

##### Summary

Agent display name. DEFAULT: hostname of the machine running the application and the application's process identifier

<a name='P-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunOptions-MaxCollectionSize'></a>
### MaxCollectionSize `property`

##### Summary

Maximum items within a collection to serialize. Default: 10

<a name='P-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunOptions-MaxDepthToSerialize'></a>
### MaxDepthToSerialize `property`

##### Summary

Maximum depth of objects to serialize. Default: 5

<a name='P-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunOptions-MaxFieldCount'></a>
### MaxFieldCount `property`

##### Summary

Maximum number of fields to serialize. Default: 100

<a name='P-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunOptions-MaxStringLength'></a>
### MaxStringLength `property`

##### Summary

Maximum length of strings to serialize. Default: 256

<a name='P-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunOptions-Secret'></a>
### Secret `property`

##### Summary

Your organization's Lightrun secret key.

<a name='P-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunOptions-ServerUrl'></a>
### ServerUrl `property`

##### Summary

Lightrun server URL.

##### Remarks

Lightrun is disabled if not provided

<a name='P-RecordPoint-Connectors-SDK-Observability-Lightrun-LightrunOptions-Tags'></a>
### Tags `property`

##### Summary

The list of tags assigned to the agent.

<a name='T-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFeatureToggleHostBuilderExtensions'></a>
## LocalFeatureToggleHostBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles

##### Summary

This file just contains the extension method to add the LocalFileToggleProvider to the host builder.

<a name='M-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFeatureToggleHostBuilderExtensions-UseLocalFileToggleProvider-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseLocalFileToggleProvider(hostBuilder) `method`

##### Summary

Use the LocalFileToggleProvider as the IToggleProvider implementation. i.e. Use a local json file to determine the value of toggles.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') |  |

<a name='T-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFeatureToggleOptions'></a>
## LocalFeatureToggleOptions `type`

##### Namespace

RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles

##### Summary

Options that allows for the configuration of the LocalFeatureToggleProvider

<a name='F-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFeatureToggleOptions-SECTION_NAME'></a>
### SECTION_NAME `constants`

##### Summary

Configuration section name

<a name='P-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFeatureToggleOptions-JsonFilePath'></a>
### JsonFilePath `property`

##### Summary

Path to the json file containing the feature toggles

<a name='T-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFileToggleProvider'></a>
## LocalFileToggleProvider `type`

##### Namespace

RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles

##### Summary

This is a toggle provider that reads the value of toggles from a local json file.

<a name='M-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFileToggleProvider-#ctor-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFeatureToggleOptions},RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-IFileReader-'></a>
### #ctor(options,fileReader) `constructor`

##### Summary

Initialises the LocalFileToggleProvider, reads the json file and stores the values in a dictionary.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| options | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles.LocalFeatureToggleOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFeatureToggleOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles.LocalFeatureToggleOptions}') |  |
| fileReader | [RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles.IFileReader](#T-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-IFileReader 'RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles.IFileReader') |  |

<a name='M-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFileToggleProvider-GetToggleBool-System-String,System-Boolean-'></a>
### GetToggleBool(toggle,default) `method`

##### Summary

Gets the toggle value from the loaded dictionary, if it doesn't exist it returns the default value.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| toggle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| default | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |

<a name='M-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFileToggleProvider-GetToggleBool-System-String,System-String,System-Boolean-'></a>
### GetToggleBool(toggle,userKey,default) `method`

##### Summary

Gets the toggle value from the loaded dictionary, also checks for user overrides, if it doesn't exist it returns the default value.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| toggle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| userKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| default | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |

<a name='M-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFileToggleProvider-GetToggleNumber-System-String,System-String,System-Int32-'></a>
### GetToggleNumber() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFileToggleProvider-GetToggleNumber-System-String,System-Int32-'></a>
### GetToggleNumber() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFileToggleProvider-GetToggleString-System-String,System-String,System-String-'></a>
### GetToggleString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFileToggleProvider-GetToggleString-System-String,System-String-'></a>
### GetToggleString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Toggles-Development-LocalJsonToggles-LocalFileToggleProvider-GetTogglesDictionaryFromJson'></a>
### GetTogglesDictionaryFromJson() `method`

##### Summary

Tries to load the json file if it fails it gets the loaded dictionary, this allows for the file to be updated without restarting the service.

##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2'></a>
## ManagedQueueableWorkBase\`2 `type`

##### Namespace

RecordPoint.Connectors.SDK.Work

##### Summary

The managed queueable work base.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TConfiguration |  |
| TState |  |

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-Work-IManagedWorkFactory,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider-'></a>
### #ctor(serviceProvider,managedWorkFactory,systemContext,observabilityScope,telemetryTracker,dateTimeProvider) `constructor`

##### Summary

Initializes a new instance of the class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| serviceProvider | [System.IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider') | The service provider. |
| managedWorkFactory | [RecordPoint.Connectors.SDK.Work.IManagedWorkFactory](#T-RecordPoint-Connectors-SDK-Work-IManagedWorkFactory 'RecordPoint.Connectors.SDK.Work.IManagedWorkFactory') | The managed work factory. |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | The scope manager. |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The telemetry tracker. |
| dateTimeProvider | [RecordPoint.Connectors.SDK.Providers.IDateTimeProvider](#T-RecordPoint-Connectors-SDK-Providers-IDateTimeProvider 'RecordPoint.Connectors.SDK.Providers.IDateTimeProvider') | The date time provider. |

<a name='F-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-SEMAPHORE_CONNECTOR_CONFIGURATION'></a>
### SEMAPHORE_CONNECTOR_CONFIGURATION `constants`

##### Summary

The SEMAPHORE CONNECTOR CONFIGURATION.

<a name='F-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-SEMAPHORE_GLOBAL'></a>
### SEMAPHORE_GLOBAL `constants`

##### Summary

The SEMAPHORE GLOBAL.

<a name='F-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-_managedWorkFactory'></a>
### _managedWorkFactory `constants`

##### Summary

The managed work factory.

<a name='P-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-Configuration'></a>
### Configuration `property`

##### Summary

Work configuration. Effectively the unchanging "input parameters" for the work.

##### Remarks

The Work configuration cannot change over time.

<a name='P-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-State'></a>
### State `property`

##### Summary

State of the Work when started

##### Remarks

State is the context for work that has continued execution and is intended to record the ongoing progress between executions.
It can be used to record information such as cursors, or tasks that have already been completed etc.

<a name='P-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-WorkManager'></a>
### WorkManager `property`

##### Summary

Managed Work that we are running

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-AbandonedAsync-System-String,System-Threading-CancellationToken-'></a>
### AbandonedAsync(reason,cancellationToken) `method`

##### Summary

Record that the long-running job has completed and no more attempts should be made
to run it

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| reason | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Reason why the work item is complete |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

##### Remarks

Reason why the work item is complete

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-CalculateBackOffSeconds-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperationOptionsBase,System-Boolean,System-Int32-'></a>
### CalculateBackOffSeconds(settings,hasResult,lastDelaySeconds) `method`

##### Summary

Calculate back off seconds.

##### Returns

An int

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| settings | [RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperationOptionsBase](#T-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperationOptionsBase 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperationOptionsBase') | The settings. |
| hasResult | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | If true, has result. |
| lastDelaySeconds | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The last delay seconds. |

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-CheckConnectorEnabledStatusAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperationOptionsBase,RecordPoint-Connectors-SDK-Abstractions-ContentManager-IContentDiscoveryState,System-Int32,System-Threading-CancellationToken-'></a>
### CheckConnectorEnabledStatusAsync(connectorConfiguration,options,maxDisabledConnectorAge,state,cancellationToken) `method`

##### Summary

Determines if a connector is enabled and if not, back off until it is re-enabled or exceeds the threshold

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfiguration | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | The Connector Configuration |
| options | [RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperationOptionsBase](#T-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperationOptionsBase 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperationOptionsBase') | Configured options for the work operation |
| maxDisabledConnectorAge | [RecordPoint.Connectors.SDK.Abstractions.ContentManager.IContentDiscoveryState](#T-RecordPoint-Connectors-SDK-Abstractions-ContentManager-IContentDiscoveryState 'RecordPoint.Connectors.SDK.Abstractions.ContentManager.IContentDiscoveryState') | The maximum time in seconds a Connector can be disabled before being abandoned |
| state | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The state object of the work operation |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-CompleteAsync-System-String,System-Threading-CancellationToken-'></a>
### CompleteAsync(reason,cancellationToken) `method`

##### Summary

Record that the long-running job has completed and no more attempts should be made
to run it

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| reason | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Reason why the work item is complete |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

##### Remarks

Reason why the work item is complete

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-ContinueAsync-System-String,`1,System-DateTimeOffset,System-Threading-CancellationToken-'></a>
### ContinueAsync(reason,state,waitTill,cancellationToken) `method`

##### Summary

Record that the long-running job should continue working

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| reason | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Reason why the job is continuing |
| state | [\`1](#T-`1 '`1') | State to use for the next run |
| waitTill | [System.DateTimeOffset](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTimeOffset 'System.DateTimeOffset') | Time to wait for the next run |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-DeserializeConfiguration-System-String,System-String-'></a>
### DeserializeConfiguration(configurationType,configurationText) `method`

##### Summary

Required override that deserializes the configuration from the work request

##### Returns

Deserialized parameters

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| configurationType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | String that describes how the configuration is structured. Mainly required for supporting upgrade scenarios. |
| configurationText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Text that needs to be deserialized |

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-DeserializeState-System-String,System-String-'></a>
### DeserializeState(stateType,stateText) `method`

##### Summary

Override that deserializes the state from the job

##### Returns

Deserialized parameters

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| stateType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | String that describes how the state is structured. Mainly required for supporting upgrade scenarios |
| stateText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | State text that needs to be serialized |

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-FailAsync-System-String,System-Exception,System-Threading-CancellationToken-'></a>
### FailAsync(reason,exception,cancellationToken) `method`

##### Summary

Set that the job has permanently failed

##### Returns

Outcome to pass onto the work queue

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| reason | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Reason why the job has failed |
| exception | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Optional cause of the failure |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-FaultedAsync-System-String,System-Exception,System-Threading-CancellationToken-'></a>
### FaultedAsync(reason,exception,cancellationToken) `method`

##### Summary

Set that the job has had a fault and work has not advanced
but the job has not critically failed and should continue

##### Returns

Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| reason | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Reason why the job has faulted |
| exception | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Optional cause of the fault |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-GetCoreKeyDimensions'></a>
### GetCoreKeyDimensions() `method`

##### Summary

Get the core key dimensions for the work item

##### Returns

Key dimensions that will be included in the work items observability scope

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-InnerDispose'></a>
### InnerDispose() `method`

##### Summary

Dispose of resources

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-RunWorkRequestAsync-RecordPoint-Connectors-SDK-Work-WorkRequest,System-Threading-CancellationToken-'></a>
### RunWorkRequestAsync(workRequest,cancellationToken) `method`

##### Summary

Execute the work request

##### Returns

Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workRequest | [RecordPoint.Connectors.SDK.Work.WorkRequest](#T-RecordPoint-Connectors-SDK-Work-WorkRequest 'RecordPoint.Connectors.SDK.Work.WorkRequest') | Work request that defines the work to execute |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-SerializeState-`1-'></a>
### SerializeState() `method`

##### Summary

Override that serialized the state

##### Returns

StateType, State text tuple

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedQueueableWorkBase`2-SetOutcome-RecordPoint-Connectors-SDK-Work-WorkResult-'></a>
### SetOutcome(workOutcome) `method`

##### Summary

Set internal outcome from an externally provided outcome

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workOutcome | [RecordPoint.Connectors.SDK.Work.WorkResult](#T-RecordPoint-Connectors-SDK-Work-WorkResult 'RecordPoint.Connectors.SDK.Work.WorkResult') | Provided outcome |

<a name='T-RecordPoint-Connectors-SDK-Work-ManagedWorkBuilderExtensions'></a>
## ManagedWorkBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Work

##### Summary

Job builder extensions

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedWorkBuilderExtensions-AddWorkStateManagement``1-Microsoft-Extensions-DependencyInjection-IServiceCollection-'></a>
### AddWorkStateManagement\`\`1(services) `method`

##### Summary

Add the standard database backed job component

##### Returns

Updated services

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| services | [Microsoft.Extensions.DependencyInjection.IServiceCollection](#T-Microsoft-Extensions-DependencyInjection-IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection') | Services to extend |

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedWorkBuilderExtensions-UseWorkStateManager``1-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseWorkStateManager\`\`1(hostBuilder) `method`

##### Summary

Use the Managed Work Status Manager

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | Host builder to target |

<a name='T-RecordPoint-Connectors-SDK-Work-ManagedWorkFactory'></a>
## ManagedWorkFactory `type`

##### Namespace

RecordPoint.Connectors.SDK.Work

##### Summary

Implemtation of a Managed Work Factory

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedWorkFactory-#ctor-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Connectors-ConnectorOptions},RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager,RecordPoint-Connectors-SDK-Work-IWorkQueueClient-'></a>
### #ctor() `constructor`

##### Summary

Implemtation of a Managed Work Factory

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedWorkFactory-CreateWork-System-String,System-String,System-String,System-String,System-String-'></a>
### CreateWork() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedWorkFactory-LoadWork-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-'></a>
### LoadWork() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Work-ManagedWorkManager'></a>
## ManagedWorkManager `type`

##### Namespace

RecordPoint.Connectors.SDK.Work

##### Summary

Manages the state of Managed Work

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedWorkManager-#ctor-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager,RecordPoint-Connectors-SDK-Work-IWorkQueueClient-'></a>
### #ctor(managedWorkStatusManager,workQueueClient) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| managedWorkStatusManager | [RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager](#T-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager 'RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager') |  |
| workQueueClient | [RecordPoint.Connectors.SDK.Work.IWorkQueueClient](#T-RecordPoint-Connectors-SDK-Work-IWorkQueueClient 'RecordPoint.Connectors.SDK.Work.IWorkQueueClient') |  |

<a name='P-RecordPoint-Connectors-SDK-Work-ManagedWorkManager-WorkStatus'></a>
### WorkStatus `property`

##### Summary

Status for this Managed Work

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedWorkManager-AbandonedAsync-System-String,System-Threading-CancellationToken-'></a>
### AbandonedAsync(reason,cancellationToken) `method`

##### Summary

Abandon the work

##### Returns

Outcome to pass onto the work queue

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| reason | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The reason the Work is being abandonded |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedWorkManager-CheckAsync-System-Threading-CancellationToken-'></a>
### CheckAsync(cancellationToken) `method`

##### Summary

Check that work is still valid prior to performing the work

##### Returns

The result to pass to the work queue if the work has failed its check, otherwise null.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedWorkManager-CompleteAsync-System-String,System-Threading-CancellationToken-'></a>
### CompleteAsync(reason,cancellationToken) `method`

##### Summary

Complete the work

##### Returns

Outcome to pass onto the work queue

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| reason | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The reason the Work is being completed |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedWorkManager-ContinueAsync-System-String,System-String,System-DateTimeOffset,System-Threading-CancellationToken-'></a>
### ContinueAsync(stateType,state,waitTill,cancellationToken) `method`

##### Summary

Continue the Work

##### Returns

Outcome to pass onto the work queue

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| stateType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | String that identifies the type of the state that was saved |
| state | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Current progress state |
| waitTill | [System.DateTimeOffset](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTimeOffset 'System.DateTimeOffset') | UTC time to wait till before continuing |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedWorkManager-CreateWorkStatus'></a>
### CreateWorkStatus() `method`

##### Summary

Create Work status from a Work message

##### Returns

Managed Work status model

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedWorkManager-Dispose-System-Boolean-'></a>
### Dispose(disposing) `method`

##### Summary

Free managed resources

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| disposing | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedWorkManager-Dispose'></a>
### Dispose() `method`

##### Summary

Dispose of the Managed Work Manager

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedWorkManager-FailedAsync-System-String,System-Threading-CancellationToken-'></a>
### FailedAsync(reason,cancellationToken) `method`

##### Summary

Set that the Work has permanently failed

##### Returns

Outcome to pass onto the work queue

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| reason | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Reason why the Work has failed |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedWorkManager-FaultyAsync-System-String,System-Exception,System-Threading-CancellationToken,System-Nullable{System-Int32}-'></a>
### FaultyAsync(reason,exception,cancellationToken,faultedCount) `method`

##### Summary

Set that the Work has had a possibly transient fault

##### Returns

Outcome to pass onto the work queue

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| reason | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Reason why |
| exception | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Exception |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |
| faultedCount | [System.Nullable{System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int32}') | faulted count |

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedWorkManager-RetryAsync-System-DateTimeOffset,System-Threading-CancellationToken,System-Nullable{System-Int32}-'></a>
### RetryAsync(waitTill,cancellationToken,faultedCount) `method`

##### Summary

Retry the Work with the same content

##### Returns

Outcome to pass onto the work queue

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| waitTill | [System.DateTimeOffset](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTimeOffset 'System.DateTimeOffset') | UTC time to wait till before continuing |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |
| faultedCount | [System.Nullable{System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int32}') | number of faulty to control retries |

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedWorkManager-StartAsync-System-Threading-CancellationToken,System-Nullable{System-DateTimeOffset}-'></a>
### StartAsync(cancellationToken,waitTill) `method`

##### Summary

Start the work running

##### Returns

Start Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |
| waitTill | [System.Nullable{System.DateTimeOffset}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.DateTimeOffset}') |  |

<a name='T-RecordPoint-Connectors-SDK-ContentManager-ManagedWorkManagerExtensions'></a>
## ManagedWorkManagerExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Content job extensions

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ManagedWorkManagerExtensions-CreateChannelDiscoveryOperation-RecordPoint-Connectors-SDK-Work-IManagedWorkFactory,RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-'></a>
### CreateChannelDiscoveryOperation(managedWorkFactory,connectorConfigModel) `method`

##### Summary

Create a new Channel Discovery Operation

##### Returns

Newly created Channel Discovery Operation

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| managedWorkFactory | [RecordPoint.Connectors.SDK.Work.IManagedWorkFactory](#T-RecordPoint-Connectors-SDK-Work-IManagedWorkFactory 'RecordPoint.Connectors.SDK.Work.IManagedWorkFactory') |  |
| connectorConfigModel | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | Connector configuration |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ManagedWorkManagerExtensions-CreateContentManagerOperation-RecordPoint-Connectors-SDK-Work-IManagedWorkFactory-'></a>
### CreateContentManagerOperation(managedWorkFactory) `method`

##### Summary

Create a new Content Manager Operation

##### Returns

Newly created Content Manager Operation

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| managedWorkFactory | [RecordPoint.Connectors.SDK.Work.IManagedWorkFactory](#T-RecordPoint-Connectors-SDK-Work-IManagedWorkFactory 'RecordPoint.Connectors.SDK.Work.IManagedWorkFactory') |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ManagedWorkManagerExtensions-CreateContentRegistrationOperation-RecordPoint-Connectors-SDK-Work-IManagedWorkFactory,RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Channel,System-Collections-Generic-Dictionary{System-String,System-String}-'></a>
### CreateContentRegistrationOperation(managedWorkFactory,connectorConfigModel,channel,context) `method`

##### Summary

Create a new Content Registration Operation

##### Returns

Newly created Content Registration Operation

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| managedWorkFactory | [RecordPoint.Connectors.SDK.Work.IManagedWorkFactory](#T-RecordPoint-Connectors-SDK-Work-IManagedWorkFactory 'RecordPoint.Connectors.SDK.Work.IManagedWorkFactory') |  |
| connectorConfigModel | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | Connector configuration |
| channel | [RecordPoint.Connectors.SDK.Content.Channel](#T-RecordPoint-Connectors-SDK-Content-Channel 'RecordPoint.Connectors.SDK.Content.Channel') | Channel on which to perform the content synchronisation |
| context | [System.Collections.Generic.Dictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.String}') | Custom Context for the Content Registration |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ManagedWorkManagerExtensions-CreateContentSynchronisationOperation-RecordPoint-Connectors-SDK-Work-IManagedWorkFactory,RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Channel-'></a>
### CreateContentSynchronisationOperation(managedWorkFactory,connectorConfigModel,channel) `method`

##### Summary

Create a new Content Synchronisation Operation

##### Returns

Newly created Content Synchronisation Operation

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| managedWorkFactory | [RecordPoint.Connectors.SDK.Work.IManagedWorkFactory](#T-RecordPoint-Connectors-SDK-Work-IManagedWorkFactory 'RecordPoint.Connectors.SDK.Work.IManagedWorkFactory') |  |
| connectorConfigModel | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | Connector configuration |
| channel | [RecordPoint.Connectors.SDK.Content.Channel](#T-RecordPoint-Connectors-SDK-Content-Channel 'RecordPoint.Connectors.SDK.Content.Channel') | Channel on which to perform the content synchronisation |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ManagedWorkManagerExtensions-DeserialiseChannelDiscoveryConfiguration-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-'></a>
### DeserialiseChannelDiscoveryConfiguration(workStatus) `method`

##### Summary

Desrialises the Channel Discovery Configuration from the Managed Work Status

##### Returns

Configuration for the Channel Discovery

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workStatus | [RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel](#T-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel') | Status of the Work Operation |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ManagedWorkManagerExtensions-DeserialiseContentManagerConfiguration-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-'></a>
### DeserialiseContentManagerConfiguration(workStatus) `method`

##### Summary

Desrialises the Content Manager Configuration from the Managed Work Status

##### Returns

Configuration for the Channel Discovery

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workStatus | [RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel](#T-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel') | Status of the Work Operation |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ManagedWorkManagerExtensions-DeserialiseContentRegistrationConfiguration-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-'></a>
### DeserialiseContentRegistrationConfiguration(workStatus) `method`

##### Summary

Desrialises the Content Registration Configuration from the Managed Work Status

##### Returns

Configuration for the Content Registration

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workStatus | [RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel](#T-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel') | Status of the Work Operation |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ManagedWorkManagerExtensions-DeserialiseContentSynchronisationConfiguration-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-'></a>
### DeserialiseContentSynchronisationConfiguration(workStatus) `method`

##### Summary

Desrialises the Content Synchronisation Configuration from the Managed Work Status

##### Returns

Configuration for the Content Synchronisation

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workStatus | [RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel](#T-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel') | Status of the Work Operation |

<a name='T-RecordPoint-Connectors-SDK-ContentManager-NullChannelDiscoveryAction'></a>
## NullChannelDiscoveryAction `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

The null channel discovery action.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-NullChannelDiscoveryAction-#ctor-RecordPoint-Connectors-SDK-Content-IChannelManager-'></a>
### #ctor(channelManager) `constructor`

##### Summary

Initializes a new instance of the [NullChannelDiscoveryAction](#T-RecordPoint-Connectors-SDK-ContentManager-NullChannelDiscoveryAction 'RecordPoint.Connectors.SDK.ContentManager.NullChannelDiscoveryAction') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| channelManager | [RecordPoint.Connectors.SDK.Content.IChannelManager](#T-RecordPoint-Connectors-SDK-Content-IChannelManager 'RecordPoint.Connectors.SDK.Content.IChannelManager') | The channel manager. |

<a name='F-RecordPoint-Connectors-SDK-ContentManager-NullChannelDiscoveryAction-_channelManager'></a>
### _channelManager `constants`

##### Summary

The channel manager.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-NullChannelDiscoveryAction-ExecuteAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,System-Threading-CancellationToken,System-String-'></a>
### ExecuteAsync(connectorConfiguration,cancellationToken,cursor) `method`

##### Summary

Execute and return a task of type channeldiscoveryresult asynchronously.

##### Returns

Task<ChannelDiscoveryResult>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfiguration | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | The connector configuration. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |
| cursor | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Scan cursor provided by a prior batch discovery operation (though not applicable here). |

<a name='T-RecordPoint-Connectors-SDK-Observability-Null-NullObservabilityScope'></a>
## NullObservabilityScope `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability.Null

##### Summary

The null observability scope.

<a name='M-RecordPoint-Connectors-SDK-Observability-Null-NullObservabilityScope-Dispose-System-Boolean-'></a>
### Dispose(disposing) `method`

##### Summary

Inner Dispose

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| disposing | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |

<a name='M-RecordPoint-Connectors-SDK-Observability-Null-NullObservabilityScope-Dispose'></a>
### Dispose() `method`

##### Summary

Dispose

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Observability-Null-NullTelemetryBuilderExtensions'></a>
## NullTelemetryBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability.Null

##### Summary

Builder extensions used to setup the null telemetry tracker

<a name='M-RecordPoint-Connectors-SDK-Observability-Null-NullTelemetryBuilderExtensions-UseNullTelemetryTracking-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseNullTelemetryTracking(hostBuilder) `method`

##### Summary

Configure the host to use the null telemetry tracking

##### Returns

Updated host builder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | Host builder to configure |

<a name='T-RecordPoint-Connectors-SDK-Observability-Null-NullTelemetryTracker'></a>
## NullTelemetryTracker `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability.Null

##### Summary

Telemetry tracker that does nothing. Useful for initial development, testing.

<a name='M-RecordPoint-Connectors-SDK-Observability-Null-NullTelemetryTracker-BeginScope-RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures-'></a>
### BeginScope() `method`

##### Summary

Begins an observability scope

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Observability-Null-NullTelemetryTracker-TrackEvent-System-String,RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures-'></a>
### TrackEvent() `method`

##### Summary

Track an Event

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Observability-Null-NullTelemetryTracker-TrackException-System-Exception,RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures-'></a>
### TrackException() `method`

##### Summary

Track an Exception

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Observability-Null-NullTelemetryTracker-TrackTrace-System-String,RecordPoint-Connectors-SDK-Observability-SeverityLevel,RecordPoint-Connectors-SDK-Observability-Dimensions-'></a>
### TrackTrace() `method`

##### Summary

Track a Trace Message

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Toggles-Null-NullToggleBuilderExtensions'></a>
## NullToggleBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Toggles.Null

##### Summary

Null toggle host builder extensions

<a name='M-RecordPoint-Connectors-SDK-Toggles-Null-NullToggleBuilderExtensions-UseNullToggleProvider-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseNullToggleProvider(hostBuilder) `method`

##### Summary

Configure the host to use the default toggle provider

##### Returns

Updated host builder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | Host builder to update |

<a name='T-RecordPoint-Connectors-SDK-Toggles-Null-NullToggleProvider'></a>
## NullToggleProvider `type`

##### Namespace

RecordPoint.Connectors.SDK.Toggles.Null

##### Summary

Toggle provider that adds no funtionality and just passes through the default values

<a name='M-RecordPoint-Connectors-SDK-Toggles-Null-NullToggleProvider-GetToggleBool-System-String,System-Boolean-'></a>
### GetToggleBool(toggle,default) `method`

##### Summary

Get toggle bool.

##### Returns

A bool

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| toggle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The toggle. |
| default | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | If true, default. |

<a name='M-RecordPoint-Connectors-SDK-Toggles-Null-NullToggleProvider-GetToggleBool-System-String,System-String,System-Boolean-'></a>
### GetToggleBool(toggle,userKey,default) `method`

##### Summary

Get toggle bool.

##### Returns

A bool

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| toggle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The toggle. |
| userKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The user key. |
| default | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | If true, default. |

<a name='M-RecordPoint-Connectors-SDK-Toggles-Null-NullToggleProvider-GetToggleNumber-System-String,System-String,System-Int32-'></a>
### GetToggleNumber() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Toggles-Null-NullToggleProvider-GetToggleNumber-System-String,System-Int32-'></a>
### GetToggleNumber() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Toggles-Null-NullToggleProvider-GetToggleString-System-String,System-String,System-String-'></a>
### GetToggleString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Toggles-Null-NullToggleProvider-GetToggleString-System-String,System-String-'></a>
### GetToggleString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Observability-ObservabilityHostBuilderExtensions'></a>
## ObservabilityHostBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability

##### Summary

Host builder extensions for Observability

<a name='M-RecordPoint-Connectors-SDK-Observability-ObservabilityHostBuilderExtensions-UseObservabilityTracking-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseObservabilityTracking(hostBuilder) `method`

##### Summary

Configure the host for observability tracking

##### Returns

Updated host builder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | Host builder to configure |

<a name='T-RecordPoint-Connectors-SDK-Observability-ObservabilityScope'></a>
## ObservabilityScope `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability

##### Summary

Standard Observability Scope

<a name='P-RecordPoint-Connectors-SDK-Observability-ObservabilityScope-Dimensions'></a>
### Dimensions `property`

##### Summary

Scope dimensions

<a name='P-RecordPoint-Connectors-SDK-Observability-ObservabilityScope-Measures'></a>
### Measures `property`

##### Summary

Scope dimensions

<a name='M-RecordPoint-Connectors-SDK-Observability-ObservabilityScope-BeginScope-RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures-'></a>
### BeginScope() `method`

##### Summary

Begins a new observability scope, local to the current async task.

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Observability-ObservabilityScopeExtensions'></a>
## ObservabilityScopeExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability

##### Summary

Observability Scope extension methods

<a name='M-RecordPoint-Connectors-SDK-Observability-ObservabilityScopeExtensions-Invoke-RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-Dimensions,System-Action-'></a>
### Invoke(observabilityScope,dimensions,action) `method`

##### Summary

Invoke an action within a observability scope + ensuring that all exceptions are properly decorated

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | Target scope manager |
| dimensions | [RecordPoint.Connectors.SDK.Observability.Dimensions](#T-RecordPoint-Connectors-SDK-Observability-Dimensions 'RecordPoint.Connectors.SDK.Observability.Dimensions') | Dimensions to extend the scope with |
| action | [System.Action](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Action 'System.Action') | Action to invoke |

<a name='M-RecordPoint-Connectors-SDK-Observability-ObservabilityScopeExtensions-InvokeAsync-RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-Dimensions,System-Func{System-Threading-Tasks-Task}-'></a>
### InvokeAsync(observabilityScope,dimensions,action) `method`

##### Summary

Invoke an action within a observability scope + ensuring that all exceptions are properly decorated

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | Target scope manager |
| dimensions | [RecordPoint.Connectors.SDK.Observability.Dimensions](#T-RecordPoint-Connectors-SDK-Observability-Dimensions 'RecordPoint.Connectors.SDK.Observability.Dimensions') | Dimensions to extend the scope with |
| action | [System.Func{System.Threading.Tasks.Task}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{System.Threading.Tasks.Task}') | Action to invoke |

<a name='M-RecordPoint-Connectors-SDK-Observability-ObservabilityScopeExtensions-InvokeAsync``1-RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-Dimensions,System-Func{System-Threading-Tasks-Task{``0}}-'></a>
### InvokeAsync\`\`1(observabilityScope,dimensions,func) `method`

##### Summary

Invoke a function so that key properties are attached to log messages and any
exceptions raised during execution

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | Scope manager |
| dimensions | [RecordPoint.Connectors.SDK.Observability.Dimensions](#T-RecordPoint-Connectors-SDK-Observability-Dimensions 'RecordPoint.Connectors.SDK.Observability.Dimensions') | Logger |
| func | [System.Func{System.Threading.Tasks.Task{\`\`0}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{System.Threading.Tasks.Task{``0}}') | Action to invoke |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | Return type |

<a name='M-RecordPoint-Connectors-SDK-Observability-ObservabilityScopeExtensions-Invoke``1-RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-Dimensions,System-Func{``0}-'></a>
### Invoke\`\`1(observabilityScope,dimensions,func) `method`

##### Summary

Invoke an action within a observability scope + ensuring that all exceptions are properly decorated

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | Target scope manager |
| dimensions | [RecordPoint.Connectors.SDK.Observability.Dimensions](#T-RecordPoint-Connectors-SDK-Observability-Dimensions 'RecordPoint.Connectors.SDK.Observability.Dimensions') | Dimensions to extend the scope with |
| func | [System.Func{\`\`0}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{``0}') | Action to invoke |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | Return type |

<a name='T-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1'></a>
## QueueableWorkBase\`1 `type`

##### Namespace

RecordPoint.Connectors.SDK.Work

##### Summary

Base Implementation of unmanaged work that is submitted to a queue for execution.

<a name='M-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider-'></a>
### #ctor() `constructor`

##### Summary

Public constructor. Used to inject dependencies

##### Parameters

This constructor has no parameters.

<a name='F-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-_semaphoreLockManager'></a>
### _semaphoreLockManager `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-_serviceProvider'></a>
### _serviceProvider `constants`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-HasDisposed'></a>
### HasDisposed `property`

##### Summary

Public access to check if the object has been disposed

<a name='P-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-MustFinishDateTime'></a>
### MustFinishDateTime `property`

##### Summary

Time work must finish by

<a name='P-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-ResultDuration'></a>
### ResultDuration `property`

##### Summary

Time from when a request was made, till it was completed.
Includes time spent in queues, getting retried, etc.

<a name='P-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-ServiceName'></a>
### ServiceName `property`

##### Summary

Required override that identifies the service the work belongs to

<a name='P-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-SubmitDateTime'></a>
### SubmitDateTime `property`

##### Summary

Submit Start Time

<a name='P-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-SystemContext'></a>
### SystemContext `property`

##### Summary

System context

<a name='P-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-WaitTill'></a>
### WaitTill `property`

##### Summary

Deferral wait till time

<a name='P-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-WorkRequest'></a>
### WorkRequest `property`

##### Summary

Work Request we are handling

<a name='M-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-Abandoned-System-String-'></a>
### Abandoned() `method`

##### Summary

Record that work has been abandoned but should be retried later

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-Abandoned-System-Exception-'></a>
### Abandoned(exception) `method`

##### Summary

Record that this work item has been abandoned due to an exception and should should be retried later

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| exception | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Exception that is cause of the failure |

<a name='M-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-CheckSemaphoreLockAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,System-Object,System-Threading-CancellationToken-'></a>
### CheckSemaphoreLockAsync(connectorConfigModel,context,cancellationToken) `method`

##### Summary

Checks to see if a semphore lock has been applied and defers execution until the lock expires

##### Returns

True if a lock has been applied

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfigModel | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') |  |
| context | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | Context for lock keys when external apis have different restrictions, ie: by channel |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-Deferred-System-String,System-Nullable{System-DateTimeOffset}-'></a>
### Deferred() `method`

##### Summary

Record that this work item will be deferred to a future time

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-DeserializeParameter'></a>
### DeserializeParameter() `method`

##### Summary

Required override that deserializes the parameter from the work request

##### Returns

Deserialized parameters

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-Dispose'></a>
### Dispose() `method`

##### Summary

Dispose

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-GetCoreKeyDimensions'></a>
### GetCoreKeyDimensions() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-GetCoreResultMeasures'></a>
### GetCoreResultMeasures() `method`

##### Summary

Add additional core outcome measures

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-GetWorkResult'></a>
### GetWorkResult() `method`

##### Summary

Outcome for the work item

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-HandleBackOffResultAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,System-Object,System-Nullable{RecordPoint-Connectors-SDK-Caching-Semaphore-SemaphoreLockType},System-Nullable{System-Int32},System-Threading-CancellationToken-'></a>
### HandleBackOffResultAsync(connectorConfigModel,context,semaphoreLockType,nextDelay,cancellationToken) `method`

##### Summary

Defers the work and applies a semaphore lock

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfigModel | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') |  |
| context | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | Context for lock keys when external apis have different restrictions, ie: by channel |
| semaphoreLockType | [System.Nullable{RecordPoint.Connectors.SDK.Caching.Semaphore.SemaphoreLockType}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{RecordPoint.Connectors.SDK.Caching.Semaphore.SemaphoreLockType}') |  |
| nextDelay | [System.Nullable{System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int32}') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [RecordPoint.Connectors.SDK.RequiredValueNullException](#T-RecordPoint-Connectors-SDK-RequiredValueNullException 'RecordPoint.Connectors.SDK.RequiredValueNullException') |  |

<a name='M-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-InnerDispose'></a>
### InnerDispose() `method`

##### Summary

Dispose

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-QueueableWorkBase`1-RunWorkRequestAsync-RecordPoint-Connectors-SDK-Work-WorkRequest,System-Threading-CancellationToken-'></a>
### RunWorkRequestAsync(workRequest,cancellationToken) `method`

##### Summary

Execute the work request

##### Returns

Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workRequest | [RecordPoint.Connectors.SDK.Work.WorkRequest](#T-RecordPoint-Connectors-SDK-Work-WorkRequest 'RecordPoint.Connectors.SDK.Work.WorkRequest') | Work request that defines the work to execute |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='T-RecordPoint-Connectors-SDK-Work-QueueableWorkManager'></a>
## QueueableWorkManager `type`

##### Namespace

RecordPoint.Connectors.SDK.Work

##### Summary

Standard implementation of a work manager

<a name='M-RecordPoint-Connectors-SDK-Work-QueueableWorkManager-#ctor-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,System-IServiceProvider-'></a>
### #ctor() `constructor`

##### Summary

Standard implementation of a work manager

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-QueueableWorkManager-HandleWorkRequestAsync-RecordPoint-Connectors-SDK-Work-WorkRequest,System-Threading-CancellationToken-'></a>
### HandleWorkRequestAsync(workRequest,cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workRequest | [RecordPoint.Connectors.SDK.Work.WorkRequest](#T-RecordPoint-Connectors-SDK-Work-WorkRequest 'RecordPoint.Connectors.SDK.Work.WorkRequest') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='T-RecordPoint-Connectors-SDK-R365-R365BuilderExtensions'></a>
## R365BuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.R365

##### Summary

R365 Builder Extensions

<a name='M-RecordPoint-Connectors-SDK-R365-R365BuilderExtensions-AddR365Integration-Microsoft-Extensions-DependencyInjection-IServiceCollection,System-Boolean-'></a>
### AddR365Integration(services,submitRecordAndBinariesSynchronously) `method`

##### Summary

Add R365 Access component

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| services | [Microsoft.Extensions.DependencyInjection.IServiceCollection](#T-Microsoft-Extensions-DependencyInjection-IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection') |  |
| submitRecordAndBinariesSynchronously | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |

<a name='M-RecordPoint-Connectors-SDK-R365-R365BuilderExtensions-CreateAuditEventPipeline-System-IServiceProvider-'></a>
### CreateAuditEventPipeline(provider) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| provider | [System.IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider') |  |

<a name='M-RecordPoint-Connectors-SDK-R365-R365BuilderExtensions-CreateRecordPipeline-System-IServiceProvider-'></a>
### CreateRecordPipeline(provider) `method`

##### Summary

Setup multiple pipeline for submission

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| provider | [System.IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider') |  |

<a name='M-RecordPoint-Connectors-SDK-R365-R365BuilderExtensions-UseR365Integration-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseR365Integration(hostBuilder) `method`

##### Summary

Use the R365 Integration Client Components

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | Host builder to target |

<a name='T-RecordPoint-Connectors-SDK-R365-R365Client'></a>
## R365Client `type`

##### Namespace

RecordPoint.Connectors.SDK.R365

##### Summary

The r365 client.

<a name='M-RecordPoint-Connectors-SDK-R365-R365Client-#ctor-RecordPoint-Connectors-SDK-Configuration-IR365ConfigurationClient,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-R365-IR365Pipelines-'></a>
### #ctor(r365ConfigurationClient,observabilityScope,r365Pipelines) `constructor`

##### Summary

Initializes a new instance of the [R365Client](#T-RecordPoint-Connectors-SDK-R365-R365Client 'RecordPoint.Connectors.SDK.R365.R365Client') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| r365ConfigurationClient | [RecordPoint.Connectors.SDK.Configuration.IR365ConfigurationClient](#T-RecordPoint-Connectors-SDK-Configuration-IR365ConfigurationClient 'RecordPoint.Connectors.SDK.Configuration.IR365ConfigurationClient') | The r365 configuration client. |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | The scope manager. |
| r365Pipelines | [RecordPoint.Connectors.SDK.R365.IR365Pipelines](#T-RecordPoint-Connectors-SDK-R365-IR365Pipelines 'RecordPoint.Connectors.SDK.R365.IR365Pipelines') | The r365 pipelines. |

<a name='F-RecordPoint-Connectors-SDK-R365-R365Client-_observabilityScope'></a>
### _observabilityScope `constants`

##### Summary

The scope manager.

<a name='F-RecordPoint-Connectors-SDK-R365-R365Client-_r365ConfigurationClient'></a>
### _r365ConfigurationClient `constants`

##### Summary

The r365 configuration client.

<a name='F-RecordPoint-Connectors-SDK-R365-R365Client-_r365Pipelines'></a>
### _r365Pipelines `constants`

##### Summary

The r365 pipelines.

<a name='M-RecordPoint-Connectors-SDK-R365-R365Client-GetApiClientFactorySettings-RecordPoint-Connectors-SDK-Configuration-R365ConfigurationModel-'></a>
### GetApiClientFactorySettings(r365Configuration) `method`

##### Summary

Get api client factory settings.

##### Returns

An ApiClientFactorySettings

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| r365Configuration | [RecordPoint.Connectors.SDK.Configuration.R365ConfigurationModel](#T-RecordPoint-Connectors-SDK-Configuration-R365ConfigurationModel 'RecordPoint.Connectors.SDK.Configuration.R365ConfigurationModel') | The r365 configuration. |

<a name='M-RecordPoint-Connectors-SDK-R365-R365Client-GetAuthenticationHelperSettings-RecordPoint-Connectors-SDK-Configuration-R365ConfigurationModel,System-String-'></a>
### GetAuthenticationHelperSettings(r365Configuration,tenantDomainName) `method`

##### Summary

Get authentication helper settings.

##### Returns

An AuthenticationHelperSettings

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| r365Configuration | [RecordPoint.Connectors.SDK.Configuration.R365ConfigurationModel](#T-RecordPoint-Connectors-SDK-Configuration-R365ConfigurationModel 'RecordPoint.Connectors.SDK.Configuration.R365ConfigurationModel') | The r365 configuration. |
| tenantDomainName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The tenant domain name. |

<a name='M-RecordPoint-Connectors-SDK-R365-R365Client-GetDimensions'></a>
### GetDimensions() `method`

##### Summary

Get the dimensions.

##### Returns

A Dimensions

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-R365-R365Client-IsConfigured'></a>
### IsConfigured() `method`

##### Summary

Checks if is configured.

##### Returns

A bool

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-R365-R365Client-LoadConfiguration-System-String-'></a>
### LoadConfiguration() `method`

##### Summary

Load configuration with optional key (For multi configuration use)

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-R365-R365Client-SubmitAggregation-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Aggregation,System-Threading-CancellationToken-'></a>
### SubmitAggregation(connectorConfig,aggregation,cancellationToken) `method`

##### Summary

Submit an aggregation to records 365

##### Returns

Submit result

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfig | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') |  |
| aggregation | [RecordPoint.Connectors.SDK.Content.Aggregation](#T-RecordPoint-Connectors-SDK-Content-Aggregation 'RecordPoint.Connectors.SDK.Content.Aggregation') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation task |

<a name='M-RecordPoint-Connectors-SDK-R365-R365Client-SubmitAuditEvent-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-AuditEvent,System-Threading-CancellationToken-'></a>
### SubmitAuditEvent(connectorConfig,auditEvent,cancellationToken) `method`

##### Summary

Submit an audit event to records 365

##### Returns

Submit result

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfig | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | Connector configuration |
| auditEvent | [RecordPoint.Connectors.SDK.Content.AuditEvent](#T-RecordPoint-Connectors-SDK-Content-AuditEvent 'RecordPoint.Connectors.SDK.Content.AuditEvent') | Record to submit |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation task |

<a name='M-RecordPoint-Connectors-SDK-R365-R365Client-SubmitBinary-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-BinaryMetaInfo,System-IO-Stream,System-Threading-CancellationToken-'></a>
### SubmitBinary(connectorConfig,binaryMetaInfo,binaryStream,cancellationToken) `method`

##### Summary

Submit a binary to records 365

##### Returns

Submit result

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfig | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | Connector configuration |
| binaryMetaInfo | [RecordPoint.Connectors.SDK.Content.BinaryMetaInfo](#T-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo 'RecordPoint.Connectors.SDK.Content.BinaryMetaInfo') | Meta info for the binary to submit |
| binaryStream | [System.IO.Stream](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.Stream 'System.IO.Stream') | Stream of the binaries content |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation task |

<a name='M-RecordPoint-Connectors-SDK-R365-R365Client-SubmitRecord-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Record,System-Threading-CancellationToken-'></a>
### SubmitRecord(connectorConfig,record,cancellationToken) `method`

##### Summary

Submit a record to records 365

##### Returns

Submit result

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfig | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | Connector configuration |
| record | [RecordPoint.Connectors.SDK.Content.Record](#T-RecordPoint-Connectors-SDK-Content-Record 'RecordPoint.Connectors.SDK.Content.Record') | Record to submit |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation task |

<a name='T-RecordPoint-Connectors-SDK-R365-R365Pipelines'></a>
## R365Pipelines `type`

##### Namespace

RecordPoint.Connectors.SDK.R365

##### Summary

The r365 pipelines.

<a name='M-RecordPoint-Connectors-SDK-R365-R365Pipelines-#ctor-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission,RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission,RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission,RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission-'></a>
### #ctor(recordPipeline,binaryPipeline,aggregationPipeline,auditEventPipeline) `constructor`

##### Summary

Initializes a new instance of the [R365Pipelines](#T-RecordPoint-Connectors-SDK-R365-R365Pipelines 'RecordPoint.Connectors.SDK.R365.R365Pipelines') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| recordPipeline | [RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission](#T-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission 'RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission') | The record pipeline. |
| binaryPipeline | [RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission](#T-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission 'RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission') | The binary pipeline. |
| aggregationPipeline | [RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission](#T-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission 'RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission') | The aggregation pipeline. |
| auditEventPipeline | [RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission](#T-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission 'RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission') | The audit event pipeline. |

<a name='P-RecordPoint-Connectors-SDK-R365-R365Pipelines-AggregationPipeline'></a>
### AggregationPipeline `property`

##### Summary

Aggregation submission pipeline

<a name='P-RecordPoint-Connectors-SDK-R365-R365Pipelines-AuditEventPipeline'></a>
### AuditEventPipeline `property`

##### Summary

Audit event submission pipeline

<a name='P-RecordPoint-Connectors-SDK-R365-R365Pipelines-BinaryPipeline'></a>
### BinaryPipeline `property`

##### Summary

Binary submission pipeline

<a name='P-RecordPoint-Connectors-SDK-R365-R365Pipelines-RecordPipeline'></a>
### RecordPipeline `property`

##### Summary

Record submission pipeline

<a name='T-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation'></a>
## RecordDisposalOperation `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

The record disposal operation.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider,RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider-'></a>
### #ctor(serviceProvider,contentManagerActionProvider,connectorManager,systemContext,observabilityScope,telemetryTracker,dateTimeProvider) `constructor`

##### Summary

Initializes a new instance of the [RecordDisposalOperation](#T-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation 'RecordPoint.Connectors.SDK.ContentManager.RecordDisposalOperation') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| serviceProvider | [System.IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider') | The service provider. |
| contentManagerActionProvider | [RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider](#T-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider 'RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider') | The content manager action provider. |
| connectorManager | [RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager](#T-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager') | The connector manager. |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | The scope manager. |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The telemetry tracker. |
| dateTimeProvider | [RecordPoint.Connectors.SDK.Providers.IDateTimeProvider](#T-RecordPoint-Connectors-SDK-Providers-IDateTimeProvider 'RecordPoint.Connectors.SDK.Providers.IDateTimeProvider') | The date time provider. |

<a name='F-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-BINARY_SUBMISSION_DELAY_SECONDS'></a>
### BINARY_SUBMISSION_DELAY_SECONDS `constants`

##### Summary

The BINARY SUBMISSION DELAY SECONDS.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-DEFAULT_DEFERRAL_SECONDS'></a>
### DEFAULT_DEFERRAL_SECONDS `constants`

##### Summary

The DEFAULT DEFERRAL SECONDS.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-WORK_TYPE'></a>
### WORK_TYPE `constants`

##### Summary

WORK TYPE.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-_connectorConfiguration'></a>
### _connectorConfiguration `constants`

##### Summary

The connector configuration.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-_connectorManager'></a>
### _connectorManager `constants`

##### Summary

The connector manager.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-_contentManagerActionProvider'></a>
### _contentManagerActionProvider `constants`

##### Summary

The content manager action provider.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-_recordDisposalResult'></a>
### _recordDisposalResult `constants`

##### Summary

The record disposal result.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-ConnectorConfigId'></a>
### ConnectorConfigId `property`

##### Summary

Gets the connector config id.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-Record'></a>
### Record `property`

##### Summary

Gets the record.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-ServiceName'></a>
### ServiceName `property`

##### Summary

Gets the service name.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-WorkType'></a>
### WorkType `property`

##### Summary

Gets the work type.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-GetCustomKeyDimensions'></a>
### GetCustomKeyDimensions() `method`

##### Summary

Get custom key dimensions.

##### Returns

A Dimensions

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-GetCustomResultDimensions'></a>
### GetCustomResultDimensions() `method`

##### Summary

Get custom result dimensions.

##### Returns

A Dimensions

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-GetCustomResultMeasures'></a>
### GetCustomResultMeasures() `method`

##### Summary

Get custom result measures.

##### Returns

A Measures

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-InnerDispose'></a>
### InnerDispose() `method`

##### Summary

Dispose invocation results

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalOperation-InnerRunAsync-System-Threading-CancellationToken-'></a>
### InnerRunAsync(cancellationToken) `method`

##### Summary

Inner the run asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') |  |

<a name='T-RecordPoint-Connectors-SDK-R365-SDKLogAdapter'></a>
## SDKLogAdapter `type`

##### Namespace

RecordPoint.Connectors.SDK.R365

##### Summary

The SDK log adapter.

<a name='M-RecordPoint-Connectors-SDK-R365-SDKLogAdapter-#ctor-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker-'></a>
### #ctor() `constructor`

##### Summary

The SDK log adapter.

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-R365-SDKLogAdapter-LogMessage-System-Type,System-String,System-String,System-Nullable{System-Int64}-'></a>
### LogMessage(callerType,methodName,message,elapsedTimeTicks) `method`

##### Summary

Log the message.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| callerType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The caller type. |
| methodName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The method name. |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message. |
| elapsedTimeTicks | [System.Nullable{System.Int64}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int64}') | The elapsed time ticks. |

<a name='M-RecordPoint-Connectors-SDK-R365-SDKLogAdapter-LogVerbose-System-Type,System-String,System-String,System-Nullable{System-Int64}-'></a>
### LogVerbose(callerType,methodName,message,elapsedTimeTicks) `method`

##### Summary

Log the verbose.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| callerType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The caller type. |
| methodName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The method name. |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message. |
| elapsedTimeTicks | [System.Nullable{System.Int64}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int64}') | The elapsed time ticks. |

<a name='M-RecordPoint-Connectors-SDK-R365-SDKLogAdapter-LogWarning-System-Type,System-String,System-String,System-Nullable{System-Int64}-'></a>
### LogWarning(callerType,methodName,message,elapsedTimeTicks) `method`

##### Summary

Log the warning.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| callerType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The caller type. |
| methodName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The method name. |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message. |
| elapsedTimeTicks | [System.Nullable{System.Int64}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int64}') | The elapsed time ticks. |

<a name='T-RecordPoint-Connectors-SDK-Providers-SettableCircuitProvider'></a>
## SettableCircuitProvider `type`

##### Namespace

RecordPoint.Connectors.SDK.Providers

##### Summary

See ISettableCircuitProvider

<a name='P-RecordPoint-Connectors-SDK-Providers-SettableCircuitProvider-DateTimeProvider'></a>
### DateTimeProvider `property`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Providers-SettableCircuitProvider-IsCircuitClosed-System-TimeSpan@-'></a>
### IsCircuitClosed(waitFor) `method`

##### Summary

See ICircuitProvider

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| waitFor | [System.TimeSpan@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.TimeSpan@ 'System.TimeSpan@') |  |

<a name='M-RecordPoint-Connectors-SDK-Providers-SettableCircuitProvider-SetOpenUntil-System-DateTime-'></a>
### SetOpenUntil() `method`

##### Summary

See ISettableCircuitProvider

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Status-StatusBuilderExtensions'></a>
## StatusBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Status

##### Summary

The status builder extensions.

<a name='M-RecordPoint-Connectors-SDK-Status-StatusBuilderExtensions-UseStatusManager-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseStatusManager(hostBuilder) `method`

##### Summary

Configure the host to use polled notifications

##### Returns

Configured host builder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | Host builder to configure |

<a name='T-RecordPoint-Connectors-SDK-Status-StatusManager'></a>
## StatusManager `type`

##### Namespace

RecordPoint.Connectors.SDK.Status

##### Summary

The status manager.

<a name='M-RecordPoint-Connectors-SDK-Status-StatusManager-#ctor-System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Status-IStatusStrategy},RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager-'></a>
### #ctor(strategies,connectorManager) `constructor`

##### Summary

Initializes a new instance of the [StatusManager](#T-RecordPoint-Connectors-SDK-Status-StatusManager 'RecordPoint.Connectors.SDK.Status.StatusManager') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| strategies | [System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Status.IStatusStrategy}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Status.IStatusStrategy}') | The strategies. |
| connectorManager | [RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager](#T-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager') | The connector manager. |

<a name='F-RecordPoint-Connectors-SDK-Status-StatusManager-_connectorManager'></a>
### _connectorManager `constants`

##### Summary

The connector manager.

<a name='F-RecordPoint-Connectors-SDK-Status-StatusManager-_strategies'></a>
### _strategies `constants`

##### Summary

The strategies.

<a name='M-RecordPoint-Connectors-SDK-Status-StatusManager-GetStatusModelAsync-System-Threading-CancellationToken-'></a>
### GetStatusModelAsync(cancellationToken) `method`

##### Summary

Get status model asynchronously.

##### Returns

Task<List<StatusModel>>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='T-RecordPoint-Connectors-SDK-Context-StopSystemException'></a>
## StopSystemException `type`

##### Namespace

RecordPoint.Connectors.SDK.Context

##### Summary

The stop system exception.

<a name='M-RecordPoint-Connectors-SDK-Context-StopSystemException-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the [StopSystemException](#T-RecordPoint-Connectors-SDK-Context-StopSystemException 'RecordPoint.Connectors.SDK.Context.StopSystemException') class.

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Context-StopSystemException-#ctor-System-String-'></a>
### #ctor(message) `constructor`

##### Summary

Initializes a new instance of the [StopSystemException](#T-RecordPoint-Connectors-SDK-Context-StopSystemException 'RecordPoint.Connectors.SDK.Context.StopSystemException') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message. |

<a name='M-RecordPoint-Connectors-SDK-Context-StopSystemException-#ctor-System-String,System-Exception-'></a>
### #ctor(message,inner) `constructor`

##### Summary

Initializes a new instance of the [StopSystemException](#T-RecordPoint-Connectors-SDK-Context-StopSystemException 'RecordPoint.Connectors.SDK.Context.StopSystemException') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message. |
| inner | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | The inner. |

<a name='T-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation'></a>
## SubmitAggregationOperation `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

The submit aggregation operation.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider,RecordPoint-Connectors-SDK-R365-IR365Client,RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider-'></a>
### #ctor(serviceProvider,contentManagerActionProvider,r365Client,workQueueClient,connectorManager,systemContext,observabilityScope,telemetryTracker,dateTimeProvider) `constructor`

##### Summary

Initializes a new instance of the [SubmitAggregationOperation](#T-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation 'RecordPoint.Connectors.SDK.ContentManager.SubmitAggregationOperation') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| serviceProvider | [System.IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider') | The service provider. |
| contentManagerActionProvider | [RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider](#T-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider 'RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider') | The content manager action provider. |
| r365Client | [RecordPoint.Connectors.SDK.R365.IR365Client](#T-RecordPoint-Connectors-SDK-R365-IR365Client 'RecordPoint.Connectors.SDK.R365.IR365Client') | The r365 client. |
| workQueueClient | [RecordPoint.Connectors.SDK.Work.IWorkQueueClient](#T-RecordPoint-Connectors-SDK-Work-IWorkQueueClient 'RecordPoint.Connectors.SDK.Work.IWorkQueueClient') | The work queue client. |
| connectorManager | [RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager](#T-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager') | The connector manager. |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | The scope manager. |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The telemetry tracker. |
| dateTimeProvider | [RecordPoint.Connectors.SDK.Providers.IDateTimeProvider](#T-RecordPoint-Connectors-SDK-Providers-IDateTimeProvider 'RecordPoint.Connectors.SDK.Providers.IDateTimeProvider') | The date time provider. |

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-CONTENT_LABEL'></a>
### CONTENT_LABEL `constants`

##### Summary

The CONTENT LABEL.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-WORK_TYPE'></a>
### WORK_TYPE `constants`

##### Summary

WORK TYPE.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-_connectorManager'></a>
### _connectorManager `constants`

##### Summary

The connector manager.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-_contentManagerActionProvider'></a>
### _contentManagerActionProvider `constants`

##### Summary

The content manager action provider.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-Aggregation'></a>
### Aggregation `property`

##### Summary

Gets the aggregation.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-ConnectorConfig'></a>
### ConnectorConfig `property`

##### Summary

Gets the connector config.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-ContentLabel'></a>
### ContentLabel `property`

##### Summary

Gets the content label.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-ServiceName'></a>
### ServiceName `property`

##### Summary

Gets the service name.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-WorkType'></a>
### WorkType `property`

##### Summary

Gets the work type.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-GetFeatureStatus-System-Threading-CancellationToken-'></a>
### GetFeatureStatus(cancellationToken) `method`

##### Summary

Get feature status.

##### Returns

Task<ConnectorFeatureStatus>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-InnerDispose'></a>
### InnerDispose() `method`

##### Summary

Dispose

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-InnerRunAsync-System-Threading-CancellationToken-'></a>
### InnerRunAsync(cancellationToken) `method`

##### Summary

Inner the run asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-PreSubmitAsync-Microsoft-Extensions-DependencyInjection-IServiceScope,System-Threading-CancellationToken-'></a>
### PreSubmitAsync() `method`

##### Summary



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-RequeueAsync-System-DateTimeOffset,System-Threading-CancellationToken-'></a>
### RequeueAsync(waitTill,cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| waitTill | [System.DateTimeOffset](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTimeOffset 'System.DateTimeOffset') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-SubmitAsync-System-Threading-CancellationToken-'></a>
### SubmitAsync(cancellationToken) `method`

##### Summary

Submits and return a task of type submitresult asynchronously.

##### Returns

Task<SubmitResult>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitAggregationOperation-SubmitSuccessfulAsync-Microsoft-Extensions-DependencyInjection-IServiceScope,System-Threading-CancellationToken-'></a>
### SubmitSuccessfulAsync() `method`

##### Summary



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation'></a>
## SubmitAuditEventOperation `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

The submit audit event operation.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider,RecordPoint-Connectors-SDK-R365-IR365Client,RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider-'></a>
### #ctor(serviceProvider,contentManagerActionProvider,r365Client,connectorManager,systemContext,observabilityScope,telemetryTracker,workQueueClient,dateTimeProvider) `constructor`

##### Summary

Initializes a new instance of the [SubmitAuditEventOperation](#T-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation 'RecordPoint.Connectors.SDK.ContentManager.SubmitAuditEventOperation') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| serviceProvider | [System.IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider') | The service provider. |
| contentManagerActionProvider | [RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider](#T-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider 'RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider') | The content manager action provider. |
| r365Client | [RecordPoint.Connectors.SDK.R365.IR365Client](#T-RecordPoint-Connectors-SDK-R365-IR365Client 'RecordPoint.Connectors.SDK.R365.IR365Client') | The r365 client. |
| connectorManager | [RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager](#T-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager') | The connector manager. |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | The scope manager. |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The telemetry tracker. |
| workQueueClient | [RecordPoint.Connectors.SDK.Work.IWorkQueueClient](#T-RecordPoint-Connectors-SDK-Work-IWorkQueueClient 'RecordPoint.Connectors.SDK.Work.IWorkQueueClient') | The work queue client. |
| dateTimeProvider | [RecordPoint.Connectors.SDK.Providers.IDateTimeProvider](#T-RecordPoint-Connectors-SDK-Providers-IDateTimeProvider 'RecordPoint.Connectors.SDK.Providers.IDateTimeProvider') | The date time provider. |

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-CONTENT_LABEL'></a>
### CONTENT_LABEL `constants`

##### Summary

The CONTENT LABEL.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-DEFAULT_DEFERRAL_SECONDS'></a>
### DEFAULT_DEFERRAL_SECONDS `constants`

##### Summary

The DEFAULT DEFERRAL SECONDS.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-WORK_TYPE'></a>
### WORK_TYPE `constants`

##### Summary

WORK TYPE.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-_connectorConfiguration'></a>
### _connectorConfiguration `constants`

##### Summary

The connector configuration.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-_connectorManager'></a>
### _connectorManager `constants`

##### Summary

The connector manager.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-_contentManagerActionProvider'></a>
### _contentManagerActionProvider `constants`

##### Summary

The content manager action provider.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-_r365Client'></a>
### _r365Client `constants`

##### Summary

The r365 client.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-_workQueueClient'></a>
### _workQueueClient `constants`

##### Summary

Work queue client.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-AuditEvent'></a>
### AuditEvent `property`

##### Summary

Gets the audit event.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-ConnectorConfigId'></a>
### ConnectorConfigId `property`

##### Summary

Gets the connector config id.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-ServiceName'></a>
### ServiceName `property`

##### Summary

Gets the service name.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-WorkType'></a>
### WorkType `property`

##### Summary

Gets the work type.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-GetBackoffDateTime-System-Nullable{System-DateTimeOffset}-'></a>
### GetBackoffDateTime(suggestedDateTime) `method`

##### Summary

Get back off till date time

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| suggestedDateTime | [System.Nullable{System.DateTimeOffset}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.DateTimeOffset}') | Date time suggested by R365, if provided |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-GetCustomKeyDimensions'></a>
### GetCustomKeyDimensions() `method`

##### Summary

Get custom key dimensions.

##### Returns

A Dimensions

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-GetDeferralDateTime-System-Nullable{System-DateTimeOffset}-'></a>
### GetDeferralDateTime(suggestedDateTime) `method`

##### Summary

Get deferral date time

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| suggestedDateTime | [System.Nullable{System.DateTimeOffset}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.DateTimeOffset}') | Date time suggested by R365, if provided |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-GetFeatureStatus-System-Threading-CancellationToken-'></a>
### GetFeatureStatus(cancellationToken) `method`

##### Summary

Get feature status.

##### Returns

Task<ConnectorFeatureStatus>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-InnerDispose'></a>
### InnerDispose() `method`

##### Summary

Dispose

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-InnerRunAsync-System-Threading-CancellationToken-'></a>
### InnerRunAsync(cancellationToken) `method`

##### Summary

Inner the run asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitAuditEventOperation-SubmitAsync-System-Threading-CancellationToken-'></a>
### SubmitAsync(cancellationToken) `method`

##### Summary

Submits and return a task of type submitresult asynchronously.

##### Returns

Task<SubmitResult>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='T-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation'></a>
## SubmitBinaryOperation `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

The submit binary operation.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider,RecordPoint-Connectors-SDK-R365-IR365Client,RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider-'></a>
### #ctor(serviceProvider,contentManagerActionProvider,r365Client,connectorManager,systemContext,observabilityScope,telemetryTracker,workQueueClient,dateTimeProvider) `constructor`

##### Summary

Initializes a new instance of the [SubmitBinaryOperation](#T-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation 'RecordPoint.Connectors.SDK.ContentManager.SubmitBinaryOperation') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| serviceProvider | [System.IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider') | The service provider. |
| contentManagerActionProvider | [RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider](#T-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider 'RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider') | The content manager action provider. |
| r365Client | [RecordPoint.Connectors.SDK.R365.IR365Client](#T-RecordPoint-Connectors-SDK-R365-IR365Client 'RecordPoint.Connectors.SDK.R365.IR365Client') | The r365 client. |
| connectorManager | [RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager](#T-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager') | The connector manager. |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | The scope manager. |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The telemetry tracker. |
| workQueueClient | [RecordPoint.Connectors.SDK.Work.IWorkQueueClient](#T-RecordPoint-Connectors-SDK-Work-IWorkQueueClient 'RecordPoint.Connectors.SDK.Work.IWorkQueueClient') | The work queue client. |
| dateTimeProvider | [RecordPoint.Connectors.SDK.Providers.IDateTimeProvider](#T-RecordPoint-Connectors-SDK-Providers-IDateTimeProvider 'RecordPoint.Connectors.SDK.Providers.IDateTimeProvider') | The date time provider. |

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-BINARY_SUBMISSION_DELAY_SECONDS'></a>
### BINARY_SUBMISSION_DELAY_SECONDS `constants`

##### Summary

The BINARY SUBMISSION DELAY SECONDS.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-DEFAULT_DEFERRAL_SECONDS'></a>
### DEFAULT_DEFERRAL_SECONDS `constants`

##### Summary

The DEFAULT DEFERRAL SECONDS.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-WORK_TYPE'></a>
### WORK_TYPE `constants`

##### Summary

WORK TYPE.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-_actionExecutionTimespan'></a>
### _actionExecutionTimespan `constants`

##### Summary

How long it took to for the operation to execute

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-_binaryRetrievalResult'></a>
### _binaryRetrievalResult `constants`

##### Summary

The binary retrieval result.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-_connectorConfiguration'></a>
### _connectorConfiguration `constants`

##### Summary

The connector configuration.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-_connectorManager'></a>
### _connectorManager `constants`

##### Summary

The connector manager.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-_contentManagerActionProvider'></a>
### _contentManagerActionProvider `constants`

##### Summary

The content manager action provider.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-_r365Client'></a>
### _r365Client `constants`

##### Summary

The r365 client.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-_submitTimespan'></a>
### _submitTimespan `constants`

##### Summary

How long it took to submit the work to the queue

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-_workQueueClient'></a>
### _workQueueClient `constants`

##### Summary

Work queue client.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-BinaryMetaInfo'></a>
### BinaryMetaInfo `property`

##### Summary

Gets the binary meta info.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-ConnectorConfigId'></a>
### ConnectorConfigId `property`

##### Summary

Gets the connector config id.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-ServiceName'></a>
### ServiceName `property`

##### Summary

Gets the service name.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-WorkType'></a>
### WorkType `property`

##### Summary

Gets the work type.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-GetCustomKeyDimensions'></a>
### GetCustomKeyDimensions() `method`

##### Summary

Get custom key dimensions.

##### Returns

A Dimensions

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-GetCustomResultMeasures'></a>
### GetCustomResultMeasures() `method`

##### Summary

Get custom result measures.

##### Returns

A Measures

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-InnerDispose'></a>
### InnerDispose() `method`

##### Summary

Dispose binary stream

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-InnerRunAsync-System-Threading-CancellationToken-'></a>
### InnerRunAsync(cancellationToken) `method`

##### Summary

Inner the run asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-InvokeSubmissionCallbackAsync-Microsoft-Extensions-DependencyInjection-IServiceScope,RecordPoint-Connectors-SDK-Abstractions-ContentManager-SubmissionActionType,System-Threading-CancellationToken-'></a>
### InvokeSubmissionCallbackAsync(scope,submissionActionType,cancellationToken) `method`

##### Summary

Invokes submission callback asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| scope | [Microsoft.Extensions.DependencyInjection.IServiceScope](#T-Microsoft-Extensions-DependencyInjection-IServiceScope 'Microsoft.Extensions.DependencyInjection.IServiceScope') | Service scope for dependency injection |
| submissionActionType | [RecordPoint.Connectors.SDK.Abstractions.ContentManager.SubmissionActionType](#T-RecordPoint-Connectors-SDK-Abstractions-ContentManager-SubmissionActionType 'RecordPoint.Connectors.SDK.Abstractions.ContentManager.SubmissionActionType') | The submission action type. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitBinaryOperation-RecordOutcomeAsync-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult,System-Threading-CancellationToken-'></a>
### RecordOutcomeAsync(submitResult,cancellationToken) `method`

##### Summary

Record the work outcome

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| submitResult | [RecordPoint.Connectors.SDK.SubmitPipeline.SubmitResult](#T-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitResult') | Submission result |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='T-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1'></a>
## SubmitOperationBase\`1 `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Base class for single item submission workers

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-R365-IR365Client,RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider-'></a>
### #ctor() `constructor`

##### Summary

Constructor used to inject dependencies

##### Parameters

This constructor has no parameters.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-DEFAULT_BACKOFF_SECONDS'></a>
### DEFAULT_BACKOFF_SECONDS `constants`

##### Summary

Number of seconds to backoff if requested too and no time is provided

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-DEFAULT_DEFERRAL_SECONDS'></a>
### DEFAULT_DEFERRAL_SECONDS `constants`

##### Summary

Default number of seconds to defer work if requested and no time is provided

<a name='P-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-ConnectorConfigId'></a>
### ConnectorConfigId `property`

##### Summary

Connector the request is for

<a name='P-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-ContentItem'></a>
### ContentItem `property`

##### Summary

Content Item being submitted

<a name='P-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-ContentLabel'></a>
### ContentLabel `property`

##### Summary

A label used to describe what the content is. I.E. Record, Aggregation etc.
Used to display diagnostics, observability messages

<a name='P-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-R365Client'></a>
### R365Client `property`

##### Summary

R365 Client

<a name='P-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-WorkQueueClient'></a>
### WorkQueueClient `property`

##### Summary

Work queue client needed to submit further work

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-GetBackoffDateTime-System-Nullable{System-DateTimeOffset}-'></a>
### GetBackoffDateTime(suggestedDateTime) `method`

##### Summary

Get back off till date time

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| suggestedDateTime | [System.Nullable{System.DateTimeOffset}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.DateTimeOffset}') | Date time suggested by R365, if provided |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-GetCoreKeyDimensions'></a>
### GetCoreKeyDimensions() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-GetDeferralDateTime-System-Nullable{System-DateTimeOffset}-'></a>
### GetDeferralDateTime(suggestedDateTime) `method`

##### Summary

Get deferral date time

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| suggestedDateTime | [System.Nullable{System.DateTimeOffset}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.DateTimeOffset}') | Date time suggested by R365, if provided |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-GetFeatureStatus-System-Threading-CancellationToken-'></a>
### GetFeatureStatus(cancellationToken) `method`

##### Summary

Required override that gets the current feature status for this submission

##### Returns

Feature status

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-InnerDispose'></a>
### InnerDispose() `method`

##### Summary

Dispose

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.NotImplementedException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.NotImplementedException 'System.NotImplementedException') |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-InnerRunAsync-System-Threading-CancellationToken-'></a>
### InnerRunAsync(cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-PreSubmitAsync-Microsoft-Extensions-DependencyInjection-IServiceScope,System-Threading-CancellationToken-'></a>
### PreSubmitAsync(scope,cancellationToken) `method`

##### Summary

Required override that is called prior to submission to R365

##### Returns

Submit result

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| scope | [Microsoft.Extensions.DependencyInjection.IServiceScope](#T-Microsoft-Extensions-DependencyInjection-IServiceScope 'Microsoft.Extensions.DependencyInjection.IServiceScope') | Service scope |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-RequeueAsync-System-DateTimeOffset,System-Threading-CancellationToken-'></a>
### RequeueAsync(waitTill,cancellationToken) `method`

##### Summary

Required override that requeues the request if needed

##### Returns

Submit result

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| waitTill | [System.DateTimeOffset](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTimeOffset 'System.DateTimeOffset') | Time to wait until |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-SubmitAsync-System-Threading-CancellationToken-'></a>
### SubmitAsync(cancellationToken) `method`

##### Summary

Required override that does the submission to R365

##### Returns

Submit result

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitOperationBase`1-SubmitSuccessfulAsync-Microsoft-Extensions-DependencyInjection-IServiceScope,System-Threading-CancellationToken-'></a>
### SubmitSuccessfulAsync(scope,cancellationToken) `method`

##### Summary

Required override that is called after a successfull submission to R365

##### Returns

Submit result

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| scope | [Microsoft.Extensions.DependencyInjection.IServiceScope](#T-Microsoft-Extensions-DependencyInjection-IServiceScope 'Microsoft.Extensions.DependencyInjection.IServiceScope') | Service scope |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='T-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation'></a>
## SubmitRecordOperation `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

The submit record operation.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider,RecordPoint-Connectors-SDK-R365-IR365Client,RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider-'></a>
### #ctor(serviceProvider,contentManagerActionProvider,r365Client,workQueueClient,connectorManager,systemContext,observabilityScope,telemetryTracker,dateTimeProvider) `constructor`

##### Summary

Initializes a new instance of the [SubmitRecordOperation](#T-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation 'RecordPoint.Connectors.SDK.ContentManager.SubmitRecordOperation') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| serviceProvider | [System.IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider') | The service provider. |
| contentManagerActionProvider | [RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider](#T-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider 'RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider') | The content manager action provider. |
| r365Client | [RecordPoint.Connectors.SDK.R365.IR365Client](#T-RecordPoint-Connectors-SDK-R365-IR365Client 'RecordPoint.Connectors.SDK.R365.IR365Client') | The r365 client. |
| workQueueClient | [RecordPoint.Connectors.SDK.Work.IWorkQueueClient](#T-RecordPoint-Connectors-SDK-Work-IWorkQueueClient 'RecordPoint.Connectors.SDK.Work.IWorkQueueClient') | The work queue client. |
| connectorManager | [RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager](#T-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager') | The connector manager. |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | The scope manager. |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The telemetry tracker. |
| dateTimeProvider | [RecordPoint.Connectors.SDK.Providers.IDateTimeProvider](#T-RecordPoint-Connectors-SDK-Providers-IDateTimeProvider 'RecordPoint.Connectors.SDK.Providers.IDateTimeProvider') | The date time provider. |

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-CONTENT_LABEL'></a>
### CONTENT_LABEL `constants`

##### Summary

The CONTENT LABEL.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-WORK_TYPE'></a>
### WORK_TYPE `constants`

##### Summary

WORK TYPE.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-_connectorManager'></a>
### _connectorManager `constants`

##### Summary

The connector manager.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-_contentManagerActionProvider'></a>
### _contentManagerActionProvider `constants`

##### Summary

The content manager action provider.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-ConnectorConfig'></a>
### ConnectorConfig `property`

##### Summary

Gets the connector config.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-ContentLabel'></a>
### ContentLabel `property`

##### Summary

Gets the content label.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-ServiceName'></a>
### ServiceName `property`

##### Summary

Gets the service name.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-WorkType'></a>
### WorkType `property`

##### Summary

Gets the work type.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-GetFeatureStatus-System-Threading-CancellationToken-'></a>
### GetFeatureStatus(cancellationToken) `method`

##### Summary

Get feature status.

##### Returns

Task<ConnectorFeatureStatus>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-InnerRunAsync-System-Threading-CancellationToken-'></a>
### InnerRunAsync(cancellationToken) `method`

##### Summary

Inner the run asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-PreSubmitAsync-Microsoft-Extensions-DependencyInjection-IServiceScope,System-Threading-CancellationToken-'></a>
### PreSubmitAsync() `method`

##### Summary



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-RequeueAsync-System-DateTimeOffset,System-Threading-CancellationToken-'></a>
### RequeueAsync(waitTill,cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| waitTill | [System.DateTimeOffset](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTimeOffset 'System.DateTimeOffset') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-SubmitAsync-System-Threading-CancellationToken-'></a>
### SubmitAsync(cancellationToken) `method`

##### Summary

Submits and return a task of type submitresult asynchronously.

##### Returns

Task<SubmitResult>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SubmitRecordOperation-SubmitSuccessfulAsync-Microsoft-Extensions-DependencyInjection-IServiceScope,System-Threading-CancellationToken-'></a>
### SubmitSuccessfulAsync() `method`

##### Summary



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-ContentManager-SynchronousSubmitRecordOperation'></a>
## SynchronousSubmitRecordOperation `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Synchronously submits records and all associated binaries to the R365 system.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| serviceProvider | [T:RecordPoint.Connectors.SDK.ContentManager.SynchronousSubmitRecordOperation](#T-T-RecordPoint-Connectors-SDK-ContentManager-SynchronousSubmitRecordOperation 'T:RecordPoint.Connectors.SDK.ContentManager.SynchronousSubmitRecordOperation') | The service provider. |

##### Remarks

Initializes a new instance of the [SynchronousSubmitRecordOperation](#T-RecordPoint-Connectors-SDK-ContentManager-SynchronousSubmitRecordOperation 'RecordPoint.Connectors.SDK.ContentManager.SynchronousSubmitRecordOperation') class.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SynchronousSubmitRecordOperation-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider,RecordPoint-Connectors-SDK-R365-IR365Client,RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider-'></a>
### #ctor(serviceProvider,contentManagerActionProvider,r365Client,connectorManager,systemContext,observabilityScope,telemetryTracker,workQueueClient,dateTimeProvider) `constructor`

##### Summary

Synchronously submits records and all associated binaries to the R365 system.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| serviceProvider | [System.IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider') | The service provider. |
| contentManagerActionProvider | [RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider](#T-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider 'RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider') | The content manager action provider. |
| r365Client | [RecordPoint.Connectors.SDK.R365.IR365Client](#T-RecordPoint-Connectors-SDK-R365-IR365Client 'RecordPoint.Connectors.SDK.R365.IR365Client') | The r365 client. |
| connectorManager | [RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager](#T-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager') | The connector manager. |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | The scope manager. |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The telemetry tracker. |
| workQueueClient | [RecordPoint.Connectors.SDK.Work.IWorkQueueClient](#T-RecordPoint-Connectors-SDK-Work-IWorkQueueClient 'RecordPoint.Connectors.SDK.Work.IWorkQueueClient') | The work queue client. |
| dateTimeProvider | [RecordPoint.Connectors.SDK.Providers.IDateTimeProvider](#T-RecordPoint-Connectors-SDK-Providers-IDateTimeProvider 'RecordPoint.Connectors.SDK.Providers.IDateTimeProvider') | The date time provider. |

##### Remarks

Initializes a new instance of the [SynchronousSubmitRecordOperation](#T-RecordPoint-Connectors-SDK-ContentManager-SynchronousSubmitRecordOperation 'RecordPoint.Connectors.SDK.ContentManager.SynchronousSubmitRecordOperation') class.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SynchronousSubmitRecordOperation-WORK_TYPE'></a>
### WORK_TYPE `constants`

##### Summary

WORK TYPE.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-SynchronousSubmitRecordOperation-_connectorConfiguration'></a>
### _connectorConfiguration `constants`

##### Summary

The connector configuration.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-SynchronousSubmitRecordOperation-ConnectorConfigId'></a>
### ConnectorConfigId `property`

##### Summary

Gets the connector config id.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-SynchronousSubmitRecordOperation-ServiceName'></a>
### ServiceName `property`

##### Summary

Gets the service name.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-SynchronousSubmitRecordOperation-WorkType'></a>
### WorkType `property`

##### Summary

Gets the work type.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SynchronousSubmitRecordOperation-GetCustomKeyDimensions'></a>
### GetCustomKeyDimensions() `method`

##### Summary

Get custom key dimensions.

##### Returns

A Dimensions

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SynchronousSubmitRecordOperation-InnerDispose'></a>
### InnerDispose() `method`

##### Summary

Dispose

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-SynchronousSubmitRecordOperation-InnerRunAsync-System-Threading-CancellationToken-'></a>
### InnerRunAsync(cancellationToken) `method`

##### Summary

Inner the run asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') |  |

<a name='T-RecordPoint-Connectors-SDK-Context-SystemContext'></a>
## SystemContext `type`

##### Namespace

RecordPoint.Connectors.SDK.Context

##### Summary

The system context.

<a name='M-RecordPoint-Connectors-SDK-Context-SystemContext-#ctor-Microsoft-Extensions-Hosting-IHostEnvironment,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Context-SystemOptions}-'></a>
### #ctor(hostEnvironment,systemOptions) `constructor`

##### Summary

Initializes a new instance of the [SystemContext](#T-RecordPoint-Connectors-SDK-Context-SystemContext 'RecordPoint.Connectors.SDK.Context.SystemContext') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostEnvironment | [Microsoft.Extensions.Hosting.IHostEnvironment](#T-Microsoft-Extensions-Hosting-IHostEnvironment 'Microsoft.Extensions.Hosting.IHostEnvironment') | The host environment. |
| systemOptions | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Context.SystemOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Context-SystemOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Context.SystemOptions}') | The system options. |

<a name='F-RecordPoint-Connectors-SDK-Context-SystemContext-_hostEnvironment'></a>
### _hostEnvironment `constants`

##### Summary

The host environment.

<a name='F-RecordPoint-Connectors-SDK-Context-SystemContext-_systemOptions'></a>
### _systemOptions `constants`

##### Summary

The system options.

<a name='M-RecordPoint-Connectors-SDK-Context-SystemContext-GetCompanyName'></a>
### GetCompanyName() `method`

##### Summary

Get company name.

##### Returns

A string

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Context-SystemContext-GetConnectorName'></a>
### GetConnectorName() `method`

##### Summary

Get connector name.

##### Returns

A string

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Context-SystemContext-GetContentRootPath'></a>
### GetContentRootPath() `method`

##### Summary

Get content root path.

##### Returns

A string

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Context-SystemContext-GetDataRootPath'></a>
### GetDataRootPath() `method`

##### Summary

Get data root path.

##### Returns

A string

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Context-SystemContext-GetDefaultDataRootPath'></a>
### GetDefaultDataRootPath() `method`

##### Summary

Get the data root path that will be used if one is not suppplied in the options

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Context-SystemContext-GetServiceName'></a>
### GetServiceName() `method`

##### Summary

Get short name.

##### Returns

A string

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Context-SystemContext-GetShortName'></a>
### GetShortName() `method`

##### Summary

Get short name.

##### Returns

A string

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Context-SystemContextBuilderExtensions'></a>
## SystemContextBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Context

##### Summary

On cloud context builder extensions

<a name='M-RecordPoint-Connectors-SDK-Context-SystemContextBuilderExtensions-UseSystemContext-Microsoft-Extensions-Hosting-IHostBuilder,System-String,System-String,System-String,System-String-'></a>
### UseSystemContext(hostBuilder,companyName,connectorName,shortName,serviceName) `method`

##### Summary

Use the on cloud system context

##### Returns

The updated host builder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | Host builder to update |
| companyName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Optional connector company name |
| connectorName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Optional connector name |
| shortName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Connector short name |
| serviceName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Connector service name |

<a name='T-RecordPoint-Connectors-SDK-Observability-AppInsights-TelemetryClientFactory'></a>
## TelemetryClientFactory `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability.AppInsights

##### Summary

Factory to create an instance of [TelemetryClient](#T-Microsoft-ApplicationInsights-TelemetryClient 'Microsoft.ApplicationInsights.TelemetryClient') based on the configuration provided in [ApplicationInsightOptions](#T-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightOptions 'RecordPoint.Connectors.SDK.Observability.AppInsights.ApplicationInsightOptions').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| applicationInsightOptions | [T:RecordPoint.Connectors.SDK.Observability.AppInsights.TelemetryClientFactory](#T-T-RecordPoint-Connectors-SDK-Observability-AppInsights-TelemetryClientFactory 'T:RecordPoint.Connectors.SDK.Observability.AppInsights.TelemetryClientFactory') | Configuration options for the [TelemetryClient](#T-Microsoft-ApplicationInsights-TelemetryClient 'Microsoft.ApplicationInsights.TelemetryClient')/ |

<a name='M-RecordPoint-Connectors-SDK-Observability-AppInsights-TelemetryClientFactory-#ctor-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightOptions}-'></a>
### #ctor(applicationInsightOptions) `constructor`

##### Summary

Factory to create an instance of [TelemetryClient](#T-Microsoft-ApplicationInsights-TelemetryClient 'Microsoft.ApplicationInsights.TelemetryClient') based on the configuration provided in [ApplicationInsightOptions](#T-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightOptions 'RecordPoint.Connectors.SDK.Observability.AppInsights.ApplicationInsightOptions').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| applicationInsightOptions | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Observability.AppInsights.ApplicationInsightOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Observability.AppInsights.ApplicationInsightOptions}') | Configuration options for the [TelemetryClient](#T-Microsoft-ApplicationInsights-TelemetryClient 'Microsoft.ApplicationInsights.TelemetryClient')/ |

<a name='M-RecordPoint-Connectors-SDK-Observability-AppInsights-TelemetryClientFactory-GetTelemetryClient'></a>
### GetTelemetryClient() `method`

##### Summary

Creates an instance of [TelemetryClient](#T-Microsoft-ApplicationInsights-TelemetryClient 'Microsoft.ApplicationInsights.TelemetryClient') based on the configuration provided in [ApplicationInsightOptions](#T-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightOptions 'RecordPoint.Connectors.SDK.Observability.AppInsights.ApplicationInsightOptions').

##### Returns

A new instance of an Application Insights [TelemetryClient](#T-Microsoft-ApplicationInsights-TelemetryClient 'Microsoft.ApplicationInsights.TelemetryClient')/

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [RecordPoint.Connectors.SDK.RequiredValueNullException](#T-RecordPoint-Connectors-SDK-RequiredValueNullException 'RecordPoint.Connectors.SDK.RequiredValueNullException') |  |

<a name='T-RecordPoint-Connectors-SDK-Observability-TelemetryTracker'></a>
## TelemetryTracker `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability

##### Summary

Implementation of a Telemetry Tracker

<a name='M-RecordPoint-Connectors-SDK-Observability-TelemetryTracker-#ctor-RecordPoint-Connectors-SDK-Observability-IObservabilityScope,System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Observability-ITelemetrySink},RecordPoint-Connectors-SDK-Context-ISystemContext-'></a>
### #ctor() `constructor`

##### Summary

Implementation of a Telemetry Tracker

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Observability-TelemetryTracker-BeginScope-RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures-'></a>
### BeginScope() `method`

##### Summary

Begin a new Observability Scope

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Observability-TelemetryTracker-GatherDimensions-RecordPoint-Connectors-SDK-Observability-Dimensions,System-Exception-'></a>
### GatherDimensions(dimensions,exception) `method`

##### Summary

Gather the dimensions.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dimensions | [RecordPoint.Connectors.SDK.Observability.Dimensions](#T-RecordPoint-Connectors-SDK-Observability-Dimensions 'RecordPoint.Connectors.SDK.Observability.Dimensions') | The dimensions. |
| exception | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | The exception. |

<a name='M-RecordPoint-Connectors-SDK-Observability-TelemetryTracker-GatherMeasures-RecordPoint-Connectors-SDK-Observability-Measures,System-Exception-'></a>
### GatherMeasures(measures,exception) `method`

##### Summary

Gather the measures.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| measures | [RecordPoint.Connectors.SDK.Observability.Measures](#T-RecordPoint-Connectors-SDK-Observability-Measures 'RecordPoint.Connectors.SDK.Observability.Measures') | The measures. |
| exception | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | The exception. |

<a name='M-RecordPoint-Connectors-SDK-Observability-TelemetryTracker-TrackEvent-System-String,RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures-'></a>
### TrackEvent() `method`

##### Summary

Track a custom event across all registered telemetry sinks

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Observability-TelemetryTracker-TrackException-System-Exception,RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures-'></a>
### TrackException() `method`

##### Summary

Track an exception across all registered telemetry sinks

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Observability-TelemetryTracker-TrackTrace-System-String,RecordPoint-Connectors-SDK-Observability-SeverityLevel,RecordPoint-Connectors-SDK-Observability-Dimensions-'></a>
### TrackTrace() `method`

##### Summary

Track a trace message across all registered telemetry sinks

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Time-TimeBuilderExtensions'></a>
## TimeBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Time

##### Summary

Time builder extensions

<a name='M-RecordPoint-Connectors-SDK-Time-TimeBuilderExtensions-UseSystemTime-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseSystemTime(hostBuilder) `method`

##### Summary

Use the system clock to get the time

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | Host builder to target |

<a name='T-RecordPoint-Connectors-SDK-Health-UptimeStrategy'></a>
## UptimeStrategy `type`

##### Namespace

RecordPoint.Connectors.SDK.Health

##### Summary

Health Checker that determines how long the service has been running.

<a name='M-RecordPoint-Connectors-SDK-Health-UptimeStrategy-#ctor-RecordPoint-Connectors-SDK-Providers-IDateTimeProvider-'></a>
### #ctor(dateTimeProvider) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dateTimeProvider | [RecordPoint.Connectors.SDK.Providers.IDateTimeProvider](#T-RecordPoint-Connectors-SDK-Providers-IDateTimeProvider 'RecordPoint.Connectors.SDK.Providers.IDateTimeProvider') |  |

<a name='F-RecordPoint-Connectors-SDK-Health-UptimeStrategy-HEALTH_CHECK_TYPE'></a>
### HEALTH_CHECK_TYPE `constants`

##### Summary

Health Check Type for Uptime.

<a name='F-RecordPoint-Connectors-SDK-Health-UptimeStrategy-UPTIME_SECONDS_NAME'></a>
### UPTIME_SECONDS_NAME `constants`

##### Summary

Health Check Measure Type

<a name='P-RecordPoint-Connectors-SDK-Health-UptimeStrategy-HealthCheckType'></a>
### HealthCheckType `property`

##### Summary

Health Check Type for Uptime.

<a name='M-RecordPoint-Connectors-SDK-Health-UptimeStrategy-HealthCheckAsync-System-Threading-CancellationToken-'></a>
### HealthCheckAsync(stoppingToken) `method`

##### Summary

Produces a health check for the service uptime

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| stoppingToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='T-RecordPoint-Connectors-SDK-Work-WorkBase`1'></a>
## WorkBase\`1 `type`

##### Namespace

RecordPoint.Connectors.SDK.Work

##### Summary

Base implementation of unmanaged work that can be executed within the context of the executing process.
units of work.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TParameter | Work item input parameter type |

<a name='M-RecordPoint-Connectors-SDK-Work-WorkBase`1-#ctor-RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider-'></a>
### #ctor() `constructor`

##### Summary

Public constructor. Used to inject dependencies

##### Parameters

This constructor has no parameters.

<a name='P-RecordPoint-Connectors-SDK-Work-WorkBase`1-DateTimeProvider'></a>
### DateTimeProvider `property`

##### Summary

Date Time provider

<a name='P-RecordPoint-Connectors-SDK-Work-WorkBase`1-Exception'></a>
### Exception `property`

##### Summary

*Inherit from parent.*

<a name='P-RecordPoint-Connectors-SDK-Work-WorkBase`1-FinishDateTime'></a>
### FinishDateTime `property`

##### Summary

*Inherit from parent.*

<a name='P-RecordPoint-Connectors-SDK-Work-WorkBase`1-HasResult'></a>
### HasResult `property`

##### Summary

*Inherit from parent.*

<a name='P-RecordPoint-Connectors-SDK-Work-WorkBase`1-Id'></a>
### Id `property`

##### Summary

*Inherit from parent.*

<a name='P-RecordPoint-Connectors-SDK-Work-WorkBase`1-Parameter'></a>
### Parameter `property`

##### Summary

*Inherit from parent.*

<a name='P-RecordPoint-Connectors-SDK-Work-WorkBase`1-ResultReason'></a>
### ResultReason `property`

##### Summary

*Inherit from parent.*

<a name='P-RecordPoint-Connectors-SDK-Work-WorkBase`1-ResultReasonDetails'></a>
### ResultReasonDetails `property`

##### Summary

*Inherit from parent.*

<a name='P-RecordPoint-Connectors-SDK-Work-WorkBase`1-ResultType'></a>
### ResultType `property`

##### Summary

*Inherit from parent.*

<a name='P-RecordPoint-Connectors-SDK-Work-WorkBase`1-ScopeManager'></a>
### ScopeManager `property`

##### Summary

Observability scope manager

<a name='P-RecordPoint-Connectors-SDK-Work-WorkBase`1-StartDateTime'></a>
### StartDateTime `property`

##### Summary

*Inherit from parent.*

<a name='P-RecordPoint-Connectors-SDK-Work-WorkBase`1-TelemetryTracker'></a>
### TelemetryTracker `property`

##### Summary

Telemetry tracker

<a name='P-RecordPoint-Connectors-SDK-Work-WorkBase`1-WorkDuration'></a>
### WorkDuration `property`

##### Summary

*Inherit from parent.*

<a name='P-RecordPoint-Connectors-SDK-Work-WorkBase`1-WorkType'></a>
### WorkType `property`

##### Summary

*Inherit from parent.*

<a name='M-RecordPoint-Connectors-SDK-Work-WorkBase`1-BeginObservabilityScope'></a>
### BeginObservabilityScope() `method`

##### Summary

Begin the observability scope for this work item

##### Returns

Scope

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-WorkBase`1-CompleteAsync-System-String,System-Threading-CancellationToken-'></a>
### CompleteAsync() `method`

##### Summary

Record that this work item has completed

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-WorkBase`1-CompleteAsync-System-String,System-String,System-Threading-CancellationToken-'></a>
### CompleteAsync() `method`

##### Summary

Record that this work item has completed, with reason details

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-WorkBase`1-EnsureHasOutcome'></a>
### EnsureHasOutcome() `method`

##### Summary

Ensure that an outcome has been recorded

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-WorkBase`1-EnsureIncomplete'></a>
### EnsureIncomplete() `method`

##### Summary

Ensure that this work item is incomplete

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-WorkBase`1-FailAsync-System-String,System-Threading-CancellationToken-'></a>
### FailAsync(reason,cancellationToken) `method`

##### Summary

Record that this work item has failed and should be reattempted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| reason | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Reason why the work item has failed |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Work-WorkBase`1-FailAsync-System-Exception,System-Threading-CancellationToken-'></a>
### FailAsync(exception,cancellationToken) `method`

##### Summary

Record that this work item has failed due to an exception and should be reattempted

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| exception | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Exception that is cause of the failure |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Work-WorkBase`1-GetCoreKeyDimensions'></a>
### GetCoreKeyDimensions() `method`

##### Summary

Get the core key dimensions for the work

##### Returns

Key dimensions that will be included in the work items observability scope

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-WorkBase`1-GetCoreResultDimensions'></a>
### GetCoreResultDimensions() `method`

##### Summary

Get the core result dimensions.

##### Parameters

This method has no parameters.

##### Remarks

This method is intended to be overridden in base classes

<a name='M-RecordPoint-Connectors-SDK-Work-WorkBase`1-GetCoreResultMeasures'></a>
### GetCoreResultMeasures() `method`

##### Summary

Get the core result measures

##### Parameters

This method has no parameters.

##### Remarks

This method is intended to be overridden in base classes

<a name='M-RecordPoint-Connectors-SDK-Work-WorkBase`1-GetCoreStartDimensions'></a>
### GetCoreStartDimensions() `method`

##### Summary

Get the core start dimensions.

##### Parameters

This method has no parameters.

##### Remarks

This method is intended to be overridden in base classes

<a name='M-RecordPoint-Connectors-SDK-Work-WorkBase`1-GetCoreStartMeasures'></a>
### GetCoreStartMeasures() `method`

##### Summary

Get the core start measures

##### Parameters

This method has no parameters.

##### Remarks

This method is intended to be overridden in base classes

<a name='M-RecordPoint-Connectors-SDK-Work-WorkBase`1-GetCustomKeyDimensions'></a>
### GetCustomKeyDimensions() `method`

##### Summary

Get custom key dimensions for the work

##### Returns

Key dimensions that will be included in the work items observability scope

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-WorkBase`1-GetCustomResultDimensions'></a>
### GetCustomResultDimensions() `method`

##### Summary

Get result dimensions that are specific to a type of work

##### Parameters

This method has no parameters.

##### Remarks

Observability dimensions

<a name='M-RecordPoint-Connectors-SDK-Work-WorkBase`1-GetCustomResultMeasures'></a>
### GetCustomResultMeasures() `method`

##### Summary

Get result measures that are specific to a type of work

##### Parameters

This method has no parameters.

##### Remarks

Observability measures

<a name='M-RecordPoint-Connectors-SDK-Work-WorkBase`1-GetCustomStartDimensions'></a>
### GetCustomStartDimensions() `method`

##### Summary

Get custom start dimensions that are specific to a type of work

##### Parameters

This method has no parameters.

##### Remarks

Observability dimensions

<a name='M-RecordPoint-Connectors-SDK-Work-WorkBase`1-GetCustomStartMeasures'></a>
### GetCustomStartMeasures() `method`

##### Summary

Get custom start measures that are specific to a type of work

##### Parameters

This method has no parameters.

##### Remarks

Observability measures

<a name='M-RecordPoint-Connectors-SDK-Work-WorkBase`1-GetKeyDimensions'></a>
### GetKeyDimensions() `method`

##### Summary

Get the key dimensions for this work

##### Returns

All Key dimensions that will be included in the work items observability scope

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-WorkBase`1-HandleAbandonedResultAsync-System-String,System-Threading-CancellationToken-'></a>
### HandleAbandonedResultAsync() `method`

##### Summary

Record that this work item should be abandoned

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-WorkBase`1-InnerRunAsync-System-Threading-CancellationToken-'></a>
### InnerRunAsync(cancellationToken) `method`

##### Summary

Performs execution of the work logic

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation Token |

<a name='M-RecordPoint-Connectors-SDK-Work-WorkBase`1-RunAsync-`0,System-Threading-CancellationToken-'></a>
### RunAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-WorkBase`1-TrackFinish'></a>
### TrackFinish() `method`

##### Summary

Track the finish of a unit of work

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-WorkBase`1-TrackStart'></a>
### TrackStart() `method`

##### Summary

Track the start of the unit of work

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Work-WorkBuilderExtensions'></a>
## WorkBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Work

##### Summary

Host builder extensions for work management

<a name='M-RecordPoint-Connectors-SDK-Work-WorkBuilderExtensions-AddQueueableWorkOperation``1-Microsoft-Extensions-DependencyInjection-IServiceCollection-'></a>
### AddQueueableWorkOperation\`\`1(services) `method`

##### Summary

Add a work queue item for a given work type

##### Returns

Original service collection

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| services | [Microsoft.Extensions.DependencyInjection.IServiceCollection](#T-Microsoft-Extensions-DependencyInjection-IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection') | Target services collection |

<a name='M-RecordPoint-Connectors-SDK-Work-WorkBuilderExtensions-UseWorkManager-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseWorkManager(hostBuilder) `method`

##### Summary

Use the standard work manager

##### Returns

Updated host builder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | Host builder to customize |

<a name='T-RecordPoint-Connectors-SDK-ContentManager-WorkQueueClientExtensions'></a>
## WorkQueueClientExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Content work queue extensions

<a name='M-RecordPoint-Connectors-SDK-ContentManager-WorkQueueClientExtensions-DisposeRecordAsync-RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-ContentManager-ContentSubmissionConfiguration,RecordPoint-Connectors-SDK-Content-Record,System-Nullable{System-DateTimeOffset},System-Threading-CancellationToken-'></a>
### DisposeRecordAsync(workQueueClient,contentSubmissionConfiguration,record,waitTill,cancellationToken) `method`

##### Summary

Submit a record disposal operation to the work queue

##### Returns

Submission task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workQueueClient | [RecordPoint.Connectors.SDK.Work.IWorkQueueClient](#T-RecordPoint-Connectors-SDK-Work-IWorkQueueClient 'RecordPoint.Connectors.SDK.Work.IWorkQueueClient') | Work queue to submit aggregation to |
| contentSubmissionConfiguration | [RecordPoint.Connectors.SDK.ContentManager.ContentSubmissionConfiguration](#T-RecordPoint-Connectors-SDK-ContentManager-ContentSubmissionConfiguration 'RecordPoint.Connectors.SDK.ContentManager.ContentSubmissionConfiguration') | Configuration information |
| record | [RecordPoint.Connectors.SDK.Content.Record](#T-RecordPoint-Connectors-SDK-Content-Record 'RecordPoint.Connectors.SDK.Content.Record') | aggregation |
| waitTill | [System.Nullable{System.DateTimeOffset}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.DateTimeOffset}') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-WorkQueueClientExtensions-SubmitAggregationAsync-RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-ContentManager-ContentSubmissionConfiguration,RecordPoint-Connectors-SDK-Content-Aggregation,System-Nullable{System-DateTimeOffset},System-Threading-CancellationToken-'></a>
### SubmitAggregationAsync(workQueueClient,contentSubmissionConfiguration,aggregation,waitTill,cancellationToken) `method`

##### Summary

Submit an aggregation submission operation to the work queue

##### Returns

Submission task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workQueueClient | [RecordPoint.Connectors.SDK.Work.IWorkQueueClient](#T-RecordPoint-Connectors-SDK-Work-IWorkQueueClient 'RecordPoint.Connectors.SDK.Work.IWorkQueueClient') | Work queue to submit aggregation to |
| contentSubmissionConfiguration | [RecordPoint.Connectors.SDK.ContentManager.ContentSubmissionConfiguration](#T-RecordPoint-Connectors-SDK-ContentManager-ContentSubmissionConfiguration 'RecordPoint.Connectors.SDK.ContentManager.ContentSubmissionConfiguration') | Configuration information |
| aggregation | [RecordPoint.Connectors.SDK.Content.Aggregation](#T-RecordPoint-Connectors-SDK-Content-Aggregation 'RecordPoint.Connectors.SDK.Content.Aggregation') | aggregation |
| waitTill | [System.Nullable{System.DateTimeOffset}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.DateTimeOffset}') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-WorkQueueClientExtensions-SubmitAuditEventAsync-RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-ContentManager-ContentSubmissionConfiguration,RecordPoint-Connectors-SDK-Content-AuditEvent,System-Nullable{System-DateTimeOffset},System-Threading-CancellationToken-'></a>
### SubmitAuditEventAsync(workQueueClient,contentSubmissionConfiguration,auditEvent,waitTill,cancellationToken) `method`

##### Summary

Submit an audit event submission operation to the work queue

##### Returns

Submission task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workQueueClient | [RecordPoint.Connectors.SDK.Work.IWorkQueueClient](#T-RecordPoint-Connectors-SDK-Work-IWorkQueueClient 'RecordPoint.Connectors.SDK.Work.IWorkQueueClient') | Work queue to submit audit event to |
| contentSubmissionConfiguration | [RecordPoint.Connectors.SDK.ContentManager.ContentSubmissionConfiguration](#T-RecordPoint-Connectors-SDK-ContentManager-ContentSubmissionConfiguration 'RecordPoint.Connectors.SDK.ContentManager.ContentSubmissionConfiguration') | Configuration information |
| auditEvent | [RecordPoint.Connectors.SDK.Content.AuditEvent](#T-RecordPoint-Connectors-SDK-Content-AuditEvent 'RecordPoint.Connectors.SDK.Content.AuditEvent') |  |
| waitTill | [System.Nullable{System.DateTimeOffset}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.DateTimeOffset}') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-WorkQueueClientExtensions-SubmitBinaryAsync-RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-ContentManager-ContentSubmissionConfiguration,RecordPoint-Connectors-SDK-Content-BinaryMetaInfo,System-Nullable{System-DateTimeOffset},System-Threading-CancellationToken-'></a>
### SubmitBinaryAsync(workQueueClient,contentSubmissionConfiguration,binaryMetaInfo,waitTill,cancellationToken) `method`

##### Summary

Submit a binary submission operation to the work queue

##### Returns

Submission task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workQueueClient | [RecordPoint.Connectors.SDK.Work.IWorkQueueClient](#T-RecordPoint-Connectors-SDK-Work-IWorkQueueClient 'RecordPoint.Connectors.SDK.Work.IWorkQueueClient') | Work queue to submit record to |
| contentSubmissionConfiguration | [RecordPoint.Connectors.SDK.ContentManager.ContentSubmissionConfiguration](#T-RecordPoint-Connectors-SDK-ContentManager-ContentSubmissionConfiguration 'RecordPoint.Connectors.SDK.ContentManager.ContentSubmissionConfiguration') | Configuration information |
| binaryMetaInfo | [RecordPoint.Connectors.SDK.Content.BinaryMetaInfo](#T-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo 'RecordPoint.Connectors.SDK.Content.BinaryMetaInfo') |  |
| waitTill | [System.Nullable{System.DateTimeOffset}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.DateTimeOffset}') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-WorkQueueClientExtensions-SubmitRecordAsync-RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-ContentManager-ContentSubmissionConfiguration,RecordPoint-Connectors-SDK-Content-Record,System-Nullable{System-DateTimeOffset},System-Threading-CancellationToken-'></a>
### SubmitRecordAsync(workQueueClient,contentSubmissionConfiguration,record,waitTill,cancellationToken) `method`

##### Summary

Submit a record submission operation to the work queue

##### Returns

Submission task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workQueueClient | [RecordPoint.Connectors.SDK.Work.IWorkQueueClient](#T-RecordPoint-Connectors-SDK-Work-IWorkQueueClient 'RecordPoint.Connectors.SDK.Work.IWorkQueueClient') | Work queue to submit record to |
| contentSubmissionConfiguration | [RecordPoint.Connectors.SDK.ContentManager.ContentSubmissionConfiguration](#T-RecordPoint-Connectors-SDK-ContentManager-ContentSubmissionConfiguration 'RecordPoint.Connectors.SDK.ContentManager.ContentSubmissionConfiguration') | Configuration information |
| record | [RecordPoint.Connectors.SDK.Content.Record](#T-RecordPoint-Connectors-SDK-Content-Record 'RecordPoint.Connectors.SDK.Content.Record') |  |
| waitTill | [System.Nullable{System.DateTimeOffset}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.DateTimeOffset}') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |
