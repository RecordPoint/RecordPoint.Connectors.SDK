#nullable enable
using Moq;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Observability;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos.Test
{
    public class CosmosDirectChannelAccessConstructionTests
    {
        [Fact]
        public void IsEnabled_IsAlwaysTrue()
        {
            var sut = new CosmosDirectChannelAccess(
                new Mock<IConnectorDatabaseClient>().Object,
                new Mock<ITelemetryTracker>().Object);

            Assert.True(sut.IsEnabled);
        }
    }
}
