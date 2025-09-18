using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Defines a Channel discovery action, which is responsible for discovering Channels within the Content Source
    /// available for a content source
    /// </summary>
    public interface IChannelDiscoveryAction
    {
        /// <summary>
        /// Executes the channel discovery logic
        /// </summary>
        /// <param name="connectorConfiguration">The connector configuration</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="cursor">Scan cursor provided by a prior batch discovery operation, if Channel Discovery is in progress</param>
        /// <returns>Result of the scan</returns>
        Task<ChannelDiscoveryResult> ExecuteAsync(ConnectorConfigModel connectorConfiguration, CancellationToken cancellationToken, string? cursor = null);
    }
}
