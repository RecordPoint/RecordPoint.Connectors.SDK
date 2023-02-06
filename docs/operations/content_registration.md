# Content Registration

Content Registration is the process of submitting all historical records from a Channel to Records 365.
Content Synchronisation continually discovers new and updated content, whilst Content Registration is a 
one-off operation that searches for all content up to the point where Content Synchronisation begins.

Content Registration can also be used to "catch-up" in scenarios where a period of time was missed by a Content Synchronisation operation.

Content Registration requires an action to be implemented with relevant logic to find and return 
Records, Binaries, and Aggregations.  The SDK then initiates relevant operations for submitting this data to Records365.

The logic for Content Registration within your Connector is implemented via the `IContentRegistrationAction`.

A Content Registration Operation is executed within the context of a Connector Configuration and Channel.
Additional context can be provided via a `Dictionary<string, string>` which remains static between executions.

Content Registration operations are optionally initiated from the result of a Channel Discovery, or manually using the `IManagedWorkFactory`.

When a Connector Configuration is removed, the Operation will automatically return a Complete status at its next execution which will end the ongoing execution of the operation.
If a Connector Configuration is disabled, the Operation will not call the associated action and will reschedule execution at a later time.

Content Registration should query the Content Source for records that were created between a given time period.

The Content Registration Action should return Aggregations, Records, and Binaries.

If a Channel no longer exists on the Content Source, the action should return an `Abandonded` result.
This will instruct the SDK to remove the Channel from the Channel Cache, and stop subsequent executions of the Content Registration Operation.

---

### Dependency Injection

To setup the service for Content Synchronisation, you need to register the operation and associated action via dependency injection.

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

        builder.HostBuilder.UseContentRegistrationOperation<ContentRegistrationAction>();

        return builder.HostBuilder;
    }
}
```

---

### Implementation

```cs
/// <summary>
/// Definition for a Content Registration action
/// </summary>
public interface IContentRegistrationAction
{
    /// <summary>
    /// Begin a new content registration operation
    /// </summary>
    /// <param name="connectorConfiguration">The onnector configuration</param>
    /// <param name="channel">The channel to perform content registration on</param>
    /// <param name="context">Custom Context for the Registration action</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result of the registration operation</returns>
    Task<ContentResult> BeginAsync(ConnectorConfigModel connectorConfiguration, Channel channel, IDictionary<string, string> context, CancellationToken cancellationToken);

    /// <summary>
    /// Continue a content registration
    /// </summary>
    /// <param name="connectorConfiguration">The onnector configuration</param>
    /// <param name="channel">The channel to perform content registration on</param>
    /// <param name="cursor">Scan cursor provided by the prior sync operation</param>
    /// <param name="context">Custom Context for the Registration action</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result of the registration operation</returns>
    Task<ContentResult> ContinueAsync(ConnectorConfigModel connectorConfiguration, Channel channel, string cursor, IDictionary<string, string> context, CancellationToken cancellationToken);

    /// summary>
    /// Stops the content synchronisation operation
    /// </summary>
    /// <param name="connectorConfiguration">The onnector configuration</param>
    /// <param name="channel">The channel to perform content registration on</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <remarks>
    /// Stops a current content synchronisation operation. Typically because a connector has been disabled, kill switch has been hit etc.
    /// Some content modules may use this to stop expensive services that might be running in order to support the synchronisation operation.
    /// Content modules should restart processing when asked to continue
    /// </remarks>
    Task StopAsync(ConnectorConfigModel connectorConfiguration, Channel channel, string cursor, CancellationToken cancellationToken);
}

/// <summary>
/// Outcome of a content operation
/// </summary>
public class ContentResult
{
    /// <summary>
    /// Work outcome type
    /// </summary>
    public ContentResultType ResultType { get; set; }

    /// <summary>
    /// Outcome reason
    /// </summary>
    public string Reason { get; set; } = string.Empty;

    /// <summary>
    /// Optional exception that caused the sync to fail
    /// </summary>
    public Exception? Exception { get; set; }

    /// <summary>
    /// Progress cursor. Wil be needed on subsequent operations to get the next section of work
    /// </summary>
    public string Cursor { get; set; } = string.Empty;

    /// <summary>
    /// Records
    /// </summary>
    public List<Record> Records { get; set; } = new List<Record>();

    /// <summary>
    /// Aggregations
    /// </summary>
    public List<Aggregation> Aggregations { get; set; } = new List<Aggregation>();

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
/// Content scan outcome type
/// </summary>
public enum ContentResultType
{
    Complete,
    Incomplete,
    Failed,
    Abandonded,
    BackOff
}
```

---

### Action Execution
The Content Registration Action includes two methods as the invocation points for the action.

The `BeginAsync(...)` method is called only once on the first execution of the action.
This method should be used for the initial scan of the content source and establish a cursor which should be returned within the action result.

The `ContinueAsync(...)` method is called at every subsequent execution of the action.
The method will pass the cursor from the previous execution and should return an updated cursor within the action result when the execution is finished.

---

### Execution Results
Both execution methods require a return resut of `ContentResult`.  This object lets the SDK know how to handle the results of the action execution and provides data for item submission and logging.

##### ResultType
A `ResultType` is required after each execution to let the SDK know what the state of the operation is. See [Result Types](#result-types) below.

##### Reason
A string based description of the result.

##### Exception
If the result type is `Failed` and optional `Exception` can be provided for logging purposes.

##### Cursor
A string payload that is provided to the next execution of the action.

##### Records
A list of Records that have been discovered by the action that should be queued for submission to Records 365.

##### Aggregations
A list of Aggregations that have been discovered by the action that should be queued for submission to Records 365.

##### SemaphoreLockType
See [Semaphore Lock](../semaphore_lock.md) for a detailed explanation of the Semaphore Locking capbility provided by the SDK.

##### NextDelay
A delay in seconds that overrides the configured execution delay for the next scheduled execution of the operation.

---
#### Result Types {#result-types}

##### Complete
Indicates that execution has completed successfully and the operation should be rescheduled for its next execution following the configured delay rules.

##### Incomplete
Indicates that execution for the current invocation is complete, however more work needs to be executed immediately.  This is effectively the same as the `Complete` result type, however schedules the next invocation for immediate execution without a delay.

##### Failed
Indicates that the invocation experienced an error during execution.  The SDK will retry the execution with the same stateful context until the maximum number of retries has been exceeded.

##### Abandonded
Indicates that subsequent executions of the operation should cease.  This would typically be used where the action determines the Channel being synchronised no longer exists on the Content Source and therefore the operation cannot continue.

##### BackOff
Indicates that the execution of the action should be repeated with the same stateful context after a provided delay period (`NextDelay`).  This should be used in scenarios where the action is experiencing Rate Limiting by the Content Source.

---

### Settings

Configurable Options for Content Registration:
```
"ContentManager": {
    "ContentRegistration": {
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