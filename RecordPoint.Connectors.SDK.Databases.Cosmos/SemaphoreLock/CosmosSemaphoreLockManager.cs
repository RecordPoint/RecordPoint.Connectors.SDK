using Microsoft.Extensions.DependencyInjection;
using RecordPoint.Connectors.SDK.Caching;
using RecordPoint.Connectors.SDK.Caching.Semaphore;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Databases.Cosmos.Manager;
using RecordPoint.Connectors.SDK.Databases.Cosmos.SemephoreLock;

namespace RecordPoint.Connectors.SDK.Databases.SemephoreLock;

/// <summary>
/// The cosmos semaphore lock manager.
/// </summary>
public class CosmosSemaphoreLockManager : ISemaphoreLockManager
{
    /// <summary>
    /// The GLOBAL SEMAPHORE KEY.
    /// </summary>
    private const string GLOBAL_SEMAPHORE_KEY = "SEMAPHORE_GLOBAL";
    /// <summary>
    /// Gets or sets the connector configuration.
    /// </summary>
    public ConnectorConfigModel? ConnectorConfiguration { get; set; }
    /// <summary>
    /// The semaphore db manager.
    /// </summary>
    private readonly ICosmosDbManager<SemaphoreLockCosmosDbItem> _semaphoreDbManager;
    /// <summary>
    /// The service provider.
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="CosmosSemaphoreLockManager"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="semaphoreDbManager">The semaphore db manager.</param>
    public CosmosSemaphoreLockManager(IServiceProvider serviceProvider, ICosmosDbManager<SemaphoreLockCosmosDbItem> semaphoreDbManager)
    {
        _semaphoreDbManager = semaphoreDbManager;
        _serviceProvider = serviceProvider;

    }
    /// <summary>
    /// Check wait semaphore asynchronously.
    /// </summary>
    /// <param name="workType">The work type.</param>
    /// <param name="context">Context for lock keys when external apis have different restrictions, ie: by channel</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task</returns>
    public async Task CheckWaitSemaphoreAsync(string workType, object? context, CancellationToken cancellationToken)
    {
        var lockExpiry = await GetSemaphoreAsync(workType, context, cancellationToken);
        if (lockExpiry.HasValue)
        {
            var delay = lockExpiry.Value - DateTimeOffset.Now;
            await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Get the semaphore asynchronously.
    /// </summary>
    /// <param name="workType">The work type.</param>
    /// <param name="context">Context for lock keys when external apis have different restrictions, ie: by channel</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns><![CDATA[Task<DateTimeOffset?>]]></returns>
    public async Task<DateTimeOffset?> GetSemaphoreAsync(string workType, object? context, CancellationToken cancellationToken)
    {
        DateTimeOffset? semaphoreExpiry = null;

        var semaphoreKeys = await GetSemaphoreKeysAsync(workType, context, cancellationToken);
        foreach (var semaphoreKey in semaphoreKeys)
        {
            var item = await _semaphoreDbManager.GetAsync(semaphoreKey, semaphoreKey, cancellationToken);
            if (item != null && (semaphoreExpiry == null || semaphoreExpiry < item.LockExpiry))
            {
                semaphoreExpiry = item.LockExpiry;
            }
        }

        return semaphoreExpiry;
    }

    /// <summary>
    /// Set the semaphore asynchronously.
    /// </summary>
    /// <param name="semaphoreLockType">The semaphore lock type.</param>
    /// <param name="workType">The work type.</param>
    /// <param name="context">Context for lock keys when external apis have different restrictions, ie: by channel</param>
    /// <param name="duration">The duration.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <exception cref="RequiredValueNullException"></exception>
    /// <returns>A Task</returns>
    public async Task SetSemaphoreAsync(SemaphoreLockType semaphoreLockType, string workType, object? context, int duration, CancellationToken cancellationToken)
    {
        var semaphoreKey = semaphoreLockType == SemaphoreLockType.Global
            ? GLOBAL_SEMAPHORE_KEY
            : await GetScopedSemaphoreKeyAsync(workType, context, cancellationToken);

        if (string.IsNullOrEmpty(semaphoreKey)) throw new RequiredValueNullException(nameof(semaphoreKey));

        var semaphoreExpiry = DateTimeOffset.Now.AddSeconds(duration);
        var item = new SemaphoreLockCosmosDbItem()
        {
            Id = semaphoreKey,
            LockExpiry = semaphoreExpiry,
            Ttl = duration
        };

        await _semaphoreDbManager.UpsertAsync(item.Id, item, cancellationToken);
    }

    /// <summary>
    /// Get scoped semaphore key asynchronously.
    /// </summary>
    /// <param name="workType">The work type.</param>
    /// <param name="context">Context for lock keys when external apis have different restrictions, ie: by channel</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <exception cref="RequiredValueNullException"></exception>
    /// <returns><![CDATA[Task<string>]]></returns>
    private async Task<string> GetScopedSemaphoreKeyAsync(string workType, object? context, CancellationToken cancellationToken)
    {
        if (ConnectorConfiguration == null)
        {
            throw new RequiredValueNullException(nameof(ConnectorConfiguration));
        }

        var action = _serviceProvider.GetRequiredService<ISemaphoreLockScopedKeyAction>();
        var key = await action.ExecuteAsync(ConnectorConfiguration, workType, context, cancellationToken);
        return string.IsNullOrEmpty(key) ? string.Empty : key;
    }

    /// <summary>
    /// Get semaphore keys asynchronously.
    /// </summary>
    /// <param name="workType">The work type.</param>
    /// <param name="context">Context for lock keys when external apis have different restrictions, ie: by channel</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns><![CDATA[Task<string[]>]]></returns>
    private async Task<string[]> GetSemaphoreKeysAsync(string workType, object? context, CancellationToken cancellationToken)
    {
        var keys = new List<string>
            {
                GLOBAL_SEMAPHORE_KEY
            };

        var action = _serviceProvider.GetService<ISemaphoreLockScopedKeyAction>();
        if (action != null)
        {
            var key = await GetScopedSemaphoreKeyAsync(workType, context, cancellationToken);
            if (!string.IsNullOrEmpty(key)) keys.Add(key);
        }

        return keys.ToArray();
    }
}
