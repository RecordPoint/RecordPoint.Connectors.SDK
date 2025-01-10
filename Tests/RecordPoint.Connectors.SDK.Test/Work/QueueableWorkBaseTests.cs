using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Work
{

    public class QueueableWorkSut : CommonSutBase
    {

    }


    /// <summary>
    /// Tests for the queue work item base class
    /// </summary>
    public class QueueableWorkBaseTests : CommonTestBase<QueueableWorkSut>
    {

        /// <summary>
        /// Work Item that does nothing
        /// </summary>
        class NullQueueWorkItem : QueueableWorkBase<object>
        {

            public NullQueueWorkItem(
                IServiceProvider serviceProvider,
                ISystemContext systemContext,
                IScopeManager scopeManager,
                ILogger logger,
                ITelemetryTracker telemetryTracker,
                IDateTimeProvider dateTimeProvider)
                : base(serviceProvider, systemContext, scopeManager, logger, telemetryTracker, dateTimeProvider)
            { }

            public override string ServiceName => nameof(QueueableWorkBaseTests);

            public override string WorkType => nameof(NullQueueWorkItem);

            protected override object DeserializeParameter()
            {
                return null;
            }

            protected override Task InnerRunAsync(CancellationToken cancellationToken)
            {
                return Task.CompletedTask;
            }

        }

        public const string TestWorkId = "TestWorkId";

        public static DateTime TestSubmitDateTime { get; set; } = new(2020, 2, 3, 4, 5, 6);

        public static WorkRequest CreateTestWorkRequest(string workType)
        {
            var workRequest = new WorkRequest()
            {
                WorkId = TestWorkId,
                WorkType = workType,
                SubmitDateTime = TestSubmitDateTime
            };
            return workRequest;
        }

        [Fact]
        public async Task WorkID_InheritedFromRequest()
        {
            try
            {
                await StartSutAsync();
                var workRequest = CreateTestWorkRequest(nameof(NullQueueWorkItem));

                var workItem = new NullQueueWorkItem(Services, Services.GetRequiredService<ISystemContext>(), Services.GetRequiredService<IScopeManager>(), Services.GetRequiredService<ILogger<NullQueueWorkItem>>(), Services.GetRequiredService<ITelemetryTracker>(), Services.GetRequiredService<IDateTimeProvider>());
                await workItem.RunWorkRequestAsync(workRequest, CancellationToken.None);

                Assert.Equal(TestWorkId, workItem.Id);
            }
            finally
            {
                await StopSUTAsync();
            }
        }

        [Fact]
        public async Task SubmitDateTime_InheritedFromRequest()
        {
            try
            {
                await StartSutAsync();
                var workRequest = CreateTestWorkRequest(nameof(NullQueueWorkItem));

                var workItem = new NullQueueWorkItem(Services, Services.GetRequiredService<ISystemContext>(), Services.GetRequiredService<IScopeManager>(), Services.GetRequiredService<ILogger<NullQueueWorkItem>>(), Services.GetRequiredService<ITelemetryTracker>(), Services.GetRequiredService<IDateTimeProvider>());
                await workItem.RunWorkRequestAsync(workRequest, CancellationToken.None);

                Assert.Equal(TestSubmitDateTime, workItem.SubmitDateTime);
            }
            finally
            {
                await StopSUTAsync();
            }
        }

        [Fact]
        public async Task StartDateTime_IsCurrentTime()
        {
            try
            {
                await StartSutAsync();
                var workRequest = CreateTestWorkRequest(nameof(NullQueueWorkItem));
                var dateTimeProvider = Services.GetRequiredService<IDateTimeProvider>();
                var startTime = dateTimeProvider.UtcNow;

                var workItem = new NullQueueWorkItem(Services, Services.GetRequiredService<ISystemContext>(), Services.GetRequiredService<IScopeManager>(), Services.GetRequiredService<ILogger<NullQueueWorkItem>>(), Services.GetRequiredService<ITelemetryTracker>(), Services.GetRequiredService<IDateTimeProvider>());
                await workItem.RunWorkRequestAsync(workRequest, CancellationToken.None);

                Assert.True(workItem.StartDateTime - startTime < TimeSpan.FromSeconds(1));
            }
            finally
            {
                await StopSUTAsync();
            }
        }

        [Fact]
        public async Task WorkType_MatchesWorkItemType()
        {
            try
            {
                await StartSutAsync();
                var workType = nameof(NullQueueWorkItem);
                var workRequest = CreateTestWorkRequest(workType);
                var dateTimeProvider = Services.GetRequiredService<IDateTimeProvider>();
                var startTime = dateTimeProvider.UtcNow;

                var workItem = new NullQueueWorkItem(Services, Services.GetRequiredService<ISystemContext>(), Services.GetRequiredService<IScopeManager>(), Services.GetRequiredService<ILogger<NullQueueWorkItem>>(), Services.GetRequiredService<ITelemetryTracker>(), Services.GetRequiredService<IDateTimeProvider>());
                await workItem.RunWorkRequestAsync(workRequest, CancellationToken.None);

                Assert.Equal(workType, workItem.WorkType);
            }
            finally
            {
                await StopSUTAsync();
            }
        }

        [Fact]
        public async Task RequestWorkTypeMismatch_ThrowInvalidOperation()
        {
            try
            {
                await StartSutAsync();
                var workType = nameof(NullQueueWorkItem);
                var workRequest = CreateTestWorkRequest(workType);
                workRequest.WorkType = "IncorrectWorkType";
                var dateTimeProvider = Services.GetRequiredService<IDateTimeProvider>();
                var startTime = dateTimeProvider.UtcNow;

                var workItem = new NullQueueWorkItem(Services, Services.GetRequiredService<ISystemContext>(), Services.GetRequiredService<IScopeManager>(), Services.GetRequiredService<ILogger<NullQueueWorkItem>>(), Services.GetRequiredService<ITelemetryTracker>(), Services.GetRequiredService<IDateTimeProvider>());
                await Assert.ThrowsAsync<InvalidOperationException>(() => workItem.RunWorkRequestAsync(workRequest, CancellationToken.None));
            }
            finally
            {
                await StopSUTAsync();
            }
        }

        [Fact]
        public async Task WorkItem_DisposeSuccess()
        {
            try
            {
                await StartSutAsync();
                var workType = nameof(NullQueueWorkItem);
                var workRequest = CreateTestWorkRequest(workType);
                var dateTimeProvider = Services.GetRequiredService<IDateTimeProvider>();
                var startTime = dateTimeProvider.UtcNow;

                NullQueueWorkItem workItem;

                using (workItem = new NullQueueWorkItem(Services, Services.GetRequiredService<ISystemContext>(), Services.GetRequiredService<IScopeManager>(), Services.GetRequiredService<ILogger<NullQueueWorkItem>>(), Services.GetRequiredService<ITelemetryTracker>(), Services.GetRequiredService<IDateTimeProvider>())) {
                    Assert.False(workItem.HasDisposed);
                }

                Assert.Throws<ObjectDisposedException>(() => workItem.HasDisposed);
            }
            finally
            {
                await StopSUTAsync();
            }
        }
    }
}
