using RecordPoint.Connectors.SDK.TaskRunner;

namespace RecordPoint.Connectors.SDK.NotificationTasks
{
    public class NotificationRunnerSettings : TaskRunnerBaseSettings
    {
        /// <summary>
        /// The default polling interval in seconds for NotificationTask.
        /// </summary>
        public const int DefaultNotificationPollIntervalSeconds = 10;

        /// <summary>
        /// How often in seconds each long-running task should poll for new Notifications.
        /// </summary>
        public int NotificationPollIntervalSeconds { get; set; } = DefaultNotificationPollIntervalSeconds;
    }
}
