﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Test.Mock.Work;
using RecordPoint.Connectors.SDK.Work;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Work
{

    public class WorkItemSut : CommonSutBase
    {
        protected override IHostBuilder CreateSutBuilder()
        {
            return base.CreateSutBuilder()
                .UseWorkManager()
                .UseWorkStateManager<DatabaseManagedWorkStatusManager>()
                .ConfigureServices(svcs =>
                    svcs.AddMockWorkQueue()
                        .AddAddQueueableWorkOperation<NullManagedQueueableWork>(nameof(NullManagedQueueableWork))
                );
        }
    }

    /// <summary>
    /// Work work item that does nothing
    /// </summary>
    class NullManagedQueueableWork : ManagedQueueableWorkBase<object, object>
    {

        public NullManagedQueueableWork(
            IServiceProvider serviceProvider,
            IManagedWorkFactory workFactory,
            ISystemContext systemContext,
            IScopeManager scopeManager,
            ILogger<NullManagedQueueableWork> logger,
            ITelemetryTracker telemetryTracker,
            IDateTimeProvider dateTimeProvider)
            : base(serviceProvider, workFactory, systemContext, scopeManager, logger, telemetryTracker, dateTimeProvider)
        { }

        public override string ServiceName => nameof(WorkWorkItemBaseTests);

        public override string WorkType => nameof(NullManagedQueueableWork);

        protected override object DeserializeConfiguration(string configurationType, string configurationText)
        {
            return null;
        }

        protected override object DeserializeState(string stateType, string stateText)
        {
            return null;
        }

        protected override Task InnerRunAsync(CancellationToken cancellationToken)
        {
            return CompleteAsync("Test", cancellationToken);
        }

        protected override (string, string) SerializeState(object state)
        {
            return ("", "");
        }
    }


    /// <summary>
    /// Tests for the work work items
    /// </summary>
    public class WorkWorkItemBaseTests : CommonTestBase<WorkItemSut>
    {

        [Fact]
        public async Task TryGetWorkWorkItemType_ReturnsFactory()
        {
            await StartSutAsync();

            var workType = nameof(NullManagedQueueableWork);
            var workManager = Services.GetRequiredService<IQueueableWorkManager>();
            var success = workManager.TryGetQueueableWorkFactory(workType, out var workItemFactory);
            Assert.True(success);
            Assert.Equal(workType, workItemFactory.WorkType);
        }

        [Fact]
        public async Task InvokeCreateWorkItem_ReturnsWorkItem()
        {
            await StartSutAsync();

            var workType = nameof(NullManagedQueueableWork);
            var workManager = Services.GetRequiredService<IQueueableWorkManager>();
            _ = workManager.TryGetQueueableWorkFactory(workType, out var workItemFactory);
            var workItem = workItemFactory.CreateWorkOperation();
            Assert.IsType<NullManagedQueueableWork>(workItem);
        }


    }

}
