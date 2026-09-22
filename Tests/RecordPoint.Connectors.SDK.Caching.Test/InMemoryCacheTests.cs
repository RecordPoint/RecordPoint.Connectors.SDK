#nullable enable
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Test;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Caching.Test
{
    /// <summary>
    /// A configurable cache action used to drive <see cref="InMemoryCache{TCacheItemType}"/> tests.
    /// </summary>
    public class MockCacheAction : ICacheAction<string>
    {
        public string? CacheItem { get; set; } = "value";
        public DateTimeOffset? Expires { get; set; }
        public int ExecutionCount { get; set; }

        public Task<CacheActionResult<string>> ExecuteAsync(CacheActionContext context, CancellationToken cancellationToken = default)
        {
            ExecutionCount++;
            return Task.FromResult(new CacheActionResult<string>
            {
                CacheItem = CacheItem,
                Expires = Expires
            });
        }
    }

    /// <summary>
    /// SUT for <see cref="InMemoryCache{TCacheItemType}"/> tests.
    /// </summary>
    public class InMemoryCacheSut : CommonSutBase
    {
        public MockCacheAction CacheAction { get; set; } = new();

        protected override IHostBuilder CreateSutBuilder()
        {
            return base
                .CreateSutBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .AddMemoryCache()
                        .AddSingleton<ICacheAction<string>>(_ => CacheAction)
                        .AddSingleton<ICache<string>, InMemoryCache<string>>();
                });
        }
    }

    public class InMemoryCacheTests : CommonTestBase<InMemoryCacheSut>
    {
        private const string Key = "test-key";

        private static CacheActionContext Context() => new();

        [Fact]
        public async Task GetAsync_StoresItemWithoutExpiry_WhenExpiresIsNull()
        {
            await StartSutAsync();
            SUT!.CacheAction.CacheItem = "no-expiry";
            SUT.CacheAction.Expires = null;
            var cache = Services!.GetRequiredService<ICache<string>>();

            var result = await cache.GetAsync(Key, Context(), CancellationToken.None);
            Assert.Equal("no-expiry", result);
            Assert.Equal(1, SUT.CacheAction.ExecutionCount);

            // The item must be cached (no-expiry Set path), so a second call is a hit.
            var second = await cache.GetAsync(Key, Context(), CancellationToken.None);
            Assert.Equal("no-expiry", second);
            Assert.Equal(1, SUT.CacheAction.ExecutionCount);
        }

        [Fact]
        public async Task GetAsync_StoresItemWithExpiry_WhenExpiresHasValue()
        {
            await StartSutAsync();
            SUT!.CacheAction.CacheItem = "with-expiry";
            SUT.CacheAction.Expires = DateTimeOffset.Now.AddMinutes(5);
            var cache = Services!.GetRequiredService<ICache<string>>();

            var result = await cache.GetAsync(Key, Context(), CancellationToken.None);
            Assert.Equal("with-expiry", result);

            // The item must be cached with the given expiry, so a second call is a hit.
            var second = await cache.GetAsync(Key, Context(), CancellationToken.None);
            Assert.Equal("with-expiry", second);
            Assert.Equal(1, SUT.CacheAction.ExecutionCount);
        }

        [Fact]
        public async Task GetAsync_ReturnsCachedItemOnSecondCall_WithoutReExecutingAction()
        {
            await StartSutAsync();
            SUT!.CacheAction.CacheItem = "cached";
            SUT.CacheAction.Expires = DateTimeOffset.Now.AddMinutes(5);
            var cache = Services!.GetRequiredService<ICache<string>>();

            var first = await cache.GetAsync(Key, Context(), CancellationToken.None);
            var second = await cache.GetAsync(Key, Context(), CancellationToken.None);

            Assert.Equal("cached", first);
            Assert.Equal("cached", second);
            // The cache hit on the second call must short-circuit the action.
            Assert.Equal(1, SUT.CacheAction.ExecutionCount);
        }

        [Fact]
        public async Task GetAsync_ReturnsNullAndDoesNotCache_WhenActionReturnsNullItem()
        {
            await StartSutAsync();
            SUT!.CacheAction.CacheItem = null;
            var cache = Services!.GetRequiredService<ICache<string>>();

            var result = await cache.GetAsync(Key, Context(), CancellationToken.None);

            Assert.Null(result);

            // A null item is not cached, so a second call re-executes the action.
            await cache.GetAsync(Key, Context(), CancellationToken.None);
            Assert.Equal(2, SUT.CacheAction.ExecutionCount);
        }

        [Fact]
        public async Task UseInMemoryCache_RegistersCacheAndAction()
        {
            // Exercise the host builder extension directly (rather than the SUT's manual
            // wiring) so CacheBuilderExtensions.UseInMemoryCache is covered.
            using var host = Host.CreateDefaultBuilder()
                .UseInMemoryCache<MockCacheAction, string>()
                .Build();

            var cache = host.Services.GetRequiredService<ICache<string>>();
            var action = host.Services.GetRequiredService<ICacheAction<string>>();

            Assert.IsType<InMemoryCache<string>>(cache);
            Assert.IsType<MockCacheAction>(action);

            var result = await cache.GetAsync("k", Context(), CancellationToken.None);
            Assert.Equal("value", result);
        }
    }
}
