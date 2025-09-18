using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Work
{
    /// <summary>
    /// Manages the state of Managed Work
    /// </summary>
    public class ManagedWorkManager : IManagedWorkManager
    {
        private readonly IManagedWorkStatusManager _managedWorkStatusManager;
        private readonly IWorkQueueClient _workQueueClient;
        private const int STATUS_DELAY_SECONDS = 1;

        private const string RETRY_COMPLETE_MSG = "Work operation is being retried";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="managedWorkStatusManager"></param>
        /// <param name="workQueueClient"></param>
        public ManagedWorkManager(
            IManagedWorkStatusManager managedWorkStatusManager,
            IWorkQueueClient workQueueClient)
        {
            _managedWorkStatusManager = managedWorkStatusManager;
            _workQueueClient = workQueueClient;
        }


        /// <summary>
        /// Status for this Managed Work
        /// </summary>
        public ManagedWorkStatusModel WorkStatus { get; set; } = new ManagedWorkStatusModel();

        // **Note** Ordering of events is important to resolve edge cases.

        // Lack of transactional updates mean there are possible race conditions
        // like the work request being accepted before the work status is updated.

        // Identified race conditions and their solutions are listed here.
        // They will be marked as TODO until a unit test is created

        // TODO - Work is executed before the work status is saved **
        // 
        // The work status is checked on work startup. If the Work ID's don't match
        // a transient error is raised. Work queue retry logic should give time for the issue to correct

        // TODO - Work is queued but the work status update fails
        // 
        // The work status is checked on work startup. A transient error is raised.
        // Eventually the request will dead-letter.


        /// <summary>
        /// Start the work running
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="waitTill"></param>
        /// <returns>Start Task</returns>
        public async Task StartAsync(CancellationToken cancellationToken, DateTimeOffset? waitTill = null)
        {
            // Does this work already exist?
            var existingWork = await _managedWorkStatusManager.GetWorkStatusAsync(WorkStatus.Id, cancellationToken);
            if (existingWork != null)
            {
                // Work has already been submitted.
                // Can happen due to receiving multiple submissions
                // Just do nothing

                // TODO Log this when implementing logging
                return;
            }

            // Its important that the message get sent then the database 
            // updated second to handle the edge case where the database update fails. The job is not
            // considered valid until the database is updated and will use the work queue retry logic
            // to hold execution off until it is ready.

            // Check cancellation token before hand and then run both operations together
            cancellationToken.ThrowIfCancellationRequested();

            var workStatus = new ManagedWorkStatusModel();
            WorkStatus.CopyTo(workStatus);
            var workRequest = new WorkRequest()
            {
                WorkId = workStatus.WorkId,
                WorkType = WorkStatus.WorkType,
                Body = workStatus.Serialize(),
                WaitTill = waitTill
            };

            // Submit the work before updating status. 
            await _workQueueClient.SubmitWorkAsync(workRequest, cancellationToken);

            // Important: Don't change work ID until after the message sent
            // Ignore the cancel to make this as atomic as possible
            var newWorkStatus = CreateWorkStatus();
            await _managedWorkStatusManager.AddWorkStatusAsync(newWorkStatus, cancellationToken);

        }

        /// <summary>
        /// Complete the work
        /// </summary>
        /// <param name="reason">The reason the Work is being completed</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Outcome to pass onto the work queue</returns>
        public async Task<WorkResult> CompleteAsync(string reason, CancellationToken cancellationToken)
        {
            await _managedWorkStatusManager.SetWorkCompleteAsync(WorkStatus.Id, cancellationToken);
            return WorkResult.Complete(reason);
        }

        /// <summary>
        /// Abandon the work
        /// </summary>
        /// <param name="reason">The reason the Work is being abandonded</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Outcome to pass onto the work queue</returns>
        public async Task<WorkResult> AbandonedAsync(string reason, CancellationToken cancellationToken)
        {
            await _managedWorkStatusManager.SetWorkAbandonedAsync(WorkStatus.Id, cancellationToken);
            return WorkResult.Abandoned(reason);
        }

        /// <summary>
        /// Check that work is still valid prior to performing the work
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The result to pass to the work queue if the work has failed its check, otherwise null.</returns>
        public async Task<WorkResult> CheckAsync(CancellationToken cancellationToken)
        {
            var workStatus = await _managedWorkStatusManager.GetWorkStatusAsync(WorkStatus.Id, cancellationToken);

            if (workStatus == null || workStatus.WorkId != WorkStatus.WorkId)
            {
                // Need to delay the work status check for race condition between workstatus and workqueue.
                workStatus = await await Task.Delay(STATUS_DELAY_SECONDS * 1000, cancellationToken).ContinueWith(async task =>
                {
                    return await _managedWorkStatusManager.GetWorkStatusAsync(WorkStatus.Id, cancellationToken);
                });

                if (workStatus == null || workStatus.WorkId != WorkStatus.WorkId)
                {
                    var reason = $"Job '{WorkStatus.Id}' not ready for work request '{WorkStatus.WorkId}'";
                    return WorkResult.Abandoned(reason);
                }
            }

            // If all checks pass return null to indicate so
            return null;
        }

        /// <summary>
        /// Continue the Work
        /// </summary>
        /// <param name="stateType">String that identifies the type of the state that was saved</param>
        /// <param name="state">Current progress state</param>
        /// <param name="waitTill">UTC time to wait till before continuing</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Outcome to pass onto the work queue</returns>
        public async Task<WorkResult> ContinueAsync(string stateType, string state, DateTimeOffset waitTill, CancellationToken cancellationToken)
        {
            // Check cancellation token before hand and then run both updates together
            cancellationToken.ThrowIfCancellationRequested();

            // Create a new job message and send it off
            // keep the same workId if job is retried
            var workId = Guid.NewGuid().ToString();
            var workStatus = new ManagedWorkStatusModel();
            WorkStatus.CopyTo(workStatus);
            workStatus.StateType = stateType;
            workStatus.State = state;
            workStatus.WorkId = workId;
            var workRequest = new WorkRequest()
            {
                WorkId = workId,
                WorkType = WorkStatus.WorkType,
                Body = workStatus.Serialize(),
                WaitTill = waitTill
            };

            await _workQueueClient.SubmitWorkAsync(workRequest, cancellationToken);

            // Important: Don't change work ID until after the message sent
            // Ignore the cancel to make this as atomic as possible
            await _managedWorkStatusManager.SetWorkContinueAsync(WorkStatus.Id, workId, state, CancellationToken.None);

            // The job keeps going but this unit of work is complete
            return WorkResult.Complete();
        }

        /// <summary>
        /// Retry the Work with the same content
        /// </summary>        
        /// <param name="waitTill">UTC time to wait till before continuing</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="faultedCount">number of faulty to control retries</param>
        /// <returns>Outcome to pass onto the work queue</returns>
        public async Task<WorkResult> RetryAsync(DateTimeOffset waitTill, CancellationToken cancellationToken, int? faultedCount = 0)
        {
            // Check cancellation token before hand and then run both updates together
            cancellationToken.ThrowIfCancellationRequested();
            var workStatus = new ManagedWorkStatusModel();
            WorkStatus.CopyTo(workStatus);
            var workRequest = new WorkRequest()
            {
                WorkId = WorkStatus.WorkId, // Keep the workId for retry
                WorkType = WorkStatus.WorkType,
                Body = workStatus.Serialize(),
                FaultedCount = faultedCount ?? 0,
                WaitTill = waitTill
            };

            await _workQueueClient.SubmitWorkAsync(workRequest, cancellationToken);

            // Important: Don't change work ID until after the message sent
            // Ignore the cancel to make this as atomic as possible
            await _managedWorkStatusManager.SetWorkContinueAsync(WorkStatus.Id, workRequest.WorkId, workStatus.State, CancellationToken.None);

            // The job keeps going but this unit of work is complete
            return WorkResult.Complete(RETRY_COMPLETE_MSG);
        }

        /// <summary>
        /// Set that the Work has permanently failed
        /// </summary>
        /// <param name="reason">Reason why the Work has failed</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Outcome to pass onto the work queue</returns>
        public async Task<WorkResult> FailedAsync(string reason, CancellationToken cancellationToken)
        {
            await _managedWorkStatusManager.SetWorkFailedAsync(WorkStatus.Id, cancellationToken);
            return WorkResult.Failed(reason);
        }

        /// <summary>
        /// Set that the Work has had a possibly transient fault
        /// </summary>
        /// <param name="reason">Reason why</param>
        /// <param name="exception">Exception</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="faultedCount">faulted count</param>
        /// <returns>Outcome to pass onto the work queue</returns>
        public async Task<WorkResult> FaultyAsync(string reason, Exception exception, CancellationToken cancellationToken, int? faultedCount = 0)
        {
            // Continue exponential backoff the job
            var faulted = faultedCount ?? 0;
            if (WorkStatus.RetryOnFailure)
            {
                if (WorkStatus.MaxRetries == -1 || faulted < WorkStatus.MaxRetries)
                {
                    faulted++;

                    //Exponential Back off now 0:30s , 2:50, 7:48, 16:00, 27:57, 44:05 with the 2.5 exponent with the retry delay of 30 seconds
                    var delay = WorkStatus.ExponentialRetryDelay
                        ? Math.Min(Math.Pow(faulted, 2.5) * WorkStatus.RetryDelay, WorkStatus.MaxRetryDelay)
                        : WorkStatus.RetryDelay;
                    return await RetryAsync(DateTimeOffset.UtcNow.AddSeconds(delay), cancellationToken, faulted).ConfigureAwait(false);
                }

                //The Work has failed the maximum allowed times, so mark the Work Status as Failed
                await _managedWorkStatusManager.SetWorkFailedAsync(WorkStatus.Id, cancellationToken).ConfigureAwait(false);
                return WorkResult.DeadLetter(reason, exception);
            }

            //The Work has failed so mark the Work Status as Failed
            await _managedWorkStatusManager.SetWorkFailedAsync(WorkStatus.Id, cancellationToken).ConfigureAwait(false);
            return WorkResult.Failed(reason, exception);
        }

        /// <summary>
        /// Create Work status from a Work message
        /// </summary>
        /// <returns>Managed Work status model</returns>
        private ManagedWorkStatusModel CreateWorkStatus()
        {
            var workStatus = new ManagedWorkStatusModel()
            {
                Id = WorkStatus.Id,
                WorkType = WorkStatus.WorkType,
                ConnectorId = WorkStatus.ConnectorId,
                Configuration = WorkStatus.Configuration,
                State = WorkStatus.State,
                ConfigurationType = WorkStatus.ConfigurationType,
                WorkId = WorkStatus.WorkId
            };
            return workStatus;
        }

        #region Disposal
        private bool disposedValue;

        /// <summary>
        /// Free managed resources
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    WorkStatus = null;
                }
                disposedValue = true;
            }
        }

        /// <summary>
        /// Dispose of the Managed Work Manager
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}
