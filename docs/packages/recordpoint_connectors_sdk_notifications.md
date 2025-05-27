# RecordPoint.Connectors.SDK.Notifications

[RecordPoint.Connectors.SDK.Notifications Api Documentation](./recordpoint_connectors_sdk_notifications_doc.md)

The Connectors SDK includes extensions that enable Records365 connector notifications.

Records365 has several connector notification types:
- Connector Configuration Created
- Connector Configuration Updated
- Connector Configuration Deleted
- Item Destroyed

Each notification type is implemented via implementations that extend from `RecordPoint.Connectors.SDK.Notifications.NotificationHandler`.

### Connector Configuration

##### Created
When a new Connector Configuration is created within Records365, a notification will be sent to the Connector so it is aware of the new configuration.
The Configuration is automatically persisted to the configuration store (see [RecordPoint.Connectors.SDK.Databases](./recordpoint_connectors_sdk_databases.md)).
The [Content Manager operation](../operations/content_manager.md) will discover the new configuration its the next execution and invoke a [Channel Discovery operation](../operations/channel_discovery.md) for the new configuration.

##### Updated
When a Connector Configuration is updated within Records365, a notification will be sent to the Connector so it is aware of the changes to the existing configuration.
The Configuration is automatically persisted to the configuration store (see [RecordPoint.Connectors.SDK.Databases](./recordpoint_connectors_sdk_databases.md)).

##### Deleted
When a Connector Configuration is deleted within Records365, a notification will be sent to the Connector so the configuration is removed from the configuration store.
The Configuration is automatically persisted to the configuration store (see [RecordPoint.Connectors.SDK.Databases](./recordpoint_connectors_sdk_databases.md)).

### Item Destroyed
The `Item Destroyed` notification submits a Record Disposal work request that is then executed by the [Record Disposal Operation](../operations/record_disposal.md).