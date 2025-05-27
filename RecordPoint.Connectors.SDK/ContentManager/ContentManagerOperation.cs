using Microsoft.Extensions.DependencyInjection;
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
    /// The content manager operation.
    /// </summary>
    public class ContentManagerOperation : ManagedQueueableWorkBase<ContentManagerConfiguration, ContentManagerState>
    {
        /// <summary>
        /// WORK TYPE.
        /// </summary>
        public const string WORK_TYPE = "Content Manager";
        /// <summary>
        /// The CONTENT SOURCE INTEGRATION COMPLETED.
        /// </summary>
        public const string CONTENT_SOURCE_INTEGRATION_COMPLETED = "Content Manager Completed";

        /// <summary>
        /// The content manager action provider.
        /// </summary>
        private readonly IContentManagerActionProvider _contentManagerActionProvider;
        /// <summary>
        /// The connector configuration manager.
        /// </summary>
        private readonly IConnectorConfigurationManager _connectorConfigurationManager;
        /// <summary>
        /// The channel manager.
        /// </summary>
        private readonly IChannelManager _channelManager;
        /// <summary>
        /// The managed work factory.
        /// </summary>
        private readonly IManagedWorkFactory _managedWorkFactory;
        /// <summary>
        /// The managed work status manager.
        /// </summary>
        private readonly IManagedWorkStatusManager _managedWorkStatusManager;
        /// <summary>
        /// The options.
        /// </summary>
        private readonly IOptions<ContentManagerOptions> _options;

        /// <summary>
        /// The connector configurations.
        /// </summary>
        private List<ConnectorConfigModel> _connectorConfigurations;

        /// <summary>
        /// Gets the service name.
        /// </summary>
        public override string ServiceName => ContentManagerObservabilityExtensions.SERVICE_NAME;

        /// <summary>
        /// Gets the work type.
        /// </summary>
        public override string WorkType => WORK_TYPE;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentManagerOperation"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="contentManagerActionProvider">The content manager action provider.</param>
        /// <param name="connectorConfigManager">The connector config manager.</param>
        /// <param name="channelManager">The channel manager.</param>
        /// <param name="managedWorkStatusManager">The managed work status manager.</param>
        /// <param name="managedWorkFactory">The managed work factory.</param>
        /// <param name="systemContext">The system context.</param>
        /// <param name="options">The options.</param>
        /// <param name="observabilityScope">The scope manager.</param>
        /// <param name="telemetryTracker">The telemetry tracker.</param>
        /// <param name="dateTimeProvider">The date time provider.</param>
        public ContentManagerOperation(
            IServiceProvider serviceProvider,
            IContentManagerActionProvider contentManagerActionProvider,
            IConnectorConfigurationManager connectorConfigManager,
            IChannelManager channelManager,
            IManagedWorkStatusManager managedWorkStatusManager,
            IManagedWorkFactory managedWorkFactory,
            ISystemContext systemContext,
            IOptions<ContentManagerOptions> options,
            IObservabilityScope observabilityScope,
            ITelemetryTracker telemetryTracker,
            IDateTimeProvider dateTimeProvider)
            : base(serviceProvider, managedWorkFactory, systemContext, observabilityScope, telemetryTracker, dateTimeProvider)
        {
            _contentManagerActionProvider = contentManagerActionProvider;
            _connectorConfigurationManager = connectorConfigManager;
            _channelManager = channelManager;
            _managedWorkStatusManager = managedWorkStatusManager;
            _managedWorkFactory = managedWorkFactory;
            _options = options;
        }

        /// <summary>
        /// Inner the run asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task</returns>
        protected async override Task InnerRunAsync(CancellationToken cancellationToken)
        {
            _connectorConfigurations = await _connectorConfigurationManager.ListConnectorsAsync(cancellationToken);

            await CreateChannelDiscoveryOperationsAsync(cancellationToken);

            if (_options.Value.RemoveCompletedWork)
            {
                await CleanupWorkAsync(ManagedWorkStatuses.Complete, _options.Value.MaxCompletedWorkAge, cancellationToken);
            }

            if (_options.Value.RemoveAbandonedWork)
            {
                await CleanupWorkAsync(ManagedWorkStatuses.Abandoned, _options.Value.MaxAbandonedWorkAge, cancellationToken);
            }

            if (_options.Value.CleanUpAggregations)
            {
                await CleanupAggregationsAsync(cancellationToken);
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
        /// <summary>
        /// Creates channel discovery operations asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task</returns>
        private async Task CreateChannelDiscoveryOperationsAsync(CancellationToken cancellationToken)
        {
            var newConnectorConfigurations = await GetNewEnabledConnectorConfigurationsAsync(cancellationToken);
            foreach (var connectorConfiguration in newConnectorConfigurations)
            {
                using var channelDiscoveryOperation = _managedWorkFactory.CreateChannelDiscoveryOperation(connectorConfiguration);
                await channelDiscoveryOperation.StartAsync(cancellationToken);
                State.ChannelDiscoveryOperationsStarted++;
            }

            await InvokeContentManagerCallbackAsync(newConnectorConfigurations, cancellationToken);
        }

        /// <summary>
        /// Determines Connector Configurations that do not have Channel Discovery work running.
        /// </summary>
        /// <returns><![CDATA[List<ConnectorConfigModel>]]></returns>
        private async Task<List<ConnectorConfigModel>> GetNewEnabledConnectorConfigurationsAsync(CancellationToken cancellationToken)
        {
            var channelDiscoveryWorkStatuses = await _managedWorkStatusManager
                .GetWorkStatusesAsync(a => 
                    a.WorkType == ChannelDiscoveryOperation.WORK_TYPE && a.Status == ManagedWorkStatuses.Running,
                    cancellationToken);

            var runningIds = channelDiscoveryWorkStatuses
                .Select(a => a.ConnectorId);

            var requiredIds = _connectorConfigurations.Select(c => c.Id).ToHashSet();
            requiredIds.ExceptWith(runningIds);

            return _connectorConfigurations.Where(c => requiredIds.Contains(c.Id) && c.IsEnabled()).ToList();
        }
        #endregion

        #region Cleanup Completed Work
        /// <summary>
        /// Cleanup the work asynchronously.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="maxWorkAge">The max work age.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task</returns>
        private async Task CleanupWorkAsync(ManagedWorkStatuses status, int maxWorkAge, CancellationToken cancellationToken)
        {
            //Get Work with the specified status and a LastStatusUpdate greater than the Max Age
            var cleanupDate = DateTimeOffset.Now.AddMinutes(maxWorkAge);
            var managedWorkItems = await _managedWorkStatusManager
                .GetWorkStatusesAsync(a => a.Status == status && cleanupDate >= a.LastStatusUpdate, cancellationToken);

            var managedWorkItemIds = managedWorkItems.Select(a => a.Id).ToArray();
            await _managedWorkStatusManager.RemoveWorkStatusesAsync(managedWorkItemIds, cancellationToken);
        }
        #endregion

        #region Cleanup Aggregations
        /// <summary>
        /// Removes dangling aggregations asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task</returns>
        private async Task CleanupAggregationsAsync(CancellationToken cancellationToken)
        {
            //Some connectors may not have registered the Aggregation Manager as they use a custom implementation
            var aggregationManager = _serviceProvider.GetService<IAggregationManager>();
            if (aggregationManager == null)
                return;

            //Get all Enabled Configurations and Configurations that have been Disabled for less than the Max Disabled Age
            var validConnectorConfigurations = _connectorConfigurations.Where(configuration => !configuration.IsDisabledConnectorExpired(_options.Value.MaxDisabledConnectorAge));

            var validConnectorConfigurationIds = validConnectorConfigurations.Select(a => a.Id);
            var obsoleteAggregations = await aggregationManager.GetAggregationsAsync(a => !validConnectorConfigurationIds.Contains(a.ConnectorId), cancellationToken);
            
            await aggregationManager.RemoveAggregationsAsync(obsoleteAggregations, cancellationToken);
        }
        #endregion

        #region Cleanup Channels
        /// <summary>
        /// Removes dangling channels asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task</returns>
        private async Task CleanupChannelsAsync(CancellationToken cancellationToken)
        {
            //Get all Enabled Configurations and Configurations that have been Disabled for less than the Max Disabled Age
            var validConnectorConfigurations = _connectorConfigurations.Where(configuration => !configuration.IsDisabledConnectorExpired(_options.Value.MaxDisabledConnectorAge));

            var validConnectorConfigurationIds = validConnectorConfigurations.Select(a => a.Id);
            var obsoleteChannels = await _channelManager.GetChannelsAsync(a => !validConnectorConfigurationIds.Contains(a.ConnectorId), cancellationToken);

            await _channelManager.RemoveChannelsAsync(obsoleteChannels, cancellationToken);
        }
        #endregion

        #region State Serialization
        /// <summary>
        /// Deserialize the configuration.
        /// </summary>
        /// <param name="configurationType">The configuration type.</param>
        /// <param name="configurationText">The configuration text.</param>
        /// <returns>A ContentManagerConfiguration</returns>
        protected override ContentManagerConfiguration DeserializeConfiguration(string configurationType, string configurationText) => ContentManagerConfiguration.Deserialize(configurationType, configurationText);

        /// <summary>
        /// Deserialize the state.
        /// </summary>
        /// <param name="stateType">The state type.</param>
        /// <param name="stateText">The state text.</param>
        /// <returns>A ContentManagerState</returns>
        protected override ContentManagerState DeserializeState(string stateType, string stateText) => ContentManagerState.Deserialize(stateType, stateText);

        /// <summary>
        /// Serialize the state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>A (string, string)</returns>
        protected override (string, string) SerializeState(ContentManagerState state) => (ContentManagerState.LatestStateType, state.Serialize());
        #endregion

        /// <summary>
        /// Get custom result measures.
        /// </summary>
        /// <returns>A Measures</returns>
        protected override Measures GetCustomResultMeasures()
        {
            var measures = base.GetCustomResultMeasures();
            measures[ContentManagerObservabilityExtensions.CONNECTOR_COUNT] = _connectorConfigurations?.Count ?? 0;
            measures[ContentManagerObservabilityExtensions.CHANNEL_DISCOVERY_OPERATIONS_STARTED_COUNT] = State.ChannelDiscoveryOperationsStarted;
            measures[ContentManagerObservabilityExtensions.CONTENT_SYNCHRONISATION_OPERATIONS_STARTED_COUNT] = State.ContentSynchronisationOperationsStarted;
            return measures;
        }

        private IContentManagerCallbackAction CreateContentManagerCallbackAction() => _contentManagerActionProvider.CreateContentManagerCallbackAction();

        private async Task InvokeContentManagerCallbackAsync(List<ConnectorConfigModel> connectorConfigurations, CancellationToken cancellationToken)
        {
            //Do not invoke if we have not found any new configurations
            if (connectorConfigurations.Count == 0)
                return;

            var contentManagerCallbackAction = CreateContentManagerCallbackAction();

            //If no callback action has been registered, just bail out now
            if (contentManagerCallbackAction == null)
                return;

            await contentManagerCallbackAction
                .ExecuteAsync(connectorConfigurations, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
