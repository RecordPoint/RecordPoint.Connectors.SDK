using RecordPoint.Connectors.SDK.TaskRunner;

namespace RecordPoint.Connectors.SDK.NotificationTasks
{
    public class NotificationRunnerSettings : TaskRunnerBaseSettings
    {
        public const int DefaultNotificationPollIntervalSeconds = 10;

        /// <summary>
        /// How often each long-running task should poll for new Notifications
        /// </summary>
        public int NotificationPollIntervalSeconds { get; set; } = DefaultNotificationPollIntervalSeconds;
    }
}
