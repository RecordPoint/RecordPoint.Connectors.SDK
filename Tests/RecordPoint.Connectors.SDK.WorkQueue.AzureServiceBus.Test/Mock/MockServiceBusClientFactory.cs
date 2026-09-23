#nullable enable
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Moq;
using RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.Test;

namespace RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.Test.Mock
{
    /// <summary>
    /// A mock <see cref="IServiceBusClientFactory"/> that hands out Moq-backed
    /// Service Bus client/sender/receiver/processor instances so that the
    /// Azure Service Bus SDK is never actually contacted.
    /// </summary>
    public class MockServiceBusClientFactory : IServiceBusClientFactory
    {
        public Mock<ServiceBusClient> ClientMock { get; } = new(MockBehavior.Loose);
        public Mock<ServiceBusAdministrationClient> AdministrationClientMock { get; } = new(MockBehavior.Loose);
        public Mock<ServiceBusSender> SenderMock { get; } = new(MockBehavior.Loose);

        /// <summary>
        /// Processors created, keyed by queue name.
        /// </summary>
        public Dictionary<string, Mock<ServiceBusProcessor>> ProcessorMocks { get; } = new();

        /// <summary>
        /// Receivers created, in creation order.
        /// </summary>
        public List<Mock<ServiceBusReceiver>> ReceiverMocks { get; } = new();

        /// <summary>
        /// Optional override to supply a specific receiver mock per creation.
        /// </summary>
        public Func<string, Mock<ServiceBusReceiver>>? ReceiverFactory { get; set; }

        public MockServiceBusClientFactory()
        {
            ClientMock
                .Setup(c => c.CreateSender(It.IsAny<string>()))
                .Returns(SenderMock.Object);

            ClientMock
                .Setup(c => c.CreateSender(It.IsAny<string>(), It.IsAny<ServiceBusSenderOptions>()))
                .Returns(SenderMock.Object);

            ClientMock
                .Setup(c => c.CreateProcessor(It.IsAny<string>(), It.IsAny<ServiceBusProcessorOptions>()))
                .Returns<string, ServiceBusProcessorOptions>((queueName, _) => CreateProcessorMock(queueName).Object);

            ClientMock
                .Setup(c => c.CreateReceiver(It.IsAny<string>(), It.IsAny<ServiceBusReceiverOptions>()))
                .Returns<string, ServiceBusReceiverOptions>((queueName, _) =>
                {
                    var receiverMock = ReceiverFactory?.Invoke(queueName) ?? new Mock<ServiceBusReceiver>(MockBehavior.Loose);
                    ReceiverMocks.Add(receiverMock);
                    return receiverMock.Object;
                });

            ClientMock
                .Setup(c => c.DisposeAsync())
                .Returns(ValueTask.CompletedTask);

            SenderMock
                .Setup(s => s.DisposeAsync())
                .Returns(ValueTask.CompletedTask);
        }

        private Mock<ServiceBusProcessor> CreateProcessorMock(string queueName)
        {
            var processorMock = new Mock<ServiceBusProcessor>(MockBehavior.Loose);
            processorMock.Setup(p => p.StartProcessingAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            processorMock.Setup(p => p.StopProcessingAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            processorMock.Setup(p => p.CloseAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            // NOTE: ServiceBusProcessor.DisposeAsync and its ProcessMessageAsync/ProcessErrorAsync
            // events are not overridable, so they cannot be mocked. The real event 'add' accessors
            // run against the mock instance and store the handler in a private backing field, which
            // we later extract via reflection (see GetMessageHandler / GetErrorHandler).

            ProcessorMocks[queueName] = processorMock;
            return processorMock;
        }

        /// <summary>
        /// Extracts the registered ProcessMessageAsync handler from a processor via reflection.
        /// </summary>
        public Func<ProcessMessageEventArgs, Task>? GetMessageHandler(string queueName)
            => ExtractDelegate<Func<ProcessMessageEventArgs, Task>>(ProcessorMocks[queueName].Object);

        /// <summary>
        /// Extracts the registered ProcessErrorAsync handler from a processor via reflection.
        /// </summary>
        public Func<ProcessErrorEventArgs, Task>? GetErrorHandler(string queueName)
            => ExtractDelegate<Func<ProcessErrorEventArgs, Task>>(ProcessorMocks[queueName].Object);

        private static TDelegate? ExtractDelegate<TDelegate>(ServiceBusProcessor processor)
            where TDelegate : class
            => ReflectionTestHelper.ExtractDelegateField<TDelegate>(processor, typeof(ServiceBusProcessor));

        public ServiceBusClient CreateServiceBusClient() => ClientMock.Object;

        public ServiceBusAdministrationClient CreateServiceBusAdministrationClient() => AdministrationClientMock.Object;

        /// <summary>
        /// Creates a real <see cref="ServiceBusMessageBatch"/> backed by an in-memory store,
        /// with a configurable predicate controlling whether messages are accepted.
        /// </summary>
        public static ServiceBusMessageBatch CreateBatch(IList<ServiceBusMessage> store, Func<ServiceBusMessage, bool> tryAdd)
            => ServiceBusModelFactory.ServiceBusMessageBatch(
                batchSizeBytes: 256_000,
                batchMessageStore: store,
                batchOptions: new CreateMessageBatchOptions(),
                tryAddCallback: tryAdd);
    }
}
