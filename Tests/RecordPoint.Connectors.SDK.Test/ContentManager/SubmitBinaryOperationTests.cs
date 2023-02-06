using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RecordPoint.Connectors.SDK.Caching.Semaphore;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Test.Common;
using RecordPoint.Connectors.SDK.Work;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.ContentManager
{
    public class SubmitBinaryOperationTests : CommonTestBase<SubmitBinaryOperationSut>
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


        [Fact]
        public async Task IfRequestedConnectorMissing_CompletesWithNoUpdates()
        {
            var cancellationToken = CancellationToken.None;

            // Mock the ContentLoader
            var binaryRetrievalActionMock = new Mock<IBinaryRetrievalAction>();
            binaryRetrievalActionMock
                .Setup(lm => lm.ExecuteAsync(It.IsAny<ConnectorConfigModel>(), It.IsAny<BinaryMetaInfo>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BinaryRetrievalResult
                {
                    ResultType = BinaryRetrievalResultType.ZeroBinary,
                    Reason = "File opened normally, skipping binary submission due to zero byte binary"
                });

            SUT.SelectBinaryRetrievalActionMock(binaryRetrievalActionMock);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            var workMessage = SUT.CreateSubmitBinaryManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var submitBinaryItem = Services.GetRequiredService<SubmitBinaryOperation>();
            await submitBinaryItem.RunWorkRequestAsync(SUT.CreateSubmitBinaryRequest(workMessage), cancellationToken);

            // Assert
            Assert.Equal(WorkResultType.Complete, submitBinaryItem.ResultType);
            Assert.Equal("Connector not found", submitBinaryItem.ResultReason);
        }

        [Fact]
        public async Task IfBinariesDisabled_CompletesWithNoUpdates()
        {
            var cancellationToken = CancellationToken.None;

            // Mock the ContentLoader
            var binaryRetrievalActionMock = new Mock<IBinaryRetrievalAction>();
            binaryRetrievalActionMock
                .Setup(lm => lm.ExecuteAsync(It.IsAny<ConnectorConfigModel>(), It.IsAny<BinaryMetaInfo>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BinaryRetrievalResult
                {
                    ResultType = BinaryRetrievalResultType.ZeroBinary,
                    Reason = "File opened normally, skipping binary submission due to zero byte binary"
                });

            SUT.SelectBinaryRetrievalActionMock(binaryRetrievalActionMock);

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

            var workMessage = SUT.CreateSubmitBinaryManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var submitBinaryItem = Services.GetRequiredService<SubmitBinaryOperation>();
            await submitBinaryItem.RunWorkRequestAsync(SUT.CreateSubmitBinaryRequest(workMessage), cancellationToken);

            // Assert
            Assert.Equal(WorkResultType.Complete, submitBinaryItem.ResultType);
            Assert.Equal(DatabaseConnectorConfigurationManager.BINARY_APPSETTING_OFF_REASON, submitBinaryItem.ResultReason);
        }

        [Fact]
        public async Task IfSubmissionDisabled_CompletesWithNoUpdates()
        {
            var cancellationToken = CancellationToken.None;

            // Mock the ContentLoader
            var binaryRetrievalActionMock = new Mock<IBinaryRetrievalAction>();
            binaryRetrievalActionMock
                .Setup(lm => lm.ExecuteAsync(It.IsAny<ConnectorConfigModel>(), It.IsAny<BinaryMetaInfo>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BinaryRetrievalResult
                {
                    ResultType = BinaryRetrievalResultType.ZeroBinary,
                    Reason = "File opened normally, skipping binary submission due to zero byte binary"
                });

            SUT.SelectBinaryRetrievalActionMock(binaryRetrievalActionMock);

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

            var workMessage = SUT.CreateSubmitBinaryManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var submitBinaryItem = Services.GetRequiredService<SubmitBinaryOperation>();
            await submitBinaryItem.RunWorkRequestAsync(SUT.CreateSubmitBinaryRequest(workMessage), cancellationToken);

            // Assert
            Assert.Equal(WorkResultType.Complete, submitBinaryItem.ResultType);
            Assert.Equal(DatabaseConnectorConfigurationManager.SUBMISSION_APPSETTING_OFF_REASON, submitBinaryItem.ResultReason);
        }

        [Fact]
        public async Task SubmitBinaryOperation_SubmitBinary_Completes()
        {
            var cancellationToken = CancellationToken.None;

            // Mock the ContentLoader
            var binaryRetrievalActionMock = new Mock<IBinaryRetrievalAction>();
            binaryRetrievalActionMock
                .Setup(lm => lm.ExecuteAsync(It.IsAny<ConnectorConfigModel>(), It.IsAny<BinaryMetaInfo>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BinaryRetrievalResult
                {
                    ResultType = BinaryRetrievalResultType.Complete
                });

            SUT.SelectBinaryRetrievalActionMock(binaryRetrievalActionMock);
            SUT.SelectBinarySubmissionCallbackActionMock(new Mock<IBinarySubmissionCallbackAction>());

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateSubmitBinaryManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var submitBinaryItem = Services.GetRequiredService<SubmitBinaryOperation>();
            await submitBinaryItem.RunWorkRequestAsync(SUT.CreateSubmitBinaryRequest(workMessage), cancellationToken);

            var workResult = submitBinaryItem.WorkResult;

            // Assert
            Assert.Equal(WorkResultType.Complete, workResult.ResultType);
            Assert.Equal("Binary successfully submitted", submitBinaryItem.ResultReason);
        }

        [Fact]
        public async Task SubmitBinaryOperation_SubmitDeletedBinary_Completes()
        {
            var cancellationToken = CancellationToken.None;

            // Mock the ContentLoader
            var binaryRetrievalActionMock = new Mock<IBinaryRetrievalAction>();
            binaryRetrievalActionMock
                .Setup(lm => lm.ExecuteAsync(It.IsAny<ConnectorConfigModel>(), It.IsAny<BinaryMetaInfo>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BinaryRetrievalResult
                {
                    ResultType = BinaryRetrievalResultType.Deleted
                });

            SUT.SelectBinaryRetrievalActionMock(binaryRetrievalActionMock);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateSubmitBinaryManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var submitBinaryItem = Services.GetRequiredService<SubmitBinaryOperation>();
            await submitBinaryItem.RunWorkRequestAsync(SUT.CreateSubmitBinaryRequest(workMessage), cancellationToken);

            var workResult = submitBinaryItem.WorkResult;

            // Assert
            Assert.Equal(WorkResultType.Complete, workResult.ResultType);
            Assert.Equal("Binary skipped as expected", submitBinaryItem.ResultReason);
        }

        [Fact]
        public async Task SubmitBinaryOperation_SubmitZeroByteBinary_IsSkipped()
        {
            var cancellationToken = CancellationToken.None;

            // Mock the ContentLoader
            var binaryRetrievalActionMock = new Mock<IBinaryRetrievalAction>();
            binaryRetrievalActionMock
                .Setup(lm => lm.ExecuteAsync(It.IsAny<ConnectorConfigModel>(), It.IsAny<BinaryMetaInfo>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BinaryRetrievalResult
                {
                    ResultType = BinaryRetrievalResultType.ZeroBinary,
                    Reason = "File opened normally, skipping binary submission due to zero byte binary"
                });

            SUT.SelectBinaryRetrievalActionMock(binaryRetrievalActionMock);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateSubmitBinaryManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var submitBinaryItem = Services.GetRequiredService<SubmitBinaryOperation>();
            await submitBinaryItem.RunWorkRequestAsync(SUT.CreateSubmitBinaryRequest(workMessage), cancellationToken);

            var workResult = submitBinaryItem.WorkResult;

            // Assert
            Assert.Equal(WorkResultType.Complete, workResult.ResultType);
            Assert.Contains("Binary skipped as expected", workResult.Reason);
        }

        [Fact]
        public async Task IfGlobalSemaphoreLockActive_IsDeferred()
        {
            var cancellationToken = CancellationToken.None;

            var scanner = new BinaryRetrievalAction
            {
                BinaryRetrievalResultType = BinaryRetrievalResultType.Complete
            };
            SUT.SelectBinaryRetrievalAction(scanner);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();
            semaphoreLockManager.ConnectorConfiguration = connector;
            await semaphoreLockManager.SetSemaphoreAsync(SemaphoreLockType.Scoped, SubmitBinaryOperation.WORK_TYPE, 10, cancellationToken);

            var workMessage = SUT.CreateSubmitBinaryManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var priorWorkItem = Services.GetRequiredService<SubmitBinaryOperation>();
            await priorWorkItem.RunWorkRequestAsync(SUT.CreateSubmitBinaryRequest(workMessage), cancellationToken);

            await SUT.SetWorkContinue(workMessage);
            var afterWorkItem = Services.GetRequiredService<SubmitBinaryOperation>();
            await afterWorkItem.RunWorkRequestAsync(SUT.CreateSubmitBinaryRequest(workMessage), cancellationToken);

            // Check results
            Assert.Equal(WorkResultType.Deferred, priorWorkItem.ResultType);
            Assert.Equal(WorkResultType.Deferred, afterWorkItem.ResultType);
        }

        [Fact]
        public async Task IfScopedSemaphoreLockActive_IsDeferred()
        {
            var cancellationToken = CancellationToken.None;

            var scanner = new BinaryRetrievalAction
            {
                BinaryRetrievalResultType = BinaryRetrievalResultType.Complete
            };
            SUT.SelectBinaryRetrievalAction(scanner);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();
            semaphoreLockManager.ConnectorConfiguration = connector;
            await semaphoreLockManager.SetSemaphoreAsync(SemaphoreLockType.Scoped, SubmitBinaryOperation.WORK_TYPE, 10, cancellationToken);

            var workMessage = SUT.CreateSubmitBinaryManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var priorWorkItem = Services.GetRequiredService<SubmitBinaryOperation>();
            await priorWorkItem.RunWorkRequestAsync(SUT.CreateSubmitBinaryRequest(workMessage), cancellationToken);

            // Check results
            Assert.Equal(WorkResultType.Deferred, priorWorkItem.ResultType);
        }

        [Fact]
        public async Task IfDifferentScopedSemaphoreLockActive_IsNotDeferred()
        {
            var cancellationToken = CancellationToken.None;

            var scanner = new BinaryRetrievalAction
            {
                BinaryRetrievalResultType = BinaryRetrievalResultType.Complete
            };
            SUT.SelectBinaryRetrievalAction(scanner);
            SUT.SelectBinarySubmissionCallbackActionMock(new Mock<IBinarySubmissionCallbackAction>());

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();
            semaphoreLockManager.ConnectorConfiguration = connector;
            await semaphoreLockManager.SetSemaphoreAsync(SemaphoreLockType.Scoped, SubmitBinaryOperation.WORK_TYPE, 10, cancellationToken);

            SUT.SemaphoreLockScopedKeyAction.Key = "KEY_456";

            var workMessage = SUT.CreateSubmitBinaryManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var priorWorkItem = Services.GetRequiredService<SubmitBinaryOperation>();
            await priorWorkItem.RunWorkRequestAsync(SUT.CreateSubmitBinaryRequest(workMessage), cancellationToken);

            // Check results
            Assert.Equal(WorkResultType.Complete, priorWorkItem.ResultType);
        }

    }

}
