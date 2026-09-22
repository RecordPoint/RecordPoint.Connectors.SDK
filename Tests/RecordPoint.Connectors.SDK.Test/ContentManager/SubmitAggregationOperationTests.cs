using Microsoft.Extensions.DependencyInjection;
using Moq;
using RecordPoint.Connectors.SDK.Abstractions.ContentManager;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Work;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.ContentManager
{
    public class SubmitAggregationOperationTests : CommonTestBase<SubmitAggregationOperationSut>
    {
        [Fact]
        public async Task IfRequestedConnectorMissing_CompletesWithNoUpdates()
        {
            var cancellationToken = CancellationToken.None;
            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            var workMessage = SUT.CreateSubmitAggregationManagedWorkStatusModel(connector);

            var operation = Services.GetRequiredService<SubmitAggregationOperation>();
            await operation.RunWorkRequestAsync(SUT.CreateSubmitAggregationRequest(workMessage), cancellationToken);

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

            var workMessage = SUT.CreateSubmitAggregationManagedWorkStatusModel(connector);

            var operation = Services.GetRequiredService<SubmitAggregationOperation>();
            await operation.RunWorkRequestAsync(SUT.CreateSubmitAggregationRequest(workMessage), cancellationToken);

            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            Assert.Equal(DatabaseConnectorConfigurationManager.CONNECTOR_DISABLED_REASON, operation.ResultReason);
        }

        [Fact]
        public async Task IfConnectorEnabled_SubmitsAggregationAndCompletes()
        {
            var cancellationToken = CancellationToken.None;

            var callback = new Mock<IAggregationSubmissionCallbackAction>();
            callback
                .Setup(x => x.ExecuteAsync(It.IsAny<Client.Models.ConnectorConfigModel>(), It.IsAny<Aggregation>(), It.IsAny<SubmissionActionType>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            SUT.SelectAggregationSubmissionCallbackActionMock(callback);
            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateSubmitAggregationManagedWorkStatusModel(connector);

            var operation = Services.GetRequiredService<SubmitAggregationOperation>();
            await operation.RunWorkRequestAsync(SUT.CreateSubmitAggregationRequest(workMessage), cancellationToken);

            Assert.True(operation.HasResult);
            Assert.Equal(WorkResultType.Complete, operation.ResultType);
        }
    }
}
