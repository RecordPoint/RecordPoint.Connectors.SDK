using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Connectors;
using System;

namespace RecordPoint.Connectors.SDK.Work;

/// <summary>
/// Implemtation of a Managed Work Factory
/// </summary>
public class ManagedWorkFactory(
    IOptions<ConnectorOptions> connectorOptions,
    IManagedWorkStatusManager managedWorkStatusManager,
    IWorkQueueClient workQueueClient) : IManagedWorkFactory
{
    /// <inheritdoc/>
    public IManagedWorkManager CreateWork(string workStatusId, string workType, string connectorId, string configurationType, string configuration)
    {
        var managedWorkManager = new ManagedWorkManager(managedWorkStatusManager, workQueueClient);
        managedWorkManager.WorkStatus.Id = workStatusId;
        managedWorkManager.WorkStatus.WorkType = workType;
        managedWorkManager.WorkStatus.ConnectorId = connectorId;
        managedWorkManager.WorkStatus.ConfigurationType = configurationType;
        managedWorkManager.WorkStatus.Configuration = configuration;
        managedWorkManager.WorkStatus.State = string.Empty;
        managedWorkManager.WorkStatus.WorkId = Guid.NewGuid().ToString();
        managedWorkManager.WorkStatus.RetryOnFailure = connectorOptions?.Value.RetryOnFailure ?? true;
        managedWorkManager.WorkStatus.MaxRetries = connectorOptions?.Value.MaxRetries ?? 5;
        managedWorkManager.WorkStatus.MaxRetryDelay = connectorOptions?.Value.MaxRetryDelay ?? 3600;
        managedWorkManager.WorkStatus.RetryDelay = connectorOptions?.Value.RetryDelay ?? 30;
        managedWorkManager.WorkStatus.ExponentialRetryDelay = connectorOptions?.Value.ExponentialRetryDelay ?? true;
        return managedWorkManager;
    }

    /// <inheritdoc/>
    public IManagedWorkManager LoadWork(ManagedWorkStatusModel workStatus)
    {
        var managedWorkManager = new ManagedWorkManager(managedWorkStatusManager, workQueueClient);
        workStatus.RetryOnFailure = connectorOptions?.Value.RetryOnFailure ?? true;
        workStatus.MaxRetries = connectorOptions?.Value.MaxRetries ?? 5;
        workStatus.RetryDelay = connectorOptions?.Value.RetryDelay ?? 30;
        workStatus.ExponentialRetryDelay = connectorOptions?.Value.ExponentialRetryDelay ?? true;
        managedWorkManager.WorkStatus.MaxRetryDelay = connectorOptions?.Value.MaxRetryDelay ?? 3600;
        workStatus.CopyTo(managedWorkManager.WorkStatus);
        return managedWorkManager;
    }

}
