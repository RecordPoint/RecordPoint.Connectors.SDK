#nullable enable
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Abstractions.ContentManager;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Work;
using Xunit;
using Record = RecordPoint.Connectors.SDK.Content.Record;

namespace RecordPoint.Connectors.SDK.Test.ContentManager
{
    /// <summary>
    /// Tests for the ContentManagerBuilderExtensions DI registration methods.
    /// </summary>
    [Collection("EnvironmentVariable")]
    public class ContentManagerBuilderExtensionsTests
    {
        private static IServiceCollection BuildAndCapture(Action<IHostBuilder> apply)
        {
            IServiceCollection? captured = null;
            var builder = Host.CreateDefaultBuilder();
            apply(builder);
            builder.ConfigureServices(s => captured = s);
            using var host = builder.Build();
            Assert.NotNull(captured);
            return captured!;
        }

        private static bool HasService<TService>(IServiceCollection services) =>
            services.Any(d => d.ServiceType == typeof(TService));

        private static bool HasImplementation<TImpl>(IServiceCollection services) =>
            services.Any(d => d.ImplementationType == typeof(TImpl));

        [Fact]
        public void UseContentManagerService_RegistersOperationAndProvider()
        {
            var services = BuildAndCapture(b => b.UseContentManagerService());
            Assert.True(HasService<IContentManagerActionProvider>(services));
            Assert.True(HasService<IHostedService>(services));
        }

        [Fact]
        public void UseContentManagerService_WithCallback_RegistersCallbackAction()
        {
            var services = BuildAndCapture(b => b.UseContentManagerService<StubContentManagerCallbackAction>());
            Assert.True(HasService<IContentManagerCallbackAction>(services));
        }

        [Fact]
        public void UseChannelDiscoveryOperation_RegistersActionAndOperation()
        {
            var services = BuildAndCapture(b => b.UseChannelDiscoveryOperation<StubChannelDiscoveryAction>());
            Assert.True(HasService<IChannelDiscoveryAction>(services));
            Assert.True(HasService<IQueueableWork>(services));
            Assert.True(HasService<IContentManagerActionProvider>(services));
        }

        [Fact]
        public void UseContentSynchronisationOperation_RegistersAction()
        {
            var services = BuildAndCapture(b => b.UseContentSynchronisationOperation<StubContentSynchronisationAction>());
            Assert.True(HasService<IContentSynchronisationAction>(services));
            Assert.True(HasService<IQueueableWork>(services));
        }

        [Fact]
        public void UseContentRegistrationOperation_RegistersAction()
        {
            var services = BuildAndCapture(b => b.UseContentRegistrationOperation<StubContentRegistrationAction>());
            Assert.True(HasService<IContentRegistrationAction>(services));
            Assert.True(HasService<IQueueableWork>(services));
        }

        [Fact]
        public void UseRecordSubmissionOperation_RegistersOperation()
        {
            var services = BuildAndCapture(b => b.UseRecordSubmissionOperation());
            Assert.True(HasImplementation<SubmitRecordOperation>(services));
        }

        [Fact]
        public void UseRecordSubmissionOperation_WithCallback_RegistersCallback()
        {
            var services = BuildAndCapture(b => b.UseRecordSubmissionOperation<StubRecordSubmissionCallbackAction>());
            Assert.True(HasService<IRecordSubmissionCallbackAction>(services));
        }

        [Fact]
        public void UseBinarySubmissionOperation_RegistersRetrievalAction()
        {
            var services = BuildAndCapture(b => b.UseBinarySubmissionOperation<StubBinaryRetrievalAction>());
            Assert.True(HasService<IBinaryRetrievalAction>(services));
            Assert.True(HasImplementation<SubmitBinaryOperation>(services));
        }

