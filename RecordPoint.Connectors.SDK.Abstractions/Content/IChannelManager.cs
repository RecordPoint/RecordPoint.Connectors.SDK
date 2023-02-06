using System.Linq.Expressions;

namespace RecordPoint.Connectors.SDK.Content
{
    /// <summary>
    /// Definition for a class that manages Channels
    /// </summary>
    public interface IChannelManager
    {
        /// <summary>
        /// Checks if the specified Channel exists
        /// </summary>
        /// <param name="connectorId">The Connector Configuration for which the Channel belongs</param>
        /// <param name="externalId">External Id of the Channel</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if it exists</returns>
        Task<bool> ChannelExistsAsync(string connectorId, string externalId, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the specified Channel
        /// </summary>
        /// <param name="connectorId">The Connector Configuration for which the Channel belongs</param>
        /// <param name="externalId">External Id of the Channel</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Channel if it exists, otherwise null</returns>
        Task<ChannelModel?> GetChannelAsync(string connectorId, string externalId, CancellationToken cancellationToken);

        /// <summary>
        /// Gets all known Channels
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of Channels across all connectors</returns>
        Task<List<ChannelModel>> GetChannelsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets all known Channels
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of Channels across all connectors</returns>
        Task<List<ChannelModel>> GetChannelsAsync(Expression<Func<ChannelModel, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Gets all Channels for the specified Connector
        /// </summary>
        /// <param name="connectorId">The Connector Configuration for which the Channels belong</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of Channels for the specified Connector</returns>
        Task<List<ChannelModel>> GetChannelsAsync(string? connectorId, CancellationToken cancellationToken);

        /// <summary>
        /// Adds a Channel to Storage or updates the existing Channel
        /// </summary>
        /// <param name="channel">Channel to add</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task</returns>
        Task UpsertChannelAsync(ChannelModel channel, CancellationToken cancellationToken);

        /// <summary>
        /// Adds the List of Channels to Storage or Updates each existing Channel
        /// </summary>
        /// <param name="channels">The Channels to add</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        Task UpsertChannelsAsync(List<ChannelModel> channels, CancellationToken cancellationToken);

        /// <summary>
        /// Removes a Channel from Storage
        /// </summary>
        /// <param name="connectorId">The Connector for which the Channel belongs</param>
        /// <param name="externalId">The Id of the Channel</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task</returns>
        Task RemoveChannelAsync(string connectorId, string externalId, CancellationToken cancellationToken);

        /// <summary>
        /// Removes the specified Channels from Storage
        /// </summary>
        /// <param name="connectorId">The Connector for which the Channel belongs</param>
        /// <param name="externalId">The Ids of the Channels</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task</returns>
        Task RemoveChannelsAsync(string connectorId, string[] externalIds, CancellationToken cancellationToken);

        /// <summary>
        /// Removes the specified Channels from Storage
        /// </summary>
        /// <param name="channelModels">A list of Channel Models to remove</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task</returns>
        Task RemoveChannelsAsync(IEnumerable<ChannelModel> channelModels, CancellationToken cancellationToken);
    }
}
