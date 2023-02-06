using Microsoft.Extensions.DependencyInjection;
using RecordPoint.Connectors.SDK.Caching;
using RecordPoint.Connectors.SDK.Caching.Semaphore;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Databases.Cosmos.Manager;
using RecordPoint.Connectors.SDK.Databases.Cosmos.SemephoreLock;

namespace RecordPoint.Connectors.SDK.Databases.SemephoreLock;

public class CosmosSemaphoreLockManager : ISemaphoreLockManager
{
    private const string GLOBAL_SEMAPHORE_KEY = "SEMAPHORE_GLOBAL";
    public ConnectorConfigModel? ConnectorConfiguration { get; set; }
    private readonly ICosmosDbManager<SemaphoreLockCosmosDbItem> _semaphoreDbManager;
    private readonly IServiceProvider _serviceProvider;

    public CosmosSemaphoreLockManager(IServiceProvider serviceProvider, ICosmosDbManager<SemaphoreLockCosmosDbItem> semaphoreDbManager)
    {
        _semaphoreDbManager = semaphoreDbManager;
        _serviceProvider = serviceProvider;

    }
    public async Task CheckWaitSemaphoreAsync(string workType, CancellationToken cancellationToken)
    {
        var lockExpiry = await GetSemaphoreAsync(workType, cancellationToken);
        if (lockExpiry.HasValue)
        {
            var delay = lockExpiry.Value - DateTimeOffset.Now;
            await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
        }
    }

    public async Task<DateTimeOffset?> GetSemaphoreAsync(string workType, CancellationToken cancellationToken)
    {
        DateTimeOffset? semaphoreExpiry = null;

        var semaphoreKeys = await GetSemaphoreKeysAsync(workType, cancellationToken);
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

    public async Task SetSemaphoreAsync(SemaphoreLockType semaphoreLockType, string workType, int duration, CancellationToken cancellationToken)
    {
        var semaphoreKey = semaphoreLockType == SemaphoreLockType.Global
            ? GLOBAL_SEMAPHORE_KEY
            : await GetScopedSemaphoreKeyAsync(workType, cancellationToken);

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

    private async Task<string> GetScopedSemaphoreKeyAsync(string workType, CancellationToken cancellationToken)
    {
        if (ConnectorConfiguration == null)
        {
            throw new RequiredValueNullException(nameof(ConnectorConfiguration));
        }

        var action = _serviceProvider.GetRequiredService<ISemaphoreLockScopedKeyAction>();
        var key = await action.ExecuteAsync(ConnectorConfiguration, workType, cancellationToken);
        return string.IsNullOrEmpty(key) ? string.Empty : key;
    }

    private async Task<string[]> GetSemaphoreKeysAsync(string workType, CancellationToken cancellationToken)
    {
        var keys = new List<string>
            {
                GLOBAL_SEMAPHORE_KEY
            };

        var action = _serviceProvider.GetService<ISemaphoreLockScopedKeyAction>();
        if (action != null)
        {
            var key = await GetScopedSemaphoreKeyAsync(workType, cancellationToken);
            if (!string.IsNullOrEmpty(key)) keys.Add(key);
        }

        return keys.ToArray();
    }
}
