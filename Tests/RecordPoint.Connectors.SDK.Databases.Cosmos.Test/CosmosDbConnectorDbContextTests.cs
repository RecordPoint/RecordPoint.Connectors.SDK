#nullable enable
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Toggles;
using RecordPoint.Connectors.SDK.Work;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos.Test
{
    /// <summary>
    /// Exercises <see cref="CosmosDbConnectorDbContext.OnModelCreating"/> by materializing the EF Core
    /// model (offline — no Cosmos connection needed) and asserting the container mappings.
    /// </summary>
    public class CosmosDbConnectorDbContextTests
    {
        private const string EmulatorConnectionString =
            "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==;";

        private static CosmosDbConnectorDbContext CreateContext()
        {
            var provider = new CosmosDbConnectorDatabaseProvider(
                new Mock<ISystemContext>().Object,
                new ConfigurationBuilder().Build(),
                new Mock<ITelemetryTracker>().Object,
                new Mock<IToggleProvider>().Object,
                Options.Create(new CosmosDbConnectorDatabaseOptions { ConnectionString = EmulatorConnectionString }));

            return (CosmosDbConnectorDbContext)provider.CreateDbContext();
        }

        [Theory]
        [InlineData(typeof(ChannelModel), "channels")]
        [InlineData(typeof(AggregationModel), "aggregations")]
        [InlineData(typeof(ConnectorConfigurationModel), "connectors")]
        [InlineData(typeof(ManagedWorkStatusModel), "managedworkstatuses")]
        public void OnModelCreating_MapsEntitiesToExpectedContainers(System.Type entityType, string expectedContainer)
        {
            using var ctx = CreateContext();

            var et = ctx.Model.FindEntityType(entityType);

            Assert.NotNull(et);
            Assert.Equal(expectedContainer, et!.GetContainer());
        }
    }
}
