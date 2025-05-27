using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Test.Mock.Databases;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.Test.ContentManager
{
    public class ContentSynchronisationOperationSut : ContentManagerSutBase
    {

        protected override IHostBuilder CreateSutBuilder()
        {
            return base
                .CreateSutBuilder()
                .UseDatabaseConnectorConfigurationManager()
                .UseMockConnectorDatabase()
                .UseDatabaseChannelManager()
                .ConfigureServices((context, svcs) => {
                    var contentManagerConfiguration = context.Configuration.GetSection("ContentManager");
                    svcs
                        .Configure<ContentManagerOptions>(contentManagerConfiguration)
                        .AddScoped<ContentSynchronisationOperation>();
                });
        }

        #region Content Synchronisation Work Request

        public string ContentSynchronisationOpterationWorkId1 { get; set; } = "ContentSynchronisationWorkId1";

        public DateTime ContentSynchronisationSubmitTime1 { get; set; } = new DateTime(2000, 1, 1, 0, 0, 0);

        public static ContentSynchronisationState CreateContentSynchronisationSyncState() => new();

        public static ContentSynchronisationConfiguration CreateContentSynchronisationConfiguration(ConnectorConfigModel connectorConfig, ChannelModel channel) => new()
        {
            ConnectorConfigurationId = connectorConfig.Id,
            TenantId = connectorConfig.TenantId,
            TenantDomainName = connectorConfig.TenantDomainName,
            ChannelExternalId = channel.ExternalId,
            ChannelTitle = channel.Title
        };

        public WorkRequest CreateContentSynchronisationRequest(ManagedWorkStatusModel workMessage) => new()
        {
            WorkId = workMessage.WorkId,
            ConnectorConfigId = CONNECTOR_CONFIGURATION_ID_1,
            WorkType = ContentSynchronisationOperation.WORK_TYPE,
            SubmitDateTime = ContentSynchronisationSubmitTime1,
            Body = workMessage.Serialize(),
        };

        public ManagedWorkStatusModel CreateContentSynchronisationManagedWorkStatusModel(ConnectorConfigModel connector, ChannelModel channel)
        {
            var state = CreateContentSynchronisationSyncState();
            var config = CreateContentSynchronisationConfiguration(connector, channel);
            var message = CreateContentSynchronisationManagedWorkStatusModel(config, state);
            return message;
        }

        public ManagedWorkStatusModel CreateContentSynchronisationManagedWorkStatusModel(ContentSynchronisationConfiguration configuration, ContentSynchronisationState state) => new()
        {
            WorkId = ContentSynchronisationOpterationWorkId1,
            WorkType = ContentSynchronisationOperation.WORK_TYPE,
            Configuration = configuration.Serialize(),
            ConfigurationType = ContentSynchronisationConfiguration.ConfigurationType,
            State = state.Serialize(),
            StateType = ContentSynchronisationState.LatestStateType,
            ConnectorId = configuration.ConnectorConfigurationId,
            Id = ContentSynchronisationOpterationWorkId1
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
