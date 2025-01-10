using Microsoft.Extensions.Logging;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.Notifications
{

    /// <summary>
    /// The poll notifications operation.
    /// </summary>
    public class PollNotificationsOperation : WorkBase<object>
    {
        /// <summary>
        /// The notification manager.
        /// </summary>
        private readonly INotificationManager _notificationManager;
        /// <summary>
        /// The r365 notification client.
        /// </summary>
        private readonly IR365NotificationClient _r365NotificationClient;

        /// <summary>
        /// The POLL WORK TYPE.
        /// </summary>
        private const string POLL_WORK_TYPE = "Poll Notifications";

        /// <summary>
        /// Initializes a new instance of the <see cref="PollNotificationsOperation"/> class.
        /// </summary>
        /// <param name="notificationManager">The notification manager.</param>
        /// <param name="r365NotificationClient">The r365 notification client.</param>
        /// <param name="scopeManager">The scope manager.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="telemetryTracker">The telemetry tracker.</param>
        /// <param name="dateTimeProvider">The date time provider.</param>
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

        /// <summary>
        /// Gets the work type.
        /// </summary>
        public override string WorkType => POLL_WORK_TYPE;

        /// <summary>
        /// Inner the run asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task</returns>
        protected override async Task InnerRunAsync(CancellationToken cancellationToken)
        {
            // If Records 365 is not configured silently do nothing
            if (!_r365NotificationClient.IsConfigured())
            {
                await CompleteAsync("Connector is not ready for operation", cancellationToken);
                return;
            }
            
            var notificationValues = await _r365NotificationClient.GetAllPendingNotifications( cancellationToken).ConfigureAwait(false);
            
            if (notificationValues.Count == 0)
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

        /// <summary>
        /// Gets the notification count.
        /// </summary>
        public int NotificationCount { get; private set; }

        /// <summary>
        /// Get custom result measures.
        /// </summary>
        /// <returns>A Measures</returns>
        protected override Measures GetCustomResultMeasures()
        {
            return new Measures()
            {
                ["NotificationCount"] = NotificationCount
            };
        }
    }

}
