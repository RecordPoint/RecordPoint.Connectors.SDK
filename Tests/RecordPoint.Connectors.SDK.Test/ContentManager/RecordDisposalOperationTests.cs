using Microsoft.Extensions.DependencyInjection;
using Moq;
using RecordPoint.Connectors.SDK.Caching.Semaphore;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Test.Common;
using RecordPoint.Connectors.SDK.Work;
using Xunit;
using Record = RecordPoint.Connectors.SDK.Content.Record;

namespace RecordPoint.Connectors.SDK.Test.ContentManager
{
    public class RecordDisposalOperationTests : CommonTestBase<RecordDisposalOperationSut>
    {
        public class RecordDisposalAction : IRecordDisposalAction
        {
            public int? NextDelay { get; set; } = null;
            public RecordDisposalResultType RecordDisposalResultType { get; set; } = RecordDisposalResultType.Complete;
            public SemaphoreLockType SemaphoreLockType { get; set; } = SemaphoreLockType.Global;

            public bool ThrowExecption { get; set; } = false;

            public Task<RecordDisposalResult> ExecuteAsync(ConnectorConfigModel connectorConfiguration, Record record, CancellationToken cancellationToken)
            {
                if (ThrowExecption) throw new TestException();

                var RecordDisposalResult = new RecordDisposalResult
                {
                    ResultType = RecordDisposalResultType,
                    NextDelay = NextDelay,
                    SemaphoreLockType = SemaphoreLockType
                };
                return Task.FromResult(RecordDisposalResult);
            }
        }


