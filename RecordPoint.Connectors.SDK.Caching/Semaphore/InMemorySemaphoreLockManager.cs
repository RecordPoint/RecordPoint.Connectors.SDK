using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.Caching.Semaphore
{
    public class InMemorySemaphoreLockManager : ISemaphoreLockManager
    {
        private const string GLOBAL_SEMAPHORE_KEY = "SEMAPHORE_GLOBAL";

        private readonly IMemoryCache _memoryCache;
        private readonly object _lockObject = new();

        private readonly IServiceProvider _serviceProvider;

        public InMemorySemaphoreLockManager(
            IMemoryCache memoryCache,
            IServiceProvider serviceProvider)
        {
            _memoryCache = memoryCache;
            _serviceProvider = serviceProvider;
        }

        public ConnectorConfigModel? ConnectorConfiguration { get; set; } = null;

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
                if (_memoryCache.TryGetValue(semaphoreKey, out DateTimeOffset lockExpiry) && (semaphoreExpiry == null || semaphoreExpiry < lockExpiry))
                {
                    semaphoreExpiry = lockExpiry;
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

            lock (_lockObject)
            {
                var semaphoreExpiry = DateTimeOffset.Now.AddSeconds(duration);
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = semaphoreExpiry
                };

                _memoryCache.Set(semaphoreKey, semaphoreExpiry, cacheOptions);
            }
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
}
