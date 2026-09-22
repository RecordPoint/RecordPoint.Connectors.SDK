using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Work;

/// <summary>
/// Provides a base class for implementing periodic background services.
/// </summary>
/// <remarks>This abstract class extends <see cref="BackgroundService"/> and provides a framework for creating
/// services that perform periodic work at a specified interval. Derived classes must implement the <see cref="ServiceIntervalInSeconds"/> 
/// property to define the interval between executions and override the <see cref="ExecuteAsync(CancellationToken)"/> 
/// method to implement the service's logic.</remarks>
public abstract class PeriodicWorkBase(IObservabilityScope observabilityScope, ITelemetryTracker telemetryTracker, IDateTimeProvider dateTimeProvider) : BackgroundService
{
    /// <summary>
    /// Gets the interval, in seconds, at which the service performs its routine operations.
    /// </summary>
    public abstract int ServiceIntervalInSeconds { get; }

    /// <summary>
    /// Gets the name of the service associated with the current instance.
    /// </summary>
    public abstract string ServiceName { get; }

    /// <summary>
    /// Gets the type of work associated with the current instance.
    /// </summary>
    public abstract string WorkType { get; }


    /// <summary>
    /// Triggered when the application host is ready to start the service.
    /// </summary>
    /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous Start operation.</returns>
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        using var workScope = observabilityScope.BeginScope(GetKeyDimensions());
        try
        {
            await InnerStartAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            telemetryTracker.TrackException(ex);
        }

        await base.StartAsync(cancellationToken);
    }

    /// <summary>
    /// Executes the background task when the service is running.
    /// </summary>
    /// <remarks>This method is called by the framework to perform the background processing.  Override this
    /// method to implement the logic for the background task.  Ensure that the operation respects the <paramref
    /// name="stoppingToken"/> to support cancellation.</remarks>
    /// <param name="stoppingToken">A <see cref="CancellationToken"/> that is triggered when the service is stopping.  Use this token to handle
    /// graceful shutdown of the task.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var workScope = observabilityScope.BeginScope(GetKeyDimensions());
                var startDateTime = dateTimeProvider.UtcNow;
                try
                {
                    ResetResult();
                    TrackStart();
                    await InnerRunAsync(stoppingToken);
                    EnsureHasOutcome();
                }
                catch (Exception ex)
                {
                    telemetryTracker.TrackException(ex);
                }
                finally
                {
                    var finishDateTime = dateTimeProvider.UtcNow;
                    TrackFinish(finishDateTime - startDateTime);
                }

                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(ServiceIntervalInSeconds), stoppingToken);
                }
                catch (OperationCanceledException)
                {
                }
            }

        }
        catch (Exception ex)
        {
            telemetryTracker.TrackException(ex);
        }

        var shutdownDimensions = new Dimensions()
        {
            [StandardDimensions.EVENT_TYPE] = EventType.Shutdown.ToString()
        };
        telemetryTracker.TrackTrace("Periodic Work Stopped", SeverityLevel.Information, shutdownDimensions);
    }

    /// <summary>
    /// Executes any startup logic required before the periodic work begins.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual Task InnerStartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Executes the core logic of the operation asynchronously.
    /// </summary>
    /// <remarks>This method is intended to be overridden in a derived class to implement the specific
    /// behavior of the operation. The operation should respect the provided <paramref name="cancellationToken"/> to
    /// allow for cooperative cancellation.</remarks>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    protected abstract Task InnerRunAsync(CancellationToken cancellationToken);


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
    /// Resets the result state of the work item
    /// </summary>
    protected void ResetResult()
    {
        HasResult = false;
        ResultReason = string.Empty;
        ResultReasonDetails = string.Empty;
        ResultType = WorkResultType.Complete;
    }

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
    protected void Complete(string reason)
    {
        EnsureIncomplete();
        HasResult = true;
        ResultReason = reason;
        ResultType = WorkResultType.Complete;
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
    /// Track the start of the unit of work
    /// </summary>
    private void TrackStart()
    {
        var eventName = $"{WorkType}.{EventType.Start}";

        var dimensions = new Dimensions(GetCoreStartDimensions().Concat(GetCustomStartDimensions()));
        var measures = new Measures(GetCoreStartMeasures().Concat(GetCustomStartMeasures()));

        telemetryTracker.TrackEvent(eventName, dimensions, measures);
    }


    /// <summary>
    /// Track the finish of a unit of work
    /// </summary>
    private void TrackFinish(TimeSpan workDuration)
    {
        EnsureHasOutcome();

        var eventName = $"{WorkType}.{ResultType}";

        var dimensions = new Dimensions(GetCoreResultDimensions().Concat(GetCustomResultDimensions()));
        var measures = new Measures(GetCoreResultMeasures(workDuration).Concat(GetCustomResultMeasures()));
        telemetryTracker.TrackEvent(eventName, dimensions, measures);
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
        [StandardDimensions.WORK] = WorkType
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
        [StandardDimensions.EVENT_TYPE] = EventType.Start.ToString(),
    };

    /// <summary>
    /// Get custom start dimensions that are specific to a type of work
    /// </summary>
    /// <remarks>Observability dimensions</remarks>
    protected virtual Dimensions GetCustomStartDimensions() => [];

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
    /// Get result dimensions that are specific to a type of work
    /// </summary>
    /// <remarks>Observability dimensions</remarks>
    protected virtual Dimensions GetCustomResultDimensions() => [];


    /// <summary>
    /// Get the core start measures
    /// </summary>
    /// <remarks>
    /// This method is intended to be overridden in base classes
    /// </remarks>
    protected virtual Measures GetCoreStartMeasures() => [];

    /// <summary>
    /// Get custom start measures that are specific to a type of work
    /// </summary>
    /// <remarks>Observability measures</remarks>
    protected virtual Measures GetCustomStartMeasures() => [];

    /// <summary>
    /// Get the core result measures
    /// </summary>
    /// <remarks>
    /// This method is intended to be overridden in base classes
    /// </remarks>
    protected virtual Measures GetCoreResultMeasures(TimeSpan workDuration) => new()
    {
        [StandardMeasures.WORK_SECONDS] = workDuration.TotalSeconds
    };

    /// <summary>
    /// Get result measures that are specific to a type of work
    /// </summary>
    /// <remarks>Observability measures</remarks>
    protected virtual Measures GetCustomResultMeasures() => [];
    #endregion
}
