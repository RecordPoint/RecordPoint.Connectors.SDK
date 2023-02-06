using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Configuration;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Secrets;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.R365
{

    /// <summary>
    /// R365 Standard Client
    /// </summary>
    public class R365Client : IR365Client
    {

        private readonly IR365ConfigurationClient _r365ConfigurationClient;
        private readonly IScopeManager _scopeManager;
        private readonly IR365Pipelines _r365Pipelines;

        public R365Client(
            IR365ConfigurationClient r365ConfigurationClient,
            IScopeManager scopeManager,
            IR365Pipelines r365Pipelines)
        {
            _r365ConfigurationClient = r365ConfigurationClient;
            _scopeManager = scopeManager;
            _r365Pipelines = r365Pipelines;
        }

        private static Dimensions GetDimensions()
        {
            var dimensions = new Dimensions()
            {
                [StandardDimensions.DEPENDANCY_TYPE] = DependancyType.Records365.ToString(),
            };
            return dimensions;
        }

        /// <summary>
        /// Ensure configuration is loaded
        /// </summary>
        private R365ConfigurationModel LoadConfiguration()
        {
            return _r365ConfigurationClient.GetR365Configuration();
        }

        public bool IsConfigured()
        {
            var r365Configuration = LoadConfiguration();
            return r365Configuration != null;
        }

        private static ApiClientFactorySettings GetApiClientFactorySettings(R365ConfigurationModel r365Configuration)
        {
            var settings = new ApiClientFactorySettings()
            {
                ConnectorApiUrl = r365Configuration.ConnectorApiUrl
            };
            return settings;
        }

        private static AuthenticationHelperSettings GetAuthenticationHelperSettings(R365ConfigurationModel r365Configuration, string tenantDomainName)
        {
            var clientSecret = EncryptionExtensions.GetSecureSecret(r365Configuration.ClientSecret);
            var settings = new AuthenticationHelperSettings
            {
                ClientId = r365Configuration.ClientId.ToString(),
                ClientSecret = clientSecret,
                TenantDomainName = tenantDomainName,
                AuthenticationResource = r365Configuration.Audience
            };
            return settings;
        }

        /// <summary>
        /// Submit an audit event to records 365
        /// </summary>
        /// <param name="auditEvent">Record to submit</param>
        /// <param name="cancellationToken">Cancellation task</param>
        /// <returns>Submit result</returns>
        public async Task<SubmitResult> SubmitAuditEvent(ConnectorConfigModel connectorConfig, AuditEvent auditEvent, CancellationToken cancellationToken)
        {
            return await _scopeManager.InvokeAsync(
                GetDimensions(), async () =>
                {
                    var r365Configuration = LoadConfiguration();
                    var submitContext = CreateAuditEventSubmitContext(connectorConfig, auditEvent, r365Configuration, cancellationToken);
                    await _r365Pipelines.AuditEventPipeline.Submit(submitContext).ConfigureAwait(false);
                    return submitContext.SubmitResult;
                }).ConfigureAwait(false);
        }

        /// <summary>
        /// Create an audit event submission context
        /// </summary>
        /// <param name="auditEvent">Audit Event to convert</param>
        /// <returns></returns>
        private static SubmitContext CreateAuditEventSubmitContext(ConnectorConfigModel connectorConfig, AuditEvent auditEvent, R365ConfigurationModel r365Configuration, CancellationToken cancellationToken)
        {
            var coreMetaData = new List<SubmissionMetaDataModel>
            {
                new SubmissionMetaDataModel(Fields.AuditEvent.Created, type: nameof(DateTimeOffset), value: auditEvent.CreatedDate.ToString("o")),
                new SubmissionMetaDataModel(Fields.AuditEvent.Description, type: nameof(String), value: auditEvent.Description),
                new SubmissionMetaDataModel(Fields.AuditEvent.EventExternalId, type: nameof(String), value: auditEvent.EventExternalId),
                new SubmissionMetaDataModel(Fields.AuditEvent.EventType, type: nameof(String), value: auditEvent.EventType),
                new SubmissionMetaDataModel(Fields.AuditEvent.ExternalId, type: nameof(String), value: auditEvent.ExternalId),
                new SubmissionMetaDataModel(Fields.AuditEvent.UserId, type: nameof(String), value: auditEvent.UserId),
                new SubmissionMetaDataModel(Fields.AuditEvent.UserName, type: nameof(String), value: auditEvent.UserName),
            };

            var sourceMetaData = new List<SubmissionMetaDataModel>();
            if (auditEvent.MetaDataItems?.Any() ?? false)
            {
                sourceMetaData = auditEvent.MetaDataItems
                    .Where(a => a.MetaDataItemType == MetaDataItemType.R365MetaData)
                    .Select(x => new SubmissionMetaDataModel(name: x.Name, type: x.Type, value: x.Value))
                    .ToList();
            }

            var submitContext = new SubmitContext
            {
                TenantId = connectorConfig.GetTenantIdAsGuid(),
                ConnectorConfigId = connectorConfig.GetIdAsGuid(),
                ApiClientFactorySettings = GetApiClientFactorySettings(r365Configuration),
                AuthenticationHelperSettings = GetAuthenticationHelperSettings(r365Configuration, connectorConfig.TenantDomainName),
                CoreMetaData = coreMetaData,
                SourceMetaData = sourceMetaData,
                CancellationToken = cancellationToken
            };

            return submitContext;
        }


        /// <summary>
        /// Submit a record to records 365
        /// </summary>
        /// <param name="record">Record to submit</param>
        /// <param name="cancellationToken">Cancellation task</param>
        /// <returns>Submit result</returns>
        public async Task<SubmitResult> SubmitRecord(ConnectorConfigModel connectorConfig, Record record, CancellationToken cancellationToken)
        {
            return await _scopeManager.InvokeAsync(
                GetDimensions(), async () =>
                {
                    var r365Configuration = LoadConfiguration();
                    var submitContext = CreateRecordSubmitContext(connectorConfig, record, r365Configuration, cancellationToken);
                    await _r365Pipelines.RecordPipeline.Submit(submitContext).ConfigureAwait(false);
                    return submitContext.SubmitResult;
                }).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a record submission context
        /// </summary>
        /// <param name="record">Record to convert</param>
        /// <returns></returns>
        private static ItemSubmitContext CreateRecordSubmitContext(ConnectorConfigModel connectorConfig, Record record, R365ConfigurationModel r365Configuration, CancellationToken cancellationToken)
        {
            var coreMetaData = new List<SubmissionMetaDataModel>
            {
                new SubmissionMetaDataModel(Fields.ExternalId, type: nameof(String), value: record.ExternalId),
                new SubmissionMetaDataModel(Fields.Title, type: nameof(String), value: record.Title),
                new SubmissionMetaDataModel(Fields.SourceLastModifiedBy, type: nameof(String), value: record.SourceLastModifiedBy),
                new SubmissionMetaDataModel(Fields.SourceLastModifiedDate, type: nameof(DateTimeOffset), value: record.SourceLastModifiedDate.ToString("o")),
                new SubmissionMetaDataModel(Fields.SourceCreatedBy, type: nameof(String), value: record.SourceCreatedBy),
                new SubmissionMetaDataModel(Fields.SourceCreatedDate, type: nameof(DateTimeOffset), value: record.SourceCreatedDate.ToString("o")),
                new SubmissionMetaDataModel(Fields.Author, type: nameof(String), value: record.Author),
                new SubmissionMetaDataModel(Fields.ParentExternalId, type: nameof(String), value: record.ParentExternalId),
                new SubmissionMetaDataModel(Fields.ContentVersion, type: nameof(String), value: record.ContentVersion),
                new SubmissionMetaDataModel(Fields.Location, type: nameof(String), value: record.Location),
                new SubmissionMetaDataModel(Fields.MediaType, type: nameof(String), value: "Electronic"),
                new SubmissionMetaDataModel(Fields.MimeType, type: nameof(String), value: record.MimeType)
            };

            var sourceMetaData = new List<SubmissionMetaDataModel>();

            if (record.MetaDataItems?.Any() ?? false)
            {
                sourceMetaData = record.MetaDataItems
                    .Where(a => a.MetaDataItemType == MetaDataItemType.R365MetaData)
                    .Select(x => new SubmissionMetaDataModel(name: x.Name, type: x.Type, value: x.Value))
                    .ToList();
            }

            var submitContext = new ItemSubmitContext
            {
                TenantId = connectorConfig.GetTenantIdAsGuid(),
                ConnectorConfigId = connectorConfig.GetIdAsGuid(),
                ApiClientFactorySettings = GetApiClientFactorySettings(r365Configuration),
                AuthenticationHelperSettings = GetAuthenticationHelperSettings(r365Configuration, connectorConfig.TenantDomainName),
                CoreMetaData = coreMetaData,
                SourceMetaData = sourceMetaData,
                CancellationToken = cancellationToken
            };

            if (record.Binaries?.Any() ?? false)
            {
                var binariesSubmitted = record.Binaries
                    .Select(a => new DirectBinarySubmissionInputModel
                    {
                        BinaryExternalId = a.ItemExternalId,
                        ConnectorId = connectorConfig.Id,
                        CorrelationId = submitContext.CorrelationId.ToString(),
                        FileHash = a.FileHash,
                        FileName = a.FileName,
                        FileSize = a.FileSize,
                        ItemExternalId = a.ItemExternalId,
                        MimeType = a.MimeType,
                        SourceLastModifiedDate = a.SourceLastModifiedDate.DateTime
                    })
                    .ToList();

                submitContext.BinariesSubmitted = binariesSubmitted;
            }

            return submitContext;
        }

        /// <summary>
        /// Submit a binary to records 365
        /// </summary>
        /// <param name="binaryMetaInfo">Meta info for the binary to submit</param>
        /// <param name="binaryStream">Stream of the binaries content</param>
        /// <param name="cancellationToken">Cancellation task</param>
        /// <returns>Submit result</returns>
        public async Task<SubmitResult> SubmitBinary(ConnectorConfigModel connectorConfig, BinaryMetaInfo binaryMetaInfo, Stream binaryStream, CancellationToken cancellationToken)
        {
            return await _scopeManager.InvokeAsync(
                GetDimensions(), async () =>
                {
                    var r365Configuration = LoadConfiguration();
                    var submitContext = CreateBinarySubmitContext(connectorConfig, binaryMetaInfo, binaryStream, r365Configuration, cancellationToken);
                    await _r365Pipelines.BinaryPipeline.Submit(submitContext).ConfigureAwait(false);
                    return submitContext.SubmitResult;
                }).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a binary submission context
        /// </summary>
        /// <param name="record">Record to convert</param>
        /// <returns></returns>
        private static SubmitContext CreateBinarySubmitContext(ConnectorConfigModel connectorConfig, BinaryMetaInfo binaryMetaInfo, Stream binaryStream, R365ConfigurationModel r365Configuration, CancellationToken cancellationToken)
        {
            var coreMetaData = new List<SubmissionMetaDataModel>();

            var sourceMetaData = new List<SubmissionMetaDataModel>();
            if (binaryMetaInfo.MetaDataItems?.Any() ?? false)
            {
                sourceMetaData = binaryMetaInfo.MetaDataItems
                    .Where(a => a.MetaDataItemType == MetaDataItemType.R365MetaData)
                    .Select(x => new SubmissionMetaDataModel(name: x.Name, type: x.Type, value: x.Value))
                    .ToList();
            }

            var submitContext = new BinarySubmitContext()
            {
                TenantId = connectorConfig.GetTenantIdAsGuid(),
                ConnectorConfigId = connectorConfig.GetIdAsGuid(),
                ApiClientFactorySettings = GetApiClientFactorySettings(r365Configuration),
                AuthenticationHelperSettings = GetAuthenticationHelperSettings(r365Configuration, connectorConfig.TenantDomainName),
                SourceMetaData = sourceMetaData,
                CoreMetaData = coreMetaData,
                ItemExternalId = binaryMetaInfo.ItemExternalId,
                ExternalId = binaryMetaInfo.ExternalId,
                FileHash = binaryMetaInfo.FileHash,
                MimeType = binaryMetaInfo.MimeType,
                FileName = binaryMetaInfo.FileName,
                Stream = binaryStream,
                SourceLastModifiedDate = binaryMetaInfo.SourceLastModifiedDate.DateTime,
                CancellationToken = cancellationToken
            };

            return submitContext;
        }

        /// <summary>
        /// Submit an aggregation to records 365
        /// </summary>
        /// <param name="record">Record to submit</param>
        /// <param name="cancellationToken">Cancellation task</param>
        /// <returns>Submit result</returns>
        public async Task<SubmitResult> SubmitAggregation(ConnectorConfigModel connectorConfig, Aggregation aggregation, CancellationToken cancellationToken)
        {
            return await _scopeManager.InvokeAsync(
                GetDimensions(), async () =>
                {
                    var r365Configuration = LoadConfiguration();
                    var submitContext = CreateAggregationSubmitContext(connectorConfig, aggregation, r365Configuration, cancellationToken);
                    await _r365Pipelines.AggregationPipeline.Submit(submitContext).ConfigureAwait(false);
                    return submitContext.SubmitResult;
                }).ConfigureAwait(false);
        }

        /// <summary>
        /// Create an aggregation submission context
        /// </summary>
        /// <param name="record">Record to convert</param>
        /// <returns></returns>
        private SubmitContext CreateAggregationSubmitContext(ConnectorConfigModel connectorConfig, Aggregation aggregation, R365ConfigurationModel r365Configuration, CancellationToken cancellationToken)
        {
            var coreMetaData = new List<SubmissionMetaDataModel>
            {
                new SubmissionMetaDataModel(Fields.ExternalId, type: nameof(String), value: aggregation.ExternalId),

                new SubmissionMetaDataModel(Fields.Title, type: nameof(String), value: aggregation.Title),
                new SubmissionMetaDataModel(Fields.Author, type: nameof(String), value: aggregation.Author),
                new SubmissionMetaDataModel(Fields.SourceCreatedBy, type: nameof(String), value: aggregation.SourceCreatedBy),
                new SubmissionMetaDataModel(Fields.SourceCreatedDate, type: nameof(DateTimeOffset), value: aggregation.SourceCreatedDate.ToString("o")),
                new SubmissionMetaDataModel(Fields.SourceLastModifiedBy, type: nameof(String), value: aggregation.SourceLastModifiedBy),
                new SubmissionMetaDataModel(Fields.SourceLastModifiedDate, type: nameof(DateTimeOffset), value: aggregation.SourceLastModifiedDate.ToString("o")),
                new SubmissionMetaDataModel(Fields.Location, type: nameof(String), value: aggregation.Location)
            };

            var sourceMetaData = new List<SubmissionMetaDataModel>();
            if (aggregation.MetaDataItems?.Any() ?? false)
            {
                sourceMetaData = aggregation.MetaDataItems
                    .Where(a => a.MetaDataItemType == MetaDataItemType.R365MetaData)
                    .Select(x => new SubmissionMetaDataModel(name: x.Name, type: x.Type, value: x.Value))
                    .ToList();
            }

            var submitContext = new SubmitContext
            {
                ItemTypeId = Aggregation.ItemTypeId,
                TenantId = connectorConfig.GetTenantIdAsGuid(),
                ConnectorConfigId = connectorConfig.GetIdAsGuid(),
                ApiClientFactorySettings = GetApiClientFactorySettings(r365Configuration),
                AuthenticationHelperSettings = GetAuthenticationHelperSettings(r365Configuration, connectorConfig.TenantDomainName),
                CoreMetaData = coreMetaData,
                SourceMetaData = sourceMetaData,
                CancellationToken = cancellationToken
            };

            return submitContext;
        }

    }
}
