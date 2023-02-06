using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentReport;
using RecordPoint.Connectors.SDK.Work;
using System.Text.Json;


namespace RecordPoint.Connectors.SDK.ContentManager
{
    public static class ContentWorkQueueExtensions
    {

        /// <summary>
        /// Submit a content discovery operation to the work queue
        /// </summary>
        /// <param name="workQueueClient">Work queue to submit aggregation to</param>
        /// <param name="contentSubmissionConfiguration">Configuration information</param>
        /// <param name="record">aggregation</param>
        /// <param name="waitTill"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Submission task</returns>
        public static Task GenerateReportAsync(this IWorkQueueClient workQueueClient, ContentSubmissionConfiguration contentSubmissionConfiguration, Record record, DateTimeOffset? waitTill, CancellationToken cancellationToken)
        {
            var workRequest = new WorkRequest
            {
                WorkId = Guid.NewGuid().ToString(),
                WorkType = GenerateReportOperation.WORK_TYPE,
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
