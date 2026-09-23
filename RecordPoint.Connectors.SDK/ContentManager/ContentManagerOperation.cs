using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Configuration;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
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
/// The content manager operation.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ContentManagerOperation"/> class.
/// </remarks>
public class ContentManagerOperation(
    IContentManagerActionProvider contentManagerActionProvider,
    IConnectorConfigurationManager connectorConfigManager,
    IChannelManager channelManager,
    IManagedWorkStatusManager managedWorkStatusManager,
    IManagedWorkFactory managedWorkFactory,
    IOptions<ContentManagerOptions> options,
    IObservabilityScope observabilityScope, 
    ITelemetryTracker telemetryTracker, 
    IDateTimeProvider dateTimeProvider,
    IServiceProvider serviceProvider) : PeriodicWorkBase(observabilityScope, telemetryTracker, dateTimeProvider)
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
    /// Maximum number of aggregation models per removal batch to stay within
    /// the Cosmos DB 524,288-character query size limit.
    /// </summary>
    private const int CLEANUP_BATCH_SIZE = 100;

    /// <inheritdoc />
    public override int ServiceIntervalInSeconds => options.Value.DelaySeconds;

    /// <inheritdoc />
    public override string ServiceName => ContentManagerObservabilityExtensions.SERVICE_NAME;

    /// <inheritdoc />
    public override string WorkType => WORK_TYPE;

    private List<ConnectorConfigurationModel> _connectorConfigurations = [];
    private List<ConnectorConfigModel> _connectorConfigModels = [];
    private int _channelDiscoveryOperationsStarted = 0;

    /// <summary>
    /// Performs migration of ChannelDiscovery work previously scheduled by the Content Manager
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected override async Task InnerStartAsync(CancellationToken cancellationToken)
    {
        if (!options.Value.PerformManagedWorkMigration) return;

        //Get All the known Connector Configurations
        var connectorConfigurations = await connectorConfigManager.GetAllConnectorConfigurationsAsync(cancellationToken);

        //Get all running Channel Discovery Work from the obsolete managedworkstatuses
        var channelDiscoveryWorkStatuses = await managedWorkStatusManager
            .GetWorkStatusesAsync(a => a.WorkType == ChannelDiscoveryOperation.WORK_TYPE && a.Status == ManagedWorkStatuses.Running, cancellationToken);

        //Set an Enqueued Date for all configurations that have known running channel discovery work
        //This will prevent duplicated Channel Discovery work from being invoked
        foreach (var connectorConfiguration in connectorConfigurations.Where(a => !a.ChannelDiscoveryEnqueuedDate.HasValue))
        {
            var channelDiscoveryWorkStatus = channelDiscoveryWorkStatuses.FirstOrDefault(a => a.ConnectorId == connectorConfiguration.ConnectorId);
            if (channelDiscoveryWorkStatus == null) continue;

            connectorConfiguration.ChannelDiscoveryEnqueuedDate = DateTime.SpecifyKind(dateTimeProvider.UtcNow, DateTimeKind.Utc);
            await connectorConfigManager.SetConnectorConfigurationAsync(connectorConfiguration, cancellationToken);
        }
    }

    /// <summary>
    /// Inner the run asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task</returns>
    protected async override Task InnerRunAsync(CancellationToken cancellationToken)
    {
        _channelDiscoveryOperationsStarted = 0;

        _connectorConfigurations = await connectorConfigManager.GetAllConnectorConfigurationsAsync(cancellationToken);
        _connectorConfigModels = [.. _connectorConfigurations.Select(a => a.ConvertToConnectorConfig())];

        await CreateChannelDiscoveryOperationsAsync(cancellationToken);

        if (options.Value.CleanUpAggregations)
        {
            await CleanupAggregationsAsync(cancellationToken);
        }

        if (options.Value.CleanUpChannels)
        {
            await CleanupChannelsAsync(cancellationToken);
        }

        Complete("Content Manager completed normally");
    }

    #region Channel Discovery
    /// <summary>
    /// Creates channel discovery operations asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task</returns>
    private async Task CreateChannelDiscoveryOperationsAsync(CancellationToken cancellationToken)
    {
        //Get Configurations that have not yet had Channel Discovery Enqueued AND do not already own a Channel Discovery WorkId.
        //The ChannelDiscoveryWorkId acts as an ownership token: once it is set, no further Channel Discovery work may be created
        //for the connector until the existing work clears it.
        var newConnectorConfigurations = _connectorConfigurations
            .Where(a => !a.ChannelDiscoveryEnqueuedDate.HasValue && string.IsNullOrEmpty(a.ChannelDiscoveryWorkId))
            .ToList();

        //Get the Ids of the "new" configurations and find the matching ConnectorConfigModels
        var newConnectorConfigurationIds = newConnectorConfigurations.Select(a => a.ConnectorId);
        var newConnectorConfigModels = _connectorConfigModels.Where(model => newConnectorConfigurationIds.Contains(model.Id)).ToList();

        //Start a Channel Discovery operation for each new configuration and mark the configuration as having had Channel Discovery Enqueued
        foreach (var connectorId in newConnectorConfigurations.Select(connector => connector.ConnectorId))
        {
            //Re-check the ChannelDiscoveryWorkId immediately before creating the work to guard against another Content Manager instance
            //having claimed ownership between when _connectorConfigurations was loaded and now.
            var currentConfiguration = await connectorConfigManager.GetConnectorConfigurationAsync(connectorId, cancellationToken);
            if (!string.IsNullOrEmpty(currentConfiguration?.ChannelDiscoveryWorkId))
            {
                telemetryTracker.TrackTrace(
                    $"Skipping Channel Discovery creation for connector [{connectorId}] - ChannelDiscoveryWorkId already set to [{currentConfiguration.ChannelDiscoveryWorkId}]",
                    SeverityLevel.Verbose);
                continue;
            }

            //Start the Channel Discovery operation with a 15 second delay to prevent a race condition when updating the enqueued date on the connector configuration.
            //The Channel discovery operation will update the ChannelDiscoveryExecutedDate on each execution which could result in the enqueued date not being correctly set
            //if the read & writes are occurring simulatenously between the Content Manager service and the Channel Discovery service
            var connectorConfigModel = _connectorConfigModels.Find(a => a.Id == connectorId);
            using var channelDiscoveryOperation = managedWorkFactory.CreateChannelDiscoveryOperation(connectorConfigModel);

            await channelDiscoveryOperation.StartAsync(cancellationToken, DateTimeOffset.Now.AddSeconds(15));

            //Update the Connector Configuration to mark that Channel Discovery has been Enqueued and to record the owning WorkId
            await connectorConfigManager.PatchConnectorConfigurationAsync(connectorId, connector =>
            {
                connector.ChannelDiscoveryWorkId = channelDiscoveryOperation.WorkStatus.WorkId;
                connector.ChannelDiscoveryEnqueuedDate = DateTime.SpecifyKind(dateTimeProvider.UtcNow, DateTimeKind.Utc);
            }, cancellationToken);

            _channelDiscoveryOperationsStarted++;
        }

        //Invoke the Content Manager Callback Action if we have found any new configurations
        await InvokeContentManagerCallbackAsync(newConnectorConfigModels, cancellationToken);
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
        var aggregationManager = serviceProvider.GetService<IAggregationManager>();
        if (aggregationManager == null)
            return;

        //Get all Enabled Configurations and Configurations that have been Disabled for less than the Max Disabled Age
        var validConnectorConfigurations = _connectorConfigModels.Where(configuration => !configuration.IsDisabledConnectorExpired(options.Value.MaxDisabledConnectorAge));

        var validConnectorConfigurationIds = validConnectorConfigurations.Select(a => a.Id);
        var obsoleteAggregations = await aggregationManager.GetAggregationsAsync(a => !validConnectorConfigurationIds.Contains(a.ConnectorId), cancellationToken);

        // Batch removals to avoid an unbounded IN (...) clause exceeding
        // Cosmos DB's 524,288-character query limit (error SC3020).
        foreach (var batch in obsoleteAggregations.Chunk(CLEANUP_BATCH_SIZE))
        {
            cancellationToken.ThrowIfCancellationRequested();
            await aggregationManager.RemoveAggregationsAsync(batch, cancellationToken);
        }
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
        var validConnectorConfigurations = _connectorConfigModels.Where(configuration => !configuration.IsDisabledConnectorExpired(options.Value.MaxDisabledConnectorAge));

        var validConnectorConfigurationIds = validConnectorConfigurations.Select(a => a.Id);
        var obsoleteChannels = await channelManager.GetChannelsAsync(a => !validConnectorConfigurationIds.Contains(a.ConnectorId), cancellationToken);

        // Batch removals to avoid an unbounded IN (...) clause exceeding
        // Cosmos DB's 524,288-character query size limit (error SC3020).
        foreach (var batch in obsoleteChannels.Chunk(CLEANUP_BATCH_SIZE))
        {
            cancellationToken.ThrowIfCancellationRequested();
            await channelManager.RemoveChannelsAsync(batch, cancellationToken);
        }
    }
    #endregion

    /// <summary>
    /// Get custom result measures.
    /// </summary>
    /// <returns>A Measures</returns>
    protected override Measures GetCustomResultMeasures()
    {
        var measures = base.GetCustomResultMeasures();
        measures[ContentManagerObservabilityExtensions.CONNECTOR_COUNT] = _connectorConfigurations?.Count ?? 0;
        measures[ContentManagerObservabilityExtensions.CHANNEL_DISCOVERY_OPERATIONS_STARTED_COUNT] = _channelDiscoveryOperationsStarted;
        return measures;
    }

    private async Task InvokeContentManagerCallbackAsync(List<ConnectorConfigModel> connectorConfigurations, CancellationToken cancellationToken)
    {
        //Do not invoke if we have not found any new configurations
        if (connectorConfigurations.Count == 0)
            return;

        using var scope = serviceProvider.CreateScope();
        var contentManagerCallbackAction = contentManagerActionProvider.CreateContentManagerCallbackAction(scope);

        //If no callback action has been registered, just bail out now
        if (contentManagerCallbackAction == null)
            return;

        await contentManagerCallbackAction
            .ExecuteAsync(connectorConfigurations, cancellationToken)
            .ConfigureAwait(false);
    }
}
