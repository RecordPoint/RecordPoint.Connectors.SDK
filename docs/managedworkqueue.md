# The Managed Work Queue
This Connectors SDK is based upon a work queue system that provides a means for tasks to be queued for execution.

There are many different types of work that are each processed in unique queues, allowing horizontal scalability for each work type.

The work operations that derive from `ManagedQueueableWorkBase` are operations that run within a defined context and share state between subsequent executions.

The Managed Work Queue system must be injected using the Host Builder extension methods:
```cs
    hostBuilder
        .UseWorkManager()
        .UseWorkStateManager<DatabaseManagedWorkStatusManager>();
```

The work queue system requires a provider for the underlying queue resource:
- [RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus](./packages/recordpoint_connectors_sdk_workqueue_azureservicebus.md)


### Error Handling & Retry Logic
The Work Queue System has built-in retry logic that is enabled by default.