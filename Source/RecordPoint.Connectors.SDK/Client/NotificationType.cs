namespace RecordPoint.Connectors.SDK.Client
{
    /// <summary>
    /// Defines constant values that may appear in the ConnectorNotificationModel.NotificationType field.
    /// Possible values include: 'ItemDestroyed', 'Ping', 'ConnectorConfigCreated', 'ConnectorConfigUpdated', and 'ConnectorConfigDeleted'
    /// </summary>
    public static class NotificationType
    {
        /// <summary>
        /// The ItemDestroyed Notification Type. 
        /// This notification is sent by Records365 vNext when an item is disposed
        /// in the platform.
        /// The connector must permanently destroy all metadata, binaries and ay other 
        /// information associated with the item in the content source.
        /// </summary>
        public const string ItemDestroyed = nameof(ItemDestroyed);

        /// <summary>
        /// The Ping Notification Type.
        /// Used for testing purposes only.
        /// </summary>
        public const string Ping = nameof(Ping);

        /// <summary>
        /// The ConnectorConfigCreated Notification Type.
        /// This notification is sent by Records365 vNext when a new instance of the
        /// connector is created in the platform.
        /// </summary>
        public const string ConnectorConfigCreated = nameof(ConnectorConfigCreated);

        /// <summary>
        /// The ConnectorConfigUpdated Notification Type.
        /// This notification is sent by Records365 vNext when an existing instance of
        /// the connector is updated in the platform. Possible updates may include
        /// configuration changes, or the connector being enabled or disabled by a user.
        /// </summary>
        public const string ConnectorConfigUpdated = nameof(ConnectorConfigUpdated);

        /// <summary>
        /// The ConnectorConfigDeleted Notification Type.
        /// This notification is sent by Records365 vNext when an instance of the 
        /// connector is deleted in the platform.
        /// </summary>
        public const string ConnectorConfigDeleted = nameof(ConnectorConfigDeleted);
    }
}
