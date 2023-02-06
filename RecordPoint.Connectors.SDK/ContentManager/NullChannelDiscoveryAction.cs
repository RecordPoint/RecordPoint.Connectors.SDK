using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Content;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Implementation of a Channel Discovery Action used when a content source doesn't support Channels
    /// </summary>
    public class NullChannelDiscoveryAction : IChannelDiscoveryAction
    {
        private readonly IChannelManager _channelManager;

        public NullChannelDiscoveryAction(IChannelManager channelManager)
        {
            _channelManager = channelManager;
        }

        public async Task<ChannelDiscoveryResult> ExecuteAsync(ConnectorConfigModel connectorConfiguration, CancellationToken cancellationToken)
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
