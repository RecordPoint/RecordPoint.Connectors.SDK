using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.Notifications.Handlers
{
    /// <summary>
    /// The item destroyed handler.
    /// </summary>
    public class ItemDestroyedHandler : NotificationHandler
    {
        /// <summary>
        /// The ITEM DESTROYED NOTIFICATION TYPE.
        /// </summary>
        public const string ITEM_DESTROYED_NOTIFICATION_TYPE = "ItemDestroyed";

        /// <summary>
        /// The connector manager.
        /// </summary>
        private readonly IConnectorConfigurationManager _connectorManager;
        /// <summary>
        /// Work queue client.
        /// </summary>
        private readonly IWorkQueueClient _workQueueClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemDestroyedHandler"/> class.
        /// </summary>
        /// <param name="connectorManager">The connector manager.</param>
        /// <param name="workQueueClient">The work queue client.</param>
        public ItemDestroyedHandler(IConnectorConfigurationManager connectorManager, IWorkQueueClient workQueueClient)
        {
            _connectorManager = connectorManager;
            _workQueueClient = workQueueClient;
        }

        /// <summary>
        /// Gets the notification type.
        /// </summary>
        public override string NotificationType => ITEM_DESTROYED_NOTIFICATION_TYPE;

        /// <summary>
        /// Handle the notification asynchronously.
        /// </summary>
        /// <param name="notification">The notification.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><![CDATA[Task<NotificationOutcome>]]></returns>
        public override async Task<NotificationOutcome> HandleNotificationAsync(ConnectorNotificationModel notification, CancellationToken cancellationToken)
        {
            var connector = await _connectorManager.GetConnectorAsync(notification.ConnectorId, cancellationToken);

            var record = new Record
            {
                Author = notification.Item.Author,
                ContentVersion = notification.Item.ContentVersion,
                ExternalId = notification.Item.ExternalId,
                Location = notification.Item.Location,
                MediaType = notification.Item.MediaType,
                MimeType = notification.Item.MimeType,
                SourceCreatedBy = notification.Item.SourceCreatedBy,
                SourceCreatedDate = ParseDateTime(notification.Item.SourceCreatedDate),
                SourceLastModifiedBy = notification.Item.SourceLastModifiedBy,
                SourceLastModifiedDate = ParseDateTime(notification.Item.SourceLastModifiedDate),
                Title = notification.Item.Title,
                ParentExternalId = notification.Item.ParentExternalId,
                MetaDataItems = ParseMetaDataModels(notification.Item.SourceProperties)
            };

            await _workQueueClient.DisposeRecordAsync(new ContentSynchronisationConfiguration
            {
                ConnectorConfigurationId = notification.ConnectorId,
                TenantId = notification.TenantId,
                TenantDomainName = connector.TenantDomainName,
            }, record, null, cancellationToken);

            return NotificationOutcome.OK();
        }

        /// <summary>
        /// Converts a DateTime to a DateTimeOffset
        /// If the input value is null, it will return a new instance of a DateTimeOffset
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private static DateTimeOffset ParseDateTime(DateTime dateTime)
        {
            return new DateTimeOffset(dateTime, TimeSpan.FromHours(0));
        }

        /// <summary>
        /// Converts a List of MetaDataModels to a List of MetaDataItems
        /// </summary>
        /// <param name="metaDataModels"></param>
        /// <returns></returns>
        private static List<MetaDataItem> ParseMetaDataModels(IList<MetaDataModel> metaDataModels)
        {
            return metaDataModels.Select(metaDataModel => new MetaDataItem
            {
                Name = metaDataModel.Name,
                Type = metaDataModel.Type,
                Value = metaDataModel.Value,
                MetaDataItemType = MetaDataItemType.R365MetaData
            }).ToList();
        }
    }
}