        [Fact]
        public void UseBinarySubmissionOperation_WithCallback_RegistersBoth()
        {
            var services = BuildAndCapture(b =>
                b.UseBinarySubmissionOperation<StubBinaryRetrievalAction, StubBinarySubmissionCallbackAction>());
            Assert.True(HasService<IBinaryRetrievalAction>(services));
            Assert.True(HasService<IBinarySubmissionCallbackAction>(services));
        }

        [Fact]
        public void UseSynchronousRecordSubmissionOperation_RegistersAction()
        {
            var services = BuildAndCapture(b =>
                b.UseSynchronousRecordSubmissionOperation<StubBinaryRetrievalAction>());
            Assert.True(HasService<IBinaryRetrievalAction>(services));
            Assert.True(HasImplementation<SynchronousSubmitRecordOperation>(services));
        }

        [Fact]
        public void UseSynchronousRecordSubmissionOperation_WithCallbacks_RegistersAll()
        {
            var services = BuildAndCapture(b =>
                b.UseSynchronousRecordSubmissionOperation<StubBinaryRetrievalAction, StubRecordSubmissionCallbackAction, StubBinarySubmissionCallbackAction>());
            Assert.True(HasService<IBinaryRetrievalAction>(services));
            Assert.True(HasService<IRecordSubmissionCallbackAction>(services));
            Assert.True(HasService<IBinarySubmissionCallbackAction>(services));
        }

        [Fact]
        public void UseAggregationSubmissionOperation_RegistersOperation()
        {
            var services = BuildAndCapture(b => b.UseAggregationSubmissionOperation());
            Assert.True(HasImplementation<SubmitAggregationOperation>(services));
        }

        [Fact]
        public void UseAggregationSubmissionOperation_WithCallback_RegistersCallback()
        {
            var services = BuildAndCapture(b => b.UseAggregationSubmissionOperation<StubAggregationSubmissionCallbackAction>());
            Assert.True(HasService<IAggregationSubmissionCallbackAction>(services));
        }

        [Fact]
        public void UseAuditEventSubmissionOperation_RegistersOperation()
        {
            var services = BuildAndCapture(b => b.UseAuditEventSubmissionOperation());
            Assert.True(HasImplementation<SubmitAuditEventOperation>(services));
        }

        [Fact]
        public void UseAuditEventSubmissionOperation_WithCallback_RegistersCallback()
        {
            var services = BuildAndCapture(b => b.UseAuditEventSubmissionOperation<StubAuditEventSubmissionCallbackAction>());
            Assert.True(HasService<IAuditEventSubmissionCallbackAction>(services));
        }

        [Fact]
        public void UseRecordDisposalOperation_RegistersAction()
        {
            var services = BuildAndCapture(b => b.UseRecordDisposalOperation<StubRecordDisposalAction>());
            Assert.True(HasService<IRecordDisposalAction>(services));
            Assert.True(HasImplementation<RecordDisposalOperation>(services));
        }

        [Fact]
        public void UseGenericQueueableWorkOperation_RegistersGenericAction()
        {
            var services = BuildAndCapture(b =>
                b.UseGenericQueueableWorkOperation<SubmitRecordOperation, StubGenericAction, string, StubActionResult>());
            Assert.True(HasService<IGenericAction<string, StubActionResult>>(services));
        }

        [Fact]
        public void UseGenericManagedWorkOperation_RegistersManagedGenericAction()
        {
            var services = BuildAndCapture(b =>
                b.UseGenericManagedWorkOperation<SubmitRecordOperation, StubGenericManagedAction, string, StubActionResult, StubOptions>("StubOptionsSection"));
            Assert.True(HasService<IGenericManagedAction<string, StubActionResult>>(services));
        }
    }

    #region Stub action classes
    internal class StubContentManagerCallbackAction : IContentManagerCallbackAction
    {
        public Task ExecuteAsync(List<ConnectorConfigModel> connectorConfigurations, CancellationToken cancellationToken) => throw new NotImplementedException();
    }

