using RecordPoint.Connectors.SDK.Abstractions.Content;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.Notifications.Handlers
{
    /// <summary>
    /// Handler for a Content Registration Notification
    /// </summary>
    public class ContentRegistrationHandler : NotificationHandler
    {
        /// <summary>
        /// Content Registration Notification Type
        /// </summary>
        public const string CONTENT_REGISTRATION_NOTIFICATION_TYPE = "ContentRegistration";

        private readonly IConnectorConfigurationManager _connectorManager;
        private readonly IManagedWorkFactory _managedWorkFactory;
        private readonly IContentRegistrationRequestAction _contentRegistrationRequestAction;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectorManager"></param>
        /// <param name="managedWorkFactory"></param>
        /// <param name="contentRegistrationRequestAction"></param>
        public ContentRegistrationHandler(IConnectorConfigurationManager connectorManager, IManagedWorkFactory managedWorkFactory, IContentRegistrationRequestAction contentRegistrationRequestAction)
        {
            _connectorManager = connectorManager;
            _managedWorkFactory = managedWorkFactory;
            _contentRegistrationRequestAction = contentRegistrationRequestAction;
        }

        /// <summary>
        /// Content Registration Notification Type
        /// </summary>
        public override string NotificationType => CONTENT_REGISTRATION_NOTIFICATION_TYPE;

        /// <summary>
        /// Handle the Content Registration Notification
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<NotificationOutcome> HandleNotificationAsync(ConnectorNotificationModel notification, CancellationToken cancellationToken)
        {
            var contentRegistrationRequests = notification.Context.ContextToList<ContentRegistrationRequest>();

            var connectorConfiguration = await _connectorManager.GetConnectorAsync(notification.ConnectorId, cancellationToken);
            if (connectorConfiguration == null)
            {
                return NotificationOutcome.Failed("Connector not found");
            }

            var connectorStatus = await _connectorManager.GetConnectorStatusAsync(notification.ConnectorId, cancellationToken);
            if (!connectorStatus.Enabled)
            {
                return NotificationOutcome.Failed("Connector is disabled");
            }

            var tasks = new List<Task>();
            foreach (var contentRegistrationRequest in contentRegistrationRequests)
            {
                var channels = await _contentRegistrationRequestAction.GetChannelsFromRequestAsync(connectorConfiguration, contentRegistrationRequest, cancellationToken);
                if (channels.Any())
                {
                    var context = new Dictionary<string, string>();
                    if (contentRegistrationRequest.StartDate.HasValue)
                    {
                        context.Add(IContentRegistrationAction.StartDate, contentRegistrationRequest.StartDate.Value.Ticks.ToString());
                    }
                    if (contentRegistrationRequest.EndDate.HasValue)
                    {
                        context.Add(IContentRegistrationAction.EndDate, contentRegistrationRequest.EndDate.Value.Ticks.ToString());
                    }

                    foreach (var channel in channels)
                    {
                        var contentRegistrationOperation = _managedWorkFactory.CreateContentRegistrationOperation(connectorConfiguration, channel, context);
                        tasks.Add(contentRegistrationOperation.StartAsync(cancellationToken));
                    }
                }
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);

            return NotificationOutcome.OK();
        }
    }
}