using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Test.Mock.Databases;
using RecordPoint.Connectors.SDK.Test.Mock.R365;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.Test.ContentManager
{
    public class RecordDisposalOperationSut : ContentManagerSutBase
    {
        protected override IHostBuilder CreateSutBuilder()
        {
            return base
                .CreateSutBuilder()
                .UseDatabaseConnectorConfigurationManager()
                .UseMockConnectorDatabase()
                .UseDatabaseChannelManager()
                .UseMockR365Client()
                .ConfigureServices(svcs => svcs.AddTransient<RecordDisposalOperation>());
        }

        #region Record Disposal Work Request

        public string RecordDisposalOperationWorkId1 { get; set; } = "RecordDisposalWorkId1";

        public DateTime RecordDisposalSubmitTime1 { get; set; } = new DateTime(2000, 1, 1, 0, 0, 0);

        public ManagedWorkStatusModel CreateRecordDisposalManagedWorkStatusModel(string connectorConfigurationId) => new()
        {
            WorkId = RecordDisposalOperationWorkId1,
            WorkType = RecordDisposalOperation.WORK_TYPE,
            ConnectorId = connectorConfigurationId,
            Id = RecordDisposalOperationWorkId1
        };

        public ManagedWorkStatusModel CreateRecordDisposalManagedWorkStatusModel(ConnectorConfigModel connector) => CreateRecordDisposalManagedWorkStatusModel(connector.Id);

        public WorkRequest CreateRecordDisposalRequest(ManagedWorkStatusModel workMessage) => new()
        {
            WorkId = workMessage.WorkId,
            ConnectorConfigId = CONNECTOR_CONFIGURATION_ID_1,
            WorkType = RecordDisposalOperation.WORK_TYPE,
            SubmitDateTime = RecordDisposalSubmitTime1,
            Body = workMessage.Serialize()
        };

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
