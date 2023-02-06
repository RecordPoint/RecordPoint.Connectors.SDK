# Record Disposal

Record Disposal is the process of removing a Record from the Content Source.

*Note: Not all content sources support disposing of records.*

The operation requires the `IRecordDisposalAction` action interface to be implemented.

The Record Disposal Operation is invoked by the SDK when a Record Disposal Notification is received from Records 365 (see [Record.Connectors.SDK.Notifications](../packages/recordpoint_connectors_sdk_notifications.md)).

### Dependency Injection

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

        builder.HostBuilder.UseRecordDisposalOperation<RecordDisposalAction>();

        return builder.HostBuilder;
    }
}
```

---

### Implementation
```cs
/// <summary>
/// Defines a record disposal action which is responsible for removing the record from the content source.
/// </summary>
public interface IRecordDisposalAction
{
    /// <summary>
    /// Destroys the record in the content source
    /// </summary>
    /// <param name="connectorConfiguration">The connector configuration</param>
    /// <param name="record">The record to dispose</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Get binary outcome</returns>
    Task<RecordDisposalResult> ExecuteAsync(ConnectorConfigModel connectorConfiguration, Record record, CancellationToken cancellationToken);
}

/// <summary>
/// The result of an attempt to dispose a record
/// </summary>
public class RecordDisposalResult
{
    /// <summary>
    /// Result type
    /// </summary>
    public RecordDisposalResultType ResultType { get; set; }

    /// <summary>
    /// Result reason
    /// </summary>
    public string Reason { get; set; } = string.Empty;

    /// <summary>
    /// Optional exception that caused the disposal to fail
    /// </summary>
    public Exception? Exception { get; set; }

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
/// Possible results of record disposal action
/// </summary>
public enum RecordDisposalResultType
{
    Complete,   // We successfully disposed of the record
    Failed,     // We failed to dispose of the record
    Deleted,    // The record cannot be disposed because it has already been deleted
    BackOff     // Content Source has throttled requests
}
```

---

### Action Execution
The Record Disposal Action has a single `ExecuteAsync(...)` method as the invocation point for the action.

The action will receive a `Record` object that contains the location of the Record within the Content Source that must be disposed.

---

### Execution Results
The action requires a return resut of `RecordDisposalResult`.  This object lets the SDK know how to handle the results of the action execution and provides data for logging.

##### ResultType
A `ResultType` is required after each execution to let the SDK know what the state of the operation is. See [Result Types](#result-types) below.

##### Reason
A string based description of the result.

##### Exception
If the result type is `Failed` and optional `Exception` can be provided for logging purposes.

##### SemaphoreLockType
See [Semaphore Lock](../semaphore_lock.md) for a detailed explanation of the Semaphore Locking capbility provided by the SDK.

##### NextDelay
A delay in seconds that overrides the configured execution delay for the next scheduled execution of the operation.

---

#### Result Types {#result-types}

##### Complete
Indicates that the Record was successfully disposed on the Content Sourcew.

##### Failed
Indicates that the invocation experienced an error during execution.  The SDK will retry the execution with the same stateful context until the maximum number of retries has been exceeded.

#### Deleted
Indicates that the Record has already been removed from the Content Source.

##### BackOff
Indicates that the execution of the action should be repeated with the same stateful context after a provided delay period (`NextDelay`).  This should be used in scenarios where the action is experiencing Rate Limiting by the Content Source.
