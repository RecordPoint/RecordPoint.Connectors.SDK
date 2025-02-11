﻿using RecordPoint.Connectors.SDK.ContentManager;

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


        public IContentManagerCallbackAction CreateContentManagerCallbackAction() => ContentManagerCallbackActionFactory();

        public IChannelDiscoveryAction CreateChannelDiscoveryAction() => ChannelDiscoveryActionFactory();

        public IContentRegistrationAction CreateContentRegistrationAction() => ContentRegistrationActionFactory();

        public IContentSynchronisationAction CreateContentSynchronisationAction() => ContentSynchronisationActionFactory();

        public IBinaryRetrievalAction CreateBinaryRetrievalAction() => BinaryRetrievalActionFactory();

        public IAggregationSubmissionCallbackAction CreateAggregationSubmissionCallbackAction() => AggregationSubmissionCallbackActionFactory();

        public IAuditEventSubmissionCallbackAction CreateAuditEventSubmissionCallbackAction() => AuditEventSubmissionCallbackActionFactory();

        public IRecordSubmissionCallbackAction CreateRecordSubmissionCallbackAction() => RecordSubmissionCallbackActionFactory();

        public IBinarySubmissionCallbackAction CreateBinarySubmissionCallbackAction() => BinarySubmissionCallbackActionFactory();

        public IRecordDisposalAction CreateRecordDisposalAction() => RecordDisposalActionFactory();

        public IGenerateReportAction CreateGenerationReportAction()
        {
            throw new NotImplementedException();
        }
        public IGenericAction<TInput, TOutput> CreateGenericAction<TInput, TOutput>() where TOutput : ActionResultBase
        {
            throw new NotImplementedException();
        }

        public IGenericManagedAction<TInput, TOutput> CreateGenericManagedAction<TInput, TOutput>() where TOutput : ActionResultBase
        {
            throw new NotImplementedException();
        }
    }
}
