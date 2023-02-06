using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Work;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Test.Mock.Databases;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.Test.Mock.R365;

namespace RecordPoint.Connectors.SDK.Test.ContentManager
{
    public class SubmitBinaryOperationSut : ContentManagerSutBase
    {
        protected override IHostBuilder CreateSutBuilder()
        {
            return base
                .CreateSutBuilder()
                .UseDatabaseConnectorConfigurationManager()
                .UseMockConnectorDatabase()
                .UseDatabaseChannelManager()
                .UseMockR365Client()
                .ConfigureServices(svcs => svcs.AddTransient<SubmitBinaryOperation>());
        }

        #region Binary Submission Work Request

        public string SubmitBinaryOperationWorkId1 { get; set; } = "SubmitBinaryWorkId1";

        public DateTime SubmitBinarySubmitTime1 { get; set; } = new DateTime(2000, 1, 1, 0, 0, 0);

        public ManagedWorkStatusModel CreateSubmitBinaryManagedWorkStatusModel(string connectorConfigurationId) => new()
        {
            WorkId = SubmitBinaryOperationWorkId1,
            WorkType = ChannelDiscoveryOperation.WORK_TYPE,
            ConnectorId = connectorConfigurationId,
            Id = SubmitBinaryOperationWorkId1
        };

        public WorkRequest CreateSubmitBinaryRequest(ManagedWorkStatusModel workMessage) => new()
        {
            WorkId = workMessage.WorkId,
            ConnectorConfigId = CONNECTOR_CONFIGURATION_ID_1,
            WorkType = SubmitBinaryOperation.WORK_TYPE,
            SubmitDateTime = SubmitBinarySubmitTime1,
            Body = workMessage.Serialize()
        };

        public ManagedWorkStatusModel CreateSubmitBinaryManagedWorkStatusModel(ConnectorConfigModel connector) => CreateSubmitBinaryManagedWorkStatusModel(connector.Id);

        public async Task SetWorkRunning(ManagedWorkStatusModel workMessage)
        {
            await Services.GetRequiredService<IManagedWorkStatusManager>()
                .AddWorkStatusAsync(workMessage, CancellationToken.None);
        }

        public async Task SetWorkContinue(ManagedWorkStatusModel workMessage)
        {
            await Services.GetRequiredService<IManagedWorkStatusManager>()
                .SetWorkContinueAsync(workMessage.WorkId, workMessage.WorkId, string.Empty, CancellationToken.None);
        }
        #endregion

    }
}
