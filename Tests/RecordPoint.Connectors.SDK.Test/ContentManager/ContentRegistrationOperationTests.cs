using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecordPoint.Connectors.SDK.Caching.Semaphore;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Work;
using Xunit;
using Record = RecordPoint.Connectors.SDK.Content.Record;

namespace RecordPoint.Connectors.SDK.Test.ContentManager
{

    /// <summary>
    /// Tests for the work work items
    /// </summary>
    public class ContentRegistrationOperationTests : CommonTestBase<ContentRegistrationOperationSut>
    {

        public class ContentRegistrationAction : IContentRegistrationAction
        {

            public List<Aggregation> Aggregations { get; set; } = new();
            public List<Record> Records { get; set; } = new();
            public ContentResultType ResultType { get; set; }
            public string Reason { get; set; } = string.Empty;
            public Exception Exception { get; set; }
            public string Cursor { get; set; } = string.Empty;
            public SemaphoreLockType SemaphoreLockType { get; set; } = SemaphoreLockType.Global;
            public int? NextDelay { get; set; } = null;

            public string ReportedContinueCursor { get; set; }
            public int BeginCalledCount { get; set; } = 0;
            public int ContinueCalledCount { get; set; } = 0;
            public int StopCalledCount { get; set; } = 0;


            public Task<ContentResult> BeginAsync(ConnectorConfigModel connectorConfiguration, Channel channel, IDictionary<string, string> context, CancellationToken cancellationToken)
            {
                BeginCalledCount++;
                return Task.FromResult(CreateContentResult());
            }

            public Task<ContentResult> ContinueAsync(ConnectorConfigModel connectorConfiguration, Channel channel, string cursor, IDictionary<string, string> context, CancellationToken cancellationToken)
            {
                ReportedContinueCursor = cursor;
                ContinueCalledCount++;
                return Task.FromResult(CreateContentResult());
            }

            public Task StopAsync(ConnectorConfigModel connectorConfiguration, Channel channel, string cursor, CancellationToken cancellationToken)
            {
                StopCalledCount++;
                return Task.CompletedTask;
            }

            private ContentResult CreateContentResult() => new()
            {
                Aggregations = Aggregations,
                Records = Records,
                ResultType = ResultType,
                Reason = Reason,
                Exception = Exception,
                Cursor = Cursor,
                SemaphoreLockType = SemaphoreLockType,
                NextDelay = NextDelay
            };
        }


        [Fact]
        public async Task IfRequestedConnectorMissing_CompletesWithNoUpdates()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            var channel = ContentManagerSutBase.CreateChannel1();

            var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);
            await SUT.SetWorkRunning(workMessage);

            var workItem = Services.GetRequiredService<ContentRegistrationOperation>();

            await workItem.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

            var workStatuses = await SUT.GetWorkStatusManager()
                .GetWorkStatusesAsync(j => j.WorkType == ContentRegistrationOperation.WORK_TYPE, cancellationToken);

