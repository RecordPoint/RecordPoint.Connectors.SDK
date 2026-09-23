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
                    svcs
                        .Configure<ContentSynchronisationOperationOptions>(context.Configuration.GetSection(ContentSynchronisationOperationOptions.SECTION_NAME))
                        .Configure<ContentManagerOptions>(context.Configuration.GetSection(ContentManagerOptions.SECTION_NAME))
                        .Configure<RecordSubmissionOptions>(context.Configuration.GetSection(RecordSubmissionOptions.SECTION_NAME))
                        .AddScoped<ContentSynchronisationOperation>();
                });
        }

        #region Content Synchronisation Work Request

        public string ContentSynchronisationOpterationWorkId1 { get; set; } = "ContentSynchronisationWorkId1";

        public DateTime ContentSynchronisationSubmitTime1 { get; set; } = new DateTime(2000, 1, 1, 0, 0, 0);

        public static ContentSynchronisationState CreateContentSynchronisationSyncState() => new();

        public static ContentSynchronisationConfiguration CreateContentSynchronisationConfiguration(ChannelModel channel) => new()
        {
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
            var config = CreateContentSynchronisationConfiguration(channel);
            var message = CreateContentSynchronisationManagedWorkStatusModel(connector, config, state);
            return message;
        }

        public ManagedWorkStatusModel CreateContentSynchronisationManagedWorkStatusModel(ConnectorConfigModel connector, ContentSynchronisationConfiguration configuration, ContentSynchronisationState state) => new()
        {
            WorkId = ContentSynchronisationOpterationWorkId1,
            WorkType = ContentSynchronisationOperation.WORK_TYPE,
            Configuration = configuration.Serialize(),
            ConfigurationType = ContentSynchronisationConfiguration.ConfigurationType,
            State = state.Serialize(),
            StateType = ContentSynchronisationState.StateType,
            ConnectorId = connector.Id,
            Id = ContentSynchronisationOpterationWorkId1
        };
        #endregion

    }

}
