using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.Notifications.Webhook;

/// <summary>
/// Queueable work operation that replays notification handling outside of the webhook request path.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="AsyncNotificationOperation"/> class.
/// </remarks>
/// <param name="serviceProvider">The service provider.</param>
/// <param name="notificationManager">The notification manager.</param>
/// <param name="systemContext">The system context.</param>
/// <param name="observabilityScope">The scope manager.</param>
/// <param name="telemetryTracker">The telemetry tracker.</param>
/// <param name="dateTimeProvider">The date time provider.</param>
public class AsyncNotificationOperation(
    IServiceProvider serviceProvider,
    INotificationManager notificationManager,
    ISystemContext systemContext,
    IObservabilityScope observabilityScope,
    ITelemetryTracker telemetryTracker,
    IDateTimeProvider dateTimeProvider) : QueueableWorkBase<ConnectorNotificationModel>(serviceProvider, systemContext, observabilityScope, telemetryTracker, dateTimeProvider)
{
    /// <summary>
    /// Async notifications work type.
    /// </summary>
    public const string WORK_TYPE = "Async Notifications";

    /// <summary>
    /// Gets the service name.
    /// </summary>
    public override string ServiceName => "Webhook Notifications";

    /// <summary>
    /// Gets the work type.
    /// </summary>
    public override string WorkType => WORK_TYPE;

    /// <summary>
    /// Processes a notification that was previously accepted by the webhook and dispatched to the
    /// async notifications queue. This executes the normal notification handling pipeline after the
    /// original HTTP request has already completed.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <exception cref="InvalidOperationException"></exception>
    /// <returns>A Task</returns>
    protected override async Task InnerRunAsync(CancellationToken cancellationToken)
    {
        try
        {
            await notificationManager.HandleNotificationAsync(Parameter, cancellationToken);
            await CompleteAsync("Notification processing completed", cancellationToken);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Exception thrown while handling notification", ex);
        }
    }

    #region Observability
    /// <summary>
    /// Get custom key dimensions.
    /// </summary>
    /// <returns>A Dimensions</returns>
    protected override Dimensions GetCustomKeyDimensions()
    {
        var dimensions = new Dimensions
        {
            [nameof(Parameter.NotificationType)] = Parameter.NotificationType,
        };
        return dimensions;
    }
    #endregion

    #region Disposable
    /// <summary>
    /// Dispose invocation results
    /// </summary>
    protected override void InnerDispose()
    {
    }
    #endregion
}
