using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Content;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// The null channel discovery action.
    /// </summary>
    public class NullChannelDiscoveryAction : IChannelDiscoveryAction
    {
        /// <summary>
        /// The channel manager.
        /// </summary>
        private readonly IChannelManager _channelManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="NullChannelDiscoveryAction"/> class.
        /// </summary>
        /// <param name="channelManager">The channel manager.</param>
        public NullChannelDiscoveryAction(IChannelManager channelManager)
        {
            _channelManager = channelManager;
        }

        /// <summary>
        /// Execute and return a task of type channeldiscoveryresult asynchronously.
        /// </summary>
        /// <param name="connectorConfiguration">The connector configuration.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="cursor">Scan cursor provided by a prior batch discovery operation (though not applicable here).</param>
        /// <returns><![CDATA[Task<ChannelDiscoveryResult>]]></returns>
        public async Task<ChannelDiscoveryResult> ExecuteAsync(ConnectorConfigModel connectorConfiguration, CancellationToken cancellationToken, string cursor = null)
        {
            var result = new ChannelDiscoveryResult()
            {
                ResultType = ChannelDiscoveryResultType.Complete,
                Reason = "Fallback to null Channel"
            };

            //If the null channel hasn't been registered yet, create a new null channel
            var exists = await _channelManager.ChannelExistsAsync(connectorConfiguration.Id, Channel.NULL_CHANNEL_ID, cancellationToken);
            if (!exists)
            {
                result.Channels = new List<Channel> { Channel.CreateNullChannel() };
            }

            return result;
        }
    }
}
