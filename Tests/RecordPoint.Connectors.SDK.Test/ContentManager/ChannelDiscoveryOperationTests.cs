using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Caching.Semaphore;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Test.Common;
using RecordPoint.Connectors.SDK.Work;
using System.Text.Json;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.ContentManager
{
    public class ChannelDiscoveryOperationTests : CommonTestBase<ChannelDiscoveryOperationSut>
    {
        private const string RETRY_COMPLETE_MSG = "Work operation is being retried";
        private const string SEMAPHORE_DEFERRAL_REASON = "Semaphore Lock enabled, deferring Channel Discovery.";

        public class ChannelDiscoveryAction : IChannelDiscoveryAction
        {
            public List<Channel> NewChannels { get; set; } = [];
            public List<Channel> NewChannelRegistrations { get; set; } = [];
            public List<Channel> RenamedChannelRegistrations { get; set; } = [];

            public int? NextDelay { get; set; } = null;
            public ChannelDiscoveryResultType ChannelDiscoveryResultType { get; set; } = ChannelDiscoveryResultType.Complete;
            public SemaphoreLockType SemaphoreLockType { get; set; } = SemaphoreLockType.Global;
            public string OutgoingCursor { get; set; } = string.Empty;

            public bool ThrowException { get; set; } = false;

            public Task<ChannelDiscoveryResult> ExecuteAsync(ConnectorConfigModel connectorConfiguration, CancellationToken cancellationToken, string cursor = null)
            {
                if (ThrowException) throw new TestException();

                var channelOutcome = new ChannelDiscoveryResult()
                {
                    Channels = NewChannels,
                    NewChannelRegistrations = NewChannelRegistrations,
                    RenamedChannelRegistrations = RenamedChannelRegistrations,
                    ResultType = ChannelDiscoveryResultType,
                    NextDelay = NextDelay,
                    SemaphoreLockType = SemaphoreLockType,
                    Cursor = OutgoingCursor
                };

                return Task.FromResult(channelOutcome);
            }
        }

        [Fact]
        public async Task IfConnectorMissing_ChannelDiscoveryIsAbandoned()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            var workMessage = SUT.CreateChannelDiscoveryManagedWorkStatusModel(connector);

            using var servicesScope = Services.CreateScope();
            var operation = servicesScope.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();

            await operation.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(Services);
            //Operation result should be abandoned
            Assert.Equal(WorkResultType.Abandoned, operation.ResultType);
            //Operation result reason should be 'Connector not found'
            Assert.Equal("Connector not found", operation.ResultReason);
            //Channel Discovery work should not be requeued
            Assert.DoesNotContain(workQueueClient.SubmittedRequests, a => a.WorkType == ChannelDiscoveryOperation.WORK_TYPE);
        }

        [Fact]
        public async Task ConnectorConfiguration_ChannelDiscoveryExecutedDateUpdated_WhenExecuted()
        {
            var cancellationToken = CancellationToken.None;

            var scanner = new ChannelDiscoveryAction
            {
                ChannelDiscoveryResultType = ChannelDiscoveryResultType.Complete
            };
            SUT.SelectChannelDiscoveryAction(scanner);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateChannelDiscoveryManagedWorkStatusModel(connector);

            using var servicesScope = Services.CreateScope();
            var operation = servicesScope.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();

            await operation.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(servicesScope.ServiceProvider);
            //Operation result shoud be Complete
            Assert.Equal(WorkResultType.Complete, operation.ResultType);

            //Ensure Channel Discovery Executed Date is updated
            var connectorConfiguation = await SUT.GetConnectorManager().GetConnectorConfigurationAsync(connector.Id, cancellationToken);
            Assert.NotNull(connectorConfiguation.ChannelDiscoveryExecutedDate);
            Assert.Equal(DateTimeOffset.Now, connectorConfiguation.ChannelDiscoveryExecutedDate.Value, TimeSpan.FromSeconds(5));
        }

        /// <summary>
        /// If the Connector does not support Channels, invoke the Null Channel Discovery action
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task IfChannelsUnsupported_NullChannelContentSynchronisationWorkAdded()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateChannelDiscoveryManagedWorkStatusModel(connector);

            using var servicesScope = Services.CreateScope();
            var operation = servicesScope.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();

            await operation.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            var channelManager = SUT.GetChannelManager();
            var channels = await channelManager.GetChannelsAsync(connector.Id, cancellationToken);

            var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(servicesScope.ServiceProvider);
            //Operation result shoud be Complete
            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            //A new channel should be created in the store for the Null Channel
            Assert.Single(channels, a => a.ExternalId == Channel.NULL_CHANNEL_ID);
            //Ensure Channel Discovery is requeued for execution
            Assert.Single(workQueueClient.SubmittedRequests, a => a.WorkType == ChannelDiscoveryOperation.WORK_TYPE);

            var enqueuedChannelDiscoveryWork = workQueueClient.SubmittedRequests.First(a => a.WorkType == ChannelDiscoveryOperation.WORK_TYPE);
            var managedWorkStatus = ManagedWorkStatusModel.Deserialize(enqueuedChannelDiscoveryWork.Body);
            var channelDiscoveryState = JsonSerializer.Deserialize<ChannelDiscoveryState>(managedWorkStatus.State);
            var channelDiscoveryOperationOptions = servicesScope.ServiceProvider.GetRequiredService<IOptions<ChannelDiscoveryOperationOptions>>();
            //Ensure back-off delay is set to the configured delay seconds
            Assert.Equal(channelDiscoveryOperationOptions.Value.DelaySeconds, channelDiscoveryState.LastBackOffDelaySeconds);

            //Ensure Content Synchronisation is queued for the Null Channel
            Assert.Single(workQueueClient.SubmittedRequests, a => a.WorkType == ContentSynchronisationOperation.WORK_TYPE);
        }

        [Fact]
        public async Task IfNullChannelExists_DoNotCreateMoreWork()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var nullChannel = Channel.CreateNullChannel();
            var nullChannelModel = nullChannel.ToChannelModel();
            nullChannelModel.ConnectorId = connector.Id;
            await SUT.GetChannelManager().UpsertChannelAsync(nullChannelModel, cancellationToken);

            var workMessage = SUT.CreateChannelDiscoveryManagedWorkStatusModel(connector);

            using var servicesScope = Services.CreateScope();
            var operation = servicesScope.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();

            await operation.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            var channels = await SUT.GetChannelManager().GetChannelsAsync(connector.Id, cancellationToken);

            var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(servicesScope.ServiceProvider);
            //Operation result shoud be Complete
            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            //The null channel should exist
            Assert.Single(channels, a => a.ExternalId == Channel.NULL_CHANNEL_ID);
            //Ensure Channel Discovery is requeued for execution
            Assert.Single(workQueueClient.SubmittedRequests, a => a.WorkType == ChannelDiscoveryOperation.WORK_TYPE);
            //Ensure Content Synchronisation is NOT queued for the Null Channel
            Assert.DoesNotContain(workQueueClient.SubmittedRequests, a => a.WorkType == ContentSynchronisationOperation.WORK_TYPE);

            var enqueuedChannelDiscoveryWork = workQueueClient.SubmittedRequests.First(a => a.WorkType == ChannelDiscoveryOperation.WORK_TYPE);
            var managedWorkStatus = ManagedWorkStatusModel.Deserialize(enqueuedChannelDiscoveryWork.Body);
            var channelDiscoveryState = JsonSerializer.Deserialize<ChannelDiscoveryState>(managedWorkStatus.State);
            var channelDiscoveryOperationOptions = servicesScope.ServiceProvider.GetRequiredService<IOptions<ChannelDiscoveryOperationOptions>>();
            //Ensure back-off delay is set to the configured delay seconds
            Assert.Equal(channelDiscoveryOperationOptions.Value.DelaySeconds, channelDiscoveryState.LastBackOffDelaySeconds);
        }

        [Fact]
        public async Task IfChannelsDiscovered_ContentSyncPerChannelAdded()
        {
            var cancellationToken = CancellationToken.None;

            var scanner = new ChannelDiscoveryAction
            {
                NewChannels =
                [
                    new() { ExternalId = ContentManagerSutBase.CHANNEL_EXTERNAL_ID_1, Title = ContentManagerSutBase.CHANNEL_TITLE_1},
                    new() { ExternalId = ContentManagerSutBase.CHANNEL_EXTERNAL_ID_2, Title = ContentManagerSutBase.CHANNEL_TITLE_2}
                ],
                ChannelDiscoveryResultType = ChannelDiscoveryResultType.Complete
            };
            SUT.SelectChannelDiscoveryAction(scanner);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateChannelDiscoveryManagedWorkStatusModel(connector);

            using var servicesScope = Services.CreateScope();
            var operation = servicesScope.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();

            await operation.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            var channelManager = SUT.GetChannelManager();
            var channels = await channelManager.GetChannelsAsync(connector.Id, cancellationToken);

            var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(servicesScope.ServiceProvider);
            //Operation result shoud be Complete
            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            //A new channel should be created in the store for each discovered channel
            Assert.Equal(scanner.NewChannels.Count, channels.Count);

            //Ensure Channel Discovery is requeued for execution
            Assert.Single(workQueueClient.SubmittedRequests, a => a.WorkType == ChannelDiscoveryOperation.WORK_TYPE);

            var enqueuedChannelDiscoveryWork = workQueueClient.SubmittedRequests.First(a => a.WorkType == ChannelDiscoveryOperation.WORK_TYPE);
            var managedWorkStatus = ManagedWorkStatusModel.Deserialize(enqueuedChannelDiscoveryWork.Body);
            var channelDiscoveryState = JsonSerializer.Deserialize<ChannelDiscoveryState>(managedWorkStatus.State);
            var channelDiscoveryOperationOptions = servicesScope.ServiceProvider.GetRequiredService<IOptions<ChannelDiscoveryOperationOptions>>();
            //Ensure back-off delay is set to the configured delay seconds
            Assert.Equal(channelDiscoveryOperationOptions.Value.DelaySeconds, channelDiscoveryState.LastBackOffDelaySeconds);

            var enqueuedContentSynchronisationWork = workQueueClient.SubmittedRequests.Where(a => a.WorkType == ContentSynchronisationOperation.WORK_TYPE).ToList();
            //Ensure Content Synchronisation is queued for each discovered channel
            Assert.Equal(scanner.NewChannels.Count, enqueuedContentSynchronisationWork.Count);
            foreach (var enqueuedWork in enqueuedContentSynchronisationWork)
            {
                var contentSyncWorkManagedWorkStatus = ManagedWorkStatusModel.Deserialize(enqueuedWork.Body);
                var contentSyncConfiguration = contentSyncWorkManagedWorkStatus.DeserialiseContentSynchronisationConfiguration();
                Assert.Contains(scanner.NewChannels, a => a.ExternalId == contentSyncConfiguration.ChannelExternalId);
            }
        }

        [Fact]
        public async Task IfRenamedChannelsDiscovered_ContentRegistrationPerRenamedChannelAddedWithUnboundedStartDate()
        {
            var cancellationToken = CancellationToken.None;

            var scanner = new ChannelDiscoveryAction
            {
                RenamedChannelRegistrations =
                [
                    new() { ExternalId = ContentManagerSutBase.CHANNEL_EXTERNAL_ID_1, Title = ContentManagerSutBase.CHANNEL_TITLE_1},
                    new() { ExternalId = ContentManagerSutBase.CHANNEL_EXTERNAL_ID_2, Title = ContentManagerSutBase.CHANNEL_TITLE_2}
                ],
                ChannelDiscoveryResultType = ChannelDiscoveryResultType.Complete
            };
            SUT.SelectChannelDiscoveryAction(scanner);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateChannelDiscoveryManagedWorkStatusModel(connector);

            using var servicesScope = Services.CreateScope();
            var operation = servicesScope.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();

            await operation.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(servicesScope.ServiceProvider);

            // Operation result should be complete.
            Assert.Equal(WorkResultType.Complete, operation.ResultType);

            var enqueuedContentRegistrationWork = workQueueClient.SubmittedRequests
                .Where(a => a.WorkType == ContentRegistrationOperation.WORK_TYPE)
                .ToList();

            // Ensure Content Registration is queued for each renamed channel.
            Assert.Equal(scanner.RenamedChannelRegistrations.Count, enqueuedContentRegistrationWork.Count);

            foreach (var enqueuedWork in enqueuedContentRegistrationWork)
            {
                var registrationManagedWorkStatus = ManagedWorkStatusModel.Deserialize(enqueuedWork.Body);
                var registrationConfiguration = registrationManagedWorkStatus.DeserialiseContentRegistrationConfiguration();

                Assert.Contains(scanner.RenamedChannelRegistrations, a => a.ExternalId == registrationConfiguration.ChannelExternalId);

                // Renamed channels should force an unbounded registration by setting StartDate to DateTime.MinValue.
                Assert.True(registrationConfiguration.Context.TryGetValue(IContentRegistrationAction.StartDate, out var configuredStartDate));
                Assert.Equal(DateTime.MinValue.ToString("o"), configuredStartDate);
            }
        }

        [Fact]
        public async Task IfRunResultIncludesCursor_EnqueuedWorkState_ContainsCursor()
        {
            var cancellationToken = CancellationToken.None;
            var incomingCursor = $"https://dummyCursorLink/{DateTimeOffset.UtcNow.Ticks}";

            var scanner = new ChannelDiscoveryAction
            {
                NewChannels =
                [
                    new() { ExternalId = ContentManagerSutBase.CHANNEL_EXTERNAL_ID_1, Title = ContentManagerSutBase.CHANNEL_TITLE_1},
                    new() { ExternalId = ContentManagerSutBase.CHANNEL_EXTERNAL_ID_2, Title = ContentManagerSutBase.CHANNEL_TITLE_2}
                ],
                ChannelDiscoveryResultType = ChannelDiscoveryResultType.Incomplete,
                OutgoingCursor = $"https://dummyCursorLink/{DateTimeOffset.UtcNow.Ticks}"
            };
            SUT.SelectChannelDiscoveryAction(scanner);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateChannelDiscoveryManagedWorkStatusModel(connector, incomingCursor);

            using var servicesScope = Services.CreateScope();
            var operation = servicesScope.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();

            await operation.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(servicesScope.ServiceProvider);

            var enqueuedChannelDiscoveryWork = workQueueClient.SubmittedRequests.First(a => a.WorkType == ChannelDiscoveryOperation.WORK_TYPE);
            var managedWorkStatus = ManagedWorkStatusModel.Deserialize(enqueuedChannelDiscoveryWork.Body);
            var channelDiscoveryState = JsonSerializer.Deserialize<ChannelDiscoveryState>(managedWorkStatus.State);
            var channelDiscoveryOperationOptions = servicesScope.ServiceProvider.GetRequiredService<IOptions<ChannelDiscoveryOperationOptions>>();
            //Ensure enqueued work state contains cursor from action result
            Assert.Equal(scanner.OutgoingCursor, channelDiscoveryState.Cursor);

        }

        [Fact]
        public async Task IfChannelDiscovered_InSubsequentRun_ContentSyncPerChannelAdded()
        {
            var cancellationToken = CancellationToken.None;

            var scanner = new ChannelDiscoveryAction
            {
                NewChannels =
                [
                    new() { ExternalId = ContentManagerSutBase.CHANNEL_EXTERNAL_ID_1, Title = ContentManagerSutBase.CHANNEL_TITLE_1}
                ],
                ChannelDiscoveryResultType = ChannelDiscoveryResultType.Complete
            };
            SUT.SelectChannelDiscoveryAction(scanner);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var channelManager = SUT.GetChannelManager();
            var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(Services);
            using (var servicesScope = Services.CreateScope())
            {
                var workMessage = SUT.CreateChannelDiscoveryManagedWorkStatusModel(connector);

                var operation = servicesScope.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();
                await operation.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);


                var channels = await channelManager.GetChannelsAsync(connector.Id, cancellationToken);

                //Operation result shoud be Complete
                Assert.Equal(WorkResultType.Complete, operation.ResultType);
                //A new channel should be created in the store for each discovered channel
                Assert.Contains(channels, a => a.ExternalId == ContentManagerSutBase.CHANNEL_EXTERNAL_ID_1);

                //Ensure Channel Discovery is requeued for execution
                Assert.Single(workQueueClient.SubmittedRequests, a => a.WorkType == ChannelDiscoveryOperation.WORK_TYPE);
            }

            using (var servicesScope = Services.CreateScope())
            {
                //Change the discovered channels for the subsequent run
                scanner.NewChannels =
                [
                    new() { ExternalId = ContentManagerSutBase.CHANNEL_EXTERNAL_ID_2, Title = ContentManagerSutBase.CHANNEL_TITLE_2}
                ];

                var workRequest = workQueueClient.SubmittedRequests.First(a => a.WorkType == ChannelDiscoveryOperation.WORK_TYPE);

                var operation = servicesScope.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();
                await operation.RunWorkRequestAsync(workRequest, cancellationToken);

                var channels = await channelManager.GetChannelsAsync(connector.Id, cancellationToken);

                //Operation result shoud be Complete
                Assert.Equal(WorkResultType.Complete, operation.ResultType);
                //A new channel should be created in the store for each discovered channel
                Assert.Contains(channels, a => a.ExternalId == ContentManagerSutBase.CHANNEL_EXTERNAL_ID_2);
                //Total Channels shoudl now be 2
                Assert.Equal(2, channels.Count);

                //Ensure Content Sync is queued for execution
                Assert.Equal(2, workQueueClient.SubmittedRequests.Count(a => a.WorkType == ContentSynchronisationOperation.WORK_TYPE));
            }
        }

        [Fact]
        public async Task IfGlobalSemaphoreLockActive_IsDeferred()
        {
            var cancellationToken = CancellationToken.None;

            const int lockDuration = 300;

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();
            semaphoreLockManager.ConnectorConfiguration = connector;
            await semaphoreLockManager.SetSemaphoreAsync(SemaphoreLockType.Global, ChannelDiscoveryOperation.WORK_TYPE, null, lockDuration, cancellationToken);

            var workMessage = SUT.CreateChannelDiscoveryManagedWorkStatusModel(connector);

            using var servicesScope = Services.CreateScope();
            var operation = servicesScope.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();
            await operation.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(servicesScope.ServiceProvider);

            //Work Result should be deferred
            Assert.Equal(WorkResultType.Deferred, operation.ResultType);
            Assert.Equal(SEMAPHORE_DEFERRAL_REASON, operation.ResultReason);
            Assert.Equal(DateTimeOffset.Now.AddSeconds(lockDuration), operation.WaitTill.Value, TimeSpan.FromSeconds(5));

            //Note: Work is requeued based on the wait time by the WorkQueue Provider (e.g. AzureServiceBusWorkServer, or RabbitMqWorkServer)
        }

        [Fact]
        public async Task IfScopedSemaphoreLockActive_IsDeferred()
        {
            var cancellationToken = CancellationToken.None;

            const int lockDuration = 300;

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();
            semaphoreLockManager.ConnectorConfiguration = connector;
            await semaphoreLockManager.SetSemaphoreAsync(SemaphoreLockType.Scoped, ChannelDiscoveryOperation.WORK_TYPE, null, lockDuration, cancellationToken);

            var workMessage = SUT.CreateChannelDiscoveryManagedWorkStatusModel(connector);

            using var servicesScope = Services.CreateScope();
            var operation = servicesScope.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();
            await operation.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(servicesScope.ServiceProvider);

            //Work Result should be deferred
            Assert.Equal(WorkResultType.Deferred, operation.ResultType);
            Assert.Equal(SEMAPHORE_DEFERRAL_REASON, operation.ResultReason);
            Assert.Equal(DateTimeOffset.Now.AddSeconds(lockDuration), operation.WaitTill.Value, TimeSpan.FromSeconds(5));

            //Note: Work is requeued based on the wait time by the WorkQueue Provider (e.g. AzureServiceBusWorkServer, or RabbitMqWorkServer)
        }

        [Fact]
        public async Task IfDifferentScopedSemaphoreLockActive_IsNotDeferred()
        {
            var cancellationToken = CancellationToken.None;

            const int lockDuration = 300;

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();
            semaphoreLockManager.ConnectorConfiguration = connector;
            await semaphoreLockManager.SetSemaphoreAsync(SemaphoreLockType.Scoped, ChannelDiscoveryOperation.WORK_TYPE, null, lockDuration, cancellationToken);

            //Set the Key to a different value so the lock is not detected
            SUT.SemaphoreLockScopedKeyAction.Key = "KEY_456";

            var workMessage = SUT.CreateChannelDiscoveryManagedWorkStatusModel(connector);

            using var servicesScope = Services.CreateScope();
            var operation = servicesScope.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();
            await operation.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(servicesScope.ServiceProvider);

            //Work Result should NOT be deferred
            Assert.Equal(WorkResultType.Complete, operation.ResultType);

        }

        [Fact]
        public async Task IfActionException_OperationIsCompleted_WhenRetryIsEnabled()
        {
            var cancellationToken = CancellationToken.None;

            var scanner = new ChannelDiscoveryAction
            {
                ThrowException = true
            };
            SUT.SelectChannelDiscoveryAction(scanner);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateChannelDiscoveryManagedWorkStatusModel(connector);

            using var servicesScope = Services.CreateScope();
            var operation = servicesScope.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();

            await operation.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            Assert.Equal(RETRY_COMPLETE_MSG, operation.ResultReason);
            Assert.NotNull(operation.Exception);

        }

        [Fact]
        public async Task IfActionException_OperationIsFailed_WhenRetryIsDisabled()
        {
            var cancellationToken = CancellationToken.None;

            var scanner = new ChannelDiscoveryAction
            {
                ThrowException = true
            };
            SUT.SelectChannelDiscoveryAction(scanner);

            var myConfig = new Dictionary<string, string>
            {
                { "Connector:RetryOnFailure", "False" },
            };

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfig)
                .Build();

            await StartSutAsync(config);

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateChannelDiscoveryManagedWorkStatusModel(connector);

            using var servicesScope = Services.CreateScope();
            var workItem = servicesScope.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();

            await workItem.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            Assert.Equal(WorkResultType.Failed, workItem.ResultType);
            Assert.NotNull(workItem.Exception);

        }
    }
}

