using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RecordPoint.Connectors.SDK.Caching.Semaphore;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Test.Common;
using RecordPoint.Connectors.SDK.Test.Mock.Work;
using RecordPoint.Connectors.SDK.Work;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.ContentManager
{
    public class SynchronousSubmitRecordOperationTests : CommonTestBase<SynchronousSubmitRecordOperationSut>
    {
        public class BinaryRetrievalAction : IBinaryRetrievalAction
        {
            public int? NextDelay { get; set; } = null;
            public BinaryRetrievalResultType BinaryRetrievalResultType { get; set; } = BinaryRetrievalResultType.Complete;
            public SemaphoreLockType SemaphoreLockType { get; set; } = SemaphoreLockType.Global;

            public bool ThrowExecption { get; set; } = false;

            public Task<BinaryRetrievalResult> ExecuteAsync(ConnectorConfigModel connectorConfiguration, BinaryMetaInfo binaryMetaInfo, CancellationToken cancellationToken)
            {
                if (ThrowExecption) throw new TestException();

                var binaryRetrievalResult = new BinaryRetrievalResult
                {
                    ResultType = BinaryRetrievalResultType,
                    NextDelay = NextDelay,
                    SemaphoreLockType = SemaphoreLockType
                };
                return Task.FromResult(binaryRetrievalResult);
            }
        }

        private static Mock<IBinaryRetrievalAction> CreateBinaryRetrievalActionMock(BinaryRetrievalResultType resultType, string reason)
        {
            var binaryRetrievalActionMock = new Mock<IBinaryRetrievalAction>();
            binaryRetrievalActionMock
                .Setup(lm => lm.ExecuteAsync(It.IsAny<ConnectorConfigModel>(), It.IsAny<BinaryMetaInfo>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BinaryRetrievalResult
                {
                    ResultType = resultType,
                    Reason = reason
                });
            return binaryRetrievalActionMock;
        }

        private static Mock<IBinaryRetrievalAction> CreateBinaryRetrievalActionMock(List<(BinaryRetrievalResultType ResultType, string Reason)> results)
        {
            var binaryRetrievalActionMock = new Mock<IBinaryRetrievalAction>();
            var sequentialResult = binaryRetrievalActionMock.SetupSequence(lm => lm.ExecuteAsync(It.IsAny<ConnectorConfigModel>(), It.IsAny<BinaryMetaInfo>(), It.IsAny<CancellationToken>()));
            foreach (var result in  results)
            {
                sequentialResult.ReturnsAsync(new BinaryRetrievalResult
                {
                    ResultType = result.ResultType,
                    Reason = result.Reason
                });
            }
            return binaryRetrievalActionMock;
        }

        [Fact]
        public async Task IfRequestedConnectorMissing_CompletesWithNoUpdates()
        {
            var cancellationToken = CancellationToken.None;

            // Mock the ContentLoader
            var binaryRetrievalActionMock = CreateBinaryRetrievalActionMock(BinaryRetrievalResultType.Abandoned, "File opened normally, skipping binary submission due to zero byte binary");
            SUT.SelectBinaryRetrievalActionMock(binaryRetrievalActionMock);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            var workMessage = SUT.CreateSynchronousSubmitRecordManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var submitRecordOperation = Services.GetRequiredService<SynchronousSubmitRecordOperation>();
            await submitRecordOperation.RunWorkRequestAsync(SUT.CreateSynchronousSubmitRecordRequest(workMessage, SynchronousSubmitRecordOperationSut.CreateRecord()), cancellationToken);

            // Assert
            Assert.Equal(WorkResultType.Complete, submitRecordOperation.ResultType);
            Assert.Equal("Connector not found", submitRecordOperation.ResultReason);
        }

        [Fact]
        public async Task IfBinariesDisabled_CompletesWithNoUpdates()
        {
            var cancellationToken = CancellationToken.None;

            // Mock the ContentLoader
            var binaryRetrievalActionMock = CreateBinaryRetrievalActionMock(BinaryRetrievalResultType.Abandoned, "File opened normally, skipping binary submission due to zero byte binary");
            SUT.SelectBinaryRetrievalActionMock(binaryRetrievalActionMock);
            SUT.SelectRecordSubmissionCallbackActionMock(new Mock<IRecordSubmissionCallbackAction>());

            var myConfig = new Dictionary<string, string>
            {
                { "Connector:BinariesEnabled", "False" },
            };

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfig)
                .Build();

            await StartSutAsync(config);

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateSynchronousSubmitRecordManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var submitRecordOperation = Services.GetRequiredService<SynchronousSubmitRecordOperation>();
            await submitRecordOperation.RunWorkRequestAsync(SUT.CreateSynchronousSubmitRecordRequest(workMessage, SynchronousSubmitRecordOperationSut.CreateRecord()), cancellationToken);

            // Assert
            Assert.Equal(WorkResultType.Complete, submitRecordOperation.ResultType);
            Assert.Equal("Record submitted", submitRecordOperation.ResultReason);
        }

        [Fact]
        public async Task IfSubmissionDisabled_CompletesWithNoUpdates()
        {
            var cancellationToken = CancellationToken.None;

            // Mock the ContentLoader
            var binaryRetrievalActionMock = CreateBinaryRetrievalActionMock(BinaryRetrievalResultType.Abandoned, "File opened normally, skipping binary submission due to zero byte binary");
            SUT.SelectBinaryRetrievalActionMock(binaryRetrievalActionMock);
            SUT.SelectRecordSubmissionCallbackActionMock(new Mock<IRecordSubmissionCallbackAction>());

            var myConfig = new Dictionary<string, string>
            {
                { "Connector:SubmissionEnabled", "False" },
            };

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfig)
                .Build();

            await StartSutAsync(config);

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateSynchronousSubmitRecordManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var submitRecordOperation = Services.GetRequiredService<SynchronousSubmitRecordOperation>();
            await submitRecordOperation.RunWorkRequestAsync(SUT.CreateSynchronousSubmitRecordRequest(workMessage, SynchronousSubmitRecordOperationSut.CreateRecord()), cancellationToken);

            // Assert
            Assert.Equal(WorkResultType.Complete, submitRecordOperation.ResultType);
            Assert.Equal(DatabaseConnectorConfigurationManager.SUBMISSION_APPSETTING_OFF_REASON, submitRecordOperation.ResultReason);
        }

        [Fact]
        public async Task SynchronousSubmitRecordOperation_SubmitRecord_Completes()
        {
            var cancellationToken = CancellationToken.None;

            // Mock the ContentLoader
            var binaryRetrievalActionMock = CreateBinaryRetrievalActionMock(BinaryRetrievalResultType.Complete, string.Empty);
            SUT.SelectBinaryRetrievalActionMock(binaryRetrievalActionMock);
            SUT.SelectBinarySubmissionCallbackActionMock(new Mock<IBinarySubmissionCallbackAction>());
            SUT.SelectRecordSubmissionCallbackActionMock(new Mock<IRecordSubmissionCallbackAction>());

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateSynchronousSubmitRecordManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var submitRecordOperation = Services.GetRequiredService<SynchronousSubmitRecordOperation>();
            await submitRecordOperation.RunWorkRequestAsync(SUT.CreateSynchronousSubmitRecordRequest(workMessage, SynchronousSubmitRecordOperationSut.CreateRecord()), cancellationToken);

            var workResult = submitRecordOperation.GetWorkResult();

            // Assert
            Assert.Equal(WorkResultType.Complete, workResult.ResultType);
            Assert.Equal("Record submitted", submitRecordOperation.ResultReason);
        }

        [Fact]
        public async Task SynchronousSubmitRecordOperation_SubmitRecordWithMultipleBinaries_Completes()
        {
            var cancellationToken = CancellationToken.None;

            // Mock the ContentLoader
            var binaryRetrievalActionMock = CreateBinaryRetrievalActionMock(BinaryRetrievalResultType.Complete, string.Empty);
            SUT.SelectBinaryRetrievalActionMock(binaryRetrievalActionMock);
            SUT.SelectBinarySubmissionCallbackActionMock(new Mock<IBinarySubmissionCallbackAction>());
            SUT.SelectRecordSubmissionCallbackActionMock(new Mock<IRecordSubmissionCallbackAction>());

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateSynchronousSubmitRecordManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var submitRecordOperation = Services.GetRequiredService<SynchronousSubmitRecordOperation>();
            await submitRecordOperation.RunWorkRequestAsync(SUT.CreateSynchronousSubmitRecordRequest(workMessage, SynchronousSubmitRecordOperationSut.CreateRecord(5)), cancellationToken);

            var workResult = submitRecordOperation.GetWorkResult();

            // Assert
            Assert.Equal(WorkResultType.Complete, workResult.ResultType);
            Assert.Equal("Record submitted", submitRecordOperation.ResultReason);
        }

        [Fact]
        public async Task SynchronousSubmitRecordOperation_SubmitRecordWithMultipleBinaries_SomeBinariesDeferred_Completes_AndRequeued()
        {
            var cancellationToken = CancellationToken.None;

            // Mock the ContentLoader
            var results = new List<(BinaryRetrievalResultType, string)>
            {
                (BinaryRetrievalResultType.Complete, string.Empty),
                (BinaryRetrievalResultType.BackOff, "Back Off"),
                (BinaryRetrievalResultType.Complete, string.Empty),
                (BinaryRetrievalResultType.Complete, string.Empty),
                (BinaryRetrievalResultType.Complete, string.Empty)
            };
            var binaryRetrievalActionMock = CreateBinaryRetrievalActionMock(results);
            SUT.SelectBinaryRetrievalActionMock(binaryRetrievalActionMock);
            SUT.SelectBinarySubmissionCallbackActionMock(new Mock<IBinarySubmissionCallbackAction>());
            SUT.SelectRecordSubmissionCallbackActionMock(new Mock<IRecordSubmissionCallbackAction>());

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateSynchronousSubmitRecordManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var submitRecordOperation = Services.GetRequiredService<SynchronousSubmitRecordOperation>();
            await submitRecordOperation.RunWorkRequestAsync(SUT.CreateSynchronousSubmitRecordRequest(workMessage, SynchronousSubmitRecordOperationSut.CreateRecord(5)), cancellationToken);

            var workResult = submitRecordOperation.GetWorkResult();

            var workQueueClient = Services.GetRequiredService<MockWorkQueueClient>();

            // Assert
            Assert.Equal(WorkResultType.Complete, workResult.ResultType);
            Assert.Equal("Record submitted", submitRecordOperation.ResultReason);
            Assert.NotEmpty(workQueueClient.SubmittedRequests); //New work was submitted for retry
        }

        [Fact]
        public async Task SynchronousSubmitRecordOperation_SubmitDeletedBinary_Completes()
        {
            var cancellationToken = CancellationToken.None;

            // Mock the ContentLoader
            var binaryRetrievalActionMock = CreateBinaryRetrievalActionMock(BinaryRetrievalResultType.Deleted, "The binary does not exist on the content source");
            SUT.SelectBinaryRetrievalActionMock(binaryRetrievalActionMock);
            SUT.SelectBinarySubmissionCallbackActionMock(new Mock<IBinarySubmissionCallbackAction>());
            SUT.SelectRecordSubmissionCallbackActionMock(new Mock<IRecordSubmissionCallbackAction>());

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateSynchronousSubmitRecordManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var submitRecordOperation = Services.GetRequiredService<SynchronousSubmitRecordOperation>();
            await submitRecordOperation.RunWorkRequestAsync(SUT.CreateSynchronousSubmitRecordRequest(workMessage, SynchronousSubmitRecordOperationSut.CreateRecord()), cancellationToken);

            var workResult = submitRecordOperation.GetWorkResult();

            // Assert
            Assert.Equal(WorkResultType.Complete, workResult.ResultType);
            Assert.Equal("Record submitted", submitRecordOperation.ResultReason);
        }

        [Fact]
        public async Task SynchronousSubmitRecordOperation_SubmitZeroByteBinary_BinaryIsSkipped()
        {
            var cancellationToken = CancellationToken.None;

            // Mock the ContentLoader
            var binaryRetrievalActionMock = CreateBinaryRetrievalActionMock(BinaryRetrievalResultType.ZeroBinary, "File opened normally, skipping binary submission due to zero byte binary");
            SUT.SelectBinaryRetrievalActionMock(binaryRetrievalActionMock);
            SUT.SelectBinarySubmissionCallbackActionMock(new Mock<IBinarySubmissionCallbackAction>());
            SUT.SelectRecordSubmissionCallbackActionMock(new Mock<IRecordSubmissionCallbackAction>());

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateSynchronousSubmitRecordManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var submitRecordOperation = Services.GetRequiredService<SynchronousSubmitRecordOperation>();
            await submitRecordOperation.RunWorkRequestAsync(SUT.CreateSynchronousSubmitRecordRequest(workMessage, SynchronousSubmitRecordOperationSut.CreateRecord()), cancellationToken);

            var workResult = submitRecordOperation.GetWorkResult();

            // Assert
            Assert.Equal(WorkResultType.Complete, workResult.ResultType);
            Assert.Equal("Record submitted", submitRecordOperation.ResultReason);
        }

        [Fact]
        public async Task IfGlobalSemaphoreLockActive_IsDeferred()
        {
            var cancellationToken = CancellationToken.None;

            var binaryRetrievalActionMock = CreateBinaryRetrievalActionMock(BinaryRetrievalResultType.Complete, string.Empty);
            SUT.SelectBinaryRetrievalActionMock(binaryRetrievalActionMock);
            SUT.SelectBinarySubmissionCallbackActionMock(new Mock<IBinarySubmissionCallbackAction>());
            SUT.SelectRecordSubmissionCallbackActionMock(new Mock<IRecordSubmissionCallbackAction>());

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();
            semaphoreLockManager.ConnectorConfiguration = connector;
            await semaphoreLockManager.SetSemaphoreAsync(SemaphoreLockType.Global, SynchronousSubmitRecordOperation.WORK_TYPE, null, 60, cancellationToken);

            var workMessage = SUT.CreateSynchronousSubmitRecordManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var record = SynchronousSubmitRecordOperationSut.CreateRecord();
            var priorWorkItem = Services.GetRequiredService<SynchronousSubmitRecordOperation>();
            await priorWorkItem.RunWorkRequestAsync(SUT.CreateSynchronousSubmitRecordRequest(workMessage, record), cancellationToken);

            // Check results
            Assert.Equal(WorkResultType.Deferred, priorWorkItem.ResultType);
        }

        [Fact]
        public async Task IfScopedSemaphoreLockActive_IsDeferred()
        {
            var cancellationToken = CancellationToken.None;

            var binaryRetrievalActionMock = CreateBinaryRetrievalActionMock(BinaryRetrievalResultType.Complete, string.Empty);
            SUT.SelectBinaryRetrievalActionMock(binaryRetrievalActionMock);
            SUT.SelectBinarySubmissionCallbackActionMock(new Mock<IBinarySubmissionCallbackAction>());
            SUT.SelectRecordSubmissionCallbackActionMock(new Mock<IRecordSubmissionCallbackAction>());

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();
            semaphoreLockManager.ConnectorConfiguration = connector;
            await semaphoreLockManager.SetSemaphoreAsync(SemaphoreLockType.Scoped, SynchronousSubmitRecordOperation.WORK_TYPE, null, 60, cancellationToken);

            var workMessage = SUT.CreateSynchronousSubmitRecordManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var record = SynchronousSubmitRecordOperationSut.CreateRecord();
            var priorWorkItem = Services.GetRequiredService<SynchronousSubmitRecordOperation>();
            await priorWorkItem.RunWorkRequestAsync(SUT.CreateSynchronousSubmitRecordRequest(workMessage, record), cancellationToken);

            // Check results
            Assert.Equal(WorkResultType.Deferred, priorWorkItem.ResultType);
        }

        [Fact]
        public async Task IfDifferentScopedSemaphoreLockActive_IsNotDeferred()
        {
            var cancellationToken = CancellationToken.None;

            var binaryRetrievalActionMock = CreateBinaryRetrievalActionMock(BinaryRetrievalResultType.Complete, string.Empty);
            SUT.SelectBinaryRetrievalActionMock(binaryRetrievalActionMock);
            SUT.SelectBinarySubmissionCallbackActionMock(new Mock<IBinarySubmissionCallbackAction>());
            SUT.SelectRecordSubmissionCallbackActionMock(new Mock<IRecordSubmissionCallbackAction>());

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();
            semaphoreLockManager.ConnectorConfiguration = connector;
            await semaphoreLockManager.SetSemaphoreAsync(SemaphoreLockType.Scoped, SubmitBinaryOperation.WORK_TYPE, null, 60, cancellationToken);

            SUT.SemaphoreLockScopedKeyAction.Key = "KEY_456";

            var workMessage = SUT.CreateSynchronousSubmitRecordManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var record = SynchronousSubmitRecordOperationSut.CreateRecord();
            var priorWorkItem = Services.GetRequiredService<SynchronousSubmitRecordOperation>();
            await priorWorkItem.RunWorkRequestAsync(SUT.CreateSynchronousSubmitRecordRequest(workMessage, record), cancellationToken);

            // Check results
            Assert.Equal(WorkResultType.Complete, priorWorkItem.ResultType);
        }

    }

}
