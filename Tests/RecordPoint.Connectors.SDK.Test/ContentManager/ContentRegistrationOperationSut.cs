using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Test.Mock.Databases;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Work;
using RecordPoint.Connectors.SDK.Content;

namespace RecordPoint.Connectors.SDK.Test.ContentManager
{
    public class ContentRegistrationOperationSut : ContentManagerSutBase
    {

        protected override IHostBuilder CreateSutBuilder()
        {
            return base
                .CreateSutBuilder()
                .UseDatabaseConnectorConfigurationManager()
                .UseMockConnectorDatabase()
                .UseDatabaseChannelManager()
                .ConfigureServices(svcs => svcs.AddTransient<ContentRegistrationOperation>());
        }

        #region Content Registration Work Request

        public string ContentRegistrationOpterationWorkId1 { get; set; } = "ContentRegistrationWorkId1";

        public DateTime ContentRegistrationSubmitTime1 { get; set; } = new DateTime(2000, 1, 1, 0, 0, 0);

        public static ContentRegistrationState CreateContentRegistrationSyncState() => new();

        public static ContentRegistrationConfiguration CreateContentRegistrationConfiguration(ConnectorConfigModel connectorConfig, ChannelModel channel) => new()
        {
            ConnectorConfigurationId = connectorConfig.Id,
            TenantId = connectorConfig.TenantId,
            TenantDomainName = connectorConfig.TenantDomainName,
            ChannelExternalId = channel.ExternalId,
            ChannelTitle = channel.Title
        };

        public WorkRequest CreateContentRegistrationRequest(ManagedWorkStatusModel workMessage) => new()
        {
            WorkId = workMessage.WorkId,
            ConnectorConfigId = CONNECTOR_CONFIGURATION_ID_1,
            WorkType = ContentRegistrationOperation.WORK_TYPE,
            SubmitDateTime = ContentRegistrationSubmitTime1,
            Body = workMessage.Serialize()
        };

        public ManagedWorkStatusModel CreateContentRegistrationManagedWorkStatusModel(ConnectorConfigModel connector, ChannelModel channel)
        {
            var state = CreateContentRegistrationSyncState();
            var config = CreateContentRegistrationConfiguration(connector, channel);
            var message = CreateContentRegistrationManagedWorkStatusModel(config, state);
            return message;
        }

        public ManagedWorkStatusModel CreateContentRegistrationManagedWorkStatusModel(ContentRegistrationConfiguration configuration, ContentRegistrationState state) => new()
        {
            WorkId = ContentRegistrationOpterationWorkId1,
            WorkType = ContentRegistrationOperation.WORK_TYPE,
            Configuration = configuration.Serialize(),
            ConfigurationType = ContentRegistrationConfiguration.ConfigurationType,
            State = state.Serialize(),
            StateType = ContentRegistrationState.LatestStateType,
            ConnectorId = configuration.ConnectorConfigurationId,
            Id = ContentRegistrationOpterationWorkId1
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
