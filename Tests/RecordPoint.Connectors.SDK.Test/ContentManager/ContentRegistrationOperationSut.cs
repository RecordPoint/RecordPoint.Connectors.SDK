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
    public class ContentRegistrationOperationSut : ContentManagerSutBase
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
                        .Configure<ContentRegistrationOperationOptions>(context.Configuration.GetSection(ContentRegistrationOperationOptions.SECTION_NAME))
                        .Configure<ContentManagerOptions>(context.Configuration.GetSection(ContentManagerOptions.SECTION_NAME))
                        .Configure<RecordSubmissionOptions>(context.Configuration.GetSection(RecordSubmissionOptions.SECTION_NAME))
                        .AddScoped<ContentRegistrationOperation>();
                 });
        }

        #region Content Registration Work Request

        public string ContentRegistrationOpterationWorkId1 { get; set; } = "ContentRegistrationWorkId1";

        public DateTime ContentRegistrationSubmitTime1 { get; set; } = new DateTime(2000, 1, 1, 0, 0, 0);

        public static ContentRegistrationState CreateContentRegistrationSyncState() => new();

        public static ContentRegistrationConfiguration CreateContentRegistrationConfiguration(ChannelModel channel) => new()
        {
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
            var config = CreateContentRegistrationConfiguration(channel);
            var message = CreateContentRegistrationManagedWorkStatusModel(connector, config, state);
            return message;
        }

        public ManagedWorkStatusModel CreateContentRegistrationManagedWorkStatusModel(ConnectorConfigModel connector, ContentRegistrationConfiguration configuration, ContentRegistrationState state) => new()
        {
            WorkId = ContentRegistrationOpterationWorkId1,
            WorkType = ContentRegistrationOperation.WORK_TYPE,
            Configuration = configuration.Serialize(),
            ConfigurationType = ContentRegistrationConfiguration.ConfigurationType,
            State = state.Serialize(),
            StateType = ContentRegistrationState.StateType,
            ConnectorId = connector.Id,
            Id = ContentRegistrationOpterationWorkId1
        };
        #endregion


    }


}
