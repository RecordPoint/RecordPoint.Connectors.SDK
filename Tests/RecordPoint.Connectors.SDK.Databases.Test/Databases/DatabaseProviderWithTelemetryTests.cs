#nullable enable
using Microsoft.EntityFrameworkCore;
using Moq;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Test.Mock.Databases;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.Test.Databases
{
    public class DatabaseProviderWithTelemetryTests
    {
        private static MockConnectorDBContext CreateContext()
            => new MockConnectorDBContext(
                new DbContextOptionsBuilder<ConnectorDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options);

        [Fact]
        public void Constructor_NullImplementation_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new DatabaseProviderWithTelemetry(null!, Mock.Of<ITelemetryTracker>()));
        }

        [Fact]
        public void GetExternalSystemName_Delegates()
        {
            var inner = new Mock<IConnectorDatabaseProvider>();
            inner.Setup(x => x.GetExternalSystemName()).Returns("System");
            var sut = new DatabaseProviderWithTelemetry(inner.Object, Mock.Of<ITelemetryTracker>());

            Assert.Equal("System", sut.GetExternalSystemName());
        }

        [Fact]
        public void GetConnectionString_Delegates()
        {
            var inner = new Mock<IConnectorDatabaseProvider>();
            inner.Setup(x => x.GetConnectionString()).Returns("conn");
            var sut = new DatabaseProviderWithTelemetry(inner.Object, Mock.Of<ITelemetryTracker>());

            Assert.Equal("conn", sut.GetConnectionString());
        }

        [Fact]
        public void Exists_Delegates()
        {
            var inner = new Mock<IConnectorDatabaseProvider>();
            inner.Setup(x => x.Exists()).Returns(true);
            var sut = new DatabaseProviderWithTelemetry(inner.Object, Mock.Of<ITelemetryTracker>());

            Assert.True(sut.Exists());
        }

        [Fact]
        public async Task LifecycleMethods_Delegate()
        {
            var inner = new Mock<IConnectorDatabaseProvider>();
            var sut = new DatabaseProviderWithTelemetry(inner.Object, Mock.Of<ITelemetryTracker>());
            var token = CancellationToken.None;

            await sut.PrepareAsync(token);
            await sut.CleanupAsync(token);
            await sut.RemoveAsync(token);
            await sut.ReadyAsync(token);
            var ex = new Exception("x");
            sut.SetReady(ex);

            inner.Verify(x => x.PrepareAsync(token), Times.Once);
            inner.Verify(x => x.CleanupAsync(token), Times.Once);
            inner.Verify(x => x.RemoveAsync(token), Times.Once);
            inner.Verify(x => x.ReadyAsync(token), Times.Once);
            inner.Verify(x => x.SetReady(ex), Times.Once);
        }

        [Fact]
        public void CreateDbContext_Delegates_OnSuccess()
        {
            var context = CreateContext();
            var inner = new Mock<IConnectorDatabaseProvider>();
            inner.Setup(x => x.CreateDbContext()).Returns(context);
            var telemetry = new Mock<ITelemetryTracker>();
            var sut = new DatabaseProviderWithTelemetry(inner.Object, telemetry.Object);

            var result = sut.CreateDbContext();

            Assert.Same(context, result);
            telemetry.Verify(x => x.TrackException(It.IsAny<Exception>(), It.IsAny<Dimensions>(), It.IsAny<Measures>()), Times.Never);
        }

        [Fact]
        public void CreateDbContext_TracksException_AndRethrows()
        {
            var thrown = new InvalidOperationException("bad");
            var inner = new Mock<IConnectorDatabaseProvider>();
            inner.Setup(x => x.CreateDbContext()).Throws(thrown);
            var telemetry = new Mock<ITelemetryTracker>();
            var sut = new DatabaseProviderWithTelemetry(inner.Object, telemetry.Object);

            var actual = Assert.Throws<InvalidOperationException>(() => sut.CreateDbContext());

            Assert.Same(thrown, actual);
            telemetry.Verify(x => x.TrackException(thrown, It.IsAny<Dimensions>(), It.IsAny<Measures>()), Times.Once);
        }

        [Fact]
        public void WithAddedTelemetry_WrapsProvider()
        {
            var inner = new Mock<IConnectorDatabaseProvider>();

            var result = inner.Object.WithAddedTelemetry(Mock.Of<ITelemetryTracker>());

            Assert.IsType<DatabaseProviderWithTelemetry>(result);
        }

        [Fact]
        public async Task ProviderDatabaseClient_ReadyAsync_Delegates()
        {
            var inner = new Mock<IConnectorDatabaseProvider>();
            var client = new ConnectorDatabaseClient(inner.Object);

            await client.ReadyAsync(CancellationToken.None);

            inner.Verify(x => x.ReadyAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
