using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;
using System.Text.Json;

namespace RecordPoint.Connectors.SDK.Notifications.Webhook;

/// <summary>
/// The webhook operation.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="WebhookOperation"/> class.
/// </remarks>
/// <param name="serviceProvider">The service provider.</param>
/// <param name="notificationManager">The notification manager.</param>
/// <param name="workQueueClient">The work queue client.</param>
/// <param name="observabilityScope">The scope manager.</param>
/// <param name="telemetryTracker">The telemetry tracker.</param>
/// <param name="dateTimeProvider">The date time provider.</param>
public class WebhookOperation(
    IServiceProvider serviceProvider,
    INotificationManager notificationManager,
    IWorkQueueClient workQueueClient,
    IObservabilityScope observabilityScope,
    ITelemetryTracker telemetryTracker,
    IDateTimeProvider dateTimeProvider) : WorkBase<object>(serviceProvider, observabilityScope, telemetryTracker, dateTimeProvider)
{
    /// <summary>
    /// The WEBHOOK WORK TYPE.
    /// </summary>
    private const string WEBHOOK_WORK_TYPE = "Webhook Notifications";

    /// <summary>
    /// Gets the work type.
    /// </summary>
    public override string WorkType => WEBHOOK_WORK_TYPE;

    /// <summary>
    /// Gets the connector notification.
    /// </summary>
    public ConnectorNotificationModel ConnectorNotification => (ConnectorNotificationModel)Parameter;

    /// <summary>
    /// Inner the run asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task</returns>
    protected override async Task InnerRunAsync(CancellationToken cancellationToken)
    {
        if (AsyncNotifications.NotificationTypes.Contains(ConnectorNotification.NotificationType)
            // Always run asynchronously
            && !ConnectorNotification.NotificationType.Equals(NotificationType.ItemDestroyed))
        {
            await SubmitAsyncNotificationWorkAsync(ConnectorNotification, cancellationToken);
        }
        else
        {
            await notificationManager.HandleNotificationAsync(ConnectorNotification, cancellationToken);
        }

        await CompleteAsync("Processing complete", cancellationToken);
    }

    private Task SubmitAsyncNotificationWorkAsync(ConnectorNotificationModel connectorNotification, CancellationToken cancellationToken)
    {
        var workRequest = new WorkRequest
        {
            WorkId = Guid.NewGuid().ToString(),
            WorkType = AsyncNotificationOperation.WORK_TYPE,
            Body = JsonSerializer.Serialize(connectorNotification),
            ConnectorConfigId = connectorNotification.ConnectorConfig.Id,
            TenantId = connectorNotification.ConnectorConfig.TenantId,
            TenantDomainName = connectorNotification.ConnectorConfig.TenantDomainName,
            WaitTill = null
        };
        return workQueueClient.SubmitWorkAsync(workRequest, cancellationToken);
    }

    /// <summary>
    /// Get custom key dimensions.
    /// </summary>
    /// <returns>A Dimensions</returns>
    protected override Dimensions GetCustomKeyDimensions()
    {
        var dimensions = base.GetCustomKeyDimensions();
        dimensions.Add("ConnectorNotificationId", ConnectorNotification.Id);
        dimensions.Add("NotificationType", ConnectorNotification.NotificationType);
        dimensions.Add("TenantId", ConnectorNotification.TenantId);
        dimensions.Add("ConnectorId", ConnectorNotification.ConnectorId);
        if (ConnectorNotification.ConnectorConfig != null)
        {
            dimensions.Add("TenantDomain", ConnectorNotification.ConnectorConfig.TenantDomainName);
            dimensions.Add("ConnectorTypeId", ConnectorNotification.ConnectorConfig.ConnectorTypeId);
        }
        return dimensions;
    }
}
