using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.Work;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.ContentManager;

/// <summary>
/// Content work queue extensions
/// </summary>
public static class WorkQueueClientExtensions
{
    /// <summary>
    /// Submit a record submission operation to the work queue
    /// </summary>
    /// <param name="workQueueClient">Work queue to submit record to</param>
    /// <param name="connectorConfiguration">Configuration information</param>
    /// <param name="record"></param>
    /// <param name="waitTill"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Submission task</returns>
    public static Task SubmitRecordAsync(this IWorkQueueClient workQueueClient, ConnectorConfigModel connectorConfiguration, Record record, DateTimeOffset? waitTill, CancellationToken cancellationToken)
    {
        var workRequest = new WorkRequest
        {
            WorkId = Guid.NewGuid().ToString(),
            WorkType = SubmitRecordOperation.WORK_TYPE,
            Body = JsonSerializer.Serialize(record),
            ConnectorConfigId = connectorConfiguration.Id,
            TenantId = connectorConfiguration.TenantId,
            TenantDomainName = connectorConfiguration.TenantDomainName,
            WaitTill = waitTill
        };
        return workQueueClient.SubmitWorkAsync(workRequest, cancellationToken);
    }

    /// <summary>
    /// Submit a binary submission operation to the work queue
    /// </summary>
    /// <param name="workQueueClient">Work queue to submit record to</param>
    /// <param name="connectorConfiguration">Configuration information</param>
    /// <param name="binaryMetaInfo"></param>
    /// <param name="waitTill"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Submission task</returns>
    public static Task SubmitBinaryAsync(this IWorkQueueClient workQueueClient, ConnectorConfigModel connectorConfiguration, BinaryMetaInfo binaryMetaInfo, DateTimeOffset? waitTill, CancellationToken cancellationToken)
    {
        var workRequest = new WorkRequest
        {
            WorkId = Guid.NewGuid().ToString(),
            WorkType = SubmitBinaryOperation.WORK_TYPE,
            Body = JsonSerializer.Serialize(binaryMetaInfo),
            ConnectorConfigId = connectorConfiguration.Id,
            TenantId = connectorConfiguration.TenantId,
            TenantDomainName = connectorConfiguration.TenantDomainName,
            WaitTill = waitTill
        };
        return workQueueClient.SubmitWorkAsync(workRequest, cancellationToken);
    }

    /// <summary>
    /// Submit an aggregation submission operation to the work queue
    /// </summary>
    /// <param name="workQueueClient">Work queue to submit aggregation to</param>
    /// <param name="connectorConfiguration">Configuration information</param>
    /// <param name="aggregation">aggregation</param>
    /// <param name="waitTill"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Submission task</returns>
    public static Task SubmitAggregationAsync(this IWorkQueueClient workQueueClient, ConnectorConfigModel connectorConfiguration, Aggregation aggregation, DateTimeOffset? waitTill, CancellationToken cancellationToken)
    {
        var workRequest = new WorkRequest
        {
            WorkId = Guid.NewGuid().ToString(),
            WorkType = SubmitAggregationOperation.WORK_TYPE,
            Body = JsonSerializer.Serialize(aggregation),
            ConnectorConfigId = connectorConfiguration.Id,
            TenantId = connectorConfiguration.TenantId,
            TenantDomainName = connectorConfiguration.TenantDomainName,
            WaitTill = waitTill
        };
        return workQueueClient.SubmitWorkAsync(workRequest, cancellationToken);
    }

    /// <summary>
    /// Submit an audit event submission operation to the work queue
    /// </summary>
    /// <param name="workQueueClient">Work queue to submit audit event to</param>
    /// <param name="connectorConfiguration">Configuration information</param>
    /// <param name="auditEvent"></param>
    /// <param name="waitTill"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Submission task</returns>
    public static Task SubmitAuditEventAsync(this IWorkQueueClient workQueueClient, ConnectorConfigModel connectorConfiguration, AuditEvent auditEvent, DateTimeOffset? waitTill, CancellationToken cancellationToken)
    {
        var workRequest = new WorkRequest
        {
            WorkId = Guid.NewGuid().ToString(),
            WorkType = SubmitAuditEventOperation.WORK_TYPE,
            Body = JsonSerializer.Serialize(auditEvent),
            ConnectorConfigId = connectorConfiguration.Id,
            TenantId = connectorConfiguration.TenantId,
            TenantDomainName = connectorConfiguration.TenantDomainName,
            WaitTill = waitTill
        };
        return workQueueClient.SubmitWorkAsync(workRequest, cancellationToken);
    }

    /// <summary>
    /// Submit a record disposal operation to the work queue
    /// </summary>
    /// <param name="workQueueClient">Work queue to submit aggregation to</param>
    /// <param name="connectorConfiguration">Configuration information</param>
    /// <param name="record">aggregation</param>
    /// <param name="waitTill"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Submission task</returns>
    public static Task DisposeRecordAsync(this IWorkQueueClient workQueueClient, ConnectorConfigModel connectorConfiguration, Record record, DateTimeOffset? waitTill, CancellationToken cancellationToken)
    {
        var workRequest = new WorkRequest
        {
            WorkId = Guid.NewGuid().ToString(),
            WorkType = RecordDisposalOperation.WORK_TYPE,
            Body = JsonSerializer.Serialize(record),
            ConnectorConfigId = connectorConfiguration.Id,
            TenantId = connectorConfiguration.TenantId,
            TenantDomainName = connectorConfiguration.TenantDomainName,
            WaitTill = waitTill
        };
        return workQueueClient.SubmitWorkAsync(workRequest, cancellationToken);
    }
}