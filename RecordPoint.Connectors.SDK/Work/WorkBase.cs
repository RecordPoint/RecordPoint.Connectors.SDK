using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Work;

/// <summary>
/// Base implementation of unmanaged work that can be executed within the context of the executing process.
/// units of work.
/// </summary>
/// <typeparam name="TParameter">Work item input parameter type</typeparam>
/// <remarks>
/// Public constructor. Used to inject dependencies
/// </remarks>
public abstract class WorkBase<TParameter>(IServiceProvider serviceProvider, IObservabilityScope observabilityScope, ITelemetryTracker telemetryTracker, IDateTimeProvider dateTimeProvider) : IWork<TParameter>
{

    #region Dependencies
    /// <summary>
    /// Observability scope manager
    /// </summary>
    protected IObservabilityScope ScopeManager { get; private set; } = observabilityScope;

    /// <summary>
    /// Telemetry tracker
    /// </summary>
    protected ITelemetryTracker TelemetryTracker { get; private set; } = telemetryTracker;

    /// <summary>
    /// Date Time provider
    /// </summary>
    protected IDateTimeProvider DateTimeProvider { get; private set; } = dateTimeProvider;

    /// <summary>
    /// Logging Options
    /// </summary>
    private LoggingOptions _loggingOptions { get; set; } = serviceProvider.GetService<IOptions<LoggingOptions>>()?.Value ?? new();
    #endregion

    #region Work Context
    /// <inheritdoc/>
    public abstract string WorkType { get; }

    /// <inheritdoc/>
    public string Id { get; protected set; } = Guid.NewGuid().ToString();

    /// <inheritdoc/>
    public DateTimeOffset StartDateTime { get; protected set; }

    /// <inheritdoc/>
    public DateTimeOffset FinishDateTime { get; protected set; }

    /// <inheritdoc/>
    public Exception Exception { get; protected set; }

    /// <inheritdoc/>
    public TimeSpan WorkDuration => FinishDateTime - StartDateTime;

    /// <inheritdoc/>
    public TParameter Parameter { get; protected set; }
    #endregion

    #region Run
    /// <inheritdoc/>
    public virtual async Task RunAsync(TParameter parameter, CancellationToken cancellationToken)
    {
        Parameter = parameter;
        StartDateTime = DateTimeProvider.UtcNow;

        using var workScope = BeginObservabilityScope();
        try
        {
            TrackStart();
            await InnerRunAsync(cancellationToken);
            EnsureHasOutcome();
        }
        catch (Exception ex)
        {
            await FailAsync(ex, cancellationToken);
        }
        finally
        {
            FinishDateTime = DateTimeProvider.UtcNow;
            TrackFinish();
        }
    }

    /// <summary>
    /// Performs execution of the work logic
    /// </summary>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns></returns>
    protected abstract Task InnerRunAsync(CancellationToken cancellationToken);
    #endregion

    #region Run Result
    /// <inheritdoc/>
    public bool HasResult { get; protected set; }

    /// <inheritdoc/>
    public string ResultReason { get; protected set; }

    /// <inheritdoc/>
    public string ResultReasonDetails { get; protected set; }

    /// <inheritdoc/>
    public WorkResultType ResultType { get; protected set; }

    /// <summary>
    /// Ensure that an outcome has been recorded
    /// </summary>
    protected void EnsureHasOutcome()
    {
        if (!HasResult)
            throw new InvalidOperationException($"An outcome for this work item has not been recorded");
    }

    /// <summary>
    /// Record that this work item has completed
    /// </summary>
    protected Task CompleteAsync(string reason, CancellationToken cancellationToken)
    {
        EnsureIncomplete();
        HasResult = true;
        ResultReason = reason;
        ResultType = WorkResultType.Complete;
        return Task.CompletedTask;
    }

