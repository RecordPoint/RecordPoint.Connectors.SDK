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
    }
}
