using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Configuration;
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

namespace RecordPoint.Connectors.SDK.ContentManager;

/// <summary>
/// The content synchronisation operation.
/// </summary>
public class ContentSynchronisationOperation : ManagedQueueableWorkBase<ContentSynchronisationConfiguration, ContentSynchronisationState>
{
    /// <summary>
    /// WORK TYPE.
    /// </summary>
    public const string WORK_TYPE = "Content Synchronisation";

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
    private readonly IOptions<ContentSynchronisationOperationOptions> _options;
    /// <summary>
    /// The content manager options.
    /// </summary>
    private readonly IOptions<ContentManagerOptions> _contentManagerOptions;
    /// <summary>
    /// The record submission options.
    /// </summary>
    private readonly IOptions<RecordSubmissionOptions> _recordSubmissionOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContentSynchronisationOperation"/> class.
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
    /// <param name="recordSubmissionOptions">The record submission options.</param>
    public ContentSynchronisationOperation(
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
        IOptions<ContentSynchronisationOperationOptions> options,
        IOptions<ContentManagerOptions> contentManagerOptions,
        IOptions<RecordSubmissionOptions> recordSubmissionOptions)
        : base(serviceProvider, managedWorkFactory, systemContext, observabilityScope, telemetryTracker, dateTimeProvider)
    {
        _contentManagerActionProvider = contentManagerActionProvider;
        _connectorManager = connectorManager;
        _channelManager = channelManager;
        _workQueueClient = workQueueClient;
        _toggleProvider = toggleProvider;
        _options = options;
        _contentManagerOptions = contentManagerOptions;
        _recordSubmissionOptions = recordSubmissionOptions;
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
        _isFirstExecution = string.IsNullOrWhiteSpace(State?.Cursor);

        _connectorConfiguration = await _connectorManager.GetConnectorAsync(WorkRequest.ConnectorConfigId, cancellationToken);

        if (!(await CheckConnectorEnabledStatusAsync(_connectorConfiguration, _options.Value, State, _contentManagerOptions.Value.MaxDisabledConnectorAge, cancellationToken)))
            return;

        var channel = await _channelManager.GetChannelAsync(WorkRequest.ConnectorConfigId, Configuration.ChannelExternalId, cancellationToken);
        if (channel == null)
        {
            // If channel not found assume its been removed from the content source
            await AbandonedAsync("Channel not found", cancellationToken);
            return;
        }

        if (!(await EnsureWorkOwnershipAsync(channel, cancellationToken)))
            return;

        if (await CheckSemaphoreLockAsync(_connectorConfiguration, Configuration.ChannelExternalId, cancellationToken))
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
                _backOffSeconds = result.NextDelay ?? 0;
                await HandleBackOffResultAsync(_connectorConfiguration, Configuration.ChannelExternalId, result.SemaphoreLockType, result.NextDelay, result.MaxNextDelay, cancellationToken);
                break;

            default:
                throw new RequiredValueOutOfRangeException(nameof(result.ResultType));
        }
    }

    /// <summary>
    /// Verify this work item owns the Content Synchronisation slot on the Channel.
    /// For backwards compatibility with work that was scheduled before <see cref="ChannelModel.ContentSynchronisationWorkId"/>
    /// was introduced, claim ownership when the slot is currently unset. Abandons the work when the slot is owned
    /// by a different work item, indicating this message has been superseded.
    /// </summary>
    /// <param name="channel">The channel loaded for this work execution.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns><c>true</c> if this work item owns (or has just claimed) the slot; otherwise <c>false</c>.</returns>
    protected async Task<bool> EnsureWorkOwnershipAsync(ChannelModel channel, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(channel.ContentSynchronisationWorkId))
        {
            TelemetryTracker.TrackTrace($"Claiming Content Synchronisation ownership for channel [{channel.ExternalId}] with WorkId [{Id}]", SeverityLevel.Verbose);
            await _channelManager.PatchChannelAsync(channel.ConnectorId, channel.ExternalId, c => c.ContentSynchronisationWorkId = Id, cancellationToken);
            channel.ContentSynchronisationWorkId = Id;
        }
        else if (!string.Equals(channel.ContentSynchronisationWorkId, Id, StringComparison.OrdinalIgnoreCase))
        {
            TelemetryTracker.TrackTrace($"Abandoning Content Synchronisation work [{Id}] - does not match ContentSynchronisationWorkId [{channel.ContentSynchronisationWorkId}] on channel [{channel.ExternalId}]", SeverityLevel.Verbose);
            await AbandonedAsync("Content Synchronisation WorkId does not match the Channel", cancellationToken);
            return false;
        }

        return true;
    }

    #region Fetching
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
            cancellationToken.ThrowIfCancellationRequested();
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
        var startDate = await GetInitialStartSyncTimeAsync(cancellationToken);
        using var scope = _serviceProvider.CreateScope();
        var action = _contentManagerActionProvider.CreateContentSynchronisationAction(scope);
        return await action.BeginAsync(_connectorConfiguration, channel, startDate, cancellationToken);
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
        using var scope = _serviceProvider.CreateScope();
        var action = _contentManagerActionProvider.CreateContentSynchronisationAction(scope);
        return await action.ContinueAsync(_connectorConfiguration, channel, cursor, cancellationToken);
    }

    /// <summary>
    /// Get the start time for a new synchronisation operation, uses UtcNow if it cannot find the authorization time from the connector config
    /// </summary>
    /// <returns>Sync start time</returns>
    protected async Task<DateTimeOffset> GetInitialStartSyncTimeAsync(CancellationToken cancellationToken)
    {
        _connectorConfiguration = await _connectorManager.GetConnectorAsync(WorkRequest.ConnectorConfigId, cancellationToken);
        return _connectorConfiguration.GetConsentAuthorizedOn() ?? DateTimeOffset.UtcNow.Date;
    }
    #endregion

    #region Handle Content

    /// <summary>
    /// Handle complete content asynchronously.
    /// </summary>
    /// <param name="contentResult">The content result.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task</returns>
    protected Task HandleCompleteContentAsync(ContentResult contentResult, CancellationToken cancellationToken)
    {
        // If we've gotten all the content available at this time wait a short while before going again
        // Determine how long we should wait until we run again
        _backOffSeconds = contentResult.NextDelay ?? CalculateBackOffSeconds(
            _options.Value,
            contentResult.Records.Any() || contentResult.Aggregations.Any() || contentResult.ResultType == ContentResultType.Incomplete,
            State.LastBackOffDelaySeconds);

        return HandleSuccessfulContentAsync(contentResult, _backOffSeconds, cancellationToken);
    }

    /// <summary>
    /// Handle incomplete content asynchronously.
    /// </summary>
    /// <param name="contentResult">The content result.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task</returns>
    protected Task HandleIncompleteContentAsync(ContentResult contentResult, CancellationToken cancellationToken)
    {
        // Incomplete result means there is still more content to sync based on the current execution
        // so we should re-execute immediately (unless a delay has been requested by the action)
        _backOffSeconds = contentResult.NextDelay ?? 0;

        return HandleSuccessfulContentAsync(contentResult, _backOffSeconds, cancellationToken);
    }

    /// <summary>
    /// Handle failed content asynchronously.
    /// </summary>
    /// <param name="contentResult">The content result.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task</returns>
    protected Task HandleFailedContentAsync(ContentResult contentResult, CancellationToken cancellationToken)
    {
        // Should continue the failed content operation in the next runs. The underlying work manager retries
        // the work (preserving the WorkId) until MaxRetries is exhausted, so the ownership slot must remain
        // claimed across retry attempts. Clearing is handled by AbandonedAsync (terminal) only.
        _contentResult = contentResult;
        return FaultedAsync(contentResult.Reason, contentResult.Exception, cancellationToken);
    }

    /// <summary>
    /// Handle abandonded content asynchronously.
    /// </summary>
    /// <param name="contentResult">The content result.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task</returns>
    protected async Task HandleAbandondedContentAsync(ContentResult contentResult, CancellationToken cancellationToken)
    {
        // Should abandon the operation as this is typically due to the Channel no longer being available
        await _channelManager.RemoveChannelAsync(WorkRequest.ConnectorConfigId, Configuration.ChannelExternalId, cancellationToken);

        _contentResult = contentResult;
        await AbandonedAsync(contentResult.Reason, cancellationToken);
    }

    /// <summary>
    /// Handle successful content asynchronously.
    /// </summary>
    /// <param name="contentResult">The content result.</param>
    /// <param name="backOffSeconds">The back off seconds.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task</returns>
    protected async Task HandleSuccessfulContentAsync(ContentResult contentResult, int backOffSeconds, CancellationToken cancellationToken)
    {
        var startTime = DateTimeProvider.UtcNow;
        var nextRunTime = DateTimeProvider.UtcNow.AddSeconds(backOffSeconds);
        var submitBinaries = _toggleProvider.ShouldSubmitBinaries(SystemContext, _connectorConfiguration);

        var tasks = new List<Task>();

        var records = contentResult.Records;
        foreach (var record in records)
        {
            if (!submitBinaries)
            {
                //If content protection is disabled, remove all binaries from the record 
                record.Binaries = [];
            }

            cancellationToken.ThrowIfCancellationRequested();
            tasks.Add(_workQueueClient.SubmitRecordAsync(_connectorConfiguration, record, null, cancellationToken));

            if (!_recordSubmissionOptions.Value.SubmitRecordAndBinariesSynchronously)
            {
                var binaries = record.Binaries;
                foreach (var binary in binaries)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    tasks.Add(_workQueueClient.SubmitBinaryAsync(_connectorConfiguration, binary, null, cancellationToken));
                }
            }
        }

        var aggregations = contentResult.Aggregations;
        foreach (var aggregation in aggregations)
        {
            cancellationToken.ThrowIfCancellationRequested();
            tasks.Add(_workQueueClient.SubmitAggregationAsync(_connectorConfiguration, aggregation, null, cancellationToken));
        }

        var auditEvents = contentResult.AuditEvents;
        foreach (var auditEvent in auditEvents)
        {
            cancellationToken.ThrowIfCancellationRequested();
            tasks.Add(_workQueueClient.SubmitAuditEventAsync(_connectorConfiguration, auditEvent, null, cancellationToken));
        }

        await Task.WhenAll(tasks).ConfigureAwait(false);

        var finalState = new ContentSynchronisationState
        {
            LastBackOffDelaySeconds = backOffSeconds,
            Cursor = contentResult.Cursor
        };
        _contentResult = contentResult;
        _submitTimespan = DateTimeProvider.UtcNow - startTime;
        await ContinueAsync("Synchronisation complete normally", finalState, nextRunTime, cancellationToken);
    }
    #endregion

    #region State Serialization
    /// <summary>
    /// Deserialize the configuration.
    /// </summary>
    /// <param name="configurationType">The configuration type.</param>
    /// <param name="configurationText">The configuration text.</param>
    /// <returns>A ContentSynchronisationConfiguration</returns>
    protected override ContentSynchronisationConfiguration DeserializeConfiguration(string configurationType, string configurationText) => ContentSynchronisationConfiguration.Deserialize(configurationType, configurationText);

    /// <summary>
    /// Serialize the configuration.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <returns>A (string, string)</returns>
    protected override (string, string) SerializeConfiguration(ContentSynchronisationConfiguration configuration) => (ContentSynchronisationConfiguration.ConfigurationType, configuration.Serialize());

    /// <summary>
    /// Deserialize the state.
    /// </summary>
    /// <param name="stateType">The state type.</param>
    /// <param name="stateText">The state text.</param>
    /// <returns>A ContentSynchronisationState</returns>
    protected override ContentSynchronisationState DeserializeState(string stateType, string stateText) => ContentSynchronisationState.Deserialize(stateType, stateText);

    /// <summary>
    /// Serialize the state.
    /// </summary>
    /// <param name="state">The state.</param>
    /// <returns>A (string, string)</returns>
    protected override (string, string) SerializeState(ContentSynchronisationState state) => (ContentSynchronisationState.StateType, state.Serialize());
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
    /// The back-off delay in seconds applied during this execution
    /// </summary>
    private int _backOffSeconds;

    /// <summary>
    /// Whether this is the first execution (no cursor yet)
    /// </summary>
    private bool _isFirstExecution;

    /// <summary>
    /// Override custom dimensions key for ConnectorId, TenantId and TenantDomainName
    /// </summary>
    /// <returns></returns>
    protected override Dimensions GetCustomKeyDimensions()
    {
        var dimensions = base.GetCustomKeyDimensions();

        dimensions[StandardDimensions.CONNECTOR_ID] = WorkRequest.ConnectorConfigId;
        dimensions[StandardDimensions.TENANT_ID] = WorkRequest.TenantId;
        dimensions[StandardDimensions.TENANT_DOMAIN_NAME] = WorkRequest.TenantDomainName;
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
        dimensions[StandardDimensions.IS_FIRST_EXECUTION] = _isFirstExecution.ToString();
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

        var backOffSeconds =
        WaitTill.HasValue && FinishDateTime != default
            ? Math.Max(0, (WaitTill.Value - FinishDateTime).TotalSeconds)
            : _backOffSeconds;

        measures[StandardMeasures.RECORD_COUNT] = _contentResult?.Records.Count ?? 0;
        measures[StandardMeasures.BINARY_COUNT] = _contentResult?.Records.SelectMany(a => a.Binaries).Count() ?? 0;
        measures[StandardMeasures.AGGREGATION_COUNT] = _contentResult?.Aggregations.Count ?? 0;
        measures[StandardMeasures.AUDIT_COUNT] = _contentResult?.AuditEvents.Count ?? 0;
        if (_actionExecutionTimespan.HasValue)
            measures[StandardMeasures.ACTION_EXECUTION_SECONDS] = _actionExecutionTimespan.Value.Milliseconds / 1000D;
        if (_submitTimespan.HasValue)
            measures[StandardMeasures.SUBMIT_SECONDS] = _submitTimespan.Value.Milliseconds / 1000D;
        measures[StandardMeasures.BACK_OFF_SECONDS] = backOffSeconds;

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