#nullable enable
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.Test.Mock
{
    /// <summary>
    /// A minimal <see cref="IQueueableWork"/> stub used to drive processor creation
    /// in the <c>AzureServiceBusWorkServer</c> tests.
    /// </summary>
    public class StubQueueableWork : IQueueableWork
    {
        public string WorkType { get; set; } = "Test Work";
        public string Id => "stub-id";
        public DateTimeOffset StartDateTime => DateTimeOffset.MinValue;
        public bool HasResult => false;
        public WorkResultType ResultType => WorkResultType.Complete;
        public string ResultReason => string.Empty;
        public string ResultReasonDetails => string.Empty;
        public DateTimeOffset FinishDateTime => DateTimeOffset.MinValue;
        public Exception Exception => null!;
        public TimeSpan WorkDuration => TimeSpan.Zero;
        public WorkRequest WorkRequest => new();

        public WorkResult GetWorkResult() => WorkResult.Complete();

        public Task RunWorkRequestAsync(WorkRequest workRequest, CancellationToken cancellationToken) => Task.CompletedTask;

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// A second distinct stub type so tests can register multiple operation types.
    /// </summary>
    public class SecondStubQueueableWork : StubQueueableWork
    {
        public SecondStubQueueableWork()
        {
            WorkType = "Second Work";
        }
    }

    /// <summary>
    /// A type that does NOT implement IQueueableWork, for validation tests.
    /// </summary>
    public class NotQueueableWork
    {
    }
}
