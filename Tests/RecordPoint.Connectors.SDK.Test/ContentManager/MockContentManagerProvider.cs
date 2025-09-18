using Microsoft.Extensions.DependencyInjection;
using RecordPoint.Connectors.SDK.ContentManager;

namespace RecordPoint.Connectors.SDK.Test.ContentManager
{
    public class MockContentManagerProvider : IContentManagerActionProvider
    {
        public MockContentManagerProvider(
            Func<IContentManagerCallbackAction> contentManagerCallbackActionFactory,
            Func<IChannelDiscoveryAction> channelDiscoveryFactory,
            Func<IContentSynchronisationAction> contentSynchronisationFactory,
            Func<IContentRegistrationAction> contentRegistrationFactory,
            Func<IBinaryRetrievalAction> binaryRetrievalFactory,
            Func<IAggregationSubmissionCallbackAction> aggregationSubmissionCallbackFactory,
            Func<IAuditEventSubmissionCallbackAction> auditEventSubmissionCallbackFactory,
            Func<IRecordSubmissionCallbackAction> recordSubmissionCallbackFactory,
            Func<IBinarySubmissionCallbackAction> binarySubmissionCallbackFactory,
            Func<IRecordDisposalAction> recordDisposalFactory
            )
        {
            ContentManagerCallbackActionFactory = contentManagerCallbackActionFactory;
            ContentSynchronisationActionFactory = contentSynchronisationFactory;
            ContentRegistrationActionFactory = contentRegistrationFactory;
            BinaryRetrievalActionFactory = binaryRetrievalFactory;
            AggregationSubmissionCallbackActionFactory = aggregationSubmissionCallbackFactory;
            AuditEventSubmissionCallbackActionFactory = auditEventSubmissionCallbackFactory;
            RecordSubmissionCallbackActionFactory = recordSubmissionCallbackFactory;
            BinarySubmissionCallbackActionFactory = binarySubmissionCallbackFactory;
            ChannelDiscoveryActionFactory = channelDiscoveryFactory;
            RecordDisposalActionFactory = recordDisposalFactory;
        }

        public Func<IContentManagerCallbackAction> ContentManagerCallbackActionFactory { get; set; }

        public Func<IContentSynchronisationAction> ContentSynchronisationActionFactory { get; set; }

        public Func<IContentRegistrationAction> ContentRegistrationActionFactory { get; set; }

        public Func<IBinaryRetrievalAction> BinaryRetrievalActionFactory { get; set; }

        public Func<IAggregationSubmissionCallbackAction> AggregationSubmissionCallbackActionFactory { get; set; }

        public Func<IAuditEventSubmissionCallbackAction> AuditEventSubmissionCallbackActionFactory { get; set; }

        public Func<IRecordSubmissionCallbackAction> RecordSubmissionCallbackActionFactory { get; set; }

        public Func<IBinarySubmissionCallbackAction> BinarySubmissionCallbackActionFactory { get; set; }

        public Func<IChannelDiscoveryAction> ChannelDiscoveryActionFactory { get; set; }

        public Func<IRecordDisposalAction> RecordDisposalActionFactory { get; set; }


        public IContentManagerCallbackAction CreateContentManagerCallbackAction(IServiceScope scope = null) => ContentManagerCallbackActionFactory();

        public IChannelDiscoveryAction CreateChannelDiscoveryAction(IServiceScope scope = null) => ChannelDiscoveryActionFactory();

        public IContentRegistrationAction CreateContentRegistrationAction(IServiceScope scope = null) => ContentRegistrationActionFactory();

        public IContentSynchronisationAction CreateContentSynchronisationAction(IServiceScope scope = null) => ContentSynchronisationActionFactory();

        public IBinaryRetrievalAction CreateBinaryRetrievalAction(IServiceScope scope = null) => BinaryRetrievalActionFactory();

        public IAggregationSubmissionCallbackAction CreateAggregationSubmissionCallbackAction(IServiceScope scope = null) => AggregationSubmissionCallbackActionFactory();

        public IAuditEventSubmissionCallbackAction CreateAuditEventSubmissionCallbackAction(IServiceScope scope = null) => AuditEventSubmissionCallbackActionFactory();

        public IRecordSubmissionCallbackAction CreateRecordSubmissionCallbackAction(IServiceScope scope = null) => RecordSubmissionCallbackActionFactory();

        public IBinarySubmissionCallbackAction CreateBinarySubmissionCallbackAction(IServiceScope scope = null) => BinarySubmissionCallbackActionFactory();

        public IRecordDisposalAction CreateRecordDisposalAction(IServiceScope scope = null) => RecordDisposalActionFactory();

        public IGenericAction<TInput, TOutput> CreateGenericAction<TInput, TOutput>(IServiceScope scope = null) 
            where TOutput : ActionResultBase
        {
            throw new NotImplementedException();
        }

        public IGenericManagedAction<TInput, TOutput> CreateGenericManagedAction<TInput, TOutput>(IServiceScope scope = null) 
            where TOutput : ActionResultBase
        {
            throw new NotImplementedException();
        }
    }
}
