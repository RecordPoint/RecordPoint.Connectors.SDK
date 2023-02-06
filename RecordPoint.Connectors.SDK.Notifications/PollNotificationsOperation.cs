using Microsoft.Extensions.Logging;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.Notifications
{

    public class PollNotificationsOperation : WorkBase<object>
    {
        private readonly INotificationManager _notificationManager;
        private readonly IR365NotificationClient _r365NotificationClient;

        private const string POLL_WORK_TYPE = "Poll Notifications";

        public PollNotificationsOperation(
            INotificationManager notificationManager,
            IR365NotificationClient r365NotificationClient,
            IScopeManager scopeManager,
            ILogger<PollNotificationsOperation> logger,
            ITelemetryTracker telemetryTracker,
            IDateTimeProvider dateTimeProvider)
            : base(scopeManager, logger, telemetryTracker, dateTimeProvider)
        {
            _notificationManager = notificationManager;
            _r365NotificationClient = r365NotificationClient;
        }

        public override string WorkType => POLL_WORK_TYPE;

        protected override async Task InnerRunAsync(CancellationToken cancellationToken)
        {
            // If Records 365 is not configured silently do nothing
            if (!_r365NotificationClient.IsConfigured())
            {
                await CompleteAsync("Connector is not ready for operation", cancellationToken);
                return;
            }

            // Get notifications
            var notificationValues = await _r365NotificationClient.GetAllPendingNotifications(cancellationToken).ConfigureAwait(false);
            if (notificationValues == null)
            {
                await CompleteAsync("No work to do", cancellationToken);
                return;
            }

            // Process notifications
            foreach (var notificationValue in notificationValues.OrderBy(x => x.Timestamp))
            {
                // Notification handler need to be executed in sequence because of these following chains events
                // Connector Created, Connector Disabled, Connector Enabled. Hence it has to wait and cannot be handled parallel
                // For long running notifications like report generation also needs to handle in sequence.

                // TODO: Error handling/Notification ack responsibility is unclear.
                // Make it match closer to work queues
                await _notificationManager.HandleNotificationAsync(notificationValue, cancellationToken).ConfigureAwait(false);

                cancellationToken.ThrowIfCancellationRequested();
            }

            NotificationCount = notificationValues.Count;
            await CompleteAsync("All processing complete", cancellationToken);
        }

        public int NotificationCount { get; private set; }

        protected override Measures GetCustomResultMeasures()
        {
            return new Measures()
            {
                ["NotificationCount"] = NotificationCount
            };
        }
    }

}
