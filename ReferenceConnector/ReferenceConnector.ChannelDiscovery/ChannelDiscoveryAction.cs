using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;
using ReferenceConnector.Common;

namespace ReferenceConnector.ChannelDiscovery
{
    internal class ChannelDiscoveryAction : IChannelDiscoveryAction
    {
        private readonly ReferenceConnectorOptions _options;
        private readonly IRandomHelper _randomHelper;
        private readonly IChannelManager _channelManager;
        private readonly ILogger<ChannelDiscoveryAction> _logger;

        public ChannelDiscoveryAction(
            IOptions<ReferenceConnectorOptions> options,
            IRandomHelper randomHelper,
            IChannelManager channelManager,
            ILogger<ChannelDiscoveryAction> logger)
        {
            _options = options.Value;
            _randomHelper = randomHelper;
            _channelManager = channelManager;
            _logger = logger;
        }

        public async Task<ChannelDiscoveryResult> ExecuteAsync(ConnectorConfigModel connectorConfiguration, CancellationToken cancellationToken)
        {
            var outcome = new ChannelDiscoveryResult
            {
                ResultType = ChannelDiscoveryResultType.Complete,
                Reason = "Completed"
            };

            var knownChannels = await _channelManager.GetChannelsAsync(connectorConfiguration.Id, cancellationToken);
            if (!knownChannels.Any())
            {
                outcome.Channels = new() { CreateChannel() };
            }
            else if (_randomHelper.CalculateLikelihood(_options.NewChannelLikelihood))
            {
                outcome.Channels = new() { CreateChannel() };
            }

            return outcome;
        }

        private Channel CreateChannel()
        {
            const int rndWordCount = 3;

            _logger.LogInformation("Creating random channel");

            return new Channel
            {
                ExternalId = _randomHelper.GenerateRandomWord(),
                Title = _randomHelper.GenerateRandomWords(rndWordCount)
            };
        }
    }
}
