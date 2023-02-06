﻿using Microsoft.Extensions.Logging;
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
    /// Implementation of a managed work item that performs a Content Registration Operation
    /// </summary>
    public class ContentRegistrationOperation : ManagedQueueableWorkBase<ContentRegistrationConfiguration, ContentRegistrationState>
    {
        public const string WORK_TYPE = "Content Registration";

        private readonly IContentManagerActionProvider _contentManagerActionProvider;
        private readonly IConnectorConfigurationManager _connectorManager;
        private readonly IChannelManager _channelManager;
        private readonly IWorkQueueClient _workQueueClient;
        private readonly IOptions<ContentRegistrationOperationOptions> _options;

        public ContentRegistrationOperation(
            IServiceProvider serviceProvider,
            IContentManagerActionProvider contentManagerActionProvider,
            IConnectorConfigurationManager connectorManager,
            IChannelManager channelManager,
            IWorkQueueClient workQueueClient,
            IManagedWorkFactory managedWorkFactory,
            ISystemContext systemContext,
            IScopeManager scopeManager,
            ILogger<ContentRegistrationOperation> logger,
            ITelemetryTracker telemetryTracker,
            IDateTimeProvider dateTimeProvider,
            IOptions<ContentRegistrationOperationOptions> options)
            : base(serviceProvider, managedWorkFactory, systemContext, scopeManager, logger, telemetryTracker, dateTimeProvider)
        {
            _contentManagerActionProvider = contentManagerActionProvider;
            _connectorManager = connectorManager;
            _channelManager = channelManager;
            _workQueueClient = workQueueClient;
            _options = options;
        }

        public override string ServiceName => ContentManagerObservabilityExtensions.SERVICE_NAME;

        public override string WorkType => WORK_TYPE;

        private ConnectorConfigModel _connectorConfiguration;

        protected override async Task InnerRunAsync(CancellationToken cancellationToken)
        {
            _connectorConfiguration = await _connectorManager.GetConnectorAsync(Configuration.ConnectorConfigurationId, cancellationToken);
            if (_connectorConfiguration == null)
            {
                // If connector not found assume its been deleted
                await AbandonedAsync("Connector not found", cancellationToken);
                return;
            }

            var channelExists = await _channelManager.ChannelExistsAsync(Configuration.ConnectorConfigurationId, Configuration.ChannelExternalId, cancellationToken);
            if (!channelExists)
            {
                // If channel not found assume its been removed from the content source
                await AbandonedAsync("Channel not found", cancellationToken);
                return;
            }

            var connectorStatus = await _connectorManager.GetConnectorStatusAsync(Configuration.ConnectorConfigurationId, cancellationToken);
            if (!connectorStatus.Enabled)
            {
                //Connector Configuration is disabled, so backoff until it is re-enabled
                var backOffSeconds = CalculateBackOffSeconds(
                    _options.Value,
                    false,
                    State.LastBackOffDelaySeconds);

                State.LastBackOffDelaySeconds = backOffSeconds;

                await ContinueAsync(connectorStatus.EnabledReason, State, DateTimeProvider.UtcNow.AddSeconds(backOffSeconds), cancellationToken);
                return;
            }

            if (await CheckSemaphoreLockAsync(_connectorConfiguration, cancellationToken))
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
                    await HandleBackOffResultAsync(_connectorConfiguration, result.SemaphoreLockType, result.NextDelay, cancellationToken);
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

        protected async Task<ContentResult> BeginAsync(Channel channel, CancellationToken cancellationToken)
        {
            var action = CreateContentRegistrationAction();
            return await action.BeginAsync(_connectorConfiguration, channel, Configuration.Context, cancellationToken);
        }

        protected async Task<ContentResult> ContinueSync(Channel channel, string cursor, CancellationToken cancellationToken)
        {
            var action = CreateContentRegistrationAction();
            return await action.ContinueAsync(_connectorConfiguration, channel, cursor, Configuration.Context, cancellationToken);
        }
        #endregion

        #region Handle Content

        protected async Task HandleCompleteContentAsync(ContentResult contentResult, CancellationToken cancellationToken)
        {
            // We've registered all available content so the operation can completed
            await SubmitContentAsync(contentResult, cancellationToken).ConfigureAwait(false);
            await CompleteAsync(contentResult.Reason, cancellationToken).ConfigureAwait(false);
        }

        protected async Task HandleIncompleteContentAsync(ContentResult contentResult, CancellationToken cancellationToken)
        {
            // If we haven't gotten everything available go again straight away.
            await SubmitContentAsync(contentResult, cancellationToken).ConfigureAwait(false);

            var backOffSeconds = _options.Value.ExponentialBackOff ? _options.Value.DelaySeconds : 0;
            var startTime = DateTimeProvider.UtcNow;
            var nextRunTime = DateTimeProvider.UtcNow.AddSeconds(backOffSeconds);

            var finalState = new ContentRegistrationState
            {
                LastBackOffDelaySeconds = backOffSeconds,
                Cursor = contentResult.Cursor
            };
            _contentResult = contentResult;
            _submitTimespan = DateTimeProvider.UtcNow - startTime;

            await ContinueAsync("Registration complete normally", finalState, nextRunTime, cancellationToken);
        }

        protected Task HandleFailedContentAsync(ContentResult contentResult, CancellationToken cancellationToken)
        {
            // Should continue the failed content operation in the next runs
            _contentResult = contentResult;
            return FaultedAsync(contentResult.Reason, contentResult.Exception, cancellationToken);
        }

        protected Task HandleAbandondedContentAsync(ContentResult contentResult, CancellationToken cancellationToken)
        {
            // Should complete the abandonded content operation as this is typically due to the Channel no longer being available
            _contentResult = contentResult;
            return AbandonedAsync(contentResult.Reason, cancellationToken);
        }

        protected async Task SubmitContentAsync(ContentResult contentResult, CancellationToken cancellationToken)
        {
            var tasks = new List<Task>();

            var records = contentResult.Records;
            foreach (var record in records)
            {
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
        }

        #endregion

        #region State Serialization
        protected override ContentRegistrationConfiguration DeserializeConfiguration(string configurationType, string configurationText) => ContentRegistrationConfiguration.Deserialize(configurationType, configurationText);

        protected override ContentRegistrationState DeserializeState(string stateType, string stateText) => ContentRegistrationState.Deserialize(stateType, stateText);

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

            return dimensions;
        }

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
            return dimensions;
        }

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

            return measures;
        }
        #endregion
    }
}