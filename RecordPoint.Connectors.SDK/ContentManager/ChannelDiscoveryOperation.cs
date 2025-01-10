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
    /// The channel discovery operation.
    /// </summary>
    public class ChannelDiscoveryOperation : ManagedQueueableWorkBase<ChannelDiscoveryConfiguration, ChannelDiscoveryState>
    {
        /// <summary>
        /// WORK TYPE.
        /// </summary>
        public const string WORK_TYPE = "Channel Discovery";

        /// <summary>
        /// The content manager action provider.
        /// </summary>
        private readonly IContentManagerActionProvider _contentManagerActionProvider;
        /// <summary>
        /// Work queue client.
        /// </summary>
        private readonly IWorkQueueClient _workQueueClient;
        /// <summary>
        /// The managed work factory.
        /// </summary>
        private readonly IManagedWorkFactory _managedWorkFactory;
        /// <summary>
        /// The managed work status manager.
        /// </summary>
        private readonly IManagedWorkStatusManager _managedWorkStatusManager;
        /// <summary>
        /// The connector manager.
        /// </summary>
        private readonly IConnectorConfigurationManager _connectorManager;
        /// <summary>
        /// The channel manager.
        /// </summary>
        private readonly IChannelManager _channelManager;
        /// <summary>
        /// The options.
        /// </summary>
        private readonly IOptions<ChannelDiscoveryOperationOptions> _options;
        /// <summary>
        /// Content Manager Options
        /// </summary>
        private readonly IOptions<ContentManagerOptions> _contentManagerOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelDiscoveryOperation"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="contentManagerActionProvider">The content manager action provider.</param>
        /// <param name="connectorManager">The connector manager.</param>
        /// <param name="channelManager">The channel manager.</param>
        /// <param name="workQueueClient">The work queue client.</param>
        /// <param name="managedWorkFactory">The managed work factory.</param>
        /// <param name="managedWorkStatusManager">The managed work status manager.</param>
        /// <param name="systemContext">The system context.</param>
        /// <param name="scopeManager">The scope manager.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="telemetryTracker">The telemetry tracker.</param>
        /// <param name="dateTimeProvider">The date time provider.</param>
        /// <param name="options">The options.</param>
        /// <param name="contentManagerOptions">The content manager options.</param>
        public ChannelDiscoveryOperation(
            IServiceProvider serviceProvider,
            IContentManagerActionProvider contentManagerActionProvider,
            IConnectorConfigurationManager connectorManager,
            IChannelManager channelManager,
            IWorkQueueClient workQueueClient,
            IManagedWorkFactory managedWorkFactory,
            IManagedWorkStatusManager managedWorkStatusManager,
            ISystemContext systemContext,
            IScopeManager scopeManager,
            ILogger<ChannelDiscoveryOperation> logger,
            ITelemetryTracker telemetryTracker,
            IDateTimeProvider dateTimeProvider,
            IOptions<ChannelDiscoveryOperationOptions> options,
            IOptions<ContentManagerOptions> contentManagerOptions)
            : base(serviceProvider, managedWorkFactory, systemContext, scopeManager, logger, telemetryTracker, dateTimeProvider)
        {
            _contentManagerActionProvider = contentManagerActionProvider;
            _workQueueClient = workQueueClient;
            _managedWorkFactory = managedWorkFactory;
            _connectorManager = connectorManager;
            _channelManager = channelManager;
            _managedWorkStatusManager = managedWorkStatusManager;
            _options = options;
            _contentManagerOptions = contentManagerOptions;
        }

        /// <summary>
        /// Gets the service name.
        /// </summary>
        public override string ServiceName => ContentManagerObservabilityExtensions.SERVICE_NAME;

        /// <summary>
        /// Gets the work type.
        /// </summary>
        public override string WorkType => WORK_TYPE;

        /// <summary>
        /// The connector configuration.
        /// </summary>
        private ConnectorConfigModel _connectorConfiguration;

        /// <summary>
        /// Inner the run asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="RequiredValueOutOfRangeException"></exception>
        /// <returns>A Task</returns>
        protected override async Task InnerRunAsync(CancellationToken cancellationToken)
        {
            _connectorConfiguration = await _connectorManager.GetConnectorAsync(Configuration.ConnectorConfigurationId, cancellationToken);

            if (!(await CheckConnectorEnabledStatusAsync(_connectorConfiguration, _options.Value, State, _contentManagerOptions.Value.MaxDisabledConnectorAge, cancellationToken)))
                return;

            if (await CheckSemaphoreLockAsync(_connectorConfiguration, null, cancellationToken))
                return;

            var startTime = DateTimeProvider.UtcNow;

            // If everything is good go and fetch new channels
            _channelDiscoveryResult = await FetchAsync(cancellationToken);
            _actionExecutionTimespan = DateTimeProvider.UtcNow - startTime;
            switch (_channelDiscoveryResult.ResultType)
            {
                case ChannelDiscoveryResultType.Complete:
                    await HandleCompleteResultAsync(_channelDiscoveryResult, cancellationToken);
                    break;

                case ChannelDiscoveryResultType.Failed:
                    await HandleFailedResultAsync(_channelDiscoveryResult, cancellationToken);
                    break;

                case ChannelDiscoveryResultType.BackOff:
                    await HandleBackOffResultAsync(_connectorConfiguration, null, _channelDiscoveryResult.SemaphoreLockType, _channelDiscoveryResult.NextDelay, cancellationToken);
                    break;

                default:
                    throw new RequiredValueOutOfRangeException(nameof(_channelDiscoveryResult.ResultType));
            }
        }

        #region Fetching
        /// <summary>
        /// Create a Channel Discovery Operation for the current connector configuration
        /// </summary>
        /// <returns>Channel scanner</returns>
        protected IChannelDiscoveryAction CreateChannelDiscoveryAction()
        {
            try
            {
                return _contentManagerActionProvider.CreateChannelDiscoveryAction();
            }
            catch (NotImplementedException)
            {
                // Fall back to null Channel scanner
                return new NullChannelDiscoveryAction(_channelManager);
            }
        }

        /// <summary>
        /// Assuming that everything is setup correct, go and fetch new Channels
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Channel outcome</returns>
        protected async Task<ChannelDiscoveryResult> FetchAsync(CancellationToken cancellationToken)
        {
            var startTime = DateTimeProvider.UtcNow;

            ChannelDiscoveryResult result;
            try
            {
                var channelScanner = CreateChannelDiscoveryAction();
                result = await channelScanner.ExecuteAsync(_connectorConfiguration, cancellationToken);
            }
            catch (Exception ex)
            {
                result = new ChannelDiscoveryResult
                {
                    ResultType = ChannelDiscoveryResultType.Failed,
                    Reason = ex.Message,
                    Exception = ex
                };
            }

            _actionExecutionTimespan = DateTimeProvider.UtcNow - startTime;
            return result;
        }
        #endregion

        #region Handle Result
        /// <summary>
        /// Handle complete result asynchronously.
        /// </summary>
        /// <param name="channelResult">The channel result.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task</returns>
        protected async Task HandleCompleteResultAsync(ChannelDiscoveryResult channelResult, CancellationToken cancellationToken)
        {
            //Add the new Channels to Storage
            var channelModels = channelResult.Channels.ToChannelModelList()
                .UnionBy(channelResult.NewChannelRegistrations.ToChannelModelList(), a => a.ExternalId)
                .ToList();
            channelModels.ForEach(channelModel => channelModel.ConnectorId = _connectorConfiguration.Id);
            await _channelManager.UpsertChannelsAsync(channelModels, cancellationToken);

            // Determine how long we should wait until we run again
            var backOffSeconds = channelResult.NextDelay ?? CalculateBackOffSeconds(
                    _options.Value,
                    channelResult.Channels.Any(),
                    State.LastBackOffDelaySeconds);

            var startTime = DateTimeProvider.UtcNow;
            var nextRunTime = DateTimeProvider.UtcNow.AddSeconds(backOffSeconds);

            var tasks = new List<Task>();

            //Create new Content Synchronisation Operations for Channels that don't currently have Work assigned
            //The running Channel Discovery Operation should handle when Channels are removed
            var newContentSynchronisationChannels = await GetMissingChannelsForWorkTypeAsync(
                channelResult.Channels,
                ContentSynchronisationOperation.WORK_TYPE,
                a => a.DeserialiseContentSynchronisationConfiguration().ChannelExternalId,
                cancellationToken);

            foreach (var channel in newContentSynchronisationChannels)
            {
                var contentSynchronisationOperation = _managedWorkFactory.CreateContentSynchronisationOperation(_connectorConfiguration, channel);
                tasks.Add(contentSynchronisationOperation.StartAsync(cancellationToken));
                _contentSynchronisationOperationsStarted++;
            }

            //Create new Content Registration Operations for Channels that don't currently have Work assigned
            var newContentRegistrationChannels = await GetMissingChannelsForWorkTypeAsync(
                channelResult.NewChannelRegistrations,
                ContentRegistrationOperation.WORK_TYPE,
                a => a.DeserialiseContentRegistrationConfiguration().ChannelExternalId,
                cancellationToken);

            foreach (var channel in newContentRegistrationChannels)
            {
                var contentRegistrationOperation = _managedWorkFactory.CreateContentRegistrationOperation(_connectorConfiguration, channel);
                tasks.Add(contentRegistrationOperation.StartAsync(cancellationToken));
                _contentRegistrationOperationsStarted++;
            }

            //Submit audit events
            var auditEvents = channelResult.AuditEvents;
            foreach (var auditEvent in auditEvents)
            {
                cancellationToken.ThrowIfCancellationRequested();
                tasks.Add(_workQueueClient.SubmitAuditEventAsync(new ContentSubmissionConfiguration
                {
                    ConnectorConfigurationId = _connectorConfiguration.Id,
                    TenantId = _connectorConfiguration.TenantId,
                    TenantDomainName = _connectorConfiguration.TenantDomainName
                }, auditEvent, null, cancellationToken));
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);

            // Ensure Work is queued for each Channel
            var finalState = new ChannelDiscoveryState
            {
                LastBackOffDelaySeconds = backOffSeconds,
            };
            _submitTimespan = DateTimeProvider.UtcNow - startTime;
            await ContinueAsync("Channel Discovery completed normally", finalState, nextRunTime, cancellationToken);
        }

        /// <summary>
        /// Handle failed result asynchronously.
        /// </summary>
        /// <param name="channelResult">The channel result.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task</returns>
        protected Task HandleFailedResultAsync(ChannelDiscoveryResult channelResult, CancellationToken cancellationToken)
        {
            // Should continue the failed Channel work in the next runs
            return FaultedAsync(channelResult.Reason, channelResult.Exception, cancellationToken);
        }

        /// Get a list of Channels for which operations are missing for the specified work type
        protected async Task<List<Channel>> GetMissingChannelsForWorkTypeAsync(List<Channel> channels, string workType, Func<ManagedWorkStatusModel, string> channelExternalIdFunction, CancellationToken cancellationToken)
        {
            var contentSynchronisationOperations =
                await _managedWorkStatusManager.GetWorkStatusesAsync(
                    a => a.WorkType == workType
                    && a.Status == ManagedWorkStatuses.Running
                    && a.ConnectorId == Configuration.ConnectorConfigurationId,
                    cancellationToken);

            var runningIds =
                contentSynchronisationOperations
                .Select(a => channelExternalIdFunction.Invoke(a))
                .ToHashSet();

            var requiredIds = channels.Select(c => c.ExternalId).ToHashSet();
            requiredIds.ExceptWith(runningIds);
            return channels.Where(c => requiredIds.Contains(c.ExternalId)).ToList();
        }
        #endregion

        #region State Serialization
        /// <summary>
        /// Deserialize the configuration.
        /// </summary>
        /// <param name="configurationType">The configuration type.</param>
        /// <param name="configurationText">The configuration text.</param>
        /// <returns>A ChannelDiscoveryConfiguration</returns>
        protected override ChannelDiscoveryConfiguration DeserializeConfiguration(string configurationType, string configurationText) => ChannelDiscoveryConfiguration.Deserialize(configurationType, configurationText);

        /// <summary>
        /// Deserialize the state.
        /// </summary>
        /// <param name="stateType">The state type.</param>
        /// <param name="stateText">The state text.</param>
        /// <returns>A ChannelDiscoveryState</returns>
        protected override ChannelDiscoveryState DeserializeState(string stateType, string stateText) => ChannelDiscoveryState.Deserialize(stateType, stateText);

        /// <summary>
        /// Serialize the state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>A (string, string)</returns>
        protected override (string, string) SerializeState(ChannelDiscoveryState state) => (ChannelDiscoveryState.LatestStateType, state.Serialize());
        #endregion

        #region Observability
        /// <summary>
        /// Outcome of the Channel operation
        /// </summary>
        private ChannelDiscoveryResult _channelDiscoveryResult;

        /// <summary>
        /// Number of contents synchronisation operations started
        /// </summary>
        private int _contentSynchronisationOperationsStarted;

        /// <summary>
        /// Number of content registration operations started
        /// </summary>
        private int _contentRegistrationOperationsStarted;

        /// <summary>
        /// How long it took to fetch content
        /// </summary>
        private TimeSpan? _actionExecutionTimespan;

        /// <summary>
        /// How long it took to submit the work to the queue
        /// </summary>
        private TimeSpan? _submitTimespan;

        /// <summary>
        /// Override custom dimensions key for ConnectorId, TenantId and TenantDomainName
        /// </summary>
        /// <returns></returns>
        protected override Dimensions GetCustomKeyDimensions()
        {
            var dimensions = base.GetCustomKeyDimensions();

            dimensions[StandardDimensions.CONNECTOR_ID] = Configuration.ConnectorConfigurationId;
            dimensions[StandardDimensions.TENANT_ID] = Configuration.TenantId;
            dimensions[StandardDimensions.TENANT_DOMAIN_NAME] = Configuration.TenantDomainName;

            if (!string.IsNullOrEmpty(_channelDiscoveryResult?.Reason))
            {
                dimensions.Add(StandardDimensions.ACTION_RESULT_REASON, _channelDiscoveryResult.Reason);
            }
            if (_channelDiscoveryResult?.Exception != null)
            {
                dimensions[StandardDimensions.EXCEPTION] = _channelDiscoveryResult.Exception.ToString();
            }

            if (_channelDiscoveryResult?.Dimensions != null)
            {
                foreach (var dimension in _channelDiscoveryResult.Dimensions)
                {
                    dimensions[dimension.Key] = dimension.Value;
                }
            }

            return dimensions;
        }

        /// <summary>
        /// Get custom result dimensions.
        /// </summary>
        /// <returns>A Dimensions</returns>
        protected override Dimensions GetCustomResultDimensions()
        {
            var dimensions = base.GetCustomResultDimensions();
            dimensions[StandardDimensions.WORK_COMPLETE] = _channelDiscoveryResult?.ResultType.ToString() ?? "Unknown";
            return dimensions;
        }

        /// <summary>
        /// Get custom result measures.
        /// </summary>
        /// <returns>A Measures</returns>
        protected override Measures GetCustomResultMeasures()
        {
            var measures = base.GetCustomResultMeasures();
            measures[ContentManagerObservabilityExtensions.CHANNEL_COUNT] = _channelDiscoveryResult?.Channels.Count ?? 0;
            measures[ContentManagerObservabilityExtensions.CONTENT_SYNCHRONISATION_OPERATIONS_STARTED_COUNT] = _contentSynchronisationOperationsStarted;
            measures[ContentManagerObservabilityExtensions.CONTENT_REGISTRATION_OPERATIONS_STARTED_COUNT] = _contentRegistrationOperationsStarted;

            if (_actionExecutionTimespan.HasValue)
                measures[StandardMeasures.ACTION_EXECUTION_SECONDS] = _actionExecutionTimespan.Value.Milliseconds / 1000D;
            if (_submitTimespan.HasValue)
                measures[StandardMeasures.SUBMIT_SECONDS] = _submitTimespan.Value.Milliseconds / 1000D;

            if (_channelDiscoveryResult?.Measures != null)
            {
                foreach (var measure in _channelDiscoveryResult.Measures)
                {
                    measures[measure.Key] = measure.Value;
                }
            }

            return measures;
        }
        #endregion
    }

}
