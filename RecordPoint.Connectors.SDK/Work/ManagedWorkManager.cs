using RecordPoint.Connectors.SDK.Providers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Work;

/// <summary>
/// Manages the state of Managed Work
/// </summary>
public class ManagedWorkManager(IWorkQueueClient workQueueClient, IDateTimeProvider dateTimeProvider) : IManagedWorkManager
{
    private const string RETRY_COMPLETE_MSG = "Work operation is being retried";

    /// <summary>
    /// Status for this Managed Work
    /// </summary>
    public ManagedWorkStatusModel WorkStatus { get; set; } = new ManagedWorkStatusModel();

    /// <summary>
    /// Start the work running
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="waitTill"></param>
    /// <returns>Start Task</returns>
    public async Task StartAsync(CancellationToken cancellationToken, DateTimeOffset? waitTill = null)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var workRequest = new WorkRequest()
        {
            ConnectorConfigId = WorkStatus.ConnectorId,
            TenantId = WorkStatus.TenantId,
            TenantDomainName = WorkStatus.TenantDomainName,
            WorkId = WorkStatus.WorkId,
            WorkType = WorkStatus.WorkType,
            Body = WorkStatus.Serialize(),
            WaitTill = waitTill
        };

        // Submit the work
        await workQueueClient.SubmitWorkAsync(workRequest, cancellationToken);
    }

    /// <summary>
    /// Complete the work
    /// </summary>
    /// <param name="reason">The reason the Work is being completed</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Outcome to pass onto the work queue</returns>
    public async Task<WorkResult> CompleteAsync(string reason, CancellationToken cancellationToken)
    {
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
        return WorkResult.Abandoned(reason);
    }

    /// <summary>
    /// Continue the Work
    /// </summary>
    /// <param name="configurationType">String that identifies the type of the configuration used</param>
    /// <param name="configuration">Configuration used to run the work</param>
    /// <param name="stateType">String that identifies the type of the state that was saved</param>
    /// <param name="state">Current progress state</param>
    /// <param name="waitTill">UTC time to wait till before continuing</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Outcome to pass onto the work queue</returns>
    public async Task<WorkResult> ContinueAsync(string configurationType, string configuration, string stateType, string state, DateTimeOffset waitTill, CancellationToken cancellationToken)
    {
        // Check cancellation token before hand and then run both updates together
        cancellationToken.ThrowIfCancellationRequested();

        // Create a new job message and send it off.
        // The WorkId is retained across continuations (and retries) so that the
        // owning entity can use it as a stable ownership token to detect and
        // discard superseded work requests.
        var workStatus = WorkStatus.Clone();
        workStatus.WorkRequestDate = dateTimeProvider.UtcNow;
        workStatus.ConfigurationType = configurationType;
        workStatus.Configuration = configuration;
        workStatus.StateType = stateType;
        workStatus.State = state;
        var workRequest = new WorkRequest()
        {
            ConnectorConfigId = workStatus.ConnectorId,
            TenantId = workStatus.TenantId,
            TenantDomainName = workStatus.TenantDomainName,
            WorkId = workStatus.WorkId,
            WorkType = workStatus.WorkType,
            Body = workStatus.Serialize(),
            WaitTill = waitTill
        };

        await workQueueClient.SubmitWorkAsync(workRequest, cancellationToken);

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

        var workRequest = new WorkRequest()
        {
            ConnectorConfigId = WorkStatus.ConnectorId,
            TenantId = WorkStatus.TenantId,
            TenantDomainName = WorkStatus.TenantDomainName,
            WorkId = WorkStatus.WorkId, // Keep the workId for retry
            WorkType = WorkStatus.WorkType,
            Body = WorkStatus.Serialize(),
            FaultedCount = faultedCount ?? 0,
            WaitTill = waitTill
        };

        await workQueueClient.SubmitWorkAsync(workRequest, cancellationToken);

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
            return WorkResult.DeadLetter(reason, exception);
        }

        //The Work has failed so mark the Work Status as Failed
        return WorkResult.Failed(reason, exception);
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
