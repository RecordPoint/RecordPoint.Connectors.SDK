# Aggregation Submission

Audit Submission is one of a few responsibilities that are simple to implement as there is no associated action.
The operation is entirely managed by the SDK so only requires dependency injection configuration to setup the operation.

The Audit Submission Operation is invoked by the SDK when Audits have been identified for submission (by the Content Synchronisation operation).

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
            .UseAuditEventSubmissionOperation();

        return builder.HostBuilder;
    }
}
```