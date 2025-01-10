using Microsoft.Extensions.DependencyInjection;
using System;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// The content manager action provider.
    /// </summary>
    public class ContentManagerActionProvider : IContentManagerActionProvider
    {
        /// <summary>
        /// The service provider.
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentManagerActionProvider"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public ContentManagerActionProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc/>
        public IContentManagerCallbackAction CreateContentManagerCallbackAction() => _serviceProvider.GetService<IContentManagerCallbackAction>();

        /// <inheritdoc/>
        public IChannelDiscoveryAction CreateChannelDiscoveryAction() => _serviceProvider.GetRequiredService<IChannelDiscoveryAction>();

        /// <inheritdoc/>
        public IContentRegistrationAction CreateContentRegistrationAction() => _serviceProvider.GetRequiredService<IContentRegistrationAction>();

        /// <inheritdoc/>
        public IContentSynchronisationAction CreateContentSynchronisationAction() => _serviceProvider.GetRequiredService<IContentSynchronisationAction>();

        /// <inheritdoc/>
        public IBinaryRetrievalAction CreateBinaryRetrievalAction() => _serviceProvider.GetRequiredService<IBinaryRetrievalAction>();

        /// <inheritdoc/>
        public IAggregationSubmissionCallbackAction CreateAggregationSubmissionCallbackAction() => _serviceProvider.GetService<IAggregationSubmissionCallbackAction>();

        /// <inheritdoc/>
        public IAuditEventSubmissionCallbackAction CreateAuditEventSubmissionCallbackAction() => _serviceProvider.GetService<IAuditEventSubmissionCallbackAction>();

        /// <inheritdoc/>
        public IRecordSubmissionCallbackAction CreateRecordSubmissionCallbackAction() => _serviceProvider.GetService<IRecordSubmissionCallbackAction>();

        /// <inheritdoc/>
        public IBinarySubmissionCallbackAction CreateBinarySubmissionCallbackAction() => _serviceProvider.GetService<IBinarySubmissionCallbackAction>();

        /// <inheritdoc/>
        public IRecordDisposalAction CreateRecordDisposalAction() => _serviceProvider.GetRequiredService<IRecordDisposalAction>();

        /// <inheritdoc/>
        public IGenerateReportAction CreateGenerationReportAction() => _serviceProvider.GetRequiredService<IGenerateReportAction>();

        /// <inheritdoc/>
        public IGenericAction<TInput, TOutput> CreateGenericAction<TInput, TOutput>() where TOutput : ActionResultBase =>
            _serviceProvider.GetRequiredService<IGenericAction<TInput, TOutput>>();

        /// <inheritdoc/>
        public IGenericManagedAction<TInput, TOutput> CreateGenericManagedAction<TInput, TOutput>() where TOutput : ActionResultBase =>
            _serviceProvider.GetRequiredService<IGenericManagedAction<TInput, TOutput>>();
    }
}
