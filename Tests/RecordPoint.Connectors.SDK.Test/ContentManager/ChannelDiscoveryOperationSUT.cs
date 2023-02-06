using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Work;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Test.Mock.Databases;

namespace RecordPoint.Connectors.SDK.Test.ContentManager
{

    public class ChannelDiscoveryOperationSut : ContentManagerSutBase
    {
        protected override IHostBuilder CreateSutBuilder()
        {
            return base
                .CreateSutBuilder()
                .UseDatabaseConnectorConfigurationManager()
                .UseMockConnectorDatabase()
                .UseDatabaseChannelManager()
                .ConfigureServices(svcs => svcs.AddTransient<ChannelDiscoveryOperation>());
        }

        #region Channel Discovery Work Request

        public string ChannelDiscoveryOpterationWorkId1 { get; set; } = "ChannelDiscoveryWorkId1";

        public DateTime ChannelDiscoverySubmitTime1 { get; set; } = new DateTime(2000, 1, 1, 0, 0, 0);

        public static ChannelDiscoveryState CreateChannelDiscoverySyncState() => new();

        public static ChannelDiscoveryConfiguration CreateChannelDiscoveryConfiguration(ConnectorConfigModel connectorConfig) => new()
        {
            ConnectorConfigurationId = connectorConfig.Id,
            TenantId = connectorConfig.TenantId,
            TenantDomainName = connectorConfig.TenantDomainName
        };

        public WorkRequest CreateChannelDiscoveryRequest(ManagedWorkStatusModel workMessage) => new()
        {
            WorkId = workMessage.WorkId,
            ConnectorConfigId = CONNECTOR_CONFIGURATION_ID_1,
            WorkType = ChannelDiscoveryOperation.WORK_TYPE,
            SubmitDateTime = ChannelDiscoverySubmitTime1,
            Body = workMessage.Serialize()
        };

        public ManagedWorkStatusModel CreateChannelDiscoveryManagedWorkStatusModel(ConnectorConfigModel connector)
        {
            var state = CreateChannelDiscoverySyncState();
            var config = CreateChannelDiscoveryConfiguration(connector);
            var message = CreateChannelDiscoveryManagedWorkStatusModel(config, state);
            return message;
        }

        public ManagedWorkStatusModel CreateChannelDiscoveryManagedWorkStatusModel(ChannelDiscoveryConfiguration configuration, ChannelDiscoveryState state) => new()
        {
            WorkId = ChannelDiscoveryOpterationWorkId1,
            WorkType = ChannelDiscoveryOperation.WORK_TYPE,
            Configuration = configuration.Serialize(),
            ConfigurationType = ChannelDiscoveryConfiguration.ConfigurationType,
            State = state.Serialize(),
            StateType = ChannelDiscoveryState.LatestStateType,
            ConnectorId = configuration.ConnectorConfigurationId,
            Id = ChannelDiscoveryOpterationWorkId1
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
