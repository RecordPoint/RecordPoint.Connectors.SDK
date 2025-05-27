<a name='assembly'></a>
# RecordPoint.Connectors.SDK.Abstractions

## Contents

- [ActionResultBase](#T-RecordPoint-Connectors-SDK-ContentManager-ActionResultBase 'RecordPoint.Connectors.SDK.ContentManager.ActionResultBase')
  - [Dimensions](#P-RecordPoint-Connectors-SDK-ContentManager-ActionResultBase-Dimensions 'RecordPoint.Connectors.SDK.ContentManager.ActionResultBase.Dimensions')
  - [Measures](#P-RecordPoint-Connectors-SDK-ContentManager-ActionResultBase-Measures 'RecordPoint.Connectors.SDK.ContentManager.ActionResultBase.Measures')
  - [NextDelay](#P-RecordPoint-Connectors-SDK-ContentManager-ActionResultBase-NextDelay 'RecordPoint.Connectors.SDK.ContentManager.ActionResultBase.NextDelay')
  - [SemaphoreLockType](#P-RecordPoint-Connectors-SDK-ContentManager-ActionResultBase-SemaphoreLockType 'RecordPoint.Connectors.SDK.ContentManager.ActionResultBase.SemaphoreLockType')
- [Aggregation](#T-RecordPoint-Connectors-SDK-Content-Aggregation 'RecordPoint.Connectors.SDK.Content.Aggregation')
  - [ItemTypeId](#F-RecordPoint-Connectors-SDK-Content-Aggregation-ItemTypeId 'RecordPoint.Connectors.SDK.Content.Aggregation.ItemTypeId')
  - [Location](#P-RecordPoint-Connectors-SDK-Content-Aggregation-Location 'RecordPoint.Connectors.SDK.Content.Aggregation.Location')
  - [ParentExternalId](#P-RecordPoint-Connectors-SDK-Content-Aggregation-ParentExternalId 'RecordPoint.Connectors.SDK.Content.Aggregation.ParentExternalId')
  - [Equals(other)](#M-RecordPoint-Connectors-SDK-Content-Aggregation-Equals-RecordPoint-Connectors-SDK-Content-Aggregation- 'RecordPoint.Connectors.SDK.Content.Aggregation.Equals(RecordPoint.Connectors.SDK.Content.Aggregation)')
  - [Equals(obj)](#M-RecordPoint-Connectors-SDK-Content-Aggregation-Equals-System-Object- 'RecordPoint.Connectors.SDK.Content.Aggregation.Equals(System.Object)')
  - [GetHashCode()](#M-RecordPoint-Connectors-SDK-Content-Aggregation-GetHashCode 'RecordPoint.Connectors.SDK.Content.Aggregation.GetHashCode')
- [AggregationExtentions](#T-RecordPoint-Connectors-SDK-Content-AggregationExtentions 'RecordPoint.Connectors.SDK.Content.AggregationExtentions')
  - [ToAggregation(aggregationModel)](#M-RecordPoint-Connectors-SDK-Content-AggregationExtentions-ToAggregation-RecordPoint-Connectors-SDK-Content-AggregationModel- 'RecordPoint.Connectors.SDK.Content.AggregationExtentions.ToAggregation(RecordPoint.Connectors.SDK.Content.AggregationModel)')
  - [ToAggregationList(aggregationModels)](#M-RecordPoint-Connectors-SDK-Content-AggregationExtentions-ToAggregationList-System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Content-AggregationModel}- 'RecordPoint.Connectors.SDK.Content.AggregationExtentions.ToAggregationList(System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Content.AggregationModel})')
  - [ToAggregationModel(aggregation)](#M-RecordPoint-Connectors-SDK-Content-AggregationExtentions-ToAggregationModel-RecordPoint-Connectors-SDK-Content-Aggregation- 'RecordPoint.Connectors.SDK.Content.AggregationExtentions.ToAggregationModel(RecordPoint.Connectors.SDK.Content.Aggregation)')
  - [ToAggregationModelList(aggregations)](#M-RecordPoint-Connectors-SDK-Content-AggregationExtentions-ToAggregationModelList-System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Content-Aggregation}- 'RecordPoint.Connectors.SDK.Content.AggregationExtentions.ToAggregationModelList(System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Content.Aggregation})')
- [AggregationModel](#T-RecordPoint-Connectors-SDK-Content-AggregationModel 'RecordPoint.Connectors.SDK.Content.AggregationModel')
  - [ConnectorId](#P-RecordPoint-Connectors-SDK-Content-AggregationModel-ConnectorId 'RecordPoint.Connectors.SDK.Content.AggregationModel.ConnectorId')
  - [CreatedDate](#P-RecordPoint-Connectors-SDK-Content-AggregationModel-CreatedDate 'RecordPoint.Connectors.SDK.Content.AggregationModel.CreatedDate')
  - [ExternalId](#P-RecordPoint-Connectors-SDK-Content-AggregationModel-ExternalId 'RecordPoint.Connectors.SDK.Content.AggregationModel.ExternalId')
  - [Location](#P-RecordPoint-Connectors-SDK-Content-AggregationModel-Location 'RecordPoint.Connectors.SDK.Content.AggregationModel.Location')
  - [MetaData](#P-RecordPoint-Connectors-SDK-Content-AggregationModel-MetaData 'RecordPoint.Connectors.SDK.Content.AggregationModel.MetaData')
  - [ParentExternalId](#P-RecordPoint-Connectors-SDK-Content-AggregationModel-ParentExternalId 'RecordPoint.Connectors.SDK.Content.AggregationModel.ParentExternalId')
  - [Title](#P-RecordPoint-Connectors-SDK-Content-AggregationModel-Title 'RecordPoint.Connectors.SDK.Content.AggregationModel.Title')
  - [Equals(other)](#M-RecordPoint-Connectors-SDK-Content-AggregationModel-Equals-RecordPoint-Connectors-SDK-Content-AggregationModel- 'RecordPoint.Connectors.SDK.Content.AggregationModel.Equals(RecordPoint.Connectors.SDK.Content.AggregationModel)')
  - [Equals(obj)](#M-RecordPoint-Connectors-SDK-Content-AggregationModel-Equals-System-Object- 'RecordPoint.Connectors.SDK.Content.AggregationModel.Equals(System.Object)')
  - [GetHashCode()](#M-RecordPoint-Connectors-SDK-Content-AggregationModel-GetHashCode 'RecordPoint.Connectors.SDK.Content.AggregationModel.GetHashCode')
- [ApplicationInsightOptions](#T-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightOptions 'RecordPoint.Connectors.SDK.Observability.AppInsights.ApplicationInsightOptions')
  - [OPTION_NAME](#F-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightOptions-OPTION_NAME 'RecordPoint.Connectors.SDK.Observability.AppInsights.ApplicationInsightOptions.OPTION_NAME')
  - [ConnectionString](#P-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightOptions-ConnectionString 'RecordPoint.Connectors.SDK.Observability.AppInsights.ApplicationInsightOptions.ConnectionString')
  - [IncludeKubernetesEnricher](#P-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightOptions-IncludeKubernetesEnricher 'RecordPoint.Connectors.SDK.Observability.AppInsights.ApplicationInsightOptions.IncludeKubernetesEnricher')
  - [InstrumentationKey](#P-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightOptions-InstrumentationKey 'RecordPoint.Connectors.SDK.Observability.AppInsights.ApplicationInsightOptions.InstrumentationKey')
  - [LogLevel](#P-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightOptions-LogLevel 'RecordPoint.Connectors.SDK.Observability.AppInsights.ApplicationInsightOptions.LogLevel')
- [AuditEvent](#T-RecordPoint-Connectors-SDK-Content-AuditEvent 'RecordPoint.Connectors.SDK.Content.AuditEvent')
  - [CreatedDate](#P-RecordPoint-Connectors-SDK-Content-AuditEvent-CreatedDate 'RecordPoint.Connectors.SDK.Content.AuditEvent.CreatedDate')
  - [Description](#P-RecordPoint-Connectors-SDK-Content-AuditEvent-Description 'RecordPoint.Connectors.SDK.Content.AuditEvent.Description')
  - [EventExternalId](#P-RecordPoint-Connectors-SDK-Content-AuditEvent-EventExternalId 'RecordPoint.Connectors.SDK.Content.AuditEvent.EventExternalId')
  - [EventType](#P-RecordPoint-Connectors-SDK-Content-AuditEvent-EventType 'RecordPoint.Connectors.SDK.Content.AuditEvent.EventType')
  - [ExternalId](#P-RecordPoint-Connectors-SDK-Content-AuditEvent-ExternalId 'RecordPoint.Connectors.SDK.Content.AuditEvent.ExternalId')
  - [MetaDataItems](#P-RecordPoint-Connectors-SDK-Content-AuditEvent-MetaDataItems 'RecordPoint.Connectors.SDK.Content.AuditEvent.MetaDataItems')
  - [UserId](#P-RecordPoint-Connectors-SDK-Content-AuditEvent-UserId 'RecordPoint.Connectors.SDK.Content.AuditEvent.UserId')
  - [UserName](#P-RecordPoint-Connectors-SDK-Content-AuditEvent-UserName 'RecordPoint.Connectors.SDK.Content.AuditEvent.UserName')
  - [Equals(other)](#M-RecordPoint-Connectors-SDK-Content-AuditEvent-Equals-RecordPoint-Connectors-SDK-Content-AuditEvent- 'RecordPoint.Connectors.SDK.Content.AuditEvent.Equals(RecordPoint.Connectors.SDK.Content.AuditEvent)')
  - [Equals(obj)](#M-RecordPoint-Connectors-SDK-Content-AuditEvent-Equals-System-Object- 'RecordPoint.Connectors.SDK.Content.AuditEvent.Equals(System.Object)')
  - [GetHashCode()](#M-RecordPoint-Connectors-SDK-Content-AuditEvent-GetHashCode 'RecordPoint.Connectors.SDK.Content.AuditEvent.GetHashCode')
- [AzureAuthenticationOptions](#T-RecordPoint-Connectors-SDK-Configuration-AzureAuthenticationOptions 'RecordPoint.Connectors.SDK.Configuration.AzureAuthenticationOptions')
  - [SECTION_NAME](#F-RecordPoint-Connectors-SDK-Configuration-AzureAuthenticationOptions-SECTION_NAME 'RecordPoint.Connectors.SDK.Configuration.AzureAuthenticationOptions.SECTION_NAME')
  - [ClientId](#P-RecordPoint-Connectors-SDK-Configuration-AzureAuthenticationOptions-ClientId 'RecordPoint.Connectors.SDK.Configuration.AzureAuthenticationOptions.ClientId')
  - [ClientSecret](#P-RecordPoint-Connectors-SDK-Configuration-AzureAuthenticationOptions-ClientSecret 'RecordPoint.Connectors.SDK.Configuration.AzureAuthenticationOptions.ClientSecret')
  - [TenantId](#P-RecordPoint-Connectors-SDK-Configuration-AzureAuthenticationOptions-TenantId 'RecordPoint.Connectors.SDK.Configuration.AzureAuthenticationOptions.TenantId')
  - [UseVsCredentials](#P-RecordPoint-Connectors-SDK-Configuration-AzureAuthenticationOptions-UseVsCredentials 'RecordPoint.Connectors.SDK.Configuration.AzureAuthenticationOptions.UseVsCredentials')
- [AzureAuthenticationOptionsExtensions](#T-RecordPoint-Connectors-SDK-Configuration-AzureAuthenticationOptionsExtensions 'RecordPoint.Connectors.SDK.Configuration.AzureAuthenticationOptionsExtensions')
  - [GetTokenCredential(options)](#M-RecordPoint-Connectors-SDK-Configuration-AzureAuthenticationOptionsExtensions-GetTokenCredential-RecordPoint-Connectors-SDK-Configuration-AzureAuthenticationOptions- 'RecordPoint.Connectors.SDK.Configuration.AzureAuthenticationOptionsExtensions.GetTokenCredential(RecordPoint.Connectors.SDK.Configuration.AzureAuthenticationOptions)')
- [BinaryMetaInfo](#T-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo 'RecordPoint.Connectors.SDK.Content.BinaryMetaInfo')
  - [ContentToken](#P-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo-ContentToken 'RecordPoint.Connectors.SDK.Content.BinaryMetaInfo.ContentToken')
  - [ContentTokenType](#P-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo-ContentTokenType 'RecordPoint.Connectors.SDK.Content.BinaryMetaInfo.ContentTokenType')
  - [FileHash](#P-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo-FileHash 'RecordPoint.Connectors.SDK.Content.BinaryMetaInfo.FileHash')
  - [FileName](#P-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo-FileName 'RecordPoint.Connectors.SDK.Content.BinaryMetaInfo.FileName')
  - [FileSize](#P-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo-FileSize 'RecordPoint.Connectors.SDK.Content.BinaryMetaInfo.FileSize')
  - [ItemExternalId](#P-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo-ItemExternalId 'RecordPoint.Connectors.SDK.Content.BinaryMetaInfo.ItemExternalId')
  - [MimeType](#P-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo-MimeType 'RecordPoint.Connectors.SDK.Content.BinaryMetaInfo.MimeType')
  - [SkipEnrichment](#P-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo-SkipEnrichment 'RecordPoint.Connectors.SDK.Content.BinaryMetaInfo.SkipEnrichment')
  - [Equals(other)](#M-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo-Equals-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo- 'RecordPoint.Connectors.SDK.Content.BinaryMetaInfo.Equals(RecordPoint.Connectors.SDK.Content.BinaryMetaInfo)')
  - [Equals(obj)](#M-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo-Equals-System-Object- 'RecordPoint.Connectors.SDK.Content.BinaryMetaInfo.Equals(System.Object)')
  - [GetHashCode()](#M-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo-GetHashCode 'RecordPoint.Connectors.SDK.Content.BinaryMetaInfo.GetHashCode')
- [BinaryMetaInfoExtensions](#T-RecordPoint-Connectors-SDK-Content-BinaryMetaInfoExtensions 'RecordPoint.Connectors.SDK.Content.BinaryMetaInfoExtensions')
  - [IsEqual(x,y)](#M-RecordPoint-Connectors-SDK-Content-BinaryMetaInfoExtensions-IsEqual-System-Collections-Generic-List{RecordPoint-Connectors-SDK-Content-BinaryMetaInfo},System-Collections-Generic-List{RecordPoint-Connectors-SDK-Content-BinaryMetaInfo}- 'RecordPoint.Connectors.SDK.Content.BinaryMetaInfoExtensions.IsEqual(System.Collections.Generic.List{RecordPoint.Connectors.SDK.Content.BinaryMetaInfo},System.Collections.Generic.List{RecordPoint.Connectors.SDK.Content.BinaryMetaInfo})')
- [BinaryRetrievalResult](#T-RecordPoint-Connectors-SDK-ContentManager-BinaryRetrievalResult 'RecordPoint.Connectors.SDK.ContentManager.BinaryRetrievalResult')
  - [Exception](#P-RecordPoint-Connectors-SDK-ContentManager-BinaryRetrievalResult-Exception 'RecordPoint.Connectors.SDK.ContentManager.BinaryRetrievalResult.Exception')
  - [Reason](#P-RecordPoint-Connectors-SDK-ContentManager-BinaryRetrievalResult-Reason 'RecordPoint.Connectors.SDK.ContentManager.BinaryRetrievalResult.Reason')
  - [ResultType](#P-RecordPoint-Connectors-SDK-ContentManager-BinaryRetrievalResult-ResultType 'RecordPoint.Connectors.SDK.ContentManager.BinaryRetrievalResult.ResultType')
  - [Stream](#P-RecordPoint-Connectors-SDK-ContentManager-BinaryRetrievalResult-Stream 'RecordPoint.Connectors.SDK.ContentManager.BinaryRetrievalResult.Stream')
- [BinaryRetrievalResultType](#T-RecordPoint-Connectors-SDK-ContentManager-BinaryRetrievalResultType 'RecordPoint.Connectors.SDK.ContentManager.BinaryRetrievalResultType')
  - [Abandoned](#F-RecordPoint-Connectors-SDK-ContentManager-BinaryRetrievalResultType-Abandoned 'RecordPoint.Connectors.SDK.ContentManager.BinaryRetrievalResultType.Abandoned')
  - [BackOff](#F-RecordPoint-Connectors-SDK-ContentManager-BinaryRetrievalResultType-BackOff 'RecordPoint.Connectors.SDK.ContentManager.BinaryRetrievalResultType.BackOff')
  - [Complete](#F-RecordPoint-Connectors-SDK-ContentManager-BinaryRetrievalResultType-Complete 'RecordPoint.Connectors.SDK.ContentManager.BinaryRetrievalResultType.Complete')
  - [Deleted](#F-RecordPoint-Connectors-SDK-ContentManager-BinaryRetrievalResultType-Deleted 'RecordPoint.Connectors.SDK.ContentManager.BinaryRetrievalResultType.Deleted')
  - [Failed](#F-RecordPoint-Connectors-SDK-ContentManager-BinaryRetrievalResultType-Failed 'RecordPoint.Connectors.SDK.ContentManager.BinaryRetrievalResultType.Failed')
  - [ZeroBinary](#F-RecordPoint-Connectors-SDK-ContentManager-BinaryRetrievalResultType-ZeroBinary 'RecordPoint.Connectors.SDK.ContentManager.BinaryRetrievalResultType.ZeroBinary')
- [Channel](#T-RecordPoint-Connectors-SDK-Content-Channel 'RecordPoint.Connectors.SDK.Content.Channel')
  - [NULL_CHANNEL_ID](#F-RecordPoint-Connectors-SDK-Content-Channel-NULL_CHANNEL_ID 'RecordPoint.Connectors.SDK.Content.Channel.NULL_CHANNEL_ID')
  - [NULL_CHANNEL_TITLE](#F-RecordPoint-Connectors-SDK-Content-Channel-NULL_CHANNEL_TITLE 'RecordPoint.Connectors.SDK.Content.Channel.NULL_CHANNEL_TITLE')
  - [ExternalId](#P-RecordPoint-Connectors-SDK-Content-Channel-ExternalId 'RecordPoint.Connectors.SDK.Content.Channel.ExternalId')
  - [MetaDataItems](#P-RecordPoint-Connectors-SDK-Content-Channel-MetaDataItems 'RecordPoint.Connectors.SDK.Content.Channel.MetaDataItems')
  - [Title](#P-RecordPoint-Connectors-SDK-Content-Channel-Title 'RecordPoint.Connectors.SDK.Content.Channel.Title')
  - [CreateNullChannel()](#M-RecordPoint-Connectors-SDK-Content-Channel-CreateNullChannel 'RecordPoint.Connectors.SDK.Content.Channel.CreateNullChannel')
  - [Equals(other)](#M-RecordPoint-Connectors-SDK-Content-Channel-Equals-RecordPoint-Connectors-SDK-Content-Channel- 'RecordPoint.Connectors.SDK.Content.Channel.Equals(RecordPoint.Connectors.SDK.Content.Channel)')
  - [Equals(obj)](#M-RecordPoint-Connectors-SDK-Content-Channel-Equals-System-Object- 'RecordPoint.Connectors.SDK.Content.Channel.Equals(System.Object)')
  - [GetHashCode()](#M-RecordPoint-Connectors-SDK-Content-Channel-GetHashCode 'RecordPoint.Connectors.SDK.Content.Channel.GetHashCode')
- [ChannelDiscoveryConfiguration](#T-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryConfiguration 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryConfiguration')
  - [ConfigurationType](#P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryConfiguration-ConfigurationType 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryConfiguration.ConfigurationType')
  - [ConnectorConfigurationId](#P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryConfiguration-ConnectorConfigurationId 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryConfiguration.ConnectorConfigurationId')
  - [TenantDomainName](#P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryConfiguration-TenantDomainName 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryConfiguration.TenantDomainName')
  - [TenantId](#P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryConfiguration-TenantId 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryConfiguration.TenantId')
  - [Deserialize(configurationType,configurationText)](#M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryConfiguration-Deserialize-System-String,System-String- 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryConfiguration.Deserialize(System.String,System.String)')
  - [Serialize()](#M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryConfiguration-Serialize 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryConfiguration.Serialize')
- [ChannelDiscoveryOperationOptions](#T-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperationOptions 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperationOptions')
  - [SECTION_NAME](#F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperationOptions-SECTION_NAME 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperationOptions.SECTION_NAME')
  - [BatchSize](#P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperationOptions-BatchSize 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryOperationOptions.BatchSize')
- [ChannelDiscoveryResult](#T-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryResult')
  - [AuditEvents](#P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult-AuditEvents 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryResult.AuditEvents')
  - [Channels](#P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult-Channels 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryResult.Channels')
  - [Exception](#P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult-Exception 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryResult.Exception')
  - [NewChannelRegistrations](#P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult-NewChannelRegistrations 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryResult.NewChannelRegistrations')
  - [Reason](#P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult-Reason 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryResult.Reason')
  - [ResultType](#P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult-ResultType 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryResult.ResultType')
- [ChannelDiscoveryResultType](#T-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResultType 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryResultType')
  - [BackOff](#F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResultType-BackOff 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryResultType.BackOff')
  - [Complete](#F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResultType-Complete 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryResultType.Complete')
  - [Failed](#F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResultType-Failed 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryResultType.Failed')
- [ChannelDiscoveryState](#T-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryState 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryState')
  - [LastBackOffDelaySeconds](#P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryState-LastBackOffDelaySeconds 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryState.LastBackOffDelaySeconds')
  - [LatestStateType](#P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryState-LatestStateType 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryState.LatestStateType')
  - [Deserialize(stateType,stateText)](#M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryState-Deserialize-System-String,System-String- 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryState.Deserialize(System.String,System.String)')
  - [Serialize()](#M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryState-Serialize 'RecordPoint.Connectors.SDK.ContentManager.ChannelDiscoveryState.Serialize')
- [ChannelExtentions](#T-RecordPoint-Connectors-SDK-Content-ChannelExtentions 'RecordPoint.Connectors.SDK.Content.ChannelExtentions')
  - [ToChannel(channelModel)](#M-RecordPoint-Connectors-SDK-Content-ChannelExtentions-ToChannel-RecordPoint-Connectors-SDK-Content-ChannelModel- 'RecordPoint.Connectors.SDK.Content.ChannelExtentions.ToChannel(RecordPoint.Connectors.SDK.Content.ChannelModel)')
  - [ToChannelList(channelModels)](#M-RecordPoint-Connectors-SDK-Content-ChannelExtentions-ToChannelList-System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Content-ChannelModel}- 'RecordPoint.Connectors.SDK.Content.ChannelExtentions.ToChannelList(System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Content.ChannelModel})')
  - [ToChannelModel(channel)](#M-RecordPoint-Connectors-SDK-Content-ChannelExtentions-ToChannelModel-RecordPoint-Connectors-SDK-Content-Channel- 'RecordPoint.Connectors.SDK.Content.ChannelExtentions.ToChannelModel(RecordPoint.Connectors.SDK.Content.Channel)')
  - [ToChannelModelList(channels)](#M-RecordPoint-Connectors-SDK-Content-ChannelExtentions-ToChannelModelList-System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Content-Channel}- 'RecordPoint.Connectors.SDK.Content.ChannelExtentions.ToChannelModelList(System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Content.Channel})')
- [ChannelModel](#T-RecordPoint-Connectors-SDK-Content-ChannelModel 'RecordPoint.Connectors.SDK.Content.ChannelModel')
  - [ConnectorId](#P-RecordPoint-Connectors-SDK-Content-ChannelModel-ConnectorId 'RecordPoint.Connectors.SDK.Content.ChannelModel.ConnectorId')
  - [CreatedDate](#P-RecordPoint-Connectors-SDK-Content-ChannelModel-CreatedDate 'RecordPoint.Connectors.SDK.Content.ChannelModel.CreatedDate')
  - [ExternalId](#P-RecordPoint-Connectors-SDK-Content-ChannelModel-ExternalId 'RecordPoint.Connectors.SDK.Content.ChannelModel.ExternalId')
  - [MetaData](#P-RecordPoint-Connectors-SDK-Content-ChannelModel-MetaData 'RecordPoint.Connectors.SDK.Content.ChannelModel.MetaData')
  - [Title](#P-RecordPoint-Connectors-SDK-Content-ChannelModel-Title 'RecordPoint.Connectors.SDK.Content.ChannelModel.Title')
  - [Equals(other)](#M-RecordPoint-Connectors-SDK-Content-ChannelModel-Equals-RecordPoint-Connectors-SDK-Content-ChannelModel- 'RecordPoint.Connectors.SDK.Content.ChannelModel.Equals(RecordPoint.Connectors.SDK.Content.ChannelModel)')
  - [Equals(obj)](#M-RecordPoint-Connectors-SDK-Content-ChannelModel-Equals-System-Object- 'RecordPoint.Connectors.SDK.Content.ChannelModel.Equals(System.Object)')
  - [GetHashCode()](#M-RecordPoint-Connectors-SDK-Content-ChannelModel-GetHashCode 'RecordPoint.Connectors.SDK.Content.ChannelModel.GetHashCode')
- [ConnectorConfigurationModel](#T-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel 'RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel')
  - [ConnectorId](#P-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel-ConnectorId 'RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel.ConnectorId')
  - [ConnectorTypeId](#P-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel-ConnectorTypeId 'RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel.ConnectorTypeId')
  - [Data](#P-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel-Data 'RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel.Data')
  - [DisplayName](#P-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel-DisplayName 'RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel.DisplayName')
  - [ReportLocation](#P-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel-ReportLocation 'RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel.ReportLocation')
  - [Status](#P-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel-Status 'RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel.Status')
  - [TenantId](#P-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel-TenantId 'RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel.TenantId')
- [ConnectorFeatureStatus](#T-RecordPoint-Connectors-SDK-Connectors-ConnectorFeatureStatus 'RecordPoint.Connectors.SDK.Connectors.ConnectorFeatureStatus')
  - [ConnectorId](#P-RecordPoint-Connectors-SDK-Connectors-ConnectorFeatureStatus-ConnectorId 'RecordPoint.Connectors.SDK.Connectors.ConnectorFeatureStatus.ConnectorId')
  - [Enabled](#P-RecordPoint-Connectors-SDK-Connectors-ConnectorFeatureStatus-Enabled 'RecordPoint.Connectors.SDK.Connectors.ConnectorFeatureStatus.Enabled')
  - [EnabledReason](#P-RecordPoint-Connectors-SDK-Connectors-ConnectorFeatureStatus-EnabledReason 'RecordPoint.Connectors.SDK.Connectors.ConnectorFeatureStatus.EnabledReason')
  - [FeatureName](#P-RecordPoint-Connectors-SDK-Connectors-ConnectorFeatureStatus-FeatureName 'RecordPoint.Connectors.SDK.Connectors.ConnectorFeatureStatus.FeatureName')
- [ConnectorOptions](#T-RecordPoint-Connectors-SDK-Connectors-ConnectorOptions 'RecordPoint.Connectors.SDK.Connectors.ConnectorOptions')
  - [SECTION_NAME](#F-RecordPoint-Connectors-SDK-Connectors-ConnectorOptions-SECTION_NAME 'RecordPoint.Connectors.SDK.Connectors.ConnectorOptions.SECTION_NAME')
  - [BinariesEnabled](#P-RecordPoint-Connectors-SDK-Connectors-ConnectorOptions-BinariesEnabled 'RecordPoint.Connectors.SDK.Connectors.ConnectorOptions.BinariesEnabled')
  - [BinarySkipThreshold](#P-RecordPoint-Connectors-SDK-Connectors-ConnectorOptions-BinarySkipThreshold 'RecordPoint.Connectors.SDK.Connectors.ConnectorOptions.BinarySkipThreshold')
  - [ExponentialRetryDelay](#P-RecordPoint-Connectors-SDK-Connectors-ConnectorOptions-ExponentialRetryDelay 'RecordPoint.Connectors.SDK.Connectors.ConnectorOptions.ExponentialRetryDelay')
  - [MaxRetries](#P-RecordPoint-Connectors-SDK-Connectors-ConnectorOptions-MaxRetries 'RecordPoint.Connectors.SDK.Connectors.ConnectorOptions.MaxRetries')
  - [MaxRetryDelay](#P-RecordPoint-Connectors-SDK-Connectors-ConnectorOptions-MaxRetryDelay 'RecordPoint.Connectors.SDK.Connectors.ConnectorOptions.MaxRetryDelay')
  - [RetryDelay](#P-RecordPoint-Connectors-SDK-Connectors-ConnectorOptions-RetryDelay 'RecordPoint.Connectors.SDK.Connectors.ConnectorOptions.RetryDelay')
  - [RetryOnFailure](#P-RecordPoint-Connectors-SDK-Connectors-ConnectorOptions-RetryOnFailure 'RecordPoint.Connectors.SDK.Connectors.ConnectorOptions.RetryOnFailure')
  - [SubmissionEnabled](#P-RecordPoint-Connectors-SDK-Connectors-ConnectorOptions-SubmissionEnabled 'RecordPoint.Connectors.SDK.Connectors.ConnectorOptions.SubmissionEnabled')
- [ConnectorSecret](#T-RecordPoint-Connectors-SDK-Abstractions-Content-ConnectorSecret 'RecordPoint.Connectors.SDK.Abstractions.Content.ConnectorSecret')
  - [Field](#P-RecordPoint-Connectors-SDK-Abstractions-Content-ConnectorSecret-Field 'RecordPoint.Connectors.SDK.Abstractions.Content.ConnectorSecret.Field')
  - [Value](#P-RecordPoint-Connectors-SDK-Abstractions-Content-ConnectorSecret-Value 'RecordPoint.Connectors.SDK.Abstractions.Content.ConnectorSecret.Value')
- [ConsoleLoggingOptions](#T-RecordPoint-Connectors-SDK-Observability-Console-ConsoleLoggingOptions 'RecordPoint.Connectors.SDK.Observability.Console.ConsoleLoggingOptions')
  - [OPTION_NAME](#F-RecordPoint-Connectors-SDK-Observability-Console-ConsoleLoggingOptions-OPTION_NAME 'RecordPoint.Connectors.SDK.Observability.Console.ConsoleLoggingOptions.OPTION_NAME')
  - [LogLevel](#P-RecordPoint-Connectors-SDK-Observability-Console-ConsoleLoggingOptions-LogLevel 'RecordPoint.Connectors.SDK.Observability.Console.ConsoleLoggingOptions.LogLevel')
  - [WriteDimensions](#P-RecordPoint-Connectors-SDK-Observability-Console-ConsoleLoggingOptions-WriteDimensions 'RecordPoint.Connectors.SDK.Observability.Console.ConsoleLoggingOptions.WriteDimensions')
  - [WriteMeasures](#P-RecordPoint-Connectors-SDK-Observability-Console-ConsoleLoggingOptions-WriteMeasures 'RecordPoint.Connectors.SDK.Observability.Console.ConsoleLoggingOptions.WriteMeasures')
- [ContentItem](#T-RecordPoint-Connectors-SDK-Content-ContentItem 'RecordPoint.Connectors.SDK.Content.ContentItem')
  - [Author](#P-RecordPoint-Connectors-SDK-Content-ContentItem-Author 'RecordPoint.Connectors.SDK.Content.ContentItem.Author')
  - [ContentVersion](#P-RecordPoint-Connectors-SDK-Content-ContentItem-ContentVersion 'RecordPoint.Connectors.SDK.Content.ContentItem.ContentVersion')
  - [ExternalId](#P-RecordPoint-Connectors-SDK-Content-ContentItem-ExternalId 'RecordPoint.Connectors.SDK.Content.ContentItem.ExternalId')
  - [MetaDataItems](#P-RecordPoint-Connectors-SDK-Content-ContentItem-MetaDataItems 'RecordPoint.Connectors.SDK.Content.ContentItem.MetaDataItems')
  - [SourceCreatedBy](#P-RecordPoint-Connectors-SDK-Content-ContentItem-SourceCreatedBy 'RecordPoint.Connectors.SDK.Content.ContentItem.SourceCreatedBy')
  - [SourceCreatedDate](#P-RecordPoint-Connectors-SDK-Content-ContentItem-SourceCreatedDate 'RecordPoint.Connectors.SDK.Content.ContentItem.SourceCreatedDate')
  - [SourceLastModifiedBy](#P-RecordPoint-Connectors-SDK-Content-ContentItem-SourceLastModifiedBy 'RecordPoint.Connectors.SDK.Content.ContentItem.SourceLastModifiedBy')
  - [SourceLastModifiedDate](#P-RecordPoint-Connectors-SDK-Content-ContentItem-SourceLastModifiedDate 'RecordPoint.Connectors.SDK.Content.ContentItem.SourceLastModifiedDate')
  - [Title](#P-RecordPoint-Connectors-SDK-Content-ContentItem-Title 'RecordPoint.Connectors.SDK.Content.ContentItem.Title')
  - [Equals(other)](#M-RecordPoint-Connectors-SDK-Content-ContentItem-Equals-RecordPoint-Connectors-SDK-Content-ContentItem- 'RecordPoint.Connectors.SDK.Content.ContentItem.Equals(RecordPoint.Connectors.SDK.Content.ContentItem)')
  - [Equals(obj)](#M-RecordPoint-Connectors-SDK-Content-ContentItem-Equals-System-Object- 'RecordPoint.Connectors.SDK.Content.ContentItem.Equals(System.Object)')
  - [Equals(x,y)](#M-RecordPoint-Connectors-SDK-Content-ContentItem-Equals-RecordPoint-Connectors-SDK-Content-ContentItem,RecordPoint-Connectors-SDK-Content-ContentItem- 'RecordPoint.Connectors.SDK.Content.ContentItem.Equals(RecordPoint.Connectors.SDK.Content.ContentItem,RecordPoint.Connectors.SDK.Content.ContentItem)')
  - [GetHashCode()](#M-RecordPoint-Connectors-SDK-Content-ContentItem-GetHashCode 'RecordPoint.Connectors.SDK.Content.ContentItem.GetHashCode')
  - [GetHashCode(obj)](#M-RecordPoint-Connectors-SDK-Content-ContentItem-GetHashCode-RecordPoint-Connectors-SDK-Content-ContentItem- 'RecordPoint.Connectors.SDK.Content.ContentItem.GetHashCode(RecordPoint.Connectors.SDK.Content.ContentItem)')
- [ContentManagerConfiguration](#T-RecordPoint-Connectors-SDK-ContentManager-ContentManagerConfiguration 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerConfiguration')
  - [ConfigurationType](#P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerConfiguration-ConfigurationType 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerConfiguration.ConfigurationType')
  - [Deserialize(configurationType,configurationText)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerConfiguration-Deserialize-System-String,System-String- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerConfiguration.Deserialize(System.String,System.String)')
  - [Serialize()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerConfiguration-Serialize 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerConfiguration.Serialize')
- [ContentManagerObservabilityExtensions](#T-RecordPoint-Connectors-SDK-ContentManager-ContentManagerObservabilityExtensions 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerObservabilityExtensions')
  - [CHANNEL_COUNT](#F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerObservabilityExtensions-CHANNEL_COUNT 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerObservabilityExtensions.CHANNEL_COUNT')
  - [CHANNEL_DISCOVERY_OPERATIONS_STARTED_COUNT](#F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerObservabilityExtensions-CHANNEL_DISCOVERY_OPERATIONS_STARTED_COUNT 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerObservabilityExtensions.CHANNEL_DISCOVERY_OPERATIONS_STARTED_COUNT')
  - [CONNECTOR_COUNT](#F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerObservabilityExtensions-CONNECTOR_COUNT 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerObservabilityExtensions.CONNECTOR_COUNT')
  - [CONTENT_REGISTRATION_OPERATIONS_STARTED_COUNT](#F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerObservabilityExtensions-CONTENT_REGISTRATION_OPERATIONS_STARTED_COUNT 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerObservabilityExtensions.CONTENT_REGISTRATION_OPERATIONS_STARTED_COUNT')
  - [CONTENT_SYNCHRONISATION_OPERATIONS_STARTED_COUNT](#F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerObservabilityExtensions-CONTENT_SYNCHRONISATION_OPERATIONS_STARTED_COUNT 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerObservabilityExtensions.CONTENT_SYNCHRONISATION_OPERATIONS_STARTED_COUNT')
  - [SERVICE_NAME](#F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerObservabilityExtensions-SERVICE_NAME 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerObservabilityExtensions.SERVICE_NAME')
- [ContentManagerOperationOptionsBase](#T-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperationOptionsBase 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperationOptionsBase')
  - [DelaySeconds](#P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperationOptionsBase-DelaySeconds 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperationOptionsBase.DelaySeconds')
  - [ExponentialBackOff](#P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperationOptionsBase-ExponentialBackOff 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperationOptionsBase.ExponentialBackOff')
  - [ImmediateReExecution](#P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperationOptionsBase-ImmediateReExecution 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperationOptionsBase.ImmediateReExecution')
  - [MaxDelaySeconds](#P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperationOptionsBase-MaxDelaySeconds 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperationOptionsBase.MaxDelaySeconds')
  - [RandomDelay](#P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperationOptionsBase-RandomDelay 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOperationOptionsBase.RandomDelay')
- [ContentManagerOptions](#T-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOptions')
  - [SECTION_NAME](#F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions-SECTION_NAME 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOptions.SECTION_NAME')
  - [CleanUpAggregations](#P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions-CleanUpAggregations 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOptions.CleanUpAggregations')
  - [CleanUpChannels](#P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions-CleanUpChannels 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOptions.CleanUpChannels')
  - [DelaySeconds](#P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions-DelaySeconds 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOptions.DelaySeconds')
  - [MaxAbandonedChannelSynchronisationAge](#P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions-MaxAbandonedChannelSynchronisationAge 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOptions.MaxAbandonedChannelSynchronisationAge')
  - [MaxAbandonedWorkAge](#P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions-MaxAbandonedWorkAge 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOptions.MaxAbandonedWorkAge')
  - [MaxCompletedWorkAge](#P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions-MaxCompletedWorkAge 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOptions.MaxCompletedWorkAge')
  - [MaxDisabledConnectorAge](#P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions-MaxDisabledConnectorAge 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOptions.MaxDisabledConnectorAge')
  - [RemoveAbandonedWork](#P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions-RemoveAbandonedWork 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOptions.RemoveAbandonedWork')
  - [RemoveCompletedWork](#P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions-RemoveCompletedWork 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerOptions.RemoveCompletedWork')
- [ContentManagerState](#T-RecordPoint-Connectors-SDK-ContentManager-ContentManagerState 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerState')
  - [ChannelDiscoveryOperationsStarted](#P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerState-ChannelDiscoveryOperationsStarted 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerState.ChannelDiscoveryOperationsStarted')
  - [ContentSynchronisationOperationsStarted](#P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerState-ContentSynchronisationOperationsStarted 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerState.ContentSynchronisationOperationsStarted')
  - [LatestStateType](#P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerState-LatestStateType 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerState.LatestStateType')
  - [Deserialize(stateType,stateText)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerState-Deserialize-System-String,System-String- 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerState.Deserialize(System.String,System.String)')
  - [Serialize()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerState-Serialize 'RecordPoint.Connectors.SDK.ContentManager.ContentManagerState.Serialize')
- [ContentRegistrationConfiguration](#T-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationConfiguration 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationConfiguration')
  - [ChannelExternalId](#P-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationConfiguration-ChannelExternalId 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationConfiguration.ChannelExternalId')
  - [ChannelTitle](#P-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationConfiguration-ChannelTitle 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationConfiguration.ChannelTitle')
  - [ConfigurationType](#P-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationConfiguration-ConfigurationType 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationConfiguration.ConfigurationType')
  - [Context](#P-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationConfiguration-Context 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationConfiguration.Context')
  - [Deserialize(configurationType,configurationText)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationConfiguration-Deserialize-System-String,System-String- 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationConfiguration.Deserialize(System.String,System.String)')
  - [Serialize()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationConfiguration-Serialize 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationConfiguration.Serialize')
- [ContentRegistrationOperationOptions](#T-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperationOptions 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperationOptions')
  - [SECTION_NAME](#F-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperationOptions-SECTION_NAME 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperationOptions.SECTION_NAME')
  - [MaxFetchBatchSize](#P-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperationOptions-MaxFetchBatchSize 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationOperationOptions.MaxFetchBatchSize')
- [ContentRegistrationRequest](#T-RecordPoint-Connectors-SDK-Abstractions-Content-ContentRegistrationRequest 'RecordPoint.Connectors.SDK.Abstractions.Content.ContentRegistrationRequest')
  - [Context](#P-RecordPoint-Connectors-SDK-Abstractions-Content-ContentRegistrationRequest-Context 'RecordPoint.Connectors.SDK.Abstractions.Content.ContentRegistrationRequest.Context')
  - [EndDate](#P-RecordPoint-Connectors-SDK-Abstractions-Content-ContentRegistrationRequest-EndDate 'RecordPoint.Connectors.SDK.Abstractions.Content.ContentRegistrationRequest.EndDate')
  - [StartDate](#P-RecordPoint-Connectors-SDK-Abstractions-Content-ContentRegistrationRequest-StartDate 'RecordPoint.Connectors.SDK.Abstractions.Content.ContentRegistrationRequest.StartDate')
- [ContentRegistrationState](#T-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationState 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationState')
  - [Cursor](#P-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationState-Cursor 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationState.Cursor')
  - [LastBackOffDelaySeconds](#P-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationState-LastBackOffDelaySeconds 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationState.LastBackOffDelaySeconds')
  - [LatestStateType](#P-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationState-LatestStateType 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationState.LatestStateType')
  - [Deserialize(stateType,stateText)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationState-Deserialize-System-String,System-String- 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationState.Deserialize(System.String,System.String)')
  - [Serialize()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationState-Serialize 'RecordPoint.Connectors.SDK.ContentManager.ContentRegistrationState.Serialize')
- [ContentResult](#T-RecordPoint-Connectors-SDK-ContentManager-ContentResult 'RecordPoint.Connectors.SDK.ContentManager.ContentResult')
  - [Aggregations](#P-RecordPoint-Connectors-SDK-ContentManager-ContentResult-Aggregations 'RecordPoint.Connectors.SDK.ContentManager.ContentResult.Aggregations')
  - [AuditEvents](#P-RecordPoint-Connectors-SDK-ContentManager-ContentResult-AuditEvents 'RecordPoint.Connectors.SDK.ContentManager.ContentResult.AuditEvents')
  - [Cursor](#P-RecordPoint-Connectors-SDK-ContentManager-ContentResult-Cursor 'RecordPoint.Connectors.SDK.ContentManager.ContentResult.Cursor')
  - [Exception](#P-RecordPoint-Connectors-SDK-ContentManager-ContentResult-Exception 'RecordPoint.Connectors.SDK.ContentManager.ContentResult.Exception')
  - [Reason](#P-RecordPoint-Connectors-SDK-ContentManager-ContentResult-Reason 'RecordPoint.Connectors.SDK.ContentManager.ContentResult.Reason')
  - [Records](#P-RecordPoint-Connectors-SDK-ContentManager-ContentResult-Records 'RecordPoint.Connectors.SDK.ContentManager.ContentResult.Records')
  - [ResultType](#P-RecordPoint-Connectors-SDK-ContentManager-ContentResult-ResultType 'RecordPoint.Connectors.SDK.ContentManager.ContentResult.ResultType')
- [ContentResultType](#T-RecordPoint-Connectors-SDK-ContentManager-ContentResultType 'RecordPoint.Connectors.SDK.ContentManager.ContentResultType')
  - [Abandonded](#F-RecordPoint-Connectors-SDK-ContentManager-ContentResultType-Abandonded 'RecordPoint.Connectors.SDK.ContentManager.ContentResultType.Abandonded')
  - [BackOff](#F-RecordPoint-Connectors-SDK-ContentManager-ContentResultType-BackOff 'RecordPoint.Connectors.SDK.ContentManager.ContentResultType.BackOff')
  - [Complete](#F-RecordPoint-Connectors-SDK-ContentManager-ContentResultType-Complete 'RecordPoint.Connectors.SDK.ContentManager.ContentResultType.Complete')
  - [Failed](#F-RecordPoint-Connectors-SDK-ContentManager-ContentResultType-Failed 'RecordPoint.Connectors.SDK.ContentManager.ContentResultType.Failed')
  - [Incomplete](#F-RecordPoint-Connectors-SDK-ContentManager-ContentResultType-Incomplete 'RecordPoint.Connectors.SDK.ContentManager.ContentResultType.Incomplete')
- [ContentSubmissionConfiguration](#T-RecordPoint-Connectors-SDK-ContentManager-ContentSubmissionConfiguration 'RecordPoint.Connectors.SDK.ContentManager.ContentSubmissionConfiguration')
  - [ConnectorConfigurationId](#P-RecordPoint-Connectors-SDK-ContentManager-ContentSubmissionConfiguration-ConnectorConfigurationId 'RecordPoint.Connectors.SDK.ContentManager.ContentSubmissionConfiguration.ConnectorConfigurationId')
  - [TenantDomainName](#P-RecordPoint-Connectors-SDK-ContentManager-ContentSubmissionConfiguration-TenantDomainName 'RecordPoint.Connectors.SDK.ContentManager.ContentSubmissionConfiguration.TenantDomainName')
  - [TenantId](#P-RecordPoint-Connectors-SDK-ContentManager-ContentSubmissionConfiguration-TenantId 'RecordPoint.Connectors.SDK.ContentManager.ContentSubmissionConfiguration.TenantId')
- [ContentSynchronisationConfiguration](#T-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationConfiguration 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationConfiguration')
  - [ChannelExternalId](#P-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationConfiguration-ChannelExternalId 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationConfiguration.ChannelExternalId')
  - [ChannelTitle](#P-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationConfiguration-ChannelTitle 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationConfiguration.ChannelTitle')
  - [ConfigurationType](#P-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationConfiguration-ConfigurationType 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationConfiguration.ConfigurationType')
  - [Deserialize(configurationType,configurationText)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationConfiguration-Deserialize-System-String,System-String- 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationConfiguration.Deserialize(System.String,System.String)')
  - [Serialize()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationConfiguration-Serialize 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationConfiguration.Serialize')
- [ContentSynchronisationOperationOptions](#T-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperationOptions 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperationOptions')
  - [SECTION_NAME](#F-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperationOptions-SECTION_NAME 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperationOptions.SECTION_NAME')
  - [MaxFetchBatchSize](#P-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperationOptions-MaxFetchBatchSize 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationOperationOptions.MaxFetchBatchSize')
- [ContentSynchronisationState](#T-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationState 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationState')
  - [Cursor](#P-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationState-Cursor 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationState.Cursor')
  - [LastBackOffDelaySeconds](#P-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationState-LastBackOffDelaySeconds 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationState.LastBackOffDelaySeconds')
  - [LatestStateType](#P-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationState-LatestStateType 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationState.LatestStateType')
  - [Deserialize(stateType,stateText)](#M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationState-Deserialize-System-String,System-String- 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationState.Deserialize(System.String,System.String)')
  - [Serialize()](#M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationState-Serialize 'RecordPoint.Connectors.SDK.ContentManager.ContentSynchronisationState.Serialize')
- [DeadLetterModel](#T-RecordPoint-Connectors-SDK-Work-Models-DeadLetterModel 'RecordPoint.Connectors.SDK.Work.Models.DeadLetterModel')
  - [DeadLetterReason](#P-RecordPoint-Connectors-SDK-Work-Models-DeadLetterModel-DeadLetterReason 'RecordPoint.Connectors.SDK.Work.Models.DeadLetterModel.DeadLetterReason')
  - [EnqueuedTime](#P-RecordPoint-Connectors-SDK-Work-Models-DeadLetterModel-EnqueuedTime 'RecordPoint.Connectors.SDK.Work.Models.DeadLetterModel.EnqueuedTime')
  - [MessageId](#P-RecordPoint-Connectors-SDK-Work-Models-DeadLetterModel-MessageId 'RecordPoint.Connectors.SDK.Work.Models.DeadLetterModel.MessageId')
  - [SequenceNumber](#P-RecordPoint-Connectors-SDK-Work-Models-DeadLetterModel-SequenceNumber 'RecordPoint.Connectors.SDK.Work.Models.DeadLetterModel.SequenceNumber')
- [DependancyType](#T-RecordPoint-Connectors-SDK-Observability-DependancyType 'RecordPoint.Connectors.SDK.Observability.DependancyType')
  - [Database](#F-RecordPoint-Connectors-SDK-Observability-DependancyType-Database 'RecordPoint.Connectors.SDK.Observability.DependancyType.Database')
  - [Records365](#F-RecordPoint-Connectors-SDK-Observability-DependancyType-Records365 'RecordPoint.Connectors.SDK.Observability.DependancyType.Records365')
- [Dimensions](#T-RecordPoint-Connectors-SDK-Observability-Dimensions 'RecordPoint.Connectors.SDK.Observability.Dimensions')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Observability-Dimensions-#ctor 'RecordPoint.Connectors.SDK.Observability.Dimensions.#ctor')
  - [#ctor(collection)](#M-RecordPoint-Connectors-SDK-Observability-Dimensions-#ctor-System-Collections-Generic-IEnumerable{System-Collections-Generic-KeyValuePair{System-String,System-String}}- 'RecordPoint.Connectors.SDK.Observability.Dimensions.#ctor(System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.String,System.String}})')
  - [ToLogState()](#M-RecordPoint-Connectors-SDK-Observability-Dimensions-ToLogState 'RecordPoint.Connectors.SDK.Observability.Dimensions.ToLogState')
- [EventType](#T-RecordPoint-Connectors-SDK-Observability-EventType 'RecordPoint.Connectors.SDK.Observability.EventType')
  - [Decision](#F-RecordPoint-Connectors-SDK-Observability-EventType-Decision 'RecordPoint.Connectors.SDK.Observability.EventType.Decision')
  - [Finish](#F-RecordPoint-Connectors-SDK-Observability-EventType-Finish 'RecordPoint.Connectors.SDK.Observability.EventType.Finish')
  - [Shutdown](#F-RecordPoint-Connectors-SDK-Observability-EventType-Shutdown 'RecordPoint.Connectors.SDK.Observability.EventType.Shutdown')
  - [Start](#F-RecordPoint-Connectors-SDK-Observability-EventType-Start 'RecordPoint.Connectors.SDK.Observability.EventType.Start')
  - [Startup](#F-RecordPoint-Connectors-SDK-Observability-EventType-Startup 'RecordPoint.Connectors.SDK.Observability.EventType.Startup')
- [FileLogOptions](#T-RecordPoint-Connectors-SDK-Observability-FileLogs-FileLogOptions 'RecordPoint.Connectors.SDK.Observability.FileLogs.FileLogOptions')
  - [OPTION_NAME](#F-RecordPoint-Connectors-SDK-Observability-FileLogs-FileLogOptions-OPTION_NAME 'RecordPoint.Connectors.SDK.Observability.FileLogs.FileLogOptions.OPTION_NAME')
  - [FileSizeLimitBytes](#P-RecordPoint-Connectors-SDK-Observability-FileLogs-FileLogOptions-FileSizeLimitBytes 'RecordPoint.Connectors.SDK.Observability.FileLogs.FileLogOptions.FileSizeLimitBytes')
  - [LogPath](#P-RecordPoint-Connectors-SDK-Observability-FileLogs-FileLogOptions-LogPath 'RecordPoint.Connectors.SDK.Observability.FileLogs.FileLogOptions.LogPath')
  - [OutputTemplate](#P-RecordPoint-Connectors-SDK-Observability-FileLogs-FileLogOptions-OutputTemplate 'RecordPoint.Connectors.SDK.Observability.FileLogs.FileLogOptions.OutputTemplate')
  - [RetainedFileCountLimit](#P-RecordPoint-Connectors-SDK-Observability-FileLogs-FileLogOptions-RetainedFileCountLimit 'RecordPoint.Connectors.SDK.Observability.FileLogs.FileLogOptions.RetainedFileCountLimit')
  - [RollOnFileSizeLimit](#P-RecordPoint-Connectors-SDK-Observability-FileLogs-FileLogOptions-RollOnFileSizeLimit 'RecordPoint.Connectors.SDK.Observability.FileLogs.FileLogOptions.RollOnFileSizeLimit')
  - [RollingInterval](#P-RecordPoint-Connectors-SDK-Observability-FileLogs-FileLogOptions-RollingInterval 'RecordPoint.Connectors.SDK.Observability.FileLogs.FileLogOptions.RollingInterval')
- [HealthCheckDimension](#T-RecordPoint-Connectors-SDK-Health-HealthCheckDimension 'RecordPoint.Connectors.SDK.Health.HealthCheckDimension')
  - [ConnectorId](#P-RecordPoint-Connectors-SDK-Health-HealthCheckDimension-ConnectorId 'RecordPoint.Connectors.SDK.Health.HealthCheckDimension.ConnectorId')
  - [Value](#P-RecordPoint-Connectors-SDK-Health-HealthCheckDimension-Value 'RecordPoint.Connectors.SDK.Health.HealthCheckDimension.Value')
- [HealthCheckFailedException](#T-RecordPoint-Connectors-SDK-Health-HealthCheckFailedException 'RecordPoint.Connectors.SDK.Health.HealthCheckFailedException')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Health-HealthCheckFailedException-#ctor 'RecordPoint.Connectors.SDK.Health.HealthCheckFailedException.#ctor')
  - [#ctor(message)](#M-RecordPoint-Connectors-SDK-Health-HealthCheckFailedException-#ctor-System-String- 'RecordPoint.Connectors.SDK.Health.HealthCheckFailedException.#ctor(System.String)')
  - [#ctor(message,inner)](#M-RecordPoint-Connectors-SDK-Health-HealthCheckFailedException-#ctor-System-String,System-Exception- 'RecordPoint.Connectors.SDK.Health.HealthCheckFailedException.#ctor(System.String,System.Exception)')
- [HealthCheckItem](#T-RecordPoint-Connectors-SDK-Health-HealthCheckItem 'RecordPoint.Connectors.SDK.Health.HealthCheckItem')
  - [HealthCheckType](#P-RecordPoint-Connectors-SDK-Health-HealthCheckItem-HealthCheckType 'RecordPoint.Connectors.SDK.Health.HealthCheckItem.HealthCheckType')
  - [HealthLevel](#P-RecordPoint-Connectors-SDK-Health-HealthCheckItem-HealthLevel 'RecordPoint.Connectors.SDK.Health.HealthCheckItem.HealthLevel')
  - [Name](#P-RecordPoint-Connectors-SDK-Health-HealthCheckItem-Name 'RecordPoint.Connectors.SDK.Health.HealthCheckItem.Name')
- [HealthCheckMeasure](#T-RecordPoint-Connectors-SDK-Health-HealthCheckMeasure 'RecordPoint.Connectors.SDK.Health.HealthCheckMeasure')
  - [Value](#P-RecordPoint-Connectors-SDK-Health-HealthCheckMeasure-Value 'RecordPoint.Connectors.SDK.Health.HealthCheckMeasure.Value')
- [HealthCheckOptions](#T-RecordPoint-Connectors-SDK-Health-HealthCheckOptions 'RecordPoint.Connectors.SDK.Health.HealthCheckOptions')
  - [SECTION_NAME](#F-RecordPoint-Connectors-SDK-Health-HealthCheckOptions-SECTION_NAME 'RecordPoint.Connectors.SDK.Health.HealthCheckOptions.SECTION_NAME')
  - [HealthCheckFrequencySeconds](#P-RecordPoint-Connectors-SDK-Health-HealthCheckOptions-HealthCheckFrequencySeconds 'RecordPoint.Connectors.SDK.Health.HealthCheckOptions.HealthCheckFrequencySeconds')
  - [HealthCheckStartDelaySeconds](#P-RecordPoint-Connectors-SDK-Health-HealthCheckOptions-HealthCheckStartDelaySeconds 'RecordPoint.Connectors.SDK.Health.HealthCheckOptions.HealthCheckStartDelaySeconds')
  - [MinimumDiskSpaceInGB](#P-RecordPoint-Connectors-SDK-Health-HealthCheckOptions-MinimumDiskSpaceInGB 'RecordPoint.Connectors.SDK.Health.HealthCheckOptions.MinimumDiskSpaceInGB')
- [HealthCheckResult](#T-RecordPoint-Connectors-SDK-Health-HealthCheckResult 'RecordPoint.Connectors.SDK.Health.HealthCheckResult')
  - [Dimensions](#P-RecordPoint-Connectors-SDK-Health-HealthCheckResult-Dimensions 'RecordPoint.Connectors.SDK.Health.HealthCheckResult.Dimensions')
  - [LastUpdate](#P-RecordPoint-Connectors-SDK-Health-HealthCheckResult-LastUpdate 'RecordPoint.Connectors.SDK.Health.HealthCheckResult.LastUpdate')
  - [Measures](#P-RecordPoint-Connectors-SDK-Health-HealthCheckResult-Measures 'RecordPoint.Connectors.SDK.Health.HealthCheckResult.Measures')
  - [OverallHealthLevel](#P-RecordPoint-Connectors-SDK-Health-HealthCheckResult-OverallHealthLevel 'RecordPoint.Connectors.SDK.Health.HealthCheckResult.OverallHealthLevel')
- [HealthLevel](#T-RecordPoint-Connectors-SDK-Health-HealthLevel 'RecordPoint.Connectors.SDK.Health.HealthLevel')
  - [Failure](#F-RecordPoint-Connectors-SDK-Health-HealthLevel-Failure 'RecordPoint.Connectors.SDK.Health.HealthLevel.Failure')
  - [Normal](#F-RecordPoint-Connectors-SDK-Health-HealthLevel-Normal 'RecordPoint.Connectors.SDK.Health.HealthLevel.Normal')
  - [Warning](#F-RecordPoint-Connectors-SDK-Health-HealthLevel-Warning 'RecordPoint.Connectors.SDK.Health.HealthLevel.Warning')
- [IAggregationManager](#T-RecordPoint-Connectors-SDK-Content-IAggregationManager 'RecordPoint.Connectors.SDK.Content.IAggregationManager')
  - [AggregationExistsAsync(connectorId,externalId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Content-IAggregationManager-AggregationExistsAsync-System-String,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.IAggregationManager.AggregationExistsAsync(System.String,System.String,System.Threading.CancellationToken)')
  - [GetAggregationAsync(connectorId,externalId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Content-IAggregationManager-GetAggregationAsync-System-String,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.IAggregationManager.GetAggregationAsync(System.String,System.String,System.Threading.CancellationToken)')
  - [GetAggregationsAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Content-IAggregationManager-GetAggregationsAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.IAggregationManager.GetAggregationsAsync(System.Threading.CancellationToken)')
  - [GetAggregationsAsync(predicate,cancellationToken)](#M-RecordPoint-Connectors-SDK-Content-IAggregationManager-GetAggregationsAsync-System-Linq-Expressions-Expression{System-Func{RecordPoint-Connectors-SDK-Content-AggregationModel,System-Boolean}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.IAggregationManager.GetAggregationsAsync(System.Linq.Expressions.Expression{System.Func{RecordPoint.Connectors.SDK.Content.AggregationModel,System.Boolean}},System.Threading.CancellationToken)')
  - [GetAggregationsAsync(connectorId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Content-IAggregationManager-GetAggregationsAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.IAggregationManager.GetAggregationsAsync(System.String,System.Threading.CancellationToken)')
  - [RemoveAggregationAsync(connectorId,externalId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Content-IAggregationManager-RemoveAggregationAsync-System-String,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.IAggregationManager.RemoveAggregationAsync(System.String,System.String,System.Threading.CancellationToken)')
  - [RemoveAggregationsAsync(connectorId,externalIds,cancellationToken)](#M-RecordPoint-Connectors-SDK-Content-IAggregationManager-RemoveAggregationsAsync-System-String,System-String[],System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.IAggregationManager.RemoveAggregationsAsync(System.String,System.String[],System.Threading.CancellationToken)')
  - [RemoveAggregationsAsync(aggregationModels,cancellationToken)](#M-RecordPoint-Connectors-SDK-Content-IAggregationManager-RemoveAggregationsAsync-System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Content-AggregationModel},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.IAggregationManager.RemoveAggregationsAsync(System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Content.AggregationModel},System.Threading.CancellationToken)')
  - [UpsertAggregationAsync(aggregation,cancellationToken)](#M-RecordPoint-Connectors-SDK-Content-IAggregationManager-UpsertAggregationAsync-RecordPoint-Connectors-SDK-Content-AggregationModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.IAggregationManager.UpsertAggregationAsync(RecordPoint.Connectors.SDK.Content.AggregationModel,System.Threading.CancellationToken)')
  - [UpsertAggregationsAsync(aggregations,cancellationToken)](#M-RecordPoint-Connectors-SDK-Content-IAggregationManager-UpsertAggregationsAsync-System-Collections-Generic-List{RecordPoint-Connectors-SDK-Content-AggregationModel},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.IAggregationManager.UpsertAggregationsAsync(System.Collections.Generic.List{RecordPoint.Connectors.SDK.Content.AggregationModel},System.Threading.CancellationToken)')
- [IAggregationSubmissionCallbackAction](#T-RecordPoint-Connectors-SDK-ContentManager-IAggregationSubmissionCallbackAction 'RecordPoint.Connectors.SDK.ContentManager.IAggregationSubmissionCallbackAction')
  - [ExecuteAsync(connectorConfiguration,aggregation,submissionActionType,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-IAggregationSubmissionCallbackAction-ExecuteAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Aggregation,RecordPoint-Connectors-SDK-Abstractions-ContentManager-SubmissionActionType,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.IAggregationSubmissionCallbackAction.ExecuteAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,RecordPoint.Connectors.SDK.Content.Aggregation,RecordPoint.Connectors.SDK.Abstractions.ContentManager.SubmissionActionType,System.Threading.CancellationToken)')
- [IAuditEventSubmissionCallbackAction](#T-RecordPoint-Connectors-SDK-ContentManager-IAuditEventSubmissionCallbackAction 'RecordPoint.Connectors.SDK.ContentManager.IAuditEventSubmissionCallbackAction')
  - [ExecuteAsync(connectorConfiguration,auditEvent,submissionActionType,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-IAuditEventSubmissionCallbackAction-ExecuteAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-AuditEvent,RecordPoint-Connectors-SDK-Abstractions-ContentManager-SubmissionActionType,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.IAuditEventSubmissionCallbackAction.ExecuteAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,RecordPoint.Connectors.SDK.Content.AuditEvent,RecordPoint.Connectors.SDK.Abstractions.ContentManager.SubmissionActionType,System.Threading.CancellationToken)')
- [IBinaryRetrievalAction](#T-RecordPoint-Connectors-SDK-ContentManager-IBinaryRetrievalAction 'RecordPoint.Connectors.SDK.ContentManager.IBinaryRetrievalAction')
  - [ExecuteAsync(connectorConfiguration,binaryMetaInfo,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-IBinaryRetrievalAction-ExecuteAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-BinaryMetaInfo,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.IBinaryRetrievalAction.ExecuteAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,RecordPoint.Connectors.SDK.Content.BinaryMetaInfo,System.Threading.CancellationToken)')
- [IBinarySubmissionCallbackAction](#T-RecordPoint-Connectors-SDK-ContentManager-IBinarySubmissionCallbackAction 'RecordPoint.Connectors.SDK.ContentManager.IBinarySubmissionCallbackAction')
  - [ExecuteAsync(connectorConfiguration,binaryMetaInfo,submissionActionType,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-IBinarySubmissionCallbackAction-ExecuteAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-BinaryMetaInfo,RecordPoint-Connectors-SDK-Abstractions-ContentManager-SubmissionActionType,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.IBinarySubmissionCallbackAction.ExecuteAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,RecordPoint.Connectors.SDK.Content.BinaryMetaInfo,RecordPoint.Connectors.SDK.Abstractions.ContentManager.SubmissionActionType,System.Threading.CancellationToken)')
- [IChannelDiscoveryAction](#T-RecordPoint-Connectors-SDK-ContentManager-IChannelDiscoveryAction 'RecordPoint.Connectors.SDK.ContentManager.IChannelDiscoveryAction')
  - [ExecuteAsync(connectorConfiguration,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-IChannelDiscoveryAction-ExecuteAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.IChannelDiscoveryAction.ExecuteAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,System.Threading.CancellationToken)')
- [IChannelManager](#T-RecordPoint-Connectors-SDK-Content-IChannelManager 'RecordPoint.Connectors.SDK.Content.IChannelManager')
  - [ChannelExistsAsync(connectorId,externalId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Content-IChannelManager-ChannelExistsAsync-System-String,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.IChannelManager.ChannelExistsAsync(System.String,System.String,System.Threading.CancellationToken)')
  - [GetChannelAsync(connectorId,externalId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Content-IChannelManager-GetChannelAsync-System-String,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.IChannelManager.GetChannelAsync(System.String,System.String,System.Threading.CancellationToken)')
  - [GetChannelsAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Content-IChannelManager-GetChannelsAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.IChannelManager.GetChannelsAsync(System.Threading.CancellationToken)')
  - [GetChannelsAsync(predicate,cancellationToken)](#M-RecordPoint-Connectors-SDK-Content-IChannelManager-GetChannelsAsync-System-Linq-Expressions-Expression{System-Func{RecordPoint-Connectors-SDK-Content-ChannelModel,System-Boolean}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.IChannelManager.GetChannelsAsync(System.Linq.Expressions.Expression{System.Func{RecordPoint.Connectors.SDK.Content.ChannelModel,System.Boolean}},System.Threading.CancellationToken)')
  - [GetChannelsAsync(connectorId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Content-IChannelManager-GetChannelsAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.IChannelManager.GetChannelsAsync(System.String,System.Threading.CancellationToken)')
  - [RemoveChannelAsync(connectorId,externalId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Content-IChannelManager-RemoveChannelAsync-System-String,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.IChannelManager.RemoveChannelAsync(System.String,System.String,System.Threading.CancellationToken)')
  - [RemoveChannelsAsync(connectorId,externalIds,cancellationToken)](#M-RecordPoint-Connectors-SDK-Content-IChannelManager-RemoveChannelsAsync-System-String,System-String[],System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.IChannelManager.RemoveChannelsAsync(System.String,System.String[],System.Threading.CancellationToken)')
  - [RemoveChannelsAsync(channelModels,cancellationToken)](#M-RecordPoint-Connectors-SDK-Content-IChannelManager-RemoveChannelsAsync-System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Content-ChannelModel},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.IChannelManager.RemoveChannelsAsync(System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Content.ChannelModel},System.Threading.CancellationToken)')
  - [UpsertChannelAsync(channel,cancellationToken)](#M-RecordPoint-Connectors-SDK-Content-IChannelManager-UpsertChannelAsync-RecordPoint-Connectors-SDK-Content-ChannelModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.IChannelManager.UpsertChannelAsync(RecordPoint.Connectors.SDK.Content.ChannelModel,System.Threading.CancellationToken)')
  - [UpsertChannelsAsync(channels,cancellationToken)](#M-RecordPoint-Connectors-SDK-Content-IChannelManager-UpsertChannelsAsync-System-Collections-Generic-List{RecordPoint-Connectors-SDK-Content-ChannelModel},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Content.IChannelManager.UpsertChannelsAsync(System.Collections.Generic.List{RecordPoint.Connectors.SDK.Content.ChannelModel},System.Threading.CancellationToken)')
- [IConnectorConfigurationManager](#T-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager')
  - [ConnectorConfigurationExistsAsync(connectorId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager-ConnectorConfigurationExistsAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager.ConnectorConfigurationExistsAsync(System.String,System.Threading.CancellationToken)')
  - [DeleteConnectorConfigurationAsync(connectorId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager-DeleteConnectorConfigurationAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager.DeleteConnectorConfigurationAsync(System.String,System.Threading.CancellationToken)')
  - [GetAllConnectorConfigurationsAsync()](#M-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager-GetAllConnectorConfigurationsAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager.GetAllConnectorConfigurationsAsync(System.Threading.CancellationToken)')
  - [GetBinarySubmissionStatusAsync(connectorId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager-GetBinarySubmissionStatusAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager.GetBinarySubmissionStatusAsync(System.String,System.Threading.CancellationToken)')
  - [GetConnectorConfigurationAsync(connectorId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager-GetConnectorConfigurationAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager.GetConnectorConfigurationAsync(System.String,System.Threading.CancellationToken)')
  - [GetConnectorStatusAsync(connectorId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager-GetConnectorStatusAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager.GetConnectorStatusAsync(System.String,System.Threading.CancellationToken)')
  - [GetSubmissionStatusAsync(connectorId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager-GetSubmissionStatusAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager.GetSubmissionStatusAsync(System.String,System.Threading.CancellationToken)')
  - [SetConnectorConfigurationAsync(connectorData,cancellationToken)](#M-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager-SetConnectorConfigurationAsync-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager.SetConnectorConfigurationAsync(RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel,System.Threading.CancellationToken)')
- [IConnectorSecretAction](#T-RecordPoint-Connectors-SDK-ContentManager-IConnectorSecretAction 'RecordPoint.Connectors.SDK.ContentManager.IConnectorSecretAction')
  - [SaveSecretsAsync(connectorConfiguration,secrets)](#M-RecordPoint-Connectors-SDK-ContentManager-IConnectorSecretAction-SaveSecretsAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Abstractions-Content-ConnectorSecret}- 'RecordPoint.Connectors.SDK.ContentManager.IConnectorSecretAction.SaveSecretsAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Abstractions.Content.ConnectorSecret})')
- [IContentDiscoveryState](#T-RecordPoint-Connectors-SDK-Abstractions-ContentManager-IContentDiscoveryState 'RecordPoint.Connectors.SDK.Abstractions.ContentManager.IContentDiscoveryState')
  - [LastBackOffDelaySeconds](#P-RecordPoint-Connectors-SDK-Abstractions-ContentManager-IContentDiscoveryState-LastBackOffDelaySeconds 'RecordPoint.Connectors.SDK.Abstractions.ContentManager.IContentDiscoveryState.LastBackOffDelaySeconds')
- [IContentManagerActionProvider](#T-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider 'RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider')
  - [CreateAggregationSubmissionCallbackAction()](#M-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider-CreateAggregationSubmissionCallbackAction 'RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider.CreateAggregationSubmissionCallbackAction')
  - [CreateAuditEventSubmissionCallbackAction()](#M-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider-CreateAuditEventSubmissionCallbackAction 'RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider.CreateAuditEventSubmissionCallbackAction')
  - [CreateBinaryRetrievalAction()](#M-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider-CreateBinaryRetrievalAction 'RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider.CreateBinaryRetrievalAction')
  - [CreateBinarySubmissionCallbackAction()](#M-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider-CreateBinarySubmissionCallbackAction 'RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider.CreateBinarySubmissionCallbackAction')
  - [CreateChannelDiscoveryAction()](#M-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider-CreateChannelDiscoveryAction 'RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider.CreateChannelDiscoveryAction')
  - [CreateContentManagerCallbackAction()](#M-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider-CreateContentManagerCallbackAction 'RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider.CreateContentManagerCallbackAction')
  - [CreateContentRegistrationAction()](#M-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider-CreateContentRegistrationAction 'RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider.CreateContentRegistrationAction')
  - [CreateContentSynchronisationAction()](#M-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider-CreateContentSynchronisationAction 'RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider.CreateContentSynchronisationAction')
  - [CreateGenerationReportAction()](#M-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider-CreateGenerationReportAction 'RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider.CreateGenerationReportAction')
  - [CreateGenericAction\`\`2()](#M-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider-CreateGenericAction``2 'RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider.CreateGenericAction``2')
  - [CreateGenericManagedAction\`\`2()](#M-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider-CreateGenericManagedAction``2 'RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider.CreateGenericManagedAction``2')
  - [CreateRecordDisposalAction()](#M-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider-CreateRecordDisposalAction 'RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider.CreateRecordDisposalAction')
  - [CreateRecordSubmissionCallbackAction()](#M-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider-CreateRecordSubmissionCallbackAction 'RecordPoint.Connectors.SDK.ContentManager.IContentManagerActionProvider.CreateRecordSubmissionCallbackAction')
- [IContentManagerCallbackAction](#T-RecordPoint-Connectors-SDK-ContentManager-IContentManagerCallbackAction 'RecordPoint.Connectors.SDK.ContentManager.IContentManagerCallbackAction')
  - [ExecuteAsync(connectorConfigurations,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-IContentManagerCallbackAction-ExecuteAsync-System-Collections-Generic-List{RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.IContentManagerCallbackAction.ExecuteAsync(System.Collections.Generic.List{RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel},System.Threading.CancellationToken)')
- [IContentRegistrationAction](#T-RecordPoint-Connectors-SDK-ContentManager-IContentRegistrationAction 'RecordPoint.Connectors.SDK.ContentManager.IContentRegistrationAction')
  - [EndDate](#F-RecordPoint-Connectors-SDK-ContentManager-IContentRegistrationAction-EndDate 'RecordPoint.Connectors.SDK.ContentManager.IContentRegistrationAction.EndDate')
  - [StartDate](#F-RecordPoint-Connectors-SDK-ContentManager-IContentRegistrationAction-StartDate 'RecordPoint.Connectors.SDK.ContentManager.IContentRegistrationAction.StartDate')
  - [BeginAsync(connectorConfiguration,channel,context,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-IContentRegistrationAction-BeginAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Channel,System-Collections-Generic-IDictionary{System-String,System-String},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.IContentRegistrationAction.BeginAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,RecordPoint.Connectors.SDK.Content.Channel,System.Collections.Generic.IDictionary{System.String,System.String},System.Threading.CancellationToken)')
  - [ContinueAsync(connectorConfiguration,channel,cursor,context,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-IContentRegistrationAction-ContinueAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Channel,System-String,System-Collections-Generic-IDictionary{System-String,System-String},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.IContentRegistrationAction.ContinueAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,RecordPoint.Connectors.SDK.Content.Channel,System.String,System.Collections.Generic.IDictionary{System.String,System.String},System.Threading.CancellationToken)')
  - [StopAsync(connectorConfiguration,channel,cursor,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-IContentRegistrationAction-StopAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Channel,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.IContentRegistrationAction.StopAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,RecordPoint.Connectors.SDK.Content.Channel,System.String,System.Threading.CancellationToken)')
- [IContentRegistrationRequestAction](#T-RecordPoint-Connectors-SDK-ContentManager-IContentRegistrationRequestAction 'RecordPoint.Connectors.SDK.ContentManager.IContentRegistrationRequestAction')
  - [GetChannelsFromRequestAsync(connectorConfiguration,contentRegistrationRequest,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-IContentRegistrationRequestAction-GetChannelsFromRequestAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Abstractions-Content-ContentRegistrationRequest,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.IContentRegistrationRequestAction.GetChannelsFromRequestAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,RecordPoint.Connectors.SDK.Abstractions.Content.ContentRegistrationRequest,System.Threading.CancellationToken)')
- [IContentSynchronisationAction](#T-RecordPoint-Connectors-SDK-ContentManager-IContentSynchronisationAction 'RecordPoint.Connectors.SDK.ContentManager.IContentSynchronisationAction')
  - [BeginAsync(connectorConfiguration,channel,startDate,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-IContentSynchronisationAction-BeginAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Channel,System-DateTimeOffset,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.IContentSynchronisationAction.BeginAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,RecordPoint.Connectors.SDK.Content.Channel,System.DateTimeOffset,System.Threading.CancellationToken)')
  - [ContinueAsync(connectorConfiguration,channel,cursor,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-IContentSynchronisationAction-ContinueAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Channel,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.IContentSynchronisationAction.ContinueAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,RecordPoint.Connectors.SDK.Content.Channel,System.String,System.Threading.CancellationToken)')
  - [StopAsync(connectorConfiguration,channel,cursor,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-IContentSynchronisationAction-StopAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Channel,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.IContentSynchronisationAction.StopAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,RecordPoint.Connectors.SDK.Content.Channel,System.String,System.Threading.CancellationToken)')
- [IDeadLetterQueueService](#T-RecordPoint-Connectors-SDK-Work-IDeadLetterQueueService 'RecordPoint.Connectors.SDK.Work.IDeadLetterQueueService')
  - [DeleteAllMessagesAsync(queueName)](#M-RecordPoint-Connectors-SDK-Work-IDeadLetterQueueService-DeleteAllMessagesAsync-System-String- 'RecordPoint.Connectors.SDK.Work.IDeadLetterQueueService.DeleteAllMessagesAsync(System.String)')
  - [DeleteMessageAsync(queueName,sequenceNumber)](#M-RecordPoint-Connectors-SDK-Work-IDeadLetterQueueService-DeleteMessageAsync-System-String,System-Int64- 'RecordPoint.Connectors.SDK.Work.IDeadLetterQueueService.DeleteMessageAsync(System.String,System.Int64)')
  - [GetAllMessagesAsync(queueName)](#M-RecordPoint-Connectors-SDK-Work-IDeadLetterQueueService-GetAllMessagesAsync-System-String- 'RecordPoint.Connectors.SDK.Work.IDeadLetterQueueService.GetAllMessagesAsync(System.String)')
  - [GetMessageAsync(queueName,sequenceNumber)](#M-RecordPoint-Connectors-SDK-Work-IDeadLetterQueueService-GetMessageAsync-System-String,System-Int64- 'RecordPoint.Connectors.SDK.Work.IDeadLetterQueueService.GetMessageAsync(System.String,System.Int64)')
  - [ResubmitMessagesAsync(queueName,sequenceNumbers)](#M-RecordPoint-Connectors-SDK-Work-IDeadLetterQueueService-ResubmitMessagesAsync-System-String,System-Int64[]- 'RecordPoint.Connectors.SDK.Work.IDeadLetterQueueService.ResubmitMessagesAsync(System.String,System.Int64[])')
- [IGenerateReportAction](#T-RecordPoint-Connectors-SDK-ContentManager-IGenerateReportAction 'RecordPoint.Connectors.SDK.ContentManager.IGenerateReportAction')
  - [BeginAsync(connectorConfiguration,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-IGenerateReportAction-BeginAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.IGenerateReportAction.BeginAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,System.Threading.CancellationToken)')
  - [ContinueAsync(connectorConfiguration,cursor,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-IGenerateReportAction-ContinueAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.IGenerateReportAction.ContinueAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,System.String,System.Threading.CancellationToken)')
- [IGenericAction\`2](#T-RecordPoint-Connectors-SDK-ContentManager-IGenericAction`2 'RecordPoint.Connectors.SDK.ContentManager.IGenericAction`2')
  - [ExecuteAsync(connectorConfiguration,item,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-IGenericAction`2-ExecuteAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,`0,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.IGenericAction`2.ExecuteAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,`0,System.Threading.CancellationToken)')
- [IGenericManagedAction\`2](#T-RecordPoint-Connectors-SDK-ContentManager-IGenericManagedAction`2 'RecordPoint.Connectors.SDK.ContentManager.IGenericManagedAction`2')
  - [BeginAsync(connectorConfiguration,item,context,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-IGenericManagedAction`2-BeginAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,`0,System-Collections-Generic-IDictionary{System-String,System-String},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.IGenericManagedAction`2.BeginAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,`0,System.Collections.Generic.IDictionary{System.String,System.String},System.Threading.CancellationToken)')
  - [ContinueAsync(connectorConfiguration,item,cursor,context,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-IGenericManagedAction`2-ContinueAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,`0,System-String,System-Collections-Generic-IDictionary{System-String,System-String},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.IGenericManagedAction`2.ContinueAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,`0,System.String,System.Collections.Generic.IDictionary{System.String,System.String},System.Threading.CancellationToken)')
- [IHealthCheckLiveAction](#T-RecordPoint-Connectors-SDK-Health-IHealthCheckLiveAction 'RecordPoint.Connectors.SDK.Health.IHealthCheckLiveAction')
  - [CheckIsLiveAsync()](#M-RecordPoint-Connectors-SDK-Health-IHealthCheckLiveAction-CheckIsLiveAsync 'RecordPoint.Connectors.SDK.Health.IHealthCheckLiveAction.CheckIsLiveAsync')
- [IHealthCheckManager](#T-RecordPoint-Connectors-SDK-Health-IHealthCheckManager 'RecordPoint.Connectors.SDK.Health.IHealthCheckManager')
  - [HealthCheckResult](#P-RecordPoint-Connectors-SDK-Health-IHealthCheckManager-HealthCheckResult 'RecordPoint.Connectors.SDK.Health.IHealthCheckManager.HealthCheckResult')
  - [RunHealthCheckAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Health-IHealthCheckManager-RunHealthCheckAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Health.IHealthCheckManager.RunHealthCheckAsync(System.Threading.CancellationToken)')
- [IHealthCheckReadyAction](#T-RecordPoint-Connectors-SDK-Health-IHealthCheckReadyAction 'RecordPoint.Connectors.SDK.Health.IHealthCheckReadyAction')
  - [CheckIsReadyAsync()](#M-RecordPoint-Connectors-SDK-Health-IHealthCheckReadyAction-CheckIsReadyAsync 'RecordPoint.Connectors.SDK.Health.IHealthCheckReadyAction.CheckIsReadyAsync')
- [IHealthCheckStrategy](#T-RecordPoint-Connectors-SDK-Health-IHealthCheckStrategy 'RecordPoint.Connectors.SDK.Health.IHealthCheckStrategy')
  - [HealthCheckType](#P-RecordPoint-Connectors-SDK-Health-IHealthCheckStrategy-HealthCheckType 'RecordPoint.Connectors.SDK.Health.IHealthCheckStrategy.HealthCheckType')
  - [HealthCheckAsync()](#M-RecordPoint-Connectors-SDK-Health-IHealthCheckStrategy-HealthCheckAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Health.IHealthCheckStrategy.HealthCheckAsync(System.Threading.CancellationToken)')
- [IManagedWorkFactory](#T-RecordPoint-Connectors-SDK-Work-IManagedWorkFactory 'RecordPoint.Connectors.SDK.Work.IManagedWorkFactory')
  - [CreateWork(workStatusId,workType,connectorId,configurationType,configuration)](#M-RecordPoint-Connectors-SDK-Work-IManagedWorkFactory-CreateWork-System-String,System-String,System-String,System-String,System-String- 'RecordPoint.Connectors.SDK.Work.IManagedWorkFactory.CreateWork(System.String,System.String,System.String,System.String,System.String)')
  - [LoadWork(workStatus)](#M-RecordPoint-Connectors-SDK-Work-IManagedWorkFactory-LoadWork-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel- 'RecordPoint.Connectors.SDK.Work.IManagedWorkFactory.LoadWork(RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel)')
- [IManagedWorkManager](#T-RecordPoint-Connectors-SDK-Work-IManagedWorkManager 'RecordPoint.Connectors.SDK.Work.IManagedWorkManager')
  - [WorkStatus](#P-RecordPoint-Connectors-SDK-Work-IManagedWorkManager-WorkStatus 'RecordPoint.Connectors.SDK.Work.IManagedWorkManager.WorkStatus')
  - [AbandonedAsync(reason,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-IManagedWorkManager-AbandonedAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.IManagedWorkManager.AbandonedAsync(System.String,System.Threading.CancellationToken)')
  - [CheckAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-IManagedWorkManager-CheckAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.IManagedWorkManager.CheckAsync(System.Threading.CancellationToken)')
  - [CompleteAsync(reason,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-IManagedWorkManager-CompleteAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.IManagedWorkManager.CompleteAsync(System.String,System.Threading.CancellationToken)')
  - [ContinueAsync(stateType,state,waitTill,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-IManagedWorkManager-ContinueAsync-System-String,System-String,System-DateTimeOffset,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.IManagedWorkManager.ContinueAsync(System.String,System.String,System.DateTimeOffset,System.Threading.CancellationToken)')
  - [FailedAsync(reason,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-IManagedWorkManager-FailedAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.IManagedWorkManager.FailedAsync(System.String,System.Threading.CancellationToken)')
  - [FaultyAsync(reason,exception,cancellationToken,faultedCount)](#M-RecordPoint-Connectors-SDK-Work-IManagedWorkManager-FaultyAsync-System-String,System-Exception,System-Threading-CancellationToken,System-Nullable{System-Int32}- 'RecordPoint.Connectors.SDK.Work.IManagedWorkManager.FaultyAsync(System.String,System.Exception,System.Threading.CancellationToken,System.Nullable{System.Int32})')
  - [RetryAsync(waitTill,cancellationToken,faultedCount)](#M-RecordPoint-Connectors-SDK-Work-IManagedWorkManager-RetryAsync-System-DateTimeOffset,System-Threading-CancellationToken,System-Nullable{System-Int32}- 'RecordPoint.Connectors.SDK.Work.IManagedWorkManager.RetryAsync(System.DateTimeOffset,System.Threading.CancellationToken,System.Nullable{System.Int32})')
  - [StartAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-IManagedWorkManager-StartAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.IManagedWorkManager.StartAsync(System.Threading.CancellationToken)')
- [IManagedWorkStatusManager](#T-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager 'RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager')
  - [AddWorkStatusAsync(managedWorkStatusModel,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager-AddWorkStatusAsync-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager.AddWorkStatusAsync(RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel,System.Threading.CancellationToken)')
  - [GetAllWorkStatusesAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager-GetAllWorkStatusesAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager.GetAllWorkStatusesAsync(System.Threading.CancellationToken)')
  - [GetWorkStatusAsync(workStatusId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager-GetWorkStatusAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager.GetWorkStatusAsync(System.String,System.Threading.CancellationToken)')
  - [GetWorkStatusesAsync(predicate,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager-GetWorkStatusesAsync-System-Linq-Expressions-Expression{System-Func{RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel,System-Boolean}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager.GetWorkStatusesAsync(System.Linq.Expressions.Expression{System.Func{RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel,System.Boolean}},System.Threading.CancellationToken)')
  - [IsAnyAsync(predicate,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager-IsAnyAsync-System-Linq-Expressions-Expression{System-Func{RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel,System-Boolean}},System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager.IsAnyAsync(System.Linq.Expressions.Expression{System.Func{RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel,System.Boolean}},System.Threading.CancellationToken)')
  - [RemoveWorkStatusesAsync(workIds,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager-RemoveWorkStatusesAsync-System-String[],System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager.RemoveWorkStatusesAsync(System.String[],System.Threading.CancellationToken)')
  - [SetWorkAbandonedAsync(workStatusId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager-SetWorkAbandonedAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager.SetWorkAbandonedAsync(System.String,System.Threading.CancellationToken)')
  - [SetWorkCompleteAsync(workStatusId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager-SetWorkCompleteAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager.SetWorkCompleteAsync(System.String,System.Threading.CancellationToken)')
  - [SetWorkContinueAsync(workStatusId,continuedWorkId,state,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager-SetWorkContinueAsync-System-String,System-String,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager.SetWorkContinueAsync(System.String,System.String,System.String,System.Threading.CancellationToken)')
  - [SetWorkFailedAsync(workStatusId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager-SetWorkFailedAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager.SetWorkFailedAsync(System.String,System.Threading.CancellationToken)')
  - [SetWorkRunningAsync(workStatusId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager-SetWorkRunningAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.IManagedWorkStatusManager.SetWorkRunningAsync(System.String,System.Threading.CancellationToken)')
- [INotificationManager](#T-RecordPoint-Connectors-SDK-Notifications-INotificationManager 'RecordPoint.Connectors.SDK.Notifications.INotificationManager')
  - [HandleNotificationAsync(notification,cancellationToken)](#M-RecordPoint-Connectors-SDK-Notifications-INotificationManager-HandleNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Notifications.INotificationManager.HandleNotificationAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel,System.Threading.CancellationToken)')
- [INotificationStrategy](#T-RecordPoint-Connectors-SDK-Notifications-INotificationStrategy 'RecordPoint.Connectors.SDK.Notifications.INotificationStrategy')
  - [NotificationType](#P-RecordPoint-Connectors-SDK-Notifications-INotificationStrategy-NotificationType 'RecordPoint.Connectors.SDK.Notifications.INotificationStrategy.NotificationType')
  - [HandleNotificationAsync(notification,cancellationToken)](#M-RecordPoint-Connectors-SDK-Notifications-INotificationStrategy-HandleNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Notifications.INotificationStrategy.HandleNotificationAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel,System.Threading.CancellationToken)')
- [INotificationWorker](#T-RecordPoint-Connectors-SDK-Notifications-INotificationWorker 'RecordPoint.Connectors.SDK.Notifications.INotificationWorker')
  - [HandleNotificationAsync(notificationRequest,cancellationToken)](#M-RecordPoint-Connectors-SDK-Notifications-INotificationWorker-HandleNotificationAsync-RecordPoint-Connectors-SDK-Notifications-Notification,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Notifications.INotificationWorker.HandleNotificationAsync(RecordPoint.Connectors.SDK.Notifications.Notification,System.Threading.CancellationToken)')
- [IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope')
  - [Dimensions](#P-RecordPoint-Connectors-SDK-Observability-IObservabilityScope-Dimensions 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope.Dimensions')
  - [Measures](#P-RecordPoint-Connectors-SDK-Observability-IObservabilityScope-Measures 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope.Measures')
  - [BeginScope(dimensions,measures)](#M-RecordPoint-Connectors-SDK-Observability-IObservabilityScope-BeginScope-RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures- 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope.BeginScope(RecordPoint.Connectors.SDK.Observability.Dimensions,RecordPoint.Connectors.SDK.Observability.Measures)')
- [IQueueableWork](#T-RecordPoint-Connectors-SDK-Work-IQueueableWork 'RecordPoint.Connectors.SDK.Work.IQueueableWork')
  - [WorkRequest](#P-RecordPoint-Connectors-SDK-Work-IQueueableWork-WorkRequest 'RecordPoint.Connectors.SDK.Work.IQueueableWork.WorkRequest')
  - [GetWorkResult()](#M-RecordPoint-Connectors-SDK-Work-IQueueableWork-GetWorkResult 'RecordPoint.Connectors.SDK.Work.IQueueableWork.GetWorkResult')
  - [RunWorkRequestAsync(workRequest,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-IQueueableWork-RunWorkRequestAsync-RecordPoint-Connectors-SDK-Work-WorkRequest,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.IQueueableWork.RunWorkRequestAsync(RecordPoint.Connectors.SDK.Work.WorkRequest,System.Threading.CancellationToken)')
- [IQueueableWorkManager](#T-RecordPoint-Connectors-SDK-Work-IQueueableWorkManager 'RecordPoint.Connectors.SDK.Work.IQueueableWorkManager')
  - [HandleWorkRequestAsync(workRequest,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-IQueueableWorkManager-HandleWorkRequestAsync-RecordPoint-Connectors-SDK-Work-WorkRequest,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.IQueueableWorkManager.HandleWorkRequestAsync(RecordPoint.Connectors.SDK.Work.WorkRequest,System.Threading.CancellationToken)')
- [IR365Client](#T-RecordPoint-Connectors-SDK-R365-IR365Client 'RecordPoint.Connectors.SDK.R365.IR365Client')
  - [IsConfigured()](#M-RecordPoint-Connectors-SDK-R365-IR365Client-IsConfigured 'RecordPoint.Connectors.SDK.R365.IR365Client.IsConfigured')
  - [SubmitAggregation(connectorConfig,aggregation,cancellationToken)](#M-RecordPoint-Connectors-SDK-R365-IR365Client-SubmitAggregation-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Aggregation,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.R365.IR365Client.SubmitAggregation(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,RecordPoint.Connectors.SDK.Content.Aggregation,System.Threading.CancellationToken)')
  - [SubmitAuditEvent(connectorConfig,auditEvent,cancellationToken)](#M-RecordPoint-Connectors-SDK-R365-IR365Client-SubmitAuditEvent-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-AuditEvent,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.R365.IR365Client.SubmitAuditEvent(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,RecordPoint.Connectors.SDK.Content.AuditEvent,System.Threading.CancellationToken)')
  - [SubmitBinary(connectorConfig,binaryMetaInfo,binaryStream,cancellationToken)](#M-RecordPoint-Connectors-SDK-R365-IR365Client-SubmitBinary-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-BinaryMetaInfo,System-IO-Stream,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.R365.IR365Client.SubmitBinary(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,RecordPoint.Connectors.SDK.Content.BinaryMetaInfo,System.IO.Stream,System.Threading.CancellationToken)')
  - [SubmitRecord(connectorConfig,record,cancellationToken)](#M-RecordPoint-Connectors-SDK-R365-IR365Client-SubmitRecord-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Record,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.R365.IR365Client.SubmitRecord(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,RecordPoint.Connectors.SDK.Content.Record,System.Threading.CancellationToken)')
- [IR365ConfigurationClient](#T-RecordPoint-Connectors-SDK-Configuration-IR365ConfigurationClient 'RecordPoint.Connectors.SDK.Configuration.IR365ConfigurationClient')
  - [GetR365Configuration()](#M-RecordPoint-Connectors-SDK-Configuration-IR365ConfigurationClient-GetR365Configuration-System-String- 'RecordPoint.Connectors.SDK.Configuration.IR365ConfigurationClient.GetR365Configuration(System.String)')
  - [R365ConfigurationExists()](#M-RecordPoint-Connectors-SDK-Configuration-IR365ConfigurationClient-R365ConfigurationExists 'RecordPoint.Connectors.SDK.Configuration.IR365ConfigurationClient.R365ConfigurationExists')
- [IR365Pipelines](#T-RecordPoint-Connectors-SDK-R365-IR365Pipelines 'RecordPoint.Connectors.SDK.R365.IR365Pipelines')
  - [AggregationPipeline](#P-RecordPoint-Connectors-SDK-R365-IR365Pipelines-AggregationPipeline 'RecordPoint.Connectors.SDK.R365.IR365Pipelines.AggregationPipeline')
  - [AuditEventPipeline](#P-RecordPoint-Connectors-SDK-R365-IR365Pipelines-AuditEventPipeline 'RecordPoint.Connectors.SDK.R365.IR365Pipelines.AuditEventPipeline')
  - [BinaryPipeline](#P-RecordPoint-Connectors-SDK-R365-IR365Pipelines-BinaryPipeline 'RecordPoint.Connectors.SDK.R365.IR365Pipelines.BinaryPipeline')
  - [RecordPipeline](#P-RecordPoint-Connectors-SDK-R365-IR365Pipelines-RecordPipeline 'RecordPoint.Connectors.SDK.R365.IR365Pipelines.RecordPipeline')
- [IRecordDisposalAction](#T-RecordPoint-Connectors-SDK-ContentManager-IRecordDisposalAction 'RecordPoint.Connectors.SDK.ContentManager.IRecordDisposalAction')
  - [ExecuteAsync(connectorConfiguration,record,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-IRecordDisposalAction-ExecuteAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Record,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.IRecordDisposalAction.ExecuteAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,RecordPoint.Connectors.SDK.Content.Record,System.Threading.CancellationToken)')
- [IRecordSubmissionCallbackAction](#T-RecordPoint-Connectors-SDK-ContentManager-IRecordSubmissionCallbackAction 'RecordPoint.Connectors.SDK.ContentManager.IRecordSubmissionCallbackAction')
  - [ExecuteAsync(connectorConfiguration,record,submissionActionType,cancellationToken)](#M-RecordPoint-Connectors-SDK-ContentManager-IRecordSubmissionCallbackAction-ExecuteAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Record,RecordPoint-Connectors-SDK-Abstractions-ContentManager-SubmissionActionType,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.ContentManager.IRecordSubmissionCallbackAction.ExecuteAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,RecordPoint.Connectors.SDK.Content.Record,RecordPoint.Connectors.SDK.Abstractions.ContentManager.SubmissionActionType,System.Threading.CancellationToken)')
- [ISemaphoreLockManager](#T-RecordPoint-Connectors-SDK-Caching-Semaphore-ISemaphoreLockManager 'RecordPoint.Connectors.SDK.Caching.Semaphore.ISemaphoreLockManager')
  - [ConnectorConfiguration](#P-RecordPoint-Connectors-SDK-Caching-Semaphore-ISemaphoreLockManager-ConnectorConfiguration 'RecordPoint.Connectors.SDK.Caching.Semaphore.ISemaphoreLockManager.ConnectorConfiguration')
  - [CheckWaitSemaphoreAsync(workType,context,cancellationToken)](#M-RecordPoint-Connectors-SDK-Caching-Semaphore-ISemaphoreLockManager-CheckWaitSemaphoreAsync-System-String,System-Object,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Caching.Semaphore.ISemaphoreLockManager.CheckWaitSemaphoreAsync(System.String,System.Object,System.Threading.CancellationToken)')
  - [GetSemaphoreAsync(workType,context,cancellationToken)](#M-RecordPoint-Connectors-SDK-Caching-Semaphore-ISemaphoreLockManager-GetSemaphoreAsync-System-String,System-Object,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Caching.Semaphore.ISemaphoreLockManager.GetSemaphoreAsync(System.String,System.Object,System.Threading.CancellationToken)')
  - [SetSemaphoreAsync(semaphoreLockType,workType,context,duration,cancellationToken)](#M-RecordPoint-Connectors-SDK-Caching-Semaphore-ISemaphoreLockManager-SetSemaphoreAsync-RecordPoint-Connectors-SDK-Caching-Semaphore-SemaphoreLockType,System-String,System-Object,System-Int32,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Caching.Semaphore.ISemaphoreLockManager.SetSemaphoreAsync(RecordPoint.Connectors.SDK.Caching.Semaphore.SemaphoreLockType,System.String,System.Object,System.Int32,System.Threading.CancellationToken)')
- [ISemaphoreLockScopedKeyAction](#T-RecordPoint-Connectors-SDK-Caching-ISemaphoreLockScopedKeyAction 'RecordPoint.Connectors.SDK.Caching.ISemaphoreLockScopedKeyAction')
  - [ExecuteAsync(connectorConfigModel,workType,context,cancellationToken)](#M-RecordPoint-Connectors-SDK-Caching-ISemaphoreLockScopedKeyAction-ExecuteAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,System-String,System-Object,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Caching.ISemaphoreLockScopedKeyAction.ExecuteAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,System.String,System.Object,System.Threading.CancellationToken)')
- [IStatusManager](#T-RecordPoint-Connectors-SDK-Status-IStatusManager 'RecordPoint.Connectors.SDK.Status.IStatusManager')
  - [GetStatusModelAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Status-IStatusManager-GetStatusModelAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Status.IStatusManager.GetStatusModelAsync(System.Threading.CancellationToken)')
- [IStatusStrategy](#T-RecordPoint-Connectors-SDK-Status-IStatusStrategy 'RecordPoint.Connectors.SDK.Status.IStatusStrategy')
  - [GetStatusText(connectorModel,cancellationToken)](#M-RecordPoint-Connectors-SDK-Status-IStatusStrategy-GetStatusText-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Status.IStatusStrategy.GetStatusText(RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel,System.Threading.CancellationToken)')
- [ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext')
  - [GetCompanyName()](#M-RecordPoint-Connectors-SDK-Context-ISystemContext-GetCompanyName 'RecordPoint.Connectors.SDK.Context.ISystemContext.GetCompanyName')
  - [GetConnectorName()](#M-RecordPoint-Connectors-SDK-Context-ISystemContext-GetConnectorName 'RecordPoint.Connectors.SDK.Context.ISystemContext.GetConnectorName')
  - [GetDataRootPath()](#M-RecordPoint-Connectors-SDK-Context-ISystemContext-GetDataRootPath 'RecordPoint.Connectors.SDK.Context.ISystemContext.GetDataRootPath')
  - [GetServiceName()](#M-RecordPoint-Connectors-SDK-Context-ISystemContext-GetServiceName 'RecordPoint.Connectors.SDK.Context.ISystemContext.GetServiceName')
  - [GetShortName()](#M-RecordPoint-Connectors-SDK-Context-ISystemContext-GetShortName 'RecordPoint.Connectors.SDK.Context.ISystemContext.GetShortName')
- [ITelemetrySink](#T-RecordPoint-Connectors-SDK-Observability-ITelemetrySink 'RecordPoint.Connectors.SDK.Observability.ITelemetrySink')
  - [TrackEvent(name,dimensions,measures)](#M-RecordPoint-Connectors-SDK-Observability-ITelemetrySink-TrackEvent-System-String,RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures- 'RecordPoint.Connectors.SDK.Observability.ITelemetrySink.TrackEvent(System.String,RecordPoint.Connectors.SDK.Observability.Dimensions,RecordPoint.Connectors.SDK.Observability.Measures)')
  - [TrackException(exception,dimensions,measures)](#M-RecordPoint-Connectors-SDK-Observability-ITelemetrySink-TrackException-System-Exception,RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures- 'RecordPoint.Connectors.SDK.Observability.ITelemetrySink.TrackException(System.Exception,RecordPoint.Connectors.SDK.Observability.Dimensions,RecordPoint.Connectors.SDK.Observability.Measures)')
  - [TrackTrace(message,severityLevel,dimensions)](#M-RecordPoint-Connectors-SDK-Observability-ITelemetrySink-TrackTrace-System-String,RecordPoint-Connectors-SDK-Observability-SeverityLevel,RecordPoint-Connectors-SDK-Observability-Dimensions- 'RecordPoint.Connectors.SDK.Observability.ITelemetrySink.TrackTrace(System.String,RecordPoint.Connectors.SDK.Observability.SeverityLevel,RecordPoint.Connectors.SDK.Observability.Dimensions)')
- [ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker')
  - [BeginScope()](#M-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker-BeginScope-RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures- 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker.BeginScope(RecordPoint.Connectors.SDK.Observability.Dimensions,RecordPoint.Connectors.SDK.Observability.Measures)')
  - [TrackEvent(name,dimensions,measures)](#M-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker-TrackEvent-System-String,RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures- 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker.TrackEvent(System.String,RecordPoint.Connectors.SDK.Observability.Dimensions,RecordPoint.Connectors.SDK.Observability.Measures)')
  - [TrackException(exception,dimensions,measures)](#M-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker-TrackException-System-Exception,RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures- 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker.TrackException(System.Exception,RecordPoint.Connectors.SDK.Observability.Dimensions,RecordPoint.Connectors.SDK.Observability.Measures)')
  - [TrackTrace(message,severityLevel,dimensions)](#M-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker-TrackTrace-System-String,RecordPoint-Connectors-SDK-Observability-SeverityLevel,RecordPoint-Connectors-SDK-Observability-Dimensions- 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker.TrackTrace(System.String,RecordPoint.Connectors.SDK.Observability.SeverityLevel,RecordPoint.Connectors.SDK.Observability.Dimensions)')
- [IToggleProvider](#T-RecordPoint-Connectors-SDK-Toggles-IToggleProvider 'RecordPoint.Connectors.SDK.Toggles.IToggleProvider')
  - [GetToggleBool(toggle,default)](#M-RecordPoint-Connectors-SDK-Toggles-IToggleProvider-GetToggleBool-System-String,System-Boolean- 'RecordPoint.Connectors.SDK.Toggles.IToggleProvider.GetToggleBool(System.String,System.Boolean)')
  - [GetToggleBool(toggle,userKey,default)](#M-RecordPoint-Connectors-SDK-Toggles-IToggleProvider-GetToggleBool-System-String,System-String,System-Boolean- 'RecordPoint.Connectors.SDK.Toggles.IToggleProvider.GetToggleBool(System.String,System.String,System.Boolean)')
  - [GetToggleNumber(toggle,userKey,default)](#M-RecordPoint-Connectors-SDK-Toggles-IToggleProvider-GetToggleNumber-System-String,System-String,System-Int32- 'RecordPoint.Connectors.SDK.Toggles.IToggleProvider.GetToggleNumber(System.String,System.String,System.Int32)')
  - [GetToggleNumber(toggle,default)](#M-RecordPoint-Connectors-SDK-Toggles-IToggleProvider-GetToggleNumber-System-String,System-Int32- 'RecordPoint.Connectors.SDK.Toggles.IToggleProvider.GetToggleNumber(System.String,System.Int32)')
  - [GetToggleString(toggle,userKey,default)](#M-RecordPoint-Connectors-SDK-Toggles-IToggleProvider-GetToggleString-System-String,System-String,System-String- 'RecordPoint.Connectors.SDK.Toggles.IToggleProvider.GetToggleString(System.String,System.String,System.String)')
  - [GetToggleString(toggle,default)](#M-RecordPoint-Connectors-SDK-Toggles-IToggleProvider-GetToggleString-System-String,System-String- 'RecordPoint.Connectors.SDK.Toggles.IToggleProvider.GetToggleString(System.String,System.String)')
- [IWork](#T-RecordPoint-Connectors-SDK-Work-IWork 'RecordPoint.Connectors.SDK.Work.IWork')
  - [Exception](#P-RecordPoint-Connectors-SDK-Work-IWork-Exception 'RecordPoint.Connectors.SDK.Work.IWork.Exception')
  - [FinishDateTime](#P-RecordPoint-Connectors-SDK-Work-IWork-FinishDateTime 'RecordPoint.Connectors.SDK.Work.IWork.FinishDateTime')
  - [HasResult](#P-RecordPoint-Connectors-SDK-Work-IWork-HasResult 'RecordPoint.Connectors.SDK.Work.IWork.HasResult')
  - [Id](#P-RecordPoint-Connectors-SDK-Work-IWork-Id 'RecordPoint.Connectors.SDK.Work.IWork.Id')
  - [ResultReason](#P-RecordPoint-Connectors-SDK-Work-IWork-ResultReason 'RecordPoint.Connectors.SDK.Work.IWork.ResultReason')
  - [ResultType](#P-RecordPoint-Connectors-SDK-Work-IWork-ResultType 'RecordPoint.Connectors.SDK.Work.IWork.ResultType')
  - [StartDateTime](#P-RecordPoint-Connectors-SDK-Work-IWork-StartDateTime 'RecordPoint.Connectors.SDK.Work.IWork.StartDateTime')
  - [WorkDuration](#P-RecordPoint-Connectors-SDK-Work-IWork-WorkDuration 'RecordPoint.Connectors.SDK.Work.IWork.WorkDuration')
  - [WorkType](#P-RecordPoint-Connectors-SDK-Work-IWork-WorkType 'RecordPoint.Connectors.SDK.Work.IWork.WorkType')
- [IWorkQueueClient](#T-RecordPoint-Connectors-SDK-Work-IWorkQueueClient 'RecordPoint.Connectors.SDK.Work.IWorkQueueClient')
  - [SubmitWorkAsync(workRequest,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-IWorkQueueClient-SubmitWorkAsync-RecordPoint-Connectors-SDK-Work-WorkRequest,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.IWorkQueueClient.SubmitWorkAsync(RecordPoint.Connectors.SDK.Work.WorkRequest,System.Threading.CancellationToken)')
- [IWork\`1](#T-RecordPoint-Connectors-SDK-Work-IWork`1 'RecordPoint.Connectors.SDK.Work.IWork`1')
  - [Parameter](#P-RecordPoint-Connectors-SDK-Work-IWork`1-Parameter 'RecordPoint.Connectors.SDK.Work.IWork`1.Parameter')
  - [RunAsync(parameter,cancellationToken)](#M-RecordPoint-Connectors-SDK-Work-IWork`1-RunAsync-`0,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Work.IWork`1.RunAsync(`0,System.Threading.CancellationToken)')
- [ManagedWorkStatusModel](#T-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel')
  - [Configuration](#P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-Configuration 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel.Configuration')
  - [ConfigurationType](#P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-ConfigurationType 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel.ConfigurationType')
  - [ConnectorId](#P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-ConnectorId 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel.ConnectorId')
  - [ExponentialRetryDelay](#P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-ExponentialRetryDelay 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel.ExponentialRetryDelay')
  - [Id](#P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-Id 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel.Id')
  - [LastStatusUpdate](#P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-LastStatusUpdate 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel.LastStatusUpdate')
  - [MaxRetries](#P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-MaxRetries 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel.MaxRetries')
  - [MaxRetryDelay](#P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-MaxRetryDelay 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel.MaxRetryDelay')
  - [RetryDelay](#P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-RetryDelay 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel.RetryDelay')
  - [RetryOnFailure](#P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-RetryOnFailure 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel.RetryOnFailure')
  - [State](#P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-State 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel.State')
  - [StateType](#P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-StateType 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel.StateType')
  - [Status](#P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-Status 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel.Status')
  - [WorkId](#P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-WorkId 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel.WorkId')
  - [WorkType](#P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-WorkType 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel.WorkType')
  - [CopyTo(target)](#M-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-CopyTo-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel- 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel.CopyTo(RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel)')
  - [Deserialize()](#M-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-Deserialize-System-String- 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel.Deserialize(System.String)')
  - [Serialize()](#M-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-Serialize 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel.Serialize')
- [ManagedWorkStatuses](#T-RecordPoint-Connectors-SDK-Work-ManagedWorkStatuses 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatuses')
  - [Abandoned](#F-RecordPoint-Connectors-SDK-Work-ManagedWorkStatuses-Abandoned 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatuses.Abandoned')
  - [Complete](#F-RecordPoint-Connectors-SDK-Work-ManagedWorkStatuses-Complete 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatuses.Complete')
  - [Failed](#F-RecordPoint-Connectors-SDK-Work-ManagedWorkStatuses-Failed 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatuses.Failed')
  - [Running](#F-RecordPoint-Connectors-SDK-Work-ManagedWorkStatuses-Running 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatuses.Running')
- [Measures](#T-RecordPoint-Connectors-SDK-Observability-Measures 'RecordPoint.Connectors.SDK.Observability.Measures')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Observability-Measures-#ctor 'RecordPoint.Connectors.SDK.Observability.Measures.#ctor')
  - [#ctor(collection)](#M-RecordPoint-Connectors-SDK-Observability-Measures-#ctor-System-Collections-Generic-IEnumerable{System-Collections-Generic-KeyValuePair{System-String,System-Double}}- 'RecordPoint.Connectors.SDK.Observability.Measures.#ctor(System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.String,System.Double}})')
- [MetaDataItem](#T-RecordPoint-Connectors-SDK-Content-MetaDataItem 'RecordPoint.Connectors.SDK.Content.MetaDataItem')
  - [MetaDataItemType](#P-RecordPoint-Connectors-SDK-Content-MetaDataItem-MetaDataItemType 'RecordPoint.Connectors.SDK.Content.MetaDataItem.MetaDataItemType')
  - [Name](#P-RecordPoint-Connectors-SDK-Content-MetaDataItem-Name 'RecordPoint.Connectors.SDK.Content.MetaDataItem.Name')
  - [Type](#P-RecordPoint-Connectors-SDK-Content-MetaDataItem-Type 'RecordPoint.Connectors.SDK.Content.MetaDataItem.Type')
  - [Value](#P-RecordPoint-Connectors-SDK-Content-MetaDataItem-Value 'RecordPoint.Connectors.SDK.Content.MetaDataItem.Value')
  - [Equals(other)](#M-RecordPoint-Connectors-SDK-Content-MetaDataItem-Equals-RecordPoint-Connectors-SDK-Content-MetaDataItem- 'RecordPoint.Connectors.SDK.Content.MetaDataItem.Equals(RecordPoint.Connectors.SDK.Content.MetaDataItem)')
  - [Equals(obj)](#M-RecordPoint-Connectors-SDK-Content-MetaDataItem-Equals-System-Object- 'RecordPoint.Connectors.SDK.Content.MetaDataItem.Equals(System.Object)')
  - [GetHashCode()](#M-RecordPoint-Connectors-SDK-Content-MetaDataItem-GetHashCode 'RecordPoint.Connectors.SDK.Content.MetaDataItem.GetHashCode')
- [MetaDataItemExtensions](#T-RecordPoint-Connectors-SDK-Content-MetaDataItemExtensions 'RecordPoint.Connectors.SDK.Content.MetaDataItemExtensions')
  - [IsEqual(x,y)](#M-RecordPoint-Connectors-SDK-Content-MetaDataItemExtensions-IsEqual-System-Collections-Generic-List{RecordPoint-Connectors-SDK-Content-MetaDataItem},System-Collections-Generic-List{RecordPoint-Connectors-SDK-Content-MetaDataItem}- 'RecordPoint.Connectors.SDK.Content.MetaDataItemExtensions.IsEqual(System.Collections.Generic.List{RecordPoint.Connectors.SDK.Content.MetaDataItem},System.Collections.Generic.List{RecordPoint.Connectors.SDK.Content.MetaDataItem})')
- [MetaDataItemType](#T-RecordPoint-Connectors-SDK-Content-MetaDataItemType 'RecordPoint.Connectors.SDK.Content.MetaDataItemType')
  - [Internal](#F-RecordPoint-Connectors-SDK-Content-MetaDataItemType-Internal 'RecordPoint.Connectors.SDK.Content.MetaDataItemType.Internal')
  - [R365MetaData](#F-RecordPoint-Connectors-SDK-Content-MetaDataItemType-R365MetaData 'RecordPoint.Connectors.SDK.Content.MetaDataItemType.R365MetaData')
- [Notification](#T-RecordPoint-Connectors-SDK-Notifications-Notification 'RecordPoint.Connectors.SDK.Notifications.Notification')
  - [#ctor(value)](#M-RecordPoint-Connectors-SDK-Notifications-Notification-#ctor-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel- 'RecordPoint.Connectors.SDK.Notifications.Notification.#ctor(RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel)')
  - [NotificationType](#P-RecordPoint-Connectors-SDK-Notifications-Notification-NotificationType 'RecordPoint.Connectors.SDK.Notifications.Notification.NotificationType')
  - [Value](#P-RecordPoint-Connectors-SDK-Notifications-Notification-Value 'RecordPoint.Connectors.SDK.Notifications.Notification.Value')
- [NotificationOutcome](#T-RecordPoint-Connectors-SDK-Notifications-NotificationOutcome 'RecordPoint.Connectors.SDK.Notifications.NotificationOutcome')
  - [OutcomeType](#P-RecordPoint-Connectors-SDK-Notifications-NotificationOutcome-OutcomeType 'RecordPoint.Connectors.SDK.Notifications.NotificationOutcome.OutcomeType')
  - [Reason](#P-RecordPoint-Connectors-SDK-Notifications-NotificationOutcome-Reason 'RecordPoint.Connectors.SDK.Notifications.NotificationOutcome.Reason')
  - [Failed()](#M-RecordPoint-Connectors-SDK-Notifications-NotificationOutcome-Failed-System-String- 'RecordPoint.Connectors.SDK.Notifications.NotificationOutcome.Failed(System.String)')
  - [OK()](#M-RecordPoint-Connectors-SDK-Notifications-NotificationOutcome-OK 'RecordPoint.Connectors.SDK.Notifications.NotificationOutcome.OK')
- [NotificationOutcomeType](#T-RecordPoint-Connectors-SDK-Notifications-NotificationOutcomeType 'RecordPoint.Connectors.SDK.Notifications.NotificationOutcomeType')
  - [Failed](#F-RecordPoint-Connectors-SDK-Notifications-NotificationOutcomeType-Failed 'RecordPoint.Connectors.SDK.Notifications.NotificationOutcomeType.Failed')
  - [Ok](#F-RecordPoint-Connectors-SDK-Notifications-NotificationOutcomeType-Ok 'RecordPoint.Connectors.SDK.Notifications.NotificationOutcomeType.Ok')
- [NotificationsPollerOptions](#T-RecordPoint-Connectors-SDK-Notifications-NotificationsPollerOptions 'RecordPoint.Connectors.SDK.Notifications.NotificationsPollerOptions')
  - [PollIntervalSeconds](#P-RecordPoint-Connectors-SDK-Notifications-NotificationsPollerOptions-PollIntervalSeconds 'RecordPoint.Connectors.SDK.Notifications.NotificationsPollerOptions.PollIntervalSeconds')
- [R365ConfigurationModel](#T-RecordPoint-Connectors-SDK-Configuration-R365ConfigurationModel 'RecordPoint.Connectors.SDK.Configuration.R365ConfigurationModel')
  - [Audience](#P-RecordPoint-Connectors-SDK-Configuration-R365ConfigurationModel-Audience 'RecordPoint.Connectors.SDK.Configuration.R365ConfigurationModel.Audience')
  - [ClientId](#P-RecordPoint-Connectors-SDK-Configuration-R365ConfigurationModel-ClientId 'RecordPoint.Connectors.SDK.Configuration.R365ConfigurationModel.ClientId')
  - [ClientSecret](#P-RecordPoint-Connectors-SDK-Configuration-R365ConfigurationModel-ClientSecret 'RecordPoint.Connectors.SDK.Configuration.R365ConfigurationModel.ClientSecret')
  - [ConnectorApiUrl](#P-RecordPoint-Connectors-SDK-Configuration-R365ConfigurationModel-ConnectorApiUrl 'RecordPoint.Connectors.SDK.Configuration.R365ConfigurationModel.ConnectorApiUrl')
  - [ServerCertificateValidation](#P-RecordPoint-Connectors-SDK-Configuration-R365ConfigurationModel-ServerCertificateValidation 'RecordPoint.Connectors.SDK.Configuration.R365ConfigurationModel.ServerCertificateValidation')
- [R365MultiConfigurationModel](#T-RecordPoint-Connectors-SDK-Configuration-R365MultiConfigurationModel 'RecordPoint.Connectors.SDK.Configuration.R365MultiConfigurationModel')
  - [DefaultConfigurationKey](#F-RecordPoint-Connectors-SDK-Configuration-R365MultiConfigurationModel-DefaultConfigurationKey 'RecordPoint.Connectors.SDK.Configuration.R365MultiConfigurationModel.DefaultConfigurationKey')
- [Record](#T-RecordPoint-Connectors-SDK-Content-Record 'RecordPoint.Connectors.SDK.Content.Record')
  - [Binaries](#P-RecordPoint-Connectors-SDK-Content-Record-Binaries 'RecordPoint.Connectors.SDK.Content.Record.Binaries')
  - [FileSize](#P-RecordPoint-Connectors-SDK-Content-Record-FileSize 'RecordPoint.Connectors.SDK.Content.Record.FileSize')
  - [Location](#P-RecordPoint-Connectors-SDK-Content-Record-Location 'RecordPoint.Connectors.SDK.Content.Record.Location')
  - [MediaType](#P-RecordPoint-Connectors-SDK-Content-Record-MediaType 'RecordPoint.Connectors.SDK.Content.Record.MediaType')
  - [MimeType](#P-RecordPoint-Connectors-SDK-Content-Record-MimeType 'RecordPoint.Connectors.SDK.Content.Record.MimeType')
  - [ParentExternalId](#P-RecordPoint-Connectors-SDK-Content-Record-ParentExternalId 'RecordPoint.Connectors.SDK.Content.Record.ParentExternalId')
  - [Equals(other)](#M-RecordPoint-Connectors-SDK-Content-Record-Equals-RecordPoint-Connectors-SDK-Content-Record- 'RecordPoint.Connectors.SDK.Content.Record.Equals(RecordPoint.Connectors.SDK.Content.Record)')
  - [Equals()](#M-RecordPoint-Connectors-SDK-Content-Record-Equals-System-Object- 'RecordPoint.Connectors.SDK.Content.Record.Equals(System.Object)')
  - [GetHashCode()](#M-RecordPoint-Connectors-SDK-Content-Record-GetHashCode 'RecordPoint.Connectors.SDK.Content.Record.GetHashCode')
- [RecordDisposalResult](#T-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalResult 'RecordPoint.Connectors.SDK.ContentManager.RecordDisposalResult')
  - [Exception](#P-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalResult-Exception 'RecordPoint.Connectors.SDK.ContentManager.RecordDisposalResult.Exception')
  - [Reason](#P-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalResult-Reason 'RecordPoint.Connectors.SDK.ContentManager.RecordDisposalResult.Reason')
  - [ResultType](#P-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalResult-ResultType 'RecordPoint.Connectors.SDK.ContentManager.RecordDisposalResult.ResultType')
- [RecordDisposalResultType](#T-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalResultType 'RecordPoint.Connectors.SDK.ContentManager.RecordDisposalResultType')
  - [BackOff](#F-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalResultType-BackOff 'RecordPoint.Connectors.SDK.ContentManager.RecordDisposalResultType.BackOff')
  - [Complete](#F-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalResultType-Complete 'RecordPoint.Connectors.SDK.ContentManager.RecordDisposalResultType.Complete')
  - [Deleted](#F-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalResultType-Deleted 'RecordPoint.Connectors.SDK.ContentManager.RecordDisposalResultType.Deleted')
  - [Failed](#F-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalResultType-Failed 'RecordPoint.Connectors.SDK.ContentManager.RecordDisposalResultType.Failed')
- [RequiredValueNullException](#T-RecordPoint-Connectors-SDK-RequiredValueNullException 'RecordPoint.Connectors.SDK.RequiredValueNullException')
  - [#ctor(paramName)](#M-RecordPoint-Connectors-SDK-RequiredValueNullException-#ctor-System-String- 'RecordPoint.Connectors.SDK.RequiredValueNullException.#ctor(System.String)')
  - [NULL_VALUE_MESSAGE](#F-RecordPoint-Connectors-SDK-RequiredValueNullException-NULL_VALUE_MESSAGE 'RecordPoint.Connectors.SDK.RequiredValueNullException.NULL_VALUE_MESSAGE')
  - [_paramName](#F-RecordPoint-Connectors-SDK-RequiredValueNullException-_paramName 'RecordPoint.Connectors.SDK.RequiredValueNullException._paramName')
  - [ParamName](#P-RecordPoint-Connectors-SDK-RequiredValueNullException-ParamName 'RecordPoint.Connectors.SDK.RequiredValueNullException.ParamName')
  - [ThrowIfNull(value,paramName)](#M-RecordPoint-Connectors-SDK-RequiredValueNullException-ThrowIfNull-System-Object,System-String- 'RecordPoint.Connectors.SDK.RequiredValueNullException.ThrowIfNull(System.Object,System.String)')
  - [ThrowIfNullOrEmpty(value,paramName)](#M-RecordPoint-Connectors-SDK-RequiredValueNullException-ThrowIfNullOrEmpty-System-String,System-String- 'RecordPoint.Connectors.SDK.RequiredValueNullException.ThrowIfNullOrEmpty(System.String,System.String)')
- [RequiredValueOutOfRangeException](#T-RecordPoint-Connectors-SDK-RequiredValueOutOfRangeException 'RecordPoint.Connectors.SDK.RequiredValueOutOfRangeException')
  - [#ctor(paramName)](#M-RecordPoint-Connectors-SDK-RequiredValueOutOfRangeException-#ctor-System-String- 'RecordPoint.Connectors.SDK.RequiredValueOutOfRangeException.#ctor(System.String)')
  - [NULL_VALUE_MESSAGE](#F-RecordPoint-Connectors-SDK-RequiredValueOutOfRangeException-NULL_VALUE_MESSAGE 'RecordPoint.Connectors.SDK.RequiredValueOutOfRangeException.NULL_VALUE_MESSAGE')
  - [_paramName](#F-RecordPoint-Connectors-SDK-RequiredValueOutOfRangeException-_paramName 'RecordPoint.Connectors.SDK.RequiredValueOutOfRangeException._paramName')
  - [ParamName](#P-RecordPoint-Connectors-SDK-RequiredValueOutOfRangeException-ParamName 'RecordPoint.Connectors.SDK.RequiredValueOutOfRangeException.ParamName')
- [SemaphoreLockType](#T-RecordPoint-Connectors-SDK-Caching-Semaphore-SemaphoreLockType 'RecordPoint.Connectors.SDK.Caching.Semaphore.SemaphoreLockType')
  - [Global](#F-RecordPoint-Connectors-SDK-Caching-Semaphore-SemaphoreLockType-Global 'RecordPoint.Connectors.SDK.Caching.Semaphore.SemaphoreLockType.Global')
  - [Scoped](#F-RecordPoint-Connectors-SDK-Caching-Semaphore-SemaphoreLockType-Scoped 'RecordPoint.Connectors.SDK.Caching.Semaphore.SemaphoreLockType.Scoped')
- [SeverityLevel](#T-RecordPoint-Connectors-SDK-Observability-SeverityLevel 'RecordPoint.Connectors.SDK.Observability.SeverityLevel')
  - [Critical](#F-RecordPoint-Connectors-SDK-Observability-SeverityLevel-Critical 'RecordPoint.Connectors.SDK.Observability.SeverityLevel.Critical')
  - [Error](#F-RecordPoint-Connectors-SDK-Observability-SeverityLevel-Error 'RecordPoint.Connectors.SDK.Observability.SeverityLevel.Error')
  - [Information](#F-RecordPoint-Connectors-SDK-Observability-SeverityLevel-Information 'RecordPoint.Connectors.SDK.Observability.SeverityLevel.Information')
  - [None](#F-RecordPoint-Connectors-SDK-Observability-SeverityLevel-None 'RecordPoint.Connectors.SDK.Observability.SeverityLevel.None')
  - [Verbose](#F-RecordPoint-Connectors-SDK-Observability-SeverityLevel-Verbose 'RecordPoint.Connectors.SDK.Observability.SeverityLevel.Verbose')
  - [Warning](#F-RecordPoint-Connectors-SDK-Observability-SeverityLevel-Warning 'RecordPoint.Connectors.SDK.Observability.SeverityLevel.Warning')
- [StandardDimensions](#T-RecordPoint-Connectors-SDK-Observability-StandardDimensions 'RecordPoint.Connectors.SDK.Observability.StandardDimensions')
  - [ACTION_RESULT_REASON](#F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-ACTION_RESULT_REASON 'RecordPoint.Connectors.SDK.Observability.StandardDimensions.ACTION_RESULT_REASON')
  - [CHANNEL_EXTERNAL_ID](#F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-CHANNEL_EXTERNAL_ID 'RecordPoint.Connectors.SDK.Observability.StandardDimensions.CHANNEL_EXTERNAL_ID')
  - [CHANNEL_TITLE](#F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-CHANNEL_TITLE 'RecordPoint.Connectors.SDK.Observability.StandardDimensions.CHANNEL_TITLE')
  - [COMPANY](#F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-COMPANY 'RecordPoint.Connectors.SDK.Observability.StandardDimensions.COMPANY')
  - [CONNECTOR_ID](#F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-CONNECTOR_ID 'RecordPoint.Connectors.SDK.Observability.StandardDimensions.CONNECTOR_ID')
  - [DECISON](#F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-DECISON 'RecordPoint.Connectors.SDK.Observability.StandardDimensions.DECISON')
  - [DEPENDANCY](#F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-DEPENDANCY 'RecordPoint.Connectors.SDK.Observability.StandardDimensions.DEPENDANCY')
  - [DEPENDANCY_TYPE](#F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-DEPENDANCY_TYPE 'RecordPoint.Connectors.SDK.Observability.StandardDimensions.DEPENDANCY_TYPE')
  - [EVENT_TYPE](#F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-EVENT_TYPE 'RecordPoint.Connectors.SDK.Observability.StandardDimensions.EVENT_TYPE')
  - [EXCEPTION](#F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-EXCEPTION 'RecordPoint.Connectors.SDK.Observability.StandardDimensions.EXCEPTION')
  - [EXTERNAL_ID](#F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-EXTERNAL_ID 'RecordPoint.Connectors.SDK.Observability.StandardDimensions.EXTERNAL_ID')
  - [OUTCOME](#F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-OUTCOME 'RecordPoint.Connectors.SDK.Observability.StandardDimensions.OUTCOME')
  - [OUTCOME_REASON](#F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-OUTCOME_REASON 'RecordPoint.Connectors.SDK.Observability.StandardDimensions.OUTCOME_REASON')
  - [SERVICE](#F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-SERVICE 'RecordPoint.Connectors.SDK.Observability.StandardDimensions.SERVICE')
  - [SERVICE_ID](#F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-SERVICE_ID 'RecordPoint.Connectors.SDK.Observability.StandardDimensions.SERVICE_ID')
  - [SYSTEM](#F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-SYSTEM 'RecordPoint.Connectors.SDK.Observability.StandardDimensions.SYSTEM')
  - [TASK](#F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-TASK 'RecordPoint.Connectors.SDK.Observability.StandardDimensions.TASK')
  - [TENANT_DOMAIN_NAME](#F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-TENANT_DOMAIN_NAME 'RecordPoint.Connectors.SDK.Observability.StandardDimensions.TENANT_DOMAIN_NAME')
  - [TENANT_ID](#F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-TENANT_ID 'RecordPoint.Connectors.SDK.Observability.StandardDimensions.TENANT_ID')
  - [WORK](#F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-WORK 'RecordPoint.Connectors.SDK.Observability.StandardDimensions.WORK')
  - [WORK_COMPLETE](#F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-WORK_COMPLETE 'RecordPoint.Connectors.SDK.Observability.StandardDimensions.WORK_COMPLETE')
  - [WORK_ID](#F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-WORK_ID 'RecordPoint.Connectors.SDK.Observability.StandardDimensions.WORK_ID')
- [StandardMeasures](#T-RecordPoint-Connectors-SDK-Observability-StandardMeasures 'RecordPoint.Connectors.SDK.Observability.StandardMeasures')
  - [ACTION_EXECUTION_SECONDS](#F-RecordPoint-Connectors-SDK-Observability-StandardMeasures-ACTION_EXECUTION_SECONDS 'RecordPoint.Connectors.SDK.Observability.StandardMeasures.ACTION_EXECUTION_SECONDS')
  - [AGGREGATION_COUNT](#F-RecordPoint-Connectors-SDK-Observability-StandardMeasures-AGGREGATION_COUNT 'RecordPoint.Connectors.SDK.Observability.StandardMeasures.AGGREGATION_COUNT')
  - [AUDIT_COUNT](#F-RecordPoint-Connectors-SDK-Observability-StandardMeasures-AUDIT_COUNT 'RecordPoint.Connectors.SDK.Observability.StandardMeasures.AUDIT_COUNT')
  - [BINARY_COUNT](#F-RecordPoint-Connectors-SDK-Observability-StandardMeasures-BINARY_COUNT 'RecordPoint.Connectors.SDK.Observability.StandardMeasures.BINARY_COUNT')
  - [OUTCOME_SECONDS](#F-RecordPoint-Connectors-SDK-Observability-StandardMeasures-OUTCOME_SECONDS 'RecordPoint.Connectors.SDK.Observability.StandardMeasures.OUTCOME_SECONDS')
  - [RECORD_COUNT](#F-RecordPoint-Connectors-SDK-Observability-StandardMeasures-RECORD_COUNT 'RecordPoint.Connectors.SDK.Observability.StandardMeasures.RECORD_COUNT')
  - [SUBMIT_SECONDS](#F-RecordPoint-Connectors-SDK-Observability-StandardMeasures-SUBMIT_SECONDS 'RecordPoint.Connectors.SDK.Observability.StandardMeasures.SUBMIT_SECONDS')
  - [WORK_SECONDS](#F-RecordPoint-Connectors-SDK-Observability-StandardMeasures-WORK_SECONDS 'RecordPoint.Connectors.SDK.Observability.StandardMeasures.WORK_SECONDS')
- [StatusModel](#T-RecordPoint-Connectors-SDK-Status-StatusModel 'RecordPoint.Connectors.SDK.Status.StatusModel')
  - [ConnectorId](#P-RecordPoint-Connectors-SDK-Status-StatusModel-ConnectorId 'RecordPoint.Connectors.SDK.Status.StatusModel.ConnectorId')
  - [Status](#P-RecordPoint-Connectors-SDK-Status-StatusModel-Status 'RecordPoint.Connectors.SDK.Status.StatusModel.Status')
- [SubmissionActionType](#T-RecordPoint-Connectors-SDK-Abstractions-ContentManager-SubmissionActionType 'RecordPoint.Connectors.SDK.Abstractions.ContentManager.SubmissionActionType')
  - [PostSubmit](#F-RecordPoint-Connectors-SDK-Abstractions-ContentManager-SubmissionActionType-PostSubmit 'RecordPoint.Connectors.SDK.Abstractions.ContentManager.SubmissionActionType.PostSubmit')
  - [PreSubmit](#F-RecordPoint-Connectors-SDK-Abstractions-ContentManager-SubmissionActionType-PreSubmit 'RecordPoint.Connectors.SDK.Abstractions.ContentManager.SubmissionActionType.PreSubmit')
- [SystemMemory](#T-RecordPoint-Connectors-SDK-Health-SystemMemory 'RecordPoint.Connectors.SDK.Health.SystemMemory')
  - [FreePhyiscalMemory](#P-RecordPoint-Connectors-SDK-Health-SystemMemory-FreePhyiscalMemory 'RecordPoint.Connectors.SDK.Health.SystemMemory.FreePhyiscalMemory')
  - [FreeVirtualMemory](#P-RecordPoint-Connectors-SDK-Health-SystemMemory-FreeVirtualMemory 'RecordPoint.Connectors.SDK.Health.SystemMemory.FreeVirtualMemory')
  - [TotalPhysicalMemorySize](#P-RecordPoint-Connectors-SDK-Health-SystemMemory-TotalPhysicalMemorySize 'RecordPoint.Connectors.SDK.Health.SystemMemory.TotalPhysicalMemorySize')
  - [TotalVirtualMemorySize](#P-RecordPoint-Connectors-SDK-Health-SystemMemory-TotalVirtualMemorySize 'RecordPoint.Connectors.SDK.Health.SystemMemory.TotalVirtualMemorySize')
- [SystemOptions](#T-RecordPoint-Connectors-SDK-Context-SystemOptions 'RecordPoint.Connectors.SDK.Context.SystemOptions')
  - [SECTION_NAME](#F-RecordPoint-Connectors-SDK-Context-SystemOptions-SECTION_NAME 'RecordPoint.Connectors.SDK.Context.SystemOptions.SECTION_NAME')
  - [CompanyName](#P-RecordPoint-Connectors-SDK-Context-SystemOptions-CompanyName 'RecordPoint.Connectors.SDK.Context.SystemOptions.CompanyName')
  - [ConnectorName](#P-RecordPoint-Connectors-SDK-Context-SystemOptions-ConnectorName 'RecordPoint.Connectors.SDK.Context.SystemOptions.ConnectorName')
  - [DataPathRoot](#P-RecordPoint-Connectors-SDK-Context-SystemOptions-DataPathRoot 'RecordPoint.Connectors.SDK.Context.SystemOptions.DataPathRoot')
  - [ServiceName](#P-RecordPoint-Connectors-SDK-Context-SystemOptions-ServiceName 'RecordPoint.Connectors.SDK.Context.SystemOptions.ServiceName')
  - [ShortName](#P-RecordPoint-Connectors-SDK-Context-SystemOptions-ShortName 'RecordPoint.Connectors.SDK.Context.SystemOptions.ShortName')
- [UnknownWorkRequestException](#T-RecordPoint-Connectors-SDK-Work-UnknownWorkRequestException 'RecordPoint.Connectors.SDK.Work.UnknownWorkRequestException')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Work-UnknownWorkRequestException-#ctor-System-Exception- 'RecordPoint.Connectors.SDK.Work.UnknownWorkRequestException.#ctor(System.Exception)')
- [WorkRequest](#T-RecordPoint-Connectors-SDK-Work-WorkRequest 'RecordPoint.Connectors.SDK.Work.WorkRequest')
  - [Body](#P-RecordPoint-Connectors-SDK-Work-WorkRequest-Body 'RecordPoint.Connectors.SDK.Work.WorkRequest.Body')
  - [ConnectorConfigId](#P-RecordPoint-Connectors-SDK-Work-WorkRequest-ConnectorConfigId 'RecordPoint.Connectors.SDK.Work.WorkRequest.ConnectorConfigId')
  - [FaultedCount](#P-RecordPoint-Connectors-SDK-Work-WorkRequest-FaultedCount 'RecordPoint.Connectors.SDK.Work.WorkRequest.FaultedCount')
  - [MustFinishDateTime](#P-RecordPoint-Connectors-SDK-Work-WorkRequest-MustFinishDateTime 'RecordPoint.Connectors.SDK.Work.WorkRequest.MustFinishDateTime')
  - [SubmitDateTime](#P-RecordPoint-Connectors-SDK-Work-WorkRequest-SubmitDateTime 'RecordPoint.Connectors.SDK.Work.WorkRequest.SubmitDateTime')
  - [TenantDomainName](#P-RecordPoint-Connectors-SDK-Work-WorkRequest-TenantDomainName 'RecordPoint.Connectors.SDK.Work.WorkRequest.TenantDomainName')
  - [TenantId](#P-RecordPoint-Connectors-SDK-Work-WorkRequest-TenantId 'RecordPoint.Connectors.SDK.Work.WorkRequest.TenantId')
  - [WaitTill](#P-RecordPoint-Connectors-SDK-Work-WorkRequest-WaitTill 'RecordPoint.Connectors.SDK.Work.WorkRequest.WaitTill')
  - [WorkId](#P-RecordPoint-Connectors-SDK-Work-WorkRequest-WorkId 'RecordPoint.Connectors.SDK.Work.WorkRequest.WorkId')
  - [WorkType](#P-RecordPoint-Connectors-SDK-Work-WorkRequest-WorkType 'RecordPoint.Connectors.SDK.Work.WorkRequest.WorkType')
- [WorkResult](#T-RecordPoint-Connectors-SDK-Work-WorkResult 'RecordPoint.Connectors.SDK.Work.WorkResult')
  - [Duration](#P-RecordPoint-Connectors-SDK-Work-WorkResult-Duration 'RecordPoint.Connectors.SDK.Work.WorkResult.Duration')
  - [Exception](#P-RecordPoint-Connectors-SDK-Work-WorkResult-Exception 'RecordPoint.Connectors.SDK.Work.WorkResult.Exception')
  - [Measures](#P-RecordPoint-Connectors-SDK-Work-WorkResult-Measures 'RecordPoint.Connectors.SDK.Work.WorkResult.Measures')
  - [Reason](#P-RecordPoint-Connectors-SDK-Work-WorkResult-Reason 'RecordPoint.Connectors.SDK.Work.WorkResult.Reason')
  - [ResultType](#P-RecordPoint-Connectors-SDK-Work-WorkResult-ResultType 'RecordPoint.Connectors.SDK.Work.WorkResult.ResultType')
  - [WaitTill](#P-RecordPoint-Connectors-SDK-Work-WorkResult-WaitTill 'RecordPoint.Connectors.SDK.Work.WorkResult.WaitTill')
  - [Abandoned()](#M-RecordPoint-Connectors-SDK-Work-WorkResult-Abandoned-System-String- 'RecordPoint.Connectors.SDK.Work.WorkResult.Abandoned(System.String)')
  - [Abandoned()](#M-RecordPoint-Connectors-SDK-Work-WorkResult-Abandoned-System-String,System-Exception- 'RecordPoint.Connectors.SDK.Work.WorkResult.Abandoned(System.String,System.Exception)')
  - [Complete()](#M-RecordPoint-Connectors-SDK-Work-WorkResult-Complete-System-String- 'RecordPoint.Connectors.SDK.Work.WorkResult.Complete(System.String)')
  - [DeadLetter()](#M-RecordPoint-Connectors-SDK-Work-WorkResult-DeadLetter-System-String,System-Exception- 'RecordPoint.Connectors.SDK.Work.WorkResult.DeadLetter(System.String,System.Exception)')
  - [Defer()](#M-RecordPoint-Connectors-SDK-Work-WorkResult-Defer-System-String,System-Nullable{System-DateTimeOffset}- 'RecordPoint.Connectors.SDK.Work.WorkResult.Defer(System.String,System.Nullable{System.DateTimeOffset})')
  - [Failed()](#M-RecordPoint-Connectors-SDK-Work-WorkResult-Failed-System-String- 'RecordPoint.Connectors.SDK.Work.WorkResult.Failed(System.String)')
  - [Failed()](#M-RecordPoint-Connectors-SDK-Work-WorkResult-Failed-System-Exception- 'RecordPoint.Connectors.SDK.Work.WorkResult.Failed(System.Exception)')
  - [Failed()](#M-RecordPoint-Connectors-SDK-Work-WorkResult-Failed-System-String,System-Exception- 'RecordPoint.Connectors.SDK.Work.WorkResult.Failed(System.String,System.Exception)')
- [WorkResultException](#T-RecordPoint-Connectors-SDK-Abstractions-Work-WorkResultException 'RecordPoint.Connectors.SDK.Abstractions.Work.WorkResultException')
  - [#ctor(message)](#M-RecordPoint-Connectors-SDK-Abstractions-Work-WorkResultException-#ctor-System-String- 'RecordPoint.Connectors.SDK.Abstractions.Work.WorkResultException.#ctor(System.String)')
  - [#ctor(message,innerException)](#M-RecordPoint-Connectors-SDK-Abstractions-Work-WorkResultException-#ctor-System-String,System-Exception- 'RecordPoint.Connectors.SDK.Abstractions.Work.WorkResultException.#ctor(System.String,System.Exception)')
- [WorkResultType](#T-RecordPoint-Connectors-SDK-Work-WorkResultType 'RecordPoint.Connectors.SDK.Work.WorkResultType')
  - [Abandoned](#F-RecordPoint-Connectors-SDK-Work-WorkResultType-Abandoned 'RecordPoint.Connectors.SDK.Work.WorkResultType.Abandoned')
  - [Complete](#F-RecordPoint-Connectors-SDK-Work-WorkResultType-Complete 'RecordPoint.Connectors.SDK.Work.WorkResultType.Complete')
  - [DeadLetter](#F-RecordPoint-Connectors-SDK-Work-WorkResultType-DeadLetter 'RecordPoint.Connectors.SDK.Work.WorkResultType.DeadLetter')
  - [Deferred](#F-RecordPoint-Connectors-SDK-Work-WorkResultType-Deferred 'RecordPoint.Connectors.SDK.Work.WorkResultType.Deferred')
  - [Failed](#F-RecordPoint-Connectors-SDK-Work-WorkResultType-Failed 'RecordPoint.Connectors.SDK.Work.WorkResultType.Failed')

<a name='T-RecordPoint-Connectors-SDK-ContentManager-ActionResultBase'></a>
## ActionResultBase `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary



<a name='P-RecordPoint-Connectors-SDK-ContentManager-ActionResultBase-Dimensions'></a>
### Dimensions `property`

##### Summary

Observability Dimensions

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ActionResultBase-Measures'></a>
### Measures `property`

##### Summary

Observability Measures

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ActionResultBase-NextDelay'></a>
### NextDelay `property`

##### Summary

The number of seconds to delay the next execution of the Channel Discovery
Also specifies the number of seconds to set a semaphore lock when a BackOff result is returned

##### Remarks

When null, the operation will use the default delay configuration

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ActionResultBase-SemaphoreLockType'></a>
### SemaphoreLockType `property`

##### Summary

When the action requests a backoff due to Content Source throttling, this specifies how the semphore should lock
other requests to the content source

<a name='T-RecordPoint-Connectors-SDK-Content-Aggregation'></a>
## Aggregation `type`

##### Namespace

RecordPoint.Connectors.SDK.Content

##### Summary

Aggreation

<a name='F-RecordPoint-Connectors-SDK-Content-Aggregation-ItemTypeId'></a>
### ItemTypeId `constants`

##### Summary

Aggregation item type

<a name='P-RecordPoint-Connectors-SDK-Content-Aggregation-Location'></a>
### Location `property`

##### Summary

Aggregation location

<a name='P-RecordPoint-Connectors-SDK-Content-Aggregation-ParentExternalId'></a>
### ParentExternalId `property`

##### Summary

Parent External Id of the Aggregation

<a name='M-RecordPoint-Connectors-SDK-Content-Aggregation-Equals-RecordPoint-Connectors-SDK-Content-Aggregation-'></a>
### Equals(other) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| other | [RecordPoint.Connectors.SDK.Content.Aggregation](#T-RecordPoint-Connectors-SDK-Content-Aggregation 'RecordPoint.Connectors.SDK.Content.Aggregation') |  |

<a name='M-RecordPoint-Connectors-SDK-Content-Aggregation-Equals-System-Object-'></a>
### Equals(obj) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| obj | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |

<a name='M-RecordPoint-Connectors-SDK-Content-Aggregation-GetHashCode'></a>
### GetHashCode() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Content-AggregationExtentions'></a>
## AggregationExtentions `type`

##### Namespace

RecordPoint.Connectors.SDK.Content

##### Summary

The aggregation extentions.

<a name='M-RecordPoint-Connectors-SDK-Content-AggregationExtentions-ToAggregation-RecordPoint-Connectors-SDK-Content-AggregationModel-'></a>
### ToAggregation(aggregationModel) `method`

##### Summary

Converts to the aggregation.

##### Returns

An Aggregation

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aggregationModel | [RecordPoint.Connectors.SDK.Content.AggregationModel](#T-RecordPoint-Connectors-SDK-Content-AggregationModel 'RecordPoint.Connectors.SDK.Content.AggregationModel') | The aggregation model. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') |  |
| [RecordPoint.Connectors.SDK.RequiredValueNullException](#T-RecordPoint-Connectors-SDK-RequiredValueNullException 'RecordPoint.Connectors.SDK.RequiredValueNullException') |  |

<a name='M-RecordPoint-Connectors-SDK-Content-AggregationExtentions-ToAggregationList-System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Content-AggregationModel}-'></a>
### ToAggregationList(aggregationModels) `method`

##### Summary

Converts to aggregation list.

##### Returns

List<Aggregation>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aggregationModels | [System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Content.AggregationModel}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Content.AggregationModel}') | The aggregation models. |

<a name='M-RecordPoint-Connectors-SDK-Content-AggregationExtentions-ToAggregationModel-RecordPoint-Connectors-SDK-Content-Aggregation-'></a>
### ToAggregationModel(aggregation) `method`

##### Summary

Converts to aggregation model.

##### Returns

An AggregationModel

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aggregation | [RecordPoint.Connectors.SDK.Content.Aggregation](#T-RecordPoint-Connectors-SDK-Content-Aggregation 'RecordPoint.Connectors.SDK.Content.Aggregation') | The aggregation. |

<a name='M-RecordPoint-Connectors-SDK-Content-AggregationExtentions-ToAggregationModelList-System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Content-Aggregation}-'></a>
### ToAggregationModelList(aggregations) `method`

##### Summary

Converts to aggregation model list.

##### Returns

List<AggregationModel>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aggregations | [System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Content.Aggregation}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Content.Aggregation}') | The aggregations. |

<a name='T-RecordPoint-Connectors-SDK-Content-AggregationModel'></a>
## AggregationModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Content

##### Summary

Aggreation

<a name='P-RecordPoint-Connectors-SDK-Content-AggregationModel-ConnectorId'></a>
### ConnectorId `property`

##### Summary

Connector Configuration that this Aggregation belongs to

<a name='P-RecordPoint-Connectors-SDK-Content-AggregationModel-CreatedDate'></a>
### CreatedDate `property`

##### Summary

The date/time the Aggregation was created

<a name='P-RecordPoint-Connectors-SDK-Content-AggregationModel-ExternalId'></a>
### ExternalId `property`

##### Summary

External ID that uniquely identifies the Aggregation for a connector instance

<a name='P-RecordPoint-Connectors-SDK-Content-AggregationModel-Location'></a>
### Location `property`

##### Summary

Aggregation location

<a name='P-RecordPoint-Connectors-SDK-Content-AggregationModel-MetaData'></a>
### MetaData `property`

##### Summary

Serialized Aggregation Meta Data

<a name='P-RecordPoint-Connectors-SDK-Content-AggregationModel-ParentExternalId'></a>
### ParentExternalId `property`

##### Summary

Parent External ID that uniquely identifies the Parent Aggregation

<a name='P-RecordPoint-Connectors-SDK-Content-AggregationModel-Title'></a>
### Title `property`

##### Summary

Aggregation title

<a name='M-RecordPoint-Connectors-SDK-Content-AggregationModel-Equals-RecordPoint-Connectors-SDK-Content-AggregationModel-'></a>
### Equals(other) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| other | [RecordPoint.Connectors.SDK.Content.AggregationModel](#T-RecordPoint-Connectors-SDK-Content-AggregationModel 'RecordPoint.Connectors.SDK.Content.AggregationModel') |  |

<a name='M-RecordPoint-Connectors-SDK-Content-AggregationModel-Equals-System-Object-'></a>
### Equals(obj) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| obj | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |

<a name='M-RecordPoint-Connectors-SDK-Content-AggregationModel-GetHashCode'></a>
### GetHashCode() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightOptions'></a>
## ApplicationInsightOptions `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability.AppInsights

##### Summary

Configurable Options for Application Insights

<a name='F-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightOptions-OPTION_NAME'></a>
### OPTION_NAME `constants`

##### Summary

The configuration section name

<a name='P-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightOptions-ConnectionString'></a>
### ConnectionString `property`

##### Summary

The Connection String used to connect to the Application Insights instance

<a name='P-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightOptions-IncludeKubernetesEnricher'></a>
### IncludeKubernetesEnricher `property`

##### Summary

Whether the Kubernetes enricher should be should be registered

<a name='P-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightOptions-InstrumentationKey'></a>
### InstrumentationKey `property`

##### Summary

The Instrumentation Key used to connect to the Application Insights instance

<a name='P-RecordPoint-Connectors-SDK-Observability-AppInsights-ApplicationInsightOptions-LogLevel'></a>
### LogLevel `property`

##### Summary

The minimum severity level to log traces to Application Insights

<a name='T-RecordPoint-Connectors-SDK-Content-AuditEvent'></a>
## AuditEvent `type`

##### Namespace

RecordPoint.Connectors.SDK.Content

##### Summary

The audit event.

<a name='P-RecordPoint-Connectors-SDK-Content-AuditEvent-CreatedDate'></a>
### CreatedDate `property`

##### Summary

Gets or sets the created date.

<a name='P-RecordPoint-Connectors-SDK-Content-AuditEvent-Description'></a>
### Description `property`

##### Summary

Gets or sets the description.

<a name='P-RecordPoint-Connectors-SDK-Content-AuditEvent-EventExternalId'></a>
### EventExternalId `property`

##### Summary

Gets or sets the event external id.

<a name='P-RecordPoint-Connectors-SDK-Content-AuditEvent-EventType'></a>
### EventType `property`

##### Summary

Gets or sets the event type.

<a name='P-RecordPoint-Connectors-SDK-Content-AuditEvent-ExternalId'></a>
### ExternalId `property`

##### Summary

Gets or sets the external id.

<a name='P-RecordPoint-Connectors-SDK-Content-AuditEvent-MetaDataItems'></a>
### MetaDataItems `property`

##### Summary

Additional source fields

<a name='P-RecordPoint-Connectors-SDK-Content-AuditEvent-UserId'></a>
### UserId `property`

##### Summary

Gets or sets the user id.

<a name='P-RecordPoint-Connectors-SDK-Content-AuditEvent-UserName'></a>
### UserName `property`

##### Summary

Gets or sets the user name.

<a name='M-RecordPoint-Connectors-SDK-Content-AuditEvent-Equals-RecordPoint-Connectors-SDK-Content-AuditEvent-'></a>
### Equals(other) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| other | [RecordPoint.Connectors.SDK.Content.AuditEvent](#T-RecordPoint-Connectors-SDK-Content-AuditEvent 'RecordPoint.Connectors.SDK.Content.AuditEvent') |  |

<a name='M-RecordPoint-Connectors-SDK-Content-AuditEvent-Equals-System-Object-'></a>
### Equals(obj) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| obj | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |

<a name='M-RecordPoint-Connectors-SDK-Content-AuditEvent-GetHashCode'></a>
### GetHashCode() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Configuration-AzureAuthenticationOptions'></a>
## AzureAuthenticationOptions `type`

##### Namespace

RecordPoint.Connectors.SDK.Configuration

##### Summary

Azure Authentication Options

<a name='F-RecordPoint-Connectors-SDK-Configuration-AzureAuthenticationOptions-SECTION_NAME'></a>
### SECTION_NAME `constants`

##### Summary

Section name for Azure Authentication Options

<a name='P-RecordPoint-Connectors-SDK-Configuration-AzureAuthenticationOptions-ClientId'></a>
### ClientId `property`

##### Summary

Client id

<a name='P-RecordPoint-Connectors-SDK-Configuration-AzureAuthenticationOptions-ClientSecret'></a>
### ClientSecret `property`

##### Summary

Client Secret

<a name='P-RecordPoint-Connectors-SDK-Configuration-AzureAuthenticationOptions-TenantId'></a>
### TenantId `property`

##### Summary

Tenant id

<a name='P-RecordPoint-Connectors-SDK-Configuration-AzureAuthenticationOptions-UseVsCredentials'></a>
### UseVsCredentials `property`

##### Summary

Setting name to use VS Credentials when connecting to various Azure Resources

<a name='T-RecordPoint-Connectors-SDK-Configuration-AzureAuthenticationOptionsExtensions'></a>
## AzureAuthenticationOptionsExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Configuration

##### Summary

Extenions for the AzureAuthenticationOptions

<a name='M-RecordPoint-Connectors-SDK-Configuration-AzureAuthenticationOptionsExtensions-GetTokenCredential-RecordPoint-Connectors-SDK-Configuration-AzureAuthenticationOptions-'></a>
### GetTokenCredential(options) `method`

##### Summary

Returns a TokenCredential for a given configuration

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| options | [RecordPoint.Connectors.SDK.Configuration.AzureAuthenticationOptions](#T-RecordPoint-Connectors-SDK-Configuration-AzureAuthenticationOptions 'RecordPoint.Connectors.SDK.Configuration.AzureAuthenticationOptions') |  |

<a name='T-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo'></a>
## BinaryMetaInfo `type`

##### Namespace

RecordPoint.Connectors.SDK.Content

##### Summary

Represents meta information about a binary

<a name='P-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo-ContentToken'></a>
### ContentToken `property`

##### Summary

Token that can be used by the content module to locate the content

<a name='P-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo-ContentTokenType'></a>
### ContentTokenType `property`

##### Summary

String that uniquely identifies how the content token is formatted.
Provided and used by content modules and typcially needed to support backwards
compatibility.

<a name='P-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo-FileHash'></a>
### FileHash `property`

##### Summary

Optional MD5 file hash is available

<a name='P-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo-FileName'></a>
### FileName `property`

##### Summary

"File name" for the content.

<a name='P-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo-FileSize'></a>
### FileSize `property`

##### Summary

File Size in bytes of the content

<a name='P-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo-ItemExternalId'></a>
### ItemExternalId `property`

##### Summary

External ID of the record this binary is associated with

<a name='P-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo-MimeType'></a>
### MimeType `property`

##### Summary

Mime Type of the content

<a name='P-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo-SkipEnrichment'></a>
### SkipEnrichment `property`

##### Summary

Flag to skip the enrichment pipeline

<a name='M-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo-Equals-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo-'></a>
### Equals(other) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| other | [RecordPoint.Connectors.SDK.Content.BinaryMetaInfo](#T-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo 'RecordPoint.Connectors.SDK.Content.BinaryMetaInfo') |  |

<a name='M-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo-Equals-System-Object-'></a>
### Equals(obj) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| obj | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |

<a name='M-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo-GetHashCode'></a>
### GetHashCode() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Content-BinaryMetaInfoExtensions'></a>
## BinaryMetaInfoExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Content

##### Summary

The binary meta info extensions.

<a name='M-RecordPoint-Connectors-SDK-Content-BinaryMetaInfoExtensions-IsEqual-System-Collections-Generic-List{RecordPoint-Connectors-SDK-Content-BinaryMetaInfo},System-Collections-Generic-List{RecordPoint-Connectors-SDK-Content-BinaryMetaInfo}-'></a>
### IsEqual(x,y) `method`

##### Summary

Checks if is equal.

##### Returns

A bool

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| x | [System.Collections.Generic.List{RecordPoint.Connectors.SDK.Content.BinaryMetaInfo}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{RecordPoint.Connectors.SDK.Content.BinaryMetaInfo}') | The X. |
| y | [System.Collections.Generic.List{RecordPoint.Connectors.SDK.Content.BinaryMetaInfo}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{RecordPoint.Connectors.SDK.Content.BinaryMetaInfo}') | The Y. |

<a name='T-RecordPoint-Connectors-SDK-ContentManager-BinaryRetrievalResult'></a>
## BinaryRetrievalResult `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

The result of an attempt to retrieve a single piece of content

<a name='P-RecordPoint-Connectors-SDK-ContentManager-BinaryRetrievalResult-Exception'></a>
### Exception `property`

##### Summary

Optional exception that caused the retrieval to fail

<a name='P-RecordPoint-Connectors-SDK-ContentManager-BinaryRetrievalResult-Reason'></a>
### Reason `property`

##### Summary

Result reason

<a name='P-RecordPoint-Connectors-SDK-ContentManager-BinaryRetrievalResult-ResultType'></a>
### ResultType `property`

##### Summary

Result type

<a name='P-RecordPoint-Connectors-SDK-ContentManager-BinaryRetrievalResult-Stream'></a>
### Stream `property`

##### Summary

Binary stream

<a name='T-RecordPoint-Connectors-SDK-ContentManager-BinaryRetrievalResultType'></a>
## BinaryRetrievalResultType `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Possible results of content retrieval operations

<a name='F-RecordPoint-Connectors-SDK-ContentManager-BinaryRetrievalResultType-Abandoned'></a>
### Abandoned `constants`

##### Summary

We decided the content item should not be submitted, should be abandoned

<a name='F-RecordPoint-Connectors-SDK-ContentManager-BinaryRetrievalResultType-BackOff'></a>
### BackOff `constants`

##### Summary

Content Source has throttled requests

<a name='F-RecordPoint-Connectors-SDK-ContentManager-BinaryRetrievalResultType-Complete'></a>
### Complete `constants`

##### Summary

We successfully got the content item requested

<a name='F-RecordPoint-Connectors-SDK-ContentManager-BinaryRetrievalResultType-Deleted'></a>
### Deleted `constants`

##### Summary

The content cannot be obtained because it has been deleted

<a name='F-RecordPoint-Connectors-SDK-ContentManager-BinaryRetrievalResultType-Failed'></a>
### Failed `constants`

##### Summary

We failed to get the content item due to an error

<a name='F-RecordPoint-Connectors-SDK-ContentManager-BinaryRetrievalResultType-ZeroBinary'></a>
### ZeroBinary `constants`

##### Summary

The BinaryStream has a length of zero and can't be written to blob

<a name='T-RecordPoint-Connectors-SDK-Content-Channel'></a>
## Channel `type`

##### Namespace

RecordPoint.Connectors.SDK.Content

##### Summary

Channel meta data

<a name='F-RecordPoint-Connectors-SDK-Content-Channel-NULL_CHANNEL_ID'></a>
### NULL_CHANNEL_ID `constants`

##### Summary

Null Channel ID

<a name='F-RecordPoint-Connectors-SDK-Content-Channel-NULL_CHANNEL_TITLE'></a>
### NULL_CHANNEL_TITLE `constants`

##### Summary

Null Channel Title

<a name='P-RecordPoint-Connectors-SDK-Content-Channel-ExternalId'></a>
### ExternalId `property`

##### Summary

External ID that uniquely identifies the Channel for a connector instance

<a name='P-RecordPoint-Connectors-SDK-Content-Channel-MetaDataItems'></a>
### MetaDataItems `property`

##### Summary

Channel Metadata

<a name='P-RecordPoint-Connectors-SDK-Content-Channel-Title'></a>
### Title `property`

##### Summary

Channel title

<a name='M-RecordPoint-Connectors-SDK-Content-Channel-CreateNullChannel'></a>
### CreateNullChannel() `method`

##### Summary

Get the null Channel.

##### Returns



##### Parameters

This method has no parameters.

##### Remarks

The Channel supplied for content sources that don't
have a "Channel" concept.

<a name='M-RecordPoint-Connectors-SDK-Content-Channel-Equals-RecordPoint-Connectors-SDK-Content-Channel-'></a>
### Equals(other) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| other | [RecordPoint.Connectors.SDK.Content.Channel](#T-RecordPoint-Connectors-SDK-Content-Channel 'RecordPoint.Connectors.SDK.Content.Channel') |  |

<a name='M-RecordPoint-Connectors-SDK-Content-Channel-Equals-System-Object-'></a>
### Equals(obj) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| obj | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |

<a name='M-RecordPoint-Connectors-SDK-Content-Channel-GetHashCode'></a>
### GetHashCode() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryConfiguration'></a>
## ChannelDiscoveryConfiguration `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Channel discovery operation configuration

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryConfiguration-ConfigurationType'></a>
### ConfigurationType `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryConfiguration-ConnectorConfigurationId'></a>
### ConnectorConfigurationId `property`

##### Summary

Id of the Connector we are performing the operation on. Content modules can use this to identify the target content

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryConfiguration-TenantDomainName'></a>
### TenantDomainName `property`

##### Summary

Tenant name

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryConfiguration-TenantId'></a>
### TenantId `property`

##### Summary

Tenant Id

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryConfiguration-Deserialize-System-String,System-String-'></a>
### Deserialize(configurationType,configurationText) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| configurationType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| configurationText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentOutOfRangeException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentOutOfRangeException 'System.ArgumentOutOfRangeException') |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryConfiguration-Serialize'></a>
### Serialize() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperationOptions'></a>
## ChannelDiscoveryOperationOptions `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Configuration settings for Channel Discovery

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperationOptions-SECTION_NAME'></a>
### SECTION_NAME `constants`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryOperationOptions-BatchSize'></a>
### BatchSize `property`

##### Summary

Size of each channel call batch

<a name='T-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult'></a>
## ChannelDiscoveryResult `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Result of a Channel Discovery operation

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult-AuditEvents'></a>
### AuditEvents `property`

##### Summary

Audit Events

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult-Channels'></a>
### Channels `property`

##### Summary

Channels that were discovered or updated.
The Channel Discovery operation will upsert any Channels returned in this list.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult-Exception'></a>
### Exception `property`

##### Summary

Optional exception that caused the operation to fail

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult-NewChannelRegistrations'></a>
### NewChannelRegistrations `property`

##### Summary

New Channel Registrations
The Channel Discovery operation will invoke new Content Registrations for Channels returned in this list.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult-Reason'></a>
### Reason `property`

##### Summary

Result reason

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResult-ResultType'></a>
### ResultType `property`

##### Summary

Work result type

<a name='T-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResultType'></a>
## ChannelDiscoveryResultType `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Channel discovery result type

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResultType-BackOff'></a>
### BackOff `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResultType-Complete'></a>
### Complete `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryResultType-Failed'></a>
### Failed `constants`

##### Summary



<a name='T-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryState'></a>
## ChannelDiscoveryState `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Operational state for a Channel Discovery Operation

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryState-LastBackOffDelaySeconds'></a>
### LastBackOffDelaySeconds `property`

##### Summary

The number of seconds the last execution was delayed by

##### Remarks

Used for exponential backoff

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryState-LatestStateType'></a>
### LatestStateType `property`

##### Summary

Latest version of the sync state type

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryState-Deserialize-System-String,System-String-'></a>
### Deserialize(stateType,stateText) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| stateType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| stateText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ChannelDiscoveryState-Serialize'></a>
### Serialize() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Content-ChannelExtentions'></a>
## ChannelExtentions `type`

##### Namespace

RecordPoint.Connectors.SDK.Content

##### Summary

The channel extentions.

<a name='M-RecordPoint-Connectors-SDK-Content-ChannelExtentions-ToChannel-RecordPoint-Connectors-SDK-Content-ChannelModel-'></a>
### ToChannel(channelModel) `method`

##### Summary

Converts to the channel.

##### Returns

A Channel

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| channelModel | [RecordPoint.Connectors.SDK.Content.ChannelModel](#T-RecordPoint-Connectors-SDK-Content-ChannelModel 'RecordPoint.Connectors.SDK.Content.ChannelModel') | The channel model. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') |  |
| [RecordPoint.Connectors.SDK.RequiredValueNullException](#T-RecordPoint-Connectors-SDK-RequiredValueNullException 'RecordPoint.Connectors.SDK.RequiredValueNullException') |  |

<a name='M-RecordPoint-Connectors-SDK-Content-ChannelExtentions-ToChannelList-System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Content-ChannelModel}-'></a>
### ToChannelList(channelModels) `method`

##### Summary

Converts to channel list.

##### Returns

List<Channel>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| channelModels | [System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Content.ChannelModel}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Content.ChannelModel}') | The channel models. |

<a name='M-RecordPoint-Connectors-SDK-Content-ChannelExtentions-ToChannelModel-RecordPoint-Connectors-SDK-Content-Channel-'></a>
### ToChannelModel(channel) `method`

##### Summary

Converts to channel model.

##### Returns

A ChannelModel

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| channel | [RecordPoint.Connectors.SDK.Content.Channel](#T-RecordPoint-Connectors-SDK-Content-Channel 'RecordPoint.Connectors.SDK.Content.Channel') | The channel. |

<a name='M-RecordPoint-Connectors-SDK-Content-ChannelExtentions-ToChannelModelList-System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Content-Channel}-'></a>
### ToChannelModelList(channels) `method`

##### Summary

Converts to channel model list.

##### Returns

List<ChannelModel>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| channels | [System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Content.Channel}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Content.Channel}') | The channels. |

<a name='T-RecordPoint-Connectors-SDK-Content-ChannelModel'></a>
## ChannelModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Content

##### Summary

Model used to Store Channel information within the Channel Cache

<a name='P-RecordPoint-Connectors-SDK-Content-ChannelModel-ConnectorId'></a>
### ConnectorId `property`

##### Summary

Connector Configuration that this Channel belongs to

<a name='P-RecordPoint-Connectors-SDK-Content-ChannelModel-CreatedDate'></a>
### CreatedDate `property`

##### Summary

The date/time the Channel was created

<a name='P-RecordPoint-Connectors-SDK-Content-ChannelModel-ExternalId'></a>
### ExternalId `property`

##### Summary

External ID that uniquely identifies the Channel for a connector instance

<a name='P-RecordPoint-Connectors-SDK-Content-ChannelModel-MetaData'></a>
### MetaData `property`

##### Summary

Serialized Channel Meta Data

<a name='P-RecordPoint-Connectors-SDK-Content-ChannelModel-Title'></a>
### Title `property`

##### Summary

Channel title

<a name='M-RecordPoint-Connectors-SDK-Content-ChannelModel-Equals-RecordPoint-Connectors-SDK-Content-ChannelModel-'></a>
### Equals(other) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| other | [RecordPoint.Connectors.SDK.Content.ChannelModel](#T-RecordPoint-Connectors-SDK-Content-ChannelModel 'RecordPoint.Connectors.SDK.Content.ChannelModel') |  |

<a name='M-RecordPoint-Connectors-SDK-Content-ChannelModel-Equals-System-Object-'></a>
### Equals(obj) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| obj | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |

<a name='M-RecordPoint-Connectors-SDK-Content-ChannelModel-GetHashCode'></a>
### GetHashCode() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel'></a>
## ConnectorConfigurationModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Connectors

##### Summary

Data model for connector data stored in the database

<a name='P-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel-ConnectorId'></a>
### ConnectorId `property`

##### Summary

Connector Id as supplied from records 365. Usually a guid.

<a name='P-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel-ConnectorTypeId'></a>
### ConnectorTypeId `property`

##### Summary

Connector Type Id as supplied from records 365. Usually a guid.

<a name='P-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel-Data'></a>
### Data `property`

##### Summary

Serialized copy of the entire model supplied by records 365

<a name='P-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel-DisplayName'></a>
### DisplayName `property`

##### Summary

Display name of the connector

<a name='P-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel-ReportLocation'></a>
### ReportLocation `property`

##### Summary

The location that generated reports are uploaded

<a name='P-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel-Status'></a>
### Status `property`

##### Summary

Status of the connector

<a name='P-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel-TenantId'></a>
### TenantId `property`

##### Summary

ID of the records 365 tenant that contains the connector.

<a name='T-RecordPoint-Connectors-SDK-Connectors-ConnectorFeatureStatus'></a>
## ConnectorFeatureStatus `type`

##### Namespace

RecordPoint.Connectors.SDK.Connectors

##### Summary

Feature status

<a name='P-RecordPoint-Connectors-SDK-Connectors-ConnectorFeatureStatus-ConnectorId'></a>
### ConnectorId `property`

##### Summary

Connector Id

<a name='P-RecordPoint-Connectors-SDK-Connectors-ConnectorFeatureStatus-Enabled'></a>
### Enabled `property`

##### Summary

Is the feature enabled

<a name='P-RecordPoint-Connectors-SDK-Connectors-ConnectorFeatureStatus-EnabledReason'></a>
### EnabledReason `property`

##### Summary

Reason why for the enabled state

<a name='P-RecordPoint-Connectors-SDK-Connectors-ConnectorFeatureStatus-FeatureName'></a>
### FeatureName `property`

##### Summary

Feature Name

<a name='T-RecordPoint-Connectors-SDK-Connectors-ConnectorOptions'></a>
## ConnectorOptions `type`

##### Namespace

RecordPoint.Connectors.SDK.Connectors

##### Summary

App-level, connector options

<a name='F-RecordPoint-Connectors-SDK-Connectors-ConnectorOptions-SECTION_NAME'></a>
### SECTION_NAME `constants`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Connectors-ConnectorOptions-BinariesEnabled'></a>
### BinariesEnabled `property`

##### Summary

Is binary submission enabled? Only has an effect if set to false in which case it
overrides any other normal binary submission settings.

<a name='P-RecordPoint-Connectors-SDK-Connectors-ConnectorOptions-BinarySkipThreshold'></a>
### BinarySkipThreshold `property`

##### Summary

The size in MegaBytes that any binary should be skipped for submission to R365.
A negative value will disable the feature and all binaryies are submitted provided BinariesEnabled and SubmissionEnabled are true.

<a name='P-RecordPoint-Connectors-SDK-Connectors-ConnectorOptions-ExponentialRetryDelay'></a>
### ExponentialRetryDelay `property`

##### Summary

Enables an exponential backoff to retry attempts (default: true)

<a name='P-RecordPoint-Connectors-SDK-Connectors-ConnectorOptions-MaxRetries'></a>
### MaxRetries `property`

##### Summary

The maximum number of times a failed work operation should be retried before being sent to the dead letter queue. This can be set to -1 for never being dead lettered (default: 5 times)

<a name='P-RecordPoint-Connectors-SDK-Connectors-ConnectorOptions-MaxRetryDelay'></a>
### MaxRetryDelay `property`

##### Summary

The maximum time a retry delay can be in seconds. This is to stop the exponential delay from getting too long. (default: 1 hour or 3600)

<a name='P-RecordPoint-Connectors-SDK-Connectors-ConnectorOptions-RetryDelay'></a>
### RetryDelay `property`

##### Summary

The amount of time on seconds between retries of a failed work operation (default: 30 seconds)

<a name='P-RecordPoint-Connectors-SDK-Connectors-ConnectorOptions-RetryOnFailure'></a>
### RetryOnFailure `property`

##### Summary

This flag is for connector to support retrying operations when an exception occurs up to the maximum retry count

<a name='P-RecordPoint-Connectors-SDK-Connectors-ConnectorOptions-SubmissionEnabled'></a>
### SubmissionEnabled `property`

##### Summary

Is any sort of submission enabled? Only have an effect if set to false in which case it
overrides any other normal submission settings. Note that the connector still operates and
thus pulls changes from the content source.

<a name='T-RecordPoint-Connectors-SDK-Abstractions-Content-ConnectorSecret'></a>
## ConnectorSecret `type`

##### Namespace

RecordPoint.Connectors.SDK.Abstractions.Content

##### Summary

Model for a Connector Secret

<a name='P-RecordPoint-Connectors-SDK-Abstractions-Content-ConnectorSecret-Field'></a>
### Field `property`

##### Summary

Connectors Secret Property Name

<a name='P-RecordPoint-Connectors-SDK-Abstractions-Content-ConnectorSecret-Value'></a>
### Value `property`

##### Summary

Connector Secrets Value

<a name='T-RecordPoint-Connectors-SDK-Observability-Console-ConsoleLoggingOptions'></a>
## ConsoleLoggingOptions `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability.Console

##### Summary

Configurable Options for Console Logging

<a name='F-RecordPoint-Connectors-SDK-Observability-Console-ConsoleLoggingOptions-OPTION_NAME'></a>
### OPTION_NAME `constants`

##### Summary

The configuration section name

<a name='P-RecordPoint-Connectors-SDK-Observability-Console-ConsoleLoggingOptions-LogLevel'></a>
### LogLevel `property`

##### Summary

The minimum severity level to log traces to the Console

<a name='P-RecordPoint-Connectors-SDK-Observability-Console-ConsoleLoggingOptions-WriteDimensions'></a>
### WriteDimensions `property`

##### Summary

Determines if the logger should write dimensions to the console

<a name='P-RecordPoint-Connectors-SDK-Observability-Console-ConsoleLoggingOptions-WriteMeasures'></a>
### WriteMeasures `property`

##### Summary

Determines if the logger should write measures to the console

<a name='T-RecordPoint-Connectors-SDK-Content-ContentItem'></a>
## ContentItem `type`

##### Namespace

RecordPoint.Connectors.SDK.Content

##### Summary

Base class for Records 365 Content

<a name='P-RecordPoint-Connectors-SDK-Content-ContentItem-Author'></a>
### Author `property`

##### Summary

Contents author

<a name='P-RecordPoint-Connectors-SDK-Content-ContentItem-ContentVersion'></a>
### ContentVersion `property`

##### Summary

String that describes the contents version

<a name='P-RecordPoint-Connectors-SDK-Content-ContentItem-ExternalId'></a>
### ExternalId `property`

##### Summary

External ID that uniquely identifies the item in the content source

<a name='P-RecordPoint-Connectors-SDK-Content-ContentItem-MetaDataItems'></a>
### MetaDataItems `property`

##### Summary

Meta Data for the Content Item

<a name='P-RecordPoint-Connectors-SDK-Content-ContentItem-SourceCreatedBy'></a>
### SourceCreatedBy `property`

##### Summary

Who created by the content

<a name='P-RecordPoint-Connectors-SDK-Content-ContentItem-SourceCreatedDate'></a>
### SourceCreatedDate `property`

##### Summary

When the content was created

<a name='P-RecordPoint-Connectors-SDK-Content-ContentItem-SourceLastModifiedBy'></a>
### SourceLastModifiedBy `property`

##### Summary

Who last modified the content

<a name='P-RecordPoint-Connectors-SDK-Content-ContentItem-SourceLastModifiedDate'></a>
### SourceLastModifiedDate `property`

##### Summary

When the content was last modified

<a name='P-RecordPoint-Connectors-SDK-Content-ContentItem-Title'></a>
### Title `property`

##### Summary

Item title

<a name='M-RecordPoint-Connectors-SDK-Content-ContentItem-Equals-RecordPoint-Connectors-SDK-Content-ContentItem-'></a>
### Equals(other) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| other | [RecordPoint.Connectors.SDK.Content.ContentItem](#T-RecordPoint-Connectors-SDK-Content-ContentItem 'RecordPoint.Connectors.SDK.Content.ContentItem') |  |

<a name='M-RecordPoint-Connectors-SDK-Content-ContentItem-Equals-System-Object-'></a>
### Equals(obj) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| obj | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |

<a name='M-RecordPoint-Connectors-SDK-Content-ContentItem-Equals-RecordPoint-Connectors-SDK-Content-ContentItem,RecordPoint-Connectors-SDK-Content-ContentItem-'></a>
### Equals(x,y) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| x | [RecordPoint.Connectors.SDK.Content.ContentItem](#T-RecordPoint-Connectors-SDK-Content-ContentItem 'RecordPoint.Connectors.SDK.Content.ContentItem') |  |
| y | [RecordPoint.Connectors.SDK.Content.ContentItem](#T-RecordPoint-Connectors-SDK-Content-ContentItem 'RecordPoint.Connectors.SDK.Content.ContentItem') |  |

<a name='M-RecordPoint-Connectors-SDK-Content-ContentItem-GetHashCode'></a>
### GetHashCode() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Content-ContentItem-GetHashCode-RecordPoint-Connectors-SDK-Content-ContentItem-'></a>
### GetHashCode(obj) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| obj | [RecordPoint.Connectors.SDK.Content.ContentItem](#T-RecordPoint-Connectors-SDK-Content-ContentItem 'RecordPoint.Connectors.SDK.Content.ContentItem') |  |

<a name='T-RecordPoint-Connectors-SDK-ContentManager-ContentManagerConfiguration'></a>
## ContentManagerConfiguration `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Content Manager operation configuration

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerConfiguration-ConfigurationType'></a>
### ConfigurationType `property`

##### Summary

Gets the configuration type.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerConfiguration-Deserialize-System-String,System-String-'></a>
### Deserialize(configurationType,configurationText) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| configurationType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| configurationText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentOutOfRangeException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentOutOfRangeException 'System.ArgumentOutOfRangeException') |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerConfiguration-Serialize'></a>
### Serialize() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-ContentManager-ContentManagerObservabilityExtensions'></a>
## ContentManagerObservabilityExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Extension methods and constants for dealing with content manager observability

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerObservabilityExtensions-CHANNEL_COUNT'></a>
### CHANNEL_COUNT `constants`

##### Summary

How many Channels were processed?

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerObservabilityExtensions-CHANNEL_DISCOVERY_OPERATIONS_STARTED_COUNT'></a>
### CHANNEL_DISCOVERY_OPERATIONS_STARTED_COUNT `constants`

##### Summary

How many Channel Discovery Operations were started?

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerObservabilityExtensions-CONNECTOR_COUNT'></a>
### CONNECTOR_COUNT `constants`

##### Summary

How many connectors were processed?

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerObservabilityExtensions-CONTENT_REGISTRATION_OPERATIONS_STARTED_COUNT'></a>
### CONTENT_REGISTRATION_OPERATIONS_STARTED_COUNT `constants`

##### Summary

How many Content Registration Operations were started?

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerObservabilityExtensions-CONTENT_SYNCHRONISATION_OPERATIONS_STARTED_COUNT'></a>
### CONTENT_SYNCHRONISATION_OPERATIONS_STARTED_COUNT `constants`

##### Summary

How many Content Synchronisation Operations were started?

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerObservabilityExtensions-SERVICE_NAME'></a>
### SERVICE_NAME `constants`

##### Summary

Global service name for the component

<a name='T-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperationOptionsBase'></a>
## ContentManagerOperationOptionsBase `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Base Configuration settings for Content Manager Operations

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperationOptionsBase-DelaySeconds'></a>
### DelaySeconds `property`

##### Summary

The typical delay between executions

##### Remarks

Defaults to 60 seconds.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperationOptionsBase-ExponentialBackOff'></a>
### ExponentialBackOff `property`

##### Summary

Increases the delay time exponentially

##### Remarks

Defaults to Enabled

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperationOptionsBase-ImmediateReExecution'></a>
### ImmediateReExecution `property`

##### Summary

Re-execute the operation immediately when the previous execution results in new work

##### Remarks

Defaults to Disabled

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperationOptionsBase-MaxDelaySeconds'></a>
### MaxDelaySeconds `property`

##### Summary

The maximum delay between operation executions when Back off is enabled

##### Remarks

Defaults to 600 seconds (10 minutes)

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOperationOptionsBase-RandomDelay'></a>
### RandomDelay `property`

##### Summary

Adds a random time seed to the delay

##### Remarks

Defaults to Disabled

<a name='T-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions'></a>
## ContentManagerOptions `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary



<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions-SECTION_NAME'></a>
### SECTION_NAME `constants`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions-CleanUpAggregations'></a>
### CleanUpAggregations `property`

##### Summary

Automatically remove Aggregations that belong to Connectors Configurations that have been removed or have been disabled beyond the configured threshold

##### Remarks

Defaults to true

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions-CleanUpChannels'></a>
### CleanUpChannels `property`

##### Summary

Automatically remove Channels that belong to Connectors Configurations that have been removed or have been disabled beyond the configured threshold

##### Remarks

Defaults to true

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions-DelaySeconds'></a>
### DelaySeconds `property`

##### Summary

Number of seconds to delay betwwen Content Manager Operation executions

##### Remarks

The Content Manager Operation ensures Channel Discovery and Content Synchronisation is being performed for all Connector Configurations and discovered Channels.
Defaults to 300 seconds (5 minutes)

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions-MaxAbandonedChannelSynchronisationAge'></a>
### MaxAbandonedChannelSynchronisationAge `property`

##### Summary

The maximum age for Channel Synchronisation Work that has been abandoned due to the Channel no longer existing

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions-MaxAbandonedWorkAge'></a>
### MaxAbandonedWorkAge `property`

##### Summary

The max age for a Abandoned Managed Work Status before it is removed

##### Remarks

Defaults to 300 seconds (5 minutes)

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions-MaxCompletedWorkAge'></a>
### MaxCompletedWorkAge `property`

##### Summary

The max age for a Completed Managed Work Status before it is removed

##### Remarks

Defaults to 300 seconds (5 minutes)

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions-MaxDisabledConnectorAge'></a>
### MaxDisabledConnectorAge `property`

##### Summary

The max age for a disabled connector configuration before all work is abandoned

##### Remarks

Defaults to -1 which means disabled connectors will not have their work abandoned

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions-RemoveAbandonedWork'></a>
### RemoveAbandonedWork `property`

##### Summary

Automatically remove Abandoned Managed Work

##### Remarks

Defaults to true

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerOptions-RemoveCompletedWork'></a>
### RemoveCompletedWork `property`

##### Summary

Automatically remove Completed Managed Work

##### Remarks

Defaults to true

<a name='T-RecordPoint-Connectors-SDK-ContentManager-ContentManagerState'></a>
## ContentManagerState `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Operational state for a Content Manager Operation

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerState-ChannelDiscoveryOperationsStarted'></a>
### ChannelDiscoveryOperationsStarted `property`

##### Summary

The number of Channel Discovery Operations that have been started

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerState-ContentSynchronisationOperationsStarted'></a>
### ContentSynchronisationOperationsStarted `property`

##### Summary

The number of Content Synchronisation Operations that have been started

##### Remarks

This is the number of operations that have been started via correction of the known channels versus running Content Synchronisation Operations
and excludes Content Synchronisation Operations that have been started via the Channel Discovery Operation

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentManagerState-LatestStateType'></a>
### LatestStateType `property`

##### Summary

Latest version of the sync state type

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerState-Deserialize-System-String,System-String-'></a>
### Deserialize(stateType,stateText) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| stateType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| stateText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentManagerState-Serialize'></a>
### Serialize() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationConfiguration'></a>
## ContentRegistrationConfiguration `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Content Registration operation configuration

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationConfiguration-ChannelExternalId'></a>
### ChannelExternalId `property`

##### Summary

External id for the Channel we are syncing content for

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationConfiguration-ChannelTitle'></a>
### ChannelTitle `property`

##### Summary

Channel title

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationConfiguration-ConfigurationType'></a>
### ConfigurationType `property`

##### Summary

Gets the configuration type.

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationConfiguration-Context'></a>
### Context `property`

##### Summary

Context for the Content Registration

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationConfiguration-Deserialize-System-String,System-String-'></a>
### Deserialize(configurationType,configurationText) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| configurationType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| configurationText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentOutOfRangeException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentOutOfRangeException 'System.ArgumentOutOfRangeException') |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationConfiguration-Serialize'></a>
### Serialize() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperationOptions'></a>
## ContentRegistrationOperationOptions `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Configuration settings for Content Registration

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperationOptions-SECTION_NAME'></a>
### SECTION_NAME `constants`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationOperationOptions-MaxFetchBatchSize'></a>
### MaxFetchBatchSize `property`

##### Summary

The maximum number of records to retrieve in a single Content Registration execution

<a name='T-RecordPoint-Connectors-SDK-Abstractions-Content-ContentRegistrationRequest'></a>
## ContentRegistrationRequest `type`

##### Namespace

RecordPoint.Connectors.SDK.Abstractions.Content

##### Summary

Model for a Content Registration Request

<a name='P-RecordPoint-Connectors-SDK-Abstractions-Content-ContentRegistrationRequest-Context'></a>
### Context `property`

##### Summary

The context of a Content Registration Request
e.g. A high level aggregation within a Content Source

<a name='P-RecordPoint-Connectors-SDK-Abstractions-Content-ContentRegistrationRequest-EndDate'></a>
### EndDate `property`

##### Summary

The point in time for which content should be registered up to

<a name='P-RecordPoint-Connectors-SDK-Abstractions-Content-ContentRegistrationRequest-StartDate'></a>
### StartDate `property`

##### Summary

The earliest point in time to register content

<a name='T-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationState'></a>
## ContentRegistrationState `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Operational state for a Content Registration Operation

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationState-Cursor'></a>
### Cursor `property`

##### Summary

Current position in registration

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationState-LastBackOffDelaySeconds'></a>
### LastBackOffDelaySeconds `property`

##### Summary

The number of seconds the last execution was delayed by

##### Remarks

Used for exponential backoff

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationState-LatestStateType'></a>
### LatestStateType `property`

##### Summary

Latest version of the synchronisation state type

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationState-Deserialize-System-String,System-String-'></a>
### Deserialize(stateType,stateText) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| stateType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| stateText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentRegistrationState-Serialize'></a>
### Serialize() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-ContentManager-ContentResult'></a>
## ContentResult `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Outcome of a content operation

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentResult-Aggregations'></a>
### Aggregations `property`

##### Summary

Aggregations

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentResult-AuditEvents'></a>
### AuditEvents `property`

##### Summary

Audit Events

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentResult-Cursor'></a>
### Cursor `property`

##### Summary

Progress cursor. Wil be needed on subsequent operations to get the next section of work

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentResult-Exception'></a>
### Exception `property`

##### Summary

Optional exception that caused the sync to fail

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentResult-Reason'></a>
### Reason `property`

##### Summary

Outcome reason

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentResult-Records'></a>
### Records `property`

##### Summary

Records

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentResult-ResultType'></a>
### ResultType `property`

##### Summary

Work outcome type

<a name='T-RecordPoint-Connectors-SDK-ContentManager-ContentResultType'></a>
## ContentResultType `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

The contents result types.

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentResultType-Abandonded'></a>
### Abandonded `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentResultType-BackOff'></a>
### BackOff `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentResultType-Complete'></a>
### Complete `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentResultType-Failed'></a>
### Failed `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentResultType-Incomplete'></a>
### Incomplete `constants`

##### Summary



<a name='T-RecordPoint-Connectors-SDK-ContentManager-ContentSubmissionConfiguration'></a>
## ContentSubmissionConfiguration `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Configuration for Content Submission Operations

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentSubmissionConfiguration-ConnectorConfigurationId'></a>
### ConnectorConfigurationId `property`

##### Summary

Connector config we are performing the operation on. Content modules can use this to identify the target content

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentSubmissionConfiguration-TenantDomainName'></a>
### TenantDomainName `property`

##### Summary

Tenant domain name

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentSubmissionConfiguration-TenantId'></a>
### TenantId `property`

##### Summary

Tenant Id

<a name='T-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationConfiguration'></a>
## ContentSynchronisationConfiguration `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Content Synchronisation operation configuration

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationConfiguration-ChannelExternalId'></a>
### ChannelExternalId `property`

##### Summary

External id for the Channel we are syncing content for

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationConfiguration-ChannelTitle'></a>
### ChannelTitle `property`

##### Summary

Channel title

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationConfiguration-ConfigurationType'></a>
### ConfigurationType `property`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationConfiguration-Deserialize-System-String,System-String-'></a>
### Deserialize(configurationType,configurationText) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| configurationType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| configurationText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentOutOfRangeException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentOutOfRangeException 'System.ArgumentOutOfRangeException') |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationConfiguration-Serialize'></a>
### Serialize() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperationOptions'></a>
## ContentSynchronisationOperationOptions `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Configuration settings for Content Synchronisation

<a name='F-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperationOptions-SECTION_NAME'></a>
### SECTION_NAME `constants`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationOperationOptions-MaxFetchBatchSize'></a>
### MaxFetchBatchSize `property`

##### Summary

The maximum number of records to retrieve in a single Content Synchronisation execution

<a name='T-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationState'></a>
## ContentSynchronisationState `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Operational state for a Content Synchronisation Operation

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationState-Cursor'></a>
### Cursor `property`

##### Summary

Current position in syncing

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationState-LastBackOffDelaySeconds'></a>
### LastBackOffDelaySeconds `property`

##### Summary

The number of seconds the last execution was delayed by

##### Remarks

Used for exponential backoff

<a name='P-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationState-LatestStateType'></a>
### LatestStateType `property`

##### Summary

Latest version of the synchronisation state type

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationState-Deserialize-System-String,System-String-'></a>
### Deserialize(stateType,stateText) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| stateType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| stateText | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-ContentSynchronisationState-Serialize'></a>
### Serialize() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Work-Models-DeadLetterModel'></a>
## DeadLetterModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Work.Models

##### Summary

DeadLetterModel

<a name='P-RecordPoint-Connectors-SDK-Work-Models-DeadLetterModel-DeadLetterReason'></a>
### DeadLetterReason `property`

##### Summary

DeadLetterReason

<a name='P-RecordPoint-Connectors-SDK-Work-Models-DeadLetterModel-EnqueuedTime'></a>
### EnqueuedTime `property`

##### Summary

EnqueuedTime

<a name='P-RecordPoint-Connectors-SDK-Work-Models-DeadLetterModel-MessageId'></a>
### MessageId `property`

##### Summary

MessageId

<a name='P-RecordPoint-Connectors-SDK-Work-Models-DeadLetterModel-SequenceNumber'></a>
### SequenceNumber `property`

##### Summary

SequenceNumber

<a name='T-RecordPoint-Connectors-SDK-Observability-DependancyType'></a>
## DependancyType `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability

##### Summary

Lists of set of standard dependancy types

<a name='F-RecordPoint-Connectors-SDK-Observability-DependancyType-Database'></a>
### Database `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Observability-DependancyType-Records365'></a>
### Records365 `constants`

##### Summary



<a name='T-RecordPoint-Connectors-SDK-Observability-Dimensions'></a>
## Dimensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability

##### Summary

The dimensions.

<a name='M-RecordPoint-Connectors-SDK-Observability-Dimensions-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the [Dimensions](#T-RecordPoint-Connectors-SDK-Observability-Dimensions 'RecordPoint.Connectors.SDK.Observability.Dimensions') class.

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Observability-Dimensions-#ctor-System-Collections-Generic-IEnumerable{System-Collections-Generic-KeyValuePair{System-String,System-String}}-'></a>
### #ctor(collection) `constructor`

##### Summary

Initializes a new instance of the [Dimensions](#T-RecordPoint-Connectors-SDK-Observability-Dimensions 'RecordPoint.Connectors.SDK.Observability.Dimensions') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| collection | [System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.String,System.String}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.String,System.String}}') | The collection. |

<a name='M-RecordPoint-Connectors-SDK-Observability-Dimensions-ToLogState'></a>
### ToLogState() `method`

##### Summary

Convert dimensions to log scope state suitable for being passed to a logging BeingScope

##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Observability-EventType'></a>
## EventType `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability

##### Summary

Observability event types

<a name='F-RecordPoint-Connectors-SDK-Observability-EventType-Decision'></a>
### Decision `constants`

##### Summary

Major decision completed

<a name='F-RecordPoint-Connectors-SDK-Observability-EventType-Finish'></a>
### Finish `constants`

##### Summary

Unit of work finished

<a name='F-RecordPoint-Connectors-SDK-Observability-EventType-Shutdown'></a>
### Shutdown `constants`

##### Summary

Service shutdown

<a name='F-RecordPoint-Connectors-SDK-Observability-EventType-Start'></a>
### Start `constants`

##### Summary

Unit of work started

<a name='F-RecordPoint-Connectors-SDK-Observability-EventType-Startup'></a>
### Startup `constants`

##### Summary

Service startup

<a name='T-RecordPoint-Connectors-SDK-Observability-FileLogs-FileLogOptions'></a>
## FileLogOptions `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability.FileLogs

##### Summary

The file log options.

<a name='F-RecordPoint-Connectors-SDK-Observability-FileLogs-FileLogOptions-OPTION_NAME'></a>
### OPTION_NAME `constants`

##### Summary

The OPTION NAME.

<a name='P-RecordPoint-Connectors-SDK-Observability-FileLogs-FileLogOptions-FileSizeLimitBytes'></a>
### FileSizeLimitBytes `property`

##### Summary

Gets or sets the file size limit bytes.

<a name='P-RecordPoint-Connectors-SDK-Observability-FileLogs-FileLogOptions-LogPath'></a>
### LogPath `property`

##### Summary

Gets or sets the log path.

<a name='P-RecordPoint-Connectors-SDK-Observability-FileLogs-FileLogOptions-OutputTemplate'></a>
### OutputTemplate `property`

##### Summary

Gets or sets the output template.

<a name='P-RecordPoint-Connectors-SDK-Observability-FileLogs-FileLogOptions-RetainedFileCountLimit'></a>
### RetainedFileCountLimit `property`

##### Summary

Gets or sets the retained file count limit.

<a name='P-RecordPoint-Connectors-SDK-Observability-FileLogs-FileLogOptions-RollOnFileSizeLimit'></a>
### RollOnFileSizeLimit `property`

##### Summary

Gets or sets a value indicating whether roll on file size limit.

<a name='P-RecordPoint-Connectors-SDK-Observability-FileLogs-FileLogOptions-RollingInterval'></a>
### RollingInterval `property`

##### Summary

Gets or sets the rolling interval.

<a name='T-RecordPoint-Connectors-SDK-Health-HealthCheckDimension'></a>
## HealthCheckDimension `type`

##### Namespace

RecordPoint.Connectors.SDK.Health

##### Summary

Health check dimensions

<a name='P-RecordPoint-Connectors-SDK-Health-HealthCheckDimension-ConnectorId'></a>
### ConnectorId `property`

##### Summary

Connector Id

<a name='P-RecordPoint-Connectors-SDK-Health-HealthCheckDimension-Value'></a>
### Value `property`

##### Summary

Dimension Value

<a name='T-RecordPoint-Connectors-SDK-Health-HealthCheckFailedException'></a>
## HealthCheckFailedException `type`

##### Namespace

RecordPoint.Connectors.SDK.Health

##### Summary

The health check failed exception.

<a name='M-RecordPoint-Connectors-SDK-Health-HealthCheckFailedException-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the [HealthCheckFailedException](#T-RecordPoint-Connectors-SDK-Health-HealthCheckFailedException 'RecordPoint.Connectors.SDK.Health.HealthCheckFailedException') class.

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Health-HealthCheckFailedException-#ctor-System-String-'></a>
### #ctor(message) `constructor`

##### Summary

Initializes a new instance of the [HealthCheckFailedException](#T-RecordPoint-Connectors-SDK-Health-HealthCheckFailedException 'RecordPoint.Connectors.SDK.Health.HealthCheckFailedException') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message. |

<a name='M-RecordPoint-Connectors-SDK-Health-HealthCheckFailedException-#ctor-System-String,System-Exception-'></a>
### #ctor(message,inner) `constructor`

##### Summary

Initializes a new instance of the [HealthCheckFailedException](#T-RecordPoint-Connectors-SDK-Health-HealthCheckFailedException 'RecordPoint.Connectors.SDK.Health.HealthCheckFailedException') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message. |
| inner | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | The inner. |

<a name='T-RecordPoint-Connectors-SDK-Health-HealthCheckItem'></a>
## HealthCheckItem `type`

##### Namespace

RecordPoint.Connectors.SDK.Health

##### Summary

Result of a health check

<a name='P-RecordPoint-Connectors-SDK-Health-HealthCheckItem-HealthCheckType'></a>
### HealthCheckType `property`

##### Summary

Health check type

<a name='P-RecordPoint-Connectors-SDK-Health-HealthCheckItem-HealthLevel'></a>
### HealthLevel `property`

##### Summary

Alert level

<a name='P-RecordPoint-Connectors-SDK-Health-HealthCheckItem-Name'></a>
### Name `property`

##### Summary

Should an alert

<a name='T-RecordPoint-Connectors-SDK-Health-HealthCheckMeasure'></a>
## HealthCheckMeasure `type`

##### Namespace

RecordPoint.Connectors.SDK.Health

##### Summary

Health check measure

<a name='P-RecordPoint-Connectors-SDK-Health-HealthCheckMeasure-Value'></a>
### Value `property`

##### Summary

Measure Value

<a name='T-RecordPoint-Connectors-SDK-Health-HealthCheckOptions'></a>
## HealthCheckOptions `type`

##### Namespace

RecordPoint.Connectors.SDK.Health

##### Summary

App-level, health check options

<a name='F-RecordPoint-Connectors-SDK-Health-HealthCheckOptions-SECTION_NAME'></a>
### SECTION_NAME `constants`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Health-HealthCheckOptions-HealthCheckFrequencySeconds'></a>
### HealthCheckFrequencySeconds `property`

##### Summary

Number of seconds to trigger health check.

Defaults to 10 minutes.

<a name='P-RecordPoint-Connectors-SDK-Health-HealthCheckOptions-HealthCheckStartDelaySeconds'></a>
### HealthCheckStartDelaySeconds `property`

##### Summary

Number of seconds to delay for the first health check to let the
initialize itself.

Defaults to 1 minute.

<a name='P-RecordPoint-Connectors-SDK-Health-HealthCheckOptions-MinimumDiskSpaceInGB'></a>
### MinimumDiskSpaceInGB `property`

##### Summary

Number of Gb which is minimum requires for disk space

<a name='T-RecordPoint-Connectors-SDK-Health-HealthCheckResult'></a>
## HealthCheckResult `type`

##### Namespace

RecordPoint.Connectors.SDK.Health

##### Summary

Reports the results of a Health Check

<a name='P-RecordPoint-Connectors-SDK-Health-HealthCheckResult-Dimensions'></a>
### Dimensions `property`

##### Summary

List of health check dimensions

<a name='P-RecordPoint-Connectors-SDK-Health-HealthCheckResult-LastUpdate'></a>
### LastUpdate `property`

##### Summary

The time of the most recent health check

<a name='P-RecordPoint-Connectors-SDK-Health-HealthCheckResult-Measures'></a>
### Measures `property`

##### Summary

List of health check measures

<a name='P-RecordPoint-Connectors-SDK-Health-HealthCheckResult-OverallHealthLevel'></a>
### OverallHealthLevel `property`

##### Summary

Resturns the highest overall health level of all returned dimensions and measures

<a name='T-RecordPoint-Connectors-SDK-Health-HealthLevel'></a>
## HealthLevel `type`

##### Namespace

RecordPoint.Connectors.SDK.Health

##### Summary

Possible health check alert levels

##### Remarks

Health levels int value represent the priority order. The overall level
for a healthcheck will be at the highest alertlevel

<a name='F-RecordPoint-Connectors-SDK-Health-HealthLevel-Failure'></a>
### Failure `constants`

##### Summary

Connector is not operating correctly

<a name='F-RecordPoint-Connectors-SDK-Health-HealthLevel-Normal'></a>
### Normal `constants`

##### Summary

Normal operation

<a name='F-RecordPoint-Connectors-SDK-Health-HealthLevel-Warning'></a>
### Warning `constants`

##### Summary

Warning of possible health issues

<a name='T-RecordPoint-Connectors-SDK-Content-IAggregationManager'></a>
## IAggregationManager `type`

##### Namespace

RecordPoint.Connectors.SDK.Content

##### Summary

Definition for a class that manages Aggregations

<a name='M-RecordPoint-Connectors-SDK-Content-IAggregationManager-AggregationExistsAsync-System-String,System-String,System-Threading-CancellationToken-'></a>
### AggregationExistsAsync(connectorId,externalId,cancellationToken) `method`

##### Summary

Checks if the specified Aggregation exists

##### Returns

True if it exists

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The Connector Configuration for which the Aggregation belongs |
| externalId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | External Id of the Aggregation |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Content-IAggregationManager-GetAggregationAsync-System-String,System-String,System-Threading-CancellationToken-'></a>
### GetAggregationAsync(connectorId,externalId,cancellationToken) `method`

##### Summary

Gets the specified Aggregation

##### Returns

Aggregation if it exists, otherwise null

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The Connector Configuration for which the Aggregation belongs |
| externalId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | External Id of the Aggregation |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Content-IAggregationManager-GetAggregationsAsync-System-Threading-CancellationToken-'></a>
### GetAggregationsAsync(cancellationToken) `method`

##### Summary

Gets all known Aggregations

##### Returns

List of Aggregations across all connectors

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Content-IAggregationManager-GetAggregationsAsync-System-Linq-Expressions-Expression{System-Func{RecordPoint-Connectors-SDK-Content-AggregationModel,System-Boolean}},System-Threading-CancellationToken-'></a>
### GetAggregationsAsync(predicate,cancellationToken) `method`

##### Summary

Gets all known Aggregations

##### Returns

List of Aggregations across all connectors

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| predicate | [System.Linq.Expressions.Expression{System.Func{RecordPoint.Connectors.SDK.Content.AggregationModel,System.Boolean}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Func{RecordPoint.Connectors.SDK.Content.AggregationModel,System.Boolean}}') | A function to test each element for a condition. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Content-IAggregationManager-GetAggregationsAsync-System-String,System-Threading-CancellationToken-'></a>
### GetAggregationsAsync(connectorId,cancellationToken) `method`

##### Summary

Gets all Aggregations for the specified Connector

##### Returns

List of Aggregations for the specified Connector

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The Connector Configuration for which the Aggregations belong |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Content-IAggregationManager-RemoveAggregationAsync-System-String,System-String,System-Threading-CancellationToken-'></a>
### RemoveAggregationAsync(connectorId,externalId,cancellationToken) `method`

##### Summary

Removes a Aggregation from Storage

##### Returns

Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The Connector for which the Aggregation belongs |
| externalId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The Id of the Aggregation |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Content-IAggregationManager-RemoveAggregationsAsync-System-String,System-String[],System-Threading-CancellationToken-'></a>
### RemoveAggregationsAsync(connectorId,externalIds,cancellationToken) `method`

##### Summary

Removes the specified Aggregations from Storage

##### Returns

Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The Connector for which the Aggregation belongs |
| externalIds | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | The Ids of the Aggregations |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Content-IAggregationManager-RemoveAggregationsAsync-System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Content-AggregationModel},System-Threading-CancellationToken-'></a>
### RemoveAggregationsAsync(aggregationModels,cancellationToken) `method`

##### Summary

Removes the specified Aggregations from Storage

##### Returns

Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aggregationModels | [System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Content.AggregationModel}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Content.AggregationModel}') | A list of Aggregation Models to remove |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Content-IAggregationManager-UpsertAggregationAsync-RecordPoint-Connectors-SDK-Content-AggregationModel,System-Threading-CancellationToken-'></a>
### UpsertAggregationAsync(aggregation,cancellationToken) `method`

##### Summary

Adds a Aggregation to Storage or updates the existing Aggregation

##### Returns

Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aggregation | [RecordPoint.Connectors.SDK.Content.AggregationModel](#T-RecordPoint-Connectors-SDK-Content-AggregationModel 'RecordPoint.Connectors.SDK.Content.AggregationModel') | Aggregation to add |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Content-IAggregationManager-UpsertAggregationsAsync-System-Collections-Generic-List{RecordPoint-Connectors-SDK-Content-AggregationModel},System-Threading-CancellationToken-'></a>
### UpsertAggregationsAsync(aggregations,cancellationToken) `method`

##### Summary

Adds the List of Aggregations to Storage or Updates each existing Aggregation

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| aggregations | [System.Collections.Generic.List{RecordPoint.Connectors.SDK.Content.AggregationModel}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{RecordPoint.Connectors.SDK.Content.AggregationModel}') | The Aggregations to add |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='T-RecordPoint-Connectors-SDK-ContentManager-IAggregationSubmissionCallbackAction'></a>
## IAggregationSubmissionCallbackAction `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Defines an aggregation submission callback which is executed after a successfull aggregation submission

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IAggregationSubmissionCallbackAction-ExecuteAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Aggregation,RecordPoint-Connectors-SDK-Abstractions-ContentManager-SubmissionActionType,System-Threading-CancellationToken-'></a>
### ExecuteAsync(connectorConfiguration,aggregation,submissionActionType,cancellationToken) `method`

##### Summary

Executes after a successfull aggregation submission operation

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfiguration | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | The connector configuration |
| aggregation | [RecordPoint.Connectors.SDK.Content.Aggregation](#T-RecordPoint-Connectors-SDK-Content-Aggregation 'RecordPoint.Connectors.SDK.Content.Aggregation') | Meta info for the aggregation that was submitted |
| submissionActionType | [RecordPoint.Connectors.SDK.Abstractions.ContentManager.SubmissionActionType](#T-RecordPoint-Connectors-SDK-Abstractions-ContentManager-SubmissionActionType 'RecordPoint.Connectors.SDK.Abstractions.ContentManager.SubmissionActionType') | Indicates if the action is being invoked pre-submit or post-submit |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='T-RecordPoint-Connectors-SDK-ContentManager-IAuditEventSubmissionCallbackAction'></a>
## IAuditEventSubmissionCallbackAction `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Defines an audit event submission callback which is executed after a successfull audit event submission

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IAuditEventSubmissionCallbackAction-ExecuteAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-AuditEvent,RecordPoint-Connectors-SDK-Abstractions-ContentManager-SubmissionActionType,System-Threading-CancellationToken-'></a>
### ExecuteAsync(connectorConfiguration,auditEvent,submissionActionType,cancellationToken) `method`

##### Summary

Executes after a successfull audit event submission operation

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfiguration | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | The connector configuration |
| auditEvent | [RecordPoint.Connectors.SDK.Content.AuditEvent](#T-RecordPoint-Connectors-SDK-Content-AuditEvent 'RecordPoint.Connectors.SDK.Content.AuditEvent') | Meta info for the audit event that was submitted |
| submissionActionType | [RecordPoint.Connectors.SDK.Abstractions.ContentManager.SubmissionActionType](#T-RecordPoint-Connectors-SDK-Abstractions-ContentManager-SubmissionActionType 'RecordPoint.Connectors.SDK.Abstractions.ContentManager.SubmissionActionType') | Indicates if the action is being invoked pre-submit or post-submit |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='T-RecordPoint-Connectors-SDK-ContentManager-IBinaryRetrievalAction'></a>
## IBinaryRetrievalAction `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Defines a binary retriever operation which is responsible for obtaining a stram to a binary within the Content Source.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IBinaryRetrievalAction-ExecuteAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-BinaryMetaInfo,System-Threading-CancellationToken-'></a>
### ExecuteAsync(connectorConfiguration,binaryMetaInfo,cancellationToken) `method`

##### Summary

Gets a stream to corresponding to the binary meta info

##### Returns

Get binary outcome

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfiguration | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | The connector configuration |
| binaryMetaInfo | [RecordPoint.Connectors.SDK.Content.BinaryMetaInfo](#T-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo 'RecordPoint.Connectors.SDK.Content.BinaryMetaInfo') | Meta info to get stream for |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='T-RecordPoint-Connectors-SDK-ContentManager-IBinarySubmissionCallbackAction'></a>
## IBinarySubmissionCallbackAction `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Defines a binary submission callback which is executed after a successfull binary submission

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IBinarySubmissionCallbackAction-ExecuteAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-BinaryMetaInfo,RecordPoint-Connectors-SDK-Abstractions-ContentManager-SubmissionActionType,System-Threading-CancellationToken-'></a>
### ExecuteAsync(connectorConfiguration,binaryMetaInfo,submissionActionType,cancellationToken) `method`

##### Summary

Executes after a successfull binary submission operation

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfiguration | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | The connector configuration |
| binaryMetaInfo | [RecordPoint.Connectors.SDK.Content.BinaryMetaInfo](#T-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo 'RecordPoint.Connectors.SDK.Content.BinaryMetaInfo') | Meta info for the binary that was submitted |
| submissionActionType | [RecordPoint.Connectors.SDK.Abstractions.ContentManager.SubmissionActionType](#T-RecordPoint-Connectors-SDK-Abstractions-ContentManager-SubmissionActionType 'RecordPoint.Connectors.SDK.Abstractions.ContentManager.SubmissionActionType') | Indicates if the action is being invoked pre-submit or post-submit |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='T-RecordPoint-Connectors-SDK-ContentManager-IChannelDiscoveryAction'></a>
## IChannelDiscoveryAction `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Defines a Channel discovery action, which is responsible for discovering Channels within the Content Source
available for a content source

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IChannelDiscoveryAction-ExecuteAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,System-Threading-CancellationToken-'></a>
### ExecuteAsync(connectorConfiguration,cancellationToken) `method`

##### Summary

Executes the channel discovery logic

##### Returns

Result of the scan

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfiguration | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | The connector configuration |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='T-RecordPoint-Connectors-SDK-Content-IChannelManager'></a>
## IChannelManager `type`

##### Namespace

RecordPoint.Connectors.SDK.Content

##### Summary

Definition for a class that manages Channels

<a name='M-RecordPoint-Connectors-SDK-Content-IChannelManager-ChannelExistsAsync-System-String,System-String,System-Threading-CancellationToken-'></a>
### ChannelExistsAsync(connectorId,externalId,cancellationToken) `method`

##### Summary

Checks if the specified Channel exists

##### Returns

True if it exists

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The Connector Configuration for which the Channel belongs |
| externalId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | External Id of the Channel |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Content-IChannelManager-GetChannelAsync-System-String,System-String,System-Threading-CancellationToken-'></a>
### GetChannelAsync(connectorId,externalId,cancellationToken) `method`

##### Summary

Gets the specified Channel

##### Returns

Channel if it exists, otherwise null

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The Connector Configuration for which the Channel belongs |
| externalId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | External Id of the Channel |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Content-IChannelManager-GetChannelsAsync-System-Threading-CancellationToken-'></a>
### GetChannelsAsync(cancellationToken) `method`

##### Summary

Gets all known Channels

##### Returns

List of Channels across all connectors

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Content-IChannelManager-GetChannelsAsync-System-Linq-Expressions-Expression{System-Func{RecordPoint-Connectors-SDK-Content-ChannelModel,System-Boolean}},System-Threading-CancellationToken-'></a>
### GetChannelsAsync(predicate,cancellationToken) `method`

##### Summary

Gets all known Channels

##### Returns

List of Channels across all connectors

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| predicate | [System.Linq.Expressions.Expression{System.Func{RecordPoint.Connectors.SDK.Content.ChannelModel,System.Boolean}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Func{RecordPoint.Connectors.SDK.Content.ChannelModel,System.Boolean}}') | A function to test each element for a condition. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Content-IChannelManager-GetChannelsAsync-System-String,System-Threading-CancellationToken-'></a>
### GetChannelsAsync(connectorId,cancellationToken) `method`

##### Summary

Gets all Channels for the specified Connector

##### Returns

List of Channels for the specified Connector

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The Connector Configuration for which the Channels belong |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Content-IChannelManager-RemoveChannelAsync-System-String,System-String,System-Threading-CancellationToken-'></a>
### RemoveChannelAsync(connectorId,externalId,cancellationToken) `method`

##### Summary

Removes a Channel from Storage

##### Returns

Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The Connector for which the Channel belongs |
| externalId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The Id of the Channel |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Content-IChannelManager-RemoveChannelsAsync-System-String,System-String[],System-Threading-CancellationToken-'></a>
### RemoveChannelsAsync(connectorId,externalIds,cancellationToken) `method`

##### Summary

Removes the specified Channels from Storage

##### Returns

Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The Connector for which the Channel belongs |
| externalIds | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | The Ids of the Channels |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Content-IChannelManager-RemoveChannelsAsync-System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Content-ChannelModel},System-Threading-CancellationToken-'></a>
### RemoveChannelsAsync(channelModels,cancellationToken) `method`

##### Summary

Removes the specified Channels from Storage

##### Returns

Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| channelModels | [System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Content.ChannelModel}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Content.ChannelModel}') | A list of Channel Models to remove |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Content-IChannelManager-UpsertChannelAsync-RecordPoint-Connectors-SDK-Content-ChannelModel,System-Threading-CancellationToken-'></a>
### UpsertChannelAsync(channel,cancellationToken) `method`

##### Summary

Adds a Channel to Storage or updates the existing Channel

##### Returns

Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| channel | [RecordPoint.Connectors.SDK.Content.ChannelModel](#T-RecordPoint-Connectors-SDK-Content-ChannelModel 'RecordPoint.Connectors.SDK.Content.ChannelModel') | Channel to add |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Content-IChannelManager-UpsertChannelsAsync-System-Collections-Generic-List{RecordPoint-Connectors-SDK-Content-ChannelModel},System-Threading-CancellationToken-'></a>
### UpsertChannelsAsync(channels,cancellationToken) `method`

##### Summary

Adds the List of Channels to Storage or Updates each existing Channel

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| channels | [System.Collections.Generic.List{RecordPoint.Connectors.SDK.Content.ChannelModel}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{RecordPoint.Connectors.SDK.Content.ChannelModel}') | The Channels to add |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='T-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager'></a>
## IConnectorConfigurationManager `type`

##### Namespace

RecordPoint.Connectors.SDK.Connectors

##### Summary

Defines the connector configuration manager which is used to access stored connector configurations

<a name='M-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager-ConnectorConfigurationExistsAsync-System-String,System-Threading-CancellationToken-'></a>
### ConnectorConfigurationExistsAsync(connectorId,cancellationToken) `method`

##### Summary

Does a connector configuration exist?

##### Returns

True if the secret exists, otherwise false

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Connector Configuration Id |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager-DeleteConnectorConfigurationAsync-System-String,System-Threading-CancellationToken-'></a>
### DeleteConnectorConfigurationAsync(connectorId,cancellationToken) `method`

##### Summary

Delete connector configuration

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Id of connector configuration to delete |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager-GetAllConnectorConfigurationsAsync-System-Threading-CancellationToken-'></a>
### GetAllConnectorConfigurationsAsync() `method`

##### Summary

Get all connector configurations

##### Returns

List of Connector Configuration models

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager-GetBinarySubmissionStatusAsync-System-String,System-Threading-CancellationToken-'></a>
### GetBinarySubmissionStatusAsync(connectorId,cancellationToken) `method`

##### Summary

Get the binary submission feature status

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | ID of the connector to get the status for |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager-GetConnectorConfigurationAsync-System-String,System-Threading-CancellationToken-'></a>
### GetConnectorConfigurationAsync(connectorId,cancellationToken) `method`

##### Summary

Get a connect configuration

##### Returns

Connector data model

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Connector Configuration Id |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager-GetConnectorStatusAsync-System-String,System-Threading-CancellationToken-'></a>
### GetConnectorStatusAsync(connectorId,cancellationToken) `method`

##### Summary

Get the connector status

##### Returns

Connector status. Always returns a value, even if the connector doesn't exist.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Id of the connector to get the status for |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager-GetSubmissionStatusAsync-System-String,System-Threading-CancellationToken-'></a>
### GetSubmissionStatusAsync(connectorId,cancellationToken) `method`

##### Summary

Get the submission feature status

##### Returns

Submission feature status. Always returns a value, even if the connector doesn't exist.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Id of the connector to get the submission feature status for |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager-SetConnectorConfigurationAsync-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel,System-Threading-CancellationToken-'></a>
### SetConnectorConfigurationAsync(connectorData,cancellationToken) `method`

##### Summary

Set connector configuration

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorData | [RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel](#T-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel 'RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel') | Connector data |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='T-RecordPoint-Connectors-SDK-ContentManager-IConnectorSecretAction'></a>
## IConnectorSecretAction `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Definition for a Connector Secret action

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IConnectorSecretAction-SaveSecretsAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Abstractions-Content-ConnectorSecret}-'></a>
### SaveSecretsAsync(connectorConfiguration,secrets) `method`

##### Summary

Save the secret for the connector configuration instance

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfiguration | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | The connector configuration |
| secrets | [System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Abstractions.Content.ConnectorSecret}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IList 'System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Abstractions.Content.ConnectorSecret}') | The secrets to be saved by the connector |

<a name='T-RecordPoint-Connectors-SDK-Abstractions-ContentManager-IContentDiscoveryState'></a>
## IContentDiscoveryState `type`

##### Namespace

RecordPoint.Connectors.SDK.Abstractions.ContentManager

##### Summary

Contract for a State object that is used to store the state of a Content Discovery operation

<a name='P-RecordPoint-Connectors-SDK-Abstractions-ContentManager-IContentDiscoveryState-LastBackOffDelaySeconds'></a>
### LastBackOffDelaySeconds `property`

##### Summary

The number of seconds the last execution was delayed by

##### Remarks

Used for exponential backoff

<a name='T-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider'></a>
## IContentManagerActionProvider `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Contract for a Content Manager Action Provider

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider-CreateAggregationSubmissionCallbackAction'></a>
### CreateAggregationSubmissionCallbackAction() `method`

##### Summary

Create a new aggregation submission callback action

##### Returns

Aggregation Submission Callback Action

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider-CreateAuditEventSubmissionCallbackAction'></a>
### CreateAuditEventSubmissionCallbackAction() `method`

##### Summary

Create a new audit event submission callback action

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider-CreateBinaryRetrievalAction'></a>
### CreateBinaryRetrievalAction() `method`

##### Summary

Create a new binary retrieval action

##### Returns

Binary Retrieval Action

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider-CreateBinarySubmissionCallbackAction'></a>
### CreateBinarySubmissionCallbackAction() `method`

##### Summary

Create a new binary submission callback action

##### Returns

Binary Submission Callback Action

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider-CreateChannelDiscoveryAction'></a>
### CreateChannelDiscoveryAction() `method`

##### Summary

Create a new Channel discovery action

##### Returns

Channel scanner

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider-CreateContentManagerCallbackAction'></a>
### CreateContentManagerCallbackAction() `method`

##### Summary

Create a new content manager callback action

##### Returns

Content Manager Callback Action

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider-CreateContentRegistrationAction'></a>
### CreateContentRegistrationAction() `method`

##### Summary

Create a new content registration action

##### Returns

Content Registration Action

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider-CreateContentSynchronisationAction'></a>
### CreateContentSynchronisationAction() `method`

##### Summary

Create a new content synchronisation action

##### Returns

Content Synchronisation Action

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider-CreateGenerationReportAction'></a>
### CreateGenerationReportAction() `method`

##### Summary

Create a new report generation action

##### Returns

Generate Report Action

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider-CreateGenericAction``2'></a>
### CreateGenericAction\`\`2() `method`

##### Summary

Create a new generic action with the provided templated input and output classes

##### Returns

Generic Action

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider-CreateGenericManagedAction``2'></a>
### CreateGenericManagedAction\`\`2() `method`

##### Summary

Create a new generic action with the provided templated input and output classes

##### Returns

Generic Action

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider-CreateRecordDisposalAction'></a>
### CreateRecordDisposalAction() `method`

##### Summary

Create a new record disposal action

##### Returns

Record Disposal Action

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IContentManagerActionProvider-CreateRecordSubmissionCallbackAction'></a>
### CreateRecordSubmissionCallbackAction() `method`

##### Summary

Create a new record submission callback action

##### Returns

Record Submission Callback Action

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-ContentManager-IContentManagerCallbackAction'></a>
## IContentManagerCallbackAction `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Defines a Content Manager callback which is executed after the content manager operation is invoked

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IContentManagerCallbackAction-ExecuteAsync-System-Collections-Generic-List{RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel},System-Threading-CancellationToken-'></a>
### ExecuteAsync(connectorConfigurations,cancellationToken) `method`

##### Summary

Executes when new connector configurations are discovered during the content manager operation

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfigurations | [System.Collections.Generic.List{RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel}') | The new Connector Configurations that have been found |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='T-RecordPoint-Connectors-SDK-ContentManager-IContentRegistrationAction'></a>
## IContentRegistrationAction `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Definition for a Content Registration action

<a name='F-RecordPoint-Connectors-SDK-ContentManager-IContentRegistrationAction-EndDate'></a>
### EndDate `constants`

##### Summary

Key for the EndDate of a Content Registration context

<a name='F-RecordPoint-Connectors-SDK-ContentManager-IContentRegistrationAction-StartDate'></a>
### StartDate `constants`

##### Summary

Key for the StartDate of a Content Registration context

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IContentRegistrationAction-BeginAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Channel,System-Collections-Generic-IDictionary{System-String,System-String},System-Threading-CancellationToken-'></a>
### BeginAsync(connectorConfiguration,channel,context,cancellationToken) `method`

##### Summary

Begin a new content registration operation

##### Returns

Result of the registration operation

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfiguration | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | The connector configuration |
| channel | [RecordPoint.Connectors.SDK.Content.Channel](#T-RecordPoint-Connectors-SDK-Content-Channel 'RecordPoint.Connectors.SDK.Content.Channel') | The channel to perform content registration on |
| context | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | Custom Context for the Registration action |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IContentRegistrationAction-ContinueAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Channel,System-String,System-Collections-Generic-IDictionary{System-String,System-String},System-Threading-CancellationToken-'></a>
### ContinueAsync(connectorConfiguration,channel,cursor,context,cancellationToken) `method`

##### Summary

Continue a content registration

##### Returns

Result of the registration operation

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfiguration | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | The connector configuration |
| channel | [RecordPoint.Connectors.SDK.Content.Channel](#T-RecordPoint-Connectors-SDK-Content-Channel 'RecordPoint.Connectors.SDK.Content.Channel') | The channel to perform content registration on |
| cursor | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Scan cursor provided by the prior sync operation |
| context | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | Custom Context for the Registration action |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IContentRegistrationAction-StopAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Channel,System-String,System-Threading-CancellationToken-'></a>
### StopAsync(connectorConfiguration,channel,cursor,cancellationToken) `method`

##### Summary

Stops the content synchronisation operation

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfiguration | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | The connector configuration |
| channel | [RecordPoint.Connectors.SDK.Content.Channel](#T-RecordPoint-Connectors-SDK-Content-Channel 'RecordPoint.Connectors.SDK.Content.Channel') | The channel to perform content registration on |
| cursor | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Scan cursor provided by the prior sync operation |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

##### Remarks

Stops a current content synchronisation operation. Typically because a connector has been disabled, kill switch has been hit etc.
Some content modules may use this to stop expensive services that might be running in order to support the synchronisation operation.
Content modules should restart processing when asked to continue

<a name='T-RecordPoint-Connectors-SDK-ContentManager-IContentRegistrationRequestAction'></a>
## IContentRegistrationRequestAction `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Definition for a Content Registration Request action

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IContentRegistrationRequestAction-GetChannelsFromRequestAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Abstractions-Content-ContentRegistrationRequest,System-Threading-CancellationToken-'></a>
### GetChannelsFromRequestAsync(connectorConfiguration,contentRegistrationRequest,cancellationToken) `method`

##### Summary

Obtains a list of channels to perform content registration from the requested context

##### Returns

Result of the registration operation

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfiguration | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | The Connector configuration |
| contentRegistrationRequest | [RecordPoint.Connectors.SDK.Abstractions.Content.ContentRegistrationRequest](#T-RecordPoint-Connectors-SDK-Abstractions-Content-ContentRegistrationRequest 'RecordPoint.Connectors.SDK.Abstractions.Content.ContentRegistrationRequest') | The request context used to obtain the channels to register |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='T-RecordPoint-Connectors-SDK-ContentManager-IContentSynchronisationAction'></a>
## IContentSynchronisationAction `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Defines content synchronisation action, which is responsible for synchronising content within the Content Source from the specified start time, or point where the last scan completed via the provided cursor

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IContentSynchronisationAction-BeginAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Channel,System-DateTimeOffset,System-Threading-CancellationToken-'></a>
### BeginAsync(connectorConfiguration,channel,startDate,cancellationToken) `method`

##### Summary

Begin a new content synchronisation operation that will generate a stream of content starting from a specific point in time

##### Returns

Result of the content synchronisation

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfiguration | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | The onnector configuration |
| channel | [RecordPoint.Connectors.SDK.Content.Channel](#T-RecordPoint-Connectors-SDK-Content-Channel 'RecordPoint.Connectors.SDK.Content.Channel') | The channel to perform content registration on |
| startDate | [System.DateTimeOffset](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTimeOffset 'System.DateTimeOffset') | Start date time of the synncronisation operation |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IContentSynchronisationAction-ContinueAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Channel,System-String,System-Threading-CancellationToken-'></a>
### ContinueAsync(connectorConfiguration,channel,cursor,cancellationToken) `method`

##### Summary

Continue a content synchronisation operation

##### Returns

Result of the content synchronisation

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfiguration | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | The onnector configuration |
| channel | [RecordPoint.Connectors.SDK.Content.Channel](#T-RecordPoint-Connectors-SDK-Content-Channel 'RecordPoint.Connectors.SDK.Content.Channel') | The channel to perform content registration on |
| cursor | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Scan cursor provided by the prior sync operation |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IContentSynchronisationAction-StopAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Channel,System-String,System-Threading-CancellationToken-'></a>
### StopAsync(connectorConfiguration,channel,cursor,cancellationToken) `method`

##### Summary

Stops the content synchronisation operation

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfiguration | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | The onnector configuration |
| channel | [RecordPoint.Connectors.SDK.Content.Channel](#T-RecordPoint-Connectors-SDK-Content-Channel 'RecordPoint.Connectors.SDK.Content.Channel') | The channel to perform content registration on |
| cursor | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Scan cursor provided by the prior sync operation |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

##### Remarks

Stops a current content synchronisation operation. Typically because a connector has been disabled, kill switch has been hit etc.
Some content modules may use this to stop expensive services that might be running in order to support the synchronisation operation.
Content modules should restart processing when asked to continue

<a name='T-RecordPoint-Connectors-SDK-Work-IDeadLetterQueueService'></a>
## IDeadLetterQueueService `type`

##### Namespace

RecordPoint.Connectors.SDK.Work

##### Summary

Deadletter queue interface

<a name='M-RecordPoint-Connectors-SDK-Work-IDeadLetterQueueService-DeleteAllMessagesAsync-System-String-'></a>
### DeleteAllMessagesAsync(queueName) `method`

##### Summary

Delete the all messages from the deadletter queue

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| queueName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-Work-IDeadLetterQueueService-DeleteMessageAsync-System-String,System-Int64-'></a>
### DeleteMessageAsync(queueName,sequenceNumber) `method`

##### Summary

Delete the message from the deadletter queue

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| queueName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| sequenceNumber | [System.Int64](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int64 'System.Int64') |  |

<a name='M-RecordPoint-Connectors-SDK-Work-IDeadLetterQueueService-GetAllMessagesAsync-System-String-'></a>
### GetAllMessagesAsync(queueName) `method`

##### Summary

Get all messages based on the queue

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| queueName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-Work-IDeadLetterQueueService-GetMessageAsync-System-String,System-Int64-'></a>
### GetMessageAsync(queueName,sequenceNumber) `method`

##### Summary

Get message based on the queue and sequenceNumber

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| queueName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| sequenceNumber | [System.Int64](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int64 'System.Int64') |  |

<a name='M-RecordPoint-Connectors-SDK-Work-IDeadLetterQueueService-ResubmitMessagesAsync-System-String,System-Int64[]-'></a>
### ResubmitMessagesAsync(queueName,sequenceNumbers) `method`

##### Summary

Resubmit to queue based on the queueName and sequenceNumbers

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| queueName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| sequenceNumbers | [System.Int64[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int64[] 'System.Int64[]') |  |

<a name='T-RecordPoint-Connectors-SDK-ContentManager-IGenerateReportAction'></a>
## IGenerateReportAction `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Definition for a Content Report Generation action

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IGenerateReportAction-BeginAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,System-Threading-CancellationToken-'></a>
### BeginAsync(connectorConfiguration,cancellationToken) `method`

##### Summary

Begin a new content report generation operation

##### Returns

Result of the discovery operation

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfiguration | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | The onnector configuration |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IGenerateReportAction-ContinueAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,System-String,System-Threading-CancellationToken-'></a>
### ContinueAsync(connectorConfiguration,cursor,cancellationToken) `method`

##### Summary

Continue a content report generation

##### Returns

Result of the discovery operation

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfiguration | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | The onnector configuration |
| cursor | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Scan cursor provided by the prior sync operation |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='T-RecordPoint-Connectors-SDK-ContentManager-IGenericAction`2'></a>
## IGenericAction\`2 `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Defines a generic action which can be executed by an Operation.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IGenericAction`2-ExecuteAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,`0,System-Threading-CancellationToken-'></a>
### ExecuteAsync(connectorConfiguration,item,cancellationToken) `method`

##### Summary

Executes a custom action

##### Returns

Action outcome

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfiguration | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | The connector configuration |
| item | [\`0](#T-`0 '`0') | The item to take action in respect to |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='T-RecordPoint-Connectors-SDK-ContentManager-IGenericManagedAction`2'></a>
## IGenericManagedAction\`2 `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Defines a generic managed action which can be executed by an Operation.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TInput |  |
| TOutput |  |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IGenericManagedAction`2-BeginAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,`0,System-Collections-Generic-IDictionary{System-String,System-String},System-Threading-CancellationToken-'></a>
### BeginAsync(connectorConfiguration,item,context,cancellationToken) `method`

##### Summary

Begin a new generic action

##### Returns

Result of the action

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfiguration | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | The connector configuration |
| item | [\`0](#T-`0 '`0') | The item to take action in respect to |
| context | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | Custom Context for the action |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IGenericManagedAction`2-ContinueAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,`0,System-String,System-Collections-Generic-IDictionary{System-String,System-String},System-Threading-CancellationToken-'></a>
### ContinueAsync(connectorConfiguration,item,cursor,context,cancellationToken) `method`

##### Summary

Continue a generic action

##### Returns

Result of the action

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfiguration | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | The connector configuration |
| item | [\`0](#T-`0 '`0') | The item to take action in respect to |
| cursor | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Cursor provided by the prior action |
| context | [System.Collections.Generic.IDictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IDictionary 'System.Collections.Generic.IDictionary{System.String,System.String}') | Custom Context for the action |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='T-RecordPoint-Connectors-SDK-Health-IHealthCheckLiveAction'></a>
## IHealthCheckLiveAction `type`

##### Namespace

RecordPoint.Connectors.SDK.Health

##### Summary

Contract to implement a health check action for the live endpoint.

<a name='M-RecordPoint-Connectors-SDK-Health-IHealthCheckLiveAction-CheckIsLiveAsync'></a>
### CheckIsLiveAsync() `method`

##### Summary

Action to determine if the service instance is Live.

##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Health-IHealthCheckManager'></a>
## IHealthCheckManager `type`

##### Namespace

RecordPoint.Connectors.SDK.Health

##### Summary

Health Check Manager definition

<a name='P-RecordPoint-Connectors-SDK-Health-IHealthCheckManager-HealthCheckResult'></a>
### HealthCheckResult `property`

##### Summary

Gets the current health checks results

<a name='M-RecordPoint-Connectors-SDK-Health-IHealthCheckManager-RunHealthCheckAsync-System-Threading-CancellationToken-'></a>
### RunHealthCheckAsync(cancellationToken) `method`

##### Summary

Run runtime health check

##### Returns

Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='T-RecordPoint-Connectors-SDK-Health-IHealthCheckReadyAction'></a>
## IHealthCheckReadyAction `type`

##### Namespace

RecordPoint.Connectors.SDK.Health

##### Summary

Contract to implement a health check action for the ready endpoint.

<a name='M-RecordPoint-Connectors-SDK-Health-IHealthCheckReadyAction-CheckIsReadyAsync'></a>
### CheckIsReadyAsync() `method`

##### Summary

Action to determine if the service instance is Ready.

##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Health-IHealthCheckStrategy'></a>
## IHealthCheckStrategy `type`

##### Namespace

RecordPoint.Connectors.SDK.Health

##### Summary

Health check strategy

<a name='P-RecordPoint-Connectors-SDK-Health-IHealthCheckStrategy-HealthCheckType'></a>
### HealthCheckType `property`

##### Summary

Health check type

<a name='M-RecordPoint-Connectors-SDK-Health-IHealthCheckStrategy-HealthCheckAsync-System-Threading-CancellationToken-'></a>
### HealthCheckAsync() `method`

##### Summary

Perform a health check

##### Returns

Health check

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Work-IManagedWorkFactory'></a>
## IManagedWorkFactory `type`

##### Namespace

RecordPoint.Connectors.SDK.Work

##### Summary

Defines the public interface used to create and load jobs

<a name='M-RecordPoint-Connectors-SDK-Work-IManagedWorkFactory-CreateWork-System-String,System-String,System-String,System-String,System-String-'></a>
### CreateWork(workStatusId,workType,connectorId,configurationType,configuration) `method`

##### Summary

Create a new job

##### Returns

Managed work status manager

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workStatusId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Work Status Id |
| workType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Work type |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Id of the connector the job is for |
| configurationType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | String that identified how the configuration is formatted |
| configuration | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Work configuration |

<a name='M-RecordPoint-Connectors-SDK-Work-IManagedWorkFactory-LoadWork-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-'></a>
### LoadWork(workStatus) `method`

##### Summary

Load work from a work state object

##### Returns

Managed work status manager

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workStatus | [RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel](#T-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel') | Work State that defines the work |

<a name='T-RecordPoint-Connectors-SDK-Work-IManagedWorkManager'></a>
## IManagedWorkManager `type`

##### Namespace

RecordPoint.Connectors.SDK.Work

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Work-IManagedWorkManager-WorkStatus'></a>
### WorkStatus `property`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Work-IManagedWorkManager-AbandonedAsync-System-String,System-Threading-CancellationToken-'></a>
### AbandonedAsync(reason,cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| reason | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Work-IManagedWorkManager-CheckAsync-System-Threading-CancellationToken-'></a>
### CheckAsync(cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Work-IManagedWorkManager-CompleteAsync-System-String,System-Threading-CancellationToken-'></a>
### CompleteAsync(reason,cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| reason | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Work-IManagedWorkManager-ContinueAsync-System-String,System-String,System-DateTimeOffset,System-Threading-CancellationToken-'></a>
### ContinueAsync(stateType,state,waitTill,cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| stateType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| state | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| waitTill | [System.DateTimeOffset](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTimeOffset 'System.DateTimeOffset') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Work-IManagedWorkManager-FailedAsync-System-String,System-Threading-CancellationToken-'></a>
### FailedAsync(reason,cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| reason | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Work-IManagedWorkManager-FaultyAsync-System-String,System-Exception,System-Threading-CancellationToken,System-Nullable{System-Int32}-'></a>
### FaultyAsync(reason,exception,cancellationToken,faultedCount) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| reason | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| exception | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |
| faultedCount | [System.Nullable{System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int32}') |  |

<a name='M-RecordPoint-Connectors-SDK-Work-IManagedWorkManager-RetryAsync-System-DateTimeOffset,System-Threading-CancellationToken,System-Nullable{System-Int32}-'></a>
### RetryAsync(waitTill,cancellationToken,faultedCount) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| waitTill | [System.DateTimeOffset](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTimeOffset 'System.DateTimeOffset') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |
| faultedCount | [System.Nullable{System.Int32}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int32}') |  |

<a name='M-RecordPoint-Connectors-SDK-Work-IManagedWorkManager-StartAsync-System-Threading-CancellationToken-'></a>
### StartAsync(cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='T-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager'></a>
## IManagedWorkStatusManager `type`

##### Namespace

RecordPoint.Connectors.SDK.Work

##### Summary

Definition for a class that manages the status of Managed Work

<a name='M-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager-AddWorkStatusAsync-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel,System-Threading-CancellationToken-'></a>
### AddWorkStatusAsync(managedWorkStatusModel,cancellationToken) `method`

##### Summary

Adds a new work status to storage

##### Returns

Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| managedWorkStatusModel | [RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel](#T-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel') | work status to add |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager-GetAllWorkStatusesAsync-System-Threading-CancellationToken-'></a>
### GetAllWorkStatusesAsync(cancellationToken) `method`

##### Summary

Lists the status of all managed work

##### Returns

A list of work statuses

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager-GetWorkStatusAsync-System-String,System-Threading-CancellationToken-'></a>
### GetWorkStatusAsync(workStatusId,cancellationToken) `method`

##### Summary

Gets the status of the specified Work

##### Returns

Work Status if it exists, otherwise null

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workStatusId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Id of the work status |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager-GetWorkStatusesAsync-System-Linq-Expressions-Expression{System-Func{RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel,System-Boolean}},System-Threading-CancellationToken-'></a>
### GetWorkStatusesAsync(predicate,cancellationToken) `method`

##### Summary

Lists the status of managed work that matches the given predicate

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| predicate | [System.Linq.Expressions.Expression{System.Func{RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel,System.Boolean}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Func{RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel,System.Boolean}}') | A function to test each element for a condition. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager-IsAnyAsync-System-Linq-Expressions-Expression{System-Func{RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel,System-Boolean}},System-Threading-CancellationToken-'></a>
### IsAnyAsync(predicate,cancellationToken) `method`

##### Summary

Determines if there is managed work that matches the given predicate

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| predicate | [System.Linq.Expressions.Expression{System.Func{RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel,System.Boolean}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Linq.Expressions.Expression 'System.Linq.Expressions.Expression{System.Func{RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel,System.Boolean}}') | A function to test each element for a condition. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager-RemoveWorkStatusesAsync-System-String[],System-Threading-CancellationToken-'></a>
### RemoveWorkStatusesAsync(workIds,cancellationToken) `method`

##### Summary

Removes the specified work

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workIds | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | The Ids of the Work to remove |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager-SetWorkAbandonedAsync-System-String,System-Threading-CancellationToken-'></a>
### SetWorkAbandonedAsync(workStatusId,cancellationToken) `method`

##### Summary

Sets the specified work status to abandoned

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workStatusId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Id of work status to update |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager-SetWorkCompleteAsync-System-String,System-Threading-CancellationToken-'></a>
### SetWorkCompleteAsync(workStatusId,cancellationToken) `method`

##### Summary

Sets the specified work status to complete

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workStatusId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Id of work status to update |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager-SetWorkContinueAsync-System-String,System-String,System-String,System-Threading-CancellationToken-'></a>
### SetWorkContinueAsync(workStatusId,continuedWorkId,state,cancellationToken) `method`

##### Summary

Sets the specified work status to continue

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workStatusId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Id of work status to update |
| continuedWorkId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Id of the work to continue on with |
| state | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The serialised state of the work |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager-SetWorkFailedAsync-System-String,System-Threading-CancellationToken-'></a>
### SetWorkFailedAsync(workStatusId,cancellationToken) `method`

##### Summary

Sets the specified work status to failed

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workStatusId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Id of work status to update |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Work-IManagedWorkStatusManager-SetWorkRunningAsync-System-String,System-Threading-CancellationToken-'></a>
### SetWorkRunningAsync(workStatusId,cancellationToken) `method`

##### Summary

Sets the specified work status to running

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workStatusId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Id of work status to update |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='T-RecordPoint-Connectors-SDK-Notifications-INotificationManager'></a>
## INotificationManager `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Notifications-INotificationManager-HandleNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken-'></a>
### HandleNotificationAsync(notification,cancellationToken) `method`

##### Summary

Handle notification

##### Returns

Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| notification | [RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel') | Notification to handle |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='T-RecordPoint-Connectors-SDK-Notifications-INotificationStrategy'></a>
## INotificationStrategy `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Notifications-INotificationStrategy-NotificationType'></a>
### NotificationType `property`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Notifications-INotificationStrategy-HandleNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken-'></a>
### HandleNotificationAsync(notification,cancellationToken) `method`

##### Summary

Handle a notification

##### Returns

Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| notification | [RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel') | Notification |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation Token |

<a name='T-RecordPoint-Connectors-SDK-Notifications-INotificationWorker'></a>
## INotificationWorker `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Notifications-INotificationWorker-HandleNotificationAsync-RecordPoint-Connectors-SDK-Notifications-Notification,System-Threading-CancellationToken-'></a>
### HandleNotificationAsync(notificationRequest,cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| notificationRequest | [RecordPoint.Connectors.SDK.Notifications.Notification](#T-RecordPoint-Connectors-SDK-Notifications-Notification 'RecordPoint.Connectors.SDK.Notifications.Notification') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope'></a>
## IObservabilityScope `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability

##### Summary

Observability scope manager. Used to keep track of key state for observability operations like tracking events, traces, and exceptions

<a name='P-RecordPoint-Connectors-SDK-Observability-IObservabilityScope-Dimensions'></a>
### Dimensions `property`

##### Summary

Get the observability dimensions for the current scope

<a name='P-RecordPoint-Connectors-SDK-Observability-IObservabilityScope-Measures'></a>
### Measures `property`

##### Summary

Get the observability measures for the current scope

<a name='M-RecordPoint-Connectors-SDK-Observability-IObservabilityScope-BeginScope-RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures-'></a>
### BeginScope(dimensions,measures) `method`

##### Summary

Begin a new observability scope, local to the current async task

##### Returns

Discardable scope

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dimensions | [RecordPoint.Connectors.SDK.Observability.Dimensions](#T-RecordPoint-Connectors-SDK-Observability-Dimensions 'RecordPoint.Connectors.SDK.Observability.Dimensions') | Observability dimensions to add to the new scope |
| measures | [RecordPoint.Connectors.SDK.Observability.Measures](#T-RecordPoint-Connectors-SDK-Observability-Measures 'RecordPoint.Connectors.SDK.Observability.Measures') | Observability measures to add to the new scope |

<a name='T-RecordPoint-Connectors-SDK-Work-IQueueableWork'></a>
## IQueueableWork `type`

##### Namespace

RecordPoint.Connectors.SDK.Work

##### Summary

Definition of work that is submitted to a queue for execution.

<a name='P-RecordPoint-Connectors-SDK-Work-IQueueableWork-WorkRequest'></a>
### WorkRequest `property`

##### Summary

Original work request

<a name='M-RecordPoint-Connectors-SDK-Work-IQueueableWork-GetWorkResult'></a>
### GetWorkResult() `method`

##### Summary

Result of the unit of work

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-IQueueableWork-RunWorkRequestAsync-RecordPoint-Connectors-SDK-Work-WorkRequest,System-Threading-CancellationToken-'></a>
### RunWorkRequestAsync(workRequest,cancellationToken) `method`

##### Summary

Use this work item to run a work request

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workRequest | [RecordPoint.Connectors.SDK.Work.WorkRequest](#T-RecordPoint-Connectors-SDK-Work-WorkRequest 'RecordPoint.Connectors.SDK.Work.WorkRequest') | Work request that is compatible with this work item |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='T-RecordPoint-Connectors-SDK-Work-IQueueableWorkManager'></a>
## IQueueableWorkManager `type`

##### Namespace

RecordPoint.Connectors.SDK.Work

##### Summary

Definition for manager responsible for executing work

<a name='M-RecordPoint-Connectors-SDK-Work-IQueueableWorkManager-HandleWorkRequestAsync-RecordPoint-Connectors-SDK-Work-WorkRequest,System-Threading-CancellationToken-'></a>
### HandleWorkRequestAsync(workRequest,cancellationToken) `method`

##### Summary

Handle a work request

##### Returns

Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workRequest | [RecordPoint.Connectors.SDK.Work.WorkRequest](#T-RecordPoint-Connectors-SDK-Work-WorkRequest 'RecordPoint.Connectors.SDK.Work.WorkRequest') | Work request to handle |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='T-RecordPoint-Connectors-SDK-R365-IR365Client'></a>
## IR365Client `type`

##### Namespace

RecordPoint.Connectors.SDK.R365

##### Summary

Provides access to R365

<a name='M-RecordPoint-Connectors-SDK-R365-IR365Client-IsConfigured'></a>
### IsConfigured() `method`

##### Summary

Is Records 365 Configured?

##### Returns

True if records 365 has been configured and we should be able to invoke it, otherwise false

##### Parameters

This method has no parameters.

##### Remarks

Note that the client can be "ready" (All its dependant services are ready) but the configuration not yet loaded.

<a name='M-RecordPoint-Connectors-SDK-R365-IR365Client-SubmitAggregation-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Aggregation,System-Threading-CancellationToken-'></a>
### SubmitAggregation(connectorConfig,aggregation,cancellationToken) `method`

##### Summary

Submit an aggregation to records 365

##### Returns

Submit result

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfig | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | Configuration for the Connector |
| aggregation | [RecordPoint.Connectors.SDK.Content.Aggregation](#T-RecordPoint-Connectors-SDK-Content-Aggregation 'RecordPoint.Connectors.SDK.Content.Aggregation') | Aggregation to submit |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-R365-IR365Client-SubmitAuditEvent-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-AuditEvent,System-Threading-CancellationToken-'></a>
### SubmitAuditEvent(connectorConfig,auditEvent,cancellationToken) `method`

##### Summary

Submit an Audit Event

##### Returns

Submit result

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfig | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | Configuration for the Connector |
| auditEvent | [RecordPoint.Connectors.SDK.Content.AuditEvent](#T-RecordPoint-Connectors-SDK-Content-AuditEvent 'RecordPoint.Connectors.SDK.Content.AuditEvent') | Audit Event to submit |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-R365-IR365Client-SubmitBinary-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-BinaryMetaInfo,System-IO-Stream,System-Threading-CancellationToken-'></a>
### SubmitBinary(connectorConfig,binaryMetaInfo,binaryStream,cancellationToken) `method`

##### Summary

Submit a binary to records 365

##### Returns

Submit result

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfig | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | Configuration for the Connector |
| binaryMetaInfo | [RecordPoint.Connectors.SDK.Content.BinaryMetaInfo](#T-RecordPoint-Connectors-SDK-Content-BinaryMetaInfo 'RecordPoint.Connectors.SDK.Content.BinaryMetaInfo') | Metainfo for the binary being submitted |
| binaryStream | [System.IO.Stream](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IO.Stream 'System.IO.Stream') | Stream of the binary to be submitted |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-R365-IR365Client-SubmitRecord-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Record,System-Threading-CancellationToken-'></a>
### SubmitRecord(connectorConfig,record,cancellationToken) `method`

##### Summary

Submit a record

##### Returns

Submit result

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfig | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | Configuration for the Connector |
| record | [RecordPoint.Connectors.SDK.Content.Record](#T-RecordPoint-Connectors-SDK-Content-Record 'RecordPoint.Connectors.SDK.Content.Record') | Record to submit |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='T-RecordPoint-Connectors-SDK-Configuration-IR365ConfigurationClient'></a>
## IR365ConfigurationClient `type`

##### Namespace

RecordPoint.Connectors.SDK.Configuration

##### Summary

Defines the Records 365 configuration client that provides access to the Records 365 configuration

<a name='M-RecordPoint-Connectors-SDK-Configuration-IR365ConfigurationClient-GetR365Configuration-System-String-'></a>
### GetR365Configuration() `method`

##### Summary

Get the Records 365 configuration

##### Returns

Default records 365 configuration, null if it doesn't exist

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Configuration-IR365ConfigurationClient-R365ConfigurationExists'></a>
### R365ConfigurationExists() `method`

##### Summary

Does a R365 configuration exists?

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-R365-IR365Pipelines'></a>
## IR365Pipelines `type`

##### Namespace

RecordPoint.Connectors.SDK.R365

##### Summary

Contains the various submission pipelines

<a name='P-RecordPoint-Connectors-SDK-R365-IR365Pipelines-AggregationPipeline'></a>
### AggregationPipeline `property`

##### Summary

Aggregation submission pipeline

<a name='P-RecordPoint-Connectors-SDK-R365-IR365Pipelines-AuditEventPipeline'></a>
### AuditEventPipeline `property`

##### Summary

Autdit event submission pipeline

<a name='P-RecordPoint-Connectors-SDK-R365-IR365Pipelines-BinaryPipeline'></a>
### BinaryPipeline `property`

##### Summary

Binary submission pipeline

<a name='P-RecordPoint-Connectors-SDK-R365-IR365Pipelines-RecordPipeline'></a>
### RecordPipeline `property`

##### Summary

Record submission pipeline

<a name='T-RecordPoint-Connectors-SDK-ContentManager-IRecordDisposalAction'></a>
## IRecordDisposalAction `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Defines a record disposal action which is responsible for removing the record from the content source.

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IRecordDisposalAction-ExecuteAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Record,System-Threading-CancellationToken-'></a>
### ExecuteAsync(connectorConfiguration,record,cancellationToken) `method`

##### Summary

Destroys the record in the content source

##### Returns

Get binary outcome

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfiguration | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | The connector configuration |
| record | [RecordPoint.Connectors.SDK.Content.Record](#T-RecordPoint-Connectors-SDK-Content-Record 'RecordPoint.Connectors.SDK.Content.Record') | The record to dispose |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='T-RecordPoint-Connectors-SDK-ContentManager-IRecordSubmissionCallbackAction'></a>
## IRecordSubmissionCallbackAction `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Defines a record submission callback which is executed after a successfull record submission

<a name='M-RecordPoint-Connectors-SDK-ContentManager-IRecordSubmissionCallbackAction-ExecuteAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Content-Record,RecordPoint-Connectors-SDK-Abstractions-ContentManager-SubmissionActionType,System-Threading-CancellationToken-'></a>
### ExecuteAsync(connectorConfiguration,record,submissionActionType,cancellationToken) `method`

##### Summary

Executes after a successfull record submission operation

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfiguration | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | The connector configuration |
| record | [RecordPoint.Connectors.SDK.Content.Record](#T-RecordPoint-Connectors-SDK-Content-Record 'RecordPoint.Connectors.SDK.Content.Record') | Meta info for the record that was submitted |
| submissionActionType | [RecordPoint.Connectors.SDK.Abstractions.ContentManager.SubmissionActionType](#T-RecordPoint-Connectors-SDK-Abstractions-ContentManager-SubmissionActionType 'RecordPoint.Connectors.SDK.Abstractions.ContentManager.SubmissionActionType') | Indicates if the action is being invoked pre-submit or post-submit |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='T-RecordPoint-Connectors-SDK-Caching-Semaphore-ISemaphoreLockManager'></a>
## ISemaphoreLockManager `type`

##### Namespace

RecordPoint.Connectors.SDK.Caching.Semaphore

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Caching-Semaphore-ISemaphoreLockManager-ConnectorConfiguration'></a>
### ConnectorConfiguration `property`

##### Summary

The Connector Configuration for the executing context

<a name='M-RecordPoint-Connectors-SDK-Caching-Semaphore-ISemaphoreLockManager-CheckWaitSemaphoreAsync-System-String,System-Object,System-Threading-CancellationToken-'></a>
### CheckWaitSemaphoreAsync(workType,context,cancellationToken) `method`

##### Summary

Checks if a valid semaphore lock is active and delays execution until the lock expires

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| context | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Caching-Semaphore-ISemaphoreLockManager-GetSemaphoreAsync-System-String,System-Object,System-Threading-CancellationToken-'></a>
### GetSemaphoreAsync(workType,context,cancellationToken) `method`

##### Summary

Checks if a sempahore lock is active and returns the Date/Time when the lock expires

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| context | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Caching-Semaphore-ISemaphoreLockManager-SetSemaphoreAsync-RecordPoint-Connectors-SDK-Caching-Semaphore-SemaphoreLockType,System-String,System-Object,System-Int32,System-Threading-CancellationToken-'></a>
### SetSemaphoreAsync(semaphoreLockType,workType,context,duration,cancellationToken) `method`

##### Summary

Sets a semaphore lock with the provided context for the specified duration

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| semaphoreLockType | [RecordPoint.Connectors.SDK.Caching.Semaphore.SemaphoreLockType](#T-RecordPoint-Connectors-SDK-Caching-Semaphore-SemaphoreLockType 'RecordPoint.Connectors.SDK.Caching.Semaphore.SemaphoreLockType') |  |
| workType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| context | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |
| duration | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='T-RecordPoint-Connectors-SDK-Caching-ISemaphoreLockScopedKeyAction'></a>
## ISemaphoreLockScopedKeyAction `type`

##### Namespace

RecordPoint.Connectors.SDK.Caching

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Caching-ISemaphoreLockScopedKeyAction-ExecuteAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,System-String,System-Object,System-Threading-CancellationToken-'></a>
### ExecuteAsync(connectorConfigModel,workType,context,cancellationToken) `method`

##### Summary

Execution point for generating a scoped semaphore key

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfigModel | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') |  |
| workType | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| context | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='T-RecordPoint-Connectors-SDK-Status-IStatusManager'></a>
## IStatusManager `type`

##### Namespace

RecordPoint.Connectors.SDK.Status

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Status-IStatusManager-GetStatusModelAsync-System-Threading-CancellationToken-'></a>
### GetStatusModelAsync(cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='T-RecordPoint-Connectors-SDK-Status-IStatusStrategy'></a>
## IStatusStrategy `type`

##### Namespace

RecordPoint.Connectors.SDK.Status

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Status-IStatusStrategy-GetStatusText-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel,System-Threading-CancellationToken-'></a>
### GetStatusText(connectorModel,cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorModel | [RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel](#T-RecordPoint-Connectors-SDK-Connectors-ConnectorConfigurationModel 'RecordPoint.Connectors.SDK.Connectors.ConnectorConfigurationModel') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='T-RecordPoint-Connectors-SDK-Context-ISystemContext'></a>
## ISystemContext `type`

##### Namespace

RecordPoint.Connectors.SDK.Context

##### Summary

Defines a context for the entire connector system

<a name='M-RecordPoint-Connectors-SDK-Context-ISystemContext-GetCompanyName'></a>
### GetCompanyName() `method`

##### Summary

Name of the company that develops the system

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Context-ISystemContext-GetConnectorName'></a>
### GetConnectorName() `method`

##### Summary

Name of the system, typically the name of the connector

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Context-ISystemContext-GetDataRootPath'></a>
### GetDataRootPath() `method`

##### Summary

Get the root path for data

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Context-ISystemContext-GetServiceName'></a>
### GetServiceName() `method`

##### Summary

Name of the running service

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Context-ISystemContext-GetShortName'></a>
### GetShortName() `method`

##### Summary

Abbreviated name of the system

##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Observability-ITelemetrySink'></a>
## ITelemetrySink `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability

##### Summary

Interface for submitting a variety of statistics and measures about the system in a consistent and structured manner.

<a name='M-RecordPoint-Connectors-SDK-Observability-ITelemetrySink-TrackEvent-System-String,RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures-'></a>
### TrackEvent(name,dimensions,measures) `method`

##### Summary

Tracks an event with the given name, dimensions, and measures.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the Event to track |
| dimensions | [RecordPoint.Connectors.SDK.Observability.Dimensions](#T-RecordPoint-Connectors-SDK-Observability-Dimensions 'RecordPoint.Connectors.SDK.Observability.Dimensions') | Dimensions to include in the event |
| measures | [RecordPoint.Connectors.SDK.Observability.Measures](#T-RecordPoint-Connectors-SDK-Observability-Measures 'RecordPoint.Connectors.SDK.Observability.Measures') | Measures to include in the event |

<a name='M-RecordPoint-Connectors-SDK-Observability-ITelemetrySink-TrackException-System-Exception,RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures-'></a>
### TrackException(exception,dimensions,measures) `method`

##### Summary

Tracks an exception with the given dimensions and measures.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| exception | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | The exception to track |
| dimensions | [RecordPoint.Connectors.SDK.Observability.Dimensions](#T-RecordPoint-Connectors-SDK-Observability-Dimensions 'RecordPoint.Connectors.SDK.Observability.Dimensions') | Dimensions to include with the exception |
| measures | [RecordPoint.Connectors.SDK.Observability.Measures](#T-RecordPoint-Connectors-SDK-Observability-Measures 'RecordPoint.Connectors.SDK.Observability.Measures') | Measures to include with the exception |

<a name='M-RecordPoint-Connectors-SDK-Observability-ITelemetrySink-TrackTrace-System-String,RecordPoint-Connectors-SDK-Observability-SeverityLevel,RecordPoint-Connectors-SDK-Observability-Dimensions-'></a>
### TrackTrace(message,severityLevel,dimensions) `method`

##### Summary

Tracks a trace message with the given severity level, message, and dimensions.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message to trace |
| severityLevel | [RecordPoint.Connectors.SDK.Observability.SeverityLevel](#T-RecordPoint-Connectors-SDK-Observability-SeverityLevel 'RecordPoint.Connectors.SDK.Observability.SeverityLevel') | The severity level of the trace |
| dimensions | [RecordPoint.Connectors.SDK.Observability.Dimensions](#T-RecordPoint-Connectors-SDK-Observability-Dimensions 'RecordPoint.Connectors.SDK.Observability.Dimensions') | Dimensions to include in the trace |

<a name='T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker'></a>
## ITelemetryTracker `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability

##### Summary

Interface for submitting a variety of statistics and measures about the system in a consistent and structured manner.

<a name='M-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker-BeginScope-RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures-'></a>
### BeginScope() `method`

##### Summary

Begins a new Observability scope

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker-TrackEvent-System-String,RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures-'></a>
### TrackEvent(name,dimensions,measures) `method`

##### Summary

Track a single occurance of an event with optional dimensions and associated measures.

Events should only be tracked upon completion (or failure) of a piece of work.
Avoid creating seperate start and end events. Each event should be entirely self-contained.

Individual events cannot and should not be correlated with other events.
They should not be exposing that level of detail (that's what logs and traces are for).

This function will never throw an exception. Any warnings or errors encountered will instead be logged.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The event name uniquely identifies the type of event to track, not the specific instance of the event.  Provide in the form of "[system].[component].[action]" or "[system].[component].[action].[subaction]" to make it standardised and user-readable. Use PascalCase for each identifier and limit to no more than 3 words. Keep it short but still understandable without context. e.g. "Eiger.AzureSearch.Sync", "Eiger.Item.Dispose", or "MachineLearning.Model.Train" or "Eiger.AzureSearch.Sync.Batch" |
| dimensions | [RecordPoint.Connectors.SDK.Observability.Dimensions](#T-RecordPoint-Connectors-SDK-Observability-Dimensions 'RecordPoint.Connectors.SDK.Observability.Dimensions') | These are the dimensions of the event which define high-level partitions or groupings of individual event instances. They are provided as key=value pairs. Dimensions are also known as properties.  Use dimensions to add additional context to an event which would be useful to filter on. e.g. "TenantId", "ConnectorId", "Success"  When considering which dimensions to add to an event:     + Do not add dimensions which always have the same value.       e.g. "BuildNumber" or "ServiceFabricVersion"       Such dimensions do not provide significant value while adding unnecessary costs and overhead.     + Do not add dimensions which contain logs, messages, or long strings.       Events are relatively lightweight and are not traces or exception logs.       As a rule of thumb, if it has a space in it then it shouldn't be added.     + In regard to the above, the more times the event occurs, the fewer and shorter the dimensions should be.     + If the property values are longer than a guid, then reconsider if the property is necessary.     + For numeric values, consider if a measure would be more appropriate.  Note that all events of the same type (i.e. with the same name) should have the same dimensions (but obviously can have different values for them). |
| measures | [RecordPoint.Connectors.SDK.Observability.Measures](#T-RecordPoint-Connectors-SDK-Observability-Measures 'RecordPoint.Connectors.SDK.Observability.Measures') | These are measurable numeric values associated with the event. Use measures to define values which can be graphed and have statistical operations performed upon them. These include counts, timings, and sizes. Metrics are calculated based on these values.  When considering which measures to add to an event:     + The unit should be clearly defined by the measure name.       e.g. "ItemsProcessed" is a count, "ProcessingTimeMs" is a timing in millieconds, "BinarySizeBytes" is a size in bytes.     + Timings should be specified in milliseconds.     + Sizes should be specified in bytes.     + Measures should relate to precisely this event.       - Do not add details relating to previous or future events (such as "NextSyncTime").         We can look at the previous events to determine that.     + Do not add cumulative progress information relating to parent events (such as "TotalProcessedSoFar").       - We can look at instances of the parent event type for a total.  Note that all events of the same type (i.e. with the same name) should have the same measure keys. |

<a name='M-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker-TrackException-System-Exception,RecordPoint-Connectors-SDK-Observability-Dimensions,RecordPoint-Connectors-SDK-Observability-Measures-'></a>
### TrackException(exception,dimensions,measures) `method`

##### Summary

Track an exception

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| exception | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Exception to track |
| dimensions | [RecordPoint.Connectors.SDK.Observability.Dimensions](#T-RecordPoint-Connectors-SDK-Observability-Dimensions 'RecordPoint.Connectors.SDK.Observability.Dimensions') | Dimensions associated with the event |
| measures | [RecordPoint.Connectors.SDK.Observability.Measures](#T-RecordPoint-Connectors-SDK-Observability-Measures 'RecordPoint.Connectors.SDK.Observability.Measures') | Measures associated with the event |

<a name='M-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker-TrackTrace-System-String,RecordPoint-Connectors-SDK-Observability-SeverityLevel,RecordPoint-Connectors-SDK-Observability-Dimensions-'></a>
### TrackTrace(message,severityLevel,dimensions) `method`

##### Summary

Logs a Trace message with optional dimensions.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The trace message to log |
| severityLevel | [RecordPoint.Connectors.SDK.Observability.SeverityLevel](#T-RecordPoint-Connectors-SDK-Observability-SeverityLevel 'RecordPoint.Connectors.SDK.Observability.SeverityLevel') | The severity level of the trace message |
| dimensions | [RecordPoint.Connectors.SDK.Observability.Dimensions](#T-RecordPoint-Connectors-SDK-Observability-Dimensions 'RecordPoint.Connectors.SDK.Observability.Dimensions') | Optional dimensions to include in the trace message |

<a name='T-RecordPoint-Connectors-SDK-Toggles-IToggleProvider'></a>
## IToggleProvider `type`

##### Namespace

RecordPoint.Connectors.SDK.Toggles

##### Summary

Defines a feature toggle provider

##### Remarks

A feature toggle provider is effectively a set of key-value pairs. This values
are used describe whether feature are available or not.

The expected use case is to provide feature toggles using launch darkly

<a name='M-RecordPoint-Connectors-SDK-Toggles-IToggleProvider-GetToggleBool-System-String,System-Boolean-'></a>
### GetToggleBool(toggle,default) `method`

##### Summary

Get a Boolean toggle value

##### Returns

Toggle value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| toggle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | toggle to get |
| default | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Default value |

<a name='M-RecordPoint-Connectors-SDK-Toggles-IToggleProvider-GetToggleBool-System-String,System-String,System-Boolean-'></a>
### GetToggleBool(toggle,userKey,default) `method`

##### Summary

Get a Boolean toggle value

##### Returns

Toggle value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| toggle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | toggle to get |
| userKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Targeted user key |
| default | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') | Default value |

<a name='M-RecordPoint-Connectors-SDK-Toggles-IToggleProvider-GetToggleNumber-System-String,System-String,System-Int32-'></a>
### GetToggleNumber(toggle,userKey,default) `method`

##### Summary

Get a number toggle value

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| toggle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | toggle to get |
| userKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Targeted user key |
| default | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Default value |

<a name='M-RecordPoint-Connectors-SDK-Toggles-IToggleProvider-GetToggleNumber-System-String,System-Int32-'></a>
### GetToggleNumber(toggle,default) `method`

##### Summary

Get a number toggle value

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| toggle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | toggle to get |
| default | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | Default value |

<a name='M-RecordPoint-Connectors-SDK-Toggles-IToggleProvider-GetToggleString-System-String,System-String,System-String-'></a>
### GetToggleString(toggle,userKey,default) `method`

##### Summary

Get a string toggle value

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| toggle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | toggle to get |
| userKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Targeted user key |
| default | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Default value |

<a name='M-RecordPoint-Connectors-SDK-Toggles-IToggleProvider-GetToggleString-System-String,System-String-'></a>
### GetToggleString(toggle,default) `method`

##### Summary

Get a string toggle value

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| toggle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | toggle to get |
| default | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Default value |

<a name='T-RecordPoint-Connectors-SDK-Work-IWork'></a>
## IWork `type`

##### Namespace

RecordPoint.Connectors.SDK.Work

##### Summary

Definition of generic work

<a name='P-RecordPoint-Connectors-SDK-Work-IWork-Exception'></a>
### Exception `property`

##### Summary

Optional exception that caused the work to fail or be abandoned

<a name='P-RecordPoint-Connectors-SDK-Work-IWork-FinishDateTime'></a>
### FinishDateTime `property`

##### Summary

The time that we got a result

<a name='P-RecordPoint-Connectors-SDK-Work-IWork-HasResult'></a>
### HasResult `property`

##### Summary

Has a result been recorded

<a name='P-RecordPoint-Connectors-SDK-Work-IWork-Id'></a>
### Id `property`

##### Summary

Unique Work ID that identifies the unit of work

<a name='P-RecordPoint-Connectors-SDK-Work-IWork-ResultReason'></a>
### ResultReason `property`

##### Summary

Optional message that describes why a particular result was reached

<a name='P-RecordPoint-Connectors-SDK-Work-IWork-ResultType'></a>
### ResultType `property`

##### Summary

Work Result Type

<a name='P-RecordPoint-Connectors-SDK-Work-IWork-StartDateTime'></a>
### StartDateTime `property`

##### Summary

Time the work item was started

<a name='P-RecordPoint-Connectors-SDK-Work-IWork-WorkDuration'></a>
### WorkDuration `property`

##### Summary

Time it took to do the work

<a name='P-RecordPoint-Connectors-SDK-Work-IWork-WorkType'></a>
### WorkType `property`

##### Summary

Unique name that defines the Work Item

<a name='T-RecordPoint-Connectors-SDK-Work-IWorkQueueClient'></a>
## IWorkQueueClient `type`

##### Namespace

RecordPoint.Connectors.SDK.Work

##### Summary

Defines the client for a work queue

<a name='M-RecordPoint-Connectors-SDK-Work-IWorkQueueClient-SubmitWorkAsync-RecordPoint-Connectors-SDK-Work-WorkRequest,System-Threading-CancellationToken-'></a>
### SubmitWorkAsync(workRequest,cancellationToken) `method`

##### Summary

Submit work

##### Returns

Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| workRequest | [RecordPoint.Connectors.SDK.Work.WorkRequest](#T-RecordPoint-Connectors-SDK-Work-WorkRequest 'RecordPoint.Connectors.SDK.Work.WorkRequest') | Work request to submit |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation Token |

<a name='T-RecordPoint-Connectors-SDK-Work-IWork`1'></a>
## IWork\`1 `type`

##### Namespace

RecordPoint.Connectors.SDK.Work

##### Summary

Definition of unmanaged work that can be executed within the context of the executing process.

<a name='P-RecordPoint-Connectors-SDK-Work-IWork`1-Parameter'></a>
### Parameter `property`

##### Summary

Generic parameter

<a name='M-RecordPoint-Connectors-SDK-Work-IWork`1-RunAsync-`0,System-Threading-CancellationToken-'></a>
### RunAsync(parameter,cancellationToken) `method`

##### Summary

Asynchronously executes the work item

##### Returns

Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| parameter | [\`0](#T-`0 '`0') | Work item parameter |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation Token |

<a name='T-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel'></a>
## ManagedWorkStatusModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Work

##### Summary

DTO model for the Managed Work Status

<a name='P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-Configuration'></a>
### Configuration `property`

##### Summary

Work configuation encoded as a string. The format depends on the work type.

<a name='P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-ConfigurationType'></a>
### ConfigurationType `property`

##### Summary

Unique string that identifies the type of the configuration, which may vary between different versions of the application

<a name='P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-ConnectorId'></a>
### ConnectorId `property`

##### Summary

Id of the connector the work belongs to

<a name='P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-ExponentialRetryDelay'></a>
### ExponentialRetryDelay `property`

##### Summary

Enables an exponential backoff to retry attempts (default: true)

<a name='P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-Id'></a>
### Id `property`

##### Summary

Id that uniquely identifies the work

<a name='P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-LastStatusUpdate'></a>
### LastStatusUpdate `property`

##### Summary

The Time the Works status was last updated.  
This provides a watchdog type function, so work that is "lost" can be cancelled and restarted after it times out.

<a name='P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-MaxRetries'></a>
### MaxRetries `property`

##### Summary

The maximum number of times a failed work operation should be retried before being sent to the dead letter queue. This can be set to -1 for never being dead lettered

<a name='P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-MaxRetryDelay'></a>
### MaxRetryDelay `property`

##### Summary

The maximum time a retry delay can be in seconds. This is to stop the exponential delay from getting too long. (default: 1 hour or 3600)

<a name='P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-RetryDelay'></a>
### RetryDelay `property`

##### Summary

The amount of time on seconds between retries of a failed work operation

<a name='P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-RetryOnFailure'></a>
### RetryOnFailure `property`

##### Summary

Flag to support deadlettering of work

<a name='P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-State'></a>
### State `property`

##### Summary

Work state encoded as a string. The format depends on the work type.

<a name='P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-StateType'></a>
### StateType `property`

##### Summary

Unique string that identifies the type of state

##### Remarks

The primary use case of this field if providing backwards compatibility for work
created in earlier versions of the application.

By setting it to a version specific string new versions of an application can
detect messages created by earlier versions and transparently upgrade them.

<a name='P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-Status'></a>
### Status `property`

##### Summary

Status of the work

<a name='P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-WorkId'></a>
### WorkId `property`

##### Summary

Current Work Id we are expecting to execute

<a name='P-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-WorkType'></a>
### WorkType `property`

##### Summary

String that identifies the type of work

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-CopyTo-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-'></a>
### CopyTo(target) `method`

##### Summary

Copy this work state to the target

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| target | [RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel](#T-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel 'RecordPoint.Connectors.SDK.Work.ManagedWorkStatusModel') | Object to copy to |

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-Deserialize-System-String-'></a>
### Deserialize() `method`

##### Summary

Deserialize the work status

##### Returns

Work Status converted from Json

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-ManagedWorkStatusModel-Serialize'></a>
### Serialize() `method`

##### Summary

Serialize the work status

##### Returns

Managed Work Status converted to Json

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Work-ManagedWorkStatuses'></a>
## ManagedWorkStatuses `type`

##### Namespace

RecordPoint.Connectors.SDK.Work

##### Summary

Managed Work statuses

<a name='F-RecordPoint-Connectors-SDK-Work-ManagedWorkStatuses-Abandoned'></a>
### Abandoned `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Work-ManagedWorkStatuses-Complete'></a>
### Complete `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Work-ManagedWorkStatuses-Failed'></a>
### Failed `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Work-ManagedWorkStatuses-Running'></a>
### Running `constants`

##### Summary



<a name='T-RecordPoint-Connectors-SDK-Observability-Measures'></a>
## Measures `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability

##### Summary

The measures.

<a name='M-RecordPoint-Connectors-SDK-Observability-Measures-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the [Measures](#T-RecordPoint-Connectors-SDK-Observability-Measures 'RecordPoint.Connectors.SDK.Observability.Measures') class.

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Observability-Measures-#ctor-System-Collections-Generic-IEnumerable{System-Collections-Generic-KeyValuePair{System-String,System-Double}}-'></a>
### #ctor(collection) `constructor`

##### Summary

Initializes a new instance of the [Measures](#T-RecordPoint-Connectors-SDK-Observability-Measures 'RecordPoint.Connectors.SDK.Observability.Measures') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| collection | [System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.String,System.Double}}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.String,System.Double}}') | The collection. |

<a name='T-RecordPoint-Connectors-SDK-Content-MetaDataItem'></a>
## MetaDataItem `type`

##### Namespace

RecordPoint.Connectors.SDK.Content

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Content-MetaDataItem-MetaDataItemType'></a>
### MetaDataItemType `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Content-MetaDataItem-Name'></a>
### Name `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Content-MetaDataItem-Type'></a>
### Type `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Content-MetaDataItem-Value'></a>
### Value `property`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Content-MetaDataItem-Equals-RecordPoint-Connectors-SDK-Content-MetaDataItem-'></a>
### Equals(other) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| other | [RecordPoint.Connectors.SDK.Content.MetaDataItem](#T-RecordPoint-Connectors-SDK-Content-MetaDataItem 'RecordPoint.Connectors.SDK.Content.MetaDataItem') |  |

<a name='M-RecordPoint-Connectors-SDK-Content-MetaDataItem-Equals-System-Object-'></a>
### Equals(obj) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| obj | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |

<a name='M-RecordPoint-Connectors-SDK-Content-MetaDataItem-GetHashCode'></a>
### GetHashCode() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Content-MetaDataItemExtensions'></a>
## MetaDataItemExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Content

##### Summary

The meta data item extensions.

<a name='M-RecordPoint-Connectors-SDK-Content-MetaDataItemExtensions-IsEqual-System-Collections-Generic-List{RecordPoint-Connectors-SDK-Content-MetaDataItem},System-Collections-Generic-List{RecordPoint-Connectors-SDK-Content-MetaDataItem}-'></a>
### IsEqual(x,y) `method`

##### Summary

Checks if is equal.

##### Returns

A bool

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| x | [System.Collections.Generic.List{RecordPoint.Connectors.SDK.Content.MetaDataItem}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{RecordPoint.Connectors.SDK.Content.MetaDataItem}') | The X. |
| y | [System.Collections.Generic.List{RecordPoint.Connectors.SDK.Content.MetaDataItem}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.List 'System.Collections.Generic.List{RecordPoint.Connectors.SDK.Content.MetaDataItem}') | The Y. |

<a name='T-RecordPoint-Connectors-SDK-Content-MetaDataItemType'></a>
## MetaDataItemType `type`

##### Namespace

RecordPoint.Connectors.SDK.Content

##### Summary

The metas data item types.

<a name='F-RecordPoint-Connectors-SDK-Content-MetaDataItemType-Internal'></a>
### Internal `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Content-MetaDataItemType-R365MetaData'></a>
### R365MetaData `constants`

##### Summary



<a name='T-RecordPoint-Connectors-SDK-Notifications-Notification'></a>
## Notification `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications

##### Summary

Records 365 notification

<a name='M-RecordPoint-Connectors-SDK-Notifications-Notification-#ctor-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel-'></a>
### #ctor(value) `constructor`

##### Summary

Initializes a new instance of the [Notification](#T-RecordPoint-Connectors-SDK-Notifications-Notification 'RecordPoint.Connectors.SDK.Notifications.Notification') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel') | The value. |

<a name='P-RecordPoint-Connectors-SDK-Notifications-Notification-NotificationType'></a>
### NotificationType `property`

##### Summary

Gets or sets the notification type.

<a name='P-RecordPoint-Connectors-SDK-Notifications-Notification-Value'></a>
### Value `property`

##### Summary

Gets the value.

<a name='T-RecordPoint-Connectors-SDK-Notifications-NotificationOutcome'></a>
## NotificationOutcome `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications

##### Summary

Outcome of a notification

<a name='P-RecordPoint-Connectors-SDK-Notifications-NotificationOutcome-OutcomeType'></a>
### OutcomeType `property`

##### Summary

Notification outcome type

<a name='P-RecordPoint-Connectors-SDK-Notifications-NotificationOutcome-Reason'></a>
### Reason `property`

##### Summary

Message that describes the reason for the outcome

<a name='M-RecordPoint-Connectors-SDK-Notifications-NotificationOutcome-Failed-System-String-'></a>
### Failed() `method`

##### Summary

Notification handling has failed

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Notifications-NotificationOutcome-OK'></a>
### OK() `method`

##### Summary

Unit of work has completed succcessfully

##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Notifications-NotificationOutcomeType'></a>
## NotificationOutcomeType `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications

##### Summary

Notification outcome type

<a name='F-RecordPoint-Connectors-SDK-Notifications-NotificationOutcomeType-Failed'></a>
### Failed `constants`

##### Summary

Notification has failed.

<a name='F-RecordPoint-Connectors-SDK-Notifications-NotificationOutcomeType-Ok'></a>
### Ok `constants`

##### Summary

Notification was successfully processed

<a name='T-RecordPoint-Connectors-SDK-Notifications-NotificationsPollerOptions'></a>
## NotificationsPollerOptions `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications

##### Summary

Notifications poller options

<a name='P-RecordPoint-Connectors-SDK-Notifications-NotificationsPollerOptions-PollIntervalSeconds'></a>
### PollIntervalSeconds `property`

##### Summary

Poll interval in seconds

<a name='T-RecordPoint-Connectors-SDK-Configuration-R365ConfigurationModel'></a>
## R365ConfigurationModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Configuration

##### Summary

Records 365 data model

<a name='P-RecordPoint-Connectors-SDK-Configuration-R365ConfigurationModel-Audience'></a>
### Audience `property`

##### Summary

Audience

<a name='P-RecordPoint-Connectors-SDK-Configuration-R365ConfigurationModel-ClientId'></a>
### ClientId `property`

##### Summary

Client Id

<a name='P-RecordPoint-Connectors-SDK-Configuration-R365ConfigurationModel-ClientSecret'></a>
### ClientSecret `property`

##### Summary

Client Secret

<a name='P-RecordPoint-Connectors-SDK-Configuration-R365ConfigurationModel-ConnectorApiUrl'></a>
### ConnectorApiUrl `property`

##### Summary

The base url of the Records365 Connector API.

<a name='P-RecordPoint-Connectors-SDK-Configuration-R365ConfigurationModel-ServerCertificateValidation'></a>
### ServerCertificateValidation `property`

##### Summary

Set to false to skip validation of the server SSL certificate at the
Records365 vNext Connector API endpoint, true otherwise.

<a name='T-RecordPoint-Connectors-SDK-Configuration-R365MultiConfigurationModel'></a>
## R365MultiConfigurationModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Configuration

##### Summary

A collection of configurations for submitting to R365.
The key should be the ConnectorTypeConfigurationId or "Default".
Use "Default" when the ConnectorTypeConfigurationId is null or empty for your Connector Config.
This is optional and can be used to support multiple configurations.

<a name='F-RecordPoint-Connectors-SDK-Configuration-R365MultiConfigurationModel-DefaultConfigurationKey'></a>
### DefaultConfigurationKey `constants`

##### Summary

The intended default configuration key

<a name='T-RecordPoint-Connectors-SDK-Content-Record'></a>
## Record `type`

##### Namespace

RecordPoint.Connectors.SDK.Content

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Content-Record-Binaries'></a>
### Binaries `property`

##### Summary

Associated binaries

<a name='P-RecordPoint-Connectors-SDK-Content-Record-FileSize'></a>
### FileSize `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Content-Record-Location'></a>
### Location `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Content-Record-MediaType'></a>
### MediaType `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Content-Record-MimeType'></a>
### MimeType `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Content-Record-ParentExternalId'></a>
### ParentExternalId `property`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Content-Record-Equals-RecordPoint-Connectors-SDK-Content-Record-'></a>
### Equals(other) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| other | [RecordPoint.Connectors.SDK.Content.Record](#T-RecordPoint-Connectors-SDK-Content-Record 'RecordPoint.Connectors.SDK.Content.Record') |  |

<a name='M-RecordPoint-Connectors-SDK-Content-Record-Equals-System-Object-'></a>
### Equals() `method`

##### Summary



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Content-Record-GetHashCode'></a>
### GetHashCode() `method`

##### Summary



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalResult'></a>
## RecordDisposalResult `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

The result of an attempt to dispose a record

<a name='P-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalResult-Exception'></a>
### Exception `property`

##### Summary

Optional exception that caused the disposal to fail

<a name='P-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalResult-Reason'></a>
### Reason `property`

##### Summary

Result reason

<a name='P-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalResult-ResultType'></a>
### ResultType `property`

##### Summary

Result type

<a name='T-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalResultType'></a>
## RecordDisposalResultType `type`

##### Namespace

RecordPoint.Connectors.SDK.ContentManager

##### Summary

Possible results of record disposal action

<a name='F-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalResultType-BackOff'></a>
### BackOff `constants`

##### Summary

Content Source has throttled requests

<a name='F-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalResultType-Complete'></a>
### Complete `constants`

##### Summary

We successfully disposed of the record

<a name='F-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalResultType-Deleted'></a>
### Deleted `constants`

##### Summary

The record cannot be disposed because it has already been deleted

<a name='F-RecordPoint-Connectors-SDK-ContentManager-RecordDisposalResultType-Failed'></a>
### Failed `constants`

##### Summary

We failed to dispose of the record

<a name='T-RecordPoint-Connectors-SDK-RequiredValueNullException'></a>
## RequiredValueNullException `type`

##### Namespace

RecordPoint.Connectors.SDK

##### Summary

The required value null exception.

<a name='M-RecordPoint-Connectors-SDK-RequiredValueNullException-#ctor-System-String-'></a>
### #ctor(paramName) `constructor`

##### Summary

Initializes a new instance of the [RequiredValueNullException](#T-RecordPoint-Connectors-SDK-RequiredValueNullException 'RecordPoint.Connectors.SDK.RequiredValueNullException') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| paramName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The param name. |

<a name='F-RecordPoint-Connectors-SDK-RequiredValueNullException-NULL_VALUE_MESSAGE'></a>
### NULL_VALUE_MESSAGE `constants`

##### Summary

The NULL VALUE MESSAGE.

<a name='F-RecordPoint-Connectors-SDK-RequiredValueNullException-_paramName'></a>
### _paramName `constants`

##### Summary

The param name.

<a name='P-RecordPoint-Connectors-SDK-RequiredValueNullException-ParamName'></a>
### ParamName `property`

##### Summary

Gets the param name.

<a name='M-RecordPoint-Connectors-SDK-RequiredValueNullException-ThrowIfNull-System-Object,System-String-'></a>
### ThrowIfNull(value,paramName) `method`

##### Summary

Throw if null.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The value. |
| paramName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The param name. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [RecordPoint.Connectors.SDK.RequiredValueNullException](#T-RecordPoint-Connectors-SDK-RequiredValueNullException 'RecordPoint.Connectors.SDK.RequiredValueNullException') |  |

<a name='M-RecordPoint-Connectors-SDK-RequiredValueNullException-ThrowIfNullOrEmpty-System-String,System-String-'></a>
### ThrowIfNullOrEmpty(value,paramName) `method`

##### Summary

Throw if null or empty.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The value. |
| paramName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The param name. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [RecordPoint.Connectors.SDK.RequiredValueNullException](#T-RecordPoint-Connectors-SDK-RequiredValueNullException 'RecordPoint.Connectors.SDK.RequiredValueNullException') |  |

<a name='T-RecordPoint-Connectors-SDK-RequiredValueOutOfRangeException'></a>
## RequiredValueOutOfRangeException `type`

##### Namespace

RecordPoint.Connectors.SDK

##### Summary

The required value out of range exception.

<a name='M-RecordPoint-Connectors-SDK-RequiredValueOutOfRangeException-#ctor-System-String-'></a>
### #ctor(paramName) `constructor`

##### Summary

Initializes a new instance of the [RequiredValueOutOfRangeException](#T-RecordPoint-Connectors-SDK-RequiredValueOutOfRangeException 'RecordPoint.Connectors.SDK.RequiredValueOutOfRangeException') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| paramName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The param name. |

<a name='F-RecordPoint-Connectors-SDK-RequiredValueOutOfRangeException-NULL_VALUE_MESSAGE'></a>
### NULL_VALUE_MESSAGE `constants`

##### Summary

The NULL VALUE MESSAGE.

<a name='F-RecordPoint-Connectors-SDK-RequiredValueOutOfRangeException-_paramName'></a>
### _paramName `constants`

##### Summary

The param name.

<a name='P-RecordPoint-Connectors-SDK-RequiredValueOutOfRangeException-ParamName'></a>
### ParamName `property`

##### Summary

Gets the param name.

<a name='T-RecordPoint-Connectors-SDK-Caching-Semaphore-SemaphoreLockType'></a>
## SemaphoreLockType `type`

##### Namespace

RecordPoint.Connectors.SDK.Caching.Semaphore

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Caching-Semaphore-SemaphoreLockType-Global'></a>
### Global `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Caching-Semaphore-SemaphoreLockType-Scoped'></a>
### Scoped `constants`

##### Summary



<a name='T-RecordPoint-Connectors-SDK-Observability-SeverityLevel'></a>
## SeverityLevel `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability

##### Summary

Trace Logging Severity Levels

<a name='F-RecordPoint-Connectors-SDK-Observability-SeverityLevel-Critical'></a>
### Critical `constants`

##### Summary

Critical severity level.

<a name='F-RecordPoint-Connectors-SDK-Observability-SeverityLevel-Error'></a>
### Error `constants`

##### Summary

Error severity level.

<a name='F-RecordPoint-Connectors-SDK-Observability-SeverityLevel-Information'></a>
### Information `constants`

##### Summary

Information severity level.

<a name='F-RecordPoint-Connectors-SDK-Observability-SeverityLevel-None'></a>
### None `constants`

##### Summary

Do not log anything.

<a name='F-RecordPoint-Connectors-SDK-Observability-SeverityLevel-Verbose'></a>
### Verbose `constants`

##### Summary

Verbose severity level.

<a name='F-RecordPoint-Connectors-SDK-Observability-SeverityLevel-Warning'></a>
### Warning `constants`

##### Summary

Warning severity level.

<a name='T-RecordPoint-Connectors-SDK-Observability-StandardDimensions'></a>
## StandardDimensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability

##### Summary

Set of standard cross-cutting dimensions

<a name='F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-ACTION_RESULT_REASON'></a>
### ACTION_RESULT_REASON `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-CHANNEL_EXTERNAL_ID'></a>
### CHANNEL_EXTERNAL_ID `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-CHANNEL_TITLE'></a>
### CHANNEL_TITLE `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-COMPANY'></a>
### COMPANY `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-CONNECTOR_ID'></a>
### CONNECTOR_ID `constants`

##### Summary

/

<a name='F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-DECISON'></a>
### DECISON `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-DEPENDANCY'></a>
### DEPENDANCY `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-DEPENDANCY_TYPE'></a>
### DEPENDANCY_TYPE `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-EVENT_TYPE'></a>
### EVENT_TYPE `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-EXCEPTION'></a>
### EXCEPTION `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-EXTERNAL_ID'></a>
### EXTERNAL_ID `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-OUTCOME'></a>
### OUTCOME `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-OUTCOME_REASON'></a>
### OUTCOME_REASON `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-SERVICE'></a>
### SERVICE `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-SERVICE_ID'></a>
### SERVICE_ID `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-SYSTEM'></a>
### SYSTEM `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-TASK'></a>
### TASK `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-TENANT_DOMAIN_NAME'></a>
### TENANT_DOMAIN_NAME `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-TENANT_ID'></a>
### TENANT_ID `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-WORK'></a>
### WORK `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-WORK_COMPLETE'></a>
### WORK_COMPLETE `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Observability-StandardDimensions-WORK_ID'></a>
### WORK_ID `constants`

##### Summary



<a name='T-RecordPoint-Connectors-SDK-Observability-StandardMeasures'></a>
## StandardMeasures `type`

##### Namespace

RecordPoint.Connectors.SDK.Observability

##### Summary

Set of standard observability measures

<a name='F-RecordPoint-Connectors-SDK-Observability-StandardMeasures-ACTION_EXECUTION_SECONDS'></a>
### ACTION_EXECUTION_SECONDS `constants`

##### Summary

How many seconds it took to complete a major task, normally within a unit of work

<a name='F-RecordPoint-Connectors-SDK-Observability-StandardMeasures-AGGREGATION_COUNT'></a>
### AGGREGATION_COUNT `constants`

##### Summary

How many aggregations were processed?

<a name='F-RecordPoint-Connectors-SDK-Observability-StandardMeasures-AUDIT_COUNT'></a>
### AUDIT_COUNT `constants`

##### Summary

How many audit events were processed?

<a name='F-RecordPoint-Connectors-SDK-Observability-StandardMeasures-BINARY_COUNT'></a>
### BINARY_COUNT `constants`

##### Summary

How many binaries were processed?

<a name='F-RecordPoint-Connectors-SDK-Observability-StandardMeasures-OUTCOME_SECONDS'></a>
### OUTCOME_SECONDS `constants`

##### Summary

How many seconds it took from when a request was made, till it was completed.
Includes time spent in queues, getting retried, etc.

<a name='F-RecordPoint-Connectors-SDK-Observability-StandardMeasures-RECORD_COUNT'></a>
### RECORD_COUNT `constants`

##### Summary

How many records were processed?

<a name='F-RecordPoint-Connectors-SDK-Observability-StandardMeasures-SUBMIT_SECONDS'></a>
### SUBMIT_SECONDS `constants`

##### Summary

How many seconds it took for an operation to submit new work operations

<a name='F-RecordPoint-Connectors-SDK-Observability-StandardMeasures-WORK_SECONDS'></a>
### WORK_SECONDS `constants`

##### Summary

How many seconds something was actually worked on

<a name='T-RecordPoint-Connectors-SDK-Status-StatusModel'></a>
## StatusModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Status

##### Summary

The status model.

<a name='P-RecordPoint-Connectors-SDK-Status-StatusModel-ConnectorId'></a>
### ConnectorId `property`

##### Summary

Gets or sets the connector id.

<a name='P-RecordPoint-Connectors-SDK-Status-StatusModel-Status'></a>
### Status `property`

##### Summary

Gets or sets the status.

<a name='T-RecordPoint-Connectors-SDK-Abstractions-ContentManager-SubmissionActionType'></a>
## SubmissionActionType `type`

##### Namespace

RecordPoint.Connectors.SDK.Abstractions.ContentManager

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Abstractions-ContentManager-SubmissionActionType-PostSubmit'></a>
### PostSubmit `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Abstractions-ContentManager-SubmissionActionType-PreSubmit'></a>
### PreSubmit `constants`

##### Summary



<a name='T-RecordPoint-Connectors-SDK-Health-SystemMemory'></a>
## SystemMemory `type`

##### Namespace

RecordPoint.Connectors.SDK.Health

##### Summary

The system memory.

<a name='P-RecordPoint-Connectors-SDK-Health-SystemMemory-FreePhyiscalMemory'></a>
### FreePhyiscalMemory `property`

##### Summary

Gets or sets the free phyiscal memory.

<a name='P-RecordPoint-Connectors-SDK-Health-SystemMemory-FreeVirtualMemory'></a>
### FreeVirtualMemory `property`

##### Summary

Gets or sets the free virtual memory.

<a name='P-RecordPoint-Connectors-SDK-Health-SystemMemory-TotalPhysicalMemorySize'></a>
### TotalPhysicalMemorySize `property`

##### Summary

Gets or sets the total physical memory size.

<a name='P-RecordPoint-Connectors-SDK-Health-SystemMemory-TotalVirtualMemorySize'></a>
### TotalVirtualMemorySize `property`

##### Summary

Gets or sets the total virtual memory size.

<a name='T-RecordPoint-Connectors-SDK-Context-SystemOptions'></a>
## SystemOptions `type`

##### Namespace

RecordPoint.Connectors.SDK.Context

##### Summary

On premise system options

<a name='F-RecordPoint-Connectors-SDK-Context-SystemOptions-SECTION_NAME'></a>
### SECTION_NAME `constants`

##### Summary

Configuration section name

<a name='P-RecordPoint-Connectors-SDK-Context-SystemOptions-CompanyName'></a>
### CompanyName `property`

##### Summary

Company name

<a name='P-RecordPoint-Connectors-SDK-Context-SystemOptions-ConnectorName'></a>
### ConnectorName `property`

##### Summary

System name

<a name='P-RecordPoint-Connectors-SDK-Context-SystemOptions-DataPathRoot'></a>
### DataPathRoot `property`

##### Summary

Root path for the connectors data.

##### Remarks

The system context has a fall-back default if the data path is not defined

<a name='P-RecordPoint-Connectors-SDK-Context-SystemOptions-ServiceName'></a>
### ServiceName `property`

##### Summary

Service name

<a name='P-RecordPoint-Connectors-SDK-Context-SystemOptions-ShortName'></a>
### ShortName `property`

##### Summary

Short name

<a name='T-RecordPoint-Connectors-SDK-Work-UnknownWorkRequestException'></a>
## UnknownWorkRequestException `type`

##### Namespace

RecordPoint.Connectors.SDK.Work

##### Summary

Exception for when an unknown exception is thrown attempting to handle a Work Request

<a name='M-RecordPoint-Connectors-SDK-Work-UnknownWorkRequestException-#ctor-System-Exception-'></a>
### #ctor() `constructor`

##### Summary

Exception for when an unknown exception is thrown attempting to handle a Work Request

##### Parameters

This constructor has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Work-WorkRequest'></a>
## WorkRequest `type`

##### Namespace

RecordPoint.Connectors.SDK.Work

##### Summary

Request to execute a unit of work

<a name='P-RecordPoint-Connectors-SDK-Work-WorkRequest-Body'></a>
### Body `property`

##### Summary

Work Request Body

<a name='P-RecordPoint-Connectors-SDK-Work-WorkRequest-ConnectorConfigId'></a>
### ConnectorConfigId `property`

##### Summary

Connector the work relates to.

<a name='P-RecordPoint-Connectors-SDK-Work-WorkRequest-FaultedCount'></a>
### FaultedCount `property`

##### Summary

Number of faulted count for a work request.

<a name='P-RecordPoint-Connectors-SDK-Work-WorkRequest-MustFinishDateTime'></a>
### MustFinishDateTime `property`

##### Summary

Time we must finish work by

<a name='P-RecordPoint-Connectors-SDK-Work-WorkRequest-SubmitDateTime'></a>
### SubmitDateTime `property`

##### Summary

Time the request was submitted

<a name='P-RecordPoint-Connectors-SDK-Work-WorkRequest-TenantDomainName'></a>
### TenantDomainName `property`

##### Summary

Tenant domain name

<a name='P-RecordPoint-Connectors-SDK-Work-WorkRequest-TenantId'></a>
### TenantId `property`

##### Summary

Tenant Id

<a name='P-RecordPoint-Connectors-SDK-Work-WorkRequest-WaitTill'></a>
### WaitTill `property`

##### Summary

Optional time to wait to submit the work

<a name='P-RecordPoint-Connectors-SDK-Work-WorkRequest-WorkId'></a>
### WorkId `property`

##### Summary

Unique Work ID that identifies the unit of work

<a name='P-RecordPoint-Connectors-SDK-Work-WorkRequest-WorkType'></a>
### WorkType `property`

##### Summary

Work Type

<a name='T-RecordPoint-Connectors-SDK-Work-WorkResult'></a>
## WorkResult `type`

##### Namespace

RecordPoint.Connectors.SDK.Work

##### Summary

Result of completing work

<a name='P-RecordPoint-Connectors-SDK-Work-WorkResult-Duration'></a>
### Duration `property`

##### Summary

Duration of the unit of work

<a name='P-RecordPoint-Connectors-SDK-Work-WorkResult-Exception'></a>
### Exception `property`

##### Summary

Exception that caused the work to fail

<a name='P-RecordPoint-Connectors-SDK-Work-WorkResult-Measures'></a>
### Measures `property`

##### Summary

Additional observability measures

<a name='P-RecordPoint-Connectors-SDK-Work-WorkResult-Reason'></a>
### Reason `property`

##### Summary

Result reason

<a name='P-RecordPoint-Connectors-SDK-Work-WorkResult-ResultType'></a>
### ResultType `property`

##### Summary

Work result type

<a name='P-RecordPoint-Connectors-SDK-Work-WorkResult-WaitTill'></a>
### WaitTill `property`

##### Summary

Wait till

<a name='M-RecordPoint-Connectors-SDK-Work-WorkResult-Abandoned-System-String-'></a>
### Abandoned() `method`

##### Summary

Unit of work has been abandoned

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-WorkResult-Abandoned-System-String,System-Exception-'></a>
### Abandoned() `method`

##### Summary

Unit of work has been abandoned

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-WorkResult-Complete-System-String-'></a>
### Complete() `method`

##### Summary

Unit of work has completed succcessfully

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-WorkResult-DeadLetter-System-String,System-Exception-'></a>
### DeadLetter() `method`

##### Summary

Unit of work will be deadletter

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-WorkResult-Defer-System-String,System-Nullable{System-DateTimeOffset}-'></a>
### Defer() `method`

##### Summary

Defer processing till a later time

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-WorkResult-Failed-System-String-'></a>
### Failed() `method`

##### Summary

Unit of work has critically failed

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-WorkResult-Failed-System-Exception-'></a>
### Failed() `method`

##### Summary

Unit of work has critically failed due to an exception

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Work-WorkResult-Failed-System-String,System-Exception-'></a>
### Failed() `method`

##### Summary

Unit of work has critically failed due to an exception

##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Abstractions-Work-WorkResultException'></a>
## WorkResultException `type`

##### Namespace

RecordPoint.Connectors.SDK.Abstractions.Work

##### Summary

Represents errors that occur during work result processing.

<a name='M-RecordPoint-Connectors-SDK-Abstractions-Work-WorkResultException-#ctor-System-String-'></a>
### #ctor(message) `constructor`

##### Summary

Initializes a new instance of the [WorkResultException](#T-RecordPoint-Connectors-SDK-Abstractions-Work-WorkResultException 'RecordPoint.Connectors.SDK.Abstractions.Work.WorkResultException') class with a specified error message.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message that describes the error. |

<a name='M-RecordPoint-Connectors-SDK-Abstractions-Work-WorkResultException-#ctor-System-String,System-Exception-'></a>
### #ctor(message,innerException) `constructor`

##### Summary

Initializes a new instance of the [WorkResultException](#T-RecordPoint-Connectors-SDK-Abstractions-Work-WorkResultException 'RecordPoint.Connectors.SDK.Abstractions.Work.WorkResultException') class with a specified error message and a reference to the inner exception that is the cause of this exception.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The message that describes the error. |
| innerException | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | The exception that is the cause of the current exception, or a null reference if no inner exception is specified. |

<a name='T-RecordPoint-Connectors-SDK-Work-WorkResultType'></a>
## WorkResultType `type`

##### Namespace

RecordPoint.Connectors.SDK.Work

##### Summary

Work results

<a name='F-RecordPoint-Connectors-SDK-Work-WorkResultType-Abandoned'></a>
### Abandoned `constants`

##### Summary

Unit of work was abandoned and should be retried

<a name='F-RecordPoint-Connectors-SDK-Work-WorkResultType-Complete'></a>
### Complete `constants`

##### Summary

Unit of work completed sucessfully

<a name='F-RecordPoint-Connectors-SDK-Work-WorkResultType-DeadLetter'></a>
### DeadLetter `constants`

##### Summary

Unit of work should move to dead letter immediately

<a name='F-RecordPoint-Connectors-SDK-Work-WorkResultType-Deferred'></a>
### Deferred `constants`

##### Summary

Unit of work should be deferred till a later time

<a name='F-RecordPoint-Connectors-SDK-Work-WorkResultType-Failed'></a>
### Failed `constants`

##### Summary

Unit of work has critically failed
