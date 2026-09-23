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
    public class SubmitRecordOperationSut : ContentManagerSutBase
    {
        protected override IHostBuilder CreateSutBuilder()
        {
            return base
                .CreateSutBuilder()
                .UseDatabaseConnectorConfigurationManager()
                .UseMockConnectorDatabase()
                .UseDatabaseChannelManager()
                .UseMockR365Client()
                .ConfigureServices(svcs => svcs.AddTransient<SubmitRecordOperation>());
        }

        public string SubmitRecordOperationWorkId1 { get; set; } = "SubmitRecordWorkId1";

        public DateTime SubmitRecordSubmitTime1 { get; set; } = new DateTime(2000, 1, 1, 0, 0, 0);

        public ManagedWorkStatusModel CreateSubmitRecordManagedWorkStatusModel(string connectorConfigurationId) => new()
        {
            WorkId = SubmitRecordOperationWorkId1,
            WorkType = SubmitRecordOperation.WORK_TYPE,
            ConnectorId = connectorConfigurationId,
            Id = SubmitRecordOperationWorkId1
        };

        public ManagedWorkStatusModel CreateSubmitRecordManagedWorkStatusModel(ConnectorConfigModel connector) => CreateSubmitRecordManagedWorkStatusModel(connector.Id);

        public WorkRequest CreateSubmitRecordRequest(ManagedWorkStatusModel workMessage) => new()
        {
            WorkId = workMessage.WorkId,
            ConnectorConfigId = CONNECTOR_CONFIGURATION_ID_1,
            WorkType = SubmitRecordOperation.WORK_TYPE,
            SubmitDateTime = SubmitRecordSubmitTime1,
            Body = workMessage.Serialize()
        };
    }
}
