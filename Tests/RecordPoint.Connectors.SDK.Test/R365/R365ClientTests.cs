#nullable enable
using Moq;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Configuration;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.Notifications;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.R365;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using Xunit;
using Record = RecordPoint.Connectors.SDK.Content.Record;

namespace RecordPoint.Connectors.SDK.Test.R365
{
    /// <summary>
    /// Unit tests for the R365Client
    /// </summary>
    public class R365ClientTests
    {
        private readonly Mock<IR365ConfigurationClient> _configurationClient = new();
        private readonly Mock<INotificationApiManager> _notificationApiManager = new();
        private readonly Mock<ISubmission> _recordPipeline = new();
        private readonly Mock<ISubmission> _binaryPipeline = new();
        private readonly Mock<ISubmission> _aggregationPipeline = new();
        private readonly Mock<ISubmission> _auditEventPipeline = new();
        private readonly IObservabilityScope _observabilityScope = new ObservabilityScope();
        private readonly R365Client _sut;

        public R365ClientTests()
        {
            var pipelines = new R365Pipelines(
                _recordPipeline.Object,
                _binaryPipeline.Object,
                _aggregationPipeline.Object,
                _auditEventPipeline.Object);

            _configurationClient
                .Setup(a => a.GetR365Configuration(It.IsAny<string>()))
                .Returns(new R365ConfigurationModel
                {
                    ConnectorApiUrl = "https://api.example.com",
                    ServerCertificateValidation = true,
                    ClientId = Guid.NewGuid().ToString(),
                    ClientSecret = "secret",
                    Audience = "audience"
                });

            _sut = new R365Client(
                _configurationClient.Object,
                _observabilityScope,
                pipelines,
                _notificationApiManager.Object);
        }

        private static ConnectorConfigModel CreateConnector() => new()
        {
            Id = Guid.NewGuid().ToString(),
            ConnectorTypeId = Guid.NewGuid().ToString(),
            TenantId = Guid.NewGuid().ToString(),
            TenantDomainName = "tenant.example.com",
            ConnectorTypeConfigurationId = "config-key",
            DisplayName = "Test",
            Status = "Enabled"
        };

        [Fact]
        public void IsConfigured_ReflectsConfigurationClient()
        {
            _configurationClient.Setup(a => a.R365ConfigurationExists()).Returns(true);
            Assert.True(_sut.IsConfigured());

            _configurationClient.Setup(a => a.R365ConfigurationExists()).Returns(false);
            Assert.False(_sut.IsConfigured());
        }

        [Fact]
        public async Task SubmitRecord_InvokesRecordPipeline_AndReturnsResult()
        {
            SubmitContext? captured = null;
            _recordPipeline
                .Setup(a => a.Submit(It.IsAny<SubmitContext>()))
                .Callback<SubmitContext>(c => captured = c)
                .Returns(Task.CompletedTask);

            var connector = CreateConnector();
            var record = new Record
            {
                ExternalId = "record-1",
                Title = "Record 1",
                MetaDataItems =
                [
                    new MetaDataItem { Name = "R365", Type = "String", Value = "v", MetaDataItemType = MetaDataItemType.R365MetaData },
                    new MetaDataItem { Name = "Internal", Type = "String", Value = "v", MetaDataItemType = MetaDataItemType.Internal }
                ],
                Binaries =
                [
                    new BinaryMetaInfo { ExternalId = "bin-1", ItemExternalId = "record-1", FileName = "f.txt", BinarySubmissionStatus = BinarySubmissionStatus.Submitted }
                ]
            };

            var result = await _sut.SubmitRecord(connector, record, CancellationToken.None);

            Assert.NotNull(result);
            _recordPipeline.Verify(a => a.Submit(It.IsAny<SubmitContext>()), Times.Once);
            Assert.NotNull(captured);
            var itemContext = Assert.IsType<ItemSubmitContext>(captured);
            // Only R365 metadata gets included in source metadata
            Assert.Single(itemContext.SourceMetaData);
            Assert.NotNull(itemContext.BinariesSubmitted);
            Assert.Single(itemContext.BinariesSubmitted);
            Assert.True(itemContext.BinariesSubmitted[0].BinaryBlobCreated);
        }

        [Fact]
        public async Task SubmitRecord_NoBinariesNoMetadata_Succeeds()
        {
            _recordPipeline.Setup(a => a.Submit(It.IsAny<SubmitContext>())).Returns(Task.CompletedTask);

            var connector = CreateConnector();
            var record = new Record { ExternalId = "record-2", Title = "Record 2" };

            var result = await _sut.SubmitRecord(connector, record, CancellationToken.None);

            Assert.NotNull(result);
            _recordPipeline.Verify(a => a.Submit(It.IsAny<SubmitContext>()), Times.Once);
        }

