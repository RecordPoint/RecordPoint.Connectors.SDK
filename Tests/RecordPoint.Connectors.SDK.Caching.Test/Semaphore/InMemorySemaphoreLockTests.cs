using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Caching.Semaphore;
using RecordPoint.Connectors.SDK.Test;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Caching.Test.Semaphore
{
    /// <summary>
    /// SUT for In Memory Semaphore Lock tests
    /// </summary>
    public class InMemorySemaphoreLockSut : CommonSutBase
    {
        protected override IHostBuilder CreateSutBuilder()
        {
            return base
                .CreateSutBuilder()
                .UseInMemorySemaphoreLock();
        }
    }

    public class InMemorySemaphoreLockTests : CommonTestBase<InMemorySemaphoreLockSut>
    {

        private const string WorkTypeContext1 = "TestOperation1";

        [Fact]
        public async Task InMemorySemaphoreLockManager_IsRegistered()
        {
            await StartSutAsync();
            var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();
            Assert.NotNull(semaphoreLockManager);
        }

        [Fact]
        public async Task HasNoLock_WithoutSettingSemaphore()
        {
            await StartSutAsync();
            var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();

            var result = await semaphoreLockManager.GetSemaphoreAsync(WorkTypeContext1, null, CancellationToken.None);
            Assert.Null(result);
        }

        [Fact]
        public async Task CanSet_GlobalSemaphoreLock()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();

            const int lockDuration = 60;

            await semaphoreLockManager.SetSemaphoreAsync(SemaphoreLockType.Global, WorkTypeContext1, null, lockDuration, cancellationToken);

            var result = await semaphoreLockManager.GetSemaphoreAsync(WorkTypeContext1, null, cancellationToken);
            var semaporeDuration = result.Value - DateTimeOffset.Now;

            Assert.NotNull(result);
            Assert.NotEqual(0, semaporeDuration.TotalSeconds);
        }

        [Fact]
        public async Task CheckWaitIsNotDelayed_WhenSemaphoreLockNotSet()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            await semaphoreLockManager.CheckWaitSemaphoreAsync(WorkTypeContext1, null, cancellationToken);
            stopWatch.Stop();

            Assert.True(stopWatch.Elapsed.Seconds < 1);
        }

        [Fact]
        public async Task CheckWaitIsDelayed_WhenSemaphoreLockIsActive()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();

            const int lockDuration = 5;

            await semaphoreLockManager.SetSemaphoreAsync(SemaphoreLockType.Global, WorkTypeContext1, null, lockDuration, cancellationToken);

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            await semaphoreLockManager.CheckWaitSemaphoreAsync(WorkTypeContext1, null, cancellationToken);
            stopWatch.Stop();

            Assert.True(stopWatch.Elapsed.Seconds > 3);
        }
    }
}
