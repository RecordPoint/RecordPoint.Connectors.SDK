# Binary Submission

Binary Submission is the process of streaming a Record binary to Records 365.

The operation requires the `IBinaryRetrievalAction` action interface to be implemented.
This action requires a return value that includes a stream to the binary on the Content Source.
The SDK itself handles the submission to Records 365.

(`IBinarySubmissionCallbackAction` may also be implemented. This is only needed if `IBinaryRetrievalAction` creates resources that must be cleaned up post-submission.)

The Binary Submission Operation is invoked by the SDK when Binaries have been identified for submission.

### Dependency Injection

*Note: The example code below omits a lot of the required dependency injection setup.  For a full example, see [Dependency Injection](../dependency_injection.md)*

*Note: As this service integrates with Records 365 via the Connector Api, we must also register the Records 365 Client via the `UseR365Integration` method.*
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

        builder.HostBuilder
            .UseR365Integration()
            .UseBinarySubmissionOperation<BinaryRetrievalAction>();

        return builder.HostBuilder;
    }
}
```

---

### Implementation
```cs
/// <summary>
/// Defines a binary retriever operation which is responsible for obtaining a stram to a binary within the Content Source.
/// </summary>
public interface IBinaryRetrievalAction
{
    /// <summary>
    /// Gets a stream to corresponding to the binary meta info
    /// </summary>
    /// <param name="connectorConfiguration">The connector configuration</param>
    /// <param name="binaryMetaInfo">Meta info to get stream for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Get binary outcome</returns>
    Task<BinaryRetrievalResult> ExecuteAsync(ConnectorConfigModel connectorConfiguration, BinaryMetaInfo binaryMetaInfo, CancellationToken cancellationToken);
}

/// <summary>
/// The result of an attempt to retrieve a single piece of content
/// </summary>
public class BinaryRetrievalResult
{
    /// <summary>
    /// Result type
    /// </summary>
    public BinaryRetrievalResultType ResultType { get; set; }

    /// <summary>
    /// Result reason
    /// </summary>
    public string Reason { get; set; } = string.Empty;

    /// <summary>
    /// Optional exception that caused the retrieval to fail
    /// </summary>
    public Exception? Exception { get; set; }

    /// <summary>
    /// Binary stream
    /// </summary>
    public Stream? Stream { get; set; }

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
/// Possible results of content retrieval operations
/// </summary>
public enum BinaryRetrievalResultType
{
    Complete,   // We successfully got the content item requested
    Failed,     // We failed to get the content item due to an error
    Deleted,    // The content cannot be obtained because it has neen deleted
    ZeroBinary, // The BinaryStream has a length of zero and can't be written to blob
    BackOff     // Content Source has throttled requests
}
```

---

### Action Execution
The Binary Submission Action has a single `ExecuteAsync(...)` method as the invocation point for the action.

The action wil receive a `BinaryMetaInfo` object that contains information that was gathered during the
Content Synchronisation or Content Registration operations, providing context for the action.

A stream to the binary should be obtained from the Content Source and returned from the action.
Do not make the stream disposable as it will be disposed within the action, preventing the SDK from submitting the binary to Records 365.

---

### Execution Results
The action requires a return result of `BinaryRetrievalResult`.  This object lets the SDK know how to handle the results of the action execution and provides data for item submission and logging.

##### ResultType
A `ResultType` is required after each execution to let the SDK know what the state of the operation is. See [Result Types](#result-types) below.

##### Reason
A string based description of the result.

##### Exception
If the result type is `Failed` and optional `Exception` can be provided for logging purposes.

#### Stream
This should hold a stream to the binary on the Content Source.

*Note: Do not make the stream disposable as it will be disposed within the action, preventing the SDK from submitting the binary to Records 365.*

##### SemaphoreLockType
See [Semaphore Lock](../semaphore_lock.md) for a detailed explanation of the Semaphore Locking capbility provided by the SDK.

##### NextDelay
A delay in seconds that overrides the configured execution delay for the next scheduled execution of the operation.

---
#### Result Types {#result-types}

##### Complete
Indicates that a stream to the binary has been successfully obtained.

##### Failed
Indicates that the invocation experienced an error during execution.  The SDK will retry the execution with the same stateful context until the maximum number of retries has been exceeded.

#### Deleted
Indicates that the Binary no longer exists on the Content Source.

#### ZeroBinary
Indicates that the size/length of the Binary is zero bytes so cannot be submitted.

##### BackOff
Indicates that the execution of the action should be repeated with the same stateful context after a provided delay period (`NextDelay`).  This should be used in scenarios where the action is experiencing Rate Limiting by the Content Source.
