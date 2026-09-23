#nullable enable
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using RecordPoint.Connectors.SDK.Caching.Semaphore;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Notifications;
using RecordPoint.Connectors.SDK.Test.Mock.R365;
using RecordPoint.Connectors.SDK.Work;
using Xunit;
using Record = RecordPoint.Connectors.SDK.Content.Record;

namespace RecordPoint.Connectors.SDK.Test.ContentManager
{
    /// <summary>
    /// SUT for record disposal with the disposal callback enabled.
    /// </summary>
    public class RecordDisposalCallbackSut : RecordDisposalOperationSut
    {
        protected override IHostBuilder CreateSutBuilder()
        {
            return base
                .CreateSutBuilder()
                .ConfigureServices(svcs => svcs.Configure<RecordDisposalOptions>(o => o.SendDisposalCallback = true));
        }
    }

    /// <summary>
    /// Exercises the disposal callback, back-off and unexpected-result branches of
    /// <see cref="RecordDisposalOperation"/>.
    /// </summary>
    public class RecordDisposalCallbackTests : CommonTestBase<RecordDisposalCallbackSut>
    {
        private static Mock<IRecordDisposalAction> DisposalMock(RecordDisposalResult result)
        {
            var mock = new Mock<IRecordDisposalAction>();
            mock.Setup(x => x.ExecuteAsync(It.IsAny<ConnectorConfigModel>(), It.IsAny<Record>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);
            return mock;
        }

        private async Task<RecordDisposalOperation> ArrangeAsync(RecordDisposalResult result)
        {
            SUT!.SelectRecordDisposalActionMock(DisposalMock(result));

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, CancellationToken.None);

            return Services!.GetRequiredService<RecordDisposalOperation>();
        }

        private async Task RunAsync(RecordDisposalOperation operation)
        {
            var connector = ContentManagerSutBase.CreateConnector1();
            var workMessage = SUT!.CreateRecordDisposalManagedWorkStatusModel(connector);
            await operation.RunWorkRequestAsync(SUT.CreateRecordDisposalRequest(workMessage), CancellationToken.None);
        }

        [Fact]
        public async Task Complete_WithCallbackEnabled_SendsDestroyedCallback()
        {
            var operation = await ArrangeAsync(new RecordDisposalResult { ResultType = RecordDisposalResultType.Complete });
            await RunAsync(operation);

            Assert.Equal(WorkResultType.Complete, operation.ResultType);

            var client = Services!.GetRequiredService<MockR365Client>();
            var callback = Assert.Single(client.DisposalCallbacks);
            Assert.Equal(ItemDisposalStatus.Destroyed.ToString(), callback.DisposalStatus);
        }

        [Fact]
        public async Task Deleted_WithCallbackEnabled_SendsDestroyedCallbackWithMessage()
        {
            var operation = await ArrangeAsync(new RecordDisposalResult { ResultType = RecordDisposalResultType.Deleted });
            await RunAsync(operation);

            Assert.Equal(WorkResultType.Complete, operation.ResultType);

            var client = Services!.GetRequiredService<MockR365Client>();
            var callback = Assert.Single(client.DisposalCallbacks);
            Assert.Equal(ItemDisposalStatus.Destroyed.ToString(), callback.DisposalStatus);
            Assert.Equal("Record already deleted in content source", callback.StatusMessage);
        }

        [Fact]
        public async Task Failed_WithCallbackEnabled_SendsDestroyFailedCallbackAndThrows()
        {
            var operation = await ArrangeAsync(new RecordDisposalResult
            {
                ResultType = RecordDisposalResultType.Failed,
                Reason = "disposal failed"
            });
            await RunAsync(operation);

            Assert.Equal(WorkResultType.Failed, operation.ResultType);

            var client = Services!.GetRequiredService<MockR365Client>();
            var callback = Assert.Single(client.DisposalCallbacks);
            Assert.Equal(ItemDisposalStatus.DestroyFailed.ToString(), callback.DisposalStatus);
        }

        [Fact]
        public async Task BackOff_Defers()
        {
            var operation = await ArrangeAsync(new RecordDisposalResult
            {
                ResultType = RecordDisposalResultType.BackOff,
                SemaphoreLockType = SemaphoreLockType.Global,
                NextDelay = 5
            });
            await RunAsync(operation);

            Assert.Equal(WorkResultType.Deferred, operation.ResultType);
        }

        [Fact]
        public async Task UnexpectedResultType_ThrowsAndFails()
        {
            var operation = await ArrangeAsync(new RecordDisposalResult { ResultType = (RecordDisposalResultType)999 });
            await RunAsync(operation);

            Assert.Equal(WorkResultType.Failed, operation.ResultType);
            Assert.StartsWith("Unexpected record disposal result", operation.ResultReason);
        }
    }
}
