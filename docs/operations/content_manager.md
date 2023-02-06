# Content Manager
The Content Manager Operation is the main orchestration point of the Connector and monitors Connector Configurations
to ensure Channel Discovery operations are started for each configuration.

*  Monitors known Connector Configurations and spawns Channel Discovery Operations for new Connector Configurations.
*  Cleans up completed Work from the state database.
*  Cleans up Channels that are no longer valid.

### Dependency Injection
There is no implementation logic required for the Channel Manager, only a service with the required Depenency Injection configuration.

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

        builder.HostBuilder.UseContentManagerService();

        return builder.HostBuilder;
    }
}
```

---
### Settings

Configurable Options for the Content Manager:
```
"ContentManager": {
    "DelaySeconds": 300,
    "CleanUpChannels": true,
    "RemoveCompletedWork": true,
    "MaxCompletedWorkAge": 300,
    "MaxAbandonedChannelSynchronisationAge": 300
}
```

##### DelaySeconds: 
Number of seconds to delay betwwen Operation executions 

Default: 300 seconds (5 minutes)

##### CleanUpChannels:
Automatically remove Channels that belong to Connectors Configurations that have been removed

Default: true

##### RemoveCompletedWork:
Automatically remove Completed Managed Work

Default: true

##### MaxCompletedWorkAge:
The max age for a Completed Managed Work Status before it is removed

Default: 300 seconds (5 minutes)

##### MaxAbandonedChannelSynchronisationAge:
The maximum age for Channel Synchronisation Work that has been abandoned due to the Channel no longer existing

Default: 300 seconds (5 minutes)