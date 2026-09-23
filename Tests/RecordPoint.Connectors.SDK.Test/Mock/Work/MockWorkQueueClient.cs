using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;
using System.Collections.Concurrent;

namespace RecordPoint.Connectors.SDK.Test.Mock.Work
{

    /// <summary>
    /// Manager for the work queue
    /// </summary>
    public class MockWorkQueueClient(IQueueableWorkManager queueableWorkManager, IOptions<MockWorkQueueOptions> options, IDateTimeProvider dateTimeProvider) : IWorkQueueClient
    {
        public const int MAX_RECENT_JOBS = 100000; // Maximum number of works that can be kept
        public const int MAX_RECENT_JOB_SECONDS = 24 * 60 * 60; // How long a work is recent for in seconds

        /// <summary>
        /// Requests in the queue currently awaiting execution
        /// </summary>
        private readonly ConcurrentBag<WorkRequestState> _queuedRequests = [];

        /// <summary>
        /// Sumitted requests
        /// </summary>
        public BlockingCollection<WorkRequestState> SubmittedRequestStates { get; private set; } = [];

        /// <summary>
        /// Submitted requests
        /// </summary>
        public List<WorkRequest> SubmittedRequests => [..SubmittedRequestStates.Select(ws => ws.WorkRequest)];

        /// <summary>
        /// Complete requests
        /// </summary>
        public BlockingCollection<WorkRequestState> CompleteRequestStates { get; private set; } = [];

        /// <summary>
        /// Completed requests
        /// </summary>
        public List<WorkRequest> CompletedRequests => [..CompleteRequestStates.Select(ws => ws.WorkRequest)];


        public Task SubmitWorkAsync(WorkRequest workRequest, CancellationToken cancellationToken)
        {
            var workRequestState = new WorkRequestState()
            {
                WorkRequest = workRequest,
                EarliestTime = dateTimeProvider.UtcNow
            };
            SubmittedRequestStates.Add(workRequestState, cancellationToken);
            _queuedRequests.Add(workRequestState);

            return Task.CompletedTask;
        }

        public Task RunRequestsAsync(CancellationToken cancellationToken)
        {
            while (_queuedRequests.TryTake(out var request))
            {
                // Execute the request and discard it
                // The execute request function is responsible for resubmission
                var _ = ExecuteWorkRequest(request, cancellationToken);
            }
            return Task.CompletedTask;
        }


        /// <summary>
        /// Execute a work request
        /// </summary>
        protected async Task ExecuteWorkRequest(WorkRequestState input, CancellationToken cancellationToken)
        {
            // We can't execute yet so just add to back of the queue
            var currentTime = dateTimeProvider.UtcNow;
            if (input.EarliestTime > currentTime)
            {
                _queuedRequests.Add(input);
                return;
            }

            WorkResult outcome;
            Exception exception;
            try
            {
                outcome = await queueableWorkManager.HandleWorkRequestAsync(input.WorkRequest, cancellationToken).ConfigureAwait(false);
                exception = null;
            }
            catch (Exception ex)
            {
                exception = ex;
                outcome = WorkResult.Abandoned(ex.Message, ex);
            }

            // Work out the next request instance
            var retry = input.Retry + 1;
            var maxRetriesReached = retry >= options.Value.MaxRetries;
            var isComplete = maxRetriesReached || outcome.ResultType == WorkResultType.Complete || outcome.ResultType == WorkResultType.Failed;
            var retryTimeSpan = outcome.ResultType == WorkResultType.Deferred && outcome.WaitTill.HasValue ? outcome.WaitTill.Value - currentTime : options.Value.RetryTimespan;
            var earliestTime = currentTime + retryTimeSpan;
            var output = new WorkRequestState()
            {
                WorkRequest = input.WorkRequest,
                LastWorkResult = outcome,
                LastException = exception,
                Retry = retry,
                EarliestTime = earliestTime
            };

            // Complete the work
            if (isComplete)
            {
                // If we are complete add the requests to the complete requests so tests can examine it
                CompleteRequestStates.Add(output, cancellationToken);
            }
            else
            {
                // Otherwise add to the active requests for further processing
                _queuedRequests.Add(output);
            }

        }

        /// <summary>
        /// Wait till the condition is true
        /// </summary>
        /// <returns></returns>
        public async Task When(Func<MockWorkQueueClient, bool> condition, int maxDelay)
        {
            var cancelSource = new CancellationTokenSource();
            var ct = cancelSource.Token;
            var _ = Task.Delay(maxDelay).ContinueWith(t =>
            {
                cancelSource.Cancel();
            });

            // Take a copy of all requests that are submitted
            var currentRequests = SubmittedRequests.ToArray();

            var complete = condition(this);
            while (!complete)
            {
                ct.ThrowIfCancellationRequested();
                await Task.Delay(100, ct);
                complete = condition(this);
            }
        }

        /// <summary>
        /// Wait for all current work to be completed
        /// </summary>
        /// <returns></returns>
        public async Task WhenCurrentWorkCompleted(int maxDelay)
        {

            var cancelSource = new CancellationTokenSource();
            var ct = cancelSource.Token;
            _ = Task.Delay(maxDelay).ContinueWith(t =>
            {
                cancelSource.Cancel();
            });

            var currentRequests = SubmittedRequests.ToArray();
            bool isComplete() => CompletedRequests.Select(cr => cr.WorkId).Intersect(currentRequests.Select(sr => sr.WorkId)).Count() == currentRequests.Length;

            var complete = isComplete();
            while (!complete)
            {
                await Task.Delay(100, ct);
                complete = isComplete();
            }
        }

    }
}
