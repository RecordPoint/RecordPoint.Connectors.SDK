#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Moq;
using RecordPoint.Connectors.SDK.Databases.Cosmos.Manager;
using RecordPoint.Connectors.SDK.Databases.Cosmos.SemephoreLock;
using RecordPoint.Connectors.SDK.Observability;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos.Test.Manager
{
    /// <summary>
    /// Unit tests for <see cref="CosmosDbManager{T}"/>. The Cosmos SDK is fully mocked
    /// (CosmosClient / Container / responses) so no live Cosmos account is required.
    /// </summary>
    public class CosmosDbManagerTests
    {
        private const string DatabaseId = "test-db";
        private const string ContainerId = "test-container";
        private const string PartitionKey = "pk";
        private const string Metric = "Cosmos.RequestCharge";
        private const string Dimension = "Container";

        private static readonly string[] ExpectedIdsAbc = ["a", "b", "c"];
        private static readonly string[] ExpectedIdsXy = ["x", "y"];

        private readonly Mock<Container> _container = new();
        private readonly Mock<CosmosClient> _cosmosClient = new();
        private readonly Mock<ITelemetryTracker> _telemetry = new();

        private CosmosDbManager<SemaphoreLockCosmosDbItem> CreateSut()
        {
            _cosmosClient
                .Setup(c => c.GetContainer(DatabaseId, ContainerId))
                .Returns(_container.Object);

            return new CosmosDbManager<SemaphoreLockCosmosDbItem>(
                _cosmosClient.Object, DatabaseId, ContainerId, _telemetry.Object);
        }

        private static SemaphoreLockCosmosDbItem Item(string id = "id-1", string? etag = "etag-1")
            => new() { Id = id, ETag = etag, LockExpiry = DateTimeOffset.Now };

        private static ItemResponse<SemaphoreLockCosmosDbItem> MockItemResponse(
            SemaphoreLockCosmosDbItem resource, double charge, HttpStatusCode status = HttpStatusCode.OK)
        {
            var m = new Mock<ItemResponse<SemaphoreLockCosmosDbItem>>();
            m.Setup(r => r.Resource).Returns(resource);
            m.Setup(r => r.RequestCharge).Returns(charge);
            m.Setup(r => r.StatusCode).Returns(status);
            return m.Object;
        }

        private static FeedResponse<SemaphoreLockCosmosDbItem> MockFeedResponse(
            SemaphoreLockCosmosDbItem[] items, double charge)
        {
            var m = new Mock<FeedResponse<SemaphoreLockCosmosDbItem>>();
            m.Setup(r => r.RequestCharge).Returns(charge);
            m.Setup(r => r.GetEnumerator()).Returns(((IEnumerable<SemaphoreLockCosmosDbItem>)items).GetEnumerator());
            m.Setup(r => r.Count).Returns(items.Length);
            return m.Object;
        }

        private static CosmosException NotFound(double charge)
            => new("not found", HttpStatusCode.NotFound, 0, "activity", charge);

        private static CosmosException ServerError(double charge)
            => new("boom", HttpStatusCode.InternalServerError, 0, "activity", charge);

        // ---------------- UpsertAsync ----------------

        [Fact]
        public async Task UpsertAsync_TracksRequestCharge_OnSuccess()
        {
            var sut = CreateSut();
            _container
                .Setup(c => c.UpsertItemAsync(
                    It.IsAny<SemaphoreLockCosmosDbItem>(), It.IsAny<PartitionKey?>(),
                    It.IsAny<ItemRequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(MockItemResponse(Item(), 3.5));

            await sut.UpsertAsync(PartitionKey, Item(), CancellationToken.None);

            _telemetry.Verify(t => t.TrackMetric(Metric, 3.5, Dimension, ContainerId), Times.Once);
        }

        [Fact]
        public async Task UpsertAsync_TracksChargeAndException_OnCosmosException()
        {
            var sut = CreateSut();
            _container
                .Setup(c => c.UpsertItemAsync(
                    It.IsAny<SemaphoreLockCosmosDbItem>(), It.IsAny<PartitionKey?>(),
                    It.IsAny<ItemRequestOptions>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(ServerError(7.0));

            await Assert.ThrowsAsync<CosmosException>(() =>
                sut.UpsertAsync(PartitionKey, Item(), CancellationToken.None));

            _telemetry.Verify(t => t.TrackMetric(Metric, 7.0, Dimension, ContainerId), Times.Once);
            _telemetry.Verify(t => t.TrackException(It.IsAny<CosmosException>(), null, null), Times.Once);
        }

        // ---------------- GetAsync ----------------

        [Fact]
        public async Task GetAsync_ReturnsItem_AndTracksCharge()
        {
            var sut = CreateSut();
            var expected = Item("found");
            _container
                .Setup(c => c.ReadItemAsync<SemaphoreLockCosmosDbItem>(
                    It.IsAny<string>(), It.IsAny<PartitionKey>(),
                    It.IsAny<ItemRequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(MockItemResponse(expected, 1.1));

            var result = await sut.GetAsync(PartitionKey, "found", CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal("found", result!.Id);
            _telemetry.Verify(t => t.TrackMetric(Metric, 1.1, Dimension, ContainerId), Times.Once);
        }

        [Fact]
        public async Task GetAsync_ReturnsNull_OnNotFound_AndTracksCharge()
        {
            var sut = CreateSut();
            _container
                .Setup(c => c.ReadItemAsync<SemaphoreLockCosmosDbItem>(
                    It.IsAny<string>(), It.IsAny<PartitionKey>(),
                    It.IsAny<ItemRequestOptions>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(NotFound(0.5));

            var result = await sut.GetAsync(PartitionKey, "missing", CancellationToken.None);

            Assert.Null(result);
            _telemetry.Verify(t => t.TrackMetric(Metric, 0.5, Dimension, ContainerId), Times.Once);
        }

        [Fact]
        public async Task GetAsync_Rethrows_OnNonNotFoundCosmosException()
        {
            var sut = CreateSut();
            _container
                .Setup(c => c.ReadItemAsync<SemaphoreLockCosmosDbItem>(
                    It.IsAny<string>(), It.IsAny<PartitionKey>(),
                    It.IsAny<ItemRequestOptions>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(ServerError(4.0));

            await Assert.ThrowsAsync<CosmosException>(() =>
                sut.GetAsync(PartitionKey, "err", CancellationToken.None));

            _telemetry.Verify(t => t.TrackMetric(Metric, 4.0, Dimension, ContainerId), Times.Once);
        }

        // ---------------- DeleteAsync ----------------

        [Fact]
        public async Task DeleteAsync_TracksCharge_OnSuccess()
        {
            var sut = CreateSut();
            _container
                .Setup(c => c.DeleteItemAsync<SemaphoreLockCosmosDbItem>(
                    It.IsAny<string>(), It.IsAny<PartitionKey>(),
                    It.IsAny<ItemRequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(MockItemResponse(Item(), 2.0));

            await sut.DeleteAsync(PartitionKey, "id-1", CancellationToken.None);

            _telemetry.Verify(t => t.TrackMetric(Metric, 2.0, Dimension, ContainerId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_TracksChargeAndRethrows_OnCosmosException()
        {
            var sut = CreateSut();
            _container
                .Setup(c => c.DeleteItemAsync<SemaphoreLockCosmosDbItem>(
                    It.IsAny<string>(), It.IsAny<PartitionKey>(),
                    It.IsAny<ItemRequestOptions>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(ServerError(9.0));

            await Assert.ThrowsAsync<CosmosException>(() =>
                sut.DeleteAsync(PartitionKey, "id-1", CancellationToken.None));

            _telemetry.Verify(t => t.TrackMetric(Metric, 9.0, Dimension, ContainerId), Times.Once);
        }

        // ---------------- QuerySqlAsync ----------------

        [Fact]
        public async Task QuerySqlAsync_YieldsItems_AndTracksSummedCharge_WithPartitionKey()
        {
            var sut = CreateSut();
            var page1 = MockFeedResponse(new[] { Item("a"), Item("b") }, 2.0);
            var page2 = MockFeedResponse(new[] { Item("c") }, 3.0);

            var iterator = new Mock<FeedIterator<SemaphoreLockCosmosDbItem>>();
            iterator.SetupSequence(i => i.HasMoreResults).Returns(true).Returns(true).Returns(false);
            iterator.SetupSequence(i => i.ReadNextAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(page1).ReturnsAsync(page2);

            _container
                .Setup(c => c.GetItemQueryIterator<SemaphoreLockCosmosDbItem>(
                    It.IsAny<QueryDefinition>(), It.IsAny<string>(), It.IsAny<QueryRequestOptions>()))
                .Returns(iterator.Object);

            var results = new List<SemaphoreLockCosmosDbItem>();
            await foreach (var item in sut.QuerySqlAsync(PartitionKey, new QueryDefinition("SELECT * FROM c"), CancellationToken.None))
            {
                results.Add(item);
            }

            Assert.Equal(ExpectedIdsAbc, results.Select(r => r.Id));
            _telemetry.Verify(t => t.TrackMetric(Metric, 5.0, Dimension, ContainerId), Times.Once);
        }

        [Fact]
        public async Task QuerySqlAsync_NoPartitionKey_UsesCrossPartitionOptions()
        {
            var sut = CreateSut();
            var page = MockFeedResponse(new[] { Item("only") }, 1.0);
            var iterator = new Mock<FeedIterator<SemaphoreLockCosmosDbItem>>();
            iterator.SetupSequence(i => i.HasMoreResults).Returns(true).Returns(false);
            iterator.Setup(i => i.ReadNextAsync(It.IsAny<CancellationToken>())).ReturnsAsync(page);

            QueryRequestOptions? captured = null;
            var captureFlag = false;
            _container
                .Setup(c => c.GetItemQueryIterator<SemaphoreLockCosmosDbItem>(
                    It.IsAny<QueryDefinition>(), It.IsAny<string>(), It.IsAny<QueryRequestOptions>()))
                .Callback<QueryDefinition, string, QueryRequestOptions>((_, _, opts) => { captured = opts; captureFlag = true; })
                .Returns(iterator.Object);

            var results = new List<SemaphoreLockCosmosDbItem>();
            await foreach (var item in sut.QuerySqlAsync(string.Empty, new QueryDefinition("SELECT * FROM c"), CancellationToken.None))
            {
                results.Add(item);
            }

            Assert.Single(results);
            Assert.True(captureFlag);
            Assert.Null(captured); // whitespace partition key => default (null) request options
            _telemetry.Verify(t => t.TrackMetric(Metric, 1.0, Dimension, ContainerId), Times.Once);
        }

        [Fact]
        public async Task QuerySqlAsync_TracksAccumulatedCharge_WhenReadThrows()
        {
            var sut = CreateSut();
            var iterator = new Mock<FeedIterator<SemaphoreLockCosmosDbItem>>();
            iterator.Setup(i => i.HasMoreResults).Returns(true);
            iterator.Setup(i => i.ReadNextAsync(It.IsAny<CancellationToken>())).ThrowsAsync(ServerError(6.0));

            _container
                .Setup(c => c.GetItemQueryIterator<SemaphoreLockCosmosDbItem>(
                    It.IsAny<QueryDefinition>(), It.IsAny<string>(), It.IsAny<QueryRequestOptions>()))
                .Returns(iterator.Object);

            await Assert.ThrowsAsync<CosmosException>(async () =>
            {
                await foreach (var _ in sut.QuerySqlAsync(PartitionKey, new QueryDefinition("SELECT * FROM c"), CancellationToken.None))
                {
                }
            });

            _telemetry.Verify(t => t.TrackMetric(Metric, 6.0, Dimension, ContainerId), Times.Once);
        }

        [Fact]
        public async Task QuerySqlAsync_DoesNotTrack_WhenNoResults()
        {
            var sut = CreateSut();
            var iterator = new Mock<FeedIterator<SemaphoreLockCosmosDbItem>>();
            iterator.Setup(i => i.HasMoreResults).Returns(false);

            _container
                .Setup(c => c.GetItemQueryIterator<SemaphoreLockCosmosDbItem>(
                    It.IsAny<QueryDefinition>(), It.IsAny<string>(), It.IsAny<QueryRequestOptions>()))
                .Returns(iterator.Object);

            var results = new List<SemaphoreLockCosmosDbItem>();
            await foreach (var item in sut.QuerySqlAsync(PartitionKey, new QueryDefinition("SELECT * FROM c"), CancellationToken.None))
            {
                results.Add(item);
            }

            Assert.Empty(results);
            _telemetry.Verify(t => t.TrackMetric(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        // ---------------- QueryLinqAsync ----------------

        [Fact]
        public async Task QueryLinqAsync_YieldsItems_AndTracksSummedCharge()
        {
            var page1 = MockFeedResponse(new[] { Item("x") }, 4.0);
            var page2 = MockFeedResponse(new[] { Item("y") }, 1.0);
            var iterator = new Mock<FeedIterator<SemaphoreLockCosmosDbItem>>();
            iterator.SetupSequence(i => i.HasMoreResults).Returns(true).Returns(true).Returns(false);
            iterator.SetupSequence(i => i.ReadNextAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(page1).ReturnsAsync(page2);

            _cosmosClient.Setup(c => c.GetContainer(DatabaseId, ContainerId)).Returns(_container.Object);
            var sut = new TestableManager(_cosmosClient.Object, DatabaseId, ContainerId, _telemetry.Object, iterator.Object);

            var query = new List<SemaphoreLockCosmosDbItem>().AsQueryable();
            var results = new List<SemaphoreLockCosmosDbItem>();
            await foreach (var item in sut.QueryLinqAsync(query))
            {
                results.Add(item);
            }

            Assert.Equal(ExpectedIdsXy, results.Select(r => r.Id));
            _telemetry.Verify(t => t.TrackMetric(Metric, 5.0, Dimension, ContainerId), Times.Once);
        }

        [Fact]
        public async Task QueryLinqAsync_TracksAccumulatedCharge_WhenReadThrows()
        {
            var iterator = new Mock<FeedIterator<SemaphoreLockCosmosDbItem>>();
            iterator.Setup(i => i.HasMoreResults).Returns(true);
            iterator.Setup(i => i.ReadNextAsync(It.IsAny<CancellationToken>())).ThrowsAsync(ServerError(8.0));

            _cosmosClient.Setup(c => c.GetContainer(DatabaseId, ContainerId)).Returns(_container.Object);
            var sut = new TestableManager(_cosmosClient.Object, DatabaseId, ContainerId, _telemetry.Object, iterator.Object);

            await Assert.ThrowsAsync<CosmosException>(async () =>
            {
                await foreach (var _ in sut.QueryLinqAsync(new List<SemaphoreLockCosmosDbItem>().AsQueryable()))
                {
                }
            });

            _telemetry.Verify(t => t.TrackMetric(Metric, 8.0, Dimension, ContainerId), Times.Once);
        }

        // ---------------- GetContainerQuery ----------------

        [Fact]
        public void GetContainerQuery_ReturnsLinqQueryable()
        {
            var sut = CreateSut();
            var backing = new List<SemaphoreLockCosmosDbItem> { Item("q") }.AsQueryable().OrderBy(x => x.Id);
            _container
                .Setup(c => c.GetItemLinqQueryable<SemaphoreLockCosmosDbItem>(
                    It.IsAny<bool>(), It.IsAny<string>(), It.IsAny<QueryRequestOptions>(), It.IsAny<CosmosLinqSerializerOptions>()))
                .Returns(backing);

            var result = sut.GetContainerQuery();

            Assert.NotNull(result);
            Assert.Single(result.ToList());
        }

        // ---------------- ReplaceAsync ----------------

        [Fact]
        public async Task ReplaceAsync_ReturnsResource_AndTracksCharge()
        {
            var sut = CreateSut();
            var replaced = Item("replaced");
            _container
                .Setup(c => c.ReplaceItemAsync(
                    It.IsAny<SemaphoreLockCosmosDbItem>(), It.IsAny<string>(), It.IsAny<PartitionKey?>(),
                    It.IsAny<ItemRequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(MockItemResponse(replaced, 2.5));

            var result = await sut.ReplaceAsync(PartitionKey, Item("replaced"));

            Assert.Equal("replaced", result.Id);
            _telemetry.Verify(t => t.TrackMetric(Metric, 2.5, Dimension, ContainerId), Times.Once);
        }

        [Fact]
        public async Task ReplaceAsync_TracksChargeAndRethrows_OnCosmosException()
        {
            var sut = CreateSut();
            _container
                .Setup(c => c.ReplaceItemAsync(
                    It.IsAny<SemaphoreLockCosmosDbItem>(), It.IsAny<string>(), It.IsAny<PartitionKey?>(),
                    It.IsAny<ItemRequestOptions>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(ServerError(5.5));

            await Assert.ThrowsAsync<CosmosException>(() => sut.ReplaceAsync(PartitionKey, Item()));

            _telemetry.Verify(t => t.TrackMetric(Metric, 5.5, Dimension, ContainerId), Times.Once);
        }

        // ---------------- UpsertMatchingEtagAsync ----------------

        [Fact]
        public async Task UpsertMatchingEtagAsync_PassesEtag_ReturnsResource_AndTracksCharge()
        {
            var sut = CreateSut();
            var item = Item("etagged", "the-etag");
            ItemRequestOptions? captured = null;
            _container
                .Setup(c => c.UpsertItemAsync(
                    It.IsAny<SemaphoreLockCosmosDbItem>(), It.IsAny<PartitionKey?>(),
                    It.IsAny<ItemRequestOptions>(), It.IsAny<CancellationToken>()))
                .Callback<SemaphoreLockCosmosDbItem, PartitionKey?, ItemRequestOptions, CancellationToken>(
                    (_, _, opts, _) => captured = opts)
                .ReturnsAsync(MockItemResponse(item, 3.3));

            var result = await sut.UpsertMatchingEtagAsync(PartitionKey, item);

            Assert.Equal("etagged", result.Id);
            Assert.NotNull(captured);
            Assert.Equal("the-etag", captured!.IfMatchEtag);
            _telemetry.Verify(t => t.TrackMetric(Metric, 3.3, Dimension, ContainerId), Times.Once);
        }

        [Fact]
        public async Task UpsertMatchingEtagAsync_TracksChargeAndRethrows_OnCosmosException()
        {
            var sut = CreateSut();
            _container
                .Setup(c => c.UpsertItemAsync(
                    It.IsAny<SemaphoreLockCosmosDbItem>(), It.IsAny<PartitionKey?>(),
                    It.IsAny<ItemRequestOptions>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(ServerError(1.5));

            await Assert.ThrowsAsync<CosmosException>(() => sut.UpsertMatchingEtagAsync(PartitionKey, Item()));

            _telemetry.Verify(t => t.TrackMetric(Metric, 1.5, Dimension, ContainerId), Times.Once);
        }

        /// <summary>
        /// Test double that overrides <see cref="CosmosDbManager{T}.GetFeedIterator"/> so
        /// <c>QueryLinqAsync</c> can be exercised without a real Cosmos LINQ provider
        /// (the base implementation calls <c>ToFeedIterator()</c>, which only works against a live account).
        /// </summary>
        private sealed class TestableManager : CosmosDbManager<SemaphoreLockCosmosDbItem>
        {
            private readonly FeedIterator<SemaphoreLockCosmosDbItem> _iterator;

            public TestableManager(
                CosmosClient cosmosClient, string databaseId, string containerId,
                ITelemetryTracker telemetryTracker, FeedIterator<SemaphoreLockCosmosDbItem> iterator)
                : base(cosmosClient, databaseId, containerId, telemetryTracker)
            {
                _iterator = iterator;
            }

            public override FeedIterator<SemaphoreLockCosmosDbItem> GetFeedIterator(IQueryable<SemaphoreLockCosmosDbItem> query)
                => _iterator;
        }
    }
}
