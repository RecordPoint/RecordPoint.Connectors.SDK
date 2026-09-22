using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Test.Mock.Databases;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.Test.ContentManager;

public class ChannelDiscoveryOperationSut : ContentManagerSutBase
{
    protected override IHostBuilder CreateSutBuilder()
    {
        return base
            .CreateSutBuilder()
            .UseDatabaseConnectorConfigurationManager()
            .UseMockConnectorDatabase()
            .UseDatabaseChannelManager()
            .ConfigureServices(svcs => svcs.AddScoped<ChannelDiscoveryOperation>());
    }

    #region Channel Discovery Work Request

    public string ChannelDiscoveryOperationWorkId1 { get; set; } = "ChannelDiscoveryWorkId1";

    public DateTime ChannelDiscoverySubmitTime1 { get; set; } = new DateTime(2000, 1, 1, 0, 0, 0);

    public static ChannelDiscoveryState CreateChannelDiscoverySyncState(string cursor = null) => new()
    {
        Cursor = cursor
    };

    public static ChannelDiscoveryConfiguration CreateChannelDiscoveryConfiguration() => new();

    public WorkRequest CreateChannelDiscoveryRequest(ManagedWorkStatusModel workMessage) => new()
    {
        WorkId = workMessage.WorkId,
        ConnectorConfigId = CONNECTOR_CONFIGURATION_ID_1,
        WorkType = ChannelDiscoveryOperation.WORK_TYPE,
        SubmitDateTime = ChannelDiscoverySubmitTime1,
        Body = workMessage.Serialize()
    };

    public ManagedWorkStatusModel CreateChannelDiscoveryManagedWorkStatusModel(ConnectorConfigModel connector, string cursor = null)
    {
        var state = CreateChannelDiscoverySyncState(cursor);
        var config = CreateChannelDiscoveryConfiguration();
        var message = CreateChannelDiscoveryManagedWorkStatusModel(connector, config, state);
        return message;
    }

    public ManagedWorkStatusModel CreateChannelDiscoveryManagedWorkStatusModel(ConnectorConfigModel connector, ChannelDiscoveryConfiguration configuration, ChannelDiscoveryState state) => new()
    {
        WorkId = ChannelDiscoveryOperationWorkId1,
        WorkType = ChannelDiscoveryOperation.WORK_TYPE,
        Configuration = configuration.Serialize(),
        ConfigurationType = ChannelDiscoveryConfiguration.ConfigurationType,
        State = state.Serialize(),
        StateType = ChannelDiscoveryState.StateType,
        ConnectorId = connector.Id,
        Id = ChannelDiscoveryOperationWorkId1
    };
    #endregion

}
