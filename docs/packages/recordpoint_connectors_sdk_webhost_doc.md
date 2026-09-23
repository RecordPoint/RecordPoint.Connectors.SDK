<a name='assembly'></a>
# RecordPoint.Connectors.SDK.WebHost

## Contents

- [DeadLetterController](#T-RecordPoint-Connectors-SDK-WebHost-Controllers-DeadLetterController 'RecordPoint.Connectors.SDK.WebHost.Controllers.DeadLetterController')
  - [#ctor(deadLetterQueueService,observabilityScope,systemContext,telemetryTracker,options)](#M-RecordPoint-Connectors-SDK-WebHost-Controllers-DeadLetterController-#ctor-RecordPoint-Connectors-SDK-Work-IDeadLetterQueueService,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Work-DeadLetterControllerOptions}- 'RecordPoint.Connectors.SDK.WebHost.Controllers.DeadLetterController.#ctor(RecordPoint.Connectors.SDK.Work.IDeadLetterQueueService,RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Context.ISystemContext,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Work.DeadLetterControllerOptions})')
  - [Delete(queueName,sequenceNumber)](#M-RecordPoint-Connectors-SDK-WebHost-Controllers-DeadLetterController-Delete-System-String,System-Int64- 'RecordPoint.Connectors.SDK.WebHost.Controllers.DeadLetterController.Delete(System.String,System.Int64)')
  - [DeleteAll(queueName)](#M-RecordPoint-Connectors-SDK-WebHost-Controllers-DeadLetterController-DeleteAll-System-String- 'RecordPoint.Connectors.SDK.WebHost.Controllers.DeadLetterController.DeleteAll(System.String)')
  - [Get(queueName)](#M-RecordPoint-Connectors-SDK-WebHost-Controllers-DeadLetterController-Get-System-String- 'RecordPoint.Connectors.SDK.WebHost.Controllers.DeadLetterController.Get(System.String)')
  - [Get(queueName,sequenceNumber)](#M-RecordPoint-Connectors-SDK-WebHost-Controllers-DeadLetterController-Get-System-String,System-Int64- 'RecordPoint.Connectors.SDK.WebHost.Controllers.DeadLetterController.Get(System.String,System.Int64)')
  - [Post(queueName,sequenceNumbers)](#M-RecordPoint-Connectors-SDK-WebHost-Controllers-DeadLetterController-Post-System-String,System-Int64[]- 'RecordPoint.Connectors.SDK.WebHost.Controllers.DeadLetterController.Post(System.String,System.Int64[])')
  - [Post(queueName,maxCount,cancellationToken)](#M-RecordPoint-Connectors-SDK-WebHost-Controllers-DeadLetterController-Post-System-String,System-Int32,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.WebHost.Controllers.DeadLetterController.Post(System.String,System.Int32,System.Threading.CancellationToken)')
- [HealthController](#T-RecordPoint-Connectors-SDK-WebHost-Api-Controllers-HealthController 'RecordPoint.Connectors.SDK.WebHost.Api.Controllers.HealthController')
  - [#ctor(serviceProvider,healthCheckManager)](#M-RecordPoint-Connectors-SDK-WebHost-Api-Controllers-HealthController-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-Health-IHealthCheckManager- 'RecordPoint.Connectors.SDK.WebHost.Api.Controllers.HealthController.#ctor(System.IServiceProvider,RecordPoint.Connectors.SDK.Health.IHealthCheckManager)')
  - [Get()](#M-RecordPoint-Connectors-SDK-WebHost-Api-Controllers-HealthController-Get 'RecordPoint.Connectors.SDK.WebHost.Api.Controllers.HealthController.Get')
  - [Livez()](#M-RecordPoint-Connectors-SDK-WebHost-Api-Controllers-HealthController-Livez 'RecordPoint.Connectors.SDK.WebHost.Api.Controllers.HealthController.Livez')
  - [Readyz()](#M-RecordPoint-Connectors-SDK-WebHost-Api-Controllers-HealthController-Readyz 'RecordPoint.Connectors.SDK.WebHost.Api.Controllers.HealthController.Readyz')
- [NotificationsController](#T-RecordPoint-Connectors-SDK-WebHost-Controllers-NotificationsController 'RecordPoint.Connectors.SDK.WebHost.Controllers.NotificationsController')
  - [#ctor(observabilityScope,serviceProvider,systemContext)](#M-RecordPoint-Connectors-SDK-WebHost-Controllers-NotificationsController-#ctor-RecordPoint-Connectors-SDK-Observability-IObservabilityScope,System-IServiceProvider,RecordPoint-Connectors-SDK-Context-ISystemContext- 'RecordPoint.Connectors.SDK.WebHost.Controllers.NotificationsController.#ctor(RecordPoint.Connectors.SDK.Observability.IObservabilityScope,System.IServiceProvider,RecordPoint.Connectors.SDK.Context.ISystemContext)')
  - [Ping()](#M-RecordPoint-Connectors-SDK-WebHost-Controllers-NotificationsController-Ping 'RecordPoint.Connectors.SDK.WebHost.Controllers.NotificationsController.Ping')
  - [Post(notification)](#M-RecordPoint-Connectors-SDK-WebHost-Controllers-NotificationsController-Post-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel- 'RecordPoint.Connectors.SDK.WebHost.Controllers.NotificationsController.Post(RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel)')
- [RemoteRequestsHttpFilterExtension](#T-RecordPoint-Connectors-SDK-WebHost-MiddleWare-RemoteRequestsHttpFilterExtension 'RecordPoint.Connectors.SDK.WebHost.MiddleWare.RemoteRequestsHttpFilterExtension')
  - [IsLocal(request)](#M-RecordPoint-Connectors-SDK-WebHost-MiddleWare-RemoteRequestsHttpFilterExtension-IsLocal-Microsoft-AspNetCore-Http-HttpRequest- 'RecordPoint.Connectors.SDK.WebHost.MiddleWare.RemoteRequestsHttpFilterExtension.IsLocal(Microsoft.AspNetCore.Http.HttpRequest)')
  - [UseRemoteRequestFilter(app)](#M-RecordPoint-Connectors-SDK-WebHost-MiddleWare-RemoteRequestsHttpFilterExtension-UseRemoteRequestFilter-Microsoft-AspNetCore-Builder-IApplicationBuilder- 'RecordPoint.Connectors.SDK.WebHost.MiddleWare.RemoteRequestsHttpFilterExtension.UseRemoteRequestFilter(Microsoft.AspNetCore.Builder.IApplicationBuilder)')
- [Startup](#T-RecordPoint-Connectors-SDK-WebHost-Startup 'RecordPoint.Connectors.SDK.WebHost.Startup')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-WebHost-Startup-#ctor-Microsoft-Extensions-Configuration-IConfiguration- 'RecordPoint.Connectors.SDK.WebHost.Startup.#ctor(Microsoft.Extensions.Configuration.IConfiguration)')
  - [Configure()](#M-RecordPoint-Connectors-SDK-WebHost-Startup-Configure-Microsoft-AspNetCore-Builder-IApplicationBuilder,Microsoft-AspNetCore-Hosting-IWebHostEnvironment,RecordPoint-Connectors-SDK-Context-ISystemContext- 'RecordPoint.Connectors.SDK.WebHost.Startup.Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder,Microsoft.AspNetCore.Hosting.IWebHostEnvironment,RecordPoint.Connectors.SDK.Context.ISystemContext)')
  - [ConfigureServices(services)](#M-RecordPoint-Connectors-SDK-WebHost-Startup-ConfigureServices-Microsoft-Extensions-DependencyInjection-IServiceCollection- 'RecordPoint.Connectors.SDK.WebHost.Startup.ConfigureServices(Microsoft.Extensions.DependencyInjection.IServiceCollection)')
- [StatusController](#T-RecordPoint-Connectors-SDK-WebHost-Controllers-StatusController 'RecordPoint.Connectors.SDK.WebHost.Controllers.StatusController')
  - [#ctor(statusManager)](#M-RecordPoint-Connectors-SDK-WebHost-Controllers-StatusController-#ctor-RecordPoint-Connectors-SDK-Status-IStatusManager- 'RecordPoint.Connectors.SDK.WebHost.Controllers.StatusController.#ctor(RecordPoint.Connectors.SDK.Status.IStatusManager)')
  - [_statusManager](#F-RecordPoint-Connectors-SDK-WebHost-Controllers-StatusController-_statusManager 'RecordPoint.Connectors.SDK.WebHost.Controllers.StatusController._statusManager')
  - [Get()](#M-RecordPoint-Connectors-SDK-WebHost-Controllers-StatusController-Get 'RecordPoint.Connectors.SDK.WebHost.Controllers.StatusController.Get')
- [TokenValidationParameters](#T-RecordPoint-Connectors-SDK-WebHost-TokenValidationParameters 'RecordPoint.Connectors.SDK.WebHost.TokenValidationParameters')
  - [ValidAudiences](#P-RecordPoint-Connectors-SDK-WebHost-TokenValidationParameters-ValidAudiences 'RecordPoint.Connectors.SDK.WebHost.TokenValidationParameters.ValidAudiences')
- [WebHostAuthenticationOptions](#T-RecordPoint-Connectors-SDK-WebHost-WebHostAuthenticationOptions 'RecordPoint.Connectors.SDK.WebHost.WebHostAuthenticationOptions')
  - [SECTION_NAME](#F-RecordPoint-Connectors-SDK-WebHost-WebHostAuthenticationOptions-SECTION_NAME 'RecordPoint.Connectors.SDK.WebHost.WebHostAuthenticationOptions.SECTION_NAME')
  - [Audience](#P-RecordPoint-Connectors-SDK-WebHost-WebHostAuthenticationOptions-Audience 'RecordPoint.Connectors.SDK.WebHost.WebHostAuthenticationOptions.Audience')
  - [ClientId](#P-RecordPoint-Connectors-SDK-WebHost-WebHostAuthenticationOptions-ClientId 'RecordPoint.Connectors.SDK.WebHost.WebHostAuthenticationOptions.ClientId')
  - [Domain](#P-RecordPoint-Connectors-SDK-WebHost-WebHostAuthenticationOptions-Domain 'RecordPoint.Connectors.SDK.WebHost.WebHostAuthenticationOptions.Domain')
  - [Instance](#P-RecordPoint-Connectors-SDK-WebHost-WebHostAuthenticationOptions-Instance 'RecordPoint.Connectors.SDK.WebHost.WebHostAuthenticationOptions.Instance')
  - [TenantId](#P-RecordPoint-Connectors-SDK-WebHost-WebHostAuthenticationOptions-TenantId 'RecordPoint.Connectors.SDK.WebHost.WebHostAuthenticationOptions.TenantId')
  - [TokenValidationParameters](#P-RecordPoint-Connectors-SDK-WebHost-WebHostAuthenticationOptions-TokenValidationParameters 'RecordPoint.Connectors.SDK.WebHost.WebHostAuthenticationOptions.TokenValidationParameters')
- [WebHostBuilderExtensions](#T-RecordPoint-Connectors-SDK-WebHost-WebHostBuilderExtensions 'RecordPoint.Connectors.SDK.WebHost.WebHostBuilderExtensions')
  - [DEFAULT_API_URLS](#F-RecordPoint-Connectors-SDK-WebHost-WebHostBuilderExtensions-DEFAULT_API_URLS 'RecordPoint.Connectors.SDK.WebHost.WebHostBuilderExtensions.DEFAULT_API_URLS')
  - [UseWebHost(hostBuilder,configuration)](#M-RecordPoint-Connectors-SDK-WebHost-WebHostBuilderExtensions-UseWebHost-Microsoft-Extensions-Hosting-IHostBuilder,Microsoft-Extensions-Configuration-IConfigurationRoot- 'RecordPoint.Connectors.SDK.WebHost.WebHostBuilderExtensions.UseWebHost(Microsoft.Extensions.Hosting.IHostBuilder,Microsoft.Extensions.Configuration.IConfigurationRoot)')
- [WebHostOptions](#T-RecordPoint-Connectors-SDK-WebHost-WebHostOptions 'RecordPoint.Connectors.SDK.WebHost.WebHostOptions')
  - [SECTION_NAME](#F-RecordPoint-Connectors-SDK-WebHost-WebHostOptions-SECTION_NAME 'RecordPoint.Connectors.SDK.WebHost.WebHostOptions.SECTION_NAME')
  - [SwaggerEndpointName](#P-RecordPoint-Connectors-SDK-WebHost-WebHostOptions-SwaggerEndpointName 'RecordPoint.Connectors.SDK.WebHost.WebHostOptions.SwaggerEndpointName')
  - [SwaggerEndpointUrl](#P-RecordPoint-Connectors-SDK-WebHost-WebHostOptions-SwaggerEndpointUrl 'RecordPoint.Connectors.SDK.WebHost.WebHostOptions.SwaggerEndpointUrl')

<a name='T-RecordPoint-Connectors-SDK-WebHost-Controllers-DeadLetterController'></a>
## DeadLetterController `type`

##### Namespace

RecordPoint.Connectors.SDK.WebHost.Controllers

##### Summary

API controller for viewing and replaying dead-letter queue messages.

<a name='M-RecordPoint-Connectors-SDK-WebHost-Controllers-DeadLetterController-#ctor-RecordPoint-Connectors-SDK-Work-IDeadLetterQueueService,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Work-DeadLetterControllerOptions}-'></a>
### #ctor(deadLetterQueueService,observabilityScope,systemContext,telemetryTracker,options) `constructor`

##### Summary

Constructor for DI

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| deadLetterQueueService | [RecordPoint.Connectors.SDK.Work.IDeadLetterQueueService](#T-RecordPoint-Connectors-SDK-Work-IDeadLetterQueueService 'RecordPoint.Connectors.SDK.Work.IDeadLetterQueueService') | The dead-letter queue service. |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | The observability scope manager. |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The telemetry tracker. |
| options | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Work.DeadLetterControllerOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Work-DeadLetterControllerOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Work.DeadLetterControllerOptions}') | The dead-letter controller options. |

<a name='M-RecordPoint-Connectors-SDK-WebHost-Controllers-DeadLetterController-Delete-System-String,System-Int64-'></a>
### Delete(queueName,sequenceNumber) `method`

##### Summary

Deletes a dead-letter message by sequence number.

##### Returns

An HTTP response indicating the delete result.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| queueName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The queue name that contains the message. |
| sequenceNumber | [System.Int64](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int64 'System.Int64') | The sequence number of the message to delete. |

<a name='M-RecordPoint-Connectors-SDK-WebHost-Controllers-DeadLetterController-DeleteAll-System-String-'></a>
### DeleteAll(queueName) `method`

##### Summary

Deletes all dead-letter messages from a queue.

##### Returns

An HTTP response indicating the delete result.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| queueName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The queue name to clear. |

<a name='M-RecordPoint-Connectors-SDK-WebHost-Controllers-DeadLetterController-Get-System-String-'></a>
### Get(queueName) `method`

##### Summary

Gets all dead-letter messages for a queue.

##### Returns

A response containing the matching dead-letter messages.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| queueName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The queue name to read dead-letter messages from. |

<a name='M-RecordPoint-Connectors-SDK-WebHost-Controllers-DeadLetterController-Get-System-String,System-Int64-'></a>
### Get(queueName,sequenceNumber) `method`

##### Summary

Gets a dead-letter message by sequence number.

##### Returns

A response containing the requested dead-letter message.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| queueName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The queue name to read from. |
| sequenceNumber | [System.Int64](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int64 'System.Int64') | The sequence number of the dead-letter message. |

<a name='M-RecordPoint-Connectors-SDK-WebHost-Controllers-DeadLetterController-Post-System-String,System-Int64[]-'></a>
### Post(queueName,sequenceNumbers) `method`

##### Summary

Requeues selected dead-letter messages by sequence number.

##### Returns

An HTTP response indicating the replay result.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| queueName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The queue name to requeue messages into. |
| sequenceNumbers | [System.Int64[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int64[] 'System.Int64[]') | The sequence numbers to requeue. |

<a name='M-RecordPoint-Connectors-SDK-WebHost-Controllers-DeadLetterController-Post-System-String,System-Int32,System-Threading-CancellationToken-'></a>
### Post(queueName,maxCount,cancellationToken) `method`

##### Summary

Requeues up to `maxCount` dead-letter messages for a queue.

##### Returns

An HTTP response indicating the replay result.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| queueName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The queue name to requeue messages into. |
| maxCount | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The maximum number of messages to replay. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | A token to cancel the operation. |

<a name='T-RecordPoint-Connectors-SDK-WebHost-Api-Controllers-HealthController'></a>
## HealthController `type`

##### Namespace

RecordPoint.Connectors.SDK.WebHost.Api.Controllers

##### Summary

The Health Check controller

<a name='M-RecordPoint-Connectors-SDK-WebHost-Api-Controllers-HealthController-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-Health-IHealthCheckManager-'></a>
### #ctor(serviceProvider,healthCheckManager) `constructor`

##### Summary

Initializes a new instance of the [HealthController](#T-RecordPoint-Connectors-SDK-WebHost-Api-Controllers-HealthController 'RecordPoint.Connectors.SDK.WebHost.Api.Controllers.HealthController') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| serviceProvider | [System.IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider') | Service provider used to resolve health check actions. |
| healthCheckManager | [RecordPoint.Connectors.SDK.Health.IHealthCheckManager](#T-RecordPoint-Connectors-SDK-Health-IHealthCheckManager 'RecordPoint.Connectors.SDK.Health.IHealthCheckManager') | Manager that tracks the current health state. |

<a name='M-RecordPoint-Connectors-SDK-WebHost-Api-Controllers-HealthController-Get'></a>
### Get() `method`

##### Summary

Gets the current health check result snapshot.

##### Returns

The current [HealthCheckResult](#T-RecordPoint-Connectors-SDK-Health-HealthCheckResult 'RecordPoint.Connectors.SDK.Health.HealthCheckResult').

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-WebHost-Api-Controllers-HealthController-Livez'></a>
### Livez() `method`

##### Summary

Check if the service is live

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-WebHost-Api-Controllers-HealthController-Readyz'></a>
### Readyz() `method`

##### Summary

Check if the service is ready

##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-WebHost-Controllers-NotificationsController'></a>
## NotificationsController `type`

##### Namespace

RecordPoint.Connectors.SDK.WebHost.Controllers

##### Summary

Notifications Controller for receiving webhook requests from Records365

<a name='M-RecordPoint-Connectors-SDK-WebHost-Controllers-NotificationsController-#ctor-RecordPoint-Connectors-SDK-Observability-IObservabilityScope,System-IServiceProvider,RecordPoint-Connectors-SDK-Context-ISystemContext-'></a>
### #ctor(observabilityScope,serviceProvider,systemContext) `constructor`

##### Summary

Initializes a new instance of the [NotificationsController](#T-RecordPoint-Connectors-SDK-WebHost-Controllers-NotificationsController 'RecordPoint.Connectors.SDK.WebHost.Controllers.NotificationsController') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | Scope provider for correlating telemetry. |
| serviceProvider | [System.IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider') | Service provider used to resolve notification operations. |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | System context for the current connector instance. |

<a name='M-RecordPoint-Connectors-SDK-WebHost-Controllers-NotificationsController-Ping'></a>
### Ping() `method`

##### Summary

Returns a 200 (OK) response to Records365 ping requests.

##### Returns

An OK status response.

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-WebHost-Controllers-NotificationsController-Post-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel-'></a>
### Post(notification) `method`

##### Summary

Receives connector notifications from Records365.

##### Returns

An HTTP response indicating the processing outcome.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| notification | [RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel') | The notification payload posted by Records365. |

<a name='T-RecordPoint-Connectors-SDK-WebHost-MiddleWare-RemoteRequestsHttpFilterExtension'></a>
## RemoteRequestsHttpFilterExtension `type`

##### Namespace

RecordPoint.Connectors.SDK.WebHost.MiddleWare

##### Summary

The remote requests http filter extension.

<a name='M-RecordPoint-Connectors-SDK-WebHost-MiddleWare-RemoteRequestsHttpFilterExtension-IsLocal-Microsoft-AspNetCore-Http-HttpRequest-'></a>
### IsLocal(request) `method`

##### Summary

Did the webrequest originate locally

##### Returns

True if the request is local

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| request | [Microsoft.AspNetCore.Http.HttpRequest](#T-Microsoft-AspNetCore-Http-HttpRequest 'Microsoft.AspNetCore.Http.HttpRequest') | Target web request |

<a name='M-RecordPoint-Connectors-SDK-WebHost-MiddleWare-RemoteRequestsHttpFilterExtension-UseRemoteRequestFilter-Microsoft-AspNetCore-Builder-IApplicationBuilder-'></a>
### UseRemoteRequestFilter(app) `method`

##### Summary

Use the remote http filter web request filter

##### Returns

Updated application builder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| app | [Microsoft.AspNetCore.Builder.IApplicationBuilder](#T-Microsoft-AspNetCore-Builder-IApplicationBuilder 'Microsoft.AspNetCore.Builder.IApplicationBuilder') | Application builder |

<a name='T-RecordPoint-Connectors-SDK-WebHost-Startup'></a>
## Startup `type`

##### Namespace

RecordPoint.Connectors.SDK.WebHost

##### Summary

Web server startup

<a name='M-RecordPoint-Connectors-SDK-WebHost-Startup-#ctor-Microsoft-Extensions-Configuration-IConfiguration-'></a>
### #ctor() `constructor`

##### Summary

Web server startup

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-WebHost-Startup-Configure-Microsoft-AspNetCore-Builder-IApplicationBuilder,Microsoft-AspNetCore-Hosting-IWebHostEnvironment,RecordPoint-Connectors-SDK-Context-ISystemContext-'></a>
### Configure() `method`

##### Summary

Configures the application.

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-WebHost-Startup-ConfigureServices-Microsoft-Extensions-DependencyInjection-IServiceCollection-'></a>
### ConfigureServices(services) `method`

##### Summary

Register services into the IServiceCollection.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| services | [Microsoft.Extensions.DependencyInjection.IServiceCollection](#T-Microsoft-Extensions-DependencyInjection-IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection') | The service collection to register the services |

<a name='T-RecordPoint-Connectors-SDK-WebHost-Controllers-StatusController'></a>
## StatusController `type`

##### Namespace

RecordPoint.Connectors.SDK.WebHost.Controllers

##### Summary

API controller for exposing connector status details.

<a name='M-RecordPoint-Connectors-SDK-WebHost-Controllers-StatusController-#ctor-RecordPoint-Connectors-SDK-Status-IStatusManager-'></a>
### #ctor(statusManager) `constructor`

##### Summary

Initializes a new instance of the [StatusController](#T-RecordPoint-Connectors-SDK-WebHost-Controllers-StatusController 'RecordPoint.Connectors.SDK.WebHost.Controllers.StatusController') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| statusManager | [RecordPoint.Connectors.SDK.Status.IStatusManager](#T-RecordPoint-Connectors-SDK-Status-IStatusManager 'RecordPoint.Connectors.SDK.Status.IStatusManager') | The status manager used to retrieve status entries. |

<a name='F-RecordPoint-Connectors-SDK-WebHost-Controllers-StatusController-_statusManager'></a>
### _statusManager `constants`

##### Summary

The status manager.

<a name='M-RecordPoint-Connectors-SDK-WebHost-Controllers-StatusController-Get'></a>
### Get() `method`

##### Summary

Get and return a task of a list of statusmodels.

##### Returns

Task<List<StatusModel>>

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-WebHost-TokenValidationParameters'></a>
## TokenValidationParameters `type`

##### Namespace

RecordPoint.Connectors.SDK.WebHost

##### Summary

The token validation parameters.

<a name='P-RecordPoint-Connectors-SDK-WebHost-TokenValidationParameters-ValidAudiences'></a>
### ValidAudiences `property`

##### Summary

Gets or sets the valid audiences.

<a name='T-RecordPoint-Connectors-SDK-WebHost-WebHostAuthenticationOptions'></a>
## WebHostAuthenticationOptions `type`

##### Namespace

RecordPoint.Connectors.SDK.WebHost

##### Summary

The web host authentication options.

<a name='F-RecordPoint-Connectors-SDK-WebHost-WebHostAuthenticationOptions-SECTION_NAME'></a>
### SECTION_NAME `constants`

##### Summary

The SECTION NAME.

<a name='P-RecordPoint-Connectors-SDK-WebHost-WebHostAuthenticationOptions-Audience'></a>
### Audience `property`

##### Summary

Gets or sets the audience.

<a name='P-RecordPoint-Connectors-SDK-WebHost-WebHostAuthenticationOptions-ClientId'></a>
### ClientId `property`

##### Summary

Gets or sets the client id.

<a name='P-RecordPoint-Connectors-SDK-WebHost-WebHostAuthenticationOptions-Domain'></a>
### Domain `property`

##### Summary

Gets or sets the domain.

<a name='P-RecordPoint-Connectors-SDK-WebHost-WebHostAuthenticationOptions-Instance'></a>
### Instance `property`

##### Summary

Gets or sets the instance.

<a name='P-RecordPoint-Connectors-SDK-WebHost-WebHostAuthenticationOptions-TenantId'></a>
### TenantId `property`

##### Summary

Gets or sets the tenant id.

<a name='P-RecordPoint-Connectors-SDK-WebHost-WebHostAuthenticationOptions-TokenValidationParameters'></a>
### TokenValidationParameters `property`

##### Summary

Gets or sets the token validation parameters.

<a name='T-RecordPoint-Connectors-SDK-WebHost-WebHostBuilderExtensions'></a>
## WebHostBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.WebHost

##### Summary

The web host builder extensions.

<a name='F-RecordPoint-Connectors-SDK-WebHost-WebHostBuilderExtensions-DEFAULT_API_URLS'></a>
### DEFAULT_API_URLS `constants`

##### Summary

Default value for the API Urls

<a name='M-RecordPoint-Connectors-SDK-WebHost-WebHostBuilderExtensions-UseWebHost-Microsoft-Extensions-Hosting-IHostBuilder,Microsoft-Extensions-Configuration-IConfigurationRoot-'></a>
### UseWebHost(hostBuilder,configuration) `method`

##### Summary

Configures and enables the SDK web host with configured or default URLs.

##### Returns

The configured host builder.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder to configure. |
| configuration | [Microsoft.Extensions.Configuration.IConfigurationRoot](#T-Microsoft-Extensions-Configuration-IConfigurationRoot 'Microsoft.Extensions.Configuration.IConfigurationRoot') | The application configuration root. |

<a name='T-RecordPoint-Connectors-SDK-WebHost-WebHostOptions'></a>
## WebHostOptions `type`

##### Namespace

RecordPoint.Connectors.SDK.WebHost

##### Summary

The web host options.

<a name='F-RecordPoint-Connectors-SDK-WebHost-WebHostOptions-SECTION_NAME'></a>
### SECTION_NAME `constants`

##### Summary

The SECTION NAME.

<a name='P-RecordPoint-Connectors-SDK-WebHost-WebHostOptions-SwaggerEndpointName'></a>
### SwaggerEndpointName `property`

##### Summary

Gets or sets the swagger endpoint name.

<a name='P-RecordPoint-Connectors-SDK-WebHost-WebHostOptions-SwaggerEndpointUrl'></a>
### SwaggerEndpointUrl `property`

##### Summary

Gets or sets the swagger endpoint url.
