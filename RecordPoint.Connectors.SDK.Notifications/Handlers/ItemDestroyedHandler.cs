using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.Notifications.Handlers
{
    public class ItemDestroyedHandler : NotificationHandler
    {
        public const string ITEM_DESTROYED_NOTIFICATION_TYPE = "ItemDestroyed";

        private readonly IConnectorConfigurationManager _connectorManager;
        private readonly IWorkQueueClient _workQueueClient;

        public ItemDestroyedHandler(IConnectorConfigurationManager connectorManager, IWorkQueueClient workQueueClient)
        {
            _connectorManager = connectorManager;
            _workQueueClient = workQueueClient;
        }

        public override string NotificationType => ITEM_DESTROYED_NOTIFICATION_TYPE;

        public override async Task<NotificationOutcome> HandleNotificationAsync(ConnectorNotificationModel notification, CancellationToken cancellationToken)
        {
            var connector = await _connectorManager.GetConnectorAsync(notification.ConnectorId, cancellationToken);

            var record = new Record
            {
                Author = notification?.Item?.Author,
                ContentVersion = notification?.Item?.ContentVersion,
                ExternalId = notification?.Item?.ExternalId,
                Location = notification?.Item?.Location,
                ParentExternalId = notification?.Item?.ParentExternalId
            };

            await _workQueueClient.DisposeRecordAsync(new ContentSynchronisationConfiguration
            {
                ConnectorConfigurationId = notification?.ConnectorId,
                TenantId = notification?.TenantId,
                TenantDomainName = connector.TenantDomainName,
            }, record, null, cancellationToken);

            return NotificationOutcome.OK();
        }
    }
}