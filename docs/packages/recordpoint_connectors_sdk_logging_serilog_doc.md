<a name='assembly'></a>
# RecordPoint.Connectors.SDK.Logging.Serilog

## Contents

- [SerilogLogBuilderExtensions](#T-RecordPoint-Connectors-SDK-Logging-Serilog-SerilogLogBuilderExtensions 'RecordPoint.Connectors.SDK.Logging.Serilog.SerilogLogBuilderExtensions')
  - [LOG_TEMPLATE](#F-RecordPoint-Connectors-SDK-Logging-Serilog-SerilogLogBuilderExtensions-LOG_TEMPLATE 'RecordPoint.Connectors.SDK.Logging.Serilog.SerilogLogBuilderExtensions.LOG_TEMPLATE')
  - [AddConsoleLogging()](#M-RecordPoint-Connectors-SDK-Logging-Serilog-SerilogLogBuilderExtensions-AddConsoleLogging-Serilog-LoggerConfiguration- 'RecordPoint.Connectors.SDK.Logging.Serilog.SerilogLogBuilderExtensions.AddConsoleLogging(Serilog.LoggerConfiguration)')
  - [ConfigureLogging(serviceProvider,loggerConfiguration,hostBuilderContext)](#M-RecordPoint-Connectors-SDK-Logging-Serilog-SerilogLogBuilderExtensions-ConfigureLogging-Microsoft-Extensions-Hosting-HostBuilderContext,System-IServiceProvider,Serilog-LoggerConfiguration- 'RecordPoint.Connectors.SDK.Logging.Serilog.SerilogLogBuilderExtensions.ConfigureLogging(Microsoft.Extensions.Hosting.HostBuilderContext,System.IServiceProvider,Serilog.LoggerConfiguration)')
  - [UseSerilogApplicationInsightsLogger(hostBuilder)](#M-RecordPoint-Connectors-SDK-Logging-Serilog-SerilogLogBuilderExtensions-UseSerilogApplicationInsightsLogger-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Logging.Serilog.SerilogLogBuilderExtensions.UseSerilogApplicationInsightsLogger(Microsoft.Extensions.Hosting.IHostBuilder)')

<a name='T-RecordPoint-Connectors-SDK-Logging-Serilog-SerilogLogBuilderExtensions'></a>
## SerilogLogBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Logging.Serilog

##### Summary

Serilog log builder extensions

<a name='F-RecordPoint-Connectors-SDK-Logging-Serilog-SerilogLogBuilderExtensions-LOG_TEMPLATE'></a>
### LOG_TEMPLATE `constants`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Logging-Serilog-SerilogLogBuilderExtensions-AddConsoleLogging-Serilog-LoggerConfiguration-'></a>
### AddConsoleLogging() `method`

##### Summary

Add console to the logger configuration

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Logging-Serilog-SerilogLogBuilderExtensions-ConfigureLogging-Microsoft-Extensions-Hosting-HostBuilderContext,System-IServiceProvider,Serilog-LoggerConfiguration-'></a>
### ConfigureLogging(serviceProvider,loggerConfiguration,hostBuilderContext) `method`

##### Summary

Config appInsights as main logging and file logging is backup

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| serviceProvider | [Microsoft.Extensions.Hosting.HostBuilderContext](#T-Microsoft-Extensions-Hosting-HostBuilderContext 'Microsoft.Extensions.Hosting.HostBuilderContext') |  |
| loggerConfiguration | [System.IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider') |  |
| hostBuilderContext | [Serilog.LoggerConfiguration](#T-Serilog-LoggerConfiguration 'Serilog.LoggerConfiguration') |  |

<a name='M-RecordPoint-Connectors-SDK-Logging-Serilog-SerilogLogBuilderExtensions-UseSerilogApplicationInsightsLogger-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseSerilogApplicationInsightsLogger(hostBuilder) `method`

##### Summary

Use the serilog based appInsights

##### Returns

Updated host builder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | Host builder to configure |
