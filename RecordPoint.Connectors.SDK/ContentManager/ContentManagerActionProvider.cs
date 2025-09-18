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
        public IContentManagerCallbackAction CreateContentManagerCallbackAction(IServiceScope scope = null) =>
            scope != null
                ? scope.ServiceProvider.GetService<IContentManagerCallbackAction>()
                : _serviceProvider.GetService<IContentManagerCallbackAction>();

        /// <inheritdoc/>
        public IChannelDiscoveryAction CreateChannelDiscoveryAction(IServiceScope scope = null) =>
            scope != null
                ? scope.ServiceProvider.GetService<IChannelDiscoveryAction>()
                : _serviceProvider.GetService<IChannelDiscoveryAction>();

        /// <inheritdoc/>
        public IContentRegistrationAction CreateContentRegistrationAction(IServiceScope scope = null) =>
            scope != null
                ? scope.ServiceProvider.GetService<IContentRegistrationAction>()
                : _serviceProvider.GetService<IContentRegistrationAction>();

        /// <inheritdoc/>
        public IContentSynchronisationAction CreateContentSynchronisationAction(IServiceScope scope = null) =>
            scope != null
                ? scope.ServiceProvider.GetService<IContentSynchronisationAction>()
                : _serviceProvider.GetService<IContentSynchronisationAction>();

        /// <inheritdoc/>
        public IBinaryRetrievalAction CreateBinaryRetrievalAction(IServiceScope scope = null) =>
            scope != null
                ? scope.ServiceProvider.GetService<IBinaryRetrievalAction>()
                : _serviceProvider.GetService<IBinaryRetrievalAction>();

        /// <inheritdoc/>
        public IAggregationSubmissionCallbackAction CreateAggregationSubmissionCallbackAction(IServiceScope scope = null) =>
            scope != null
                ? scope.ServiceProvider.GetService<IAggregationSubmissionCallbackAction>()
                : _serviceProvider.GetService<IAggregationSubmissionCallbackAction>();

        /// <inheritdoc/>
        public IAuditEventSubmissionCallbackAction CreateAuditEventSubmissionCallbackAction(IServiceScope scope = null) =>
            scope != null
                ? scope.ServiceProvider.GetService<IAuditEventSubmissionCallbackAction>()
                : _serviceProvider.GetService<IAuditEventSubmissionCallbackAction>();

        /// <inheritdoc/>
        public IRecordSubmissionCallbackAction CreateRecordSubmissionCallbackAction(IServiceScope scope = null) =>
            scope != null
                ? scope.ServiceProvider.GetService<IRecordSubmissionCallbackAction>()
                : _serviceProvider.GetService<IRecordSubmissionCallbackAction>();

        /// <inheritdoc/>
        public IBinarySubmissionCallbackAction CreateBinarySubmissionCallbackAction(IServiceScope scope = null) =>
            scope != null
                ? scope.ServiceProvider.GetService<IBinarySubmissionCallbackAction>()
                : _serviceProvider.GetService<IBinarySubmissionCallbackAction>();

        /// <inheritdoc/>
        public IRecordDisposalAction CreateRecordDisposalAction(IServiceScope scope = null) =>
            scope != null
                ? scope.ServiceProvider.GetService<IRecordDisposalAction>()
                : _serviceProvider.GetService<IRecordDisposalAction>();

        /// <inheritdoc/>
        public IGenericAction<TInput, TOutput> CreateGenericAction<TInput, TOutput>(IServiceScope scope = null) where TOutput : ActionResultBase =>
           scope != null
                ? scope.ServiceProvider.GetService<IGenericAction<TInput, TOutput>>()
                : _serviceProvider.GetService<IGenericAction<TInput, TOutput>>();

        /// <inheritdoc/>
        public IGenericManagedAction<TInput, TOutput> CreateGenericManagedAction<TInput, TOutput>(IServiceScope scope = null) where TOutput : ActionResultBase =>
            scope != null
                ? scope.ServiceProvider.GetService<IGenericManagedAction<TInput, TOutput>>()
                : _serviceProvider.GetService<IGenericManagedAction<TInput, TOutput>>();
    }
}
