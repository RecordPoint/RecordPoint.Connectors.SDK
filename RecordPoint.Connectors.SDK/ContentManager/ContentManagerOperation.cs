using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Implementation of unmanaged work that manages Channel & Content Discovery operations
    /// </summary>
    public class ContentManagerOperation : ManagedQueueableWorkBase<ContentManagerConfiguration, ContentManagerState>
    {
        public const string WORK_TYPE = "Content Manager";
        public const string CONTENT_SOURCE_INTEGRATION_COMPLETED = "Content Manager Completed";

        private readonly IConnectorConfigurationManager _connectorConfigurationManager;
        private readonly IChannelManager _channelManager;
        private readonly IManagedWorkFactory _managedWorkFactory;
        private readonly IManagedWorkStatusManager _managedWorkStatusManager;
        private readonly IOptions<ContentManagerOptions> _options;

        private List<ManagedWorkStatusModel> _managedWorkStatuses;
        private List<ConnectorConfigModel> _connectorConfigurations;
        private List<ChannelModel> _channels;

        public override string ServiceName => ContentManagerObservabilityExtensions.SERVICE_NAME;

        public override string WorkType => WORK_TYPE;

        public ContentManagerOperation(
            IServiceProvider serviceProvider,
            IConnectorConfigurationManager connectorConfigManager,
            IChannelManager channelManager,
            IManagedWorkStatusManager managedWorkStatusManager,
            IManagedWorkFactory managedWorkFactory,
            ISystemContext systemContext,
            IOptions<ContentManagerOptions> options,
            IScopeManager scopeManager,
            ILogger<ContentManagerOperation> logger,
            ITelemetryTracker telemetryTracker,
            IDateTimeProvider dateTimeProvider)
            : base(serviceProvider, managedWorkFactory, systemContext, scopeManager, logger, telemetryTracker, dateTimeProvider)
        {
            _connectorConfigurationManager = connectorConfigManager;
            _channelManager = channelManager;
            _managedWorkStatusManager = managedWorkStatusManager;
            _managedWorkFactory = managedWorkFactory;
            _options = options;
        }

        protected async override Task InnerRunAsync(CancellationToken cancellationToken)
        {
            _connectorConfigurations = await _connectorConfigurationManager.ListConnectorsAsync(cancellationToken);
            _managedWorkStatuses = await _managedWorkStatusManager.GetAllWorkStatusesAsync(cancellationToken);
            _channels = await _channelManager.GetChannelsAsync(cancellationToken);

            await CreateChannelDiscoveryOperationsAsync(cancellationToken);
            await CreateContentSynchronisationOperationsAsync(cancellationToken);

            if (_options.Value.RemoveCompletedWork)
            {
                await CleanupWorkAsync(ManagedWorkStatuses.Complete, _options.Value.MaxCompletedWorkAge, cancellationToken);
            }

            if (_options.Value.RemoveAbandonedWork)
            {
                await CleanupWorkAsync(ManagedWorkStatuses.Abandoned, _options.Value.MaxAbandonedWorkAge, cancellationToken);
            }

            if (_options.Value.CleanUpChannels)
            {
                await CleanupChannelsAsync(cancellationToken);
            }

            var finalState = new ContentManagerState
            {
                ChannelDiscoveryOperationsStarted = State.ChannelDiscoveryOperationsStarted,
                ContentSynchronisationOperationsStarted = State.ContentSynchronisationOperationsStarted
            };
            var nextRunTime = DateTimeProvider.UtcNow.AddSeconds(_options.Value.DelaySeconds);
            await ContinueAsync("Channel Discovery completed normally", finalState, nextRunTime, cancellationToken);
        }

        #region Channel Discovery
        private async Task CreateChannelDiscoveryOperationsAsync(CancellationToken cancellationToken)
        {
            var newConnectorConfigurations = GetNewConnectorConfigurations();
            foreach (var connectorConfiguration in newConnectorConfigurations)
            {
                var channelDiscoveryOperation = _managedWorkFactory.CreateChannelDiscoveryOperation(connectorConfiguration);
                await channelDiscoveryOperation.StartAsync(cancellationToken);
                State.ChannelDiscoveryOperationsStarted++;
            }
        }

        private List<ConnectorConfigModel> GetNewConnectorConfigurations()
        {
            var channelDiscoveryWorkStatuses = _managedWorkStatuses
                .Where(a =>
                    a.WorkType == ChannelDiscoveryOperation.WORK_TYPE
                    && a.Status == ManagedWorkStatuses.Running
                );

            var runningIds = channelDiscoveryWorkStatuses
                .Select(a => a.ConnectorId);

            var requiredIds = _connectorConfigurations.Select(c => c.Id).ToHashSet();
            requiredIds.ExceptWith(runningIds);

            return _connectorConfigurations.Where(c => requiredIds.Contains(c.Id)).ToList();
        }
        #endregion

        #region Content Synchronisation
        private async Task CreateContentSynchronisationOperationsAsync(CancellationToken cancellationToken)
        {
            var newWorkChannels = GetMissingContentSynchronisationOperations();
            foreach (var channel in newWorkChannels)
            {
                var connectorConfiguration = _connectorConfigurations.FirstOrDefault(a => a.Id == channel.ConnectorId);
                if (connectorConfiguration != null)
                {
                    var contentSynchronisationOperation = _managedWorkFactory.CreateContentSynchronisationOperation(connectorConfiguration, channel.ToChannel());
                    await contentSynchronisationOperation.StartAsync(cancellationToken);
                    State.ContentSynchronisationOperationsStarted++;
                }
            }
        }

        private IEnumerable<ChannelModel> GetMissingContentSynchronisationOperations()
        {
            //Get Channels that were created at least 5 minutes prior to ensure the
            //Channel Discovery Operation has had a chance to initiate a Content Synchronisation Operation
            var channelsCreatedDate = DateTimeOffset.Now.AddMinutes(-5);
            var channels = _channels.Where(a => channelsCreatedDate >= a.CreatedDate);

            var contentSynchronisationOperations = _managedWorkStatuses
                .Where(a => a.WorkType == ContentSynchronisationOperation.WORK_TYPE);

            var runningIds =
                contentSynchronisationOperations
                .Select(a => a.DeserialiseContentSynchronisationConfiguration().ChannelExternalId)
                .ToHashSet();

            var requiredIds = channels.Select(c => c.ExternalId).ToHashSet();
            requiredIds.ExceptWith(runningIds);
            var result = channels.Where(c => requiredIds.Contains(c.ExternalId));

            return result;
        }
        #endregion

        #region Cleanup Completed Work
        private async Task CleanupWorkAsync(ManagedWorkStatuses status, int maxWorkAge, CancellationToken cancellationToken)
        {
            //Get Completed Work with a LastStatusUpdate greater than the Max Age for Completed Work
            var cleanupDate = DateTimeOffset.Now.AddMinutes(maxWorkAge);
            var completedWorkIds = _managedWorkStatuses
                .Where(a => a.Status == status && cleanupDate >= a.LastStatusUpdate)
                .Select(a => a.Id)
                .ToArray();

            await _managedWorkStatusManager.RemoveWorkStatusesAsync(completedWorkIds, cancellationToken);
        }
        #endregion

        #region Cleanup Channels
        private async Task CleanupChannelsAsync(CancellationToken cancellationToken)
        {
            var allConnectorConfigurations = await _connectorConfigurationManager.GetAllConnectorConfigurationsAsync(cancellationToken);
            var allConnectorConfigurationIds = allConnectorConfigurations.Select(a => a.ConnectorId);
            var allChannels = await _channelManager.GetChannelsAsync(cancellationToken);

            var obsoleteChannels = allChannels.Where(a => !allConnectorConfigurationIds.Contains(a.ConnectorId));
            await _channelManager.RemoveChannelsAsync(obsoleteChannels, cancellationToken);
        }
        #endregion

        #region State Serialization
        protected override ContentManagerConfiguration DeserializeConfiguration(string configurationType, string configurationText) => ContentManagerConfiguration.Deserialize(configurationType, configurationText);

        protected override ContentManagerState DeserializeState(string stateType, string stateText) => ContentManagerState.Deserialize(stateType, stateText);

        protected override (string, string) SerializeState(ContentManagerState state) => (ContentManagerState.LatestStateType, state.Serialize());
        #endregion

        protected override Measures GetCustomResultMeasures()
        {
            var measures = base.GetCustomResultMeasures();
            measures[ContentManagerObservabilityExtensions.CONNECTOR_COUNT] = _connectorConfigurations?.Count ?? 0;
            measures[ContentManagerObservabilityExtensions.CHANNEL_DISCOVERY_OPERATIONS_STARTED_COUNT] = State.ChannelDiscoveryOperationsStarted;
            measures[ContentManagerObservabilityExtensions.CONTENT_SYNCHRONISATION_OPERATIONS_STARTED_COUNT] = State.ContentSynchronisationOperationsStarted;
            return measures;
        }

    }
}
