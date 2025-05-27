# RecordPoint.Connectors.SDK.Caching

[RecordPoint.Connectors.SDK.Caching Api Documentation](./recordpoint_connectors_sdk_caching_doc.md)

The Caching package contains services and providers for Caching.

This package also includes the In-Memory provider for the [Semaphore Lock](../semaphore_lock.md) feature.

Within this package is a simple caching interface and action that provides an easy to use solution for caching data.
At present there is only an In-Memory provider, however it should be trivial to implement additional providers using Redis or Cosmos.

The cache solution is made up of implementations of the `ICaache` and `ICacheAction` interfaces,

* The `ICache` interface provides the `GetAsync(string key, CacheActionContext context)` method which checks the cache for the requested item (specified by Key).
* The `ICacheAction` interface provides the `ExecuteAsync(CacheActionContext context)` method which is used to obtain a value to add to the cache when an existing value cannot be found within the cache.

### Dependency Injection
To setup the cache service, you need to register the provider and action via dependency injection.

*Note: The example code below omits a lot of the required dependency injection setup.  For a full example, see [Dependency Injection](../dependency_injection.md)*

When registering the Cache Provider, you must specify both the `ICacheAction` implementation as well as the type of the values that are being cached.

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

        builder.HostBuilder.UseInMemoryCache<CacheAction, CacheValueType>();

        return builder.HostBuilder;
    }
}
```

---

### Implementation

```cs
public interface ICache<TCacheItemType>
{
    Task<TCacheItemType?> GetAsync(string key, CacheActionContext context);
}

public interface ICacheAction<TCacheItemType>
{
    Task<CacheActionResult<TCacheItemType>> ExecuteAsync(CacheActionContext context);
}

public class CacheActionContext
{
    public Dictionary<string, object> Properties { get; set; } = new();
}

public class CacheActionResult<TCacheItemType>
{
    public TCacheItemType? CacheItem { get; set; }
    public DateTimeOffset? Expires { get; set; }
}
```

---

### Action Execution
The `ICacheAction` is executed when a requested item in the cache cannot be found.
The return result should include a new item to add to the cache.
Context is provided to the action when the `GetAsync(...)` method is called on the Cache provider.
The Context is a `Dictionary<string, object>` that can include anything that will allow the Action to return a valid item.

---
