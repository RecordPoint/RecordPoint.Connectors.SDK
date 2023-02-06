using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.Test;
using RecordPoint.Connectors.SDK.Test.Mock.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.Test.Content
{
    public class DatabaseAggregationManagerSut : CommonSutBase
    {
        protected override IHostBuilder CreateSutBuilder()
        {
            return base
                .CreateSutBuilder()
                .UseMockConnectorDatabase()
                .UseDatabaseAggregationManager();
        }

        public override async Task StopSUTAsync()
        {
            var databaseProvider = Services.GetRequiredService<IConnectorDatabaseProvider>();
            await databaseProvider.RemoveAsync(CancellationToken.None);
            await base.StopSUTAsync();
        }
    }

    public class DatabaseAggregationManagerTests : CommonTestBase<DatabaseAggregationManagerSut>
    {
        private static AggregationModel CreateAggregationModel(string connectorId = null)
            => new()
            {
                ConnectorId = connectorId ?? Guid.NewGuid().ToString(),
                ExternalId = Guid.NewGuid().ToString(),
                Title = Guid.NewGuid().ToString(),
                MetaData = JsonConvert.SerializeObject(new List<MetaDataItem> {
                    new MetaDataItem {
                        Name = Guid.NewGuid().ToString(),
                        Type = "String",
                        Value = Guid.NewGuid().ToString()
                    }
                })
            };

        [Fact]
        public async Task DatabaseAggregationManager_CanAdd_NewAggregation()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var aggregationManager = Services.GetRequiredService<IAggregationManager>();
            var databaseProvider = Services.GetRequiredService<IConnectorDatabaseProvider>();

            await aggregationManager.UpsertAggregationAsync(CreateAggregationModel(), cancellationToken);

            using var dbContext = databaseProvider.CreateDbContext();
            Assert.Equal(1, dbContext.Aggregations.Count());
        }

        [Fact]
        public async Task DatabaseAggregationManager_CanGet_Aggregations()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var aggregationManager = Services.GetRequiredService<IAggregationManager>();

            await aggregationManager.UpsertAggregationAsync(CreateAggregationModel(), cancellationToken);
            await aggregationManager.UpsertAggregationAsync(CreateAggregationModel(), cancellationToken);

            var aggregations = await aggregationManager.GetAggregationsAsync(cancellationToken);

            Assert.Equal(2, aggregations.Count);
        }

        [Fact]
        public async Task DatabaseAggregationManager_CanGet_Aggregation()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var aggregationManager = Services.GetRequiredService<IAggregationManager>();

            var createdAggregation = CreateAggregationModel();
            await aggregationManager.UpsertAggregationAsync(createdAggregation, cancellationToken);

            var retrievedAggregation = await aggregationManager.GetAggregationAsync(createdAggregation.ConnectorId, createdAggregation.ExternalId, cancellationToken);

            Assert.True(createdAggregation.Equals(retrievedAggregation));
        }

        [Fact]
        public async Task DatabaseAggregationManager_CanUpdate_ExistingAggregations()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var aggregationManager = Services.GetRequiredService<IAggregationManager>();

            var createdAggregation = CreateAggregationModel();
            await aggregationManager.UpsertAggregationAsync(createdAggregation, cancellationToken);

            var clonedAggregation = Clone(createdAggregation);
            clonedAggregation.Title = Guid.NewGuid().ToString();
            await aggregationManager.UpsertAggregationAsync(clonedAggregation, cancellationToken);

            var retrievedAggregation = await aggregationManager.GetAggregationAsync(createdAggregation.ConnectorId, createdAggregation.ExternalId, cancellationToken);

            Assert.False(createdAggregation.Equals(retrievedAggregation));
            Assert.True(clonedAggregation.Equals(retrievedAggregation));
        }

        [Fact]
        public async Task DatabaseAggregationManager_CanAdd_MultipleNewAggregations()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var aggregationManager = Services.GetRequiredService<IAggregationManager>();
            var databaseProvider = Services.GetRequiredService<IConnectorDatabaseProvider>();

            var createdAggregations = new List<AggregationModel> {
                CreateAggregationModel(),
                CreateAggregationModel(),
                CreateAggregationModel(),
                CreateAggregationModel()
            };
            await aggregationManager.UpsertAggregationsAsync(createdAggregations, cancellationToken);

            using var dbContext = databaseProvider.CreateDbContext();
            Assert.Equal(4, dbContext.Aggregations.Count());
        }

        [Fact]
        public async Task DatabaseAggregationManager_CanAddAndUpdate_MultipleAggregations()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var aggregationManager = Services.GetRequiredService<IAggregationManager>();
            var databaseProvider = Services.GetRequiredService<IConnectorDatabaseProvider>();

            var createdAggregations = new List<AggregationModel> {
                CreateAggregationModel(),
                CreateAggregationModel(),
                CreateAggregationModel(),
                CreateAggregationModel()
            };
            await aggregationManager.UpsertAggregationsAsync(createdAggregations, cancellationToken);

            var updatedAggregations = new List<AggregationModel>
            {
                createdAggregations[0],
                createdAggregations[1],
                CreateAggregationModel(),
                CreateAggregationModel()
            };

            updatedAggregations[0].Title = Guid.NewGuid().ToString();
            updatedAggregations[1].Title = Guid.NewGuid().ToString();

            await aggregationManager.UpsertAggregationsAsync(updatedAggregations, cancellationToken);

            using var dbContext = databaseProvider.CreateDbContext();
            Assert.Equal(6, dbContext.Aggregations.Count());
        }

        [Fact]
        public async Task DatabaseAggregationManager_Aggregation_Exists()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var aggregationManager = Services.GetRequiredService<IAggregationManager>();

            var createdAggregation = CreateAggregationModel();
            await aggregationManager.UpsertAggregationAsync(createdAggregation, cancellationToken);

            var exists = await aggregationManager.AggregationExistsAsync(createdAggregation.ConnectorId, createdAggregation.ExternalId, cancellationToken);

            Assert.True(exists);
        }

        [Fact]
        public async Task DatabaseAggregationManager_Aggregation_DoesNotExist()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var aggregationManager = Services.GetRequiredService<IAggregationManager>();

            var createdAggregation = CreateAggregationModel();
            await aggregationManager.UpsertAggregationAsync(createdAggregation, cancellationToken);

            var newAggregation = CreateAggregationModel();
            var exists = await aggregationManager.AggregationExistsAsync(newAggregation.ConnectorId, newAggregation.ExternalId, cancellationToken);

            Assert.False(exists);
        }

        [Fact]
        public async Task DatabaseAggregationManager_CanRemove_Aggregation()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var aggregationManager = Services.GetRequiredService<IAggregationManager>();
            var databaseProvider = Services.GetRequiredService<IConnectorDatabaseProvider>();

            var createdAggregation = CreateAggregationModel();
            await aggregationManager.UpsertAggregationAsync(createdAggregation, cancellationToken);

            await aggregationManager.RemoveAggregationAsync(createdAggregation.ConnectorId, createdAggregation.ExternalId, cancellationToken);

            using var dbContext = databaseProvider.CreateDbContext();
            Assert.Equal(0, dbContext.Aggregations.Count());
        }

        [Fact]
        public async Task DatabaseAggregationManager_CanRemove_Aggregations()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var aggregationManager = Services.GetRequiredService<IAggregationManager>();
            var databaseProvider = Services.GetRequiredService<IConnectorDatabaseProvider>();

            var connectorId = Guid.NewGuid().ToString();
            var createdAggregations = new List<AggregationModel> {
                CreateAggregationModel(connectorId),
                CreateAggregationModel(connectorId),
                CreateAggregationModel(connectorId),
                CreateAggregationModel(connectorId)
            };
            await aggregationManager.UpsertAggregationsAsync(createdAggregations, cancellationToken);

            var externalIds = createdAggregations.Select(a => a.ExternalId).ToArray();
            await aggregationManager.RemoveAggregationsAsync(connectorId, externalIds, cancellationToken);

            using var dbContext = databaseProvider.CreateDbContext();
            Assert.Equal(0, dbContext.Aggregations.Count());
        }
    }
}
