#nullable enable
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RecordPoint.Connectors.SDK.Caching.Semaphore;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using RecordPoint.Connectors.SDK.Test.Common;
using RecordPoint.Connectors.SDK.Test.Mock.R365;
using RecordPoint.Connectors.SDK.Test.Mock.Work;
using RecordPoint.Connectors.SDK.Work;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.ContentManager
{
    /// <summary>
    /// Exercises the submit-result and binary-retrieval branches of
    /// <see cref="SubmitBinaryOperation"/> that are not covered by the happy-path tests.
    /// </summary>
    public class SubmitBinaryOperationStatusTests : CommonTestBase<SubmitBinaryOperationSut>
    {
        private static Mock<IBinaryRetrievalAction> RetrievalMock(BinaryRetrievalResult result)
        {
            var mock = new Mock<IBinaryRetrievalAction>();
            mock.Setup(x => x.ExecuteAsync(It.IsAny<ConnectorConfigModel>(), It.IsAny<BinaryMetaInfo>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);
            return mock;
        }

        private async Task<SubmitBinaryOperation> ArrangeCompleteRetrievalAsync(Func<SubmitResult?> submitFactory)
        {
            SUT!.SelectBinaryRetrievalActionMock(RetrievalMock(new BinaryRetrievalResult { ResultType = BinaryRetrievalResultType.Complete }));
            SUT.SelectBinarySubmissionCallbackActionMock(new Mock<IBinarySubmissionCallbackAction>());

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, CancellationToken.None);

            var mockClient = Services!.GetRequiredService<MockR365Client>();
            mockClient.BinarySubmitResultFactory = submitFactory;

            return Services!.GetRequiredService<SubmitBinaryOperation>();
        }

        private async Task RunAsync(SubmitBinaryOperation operation)
        {
            var connector = ContentManagerSutBase.CreateConnector1();
            var workMessage = SUT!.CreateSubmitBinaryManagedWorkStatusModel(connector);
            await operation.RunWorkRequestAsync(SUT.CreateSubmitBinaryRequest(workMessage), CancellationToken.None);
        }

        [Fact]
        public async Task SubmitBinary_Deferred_Completes()
        {
            var operation = await ArrangeCompleteRetrievalAsync(() => new SubmitResult { SubmitStatus = SubmitResult.Status.Deferred, WaitUntilTime = new DateTime(2030, 1, 1, 0, 0, 0, DateTimeKind.Utc) });
            await RunAsync(operation);

            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            Assert.Equal("Item was deferred by Records365", operation.ResultReason);

            var workQueueClient = Services!.GetRequiredService<MockWorkQueueClient>();
            Assert.Contains(workQueueClient.SubmittedRequests, r => r.WorkType == SubmitBinaryOperation.WORK_TYPE);
        }

        [Fact]
        public async Task SubmitBinary_TooManyRequests_Completes_AndRequeuesWithDefaultDelay()
        {
            var operation = await ArrangeCompleteRetrievalAsync(() => new SubmitResult { SubmitStatus = SubmitResult.Status.TooManyRequests, WaitUntilTime = null });
            await RunAsync(operation);

            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            Assert.Equal("Backoff requested by Records365", operation.ResultReason);

            var workQueueClient = Services!.GetRequiredService<MockWorkQueueClient>();
            Assert.Contains(workQueueClient.SubmittedRequests, r => r.WorkType == SubmitBinaryOperation.WORK_TYPE);
        }

        [Fact]
        public async Task SubmitBinary_ConnectorDisabledStatus_Abandons()
        {
            var operation = await ArrangeCompleteRetrievalAsync(() => new SubmitResult { SubmitStatus = SubmitResult.Status.ConnectorDisabled });
            await RunAsync(operation);

            Assert.Equal(WorkResultType.Abandoned, operation.ResultType);
            Assert.Equal("The Records365 connector was disabled", operation.ResultReason);
        }

        [Fact]
        public async Task SubmitBinary_ConnectorNotFoundStatus_Abandons()
        {
            var operation = await ArrangeCompleteRetrievalAsync(() => new SubmitResult { SubmitStatus = SubmitResult.Status.ConnectorNotFound });
            await RunAsync(operation);

            Assert.Equal(WorkResultType.Abandoned, operation.ResultType);
            Assert.Equal("The connector was not found in Records365", operation.ResultReason);
        }

        [Fact]
        public async Task SubmitBinary_UnexpectedStatus_Fails()
        {
            var operation = await ArrangeCompleteRetrievalAsync(() => new SubmitResult { SubmitStatus = (SubmitResult.Status)999 });
            await RunAsync(operation);

            Assert.Equal(WorkResultType.Failed, operation.ResultType);
            Assert.StartsWith("Unexpected result", operation.ResultReason);
        }

        [Fact]
        public async Task SubmitBinary_RetrievalFailed_ThrowsAndFails()
        {
            SUT!.SelectBinaryRetrievalActionMock(RetrievalMock(new BinaryRetrievalResult
            {
                ResultType = BinaryRetrievalResultType.Failed,
                Reason = "boom",
                Exception = new TestException()
            }));

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, CancellationToken.None);

            var operation = Services!.GetRequiredService<SubmitBinaryOperation>();
            await RunAsync(operation);

            Assert.Equal(WorkResultType.Failed, operation.ResultType);
            Assert.Equal("boom", operation.ResultReason);
        }

        [Fact]
        public async Task SubmitBinary_RetrievalBackOff_Defers()
        {
            SUT!.SelectBinaryRetrievalActionMock(RetrievalMock(new BinaryRetrievalResult
            {
                ResultType = BinaryRetrievalResultType.BackOff,
                SemaphoreLockType = SemaphoreLockType.Global,
                NextDelay = 5
            }));

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, CancellationToken.None);

            var operation = Services!.GetRequiredService<SubmitBinaryOperation>();
            await RunAsync(operation);

            Assert.Equal(WorkResultType.Deferred, operation.ResultType);
        }

        [Fact]
        public async Task SubmitBinary_RetrievalAbandoned_Abandons()
        {
            SUT!.SelectBinaryRetrievalActionMock(RetrievalMock(new BinaryRetrievalResult
            {
                ResultType = BinaryRetrievalResultType.Abandoned,
                Reason = "abandon reason"
            }));

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, CancellationToken.None);

            var operation = Services!.GetRequiredService<SubmitBinaryOperation>();
            await RunAsync(operation);

            Assert.Equal(WorkResultType.Abandoned, operation.ResultType);
            Assert.Equal("abandon reason", operation.ResultReason);
        }
    }
}
