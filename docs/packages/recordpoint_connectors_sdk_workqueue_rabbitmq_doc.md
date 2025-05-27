<a name='assembly'></a>
# RecordPoint.Connectors.SDK.WorkQueue.RabbitMq

## Contents

- [IRabbitMqClientFactory](#T-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-IRabbitMqClientFactory 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.IRabbitMqClientFactory')
  - [CreateRabbitMqConnection()](#M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-IRabbitMqClientFactory-CreateRabbitMqConnection 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.IRabbitMqClientFactory.CreateRabbitMqConnection')
- [QueueNameHelper](#T-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-QueueNameHelper 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.QueueNameHelper')
  - [DeadLetterSuffix](#F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-QueueNameHelper-DeadLetterSuffix 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.QueueNameHelper.DeadLetterSuffix')
  - [GetDLQueueName(workType,queuePrefix)](#M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-QueueNameHelper-GetDLQueueName-System-String,System-String- 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.QueueNameHelper.GetDLQueueName(System.String,System.String)')
  - [GetQueueName(workType,queuePrefix)](#M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-QueueNameHelper-GetQueueName-System-String,System-String- 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.QueueNameHelper.GetQueueName(System.String,System.String)')
- [RabbitMqBuilderExtensions](#T-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqBuilderExtensions 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqBuilderExtensions')
  - [UseRabbitMqDeadLetterQueueService(hostBuilder)](#M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqBuilderExtensions-UseRabbitMqDeadLetterQueueService-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqBuilderExtensions.UseRabbitMqDeadLetterQueueService(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseRabbitMqWorkQueue(hostBuilder)](#M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqBuilderExtensions-UseRabbitMqWorkQueue-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqBuilderExtensions.UseRabbitMqWorkQueue(Microsoft.Extensions.Hosting.IHostBuilder)')
- [RabbitMqClientFactory](#T-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqClientFactory 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqClientFactory')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqClientFactory-#ctor-Microsoft-Extensions-Configuration-IConfiguration,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqOptions}- 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqClientFactory.#ctor(Microsoft.Extensions.Configuration.IConfiguration,Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqOptions})')
  - [CreateRabbitMqConnection()](#M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqClientFactory-CreateRabbitMqConnection 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqClientFactory.CreateRabbitMqConnection')
- [RabbitMqDeadLetterQueueService](#T-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqDeadLetterQueueService 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqDeadLetterQueueService')
  - [#ctor(rabbitMqClientFactory,managedWorkStatusManager,dateTimeProvider)](#M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqDeadLetterQueueService-#ctor-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-IRabbitMqClientFactory,RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider- 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqDeadLetterQueueService.#ctor(RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.IRabbitMqClientFactory,RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager,RecordPoint.Connectors.SDK.Providers.IDateTimeProvider)')
  - [DeleteAllMessagesAsync(queueName)](#M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqDeadLetterQueueService-DeleteAllMessagesAsync-System-String- 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqDeadLetterQueueService.DeleteAllMessagesAsync(System.String)')
  - [DeleteMessageAsync(queueName,sequenceNumber)](#M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqDeadLetterQueueService-DeleteMessageAsync-System-String,System-Int64- 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqDeadLetterQueueService.DeleteMessageAsync(System.String,System.Int64)')
  - [GetAllMessagesAsync(queueName)](#M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqDeadLetterQueueService-GetAllMessagesAsync-System-String- 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqDeadLetterQueueService.GetAllMessagesAsync(System.String)')
  - [GetMessageAsync(queueName,sequenceNumber)](#M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqDeadLetterQueueService-GetMessageAsync-System-String,System-Int64- 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqDeadLetterQueueService.GetMessageAsync(System.String,System.Int64)')
  - [ResubmitMessagesAsync(queueName,sequenceNumbers)](#M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqDeadLetterQueueService-ResubmitMessagesAsync-System-String,System-Int64[]- 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqDeadLetterQueueService.ResubmitMessagesAsync(System.String,System.Int64[])')
- [RabbitMqOptions](#T-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqOptions 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqOptions')
  - [SECTION_NAME](#F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqOptions-SECTION_NAME 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqOptions.SECTION_NAME')
  - [HostName](#P-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqOptions-HostName 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqOptions.HostName')
  - [HostPassword](#P-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqOptions-HostPassword 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqOptions.HostPassword')
  - [HostUserName](#P-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqOptions-HostUserName 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqOptions.HostUserName')
  - [KillswitchCheckInterval](#P-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqOptions-KillswitchCheckInterval 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqOptions.KillswitchCheckInterval')
  - [MaxDegreeOfParallelism](#P-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqOptions-MaxDegreeOfParallelism 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqOptions.MaxDegreeOfParallelism')
  - [QueuePrefix](#P-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqOptions-QueuePrefix 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqOptions.QueuePrefix')
  - [ServiceShutdownDelay](#P-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqOptions-ServiceShutdownDelay 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqOptions.ServiceShutdownDelay')
- [RabbitMqProcessModel](#T-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqProcessModel 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqProcessModel')
  - [RabbitMqEventingBasicConsumer](#P-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqProcessModel-RabbitMqEventingBasicConsumer 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqProcessModel.RabbitMqEventingBasicConsumer')
  - [RabbitMqModel](#P-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqProcessModel-RabbitMqModel 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqProcessModel.RabbitMqModel')
- [RabbitMqReceivedMessageExtensions](#T-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqReceivedMessageExtensions 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqReceivedMessageExtensions')
  - [DeadLetterReasonKey](#F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqReceivedMessageExtensions-DeadLetterReasonKey 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqReceivedMessageExtensions.DeadLetterReasonKey')
  - [ToDeadLetterModel()](#M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqReceivedMessageExtensions-ToDeadLetterModel-RabbitMQ-Client-BasicGetResult- 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqReceivedMessageExtensions.ToDeadLetterModel(RabbitMQ.Client.BasicGetResult)')
- [RabbitMqWorkClient](#T-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkClient 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkClient')
  - [#ctor(rabbitMqClientFactory,rabbitMqOptions,dateTimeProvider)](#M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkClient-#ctor-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-IRabbitMqClientFactory,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqOptions},RecordPoint-Connectors-SDK-Providers-IDateTimeProvider- 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkClient.#ctor(RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.IRabbitMqClientFactory,Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqOptions},RecordPoint.Connectors.SDK.Providers.IDateTimeProvider)')
  - [DisposeAsync()](#M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkClient-DisposeAsync 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkClient.DisposeAsync')
  - [SubmitWorkAsync()](#M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkClient-SubmitWorkAsync-RecordPoint-Connectors-SDK-Work-WorkRequest,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkClient.SubmitWorkAsync(RecordPoint.Connectors.SDK.Work.WorkRequest,System.Threading.CancellationToken)')
- [RabbitMqWorkServer](#T-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkServer')
  - [#ctor(workQueueClient,serviceProvider,systemContext,workManager,rabbitMqClientFactory,rabbitMqOptions,observabilityScope,telemetryTracker,dateTimeProvider,toggleProvider,operationTypes)](#M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-#ctor-RecordPoint-Connectors-SDK-Work-IWorkQueueClient,System-IServiceProvider,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Work-IQueueableWorkManager,RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-IRabbitMqClientFactory,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqOptions},RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider,RecordPoint-Connectors-SDK-Toggles-IToggleProvider,System-Collections-Generic-IList{System-Type}- 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkServer.#ctor(RecordPoint.Connectors.SDK.Work.IWorkQueueClient,System.IServiceProvider,RecordPoint.Connectors.SDK.Context.ISystemContext,RecordPoint.Connectors.SDK.Work.IQueueableWorkManager,RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.IRabbitMqClientFactory,Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqOptions},RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,RecordPoint.Connectors.SDK.Providers.IDateTimeProvider,RecordPoint.Connectors.SDK.Toggles.IToggleProvider,System.Collections.Generic.IList{System.Type})')
  - [DeadletterExchange](#F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-DeadletterExchange 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkServer.DeadletterExchange')
  - [DeadletterExchangeType](#F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-DeadletterExchangeType 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkServer.DeadletterExchangeType')
  - [DefaultOperationTypes](#F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-DefaultOperationTypes 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkServer.DefaultOperationTypes')
  - [ExchangeName](#F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-ExchangeName 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkServer.ExchangeName')
  - [ExchangeType](#F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-ExchangeType 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkServer.ExchangeType')
  - [_dateTimeProvider](#F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-_dateTimeProvider 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkServer._dateTimeProvider')
  - [_observabilityScope](#F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-_observabilityScope 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkServer._observabilityScope')
  - [_operationTypes](#F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-_operationTypes 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkServer._operationTypes')
  - [_processingToken](#F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-_processingToken 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkServer._processingToken')
  - [_rabbitMqConnection](#F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-_rabbitMqConnection 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkServer._rabbitMqConnection')
  - [_rabbitMqOptions](#F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-_rabbitMqOptions 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkServer._rabbitMqOptions')
  - [_rabbitMqProcessors](#F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-_rabbitMqProcessors 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkServer._rabbitMqProcessors')
  - [_serviceProvider](#F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-_serviceProvider 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkServer._serviceProvider')
  - [_systemContext](#F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-_systemContext 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkServer._systemContext')
  - [_telemetryTracker](#F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-_telemetryTracker 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkServer._telemetryTracker')
  - [_toggleProvider](#F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-_toggleProvider 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkServer._toggleProvider')
  - [_workManager](#F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-_workManager 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkServer._workManager')
  - [_workQueueClient](#F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-_workQueueClient 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkServer._workQueueClient')
  - [Dispose()](#M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-Dispose 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkServer.Dispose')
  - [ExecuteAsync(stoppingToken)](#M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-ExecuteAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkServer.ExecuteAsync(System.Threading.CancellationToken)')
  - [StopAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-StopAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkServer.StopAsync(System.Threading.CancellationToken)')
  - [ValidateOperationTypes(types)](#M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-ValidateOperationTypes-System-Collections-Generic-IList{System-Type}- 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkServer.ValidateOperationTypes(System.Collections.Generic.IList{System.Type})')

<a name='T-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-IRabbitMqClientFactory'></a>
## IRabbitMqClientFactory `type`

##### Namespace

RecordPoint.Connectors.SDK.WorkQueue.RabbitMq

##### Summary



<a name='M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-IRabbitMqClientFactory-CreateRabbitMqConnection'></a>
### CreateRabbitMqConnection() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-QueueNameHelper'></a>
## QueueNameHelper `type`

##### Namespace

RecordPoint.Connectors.SDK.WorkQueue.RabbitMq

##### Summary

The queue name helper.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-QueueNameHelper-DeadLetterSuffix'></a>
### DeadLetterSuffix `constants`

##### Summary

Suffix used on dead-letter queue names.

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-QueueNameHelper-GetDLQueueName-System-String,System-String-'></a>
### GetDLQueueName(workType,queuePrefix) `method`

##### Summary

Get DL queue name.

##### Returns

A string

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The work type. |
| queuePrefix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The queue prefix. |

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-QueueNameHelper-GetQueueName-System-String,System-String-'></a>
### GetQueueName(workType,queuePrefix) `method`

##### Summary

Get queue name.

##### Returns

A string

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The work type. |
| queuePrefix | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The queue prefix. |

<a name='T-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqBuilderExtensions'></a>
## RabbitMqBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.WorkQueue.RabbitMq

##### Summary

The rabbit mq builder extensions.

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqBuilderExtensions-UseRabbitMqDeadLetterQueueService-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseRabbitMqDeadLetterQueueService(hostBuilder) `method`

##### Summary

Use rabbit mq dead letter queue service.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqBuilderExtensions-UseRabbitMqWorkQueue-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseRabbitMqWorkQueue(hostBuilder) `method`

##### Summary

Use rabbit mq work queue.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |

<a name='T-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqClientFactory'></a>
## RabbitMqClientFactory `type`

##### Namespace

RecordPoint.Connectors.SDK.WorkQueue.RabbitMq

##### Summary

A factory that creates a RabbitMqClient

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqClientFactory-#ctor-Microsoft-Extensions-Configuration-IConfiguration,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqOptions}-'></a>
### #ctor() `constructor`

##### Summary

Constructor for the Factory

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqClientFactory-CreateRabbitMqConnection'></a>
### CreateRabbitMqConnection() `method`

##### Summary

Creates an instance of a RabbitMqClient

##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqDeadLetterQueueService'></a>
## RabbitMqDeadLetterQueueService `type`

##### Namespace

RecordPoint.Connectors.SDK.WorkQueue.RabbitMq

##### Summary

Deadletter queue service class

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqDeadLetterQueueService-#ctor-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-IRabbitMqClientFactory,RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider-'></a>
### #ctor(rabbitMqClientFactory,managedWorkStatusManager,dateTimeProvider) `constructor`

##### Summary

Constructor

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| rabbitMqClientFactory | [RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.IRabbitMqClientFactory](#T-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-IRabbitMqClientFactory 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.IRabbitMqClientFactory') |  |
| managedWorkStatusManager | [RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager](#T-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager 'RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager') |  |
| dateTimeProvider | [RecordPoint.Connectors.SDK.Providers.IDateTimeProvider](#T-RecordPoint-Connectors-SDK-Providers-IDateTimeProvider 'RecordPoint.Connectors.SDK.Providers.IDateTimeProvider') |  |

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqDeadLetterQueueService-DeleteAllMessagesAsync-System-String-'></a>
### DeleteAllMessagesAsync(queueName) `method`

##### Summary

Delete all messages from the dead-letter queue.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| queueName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqDeadLetterQueueService-DeleteMessageAsync-System-String,System-Int64-'></a>
### DeleteMessageAsync(queueName,sequenceNumber) `method`

##### Summary

Delete a specific message from the dead-letter queue

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| queueName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| sequenceNumber | [System.Int64](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int64 'System.Int64') |  |

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqDeadLetterQueueService-GetAllMessagesAsync-System-String-'></a>
### GetAllMessagesAsync(queueName) `method`

##### Summary

Get all messages based on the queue

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| queueName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqDeadLetterQueueService-GetMessageAsync-System-String,System-Int64-'></a>
### GetMessageAsync(queueName,sequenceNumber) `method`

##### Summary

Get message based on the queue and sequenceNumber

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| queueName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| sequenceNumber | [System.Int64](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int64 'System.Int64') |  |

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqDeadLetterQueueService-ResubmitMessagesAsync-System-String,System-Int64[]-'></a>
### ResubmitMessagesAsync(queueName,sequenceNumbers) `method`

##### Summary

Resubmit to queue based on the queueName and sequenceNumbers

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| queueName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| sequenceNumbers | [System.Int64[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int64[] 'System.Int64[]') |  |

<a name='T-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqOptions'></a>
## RabbitMqOptions `type`

##### Namespace

RecordPoint.Connectors.SDK.WorkQueue.RabbitMq

##### Summary

The rabbit mq options.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqOptions-SECTION_NAME'></a>
### SECTION_NAME `constants`

##### Summary

The SECTION NAME.

<a name='P-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqOptions-HostName'></a>
### HostName `property`

##### Summary

HostName for RabbitMq server

<a name='P-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqOptions-HostPassword'></a>
### HostPassword `property`

##### Summary

HostPassword password for RabbitMq server

<a name='P-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqOptions-HostUserName'></a>
### HostUserName `property`

##### Summary

HostUserName username for RabbitMq server

<a name='P-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqOptions-KillswitchCheckInterval'></a>
### KillswitchCheckInterval `property`

##### Summary

The amount of time in milliseconds to delay between checking the killswitch

<a name='P-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqOptions-MaxDegreeOfParallelism'></a>
### MaxDegreeOfParallelism `property`

##### Summary

PrefetchCount (degree of parallelism) to be used with RabbitMq queue.

##### Remarks

If not supplied, RabbitMq will feed out as many messages
as possible according to the current available system resources.

<a name='P-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqOptions-QueuePrefix'></a>
### QueuePrefix `property`

##### Summary

Gets or sets the queue prefix.

<a name='P-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqOptions-ServiceShutdownDelay'></a>
### ServiceShutdownDelay `property`

##### Summary

The number of milliseconds to delay to allow in-process operations to complete before exiting the host.

<a name='T-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqProcessModel'></a>
## RabbitMqProcessModel `type`

##### Namespace

RecordPoint.Connectors.SDK.WorkQueue.RabbitMq

##### Summary

The rabbit mq process model.

<a name='P-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqProcessModel-RabbitMqEventingBasicConsumer'></a>
### RabbitMqEventingBasicConsumer `property`

##### Summary

Gets or sets the rabbit mq eventing basic consumer.

<a name='P-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqProcessModel-RabbitMqModel'></a>
### RabbitMqModel `property`

##### Summary

Gets or sets the rabbit mq model.

<a name='T-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqReceivedMessageExtensions'></a>
## RabbitMqReceivedMessageExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.WorkQueue.RabbitMq

##### Summary

ServiceBus Received Message Extensions

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqReceivedMessageExtensions-DeadLetterReasonKey'></a>
### DeadLetterReasonKey `constants`

##### Summary

DeadLetterReasonKey key used for deadletter reason

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqReceivedMessageExtensions-ToDeadLetterModel-RabbitMQ-Client-BasicGetResult-'></a>
### ToDeadLetterModel() `method`

##### Summary

Deadletter extensions model

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkClient'></a>
## RabbitMqWorkClient `type`

##### Namespace

RecordPoint.Connectors.SDK.WorkQueue.RabbitMq

##### Summary

Implementation of an IWorkQueueClient that utilises RabbitMq as the underlying Queue

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkClient-#ctor-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-IRabbitMqClientFactory,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqOptions},RecordPoint-Connectors-SDK-Providers-IDateTimeProvider-'></a>
### #ctor(rabbitMqClientFactory,rabbitMqOptions,dateTimeProvider) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| rabbitMqClientFactory | [RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.IRabbitMqClientFactory](#T-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-IRabbitMqClientFactory 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.IRabbitMqClientFactory') |  |
| rabbitMqOptions | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqOptions}') |  |
| dateTimeProvider | [RecordPoint.Connectors.SDK.Providers.IDateTimeProvider](#T-RecordPoint-Connectors-SDK-Providers-IDateTimeProvider 'RecordPoint.Connectors.SDK.Providers.IDateTimeProvider') |  |

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkClient-DisposeAsync'></a>
### DisposeAsync() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkClient-SubmitWorkAsync-RecordPoint-Connectors-SDK-Work-WorkRequest,System-Threading-CancellationToken-'></a>
### SubmitWorkAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer'></a>
## RabbitMqWorkServer `type`

##### Namespace

RecordPoint.Connectors.SDK.WorkQueue.RabbitMq

##### Summary

The rabbit mq work server.

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-#ctor-RecordPoint-Connectors-SDK-Work-IWorkQueueClient,System-IServiceProvider,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Work-IQueueableWorkManager,RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-IRabbitMqClientFactory,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqOptions},RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider,RecordPoint-Connectors-SDK-Toggles-IToggleProvider,System-Collections-Generic-IList{System-Type}-'></a>
### #ctor(workQueueClient,serviceProvider,systemContext,workManager,rabbitMqClientFactory,rabbitMqOptions,observabilityScope,telemetryTracker,dateTimeProvider,toggleProvider,operationTypes) `constructor`

##### Summary

Initializes a new instance of the [RabbitMqWorkServer](#T-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqWorkServer') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workQueueClient | [RecordPoint.Connectors.SDK.Work.IWorkQueueClient](#T-RecordPoint-Connectors-SDK-Work-IWorkQueueClient 'RecordPoint.Connectors.SDK.Work.IWorkQueueClient') | The work queue client. |
| serviceProvider | [System.IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider') | The service provider. |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |
| workManager | [RecordPoint.Connectors.SDK.Work.IQueueableWorkManager](#T-RecordPoint-Connectors-SDK-Work-IQueueableWorkManager 'RecordPoint.Connectors.SDK.Work.IQueueableWorkManager') | The work manager. |
| rabbitMqClientFactory | [RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.IRabbitMqClientFactory](#T-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-IRabbitMqClientFactory 'RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.IRabbitMqClientFactory') | The rabbit mq client factory. |
| rabbitMqOptions | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.RabbitMqOptions}') | The rabbit mq options. |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | The scope manager. |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The telemetry tracker. |
| dateTimeProvider | [RecordPoint.Connectors.SDK.Providers.IDateTimeProvider](#T-RecordPoint-Connectors-SDK-Providers-IDateTimeProvider 'RecordPoint.Connectors.SDK.Providers.IDateTimeProvider') | The date time provider. |
| toggleProvider | [RecordPoint.Connectors.SDK.Toggles.IToggleProvider](#T-RecordPoint-Connectors-SDK-Toggles-IToggleProvider 'RecordPoint.Connectors.SDK.Toggles.IToggleProvider') | The toggle provider. |
| operationTypes | [System.Collections.Generic.IList{System.Type}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IList 'System.Collections.Generic.IList{System.Type}') | An optional list of operation types to create RabbitMq consumers for |

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-DeadletterExchange'></a>
### DeadletterExchange `constants`

##### Summary

The deadletter exchange.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-DeadletterExchangeType'></a>
### DeadletterExchangeType `constants`

##### Summary

The deadletter exchange type.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-DefaultOperationTypes'></a>
### DefaultOperationTypes `constants`

##### Summary

Default list of Operation types to be used if none are provided

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-ExchangeName'></a>
### ExchangeName `constants`

##### Summary

The exchange name.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-ExchangeType'></a>
### ExchangeType `constants`

##### Summary

The exchange type.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-_dateTimeProvider'></a>
### _dateTimeProvider `constants`

##### Summary

The date time provider.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-_observabilityScope'></a>
### _observabilityScope `constants`

##### Summary

The scope manager.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-_operationTypes'></a>
### _operationTypes `constants`

##### Summary

Operation types to be used when creating Service Bus Processors

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-_processingToken'></a>
### _processingToken `constants`

##### Summary

Processing token.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-_rabbitMqConnection'></a>
### _rabbitMqConnection `constants`

##### Summary

The rabbit mq connection.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-_rabbitMqOptions'></a>
### _rabbitMqOptions `constants`

##### Summary

The rabbit mq options.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-_rabbitMqProcessors'></a>
### _rabbitMqProcessors `constants`

##### Summary

The rabbit mq processors.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-_serviceProvider'></a>
### _serviceProvider `constants`

##### Summary

The service provider.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-_systemContext'></a>
### _systemContext `constants`

##### Summary

The system context.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-_telemetryTracker'></a>
### _telemetryTracker `constants`

##### Summary

The telemetry tracker.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-_toggleProvider'></a>
### _toggleProvider `constants`

##### Summary

The toggle provider.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-_workManager'></a>
### _workManager `constants`

##### Summary

Work manager.

<a name='F-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-_workQueueClient'></a>
### _workQueueClient `constants`

##### Summary

Work queue client.

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-Dispose'></a>
### Dispose() `method`

##### Summary

Basic IDisposable implementation.

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-ExecuteAsync-System-Threading-CancellationToken-'></a>
### ExecuteAsync(stoppingToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| stoppingToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-StopAsync-System-Threading-CancellationToken-'></a>
### StopAsync(cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-WorkQueue-RabbitMq-RabbitMqWorkServer-ValidateOperationTypes-System-Collections-Generic-IList{System-Type}-'></a>
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
