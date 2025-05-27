<a name='assembly'></a>
# RecordPoint.Connectors.SDK.ContentReport

## Contents

- [ContentReportBuilderExtension](#T-RecordPoint-Connectors-SDK-ContentReport-ContentReportBuilderExtension 'RecordPoint.Connectors.SDK.ContentReport.ContentReportBuilderExtension')
  - [AddContentReports(services)](#M-RecordPoint-Connectors-SDK-ContentReport-ContentReportBuilderExtension-AddContentReports-Microsoft-Extensions-DependencyInjection-IServiceCollection- 'RecordPoint.Connectors.SDK.ContentReport.ContentReportBuilderExtension.AddContentReports(Microsoft.Extensions.DependencyInjection.IServiceCollection)')
- [ContentReportContext](#T-RecordPoint-Connectors-SDK-ContentReport-ContentReportContext 'RecordPoint.Connectors.SDK.ContentReport.ContentReportContext')
- [ContentReportFilter](#T-RecordPoint-Connectors-SDK-ContentReport-ContentReportFilter 'RecordPoint.Connectors.SDK.ContentReport.ContentReportFilter')
  - [GetRecords(config,input)](#M-RecordPoint-Connectors-SDK-ContentReport-ContentReportFilter-GetRecords-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Content-Record}- 'RecordPoint.Connectors.SDK.ContentReport.ContentReportFilter.GetRecords(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Content.Record})')
- [ContentReportHostBuilderExtensions](#T-RecordPoint-Connectors-SDK-ContentReport-ContentReportHostBuilderExtensions 'RecordPoint.Connectors.SDK.ContentReport.ContentReportHostBuilderExtensions')
  - [UseContentReport(hostBuilder)](#M-RecordPoint-Connectors-SDK-ContentReport-ContentReportHostBuilderExtensions-UseContentReport-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.ContentReport.ContentReportHostBuilderExtensions.UseContentReport(Microsoft.Extensions.Hosting.IHostBuilder)')
- [ContentReportStandardDimensions](#T-RecordPoint-Connectors-SDK-ContentReport-ContentReportStandardDimensions 'RecordPoint.Connectors.SDK.ContentReport.ContentReportStandardDimensions')
  - [SERVICE_NAME](#F-RecordPoint-Connectors-SDK-ContentReport-ContentReportStandardDimensions-SERVICE_NAME 'RecordPoint.Connectors.SDK.ContentReport.ContentReportStandardDimensions.SERVICE_NAME')
- [ContentReportState](#T-RecordPoint-Connectors-SDK-ContentReport-ContentReportState 'RecordPoint.Connectors.SDK.ContentReport.ContentReportState')
  - [LatestStateType](#P-RecordPoint-Connectors-SDK-ContentReport-ContentReportState-LatestStateType 'RecordPoint.Connectors.SDK.ContentReport.ContentReportState.LatestStateType')
- [ContentWorkQueueExtensions](#T-RecordPoint-Connectors-SDK-ContentManager-ContentWorkQueueExtensions 'RecordPoint.Connectors.SDK.ContentManager.ContentWorkQueueExtensions')
  - [GenerateReportAsync(workQueueClient,contentSubmissionConfiguration,record,waitTill,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentWorkQueueExtensions-GenerateReportAsync-RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-ContentManager-ContentSubmissionConfiguration,RecordPoint-Connectors-SDK-Content-Record,System-Nullable{System-DateTimeOffset},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.ContentWorkQueueExtensions.GenerateReportAsync(RecordPoint.Connectors.SDK.Work.IWorkQueueClient,RecordPoint.Connectors.SDK.ContentManager.ContentSubmissionConfiguration,RecordPoint.Connectors.SDK.Content.Record,System.Nullable{System.DateTimeOffset},System.Threading.CancellationToken)')
- [ExportBuilderExtensions](#T-RecordPoint-Connectors-SDK-ContentReport-Export-ExportBuilderExtensions 'RecordPoint.Connectors.SDK.ContentReport.Export.ExportBuilderExtensions')
- [ExportOptions](#T-RecordPoint-Connectors-SDK-ContentReport-Export-ExportOptions 'RecordPoint.Connectors.SDK.ContentReport.Export.ExportOptions')
  - [DirectoryFormat](#P-RecordPoint-Connectors-SDK-ContentReport-Export-ExportOptions-DirectoryFormat 'RecordPoint.Connectors.SDK.ContentReport.Export.ExportOptions.DirectoryFormat')
  - [MaximumRecordsRollover](#P-RecordPoint-Connectors-SDK-ContentReport-Export-ExportOptions-MaximumRecordsRollover 'RecordPoint.Connectors.SDK.ContentReport.Export.ExportOptions.MaximumRecordsRollover')
- [GenerateReportHandler](#T-RecordPoint-Connectors-SDK-ContentReport-GenerateReportHandler 'RecordPoint.Connectors.SDK.ContentReport.GenerateReportHandler')
- [GenerateReportOperation](#T-RecordPoint-Connectors-SDK-ContentReport-GenerateReportOperation 'RecordPoint.Connectors.SDK.ContentReport.GenerateReportOperation')
  - [GetCustomKeyDimensions()](#M-RecordPoint-Connectors-SDK-ContentReport-GenerateReportOperation-GetCustomKeyDimensions 'RecordPoint.Connectors.SDK.ContentReport.GenerateReportOperation.GetCustomKeyDimensions')
- [IContentReportContext](#T-RecordPoint-Connectors-SDK-ContentReport-IContentReportContext 'RecordPoint.Connectors.SDK.ContentReport.IContentReportContext')
  - [GetServiceId()](#M-RecordPoint-Connectors-SDK-ContentReport-IContentReportContext-GetServiceId 'RecordPoint.Connectors.SDK.ContentReport.IContentReportContext.GetServiceId')
  - [GetServiceName()](#M-RecordPoint-Connectors-SDK-ContentReport-IContentReportContext-GetServiceName 'RecordPoint.Connectors.SDK.ContentReport.IContentReportContext.GetServiceName')

<a name='T-RecordPoint-Connectors-SDK-ContentReport-ContentReportBuilderExtension'></a>
## ContentReportBuilderExtension `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentReport

##### Summary

Content reports builder extensions

<a name='M-RecordPoint-Connectors-SDK-ContentReport-ContentReportBuilderExtension-AddContentReports-Microsoft-Extensions-DependencyInjection-IServiceCollection-'></a>
### AddContentReports(services) `method`

##### Summary

Add the content manager

##### Returns

Updated service collection

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| services | [Microsoft.Extensions.DependencyInjection.IServiceCollection](#T-Microsoft-Extensions-DependencyInjection-IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection') | Service collection to add to |

<a name='T-RecordPoint-Connectors-SDK-ContentReport-ContentReportContext'></a>
## ContentReportContext `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentReport

##### Summary

Provides overall operating context to the content report components.
Allows consistent logging scopes to be created.

TODO Standardise! Base Classes! Namespaces!

<a name='T-RecordPoint-Connectors-SDK-ContentReport-ContentReportFilter'></a>
## ContentReportFilter `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentReport

<a name='M-RecordPoint-Connectors-SDK-ContentReport-ContentReportFilter-GetRecords-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Content-Record}-'></a>
### GetRecords(config,input) `method`

##### Summary

Get filtered records for report

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| config | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') |  |
| input | [System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Content.Record}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IList 'System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Content.Record}') |  |

<a name='T-RecordPoint-Connectors-SDK-ContentReport-ContentReportHostBuilderExtensions'></a>
## ContentReportHostBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentReport

##### Summary

Content Report Host Builder Extensions

<a name='M-RecordPoint-Connectors-SDK-ContentReport-ContentReportHostBuilderExtensions-UseContentReport-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseContentReport(hostBuilder) `method`

##### Summary

Use Connector Content Reporting

##### Returns

Updated host builder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | App host builder |

<a name='T-RecordPoint-Connectors-SDK-ContentReport-ContentReportStandardDimensions'></a>
## ContentReportStandardDimensions `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentReport

##### Summary

Content report standard dimensions

<a name='F-RecordPoint-Connectors-SDK-ContentReport-ContentReportStandardDimensions-SERVICE_NAME'></a>
### SERVICE_NAME `constants`

##### Summary

Service Name for content report components

<a name='T-RecordPoint-Connectors-SDK-ContentReport-ContentReportState'></a>
## ContentReportState `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentReport

##### Summary

Running state for the content report work item

<a name='P-RecordPoint-Connectors-SDK-ContentReport-ContentReportState-LatestStateType'></a>
### LatestStateType `property`

##### Summary

Latest version of the sync state type

<a name='T-RecordPoint-Connectors-SDK-ContentManager-ContentWorkQueueExtensions'></a>
## ContentWorkQueueExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentWorkQueueExtensions-GenerateReportAsync-RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-ContentManager-ContentSubmissionConfiguration,RecordPoint-Connectors-SDK-Content-Record,System-Nullable{System-DateTimeOffset},System-Threading-CancellationToken-'></a>
### GenerateReportAsync(workQueueClient,contentSubmissionConfiguration,record,waitTill,cancellationToken) `method`

##### Summary

Submit a content discovery operation to the work queue

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

<a name='T-RecordPoint-Connectors-SDK-ContentReport-Export-ExportBuilderExtensions'></a>
## ExportBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentReport.Export

##### Summary

Export manager builder extensions

<a name='T-RecordPoint-Connectors-SDK-ContentReport-Export-ExportOptions'></a>
## ExportOptions `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentReport.Export

<a name='P-RecordPoint-Connectors-SDK-ContentReport-Export-ExportOptions-DirectoryFormat'></a>
### DirectoryFormat `property`

##### Summary

Format of folder to store exported csv files per report request

<a name='P-RecordPoint-Connectors-SDK-ContentReport-Export-ExportOptions-MaximumRecordsRollover'></a>
### MaximumRecordsRollover `property`

##### Summary

Maxiumn records before rolling to new file

<a name='T-RecordPoint-Connectors-SDK-ContentReport-GenerateReportHandler'></a>
## GenerateReportHandler `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentReport

##### Summary

Generate report notification handler that just forwards it to a work queue

<a name='T-RecordPoint-Connectors-SDK-ContentReport-GenerateReportOperation'></a>
## GenerateReportOperation `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentReport

<a name='M-RecordPoint-Connectors-SDK-ContentReport-GenerateReportOperation-GetCustomKeyDimensions'></a>
### GetCustomKeyDimensions() `method`

##### Summary

Get custom key dimensions

##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-ContentReport-IContentReportContext'></a>
## IContentReportContext `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentReport

##### Summary

Provides a context which the components of content report can use to get
cross-cutting information.

TODO Replace "Environment" across the board with "Context"
TODO Make a new top level namespace

<a name='M-RecordPoint-Connectors-SDK-ContentReport-IContentReportContext-GetServiceId'></a>
### GetServiceId() `method`

##### Summary

Get the service correlation ID as used in standard loggging

##### Returns

Service Id

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentReport-IContentReportContext-GetServiceName'></a>
### GetServiceName() `method`

##### Summary

Get the name of the service as used in standard logging

##### Returns

Service name

##### Parameters

This method has no parameters.
