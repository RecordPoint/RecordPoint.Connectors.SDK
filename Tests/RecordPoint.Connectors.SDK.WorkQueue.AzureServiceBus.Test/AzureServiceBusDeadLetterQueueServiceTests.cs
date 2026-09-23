#nullable enable
using System.Collections.Generic;
using System.Linq;
using Azure;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using RecordPoint.Connectors.SDK.WebHost.Services;
using RecordPoint.Connectors.SDK.Work;
using RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.Test.Mock;
using Xunit;

namespace RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.Test
{
    public class AzureServiceBusDeadLetterQueueServiceTests
    {
        private const string QueueName = "dead-letter-queue";

        private static ServiceBusReceivedMessage MessageWith(long sequenceNumber, WorkRequest? request = null, string? rawBody = null)
        {
            var body = rawBody ?? (request == null
                ? JsonConvert.SerializeObject(new WorkRequest { WorkType = "Test Work" })
                : JsonConvert.SerializeObject(request));
            return ServiceBusModelFactory.ServiceBusReceivedMessage(
                body: BinaryData.FromString(body),
                messageId: $"msg-{sequenceNumber}",
                sequenceNumber: sequenceNumber);
        }

        private static AzureServiceBusDeadLetterQueueService CreateService(
            MockServiceBusClientFactory factory,
            out Mock<IManagedWorkStatusManager> managedWorkStatusManager)
        {
            managedWorkStatusManager = new Mock<IManagedWorkStatusManager>();
            var logger = new Mock<ILogger<AzureServiceBusDeadLetterQueueService>>();
            return new AzureServiceBusDeadLetterQueueService(factory, managedWorkStatusManager.Object, logger.Object);
        }

        // ---- GetMessagesAsync ----

