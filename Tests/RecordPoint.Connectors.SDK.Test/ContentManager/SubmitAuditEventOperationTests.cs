using Microsoft.Extensions.DependencyInjection;
using RecordPoint.Connectors.SDK.Caching.Semaphore;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Work;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.ContentManager
{
    public class SubmitAuditEventOperationTests : CommonTestBase<SubmitAuditEventOperationSut>
    {

        [Fact]
        public async Task IfRequestedConnectorMissing_CompletesWithNoUpdates()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            var workMessage = SUT.CreateSubmitAuditEventManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var submitBinaryItem = Services.GetRequiredService<SubmitAuditEventOperation>();
            await submitBinaryItem.RunWorkRequestAsync(SUT.CreateSubmitAuditEventRequest(workMessage), cancellationToken);

            // Assert
            Assert.Equal(WorkResultType.Complete, submitBinaryItem.ResultType);
            Assert.Equal("Connector not found", submitBinaryItem.ResultReason);
        }

        [Fact]
        public async Task IfConnectorDisabled_CompletesWithNoUpdates()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            connector.Status = "Disabled";
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateSubmitAuditEventManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var submitBinaryItem = Services.GetRequiredService<SubmitAuditEventOperation>();
            await submitBinaryItem.RunWorkRequestAsync(SUT.CreateSubmitAuditEventRequest(workMessage), cancellationToken);

            // Assert
            Assert.Equal(WorkResultType.Complete, submitBinaryItem.ResultType);
            Assert.Equal(DatabaseConnectorConfigurationManager.CONNECTOR_DISABLED_REASON, submitBinaryItem.ResultReason);
        }

        [Fact]
        public async Task SubmitAuditEventOperation_DisposalFailedException_ThrowsInvalidOperationException()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateSubmitAuditEventManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            var submitBinaryItem = Services.GetRequiredService<SubmitAuditEventOperation>();
            try
            {
                await submitBinaryItem.RunWorkRequestAsync(SUT.CreateSubmitAuditEventRequest(workMessage), cancellationToken);
            }
            catch (Exception ex)
            {
                // Assert
                Assert.IsType<InvalidOperationException>(ex);
                Assert.Equal("Unknown Exception", ex.Message);
            }
        }

    }

}
