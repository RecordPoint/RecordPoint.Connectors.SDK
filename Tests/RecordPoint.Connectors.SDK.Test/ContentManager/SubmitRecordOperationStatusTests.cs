#nullable enable
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RecordPoint.Connectors.SDK.Abstractions.ContentManager;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using RecordPoint.Connectors.SDK.Test.Mock.R365;
using RecordPoint.Connectors.SDK.Test.Mock.Work;
using RecordPoint.Connectors.SDK.Work;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.ContentManager
{
    /// <summary>
    /// Exercises the submit-result switch branches of <see cref="SubmitOperationBase{T}"/>
    /// (via <see cref="SubmitRecordOperation"/>): Skipped, Deferred, TooManyRequests,
    /// ConnectorDisabled, ConnectorNotFound, null result and unexpected status.
    /// </summary>
    public class SubmitRecordOperationStatusTests : CommonTestBase<SubmitRecordOperationSut>
    {
        private async Task<SubmitRecordOperation> ArrangeAsync(Func<SubmitResult?> resultFactory)
        {
            SUT!.SelectRecordSubmissionCallbackActionMock(new Mock<IRecordSubmissionCallbackAction>());

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, CancellationToken.None);

            var mockClient = Services!.GetRequiredService<MockR365Client>();
            mockClient.RecordSubmitResultFactory = resultFactory;

            return Services!.GetRequiredService<SubmitRecordOperation>();
        }

        private async Task RunAsync(SubmitRecordOperation operation)
        {
            var connector = ContentManagerSutBase.CreateConnector1();
            var workMessage = SUT!.CreateSubmitRecordManagedWorkStatusModel(connector);
            await operation.RunWorkRequestAsync(SUT.CreateSubmitRecordRequest(workMessage), CancellationToken.None);
        }

        [Fact]
        public async Task SubmitRecord_Skipped_Completes()
        {
            var operation = await ArrangeAsync(() => new SubmitResult { SubmitStatus = SubmitResult.Status.Skipped });
            await RunAsync(operation);

            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            Assert.Equal("Record skipped", operation.ResultReason);
        }

        [Fact]
        public async Task SubmitRecord_Deferred_CompletesAndRequeues()
        {
            var operation = await ArrangeAsync(() => new SubmitResult { SubmitStatus = SubmitResult.Status.Deferred, WaitUntilTime = new DateTime(2030, 1, 1, 0, 0, 0, DateTimeKind.Utc) });
            await RunAsync(operation);

            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            Assert.Equal("Record was deferred by Records365", operation.ResultReason);

            var workQueueClient = Services!.GetRequiredService<MockWorkQueueClient>();
            Assert.Contains(workQueueClient.SubmittedRequests, r => r.WorkType == SubmitRecordOperation.WORK_TYPE);
        }

        [Fact]
        public async Task SubmitRecord_TooManyRequests_CompletesAndRequeues()
        {
            var operation = await ArrangeAsync(() => new SubmitResult { SubmitStatus = SubmitResult.Status.TooManyRequests, WaitUntilTime = null });
            await RunAsync(operation);

            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            Assert.Equal("Backoff requested by Records365", operation.ResultReason);

            var workQueueClient = Services!.GetRequiredService<MockWorkQueueClient>();
            Assert.Contains(workQueueClient.SubmittedRequests, r => r.WorkType == SubmitRecordOperation.WORK_TYPE);
        }

        [Fact]
        public async Task SubmitRecord_ConnectorDisabledStatus_Abandons()
        {
            var operation = await ArrangeAsync(() => new SubmitResult { SubmitStatus = SubmitResult.Status.ConnectorDisabled });
            await RunAsync(operation);

            Assert.Equal(WorkResultType.Abandoned, operation.ResultType);
            Assert.Equal("Connector was disabled", operation.ResultReason);
        }

        [Fact]
        public async Task SubmitRecord_ConnectorNotFoundStatus_Abandons()
        {
            var operation = await ArrangeAsync(() => new SubmitResult { SubmitStatus = SubmitResult.Status.ConnectorNotFound });
            await RunAsync(operation);

            Assert.Equal(WorkResultType.Abandoned, operation.ResultType);
            Assert.Equal("The connector was not found in Records365", operation.ResultReason);
        }

        [Fact]
        public async Task SubmitRecord_NullResult_Fails()
        {
            var operation = await ArrangeAsync(() => null);
            await RunAsync(operation);

            Assert.Equal(WorkResultType.Failed, operation.ResultType);
            Assert.Equal("Unexpected no submit result", operation.ResultReason);
        }

        [Fact]
        public async Task SubmitRecord_UnexpectedStatus_Fails()
        {
            var operation = await ArrangeAsync(() => new SubmitResult { SubmitStatus = (SubmitResult.Status)999 });
            await RunAsync(operation);

            Assert.Equal(WorkResultType.Failed, operation.ResultType);
            Assert.StartsWith("Unexpected submit result", operation.ResultReason);
        }
    }
}
