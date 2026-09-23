using Microsoft.Extensions.DependencyInjection;
using System;

namespace RecordPoint.Connectors.SDK.ContentManager;

/// <summary>
/// The content manager action provider.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ContentManagerActionProvider"/> class.
/// </remarks>
/// <param name="serviceProvider">The service provider.</param>
public class ContentManagerActionProvider(IServiceProvider serviceProvider) : IContentManagerActionProvider
{

    /// <inheritdoc/>
    public IContentManagerCallbackAction CreateContentManagerCallbackAction(IServiceScope scope = null) =>
        scope != null
            ? scope.ServiceProvider.GetService<IContentManagerCallbackAction>()
            : serviceProvider.GetService<IContentManagerCallbackAction>();

    /// <inheritdoc/>
    public IChannelDiscoveryAction CreateChannelDiscoveryAction(IServiceScope scope = null) =>
        scope != null
            ? scope.ServiceProvider.GetService<IChannelDiscoveryAction>()
            : serviceProvider.GetService<IChannelDiscoveryAction>();

    /// <inheritdoc/>
    public IContentRegistrationAction CreateContentRegistrationAction(IServiceScope scope = null) =>
        scope != null
            ? scope.ServiceProvider.GetService<IContentRegistrationAction>()
            : serviceProvider.GetService<IContentRegistrationAction>();

    /// <inheritdoc/>
    public IContentSynchronisationAction CreateContentSynchronisationAction(IServiceScope scope = null) =>
        scope != null
            ? scope.ServiceProvider.GetService<IContentSynchronisationAction>()
            : serviceProvider.GetService<IContentSynchronisationAction>();

    /// <inheritdoc/>
    public IBinaryRetrievalAction CreateBinaryRetrievalAction(IServiceScope scope = null) =>
        scope != null
            ? scope.ServiceProvider.GetService<IBinaryRetrievalAction>()
            : serviceProvider.GetService<IBinaryRetrievalAction>();

    /// <inheritdoc/>
    public IAggregationSubmissionCallbackAction CreateAggregationSubmissionCallbackAction(IServiceScope scope = null) =>
        scope != null
            ? scope.ServiceProvider.GetService<IAggregationSubmissionCallbackAction>()
            : serviceProvider.GetService<IAggregationSubmissionCallbackAction>();

    /// <inheritdoc/>
    public IAuditEventSubmissionCallbackAction CreateAuditEventSubmissionCallbackAction(IServiceScope scope = null) =>
        scope != null
            ? scope.ServiceProvider.GetService<IAuditEventSubmissionCallbackAction>()
            : serviceProvider.GetService<IAuditEventSubmissionCallbackAction>();

    /// <inheritdoc/>
    public IRecordSubmissionCallbackAction CreateRecordSubmissionCallbackAction(IServiceScope scope = null) =>
        scope != null
            ? scope.ServiceProvider.GetService<IRecordSubmissionCallbackAction>()
            : serviceProvider.GetService<IRecordSubmissionCallbackAction>();

    /// <inheritdoc/>
    public IBinarySubmissionCallbackAction CreateBinarySubmissionCallbackAction(IServiceScope scope = null) =>
        scope != null
            ? scope.ServiceProvider.GetService<IBinarySubmissionCallbackAction>()
            : serviceProvider.GetService<IBinarySubmissionCallbackAction>();

    /// <inheritdoc/>
    public IRecordDisposalAction CreateRecordDisposalAction(IServiceScope scope = null) =>
        scope != null
            ? scope.ServiceProvider.GetService<IRecordDisposalAction>()
            : serviceProvider.GetService<IRecordDisposalAction>();

    /// <inheritdoc/>
    public IGenericAction<TInput, TOutput> CreateGenericAction<TInput, TOutput>(IServiceScope scope = null) where TOutput : ActionResultBase =>
       scope != null
            ? scope.ServiceProvider.GetService<IGenericAction<TInput, TOutput>>()
            : serviceProvider.GetService<IGenericAction<TInput, TOutput>>();

    /// <inheritdoc/>
    public IGenericManagedAction<TInput, TOutput> CreateGenericManagedAction<TInput, TOutput>(IServiceScope scope = null) where TOutput : ActionResultBase =>
        scope != null
            ? scope.ServiceProvider.GetService<IGenericManagedAction<TInput, TOutput>>()
            : serviceProvider.GetService<IGenericManagedAction<TInput, TOutput>>();
}
