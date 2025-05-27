<a name='assembly'></a>
# RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus

## Contents

- [AzureServiceBusBuilderExtensions](#T-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusBuilderExtensions 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusBuilderExtensions')
  - [UseASBDeadLetterQueueService(hostBuilder)](#M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusBuilderExtensions-UseASBDeadLetterQueueService-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusBuilderExtensions.UseASBDeadLetterQueueService(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseASBWorkQueue(hostBuilder,operationTypes)](#M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusBuilderExtensions-UseASBWorkQueue-Microsoft-Extensions-Hosting-IHostBuilder,System-Collections-Generic-IList{System-Type}- 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusBuilderExtensions.UseASBWorkQueue(Microsoft.Extensions.Hosting.IHostBuilder,System.Collections.Generic.IList{System.Type})')
- [AzureServiceBusDeadLetterQueueService](#T-RecordPoint-Connectors-SDK-WebHost-Services-AzureServiceBusDeadLetterQueueService 'RecordPoint.Connectors.SDK.WebHost.Services.AzureServiceBusDeadLetterQueueService')
  - [#ctor(serviceBusClientFactory,managedWorkStatusManager)](#M-RecordPoint-Connectors-SDK-WebHost-Services-AzureServiceBusDeadLetterQueueService-#ctor-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-IServiceBusClientFactory,RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager- 'RecordPoint.Connectors.SDK.WebHost.Services.AzureServiceBusDeadLetterQueueService.#ctor(RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.IServiceBusClientFactory,RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager)')
  - [MaxMessages](#F-RecordPoint-Connectors-SDK-WebHost-Services-AzureServiceBusDeadLetterQueueService-MaxMessages 'RecordPoint.Connectors.SDK.WebHost.Services.AzureServiceBusDeadLetterQueueService.MaxMessages')
  - [DeleteAllMessagesAsync(queueName)](#M-RecordPoint-Connectors-SDK-WebHost-Services-AzureServiceBusDeadLetterQueueService-DeleteAllMessagesAsync-System-String- 'RecordPoint.Connectors.SDK.WebHost.Services.AzureServiceBusDeadLetterQueueService.DeleteAllMessagesAsync(System.String)')
  - [DeleteMessageAsync(queueName,sequenceNumber)](#M-RecordPoint-Connectors-SDK-WebHost-Services-AzureServiceBusDeadLetterQueueService-DeleteMessageAsync-System-String,System-Int64- 'RecordPoint.Connectors.SDK.WebHost.Services.AzureServiceBusDeadLetterQueueService.DeleteMessageAsync(System.String,System.Int64)')
  - [GetAllMessagesAsync(queueName)](#M-RecordPoint-Connectors-SDK-WebHost-Services-AzureServiceBusDeadLetterQueueService-GetAllMessagesAsync-System-String- 'RecordPoint.Connectors.SDK.WebHost.Services.AzureServiceBusDeadLetterQueueService.GetAllMessagesAsync(System.String)')
  - [GetMessageAsync(queueName,sequenceNumber)](#M-RecordPoint-Connectors-SDK-WebHost-Services-AzureServiceBusDeadLetterQueueService-GetMessageAsync-System-String,System-Int64- 'RecordPoint.Connectors.SDK.WebHost.Services.AzureServiceBusDeadLetterQueueService.GetMessageAsync(System.String,System.Int64)')
  - [ResubmitMessagesAsync(queueName,sequenceNumbers)](#M-RecordPoint-Connectors-SDK-WebHost-Services-AzureServiceBusDeadLetterQueueService-ResubmitMessagesAsync-System-String,System-Int64[]- 'RecordPoint.Connectors.SDK.WebHost.Services.AzureServiceBusDeadLetterQueueService.ResubmitMessagesAsync(System.String,System.Int64[])')
- [AzureServiceBusOptions](#T-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusOptions 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusOptions')
  - [SECTION_NAME](#F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusOptions-SECTION_NAME 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusOptions.SECTION_NAME')
  - [KillswitchCheckInterval](#P-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusOptions-KillswitchCheckInterval 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusOptions.KillswitchCheckInterval')
  - [MaxAutoLockRenewalDurationSeconds](#P-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusOptions-MaxAutoLockRenewalDurationSeconds 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusOptions.MaxAutoLockRenewalDurationSeconds')
  - [MaxDegreeOfParallelism](#P-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusOptions-MaxDegreeOfParallelism 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusOptions.MaxDegreeOfParallelism')
  - [QueuePrefix](#P-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusOptions-QueuePrefix 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusOptions.QueuePrefix')
  - [ServiceBusConnectionString](#P-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusOptions-ServiceBusConnectionString 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusOptions.ServiceBusConnectionString')
  - [ServiceBusName](#P-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusOptions-ServiceBusName 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusOptions.ServiceBusName')
  - [ServiceShutdownDelay](#P-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusOptions-ServiceShutdownDelay 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusOptions.ServiceShutdownDelay')
- [AzureServiceBusWorkClient](#T-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkClient 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusWorkClient')
  - [#ctor(serviceBusClientFactory,serviceBusOptions,dateTimeProvider)](#M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkClient-#ctor-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-IServiceBusClientFactory,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusOptions},RecordPoint-Connectors-SDK-Providers-IDateTimeProvider- 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusWorkClient.#ctor(RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.IServiceBusClientFactory,Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusOptions},RecordPoint.Connectors.SDK.Providers.IDateTimeProvider)')
  - [DisposeAsync()](#M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkClient-DisposeAsync 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusWorkClient.DisposeAsync')
  - [SubmitWorkAsync()](#M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkClient-SubmitWorkAsync-RecordPoint-Connectors-SDK-Work-WorkRequest,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusWorkClient.SubmitWorkAsync(RecordPoint.Connectors.SDK.Work.WorkRequest,System.Threading.CancellationToken)')
- [AzureServiceBusWorkServer](#T-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusWorkServer')
  - [#ctor(workQueueClient,serviceProvider,systemContext,workManager,serviceBusClientFactory,serviceBusOptions,observabilityScope,telemetryTracker,dateTimeProvider,toggleProvider,operationTypes,configuration)](#M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-#ctor-RecordPoint-Connectors-SDK-Work-IWorkQueueClient,System-IServiceProvider,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Work-IQueueableWorkManager,RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-IServiceBusClientFactory,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusOptions},RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider,RecordPoint-Connectors-SDK-Toggles-IToggleProvider,System-Collections-Generic-IList{System-Type},Microsoft-Extensions-Configuration-IConfiguration- 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusWorkServer.#ctor(RecordPoint.Connectors.SDK.Work.IWorkQueueClient,System.IServiceProvider,RecordPoint.Connectors.SDK.Context.ISystemContext,RecordPoint.Connectors.SDK.Work.IQueueableWorkManager,RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.IServiceBusClientFactory,Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusOptions},RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,RecordPoint.Connectors.SDK.Providers.IDateTimeProvider,RecordPoint.Connectors.SDK.Toggles.IToggleProvider,System.Collections.Generic.IList{System.Type},Microsoft.Extensions.Configuration.IConfiguration)')
  - [DefaultOperationTypes](#F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-DefaultOperationTypes 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusWorkServer.DefaultOperationTypes')
  - [_configuration](#F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-_configuration 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusWorkServer._configuration')
  - [_dateTimeProvider](#F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-_dateTimeProvider 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusWorkServer._dateTimeProvider')
  - [_observabilityScope](#F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-_observabilityScope 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusWorkServer._observabilityScope')
  - [_operationTypes](#F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-_operationTypes 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusWorkServer._operationTypes')
  - [_processingToken](#F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-_processingToken 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusWorkServer._processingToken')
  - [_serviceBusClient](#F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-_serviceBusClient 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusWorkServer._serviceBusClient')
  - [_serviceBusOptions](#F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-_serviceBusOptions 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusWorkServer._serviceBusOptions')
  - [_serviceBusProcessors](#F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-_serviceBusProcessors 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusWorkServer._serviceBusProcessors')
  - [_serviceProvider](#F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-_serviceProvider 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusWorkServer._serviceProvider')
  - [_systemContext](#F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-_systemContext 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusWorkServer._systemContext')
  - [_telemetryTracker](#F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-_telemetryTracker 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusWorkServer._telemetryTracker')
  - [_toggleProvider](#F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-_toggleProvider 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusWorkServer._toggleProvider')
  - [_workManager](#F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-_workManager 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusWorkServer._workManager')
  - [_workQueueClient](#F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-_workQueueClient 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusWorkServer._workQueueClient')
  - [ExecuteAsync(stoppingToken)](#M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-ExecuteAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusWorkServer.ExecuteAsync(System.Threading.CancellationToken)')
  - [GetMaxAutoLockRenewalDuration(workType)](#M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-GetMaxAutoLockRenewalDuration-System-String- 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusWorkServer.GetMaxAutoLockRenewalDuration(System.String)')
  - [GetMaxDegreeOfParallelism(workType)](#M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-GetMaxDegreeOfParallelism-System-String- 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusWorkServer.GetMaxDegreeOfParallelism(System.String)')
  - [StopAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-StopAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusWorkServer.StopAsync(System.Threading.CancellationToken)')
  - [ValidateOperationTypes(types)](#M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-ValidateOperationTypes-System-Collections-Generic-IList{System-Type}- 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusWorkServer.ValidateOperationTypes(System.Collections.Generic.IList{System.Type})')
- [IServiceBusClientFactory](#T-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-IServiceBusClientFactory 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.IServiceBusClientFactory')
  - [CreateServiceBusAdministrationClient()](#M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-IServiceBusClientFactory-CreateServiceBusAdministrationClient 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.IServiceBusClientFactory.CreateServiceBusAdministrationClient')
  - [CreateServiceBusClient()](#M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-IServiceBusClientFactory-CreateServiceBusClient 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.IServiceBusClientFactory.CreateServiceBusClient')
- [ServiceBusClientFactory](#T-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-ServiceBusClientFactory 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.ServiceBusClientFactory')
  - [#ctor(configuration,serviceBusOptions)](#M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-ServiceBusClientFactory-#ctor-Microsoft-Extensions-Configuration-IConfiguration,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusOptions}- 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.ServiceBusClientFactory.#ctor(Microsoft.Extensions.Configuration.IConfiguration,Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusOptions})')
  - [CreateServiceBusAdministrationClient()](#M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-ServiceBusClientFactory-CreateServiceBusAdministrationClient 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.ServiceBusClientFactory.CreateServiceBusAdministrationClient')
  - [CreateServiceBusClient()](#M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-ServiceBusClientFactory-CreateServiceBusClient 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.ServiceBusClientFactory.CreateServiceBusClient')
- [ServiceBusReceivedMessageExtentions](#T-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-Extensions-ServiceBusReceivedMessageExtentions 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.Extensions.ServiceBusReceivedMessageExtentions')
  - [ToDeadLetterModel(source)](#M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-Extensions-ServiceBusReceivedMessageExtentions-ToDeadLetterModel-Azure-Messaging-ServiceBus-ServiceBusReceivedMessage- 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.Extensions.ServiceBusReceivedMessageExtentions.ToDeadLetterModel(Azure.Messaging.ServiceBus.ServiceBusReceivedMessage)')

<a name='T-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusBuilderExtensions'></a>
## AzureServiceBusBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus

##### Summary

The azure service bus builder extensions.

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusBuilderExtensions-UseASBDeadLetterQueueService-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseASBDeadLetterQueueService(hostBuilder) `method`

##### Summary

Use ASB dead letter queue service.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusBuilderExtensions-UseASBWorkQueue-Microsoft-Extensions-Hosting-IHostBuilder,System-Collections-Generic-IList{System-Type}-'></a>
### UseASBWorkQueue(hostBuilder,operationTypes) `method`

##### Summary

Use ASB work queue.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |
| operationTypes | [System.Collections.Generic.IList{System.Type}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IList 'System.Collections.Generic.IList{System.Type}') | Optional list of operation types to register for use by AzureServiceBusWorkServer |

<a name='T-RecordPoint-Connectors-SDK-WebHost-Services-AzureServiceBusDeadLetterQueueService'></a>
## AzureServiceBusDeadLetterQueueService `type`

##### Namespace

RecordPoint.Connectors.SDK.WebHost.Services

##### Summary

Deadletter queue service class

<a name='M-RecordPoint-Connectors-SDK-WebHost-Services-AzureServiceBusDeadLetterQueueService-#ctor-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-IServiceBusClientFactory,RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager-'></a>
### #ctor(serviceBusClientFactory,managedWorkStatusManager) `constructor`

##### Summary

Constructor

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| serviceBusClientFactory | [RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.IServiceBusClientFactory](#T-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-IServiceBusClientFactory 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.IServiceBusClientFactory') |  |
| managedWorkStatusManager | [RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager](#T-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager 'RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager') |  |

<a name='F-RecordPoint-Connectors-SDK-WebHost-Services-AzureServiceBusDeadLetterQueueService-MaxMessages'></a>
### MaxMessages `constants`

##### Summary

Maximum number of messages from the queue

<a name='M-RecordPoint-Connectors-SDK-WebHost-Services-AzureServiceBusDeadLetterQueueService-DeleteAllMessagesAsync-System-String-'></a>
### DeleteAllMessagesAsync(queueName) `method`

##### Summary

Delete the all messages from the deadletter queue

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| queueName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-WebHost-Services-AzureServiceBusDeadLetterQueueService-DeleteMessageAsync-System-String,System-Int64-'></a>
### DeleteMessageAsync(queueName,sequenceNumber) `method`

##### Summary

Delete the message from the deadletter queue

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| queueName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| sequenceNumber | [System.Int64](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int64 'System.Int64') |  |

<a name='M-RecordPoint-Connectors-SDK-WebHost-Services-AzureServiceBusDeadLetterQueueService-GetAllMessagesAsync-System-String-'></a>
### GetAllMessagesAsync(queueName) `method`

##### Summary

Get all messages based on the queue

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| queueName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-WebHost-Services-AzureServiceBusDeadLetterQueueService-GetMessageAsync-System-String,System-Int64-'></a>
### GetMessageAsync(queueName,sequenceNumber) `method`

##### Summary

Get message based on the queue and sequenceNumber

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| queueName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| sequenceNumber | [System.Int64](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int64 'System.Int64') |  |

<a name='M-RecordPoint-Connectors-SDK-WebHost-Services-AzureServiceBusDeadLetterQueueService-ResubmitMessagesAsync-System-String,System-Int64[]-'></a>
### ResubmitMessagesAsync(queueName,sequenceNumbers) `method`

##### Summary

Resubmit to queue based on the queueName and sequenceNumbers

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| queueName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| sequenceNumbers | [System.Int64[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int64[] 'System.Int64[]') |  |

<a name='T-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusOptions'></a>
## AzureServiceBusOptions `type`

##### Namespace

RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus

##### Summary

The azure service bus options.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusOptions-SECTION_NAME'></a>
### SECTION_NAME `constants`

##### Summary

The SECTION NAME.

<a name='P-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusOptions-KillswitchCheckInterval'></a>
### KillswitchCheckInterval `property`

##### Summary

The amount of time in milliseconds to delay between checking the killswitch

<a name='P-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusOptions-MaxAutoLockRenewalDurationSeconds'></a>
### MaxAutoLockRenewalDurationSeconds `property`

##### Summary

Gets or sets the max auto lock renewal duration seconds.

<a name='P-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusOptions-MaxDegreeOfParallelism'></a>
### MaxDegreeOfParallelism `property`

##### Summary

Gets or sets the max degree of parallelism.

<a name='P-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusOptions-QueuePrefix'></a>
### QueuePrefix `property`

##### Summary

Gets or sets the queue prefix.

<a name='P-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusOptions-ServiceBusConnectionString'></a>
### ServiceBusConnectionString `property`

##### Summary

Gets or sets the service bus connection string.

<a name='P-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusOptions-ServiceBusName'></a>
### ServiceBusName `property`

##### Summary

Gets or sets the service bus name.

<a name='P-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusOptions-ServiceShutdownDelay'></a>
### ServiceShutdownDelay `property`

##### Summary

The number of milliseconds to delay to allow in-process operations to complete before exiting the host.

<a name='T-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkClient'></a>
## AzureServiceBusWorkClient `type`

##### Namespace

RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus

##### Summary

Implementation of an IWorkQueueClient that utilises Azure Service Bus as the underlying Queue

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkClient-#ctor-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-IServiceBusClientFactory,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusOptions},RecordPoint-Connectors-SDK-Providers-IDateTimeProvider-'></a>
### #ctor(serviceBusClientFactory,serviceBusOptions,dateTimeProvider) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| serviceBusClientFactory | [RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.IServiceBusClientFactory](#T-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-IServiceBusClientFactory 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.IServiceBusClientFactory') |  |
| serviceBusOptions | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusOptions}') |  |
| dateTimeProvider | [RecordPoint.Connectors.SDK.Providers.IDateTimeProvider](#T-RecordPoint-Connectors-SDK-Providers-IDateTimeProvider 'RecordPoint.Connectors.SDK.Providers.IDateTimeProvider') |  |

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkClient-DisposeAsync'></a>
### DisposeAsync() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkClient-SubmitWorkAsync-RecordPoint-Connectors-SDK-Work-WorkRequest,System-Threading-CancellationToken-'></a>
### SubmitWorkAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer'></a>
## AzureServiceBusWorkServer `type`

##### Namespace

RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus

##### Summary

The azure service bus work server.

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-#ctor-RecordPoint-Connectors-SDK-Work-IWorkQueueClient,System-IServiceProvider,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Work-IQueueableWorkManager,RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-IServiceBusClientFactory,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusOptions},RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider,RecordPoint-Connectors-SDK-Toggles-IToggleProvider,System-Collections-Generic-IList{System-Type},Microsoft-Extensions-Configuration-IConfiguration-'></a>
### #ctor(workQueueClient,serviceProvider,systemContext,workManager,serviceBusClientFactory,serviceBusOptions,observabilityScope,telemetryTracker,dateTimeProvider,toggleProvider,operationTypes,configuration) `constructor`

##### Summary

Initializes a new instance of the [AzureServiceBusWorkServer](#T-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusWorkServer') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workQueueClient | [RecordPoint.Connectors.SDK.Work.IWorkQueueClient](#T-RecordPoint-Connectors-SDK-Work-IWorkQueueClient 'RecordPoint.Connectors.SDK.Work.IWorkQueueClient') | The work queue client. |
| serviceProvider | [System.IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider') | The service provider. |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |
| workManager | [RecordPoint.Connectors.SDK.Work.IQueueableWorkManager](#T-RecordPoint-Connectors-SDK-Work-IQueueableWorkManager 'RecordPoint.Connectors.SDK.Work.IQueueableWorkManager') | The work manager. |
| serviceBusClientFactory | [RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.IServiceBusClientFactory](#T-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-IServiceBusClientFactory 'RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.IServiceBusClientFactory') | The service bus client factory. |
| serviceBusOptions | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusOptions}') | The service bus options. |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | The scope manager. |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The telemetry tracker. |
| dateTimeProvider | [RecordPoint.Connectors.SDK.Providers.IDateTimeProvider](#T-RecordPoint-Connectors-SDK-Providers-IDateTimeProvider 'RecordPoint.Connectors.SDK.Providers.IDateTimeProvider') | The date time provider. |
| toggleProvider | [RecordPoint.Connectors.SDK.Toggles.IToggleProvider](#T-RecordPoint-Connectors-SDK-Toggles-IToggleProvider 'RecordPoint.Connectors.SDK.Toggles.IToggleProvider') | The toggle provider. |
| operationTypes | [System.Collections.Generic.IList{System.Type}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IList 'System.Collections.Generic.IList{System.Type}') | An optional list of operation types to create service bus processors for |
| configuration | [Microsoft.Extensions.Configuration.IConfiguration](#T-Microsoft-Extensions-Configuration-IConfiguration 'Microsoft.Extensions.Configuration.IConfiguration') | The configuration. |

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-DefaultOperationTypes'></a>
### DefaultOperationTypes `constants`

##### Summary

Default list of Operation types to be used if none are provided

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-_configuration'></a>
### _configuration `constants`

##### Summary

The configuration.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-_dateTimeProvider'></a>
### _dateTimeProvider `constants`

##### Summary

The date time provider.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-_observabilityScope'></a>
### _observabilityScope `constants`

##### Summary

The scope manager.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-_operationTypes'></a>
### _operationTypes `constants`

##### Summary

Operation types to be used when creating Service Bus Processors

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-_processingToken'></a>
### _processingToken `constants`

##### Summary

Processing token.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-_serviceBusClient'></a>
### _serviceBusClient `constants`

##### Summary

The service bus client.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-_serviceBusOptions'></a>
### _serviceBusOptions `constants`

##### Summary

The service bus options.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-_serviceBusProcessors'></a>
### _serviceBusProcessors `constants`

##### Summary

The service bus processors.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-_serviceProvider'></a>
### _serviceProvider `constants`

##### Summary

The service provider.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-_systemContext'></a>
### _systemContext `constants`

##### Summary

The system context.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-_telemetryTracker'></a>
### _telemetryTracker `constants`

##### Summary

The telemetry tracker.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-_toggleProvider'></a>
### _toggleProvider `constants`

##### Summary

The toggle provider.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-_workManager'></a>
### _workManager `constants`

##### Summary

Work manager.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-_workQueueClient'></a>
### _workQueueClient `constants`

##### Summary

Work queue client.

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-ExecuteAsync-System-Threading-CancellationToken-'></a>
### ExecuteAsync(stoppingToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| stoppingToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-GetMaxAutoLockRenewalDuration-System-String-'></a>
### GetMaxAutoLockRenewalDuration(workType) `method`

##### Summary

Gets the Max Auto Lock Renewal Duration for the given work type.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-GetMaxDegreeOfParallelism-System-String-'></a>
### GetMaxDegreeOfParallelism(workType) `method`

##### Summary

Gets the Max degree of parallelism for the given work type.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-StopAsync-System-Threading-CancellationToken-'></a>
### StopAsync(cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusWorkServer-ValidateOperationTypes-System-Collections-Generic-IList{System-Type}-'></a>
### ValidateOperationTypes(types) `method`

##### Summary

Ensures a default list of types is used if the provided list is empty.
Also validates that the List of types provided all implement IQueueableWork,
throws if it does not.

##### Returns

List of types of IQueueableWork

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| types | [System.Collections.Generic.IList{System.Type}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IList 'System.Collections.Generic.IList{System.Type}') | List of types to validate |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentException 'System.ArgumentException') |  |

<a name='T-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-IServiceBusClientFactory'></a>
## IServiceBusClientFactory `type`

##### Namespace

RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus

##### Summary



<a name='M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-IServiceBusClientFactory-CreateServiceBusAdministrationClient'></a>
### CreateServiceBusAdministrationClient() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-IServiceBusClientFactory-CreateServiceBusClient'></a>
### CreateServiceBusClient() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-ServiceBusClientFactory'></a>
## ServiceBusClientFactory `type`

##### Namespace

RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus

##### Summary

A factory that creates a ServiceBusClient

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-ServiceBusClientFactory-#ctor-Microsoft-Extensions-Configuration-IConfiguration,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusOptions}-'></a>
### #ctor(configuration,serviceBusOptions) `constructor`

##### Summary

Constructor for the Factory

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| configuration | [Microsoft.Extensions.Configuration.IConfiguration](#T-Microsoft-Extensions-Configuration-IConfiguration 'Microsoft.Extensions.Configuration.IConfiguration') |  |
| serviceBusOptions | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-AzureServiceBusOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.AzureServiceBusOptions}') |  |

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-ServiceBusClientFactory-CreateServiceBusAdministrationClient'></a>
### CreateServiceBusAdministrationClient() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-ServiceBusClientFactory-CreateServiceBusClient'></a>
### CreateServiceBusClient() `method`

##### Summary

Creates an instance of a ServiceBusClient

##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-Extensions-ServiceBusReceivedMessageExtentions'></a>
## ServiceBusReceivedMessageExtentions `type`

##### Namespace

RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.Extensions

##### Summary

ServiceBus Received Message Extentions

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-AzureServiceBus-Extensions-ServiceBusReceivedMessageExtentions-ToDeadLetterModel-Azure-Messaging-ServiceBus-ServiceBusReceivedMessage-'></a>
### ToDeadLetterModel(source) `method`

##### Summary

Deadletter Extentions Model

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| source | [Azure.Messaging.ServiceBus.ServiceBusReceivedMessage](#T-Azure-Messaging-ServiceBus-ServiceBusReceivedMessage 'Azure.Messaging.ServiceBus.ServiceBusReceivedMessage') |  |
