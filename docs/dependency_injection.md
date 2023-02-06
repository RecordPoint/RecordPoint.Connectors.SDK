# Dependency Injection
The Connectors SDK provides many extensions for configuring various services and providers via dependency injection.

The below code provides some example methods that configure dependency injection for various connector services.

The `CreateHostBuilder(...)` method sets up the core services for:
- Application Configuration using `appsettings.json` files
- Telemetry Tracking (Using the Null Provider.  An [Azure Application Insights provider](../docs/observability/application_insights.md) is also available)
- Feature Toggles (Using the Null Provider. A [Launch Darkly provider](../docs/packages/recordpoint_connectors_sdk_toggles_launchdarkly.md) is also available)
- Status & Health Monitoring
- A Semaphore Locking mechanism that can be useful when experiencing rate limiting from Content Sources (see [RecordPoint.Connectors.SDK.Caching](../docs/packages/recordpoint_connectors_sdk_caching.md))

```cs
public static class HostBuilderHelper
{
    public const string CompanyName = "RecordPoint";
    public const string ConnectorName = "MS Teams Connector";
    public const string ConnectorShortName = "MST";

    /// <summary>
    /// Creates a Host builder that is suitable for all Connector Services
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public static (IHostBuilder HostBuilder, IConfigurationRoot Configuration) CreateHostBuilder(string[] args)
    {
        //Setup Configuration
        var configuration = ConnectorConfigurationBuilder
            .CreateConfigurationBuilder(args, Assembly.GetExecutingAssembly())
            .Build();

        //Create Host Builder
        var hostBuilder = Host.CreateDefaultBuilder(args)
            .UseConfiguration(configuration)
            .UseSystemContext(CompanyName, ConnectorName, ConnectorShortName)
            .UseSystemTime()
            .ConfigureServices(services =>
            {
                services.AddConsoleLogging();
            });

        //Setup R365 Configuration (AppSettings)
        hostBuilder.UseR365AppSettingsConfiguration();

        //Setup Telemetry
        hostBuilder.UseNullTelemetryTracking();

        //Setup Feature Toggles
        hostBuilder.UseNullToggleProvider();

        //Use Status Manager
        hostBuilder.UseStatusManager();

        //Use Health Checker
        hostBuilder.UseHealthChecker();

        //In Memory Locking for Retrying Teams
        hostBuilder.UseInMemorySemaphoreLock();

        return (hostBuilder, configuration);
    }
```

The `CreateConnectorHostBuilder(...)` method provides additional services for:
- The Connector Database (See [RecordPoint.Connectors.SDK.Databases](../docs/packages/recordpoint_connectors_sdk_databases.md))
- The Work Queue system (See [The Managed Work Queue](./managedworkqueue.md))

```cs
    /// <summary>
    /// /Creates a Host builder that is suitable for Connector Services that perform work based operations
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public static (IHostBuilder HostBuilder, IConfigurationRoot Configuration) CreateConnectorHostBuilder(string[] args)
    {
        (var hostBuilder, var configuration) = CreateHostBuilder(args);

        //Setup Connector Database
        hostBuilder
            .UseCosmosDbConnectorDatabase()
            .UseDatabaseConnectorConfigurationManager()
            .UseDatabaseChannelManager();

        //Setup Work Queue
        hostBuilder
            .UseWorkManager()
            .UseWorkStateManager<DatabaseManagedWorkStatusManager>()
            .UseASBWorkQueue();

        return (hostBuilder, configuration);
    }
}
```