    /// <summary>
    /// Record that this work item has completed, with reason details
    /// </summary>
    protected Task CompleteAsync(string reason, string reasonDetails, CancellationToken cancellationToken)
    {
        ResultReasonDetails = reasonDetails;
        CompleteAsync(reason, cancellationToken);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Record that this work item has failed and should be reattempted
    /// </summary>
    /// <param name="reason">Reason why the work item has failed</param>
    /// <param name="cancellationToken"></param>
    protected virtual Task FailAsync(string reason, CancellationToken cancellationToken)
    {
        EnsureIncomplete();
        HasResult = true;
        ResultReason = reason;
        ResultType = WorkResultType.Failed;
        Exception = new InvalidOperationException(reason);
        TelemetryTracker.TrackException(Exception);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Record that this work item has failed due to an exception and should be reattempted
    /// </summary>
    /// <param name="exception">Exception that is cause of the failure</param>
    /// <param name="cancellationToken"></param>
    protected virtual Task FailAsync(Exception exception, CancellationToken cancellationToken)
    {
        EnsureIncomplete();
        HasResult = true;
        ResultReason = exception.Message;
        ResultType = WorkResultType.Failed;
        Exception = exception;
        TelemetryTracker.TrackException(exception);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Record that this work item should be abandoned
    /// </summary>
    protected virtual Task HandleAbandonedResultAsync(string reason, CancellationToken cancellationToken)
    {
        EnsureIncomplete();
        HasResult = true;
        ResultReason = reason;
        ResultType = WorkResultType.Abandoned;
        return Task.CompletedTask;
    }

    /// <summary>
    /// Ensure that this work item is incomplete
    /// </summary>
    protected void EnsureIncomplete()
    {
        if (HasResult)
            throw new InvalidOperationException($"Work already has an outcome");
    }
    #endregion

    #region Observability
    /// <summary>
    /// Begin the observability scope for this work item
    /// </summary>
    /// <returns>Scope</returns>
    protected virtual IDisposable BeginObservabilityScope()
    {
        return ScopeManager.BeginScope(GetKeyDimensions());
    }

    /// <summary>
    /// Track the start of the unit of work
    /// </summary>
    protected virtual void TrackStart()
    {
        if (!_loggingOptions.LogStartWorkEvents)
            return;

        var eventName = $"{WorkType}.{EventType.Start}";

        var dimensions = new Dimensions(GetCoreStartDimensions().Concat(GetCustomStartDimensions()));
        var measures = new Measures(GetCoreStartMeasures().Concat(GetCustomStartMeasures()));

        TelemetryTracker.TrackEvent(eventName, dimensions, measures);
    }

    /// <summary>
    /// Track the finish of a unit of work
    /// </summary>
    protected virtual void TrackFinish()
    {
        EnsureHasOutcome();

        var eventName = $"{WorkType}.{ResultType}";

        var dimensions = new Dimensions(GetCoreResultDimensions().Concat(GetCustomResultDimensions()));
        var measures = new Measures(GetCoreResultMeasures().Concat(GetCustomResultMeasures()));
        TelemetryTracker.TrackEvent(eventName, dimensions, measures);
    }

    /// <summary>
    /// Get the key dimensions for this work
    /// </summary>
    /// <returns>All Key dimensions that will be included in the work items observability scope</returns>
    public Dimensions GetKeyDimensions() => new(GetCoreKeyDimensions().Concat(GetCustomKeyDimensions()));

    /// <summary>
    /// Get the core key dimensions for the work
    /// </summary>
    /// <returns>Key dimensions that will be included in the work items observability scope</returns>
    protected virtual Dimensions GetCoreKeyDimensions() => new()
    {
        [StandardDimensions.WORK] = WorkType,
        [StandardDimensions.WORK_ID] = Id
    };

    /// <summary>
    /// Get custom key dimensions for the work
    /// </summary>
    /// <returns>Key dimensions that will be included in the work items observability scope</returns>
    protected virtual Dimensions GetCustomKeyDimensions() => [];

    /// <summary>
    /// Get the core start dimensions.
    /// </summary>
    /// <remarks>
    /// This method is intended to be overridden in base classes
    /// </remarks>
    protected virtual Dimensions GetCoreStartDimensions() => new()
    {
        [StandardDimensions.EVENT_TYPE] = EventType.Start.ToString()
    };

    /// <summary>
    /// Get the core start measures
    /// </summary>
    /// <remarks>
    /// This method is intended to be overridden in base classes
    /// </remarks>
    protected virtual Measures GetCoreStartMeasures() => [];

    /// <summary>
    /// Get custom start dimensions that are specific to a type of work
    /// </summary>
    /// <remarks>Observability dimensions</remarks>
    protected virtual Dimensions GetCustomStartDimensions() => [];

    /// <summary>
    /// Get custom start measures that are specific to a type of work
    /// </summary>
    /// <remarks>Observability measures</remarks>
    protected virtual Measures GetCustomStartMeasures() => [];

    /// <summary>
    /// Get the core result dimensions.
    /// </summary>
    /// <remarks>
    /// This method is intended to be overridden in base classes
    /// </remarks>
    protected virtual Dimensions GetCoreResultDimensions()
    {
        var output = new Dimensions()
        {
            [StandardDimensions.EVENT_TYPE] = EventType.Finish.ToString(),
            [StandardDimensions.OUTCOME] = ResultType.ToString(),
            [StandardDimensions.OUTCOME_REASON] = ResultReason,
        };

        if (!string.IsNullOrEmpty(ResultReasonDetails))
        {
            output[StandardDimensions.ACTION_RESULT_REASON] = ResultReasonDetails;
        }

        return output;
    }

    /// <summary>
    /// Get the core result measures
    /// </summary>
    /// <remarks>
    /// This method is intended to be overridden in base classes
    /// </remarks>
    protected virtual Measures GetCoreResultMeasures() => new()
    {
        [StandardMeasures.WORK_SECONDS] = WorkDuration.TotalSeconds
    };

    /// <summary>
    /// Get result dimensions that are specific to a type of work
    /// </summary>
    /// <remarks>Observability dimensions</remarks>
    protected virtual Dimensions GetCustomResultDimensions() => new();

    /// <summary>
    /// Get result measures that are specific to a type of work
    /// </summary>
    /// <remarks>Observability measures</remarks>
    protected virtual Measures GetCustomResultMeasures() => new();
    #endregion
}
