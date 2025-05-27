<a name='assembly'></a>
# RecordPoint.Connectors.SDK.Caching

## Contents

- [CacheActionContext](#T-RecordPoint-Connectors-SDK-Caching-CacheActionContext 'RecordPoint.Connectors.SDK.Caching.CacheActionContext')
  - [Properties](#P-RecordPoint-Connectors-SDK-Caching-CacheActionContext-Properties 'RecordPoint.Connectors.SDK.Caching.CacheActionContext.Properties')
- [CacheActionResult\`1](#T-RecordPoint-Connectors-SDK-Caching-CacheActionResult`1 'RecordPoint.Connectors.SDK.Caching.CacheActionResult`1')
  - [CacheItem](#P-RecordPoint-Connectors-SDK-Caching-CacheActionResult`1-CacheItem 'RecordPoint.Connectors.SDK.Caching.CacheActionResult`1.CacheItem')
  - [Expires](#P-RecordPoint-Connectors-SDK-Caching-CacheActionResult`1-Expires 'RecordPoint.Connectors.SDK.Caching.CacheActionResult`1.Expires')
- [CacheBuilderExtensions](#T-RecordPoint-Connectors-SDK-Caching-CacheBuilderExtensions 'RecordPoint.Connectors.SDK.Caching.CacheBuilderExtensions')
  - [UseInMemoryCache\`\`2(hostBuilder)](#M-RecordPoint-Connectors-SDK-Caching-CacheBuilderExtensions-UseInMemoryCache``2-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Caching.CacheBuilderExtensions.UseInMemoryCache``2(Microsoft.Extensions.Hosting.IHostBuilder)')
- [ICacheAction\`1](#T-RecordPoint-Connectors-SDK-Caching-ICacheAction`1 'RecordPoint.Connectors.SDK.Caching.ICacheAction`1')
  - [ExecuteAsync(context)](#M-RecordPoint-Connectors-SDK-Caching-ICacheAction`1-ExecuteAsync-RecordPoint-Connectors-SDK-Caching-CacheActionContext- 'RecordPoint.Connectors.SDK.Caching.ICacheAction`1.ExecuteAsync(RecordPoint.Connectors.SDK.Caching.CacheActionContext)')
- [ICache\`1](#T-RecordPoint-Connectors-SDK-Caching-ICache`1 'RecordPoint.Connectors.SDK.Caching.ICache`1')
  - [GetAsync(key,context)](#M-RecordPoint-Connectors-SDK-Caching-ICache`1-GetAsync-System-String,RecordPoint-Connectors-SDK-Caching-CacheActionContext- 'RecordPoint.Connectors.SDK.Caching.ICache`1.GetAsync(System.String,RecordPoint.Connectors.SDK.Caching.CacheActionContext)')
- [InMemoryCache\`1](#T-RecordPoint-Connectors-SDK-Caching-InMemoryCache`1 'RecordPoint.Connectors.SDK.Caching.InMemoryCache`1')
  - [#ctor(serviceProvider,memoryCache)](#M-RecordPoint-Connectors-SDK-Caching-InMemoryCache`1-#ctor-System-IServiceProvider,Microsoft-Extensions-Caching-Memory-IMemoryCache- 'RecordPoint.Connectors.SDK.Caching.InMemoryCache`1.#ctor(System.IServiceProvider,Microsoft.Extensions.Caching.Memory.IMemoryCache)')
  - [_memoryCache](#F-RecordPoint-Connectors-SDK-Caching-InMemoryCache`1-_memoryCache 'RecordPoint.Connectors.SDK.Caching.InMemoryCache`1._memoryCache')
  - [_serviceProvider](#F-RecordPoint-Connectors-SDK-Caching-InMemoryCache`1-_serviceProvider 'RecordPoint.Connectors.SDK.Caching.InMemoryCache`1._serviceProvider')
  - [GetAsync(key,context)](#M-RecordPoint-Connectors-SDK-Caching-InMemoryCache`1-GetAsync-System-String,RecordPoint-Connectors-SDK-Caching-CacheActionContext- 'RecordPoint.Connectors.SDK.Caching.InMemoryCache`1.GetAsync(System.String,RecordPoint.Connectors.SDK.Caching.CacheActionContext)')
- [InMemorySemaphoreLockBuilderExtensions](#T-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockBuilderExtensions 'RecordPoint.Connectors.SDK.Caching.Semaphore.InMemorySemaphoreLockBuilderExtensions')
  - [UseInMemorySemaphoreLock(hostBuilder)](#M-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockBuilderExtensions-UseInMemorySemaphoreLock-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Caching.Semaphore.InMemorySemaphoreLockBuilderExtensions.UseInMemorySemaphoreLock(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseInMemorySemaphoreLock\`\`1(hostBuilder)](#M-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockBuilderExtensions-UseInMemorySemaphoreLock``1-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Caching.Semaphore.InMemorySemaphoreLockBuilderExtensions.UseInMemorySemaphoreLock``1(Microsoft.Extensions.Hosting.IHostBuilder)')
- [InMemorySemaphoreLockManager](#T-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockManager 'RecordPoint.Connectors.SDK.Caching.Semaphore.InMemorySemaphoreLockManager')
  - [#ctor(memoryCache,serviceProvider)](#M-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockManager-#ctor-Microsoft-Extensions-Caching-Memory-IMemoryCache,System-IServiceProvider- 'RecordPoint.Connectors.SDK.Caching.Semaphore.InMemorySemaphoreLockManager.#ctor(Microsoft.Extensions.Caching.Memory.IMemoryCache,System.IServiceProvider)')
  - [GLOBAL_SEMAPHORE_KEY](#F-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockManager-GLOBAL_SEMAPHORE_KEY 'RecordPoint.Connectors.SDK.Caching.Semaphore.InMemorySemaphoreLockManager.GLOBAL_SEMAPHORE_KEY')
  - [_lockObject](#F-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockManager-_lockObject 'RecordPoint.Connectors.SDK.Caching.Semaphore.InMemorySemaphoreLockManager._lockObject')
  - [_memoryCache](#F-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockManager-_memoryCache 'RecordPoint.Connectors.SDK.Caching.Semaphore.InMemorySemaphoreLockManager._memoryCache')
  - [_serviceProvider](#F-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockManager-_serviceProvider 'RecordPoint.Connectors.SDK.Caching.Semaphore.InMemorySemaphoreLockManager._serviceProvider')
  - [ConnectorConfiguration](#P-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockManager-ConnectorConfiguration 'RecordPoint.Connectors.SDK.Caching.Semaphore.InMemorySemaphoreLockManager.ConnectorConfiguration')
  - [CheckWaitSemaphoreAsync(workType,context,cancellationToken)](#M-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockManager-CheckWaitSemaphoreAsync-System-String,System-Object,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Caching.Semaphore.InMemorySemaphoreLockManager.CheckWaitSemaphoreAsync(System.String,System.Object,System.Threading.CancellationToken)')
  - [GetScopedSemaphoreKeyAsync(workType,context,cancellationToken)](#M-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockManager-GetScopedSemaphoreKeyAsync-System-String,System-Object,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Caching.Semaphore.InMemorySemaphoreLockManager.GetScopedSemaphoreKeyAsync(System.String,System.Object,System.Threading.CancellationToken)')
  - [GetSemaphoreAsync(workType,context,cancellationToken)](#M-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockManager-GetSemaphoreAsync-System-String,System-Object,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Caching.Semaphore.InMemorySemaphoreLockManager.GetSemaphoreAsync(System.String,System.Object,System.Threading.CancellationToken)')
  - [GetSemaphoreKeysAsync(workType,context,cancellationToken)](#M-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockManager-GetSemaphoreKeysAsync-System-String,System-Object,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Caching.Semaphore.InMemorySemaphoreLockManager.GetSemaphoreKeysAsync(System.String,System.Object,System.Threading.CancellationToken)')
  - [SetSemaphoreAsync(semaphoreLockType,workType,context,duration,cancellationToken)](#M-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockManager-SetSemaphoreAsync-RecordPoint-Connectors-SDK-Caching-Semaphore-SemaphoreLockType,System-String,System-Object,System-Int32,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Caching.Semaphore.InMemorySemaphoreLockManager.SetSemaphoreAsync(RecordPoint.Connectors.SDK.Caching.Semaphore.SemaphoreLockType,System.String,System.Object,System.Int32,System.Threading.CancellationToken)')

<a name='T-RecordPoint-Connectors-SDK-Caching-CacheActionContext'></a>
## CacheActionContext `type`

##### Namespace

RecordPoint.Connectors.SDK.Caching

##### Summary

The cache action context.

<a name='P-RecordPoint-Connectors-SDK-Caching-CacheActionContext-Properties'></a>
### Properties `property`

##### Summary

Gets or sets the properties.

<a name='T-RecordPoint-Connectors-SDK-Caching-CacheActionResult`1'></a>
## CacheActionResult\`1 `type`

##### Namespace

RecordPoint.Connectors.SDK.Caching

##### Summary

The cache action result.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TCacheItemType |  |

<a name='P-RecordPoint-Connectors-SDK-Caching-CacheActionResult`1-CacheItem'></a>
### CacheItem `property`

##### Summary

Gets or sets the cache item.

<a name='P-RecordPoint-Connectors-SDK-Caching-CacheActionResult`1-Expires'></a>
### Expires `property`

##### Summary

Gets or sets the expires.

<a name='T-RecordPoint-Connectors-SDK-Caching-CacheBuilderExtensions'></a>
## CacheBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Caching

##### Summary

The cache builder extensions.

<a name='M-RecordPoint-Connectors-SDK-Caching-CacheBuilderExtensions-UseInMemoryCache``2-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseInMemoryCache\`\`2(hostBuilder) `method`

##### Summary

Use in memory cache.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TCacheAction |  |
| TCacheItemType |  |

<a name='T-RecordPoint-Connectors-SDK-Caching-ICacheAction`1'></a>
## ICacheAction\`1 `type`

##### Namespace

RecordPoint.Connectors.SDK.Caching

##### Summary



##### Generic Types

| Name | Description |
| ---- | ----------- |
| TCacheItemType |  |

<a name='M-RecordPoint-Connectors-SDK-Caching-ICacheAction`1-ExecuteAsync-RecordPoint-Connectors-SDK-Caching-CacheActionContext-'></a>
### ExecuteAsync(context) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [RecordPoint.Connectors.SDK.Caching.CacheActionContext](#T-RecordPoint-Connectors-SDK-Caching-CacheActionContext 'RecordPoint.Connectors.SDK.Caching.CacheActionContext') |  |

<a name='T-RecordPoint-Connectors-SDK-Caching-ICache`1'></a>
## ICache\`1 `type`

##### Namespace

RecordPoint.Connectors.SDK.Caching

##### Summary



##### Generic Types

| Name | Description |
| ---- | ----------- |
| TCacheItemType |  |

<a name='M-RecordPoint-Connectors-SDK-Caching-ICache`1-GetAsync-System-String,RecordPoint-Connectors-SDK-Caching-CacheActionContext-'></a>
### GetAsync(key,context) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| context | [RecordPoint.Connectors.SDK.Caching.CacheActionContext](#T-RecordPoint-Connectors-SDK-Caching-CacheActionContext 'RecordPoint.Connectors.SDK.Caching.CacheActionContext') |  |

<a name='T-RecordPoint-Connectors-SDK-Caching-InMemoryCache`1'></a>
## InMemoryCache\`1 `type`

##### Namespace

RecordPoint.Connectors.SDK.Caching

##### Summary

The in memory cache.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TCacheItemType |  |

<a name='M-RecordPoint-Connectors-SDK-Caching-InMemoryCache`1-#ctor-System-IServiceProvider,Microsoft-Extensions-Caching-Memory-IMemoryCache-'></a>
### #ctor(serviceProvider,memoryCache) `constructor`

##### Summary

Initializes a new instance of the class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| serviceProvider | [System.IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider') | The service provider. |
| memoryCache | [Microsoft.Extensions.Caching.Memory.IMemoryCache](#T-Microsoft-Extensions-Caching-Memory-IMemoryCache 'Microsoft.Extensions.Caching.Memory.IMemoryCache') | The memory cache. |

<a name='F-RecordPoint-Connectors-SDK-Caching-InMemoryCache`1-_memoryCache'></a>
### _memoryCache `constants`

##### Summary

The memory cache.

<a name='F-RecordPoint-Connectors-SDK-Caching-InMemoryCache`1-_serviceProvider'></a>
### _serviceProvider `constants`

##### Summary

The service provider.

<a name='M-RecordPoint-Connectors-SDK-Caching-InMemoryCache`1-GetAsync-System-String,RecordPoint-Connectors-SDK-Caching-CacheActionContext-'></a>
### GetAsync(key,context) `method`

##### Summary

Get and return a task of type tcacheitemtype? asynchronously.

##### Returns

Task<TCacheItemType?>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The key. |
| context | [RecordPoint.Connectors.SDK.Caching.CacheActionContext](#T-RecordPoint-Connectors-SDK-Caching-CacheActionContext 'RecordPoint.Connectors.SDK.Caching.CacheActionContext') | The context. |

<a name='T-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockBuilderExtensions'></a>
## InMemorySemaphoreLockBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Caching.Semaphore

##### Summary

The in memory semaphore lock builder extensions.

<a name='M-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockBuilderExtensions-UseInMemorySemaphoreLock-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseInMemorySemaphoreLock(hostBuilder) `method`

##### Summary

Use in memory semaphore lock.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |

<a name='M-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockBuilderExtensions-UseInMemorySemaphoreLock``1-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseInMemorySemaphoreLock\`\`1(hostBuilder) `method`

##### Summary

Use in memory semaphore lock.

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

<a name='T-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockManager'></a>
## InMemorySemaphoreLockManager `type`

##### Namespace

RecordPoint.Connectors.SDK.Caching.Semaphore

##### Summary

The in memory semaphore lock manager.

<a name='M-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockManager-#ctor-Microsoft-Extensions-Caching-Memory-IMemoryCache,System-IServiceProvider-'></a>
### #ctor(memoryCache,serviceProvider) `constructor`

##### Summary

Initializes a new instance of the [InMemorySemaphoreLockManager](#T-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockManager 'RecordPoint.Connectors.SDK.Caching.Semaphore.InMemorySemaphoreLockManager') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| memoryCache | [Microsoft.Extensions.Caching.Memory.IMemoryCache](#T-Microsoft-Extensions-Caching-Memory-IMemoryCache 'Microsoft.Extensions.Caching.Memory.IMemoryCache') | The memory cache. |
| serviceProvider | [System.IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider') | The service provider. |

<a name='F-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockManager-GLOBAL_SEMAPHORE_KEY'></a>
### GLOBAL_SEMAPHORE_KEY `constants`

##### Summary

The GLOBAL SEMAPHORE KEY.

<a name='F-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockManager-_lockObject'></a>
### _lockObject `constants`

##### Summary

Lock object.

<a name='F-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockManager-_memoryCache'></a>
### _memoryCache `constants`

##### Summary

The memory cache.

<a name='F-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockManager-_serviceProvider'></a>
### _serviceProvider `constants`

##### Summary

The service provider.

<a name='P-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockManager-ConnectorConfiguration'></a>
### ConnectorConfiguration `property`

##### Summary

Gets or sets the connector configuration.

<a name='M-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockManager-CheckWaitSemaphoreAsync-System-String,System-Object,System-Threading-CancellationToken-'></a>
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

<a name='M-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockManager-GetScopedSemaphoreKeyAsync-System-String,System-Object,System-Threading-CancellationToken-'></a>
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

<a name='M-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockManager-GetSemaphoreAsync-System-String,System-Object,System-Threading-CancellationToken-'></a>
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

<a name='M-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockManager-GetSemaphoreKeysAsync-System-String,System-Object,System-Threading-CancellationToken-'></a>
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

<a name='M-RecordPoint-Connectors-SDK-Caching-Semaphore-InMemorySemaphoreLockManager-SetSemaphoreAsync-RecordPoint-Connectors-SDK-Caching-Semaphore-SemaphoreLockType,System-String,System-Object,System-Int32,System-Threading-CancellationToken-'></a>
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
