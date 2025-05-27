using Microsoft.EntityFrameworkCore;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Observability;
using System.Linq.Expressions;

namespace RecordPoint.Connectors.SDK.Content
{
    /// <summary>
    /// Implementation of a Channel manager that uses the connector database for persistence
    /// </summary>
    public class DatabaseChannelManager : IChannelManager
    {
        /// <summary>
        /// 
        /// </summary>
        public const string CONNECTOR_ID_DIMENSION = "ConnectorId";

        private readonly IConnectorDatabaseClient _databaseClient;
        private readonly IObservabilityScope _observabilityScope;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseClient"></param>
        /// <param name="observabilityScope"></param>
        public DatabaseChannelManager(IConnectorDatabaseClient databaseClient, IObservabilityScope observabilityScope)
        {
            _databaseClient = databaseClient;
            _observabilityScope = observabilityScope;
        }

        /// <inheritdoc/>
        public async Task<bool> ChannelExistsAsync(string connectorId, string externalId, CancellationToken cancellationToken)
        {
            return await _observabilityScope.Invoke(GetDimensions(connectorId), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();
                return (await dbContext.Channels
                        .Where(a => a.ConnectorId == connectorId && a.ExternalId == externalId)
                        .ToListAsync(cancellationToken))
                        .Any();
            });

        }

