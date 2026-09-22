using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Configuration;
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

namespace RecordPoint.Connectors.SDK.ContentManager;

/// <summary>
/// The channel discovery operation.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ChannelDiscoveryOperation"/> class.
/// </remarks>
/// <param name="serviceProvider">The service provider.</param>
/// <param name="contentManagerActionProvider">The content manager action provider.</param>
/// <param name="connectorManager">The connector manager.</param>
/// <param name="channelManager">The channel manager.</param>
/// <param name="managedWorkFactory">The managed work factory.</param>
/// <param name="systemContext">The system context.</param>
/// <param name="observabilityScope">The scope manager.</param>
/// <param name="telemetryTracker">The telemetry tracker.</param>
/// <param name="dateTimeProvider">The date time provider.</param>
/// <param name="options">The options.</param>
/// <param name="contentManagerOptions">The content manager options.</param>
public class ChannelDiscoveryOperation(
    IServiceProvider serviceProvider,
    IContentManagerActionProvider contentManagerActionProvider,
    IConnectorConfigurationManager connectorManager,
    IChannelManager channelManager,
    IManagedWorkFactory managedWorkFactory,
    ISystemContext systemContext,
    IObservabilityScope observabilityScope,
    ITelemetryTracker telemetryTracker,
    IDateTimeProvider dateTimeProvider,
    IOptions<ChannelDiscoveryOperationOptions> options,
    IOptions<ContentManagerOptions> contentManagerOptions) : ManagedQueueableWorkBase<ChannelDiscoveryConfiguration, ChannelDiscoveryState>(serviceProvider, managedWorkFactory, systemContext, observabilityScope, telemetryTracker, dateTimeProvider)
{
    /// <summary>
    /// WORK TYPE.
    /// </summary>
    public const string WORK_TYPE = "Channel Discovery";

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
        //Get the Connector Configuration for this work operation
        var connectorConfiguration = await connectorManager.GetConnectorConfigurationAsync(WorkRequest.ConnectorConfigId, cancellationToken);

        //Convert to a Connector Config model for use in actions
        _connectorConfiguration = connectorConfiguration?.ConvertToConnectorConfig();

        if (!(await CheckConnectorEnabledStatusAsync(_connectorConfiguration, options.Value, State, contentManagerOptions.Value.MaxDisabledConnectorAge, cancellationToken)))
            return;

        if (!(await EnsureWorkOwnershipAsync(connectorConfiguration, cancellationToken)))
            return;

        if (await CheckSemaphoreLockAsync(_connectorConfiguration, null, cancellationToken))
            return;

        //Set the Channel Discovery executed date on the Connector Configuration
        await connectorManager.PatchConnectorConfigurationAsync(connectorConfiguration.ConnectorId, connector => connector.ChannelDiscoveryExecutedDate = DateTime.SpecifyKind(DateTimeProvider.UtcNow, DateTimeKind.Utc), cancellationToken);

        var startTime = DateTimeProvider.UtcNow;

        // If everything is good go and fetch new channels
        _channelDiscoveryResult = await FetchAsync(cancellationToken);
        _actionExecutionTimespan = DateTimeProvider.UtcNow - startTime;
        switch (_channelDiscoveryResult.ResultType)
        {
            case ChannelDiscoveryResultType.Complete:
                await HandleCompleteResultAsync(_channelDiscoveryResult, cancellationToken);
                break;
            case ChannelDiscoveryResultType.Incomplete:
                await HandleIncompleteResultAsync(_channelDiscoveryResult, cancellationToken);
                break;
            case ChannelDiscoveryResultType.Failed:
                await HandleFailedResultAsync(_channelDiscoveryResult, cancellationToken);
                break;
            case ChannelDiscoveryResultType.Abandoned:
                await HandleAbandonedResultAsync(_channelDiscoveryResult, cancellationToken);
                break;
            case ChannelDiscoveryResultType.BackOff:
                await HandleBackOffResultAsync(_connectorConfiguration, null, _channelDiscoveryResult.SemaphoreLockType, _channelDiscoveryResult.NextDelay, _channelDiscoveryResult.MaxNextDelay, cancellationToken);
                break;

            default:
                throw new RequiredValueOutOfRangeException(nameof(_channelDiscoveryResult.ResultType));
        }
    }

    /// <summary>
    /// Verify this work item owns the Channel Discovery slot on the Connector Configuration.
    /// For backwards compatibility with work that was scheduled before <see cref="ConnectorConfigurationModel.ChannelDiscoveryWorkId"/>
    /// was introduced, claim ownership when the slot is currently unset. Abandons the work when the slot is owned
    /// by a different work item, indicating this message has been superseded.
    /// </summary>
    /// <param name="connectorConfiguration">The connector configuration loaded for this work execution.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns><c>true</c> if this work item owns (or has just claimed) the slot and execution should continue; otherwise <c>false</c>.</returns>
    protected async Task<bool> EnsureWorkOwnershipAsync(ConnectorConfigurationModel connectorConfiguration, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(connectorConfiguration.ChannelDiscoveryWorkId))
        {
            TelemetryTracker.TrackTrace($"Claiming Channel Discovery ownership for connector [{connectorConfiguration.ConnectorId}] with WorkId [{Id}]", SeverityLevel.Verbose);
            await connectorManager.PatchConnectorConfigurationAsync(connectorConfiguration.ConnectorId, connector => connector.ChannelDiscoveryWorkId = Id, cancellationToken);
            connectorConfiguration.ChannelDiscoveryWorkId = Id;
        }
        else if (!string.Equals(connectorConfiguration.ChannelDiscoveryWorkId, Id, StringComparison.OrdinalIgnoreCase))
        {
            TelemetryTracker.TrackTrace($"Abandoning Channel Discovery work [{Id}] - does not match ChannelDiscoveryWorkId [{connectorConfiguration.ChannelDiscoveryWorkId}] on connector [{connectorConfiguration.ConnectorId}]", SeverityLevel.Verbose);
            await AbandonedAsync("Channel Discovery WorkId does not match the Connector Configuration", cancellationToken);
            return false;
        }

        return true;
    }

    #region Fetching
    /// <summary>
    /// Create a Channel Discovery Operation for the current connector configuration
    /// </summary>
    /// <returns>Channel scanner</returns>
    protected IChannelDiscoveryAction CreateChannelDiscoveryAction(IServiceScope scope)
    {
        try
        {
            return contentManagerActionProvider.CreateChannelDiscoveryAction(scope);
        }
        catch (NotImplementedException)
        {
            // Fall back to null Channel scanner
            return new NullChannelDiscoveryAction(channelManager);
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
            using var scope = _serviceProvider.CreateScope();
            var channelScanner = CreateChannelDiscoveryAction(scope);
            var cursor = State.Cursor;
            result = await channelScanner.ExecuteAsync(_connectorConfiguration, cancellationToken, cursor);
            cancellationToken.ThrowIfCancellationRequested();
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
        var startTime = await HandleSuccessfulResultAsync(channelResult, cancellationToken);

        // Determine how long we should wait until we start a full channel discovery again
        var backOffSeconds = channelResult.NextDelay
            ?? CalculateBackOffSeconds(options.Value, channelResult.Channels.Count != 0, State.LastBackOffDelaySeconds);

        var nextRunTime = DateTimeProvider.UtcNow.AddSeconds(backOffSeconds);

        // Ensure Work is queued for each Channel
        var finalState = new ChannelDiscoveryState
        {
            LastBackOffDelaySeconds = backOffSeconds
        };
        _submitTimespan = DateTimeProvider.UtcNow - startTime;
        await ContinueAsync("Channel Discovery completed normally", finalState, nextRunTime, cancellationToken);
    }

    /// <summary>
    /// Handle incomplete result asynchronously. Further Channel Discovery work is expected, tracked via cursor.
    /// </summary>
    /// <param name="channelResult">The channel result.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task</returns>
    protected async Task HandleIncompleteResultAsync(ChannelDiscoveryResult channelResult, CancellationToken cancellationToken)
    {
        var startTime = await HandleSuccessfulResultAsync(channelResult, cancellationToken);

        // Incomplete result means there is still more channels to discover based on the current execution
        // so we should re-execute immediately (unless a delay has been requested by the action)
        var backOffSeconds = channelResult.NextDelay ?? 0;
        var nextRunTime = DateTimeProvider.UtcNow.AddSeconds(backOffSeconds);

        var finalState = new ChannelDiscoveryState
        {
            LastBackOffDelaySeconds = backOffSeconds,
            Cursor = channelResult.Cursor
        };
        _submitTimespan = DateTimeProvider.UtcNow - startTime;
        await ContinueAsync("Channel Discovery batch completed normally", finalState, nextRunTime, cancellationToken);
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

    /// <summary>
    /// Handle abandoned result asynchronously.
    /// </summary>
    /// <param name="channelResult">The channel result.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task</returns>
    protected Task HandleAbandonedResultAsync(ChannelDiscoveryResult channelResult, CancellationToken cancellationToken)
    {
        return AbandonedAsync(channelResult.Reason, cancellationToken);
    }

    /// <summary>
    /// Common subsequent logic for successful Channel Discovery fetch.
    /// </summary>
    /// <param name="channelResult">The channel result.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Start time of successful Channel Discovery result handling.</returns>
    protected async Task<DateTime> HandleSuccessfulResultAsync(ChannelDiscoveryResult channelResult, CancellationToken cancellationToken)
    {
        var batchSize = options.Value.BatchSize;

        // Deduplicate on lightweight Channel objects first (no JSON serialization),
        // then convert to ChannelModel per-batch to avoid O(N) memory allocation
        // that causes OOM when cursor pages contain tens of thousands of channels.
        var allChannels = channelResult.Channels
            .UnionBy(channelResult.NewChannelRegistrations, a => a.ExternalId)
            .UnionBy(channelResult.RenamedChannelRegistrations, a => a.ExternalId);

        foreach (var batch in allChannels.Chunk(batchSize))
        {
            var batchModels = batch.Select(c => c.ToChannelModel()).ToList();
            batchModels.ForEach(m => m.ConnectorId = _connectorConfiguration.Id);
            await channelManager.UpsertChannelsAsync(batchModels, cancellationToken);
        }

        var startTime = DateTimeProvider.UtcNow;

        // Start operations in batches to avoid holding tens of thousands of
        // concurrent pending tasks in memory. Collect exceptions per batch so
        // that a failure in one batch doesn't prevent remaining work from being
        // attempted (preserving the original "attempt all work" behavior).
        var batchExceptions = new List<Exception>();

        await RunBatchedAsync(channelResult.Channels.DistinctBy(channel => channel.ExternalId).ToList(), batchSize, batchExceptions, channel =>
            SubmitContentSynchronisationAsync(channel, cancellationToken));

        await RunBatchedAsync(channelResult.NewChannelRegistrations.DistinctBy(channel => channel.ExternalId).ToList(), batchSize, batchExceptions, channel =>
            SubmitContentRegistrationAsync(channel, null, cancellationToken));

        // When channels are renamed, trigger unlimited content registrations.
        await RunBatchedAsync(channelResult.RenamedChannelRegistrations.DistinctBy(channel => channel.ExternalId).ToList(), batchSize, batchExceptions, channel =>
        {
            var context = new Dictionary<string, string>
            {
                {IContentRegistrationAction.StartDate, DateTime.MinValue.ToString("o")},
                {nameof(ChannelDiscoveryResult.RenamedChannelRegistrations) , "true"}
            };

            return SubmitContentRegistrationAsync(channel, context, cancellationToken);
        });

        await RunBatchedAsync(channelResult.AuditEvents, batchSize, batchExceptions, auditEvent =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _workQueueClient.SubmitAuditEventAsync(_connectorConfiguration, auditEvent, null, cancellationToken);
        });

        if (batchExceptions.Count > 0)
        {
            throw new AggregateException("One or more operation batches failed", batchExceptions);
        }

        return startTime;
    }

    /// <summary>
    /// Submits Content Synchronisation work for the supplied channel if no Content Synchronisation work currently
    /// owns the Channel. Records the new WorkId on the Channel so the consumer can detect supersession.
    /// </summary>
    private async Task SubmitContentSynchronisationAsync(Channel channel, CancellationToken cancellationToken)
    {
        var channelModel = await channelManager.GetChannelAsync(_connectorConfiguration.Id, channel.ExternalId, cancellationToken);
        if (channelModel == null)
        {
            TelemetryTracker.TrackTrace($"Skipping Content Synchronisation creation for channel [{channel.ExternalId}] - Channel not found", SeverityLevel.Verbose);
            return;
        }

        if (!string.IsNullOrEmpty(channelModel.ContentSynchronisationWorkId))
        {
            TelemetryTracker.TrackTrace($"Skipping Content Synchronisation creation for channel [{channel.ExternalId}] - ContentSynchronisationWorkId already set to [{channelModel.ContentSynchronisationWorkId}]", SeverityLevel.Verbose);
            return;
        }

        using var op = managedWorkFactory.CreateContentSynchronisationOperation(_connectorConfiguration, channel);
        var workId = op.WorkStatus.WorkId;
        await op.StartAsync(cancellationToken);
        await channelManager.PatchChannelAsync(_connectorConfiguration.Id, channel.ExternalId, c => c.ContentSynchronisationWorkId = workId, cancellationToken);
        _contentSynchronisationOperationsStarted++;
    }

    /// <summary>
    /// Submits Content Registration work for the supplied channel if no Content Registration work currently
    /// owns the Channel. Records the new WorkId on the Channel so the consumer can detect supersession.
    /// </summary>
    private async Task SubmitContentRegistrationAsync(Channel channel, Dictionary<string, string> context, CancellationToken cancellationToken)
    {
        var channelModel = await channelManager.GetChannelAsync(_connectorConfiguration.Id, channel.ExternalId, cancellationToken);
        if (channelModel == null)
        {
            TelemetryTracker.TrackTrace($"Skipping Content Registration creation for channel [{channel.ExternalId}] - Channel not found", SeverityLevel.Verbose);
            return;
        }

        using var op = context == null
            ? managedWorkFactory.CreateContentRegistrationOperation(_connectorConfiguration, channel)
            : managedWorkFactory.CreateContentRegistrationOperation(_connectorConfiguration, channel, context);
        await op.StartAsync(cancellationToken);
        _contentRegistrationOperationsStarted++;
    }

    /// <summary>
    /// Executes an async operation for each item in batches, collecting failures
    /// so that one batch failure doesn't prevent remaining batches from running.
    /// </summary>
    private static async Task RunBatchedAsync<T>(
        IReadOnlyList<T> items,
        int batchSize,
        List<Exception> exceptions,
        Func<T, Task> operationFactory)
    {
        foreach (var batch in items.Chunk(batchSize))
        {
            var tasks = new List<Task>(batch.Length);
            foreach (var item in batch)
            {
                tasks.Add(operationFactory(item));
            }
            try { await Task.WhenAll(tasks).ConfigureAwait(false); }
            catch (Exception ex) { exceptions.Add(ex); }
        }
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
    /// Serialize the configuration.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <returns>A (string, string)</returns>
    protected override (string, string) SerializeConfiguration(ChannelDiscoveryConfiguration configuration) => (ChannelDiscoveryConfiguration.ConfigurationType, configuration.Serialize());

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
    protected override (string, string) SerializeState(ChannelDiscoveryState state) => (ChannelDiscoveryState.StateType, state.Serialize());
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

        dimensions[StandardDimensions.CONNECTOR_ID] = WorkRequest.ConnectorConfigId;
        dimensions[StandardDimensions.TENANT_ID] = WorkRequest.TenantId;
        dimensions[StandardDimensions.TENANT_DOMAIN_NAME] = WorkRequest.TenantDomainName;

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

    #region Disposable
    /// <summary>
    /// Dispose invocation results
    /// </summary>
    protected override void InnerDispose()
    {
        _connectorConfiguration = null;
        _channelDiscoveryResult = null;
        _actionExecutionTimespan = null;
        _submitTimespan = null;

        base.InnerDispose();
    }
    #endregion
}
