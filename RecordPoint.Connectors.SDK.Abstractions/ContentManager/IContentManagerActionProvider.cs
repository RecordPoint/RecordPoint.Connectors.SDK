namespace RecordPoint.Connectors.SDK.ContentManager
{
    public interface IContentManagerActionProvider
    {
        /// <summary>
        /// Create a new Channel discovery action
        /// </summary>
        /// <returns>Channel scanner</returns>
        IChannelDiscoveryAction CreateChannelDiscoveryAction();

        /// <summary>
        /// Create a new content registration action
        /// </summary>
        /// <returns>Content Registration Action</returns>
        IContentRegistrationAction CreateContentRegistrationAction();

        /// <summary>
        /// Create a new content synchronisation action
        /// </summary>
        /// <returns>Content Synchronisation Action</returns>
        IContentSynchronisationAction CreateContentSynchronisationAction();

        /// <summary>
        /// Create a new binary retrieval action
        /// </summary>
        /// <returns>Binary Retrieval Action</returns>
        IBinaryRetrievalAction CreateBinaryRetrievalAction();

        /// <summary>
        /// Create a new aggregation submission callback action
        /// </summary>
        /// <returns>Aggregation Submission Callback Action</returns>
        IAggregationSubmissionCallbackAction CreateAggregationSubmissionCallbackAction();

        /// <summary>
        /// Create a new audit event submission callback action
        /// </summary>
        /// <returns></returns>
        IAuditEventSubmissionCallbackAction CreateAuditEventSubmissionCallbackAction();

        /// <summary>
        /// Create a new record submission callback action
        /// </summary>
        /// <returns>Record Submission Callback Action</returns>
        IRecordSubmissionCallbackAction CreateRecordSubmissionCallbackAction();

        /// <summary>
        /// Create a new binary submission callback action
        /// </summary>
        /// <returns>Binary Submission Callback Action</returns>
        IBinarySubmissionCallbackAction CreateBinarySubmissionCallbackAction();

        /// <summary>
        /// Create a new record disposal action
        /// </summary>
        /// <returns>Record Disposal Action</returns>
        IRecordDisposalAction CreateRecordDisposalAction();

        /// <summary>
        /// Create a new report generation action
        /// </summary>
        /// <returns>Generate Report Action</returns>
        IGenerateReportAction CreateGenerationReportAction();
    }
}