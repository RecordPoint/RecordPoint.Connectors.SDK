#nullable enable
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using RecordPoint.Connectors.SDK;
using RecordPoint.Connectors.SDK.Caching;
using RecordPoint.Connectors.SDK.Caching.Semaphore;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Databases.Cosmos.Manager;
using RecordPoint.Connectors.SDK.Databases.Cosmos.SemephoreLock;
using RecordPoint.Connectors.SDK.Databases.SemephoreLock;
using Xunit;
using CosmosClientException = Microsoft.Azure.Cosmos.CosmosException;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos.Test.SemaphoreLock
{
    /// <summary>
    /// Additional coverage for <see cref="CosmosSemaphoreLockManager"/> focusing on the scoped-key
    /// paths, CheckWaitSemaphoreAsync, and the CosmosException(NotFound) recovery in SetSemaphoreAsync.
    /// </summary>
    public class CosmosSemaphoreLockManagerAdditionalTests
    {
        private const string WorkType = "TestOperation";
        private const string GlobalKey = "SEMAPHORE_GLOBAL";
        private const string ScopedKey = "scoped-key";

        private readonly Mock<ICosmosDbManager<SemaphoreLockCosmosDbItem>> _dbManager = new();
        private readonly Mock<IServiceProvider> _serviceProvider = new();
        private readonly Mock<ISemaphoreLockScopedKeyAction> _scopedKeyAction = new();
        private readonly CosmosSemaphoreLockManager _sut;

        public CosmosSemaphoreLockManagerAdditionalTests()
        {
            _sut = new CosmosSemaphoreLockManager(_serviceProvider.Object, _dbManager.Object);
        }

        private void RegisterScopedKeyAction(string keyToReturn)
        {
            _scopedKeyAction
                .Setup(a => a.ExecuteAsync(It.IsAny<ConnectorConfigModel>(), It.IsAny<string>(), It.IsAny<object?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(keyToReturn);
            _serviceProvider
                .Setup(sp => sp.GetService(typeof(ISemaphoreLockScopedKeyAction)))
                .Returns(_scopedKeyAction.Object);
        }

        private static SemaphoreLockCosmosDbItem Lock(string id, DateTimeOffset expiry)
            => new() { Id = id, LockExpiry = expiry };

        [Fact]
        public async Task CheckWaitSemaphoreAsync_DoesNotDelay_WhenNoLock()
        {
            _dbManager
                .Setup(db => db.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((SemaphoreLockCosmosDbItem?)null);

            var start = DateTimeOffset.Now;
            await _sut.CheckWaitSemaphoreAsync(WorkType, null, CancellationToken.None);

            Assert.True(DateTimeOffset.Now - start < TimeSpan.FromSeconds(1));
        }

        [Fact]
        public async Task CheckWaitSemaphoreAsync_Delays_WhenLockInFuture()
        {
            var lockDuration = TimeSpan.FromMilliseconds(500);
            var expiry = DateTimeOffset.Now.Add(lockDuration);
            _dbManager
                .Setup(db => db.GetAsync(GlobalKey, GlobalKey, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Lock(GlobalKey, expiry));

            var start = DateTimeOffset.Now;
            await _sut.CheckWaitSemaphoreAsync(WorkType, null, CancellationToken.None);
            var elapsed = DateTimeOffset.Now - start;

            // The manager should have waited until roughly the lock expiry. Use a
            // wide tolerance (200ms) so the assertion verifies a meaningful delay
            // occurred without being flaky on loaded CI agents.
            Assert.True(
                elapsed >= lockDuration - TimeSpan.FromMilliseconds(200),
                $"Expected the wait to be at least {(lockDuration - TimeSpan.FromMilliseconds(200)).TotalMilliseconds}ms but was {elapsed.TotalMilliseconds}ms.");
        }

        [Fact]
        public async Task GetSemaphoreAsync_IncludesScopedKey_AndReturnsMaxExpiry()
        {
            RegisterScopedKeyAction(ScopedKey);
            var globalExpiry = DateTimeOffset.Now.AddSeconds(100);
            var scopedExpiry = DateTimeOffset.Now.AddSeconds(500);

            _dbManager.Setup(db => db.GetAsync(GlobalKey, GlobalKey, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Lock(GlobalKey, globalExpiry));
            _dbManager.Setup(db => db.GetAsync(ScopedKey, ScopedKey, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Lock(ScopedKey, scopedExpiry));

            _sut.ConnectorConfiguration = new ConnectorConfigModel();

            var result = await _sut.GetSemaphoreAsync(WorkType, null, CancellationToken.None);

            Assert.Equal(scopedExpiry, result);
        }

        [Fact]
        public async Task GetSemaphoreAsync_SkipsScopedKey_WhenActionReturnsEmpty()
        {
            RegisterScopedKeyAction(string.Empty);
            _sut.ConnectorConfiguration = new ConnectorConfigModel();
            _dbManager.Setup(db => db.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((SemaphoreLockCosmosDbItem?)null);

            var result = await _sut.GetSemaphoreAsync(WorkType, null, CancellationToken.None);

            Assert.Null(result);
            // Only the global key should have been queried.
            _dbManager.Verify(db => db.GetAsync(GlobalKey, GlobalKey, It.IsAny<CancellationToken>()), Times.Once);
            _dbManager.Verify(db => db.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task SetSemaphoreAsync_Scoped_CreatesLockWithScopedKey()
        {
            RegisterScopedKeyAction(ScopedKey);
            _sut.ConnectorConfiguration = new ConnectorConfigModel();
            _dbManager.Setup(db => db.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((SemaphoreLockCosmosDbItem?)null);

            await _sut.SetSemaphoreAsync(SemaphoreLockType.Scoped, WorkType, null, 120, CancellationToken.None);

            _dbManager.Verify(db => db.UpsertAsync(
                ScopedKey,
                It.Is<SemaphoreLockCosmosDbItem>(i => i.Id == ScopedKey && i.Ttl == 120),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task SetSemaphoreAsync_Scoped_Throws_WhenConnectorConfigurationNull()
        {
            RegisterScopedKeyAction(ScopedKey);
            _sut.ConnectorConfiguration = null;

            await Assert.ThrowsAsync<RequiredValueNullException>(() =>
                _sut.SetSemaphoreAsync(SemaphoreLockType.Scoped, WorkType, null, 60, CancellationToken.None));
        }

        [Fact]
        public async Task SetSemaphoreAsync_Scoped_Throws_WhenScopedKeyEmpty()
        {
            RegisterScopedKeyAction(string.Empty);
            _sut.ConnectorConfiguration = new ConnectorConfigModel();

            await Assert.ThrowsAsync<RequiredValueNullException>(() =>
                _sut.SetSemaphoreAsync(SemaphoreLockType.Scoped, WorkType, null, 60, CancellationToken.None));
        }

        [Fact]
        public async Task SetSemaphoreAsync_CreatesLock_WhenGetThrowsNotFound()
        {
            _dbManager
                .Setup(db => db.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new CosmosClientException("missing", HttpStatusCode.NotFound, 0, "activity", 1.0));

            await _sut.SetSemaphoreAsync(SemaphoreLockType.Global, WorkType, null, 90, CancellationToken.None);

            _dbManager.Verify(db => db.UpsertAsync(
                GlobalKey,
                It.Is<SemaphoreLockCosmosDbItem>(i => i.Id == GlobalKey && i.Ttl == 90),
                It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
