namespace RecordPoint.Connectors.SDK.Notifications;

/// <summary>
/// Configuration for notification types that should be routed to the async notifications queue.
/// By default, all notification types are processed synchronously in the webhook request path.
/// Add a notification type to this list only after the connector has registered
/// <c>UseAsyncNotificationOperation()</c> and the underlying work queue for
/// <c>AsyncNotificationOperation.WORK_TYPE</c> is available in the deployment environment.
/// </summary>
public static class AsyncNotifications
{
    /// <summary>
    /// List of notification types that should be processed asynchronously.
    /// For Azure Service Bus, the queue name is derived from the work type using the standard SDK rule:
    /// <c>{QueuePrefix}-async-notifications</c> when <c>AzureServiceBusSettings:QueuePrefix</c> is configured,
    /// otherwise <c>async-notifications</c>. The queue name is lower-case with spaces replaced by hyphens.
    /// </summary>
    public static List<string> NotificationTypes { get; set; } = [];
}
