#nullable enable
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using RecordPoint.Connectors.SDK.Test.Mock.R365;
using RecordPoint.Connectors.SDK.Test.Mock.Work;
using RecordPoint.Connectors.SDK.Work;
using Xunit;
using Record = RecordPoint.Connectors.SDK.Content.Record;

namespace RecordPoint.Connectors.SDK.Test.ContentManager
{
    /// <summary>
    /// Exercises the record- and binary-submit switch branches of
    /// <see cref="SynchronousSubmitRecordOperation"/>.
    /// </summary>
    public class SynchronousSubmitRecordOperationStatusTests : CommonTestBase<SynchronousSubmitRecordOperationSut>
    {
        private static Mock<IBinaryRetrievalAction> CompleteRetrievalMock()
        {
            var mock = new Mock<IBinaryRetrievalAction>();
            mock.Setup(x => x.ExecuteAsync(It.IsAny<ConnectorConfigModel>(), It.IsAny<BinaryMetaInfo>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => new BinaryRetrievalResult
                {
                    ResultType = BinaryRetrievalResultType.Complete,
                    Stream = new MemoryStream(new byte[] { 1, 2, 3 })
                });
            return mock;
        }

        private async Task<(SynchronousSubmitRecordOperation Operation, WorkRequest Request)> ArrangeAsync(
            Record record,
            Func<SubmitResult?>? recordSubmitFactory = null,
            Func<SubmitResult?>? binarySubmitFactory = null)
        {
            SUT!.SelectBinaryRetrievalActionMock(CompleteRetrievalMock());
            SUT.SelectBinarySubmissionCallbackActionMock(new Mock<IBinarySubmissionCallbackAction>());
            SUT.SelectRecordSubmissionCallbackActionMock(new Mock<IRecordSubmissionCallbackAction>());

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, CancellationToken.None);

            var mockClient = Services!.GetRequiredService<MockR365Client>();
            if (recordSubmitFactory != null) mockClient.RecordSubmitResultFactory = recordSubmitFactory;
            if (binarySubmitFactory != null) mockClient.BinarySubmitResultFactory = binarySubmitFactory;

            var workMessage = SUT.CreateSynchronousSubmitRecordManagedWorkStatusModel(connector);
            var request = SUT.CreateSynchronousSubmitRecordRequest(workMessage, record);
            var operation = Services!.GetRequiredService<SynchronousSubmitRecordOperation>();
            return (operation, request);
        }

        // ---- Record submit status branches (no binaries so binary loop is skipped) ----

        [Fact]
        public async Task Record_Skipped_Completes()
        {
            var (operation, request) = await ArrangeAsync(
                SynchronousSubmitRecordOperationSut.CreateRecord(0),
                recordSubmitFactory: () => new SubmitResult { SubmitStatus = SubmitResult.Status.Skipped, Reason = "not needed" });

            await operation.RunWorkRequestAsync(request, CancellationToken.None);

            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            Assert.Equal("Record skipped", operation.ResultReason);
        }

        [Fact]
        public async Task Record_Deferred_CompletesAndRequeues()
        {
            var (operation, request) = await ArrangeAsync(
                SynchronousSubmitRecordOperationSut.CreateRecord(0),
                recordSubmitFactory: () => new SubmitResult { SubmitStatus = SubmitResult.Status.Deferred, WaitUntilTime = null });

            await operation.RunWorkRequestAsync(request, CancellationToken.None);

            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            Assert.Equal("Record was deferred by Records365", operation.ResultReason);

            var workQueueClient = Services!.GetRequiredService<MockWorkQueueClient>();
            Assert.Contains(workQueueClient.SubmittedRequests, r => r.WorkType == SynchronousSubmitRecordOperation.WORK_TYPE);
        }

        [Fact]
        public async Task Record_TooManyRequests_CompletesAndRequeues()
        {
            var (operation, request) = await ArrangeAsync(
                SynchronousSubmitRecordOperationSut.CreateRecord(0),
                recordSubmitFactory: () => new SubmitResult { SubmitStatus = SubmitResult.Status.TooManyRequests, WaitUntilTime = null });

            await operation.RunWorkRequestAsync(request, CancellationToken.None);

            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            Assert.Equal("Backoff requested by Records365", operation.ResultReason);
        }

        [Fact]
        public async Task Record_ConnectorDisabledStatus_Abandons()
        {
            var (operation, request) = await ArrangeAsync(
                SynchronousSubmitRecordOperationSut.CreateRecord(0),
                recordSubmitFactory: () => new SubmitResult { SubmitStatus = SubmitResult.Status.ConnectorDisabled });

            await operation.RunWorkRequestAsync(request, CancellationToken.None);

            Assert.Equal(WorkResultType.Abandoned, operation.ResultType);
            Assert.Equal("Connector was disabled", operation.ResultReason);
        }

