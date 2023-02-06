namespace RecordPoint.Connectors.SDK.Notifications
{
    public interface INotificationWorker
    {
        public Task HandleNotificationAsync(Notification notificationRequest, CancellationToken cancellationToken);
    }
}
