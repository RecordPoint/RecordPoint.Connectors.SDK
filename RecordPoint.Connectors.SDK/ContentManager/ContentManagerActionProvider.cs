using Microsoft.Extensions.DependencyInjection;
using System;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    public class ContentManagerActionProvider : IContentManagerActionProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public ContentManagerActionProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

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
    }
}
