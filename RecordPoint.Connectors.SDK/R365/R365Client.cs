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
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.R365
{
    /// <summary>
    /// The r365 client.
    /// </summary>
    public class R365Client : IR365Client
    {

        /// <summary>
        /// The r365 configuration client.
        /// </summary>
        private readonly IR365ConfigurationClient _r365ConfigurationClient;
        /// <summary>
        /// The scope manager.
        /// </summary>
        private readonly IScopeManager _scopeManager;
        /// <summary>
        /// The r365 pipelines.
        /// </summary>
        private readonly IR365Pipelines _r365Pipelines;

        /// <summary>
        /// Initializes a new instance of the <see cref="R365Client"/> class.
        /// </summary>
        /// <param name="r365ConfigurationClient">The r365 configuration client.</param>
        /// <param name="scopeManager">The scope manager.</param>
        /// <param name="r365Pipelines">The r365 pipelines.</param>
        public R365Client(
            IR365ConfigurationClient r365ConfigurationClient,
            IScopeManager scopeManager,
            IR365Pipelines r365Pipelines)
        {
            _r365ConfigurationClient = r365ConfigurationClient;
            _scopeManager = scopeManager;
            _r365Pipelines = r365Pipelines;
        }

        /// <summary>
        /// Get the dimensions.
        /// </summary>
        /// <returns>A Dimensions</returns>
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

        /// <summary>
        /// Checks if is configured.
        /// </summary>
        /// <returns>A bool</returns>
        public bool IsConfigured()
        {
            var r365Configuration = LoadConfiguration();
            return r365Configuration != null;
        }

        /// <summary>
        /// Get api client factory settings.
        /// </summary>
        /// <param name="r365Configuration">The r365 configuration.</param>
        /// <returns>An ApiClientFactorySettings</returns>
        private static ApiClientFactorySettings GetApiClientFactorySettings(R365ConfigurationModel r365Configuration)
        {
            var settings = new ApiClientFactorySettings()
            {
                ConnectorApiUrl = r365Configuration.ConnectorApiUrl,
                ServerCertificateValidation = r365Configuration.ServerCertificateValidation
            };
            return settings;
        }

        /// <summary>
        /// Get authentication helper settings.
        /// </summary>
        /// <param name="r365Configuration">The r365 configuration.</param>
        /// <param name="tenantDomainName">The tenant domain name.</param>
        /// <returns>An AuthenticationHelperSettings</returns>
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
        /// <param name="connectorConfig">Connector configuration</param>
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

        private static SubmitContext CreateAuditEventSubmitContext(ConnectorConfigModel connectorConfig, AuditEvent auditEvent, R365ConfigurationModel r365Configuration, CancellationToken cancellationToken)
        {
            var coreMetaData = new List<SubmissionMetaDataModel>
            {
                new(Fields.AuditEvent.Created, type: nameof(DateTimeOffset), value: auditEvent.CreatedDate.ToString("o")),
                new(Fields.AuditEvent.Description, type: nameof(String), value: auditEvent.Description),
                new(Fields.AuditEvent.EventExternalId, type: nameof(String), value: auditEvent.EventExternalId),
                new(Fields.AuditEvent.EventType, type: nameof(String), value: auditEvent.EventType),
                new(Fields.AuditEvent.ExternalId, type: nameof(String), value: auditEvent.ExternalId),
                new(Fields.AuditEvent.UserId, type: nameof(String), value: auditEvent.UserId),
                new(Fields.AuditEvent.UserName, type: nameof(String), value: auditEvent.UserName),
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
        /// <param name="connectorConfig">Connector configuration</param>
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

        private static ItemSubmitContext CreateRecordSubmitContext(ConnectorConfigModel connectorConfig, Record record, R365ConfigurationModel r365Configuration, CancellationToken cancellationToken)
        {
            var coreMetaData = new List<SubmissionMetaDataModel>
            {
                new(Fields.ExternalId, type: nameof(String), value: record.ExternalId),
                new(Fields.Title, type: nameof(String), value: record.Title),
                new(Fields.SourceLastModifiedBy, type: nameof(String), value: record.SourceLastModifiedBy),
                new(Fields.SourceLastModifiedDate, type: nameof(DateTimeOffset), value: record.SourceLastModifiedDate.ToString("o")),
                new(Fields.SourceCreatedBy, type: nameof(String), value: record.SourceCreatedBy),
                new(Fields.SourceCreatedDate, type: nameof(DateTimeOffset), value: record.SourceCreatedDate.ToString("o")),
                new(Fields.Author, type: nameof(String), value: record.Author),
                new(Fields.ParentExternalId, type: nameof(String), value: record.ParentExternalId),
                new(Fields.ContentVersion, type: nameof(String), value: record.ContentVersion),
                new(Fields.Location, type: nameof(String), value: record.Location),
                new(Fields.MediaType, type: nameof(String), value: "Electronic"),
                new(Fields.MimeType, type: nameof(String), value: record.MimeType)
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
        /// <param name="connectorConfig">Connector configuration</param>
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
                CancellationToken = cancellationToken,
                SkipEnrichment = binaryMetaInfo.SkipEnrichment ?? default
            };

            return submitContext;
        }

        /// <summary>
        /// Submit an aggregation to records 365
        /// </summary>
        /// <param name="connectorConfig"></param>
        /// <param name="aggregation"></param>
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

        private SubmitContext CreateAggregationSubmitContext(ConnectorConfigModel connectorConfig, Aggregation aggregation, R365ConfigurationModel r365Configuration, CancellationToken cancellationToken)
        {
            var coreMetaData = new List<SubmissionMetaDataModel>
            {
                new(Fields.ExternalId, type: nameof(String), value: aggregation.ExternalId),
                new(Fields.ParentExternalId, type: nameof(String), value: aggregation.ParentExternalId),
                new(Fields.Title, type: nameof(String), value: aggregation.Title),
                new(Fields.Author, type: nameof(String), value: aggregation.Author),
                new(Fields.SourceCreatedBy, type: nameof(String), value: aggregation.SourceCreatedBy),
                new(Fields.SourceCreatedDate, type: nameof(DateTimeOffset), value: aggregation.SourceCreatedDate.ToString("o")),
                new(Fields.SourceLastModifiedBy, type: nameof(String), value: aggregation.SourceLastModifiedBy),
                new(Fields.SourceLastModifiedDate, type: nameof(DateTimeOffset), value: aggregation.SourceLastModifiedDate.ToString("o")),
                new(Fields.Location, type: nameof(String), value: aggregation.Location)
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
