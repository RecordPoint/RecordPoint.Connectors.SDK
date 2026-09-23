using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Abstractions.ContentManager;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Providers;
using System;
using System.Text.Json;

namespace RecordPoint.Connectors.SDK.Work;

/// <summary>
/// Implemtation of a Managed Work Factory
/// </summary>
public class ManagedWorkFactory(
    IOptions<ConnectorOptions> connectorOptions,
    IDateTimeProvider dateTimeProvider,
    IWorkQueueClient workQueueClient) : IManagedWorkFactory
{
    /// <inheritdoc/>
    public virtual IManagedWorkManager CreateWork(ConnectorConfigModel connectorConfig, string workStatusId, string workType, string configurationType, string configuration)
    {
        var managedWorkManager = new ManagedWorkManager(workQueueClient, dateTimeProvider);
        managedWorkManager.WorkStatus.Id = workStatusId;
        managedWorkManager.WorkStatus.WorkId = Guid.NewGuid().ToString();
        managedWorkManager.WorkStatus.WorkType = workType;

        managedWorkManager.WorkStatus.ConnectorId = connectorConfig.Id;
        managedWorkManager.WorkStatus.TenantId = connectorConfig.TenantId;
        managedWorkManager.WorkStatus.TenantDomainName = connectorConfig.TenantDomainName;

        managedWorkManager.WorkStatus.ConfigurationType = configurationType;
        managedWorkManager.WorkStatus.Configuration = configuration;
        managedWorkManager.WorkStatus.StateType = string.Empty;
        managedWorkManager.WorkStatus.State = string.Empty;
        
        managedWorkManager.WorkStatus.WorkInitiatedDate = dateTimeProvider.UtcNow;
        managedWorkManager.WorkStatus.WorkRequestDate = dateTimeProvider.UtcNow;

        managedWorkManager.WorkStatus.RetryOnFailure = connectorOptions?.Value.RetryOnFailure ?? true;
        managedWorkManager.WorkStatus.MaxRetries = connectorOptions?.Value.MaxRetries ?? 5;
        managedWorkManager.WorkStatus.MaxRetryDelay = connectorOptions?.Value.MaxRetryDelay ?? 3600;
        managedWorkManager.WorkStatus.RetryDelay = connectorOptions?.Value.RetryDelay ?? 30;
        managedWorkManager.WorkStatus.ExponentialRetryDelay = connectorOptions?.Value.ExponentialRetryDelay ?? true;

        return managedWorkManager;
    }

    /// <inheritdoc/>
    public virtual IManagedWorkManager LoadWork(WorkRequest workRequest, ManagedWorkStatusModel workStatus)
    {
        var managedWorkManager = new ManagedWorkManager(workQueueClient, dateTimeProvider);

        if (string.IsNullOrEmpty(workRequest.ConnectorConfigId))
            MigrateWorkRequest(workRequest, workStatus);

        workStatus.CopyTo(managedWorkManager.WorkStatus);

        managedWorkManager.WorkStatus.ConnectorId = workRequest.ConnectorConfigId;
        managedWorkManager.WorkStatus.TenantId = workRequest.TenantId;
        managedWorkManager.WorkStatus.TenantDomainName = workRequest.TenantDomainName;

        managedWorkManager.WorkStatus.RetryOnFailure = connectorOptions?.Value.RetryOnFailure ?? true;
        managedWorkManager.WorkStatus.MaxRetries = connectorOptions?.Value.MaxRetries ?? 5;
        managedWorkManager.WorkStatus.MaxRetryDelay = connectorOptions?.Value.MaxRetryDelay ?? 3600;
        managedWorkManager.WorkStatus.RetryDelay = connectorOptions?.Value.RetryDelay ?? 30;
        managedWorkManager.WorkStatus.ExponentialRetryDelay = connectorOptions?.Value.ExponentialRetryDelay ?? true;

        managedWorkManager.WorkStatus.WorkInitiatedDate ??= dateTimeProvider.UtcNow;

        return managedWorkManager;
    }

    [Obsolete("This method has been implemented for backwards compatibility and will be removed in future versions.")]
    private static void MigrateWorkRequest(WorkRequest workRequest, ManagedWorkStatusModel workStatus)
    {
        var workConfiguration = JsonSerializer.Deserialize<ContentSubmissionConfiguration>(workStatus.Configuration);
        if (string.IsNullOrEmpty(workConfiguration.ConnectorConfigurationId) || string.IsNullOrEmpty(workConfiguration.TenantId) || string.IsNullOrEmpty(workConfiguration.TenantDomainName))
            throw new RequiredValueNullException(nameof(workRequest.ConnectorConfigId));

        workRequest.ConnectorConfigId = workConfiguration.ConnectorConfigurationId;
        workRequest.TenantId = workConfiguration.TenantId;
        workRequest.TenantDomainName = workConfiguration.TenantDomainName;
    }
}