        [Fact]
        public async Task IfRequestedConnectorMissing_CompletesWithNoUpdates()
        {
            var cancellationToken = CancellationToken.None;

            // Mock the ContentLoader
            var recordDisposalActionMock = new Mock<IRecordDisposalAction>();
            recordDisposalActionMock
                .Setup(lm => lm.ExecuteAsync(It.IsAny<ConnectorConfigModel>(), It.IsAny<Record>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RecordDisposalResult
                {
                    ResultType = RecordDisposalResultType.Complete
                });

            SUT.SelectRecordDisposalActionMock(recordDisposalActionMock);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            var workMessage = SUT.CreateRecordDisposalManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var submitBinaryItem = Services.GetRequiredService<RecordDisposalOperation>();
            await submitBinaryItem.RunWorkRequestAsync(SUT.CreateRecordDisposalRequest(workMessage), cancellationToken);

            // Assert
            Assert.Equal(WorkResultType.Complete, submitBinaryItem.ResultType);
            Assert.Equal("Connector not found", submitBinaryItem.ResultReason);
        }

        [Fact]
        public async Task IfConnectorDisabled_CompletesWithNoUpdates()
        {
            var cancellationToken = CancellationToken.None;

            // Mock the ContentLoader
            var recordDisposalActionMock = new Mock<IRecordDisposalAction>();
            recordDisposalActionMock
                .Setup(lm => lm.ExecuteAsync(It.IsAny<ConnectorConfigModel>(), It.IsAny<Record>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RecordDisposalResult
                {
                    ResultType = RecordDisposalResultType.Complete
                });

            SUT.SelectRecordDisposalActionMock(recordDisposalActionMock);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            ContentManagerSutBase.DisableConnector(connector, DateTimeOffset.Now);
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateRecordDisposalManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var submitBinaryItem = Services.GetRequiredService<RecordDisposalOperation>();
            await submitBinaryItem.RunWorkRequestAsync(SUT.CreateRecordDisposalRequest(workMessage), cancellationToken);

            // Assert
            Assert.Equal(WorkResultType.Complete, submitBinaryItem.ResultType);
            Assert.Equal(DatabaseConnectorConfigurationManager.CONNECTOR_DISABLED_REASON, submitBinaryItem.ResultReason);
        }

        [Fact]
        public async Task RecordDisposalOperation_DisposeRecord_Completes()
        {
            var cancellationToken = CancellationToken.None;

            // Mock the ContentLoader
            var recordDisposalActionMock = new Mock<IRecordDisposalAction>();
            recordDisposalActionMock
                .Setup(lm => lm.ExecuteAsync(It.IsAny<ConnectorConfigModel>(), It.IsAny<Record>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RecordDisposalResult
                {
                    ResultType = RecordDisposalResultType.Complete,
                });

            SUT.SelectRecordDisposalActionMock(recordDisposalActionMock);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateRecordDisposalManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var submitBinaryItem = Services.GetRequiredService<RecordDisposalOperation>();
            await submitBinaryItem.RunWorkRequestAsync(SUT.CreateRecordDisposalRequest(workMessage), cancellationToken);

            var workResult = submitBinaryItem.GetWorkResult();

            // Assert
            Assert.Equal(WorkResultType.Complete, workResult.ResultType);
            Assert.Equal("Record successfully disposed", submitBinaryItem.ResultReason);
        }

        [Fact]
        public async Task RecordDisposalOperation_DisposeDeletedRecord_Completes()
        {
            var cancellationToken = CancellationToken.None;

            // Mock the ContentLoader
            var recordDisposalActionMock = new Mock<IRecordDisposalAction>();
            recordDisposalActionMock
                .Setup(lm => lm.ExecuteAsync(It.IsAny<ConnectorConfigModel>(), It.IsAny<Record>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RecordDisposalResult
                {
                    ResultType = RecordDisposalResultType.Deleted,
                });

            SUT.SelectRecordDisposalActionMock(recordDisposalActionMock);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateRecordDisposalManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var submitBinaryItem = Services.GetRequiredService<RecordDisposalOperation>();
            await submitBinaryItem.RunWorkRequestAsync(SUT.CreateRecordDisposalRequest(workMessage), cancellationToken);

            var workResult = submitBinaryItem.GetWorkResult();

            // Assert
            Assert.Equal(WorkResultType.Complete, workResult.ResultType);
            Assert.Equal("Record already deleted on content source", submitBinaryItem.ResultReason);
        }

        [Fact]
        public async Task RecordDisposalOperation_DisposalFailedException_ThrowsInvalidOperationException()
        {
            var cancellationToken = CancellationToken.None;

            // Mock the ContentLoader
            var recordDisposalActionMock = new Mock<IRecordDisposalAction>();
            recordDisposalActionMock
                .Setup(lm => lm.ExecuteAsync(It.IsAny<ConnectorConfigModel>(), It.IsAny<Record>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RecordDisposalResult
                {
                    ResultType = RecordDisposalResultType.Failed,
                    Reason = "Unknown Exception",
                    Exception = new TestException()
                });

            SUT.SelectRecordDisposalActionMock(recordDisposalActionMock);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateRecordDisposalManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var submitBinaryItem = Services.GetRequiredService<RecordDisposalOperation>();
            try
            {
                await submitBinaryItem.RunWorkRequestAsync(SUT.CreateRecordDisposalRequest(workMessage), cancellationToken);
            }
            catch (Exception ex)
            {
                // Assert
                Assert.IsType<InvalidOperationException>(ex);
                Assert.Equal("Unknown Exception", ex.Message);
            }
        }

        [Fact]
        public async Task IfGlobalSemaphoreLockActive_IsDeferred()
        {
            var cancellationToken = CancellationToken.None;

            var scanner = new RecordDisposalAction
            {
                RecordDisposalResultType = RecordDisposalResultType.Complete
            };
            SUT.SelectRecordDisposalAction(scanner);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();
            semaphoreLockManager.ConnectorConfiguration = connector;
            await semaphoreLockManager.SetSemaphoreAsync(SemaphoreLockType.Scoped, RecordDisposalOperation.WORK_TYPE, null, 10, cancellationToken);

            var workMessage = SUT.CreateRecordDisposalManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var priorWorkItem = Services.GetRequiredService<RecordDisposalOperation>();
            await priorWorkItem.RunWorkRequestAsync(SUT.CreateRecordDisposalRequest(workMessage), cancellationToken);

            await SUT.SetWorkContinue(workMessage);
            var afterWorkItem = Services.GetRequiredService<RecordDisposalOperation>();
            await afterWorkItem.RunWorkRequestAsync(SUT.CreateRecordDisposalRequest(workMessage), cancellationToken);

            // Check results
            Assert.Equal(WorkResultType.Deferred, priorWorkItem.ResultType);
            Assert.Equal(WorkResultType.Deferred, afterWorkItem.ResultType);
        }

        [Fact]
        public async Task IfScopedSemaphoreLockActive_IsDeferred()
        {
            var cancellationToken = CancellationToken.None;

            var scanner = new RecordDisposalAction
            {
                RecordDisposalResultType = RecordDisposalResultType.Complete
            };
            SUT.SelectRecordDisposalAction(scanner);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();
            semaphoreLockManager.ConnectorConfiguration = connector;
            await semaphoreLockManager.SetSemaphoreAsync(SemaphoreLockType.Scoped, RecordDisposalOperation.WORK_TYPE, null, 10, cancellationToken);

            var workMessage = SUT.CreateRecordDisposalManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var priorWorkItem = Services.GetRequiredService<RecordDisposalOperation>();
            await priorWorkItem.RunWorkRequestAsync(SUT.CreateRecordDisposalRequest(workMessage), cancellationToken);

            // Check results
            Assert.Equal(WorkResultType.Deferred, priorWorkItem.ResultType);
        }

        [Fact]
        public async Task IfDifferentScopedSemaphoreLockActive_IsNotDeferred()
        {
            var cancellationToken = CancellationToken.None;

            var scanner = new RecordDisposalAction
            {
                RecordDisposalResultType = RecordDisposalResultType.Complete
            };
            SUT.SelectRecordDisposalAction(scanner);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();
            semaphoreLockManager.ConnectorConfiguration = connector;
            await semaphoreLockManager.SetSemaphoreAsync(SemaphoreLockType.Scoped, RecordDisposalOperation.WORK_TYPE, null, 10, cancellationToken);

            SUT.SemaphoreLockScopedKeyAction.Key = "KEY_456";

            var workMessage = SUT.CreateRecordDisposalManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var priorWorkItem = Services.GetRequiredService<RecordDisposalOperation>();
            await priorWorkItem.RunWorkRequestAsync(SUT.CreateRecordDisposalRequest(workMessage), cancellationToken);

            // Check results
            Assert.Equal(WorkResultType.Complete, priorWorkItem.ResultType);
        }

    }

}
