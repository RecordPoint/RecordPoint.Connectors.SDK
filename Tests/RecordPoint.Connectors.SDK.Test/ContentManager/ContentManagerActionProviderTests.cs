#nullable enable
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RecordPoint.Connectors.SDK.ContentManager;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.ContentManager
{
    public class ContentManagerActionProviderTests
    {
        private static ServiceProvider BuildProvider(Action<IServiceCollection> configure)
        {
            var services = new ServiceCollection();
            configure(services);
            return services.BuildServiceProvider();
        }

        [Fact]
        public void CreateActions_ResolveFromRootProvider()
        {
            var channelDiscovery = Moq.Mock.Of<IChannelDiscoveryAction>();
            var recordCallback = Moq.Mock.Of<IRecordSubmissionCallbackAction>();
            var provider = BuildProvider(s =>
            {
                s.AddSingleton(channelDiscovery);
                s.AddSingleton(recordCallback);
            });

            var sut = new ContentManagerActionProvider(provider);

            Assert.Same(channelDiscovery, sut.CreateChannelDiscoveryAction());
            Assert.Same(recordCallback, sut.CreateRecordSubmissionCallbackAction());
        }

        [Fact]
        public void CreateActions_ReturnNull_WhenNotRegistered()
        {
            var provider = BuildProvider(_ => { });
            var sut = new ContentManagerActionProvider(provider);

            Assert.Null(sut.CreateContentManagerCallbackAction());
            Assert.Null(sut.CreateContentRegistrationAction());
            Assert.Null(sut.CreateContentSynchronisationAction());
            Assert.Null(sut.CreateBinaryRetrievalAction());
            Assert.Null(sut.CreateAggregationSubmissionCallbackAction());
            Assert.Null(sut.CreateAuditEventSubmissionCallbackAction());
            Assert.Null(sut.CreateRecordSubmissionCallbackAction());
            Assert.Null(sut.CreateBinarySubmissionCallbackAction());
            Assert.Null(sut.CreateRecordDisposalAction());
            Assert.Null(sut.CreateChannelDiscoveryAction());
        }

        [Fact]
        public void CreateActions_ResolveFromScope_WhenScopeProvided()
        {
            var scopedAction = Moq.Mock.Of<IBinaryRetrievalAction>();
            var provider = BuildProvider(s => s.AddScoped(_ => scopedAction));
            var sut = new ContentManagerActionProvider(provider);

            using var scope = provider.CreateScope();
            Assert.Same(scopedAction, sut.CreateBinaryRetrievalAction(scope));
        }
    }
}
