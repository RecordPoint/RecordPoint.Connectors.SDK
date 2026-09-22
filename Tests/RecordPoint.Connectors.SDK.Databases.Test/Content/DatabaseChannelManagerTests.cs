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
    public class DatabaseChannelManagerSut : CommonSutBase
    {
        protected override IHostBuilder CreateSutBuilder()
        {
            return base
                .CreateSutBuilder()
                .UseMockConnectorDatabase()
                .UseDatabaseChannelManager();
        }

        public override async Task StopSUTAsync()
        {
            var databaseProvider = Services.GetRequiredService<IConnectorDatabaseProvider>();
            await databaseProvider.RemoveAsync(CancellationToken.None);
            await base.StopSUTAsync();
        }
    }

    public class DatabaseChannelManagerTests : CommonTestBase<DatabaseChannelManagerSut>
    {
        private static ChannelModel CreateChannelModel(string connectorId = null)
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
        public async Task DatabaseChannelManager_CanAdd_NewChannel()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var channelManager = Services.GetRequiredService<IChannelManager>();
            var databaseProvider = Services.GetRequiredService<IConnectorDatabaseProvider>();

            await channelManager.UpsertChannelAsync(CreateChannelModel(), cancellationToken);

            using var dbContext = databaseProvider.CreateDbContext();
            Assert.Equal(1, dbContext.Channels.Count());
        }

        [Fact]
        public async Task DatabaseChannelManager_CanGet_Channels()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var channelManager = Services.GetRequiredService<IChannelManager>();

            await channelManager.UpsertChannelAsync(CreateChannelModel(), cancellationToken);
            await channelManager.UpsertChannelAsync(CreateChannelModel(), cancellationToken);

            var channels = await channelManager.GetChannelsAsync(cancellationToken);

            Assert.Equal(2, channels.Count);
        }

        [Fact]
        public async Task DatabaseChannelManager_CanStream_ChannelClassifications_ForConnector()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var channelManager = Services.GetRequiredService<IChannelManager>();

            var connectorId = Guid.NewGuid().ToString();
            var matching1 = CreateChannelModel(connectorId);
            var matching2 = CreateChannelModel(connectorId);
            var other = CreateChannelModel(Guid.NewGuid().ToString());

            await channelManager.UpsertChannelAsync(matching1, cancellationToken);
            await channelManager.UpsertChannelAsync(matching2, cancellationToken);
            await channelManager.UpsertChannelAsync(other, cancellationToken);

            var classifications = new List<ChannelClassificationModel>();
            await foreach (var item in channelManager.GetChannelClassificationsAsync(connectorId, 10, cancellationToken))
            {
                classifications.Add(item);
            }

            Assert.Equal(2, classifications.Count);
            Assert.Contains(classifications, c => c.ExternalId == matching1.ExternalId && c.MetaData == matching1.MetaData);
            Assert.Contains(classifications, c => c.ExternalId == matching2.ExternalId && c.MetaData == matching2.MetaData);
            Assert.DoesNotContain(classifications, c => c.ExternalId == other.ExternalId);
        }

        [Fact]
        public async Task DatabaseChannelManager_CanGet_Channel()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var channelManager = Services.GetRequiredService<IChannelManager>();

            var createdChannel = CreateChannelModel();
            await channelManager.UpsertChannelAsync(createdChannel, cancellationToken);

            var retrievedChannel = await channelManager.GetChannelAsync(createdChannel.ConnectorId, createdChannel.ExternalId, cancellationToken);

            Assert.True(createdChannel.Equals(retrievedChannel));
        }

        [Fact]
        public async Task GetChannelAsync_ReturnsNull_WhenChannelDoesNotExist()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var channelManager = Services.GetRequiredService<IChannelManager>();

            var result = await channelManager.GetChannelAsync(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                cancellationToken);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetChannelAsync_ReturnsNull_WhenConnectorIdDoesNotMatch()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var channelManager = Services.GetRequiredService<IChannelManager>();

            var createdChannel = CreateChannelModel();
            await channelManager.UpsertChannelAsync(createdChannel, cancellationToken);

            var result = await channelManager.GetChannelAsync(
                Guid.NewGuid().ToString(),
                createdChannel.ExternalId,
                cancellationToken);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetChannelAsync_ReturnsNull_WhenExternalIdDoesNotMatch()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var channelManager = Services.GetRequiredService<IChannelManager>();

            var createdChannel = CreateChannelModel();
            await channelManager.UpsertChannelAsync(createdChannel, cancellationToken);

            var result = await channelManager.GetChannelAsync(
                createdChannel.ConnectorId,
                Guid.NewGuid().ToString(),
                cancellationToken);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetChannelAsync_ReturnsCorrectChannel_WhenMultipleExist()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var channelManager = Services.GetRequiredService<IChannelManager>();

            var connectorId = Guid.NewGuid().ToString();
            var channel1 = CreateChannelModel(connectorId);
            var channel2 = CreateChannelModel(connectorId);
            var channel3 = CreateChannelModel(connectorId);

            await channelManager.UpsertChannelAsync(channel1, cancellationToken);
            await channelManager.UpsertChannelAsync(channel2, cancellationToken);
            await channelManager.UpsertChannelAsync(channel3, cancellationToken);

            var result = await channelManager.GetChannelAsync(connectorId, channel2.ExternalId, cancellationToken);

            Assert.NotNull(result);
            Assert.True(channel2.Equals(result));
        }

        [Fact]
        public async Task GetChannelAsync_ReturnsCorrectChannel_WhenSameExternalIdAcrossConnectors()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var channelManager = Services.GetRequiredService<IChannelManager>();

            var sharedExternalId = Guid.NewGuid().ToString();
            var connector1 = Guid.NewGuid().ToString();
            var connector2 = Guid.NewGuid().ToString();

            var channel1 = CreateChannelModel(connector1);
            channel1.ExternalId = sharedExternalId;
            channel1.Title = "Connector1-Channel";

            var channel2 = CreateChannelModel(connector2);
            channel2.ExternalId = sharedExternalId;
            channel2.Title = "Connector2-Channel";

            await channelManager.UpsertChannelAsync(channel1, cancellationToken);
            await channelManager.UpsertChannelAsync(channel2, cancellationToken);

            var result1 = await channelManager.GetChannelAsync(connector1, sharedExternalId, cancellationToken);
            var result2 = await channelManager.GetChannelAsync(connector2, sharedExternalId, cancellationToken);

            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.Equal("Connector1-Channel", result1.Title);
            Assert.Equal("Connector2-Channel", result2.Title);
            Assert.Equal(connector1, result1.ConnectorId);
            Assert.Equal(connector2, result2.ConnectorId);
        }

        [Fact]
        public async Task GetChannelAsync_PreservesAllProperties()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var channelManager = Services.GetRequiredService<IChannelManager>();

            var createdChannel = CreateChannelModel();

            await channelManager.UpsertChannelAsync(createdChannel, cancellationToken);

            var result = await channelManager.GetChannelAsync(
                createdChannel.ConnectorId,
                createdChannel.ExternalId,
                cancellationToken);

            Assert.NotNull(result);
            Assert.Equal(createdChannel.ConnectorId, result.ConnectorId);
            Assert.Equal(createdChannel.ExternalId, result.ExternalId);
            Assert.Equal(createdChannel.Title, result.Title);
            Assert.Equal(createdChannel.MetaData, result.MetaData);
        }

        [Fact]
        public async Task GetChannelAsync_ReturnsUpdatedChannel_AfterUpsert()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var channelManager = Services.GetRequiredService<IChannelManager>();

            var createdChannel = CreateChannelModel();
            await channelManager.UpsertChannelAsync(createdChannel, cancellationToken);

            var updatedChannel = Clone(createdChannel);
            updatedChannel.Title = "Updated Title";
            updatedChannel.MetaData = JsonConvert.SerializeObject(new List<MetaDataItem>
            {
                new MetaDataItem { Name = "Updated", Type = "String", Value = "Value" }
            });
            await channelManager.UpsertChannelAsync(updatedChannel, cancellationToken);

            var result = await channelManager.GetChannelAsync(
                createdChannel.ConnectorId,
                createdChannel.ExternalId,
                cancellationToken);

            Assert.NotNull(result);
            Assert.Equal("Updated Title", result.Title);
            Assert.Equal(updatedChannel.MetaData, result.MetaData);
        }

        [Fact]
        public async Task GetChannelAsync_ReturnsNull_AfterChannelRemoved()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var channelManager = Services.GetRequiredService<IChannelManager>();

            var createdChannel = CreateChannelModel();
            await channelManager.UpsertChannelAsync(createdChannel, cancellationToken);

            await channelManager.RemoveChannelAsync(
                createdChannel.ConnectorId,
                createdChannel.ExternalId,
                cancellationToken);

            var result = await channelManager.GetChannelAsync(
                createdChannel.ConnectorId,
                createdChannel.ExternalId,
                cancellationToken);

            Assert.Null(result);
        }

        [Fact]
        public async Task DatabaseChannelManager_CanUpdate_ExistingChannels()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var channelManager = Services.GetRequiredService<IChannelManager>();

            var createdChannel = CreateChannelModel();
            await channelManager.UpsertChannelAsync(createdChannel, cancellationToken);

            var clonedChannel = Clone(createdChannel);
            clonedChannel.Title = Guid.NewGuid().ToString();
            await channelManager.UpsertChannelAsync(clonedChannel, cancellationToken);

            var retrievedChannel = await channelManager.GetChannelAsync(createdChannel.ConnectorId, createdChannel.ExternalId, cancellationToken);

            Assert.False(createdChannel.Equals(retrievedChannel));
            Assert.True(clonedChannel.Equals(retrievedChannel));
        }

        [Fact]
        public async Task DatabaseChannelManager_CanAdd_MultipleNewChannels()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var channelManager = Services.GetRequiredService<IChannelManager>();
            var databaseProvider = Services.GetRequiredService<IConnectorDatabaseProvider>();

            var createdChannels = new List<ChannelModel> {
                CreateChannelModel(),
                CreateChannelModel(),
                CreateChannelModel(),
                CreateChannelModel()
            };
            await channelManager.UpsertChannelsAsync(createdChannels, cancellationToken);

            using var dbContext = databaseProvider.CreateDbContext();
            Assert.Equal(4, dbContext.Channels.Count());
        }

        [Fact]
        public async Task DatabaseChannelManager_CanAddAndUpdate_MultipleChannels()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var channelManager = Services.GetRequiredService<IChannelManager>();
            var databaseProvider = Services.GetRequiredService<IConnectorDatabaseProvider>();

            var createdChannels = new List<ChannelModel> {
                CreateChannelModel(),
                CreateChannelModel(),
                CreateChannelModel(),
                CreateChannelModel()
            };
            await channelManager.UpsertChannelsAsync(createdChannels, cancellationToken);

            var updatedChannels = new List<ChannelModel>
            {
                createdChannels[0],
                createdChannels[1],
                CreateChannelModel(),
                CreateChannelModel()
            };

            updatedChannels[0].Title = Guid.NewGuid().ToString();
            updatedChannels[1].Title = Guid.NewGuid().ToString();

            await channelManager.UpsertChannelsAsync(updatedChannels, cancellationToken);

            using var dbContext = databaseProvider.CreateDbContext();
            Assert.Equal(6, dbContext.Channels.Count());
        }

        [Fact]
        public async Task DatabaseChannelManager_Channel_Exists()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var channelManager = Services.GetRequiredService<IChannelManager>();

            var createdChannel = CreateChannelModel();
            await channelManager.UpsertChannelAsync(createdChannel, cancellationToken);

            var exists = await channelManager.ChannelExistsAsync(createdChannel.ConnectorId, createdChannel.ExternalId, cancellationToken);

            Assert.True(exists);
        }

        [Fact]
        public async Task DatabaseChannelManager_Channel_DoesNotExist()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var channelManager = Services.GetRequiredService<IChannelManager>();

            var createdChannel = CreateChannelModel();
            await channelManager.UpsertChannelAsync(createdChannel, cancellationToken);

            var newChannel = CreateChannelModel();
            var exists = await channelManager.ChannelExistsAsync(newChannel.ConnectorId, newChannel.ExternalId, cancellationToken);

            Assert.False(exists);
        }

        [Fact]
        public async Task DatabaseChannelManager_CanRemove_Channel()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var channelManager = Services.GetRequiredService<IChannelManager>();
            var databaseProvider = Services.GetRequiredService<IConnectorDatabaseProvider>();

            var createdChannel = CreateChannelModel();
            await channelManager.UpsertChannelAsync(createdChannel, cancellationToken);

            await channelManager.RemoveChannelAsync(createdChannel.ConnectorId, createdChannel.ExternalId, cancellationToken);

            using var dbContext = databaseProvider.CreateDbContext();
            Assert.Equal(0, dbContext.Channels.Count());
        }

        [Fact]
        public async Task DatabaseChannelManager_CanRemove_Channels()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var channelManager = Services.GetRequiredService<IChannelManager>();
            var databaseProvider = Services.GetRequiredService<IConnectorDatabaseProvider>();

            var connectorId = Guid.NewGuid().ToString();
            var createdChannels = new List<ChannelModel> {
                CreateChannelModel(connectorId),
                CreateChannelModel(connectorId),
                CreateChannelModel(connectorId),
                CreateChannelModel(connectorId)
            };
            await channelManager.UpsertChannelsAsync(createdChannels, cancellationToken);

            var externalIds = createdChannels.Select(a => a.ExternalId).ToArray();
            await channelManager.RemoveChannelsAsync(connectorId, externalIds, cancellationToken);

            using var dbContext = databaseProvider.CreateDbContext();
            Assert.Equal(0, dbContext.Channels.Count());
        }

        [Fact]
        public async Task DatabaseChannelManager_CanGet_ChannelsForConnector()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var channelManager = Services.GetRequiredService<IChannelManager>();

            var connectorId = Guid.NewGuid().ToString();
            await channelManager.UpsertChannelAsync(CreateChannelModel(connectorId), cancellationToken);
            await channelManager.UpsertChannelAsync(CreateChannelModel(connectorId), cancellationToken);
            await channelManager.UpsertChannelAsync(CreateChannelModel(), cancellationToken);

            var channels = await channelManager.GetChannelsAsync(connectorId, cancellationToken);

            Assert.Equal(2, channels.Count);
            Assert.All(channels, c => Assert.Equal(connectorId, c.ConnectorId));
        }

        [Fact]
        public async Task DatabaseChannelManager_CanGet_ChannelsByPredicate()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var channelManager = Services.GetRequiredService<IChannelManager>();

            var target = CreateChannelModel();
            await channelManager.UpsertChannelAsync(target, cancellationToken);
            await channelManager.UpsertChannelAsync(CreateChannelModel(), cancellationToken);

            var channels = await channelManager.GetChannelsAsync(
                c => c.ExternalId == target.ExternalId, cancellationToken);

            Assert.Single(channels);
            Assert.Equal(target.ExternalId, channels[0].ExternalId);
        }

        [Fact]
        public async Task DatabaseChannelManager_CanPatch_ExistingChannel()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var channelManager = Services.GetRequiredService<IChannelManager>();

            var createdChannel = CreateChannelModel();
            await channelManager.UpsertChannelAsync(createdChannel, cancellationToken);

            await channelManager.PatchChannelAsync(
                createdChannel.ConnectorId,
                createdChannel.ExternalId,
                c => c.Title = "Patched Title",
                cancellationToken);

            var result = await channelManager.GetChannelAsync(
                createdChannel.ConnectorId, createdChannel.ExternalId, cancellationToken);

            Assert.NotNull(result);
            Assert.Equal("Patched Title", result.Title);
        }

        [Fact]
        public async Task DatabaseChannelManager_Patch_MissingChannel_DoesNothing()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var channelManager = Services.GetRequiredService<IChannelManager>();

            var patched = false;
            await channelManager.PatchChannelAsync(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                c => patched = true,
                cancellationToken);

            Assert.False(patched);
        }

        [Fact]
        public async Task DatabaseChannelManager_Patch_NullAction_Throws()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var channelManager = Services.GetRequiredService<IChannelManager>();

            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                channelManager.PatchChannelAsync(
                    Guid.NewGuid().ToString(),
                    Guid.NewGuid().ToString(),
                    null,
                    cancellationToken));
        }

        [Fact]
        public async Task DatabaseChannelManager_CanRemove_ChannelsByModels()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var channelManager = Services.GetRequiredService<IChannelManager>();
            var databaseProvider = Services.GetRequiredService<IConnectorDatabaseProvider>();

            var connector1 = Guid.NewGuid().ToString();
            var connector2 = Guid.NewGuid().ToString();
            var toRemove = new List<ChannelModel>
            {
                CreateChannelModel(connector1),
                CreateChannelModel(connector1),
                CreateChannelModel(connector2)
            };
            var toKeep = CreateChannelModel(connector1);

            await channelManager.UpsertChannelsAsync(toRemove, cancellationToken);
            await channelManager.UpsertChannelAsync(toKeep, cancellationToken);

            await channelManager.RemoveChannelsAsync(toRemove, cancellationToken);

            using var dbContext = databaseProvider.CreateDbContext();
            Assert.Equal(1, dbContext.Channels.Count());
            Assert.Equal(toKeep.ExternalId, dbContext.Channels.Single().ExternalId);
        }

        [Fact]
        public async Task DatabaseChannelManager_GetChannelClassifications_InvalidPageSize_Throws()
        {
            var cancellationToken = CancellationToken.None;

            await StartSutAsync();
            var channelManager = Services.GetRequiredService<IChannelManager>();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                channelManager.GetChannelClassificationsAsync(Guid.NewGuid().ToString(), 0, cancellationToken));
        }
    }
}
