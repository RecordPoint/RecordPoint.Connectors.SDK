using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.Tests.SUT
{
    /// <summary>
    /// Worker that just collects all work requests
    /// </summary>
    public class TestWorkManager : IQueueableWorkManager
    {

        public List<WorkRequest> HandledRequests = new();

        public Task<WorkResult> HandleWorkRequestAsync(WorkRequest workRequest, CancellationToken cancellationToken)
        {
            HandledRequests.Add(workRequest);
            var outcome = new WorkResult()
            {
                ResultType = WorkResultType.Complete
            };
            return Task.FromResult(outcome);
        }

        public bool TryGetQueueableWorkFactory(string workType, out IQueueableWorkFactory queueableWorkFactory)
        {
            queueableWorkFactory = null;
            return false;
        }
    }
}
