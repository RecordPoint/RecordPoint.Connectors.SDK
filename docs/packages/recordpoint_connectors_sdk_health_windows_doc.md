<a name='assembly'></a>
# RecordPoint.Connectors.SDK.Health.Windows

## Contents

- [WindowsHealthBuilderExtension](#T-RecordPoint-Connectors-SDK-Health-Windows-WindowsHealthBuilderExtension 'RecordPoint.Connectors.SDK.Health.Windows.WindowsHealthBuilderExtension')
  - [AddOnPremHealthComponents(services)](#M-RecordPoint-Connectors-SDK-Health-Windows-WindowsHealthBuilderExtension-AddOnPremHealthComponents-Microsoft-Extensions-DependencyInjection-IServiceCollection- 'RecordPoint.Connectors.SDK.Health.Windows.WindowsHealthBuilderExtension.AddOnPremHealthComponents(Microsoft.Extensions.DependencyInjection.IServiceCollection)')
- [WindowsHealthChecker](#T-RecordPoint-Connectors-SDK-Health-Windows-WindowsHealthChecker 'RecordPoint.Connectors.SDK.Health.Windows.WindowsHealthChecker')
  - [#ctor(healthCheckOptions)](#M-RecordPoint-Connectors-SDK-Health-Windows-WindowsHealthChecker-#ctor-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Health-HealthCheckOptions}- 'RecordPoint.Connectors.SDK.Health.Windows.WindowsHealthChecker.#ctor(Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Health.HealthCheckOptions})')
  - [STATUS_HEALTH_CHECK_TYPE](#F-RecordPoint-Connectors-SDK-Health-Windows-WindowsHealthChecker-STATUS_HEALTH_CHECK_TYPE 'RecordPoint.Connectors.SDK.Health.Windows.WindowsHealthChecker.STATUS_HEALTH_CHECK_TYPE')
  - [_healthCheckOptions](#F-RecordPoint-Connectors-SDK-Health-Windows-WindowsHealthChecker-_healthCheckOptions 'RecordPoint.Connectors.SDK.Health.Windows.WindowsHealthChecker._healthCheckOptions')
  - [HealthCheckType](#P-RecordPoint-Connectors-SDK-Health-Windows-WindowsHealthChecker-HealthCheckType 'RecordPoint.Connectors.SDK.Health.Windows.WindowsHealthChecker.HealthCheckType')
  - [GetDiskSpaceInfo()](#M-RecordPoint-Connectors-SDK-Health-Windows-WindowsHealthChecker-GetDiskSpaceInfo 'RecordPoint.Connectors.SDK.Health.Windows.WindowsHealthChecker.GetDiskSpaceInfo')
  - [GetMemoryGb(managementBaseObject,key)](#M-RecordPoint-Connectors-SDK-Health-Windows-WindowsHealthChecker-GetMemoryGb-System-Management-ManagementBaseObject,System-String- 'RecordPoint.Connectors.SDK.Health.Windows.WindowsHealthChecker.GetMemoryGb(System.Management.ManagementBaseObject,System.String)')
  - [GetSystemMemoryInfo()](#M-RecordPoint-Connectors-SDK-Health-Windows-WindowsHealthChecker-GetSystemMemoryInfo 'RecordPoint.Connectors.SDK.Health.Windows.WindowsHealthChecker.GetSystemMemoryInfo')
  - [HealthCheckAsync(stoppingToken)](#M-RecordPoint-Connectors-SDK-Health-Windows-WindowsHealthChecker-HealthCheckAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Health.Windows.WindowsHealthChecker.HealthCheckAsync(System.Threading.CancellationToken)')
  - [IsRootPathOrSystemDiskSpaceWarning(volumeLabel,volumeSize)](#M-RecordPoint-Connectors-SDK-Health-Windows-WindowsHealthChecker-IsRootPathOrSystemDiskSpaceWarning-System-String,System-Double- 'RecordPoint.Connectors.SDK.Health.Windows.WindowsHealthChecker.IsRootPathOrSystemDiskSpaceWarning(System.String,System.Double)')

<a name='T-RecordPoint-Connectors-SDK-Health-Windows-WindowsHealthBuilderExtension'></a>
## WindowsHealthBuilderExtension `type`

##### Namespace

RecordPoint.Connectors.SDK.Health.Windows

##### Summary

Windows Health builder extensions

<a name='M-RecordPoint-Connectors-SDK-Health-Windows-WindowsHealthBuilderExtension-AddOnPremHealthComponents-Microsoft-Extensions-DependencyInjection-IServiceCollection-'></a>
### AddOnPremHealthComponents(services) `method`

##### Summary

Add Windows health components

##### Returns

Updated service collection

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| services | [Microsoft.Extensions.DependencyInjection.IServiceCollection](#T-Microsoft-Extensions-DependencyInjection-IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection') | Service collection to add to |

<a name='T-RecordPoint-Connectors-SDK-Health-Windows-WindowsHealthChecker'></a>
## WindowsHealthChecker `type`

##### Namespace

RecordPoint.Connectors.SDK.Health.Windows

##### Summary

The windows health checker.

<a name='M-RecordPoint-Connectors-SDK-Health-Windows-WindowsHealthChecker-#ctor-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Health-HealthCheckOptions}-'></a>
### #ctor(healthCheckOptions) `constructor`

##### Summary

Initializes a new instance of the [WindowsHealthChecker](#T-RecordPoint-Connectors-SDK-Health-Windows-WindowsHealthChecker 'RecordPoint.Connectors.SDK.Health.Windows.WindowsHealthChecker') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| healthCheckOptions | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Health.HealthCheckOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Health-HealthCheckOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Health.HealthCheckOptions}') | The health check options. |

<a name='F-RecordPoint-Connectors-SDK-Health-Windows-WindowsHealthChecker-STATUS_HEALTH_CHECK_TYPE'></a>
### STATUS_HEALTH_CHECK_TYPE `constants`

##### Summary

The STATUS HEALTH CHECK TYPE.

<a name='F-RecordPoint-Connectors-SDK-Health-Windows-WindowsHealthChecker-_healthCheckOptions'></a>
### _healthCheckOptions `constants`

##### Summary

The health check options.

<a name='P-RecordPoint-Connectors-SDK-Health-Windows-WindowsHealthChecker-HealthCheckType'></a>
### HealthCheckType `property`

##### Summary

Gets the health check type.

<a name='M-RecordPoint-Connectors-SDK-Health-Windows-WindowsHealthChecker-GetDiskSpaceInfo'></a>
### GetDiskSpaceInfo() `method`

##### Summary

Get disk space info.

##### Returns

List<HealthCheckMeasure>

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Health-Windows-WindowsHealthChecker-GetMemoryGb-System-Management-ManagementBaseObject,System-String-'></a>
### GetMemoryGb(managementBaseObject,key) `method`

##### Summary

Get memory gb.

##### Returns

A long

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| managementBaseObject | [System.Management.ManagementBaseObject](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Management.ManagementBaseObject 'System.Management.ManagementBaseObject') | The management base object. |
| key | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The key. |

<a name='M-RecordPoint-Connectors-SDK-Health-Windows-WindowsHealthChecker-GetSystemMemoryInfo'></a>
### GetSystemMemoryInfo() `method`

##### Summary

Get window's memory info

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Health-Windows-WindowsHealthChecker-HealthCheckAsync-System-Threading-CancellationToken-'></a>
### HealthCheckAsync(stoppingToken) `method`

##### Summary

Health the check asynchronously.

##### Returns

Task<HealthCheckResult>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| stoppingToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The stopping token. |

<a name='M-RecordPoint-Connectors-SDK-Health-Windows-WindowsHealthChecker-IsRootPathOrSystemDiskSpaceWarning-System-String,System-Double-'></a>
### IsRootPathOrSystemDiskSpaceWarning(volumeLabel,volumeSize) `method`

##### Summary

Checks if is root path or system disk space warning.

##### Returns

A bool

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| volumeLabel | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The volume label. |
| volumeSize | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | The volume size. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [RecordPoint.Connectors.SDK.RequiredValueNullException](#T-RecordPoint-Connectors-SDK-RequiredValueNullException 'RecordPoint.Connectors.SDK.RequiredValueNullException') |  |
