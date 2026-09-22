using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using RecordPoint.Connectors.SDK.Databases.Cosmos.Manager;
using RecordPoint.Connectors.SDK.Databases.Cosmos.SemephoreLock;
using RecordPoint.Connectors.SDK.Databases.SemephoreLock;
using RecordPoint.Connectors.SDK.Caching.Semaphore;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos.Test.SemaphoreLock
{
    public class CosmosSemaphoreLockManagerTests
    {
        private const string WorkType = "TestOperation";

        private readonly Mock<ICosmosDbManager<SemaphoreLockCosmosDbItem>> _mockDbManager;
        private readonly Mock<IServiceProvider> _mockServiceProvider;
        private readonly CosmosSemaphoreLockManager _sut;

        public CosmosSemaphoreLockManagerTests()
        {
            _mockDbManager = new Mock<ICosmosDbManager<SemaphoreLockCosmosDbItem>>();
            _mockServiceProvider = new Mock<IServiceProvider>();
            _sut = new CosmosSemaphoreLockManager(_mockServiceProvider.Object, _mockDbManager.Object);
        }

        [Fact]
        public async Task GetSemaphoreAsync_ReturnsNull_WhenNoLockExists()
        {
            _mockDbManager
                .Setup(db => db.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((SemaphoreLockCosmosDbItem)null);

            var result = await _sut.GetSemaphoreAsync(WorkType, null, CancellationToken.None);

            Assert.Null(result);
        }

        [Fact]
        public async Task SetSemaphoreAsync_CreatesLock_WhenNoneExists()
        {
            var cancellationToken = CancellationToken.None;

            _mockDbManager
                .Setup(db => db.GetAsync(It.IsAny<string>(), It.IsAny<string>(), cancellationToken))
                .ReturnsAsync((SemaphoreLockCosmosDbItem)null);

            await _sut.SetSemaphoreAsync(SemaphoreLockType.Global, WorkType, null, 60, cancellationToken);

            _mockDbManager.Verify(
                db => db.UpsertAsync(It.IsAny<string>(), It.Is<SemaphoreLockCosmosDbItem>(item =>
                    item.LockExpiry > DateTimeOffset.Now.AddSeconds(50) &&
                    item.Ttl == 60),
                cancellationToken),
                Times.Once);
        }

        [Fact]
        public async Task SetSemaphoreAsync_DoesNotOverwrite_WhenLockHasTimeRemaining()
        {
            var cancellationToken = CancellationToken.None;
            var existingLock = new SemaphoreLockCosmosDbItem
            {
                Id = "SEMAPHORE_GLOBAL",
                LockExpiry = DateTimeOffset.Now.AddSeconds(600),
                Ttl = 600
            };

            _mockDbManager
                .Setup(db => db.GetAsync(It.IsAny<string>(), It.IsAny<string>(), cancellationToken))
                .ReturnsAsync(existingLock);

            await _sut.SetSemaphoreAsync(SemaphoreLockType.Global, WorkType, null, 300, cancellationToken);

            _mockDbManager.Verify(
                db => db.UpsertAsync(It.IsAny<string>(), It.IsAny<SemaphoreLockCosmosDbItem>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task SetSemaphoreAsync_OverwritesLock_WhenExistingLockExpired()
        {
            var cancellationToken = CancellationToken.None;
            var expiredLock = new SemaphoreLockCosmosDbItem
            {
                Id = "SEMAPHORE_GLOBAL",
                LockExpiry = DateTimeOffset.Now.AddSeconds(-10),
                Ttl = 60
            };

            _mockDbManager
                .Setup(db => db.GetAsync(It.IsAny<string>(), It.IsAny<string>(), cancellationToken))
                .ReturnsAsync(expiredLock);

            await _sut.SetSemaphoreAsync(SemaphoreLockType.Global, WorkType, null, 600, cancellationToken);

            _mockDbManager.Verify(
                db => db.UpsertAsync(It.IsAny<string>(), It.Is<SemaphoreLockCosmosDbItem>(item =>
                    item.LockExpiry > DateTimeOffset.Now.AddSeconds(500) &&
                    item.Ttl == 600),
                cancellationToken),
                Times.Once);
        }

        [Fact]
        public async Task GetSemaphoreAsync_ReturnsExpiry_WhenLockExists()
        {
            var expectedExpiry = DateTimeOffset.Now.AddSeconds(300);
            var lockItem = new SemaphoreLockCosmosDbItem
            {
                Id = "SEMAPHORE_GLOBAL",
                LockExpiry = expectedExpiry,
                Ttl = 300
            };

            _mockDbManager
                .Setup(db => db.GetAsync("SEMAPHORE_GLOBAL", "SEMAPHORE_GLOBAL", It.IsAny<CancellationToken>()))
                .ReturnsAsync(lockItem);

            var result = await _sut.GetSemaphoreAsync(WorkType, null, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(expectedExpiry, result.Value);
        }
    }
}
