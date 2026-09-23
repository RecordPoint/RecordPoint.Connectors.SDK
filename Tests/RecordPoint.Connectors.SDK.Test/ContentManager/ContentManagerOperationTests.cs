using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Work;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.ContentManager
{
    public class ContentManagerOperationTests : CommonTestBase<ContentManagerOperationSut>
    {
        [Fact]
        public async Task WorkAddedIfConnectorExists()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            using var servicesScope = Services.CreateScope();
            var operation = servicesScope.ServiceProvider.GetRequiredService<ContentManagerOperationTestWrapper>();
            await operation.InvokeInnerRunAsync(cancellationToken);

            var connectorConfiguation = await SUT.GetConnectorManager().GetConnectorConfigurationAsync(connector.Id, cancellationToken);
            var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(servicesScope.ServiceProvider);

            //Ensure Operation is Complete and Channel Discovery Work is Added
            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            Assert.Contains(workQueueClient.SubmittedRequests, a => a.WorkType == ChannelDiscoveryOperation.WORK_TYPE);

            //Ensure Channel Discovery Enqueued Date is updated
            Assert.NotNull(connectorConfiguation.ChannelDiscoveryEnqueuedDate);
            Assert.Equal(DateTimeOffset.Now, connectorConfiguation.ChannelDiscoveryEnqueuedDate.Value, TimeSpan.FromSeconds(5));
        }

        [Fact]
        public async Task WorkNotAddedIfNoConnectorExists()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();

            using var servicesScope = Services.CreateScope();
            var operation = servicesScope.ServiceProvider.GetRequiredService<ContentManagerOperationTestWrapper>();
            await operation.InvokeInnerRunAsync(cancellationToken);

            var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(servicesScope.ServiceProvider);

            //Ensure Operation is Complete and Channel Discovery Work is NOT Added
            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            Assert.DoesNotContain(workQueueClient.SubmittedRequests, a => a.WorkType == ChannelDiscoveryOperation.WORK_TYPE);
        }

        [Fact]
        public async Task WorkNotAddedIfConnectorWorkAlreadyEnqueued()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();

            var enqueuedDate = DateTimeOffset.Now.AddDays(-1);

            //Create a connector configuration
            var connector = ContentManagerSutBase.CreateConnector1();
            var connectorConfiguration = connector.ConvertToConnectorData();
            await SUT.GetConnectorManager().SetConnectorConfigurationAsync(connectorConfiguration, cancellationToken);

            //Patch the Channel Discovery Enqueued Date to simulate work already enqueued
            await SUT.GetConnectorManager().PatchConnectorConfigurationAsync(connector.Id, connector => connector.ChannelDiscoveryEnqueuedDate = enqueuedDate, cancellationToken);

            using var servicesScope = Services.CreateScope();
            var operation = servicesScope.ServiceProvider.GetRequiredService<ContentManagerOperationTestWrapper>();
            await operation.InvokeInnerRunAsync(cancellationToken);

            var connectorConfiguation = await SUT.GetConnectorManager().GetConnectorConfigurationAsync(connector.Id, cancellationToken);
            var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(servicesScope.ServiceProvider);

            //Ensure Operation is Complete and Channel Discovery Work is NOT Added
            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            Assert.DoesNotContain(workQueueClient.SubmittedRequests, a => a.WorkType == ChannelDiscoveryOperation.WORK_TYPE);

            //Ensure Channel Discovery Enqueued Date is unchanged
            Assert.NotNull(connectorConfiguation.ChannelDiscoveryEnqueuedDate);
            Assert.Equal(enqueuedDate, connectorConfiguation.ChannelDiscoveryEnqueuedDate.Value, TimeSpan.FromSeconds(5));
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

            using var servicesScope = Services.CreateScope();
            var operation = servicesScope.ServiceProvider.GetRequiredService<ContentManagerOperationTestWrapper>();
            await operation.InvokeInnerRunAsync(cancellationToken);

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

            using var servicesScope = Services.CreateScope();
            var operation = servicesScope.ServiceProvider.GetRequiredService<ContentManagerOperationTestWrapper>();
            await operation.InvokeInnerRunAsync(cancellationToken);

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

            using var servicesScope = Services.CreateScope();
            var operation = servicesScope.ServiceProvider.GetRequiredService<ContentManagerOperationTestWrapper>();
            await operation.InvokeInnerRunAsync(cancellationToken);

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

            using var servicesScope = Services.CreateScope();
            var operation = servicesScope.ServiceProvider.GetRequiredService<ContentManagerOperationTestWrapper>();
            await operation.InvokeInnerRunAsync(cancellationToken);

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

            using var servicesScope = Services.CreateScope();
            var operation = servicesScope.ServiceProvider.GetRequiredService<ContentManagerOperationTestWrapper>();
            await operation.InvokeInnerRunAsync(cancellationToken);

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

            using var servicesScope = Services.CreateScope();
            var operation = servicesScope.ServiceProvider.GetRequiredService<ContentManagerOperationTestWrapper>();
            await operation.InvokeInnerRunAsync(cancellationToken);

            aggregations = await SUT.GetAggregationManager().GetAggregationsAsync(cancellationToken);

            Assert.NotEmpty(aggregations);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(150)]
        public async Task AggregationsCleanedUpInBatchesIfConnectorDoesNotExist(int count)
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();

            var aggregationManager = SUT.GetAggregationManager();
            for (var i = 0; i < count; i++)
            {
                await aggregationManager.UpsertAggregationAsync(new AggregationModel
                {
                    ExternalId = $"Aggregation_{i}",
                    Title = $"Aggregation {i}",
                    ConnectorId = ContentManagerSutBase.CONNECTOR_CONFIGURATION_ID_1
                }, cancellationToken);
            }

            var aggregations = await aggregationManager.GetAggregationsAsync(cancellationToken);
            Assert.Equal(count, aggregations.Count);

            using var servicesScope = Services.CreateScope();
            var operation = servicesScope.ServiceProvider.GetRequiredService<ContentManagerOperationTestWrapper>();
            await operation.InvokeInnerRunAsync(cancellationToken);

            aggregations = await aggregationManager.GetAggregationsAsync(cancellationToken);
            Assert.Empty(aggregations);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(150)]
        public async Task ChannelsCleanedUpInBatchesIfConnectorDoesNotExist(int count)
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();

            var channelManager = SUT.GetChannelManager();
            for (var i = 0; i < count; i++)
            {
                await channelManager.UpsertChannelAsync(new ChannelModel
                {
                    ExternalId = $"Channel_{i}",
                    Title = $"Channel {i}",
                    ConnectorId = ContentManagerSutBase.CONNECTOR_CONFIGURATION_ID_1
                }, cancellationToken);
            }

            var channels = await channelManager.GetChannelsAsync(cancellationToken);
            Assert.Equal(count, channels.Count);

            using var servicesScope = Services.CreateScope();
            var operation = servicesScope.ServiceProvider.GetRequiredService<ContentManagerOperationTestWrapper>();
            await operation.InvokeInnerRunAsync(cancellationToken);

            channels = await channelManager.GetChannelsAsync(cancellationToken);
            Assert.Empty(channels);
        }
    }
}
