<a name='assembly'></a>
# RecordPoint.Connectors.SDK.Client.Abstractions

## Contents

- [AggregationSubmissionInputModel](#T-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-#ctor 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel.#ctor')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-#ctor-System-String,System-String,System-String,System-DateTime,System-String,System-DateTime,System-String,System-Nullable{System-Int32},System-String,System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel},System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-RelationshipDataModel},System-String,System-String,System-String,System-String,System-String,System-String,System-String,System-String- 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel.#ctor(System.String,System.String,System.String,System.DateTime,System.String,System.DateTime,System.String,System.Nullable{System.Int32},System.String,System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.SubmissionMetaDataModel},System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.RelationshipDataModel},System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String)')
  - [Author](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-Author 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel.Author')
  - [BarcodeType](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-BarcodeType 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel.BarcodeType')
  - [BarcodeValue](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-BarcodeValue 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel.BarcodeValue')
  - [ConnectorId](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-ConnectorId 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel.ConnectorId')
  - [CorrelationId](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-CorrelationId 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel.CorrelationId')
  - [ExternalId](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-ExternalId 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel.ExternalId')
  - [ItemTypeId](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-ItemTypeId 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel.ItemTypeId')
  - [Location](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-Location 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel.Location')
  - [MediaType](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-MediaType 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel.MediaType')
  - [ParentExternalId](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-ParentExternalId 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel.ParentExternalId')
  - [RecordCategoryId](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-RecordCategoryId 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel.RecordCategoryId')
  - [Relationships](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-Relationships 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel.Relationships')
  - [SecurityProfileIdentifier](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-SecurityProfileIdentifier 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel.SecurityProfileIdentifier')
  - [SourceCreatedBy](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-SourceCreatedBy 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel.SourceCreatedBy')
  - [SourceCreatedDate](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-SourceCreatedDate 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel.SourceCreatedDate')
  - [SourceLastModifiedBy](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-SourceLastModifiedBy 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel.SourceLastModifiedBy')
  - [SourceLastModifiedDate](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-SourceLastModifiedDate 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel.SourceLastModifiedDate')
  - [SourceProperties](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-SourceProperties 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel.SourceProperties')
  - [Title](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-Title 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel.Title')
  - [Validate()](#M-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-Validate 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionInputModel.Validate')
- [AggregationSubmissionOutputModel](#T-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-#ctor 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel.#ctor')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-#ctor-System-String,System-String,System-String,System-DateTime,System-String,System-DateTime,System-String,System-String,System-String,System-String,System-Nullable{System-DateTime},System-String,System-Nullable{System-DateTime},System-String,System-String,System-Nullable{System-Boolean},System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-MetaDataModel},System-String,System-String,System-String,System-String,System-String,System-String,System-String,System-String- 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel.#ctor(System.String,System.String,System.String,System.DateTime,System.String,System.DateTime,System.String,System.String,System.String,System.String,System.Nullable{System.DateTime},System.String,System.Nullable{System.DateTime},System.String,System.String,System.Nullable{System.Boolean},System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.MetaDataModel},System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String)')
  - [Author](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-Author 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel.Author')
  - [BarcodeType](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-BarcodeType 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel.BarcodeType')
  - [BarcodeValue](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-BarcodeValue 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel.BarcodeValue')
  - [ConnectorId](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-ConnectorId 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel.ConnectorId')
  - [ContentSource](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-ContentSource 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel.ContentSource')
  - [CorrelationId](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-CorrelationId 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel.CorrelationId')
  - [CreatedBy](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-CreatedBy 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel.CreatedBy')
  - [CreatedDate](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-CreatedDate 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel.CreatedDate')
  - [ExternalId](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-ExternalId 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel.ExternalId')
  - [Id](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-Id 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel.Id')
  - [IsVitalRecord](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-IsVitalRecord 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel.IsVitalRecord')
  - [ItemNumber](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-ItemNumber 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel.ItemNumber')
  - [ItemType](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-ItemType 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel.ItemType')
  - [LastModifiedBy](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-LastModifiedBy 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel.LastModifiedBy')
  - [LastModifiedDate](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-LastModifiedDate 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel.LastModifiedDate')
  - [Location](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-Location 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel.Location')
  - [MediaType](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-MediaType 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel.MediaType')
  - [ParentExternalId](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-ParentExternalId 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel.ParentExternalId')
  - [RecordCategoryId](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-RecordCategoryId 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel.RecordCategoryId')
  - [SourceCreatedBy](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-SourceCreatedBy 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel.SourceCreatedBy')
  - [SourceCreatedDate](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-SourceCreatedDate 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel.SourceCreatedDate')
  - [SourceLastModifiedBy](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-SourceLastModifiedBy 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel.SourceLastModifiedBy')
  - [SourceLastModifiedDate](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-SourceLastModifiedDate 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel.SourceLastModifiedDate')
  - [SourceProperties](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-SourceProperties 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel.SourceProperties')
  - [Title](#P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-Title 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel.Title')
  - [Validate()](#M-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-Validate 'RecordPoint.Connectors.SDK.Client.Models.AggregationSubmissionOutputModel.Validate')
- [ApiClientFactorySettings](#T-RecordPoint-Connectors-SDK-Client-ApiClientFactorySettings 'RecordPoint.Connectors.SDK.Client.ApiClientFactorySettings')
  - [ConnectorApiUrl](#P-RecordPoint-Connectors-SDK-Client-ApiClientFactorySettings-ConnectorApiUrl 'RecordPoint.Connectors.SDK.Client.ApiClientFactorySettings.ConnectorApiUrl')
  - [ServerCertificateValidation](#P-RecordPoint-Connectors-SDK-Client-ApiClientFactorySettings-ServerCertificateValidation 'RecordPoint.Connectors.SDK.Client.ApiClientFactorySettings.ServerCertificateValidation')
- [AuditEvent](#T-RecordPoint-Connectors-SDK-Fields-AuditEvent 'RecordPoint.Connectors.SDK.Fields.AuditEvent')
  - [Created](#F-RecordPoint-Connectors-SDK-Fields-AuditEvent-Created 'RecordPoint.Connectors.SDK.Fields.AuditEvent.Created')
  - [Description](#F-RecordPoint-Connectors-SDK-Fields-AuditEvent-Description 'RecordPoint.Connectors.SDK.Fields.AuditEvent.Description')
  - [EventExternalId](#F-RecordPoint-Connectors-SDK-Fields-AuditEvent-EventExternalId 'RecordPoint.Connectors.SDK.Fields.AuditEvent.EventExternalId')
  - [EventType](#F-RecordPoint-Connectors-SDK-Fields-AuditEvent-EventType 'RecordPoint.Connectors.SDK.Fields.AuditEvent.EventType')
  - [ExternalId](#F-RecordPoint-Connectors-SDK-Fields-AuditEvent-ExternalId 'RecordPoint.Connectors.SDK.Fields.AuditEvent.ExternalId')
  - [UserId](#F-RecordPoint-Connectors-SDK-Fields-AuditEvent-UserId 'RecordPoint.Connectors.SDK.Fields.AuditEvent.UserId')
  - [UserName](#F-RecordPoint-Connectors-SDK-Fields-AuditEvent-UserName 'RecordPoint.Connectors.SDK.Fields.AuditEvent.UserName')
- [AuthenticationHelperSettings](#T-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings 'RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings')
  - [AuthenticationResource](#P-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings-AuthenticationResource 'RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings.AuthenticationResource')
  - [ClientId](#P-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings-ClientId 'RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings.ClientId')
  - [ClientSecret](#P-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings-ClientSecret 'RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings.ClientSecret')
  - [TenantDomainName](#P-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings-TenantDomainName 'RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings.TenantDomainName')
- [BinarySubmissionInputModel](#T-RecordPoint-Connectors-SDK-Client-Models-BinarySubmissionInputModel 'RecordPoint.Connectors.SDK.Client.Models.BinarySubmissionInputModel')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-BinarySubmissionInputModel-#ctor 'RecordPoint.Connectors.SDK.Client.Models.BinarySubmissionInputModel.#ctor')
  - [#ctor(connectorId,itemExternalId,binaryExternalId,fileName,correlationId)](#M-RecordPoint-Connectors-SDK-Client-Models-BinarySubmissionInputModel-#ctor-System-String,System-String,System-String,System-String,System-String- 'RecordPoint.Connectors.SDK.Client.Models.BinarySubmissionInputModel.#ctor(System.String,System.String,System.String,System.String,System.String)')
  - [BinaryExternalId](#P-RecordPoint-Connectors-SDK-Client-Models-BinarySubmissionInputModel-BinaryExternalId 'RecordPoint.Connectors.SDK.Client.Models.BinarySubmissionInputModel.BinaryExternalId')
  - [ConnectorId](#P-RecordPoint-Connectors-SDK-Client-Models-BinarySubmissionInputModel-ConnectorId 'RecordPoint.Connectors.SDK.Client.Models.BinarySubmissionInputModel.ConnectorId')
  - [CorrelationId](#P-RecordPoint-Connectors-SDK-Client-Models-BinarySubmissionInputModel-CorrelationId 'RecordPoint.Connectors.SDK.Client.Models.BinarySubmissionInputModel.CorrelationId')
  - [FileName](#P-RecordPoint-Connectors-SDK-Client-Models-BinarySubmissionInputModel-FileName 'RecordPoint.Connectors.SDK.Client.Models.BinarySubmissionInputModel.FileName')
  - [ItemExternalId](#P-RecordPoint-Connectors-SDK-Client-Models-BinarySubmissionInputModel-ItemExternalId 'RecordPoint.Connectors.SDK.Client.Models.BinarySubmissionInputModel.ItemExternalId')
  - [Validate()](#M-RecordPoint-Connectors-SDK-Client-Models-BinarySubmissionInputModel-Validate 'RecordPoint.Connectors.SDK.Client.Models.BinarySubmissionInputModel.Validate')
- [ConnectorAuditEventModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorAuditEventModel')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel-#ctor 'RecordPoint.Connectors.SDK.Client.Models.ConnectorAuditEventModel.#ctor')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel-#ctor-System-String,System-String,System-String,System-String,System-Nullable{System-DateTime},System-String,System-String,System-String,System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel}- 'RecordPoint.Connectors.SDK.Client.Models.ConnectorAuditEventModel.#ctor(System.String,System.String,System.String,System.String,System.Nullable{System.DateTime},System.String,System.String,System.String,System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.SubmissionMetaDataModel})')
  - [ConnectorId](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel-ConnectorId 'RecordPoint.Connectors.SDK.Client.Models.ConnectorAuditEventModel.ConnectorId')
  - [CreatedDate](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel-CreatedDate 'RecordPoint.Connectors.SDK.Client.Models.ConnectorAuditEventModel.CreatedDate')
  - [Description](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel-Description 'RecordPoint.Connectors.SDK.Client.Models.ConnectorAuditEventModel.Description')
  - [EventExternalId](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel-EventExternalId 'RecordPoint.Connectors.SDK.Client.Models.ConnectorAuditEventModel.EventExternalId')
  - [EventType](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel-EventType 'RecordPoint.Connectors.SDK.Client.Models.ConnectorAuditEventModel.EventType')
  - [ItemExternalId](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel-ItemExternalId 'RecordPoint.Connectors.SDK.Client.Models.ConnectorAuditEventModel.ItemExternalId')
  - [SourceProperties](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel-SourceProperties 'RecordPoint.Connectors.SDK.Client.Models.ConnectorAuditEventModel.SourceProperties')
  - [UserId](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel-UserId 'RecordPoint.Connectors.SDK.Client.Models.ConnectorAuditEventModel.UserId')
  - [UserName](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel-UserName 'RecordPoint.Connectors.SDK.Client.Models.ConnectorAuditEventModel.UserName')
  - [Validate()](#M-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel-Validate 'RecordPoint.Connectors.SDK.Client.Models.ConnectorAuditEventModel.Validate')
- [ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-#ctor 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.#ctor')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-#ctor-System-String,System-String,System-Boolean,System-String,System-String,System-String,System-String,System-String,System-Nullable{System-DateTime},System-Nullable{System-DateTime},System-String,System-String,System-String,System-String,System-String,System-String,System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-MetaDataModel},System-String,System-Nullable{System-Boolean},RecordPoint-Connectors-SDK-Client-Models-FiltersModel- 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.#ctor(System.String,System.String,System.Boolean,System.String,System.String,System.String,System.String,System.String,System.Nullable{System.DateTime},System.Nullable{System.DateTime},System.String,System.String,System.String,System.String,System.String,System.String,System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.MetaDataModel},System.String,System.Nullable{System.Boolean},RecordPoint.Connectors.SDK.Client.Models.FiltersModel)')
  - [_connectorTypeIdAsGuid](#F-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-_connectorTypeIdAsGuid 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel._connectorTypeIdAsGuid')
  - [_idAsGuid](#F-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-_idAsGuid 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel._idAsGuid')
  - [_tenantIdAsGuid](#F-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-_tenantIdAsGuid 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel._tenantIdAsGuid')
  - [ClientId](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-ClientId 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.ClientId')
  - [ConnectorTypeConfigurationId](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-ConnectorTypeConfigurationId 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.ConnectorTypeConfigurationId')
  - [ConnectorTypeId](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-ConnectorTypeId 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.ConnectorTypeId')
  - [CreatedBy](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-CreatedBy 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.CreatedBy')
  - [CreatedDate](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-CreatedDate 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.CreatedDate')
  - [DisplayName](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-DisplayName 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.DisplayName')
  - [EnabledHistory](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-EnabledHistory 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.EnabledHistory')
  - [Filters](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-Filters 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.Filters')
  - [HasSubmittedData](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-HasSubmittedData 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.HasSubmittedData')
  - [Id](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-Id 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.Id')
  - [ModifiedBy](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-ModifiedBy 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.ModifiedBy')
  - [ModifiedDate](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-ModifiedDate 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.ModifiedDate')
  - [OriginatingOrganization](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-OriginatingOrganization 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.OriginatingOrganization')
  - [Properties](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-Properties 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.Properties')
  - [ProtectionEnabled](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-ProtectionEnabled 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.ProtectionEnabled')
  - [Status](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-Status 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.Status')
  - [StatusCode](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-StatusCode 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.StatusCode')
  - [TenantDomainName](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-TenantDomainName 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.TenantDomainName')
  - [TenantId](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-TenantId 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.TenantId')
  - [TransactionId](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-TransactionId 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.TransactionId')
  - [ConnectorTypeIdAsGuid()](#M-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-ConnectorTypeIdAsGuid 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.ConnectorTypeIdAsGuid')
  - [CustomInit()](#M-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-CustomInit 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.CustomInit')
  - [GetADTenantId()](#M-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-GetADTenantId 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.GetADTenantId')
  - [GetIdAsGuid()](#M-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-GetIdAsGuid 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.GetIdAsGuid')
  - [GetPropertyOrDefault(name)](#M-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-GetPropertyOrDefault-System-String- 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.GetPropertyOrDefault(System.String)')
  - [GetTenantIdAsGuid()](#M-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-GetTenantIdAsGuid 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.GetTenantIdAsGuid')
  - [HasBeenEnabled()](#M-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-HasBeenEnabled 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.HasBeenEnabled')
  - [IsDisabledConnectorExpired(maxDisabledConnectorAge)](#M-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-IsDisabledConnectorExpired-System-Int32- 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.IsDisabledConnectorExpired(System.Int32)')
  - [IsEnabled()](#M-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-IsEnabled 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.IsEnabled')
  - [SetProperty(name,value,type)](#M-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-SetProperty-System-String,System-String,System-String- 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.SetProperty(System.String,System.String,System.String)')
  - [SetProperty(name,value)](#M-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-SetProperty-System-String,System-Object- 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.SetProperty(System.String,System.Object)')
  - [Validate()](#M-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-Validate 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel.Validate')
- [ConnectorNotificationAcknowledgeModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationAcknowledgeModel')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel-#ctor 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationAcknowledgeModel.#ctor')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel-#ctor-System-String,System-String,System-String,System-String- 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationAcknowledgeModel.#ctor(System.String,System.String,System.String,System.String)')
  - [ConnectorId](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel-ConnectorId 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationAcknowledgeModel.ConnectorId')
  - [ConnectorStatusMessage](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel-ConnectorStatusMessage 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationAcknowledgeModel.ConnectorStatusMessage')
  - [NotificationId](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel-NotificationId 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationAcknowledgeModel.NotificationId')
  - [ProcessingResult](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel-ProcessingResult 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationAcknowledgeModel.ProcessingResult')
  - [Validate()](#M-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel-Validate 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationAcknowledgeModel.Validate')
- [ConnectorNotificationModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel-#ctor 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel.#ctor')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel-#ctor-System-String,System-String,System-Nullable{System-DateTime},System-String,System-String,RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel,RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,System-Object- 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel.#ctor(System.String,System.String,System.Nullable{System.DateTime},System.String,System.String,RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel,RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,System.Object)')
  - [ConnectorConfig](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel-ConnectorConfig 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel.ConnectorConfig')
  - [ConnectorId](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel-ConnectorId 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel.ConnectorId')
  - [Context](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel-Context 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel.Context')
  - [Id](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel-Id 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel.Id')
  - [Item](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel-Item 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel.Item')
  - [NotificationType](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel-NotificationType 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel.NotificationType')
  - [TenantId](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel-TenantId 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel.TenantId')
  - [Timestamp](#P-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel-Timestamp 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel.Timestamp')
  - [Validate()](#M-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel-Validate 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel.Validate')
- [DirectBinarySubmissionInputModel](#T-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-#ctor 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel.#ctor')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-#ctor-System-String,System-String,System-String,System-String,System-Nullable{System-Int64},System-String,System-Nullable{System-DateTime},System-String,System-String,System-String,System-Nullable{System-Boolean},System-Nullable{System-Boolean},System-Nullable{System-DateTime}- 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel.#ctor(System.String,System.String,System.String,System.String,System.Nullable{System.Int64},System.String,System.Nullable{System.DateTime},System.String,System.String,System.String,System.Nullable{System.Boolean},System.Nullable{System.Boolean},System.Nullable{System.DateTime})')
  - [BinaryExternalId](#P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-BinaryExternalId 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel.BinaryExternalId')
  - [ConnectorId](#P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-ConnectorId 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel.ConnectorId')
  - [CorrelationId](#P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-CorrelationId 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel.CorrelationId')
  - [FileHash](#P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-FileHash 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel.FileHash')
  - [FileName](#P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-FileName 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel.FileName')
  - [FileSize](#P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-FileSize 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel.FileSize')
  - [IsOldVersion](#P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-IsOldVersion 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel.IsOldVersion')
  - [ItemExternalId](#P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-ItemExternalId 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel.ItemExternalId')
  - [ItemSourceLastModifiedDate](#P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-ItemSourceLastModifiedDate 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel.ItemSourceLastModifiedDate')
  - [Location](#P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-Location 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel.Location')
  - [MimeType](#P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-MimeType 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel.MimeType')
  - [SkipEnrichment](#P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-SkipEnrichment 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel.SkipEnrichment')
  - [SourceLastModifiedDate](#P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-SourceLastModifiedDate 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel.SourceLastModifiedDate')
  - [Validate()](#M-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-Validate 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel.Validate')
- [DirectBinarySubmissionResponseModel](#T-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionResponseModel 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionResponseModel')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionResponseModel-#ctor 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionResponseModel.#ctor')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionResponseModel-#ctor-System-String,System-Nullable{System-Int64}- 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionResponseModel.#ctor(System.String,System.Nullable{System.Int64})')
  - [MaxFileSize](#P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionResponseModel-MaxFileSize 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionResponseModel.MaxFileSize')
  - [Url](#P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionResponseModel-Url 'RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionResponseModel.Url')
- [ErrorModel](#T-RecordPoint-Connectors-SDK-Client-Models-ErrorModel 'RecordPoint.Connectors.SDK.Client.Models.ErrorModel')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-ErrorModel-#ctor 'RecordPoint.Connectors.SDK.Client.Models.ErrorModel.#ctor')
  - [#ctor(type,severity)](#M-RecordPoint-Connectors-SDK-Client-Models-ErrorModel-#ctor-System-String,System-String,System-String,System-String,System-Nullable{System-DateTime},System-String,System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-ErrorModel},System-Collections-Generic-IDictionary{System-String,System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-ErrorModel}}- 'RecordPoint.Connectors.SDK.Client.Models.ErrorModel.#ctor(System.String,System.String,System.String,System.String,System.Nullable{System.DateTime},System.String,System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.ErrorModel},System.Collections.Generic.IDictionary{System.String,System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.ErrorModel}})')
  - [AggregateErrors](#P-RecordPoint-Connectors-SDK-Client-Models-ErrorModel-AggregateErrors 'RecordPoint.Connectors.SDK.Client.Models.ErrorModel.AggregateErrors')
  - [DateTime](#P-RecordPoint-Connectors-SDK-Client-Models-ErrorModel-DateTime 'RecordPoint.Connectors.SDK.Client.Models.ErrorModel.DateTime')
  - [InnerError](#P-RecordPoint-Connectors-SDK-Client-Models-ErrorModel-InnerError 'RecordPoint.Connectors.SDK.Client.Models.ErrorModel.InnerError')
  - [Message](#P-RecordPoint-Connectors-SDK-Client-Models-ErrorModel-Message 'RecordPoint.Connectors.SDK.Client.Models.ErrorModel.Message')
  - [MessageCode](#P-RecordPoint-Connectors-SDK-Client-Models-ErrorModel-MessageCode 'RecordPoint.Connectors.SDK.Client.Models.ErrorModel.MessageCode')
  - [Severity](#P-RecordPoint-Connectors-SDK-Client-Models-ErrorModel-Severity 'RecordPoint.Connectors.SDK.Client.Models.ErrorModel.Severity')
  - [Target](#P-RecordPoint-Connectors-SDK-Client-Models-ErrorModel-Target 'RecordPoint.Connectors.SDK.Client.Models.ErrorModel.Target')
  - [Type](#P-RecordPoint-Connectors-SDK-Client-Models-ErrorModel-Type 'RecordPoint.Connectors.SDK.Client.Models.ErrorModel.Type')
- [ErrorResponseModel](#T-RecordPoint-Connectors-SDK-Client-Models-ErrorResponseModel 'RecordPoint.Connectors.SDK.Client.Models.ErrorResponseModel')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-ErrorResponseModel-#ctor 'RecordPoint.Connectors.SDK.Client.Models.ErrorResponseModel.#ctor')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-ErrorResponseModel-#ctor-RecordPoint-Connectors-SDK-Client-Models-ErrorModel- 'RecordPoint.Connectors.SDK.Client.Models.ErrorResponseModel.#ctor(RecordPoint.Connectors.SDK.Client.Models.ErrorModel)')
  - [Error](#P-RecordPoint-Connectors-SDK-Client-Models-ErrorResponseModel-Error 'RecordPoint.Connectors.SDK.Client.Models.ErrorResponseModel.Error')
- [Fields](#T-RecordPoint-Connectors-SDK-Fields 'RecordPoint.Connectors.SDK.Fields')
  - [Author](#F-RecordPoint-Connectors-SDK-Fields-Author 'RecordPoint.Connectors.SDK.Fields.Author')
  - [BarcodeType](#F-RecordPoint-Connectors-SDK-Fields-BarcodeType 'RecordPoint.Connectors.SDK.Fields.BarcodeType')
  - [BarcodeValue](#F-RecordPoint-Connectors-SDK-Fields-BarcodeValue 'RecordPoint.Connectors.SDK.Fields.BarcodeValue')
  - [ConnectorId](#F-RecordPoint-Connectors-SDK-Fields-ConnectorId 'RecordPoint.Connectors.SDK.Fields.ConnectorId')
  - [ContentVersion](#F-RecordPoint-Connectors-SDK-Fields-ContentVersion 'RecordPoint.Connectors.SDK.Fields.ContentVersion')
  - [ExternalId](#F-RecordPoint-Connectors-SDK-Fields-ExternalId 'RecordPoint.Connectors.SDK.Fields.ExternalId')
  - [ItemTypeId](#F-RecordPoint-Connectors-SDK-Fields-ItemTypeId 'RecordPoint.Connectors.SDK.Fields.ItemTypeId')
  - [Location](#F-RecordPoint-Connectors-SDK-Fields-Location 'RecordPoint.Connectors.SDK.Fields.Location')
  - [MediaType](#F-RecordPoint-Connectors-SDK-Fields-MediaType 'RecordPoint.Connectors.SDK.Fields.MediaType')
  - [MimeType](#F-RecordPoint-Connectors-SDK-Fields-MimeType 'RecordPoint.Connectors.SDK.Fields.MimeType')
  - [ParentExternalId](#F-RecordPoint-Connectors-SDK-Fields-ParentExternalId 'RecordPoint.Connectors.SDK.Fields.ParentExternalId')
  - [RecordCategoryID](#F-RecordPoint-Connectors-SDK-Fields-RecordCategoryID 'RecordPoint.Connectors.SDK.Fields.RecordCategoryID')
  - [SecurityProfileIdentifier](#F-RecordPoint-Connectors-SDK-Fields-SecurityProfileIdentifier 'RecordPoint.Connectors.SDK.Fields.SecurityProfileIdentifier')
  - [SourceCreatedBy](#F-RecordPoint-Connectors-SDK-Fields-SourceCreatedBy 'RecordPoint.Connectors.SDK.Fields.SourceCreatedBy')
  - [SourceCreatedDate](#F-RecordPoint-Connectors-SDK-Fields-SourceCreatedDate 'RecordPoint.Connectors.SDK.Fields.SourceCreatedDate')
  - [SourceLastModifiedBy](#F-RecordPoint-Connectors-SDK-Fields-SourceLastModifiedBy 'RecordPoint.Connectors.SDK.Fields.SourceLastModifiedBy')
  - [SourceLastModifiedDate](#F-RecordPoint-Connectors-SDK-Fields-SourceLastModifiedDate 'RecordPoint.Connectors.SDK.Fields.SourceLastModifiedDate')
  - [Title](#F-RecordPoint-Connectors-SDK-Fields-Title 'RecordPoint.Connectors.SDK.Fields.Title')
- [FiltersModel](#T-RecordPoint-Connectors-SDK-Client-Models-FiltersModel 'RecordPoint.Connectors.SDK.Client.Models.FiltersModel')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-FiltersModel-#ctor 'RecordPoint.Connectors.SDK.Client.Models.FiltersModel.#ctor')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-FiltersModel-#ctor-RecordPoint-Connectors-SDK-Client-Models-SearchTreeNodeModel,RecordPoint-Connectors-SDK-Client-Models-SearchTreeNodeModel,System-String,System-String- 'RecordPoint.Connectors.SDK.Client.Models.FiltersModel.#ctor(RecordPoint.Connectors.SDK.Client.Models.SearchTreeNodeModel,RecordPoint.Connectors.SDK.Client.Models.SearchTreeNodeModel,System.String,System.String)')
  - [Excluded](#P-RecordPoint-Connectors-SDK-Client-Models-FiltersModel-Excluded 'RecordPoint.Connectors.SDK.Client.Models.FiltersModel.Excluded')
  - [ExcludedExpression](#P-RecordPoint-Connectors-SDK-Client-Models-FiltersModel-ExcludedExpression 'RecordPoint.Connectors.SDK.Client.Models.FiltersModel.ExcludedExpression')
  - [Included](#P-RecordPoint-Connectors-SDK-Client-Models-FiltersModel-Included 'RecordPoint.Connectors.SDK.Client.Models.FiltersModel.Included')
  - [IncludedExpression](#P-RecordPoint-Connectors-SDK-Client-Models-FiltersModel-IncludedExpression 'RecordPoint.Connectors.SDK.Client.Models.FiltersModel.IncludedExpression')
  - [Validate()](#M-RecordPoint-Connectors-SDK-Client-Models-FiltersModel-Validate 'RecordPoint.Connectors.SDK.Client.Models.FiltersModel.Validate')
- [ICircuitEventHandler](#T-RecordPoint-Connectors-SDK-Interfaces-ICircuitEventHandler 'RecordPoint.Connectors.SDK.Interfaces.ICircuitEventHandler')
- [ICircuitProvider](#T-RecordPoint-Connectors-SDK-Providers-ICircuitProvider 'RecordPoint.Connectors.SDK.Providers.ICircuitProvider')
  - [IsCircuitClosed()](#M-RecordPoint-Connectors-SDK-Providers-ICircuitProvider-IsCircuitClosed-System-TimeSpan@- 'RecordPoint.Connectors.SDK.Providers.ICircuitProvider.IsCircuitClosed(System.TimeSpan@)')
- [IConnectorConfigAccessor](#T-RecordPoint-Connectors-SDK-Interfaces-IConnectorConfigAccessor 'RecordPoint.Connectors.SDK.Interfaces.IConnectorConfigAccessor')
  - [GetConnectorConfig(tenantId,connectorConfigId)](#M-RecordPoint-Connectors-SDK-Interfaces-IConnectorConfigAccessor-GetConnectorConfig-System-Guid,System-Guid- 'RecordPoint.Connectors.SDK.Interfaces.IConnectorConfigAccessor.GetConnectorConfig(System.Guid,System.Guid)')
- [IDateTimeProvider](#T-RecordPoint-Connectors-SDK-Providers-IDateTimeProvider 'RecordPoint.Connectors.SDK.Providers.IDateTimeProvider')
  - [UtcNow](#P-RecordPoint-Connectors-SDK-Providers-IDateTimeProvider-UtcNow 'RecordPoint.Connectors.SDK.Providers.IDateTimeProvider.UtcNow')
- [ILog](#T-RecordPoint-Connectors-SDK-Diagnostics-ILog 'RecordPoint.Connectors.SDK.Diagnostics.ILog')
  - [LogMessage(callerType,methodName,message,elapsedTimeTicks)](#M-RecordPoint-Connectors-SDK-Diagnostics-ILog-LogMessage-System-Type,System-String,System-String,System-Nullable{System-Int64}- 'RecordPoint.Connectors.SDK.Diagnostics.ILog.LogMessage(System.Type,System.String,System.String,System.Nullable{System.Int64})')
  - [LogVerbose(callerType,methodName,message,elapsedTimeTicks)](#M-RecordPoint-Connectors-SDK-Diagnostics-ILog-LogVerbose-System-Type,System-String,System-String,System-Nullable{System-Int64}- 'RecordPoint.Connectors.SDK.Diagnostics.ILog.LogVerbose(System.Type,System.String,System.String,System.Nullable{System.Int64})')
  - [LogWarning(callerType,methodName,message,elapsedTimeTicks)](#M-RecordPoint-Connectors-SDK-Diagnostics-ILog-LogWarning-System-Type,System-String,System-String,System-Nullable{System-Int64}- 'RecordPoint.Connectors.SDK.Diagnostics.ILog.LogWarning(System.Type,System.String,System.String,System.Nullable{System.Int64})')
- [INotificationHandler](#T-RecordPoint-Connectors-SDK-Notifications-INotificationHandler 'RecordPoint.Connectors.SDK.Notifications.INotificationHandler')
  - [HandleNotification(connectorConfigModel,notification,ct)](#M-RecordPoint-Connectors-SDK-Notifications-INotificationHandler-HandleNotification-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Notifications.INotificationHandler.HandleNotification(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel,System.Threading.CancellationToken)')
- [INotificationPullManager](#T-RecordPoint-Connectors-SDK-Notifications-INotificationPullManager 'RecordPoint.Connectors.SDK.Notifications.INotificationPullManager')
  - [AcknowledgeNotification(factorySettings,authenticationSettings,acknowledgement,cancellationToken)](#M-RecordPoint-Connectors-SDK-Notifications-INotificationPullManager-AcknowledgeNotification-RecordPoint-Connectors-SDK-Client-ApiClientFactorySettings,RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings,RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Notifications.INotificationPullManager.AcknowledgeNotification(RecordPoint.Connectors.SDK.Client.ApiClientFactorySettings,RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings,RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationAcknowledgeModel,System.Threading.CancellationToken)')
  - [GetAllPendingConnectorNotifications(factorySettings,authenticationSettings,connectorConfigId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Notifications-INotificationPullManager-GetAllPendingConnectorNotifications-RecordPoint-Connectors-SDK-Client-ApiClientFactorySettings,RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Notifications.INotificationPullManager.GetAllPendingConnectorNotifications(RecordPoint.Connectors.SDK.Client.ApiClientFactorySettings,RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings,System.String,System.Threading.CancellationToken)')
- [IPerformanceEvent](#T-RecordPoint-Connectors-SDK-Diagnostics-IPerformanceEvent 'RecordPoint.Connectors.SDK.Diagnostics.IPerformanceEvent')
  - [Exception(ex)](#M-RecordPoint-Connectors-SDK-Diagnostics-IPerformanceEvent-Exception-System-Exception- 'RecordPoint.Connectors.SDK.Diagnostics.IPerformanceEvent.Exception(System.Exception)')
- [ISdkAzureBlobCircuitProvider](#T-RecordPoint-Connectors-SDK-Providers-ISdkAzureBlobCircuitProvider 'RecordPoint.Connectors.SDK.Providers.ISdkAzureBlobCircuitProvider')
- [ISdkAzureBlobRetryProvider](#T-RecordPoint-Connectors-SDK-Providers-ISdkAzureBlobRetryProvider 'RecordPoint.Connectors.SDK.Providers.ISdkAzureBlobRetryProvider')
  - [ExecuteWithRetry(codeToExecute,type,methodName)](#M-RecordPoint-Connectors-SDK-Providers-ISdkAzureBlobRetryProvider-ExecuteWithRetry-System-Func{System-Threading-Tasks-Task},System-Type,System-String- 'RecordPoint.Connectors.SDK.Providers.ISdkAzureBlobRetryProvider.ExecuteWithRetry(System.Func{System.Threading.Tasks.Task},System.Type,System.String)')
- [ISettableCircuitProvider](#T-RecordPoint-Connectors-SDK-Providers-ISettableCircuitProvider 'RecordPoint.Connectors.SDK.Providers.ISettableCircuitProvider')
  - [SetOpenUntil(newTime)](#M-RecordPoint-Connectors-SDK-Providers-ISettableCircuitProvider-SetOpenUntil-System-DateTime- 'RecordPoint.Connectors.SDK.Providers.ISettableCircuitProvider.SetOpenUntil(System.DateTime)')
- [ISubmission](#T-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission 'RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission')
  - [Submit(submitContext)](#M-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission-Submit-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext- 'RecordPoint.Connectors.SDK.SubmitPipeline.ISubmission.Submit(RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext)')
- [ItemAcceptanceModel](#T-RecordPoint-Connectors-SDK-Client-Models-ItemAcceptanceModel 'RecordPoint.Connectors.SDK.Client.Models.ItemAcceptanceModel')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-ItemAcceptanceModel-#ctor 'RecordPoint.Connectors.SDK.Client.Models.ItemAcceptanceModel.#ctor')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-ItemAcceptanceModel-#ctor-System-String,System-Nullable{System-DateTime},System-String- 'RecordPoint.Connectors.SDK.Client.Models.ItemAcceptanceModel.#ctor(System.String,System.Nullable{System.DateTime},System.String)')
  - [AggregationStatus](#P-RecordPoint-Connectors-SDK-Client-Models-ItemAcceptanceModel-AggregationStatus 'RecordPoint.Connectors.SDK.Client.Models.ItemAcceptanceModel.AggregationStatus')
  - [ExternalId](#P-RecordPoint-Connectors-SDK-Client-Models-ItemAcceptanceModel-ExternalId 'RecordPoint.Connectors.SDK.Client.Models.ItemAcceptanceModel.ExternalId')
  - [SourceLastModifiedDate](#P-RecordPoint-Connectors-SDK-Client-Models-ItemAcceptanceModel-SourceLastModifiedDate 'RecordPoint.Connectors.SDK.Client.Models.ItemAcceptanceModel.SourceLastModifiedDate')
- [ItemSubmissionInputModel](#T-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-#ctor 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel.#ctor')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-#ctor-System-String,System-String,System-String,System-String,System-DateTime,System-String,System-String,System-DateTime,System-String,System-String,System-String,System-String,System-String,System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel},System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-RelationshipDataModel},System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel},System-String,System-String,System-String,System-String- 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel.#ctor(System.String,System.String,System.String,System.String,System.DateTime,System.String,System.String,System.DateTime,System.String,System.String,System.String,System.String,System.String,System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.SubmissionMetaDataModel},System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.RelationshipDataModel},System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.DirectBinarySubmissionInputModel},System.String,System.String,System.String,System.String)')
  - [Author](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-Author 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel.Author')
  - [BarcodeType](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-BarcodeType 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel.BarcodeType')
  - [BarcodeValue](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-BarcodeValue 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel.BarcodeValue')
  - [BinariesSubmitted](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-BinariesSubmitted 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel.BinariesSubmitted')
  - [ConnectorId](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-ConnectorId 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel.ConnectorId')
  - [ContentVersion](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-ContentVersion 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel.ContentVersion')
  - [CorrelationId](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-CorrelationId 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel.CorrelationId')
  - [ExternalId](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-ExternalId 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel.ExternalId')
  - [Location](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-Location 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel.Location')
  - [MediaType](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-MediaType 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel.MediaType')
  - [MimeType](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-MimeType 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel.MimeType')
  - [ParentExternalId](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-ParentExternalId 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel.ParentExternalId')
  - [Relationships](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-Relationships 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel.Relationships')
  - [SecurityProfileIdentifier](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-SecurityProfileIdentifier 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel.SecurityProfileIdentifier')
  - [SourceCreatedBy](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-SourceCreatedBy 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel.SourceCreatedBy')
  - [SourceCreatedDate](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-SourceCreatedDate 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel.SourceCreatedDate')
  - [SourceLastModifiedBy](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-SourceLastModifiedBy 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel.SourceLastModifiedBy')
  - [SourceLastModifiedDate](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-SourceLastModifiedDate 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel.SourceLastModifiedDate')
  - [SourceProperties](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-SourceProperties 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel.SourceProperties')
  - [Title](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-Title 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel.Title')
  - [Validate()](#M-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-Validate 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionInputModel.Validate')
- [ItemSubmissionOutputModel](#T-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-#ctor 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.#ctor')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-#ctor-System-String,System-String,System-String,System-String,System-DateTime,System-String,System-String,System-DateTime,System-String,System-String,System-String,System-String,System-String,System-String,System-String,System-String,System-Nullable{System-DateTime},System-String,System-Nullable{System-DateTime},System-String,System-String,System-String,System-Nullable{System-Boolean},System-String,System-String,System-Nullable{System-DateTime},System-String,System-String,System-String,System-Nullable{System-DateTime},System-String,System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-MetaDataModel},System-String,System-String,System-String,System-String- 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.#ctor(System.String,System.String,System.String,System.String,System.DateTime,System.String,System.String,System.DateTime,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.Nullable{System.DateTime},System.String,System.Nullable{System.DateTime},System.String,System.String,System.String,System.Nullable{System.Boolean},System.String,System.String,System.Nullable{System.DateTime},System.String,System.String,System.String,System.Nullable{System.DateTime},System.String,System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.MetaDataModel},System.String,System.String,System.String,System.String)')
  - [Author](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-Author 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.Author')
  - [BarcodeType](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-BarcodeType 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.BarcodeType')
  - [BarcodeValue](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-BarcodeValue 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.BarcodeValue')
  - [ConnectorDisplayName](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-ConnectorDisplayName 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.ConnectorDisplayName')
  - [ConnectorId](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-ConnectorId 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.ConnectorId')
  - [ContentSource](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-ContentSource 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.ContentSource')
  - [ContentVersion](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-ContentVersion 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.ContentVersion')
  - [CorrelationId](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-CorrelationId 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.CorrelationId')
  - [CreatedBy](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-CreatedBy 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.CreatedBy')
  - [CreatedDate](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-CreatedDate 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.CreatedDate')
  - [CurrentDisposalStatus](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-CurrentDisposalStatus 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.CurrentDisposalStatus')
  - [ExternalId](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-ExternalId 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.ExternalId')
  - [Format](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-Format 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.Format')
  - [Id](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-Id 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.Id')
  - [IsVitalRecord](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-IsVitalRecord 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.IsVitalRecord')
  - [ItemNumber](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-ItemNumber 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.ItemNumber')
  - [ItemType](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-ItemType 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.ItemType')
  - [LastModifiedBy](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-LastModifiedBy 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.LastModifiedBy')
  - [LastModifiedDate](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-LastModifiedDate 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.LastModifiedDate')
  - [Location](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-Location 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.Location')
  - [MediaType](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-MediaType 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.MediaType')
  - [MimeType](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-MimeType 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.MimeType')
  - [NextDisposalAction](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-NextDisposalAction 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.NextDisposalAction')
  - [NextDisposalDate](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-NextDisposalDate 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.NextDisposalDate')
  - [OriginatingOrganization](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-OriginatingOrganization 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.OriginatingOrganization')
  - [ParentExternalId](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-ParentExternalId 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.ParentExternalId')
  - [PreviousDisposalAction](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-PreviousDisposalAction 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.PreviousDisposalAction')
  - [PreviousDisposalBy](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-PreviousDisposalBy 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.PreviousDisposalBy')
  - [PreviousDisposalById](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-PreviousDisposalById 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.PreviousDisposalById')
  - [PreviousDisposalDate](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-PreviousDisposalDate 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.PreviousDisposalDate')
  - [SourceCreatedBy](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-SourceCreatedBy 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.SourceCreatedBy')
  - [SourceCreatedDate](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-SourceCreatedDate 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.SourceCreatedDate')
  - [SourceLastModifiedBy](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-SourceLastModifiedBy 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.SourceLastModifiedBy')
  - [SourceLastModifiedDate](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-SourceLastModifiedDate 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.SourceLastModifiedDate')
  - [SourceProperties](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-SourceProperties 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.SourceProperties')
  - [Title](#P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-Title 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.Title')
  - [Validate()](#M-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-Validate 'RecordPoint.Connectors.SDK.Client.Models.ItemSubmissionOutputModel.Validate')
- [MessageCode](#T-RecordPoint-Connectors-SDK-Fields-MessageCode 'RecordPoint.Connectors.SDK.Fields.MessageCode')
  - [ConnectorNotEnabled](#F-RecordPoint-Connectors-SDK-Fields-MessageCode-ConnectorNotEnabled 'RecordPoint.Connectors.SDK.Fields.MessageCode.ConnectorNotEnabled')
  - [ProtectionNotEnabled](#F-RecordPoint-Connectors-SDK-Fields-MessageCode-ProtectionNotEnabled 'RecordPoint.Connectors.SDK.Fields.MessageCode.ProtectionNotEnabled')
- [MetaDataKeys](#T-RecordPoint-Connectors-SDK-Fields-MetaDataKeys 'RecordPoint.Connectors.SDK.Fields.MetaDataKeys')
  - [ItemBinary_CorrelationId](#F-RecordPoint-Connectors-SDK-Fields-MetaDataKeys-ItemBinary_CorrelationId 'RecordPoint.Connectors.SDK.Fields.MetaDataKeys.ItemBinary_CorrelationId')
  - [ItemBinary_FileName](#F-RecordPoint-Connectors-SDK-Fields-MetaDataKeys-ItemBinary_FileName 'RecordPoint.Connectors.SDK.Fields.MetaDataKeys.ItemBinary_FileName')
- [MetaDataModel](#T-RecordPoint-Connectors-SDK-Client-Models-MetaDataModel 'RecordPoint.Connectors.SDK.Client.Models.MetaDataModel')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-MetaDataModel-#ctor 'RecordPoint.Connectors.SDK.Client.Models.MetaDataModel.#ctor')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-MetaDataModel-#ctor-System-String,System-String,System-String,System-Nullable{System-Boolean}- 'RecordPoint.Connectors.SDK.Client.Models.MetaDataModel.#ctor(System.String,System.String,System.String,System.Nullable{System.Boolean})')
  - [IsSysAdminOnly](#P-RecordPoint-Connectors-SDK-Client-Models-MetaDataModel-IsSysAdminOnly 'RecordPoint.Connectors.SDK.Client.Models.MetaDataModel.IsSysAdminOnly')
  - [Name](#P-RecordPoint-Connectors-SDK-Client-Models-MetaDataModel-Name 'RecordPoint.Connectors.SDK.Client.Models.MetaDataModel.Name')
  - [Type](#P-RecordPoint-Connectors-SDK-Client-Models-MetaDataModel-Type 'RecordPoint.Connectors.SDK.Client.Models.MetaDataModel.Type')
  - [Value](#P-RecordPoint-Connectors-SDK-Client-Models-MetaDataModel-Value 'RecordPoint.Connectors.SDK.Client.Models.MetaDataModel.Value')
  - [Validate()](#M-RecordPoint-Connectors-SDK-Client-Models-MetaDataModel-Validate 'RecordPoint.Connectors.SDK.Client.Models.MetaDataModel.Validate')
- [MetaDataModelListExtensions](#T-RecordPoint-Connectors-SDK-Client-MetaDataModelListExtensions 'RecordPoint.Connectors.SDK.Client.MetaDataModelListExtensions')
  - [AddOrUpdate(metaDataList,name,type,value)](#M-RecordPoint-Connectors-SDK-Client-MetaDataModelListExtensions-AddOrUpdate-System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-MetaDataModel},System-String,System-String,System-String- 'RecordPoint.Connectors.SDK.Client.MetaDataModelListExtensions.AddOrUpdate(System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.MetaDataModel},System.String,System.String,System.String)')
  - [AddOrUpdate(metaDataList,name,value)](#M-RecordPoint-Connectors-SDK-Client-MetaDataModelListExtensions-AddOrUpdate-System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-MetaDataModel},System-String,System-String- 'RecordPoint.Connectors.SDK.Client.MetaDataModelListExtensions.AddOrUpdate(System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.MetaDataModel},System.String,System.String)')
  - [GetValueOrDefault(metaDataList,name)](#M-RecordPoint-Connectors-SDK-Client-MetaDataModelListExtensions-GetValueOrDefault-System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-MetaDataModel},System-String- 'RecordPoint.Connectors.SDK.Client.MetaDataModelListExtensions.GetValueOrDefault(System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.MetaDataModel},System.String)')
- [PerformanceEvent](#T-RecordPoint-Connectors-SDK-Diagnostics-PerformanceEvent 'RecordPoint.Connectors.SDK.Diagnostics.PerformanceEvent')
  - [#ctor(type,method,message,log)](#M-RecordPoint-Connectors-SDK-Diagnostics-PerformanceEvent-#ctor-System-Type,System-String,System-String,RecordPoint-Connectors-SDK-Diagnostics-ILog- 'RecordPoint.Connectors.SDK.Diagnostics.PerformanceEvent.#ctor(System.Type,System.String,System.String,RecordPoint.Connectors.SDK.Diagnostics.ILog)')
  - [Dispose()](#M-RecordPoint-Connectors-SDK-Diagnostics-PerformanceEvent-Dispose-System-Boolean- 'RecordPoint.Connectors.SDK.Diagnostics.PerformanceEvent.Dispose(System.Boolean)')
  - [Dispose()](#M-RecordPoint-Connectors-SDK-Diagnostics-PerformanceEvent-Dispose 'RecordPoint.Connectors.SDK.Diagnostics.PerformanceEvent.Dispose')
  - [Exception(ex)](#M-RecordPoint-Connectors-SDK-Diagnostics-PerformanceEvent-Exception-System-Exception- 'RecordPoint.Connectors.SDK.Diagnostics.PerformanceEvent.Exception(System.Exception)')
- [ProcessingResult](#T-RecordPoint-Connectors-SDK-Client-ProcessingResult 'RecordPoint.Connectors.SDK.Client.ProcessingResult')
  - [ConnectorDisabled](#F-RecordPoint-Connectors-SDK-Client-ProcessingResult-ConnectorDisabled 'RecordPoint.Connectors.SDK.Client.ProcessingResult.ConnectorDisabled')
  - [ConnectorNotReachable](#F-RecordPoint-Connectors-SDK-Client-ProcessingResult-ConnectorNotReachable 'RecordPoint.Connectors.SDK.Client.ProcessingResult.ConnectorNotReachable')
  - [ConnectorNotSubscribed](#F-RecordPoint-Connectors-SDK-Client-ProcessingResult-ConnectorNotSubscribed 'RecordPoint.Connectors.SDK.Client.ProcessingResult.ConnectorNotSubscribed')
  - [NotificationError](#F-RecordPoint-Connectors-SDK-Client-ProcessingResult-NotificationError 'RecordPoint.Connectors.SDK.Client.ProcessingResult.NotificationError')
  - [OK](#F-RecordPoint-Connectors-SDK-Client-ProcessingResult-OK 'RecordPoint.Connectors.SDK.Client.ProcessingResult.OK')
  - [Unknown](#F-RecordPoint-Connectors-SDK-Client-ProcessingResult-Unknown 'RecordPoint.Connectors.SDK.Client.ProcessingResult.Unknown')
- [RelationshipDataModel](#T-RecordPoint-Connectors-SDK-Client-Models-RelationshipDataModel 'RecordPoint.Connectors.SDK.Client.Models.RelationshipDataModel')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-RelationshipDataModel-#ctor 'RecordPoint.Connectors.SDK.Client.Models.RelationshipDataModel.#ctor')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-RelationshipDataModel-#ctor-System-String,System-String- 'RecordPoint.Connectors.SDK.Client.Models.RelationshipDataModel.#ctor(System.String,System.String)')
  - [RelatedItemNumber](#P-RecordPoint-Connectors-SDK-Client-Models-RelationshipDataModel-RelatedItemNumber 'RecordPoint.Connectors.SDK.Client.Models.RelationshipDataModel.RelatedItemNumber')
  - [RelationshipType](#P-RecordPoint-Connectors-SDK-Client-Models-RelationshipDataModel-RelationshipType 'RecordPoint.Connectors.SDK.Client.Models.RelationshipDataModel.RelationshipType')
  - [Validate()](#M-RecordPoint-Connectors-SDK-Client-Models-RelationshipDataModel-Validate 'RecordPoint.Connectors.SDK.Client.Models.RelationshipDataModel.Validate')
- [RequiredValueNullException](#T-RecordPoint-Connectors-SDK-Client-RequiredValueNullException 'RecordPoint.Connectors.SDK.Client.RequiredValueNullException')
  - [#ctor(paramName)](#M-RecordPoint-Connectors-SDK-Client-RequiredValueNullException-#ctor-System-String- 'RecordPoint.Connectors.SDK.Client.RequiredValueNullException.#ctor(System.String)')
  - [NULL_VALUE_MESSAGE](#F-RecordPoint-Connectors-SDK-Client-RequiredValueNullException-NULL_VALUE_MESSAGE 'RecordPoint.Connectors.SDK.Client.RequiredValueNullException.NULL_VALUE_MESSAGE')
  - [_paramName](#F-RecordPoint-Connectors-SDK-Client-RequiredValueNullException-_paramName 'RecordPoint.Connectors.SDK.Client.RequiredValueNullException._paramName')
  - [ParamName](#P-RecordPoint-Connectors-SDK-Client-RequiredValueNullException-ParamName 'RecordPoint.Connectors.SDK.Client.RequiredValueNullException.ParamName')
- [ResourceNotFoundException](#T-RecordPoint-Connectors-SDK-Exceptions-ResourceNotFoundException 'RecordPoint.Connectors.SDK.Exceptions.ResourceNotFoundException')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Exceptions-ResourceNotFoundException-#ctor 'RecordPoint.Connectors.SDK.Exceptions.ResourceNotFoundException.#ctor')
  - [#ctor(message)](#M-RecordPoint-Connectors-SDK-Exceptions-ResourceNotFoundException-#ctor-System-String- 'RecordPoint.Connectors.SDK.Exceptions.ResourceNotFoundException.#ctor(System.String)')
  - [#ctor(message,innerException)](#M-RecordPoint-Connectors-SDK-Exceptions-ResourceNotFoundException-#ctor-System-String,System-Exception- 'RecordPoint.Connectors.SDK.Exceptions.ResourceNotFoundException.#ctor(System.String,System.Exception)')
- [SearchTermModel](#T-RecordPoint-Connectors-SDK-Client-Models-SearchTermModel 'RecordPoint.Connectors.SDK.Client.Models.SearchTermModel')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-SearchTermModel-#ctor 'RecordPoint.Connectors.SDK.Client.Models.SearchTermModel.#ctor')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-SearchTermModel-#ctor-System-String,System-String,System-String,System-String,System-String,System-String- 'RecordPoint.Connectors.SDK.Client.Models.SearchTermModel.#ctor(System.String,System.String,System.String,System.String,System.String,System.String)')
  - [CategoricalValueType](#P-RecordPoint-Connectors-SDK-Client-Models-SearchTermModel-CategoricalValueType 'RecordPoint.Connectors.SDK.Client.Models.SearchTermModel.CategoricalValueType')
  - [FieldName](#P-RecordPoint-Connectors-SDK-Client-Models-SearchTermModel-FieldName 'RecordPoint.Connectors.SDK.Client.Models.SearchTermModel.FieldName')
  - [FieldType](#P-RecordPoint-Connectors-SDK-Client-Models-SearchTermModel-FieldType 'RecordPoint.Connectors.SDK.Client.Models.SearchTermModel.FieldType')
  - [FieldValue](#P-RecordPoint-Connectors-SDK-Client-Models-SearchTermModel-FieldValue 'RecordPoint.Connectors.SDK.Client.Models.SearchTermModel.FieldValue')
  - [OperatorProperty](#P-RecordPoint-Connectors-SDK-Client-Models-SearchTermModel-OperatorProperty 'RecordPoint.Connectors.SDK.Client.Models.SearchTermModel.OperatorProperty')
  - [SearchContextIdentifier](#P-RecordPoint-Connectors-SDK-Client-Models-SearchTermModel-SearchContextIdentifier 'RecordPoint.Connectors.SDK.Client.Models.SearchTermModel.SearchContextIdentifier')
  - [Validate()](#M-RecordPoint-Connectors-SDK-Client-Models-SearchTermModel-Validate 'RecordPoint.Connectors.SDK.Client.Models.SearchTermModel.Validate')
- [SearchTreeNodeModel](#T-RecordPoint-Connectors-SDK-Client-Models-SearchTreeNodeModel 'RecordPoint.Connectors.SDK.Client.Models.SearchTreeNodeModel')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-SearchTreeNodeModel-#ctor 'RecordPoint.Connectors.SDK.Client.Models.SearchTreeNodeModel.#ctor')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-SearchTreeNodeModel-#ctor-System-String,System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-SearchTreeNodeModel},RecordPoint-Connectors-SDK-Client-Models-SearchTermModel- 'RecordPoint.Connectors.SDK.Client.Models.SearchTreeNodeModel.#ctor(System.String,System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.SearchTreeNodeModel},RecordPoint.Connectors.SDK.Client.Models.SearchTermModel)')
  - [BoolOperator](#P-RecordPoint-Connectors-SDK-Client-Models-SearchTreeNodeModel-BoolOperator 'RecordPoint.Connectors.SDK.Client.Models.SearchTreeNodeModel.BoolOperator')
  - [Children](#P-RecordPoint-Connectors-SDK-Client-Models-SearchTreeNodeModel-Children 'RecordPoint.Connectors.SDK.Client.Models.SearchTreeNodeModel.Children')
  - [SearchTerm](#P-RecordPoint-Connectors-SDK-Client-Models-SearchTreeNodeModel-SearchTerm 'RecordPoint.Connectors.SDK.Client.Models.SearchTreeNodeModel.SearchTerm')
  - [Validate()](#M-RecordPoint-Connectors-SDK-Client-Models-SearchTreeNodeModel-Validate 'RecordPoint.Connectors.SDK.Client.Models.SearchTreeNodeModel.Validate')
- [Status](#T-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult-Status 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitResult.Status')
  - [ConnectorDisabled](#F-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult-Status-ConnectorDisabled 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitResult.Status.ConnectorDisabled')
  - [ConnectorNotFound](#F-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult-Status-ConnectorNotFound 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitResult.Status.ConnectorNotFound')
  - [Deferred](#F-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult-Status-Deferred 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitResult.Status.Deferred')
  - [OK](#F-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult-Status-OK 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitResult.Status.OK')
  - [Skipped](#F-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult-Status-Skipped 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitResult.Status.Skipped')
  - [TooManyRequests](#F-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult-Status-TooManyRequests 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitResult.Status.TooManyRequests')
- [SubmissionMetaDataModel](#T-RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel 'RecordPoint.Connectors.SDK.Client.Models.SubmissionMetaDataModel')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel-#ctor 'RecordPoint.Connectors.SDK.Client.Models.SubmissionMetaDataModel.#ctor')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel-#ctor-System-String,System-String,System-String,System-String,System-Nullable{System-Boolean}- 'RecordPoint.Connectors.SDK.Client.Models.SubmissionMetaDataModel.#ctor(System.String,System.String,System.String,System.String,System.Nullable{System.Boolean})')
  - [DisplayName](#P-RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel-DisplayName 'RecordPoint.Connectors.SDK.Client.Models.SubmissionMetaDataModel.DisplayName')
  - [IsSysAdminOnly](#P-RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel-IsSysAdminOnly 'RecordPoint.Connectors.SDK.Client.Models.SubmissionMetaDataModel.IsSysAdminOnly')
  - [Name](#P-RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel-Name 'RecordPoint.Connectors.SDK.Client.Models.SubmissionMetaDataModel.Name')
  - [Type](#P-RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel-Type 'RecordPoint.Connectors.SDK.Client.Models.SubmissionMetaDataModel.Type')
  - [Value](#P-RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel-Value 'RecordPoint.Connectors.SDK.Client.Models.SubmissionMetaDataModel.Value')
  - [Validate()](#M-RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel-Validate 'RecordPoint.Connectors.SDK.Client.Models.SubmissionMetaDataModel.Validate')
- [SubmissionMetaDataModelExtensions](#T-RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModelExtensions 'RecordPoint.Connectors.SDK.Client.Models.SubmissionMetaDataModelExtensions')
  - [AddOrUpdate(metaDataList,metaData)](#M-RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModelExtensions-AddOrUpdate-System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel},RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel- 'RecordPoint.Connectors.SDK.Client.Models.SubmissionMetaDataModelExtensions.AddOrUpdate(System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.SubmissionMetaDataModel},RecordPoint.Connectors.SDK.Client.Models.SubmissionMetaDataModel)')
  - [GetValueOrDefault(metaDataList,name)](#M-RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModelExtensions-GetValueOrDefault-System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel},System-String- 'RecordPoint.Connectors.SDK.Client.Models.SubmissionMetaDataModelExtensions.GetValueOrDefault(System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.SubmissionMetaDataModel},System.String)')
- [SubmitContext](#T-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext')
  - [NoExternalIdFound](#F-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-NoExternalIdFound 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext.NoExternalIdFound')
  - [NoTitleFound](#F-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-NoTitleFound 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext.NoTitleFound')
  - [AggregationFoundDuringItemSubmission](#P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-AggregationFoundDuringItemSubmission 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext.AggregationFoundDuringItemSubmission')
  - [ApiClientFactorySettings](#P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-ApiClientFactorySettings 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext.ApiClientFactorySettings')
  - [AuthenticationHelperSettings](#P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-AuthenticationHelperSettings 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext.AuthenticationHelperSettings')
  - [CancellationToken](#P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-CancellationToken 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext.CancellationToken')
  - [ConnectorConfigId](#P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-ConnectorConfigId 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext.ConnectorConfigId')
  - [CoreMetaData](#P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-CoreMetaData 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext.CoreMetaData')
  - [CorrelationId](#P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-CorrelationId 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext.CorrelationId')
  - [Filters](#P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-Filters 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext.Filters')
  - [ItemTypeId](#P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-ItemTypeId 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext.ItemTypeId')
  - [Relationships](#P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-Relationships 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext.Relationships')
  - [SourceMetaData](#P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-SourceMetaData 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext.SourceMetaData')
  - [SubmitResult](#P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-SubmitResult 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext.SubmitResult')
  - [TenantId](#P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-TenantId 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext.TenantId')
  - [GetExternalId()](#M-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-GetExternalId 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext.GetExternalId')
  - [GetTitle()](#M-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-GetTitle 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext.GetTitle')
  - [LogPrefix()](#M-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-LogPrefix 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext.LogPrefix')
- [SubmitResult](#T-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitResult')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult-#ctor 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitResult.#ctor')
  - [Exception](#P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult-Exception 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitResult.Exception')
  - [Reason](#P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult-Reason 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitResult.Reason')
  - [SubmitStatus](#P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult-SubmitStatus 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitResult.SubmitStatus')
  - [WaitUntilTime](#P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult-WaitUntilTime 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitResult.WaitUntilTime')
- [TooManyRequestsException](#T-RecordPoint-Connectors-SDK-Exceptions-TooManyRequestsException 'RecordPoint.Connectors.SDK.Exceptions.TooManyRequestsException')
  - [#ctor(message,time)](#M-RecordPoint-Connectors-SDK-Exceptions-TooManyRequestsException-#ctor-System-String,System-DateTime- 'RecordPoint.Connectors.SDK.Exceptions.TooManyRequestsException.#ctor(System.String,System.DateTime)')
  - [#ctor(info,context)](#M-RecordPoint-Connectors-SDK-Exceptions-TooManyRequestsException-#ctor-System-Runtime-Serialization-SerializationInfo,System-Runtime-Serialization-StreamingContext- 'RecordPoint.Connectors.SDK.Exceptions.TooManyRequestsException.#ctor(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)')
  - [WaitUntilTime](#P-RecordPoint-Connectors-SDK-Exceptions-TooManyRequestsException-WaitUntilTime 'RecordPoint.Connectors.SDK.Exceptions.TooManyRequestsException.WaitUntilTime')
  - [GetObjectData(info,context)](#M-RecordPoint-Connectors-SDK-Exceptions-TooManyRequestsException-GetObjectData-System-Runtime-Serialization-SerializationInfo,System-Runtime-Serialization-StreamingContext- 'RecordPoint.Connectors.SDK.Exceptions.TooManyRequestsException.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)')
- [ValidationHelper](#T-RecordPoint-Connectors-SDK-Helpers-ValidationHelper 'RecordPoint.Connectors.SDK.Helpers.ValidationHelper')
  - [ArgumentNotNull(argumentValue,argumentName)](#M-RecordPoint-Connectors-SDK-Helpers-ValidationHelper-ArgumentNotNull-System-Object,System-String- 'RecordPoint.Connectors.SDK.Helpers.ValidationHelper.ArgumentNotNull(System.Object,System.String)')
  - [ArgumentNotNullOrEmpty(argumentValue,argumentName)](#M-RecordPoint-Connectors-SDK-Helpers-ValidationHelper-ArgumentNotNullOrEmpty-System-String,System-String- 'RecordPoint.Connectors.SDK.Helpers.ValidationHelper.ArgumentNotNullOrEmpty(System.String,System.String)')
  - [ArgumentNotNullOrEmpty(argumentValue,argumentName)](#M-RecordPoint-Connectors-SDK-Helpers-ValidationHelper-ArgumentNotNullOrEmpty-System-Security-SecureString,System-String- 'RecordPoint.Connectors.SDK.Helpers.ValidationHelper.ArgumentNotNullOrEmpty(System.Security.SecureString,System.String)')
  - [ArgumentNotNullOrWhiteSpace(argumentValue,argumentName)](#M-RecordPoint-Connectors-SDK-Helpers-ValidationHelper-ArgumentNotNullOrWhiteSpace-System-String,System-String- 'RecordPoint.Connectors.SDK.Helpers.ValidationHelper.ArgumentNotNullOrWhiteSpace(System.String,System.String)')

<a name='T-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel'></a>
## AggregationSubmissionInputModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Client.Models

<a name='M-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the AggregationSubmissionInputModel
class.

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-#ctor-System-String,System-String,System-String,System-DateTime,System-String,System-DateTime,System-String,System-Nullable{System-Int32},System-String,System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel},System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-RelationshipDataModel},System-String,System-String,System-String,System-String,System-String,System-String,System-String,System-String-'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the AggregationSubmissionInputModel
class.

##### Parameters

This constructor has no parameters.

<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-Author'></a>
### Author `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-BarcodeType'></a>
### BarcodeType `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-BarcodeValue'></a>
### BarcodeValue `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-ConnectorId'></a>
### ConnectorId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-CorrelationId'></a>
### CorrelationId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-ExternalId'></a>
### ExternalId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-ItemTypeId'></a>
### ItemTypeId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-Location'></a>
### Location `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-MediaType'></a>
### MediaType `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-ParentExternalId'></a>
### ParentExternalId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-RecordCategoryId'></a>
### RecordCategoryId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-Relationships'></a>
### Relationships `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-SecurityProfileIdentifier'></a>
### SecurityProfileIdentifier `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-SourceCreatedBy'></a>
### SourceCreatedBy `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-SourceCreatedDate'></a>
### SourceCreatedDate `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-SourceLastModifiedBy'></a>
### SourceLastModifiedBy `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-SourceLastModifiedDate'></a>
### SourceLastModifiedDate `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-SourceProperties'></a>
### SourceProperties `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-Title'></a>
### Title `property`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionInputModel-Validate'></a>
### Validate() `method`

##### Summary

Validate the object.

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.ValidationException](#T-Microsoft-Rest-ValidationException 'Microsoft.Rest.ValidationException') | Thrown if validation fails |

<a name='T-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel'></a>
## AggregationSubmissionOutputModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Client.Models

<a name='M-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the AggregationSubmissionOutputModel
class.

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-#ctor-System-String,System-String,System-String,System-DateTime,System-String,System-DateTime,System-String,System-String,System-String,System-String,System-Nullable{System-DateTime},System-String,System-Nullable{System-DateTime},System-String,System-String,System-Nullable{System-Boolean},System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-MetaDataModel},System-String,System-String,System-String,System-String,System-String,System-String,System-String,System-String-'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the AggregationSubmissionOutputModel
class.

##### Parameters

This constructor has no parameters.

<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-Author'></a>
### Author `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-BarcodeType'></a>
### BarcodeType `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-BarcodeValue'></a>
### BarcodeValue `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-ConnectorId'></a>
### ConnectorId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-ContentSource'></a>
### ContentSource `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-CorrelationId'></a>
### CorrelationId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-CreatedBy'></a>
### CreatedBy `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-CreatedDate'></a>
### CreatedDate `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-ExternalId'></a>
### ExternalId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-Id'></a>
### Id `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-IsVitalRecord'></a>
### IsVitalRecord `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-ItemNumber'></a>
### ItemNumber `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-ItemType'></a>
### ItemType `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-LastModifiedBy'></a>
### LastModifiedBy `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-LastModifiedDate'></a>
### LastModifiedDate `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-Location'></a>
### Location `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-MediaType'></a>
### MediaType `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-ParentExternalId'></a>
### ParentExternalId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-RecordCategoryId'></a>
### RecordCategoryId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-SourceCreatedBy'></a>
### SourceCreatedBy `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-SourceCreatedDate'></a>
### SourceCreatedDate `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-SourceLastModifiedBy'></a>
### SourceLastModifiedBy `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-SourceLastModifiedDate'></a>
### SourceLastModifiedDate `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-SourceProperties'></a>
### SourceProperties `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-Title'></a>
### Title `property`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Client-Models-AggregationSubmissionOutputModel-Validate'></a>
### Validate() `method`

##### Summary

Validate the object.

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.ValidationException](#T-Microsoft-Rest-ValidationException 'Microsoft.Rest.ValidationException') | Thrown if validation fails |

<a name='T-RecordPoint-Connectors-SDK-Client-ApiClientFactorySettings'></a>
## ApiClientFactorySettings `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary

Settings used when creating a new ApiClient used for calling the Records365 vNext Connector API.

<a name='P-RecordPoint-Connectors-SDK-Client-ApiClientFactorySettings-ConnectorApiUrl'></a>
### ConnectorApiUrl `property`

##### Summary

The base URL of the Records365 vNext Connector API to call.

<a name='P-RecordPoint-Connectors-SDK-Client-ApiClientFactorySettings-ServerCertificateValidation'></a>
### ServerCertificateValidation `property`

##### Summary

Set to false to skip validation of the server SSL certificate at the
Records365 vNext Connector API endpoint, true otherwise.

<a name='T-RecordPoint-Connectors-SDK-Fields-AuditEvent'></a>
## AuditEvent `type`

##### Namespace

RecordPoint.Connectors.SDK.Fields

<a name='F-RecordPoint-Connectors-SDK-Fields-AuditEvent-Created'></a>
### Created `constants`

##### Summary

The created.

<a name='F-RecordPoint-Connectors-SDK-Fields-AuditEvent-Description'></a>
### Description `constants`

##### Summary

The description.

<a name='F-RecordPoint-Connectors-SDK-Fields-AuditEvent-EventExternalId'></a>
### EventExternalId `constants`

##### Summary

The event external id.

<a name='F-RecordPoint-Connectors-SDK-Fields-AuditEvent-EventType'></a>
### EventType `constants`

##### Summary

The event type.

<a name='F-RecordPoint-Connectors-SDK-Fields-AuditEvent-ExternalId'></a>
### ExternalId `constants`

##### Summary

The external id.

<a name='F-RecordPoint-Connectors-SDK-Fields-AuditEvent-UserId'></a>
### UserId `constants`

##### Summary

The user id.

<a name='F-RecordPoint-Connectors-SDK-Fields-AuditEvent-UserName'></a>
### UserName `constants`

##### Summary

The user name.

<a name='T-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings'></a>
## AuthenticationHelperSettings `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary

Provides the necessary information required to authenticate and obtain an access token for use
with the Records365 Connector API.

<a name='P-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings-AuthenticationResource'></a>
### AuthenticationResource `property`

##### Summary

Authentication resource.

<a name='P-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings-ClientId'></a>
### ClientId `property`

##### Summary

Client Id.

<a name='P-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings-ClientSecret'></a>
### ClientSecret `property`

##### Summary

Client secret.

<a name='P-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings-TenantDomainName'></a>
### TenantDomainName `property`

##### Summary

Tenant domain name.

<a name='T-RecordPoint-Connectors-SDK-Client-Models-BinarySubmissionInputModel'></a>
## BinarySubmissionInputModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Client.Models

##### Summary

This class has been added for backwards compatibility reasons

<a name='M-RecordPoint-Connectors-SDK-Client-Models-BinarySubmissionInputModel-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the BinarySubmissionInputModel class.

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-BinarySubmissionInputModel-#ctor-System-String,System-String,System-String,System-String,System-String-'></a>
### #ctor(connectorId,itemExternalId,binaryExternalId,fileName,correlationId) `constructor`

##### Summary

Initializes a new instance of the BinarySubmissionInputModel class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The ID of the connector submitting the binary |
| itemExternalId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The ExternalID of the item that the binary belongs to |
| binaryExternalId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The ExternalID of the binary |
| fileName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | An optional file name to associate with the binary |
| correlationId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | An optional ID used to track the binary version as it moves through the pipeline |

<a name='P-RecordPoint-Connectors-SDK-Client-Models-BinarySubmissionInputModel-BinaryExternalId'></a>
### BinaryExternalId `property`

##### Summary

Gets or sets the ExternalID of the binary

<a name='P-RecordPoint-Connectors-SDK-Client-Models-BinarySubmissionInputModel-ConnectorId'></a>
### ConnectorId `property`

##### Summary

Gets or sets the ID of the connector submitting the binary

<a name='P-RecordPoint-Connectors-SDK-Client-Models-BinarySubmissionInputModel-CorrelationId'></a>
### CorrelationId `property`

##### Summary

Gets or sets an optional ID used to track the binary version as it
moves through the pipeline

<a name='P-RecordPoint-Connectors-SDK-Client-Models-BinarySubmissionInputModel-FileName'></a>
### FileName `property`

##### Summary

Gets or sets an optional file name to associate with the binary

<a name='P-RecordPoint-Connectors-SDK-Client-Models-BinarySubmissionInputModel-ItemExternalId'></a>
### ItemExternalId `property`

##### Summary

Gets or sets the ExternalID of the item that the binary belongs to

<a name='M-RecordPoint-Connectors-SDK-Client-Models-BinarySubmissionInputModel-Validate'></a>
### Validate() `method`

##### Summary

Validate the object.

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.ValidationException](#T-Microsoft-Rest-ValidationException 'Microsoft.Rest.ValidationException') | Thrown if validation fails |

<a name='T-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel'></a>
## ConnectorAuditEventModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Client.Models

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the ConnectorAuditEventModel class.

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel-#ctor-System-String,System-String,System-String,System-String,System-Nullable{System-DateTime},System-String,System-String,System-String,System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel}-'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the ConnectorAuditEventModel class.

##### Parameters

This constructor has no parameters.

<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel-ConnectorId'></a>
### ConnectorId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel-CreatedDate'></a>
### CreatedDate `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel-Description'></a>
### Description `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel-EventExternalId'></a>
### EventExternalId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel-EventType'></a>
### EventType `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel-ItemExternalId'></a>
### ItemExternalId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel-SourceProperties'></a>
### SourceProperties `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel-UserId'></a>
### UserId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel-UserName'></a>
### UserName `property`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Client-Models-ConnectorAuditEventModel-Validate'></a>
### Validate() `method`

##### Summary

Validate the object.

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.ValidationException](#T-Microsoft-Rest-ValidationException 'Microsoft.Rest.ValidationException') | Thrown if validation fails |

<a name='T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel'></a>
## ConnectorConfigModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Client.Models

##### Summary

The connector config model.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the ConnectorConfigModel class.

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-#ctor-System-String,System-String,System-Boolean,System-String,System-String,System-String,System-String,System-String,System-Nullable{System-DateTime},System-Nullable{System-DateTime},System-String,System-String,System-String,System-String,System-String,System-String,System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-MetaDataModel},System-String,System-Nullable{System-Boolean},RecordPoint-Connectors-SDK-Client-Models-FiltersModel-'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the ConnectorConfigModel class.

##### Parameters

This constructor has no parameters.

<a name='F-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-_connectorTypeIdAsGuid'></a>
### _connectorTypeIdAsGuid `constants`

##### Summary

The connector type id as guid.

<a name='F-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-_idAsGuid'></a>
### _idAsGuid `constants`

##### Summary

The id as guid.

<a name='F-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-_tenantIdAsGuid'></a>
### _tenantIdAsGuid `constants`

##### Summary

The tenant id as guid.

<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-ClientId'></a>
### ClientId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-ConnectorTypeConfigurationId'></a>
### ConnectorTypeConfigurationId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-ConnectorTypeId'></a>
### ConnectorTypeId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-CreatedBy'></a>
### CreatedBy `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-CreatedDate'></a>
### CreatedDate `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-DisplayName'></a>
### DisplayName `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-EnabledHistory'></a>
### EnabledHistory `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-Filters'></a>
### Filters `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-HasSubmittedData'></a>
### HasSubmittedData `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-Id'></a>
### Id `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-ModifiedBy'></a>
### ModifiedBy `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-ModifiedDate'></a>
### ModifiedDate `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-OriginatingOrganization'></a>
### OriginatingOrganization `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-Properties'></a>
### Properties `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-ProtectionEnabled'></a>
### ProtectionEnabled `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-Status'></a>
### Status `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-StatusCode'></a>
### StatusCode `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-TenantDomainName'></a>
### TenantDomainName `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-TenantId'></a>
### TenantId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-TransactionId'></a>
### TransactionId `property`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-ConnectorTypeIdAsGuid'></a>
### ConnectorTypeIdAsGuid() `method`

##### Summary

Gets the ID of the ConnectorType for this instance as a GUID.
Assumes that the ConnectorTypeId field never changes.

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-CustomInit'></a>
### CustomInit() `method`

##### Summary

Customs the init.

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-GetADTenantId'></a>
### GetADTenantId() `method`

##### Summary

Get AD tenant id.

##### Returns

A string

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-GetIdAsGuid'></a>
### GetIdAsGuid() `method`

##### Summary

Gets the ID of this Connector instance as a GUID.
Assumes that the Id field never changes.

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-GetPropertyOrDefault-System-String-'></a>
### GetPropertyOrDefault(name) `method`

##### Summary

Get property or default.

##### Returns

A string

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name. |

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-GetTenantIdAsGuid'></a>
### GetTenantIdAsGuid() `method`

##### Summary

Gets the ID of the Records365 vNext tenant that owns this
connector instance as a GUID.
Assumes that the TenantId field never changes.

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-HasBeenEnabled'></a>
### HasBeenEnabled() `method`

##### Summary

Is or has the Connector ever been enabled?

##### Returns

Boolean

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-IsDisabledConnectorExpired-System-Int32-'></a>
### IsDisabledConnectorExpired(maxDisabledConnectorAge) `method`

##### Summary

Checks if the Connector is Disabled and if it has been disabled for more than the specified time.
If the Configuration does not have a DisabledTime property, it is considered expired.
If the maxDisabledConnectorAge is less than 0, the Configuration is not considered expired.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| maxDisabledConnectorAge | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The maximum time in seconds a configuration can be disabled before it should be abandoned |

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-IsEnabled'></a>
### IsEnabled() `method`

##### Summary

Is the Connector Configuration currently enabled?

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-SetProperty-System-String,System-String,System-String-'></a>
### SetProperty(name,value,type) `method`

##### Summary

Set the property.

##### Returns

A ConnectorConfigModel

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name. |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The value. |
| type | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The type. |

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-SetProperty-System-String,System-Object-'></a>
### SetProperty(name,value) `method`

##### Summary

Set the property.

##### Returns

A ConnectorConfigModel

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name. |
| value | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The value. |

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-Validate'></a>
### Validate() `method`

##### Summary

Validate the object.

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.ValidationException](#T-Microsoft-Rest-ValidationException 'Microsoft.Rest.ValidationException') | Thrown if validation fails |

<a name='T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel'></a>
## ConnectorNotificationAcknowledgeModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Client.Models

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the
ConnectorNotificationAcknowledgeModel class.

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel-#ctor-System-String,System-String,System-String,System-String-'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the
ConnectorNotificationAcknowledgeModel class.

##### Parameters

This constructor has no parameters.

<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel-ConnectorId'></a>
### ConnectorId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel-ConnectorStatusMessage'></a>
### ConnectorStatusMessage `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel-NotificationId'></a>
### NotificationId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel-ProcessingResult'></a>
### ProcessingResult `property`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel-Validate'></a>
### Validate() `method`

##### Summary

Validate the object.

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.ValidationException](#T-Microsoft-Rest-ValidationException 'Microsoft.Rest.ValidationException') | Thrown if validation fails |

<a name='T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel'></a>
## ConnectorNotificationModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Client.Models

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the ConnectorNotificationModel class.

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel-#ctor-System-String,System-String,System-Nullable{System-DateTime},System-String,System-String,RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel,RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,System-Object-'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the ConnectorNotificationModel class.

##### Parameters

This constructor has no parameters.

<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel-ConnectorConfig'></a>
### ConnectorConfig `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel-ConnectorId'></a>
### ConnectorId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel-Context'></a>
### Context `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel-Id'></a>
### Id `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel-Item'></a>
### Item `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel-NotificationType'></a>
### NotificationType `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel-TenantId'></a>
### TenantId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel-Timestamp'></a>
### Timestamp `property`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel-Validate'></a>
### Validate() `method`

##### Summary

Validate the object.

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.ValidationException](#T-Microsoft-Rest-ValidationException 'Microsoft.Rest.ValidationException') | Thrown if validation fails |

<a name='T-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel'></a>
## DirectBinarySubmissionInputModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Client.Models

<a name='M-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the DirectBinarySubmissionInputModel
class.

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-#ctor-System-String,System-String,System-String,System-String,System-Nullable{System-Int64},System-String,System-Nullable{System-DateTime},System-String,System-String,System-String,System-Nullable{System-Boolean},System-Nullable{System-Boolean},System-Nullable{System-DateTime}-'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the DirectBinarySubmissionInputModel
class.

##### Parameters

This constructor has no parameters.

<a name='P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-BinaryExternalId'></a>
### BinaryExternalId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-ConnectorId'></a>
### ConnectorId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-CorrelationId'></a>
### CorrelationId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-FileHash'></a>
### FileHash `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-FileName'></a>
### FileName `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-FileSize'></a>
### FileSize `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-IsOldVersion'></a>
### IsOldVersion `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-ItemExternalId'></a>
### ItemExternalId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-ItemSourceLastModifiedDate'></a>
### ItemSourceLastModifiedDate `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-Location'></a>
### Location `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-MimeType'></a>
### MimeType `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-SkipEnrichment'></a>
### SkipEnrichment `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-SourceLastModifiedDate'></a>
### SourceLastModifiedDate `property`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel-Validate'></a>
### Validate() `method`

##### Summary

Validate the object.

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.ValidationException](#T-Microsoft-Rest-ValidationException 'Microsoft.Rest.ValidationException') | Thrown if validation fails |

<a name='T-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionResponseModel'></a>
## DirectBinarySubmissionResponseModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Client.Models

<a name='M-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionResponseModel-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the
DirectBinarySubmissionResponseModel class.

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionResponseModel-#ctor-System-String,System-Nullable{System-Int64}-'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the
DirectBinarySubmissionResponseModel class.

##### Parameters

This constructor has no parameters.

<a name='P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionResponseModel-MaxFileSize'></a>
### MaxFileSize `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionResponseModel-Url'></a>
### Url `property`

##### Summary



<a name='T-RecordPoint-Connectors-SDK-Client-Models-ErrorModel'></a>
## ErrorModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Client.Models

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ErrorModel-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the ErrorModel class.

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ErrorModel-#ctor-System-String,System-String,System-String,System-String,System-Nullable{System-DateTime},System-String,System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-ErrorModel},System-Collections-Generic-IDictionary{System-String,System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-ErrorModel}}-'></a>
### #ctor(type,severity) `constructor`

##### Summary

Initializes a new instance of the ErrorModel class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Possible values include: 'General', 'Aggregate', 'Validation', 'PartialSuccess' |
| severity | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Possible values include: 'Critical', 'Error', 'Warning', 'Informational' |

<a name='P-RecordPoint-Connectors-SDK-Client-Models-ErrorModel-AggregateErrors'></a>
### AggregateErrors `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ErrorModel-DateTime'></a>
### DateTime `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ErrorModel-InnerError'></a>
### InnerError `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ErrorModel-Message'></a>
### Message `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ErrorModel-MessageCode'></a>
### MessageCode `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ErrorModel-Severity'></a>
### Severity `property`

##### Summary

Gets or sets possible values include: 'Critical', 'Error',
'Warning', 'Informational'

<a name='P-RecordPoint-Connectors-SDK-Client-Models-ErrorModel-Target'></a>
### Target `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ErrorModel-Type'></a>
### Type `property`

##### Summary

Gets or sets possible values include: 'General', 'Aggregate',
'Validation', 'PartialSuccess'

<a name='T-RecordPoint-Connectors-SDK-Client-Models-ErrorResponseModel'></a>
## ErrorResponseModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Client.Models

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ErrorResponseModel-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the ErrorResponseModel class.

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ErrorResponseModel-#ctor-RecordPoint-Connectors-SDK-Client-Models-ErrorModel-'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the ErrorResponseModel class.

##### Parameters

This constructor has no parameters.

<a name='P-RecordPoint-Connectors-SDK-Client-Models-ErrorResponseModel-Error'></a>
### Error `property`

##### Summary



<a name='T-RecordPoint-Connectors-SDK-Fields'></a>
## Fields `type`

##### Namespace

RecordPoint.Connectors.SDK

##### Summary

The fields.

<a name='F-RecordPoint-Connectors-SDK-Fields-Author'></a>
### Author `constants`

##### Summary

The author.

<a name='F-RecordPoint-Connectors-SDK-Fields-BarcodeType'></a>
### BarcodeType `constants`

##### Summary

The barcode type.

<a name='F-RecordPoint-Connectors-SDK-Fields-BarcodeValue'></a>
### BarcodeValue `constants`

##### Summary

The barcode value.

<a name='F-RecordPoint-Connectors-SDK-Fields-ConnectorId'></a>
### ConnectorId `constants`

##### Summary

The connector id.

<a name='F-RecordPoint-Connectors-SDK-Fields-ContentVersion'></a>
### ContentVersion `constants`

##### Summary

The content version.

<a name='F-RecordPoint-Connectors-SDK-Fields-ExternalId'></a>
### ExternalId `constants`

##### Summary

The external id.

<a name='F-RecordPoint-Connectors-SDK-Fields-ItemTypeId'></a>
### ItemTypeId `constants`

##### Summary

The item type id.

<a name='F-RecordPoint-Connectors-SDK-Fields-Location'></a>
### Location `constants`

##### Summary

The location.

<a name='F-RecordPoint-Connectors-SDK-Fields-MediaType'></a>
### MediaType `constants`

##### Summary

The media type.

<a name='F-RecordPoint-Connectors-SDK-Fields-MimeType'></a>
### MimeType `constants`

##### Summary

The mime type.

<a name='F-RecordPoint-Connectors-SDK-Fields-ParentExternalId'></a>
### ParentExternalId `constants`

##### Summary

The parent external id.

<a name='F-RecordPoint-Connectors-SDK-Fields-RecordCategoryID'></a>
### RecordCategoryID `constants`

##### Summary

The record category ID.

<a name='F-RecordPoint-Connectors-SDK-Fields-SecurityProfileIdentifier'></a>
### SecurityProfileIdentifier `constants`

##### Summary

The security profile identifier.

<a name='F-RecordPoint-Connectors-SDK-Fields-SourceCreatedBy'></a>
### SourceCreatedBy `constants`

##### Summary

The source created by.

<a name='F-RecordPoint-Connectors-SDK-Fields-SourceCreatedDate'></a>
### SourceCreatedDate `constants`

##### Summary

The source created date.

<a name='F-RecordPoint-Connectors-SDK-Fields-SourceLastModifiedBy'></a>
### SourceLastModifiedBy `constants`

##### Summary

The source last modified by.

<a name='F-RecordPoint-Connectors-SDK-Fields-SourceLastModifiedDate'></a>
### SourceLastModifiedDate `constants`

##### Summary

The source last modified date.

<a name='F-RecordPoint-Connectors-SDK-Fields-Title'></a>
### Title `constants`

##### Summary

The title.

<a name='T-RecordPoint-Connectors-SDK-Client-Models-FiltersModel'></a>
## FiltersModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Client.Models

<a name='M-RecordPoint-Connectors-SDK-Client-Models-FiltersModel-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the FiltersModel class.

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-FiltersModel-#ctor-RecordPoint-Connectors-SDK-Client-Models-SearchTreeNodeModel,RecordPoint-Connectors-SDK-Client-Models-SearchTreeNodeModel,System-String,System-String-'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the FiltersModel class.

##### Parameters

This constructor has no parameters.

<a name='P-RecordPoint-Connectors-SDK-Client-Models-FiltersModel-Excluded'></a>
### Excluded `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-FiltersModel-ExcludedExpression'></a>
### ExcludedExpression `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-FiltersModel-Included'></a>
### Included `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-FiltersModel-IncludedExpression'></a>
### IncludedExpression `property`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Client-Models-FiltersModel-Validate'></a>
### Validate() `method`

##### Summary

Validate the object.

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.ValidationException](#T-Microsoft-Rest-ValidationException 'Microsoft.Rest.ValidationException') | Thrown if validation fails |

<a name='T-RecordPoint-Connectors-SDK-Interfaces-ICircuitEventHandler'></a>
## ICircuitEventHandler `type`

##### Namespace

RecordPoint.Connectors.SDK.Interfaces

##### Summary



<a name='T-RecordPoint-Connectors-SDK-Providers-ICircuitProvider'></a>
## ICircuitProvider `type`

##### Namespace

RecordPoint.Connectors.SDK.Providers

##### Summary

Should not be used directly, as this restricts our ability to do an IoC injection of a CircuitProvider.
We may need the ability to have two different CircuitProviders in a single service.
and services may wish to react to these CircuitProviders in different fashions
Instead, should create an empty interface that inherits from this, e.g. IPostgresqlCircuitProvider

<a name='M-RecordPoint-Connectors-SDK-Providers-ICircuitProvider-IsCircuitClosed-System-TimeSpan@-'></a>
### IsCircuitClosed() `method`

##### Summary

Returns the state of this Circuit. Can be referenced to understand whether or not a long-running operation
(e.g. queue processing or an Azure Service Bus message pump) should be enabled and attempt operations
which will interact with the resource the circuit protects.
Additionally, outputs a recommended time to wait for before retrying.

##### Returns

True if the circuit is closed (the resource can be used)
False if the circuit is open (you should wait before using the resource)

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Interfaces-IConnectorConfigAccessor'></a>
## IConnectorConfigAccessor `type`

##### Namespace

RecordPoint.Connectors.SDK.Interfaces

##### Summary

An abstraction over access of connector configuration objects.

<a name='M-RecordPoint-Connectors-SDK-Interfaces-IConnectorConfigAccessor-GetConnectorConfig-System-Guid,System-Guid-'></a>
### GetConnectorConfig(tenantId,connectorConfigId) `method`

##### Summary

Gets a ConnectorConfigModel by tenant ID and connector config ID.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| tenantId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') |  |
| connectorConfigId | [System.Guid](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Guid 'System.Guid') |  |

<a name='T-RecordPoint-Connectors-SDK-Providers-IDateTimeProvider'></a>
## IDateTimeProvider `type`

##### Namespace

RecordPoint.Connectors.SDK.Providers

##### Summary

Provides an ability to inject and Moq UtcNow to test with a different time

<a name='P-RecordPoint-Connectors-SDK-Providers-IDateTimeProvider-UtcNow'></a>
### UtcNow `property`

##### Summary



<a name='T-RecordPoint-Connectors-SDK-Diagnostics-ILog'></a>
## ILog `type`

##### Namespace

RecordPoint.Connectors.SDK.Diagnostics

##### Summary

Provides a generic log interface.

<a name='M-RecordPoint-Connectors-SDK-Diagnostics-ILog-LogMessage-System-Type,System-String,System-String,System-Nullable{System-Int64}-'></a>
### LogMessage(callerType,methodName,message,elapsedTimeTicks) `method`

##### Summary

Logs a message.
Used for logging relatively common operations which have succeeded (But are important enough that a record of their success is required
or transient exceptions which are being retried and have not yet reached their maximum retries

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| callerType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The Type of the calling class (Use GetType() or typeof() depending on context) |
| methodName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the method being timed. The name of the method will be appended with _Faulted if an exception occurs |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Any message to be output. Generally, this should just be enough information to provide some context for the event. |
| elapsedTimeTicks | [System.Nullable{System.Int64}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int64}') | Optionally, provide the duration which the operation took in ticks. If a duration is not provided, no indication of the duration should occur in the final logging |

<a name='M-RecordPoint-Connectors-SDK-Diagnostics-ILog-LogVerbose-System-Type,System-String,System-String,System-Nullable{System-Int64}-'></a>
### LogVerbose(callerType,methodName,message,elapsedTimeTicks) `method`

##### Summary

Logs a verbose message.
Used for logging operations which are extremely numerous and are busness as usual operations

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| callerType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The Type of the calling class (Use GetType() or typeof() depending on context) |
| methodName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the method being timed. The name of the method will be appended with _Faulted if an exception occurs |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Any message to be output. Generally, this should just be enough information to provide some context for the event. |
| elapsedTimeTicks | [System.Nullable{System.Int64}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int64}') | Optionally, provide the duration which the operation took in ticks. If a duration is not provided, no indication of the duration should occur in the final logging |

<a name='M-RecordPoint-Connectors-SDK-Diagnostics-ILog-LogWarning-System-Type,System-String,System-String,System-Nullable{System-Int64}-'></a>
### LogWarning(callerType,methodName,message,elapsedTimeTicks) `method`

##### Summary

Logs a warning message.
Used to indicate a fault of some sort that will not affect the overall service, most likely a handled exception

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| callerType | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The Type of the calling class (Use GetType() or typeof() depending on context) |
| methodName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the method being timed. The name of the method will be appended with _Faulted if an exception occurs |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Any message to be output. Generally, this should just be enough information to provide some context for the event. |
| elapsedTimeTicks | [System.Nullable{System.Int64}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.Int64}') | Optionally, provide the duration which the operation took in ticks. If a duration is not provided, no indication of the duration should occur in the final logging |

<a name='T-RecordPoint-Connectors-SDK-Notifications-INotificationHandler'></a>
## INotificationHandler `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications

##### Summary

Provide an implementation of this interface to NotificationTask to implement
connector-specific details of notification handling.

<a name='M-RecordPoint-Connectors-SDK-Notifications-INotificationHandler-HandleNotification-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken-'></a>
### HandleNotification(connectorConfigModel,notification,ct) `method`

##### Summary

Provides the implementation for handling of a notification.
If this method runs to completion, NotificationTask will acknowledge the notification with 
ProcessingResult.OK.
If any exception (other than TaskCanceledException) is thrown from this method,
NotificationTask will acknowledge the notification with ProcessingResult.NotificationError.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorConfigModel | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') |  |
| notification | [RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel') |  |
| ct | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='T-RecordPoint-Connectors-SDK-Notifications-INotificationPullManager'></a>
## INotificationPullManager `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Notifications-INotificationPullManager-AcknowledgeNotification-RecordPoint-Connectors-SDK-Client-ApiClientFactorySettings,RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings,RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel,System-Threading-CancellationToken-'></a>
### AcknowledgeNotification(factorySettings,authenticationSettings,acknowledgement,cancellationToken) `method`

##### Summary

Acknowledges a notification as having been processed.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| factorySettings | [RecordPoint.Connectors.SDK.Client.ApiClientFactorySettings](#T-RecordPoint-Connectors-SDK-Client-ApiClientFactorySettings 'RecordPoint.Connectors.SDK.Client.ApiClientFactorySettings') |  |
| authenticationSettings | [RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings](#T-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings 'RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings') |  |
| acknowledgement | [RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationAcknowledgeModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationAcknowledgeModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationAcknowledgeModel') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Notifications-INotificationPullManager-GetAllPendingConnectorNotifications-RecordPoint-Connectors-SDK-Client-ApiClientFactorySettings,RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings,System-String,System-Threading-CancellationToken-'></a>
### GetAllPendingConnectorNotifications(factorySettings,authenticationSettings,connectorConfigId,cancellationToken) `method`

##### Summary

Queries for all connector notifications for a given connector instance that are pending acknowledgement.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| factorySettings | [RecordPoint.Connectors.SDK.Client.ApiClientFactorySettings](#T-RecordPoint-Connectors-SDK-Client-ApiClientFactorySettings 'RecordPoint.Connectors.SDK.Client.ApiClientFactorySettings') |  |
| authenticationSettings | [RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings](#T-RecordPoint-Connectors-SDK-Client-AuthenticationHelperSettings 'RecordPoint.Connectors.SDK.Client.AuthenticationHelperSettings') |  |
| connectorConfigId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='T-RecordPoint-Connectors-SDK-Diagnostics-IPerformanceEvent'></a>
## IPerformanceEvent `type`

##### Namespace

RecordPoint.Connectors.SDK.Diagnostics

##### Summary

Represents a single performance monitored event.

<a name='M-RecordPoint-Connectors-SDK-Diagnostics-IPerformanceEvent-Exception-System-Exception-'></a>
### Exception(ex) `method`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| ex | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') |  |

<a name='T-RecordPoint-Connectors-SDK-Providers-ISdkAzureBlobCircuitProvider'></a>
## ISdkAzureBlobCircuitProvider `type`

##### Namespace

RecordPoint.Connectors.SDK.Providers

##### Summary



<a name='T-RecordPoint-Connectors-SDK-Providers-ISdkAzureBlobRetryProvider'></a>
## ISdkAzureBlobRetryProvider `type`

##### Namespace

RecordPoint.Connectors.SDK.Providers

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Providers-ISdkAzureBlobRetryProvider-ExecuteWithRetry-System-Func{System-Threading-Tasks-Task},System-Type,System-String-'></a>
### ExecuteWithRetry(codeToExecute,type,methodName) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| codeToExecute | [System.Func{System.Threading.Tasks.Task}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Func 'System.Func{System.Threading.Tasks.Task}') |  |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') |  |
| methodName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-RecordPoint-Connectors-SDK-Providers-ISettableCircuitProvider'></a>
## ISettableCircuitProvider `type`

##### Namespace

RecordPoint.Connectors.SDK.Providers

##### Summary

Interface to the Settable Circuit Provider.
This is a special type of circuit breaker which is set manually when a problem is detected.
To use:
* Inject into dependant code as a singleton.
* Call IsCircuitClosed before accessing protected resources.
* If the protected resource indicates it is under heavy load, call SetOpenUntil.

<a name='M-RecordPoint-Connectors-SDK-Providers-ISettableCircuitProvider-SetOpenUntil-System-DateTime-'></a>
### SetOpenUntil(newTime) `method`

##### Summary

Dependant code should call this when the protected resource is under heavy load.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| newTime | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') | The time that the dependant code should wait until before attempting to use the resource again. |

<a name='T-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission'></a>
## ISubmission `type`

##### Namespace

RecordPoint.Connectors.SDK.SubmitPipeline

##### Summary

Top level interface for classes that submit content or otherwise 
influence the submission behaviour.

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-ISubmission-Submit-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-'></a>
### Submit(submitContext) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| submitContext | [RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext](#T-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext 'RecordPoint.Connectors.SDK.SubmitPipeline.SubmitContext') |  |

<a name='T-RecordPoint-Connectors-SDK-Client-Models-ItemAcceptanceModel'></a>
## ItemAcceptanceModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Client.Models

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ItemAcceptanceModel-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the ItemAcceptanceModel class.

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ItemAcceptanceModel-#ctor-System-String,System-Nullable{System-DateTime},System-String-'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the ItemAcceptanceModel class.

##### Parameters

This constructor has no parameters.

<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemAcceptanceModel-AggregationStatus'></a>
### AggregationStatus `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemAcceptanceModel-ExternalId'></a>
### ExternalId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemAcceptanceModel-SourceLastModifiedDate'></a>
### SourceLastModifiedDate `property`

##### Summary



<a name='T-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel'></a>
## ItemSubmissionInputModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Client.Models

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the ItemSubmissionInputModel class.

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-#ctor-System-String,System-String,System-String,System-String,System-DateTime,System-String,System-String,System-DateTime,System-String,System-String,System-String,System-String,System-String,System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel},System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-RelationshipDataModel},System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-DirectBinarySubmissionInputModel},System-String,System-String,System-String,System-String-'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the ItemSubmissionInputModel class.

##### Parameters

This constructor has no parameters.

<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-Author'></a>
### Author `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-BarcodeType'></a>
### BarcodeType `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-BarcodeValue'></a>
### BarcodeValue `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-BinariesSubmitted'></a>
### BinariesSubmitted `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-ConnectorId'></a>
### ConnectorId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-ContentVersion'></a>
### ContentVersion `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-CorrelationId'></a>
### CorrelationId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-ExternalId'></a>
### ExternalId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-Location'></a>
### Location `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-MediaType'></a>
### MediaType `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-MimeType'></a>
### MimeType `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-ParentExternalId'></a>
### ParentExternalId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-Relationships'></a>
### Relationships `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-SecurityProfileIdentifier'></a>
### SecurityProfileIdentifier `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-SourceCreatedBy'></a>
### SourceCreatedBy `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-SourceCreatedDate'></a>
### SourceCreatedDate `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-SourceLastModifiedBy'></a>
### SourceLastModifiedBy `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-SourceLastModifiedDate'></a>
### SourceLastModifiedDate `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-SourceProperties'></a>
### SourceProperties `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-Title'></a>
### Title `property`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionInputModel-Validate'></a>
### Validate() `method`

##### Summary

Validate the object.

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.ValidationException](#T-Microsoft-Rest-ValidationException 'Microsoft.Rest.ValidationException') | Thrown if validation fails |

<a name='T-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel'></a>
## ItemSubmissionOutputModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Client.Models

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the ItemSubmissionOutputModel class.

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-#ctor-System-String,System-String,System-String,System-String,System-DateTime,System-String,System-String,System-DateTime,System-String,System-String,System-String,System-String,System-String,System-String,System-String,System-String,System-Nullable{System-DateTime},System-String,System-Nullable{System-DateTime},System-String,System-String,System-String,System-Nullable{System-Boolean},System-String,System-String,System-Nullable{System-DateTime},System-String,System-String,System-String,System-Nullable{System-DateTime},System-String,System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-MetaDataModel},System-String,System-String,System-String,System-String-'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the ItemSubmissionOutputModel class.

##### Parameters

This constructor has no parameters.

<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-Author'></a>
### Author `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-BarcodeType'></a>
### BarcodeType `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-BarcodeValue'></a>
### BarcodeValue `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-ConnectorDisplayName'></a>
### ConnectorDisplayName `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-ConnectorId'></a>
### ConnectorId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-ContentSource'></a>
### ContentSource `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-ContentVersion'></a>
### ContentVersion `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-CorrelationId'></a>
### CorrelationId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-CreatedBy'></a>
### CreatedBy `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-CreatedDate'></a>
### CreatedDate `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-CurrentDisposalStatus'></a>
### CurrentDisposalStatus `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-ExternalId'></a>
### ExternalId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-Format'></a>
### Format `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-Id'></a>
### Id `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-IsVitalRecord'></a>
### IsVitalRecord `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-ItemNumber'></a>
### ItemNumber `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-ItemType'></a>
### ItemType `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-LastModifiedBy'></a>
### LastModifiedBy `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-LastModifiedDate'></a>
### LastModifiedDate `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-Location'></a>
### Location `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-MediaType'></a>
### MediaType `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-MimeType'></a>
### MimeType `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-NextDisposalAction'></a>
### NextDisposalAction `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-NextDisposalDate'></a>
### NextDisposalDate `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-OriginatingOrganization'></a>
### OriginatingOrganization `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-ParentExternalId'></a>
### ParentExternalId `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-PreviousDisposalAction'></a>
### PreviousDisposalAction `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-PreviousDisposalBy'></a>
### PreviousDisposalBy `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-PreviousDisposalById'></a>
### PreviousDisposalById `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-PreviousDisposalDate'></a>
### PreviousDisposalDate `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-SourceCreatedBy'></a>
### SourceCreatedBy `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-SourceCreatedDate'></a>
### SourceCreatedDate `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-SourceLastModifiedBy'></a>
### SourceLastModifiedBy `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-SourceLastModifiedDate'></a>
### SourceLastModifiedDate `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-SourceProperties'></a>
### SourceProperties `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-Title'></a>
### Title `property`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Client-Models-ItemSubmissionOutputModel-Validate'></a>
### Validate() `method`

##### Summary

Validate the object.

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.ValidationException](#T-Microsoft-Rest-ValidationException 'Microsoft.Rest.ValidationException') | Thrown if validation fails |

<a name='T-RecordPoint-Connectors-SDK-Fields-MessageCode'></a>
## MessageCode `type`

##### Namespace

RecordPoint.Connectors.SDK.Fields

<a name='F-RecordPoint-Connectors-SDK-Fields-MessageCode-ConnectorNotEnabled'></a>
### ConnectorNotEnabled `constants`

##### Summary

The connector not enabled.

<a name='F-RecordPoint-Connectors-SDK-Fields-MessageCode-ProtectionNotEnabled'></a>
### ProtectionNotEnabled `constants`

##### Summary

The protection not enabled.

<a name='T-RecordPoint-Connectors-SDK-Fields-MetaDataKeys'></a>
## MetaDataKeys `type`

##### Namespace

RecordPoint.Connectors.SDK.Fields

##### Summary

Names of metadata keys must be valid C# identifiers:
https://docs.microsoft.com/en-us/rest/api/storageservices/Naming-and-Referencing-Containers--Blobs--and-Metadata#metadata-names

<a name='F-RecordPoint-Connectors-SDK-Fields-MetaDataKeys-ItemBinary_CorrelationId'></a>
### ItemBinary_CorrelationId `constants`

##### Summary

The item binary correlation id.

<a name='F-RecordPoint-Connectors-SDK-Fields-MetaDataKeys-ItemBinary_FileName'></a>
### ItemBinary_FileName `constants`

##### Summary

The item binary file name.

<a name='T-RecordPoint-Connectors-SDK-Client-Models-MetaDataModel'></a>
## MetaDataModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Client.Models

<a name='M-RecordPoint-Connectors-SDK-Client-Models-MetaDataModel-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the MetaDataModel class.

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-MetaDataModel-#ctor-System-String,System-String,System-String,System-Nullable{System-Boolean}-'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the MetaDataModel class.

##### Parameters

This constructor has no parameters.

<a name='P-RecordPoint-Connectors-SDK-Client-Models-MetaDataModel-IsSysAdminOnly'></a>
### IsSysAdminOnly `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-MetaDataModel-Name'></a>
### Name `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-MetaDataModel-Type'></a>
### Type `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-MetaDataModel-Value'></a>
### Value `property`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Client-Models-MetaDataModel-Validate'></a>
### Validate() `method`

##### Summary

Validate the object.

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.ValidationException](#T-Microsoft-Rest-ValidationException 'Microsoft.Rest.ValidationException') | Thrown if validation fails |

<a name='T-RecordPoint-Connectors-SDK-Client-MetaDataModelListExtensions'></a>
## MetaDataModelListExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary

The meta data model list extensions.

<a name='M-RecordPoint-Connectors-SDK-Client-MetaDataModelListExtensions-AddOrUpdate-System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-MetaDataModel},System-String,System-String,System-String-'></a>
### AddOrUpdate(metaDataList,name,type,value) `method`

##### Summary

Add or update.

##### Returns

IList<MetaDataModel>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| metaDataList | [System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.MetaDataModel}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IList 'System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.MetaDataModel}') | The meta data list. |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name. |
| type | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The type. |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The value. |

<a name='M-RecordPoint-Connectors-SDK-Client-MetaDataModelListExtensions-AddOrUpdate-System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-MetaDataModel},System-String,System-String-'></a>
### AddOrUpdate(metaDataList,name,value) `method`

##### Summary

Add or update.

##### Returns

IList<MetaDataModel>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| metaDataList | [System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.MetaDataModel}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IList 'System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.MetaDataModel}') | The meta data list. |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name. |
| value | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The value. |

<a name='M-RecordPoint-Connectors-SDK-Client-MetaDataModelListExtensions-GetValueOrDefault-System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-MetaDataModel},System-String-'></a>
### GetValueOrDefault(metaDataList,name) `method`

##### Summary

Get value or default.

##### Returns

A string

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| metaDataList | [System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.MetaDataModel}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IList 'System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.MetaDataModel}') | The meta data list. |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name. |

<a name='T-RecordPoint-Connectors-SDK-Diagnostics-PerformanceEvent'></a>
## PerformanceEvent `type`

##### Namespace

RecordPoint.Connectors.SDK.Diagnostics

##### Summary

Provides a simple wrapper to measure the timing of method calls.
Surround any method call with a using statement which instantiates
a PerformanceEvent and disposes of it once the method call has completed
to use.

<a name='M-RecordPoint-Connectors-SDK-Diagnostics-PerformanceEvent-#ctor-System-Type,System-String,System-String,RecordPoint-Connectors-SDK-Diagnostics-ILog-'></a>
### #ctor(type,method,message,log) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| type | [System.Type](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Type 'System.Type') | The Type of the calling class (Use GetType() or typeof() depending on context) |
| method | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the method being timed. The name of the method will be appended with _Faulted if an exception occurs |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Any message to be output. Generally, this should just be enough information to provide some context for the event. In the event that an exception occurs, the exception message will be appended to the message. |
| log | [RecordPoint.Connectors.SDK.Diagnostics.ILog](#T-RecordPoint-Connectors-SDK-Diagnostics-ILog 'RecordPoint.Connectors.SDK.Diagnostics.ILog') | An ILog instance which will do the actual recording |

<a name='M-RecordPoint-Connectors-SDK-Diagnostics-PerformanceEvent-Dispose-System-Boolean-'></a>
### Dispose() `method`

##### Summary

Logs to LogTimingVerbose on disposal of the PerformanceEvent.
If an exception has been recorded on the PerformanceEvent, instead
logs to LogTimingWarning after appending _Faulted to the method name
and appending the exception to the message

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Diagnostics-PerformanceEvent-Dispose'></a>
### Dispose() `method`

##### Summary



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Diagnostics-PerformanceEvent-Exception-System-Exception-'></a>
### Exception(ex) `method`

##### Summary

Set the Exception property of the PerformanceEvent.
Used to communicate any Exceptions when the trace is logged.
Typically used in a finally or catch block so that the exception can be 
traced out before being rethrown from the code wrapped by a PerformanceEvent

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| ex | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') |  |

<a name='T-RecordPoint-Connectors-SDK-Client-ProcessingResult'></a>
## ProcessingResult `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary

Describes the results a processing operation for a connector notification.

<a name='F-RecordPoint-Connectors-SDK-Client-ProcessingResult-ConnectorDisabled'></a>
### ConnectorDisabled `constants`

##### Summary

Indicates that the notification was not processed because the 
connector is not enabled.

<a name='F-RecordPoint-Connectors-SDK-Client-ProcessingResult-ConnectorNotReachable'></a>
### ConnectorNotReachable `constants`

##### Summary

Indicates that the notification was not processed because the
connector could not be reached.

<a name='F-RecordPoint-Connectors-SDK-Client-ProcessingResult-ConnectorNotSubscribed'></a>
### ConnectorNotSubscribed `constants`

##### Summary

Indicates that the notification was not processed because the 
connector is not subscribed to the notification type.

<a name='F-RecordPoint-Connectors-SDK-Client-ProcessingResult-NotificationError'></a>
### NotificationError `constants`

##### Summary

Indicates that an error occured during processing of a notification.

<a name='F-RecordPoint-Connectors-SDK-Client-ProcessingResult-OK'></a>
### OK `constants`

##### Summary

Indicates that the required processing for the notification completed successfully.

<a name='F-RecordPoint-Connectors-SDK-Client-ProcessingResult-Unknown'></a>
### Unknown `constants`

##### Summary

Indicates that the result of processing a notification is unknown.

<a name='T-RecordPoint-Connectors-SDK-Client-Models-RelationshipDataModel'></a>
## RelationshipDataModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Client.Models

<a name='M-RecordPoint-Connectors-SDK-Client-Models-RelationshipDataModel-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the RelationshipDataModel class.

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-RelationshipDataModel-#ctor-System-String,System-String-'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the RelationshipDataModel class.

##### Parameters

This constructor has no parameters.

<a name='P-RecordPoint-Connectors-SDK-Client-Models-RelationshipDataModel-RelatedItemNumber'></a>
### RelatedItemNumber `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-RelationshipDataModel-RelationshipType'></a>
### RelationshipType `property`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Client-Models-RelationshipDataModel-Validate'></a>
### Validate() `method`

##### Summary

Validate the object.

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.ValidationException](#T-Microsoft-Rest-ValidationException 'Microsoft.Rest.ValidationException') | Thrown if validation fails |

<a name='T-RecordPoint-Connectors-SDK-Client-RequiredValueNullException'></a>
## RequiredValueNullException `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary

The required value null exception.

<a name='M-RecordPoint-Connectors-SDK-Client-RequiredValueNullException-#ctor-System-String-'></a>
### #ctor(paramName) `constructor`

##### Summary

Initializes a new instance of the [RequiredValueNullException](#T-RecordPoint-Connectors-SDK-Client-RequiredValueNullException 'RecordPoint.Connectors.SDK.Client.RequiredValueNullException') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| paramName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The param name. |

<a name='F-RecordPoint-Connectors-SDK-Client-RequiredValueNullException-NULL_VALUE_MESSAGE'></a>
### NULL_VALUE_MESSAGE `constants`

##### Summary

The NULL VALUE MESSAGE.

<a name='F-RecordPoint-Connectors-SDK-Client-RequiredValueNullException-_paramName'></a>
### _paramName `constants`

##### Summary

The param name.

<a name='P-RecordPoint-Connectors-SDK-Client-RequiredValueNullException-ParamName'></a>
### ParamName `property`

##### Summary

Gets the param name.

<a name='T-RecordPoint-Connectors-SDK-Exceptions-ResourceNotFoundException'></a>
## ResourceNotFoundException `type`

##### Namespace

RecordPoint.Connectors.SDK.Exceptions

##### Summary

Thrown by the SDK when trying to access an API resource that does not exist.
For example, when trying to acknowledge a connector notification that has already
been acknowledged.

<a name='M-RecordPoint-Connectors-SDK-Exceptions-ResourceNotFoundException-#ctor'></a>
### #ctor() `constructor`

##### Summary

Constructs a new instance of ResourceNotFoundException.

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Exceptions-ResourceNotFoundException-#ctor-System-String-'></a>
### #ctor(message) `constructor`

##### Summary

Constructs a new instance of ResourceNotFoundException with an exception message.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-Exceptions-ResourceNotFoundException-#ctor-System-String,System-Exception-'></a>
### #ctor(message,innerException) `constructor`

##### Summary

Constructs a new instance of ResourceNotFoundException with an exception message and an inner exception.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| innerException | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') |  |

<a name='T-RecordPoint-Connectors-SDK-Client-Models-SearchTermModel'></a>
## SearchTermModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Client.Models

<a name='M-RecordPoint-Connectors-SDK-Client-Models-SearchTermModel-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the SearchTermModel class.

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-SearchTermModel-#ctor-System-String,System-String,System-String,System-String,System-String,System-String-'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the SearchTermModel class.

##### Parameters

This constructor has no parameters.

<a name='P-RecordPoint-Connectors-SDK-Client-Models-SearchTermModel-CategoricalValueType'></a>
### CategoricalValueType `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-SearchTermModel-FieldName'></a>
### FieldName `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-SearchTermModel-FieldType'></a>
### FieldType `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-SearchTermModel-FieldValue'></a>
### FieldValue `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-SearchTermModel-OperatorProperty'></a>
### OperatorProperty `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-SearchTermModel-SearchContextIdentifier'></a>
### SearchContextIdentifier `property`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Client-Models-SearchTermModel-Validate'></a>
### Validate() `method`

##### Summary

Validate the object.

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.ValidationException](#T-Microsoft-Rest-ValidationException 'Microsoft.Rest.ValidationException') | Thrown if validation fails |

<a name='T-RecordPoint-Connectors-SDK-Client-Models-SearchTreeNodeModel'></a>
## SearchTreeNodeModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Client.Models

<a name='M-RecordPoint-Connectors-SDK-Client-Models-SearchTreeNodeModel-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the SearchTreeNodeModel class.

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-SearchTreeNodeModel-#ctor-System-String,System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-SearchTreeNodeModel},RecordPoint-Connectors-SDK-Client-Models-SearchTermModel-'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the SearchTreeNodeModel class.

##### Parameters

This constructor has no parameters.

<a name='P-RecordPoint-Connectors-SDK-Client-Models-SearchTreeNodeModel-BoolOperator'></a>
### BoolOperator `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-SearchTreeNodeModel-Children'></a>
### Children `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-SearchTreeNodeModel-SearchTerm'></a>
### SearchTerm `property`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Client-Models-SearchTreeNodeModel-Validate'></a>
### Validate() `method`

##### Summary

Validate the object.

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.ValidationException](#T-Microsoft-Rest-ValidationException 'Microsoft.Rest.ValidationException') | Thrown if validation fails |

<a name='T-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult-Status'></a>
## Status `type`

##### Namespace

RecordPoint.Connectors.SDK.SubmitPipeline.SubmitResult

##### Summary

Submit result status indicator

<a name='F-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult-Status-ConnectorDisabled'></a>
### ConnectorDisabled `constants`

##### Summary

Indicates that the Records365 vNext platform rejected the submit
because the connector was disabled.
Submissions that fail with this status should not be dead-lettered.
The connector implementation should stop running if this status is returned.

<a name='F-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult-Status-ConnectorNotFound'></a>
### ConnectorNotFound `constants`

##### Summary

Indicates that the Records365 vNext platform rejected the submit
because the connector was not found.
Submissions that fail with this status should not be dead-lettered.
The connector implementation should stop running if this status is returned.

<a name='F-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult-Status-Deferred'></a>
### Deferred `constants`

##### Summary

Indicates that the Records365 vNext platform rejected the submit
because it was not yet valid - for example, a dependent item may not yet
be processed and available in the platform. The connector implementation
should retry the submit at a later time. The connector should abandon the 
submit if Deferred is returned for the same submit for many repeated
attempts over a long period of time.

<a name='F-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult-Status-OK'></a>
### OK `constants`

##### Summary

Submit succeeded.

<a name='F-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult-Status-Skipped'></a>
### Skipped `constants`

##### Summary

Indicates that the submission was skipped by the submission pipleline.

<a name='F-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult-Status-TooManyRequests'></a>
### TooManyRequests `constants`

##### Summary

Indicates that the Records365 vNext platform rejected the submit
because a part of the submission chain is experiencing heavy load.
The connector implementation should use a circuit breaker to wait 
on all submissions and retry the submit at a later time.

<a name='T-RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel'></a>
## SubmissionMetaDataModel `type`

##### Namespace

RecordPoint.Connectors.SDK.Client.Models

<a name='M-RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel-#ctor'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the SubmissionMetaDataModel class.

##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel-#ctor-System-String,System-String,System-String,System-String,System-Nullable{System-Boolean}-'></a>
### #ctor() `constructor`

##### Summary

Initializes a new instance of the SubmissionMetaDataModel class.

##### Parameters

This constructor has no parameters.

<a name='P-RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel-DisplayName'></a>
### DisplayName `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel-IsSysAdminOnly'></a>
### IsSysAdminOnly `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel-Name'></a>
### Name `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel-Type'></a>
### Type `property`

##### Summary



<a name='P-RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel-Value'></a>
### Value `property`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel-Validate'></a>
### Validate() `method`

##### Summary

Validate the object.

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [Microsoft.Rest.ValidationException](#T-Microsoft-Rest-ValidationException 'Microsoft.Rest.ValidationException') | Thrown if validation fails |

<a name='T-RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModelExtensions'></a>
## SubmissionMetaDataModelExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Client.Models

##### Summary

The submission meta data model extensions.

<a name='M-RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModelExtensions-AddOrUpdate-System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel},RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel-'></a>
### AddOrUpdate(metaDataList,metaData) `method`

##### Summary

Add or update.

##### Returns

IList<SubmissionMetaDataModel>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| metaDataList | [System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.SubmissionMetaDataModel}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IList 'System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.SubmissionMetaDataModel}') | The meta data list. |
| metaData | [RecordPoint.Connectors.SDK.Client.Models.SubmissionMetaDataModel](#T-RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel 'RecordPoint.Connectors.SDK.Client.Models.SubmissionMetaDataModel') | The meta data. |

<a name='M-RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModelExtensions-GetValueOrDefault-System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-SubmissionMetaDataModel},System-String-'></a>
### GetValueOrDefault(metaDataList,name) `method`

##### Summary

Get value or default.

##### Returns

A string

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| metaDataList | [System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.SubmissionMetaDataModel}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IList 'System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.SubmissionMetaDataModel}') | The meta data list. |
| name | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name. |

<a name='T-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext'></a>
## SubmitContext `type`

##### Namespace

RecordPoint.Connectors.SDK.SubmitPipeline

##### Summary

A SubmitContext captures all the state that relates to an individual submission from
the content source to the Records365 vNext Connector API. This class can be subclassed
to define more state that is specific to a particular connector or submission type.

<a name='F-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-NoExternalIdFound'></a>
### NoExternalIdFound `constants`

##### Summary

String to be returned if the SubmitContext's LogPrefix method is called and no
External ID is present on the SubmitContext

<a name='F-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-NoTitleFound'></a>
### NoTitleFound `constants`

##### Summary

String to be returned if the SubmitContext's LogPrefix method is called and no
title is present on the SubmitContext

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-AggregationFoundDuringItemSubmission'></a>
### AggregationFoundDuringItemSubmission `property`

##### Summary

Value that indicates if Records365 vNext has an items aggregation.
If this SubmitContext has passed through the HttpSubmitItemPipelineElement, this value
will be populated with a flag that indicates if the platform knows about the aggregation
for the item. If this is false, the connector may take some action to submit the aggregation.
If this is true, the platform already has the aggregation in the system and the connector
doesn't need to submit the aggregation again.

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-ApiClientFactorySettings'></a>
### ApiClientFactorySettings `property`

##### Summary

The settings used to create an ApiClient

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-AuthenticationHelperSettings'></a>
### AuthenticationHelperSettings `property`

##### Summary

The settings used for submission to the Records365 vNext Connector API.

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-CancellationToken'></a>
### CancellationToken `property`

##### Summary

A cancellation token that can be used to cancel a submission while it is in the middle of the pipeline.

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-ConnectorConfigId'></a>
### ConnectorConfigId `property`

##### Summary

The ID of the Records365 vNext connector that this submission is being made through.

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-CoreMetaData'></a>
### CoreMetaData `property`

##### Summary

The core metadata fields of the submission.

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-CorrelationId'></a>
### CorrelationId `property`

##### Summary

An ID used for correlating log messages for debugging purposes.

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-Filters'></a>
### Filters `property`

##### Summary

Defines any filtering to be done. If Filters are defined and the Connector makes use of the FilterPipelineElement,
then the Core and Source metadata of this SubmitContext will be checked against the Filters, as followds:
If the Excluded filter is defined and is matched, the submission will be skipped.
If the Included filter is not defined, any submission which does not match the Excluded filter will continue in the pipeline.
If the Included filter is defined, only submissions which match the Included filter will continue in the pipeline - All others will be skipped.

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-ItemTypeId'></a>
### ItemTypeId `property`

##### Summary

When submitting an aggregation, this value indicates whether the aggregation
is a record folder or a box.

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-Relationships'></a>
### Relationships `property`

##### Summary

Relationship info of the submission

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-SourceMetaData'></a>
### SourceMetaData `property`

##### Summary

The source metadata fields of the submission.

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-SubmitResult'></a>
### SubmitResult `property`

##### Summary

The result of the submission. This will be set by one of the HttpSubmit* pipeline elements.

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-TenantId'></a>
### TenantId `property`

##### Summary

The ID of the Records365 vNext tenant that this submission is being made to.

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-GetExternalId'></a>
### GetExternalId() `method`

##### Summary

Returns the External ID of the object the SubmitContext is related to. Typically this is sourced from the Core metadata on the 
SubmitContext, but in some cases (e.g. on the BinarySubmitContext) it may be stored in a strongly typed field

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-GetTitle'></a>
### GetTitle() `method`

##### Summary

Returns the title of the object the SubmitContext is related to. Typically this is sourced from the Core metadata on the 
SubmitContext, but in some cases (e.g. on the BinarySubmitContext) it may be stored in a strongly typed field

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitContext-LogPrefix'></a>
### LogPrefix() `method`

##### Summary

A prefix that will be applied to all logs created using the SubmitPipelineElementBase Log* methods (e.g. LogMessage)
Any messages output by Log* methods will be prefixed with this string

##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult'></a>
## SubmitResult `type`

##### Namespace

RecordPoint.Connectors.SDK.SubmitPipeline

##### Summary

Result of an attempt to submit an item via the submit pipeline

<a name='M-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult-#ctor'></a>
### #ctor() `constructor`

##### Summary

Default constructor, which sets an okay result

##### Parameters

This constructor has no parameters.

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult-Exception'></a>
### Exception `property`

##### Summary

The underlying exception if a status was selected in response to an exception.

##### Remarks

This is intended for diagnostics purposes

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult-Reason'></a>
### Reason `property`

##### Summary

Human readable reason why a particular status was selected.

##### Remarks

This is intended for diagnostics purposes

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult-SubmitStatus'></a>
### SubmitStatus `property`

##### Summary

Indicates the outcome of the submission

<a name='P-RecordPoint-Connectors-SDK-SubmitPipeline-SubmitResult-WaitUntilTime'></a>
### WaitUntilTime `property`

##### Summary

For deferrals and too many requests, indicates the time that should be waited until
next submission attempt.

<a name='T-RecordPoint-Connectors-SDK-Exceptions-TooManyRequestsException'></a>
## TooManyRequestsException `type`

##### Namespace

RecordPoint.Connectors.SDK.Exceptions

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Exceptions-TooManyRequestsException-#ctor-System-String,System-DateTime-'></a>
### #ctor(message,time) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| time | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') |  |

<a name='M-RecordPoint-Connectors-SDK-Exceptions-TooManyRequestsException-#ctor-System-Runtime-Serialization-SerializationInfo,System-Runtime-Serialization-StreamingContext-'></a>
### #ctor(info,context) `constructor`

##### Summary

Constructs a new instance of TooManyRequestsException from a serialization context.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| info | [System.Runtime.Serialization.SerializationInfo](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Runtime.Serialization.SerializationInfo 'System.Runtime.Serialization.SerializationInfo') |  |
| context | [System.Runtime.Serialization.StreamingContext](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Runtime.Serialization.StreamingContext 'System.Runtime.Serialization.StreamingContext') |  |

<a name='P-RecordPoint-Connectors-SDK-Exceptions-TooManyRequestsException-WaitUntilTime'></a>
### WaitUntilTime `property`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Exceptions-TooManyRequestsException-GetObjectData-System-Runtime-Serialization-SerializationInfo,System-Runtime-Serialization-StreamingContext-'></a>
### GetObjectData(info,context) `method`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| info | [System.Runtime.Serialization.SerializationInfo](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Runtime.Serialization.SerializationInfo 'System.Runtime.Serialization.SerializationInfo') |  |
| context | [System.Runtime.Serialization.StreamingContext](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Runtime.Serialization.StreamingContext 'System.Runtime.Serialization.StreamingContext') |  |

<a name='T-RecordPoint-Connectors-SDK-Helpers-ValidationHelper'></a>
## ValidationHelper `type`

##### Namespace

RecordPoint.Connectors.SDK.Helpers

##### Summary

Provides helpers for validating arguments to methods.

<a name='M-RecordPoint-Connectors-SDK-Helpers-ValidationHelper-ArgumentNotNull-System-Object,System-String-'></a>
### ArgumentNotNull(argumentValue,argumentName) `method`

##### Summary

Throws [ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') if the given argument is null.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| argumentValue | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') | The argument value to test. |
| argumentName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the argument to test. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | The value is null. |

<a name='M-RecordPoint-Connectors-SDK-Helpers-ValidationHelper-ArgumentNotNullOrEmpty-System-String,System-String-'></a>
### ArgumentNotNullOrEmpty(argumentValue,argumentName) `method`

##### Summary

Throws an exception if the tested string argument is null or an empty string.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| argumentValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The argument value to test. |
| argumentName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the argument to test. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | The string value is null or empty. |

<a name='M-RecordPoint-Connectors-SDK-Helpers-ValidationHelper-ArgumentNotNullOrEmpty-System-Security-SecureString,System-String-'></a>
### ArgumentNotNullOrEmpty(argumentValue,argumentName) `method`

##### Summary

Throws an exception if the tested SecureString argument is null or an empty string.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| argumentValue | [System.Security.SecureString](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Security.SecureString 'System.Security.SecureString') | The argument value to test. |
| argumentName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the argument to test. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | The string value is null. |

<a name='M-RecordPoint-Connectors-SDK-Helpers-ValidationHelper-ArgumentNotNullOrWhiteSpace-System-String,System-String-'></a>
### ArgumentNotNullOrWhiteSpace(argumentValue,argumentName) `method`

##### Summary

Throws an exception if the tested string argument is null or a string that contains only whitespace.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| argumentValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The argument value to test. |
| argumentName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The name of the argument to test. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentNullException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentNullException 'System.ArgumentNullException') | The string value is null or contains only whitespace. |
