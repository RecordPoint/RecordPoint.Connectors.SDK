# RecordPoint.Connectors.SDK.WorkQueue.RabbitMq

[RecordPoint.Connectors.SDK.WorkQueue.RabbitMq Api Documentation](./recordpoint_connectors_sdk_workqueue_rabbitmq_doc.md)

This package contains the RabbitMq work queue provider for the Connectors SDK.

To use this package, you need to inject the required services using the Host Builder extension method:
```cs
    hostBuilder.UseRabbitMqWorkQueue();
```

The work queue system requires other services to be injected.

Configuration must be provided via appsettings.json or KeyVault:
```
"RabbitMqSettings": {
    "KillswitchIntervalMilliSeconds": {defaults to 5000},
    "HostName": "{host name for RabbitMq. Default value for the local system is localhost.}",
    "HostUserName": "{username for the RabbitMq server. Default username for RabbitMq is guest.}",
    "HostPassword": "{password for the RabbitMq server Default password for RabbitMq is guest}"
}
```

The RabbitMq must contain the following queues:
- 'aggregation_submission'
- 'binary_submission'
- 'channel_discovery'
- 'content_manager'
- 'content_registration'
- 'content_synchronisation'
- 'record_disposal'
- 'record_submission'

The RabbitMq must contain the following queues for DeadLetter:
- 'aggregation_submission-DL'
- 'binary_submission-DL'
- 'channel_discovery-DL'
- 'content_manager-DL'
- 'content_registration-DL'
- 'content_synchronisation-DL'
- 'record_disposal-DL'
- 'record_submission-DL'