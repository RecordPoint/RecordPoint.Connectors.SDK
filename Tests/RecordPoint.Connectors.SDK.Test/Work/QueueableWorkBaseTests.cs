using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Caching.Semaphore;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Work
{
    public class QueueableWorkSut : CommonSutBase
    {
        protected override IHostBuilder CreateSutBuilder()
        {
            return base
                .CreateSutBuilder()
                .UseInMemorySemaphoreLock();
        }
    }

    public class QueueableWorkBaseTests : CommonTestBase<QueueableWorkSut>
    {
        class NullQueueWorkItem : QueueableWorkBase<object>
        {
            public NullQueueWorkItem(
                IServiceProvider serviceProvider,
                ISystemContext systemContext,
                IObservabilityScope observabilityScope,
                ITelemetryTracker telemetryTracker,
                IDateTimeProvider dateTimeProvider)
                : base(serviceProvider, systemContext, observabilityScope, telemetryTracker, dateTimeProvider)
            { }

            public override string ServiceName => nameof(QueueableWorkBaseTests);
            public override string WorkType => nameof(NullQueueWorkItem);

            protected override object DeserializeParameter() => null;
            protected override void InnerDispose() { }
            protected override Task InnerRunAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        }

        public const string TestWorkId = "TestWorkId";
        public static DateTime TestSubmitDateTime { get; set; } = new(2020, 2, 3, 4, 5, 6);

        public static WorkRequest CreateTestWorkRequest(string workType)
        {
            return new WorkRequest()
            {
                WorkId = TestWorkId,
                WorkType = workType,
                SubmitDateTime = TestSubmitDateTime
            };
        }

        [Fact]
        public async Task WorkID_InheritedFromRequest()
        {
            try
            {
                await StartSutAsync();
                var workRequest = CreateTestWorkRequest(nameof(NullQueueWorkItem));
                var workItem = new NullQueueWorkItem(Services, Services.GetRequiredService<ISystemContext>(), Services.GetRequiredService<IObservabilityScope>(), Services.GetRequiredService<ITelemetryTracker>(), Services.GetRequiredService<IDateTimeProvider>());
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
                var workItem = new NullQueueWorkItem(Services, Services.GetRequiredService<ISystemContext>(), Services.GetRequiredService<IObservabilityScope>(), Services.GetRequiredService<ITelemetryTracker>(), Services.GetRequiredService<IDateTimeProvider>());
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
                var workItem = new NullQueueWorkItem(Services, Services.GetRequiredService<ISystemContext>(), Services.GetRequiredService<IObservabilityScope>(), Services.GetRequiredService<ITelemetryTracker>(), Services.GetRequiredService<IDateTimeProvider>());
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
                var workItem = new NullQueueWorkItem(Services, Services.GetRequiredService<ISystemContext>(), Services.GetRequiredService<IObservabilityScope>(), Services.GetRequiredService<ITelemetryTracker>(), Services.GetRequiredService<IDateTimeProvider>());
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
                var workRequest = CreateTestWorkRequest(nameof(NullQueueWorkItem));
                workRequest.WorkType = "IncorrectWorkType";
                var workItem = new NullQueueWorkItem(Services, Services.GetRequiredService<ISystemContext>(), Services.GetRequiredService<IObservabilityScope>(), Services.GetRequiredService<ITelemetryTracker>(), Services.GetRequiredService<IDateTimeProvider>());
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
                var workRequest = CreateTestWorkRequest(nameof(NullQueueWorkItem));
                NullQueueWorkItem workItem;
                using (workItem = new NullQueueWorkItem(Services, Services.GetRequiredService<ISystemContext>(), Services.GetRequiredService<IObservabilityScope>(), Services.GetRequiredService<ITelemetryTracker>(), Services.GetRequiredService<IDateTimeProvider>()))
                {
                    Assert.False(workItem.HasDisposed);
                }
                Assert.Throws<ObjectDisposedException>(() => workItem.HasDisposed);
            }
            finally
            {
                await StopSUTAsync();
            }
        }

        [Fact]
        public async Task HandleBackOffResultAsync_Prevents_Lock_Extension()
        {
            var cancellationToken = CancellationToken.None;
            await StartSutAsync();

            try
            {
                var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();
                var config = new RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel { TenantId = "testTenant" };
                semaphoreLockManager.ConnectorConfiguration = config;

                var workItem1 = new TestableQueueWorkItem(Services, Services.GetRequiredService<ISystemContext>(), Services.GetRequiredService<IObservabilityScope>(), Services.GetRequiredService<ITelemetryTracker>(), Services.GetRequiredService<IDateTimeProvider>());
                workItem1.InitializeWorkRequest(CreateTestWorkRequest(nameof(TestableQueueWorkItem)));

                // Set a 600s lock
                await workItem1.CallHandleBackOffResultAsync(config, null, SemaphoreLockType.Global, 600, cancellationToken);
                var lockAfterFirst = await semaphoreLockManager.GetSemaphoreAsync(nameof(TestableQueueWorkItem), null, cancellationToken);
                Assert.NotNull(lockAfterFirst);
                Assert.True(lockAfterFirst.Value > DateTimeOffset.Now.AddSeconds(500),
                    $"First lock should be ~600s from now, but was {lockAfterFirst}");

                // Try to set a shorter 300s lock - should be prevented by Fix 1
                var workItem2 = new TestableQueueWorkItem(Services, Services.GetRequiredService<ISystemContext>(), Services.GetRequiredService<IObservabilityScope>(), Services.GetRequiredService<ITelemetryTracker>(), Services.GetRequiredService<IDateTimeProvider>());
                workItem2.InitializeWorkRequest(CreateTestWorkRequest(nameof(TestableQueueWorkItem)));

                await workItem2.CallHandleBackOffResultAsync(config, null, SemaphoreLockType.Global, 300, cancellationToken);

                // Verify the semaphore lock was NOT overwritten
                var lockAfterSecond = await semaphoreLockManager.GetSemaphoreAsync(nameof(TestableQueueWorkItem), null, cancellationToken);
                Assert.NotNull(lockAfterSecond);
                var timeDiff = Math.Abs((lockAfterSecond.Value - lockAfterFirst.Value).TotalSeconds);
                Assert.True(timeDiff < 1,
                    $"Lock should not be updated. After first: {lockAfterFirst}, After second: {lockAfterSecond}, Difference: {timeDiff}s");
            }
            finally
            {
                await StopSUTAsync();
            }
        }

        [Fact]
        public async Task HandleBackOffResultAsync_Sets_Lock_When_None_Exists()
        {
            var cancellationToken = CancellationToken.None;
            await StartSutAsync();

            try
            {
                var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();
                var config = new RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel { TenantId = "testTenant" };
                semaphoreLockManager.ConnectorConfiguration = config;

                // Verify no lock exists initially
                var initialLock = await semaphoreLockManager.GetSemaphoreAsync(nameof(TestableQueueWorkItem), null, cancellationToken);
                Assert.Null(initialLock);

                var workItem = new TestableQueueWorkItem(Services, Services.GetRequiredService<ISystemContext>(), Services.GetRequiredService<IObservabilityScope>(), Services.GetRequiredService<ITelemetryTracker>(), Services.GetRequiredService<IDateTimeProvider>());
                workItem.InitializeWorkRequest(CreateTestWorkRequest(nameof(TestableQueueWorkItem)));

                await workItem.CallHandleBackOffResultAsync(config, null, SemaphoreLockType.Global, 600, cancellationToken);

                // Verify lock was set
                var lockAfterSet = await semaphoreLockManager.GetSemaphoreAsync(nameof(TestableQueueWorkItem), null, cancellationToken);
                Assert.NotNull(lockAfterSet);
                Assert.True(lockAfterSet.Value > DateTimeOffset.Now.AddSeconds(500),
                    $"Lock should be ~600s from now, but was {lockAfterSet}");

                // Verify the work item was deferred
                var deferredTime = workItem.GetDeferredTime();
                Assert.NotNull(deferredTime);
                Assert.True(deferredTime.Value > DateTimeOffset.Now.AddSeconds(500),
                    $"Deferred time should be ~600s from now, but was {deferredTime}");
            }
            finally
            {
                await StopSUTAsync();
            }
        }

        [Fact]
        public async Task HandleBackOffResultAsync_Applies_Jitter()
        {
            var cancellationToken = CancellationToken.None;
            await StartSutAsync();

            try
            {
                var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();
                var config = new RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel { TenantId = "testTenant" };
                semaphoreLockManager.ConnectorConfiguration = config;

                var workItem = new TestableQueueWorkItem(Services, Services.GetRequiredService<ISystemContext>(), Services.GetRequiredService<IObservabilityScope>(), Services.GetRequiredService<ITelemetryTracker>(), Services.GetRequiredService<IDateTimeProvider>());
                workItem.InitializeWorkRequest(CreateTestWorkRequest(nameof(TestableQueueWorkItem)));

                await workItem.CallHandleBackOffResultAsync(config, null, SemaphoreLockType.Global, 100, cancellationToken);

                var deferredTime = workItem.GetDeferredTime();
                Assert.NotNull(deferredTime);

                // Delay should be between 100s and 130s (100 + 0-30 jitter)
                var delaySeconds = (deferredTime.Value - DateTimeOffset.Now).TotalSeconds;
                Assert.True(delaySeconds >= 99 && delaySeconds <= 131,
                    $"Delay should be 100-130s with jitter, but was {delaySeconds}s");
            }
            finally
            {
                await StopSUTAsync();
            }
        }

        [Fact]
        public async Task HandleBackOffResultAsync_Caps_At_Max_Delay()
        {
            var cancellationToken = CancellationToken.None;
            await StartSutAsync();

            try
            {
                var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();
                var config = new RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel { TenantId = "testTenant" };
                semaphoreLockManager.ConnectorConfiguration = config;

                var workItem = new TestableQueueWorkItem(Services, Services.GetRequiredService<ISystemContext>(), Services.GetRequiredService<IObservabilityScope>(), Services.GetRequiredService<ITelemetryTracker>(), Services.GetRequiredService<IDateTimeProvider>());
                workItem.InitializeWorkRequest(CreateTestWorkRequest(nameof(TestableQueueWorkItem)));

                // Request a delay larger than DEFAULT_MAX_BACKOFF_DELAY_SECONDS (3600), with no override
                await workItem.CallHandleBackOffResultAsync(config, null, SemaphoreLockType.Global, 4000, cancellationToken);

                var deferredTime = workItem.GetDeferredTime();
                Assert.NotNull(deferredTime);

                // The ceiling bounds the REQUESTED delay; jitter is then added on top, so the effective
                // delay can exceed it by up to MAX_JITTER_SECONDS. Asserting a hard 3600 here would be
                // asserting that jitter is discarded, which is the behaviour this ordering deliberately fixes.
                var ceiling = QueueableWorkBase<object>.DEFAULT_MAX_BACKOFF_DELAY_SECONDS;
                var delaySeconds = (deferredTime.Value - DateTimeOffset.Now).TotalSeconds;
                Assert.True(delaySeconds <= ceiling + 31,
                    $"Delay should be capped at {ceiling}s plus jitter, but was {delaySeconds}s");
                Assert.True(delaySeconds > ceiling - 60,
                    $"Delay should be at the {ceiling}s ceiling, but was {delaySeconds}s");
            }
            finally
            {
                await StopSUTAsync();
            }
        }

        [Fact]
        public async Task HandleBackOffResultAsync_MaxNextDelay_Raises_The_Ceiling()
        {
            var cancellationToken = CancellationToken.None;
            await StartSutAsync();

            try
            {
                var config = new RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel { TenantId = "testTenant" };
                Services.GetRequiredService<ISemaphoreLockManager>().ConnectorConfiguration = config;

                var workItem = new TestableQueueWorkItem(Services, Services.GetRequiredService<ISystemContext>(), Services.GetRequiredService<IObservabilityScope>(), Services.GetRequiredService<ITelemetryTracker>(), Services.GetRequiredService<IDateTimeProvider>());
                workItem.InitializeWorkRequest(CreateTestWorkRequest(nameof(TestableQueueWorkItem)));

                // A day, which the default 3600s ceiling would otherwise clamp away
                await workItem.CallHandleBackOffResultAsync(config, null, SemaphoreLockType.Global, 86400, cancellationToken, 86400);

                var deferredTime = workItem.GetDeferredTime();
                Assert.NotNull(deferredTime);

                var delaySeconds = (deferredTime.Value - DateTimeOffset.Now).TotalSeconds;
                Assert.True(delaySeconds > 86000 && delaySeconds <= 86431,
                    $"Delay should be ~86400s when the override permits it, but was {delaySeconds}s");
            }
            finally
            {
                await StopSUTAsync();
            }
        }

        [Fact]
        public async Task HandleBackOffResultAsync_MaxNextDelay_Can_Lower_The_Ceiling()
        {
            var cancellationToken = CancellationToken.None;
            await StartSutAsync();

            try
            {
                var config = new RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel { TenantId = "testTenant" };
                Services.GetRequiredService<ISemaphoreLockManager>().ConnectorConfiguration = config;

                var workItem = new TestableQueueWorkItem(Services, Services.GetRequiredService<ISystemContext>(), Services.GetRequiredService<IObservabilityScope>(), Services.GetRequiredService<ITelemetryTracker>(), Services.GetRequiredService<IDateTimeProvider>());
                workItem.InitializeWorkRequest(CreateTestWorkRequest(nameof(TestableQueueWorkItem)));

                await workItem.CallHandleBackOffResultAsync(config, null, SemaphoreLockType.Global, 600, cancellationToken, 60);

                var deferredTime = workItem.GetDeferredTime();
                Assert.NotNull(deferredTime);

                var delaySeconds = (deferredTime.Value - DateTimeOffset.Now).TotalSeconds;
                Assert.True(delaySeconds >= 59 && delaySeconds <= 91,
                    $"Delay should be clamped to the lowered 60s ceiling, but was {delaySeconds}s");
            }
            finally
            {
                await StopSUTAsync();
            }
        }

        [Fact]
        public async Task HandleBackOffResultAsync_MaxNextDelay_Clamped_To_Absolute_Maximum()
        {
            var cancellationToken = CancellationToken.None;
            await StartSutAsync();

            try
            {
                var config = new RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel { TenantId = "testTenant" };
                Services.GetRequiredService<ISemaphoreLockManager>().ConnectorConfiguration = config;

                var workItem = new TestableQueueWorkItem(Services, Services.GetRequiredService<ISystemContext>(), Services.GetRequiredService<IObservabilityScope>(), Services.GetRequiredService<ITelemetryTracker>(), Services.GetRequiredService<IDateTimeProvider>());
                workItem.InitializeWorkRequest(CreateTestWorkRequest(nameof(TestableQueueWorkItem)));

                // A fat-fingered override must not hold the lock indefinitely
                await workItem.CallHandleBackOffResultAsync(config, null, SemaphoreLockType.Global, int.MaxValue, cancellationToken, int.MaxValue);

                var deferredTime = workItem.GetDeferredTime();
                Assert.NotNull(deferredTime);

                var absoluteMax = QueueableWorkBase<object>.ABSOLUTE_MAX_BACKOFF_DELAY_SECONDS;
                var delaySeconds = (deferredTime.Value - DateTimeOffset.Now).TotalSeconds;
                Assert.True(delaySeconds <= absoluteMax + 31,
                    $"Delay should be clamped to {absoluteMax}s, but was {delaySeconds}s");
                // The lower bound is what proves the absolute-maximum path ran at all: an upper bound alone
                // is satisfied by the default 3600s ceiling, so the override could silently stop being
                // honoured without failing this test.
                Assert.True(delaySeconds > absoluteMax - 60,
                    $"Delay should sit at the {absoluteMax}s absolute maximum, not fall back to the default ceiling, but was {delaySeconds}s");
            }
            finally
            {
                await StopSUTAsync();
            }
        }

        [Fact]
        public async Task HandleBackOffResultAsync_Applies_Jitter_At_The_Ceiling()
        {
            var cancellationToken = CancellationToken.None;
            await StartSutAsync();

            try
            {
                var config = new RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel { TenantId = "testTenant" };
                Services.GetRequiredService<ISemaphoreLockManager>().ConnectorConfiguration = config;

                var ceiling = QueueableWorkBase<object>.DEFAULT_MAX_BACKOFF_DELAY_SECONDS;
                var sawJitter = false;

                // Before the clamp was reordered, a caller asking for exactly the ceiling got the ceiling
                // back with the jitter discarded, so all such callers re-queued in lockstep. Jitter is
                // 0-30 inclusive, so 12 attempts all landing on zero is not a realistic outcome.
                for (var attempt = 0; attempt < 12 && !sawJitter; attempt++)
                {
                    var workItem = new TestableQueueWorkItem(Services, Services.GetRequiredService<ISystemContext>(), Services.GetRequiredService<IObservabilityScope>(), Services.GetRequiredService<ITelemetryTracker>(), Services.GetRequiredService<IDateTimeProvider>());
                    workItem.InitializeWorkRequest(CreateTestWorkRequest(nameof(TestableQueueWorkItem)));

                    await workItem.CallHandleBackOffResultAsync(config, null, SemaphoreLockType.Global, ceiling, cancellationToken);

                    var deferredTime = workItem.GetDeferredTime();
                    Assert.NotNull(deferredTime);
                    if ((deferredTime.Value - DateTimeOffset.Now).TotalSeconds > ceiling) sawJitter = true;
                }

                Assert.True(sawJitter, $"Jitter should still apply when the requested delay equals the {ceiling}s ceiling");
            }
            finally
            {
                await StopSUTAsync();
            }
        }

        [Fact]
        public async Task HandleBackOffResultAsync_LegacyOverload_Uses_The_Default_Ceiling()
        {
            var cancellationToken = CancellationToken.None;
            await StartSutAsync();

            try
            {
                var config = new RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel { TenantId = "testTenant" };
                Services.GetRequiredService<ISemaphoreLockManager>().ConnectorConfiguration = config;

                var workItem = new TestableQueueWorkItem(Services, Services.GetRequiredService<ISystemContext>(), Services.GetRequiredService<IObservabilityScope>(), Services.GetRequiredService<ITelemetryTracker>(), Services.GetRequiredService<IDateTimeProvider>());
                workItem.InitializeWorkRequest(CreateTestWorkRequest(nameof(TestableQueueWorkItem)));

                // The overload without a ceiling argument must behave exactly as it did before
                await workItem.CallLegacyHandleBackOffResultAsync(config, null, SemaphoreLockType.Global, 86400, cancellationToken);

                var deferredTime = workItem.GetDeferredTime();
                Assert.NotNull(deferredTime);

                var ceiling = QueueableWorkBase<object>.DEFAULT_MAX_BACKOFF_DELAY_SECONDS;
                var delaySeconds = (deferredTime.Value - DateTimeOffset.Now).TotalSeconds;
                Assert.True(delaySeconds <= ceiling + 31,
                    $"Legacy overload should still clamp to {ceiling}s, but was {delaySeconds}s");
                Assert.True(delaySeconds > ceiling - 60,
                    $"Legacy overload should clamp TO the {ceiling}s ceiling, not below it, but was {delaySeconds}s");
            }
            finally
            {
                await StopSUTAsync();
            }
        }

        class TestableQueueWorkItem : QueueableWorkBase<object>
        {
            public TestableQueueWorkItem(
                IServiceProvider serviceProvider,
                ISystemContext systemContext,
                IObservabilityScope observabilityScope,
                ITelemetryTracker telemetryTracker,
                IDateTimeProvider dateTimeProvider)
                : base(serviceProvider, systemContext, observabilityScope, telemetryTracker, dateTimeProvider)
            { }

            public override string ServiceName => nameof(QueueableWorkBaseTests);
            public override string WorkType => nameof(TestableQueueWorkItem);

            protected override object DeserializeParameter() => null;
            protected override Task InnerRunAsync(CancellationToken cancellationToken) => Task.CompletedTask;
            protected override void InnerDispose() { }

            public async Task CallHandleBackOffResultAsync(
                RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel config,
                object context,
                SemaphoreLockType? lockType,
                int? delay,
                CancellationToken cancellationToken,
                int? maxDelay = null)
            {
                await HandleBackOffResultAsync(config, context, lockType, delay, maxDelay, cancellationToken);
            }

            public async Task CallLegacyHandleBackOffResultAsync(
                RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel config,
                object context,
                SemaphoreLockType? lockType,
                int? delay,
                CancellationToken cancellationToken)
            {
                // Binds the 5-argument overload deliberately: this stops the compile if that overload is
                // removed, which would break derived work items outside the SDK (e.g. Egnyte's
                // EventSynchronisationOperation).
                await HandleBackOffResultAsync(config, context, lockType, delay, cancellationToken);
            }

            public DateTimeOffset? GetDeferredTime() => WaitTill;

            public void InitializeWorkRequest(WorkRequest workRequest)
            {
                WorkRequest = workRequest;
                Id = workRequest.WorkId;
                SubmitDateTime = workRequest.SubmitDateTime;
            }
        }
    }
}