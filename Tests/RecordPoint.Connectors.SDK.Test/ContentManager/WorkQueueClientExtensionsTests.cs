#nullable enable
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Work;
using Xunit;
using Record = RecordPoint.Connectors.SDK.Content.Record;

namespace RecordPoint.Connectors.SDK.Test.ContentManager
{
    /// <summary>
    /// Unit tests for the <see cref="WorkQueueClientExtensions"/> submission helpers.
    /// </summary>
    public class WorkQueueClientExtensionsTests
    {
        private static ConnectorConfigModel Connector() => new()
        {
            Id = "connector-1",
            TenantId = "tenant-1",
            TenantDomainName = "tenant.example.com"
        };

        private static (Mock<IWorkQueueClient> Mock, List<WorkRequest> Captured) CreateClient()
        {
            var captured = new List<WorkRequest>();
            var mock = new Mock<IWorkQueueClient>();
            mock.Setup(x => x.SubmitWorkAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
                .Callback<WorkRequest, CancellationToken>((wr, _) => captured.Add(wr))
                .Returns(Task.CompletedTask);
            return (mock, captured);
        }

        private static void AssertCommonFields(WorkRequest request, string expectedWorkType)
        {
            Assert.Equal(expectedWorkType, request.WorkType);
            Assert.Equal("connector-1", request.ConnectorConfigId);
            Assert.Equal("tenant-1", request.TenantId);
            Assert.Equal("tenant.example.com", request.TenantDomainName);
            Assert.False(string.IsNullOrEmpty(request.WorkId));
            Assert.False(string.IsNullOrEmpty(request.Body));
        }

        [Fact]
        public async Task SubmitRecordAsync_BuildsRecordWorkRequest()
        {
            var (mock, captured) = CreateClient();
            await mock.Object.SubmitRecordAsync(Connector(), new Record { ExternalId = "r1" }, null, CancellationToken.None);

            AssertCommonFields(Assert.Single(captured), SubmitRecordOperation.WORK_TYPE);
        }

        [Fact]
        public async Task SubmitBinaryAsync_BuildsBinaryWorkRequest()
        {
            var (mock, captured) = CreateClient();
            await mock.Object.SubmitBinaryAsync(Connector(), new BinaryMetaInfo { ExternalId = "b1" }, null, CancellationToken.None);

            AssertCommonFields(Assert.Single(captured), SubmitBinaryOperation.WORK_TYPE);
        }

        [Fact]
        public async Task SubmitAggregationAsync_BuildsAggregationWorkRequest()
        {
            var (mock, captured) = CreateClient();
            await mock.Object.SubmitAggregationAsync(Connector(), new Aggregation { ExternalId = "a1" }, null, CancellationToken.None);

            AssertCommonFields(Assert.Single(captured), SubmitAggregationOperation.WORK_TYPE);
        }

        [Fact]
        public async Task SubmitAuditEventAsync_BuildsAuditEventWorkRequest()
        {
            var (mock, captured) = CreateClient();
            await mock.Object.SubmitAuditEventAsync(Connector(), new AuditEvent { ExternalId = "e1" }, null, CancellationToken.None);

            AssertCommonFields(Assert.Single(captured), SubmitAuditEventOperation.WORK_TYPE);
        }

        [Fact]
        public async Task DisposeRecordAsync_BuildsRecordDisposalWorkRequest()
        {
            var (mock, captured) = CreateClient();
            await mock.Object.DisposeRecordAsync(Connector(), new Record { ExternalId = "r1" }, null, CancellationToken.None);

            AssertCommonFields(Assert.Single(captured), RecordDisposalOperation.WORK_TYPE);
        }
    }
}
