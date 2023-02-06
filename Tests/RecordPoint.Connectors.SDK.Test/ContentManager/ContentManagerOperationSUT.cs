using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Test.Mock.Databases;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.Test.ContentManager
{
    public class ContentManagerOperationSut : ContentManagerSutBase
    {

        protected override IHostBuilder CreateSutBuilder()
        {
            return base
                .CreateSutBuilder()
                .UseDatabaseConnectorConfigurationManager()
                .UseMockConnectorDatabase()
                .UseDatabaseChannelManager()
                .ConfigureServices(svcs => svcs.AddTransient<ContentManagerOperation>());
        }

        #region Content Manager Work Request

        public string ContentManagerOpterationWorkId1 { get; set; } = "ContentManagerWorkId1";

        public DateTime ContentManagerSubmitTime1 { get; set; } = new DateTime(2000, 1, 1, 0, 0, 0);

        public static ContentManagerState CreateContentManagerSyncState() => new();

        public static ContentManagerConfiguration CreateContentManagerConfiguration() => new();

        public WorkRequest CreateContentManagerRequest(ManagedWorkStatusModel workMessage) => new()
        {
            WorkId = workMessage.WorkId,
            ConnectorConfigId = CONNECTOR_CONFIGURATION_ID_1,
            WorkType = ContentManagerOperation.WORK_TYPE,
            SubmitDateTime = ContentManagerSubmitTime1,
            Body = workMessage.Serialize()
        };

        public ManagedWorkStatusModel CreateContentManagerManagedWorkStatusModel()
        {
            var state = CreateContentManagerSyncState();
            var config = CreateContentManagerConfiguration();
            var message = CreateContentManagerManagedWorkStatusModel(config, state);
            return message;
        }

        public ManagedWorkStatusModel CreateContentManagerManagedWorkStatusModel(ContentManagerConfiguration configuration, ContentManagerState state) => new()
        {
            WorkId = ContentManagerOpterationWorkId1,
            WorkType = ContentManagerOperation.WORK_TYPE,
            Configuration = configuration.Serialize(),
            ConfigurationType = ContentManagerConfiguration.ConfigurationType,
            State = state.Serialize(),
            StateType = ContentManagerState.LatestStateType,
            Id = ContentManagerOpterationWorkId1
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
