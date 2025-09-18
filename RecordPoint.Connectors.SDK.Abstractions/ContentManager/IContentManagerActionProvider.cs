using Microsoft.Extensions.DependencyInjection;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Contract for a Content Manager Action Provider
    /// </summary>
    public interface IContentManagerActionProvider
    {
        /// <summary>
        /// Create a new content manager callback action
        /// </summary>
        /// <returns>Content Manager Callback Action</returns>
        IContentManagerCallbackAction CreateContentManagerCallbackAction(IServiceScope? scope = null);

        /// <summary>
        /// Create a new Channel discovery action
        /// </summary>
        /// <returns>Channel scanner</returns>
        IChannelDiscoveryAction CreateChannelDiscoveryAction(IServiceScope? scope = null);

        /// <summary>
        /// Create a new content registration action
        /// </summary>
        /// <returns>Content Registration Action</returns>
        IContentRegistrationAction CreateContentRegistrationAction(IServiceScope? scope = null);

        /// <summary>
        /// Create a new content synchronisation action
        /// </summary>
        /// <returns>Content Synchronisation Action</returns>
        IContentSynchronisationAction CreateContentSynchronisationAction(IServiceScope? scope = null);

        /// <summary>
        /// Create a new binary retrieval action
        /// </summary>
        /// <returns>Binary Retrieval Action</returns>
        IBinaryRetrievalAction CreateBinaryRetrievalAction(IServiceScope? scope = null);

        /// <summary>
        /// Create a new aggregation submission callback action
        /// </summary>
        /// <returns>Aggregation Submission Callback Action</returns>
        IAggregationSubmissionCallbackAction CreateAggregationSubmissionCallbackAction(IServiceScope? scope = null);

        /// <summary>
        /// Create a new audit event submission callback action
        /// </summary>
        /// <returns></returns>
        IAuditEventSubmissionCallbackAction CreateAuditEventSubmissionCallbackAction(IServiceScope? scope = null);

        /// <summary>
        /// Create a new record submission callback action
        /// </summary>
        /// <returns>Record Submission Callback Action</returns>
        IRecordSubmissionCallbackAction CreateRecordSubmissionCallbackAction(IServiceScope? scope = null);

        /// <summary>
        /// Create a new binary submission callback action
        /// </summary>
        /// <returns>Binary Submission Callback Action</returns>
        IBinarySubmissionCallbackAction CreateBinarySubmissionCallbackAction(IServiceScope? scope = null);

        /// <summary>
        /// Create a new record disposal action
        /// </summary>
        /// <returns>Record Disposal Action</returns>
        IRecordDisposalAction CreateRecordDisposalAction(IServiceScope? scope = null);

        /// <summary>
        /// Create a new generic action with the provided templated input and output classes
        /// </summary>
        /// <returns>Generic Action</returns>
        IGenericAction<TInput, TOutput> CreateGenericAction<TInput, TOutput>(IServiceScope? scope = null) where TOutput : ActionResultBase;

        /// <summary>
        /// Create a new generic action with the provided templated input and output classes
        /// </summary>
        /// <returns>Generic Action</returns>
        IGenericManagedAction<TInput, TOutput> CreateGenericManagedAction<TInput, TOutput>(IServiceScope? scope = null) where TOutput : ActionResultBase;
    }
}