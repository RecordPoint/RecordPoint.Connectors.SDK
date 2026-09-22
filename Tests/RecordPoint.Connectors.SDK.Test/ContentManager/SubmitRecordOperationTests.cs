using Microsoft.Extensions.DependencyInjection;
using Moq;
using RecordPoint.Connectors.SDK.Abstractions.ContentManager;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Work;
using Xunit;
using Record = RecordPoint.Connectors.SDK.Content.Record;

namespace RecordPoint.Connectors.SDK.Test.ContentManager
{
    public class SubmitRecordOperationTests : CommonTestBase<SubmitRecordOperationSut>
    {
        [Fact]
        public async Task IfRequestedConnectorMissing_CompletesWithNoUpdates()
        {
            var cancellationToken = CancellationToken.None;
            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            var workMessage = SUT.CreateSubmitRecordManagedWorkStatusModel(connector);

            var operation = Services.GetRequiredService<SubmitRecordOperation>();
            await operation.RunWorkRequestAsync(SUT.CreateSubmitRecordRequest(workMessage), cancellationToken);

            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            Assert.Equal("Connector not found", operation.ResultReason);
        }

        [Fact]
        public async Task IfConnectorDisabled_CompletesWithNoUpdates()
        {
            var cancellationToken = CancellationToken.None;
            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            ContentManagerSutBase.DisableConnector(connector, DateTimeOffset.Now);
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateSubmitRecordManagedWorkStatusModel(connector);

            var operation = Services.GetRequiredService<SubmitRecordOperation>();
            await operation.RunWorkRequestAsync(SUT.CreateSubmitRecordRequest(workMessage), cancellationToken);

            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            Assert.Equal(DatabaseConnectorConfigurationManager.CONNECTOR_DISABLED_REASON, operation.ResultReason);
        }

        [Fact]
        public async Task IfConnectorEnabled_SubmitsRecordAndCompletes()
        {
            var cancellationToken = CancellationToken.None;

            // Register a record submission callback so the post-submit path runs.
            var callback = new Mock<IRecordSubmissionCallbackAction>();
            callback
                .Setup(x => x.ExecuteAsync(It.IsAny<Client.Models.ConnectorConfigModel>(), It.IsAny<Record>(), It.IsAny<SubmissionActionType>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            SUT.SelectRecordSubmissionCallbackActionMock(callback);
            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateSubmitRecordManagedWorkStatusModel(connector);

            var operation = Services.GetRequiredService<SubmitRecordOperation>();
            await operation.RunWorkRequestAsync(SUT.CreateSubmitRecordRequest(workMessage), cancellationToken);

            Assert.True(operation.HasResult);
            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            Assert.NotNull(operation.ConnectorConfig);
        }
    }
}
