using Microsoft.Extensions.DependencyInjection;
using RecordPoint.Connectors.SDK.Caching.Semaphore;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using System;
using System.Threading;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RecordPoint.Connectors.SDK.Work;

/// <summary>
/// Base Implementation of unmanaged work that is submitted to a queue for execution.
/// </summary>
/// <remarks>
/// Public constructor. Used to inject dependencies
/// </remarks>
public abstract class QueueableWorkBase<TParameter>(
    IServiceProvider serviceProvider,
    ISystemContext systemContext,
    IObservabilityScope observabilityScope,
    ITelemetryTracker telemetryTracker,
    IDateTimeProvider dateTimeProvider) : WorkBase<TParameter>(serviceProvider, observabilityScope, telemetryTracker, dateTimeProvider), IQueueableWork
{

    /// <summary>
    /// Maximum backoff delay in seconds, applied when an action result supplies no MaxNextDelay override.
    /// </summary>
    public const int DEFAULT_MAX_BACKOFF_DELAY_SECONDS = 3600;

    /// <summary>
    /// Hard ceiling for an action-supplied MaxNextDelay override (30 days), so a bad value cannot hold a
    /// semaphore lock for an unreasonable time.
    /// </summary>
    public const int ABSOLUTE_MAX_BACKOFF_DELAY_SECONDS = 30 * 24 * 60 * 60;

    /// <summary>
    /// Upper bound of the random jitter added to a backoff delay, in seconds.
    /// </summary>
    private const int MAX_JITTER_SECONDS = 30;

    #region Dependencies

    /// <summary>
    /// System context
    /// </summary>
    protected ISystemContext SystemContext { get; private set; } = systemContext;

    /// <summary>
    /// Provides access to the application's service provider for resolving dependencies within the class.
    /// </summary>
    /// <remarks>This field is intended for use by derived classes to obtain registered services. It
    /// should not be modified after initialization.</remarks>
    protected readonly IServiceProvider _serviceProvider = serviceProvider;
    /// <summary>
    /// Provides access to the semaphore-based lock manager used for coordinating concurrent operations.
    /// </summary>
    protected readonly ISemaphoreLockManager _semaphoreLockManager = serviceProvider.GetService<ISemaphoreLockManager>();
    /// <summary>
    /// Provides access to the work queue client used for interacting with the underlying work queue system.
    /// </summary>
    protected readonly IWorkQueueClient _workQueueClient = serviceProvider.GetService<IWorkQueueClient>();
    #endregion

    #region Work Context
    /// <summary>
    /// Required override that identifies the service the work belongs to
    /// </summary>
    public abstract string ServiceName { get; }

    /// <summary>
    /// Work Request we are handling
    /// </summary>
    public WorkRequest WorkRequest { get; protected set; }

    /// <summary>
    /// Submit Start Time
    /// </summary>
    public DateTimeOffset SubmitDateTime { get; protected set; }

    /// <summary>
    /// Required override that deserializes the parameter from the work request
    /// </summary>
    /// <returns>Deserialized parameters</returns>
    protected virtual TParameter DeserializeParameter()
    {
        return JsonSerializer.Deserialize<TParameter>(WorkRequest.Body);
    }
    #endregion

    #region Run
    /// <summary>
    /// Execute the work request
    /// </summary>
    /// <param name="workRequest">Work request that defines the work to execute</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Task</returns>
    public virtual Task RunWorkRequestAsync(WorkRequest workRequest, CancellationToken cancellationToken)
    {
        if (workRequest.WorkType != WorkType)
            throw new InvalidOperationException($"Work request of type {workRequest.WorkType} does not match this work item");

        WorkRequest = workRequest;
        Id = workRequest.WorkId;
        SubmitDateTime = workRequest.SubmitDateTime;

        var parameter = DeserializeParameter();

        return RunAsync(parameter, cancellationToken);
    }

    #nullable enable
    /// <summary>
    /// Checks to see if a semphore lock has been applied and defers execution until the lock expires
    /// </summary>
    /// <param name="connectorConfigModel"></param>
    /// <param name="context">Context for lock keys when external apis have different restrictions, ie: by channel</param>
    /// <param name="cancellationToken"></param>
    /// <returns>True if a lock has been applied</returns>
    /// enable
    protected async Task<bool> CheckSemaphoreLockAsync(ConnectorConfigModel connectorConfigModel, object? context, CancellationToken cancellationToken)
    {
        if (_semaphoreLockManager != null)
        {
            _semaphoreLockManager.ConnectorConfiguration = connectorConfigModel;
            var semaphoreLockExpiry = await _semaphoreLockManager.GetSemaphoreAsync(WorkType, context, cancellationToken);
            if (semaphoreLockExpiry.HasValue)
            {
                //Semaphore lock is active so backoff until the semaphore lock expires
                Deferred($"Semaphore Lock enabled, deferring {WorkType}.", semaphoreLockExpiry);
                return true;
            }
        }
        return false;
    }
    #nullable disable

    #endregion

    #region Result

    /// <summary>
    /// Deferral wait till time
    /// </summary>
    public DateTimeOffset? WaitTill { get; protected set; }

    /// <summary>
    /// Time from when a request was made, till it was completed.
    /// Includes time spent in queues, getting retried, etc.
    /// </summary>
    public virtual TimeSpan ResultDuration => FinishDateTime - SubmitDateTime;

    /// <summary>
    /// Outcome for the work item
    /// </summary>
    public WorkResult GetWorkResult() => new()
    {
        ResultType = ResultType,
        Reason = ResultReason,
        WaitTill = WaitTill,
        Exception = Exception,
        Duration = ResultDuration
    };

    /// <summary>
    /// Record that work has been abandoned but should be retried later
    /// </summary>
    protected void Abandoned(string reason)
    {
        EnsureIncomplete();
        HasResult = true;
        ResultType = WorkResultType.Abandoned;
        ResultReason = reason;
    }

    /// <summary>
    /// Record that this work item has been abandoned due to an exception and should should be retried later
    /// </summary>
    /// <param name="exception">Exception that is cause of the failure</param>
    protected void Abandoned(Exception exception)
    {
        EnsureIncomplete();
        HasResult = true;
        ResultType = WorkResultType.Abandoned;
        ResultReason = exception.Message;
        Exception = exception;
    }

    /// <summary>
    /// Record that this work item will be deferred to a future time
    /// </summary>
    protected void Deferred(string reason, DateTimeOffset? waitTill)
    {
        EnsureIncomplete();
        HasResult = true;
        ResultType = WorkResultType.Deferred;
        ResultReason = reason;
        WaitTill = waitTill;
    }

    #nullable enable
    /// <summary>
    /// Defers the work and applies a semaphore lock, capped at the default delay ceiling.
    /// </summary>
    /// <remarks>
    /// Retained so existing derived work items keep compiling and binding unchanged. Prefer the overload
    /// taking maxNextDelay when the content source is legitimately slow to respond.
    /// </remarks>
    /// <param name="connectorConfigModel"></param>
    /// <param name="context">Context for lock keys when external apis have different restrictions, ie: by channel</param>
    /// <param name="semaphoreLockType"></param>
    /// <param name="nextDelay"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="RequiredValueNullException"></exception>
    protected Task HandleBackOffResultAsync(ConnectorConfigModel connectorConfigModel, object? context, SemaphoreLockType? semaphoreLockType, int? nextDelay, CancellationToken cancellationToken)
        => HandleBackOffResultAsync(connectorConfigModel, context, semaphoreLockType, nextDelay, null, cancellationToken);

    /// <summary>
    /// Defers the work and applies a semaphore lock
    /// </summary>
    /// <param name="connectorConfigModel"></param>
    /// <param name="context">Context for lock keys when external apis have different restrictions, ie: by channel</param>
    /// <param name="semaphoreLockType"></param>
    /// <param name="nextDelay"></param>
    /// <param name="maxNextDelay">Optional override for the delay ceiling, in seconds. Null uses DEFAULT_MAX_BACKOFF_DELAY_SECONDS.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="RequiredValueNullException"></exception>
    protected async Task HandleBackOffResultAsync(ConnectorConfigModel connectorConfigModel, object? context, SemaphoreLockType? semaphoreLockType, int? nextDelay, int? maxNextDelay, CancellationToken cancellationToken)
    {
        if (!semaphoreLockType.HasValue)
            throw new RequiredValueNullException(nameof(semaphoreLockType));

        if (!nextDelay.HasValue)
            throw new RequiredValueNullException(nameof(nextDelay));

        if (_semaphoreLockManager == null)
            throw new RequiredValueNullException(nameof(_semaphoreLockManager));

        // An action may raise (or lower) its own ceiling when the content source is legitimately slow to
        // respond; the override is itself clamped so a bad value cannot hold the lock indefinitely.
        var maxDelaySeconds = maxNextDelay.HasValue
            ? Math.Clamp(maxNextDelay.Value, 1, ABSOLUTE_MAX_BACKOFF_DELAY_SECONDS)
            : DEFAULT_MAX_BACKOFF_DELAY_SECONDS;

        // Add jitter to reduce thundering herd when multiple services re-queue simultaneously.
        // Applied after the ceiling so that a caller asking for exactly the ceiling still gets jitter.
        var jitterSeconds = Random.Shared.Next(0, MAX_JITTER_SECONDS + 1); // 0-30 seconds inclusive
        var delayWithJitter = Math.Min(nextDelay.Value, maxDelaySeconds) + jitterSeconds;

        _semaphoreLockManager.ConnectorConfiguration = connectorConfigModel;
        await _semaphoreLockManager.SetSemaphoreAsync(semaphoreLockType.Value, WorkType, context, delayWithJitter, cancellationToken);

        var delay = DateTimeOffset.Now.AddSeconds(delayWithJitter);
        Deferred("Semaphore Lock enabled.", delay);
    }
    #nullable disable

    #endregion

    #region Observability
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected override Dimensions GetCoreKeyDimensions()
    {
        var dimensions = base.GetCoreKeyDimensions();
        dimensions[StandardDimensions.COMPANY] = SystemContext.GetCompanyName();
        dimensions[StandardDimensions.SYSTEM] = SystemContext.GetConnectorName();
        dimensions[StandardDimensions.SERVICE] = ServiceName;
        dimensions[StandardDimensions.CONNECTOR_ID] = WorkRequest.ConnectorConfigId;
        dimensions[StandardDimensions.TENANT_ID] = WorkRequest.TenantId;
        dimensions[StandardDimensions.TENANT_DOMAIN_NAME] = WorkRequest.TenantDomainName;
        return dimensions;
    }

    /// <summary>
    /// Add additional core outcome measures
    /// </summary>
    /// <returns></returns>
    protected override Measures GetCoreResultMeasures()
    {
        var dimensions = base.GetCoreResultMeasures();
        dimensions[StandardMeasures.OUTCOME_SECONDS] = ResultDuration.TotalSeconds;
        return dimensions;
    }
    #endregion

    #region Disposable
    private bool _hasDisposed = false;

    /// <summary>
    /// Public access to check if the object has been disposed
    /// </summary>
    public bool HasDisposed
    {
        get
        {
            if (!_hasDisposed) return _hasDisposed;
            throw new ObjectDisposedException(WorkType);
        }
    }

    /// <summary>
    /// Dispose
    /// </summary>
    protected abstract void InnerDispose();

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        if (!_hasDisposed)
        {
            WorkRequest = null;
            InnerDispose();
            _hasDisposed = true;
        }
        GC.SuppressFinalize(this);
    }
    #endregion
}