        [Fact]
        public async Task SubmitAuditEvent_InvokesAuditEventPipeline()
        {
            SubmitContext? captured = null;
            _auditEventPipeline
                .Setup(a => a.Submit(It.IsAny<SubmitContext>()))
                .Callback<SubmitContext>(c => captured = c)
                .Returns(Task.CompletedTask);

            var connector = CreateConnector();
            var auditEvent = new AuditEvent
            {
                ExternalId = "ae-1",
                Description = "audit",
                EventType = "type",
                MetaDataItems =
                [
                    new MetaDataItem { Name = "R365", Type = "String", Value = "v", MetaDataItemType = MetaDataItemType.R365MetaData }
                ]
            };

            var result = await _sut.SubmitAuditEvent(connector, auditEvent, CancellationToken.None);

            Assert.NotNull(result);
            _auditEventPipeline.Verify(a => a.Submit(It.IsAny<SubmitContext>()), Times.Once);
            Assert.NotNull(captured);
            Assert.Single(captured.SourceMetaData);
        }

        [Fact]
        public async Task SubmitBinary_InvokesBinaryPipeline()
        {
            SubmitContext? captured = null;
            _binaryPipeline
                .Setup(a => a.Submit(It.IsAny<SubmitContext>()))
                .Callback<SubmitContext>(c => captured = c)
                .Returns(Task.CompletedTask);

            var connector = CreateConnector();
            var binaryMetaInfo = new BinaryMetaInfo
            {
                ExternalId = "bin-1",
                ItemExternalId = "record-1",
                FileName = "f.txt",
                FileHash = "hash",
                MimeType = "text/plain",
                SkipEnrichment = true,
                MetaDataItems =
                [
                    new MetaDataItem { Name = "R365", Type = "String", Value = "v", MetaDataItemType = MetaDataItemType.R365MetaData }
                ]
            };
            using var stream = new MemoryStream([1, 2, 3]);

            var result = await _sut.SubmitBinary(connector, binaryMetaInfo, stream, CancellationToken.None);

            Assert.NotNull(result);
            _binaryPipeline.Verify(a => a.Submit(It.IsAny<SubmitContext>()), Times.Once);
            var binaryContext = Assert.IsType<BinarySubmitContext>(captured);
            Assert.True(binaryContext.SkipEnrichment);
            Assert.Single(binaryContext.SourceMetaData);
        }

        [Fact]
        public async Task SubmitAggregation_InvokesAggregationPipeline()
        {
            SubmitContext? captured = null;
            _aggregationPipeline
                .Setup(a => a.Submit(It.IsAny<SubmitContext>()))
                .Callback<SubmitContext>(c => captured = c)
                .Returns(Task.CompletedTask);

            var connector = CreateConnector();
            var aggregation = new Aggregation
            {
                ExternalId = "agg-1",
                Title = "Aggregation 1",
                MetaDataItems =
                [
                    new MetaDataItem { Name = "R365", Type = "String", Value = "v", MetaDataItemType = MetaDataItemType.R365MetaData }
                ]
            };

            var result = await _sut.SubmitAggregation(connector, aggregation, CancellationToken.None);

            Assert.NotNull(result);
            _aggregationPipeline.Verify(a => a.Submit(It.IsAny<SubmitContext>()), Times.Once);
            Assert.NotNull(captured);
            Assert.Equal(Aggregation.ItemTypeId, captured.ItemTypeId);
            Assert.Single(captured.SourceMetaData);
        }

        [Fact]
        public async Task DisposalCallback_InvokesNotificationApiManager()
        {
            _notificationApiManager
                .Setup(a => a.DisposalCallback(
                    It.IsAny<ApiClientFactorySettings>(),
                    It.IsAny<AuthenticationHelperSettings>(),
                    It.IsAny<ItemNotificationDisposalCallbackModel>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var connector = CreateConnector();
            var callback = new ItemNotificationDisposalCallbackModel { ExternalId = "ext-1" };

            await _sut.DisposalCallback(callback, connector, CancellationToken.None);

            _notificationApiManager.Verify(a => a.DisposalCallback(
                It.IsAny<ApiClientFactorySettings>(),
                It.IsAny<AuthenticationHelperSettings>(),
                It.Is<ItemNotificationDisposalCallbackModel>(m => m.ExternalId == "ext-1"),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task SubmitRecord_PipelineThrows_ExceptionIsScopedAndRethrown()
        {
            _recordPipeline
                .Setup(a => a.Submit(It.IsAny<SubmitContext>()))
                .ThrowsAsync(new InvalidOperationException("boom"));

            var connector = CreateConnector();
            var record = new Record { ExternalId = "record-3", Title = "Record 3" };

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(
                () => _sut.SubmitRecord(connector, record, CancellationToken.None));
            Assert.Equal("boom", ex.Message);
            // The observability scope decorates the exception
            Assert.True(ex.HasScope());
        }
    }

    /// <summary>
    /// Tests for R365Pipelines container
    /// </summary>
    public class R365PipelinesTests
    {
        [Fact]
        public void Constructor_ExposesPipelines()
        {
            var record = new Mock<ISubmission>().Object;
            var binary = new Mock<ISubmission>().Object;
            var aggregation = new Mock<ISubmission>().Object;
            var auditEvent = new Mock<ISubmission>().Object;

            var pipelines = new R365Pipelines(record, binary, aggregation, auditEvent);

            Assert.Same(record, pipelines.RecordPipeline);
            Assert.Same(binary, pipelines.BinaryPipeline);
            Assert.Same(aggregation, pipelines.AggregationPipeline);
            Assert.Same(auditEvent, pipelines.AuditEventPipeline);
        }
    }
}
