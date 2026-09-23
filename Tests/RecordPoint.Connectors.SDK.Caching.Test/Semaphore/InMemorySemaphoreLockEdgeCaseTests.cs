#nullable enable
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Caching.Semaphore;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Test;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Caching.Test.Semaphore
{
    /// <summary>
    /// A scoped key action that always resolves to an empty key, used to exercise
    /// the empty-key guard in <see cref="InMemorySemaphoreLockManager"/>.
    /// </summary>
    public class EmptyKeyScopedKeyAction : ISemaphoreLockScopedKeyAction
    {
        public Task<string> ExecuteAsync(ConnectorConfigModel connectorConfigModel, string workType, object? context, CancellationToken cancellationToken)
            => Task.FromResult(string.Empty);
    }

    /// <summary>
    /// SUT whose scoped key action always returns an empty key.
    /// </summary>
    public class EmptyKeyScopedSemaphoreLockSut : CommonSutBase
    {
        protected override IHostBuilder CreateSutBuilder()
        {
            return base
                .CreateSutBuilder()
                .UseInMemorySemaphoreLock<EmptyKeyScopedKeyAction>();
        }
    }

    public class InMemorySemaphoreLockEmptyKeyTests : CommonTestBase<EmptyKeyScopedSemaphoreLockSut>
    {
        private const string WorkType = "TestOperation";

        [Fact]
        public async Task SetSemaphoreAsync_Scoped_Throws_WhenScopedKeyResolvesToEmpty()
        {
            await StartSutAsync();
            var semaphoreLockManager = Services!.GetRequiredService<ISemaphoreLockManager>();
            semaphoreLockManager.ConnectorConfiguration = new();

            var ex = await Assert.ThrowsAsync<RequiredValueNullException>(() =>
                semaphoreLockManager.SetSemaphoreAsync(SemaphoreLockType.Scoped, WorkType, null, 60, CancellationToken.None));

            Assert.Equal("Value semaphoreKey cannot be null.", ex.Message);
        }
    }

    public class InMemorySemaphoreLockExpiryTests : CommonTestBase<InMemoryScopedSemaphoreLockSut>
    {
        private const string WorkType = "TestOperation";

        [Fact]
        public async Task GetSemaphoreAsync_ReturnsLongestExpiry_WhenGlobalLockOutlastsScopedLock()
        {
            await StartSutAsync();
            var semaphoreLockManager = Services!.GetRequiredService<ISemaphoreLockManager>();
            semaphoreLockManager.ConnectorConfiguration = new();

            // Global lock lasts longer than the scoped lock. GetSemaphore iterates the
            // global key first (setting the running max), then the scoped key whose
            // expiry is shorter, exercising the "existing max is not less" branch.
            const int globalLockSeconds = 600;
            const int scopedLockSeconds = 30;
            await semaphoreLockManager.SetSemaphoreAsync(SemaphoreLockType.Global, WorkType, null, globalLockSeconds, CancellationToken.None);
            await semaphoreLockManager.SetSemaphoreAsync(SemaphoreLockType.Scoped, WorkType, null, scopedLockSeconds, CancellationToken.None);

            var result = await semaphoreLockManager.GetSemaphoreAsync(WorkType, null, CancellationToken.None);

            Assert.NotNull(result);
            var remaining = (result!.Value - DateTimeOffset.Now).TotalSeconds;
            // The manager must return the longest-lived lock, so the remaining time should
            // track the global lock (600s), not the scoped lock (30s). Assert it sits within
            // the global window, allowing a generous margin for elapsed test/CI time. This
            // range (540-600s) is nowhere near the 30s scoped expiry, so it unambiguously
            // proves the global lock won without being brittle to timing jitter.
            Assert.InRange(remaining, globalLockSeconds - 60, globalLockSeconds);
        }
    }
}
