namespace RecordPoint.Connectors.SDK.Notifications
{
    /// <summary>
    /// 
    /// </summary>
    public interface INotificationWorker
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notificationRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task HandleNotificationAsync(Notification notificationRequest, CancellationToken cancellationToken);
    }
}
