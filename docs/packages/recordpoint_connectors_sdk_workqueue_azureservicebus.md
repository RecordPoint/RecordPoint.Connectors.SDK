# RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus

[RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus Api Documentation](./recordpoint_connectors_sdk_workqueue_azureservicebus_doc.md)

This package contains the Azure Service Bus work queue provider for the Connectors SDK.

To use this package, you need to inject the required services using the Host Builder extension method:
```cs
    hostBuilder.UseASBWorkQueue();
```

The work queue system requires other services to be injected.

Configuration must be provided via appsettings.json or KeyVault:
```
"AzureServiceBusSettings": {
    "KillswitchIntervalMilliSeconds": {defaults to 5000},
    "MaxDegreeOfParallelism": {defaults to 1},
    "MaxAutoLockRenewalDurationSeconds": {defaults to 300},
    "ServiceBusConnectionString": "{connection string for service bus}"
}
```

The service bus must contain the following queues:
- 'aggregation_submission'
- 'binary_submission'
- 'channel_discovery'
- 'content_manager'
- 'content_registration'
- 'content_synchronisation'
- 'record_disposal'
- 'record_submission'