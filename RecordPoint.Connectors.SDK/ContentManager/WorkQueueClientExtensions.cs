using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.Work;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Content work queue extensions
    /// </summary>
    public static class WorkQueueClientExtensions
    {
        /// <summary>
        /// Submit a record submission operation to the work queue
        /// </summary>
        /// <param name="workQueueClient">Work queue to submit record to</param>
        /// <param name="contentSubmissionConfiguration">Configuration information</param>
        /// <param name="record"></param>
        /// <param name="waitTill"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Submission task</returns>
        public static Task SubmitRecordAsync(this IWorkQueueClient workQueueClient, ContentSubmissionConfiguration contentSubmissionConfiguration, Record record, DateTimeOffset? waitTill, CancellationToken cancellationToken)
        {
            var workRequest = new WorkRequest
            {
                WorkId = Guid.NewGuid().ToString(),
                WorkType = SubmitRecordOperation.WORK_TYPE,
                Body = JsonSerializer.Serialize(record),
                ConnectorConfigId = contentSubmissionConfiguration?.ConnectorConfigurationId,
                TenantId = contentSubmissionConfiguration?.TenantId,
                TenantDomainName = contentSubmissionConfiguration?.TenantDomainName,
                WaitTill = waitTill
            };
            return workQueueClient.SubmitWorkAsync(workRequest, cancellationToken);
        }

        /// <summary>
        /// Submit a binary submission operation to the work queue
        /// </summary>
        /// <param name="workQueueClient">Work queue to submit record to</param>
        /// <param name="contentSubmissionConfiguration">Configuration information</param>
        /// <param name="binaryMetaInfo"></param>
        /// <param name="waitTill"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Submission task</returns>
        public static Task SubmitBinaryAsync(this IWorkQueueClient workQueueClient, ContentSubmissionConfiguration contentSubmissionConfiguration, BinaryMetaInfo binaryMetaInfo, DateTimeOffset? waitTill, CancellationToken cancellationToken)
        {
            var workRequest = new WorkRequest
            {
                WorkId = Guid.NewGuid().ToString(),
                WorkType = SubmitBinaryOperation.WORK_TYPE,
                Body = JsonSerializer.Serialize(binaryMetaInfo),
                ConnectorConfigId = contentSubmissionConfiguration?.ConnectorConfigurationId,
                TenantId = contentSubmissionConfiguration?.TenantId,
                TenantDomainName = contentSubmissionConfiguration?.TenantDomainName,
                WaitTill = waitTill
            };
            return workQueueClient.SubmitWorkAsync(workRequest, cancellationToken);
        }

        /// <summary>
        /// Submit an aggregation submission operation to the work queue
        /// </summary>
        /// <param name="workQueueClient">Work queue to submit aggregation to</param>
        /// <param name="contentSubmissionConfiguration">Configuration information</param>
        /// <param name="aggregation">aggregation</param>
        /// <param name="waitTill"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Submission task</returns>
        public static Task SubmitAggregationAsync(this IWorkQueueClient workQueueClient, ContentSubmissionConfiguration contentSubmissionConfiguration, Aggregation aggregation, DateTimeOffset? waitTill, CancellationToken cancellationToken)
        {
            var workRequest = new WorkRequest
            {
                WorkId = Guid.NewGuid().ToString(),
                WorkType = SubmitAggregationOperation.WORK_TYPE,
                Body = JsonSerializer.Serialize(aggregation),
                ConnectorConfigId = contentSubmissionConfiguration?.ConnectorConfigurationId,
                TenantId = contentSubmissionConfiguration?.TenantId,
                TenantDomainName = contentSubmissionConfiguration?.TenantDomainName,
                WaitTill = waitTill
            };
            return workQueueClient.SubmitWorkAsync(workRequest, cancellationToken);
        }

        /// <summary>
        /// Submit an audit event submission operation to the work queue
        /// </summary>
        /// <param name="workQueueClient">Work queue to submit audit event to</param>
        /// <param name="contentSubmissionConfiguration">Configuration information</param>
        /// <param name="auditEvent"></param>
        /// <param name="waitTill"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Submission task</returns>
        public static Task SubmitAuditEventAsync(this IWorkQueueClient workQueueClient, ContentSubmissionConfiguration contentSubmissionConfiguration, AuditEvent auditEvent, DateTimeOffset? waitTill, CancellationToken cancellationToken)
        {
            var workRequest = new WorkRequest
            {
                WorkId = Guid.NewGuid().ToString(),
                WorkType = SubmitAuditEventOperation.WORK_TYPE,
                Body = JsonSerializer.Serialize(auditEvent),
                ConnectorConfigId = contentSubmissionConfiguration?.ConnectorConfigurationId,
                TenantId = contentSubmissionConfiguration?.TenantId,
                TenantDomainName = contentSubmissionConfiguration?.TenantDomainName,
                WaitTill = waitTill
            };
            return workQueueClient.SubmitWorkAsync(workRequest, cancellationToken);
        }

        /// <summary>
        /// Submit a record disposal operation to the work queue
        /// </summary>
        /// <param name="workQueueClient">Work queue to submit aggregation to</param>
        /// <param name="contentSubmissionConfiguration">Configuration information</param>
        /// <param name="record">aggregation</param>
        /// <param name="waitTill"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Submission task</returns>
        public static Task DisposeRecordAsync(this IWorkQueueClient workQueueClient, ContentSubmissionConfiguration contentSubmissionConfiguration, Record record, DateTimeOffset? waitTill, CancellationToken cancellationToken)
        {
            var workRequest = new WorkRequest
            {
                WorkId = Guid.NewGuid().ToString(),
                WorkType = RecordDisposalOperation.WORK_TYPE,
                Body = JsonSerializer.Serialize(record),
                ConnectorConfigId = contentSubmissionConfiguration?.ConnectorConfigurationId,
                TenantId = contentSubmissionConfiguration?.TenantId,
                TenantDomainName = contentSubmissionConfiguration?.TenantDomainName,
                WaitTill = waitTill
            };
            return workQueueClient.SubmitWorkAsync(workRequest, cancellationToken);
        }
    }
}