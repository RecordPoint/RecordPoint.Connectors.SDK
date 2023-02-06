using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Notifications;
using RecordPoint.Connectors.SDK.Work;
using System.Text.Json;

namespace RecordPoint.Connectors.SDK.ContentReport
{
    /// <summary>
    /// Generate report notification handler that just forwards it to a work queue
    /// </summary>
    public class GenerateReportHandler : INotificationStrategy
    {
        public const string GENERATE_REPORT_TYPE = "GenerateReport";

        public string NotificationType => GENERATE_REPORT_TYPE;

        private readonly IManagedWorkFactory _managedWorkFactory;

        public GenerateReportHandler(IManagedWorkFactory managedWorkFactory)
        {
            _managedWorkFactory = managedWorkFactory;
        }

        public Task<NotificationOutcome> HandleNotificationAsync(ConnectorNotificationModel notification, CancellationToken cancellationToken)
        {
            if (notification == null)
            {
                throw new ArgumentNullException(nameof(notification));
            }
            if (string.IsNullOrEmpty(notification.Id))
            {
                throw new RequiredValueNullException(nameof(notification.Id));
            }
            if (string.IsNullOrEmpty(notification.ConnectorId))
            {
                throw new RequiredValueNullException(nameof(notification.ConnectorId));
            }

            return InnerHandleNotificationAsync(notification.Id, notification.ConnectorId, JsonSerializer.Serialize(notification), cancellationToken);
        }

        private async Task<NotificationOutcome> InnerHandleNotificationAsync(string notificationId, string connectorId, string serializedNotification, CancellationToken cancellationToken)
        {
            var workOperation = _managedWorkFactory.CreateWork(
                notificationId,
                GenerateReportOperation.WORK_TYPE,
                connectorId,
                nameof(ConnectorNotificationModel),
                serializedNotification
            );
            await workOperation.StartAsync(cancellationToken).ConfigureAwait(false);

            return NotificationOutcome.OK();
        }
    }

}
