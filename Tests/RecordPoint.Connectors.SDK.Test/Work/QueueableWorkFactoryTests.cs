using Microsoft.Extensions.DependencyInjection;
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

    /// <summary>
    /// SUT for work manager tests
    /// </summary>
    public class QueueableWorkFactorySUT : CommonSutBase
    {

        protected override IHostBuilder CreateSutBuilder()
        {
            return base.CreateSutBuilder()
                .UseWorkManager()
                .UseWorkStateManager<DatabaseManagedWorkStatusManager>()
                .ConfigureServices(svcs =>
                    svcs.AddMockWorkQueue()
                        .AddQueueableWorkOperation<NotImplementedWorkItem>()
                        .AddQueueableWorkOperation<CompletesWorkItem>()
                );
        }
    }

    public class NotImplementedWorkItem : IQueueableWork
    {
        public string WorkType => nameof(NotImplementedWorkItem);

        public string Id => throw new NotImplementedException();

        public DateTime SubmitDateTime => throw new NotImplementedException();

        public DateTimeOffset StartDateTime => throw new NotImplementedException();

        public WorkResult GetWorkResult() => throw new NotImplementedException();

        public WorkRequest WorkRequest => throw new NotImplementedException();

        public bool HasResult => throw new NotImplementedException();

        public WorkResultType ResultType => throw new NotImplementedException();

        public string ResultReason => throw new NotImplementedException();

        public DateTimeOffset FinishDateTime => throw new NotImplementedException();

        public Exception Exception => throw new NotImplementedException();

        public TimeSpan WorkDuration => throw new NotImplementedException();

        public Task RunAsync(WorkRequest workRequest, CancellationToken cancellationToken) => throw new NotImplementedException();

        public Task RunWorkRequestAsync(WorkRequest workRequest, CancellationToken cancellationToken) => throw new NotImplementedException();

        #region Disposable
        private bool hasDisposed = false;

        /// <summary>
        /// Dispose
        /// </summary>
        protected virtual void InnerDispose()
        {
            // free unmanaged resources (unmanaged objects) and override finalizer
            // set large fields to null
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (!hasDisposed)
            {
                InnerDispose();
                hasDisposed = true;
            }
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class CompletesWorkItem : QueueableWorkBase<object>
    {

        public CompletesWorkItem(
            IServiceProvider serviceProvider,
            ISystemContext systemContext,
            IObservabilityScope observabilityScope,
            ITelemetryTracker telemetryTracker,
            IDateTimeProvider dateTimeProvider)
            : base(serviceProvider, systemContext, observabilityScope, telemetryTracker, dateTimeProvider)
        {

        }

        public override string ServiceName => nameof(QueueableWorkFactoryTests);

        public override string WorkType => nameof(CompletesWorkItem);

        protected override object DeserializeParameter()
        {
            return null;
        }

        protected override void InnerDispose()
        {
        }

        protected override async Task InnerRunAsync(CancellationToken cancellationToken)
        {
            await CompleteAsync("Test", cancellationToken);
        }
    }


    /// <summary>
    /// Work manager tests
    /// </summary>
    public class QueueableWorkFactoryTests : CommonTestBase<QueueableWorkFactorySUT>
    {
        [Fact]
        public async Task HandleWorkRequest_SuccessfullyRuns()
        {
            await StartSutAsync();

            var workType = nameof(CompletesWorkItem);
            var workId = "TestWorkId";
            var workRequest = new WorkRequest()
            {
                WorkId = workId,
                WorkType = workType
            };
            var queueableWorkFactory = Services.GetRequiredService<IQueueableWorkManager>();
            var outcome = await queueableWorkFactory.HandleWorkRequestAsync(workRequest, CancellationToken.None);

            // Make sure we ran the request strategy
            Assert.Equal(WorkResultType.Complete, outcome.ResultType);
        }

    }
}
