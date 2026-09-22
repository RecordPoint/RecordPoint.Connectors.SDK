#nullable enable
using Moq;
using RecordPoint.Connectors.SDK.Caching;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Observability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.Test.Content
{
    /// <summary>
    /// Tests the <see cref="DatabaseChannelManager"/> paths that delegate to an
    /// <see cref="IDirectChannelAccess"/> implementation whose <c>IsEnabled</c> is true.
    /// These paths cannot be exercised through the default DI wiring which always
    /// registers the no-op <see cref="NullDirectChannelAccess"/>.
    /// </summary>
    public class DatabaseChannelManagerDirectAccessTests
    {
        private static readonly string[] ExpectedExternalIds = { "a", "b" };

        private static Mock<IObservabilityScope> CreateScope()
        {
            var scope = new Mock<IObservabilityScope>();
            scope.Setup(x => x.BeginScope(It.IsAny<Dimensions>(), It.IsAny<Measures>()))
                .Returns(Mock.Of<IDisposable>());
            return scope;
        }

        private static DatabaseChannelManager CreateManager(IDirectChannelAccess directChannelAccess)
        {
            var databaseClient = new Mock<IConnectorDatabaseClient>();
            databaseClient.Setup(x => x.GetExternalSystemName()).Returns("TestSystem");

            return new DatabaseChannelManager(
                databaseClient.Object,
                CreateScope().Object,
                Mock.Of<ICache<bool>>(),
                directChannelAccess);
        }

        [Fact]
        public async Task GetChannelAsync_UsesDirectAccess_WhenEnabled()
        {
            var connectorId = Guid.NewGuid().ToString();
            var externalId = Guid.NewGuid().ToString();
            var expected = new ChannelModel { ConnectorId = connectorId, ExternalId = externalId, Title = "Direct" };

            var directAccess = new Mock<IDirectChannelAccess>();
            directAccess.SetupGet(x => x.IsEnabled).Returns(true);
            directAccess
                .Setup(x => x.ReadChannelAsync(connectorId, externalId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var manager = CreateManager(directAccess.Object);

            var result = await manager.GetChannelAsync(connectorId, externalId, CancellationToken.None);

            Assert.Same(expected, result);
            directAccess.Verify(x => x.ReadChannelAsync(connectorId, externalId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetChannelClassificationsAsync_UsesDirectAccess_WhenEnabled()
        {
            var connectorId = Guid.NewGuid().ToString();
            var classifications = new List<ChannelClassificationModel>
            {
                new() { ExternalId = "a", MetaData = "m1" },
                new() { ExternalId = "b", MetaData = "m2" }
            };

            var directAccess = new Mock<IDirectChannelAccess>();
            directAccess.SetupGet(x => x.IsEnabled).Returns(true);
            directAccess
                .Setup(x => x.ReadChannelClassificationsAsync(connectorId, It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(ToAsyncEnumerable(classifications));

            var manager = CreateManager(directAccess.Object);

            var results = new List<ChannelClassificationModel>();
            await foreach (var item in manager.GetChannelClassificationsAsync(connectorId, 10, CancellationToken.None))
            {
                results.Add(item);
            }

            Assert.Equal(2, results.Count);
            Assert.Equal(ExpectedExternalIds, results.Select(r => r.ExternalId).ToArray());
            directAccess.Verify(x => x.ReadChannelClassificationsAsync(connectorId, 10, It.IsAny<CancellationToken>()), Times.Once);
        }

        private static async IAsyncEnumerable<ChannelClassificationModel> ToAsyncEnumerable(
            IEnumerable<ChannelClassificationModel> items)
        {
            foreach (var item in items)
            {
                await Task.Yield();
                yield return item;
            }
        }
    }
}