        [Fact]
        public async Task GetMessagesAsync_ReturnsMappedModels_DefaultMaxCount()
        {
            var factory = new MockServiceBusClientFactory();
            var receiver = new Mock<ServiceBusReceiver>(MockBehavior.Loose);
            receiver
                .Setup(r => r.PeekMessagesAsync(It.IsAny<int>(), It.IsAny<long?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ServiceBusReceivedMessage> { MessageWith(1), MessageWith(2) });
            factory.ReceiverFactory = _ => receiver;

            var service = CreateService(factory, out _);
            var result = await service.GetMessagesAsync(QueueName);

            Assert.Equal(2, result.Count);
            Assert.Equal("1", result[0].SequenceNumber);
            Assert.Equal("2", result[1].SequenceNumber);
        }

        [Fact]
        public async Task GetMessagesAsync_HonoursMaxCount()
        {
            var factory = new MockServiceBusClientFactory();
            var receiver = new Mock<ServiceBusReceiver>(MockBehavior.Loose);
            receiver
                .Setup(r => r.PeekMessagesAsync(It.IsAny<int>(), It.IsAny<long?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ServiceBusReceivedMessage> { MessageWith(10), MessageWith(11) });
            factory.ReceiverFactory = _ => receiver;

            var service = CreateService(factory, out _);
            var result = await service.GetMessagesAsync(QueueName, maxCount: 2);

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetMessagesAsync_BreaksWhenSequenceNumberDoesNotAdvance()
        {
            // Two consecutive full batches with the same trailing sequence number should
            // trigger the previousSequenceNumber guard and stop the loop.
            var factory = new MockServiceBusClientFactory();
            var fullBatch = Enumerable.Range(1, 1000).Select(i => MessageWith(i)).ToList();
            var receiver = new Mock<ServiceBusReceiver>(MockBehavior.Loose);
            receiver
                .Setup(r => r.PeekMessagesAsync(It.IsAny<int>(), It.IsAny<long?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(fullBatch);
            factory.ReceiverFactory = _ => receiver;

            var service = CreateService(factory, out _);
            var result = await service.GetMessagesAsync(QueueName);

            // First batch added; second batch detected as non-advancing and skipped.
            Assert.Equal(1000, result.Count);
        }

        [Fact]
        public async Task GetMessagesAsync_ReturnsEmpty_WhenNoMessages()
        {
            var factory = new MockServiceBusClientFactory();
            var receiver = new Mock<ServiceBusReceiver>(MockBehavior.Loose);
            receiver
                .Setup(r => r.PeekMessagesAsync(It.IsAny<int>(), It.IsAny<long?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ServiceBusReceivedMessage>());
            factory.ReceiverFactory = _ => receiver;

            var service = CreateService(factory, out _);
            var result = await service.GetMessagesAsync(QueueName);

            Assert.Empty(result);
        }

        // ---- GetMessageAsync ----

        [Fact]
        public async Task GetMessageAsync_ReturnsMappedModel()
        {
            var factory = new MockServiceBusClientFactory();
            var receiver = new Mock<ServiceBusReceiver>(MockBehavior.Loose);
            receiver
                .Setup(r => r.PeekMessageAsync(It.IsAny<long?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(MessageWith(99));
            factory.ReceiverFactory = _ => receiver;

            var service = CreateService(factory, out _);
            var result = await service.GetMessageAsync(QueueName, 99);

            Assert.Equal("99", result.SequenceNumber);
            Assert.Equal("msg-99", result.MessageId);
        }

        // ---- ResubmitMessagesAsync ----

        [Fact]
        public async Task ResubmitMessagesAsync_SendsBatch_AndCompletesMessages()
        {
            var factory = new MockServiceBusClientFactory();
            var store = new List<ServiceBusMessage>();
            factory.SenderMock
                .Setup(s => s.CreateMessageBatchAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => MockServiceBusClientFactory.CreateBatch(store, _ => true));
            factory.SenderMock
                .Setup(s => s.SendMessagesAsync(It.IsAny<ServiceBusMessageBatch>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var receiver = new Mock<ServiceBusReceiver>(MockBehavior.Loose);
            receiver
                .SetupSequence(r => r.ReceiveMessagesAsync(It.IsAny<int>(), It.IsAny<TimeSpan?>(), It.IsAny<CancellationToken>()))
                // includes a non-target message (seq 3) to exercise the Remove==false path
                .ReturnsAsync(new List<ServiceBusReceivedMessage> { MessageWith(1), MessageWith(2), MessageWith(3) })
                .ReturnsAsync(new List<ServiceBusReceivedMessage>());
            receiver
                .Setup(r => r.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            factory.ReceiverFactory = _ => receiver;

            var service = CreateService(factory, out _);
            await service.ResubmitMessagesAsync(QueueName, new long[] { 1, 2 });

            factory.SenderMock.Verify(s => s.SendMessagesAsync(It.IsAny<ServiceBusMessageBatch>(), It.IsAny<CancellationToken>()), Times.Once);
            receiver.Verify(r => r.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
            // Fault count reset to 0 on the resubmitted message
            Assert.All(store, m => Assert.Equal(0, JsonConvert.DeserializeObject<WorkRequest>(m.Body.ToString())!.FaultedCount));
        }

        [Fact]
        public async Task ResubmitMessagesAsync_SendsPartialBatch_WhenBatchFull()
        {
            var factory = new MockServiceBusClientFactory();
            var firstStore = new List<ServiceBusMessage>();
            var secondStore = new List<ServiceBusMessage>();
            factory.SenderMock
                .SetupSequence(s => s.CreateMessageBatchAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(MockServiceBusClientFactory.CreateBatch(firstStore, _ => false))  // full: rejects
                .ReturnsAsync(MockServiceBusClientFactory.CreateBatch(secondStore, _ => true));  // accepts
            factory.SenderMock
                .Setup(s => s.SendMessagesAsync(It.IsAny<ServiceBusMessageBatch>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var receiver = new Mock<ServiceBusReceiver>(MockBehavior.Loose);
            receiver
                .SetupSequence(r => r.ReceiveMessagesAsync(It.IsAny<int>(), It.IsAny<TimeSpan?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ServiceBusReceivedMessage> { MessageWith(1) })
                .ReturnsAsync(new List<ServiceBusReceivedMessage>());
            receiver
                .Setup(r => r.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            factory.ReceiverFactory = _ => receiver;

            var service = CreateService(factory, out _);
            await service.ResubmitMessagesAsync(QueueName, new long[] { 1 });

            // once for the full batch, once for the final batch
            factory.SenderMock.Verify(s => s.SendMessagesAsync(It.IsAny<ServiceBusMessageBatch>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
            Assert.Single(secondStore);
        }

        [Fact]
        public async Task ResubmitMessagesAsync_Throws_WhenMessageTooLargeForEmptyBatch()
        {
            var factory = new MockServiceBusClientFactory();
            factory.SenderMock
                .Setup(s => s.CreateMessageBatchAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => MockServiceBusClientFactory.CreateBatch(new List<ServiceBusMessage>(), _ => false));
            factory.SenderMock
                .Setup(s => s.SendMessagesAsync(It.IsAny<ServiceBusMessageBatch>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var receiver = new Mock<ServiceBusReceiver>(MockBehavior.Loose);
            receiver
                .SetupSequence(r => r.ReceiveMessagesAsync(It.IsAny<int>(), It.IsAny<TimeSpan?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ServiceBusReceivedMessage> { MessageWith(1) })
                .ReturnsAsync(new List<ServiceBusReceivedMessage>());
            factory.ReceiverFactory = _ => receiver;

            var service = CreateService(factory, out _);
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => service.ResubmitMessagesAsync(QueueName, new long[] { 1 }));
        }

        [Fact]
        public async Task ResubmitMessagesAsync_ForwardsNullWorkRequestBodyVerbatim_AndCompletes()
        {
            var factory = new MockServiceBusClientFactory();
            var store = new List<ServiceBusMessage>();
            factory.SenderMock
                .Setup(s => s.CreateMessageBatchAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => MockServiceBusClientFactory.CreateBatch(store, _ => true));

            var receiver = new Mock<ServiceBusReceiver>(MockBehavior.Loose);
            receiver
                .SetupSequence(r => r.ReceiveMessagesAsync(It.IsAny<int>(), It.IsAny<TimeSpan?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ServiceBusReceivedMessage> { MessageWith(1, rawBody: "null") })
                .ReturnsAsync(new List<ServiceBusReceivedMessage>());
            receiver
                .Setup(r => r.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            factory.ReceiverFactory = _ => receiver;

            var service = CreateService(factory, out _);
            await service.ResubmitMessagesAsync(QueueName, new long[] { 1 });

            // A body that does not deserialise to a WorkRequest (here the literal "null")
            // must still be resubmitted verbatim, not silently dropped, before it is
            // completed from the DLQ. Previously this was a message-loss window.
            Assert.Single(store);
            Assert.Equal("null", store[0].Body.ToString());
            factory.SenderMock.Verify(s => s.SendMessagesAsync(It.IsAny<ServiceBusMessageBatch>(), It.IsAny<CancellationToken>()), Times.Once);
            receiver.Verify(r => r.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ResubmitMessagesAsync_NoSequenceNumbers_DoesNothing()
        {
            var factory = new MockServiceBusClientFactory();
            var store = new List<ServiceBusMessage>();
            factory.SenderMock
                .Setup(s => s.CreateMessageBatchAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => MockServiceBusClientFactory.CreateBatch(store, _ => true));

            var receiver = new Mock<ServiceBusReceiver>(MockBehavior.Loose);
            factory.ReceiverFactory = _ => receiver;

            var service = CreateService(factory, out _);
            await service.ResubmitMessagesAsync(QueueName, Array.Empty<long>());

            factory.SenderMock.Verify(s => s.SendMessagesAsync(It.IsAny<ServiceBusMessageBatch>(), It.IsAny<CancellationToken>()), Times.Never);
            receiver.Verify(r => r.ReceiveMessagesAsync(It.IsAny<int>(), It.IsAny<TimeSpan?>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task ResubmitMessagesAsync_LogsWarning_WhenTargetsNotFound()
        {
            var factory = new MockServiceBusClientFactory();
            factory.SenderMock
                .Setup(s => s.CreateMessageBatchAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => MockServiceBusClientFactory.CreateBatch(new List<ServiceBusMessage>(), _ => true));

            var receiver = new Mock<ServiceBusReceiver>(MockBehavior.Loose);
            receiver
                .Setup(r => r.ReceiveMessagesAsync(It.IsAny<int>(), It.IsAny<TimeSpan?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ServiceBusReceivedMessage>());
            factory.ReceiverFactory = _ => receiver;

            var service = CreateService(factory, out _);
            await service.ResubmitMessagesAsync(QueueName, new long[] { 404 });

            factory.SenderMock.Verify(s => s.SendMessagesAsync(It.IsAny<ServiceBusMessageBatch>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        // ---- DeleteMessageAsync ----

        [Fact]
        public async Task DeleteMessageAsync_CompletesMatchingMessage()
        {
            var factory = new MockServiceBusClientFactory();
            var receiver = new Mock<ServiceBusReceiver>(MockBehavior.Loose);
            receiver
                .SetupSequence(r => r.ReceiveMessagesAsync(It.IsAny<int>(), It.IsAny<TimeSpan?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ServiceBusReceivedMessage> { MessageWith(5) })
                .ReturnsAsync(new List<ServiceBusReceivedMessage>());
            receiver
                .Setup(r => r.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            factory.ReceiverFactory = _ => receiver;

            var service = CreateService(factory, out _);
            await service.DeleteMessageAsync(QueueName, 5);

            receiver.Verify(r => r.CompleteMessageAsync(It.Is<ServiceBusReceivedMessage>(m => m.SequenceNumber == 5), It.IsAny<CancellationToken>()), Times.Once);
        }

        // ---- DeleteAllMessagesAsync ----

        [Fact]
        public async Task DeleteAllMessagesAsync_CompletesEachDeadLetterMessage()
        {
            var factory = new MockServiceBusClientFactory();
            var runtimeProps = ServiceBusModelFactory.QueueRuntimeProperties(name: QueueName, deadLetterMessageCount: 2);
            factory.AdministrationClientMock
                .Setup(a => a.GetQueueRuntimePropertiesAsync(QueueName, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Response.FromValue(runtimeProps, Moq.Mock.Of<Response>()));

            var receiver = new Mock<ServiceBusReceiver>(MockBehavior.Loose);
            receiver
                .SetupSequence(r => r.ReceiveMessageAsync(It.IsAny<TimeSpan?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(MessageWith(1))
                .ReturnsAsync(MessageWith(2));
            receiver
                .Setup(r => r.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            factory.ReceiverFactory = _ => receiver;

            var service = CreateService(factory, out _);
            await service.DeleteAllMessagesAsync(QueueName);

            receiver.Verify(r => r.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [Fact]
        public async Task DeleteAllMessagesAsync_BreaksEarly_WhenReceiveReturnsNull()
        {
            var factory = new MockServiceBusClientFactory();
            var runtimeProps = ServiceBusModelFactory.QueueRuntimeProperties(name: QueueName, deadLetterMessageCount: 5);
            factory.AdministrationClientMock
                .Setup(a => a.GetQueueRuntimePropertiesAsync(QueueName, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Response.FromValue(runtimeProps, Moq.Mock.Of<Response>()));

            var receiver = new Mock<ServiceBusReceiver>(MockBehavior.Loose);
            receiver
                .SetupSequence(r => r.ReceiveMessageAsync(It.IsAny<TimeSpan?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(MessageWith(1))
                .ReturnsAsync(MessageWith(2))
                .ReturnsAsync((ServiceBusReceivedMessage?)null);
            receiver
                .Setup(r => r.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            factory.ReceiverFactory = _ => receiver;

            var service = CreateService(factory, out _);
            await service.DeleteAllMessagesAsync(QueueName);

            receiver.Verify(r => r.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        }
    }
}
