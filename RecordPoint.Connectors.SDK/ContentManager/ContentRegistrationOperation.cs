﻿using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Toggles;
using RecordPoint.Connectors.SDK.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// The content registration operation.
    /// </summary>
    public class ContentRegistrationOperation : ManagedQueueableWorkBase<ContentRegistrationConfiguration, ContentRegistrationState>
    {
        /// <summary>
        /// WORK TYPE.
        /// </summary>
        public const string WORK_TYPE = "Content Registration";

        /// <summary>
        /// The content manager action provider.
        /// </summary>
        private readonly IContentManagerActionProvider _contentManagerActionProvider;
        /// <summary>
        /// The connector manager.
        /// </summary>
        private readonly IConnectorConfigurationManager _connectorManager;
        /// <summary>
        /// The channel manager.
        /// </summary>
        private readonly IChannelManager _channelManager;
        /// <summary>
        /// Work queue client.
        /// </summary>
        private readonly IWorkQueueClient _workQueueClient;
        /// <summary>
        /// Feature Toggle Provider.
        /// </summary>
        private readonly IToggleProvider _toggleProvider;
        /// <summary>
        /// The options.
        /// </summary>
        private readonly IOptions<ContentRegistrationOperationOptions> _options;
        /// <summary>
        /// The content manager options.
        /// </summary>
        private readonly IOptions<ContentManagerOptions> _contentManagerOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentRegistrationOperation"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="contentManagerActionProvider">The content manager action provider.</param>
        /// <param name="connectorManager">The connector manager.</param>
        /// <param name="channelManager">The channel manager.</param>
        /// <param name="workQueueClient">The work queue client.</param>
        /// <param name="managedWorkFactory">The managed work factory.</param>
        /// <param name="systemContext">The system context.</param>
        /// <param name="observabilityScope">The scope manager.</param>
        /// <param name="toggleProvider">The toggle provider.</param>
        /// <param name="telemetryTracker">The telemetry tracker.</param>
        /// <param name="dateTimeProvider">The date time provider.</param>
        /// <param name="options">The options.</param>
        /// <param name="contentManagerOptions">The content manager options.</param>
        public ContentRegistrationOperation(
            IServiceProvider serviceProvider,
            IContentManagerActionProvider contentManagerActionProvider,
            IConnectorConfigurationManager connectorManager,
            IChannelManager channelManager,
            IWorkQueueClient workQueueClient,
            IManagedWorkFactory managedWorkFactory,
            ISystemContext systemContext,
            IObservabilityScope observabilityScope,
            IToggleProvider toggleProvider,
            ITelemetryTracker telemetryTracker,
            IDateTimeProvider dateTimeProvider,
            IOptions<ContentRegistrationOperationOptions> options,
            IOptions<ContentManagerOptions> contentManagerOptions)
            : base(serviceProvider, managedWorkFactory, systemContext, observabilityScope, telemetryTracker, dateTimeProvider)
        {
            _contentManagerActionProvider = contentManagerActionProvider;
            _connectorManager = connectorManager;
            _channelManager = channelManager;
            _workQueueClient = workQueueClient;
            _toggleProvider = toggleProvider;
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

            var channelExists = await _channelManager.ChannelExistsAsync(Configuration.ConnectorConfigurationId, Configuration.ChannelExternalId, cancellationToken);
            if (!channelExists)
            {
                // If channel not found assume its been removed from the content source
                await AbandonedAsync("Channel not found", cancellationToken);
                return;
            }

            if (await CheckSemaphoreLockAsync(_connectorConfiguration, Configuration!.ChannelExternalId, cancellationToken))
            {
                return;
            }

            var startTime = DateTimeProvider.UtcNow;

            // If everything is good go and fetch new content
            var result = await FetchAsync(cancellationToken);
            _actionExecutionTimespan = DateTimeProvider.UtcNow - startTime;
            switch (result.ResultType)
            {
                case ContentResultType.Complete:
                    await HandleCompleteContentAsync(result, cancellationToken);
                    break;

                case ContentResultType.Incomplete:
                    await HandleIncompleteContentAsync(result, cancellationToken);
                    break;

                case ContentResultType.Failed:
                    await HandleFailedContentAsync(result, cancellationToken);
                    break;

                case ContentResultType.Abandonded:
                    await HandleAbandondedContentAsync(result, cancellationToken);
                    break;

                case ContentResultType.BackOff:
                    await HandleBackOffResultAsync(_connectorConfiguration, Configuration!.ChannelExternalId, result.SemaphoreLockType, result.NextDelay, cancellationToken);
                    break;

                default:
                    throw new RequiredValueOutOfRangeException(nameof(result.ResultType));
            }
        }

        #region Fetching
        /// <summary>
        /// Create a Content Registration Operation for the current connector configuration
        /// </summary>
        /// <returns>Content syncer</returns>
        protected IContentRegistrationAction CreateContentRegistrationAction() => _contentManagerActionProvider.CreateContentRegistrationAction();

        /// <summary>
        /// Assuming that everything is setup correct, go and fetch new Content
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Fetch content outcome</returns>
        protected async Task<ContentResult> FetchAsync(CancellationToken cancellationToken)
        {
            var startTime = DateTimeProvider.UtcNow;

            var channel = new Channel
            {
                ExternalId = Configuration.ChannelExternalId,
                Title = Configuration.ChannelTitle
            };
            var cursor = State.Cursor;
            var operation = string.IsNullOrEmpty(cursor)
                ? BeginAsync(channel, cancellationToken)
                : ContinueSync(channel, cursor, cancellationToken);

            ContentResult result;
            try
            {
                result = await operation;
            }
            catch (Exception ex)
            {
                result = new ContentResult
                {
                    ResultType = ContentResultType.Failed,
                    Reason = ex.Message,
                    Exception = ex
                };
            }

            _actionExecutionTimespan = DateTimeProvider.UtcNow - startTime;
            return result;
        }

        /// <summary>
        /// Begins and return a task of type contentresult asynchronously.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><![CDATA[Task<ContentResult>]]></returns>
        protected async Task<ContentResult> BeginAsync(Channel channel, CancellationToken cancellationToken)
        {
            var action = CreateContentRegistrationAction();
            return await action.BeginAsync(_connectorConfiguration, channel, Configuration.Context, cancellationToken);
        }

        /// <summary>
        /// Continue the sync.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="cursor">The cursor.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><![CDATA[Task<ContentResult>]]></returns>
        protected async Task<ContentResult> ContinueSync(Channel channel, string cursor, CancellationToken cancellationToken)
        {
            var action = CreateContentRegistrationAction();
            return await action.ContinueAsync(_connectorConfiguration, channel, cursor, Configuration.Context, cancellationToken);
        }
        #endregion

        #region Handle Content

        /// <summary>
        /// Handle complete content asynchronously.
        /// </summary>
        /// <param name="contentResult">The content result.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task</returns>
        protected async Task HandleCompleteContentAsync(ContentResult contentResult, CancellationToken cancellationToken)
        {
            // We've registered all available content so the operation can completed
            await SubmitContentAsync(contentResult, cancellationToken).ConfigureAwait(false);

            await CompleteAsync(contentResult.Reason, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Handle incomplete content asynchronously.
        /// </summary>
        /// <param name="contentResult">The content result.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task</returns>
        protected async Task HandleIncompleteContentAsync(ContentResult contentResult, CancellationToken cancellationToken)
        {
            // If we haven't gotten everything available go again straight away.
            await SubmitContentAsync(contentResult, cancellationToken).ConfigureAwait(false);

            var backOffSeconds = contentResult.NextDelay ?? CalculateBackOffSeconds(
                _options.Value,
                contentResult.Records.Any() || contentResult.Aggregations.Any(),
                State.LastBackOffDelaySeconds);

            var nextRunTime = DateTimeProvider.UtcNow.AddSeconds(backOffSeconds);

            var state = new ContentRegistrationState
            {
                LastBackOffDelaySeconds = backOffSeconds,
                Cursor = contentResult.Cursor
            };

            await ContinueAsync("Registration complete normally", state, nextRunTime, cancellationToken);
        }

        /// <summary>
        /// Handle failed content asynchronously.
        /// </summary>
        /// <param name="contentResult">The content result.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task</returns>
        protected Task HandleFailedContentAsync(ContentResult contentResult, CancellationToken cancellationToken)
        {
            // Should continue the failed content operation in the next runs
            _contentResult = contentResult;
            return FaultedAsync(contentResult.Reason, contentResult.Exception, cancellationToken);
        }

        /// <summary>
        /// Handle abandonded content asynchronously.
        /// </summary>
        /// <param name="contentResult">The content result.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task</returns>
        protected Task HandleAbandondedContentAsync(ContentResult contentResult, CancellationToken cancellationToken)
        {
            // Should complete the abandonded content operation as this is typically due to the Channel no longer being available
            _contentResult = contentResult;
            return AbandonedAsync(contentResult.Reason, cancellationToken);
        }

        /// <summary>
        /// Submits the content asynchronously.
        /// </summary>
        /// <param name="contentResult">The content result.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task</returns>
        protected async Task SubmitContentAsync(ContentResult contentResult, CancellationToken cancellationToken)
        {
            var startTime = DateTimeProvider.UtcNow;
            var tasks = new List<Task>();
            var contentProtectionEnabled = _toggleProvider.GetConnectorContentProtection(SystemContext, _connectorConfiguration.TenantId);

            var records = contentResult.Records;
            foreach (var record in records)
            {
                if (!contentProtectionEnabled)
                {
                    //If content protection is disabled, remove all binaries from the record 
                    record.Binaries = new List<BinaryMetaInfo>();
                }

                cancellationToken.ThrowIfCancellationRequested();
                tasks.Add(_workQueueClient.SubmitRecordAsync(new ContentSubmissionConfiguration
                {
                    ConnectorConfigurationId = _connectorConfiguration.Id,
                    TenantId = _connectorConfiguration.TenantId,
                    TenantDomainName = _connectorConfiguration.TenantDomainName
                }, record, null, cancellationToken));

                var binaries = record.Binaries;
                foreach (var binary in binaries)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    tasks.Add(_workQueueClient.SubmitBinaryAsync(new ContentSubmissionConfiguration
                    {
                        ConnectorConfigurationId = _connectorConfiguration.Id,
                        TenantId = _connectorConfiguration.TenantId,
                        TenantDomainName = _connectorConfiguration.TenantDomainName
                    }, binary, null, cancellationToken));
                }
            }

            var aggregations = contentResult.Aggregations;
            foreach (var aggregation in aggregations)
            {
                cancellationToken.ThrowIfCancellationRequested();
                tasks.Add(_workQueueClient.SubmitAggregationAsync(new ContentSubmissionConfiguration
                {
                    ConnectorConfigurationId = _connectorConfiguration.Id,
                    TenantId = _connectorConfiguration.TenantId,
                    TenantDomainName = _connectorConfiguration.TenantDomainName
                }, aggregation, null, cancellationToken));
            }

            var auditEvents = contentResult.AuditEvents;
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

            _contentResult = contentResult;
            _submitTimespan = DateTimeProvider.UtcNow - startTime;
        }

        #endregion

        #region State Serialization
        /// <summary>
        /// Deserialize the configuration.
        /// </summary>
        /// <param name="configurationType">The configuration type.</param>
        /// <param name="configurationText">The configuration text.</param>
        /// <returns>A ContentRegistrationConfiguration</returns>
        protected override ContentRegistrationConfiguration DeserializeConfiguration(string configurationType, string configurationText) => ContentRegistrationConfiguration.Deserialize(configurationType, configurationText);

        /// <summary>
        /// Deserialize the state.
        /// </summary>
        /// <param name="stateType">The state type.</param>
        /// <param name="stateText">The state text.</param>
        /// <returns>A ContentRegistrationState</returns>
        protected override ContentRegistrationState DeserializeState(string stateType, string stateText) => ContentRegistrationState.Deserialize(stateType, stateText);

        /// <summary>
        /// Serialize the state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>A (string, string)</returns>
        protected override (string, string) SerializeState(ContentRegistrationState state) => (ContentRegistrationState.LatestStateType, state.Serialize());
        #endregion

        #region Observability
        /// <summary>
        /// Outcome of the operation
        /// </summary>
        private ContentResult _contentResult;

        /// <summary>
        /// How long it took to for the operation to execute
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
            dimensions[StandardDimensions.CHANNEL_EXTERNAL_ID] = Configuration.ChannelExternalId;
            dimensions[StandardDimensions.CHANNEL_TITLE] = Configuration.ChannelTitle;

            return dimensions;
        }

        /// <summary>
        /// Get custom result dimensions.
        /// </summary>
        /// <returns>A Dimensions</returns>
        protected override Dimensions GetCustomResultDimensions()
        {
            var dimensions = base.GetCustomResultDimensions();
            dimensions[StandardDimensions.WORK_COMPLETE] = _contentResult?.ResultType.ToString() ?? "Unknown";
            if (!string.IsNullOrEmpty(_contentResult?.Reason))
            {
                dimensions.Add(StandardDimensions.ACTION_RESULT_REASON, _contentResult.Reason);
            }
            if (_contentResult?.Exception != null)
            {
                dimensions[StandardDimensions.EXCEPTION] = _contentResult.Exception.ToString();
            }

            if (_contentResult?.Dimensions != null)
            {
                foreach (var dimension in _contentResult.Dimensions)
                {
                    dimensions[dimension.Key] = dimension.Value;
                }
            }

            return dimensions;
        }

        /// <summary>
        /// Get custom result measures.
        /// </summary>
        /// <returns>A Measures</returns>
        protected override Measures GetCustomResultMeasures()
        {
            var measures = base.GetCustomResultMeasures();
            measures[StandardMeasures.RECORD_COUNT] = _contentResult?.Records.Count ?? 0;
            measures[StandardMeasures.BINARY_COUNT] = _contentResult?.Records.SelectMany(a => a.Binaries).Count() ?? 0;
            measures[StandardMeasures.AGGREGATION_COUNT] = _contentResult?.Aggregations.Count ?? 0;
            measures[StandardMeasures.AUDIT_COUNT] = _contentResult?.AuditEvents.Count ?? 0;
            if (_actionExecutionTimespan.HasValue)
                measures[StandardMeasures.ACTION_EXECUTION_SECONDS] = _actionExecutionTimespan.Value.Milliseconds / 1000D;
            if (_submitTimespan.HasValue)
                measures[StandardMeasures.SUBMIT_SECONDS] = _submitTimespan.Value.Milliseconds / 1000D;

            if (_contentResult?.Measures != null)
            {
                foreach (var measure in _contentResult.Measures)
                {
                    measures[measure.Key] = measure.Value;
                }
            }

            return measures;
        }
        #endregion

        #region Disposable
        /// <summary>
        /// Dispose invocation results
        /// </summary>
        protected override void InnerDispose()
        {
            _connectorConfiguration = null;
            _contentResult = null;
            _actionExecutionTimespan = null;
            _submitTimespan = null;

            base.InnerDispose();
        }
        #endregion
    }
}