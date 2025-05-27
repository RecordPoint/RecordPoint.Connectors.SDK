using Microsoft.Extensions.Configuration;
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

            using var servicesScope = Services.CreateScope();
            var workItem = servicesScope.ServiceProvider.GetRequiredService<ContentManagerOperation>();
            await workItem.RunWorkRequestAsync(SUT.CreateContentManagerRequest(workMessage), cancellationToken);

            var managedWorkStatuses = await ContentManagerSutBase.GetWorkStatusManager(servicesScope.ServiceProvider)
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

            using var servicesScope = Services.CreateScope();
            var workItem = servicesScope.ServiceProvider.GetRequiredService<ContentManagerOperation>();
            var workRequest = SUT.CreateContentManagerRequest(workMessage);
            await workItem.RunWorkRequestAsync(workRequest, cancellationToken);

            var managedWorkStatuses = await ContentManagerSutBase.GetWorkStatusManager(servicesScope.ServiceProvider)
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

            using var servicesScope1 = Services.CreateScope();
            var priorWorkItem = servicesScope1.ServiceProvider.GetRequiredService<ContentManagerOperation>();
            await priorWorkItem.RunWorkRequestAsync(SUT.CreateContentManagerRequest(workMessage), cancellationToken);

            var priorWorks = await ContentManagerSutBase.GetWorkStatusManager(servicesScope1.ServiceProvider)
                .GetWorkStatusesAsync(a => a.WorkType == ContentManagerOperation.WORK_TYPE, cancellationToken);
            var priorWork = priorWorks.Single();

            await SUT.SetWorkContinue(workMessage);

            using var servicesScope2 = Services.CreateScope();
            var afterWorkItem = servicesScope2.ServiceProvider.GetRequiredService<ContentManagerOperation>();
            await afterWorkItem.RunWorkRequestAsync(SUT.CreateContentManagerRequest(workMessage), cancellationToken);

            Assert.Equal(WorkResultType.Complete, afterWorkItem.ResultType);

            var afterWorks = await ContentManagerSutBase.GetWorkStatusManager(servicesScope2.ServiceProvider)
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

            using var servicesScope = Services.CreateScope();
            var priorWorkItem = servicesScope.ServiceProvider.GetRequiredService<ContentManagerOperation>();
            await priorWorkItem.RunWorkRequestAsync(SUT.CreateContentManagerRequest(workMessage), cancellationToken);

            channels = await SUT.GetChannelManager().GetChannelsAsync(cancellationToken);

            Assert.Empty(channels);
        }

        [Fact]
        public async Task ChannelRemovedIfConnectorDisabledPassedThreshold()
        {
            var cancellationToken = CancellationToken.None;

            var myConfig = new Dictionary<string, string>
            {
                { "ContentManager:MaxDisabledConnectorAge", "1209600" },
            };

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfig)
                .Build();

            await StartSutAsync(config);

            var connector = ContentManagerSutBase.CreateConnector1();
            ContentManagerSutBase.DisableConnector(connector, DateTimeOffset.Now.AddDays(-30));
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var channel = ContentManagerSutBase.CreateChannel1();
            await SUT.GetChannelManager().UpsertChannelAsync(channel, cancellationToken);

            var channels = await SUT.GetChannelManager().GetChannelsAsync(cancellationToken);

            Assert.NotEmpty(channels);

            var workMessage = SUT.CreateContentManagerManagedWorkStatusModel();
            await SUT.SetWorkRunning(workMessage);

            using var servicesScope = Services.CreateScope();
            var workItem = servicesScope.ServiceProvider.GetRequiredService<ContentManagerOperation>();
            await workItem.RunWorkRequestAsync(SUT.CreateContentManagerRequest(workMessage), cancellationToken);

            channels = await SUT.GetChannelManager().GetChannelsAsync(cancellationToken);

            Assert.Empty(channels);
        }

        [Fact]
        public async Task ChannelNotRemovedIfConnectorDisabledNotPassedThreshold()
        {
            var cancellationToken = CancellationToken.None;

            var myConfig = new Dictionary<string, string>
            {
                { "ContentManager:MaxDisabledConnectorAge", "1209600" },
            };

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfig)
                .Build();

            await StartSutAsync(config);

            var connector = ContentManagerSutBase.CreateConnector1();
            ContentManagerSutBase.DisableConnector(connector, DateTimeOffset.Now);
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var channel = ContentManagerSutBase.CreateChannel1();
            await SUT.GetChannelManager().UpsertChannelAsync(channel, cancellationToken);

            var channels = await SUT.GetChannelManager().GetChannelsAsync(cancellationToken);

            Assert.NotEmpty(channels);

            var workMessage = SUT.CreateContentManagerManagedWorkStatusModel();
            await SUT.SetWorkRunning(workMessage);

            using var servicesScope = Services.CreateScope();
            var workItem = servicesScope.ServiceProvider.GetRequiredService<ContentManagerOperation>();
            await workItem.RunWorkRequestAsync(SUT.CreateContentManagerRequest(workMessage), cancellationToken);

            channels = await SUT.GetChannelManager().GetChannelsAsync(cancellationToken);

            Assert.NotEmpty(channels);
        }

        [Fact]
        public async Task AggregationRemovedIfConnectorDoesNotExist()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();

            var aggregation = ContentManagerSutBase.CreateAggregation1();
            await SUT.GetAggregationManager().UpsertAggregationAsync(aggregation, cancellationToken);

            var aggregations = await SUT.GetAggregationManager().GetAggregationsAsync(cancellationToken);

            Assert.NotEmpty(aggregations);

            var workMessage = SUT.CreateContentManagerManagedWorkStatusModel();
            await SUT.SetWorkRunning(workMessage);

            using var servicesScope = Services.CreateScope();
            var workItem = servicesScope.ServiceProvider.GetRequiredService<ContentManagerOperation>();
            await workItem.RunWorkRequestAsync(SUT.CreateContentManagerRequest(workMessage), cancellationToken);

            aggregations = await SUT.GetAggregationManager().GetAggregationsAsync(cancellationToken);

            Assert.Empty(aggregations);
        }

        [Fact]
        public async Task AggregationRemovedIfConnectorDisabledPassedThreshold()
        {
            var cancellationToken = CancellationToken.None;

            var myConfig = new Dictionary<string, string>
            {
                { "ContentManager:MaxDisabledConnectorAge", "1209600" },
            };

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfig)
                .Build();

            await StartSutAsync(config);

            var connector = ContentManagerSutBase.CreateConnector1();
            ContentManagerSutBase.DisableConnector(connector, DateTimeOffset.Now.AddDays(-30));
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var aggregation = ContentManagerSutBase.CreateAggregation1();
            await SUT.GetAggregationManager().UpsertAggregationAsync(aggregation, cancellationToken);

            var aggregations = await SUT.GetAggregationManager().GetAggregationsAsync(cancellationToken);

            Assert.NotEmpty(aggregations);

            var workMessage = SUT.CreateContentManagerManagedWorkStatusModel();
            await SUT.SetWorkRunning(workMessage);

            using var servicesScope = Services.CreateScope();
            var workItem = servicesScope.ServiceProvider.GetRequiredService<ContentManagerOperation>();
            await workItem.RunWorkRequestAsync(SUT.CreateContentManagerRequest(workMessage), cancellationToken);

            aggregations = await SUT.GetAggregationManager().GetAggregationsAsync(cancellationToken);

            Assert.Empty(aggregations);
        }

        [Fact]
        public async Task AggregationNotRemovedIfConnectorDisabledNotPassedThreshold()
        {
            var cancellationToken = CancellationToken.None;

            var myConfig = new Dictionary<string, string>
            {
                { "ContentManager:MaxDisabledConnectorAge", "1209600" },
            };

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfig)
                .Build();

            await StartSutAsync(config);

            var connector = ContentManagerSutBase.CreateConnector1();
            ContentManagerSutBase.DisableConnector(connector, DateTimeOffset.Now);
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var aggregation = ContentManagerSutBase.CreateAggregation1();
            await SUT.GetAggregationManager().UpsertAggregationAsync(aggregation, cancellationToken);

            var aggregations = await SUT.GetAggregationManager().GetAggregationsAsync(cancellationToken);

            Assert.NotEmpty(aggregations);

            var workMessage = SUT.CreateContentManagerManagedWorkStatusModel();
            await SUT.SetWorkRunning(workMessage);

            using var servicesScope = Services.CreateScope();
            var workItem = servicesScope.ServiceProvider.GetRequiredService<ContentManagerOperation>();
            await workItem.RunWorkRequestAsync(SUT.CreateContentManagerRequest(workMessage), cancellationToken);

            aggregations = await SUT.GetAggregationManager().GetAggregationsAsync(cancellationToken);

            Assert.NotEmpty(aggregations);
        }
    }
}
