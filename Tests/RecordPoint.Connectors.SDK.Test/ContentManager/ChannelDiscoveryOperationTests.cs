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

        public class ChannelDiscoveryAction : IChannelDiscoveryAction
        {
            public List<Channel> NewChannels { get; set; } = new();
            public List<Channel> NewChannelRegistrations { get; set; } = new();

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
                    ResultType = ChannelDiscoveryResultType,
                    NextDelay = NextDelay,
                    SemaphoreLockType = SemaphoreLockType,
                    Cursor = OutgoingCursor
                };

                return Task.FromResult(channelOutcome);
            }
        }


        [Fact]
        public async Task IfRequestedConnectorMissing_CompletesWithNoUpdates()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            var workMessage = SUT.CreateChannelDiscoveryManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            using var servicesScope = Services.CreateScope();
            var workItem = servicesScope.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();

            await workItem.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            var workStatuses = await ContentManagerSutBase.GetWorkStatusManager(servicesScope.ServiceProvider)
                .GetWorkStatusesAsync(j => j.WorkType == ContentSynchronisationOperation.WORK_TYPE, cancellationToken);

            Assert.Equal(WorkResultType.Abandoned, workItem.ResultType);
            Assert.Equal("Connector not found", workItem.ResultReason);
            Assert.Empty(workStatuses);
            Assert.True(workStatuses.All(a => a.Status == ManagedWorkStatuses.Complete));
        }

        [Fact]
        public async Task IfChannelsUnsupport_NullChannelContentSynchronisationWorkAdded()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateChannelDiscoveryManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            using var servicesScope = Services.CreateScope();
            var workItem = servicesScope.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();

            await workItem.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            var workStatuses = await ContentManagerSutBase.GetWorkStatusManager(servicesScope.ServiceProvider)
                .GetWorkStatusesAsync(j => j.WorkType == ContentSynchronisationOperation.WORK_TYPE, cancellationToken);

            Assert.Single(workStatuses);
            Assert.Equal(ManagedWorkStatuses.Running, workItem.WorkManager.WorkStatus.Status);

            var work = workStatuses.Single();

            Assert.Equal(ManagedWorkStatuses.Running, work.Status);
            Assert.Equal(ContentSynchronisationOperation.WORK_TYPE, work.WorkType);
            Assert.Equal(ContentSynchronisationConfiguration.ConfigurationType, work.ConfigurationType);
            Assert.Equal(connector.Id, work.ConnectorId);
        }

        [Fact]
        public async Task IfNullChannelWorkAlreadyAdded_NoChange()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateChannelDiscoveryManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            using var servicesScope1 = Services.CreateScope();
            var priorWorkItem = servicesScope1.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();
            await priorWorkItem.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            var priorWorks = await ContentManagerSutBase.GetWorkStatusManager(servicesScope1.ServiceProvider)
                .GetWorkStatusesAsync(j => j.WorkType == ContentSynchronisationOperation.WORK_TYPE, cancellationToken);

            Assert.Single(priorWorks);
            Assert.Equal(WorkResultType.Complete, priorWorkItem.ResultType);

            var priorWork = priorWorks.Single();

            await SUT.SetWorkContinue(workMessage);

            using var servicesScope2 = Services.CreateScope();
            var afterWorkItem = servicesScope2.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();
            await afterWorkItem.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            Assert.Equal(WorkResultType.Complete, afterWorkItem.ResultType);

            var afterWorks = await ContentManagerSutBase.GetWorkStatusManager(servicesScope2.ServiceProvider)
                .GetWorkStatusesAsync(j => j.WorkType == ContentSynchronisationOperation.WORK_TYPE, cancellationToken);

            Assert.Single(afterWorks);
            var afterWork = afterWorks.Single();

            Assert.Equal(priorWork.WorkId, afterWork.WorkId);

            Assert.Equal(ManagedWorkStatuses.Running, afterWork.Status);
            Assert.Equal(ContentSynchronisationOperation.WORK_TYPE, afterWork.WorkType);
            Assert.Equal(ContentSynchronisationConfiguration.ConfigurationType, afterWork.ConfigurationType);
            Assert.Equal(connector.Id, afterWork.ConnectorId);
        }

        [Fact]
        public async Task IfChannelsProvided_ReturnsComplete_ContentSyncPerChannelAdded_NewChannelDiscoveryWorkAdded()
        {
            var cancellationToken = CancellationToken.None;

            var scanner = new ChannelDiscoveryAction
            {
                NewChannels = new List<Channel>
                {
                    new() { ExternalId = "CHANNEL_1", Title = "Channel 1"},
                    new() { ExternalId = "CHANNEL_2", Title = "Channel 2"}
                },
                ChannelDiscoveryResultType = ChannelDiscoveryResultType.Complete
            };
            SUT.SelectChannelDiscoveryAction(scanner);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateChannelDiscoveryManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            using var servicesScope = Services.CreateScope();
            var workItem = servicesScope.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();
            var channelDiscoveryOperationOptions = servicesScope.ServiceProvider.GetRequiredService<IOptions<ChannelDiscoveryOperationOptions>>();

            var channelDiscoveryManagedWorkStatusesBeforeRun = await ContentManagerSutBase.GetWorkStatusManager(servicesScope.ServiceProvider)
                .GetWorkStatusesAsync(a => a.WorkType == ChannelDiscoveryOperation.WORK_TYPE, cancellationToken);
            Assert.Single(channelDiscoveryManagedWorkStatusesBeforeRun);
            var channelDiscoveryManagedWorkStatusBeforeRunState = JsonSerializer.Deserialize<ChannelDiscoveryState>(channelDiscoveryManagedWorkStatusesBeforeRun[0].State);
            Assert.Null(channelDiscoveryManagedWorkStatusBeforeRunState.Cursor);

            await workItem.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            var managedWorkStatuses = await ContentManagerSutBase.GetWorkStatusManager(servicesScope.ServiceProvider)
                .GetWorkStatusesAsync(a => a.WorkType == ContentSynchronisationOperation.WORK_TYPE, cancellationToken);

            var channelDiscoveryManagedWorkStatusesAfterRun = await ContentManagerSutBase.GetWorkStatusManager(servicesScope.ServiceProvider)
                .GetWorkStatusesAsync(a => a.WorkType == ChannelDiscoveryOperation.WORK_TYPE, cancellationToken);

            Assert.Equal(2, managedWorkStatuses.Count);
            Assert.Equal(WorkResultType.Complete, workItem.ResultType);
            Assert.Equal(ManagedWorkStatuses.Running, workItem.WorkManager.WorkStatus.Status);

            Assert.Contains(managedWorkStatuses, j => j.DeserialiseContentSynchronisationConfiguration().ChannelExternalId == "CHANNEL_1");
            Assert.Contains(managedWorkStatuses, j => j.DeserialiseContentSynchronisationConfiguration().ChannelExternalId == "CHANNEL_2");

            Assert.Single(channelDiscoveryManagedWorkStatusesAfterRun);
            Assert.NotEqual(channelDiscoveryManagedWorkStatusesBeforeRun[0].WorkId, channelDiscoveryManagedWorkStatusesAfterRun[0].WorkId);
            var channelDiscoveryManagedWorkStatusAfterRunState = JsonSerializer.Deserialize<ChannelDiscoveryState>(channelDiscoveryManagedWorkStatusesAfterRun[0].State);
            Assert.Null(channelDiscoveryManagedWorkStatusAfterRunState.Cursor);
            Assert.Equal(channelDiscoveryOperationOptions.Value.DelaySeconds, channelDiscoveryManagedWorkStatusAfterRunState.LastBackOffDelaySeconds);
        }

        [Fact]
        public async Task IfChannelsProvided_BatchCursorIsProvided_ReturnsIncomplete_ContentSyncPerChannelAdded_NewChannelDiscoveryBatchWorkAddedIncludingNextBatchCursor()
        {
            var cancellationToken = CancellationToken.None;
            var incomingCursor = $"https://dummyCursorLink/{DateTimeOffset.UtcNow.Ticks}";

            var scanner = new ChannelDiscoveryAction
            {
                NewChannels = new List<Channel>
                {
                    new() { ExternalId = "CHANNEL_1", Title = "Channel 1"},
                    new() { ExternalId = "CHANNEL_2", Title = "Channel 2"}
                },
                ChannelDiscoveryResultType = ChannelDiscoveryResultType.Incomplete,
                OutgoingCursor = $"https://dummyCursorLink/{DateTimeOffset.UtcNow.Ticks}"
            };
            SUT.SelectChannelDiscoveryAction(scanner);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateChannelDiscoveryManagedWorkStatusModel(connector, incomingCursor);
            await SUT.SetWorkRunning(workMessage);

            using var servicesScope = Services.CreateScope();
            var workItem = servicesScope.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();

            var channelDiscoveryManagedWorkStatusesBeforeRun = await ContentManagerSutBase.GetWorkStatusManager(servicesScope.ServiceProvider)
                .GetWorkStatusesAsync(a => a.WorkType == ChannelDiscoveryOperation.WORK_TYPE, cancellationToken);
            Assert.Single(channelDiscoveryManagedWorkStatusesBeforeRun);
            var channelDiscoveryManagedWorkStatusBeforeRunState = JsonSerializer.Deserialize<ChannelDiscoveryState>(channelDiscoveryManagedWorkStatusesBeforeRun[0].State);
            Assert.Equal(incomingCursor, channelDiscoveryManagedWorkStatusBeforeRunState.Cursor);

            await workItem.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            var contentSyncManagedWorkStatuses = await ContentManagerSutBase.GetWorkStatusManager(servicesScope.ServiceProvider)
                .GetWorkStatusesAsync(a => a.WorkType == ContentSynchronisationOperation.WORK_TYPE, cancellationToken);

            var channelDiscoveryManagedWorkStatusesAfterRun = await ContentManagerSutBase.GetWorkStatusManager(servicesScope.ServiceProvider)
                .GetWorkStatusesAsync(a => a.WorkType == ChannelDiscoveryOperation.WORK_TYPE, cancellationToken);

            Assert.Equal(2, contentSyncManagedWorkStatuses.Count);
            Assert.Equal(WorkResultType.Complete, workItem.ResultType);
            Assert.Equal(ManagedWorkStatuses.Running, workItem.WorkManager.WorkStatus.Status);

            Assert.Contains(contentSyncManagedWorkStatuses, j => j.DeserialiseContentSynchronisationConfiguration().ChannelExternalId == "CHANNEL_1");
            Assert.Contains(contentSyncManagedWorkStatuses, j => j.DeserialiseContentSynchronisationConfiguration().ChannelExternalId == "CHANNEL_2");

            Assert.Single(channelDiscoveryManagedWorkStatusesAfterRun);
            Assert.NotEqual(channelDiscoveryManagedWorkStatusesBeforeRun[0].WorkId, channelDiscoveryManagedWorkStatusesAfterRun[0].WorkId);
            var channelDiscoveryManagedWorkStatusAfterRunState = JsonSerializer.Deserialize<ChannelDiscoveryState>(channelDiscoveryManagedWorkStatusesAfterRun[0].State);
            Assert.Equal(scanner.OutgoingCursor, channelDiscoveryManagedWorkStatusAfterRunState.Cursor);
            Assert.Equal(0, channelDiscoveryManagedWorkStatusAfterRunState.LastBackOffDelaySeconds);
        }

        [Fact]
        public async Task IfChannelsProvided_BatchCursorIsProvided_ReturnsAbandoned_ContentSyncPerChannelNotAdded_NewChannelDiscoveryBatchWorkNotAdded()
        {
            var cancellationToken = CancellationToken.None;
            var incomingCursor = $"https://dummyCursorLink/{DateTimeOffset.UtcNow.Ticks}";

            var scanner = new ChannelDiscoveryAction
            {
                NewChannels = new List<Channel>
                {
                    new() { ExternalId = "CHANNEL_1", Title = "Channel 1"}
                },
                ChannelDiscoveryResultType = ChannelDiscoveryResultType.Abandoned
            };
            SUT.SelectChannelDiscoveryAction(scanner);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateChannelDiscoveryManagedWorkStatusModel(connector, incomingCursor);
            await SUT.SetWorkRunning(workMessage);

            using var servicesScope = Services.CreateScope();
            var workItem = servicesScope.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();

            var channelDiscoveryManagedWorkStatusesBeforeRun = await ContentManagerSutBase.GetWorkStatusManager(servicesScope.ServiceProvider)
                .GetWorkStatusesAsync(a => a.WorkType == ChannelDiscoveryOperation.WORK_TYPE, cancellationToken);
            Assert.Single(channelDiscoveryManagedWorkStatusesBeforeRun);
            var channelDiscoveryManagedWorkStatusBeforeRunState = JsonSerializer.Deserialize<ChannelDiscoveryState>(channelDiscoveryManagedWorkStatusesBeforeRun[0].State);
            Assert.Equal(incomingCursor, channelDiscoveryManagedWorkStatusBeforeRunState.Cursor);

            await workItem.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            var contentSyncManagedWorkStatuses = await ContentManagerSutBase.GetWorkStatusManager(servicesScope.ServiceProvider)
                .GetWorkStatusesAsync(a => a.WorkType == ContentSynchronisationOperation.WORK_TYPE, cancellationToken);

            var channelDiscoveryManagedWorkStatusesAfterRun = await ContentManagerSutBase.GetWorkStatusManager(servicesScope.ServiceProvider)
                .GetWorkStatusesAsync(a => a.WorkType == ChannelDiscoveryOperation.WORK_TYPE, cancellationToken);

            Assert.Empty(contentSyncManagedWorkStatuses);
            Assert.Equal(WorkResultType.Abandoned, workItem.ResultType);
            Assert.Equal(ManagedWorkStatuses.Running, workItem.WorkManager.WorkStatus.Status);

            // Only contains same Channel Discovery work as before, just abandoned
            Assert.Single(channelDiscoveryManagedWorkStatusesAfterRun);
            Assert.Equal(channelDiscoveryManagedWorkStatusesBeforeRun[0].WorkId, channelDiscoveryManagedWorkStatusesAfterRun[0].WorkId);
            Assert.Equal(ManagedWorkStatuses.Abandoned, channelDiscoveryManagedWorkStatusesAfterRun[0].Status);
        }

        [Fact]
        public async Task IfChannelWorkMissing_ContentSyncPerChannelAdded()
        {
            var cancellationToken = CancellationToken.None;

            var scanner = new ChannelDiscoveryAction
            {
                NewChannels = new()
                {
                    new Channel() { ExternalId = "CHANNEL_1", Title = "Channel 1" }
                }
            };
            SUT.SelectChannelDiscoveryAction(scanner);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateChannelDiscoveryManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            using var servicesScope1 = Services.CreateScope();
            var priorWorkItem = servicesScope1.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();
            await priorWorkItem.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            var priorWorks = await ContentManagerSutBase.GetWorkStatusManager(servicesScope1.ServiceProvider)
                .GetWorkStatusesAsync(j => j.WorkType == ContentSynchronisationOperation.WORK_TYPE, cancellationToken);

            var priorWork = priorWorks.Single();

            // Add new channel and run again
            scanner.NewChannels = new()
            {
                new Channel() { ExternalId = "CHANNEL_2", Title = "Channel 2" }
            };
            await SUT.SetWorkContinue(workMessage);

            using var servicesScope2 = Services.CreateScope();
            var afterWorkItem = servicesScope2.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();
            await afterWorkItem.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            var afterWorks = await ContentManagerSutBase.GetWorkStatusManager(servicesScope2.ServiceProvider)
                .GetWorkStatusesAsync(a => a.WorkType == ContentSynchronisationOperation.WORK_TYPE, cancellationToken);

            // Check results
            Assert.Single(priorWorks);
            Assert.Equal("CHANNEL_1", priorWork.DeserialiseContentSynchronisationConfiguration().ChannelExternalId);
            Assert.Equal(WorkResultType.Complete, priorWorkItem.ResultType);
            Assert.Equal(WorkResultType.Complete, afterWorkItem.ResultType);
            Assert.Equal(ManagedWorkStatuses.Running, afterWorkItem.WorkManager.WorkStatus.Status);
            Assert.Equal(2, afterWorks.Count);
            Assert.Contains(afterWorks, j => j.DeserialiseContentSynchronisationConfiguration().ChannelExternalId == "CHANNEL_1");
            Assert.Contains(afterWorks, j => j.DeserialiseContentSynchronisationConfiguration().ChannelExternalId == "CHANNEL_2");
        }

        [Fact]
        public async Task IfGlobalSemaphoreLockActive_IsDeferred()
        {
            var cancellationToken = CancellationToken.None;

            var scanner = new ChannelDiscoveryAction
            {
                ChannelDiscoveryResultType = ChannelDiscoveryResultType.BackOff,
                NextDelay = 30
            };
            SUT.SelectChannelDiscoveryAction(scanner);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateChannelDiscoveryManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            using var servicesScope1 = Services.CreateScope();
            var priorWorkItem = servicesScope1.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();
            await priorWorkItem.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            await SUT.SetWorkContinue(workMessage);
            using var servicesScope2 = Services.CreateScope();
            var afterWorkItem = servicesScope2.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();
            await afterWorkItem.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            // Check results
            Assert.Equal(WorkResultType.Deferred, priorWorkItem.ResultType);
            Assert.Equal(ManagedWorkStatuses.Running, priorWorkItem.WorkManager.WorkStatus.Status);
            Assert.Equal(WorkResultType.Deferred, afterWorkItem.ResultType);
            Assert.Equal(ManagedWorkStatuses.Running, afterWorkItem.WorkManager.WorkStatus.Status);
        }

        [Fact]
        public async Task IfScopedSemaphoreLockActive_IsDeferred()
        {
            var cancellationToken = CancellationToken.None;

            var scanner = new ChannelDiscoveryAction
            {
                ChannelDiscoveryResultType = ChannelDiscoveryResultType.BackOff,
                NextDelay = 30,
                SemaphoreLockType = SemaphoreLockType.Scoped
            };
            SUT.SelectChannelDiscoveryAction(scanner);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateChannelDiscoveryManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            using var servicesScope1 = Services.CreateScope();
            var priorWorkItem = servicesScope1.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();
            await priorWorkItem.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            await SUT.SetWorkContinue(workMessage);
            using var servicesScope2 = Services.CreateScope();
            var afterWorkItem = servicesScope2.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();
            await afterWorkItem.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            // Check results
            Assert.Equal(WorkResultType.Deferred, priorWorkItem.ResultType);
            Assert.Equal(ManagedWorkStatuses.Running, priorWorkItem.WorkManager.WorkStatus.Status);
            Assert.Equal(WorkResultType.Deferred, afterWorkItem.ResultType);
            Assert.Equal(ManagedWorkStatuses.Running, afterWorkItem.WorkManager.WorkStatus.Status);
        }

        [Fact]
        public async Task IfDifferentScopedSemaphoreLockActive_IsNotDeferred()
        {
            var cancellationToken = CancellationToken.None;

            var scanner = new ChannelDiscoveryAction
            {
                ChannelDiscoveryResultType = ChannelDiscoveryResultType.BackOff,
                NextDelay = 30,
                SemaphoreLockType = SemaphoreLockType.Scoped
            };
            SUT.SelectChannelDiscoveryAction(scanner);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateChannelDiscoveryManagedWorkStatusModel(connector);
            await SUT.SetWorkRunning(workMessage);

            using var servicesScope1 = Services.CreateScope();
            var priorWorkItem = servicesScope1.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();
            await priorWorkItem.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            SUT.SemaphoreLockScopedKeyAction.Key = "KEY_456";
            scanner.ChannelDiscoveryResultType = ChannelDiscoveryResultType.Complete;

            await SUT.SetWorkContinue(workMessage);
            using var servicesScope2 = Services.CreateScope();
            var afterWorkItem = servicesScope2.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();
            await afterWorkItem.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            // Check results
            Assert.Equal(WorkResultType.Deferred, priorWorkItem.ResultType);
            Assert.Equal(ManagedWorkStatuses.Running, priorWorkItem.WorkManager.WorkStatus.Status);
            Assert.Equal(WorkResultType.Complete, afterWorkItem.ResultType);
            Assert.Equal(ManagedWorkStatuses.Running, afterWorkItem.WorkManager.WorkStatus.Status);
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
            await SUT.SetWorkRunning(workMessage);

            using var servicesScope = Services.CreateScope();
            var workItem = servicesScope.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();

            await workItem.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            Assert.Equal(WorkResultType.Complete, workItem.ResultType);
            Assert.Equal(RETRY_COMPLETE_MSG, workItem.ResultReason);
            Assert.Equal(ManagedWorkStatuses.Running, workItem.WorkManager.WorkStatus.Status);
            Assert.NotNull(workItem.Exception);

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
            await SUT.SetWorkRunning(workMessage);

            using var servicesScope = Services.CreateScope();
            var workItem = servicesScope.ServiceProvider.GetRequiredService<ChannelDiscoveryOperation>();

            await workItem.RunWorkRequestAsync(SUT.CreateChannelDiscoveryRequest(workMessage), cancellationToken);

            Assert.Equal(WorkResultType.Failed, workItem.ResultType);
            Assert.Equal(ManagedWorkStatuses.Running, workItem.WorkManager.WorkStatus.Status);
            Assert.NotNull(workItem.Exception);

        }
    }
}