            Assert.Equal(WorkResultType.Abandoned, workItem.ResultType);
            Assert.Equal("Connector not found", workItem.ResultReason);
            Assert.True(workStatuses.All(a => a.Status == ManagedWorkStatuses.Abandoned));
        }

        [Fact]
        public async Task IfChannelMissing_OperationIsAbandonded()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            var channel = ContentManagerSutBase.CreateChannel1();

            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);
            await SUT.SetWorkRunning(workMessage);

            var workItem = Services.GetRequiredService<ContentRegistrationOperation>();

            await workItem.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

            var workStatuses = await SUT.GetWorkStatusManager()
                .GetWorkStatusesAsync(j => j.WorkType == ContentRegistrationOperation.WORK_TYPE, cancellationToken);

            Assert.Equal(WorkResultType.Abandoned, workItem.ResultType);
            Assert.Equal("Channel not found", workItem.ResultReason);
            Assert.True(workStatuses.All(a => a.Status == ManagedWorkStatuses.Abandoned));
        }

        [Fact]
        public async Task OnFirstSyncWithConnectorDisabled_ContinuesWorkWithoutScanning()
        {
            var cancellationToken = CancellationToken.None;

            var testCursor1 = "TestCursor1";
            var scanner = new ContentRegistrationAction
            {
                ResultType = ContentResultType.Complete,
                Cursor = testCursor1
            };
            SUT.SelectContentRegistrationAction(scanner);
            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            ContentManagerSutBase.DisableConnector(connector, DateTimeOffset.Now);
            var channel = ContentManagerSutBase.CreateChannel1();

            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);
            await SUT.GetChannelManager().UpsertChannelAsync(channel, cancellationToken);

            var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);
            await SUT.SetWorkRunning(workMessage);

            var workItem = Services.GetRequiredService<ContentRegistrationOperation>();

            await workItem.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

            var workStatuses = await SUT.GetWorkStatusManager()
                .GetWorkStatusesAsync(j => j.WorkType == ContentRegistrationOperation.WORK_TYPE, cancellationToken);

            Assert.Equal(WorkResultType.Complete, workItem.ResultType);
            Assert.Equal("Connector disabled", workItem.ResultReason);
            Assert.Equal(0, scanner.BeginCalledCount);
            Assert.Equal(0, scanner.ContinueCalledCount);
            Assert.True(workStatuses.All(a => a.Status == ManagedWorkStatuses.Running));
        }

        [Fact]
        public async Task OnFirstSyncWithConnectorDisabled_ExceedsDisabledThreshold_AbandonsWork()
        {
            var cancellationToken = CancellationToken.None;

            var testCursor1 = "TestCursor1";
            var scanner = new ContentRegistrationAction
            {
                ResultType = ContentResultType.Complete,
                Cursor = testCursor1
            };
            SUT.SelectContentRegistrationAction(scanner);

            var myConfig = new Dictionary<string, string>
            {
                { "ContentManager:MaxDisabledConnectorAge", "1209600" },
            };

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfig)
                .Build();

            await StartSutAsync(config);

            var connector = ContentManagerSutBase.CreateConnector1();
            var channel = ContentManagerSutBase.CreateChannel1();
            ContentManagerSutBase.DisableConnector(connector, DateTimeOffset.Now.AddDays(-30));

            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);
            await SUT.GetChannelManager().UpsertChannelAsync(channel, cancellationToken);

            var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);
            await SUT.SetWorkRunning(workMessage);

            var workItem = Services.GetRequiredService<ContentRegistrationOperation>();

            await workItem.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

            var workStatuses = await SUT.GetWorkStatusManager()
                .GetWorkStatusesAsync(j => j.WorkType == ContentRegistrationOperation.WORK_TYPE, cancellationToken);

            Assert.Equal(WorkResultType.Abandoned, workItem.ResultType);
            Assert.Equal("Connector disabled", workItem.ResultReason);
            Assert.Equal(0, scanner.BeginCalledCount);
            Assert.Equal(0, scanner.ContinueCalledCount);
            Assert.True(workStatuses.All(a => a.Status == ManagedWorkStatuses.Abandoned));
        }

        [Fact]
        public async Task OnFirstSync_BeginsSync()
        {
            var cancellationToken = CancellationToken.None;

            var testCursor1 = "TestCursor1";
            var scanner = new ContentRegistrationAction
            {
                ResultType = ContentResultType.Incomplete,
                Cursor = testCursor1
            };
            SUT.SelectContentRegistrationAction(scanner);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            var channel = ContentManagerSutBase.CreateChannel1();

            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);
            await SUT.GetChannelManager().UpsertChannelAsync(channel, cancellationToken);

            var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);
            await SUT.SetWorkRunning(workMessage);

            var workItem = Services.GetRequiredService<ContentRegistrationOperation>();

            await workItem.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

            Assert.Equal(testCursor1, workItem.State.Cursor);
            Assert.Equal(WorkResultType.Complete, workItem.ResultType);
            Assert.Equal(ManagedWorkStatuses.Running, workItem.WorkManager.WorkStatus.Status);
            Assert.Equal(1, scanner.BeginCalledCount);
            Assert.Equal(0, scanner.ContinueCalledCount);
        }

        [Fact]
        public async Task OnSecondSyncRequest_ContinuesSyncWithCursor()
        {
            var cancellationToken = CancellationToken.None;

            var testCursor1 = "TestCursor1";
            var testCursor2 = "TestCursor2";
            var scanner = new ContentRegistrationAction
            {
                ResultType = ContentResultType.Incomplete,
                Cursor = testCursor1
            };
            SUT.SelectContentRegistrationAction(scanner);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            var channel = ContentManagerSutBase.CreateChannel1();

            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);
            await SUT.GetChannelManager().UpsertChannelAsync(channel, cancellationToken);

            var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);
            await SUT.SetWorkRunning(workMessage);

            var beginWorkItem = Services.GetRequiredService<ContentRegistrationOperation>();
            await beginWorkItem.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

            var beginWorkStatus = await SUT.GetWorkStatusManager()
                .GetWorkStatusesAsync(j => j.WorkType == ContentRegistrationOperation.WORK_TYPE, cancellationToken);

            var continueWorkMessage = beginWorkStatus.Single();

            Assert.Equal(testCursor1, beginWorkItem.State.Cursor);
            Assert.Equal(WorkResultType.Complete, beginWorkItem.ResultType);
            Assert.Equal(ManagedWorkStatuses.Running, beginWorkItem.WorkManager.WorkStatus.Status);
            Assert.Equal(1, scanner.BeginCalledCount);

            scanner.Cursor = testCursor2;
            await SUT.SetWorkContinue(continueWorkMessage);
            var continueWorkItem = Services.GetRequiredService<ContentRegistrationOperation>();
            await continueWorkItem.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(continueWorkMessage), cancellationToken);

            Assert.Equal(testCursor1, scanner.ReportedContinueCursor);
            Assert.Equal(testCursor2, continueWorkItem.State.Cursor);
            Assert.Equal(WorkResultType.Complete, continueWorkItem.ResultType);
            Assert.Equal(ManagedWorkStatuses.Running, continueWorkItem.WorkManager.WorkStatus.Status);
            Assert.Equal(1, scanner.ContinueCalledCount);
        }

        [Fact]
        public async Task OnSecondSyncWithConnectorDisabled_ContinuesWorkWithoutScanning()
        {
            var cancellationToken = CancellationToken.None;

            var testCursor1 = "TestCursor1";
            var testCursor2 = "TestCursor2";
            var scanner = new ContentRegistrationAction
            {
                ResultType = ContentResultType.Incomplete,
                Cursor = testCursor1
            };
            SUT.SelectContentRegistrationAction(scanner);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            var channel = ContentManagerSutBase.CreateChannel1();

            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);
            await SUT.GetChannelManager().UpsertChannelAsync(channel, cancellationToken);

            var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);
            await SUT.SetWorkRunning(workMessage);

            var beginWorkItem = Services.GetRequiredService<ContentRegistrationOperation>();
            await beginWorkItem.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

            var beginWorkStatus = await SUT.GetWorkStatusManager()
                .GetWorkStatusesAsync(j => j.WorkType == ContentRegistrationOperation.WORK_TYPE, cancellationToken);

            var continueWorkMessage = beginWorkStatus.Single();

            Assert.Equal(testCursor1, beginWorkItem.State.Cursor);
            Assert.Equal(WorkResultType.Complete, beginWorkItem.ResultType);
            Assert.Equal(ManagedWorkStatuses.Running, beginWorkItem.WorkManager.WorkStatus.Status);
            Assert.Equal(1, scanner.BeginCalledCount);

            ContentManagerSutBase.DisableConnector(connector, DateTimeOffset.Now);
            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

            scanner.Cursor = testCursor2;
            await SUT.SetWorkContinue(continueWorkMessage);
            var continueWorkItem = Services.GetRequiredService<ContentRegistrationOperation>();
            await continueWorkItem.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(continueWorkMessage), cancellationToken);

            var workStatuses = await SUT.GetWorkStatusManager()
                .GetWorkStatusesAsync(j => j.WorkType == ContentRegistrationOperation.WORK_TYPE, cancellationToken);

            Assert.Equal(WorkResultType.Complete, continueWorkItem.ResultType);
            Assert.Equal("Connector disabled", continueWorkItem.ResultReason);
            Assert.Equal(0, scanner.ContinueCalledCount);
            Assert.True(workStatuses.All(a => a.Status == ManagedWorkStatuses.Running));
        }

        [Fact]
        public async Task IfContinueScanFails_WorkCompletedWithExistingCursor_WhenRetryIsEnabled()
        {
            var cancellationToken = CancellationToken.None;

            var testCursor1 = "TestCursor1";
            var testCursor2 = "TestCursor2";
            var scanner = new ContentRegistrationAction
            {
                ResultType = ContentResultType.Incomplete,
                Cursor = testCursor1
            };
            SUT.SelectContentRegistrationAction(scanner);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            var channel = ContentManagerSutBase.CreateChannel1();

            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);
            await SUT.GetChannelManager().UpsertChannelAsync(channel, cancellationToken);

            var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);
            await SUT.SetWorkRunning(workMessage);

            var beginWorkItem = Services.GetRequiredService<ContentRegistrationOperation>();
            await beginWorkItem.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

            var beginWorkStatus = await SUT.GetWorkStatusManager()
                .GetWorkStatusesAsync(j => j.WorkType == ContentRegistrationOperation.WORK_TYPE, cancellationToken);

            var continueWorkMessage = beginWorkStatus.Single();

            Assert.Equal(testCursor1, beginWorkItem.State.Cursor);
            Assert.Equal(WorkResultType.Complete, beginWorkItem.ResultType);
            Assert.Equal(ManagedWorkStatuses.Running, beginWorkItem.WorkManager.WorkStatus.Status);
            Assert.Equal(1, scanner.BeginCalledCount);

            scanner.Cursor = testCursor2;
            scanner.ResultType = ContentResultType.Failed;
            await SUT.SetWorkContinue(continueWorkMessage);
            var continueWorkItem = Services.GetRequiredService<ContentRegistrationOperation>();
            await continueWorkItem.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(continueWorkMessage), cancellationToken);

            var workStatuses = await SUT.GetWorkStatusManager()
                .GetWorkStatusesAsync(j => j.WorkType == ContentRegistrationOperation.WORK_TYPE, cancellationToken);

            Assert.Equal(testCursor1, continueWorkItem.State.Cursor);
            Assert.Equal(WorkResultType.Complete, continueWorkItem.ResultType);
            Assert.Equal(ManagedWorkStatuses.Running, continueWorkItem.WorkManager.WorkStatus.Status);
            Assert.Equal(1, scanner.ContinueCalledCount);
            Assert.True(workStatuses.All(a => a.Status == ManagedWorkStatuses.Running));
        }

        [Fact]
        public async Task IfContinueScanFails_WorkFailedWithExistingCursor_WhenRetryIsDisabled()
        {
            var cancellationToken = CancellationToken.None;

            var testCursor1 = "TestCursor1";
            var testCursor2 = "TestCursor2";
            var scanner = new ContentRegistrationAction
            {
                ResultType = ContentResultType.Incomplete,
                Cursor = testCursor1
            };
            SUT.SelectContentRegistrationAction(scanner);

            var myConfig = new Dictionary<string, string>
            {
                { "Connector:RetryOnFailure", "False" },
            };

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfig)
                .Build();

            await StartSutAsync(config);

            var connector = ContentManagerSutBase.CreateConnector1();
            var channel = ContentManagerSutBase.CreateChannel1();

            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);
            await SUT.GetChannelManager().UpsertChannelAsync(channel, cancellationToken);

            var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);
            await SUT.SetWorkRunning(workMessage);

            var beginWorkItem = Services.GetRequiredService<ContentRegistrationOperation>();
            await beginWorkItem.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

            var beginWorkStatus = await SUT.GetWorkStatusManager()
                .GetWorkStatusesAsync(j => j.WorkType == ContentRegistrationOperation.WORK_TYPE, cancellationToken);

            var continueWorkMessage = beginWorkStatus.Single();

            Assert.Equal(testCursor1, beginWorkItem.State.Cursor);
            Assert.Equal(WorkResultType.Complete, beginWorkItem.ResultType);
            Assert.Equal(ManagedWorkStatuses.Running, beginWorkItem.WorkManager.WorkStatus.Status);
            Assert.Equal(1, scanner.BeginCalledCount);

            scanner.Cursor = testCursor2;
            scanner.ResultType = ContentResultType.Failed;
            await SUT.SetWorkContinue(continueWorkMessage);
            var continueWorkItem = Services.GetRequiredService<ContentRegistrationOperation>();
            await continueWorkItem.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(continueWorkMessage), cancellationToken);

            var workStatuses = await SUT.GetWorkStatusManager()
                .GetWorkStatusesAsync(j => j.WorkType == ContentRegistrationOperation.WORK_TYPE, cancellationToken);

            Assert.Equal(testCursor1, continueWorkItem.State.Cursor);
            Assert.Equal(WorkResultType.Failed, continueWorkItem.ResultType);
            Assert.Equal(ManagedWorkStatuses.Running, continueWorkItem.WorkManager.WorkStatus.Status);
            Assert.Equal(1, scanner.ContinueCalledCount);
            Assert.True(workStatuses.All(a => a.Status == ManagedWorkStatuses.Failed));
        }

        [Fact]
        public async Task IfGlobalSemaphoreLockActive_IsDeferred()
        {
            var cancellationToken = CancellationToken.None;

            var testCursor1 = "TestCursor1";
            var scanner = new ContentRegistrationAction
            {
                ResultType = ContentResultType.BackOff,
                NextDelay = 30
            };
            SUT.SelectContentRegistrationAction(scanner);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            var channel = ContentManagerSutBase.CreateChannel1();

            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);
            await SUT.GetChannelManager().UpsertChannelAsync(channel, cancellationToken);

            var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);
            await SUT.SetWorkRunning(workMessage);

            var beginWorkItem = Services.GetRequiredService<ContentRegistrationOperation>();
            await beginWorkItem.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

            var beginWorkStatus = await SUT.GetWorkStatusManager()
                .GetWorkStatusesAsync(j => j.WorkType == ContentRegistrationOperation.WORK_TYPE, cancellationToken);

            var continueWorkMessage = beginWorkStatus.Single();

            scanner.Cursor = testCursor1;
            await SUT.SetWorkContinue(continueWorkMessage);
            var continueWorkItem = Services.GetRequiredService<ContentRegistrationOperation>();
            await continueWorkItem.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(continueWorkMessage), cancellationToken);

            // Check results
            Assert.Equal(WorkResultType.Deferred, beginWorkItem.ResultType);
            Assert.Equal(ManagedWorkStatuses.Running, beginWorkItem.WorkManager.WorkStatus.Status);
            Assert.Equal(WorkResultType.Deferred, continueWorkItem.ResultType);
            Assert.Equal(ManagedWorkStatuses.Running, continueWorkItem.WorkManager.WorkStatus.Status);
        }

        [Fact]
        public async Task IfScopedSemaphoreLockActive_IsDeferred()
        {
            var cancellationToken = CancellationToken.None;

            var testCursor1 = "TestCursor1";
            var scanner = new ContentRegistrationAction
            {
                ResultType = ContentResultType.BackOff,
                NextDelay = 30,
                SemaphoreLockType = SemaphoreLockType.Scoped
            };
            SUT.SelectContentRegistrationAction(scanner);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            var channel = ContentManagerSutBase.CreateChannel1();

            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);
            await SUT.GetChannelManager().UpsertChannelAsync(channel, cancellationToken);

            var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);
            await SUT.SetWorkRunning(workMessage);

            var beginWorkItem = Services.GetRequiredService<ContentRegistrationOperation>();
            await beginWorkItem.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

            var beginWorkStatus = await SUT.GetWorkStatusManager()
                .GetWorkStatusesAsync(j => j.WorkType == ContentRegistrationOperation.WORK_TYPE, cancellationToken);

            var continueWorkMessage = beginWorkStatus.Single();

            scanner.Cursor = testCursor1;
            await SUT.SetWorkContinue(continueWorkMessage);
            var continueWorkItem = Services.GetRequiredService<ContentRegistrationOperation>();
            await continueWorkItem.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(continueWorkMessage), cancellationToken);

            // Check results
            Assert.Equal(WorkResultType.Deferred, beginWorkItem.ResultType);
            Assert.Equal(ManagedWorkStatuses.Running, beginWorkItem.WorkManager.WorkStatus.Status);
            Assert.Equal(WorkResultType.Deferred, continueWorkItem.ResultType);
            Assert.Equal(ManagedWorkStatuses.Running, continueWorkItem.WorkManager.WorkStatus.Status);
        }

        [Fact]
        public async Task IfDifferentScopedSemaphoreLockActive_IsNotDeferred()
        {
            var cancellationToken = CancellationToken.None;

            var testCursor1 = "TestCursor1";
            var scanner = new ContentRegistrationAction
            {
                ResultType = ContentResultType.BackOff,
                NextDelay = 30,
                SemaphoreLockType = SemaphoreLockType.Scoped
            };
            SUT.SelectContentRegistrationAction(scanner);

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            var channel = ContentManagerSutBase.CreateChannel1();

            await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);
            await SUT.GetChannelManager().UpsertChannelAsync(channel, cancellationToken);

            var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);
            await SUT.SetWorkRunning(workMessage);

            var beginWorkItem = Services.GetRequiredService<ContentRegistrationOperation>();
            await beginWorkItem.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

            var beginWorkStatus = await SUT.GetWorkStatusManager()
                .GetWorkStatusesAsync(j => j.WorkType == ContentRegistrationOperation.WORK_TYPE, cancellationToken);

            var continueWorkMessage = beginWorkStatus.Single();

            SUT.SemaphoreLockScopedKeyAction.Key = "KEY_456";
            scanner.Cursor = testCursor1;
            scanner.ResultType = ContentResultType.Complete;
            await SUT.SetWorkContinue(continueWorkMessage);
            var continueWorkItem = Services.GetRequiredService<ContentRegistrationOperation>();
            await continueWorkItem.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(continueWorkMessage), cancellationToken);

            // Check results
            Assert.Equal(WorkResultType.Deferred, beginWorkItem.ResultType);
            Assert.Equal(ManagedWorkStatuses.Running, beginWorkItem.WorkManager.WorkStatus.Status);
            Assert.Equal(WorkResultType.Complete, continueWorkItem.ResultType);
            Assert.Equal(ManagedWorkStatuses.Running, continueWorkItem.WorkManager.WorkStatus.Status);
        }
    }

}
