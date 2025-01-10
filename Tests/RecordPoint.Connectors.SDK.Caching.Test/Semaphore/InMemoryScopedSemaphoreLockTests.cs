using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Caching.Semaphore;
using RecordPoint.Connectors.SDK.Test;
using RecordPoint.Connectors.SDK.Test.Common.Mock.Caching.Semaphore;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Caching.Test.Semaphore
{
    /// <summary>
    /// SUT for In Memory Scoped Semaphore Lock tests
    /// </summary>
    public class InMemoryScopedSemaphoreLockSut : CommonSutBase
    {
        public MockSemaphoreLockScopedKeyAction SemaphoreLockScopedKeyAction { get; set; } = new();

        protected override IHostBuilder CreateSutBuilder()
        {
            return base
                .CreateSutBuilder()
                .UseInMemorySemaphoreLock()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddScoped<ISemaphoreLockScopedKeyAction>(a => SemaphoreLockScopedKeyAction);
                });
        }
    }

    public class InMemoryScopedSemaphoreLockTests : CommonTestBase<InMemoryScopedSemaphoreLockSut>
    {
        private const string WorkTypeContext1 = "TestOperation1";

        [Fact]
        public void InMemorySemaphoreLockManager_IsRegistered()
        {
            //Don't use the SUT for this test as we want to provide test coverage for the host builder extension.

            var builder = Host.CreateDefaultBuilder()
                .UseInMemorySemaphoreLock<MockSemaphoreLockScopedKeyAction>();

            SUT.Host = builder.Build();

            var semaphoreLockManager = SUT.Host.Services.GetRequiredService<ISemaphoreLockManager>();
            var semaphoreLockScopedKeyAction = SUT.Host.Services.GetRequiredService<ISemaphoreLockScopedKeyAction>();

            Assert.NotNull(semaphoreLockManager);
            Assert.NotNull(semaphoreLockScopedKeyAction);
        }

        [Fact]
        public async Task HasNoLock_WithoutSettingSemaphore()
        {
            await StartSutAsync();
            var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();
            semaphoreLockManager.ConnectorConfiguration = new();

            var result = await semaphoreLockManager.GetSemaphoreAsync(WorkTypeContext1, null, CancellationToken.None);
            Assert.Null(result);
        }

        [Fact]
        public async Task CannotSet_ScopedSemaphoreLock_WithoutConnectorConfiguration()
        {
            const int lockDuration = 60;

            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();

            try
            {
                await semaphoreLockManager.SetSemaphoreAsync(SemaphoreLockType.Scoped, WorkTypeContext1, null, lockDuration, cancellationToken);
            }
            catch (Exception ex)
            {
                Assert.IsType<RequiredValueNullException>(ex);
                Assert.Equal("Value ConnectorConfiguration cannot be null.", ex.Message);
            }
        }

        [Fact]
        public async Task CannotGet_ScopedSemaphoreLock_WithoutConnectorConfiguration()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();

            try
            {
                var result = await semaphoreLockManager.GetSemaphoreAsync(WorkTypeContext1, null, cancellationToken);
            }
            catch (Exception ex)
            {
                Assert.IsType<RequiredValueNullException>(ex);
                Assert.Equal("Value ConnectorConfiguration cannot be null.", ex.Message);
            }
        }

        [Fact]
        public async Task CanSet_ScopedSemaphoreLock()
        {
            const int lockDuration = 60;

            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();
            semaphoreLockManager.ConnectorConfiguration = new();

            await semaphoreLockManager.SetSemaphoreAsync(SemaphoreLockType.Scoped, WorkTypeContext1, null, lockDuration, cancellationToken);

            var result = await semaphoreLockManager.GetSemaphoreAsync(WorkTypeContext1, null, cancellationToken);
            var semaporeDuration = result.Value - DateTimeOffset.Now;

            Assert.NotNull(result);
            Assert.NotEqual(0, semaporeDuration.TotalSeconds);
        }

        [Fact]
        public async Task SemaphoreFromDifferentScope_IsIgnored()
        {
            const int lockDuration = 60;

            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();
            semaphoreLockManager.ConnectorConfiguration = new();

            await semaphoreLockManager.SetSemaphoreAsync(SemaphoreLockType.Scoped, WorkTypeContext1, null, lockDuration, cancellationToken);

            SUT.SemaphoreLockScopedKeyAction.Key = "KEY_456";

            var result = await semaphoreLockManager.GetSemaphoreAsync(WorkTypeContext1, null, cancellationToken);

            Assert.Null(result);
        }

        [Fact]
        public async Task CheckWaitIsDelayed_WhenSemaphoreLockIsActive()
        {
            const int lockDuration = 5;

            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();
            semaphoreLockManager.ConnectorConfiguration = new();

            await semaphoreLockManager.SetSemaphoreAsync(SemaphoreLockType.Scoped, WorkTypeContext1, null, lockDuration, cancellationToken);

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            await semaphoreLockManager.CheckWaitSemaphoreAsync(WorkTypeContext1, null, cancellationToken);
            stopWatch.Stop();

            Assert.True(stopWatch.Elapsed.Seconds > 3);
        }

        [Fact]
        public async Task CheckWaitIsNotDelayed_WhenSemaphoreLockFromAnotherScopeIsActive()
        {
            const int lockDuration = 5;

            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();
            semaphoreLockManager.ConnectorConfiguration = new();

            await semaphoreLockManager.SetSemaphoreAsync(SemaphoreLockType.Scoped, WorkTypeContext1, null, lockDuration, cancellationToken);

            SUT.SemaphoreLockScopedKeyAction.Key = "KEY_456";

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            await semaphoreLockManager.CheckWaitSemaphoreAsync(WorkTypeContext1, null, cancellationToken);
            stopWatch.Stop();

            Assert.True(stopWatch.Elapsed.Seconds < 2);
        }


        [Fact]
        public async Task SemaphoreLockScopedKeyAction_IsExecuted()
        {
            const int lockDuration = 5;

            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();
            semaphoreLockManager.ConnectorConfiguration = new();

            await semaphoreLockManager.SetSemaphoreAsync(SemaphoreLockType.Scoped, WorkTypeContext1, null, lockDuration, cancellationToken);
            Assert.Equal(1, SUT.SemaphoreLockScopedKeyAction.ExecutionCount);

            await semaphoreLockManager.GetSemaphoreAsync(WorkTypeContext1, null, cancellationToken);
            Assert.Equal(2, SUT.SemaphoreLockScopedKeyAction.ExecutionCount);

            SUT.SemaphoreLockScopedKeyAction.Key = "KEY_456";

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            await semaphoreLockManager.CheckWaitSemaphoreAsync(WorkTypeContext1, null, cancellationToken);
            stopWatch.Stop();

            Assert.Equal(3, SUT.SemaphoreLockScopedKeyAction.ExecutionCount);
        }
    }
}