        /// <inheritdoc/>
        public async Task<ChannelModel> GetChannelAsync(string connectorId, string externalId, CancellationToken cancellationToken)
        {
            return await _observabilityScope.Invoke(GetDimensions(connectorId), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();
                return await dbContext.Channels
                    .FirstOrDefaultAsync(a => a.ConnectorId == connectorId && a.ExternalId == externalId, cancellationToken);
            });
        }

        /// <inheritdoc/>
        public async Task<List<ChannelModel>> GetChannelsAsync(CancellationToken cancellationToken)
        {
            return await _observabilityScope.Invoke(GetDimensions(null), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();
                return await dbContext.Channels.ToListAsync(cancellationToken);
            });
        }

        /// <inheritdoc/>
        public async Task<List<ChannelModel>> GetChannelsAsync(Expression<Func<ChannelModel, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _observabilityScope.Invoke(GetDimensions(null), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();
                return await dbContext.Channels
                    .Where(predicate)
                    .ToListAsync(cancellationToken);
            });
        }

        /// <inheritdoc/>
        public async Task<List<ChannelModel>> GetChannelsAsync(string connectorId, CancellationToken cancellationToken)
        {
            return await _observabilityScope.Invoke(GetDimensions(connectorId), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();
                return await dbContext.Channels
                    .Where(a => a.ConnectorId == connectorId)
                    .ToListAsync(cancellationToken);
            });
        }

        /// <inheritdoc/>
        public async Task UpsertChannelAsync(ChannelModel channel, CancellationToken cancellationToken)
        {
            await _observabilityScope.Invoke(GetDimensions(null), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();

                var existingChannel = await dbContext.Channels
                    .FirstOrDefaultAsync(a => a.ConnectorId == channel.ConnectorId && a.ExternalId == channel.ExternalId, cancellationToken);
                if (existingChannel == null)
                {
                    dbContext.Channels.Add(channel);
                    await dbContext.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    if (!channel.Equals(existingChannel))
                    {
                        existingChannel.Title = channel.Title;
                        existingChannel.MetaData = channel.MetaData;
                        await dbContext.SaveChangesAsync(cancellationToken);
                    }
                }
            });
        }

        /// <inheritdoc/>
        public async Task UpsertChannelsAsync(List<ChannelModel> channels, CancellationToken cancellationToken)
        {
            await _observabilityScope.Invoke(GetDimensions(null), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();

                var connectorIdGroups = channels.GroupBy(a => a.ConnectorId, a => a);
                foreach (var channelsGroupedByConnectorId in connectorIdGroups)
                {
                    var externalIds = channelsGroupedByConnectorId.Select(a => a.ExternalId);
                    var existingChannels = await dbContext.Channels
                        .Where(a => a.ConnectorId == channelsGroupedByConnectorId.Key && externalIds.Contains(a.ExternalId))
                        .ToListAsync(cancellationToken);

                    var existingChannelIds = existingChannels
                        .Select(a => a.ExternalId)
                        .ToList();

                    //Handle New Channels
                    await HandleNewChannelsAsync(dbContext, channelsGroupedByConnectorId, existingChannelIds, cancellationToken);

                    //Handle Updated Channels
                    await HandleUpdatedChannelsAsync(dbContext, channelsGroupedByConnectorId, existingChannels, existingChannelIds, cancellationToken);
                }
            });
        }

        // Determines which channels do not currently existing in the cache and persists them
        private static async Task HandleNewChannelsAsync(ConnectorDbContext dbContext, IGrouping<string, ChannelModel> channelsGroupedByConnectorId, List<string> existingChannelIds, CancellationToken cancellationToken)
        {
            var newChannels = channelsGroupedByConnectorId
                .Where(a => !existingChannelIds.Contains(a.ExternalId));

            if (newChannels.Any())
            {
                dbContext.Channels.AddRange(newChannels);
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        // Determines which channels already exist in the cache and updates them
        private static async Task HandleUpdatedChannelsAsync(ConnectorDbContext dbContext, IGrouping<string, ChannelModel> channelsGroupedByConnectorId, List<ChannelModel> existingChannels, List<string> existingChannelIds, CancellationToken cancellationToken)
        {
            var updateChannels = channelsGroupedByConnectorId
                .Where(a => existingChannelIds.Contains(a.ExternalId));

            if (updateChannels.Any())
            {
                var hasUpdates = false;
                foreach (var channel in updateChannels)
                {
                    var existingChannel = existingChannels.First(a => a.ExternalId == channel.ExternalId);
                    if (!channel.Equals(existingChannel))
                    {
                        existingChannel.Title = channel.Title;
                        existingChannel.MetaData = channel.MetaData;
                        hasUpdates = true;
                    }
                }
                if (hasUpdates)
                    await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        /// <inheritdoc/>
        public async Task RemoveChannelAsync(string connectorId, string externalId, CancellationToken cancellationToken)
        {
            await _observabilityScope.Invoke(GetDimensions(connectorId), async () =>
             {
                 using var dbContext = _databaseClient.CreateDbContext();
                 var channel = await dbContext.Channels
                     .FirstOrDefaultAsync(a => a.ConnectorId == connectorId && a.ExternalId == externalId, cancellationToken);

                 if (channel == null) return;

                 dbContext.Channels.Remove(channel);
                 await dbContext.SaveChangesAsync(cancellationToken);
             });
        }

        /// <inheritdoc/>
        public async Task RemoveChannelsAsync(string connectorId, string[] externalIds, CancellationToken cancellationToken)
        {
            await _observabilityScope.Invoke(GetDimensions(connectorId), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();
                var channels = await dbContext.Channels
                    .Where(a => a.ConnectorId == connectorId && externalIds.Contains(a.ExternalId))
                    .ToListAsync(cancellationToken);

                dbContext.Channels.RemoveRange(channels);
                await dbContext.SaveChangesAsync(cancellationToken);
            });
        }

        /// <inheritdoc/>
        public async Task RemoveChannelsAsync(IEnumerable<ChannelModel> channelModels, CancellationToken cancellationToken)
        {
            await _observabilityScope.Invoke(GetDimensions(null), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();

                //Group the Channels by ConnectorId so we can efficiently select & remove the Channels from the DbSet
                var groupedChannels = channelModels
                    .GroupBy(a => a.ConnectorId)
                    .Select(a => new { ConnectorId = a.Key, Channels = a.ToList() });

                foreach (var channelGroup in groupedChannels)
                {
                    var externalIds = channelGroup.Channels.Select(a => a.ExternalId);
                    var channels = await dbContext.Channels
                        .Where(a => a.ConnectorId == channelGroup.ConnectorId && externalIds.Contains(a.ExternalId))
                        .ToListAsync(cancellationToken);

                    dbContext.Channels.RemoveRange(channels);
                }

                await dbContext.SaveChangesAsync(cancellationToken);
            });
        }


        private Dimensions GetDimensions(string connectorId)
        {
            return new Dimensions
            {
                [StandardDimensions.DEPENDANCY_TYPE] = DependancyType.Database.ToString(),
                [StandardDimensions.DEPENDANCY] = _databaseClient.GetExternalSystemName(),
                [CONNECTOR_ID_DIMENSION] = connectorId
            };
        }

    }
}