    internal class StubChannelDiscoveryAction : IChannelDiscoveryAction
    {
        public Task<ChannelDiscoveryResult> ExecuteAsync(ConnectorConfigModel connectorConfiguration, CancellationToken cancellationToken, string? cursor = null) => throw new NotImplementedException();
    }

    internal class StubContentSynchronisationAction : IContentSynchronisationAction
    {
        public Task<ContentResult> BeginAsync(ConnectorConfigModel connectorConfiguration, Channel channel, DateTimeOffset startDate, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<ContentResult> ContinueAsync(ConnectorConfigModel connectorConfiguration, Channel channel, string cursor, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task StopAsync(ConnectorConfigModel connectorConfiguration, Channel channel, string cursor, CancellationToken cancellationToken) => throw new NotImplementedException();
    }

    internal class StubContentRegistrationAction : IContentRegistrationAction
    {
        public Task<ContentResult> BeginAsync(ConnectorConfigModel connectorConfiguration, Channel channel, IDictionary<string, string> context, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<ContentResult> ContinueAsync(ConnectorConfigModel connectorConfiguration, Channel channel, string cursor, IDictionary<string, string> context, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task StopAsync(ConnectorConfigModel connectorConfiguration, Channel channel, string cursor, CancellationToken cancellationToken) => throw new NotImplementedException();
    }

    internal class StubBinaryRetrievalAction : IBinaryRetrievalAction
    {
        public Task<BinaryRetrievalResult> ExecuteAsync(ConnectorConfigModel connectorConfiguration, BinaryMetaInfo binaryMetaInfo, CancellationToken cancellationToken) => throw new NotImplementedException();
    }

    internal class StubRecordSubmissionCallbackAction : IRecordSubmissionCallbackAction
    {
        public Task ExecuteAsync(ConnectorConfigModel connectorConfiguration, Record record, SubmissionActionType submissionActionType, CancellationToken cancellationToken) => throw new NotImplementedException();
    }

    internal class StubBinarySubmissionCallbackAction : IBinarySubmissionCallbackAction
    {
        public Task ExecuteAsync(ConnectorConfigModel connectorConfiguration, BinaryMetaInfo binaryMetaInfo, SubmissionActionType submissionActionType, CancellationToken cancellationToken) => throw new NotImplementedException();
    }

    internal class StubAggregationSubmissionCallbackAction : IAggregationSubmissionCallbackAction
    {
        public Task ExecuteAsync(ConnectorConfigModel connectorConfiguration, Aggregation aggregation, SubmissionActionType submissionActionType, CancellationToken cancellationToken) => throw new NotImplementedException();
    }

    internal class StubAuditEventSubmissionCallbackAction : IAuditEventSubmissionCallbackAction
    {
        public Task ExecuteAsync(ConnectorConfigModel connectorConfiguration, AuditEvent auditEvent, SubmissionActionType submissionActionType, CancellationToken cancellationToken) => throw new NotImplementedException();
    }

    internal class StubRecordDisposalAction : IRecordDisposalAction
    {
        public Task<RecordDisposalResult> ExecuteAsync(ConnectorConfigModel connectorConfiguration, Record record, CancellationToken cancellationToken) => throw new NotImplementedException();
    }

    internal class StubActionResult : ActionResultBase { }

    internal class StubGenericAction : IGenericAction<string, StubActionResult>
    {
        public Task<StubActionResult> ExecuteAsync(ConnectorConfigModel connectorConfiguration, string item, CancellationToken cancellationToken) => throw new NotImplementedException();
    }

    internal class StubGenericManagedAction : IGenericManagedAction<string, StubActionResult>
    {
        public Task<StubActionResult> BeginAsync(ConnectorConfigModel connectorConfiguration, string item, IDictionary<string, string>? context, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<StubActionResult> ContinueAsync(ConnectorConfigModel connectorConfiguration, string item, string cursor, IDictionary<string, string>? context, CancellationToken cancellationToken) => throw new NotImplementedException();
    }

    internal class StubOptions { }
    #endregion
}
