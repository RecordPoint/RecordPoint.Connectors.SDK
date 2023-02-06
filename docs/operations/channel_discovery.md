# Channel Discovery

Content Sources are typically broken up in to "Channels".  A Channel can be any high level aggregation of content within the Content Source.

The logic for a Channel Discovery within your Connector is implemented via the `IChannelDiscoveryAction`.

A Channel Discovery Operation is executed within the context of a Connector Configuration.
There will be one Operation per Connector Configuration.
When a Connector Configuration is removed, the Operation will automatically return a Complete status at its next execution which will end the ongoing execution of the operation.
If a Connector Configuration is disabled, the Operation will not call the associated action and will reschedule execution at a later time.

Channel Discovery should query the Content Source and determine:
*  if a new Channel has been discovered (results in the initiation of Content Synchronisation)
*  If an existing Channel has been updated
*  if a previously known Channel no longer exists
*  if Content Registration should be initiated for a Channel

---

### Dependency Injection
To setup the service for Channel Discovery, you need to register the operation and associated action via dependency injection.

*Note: The example code below omits a lot of the required dependency injection setup.  For a full example, see [Dependency Injection](../dependency_injection.md)*
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

        builder.HostBuilder.UseChannelDiscoveryOperation<ChannelDiscoveryAction>();

        return builder.HostBuilder;
    }
}
```

---

### Implementation

```cs
/// <summary>
/// Defines a Channel discovery action, which is responsible for discovering Channels within the Content Source
/// available for a content source
/// </summary>
public interface IChannelDiscoveryAction
{
    /// <summary>
    /// Executes the channel discovery logic
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result of the scan</returns>
    Task<ChannelDiscoveryResult> ExecuteAsync(ConnectorConfigModel connectorConfiguration, CancellationToken cancellationToken);
}

/// <summary>
/// Result of a Channel Discovery operation
/// </summary>
public class ChannelDiscoveryResult
{
    /// <summary>
    /// Work result type
    /// </summary>
    public ChannelDiscoveryResultType ResultType { get; set; }

    /// <summary>
    /// Result reason
    /// </summary>
    public string Reason { get; set; } = string.Empty;

    /// <summary>
    /// Optional exception that caused the operation to fail
    /// </summary>
    public Exception? Exception { get; set; }

    /// <summary>
    /// New Channels that were discovered
    /// </summary>
    public List<Channel> Channels { get; set; } = new();

    /// <summary>
    /// New Channel Registrations
    /// </summary>
    public List<Channel> NewChannelRegistrations { get; set; } = new();

    /// <summary>
    /// When the action requests a backoff due to Content Source throttling, this specifies how the semphore should lock
    /// other requests to the content source
    /// </summary>
    public SemaphoreLockType SemaphoreLockType { get; set; }

    /// <summary>
    /// The number of seconds to delay the next execution of the Channel Discovery
    /// Also specifies the number of seconds to set a semaphore lock when a BackOff result is returned
    /// </summary>
    /// <remarks>When null, the operation will use the default delay configuration</remarks>
    public int? NextDelay { get; set; } = null;
}

/// <summary>
/// Channel discovery result type
/// </summary>
public enum ChannelDiscoveryResultType
{
    Complete,
    Failed,
    BackOff
}  
```

---

### Action Execution
The Channel Discovery Action includes an `ExecuteAsync(...)` method as the invocation point for the action.

The action should query the Content Source looking for Channels.
The `IChannelManager` can be used for obtaining access to the Channel cache.
This can be used to determine if there are newly discovered Channels, or if previously discovered Channels have been updated on the Content Source.

The `ExecuteAsync(...)` method should return a `ChannelDiscoveryResult`.

Channels returned within the `Channels` property are considered new or updated.  
The SDK will determine if the Channel should be added or updated in the Channel Cache (within the Connector state database).
If the Channel is considered new (doesn't exist in the Channel Cache), the SDK will spawn a new [Content Synchronisation](./content_synchronisation.md) Operation for the Channel.

If Channels are returned in the `NewChannelRegistrations` property, new [Content Registration](./content_registration.md) operations will be initiated for each Channel.

*Note: New Channels must always be returned via the `Channels` property.  If unknown Channels are returned via the `NewChannelRegistrations`, the SDK will not spawn with Content Synchronisation or Registration operations.*

---

### Execution Results
The action requires a return result of `ChannelDiscoveryResult`.  This object lets the SDK know how to handle the results of the action execution and provides data for spawning new operations and logging.

##### ResultType
A `ResultType` is required after each execution to let the SDK know what the state of the operation is. See [Result Types](#result-types) below.

##### Reason
A string based description of the result.

##### Exception
If the result type is `Failed` and optional `Exception` can be provided for logging purposes.

##### Channels
A list of Channels that have been discovered or require updating in the Channel Cache.
The SDK will determine which Channels are new and spawn Content Synchronisation operations where appropriate.

##### NewChannelRegistrations
A list of Channels that should have Content Registration operations spawned.

##### SemaphoreLockType
See [Semaphore Lock](../semaphore_lock.md) for a detailed explanation of the Semaphore Locking capbility provided by the SDK.

##### NextDelay
A delay in seconds that overrides the configured execution delay for the next scheduled execution of the operation.

---

#### Result Types {#result-types}

##### Complete
Indicates that execution has completed successfully and the operation should be rescheduled for its next execution following the configured delay rules.

##### Failed
Indicates that the invocation experienced an error during execution.  The SDK will retry the execution with the same stateful context until the maximum number of retries has been exceeded.

##### BackOff
Indicates that the execution of the action should be repeated with the same stateful context after a provided delay period (`NextDelay`).  This should be used in scenarios where the action is experiencing Rate Limiting by the Content Source.

---

### Settings

Configurable Options for Channel Discovery:
```
"ContentManager": {
    "ChannelDiscovery": {
        "DelaySeconds": 300,
        "ExponentialBackOff": true,
        "ImmediateReExecution": false,
        "RandomDelay": false,
        "MaxDelaySeconds": 600
    }
}
```

##### DelaySeconds: 
Number of seconds to delay betwwen Operation executions 

Default: 300 seconds (5 minutes)

##### ExponentialBackOff:
Instructs the SDK to exponentially increase the delay between executions when no results have been returned

Default: true

##### ImmediateReExecution:
Instructs the SDK to re-execute the operation immediately if a result was returned from the previous execution

Default: false

##### RandomDelay:
Adds a random delay between 1 and `DelaySeconds` to the total delay between operations

Default: false

##### MaxDelaySeconds:
The maximum delay (in seconds) between executions. Only applicable when `ExponentialBackOff` is enabled.

Default: 600 seconds (10 minutes)