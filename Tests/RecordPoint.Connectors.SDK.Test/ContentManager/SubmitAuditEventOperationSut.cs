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
    public class SubmitAuditEventOperationSut : ContentManagerSutBase
    {
        protected override IHostBuilder CreateSutBuilder()
        {
            return base
                .CreateSutBuilder()
                .UseDatabaseConnectorConfigurationManager()
                .UseMockConnectorDatabase()
                .UseDatabaseChannelManager()
                .UseMockR365Client()
                .ConfigureServices(svcs => svcs.AddTransient<SubmitAuditEventOperation>());
        }

        #region Submit Audit Event Work Request

        public string SubmitAuditEventOperationWorkId1 { get; set; } = "SubmitAuditEventWorkId1";

        public DateTime SubmitAuditEventSubmitTime1 { get; set; } = new DateTime(2000, 1, 1, 0, 0, 0);

        public ManagedWorkStatusModel CreateSubmitAuditEventManagedWorkStatusModel(string connectorConfigurationId) => new()
        {
            WorkId = SubmitAuditEventOperationWorkId1,
            WorkType = SubmitAuditEventOperation.WORK_TYPE,
            ConnectorId = connectorConfigurationId,
            Id = SubmitAuditEventOperationWorkId1
        };

        public ManagedWorkStatusModel CreateSubmitAuditEventManagedWorkStatusModel(ConnectorConfigModel connector) => CreateSubmitAuditEventManagedWorkStatusModel(connector.Id);

        public WorkRequest CreateSubmitAuditEventRequest(ManagedWorkStatusModel workMessage) => new()
        {
            WorkId = workMessage.WorkId,
            ConnectorConfigId = CONNECTOR_CONFIGURATION_ID_1,
            WorkType = SubmitAuditEventOperation.WORK_TYPE,
            SubmitDateTime = SubmitAuditEventSubmitTime1,
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