        [Fact]
        public async Task Record_ConnectorNotFoundStatus_Abandons()
        {
            var (operation, request) = await ArrangeAsync(
                SynchronousSubmitRecordOperationSut.CreateRecord(0),
                recordSubmitFactory: () => new SubmitResult { SubmitStatus = SubmitResult.Status.ConnectorNotFound });

            await operation.RunWorkRequestAsync(request, CancellationToken.None);

            Assert.Equal(WorkResultType.Abandoned, operation.ResultType);
            Assert.Equal("The connector was not found in Records365", operation.ResultReason);
        }

        [Fact]
        public async Task Record_NullResult_Fails()
        {
            var (operation, request) = await ArrangeAsync(
                SynchronousSubmitRecordOperationSut.CreateRecord(0),
                recordSubmitFactory: () => null);

            await operation.RunWorkRequestAsync(request, CancellationToken.None);

            Assert.Equal(WorkResultType.Failed, operation.ResultType);
            Assert.Equal("Unexpected no submit result", operation.ResultReason);
        }

        [Fact]
        public async Task Record_UnexpectedStatus_Fails()
        {
            var (operation, request) = await ArrangeAsync(
                SynchronousSubmitRecordOperationSut.CreateRecord(0),
                recordSubmitFactory: () => new SubmitResult { SubmitStatus = (SubmitResult.Status)999 });

            await operation.RunWorkRequestAsync(request, CancellationToken.None);

            Assert.Equal(WorkResultType.Failed, operation.ResultType);
            Assert.StartsWith("Unexpected submit result", operation.ResultReason);
        }

        // ---- Binary submit status branches ----

        [Fact]
        public async Task Binary_Deferred_DefersRecord()
        {
            var (operation, request) = await ArrangeAsync(
                SynchronousSubmitRecordOperationSut.CreateRecord(1),
                binarySubmitFactory: () => new SubmitResult
                {
                    SubmitStatus = SubmitResult.Status.Deferred,
                    WaitUntilTime = new DateTime(2030, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                });

            await operation.RunWorkRequestAsync(request, CancellationToken.None);

            Assert.Equal(WorkResultType.Deferred, operation.ResultType);
            Assert.Contains("Binary submission deferred", operation.ResultReason);
        }

        [Fact]
        public async Task Binary_TooManyRequests_DefersRecord()
        {
            var (operation, request) = await ArrangeAsync(
                SynchronousSubmitRecordOperationSut.CreateRecord(1),
                binarySubmitFactory: () => new SubmitResult
                {
                    SubmitStatus = SubmitResult.Status.TooManyRequests,
                    WaitUntilTime = new DateTime(2030, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                });

            await operation.RunWorkRequestAsync(request, CancellationToken.None);

            Assert.Equal(WorkResultType.Deferred, operation.ResultType);
            Assert.Contains("Too many requests", operation.ResultReason);
        }

        [Fact]
        public async Task Binary_ConnectorDisabled_AbandonsRecord()
        {
            var (operation, request) = await ArrangeAsync(
                SynchronousSubmitRecordOperationSut.CreateRecord(1),
                binarySubmitFactory: () => new SubmitResult { SubmitStatus = SubmitResult.Status.ConnectorDisabled });

            await operation.RunWorkRequestAsync(request, CancellationToken.None);

            Assert.Equal(WorkResultType.Abandoned, operation.ResultType);
            Assert.Equal("Connector disabled", operation.ResultReason);
        }

        [Fact]
        public async Task Binary_ConnectorNotFound_AbandonsRecord()
        {
            var (operation, request) = await ArrangeAsync(
                SynchronousSubmitRecordOperationSut.CreateRecord(1),
                binarySubmitFactory: () => new SubmitResult { SubmitStatus = SubmitResult.Status.ConnectorNotFound });

            await operation.RunWorkRequestAsync(request, CancellationToken.None);

            Assert.Equal(WorkResultType.Abandoned, operation.ResultType);
            Assert.Equal("Connector not found", operation.ResultReason);
        }

        [Fact]
        public async Task Binary_Skipped_RecordStillSubmitted()
        {
            var (operation, request) = await ArrangeAsync(
                SynchronousSubmitRecordOperationSut.CreateRecord(1),
                binarySubmitFactory: () => new SubmitResult { SubmitStatus = SubmitResult.Status.Skipped });

            await operation.RunWorkRequestAsync(request, CancellationToken.None);

            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            Assert.Equal("Record submitted", operation.ResultReason);
        }
    }
}
