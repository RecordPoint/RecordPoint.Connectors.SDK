using Microsoft.Extensions.DependencyInjection;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Work;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.ContentManager
{
    public class ContentManagerOperationTests : CommonTestBase<ContentManagerOperationSut>
    {

        [Fact]
        public async Task NoWorksIfNoConnectorsExist()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();

            var workMessage = SUT.CreateContentManagerManagedWorkStatusModel();
            await SUT.SetWorkRunning(workMessage);

            var workItem = Services.GetRequiredService<ContentManagerOperation>();
            await workItem.RunWorkRequestAsync(SUT.CreateContentManagerRequest(workMessage), cancellationToken);

            var managedWorkStatuses = await SUT.GetWorkStatusManager()
                .GetWorkStatusesAsync(a => a.WorkType != ContentManagerOperation.WORK_TYPE, cancellationToken);

            Assert.Equal(WorkResultType.Complete, workItem.ResultType);
            Assert.Empty(managedWorkStatuses);
        }

        [Fact]
        public async Task WorkAddedIfConnectorExists()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateContentManagerManagedWorkStatusModel();
            await SUT.SetWorkRunning(workMessage);

            var workItem = Services.GetRequiredService<ContentManagerOperation>();
            await workItem.RunWorkRequestAsync(SUT.CreateContentManagerRequest(workMessage), cancellationToken);

            var managedWorkStatuses = await SUT.GetWorkStatusManager()
                .GetWorkStatusesAsync(a => a.WorkType != ContentManagerOperation.WORK_TYPE, cancellationToken);

            Assert.Equal(WorkResultType.Complete, workItem.ResultType);
            Assert.Single(managedWorkStatuses);
            var work = managedWorkStatuses.Single();

            Assert.Equal(ManagedWorkStatuses.Running, work.Status);
            Assert.Equal(ChannelDiscoveryOperation.WORK_TYPE, work.WorkType);
            Assert.Equal(ChannelDiscoveryConfiguration.ConfigurationType, work.ConfigurationType);
            Assert.Equal(connector.Id, work.ConnectorId);

            var config = work.DeserialiseChannelDiscoveryConfiguration();
            config.ConnectorConfigurationId = work.ConnectorId;
        }

        [Fact]
        public async Task NoChangeIfWorkAlreadyAdded()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateContentManagerManagedWorkStatusModel();
            await SUT.SetWorkRunning(workMessage);

            var priorWorkItem = Services.GetRequiredService<ContentManagerOperation>();
            await priorWorkItem.RunWorkRequestAsync(SUT.CreateContentManagerRequest(workMessage), cancellationToken);

            var priorWorks = await SUT.GetWorkStatusManager()
                .GetWorkStatusesAsync(a => a.WorkType == ContentManagerOperation.WORK_TYPE, cancellationToken);
            var priorWork = priorWorks.Single();

            await SUT.SetWorkContinue(workMessage);

            var afterWorkItem = Services.GetRequiredService<ContentManagerOperation>();
            await afterWorkItem.RunWorkRequestAsync(SUT.CreateContentManagerRequest(workMessage), cancellationToken);

            Assert.Equal(WorkResultType.Complete, afterWorkItem.ResultType);

            var afterWorks = await SUT.GetWorkStatusManager()
                .GetWorkStatusesAsync(a => a.WorkType == ContentManagerOperation.WORK_TYPE, cancellationToken);

            Assert.Single(afterWorks);
            var afterWork = afterWorks.Single();

            // Should be same work instance
            Assert.Equal(priorWork.Id, afterWork.Id);

            Assert.Equal(ContentManagerOperation.WORK_TYPE, afterWork.WorkType);
            Assert.Equal(ContentManagerConfiguration.ConfigurationType, afterWork.ConfigurationType);
        }


        [Fact]
        public async Task ChannelRemovedIfConnectorDoesNotExist()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();

            var channel = ContentManagerSutBase.CreateChannel1();
            await SUT.GetChannelManager().UpsertChannelAsync(channel, cancellationToken);

            var channels = await SUT.GetChannelManager().GetChannelsAsync(cancellationToken);

            Assert.NotEmpty(channels);

            var workMessage = SUT.CreateContentManagerManagedWorkStatusModel();
            await SUT.SetWorkRunning(workMessage);

            var priorWorkItem = Services.GetRequiredService<ContentManagerOperation>();
            await priorWorkItem.RunWorkRequestAsync(SUT.CreateContentManagerRequest(workMessage), cancellationToken);

            channels = await SUT.GetChannelManager().GetChannelsAsync(cancellationToken);

            Assert.Empty(channels);
        }

    }
}
