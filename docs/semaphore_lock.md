# Semaphore Lock
External Content Sources often have rate limiting restrictions on Api's used to query when performing operations
such as Channel Discovery, Content Synchronisation or Registration, Binary Submission, or Record Disposal.

As the Connectors SDK encourages the use of small independant services with isolated responsibilities,
and along with horizontal scaling, there are times where a single instance of a service experiences a rate limiting request
from the Content Source.

The Semaphore Lock allows the various Connector actions to report the back-off duration and prevent other services/instances
from attempting to communicate with the Content Source during the back-off period.

Various Connector action results have a set of properties that can force a lock to be put in place.
When the `ResultType` of an action is set as `BackOff`, the `NextDelay` and `SemaphoreLockType` properties need to be specified.

The `NextDelay` property should be set (in seconds) to the amount of time the Content Source has instructed to "back-off".
This will be used to set the duration of the lock.

The Semaphore Lock has two modes, being Global and Scoped.  

### Global Locks
When a Global Semaphore Lock is applied, all Connector Operations across all Connector Configurations will be delayed by the specified duration.

### Scoped Locks
Scoped Semaphore Locks are, as the name suggests, set to a specific scope.
The scope is a unique key that is set by the locking process and is able to be reproduced by another potentially affected operation.

To use Scoped Semaphore Locks, the `ISemaphoreLockScopedKeyAction` needs to be implemented and registered via Dependency Injection.

The action receives the Connector Configuration, the running operations "Work Type" and a nullable "context" (ie: ChannelExternalId) as context that can be used to establish the locking key.

---

### Semaphore Lock Providers
There is currently only an "In-Memory" provider that exists in the [RecordPoint.Connectors.SDK.Caching](./packages/recordpoint_connectors_sdk_caching.md) package.
This provider uses the `Microsoft.Extensions.Caching.Memory.IMemoryCache` for storing each semaphore lock when activated.

This limits the usefulness of the lock, as it can only be applied within a single running executable, as the cache cannot be shared with other running executables.

A Redis &/or Cosmos provider will be built to provide a cross-service/cross-instance locking capability.

---

### Dependency Injection

To use the Semaphore Lock capability, you need to register the related services via dependency injection.

*Note: The example code below omits a lot of the required dependency injection setup.  For a full example, see [Dependency Injection](./dependency_injection.md)*
```cs
public static class Program
{
    public static void Main(string[] args)
    {
        CreateConnectorHostBuilder(args)
            .Build()
            .Run();
    }

    private static IHostBuilder CreateConnectorHostBuilder(string[] args)
    {
        var builder = HostBuilderHelper.CreateConnectorHostBuilder(args);

        builder.HostBuilder.UseInMemorySemaphoreLock<SemaphoreLockScopedKeyAction>();

        return builder.HostBuilder;
    }
}
```

---

### Implementation

```cs
public interface ISemaphoreLockScopedKeyAction
{
    /// <summary>
    /// Execution point for generating a scoped semaphore key
    /// </summary>
    /// <param name="connectorConfigModel"></param>
    /// <param name="workType"></param>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<string> ExecuteAsync(ConnectorConfigModel connectorConfigModel, string workType, object? context, CancellationToken cancellationToken);
}
```

---

### Action Execution
The Semaphore Lock Scoped Key Action contains an `ExecuteAsync(...)` method as the invocation point for the action.

The implementation should utilise data within the provided Connector Configuration and optionally the "Work Type" and "Context" to produce a locking key.
Context can be used to pass additional information such as channel, binary or record, for a more complete picture.

The logic should be simple and not call external resources in order to reduce the cost & latency of producing a locking key.

---

### Requesting a lock from a Connector Action
The `ChannelDiscovery`, `ContentRegistration`, and `ContentSynchronisation` actions allow a semaphore lock to be implemented by returning an appropriate result from the action.

- The `ResultType` property should be set to `BackOff`.
- The `SemaphoreLockType` property should be set to either `Global` or 'Scoped`.
- The `NextDelay` property should be set to the duration in seconds that the lock should apply.
