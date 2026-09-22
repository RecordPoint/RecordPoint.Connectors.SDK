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
    public class SubmitAggregationOperationSut : ContentManagerSutBase
    {
        protected override IHostBuilder CreateSutBuilder()
        {
            return base
                .CreateSutBuilder()
                .UseDatabaseConnectorConfigurationManager()
                .UseMockConnectorDatabase()
                .UseDatabaseChannelManager()
                .UseMockR365Client()
                .ConfigureServices(svcs => svcs.AddTransient<SubmitAggregationOperation>());
        }

        public string SubmitAggregationOperationWorkId1 { get; set; } = "SubmitAggregationWorkId1";

        public DateTime SubmitAggregationSubmitTime1 { get; set; } = new DateTime(2000, 1, 1, 0, 0, 0);

        public ManagedWorkStatusModel CreateSubmitAggregationManagedWorkStatusModel(string connectorConfigurationId) => new()
        {
            WorkId = SubmitAggregationOperationWorkId1,
            WorkType = SubmitAggregationOperation.WORK_TYPE,
            ConnectorId = connectorConfigurationId,
            Id = SubmitAggregationOperationWorkId1
        };

        public ManagedWorkStatusModel CreateSubmitAggregationManagedWorkStatusModel(ConnectorConfigModel connector) => CreateSubmitAggregationManagedWorkStatusModel(connector.Id);

        public WorkRequest CreateSubmitAggregationRequest(ManagedWorkStatusModel workMessage) => new()
        {
            WorkId = workMessage.WorkId,
            ConnectorConfigId = CONNECTOR_CONFIGURATION_ID_1,
            WorkType = SubmitAggregationOperation.WORK_TYPE,
            SubmitDateTime = SubmitAggregationSubmitTime1,
            Body = workMessage.Serialize()
        };
    }
}
