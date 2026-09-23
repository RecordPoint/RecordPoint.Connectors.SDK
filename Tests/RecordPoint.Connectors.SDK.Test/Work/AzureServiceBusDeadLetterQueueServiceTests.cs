using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.WebHost.Services;
using RecordPoint.Connectors.SDK.Work;
using RecordPoint.Connectors.SDK.Work.Models;
using RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus;
using System.Linq;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Work
{
    public class AzureServiceBusDeadLetterQueueServiceTests
    {
        private const string QueueName = "test-queue";

        private readonly Mock<IServiceBusClientFactory> _clientFactory = new();
        private readonly Mock<ServiceBusClient> _serviceBusClient = new();
        private readonly Mock<ServiceBusReceiver> _receiver = new();
        private readonly Mock<IManagedWorkStatusManager> _managedWorkStatusManager = new();
        private readonly Mock<ILogger<AzureServiceBusDeadLetterQueueService>> _logger = new();

        public AzureServiceBusDeadLetterQueueServiceTests()
        {
            _clientFactory
                .Setup(f => f.CreateServiceBusClient())
                .Returns(_serviceBusClient.Object);

            _clientFactory
                .Setup(f => f.CreateServiceBusAdministrationClient())
                .Returns(new Mock<ServiceBusAdministrationClient>().Object);

            _serviceBusClient
                .Setup(c => c.CreateReceiver(QueueName, It.IsAny<ServiceBusReceiverOptions>()))
                .Returns(_receiver.Object);

            // Default: the post-drain emptiness peek (only invoked when a pass resubmits 0)
            // returns nothing, i.e. "confirmed empty". Tests that need the not-empty path
            // override this to return a message.
            _receiver
                .Setup(r => r.PeekMessagesAsync(It.IsAny<int>(), It.IsAny<long?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ServiceBusReceivedMessage>());
        }

        // A dead-letter message with a healthy (far-future) lock, as PeekLock receive
        // would produce. Tests that exercise the lock-expiry guard override lockedUntil.
        private static ServiceBusReceivedMessage DrainMsg(long seq, DateTimeOffset? lockedUntil = null) =>
            ServiceBusModelFactory.ServiceBusReceivedMessage(
                body: BinaryData.FromString($"{{\"WorkId\":\"{seq}\"}}"),
                sequenceNumber: seq,
                lockedUntil: lockedUntil ?? DateTimeOffset.UtcNow.AddMinutes(5));

        [Fact]
        public async Task GetReceivedMessages_WhenTargetsBeyondScanBound_ShouldStopAndReturnPartialResults()
        {
            // Arrange: request 2 sequence numbers, but only one exists within scan range.
            // maxScanCount = 2 * 10 + 1000 = 1020
            // We'll return batches of 100 non-matching messages, plus one match at batch 3.
            // The second target (seq 9999) is never returned — it's "beyond the scan bound".

            var targetSequenceNumbers = new long[] { 300, 9999 };
            var batchNumber = 0;

            _receiver
                .Setup(r => r.ReceiveMessagesAsync(
                    It.IsAny<int>(),
                    It.IsAny<TimeSpan?>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() =>
                {
                    batchNumber++;
                    var messages = new List<ServiceBusReceivedMessage>();

                    // Each batch has 100 messages with sequential sequence numbers
                    var startSeq = (batchNumber - 1) * 100 + 1;
                    for (var i = 0; i < 100; i++)
                    {
                        var seqNum = startSeq + i;
                        messages.Add(ServiceBusModelFactory.ServiceBusReceivedMessage(
                            body: BinaryData.FromString($"{{\"msg\":{seqNum}}}"),
                            sequenceNumber: seqNum));
                    }

                    return messages;
                });

            var sut = new AzureServiceBusDeadLetterQueueService(_clientFactory.Object, _managedWorkStatusManager.Object, _logger.Object);

            // Act: call via ResubmitMessagesAsync would be complex, so use reflection
            // to test the private method directly, or test through the public surface.
            // We'll test through DeleteMessageAsync which calls GetReceivedServiceBusMessagesAsync
            // with a single sequence number. Instead, let's test the scenario through ResubmitMessagesAsync.

            // Actually, let's test via the public method. We need a sender mock too.
            var mockSender = new Mock<ServiceBusSender>();
            _serviceBusClient
                .Setup(c => c.CreateSender(QueueName))
                .Returns(mockSender.Object);

            var mockBatch = ServiceBusModelFactory.ServiceBusMessageBatch(
                256_000, // max size
                new List<ServiceBusMessage>(),
                new CreateMessageBatchOptions(),
                (msg) => true); // accept all messages

            mockSender
                .Setup(s => s.CreateMessageBatchAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockBatch);

            mockSender
                .Setup(s => s.SendMessagesAsync(
                    It.IsAny<ServiceBusMessageBatch>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _receiver
                .Setup(r => r.CompleteMessageAsync(
                    It.IsAny<ServiceBusReceivedMessage>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            await sut.ResubmitMessagesAsync(QueueName, targetSequenceNumbers);

            // Assert: should have stopped scanning (not looped forever)
            // maxScanCount = 2 * 10 + 1000 = 1020, batches of 100 = max ~11 batches
            Assert.True(batchNumber <= 11,
                $"scanning should stop at the scan bound, not loop indefinitely (batchNumber={batchNumber})");

            // Should have found sequence 300 (in batch 3) but not 9999
            _receiver.Verify(
                r => r.CompleteMessageAsync(
                    It.Is<ServiceBusReceivedMessage>(m => m.SequenceNumber == 300),
                    It.IsAny<CancellationToken>()),
                Times.Once,
                "the message with sequence 300 should be found and completed");
        }

        [Fact]
        public async Task ResubmitMessagesAsync_AbandonsNonTargetMessages_ToReleaseLocks()
        {
            // Arrange: one target (seq 50) sits in a batch alongside 99 non-target
            // messages. The non-targets must be abandoned so their PeekLocks release
            // immediately rather than being left to expire after the lock duration.
            const long targetSeq = 50;
            var callCount = 0;

            _receiver
                .Setup(r => r.ReceiveMessagesAsync(
                    It.IsAny<int>(),
                    It.IsAny<TimeSpan?>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() =>
                {
                    callCount++;
                    if (callCount > 1) return new List<ServiceBusReceivedMessage>();

                    var messages = new List<ServiceBusReceivedMessage>();
                    for (var seq = 1; seq <= 100; seq++)
                    {
                        messages.Add(ServiceBusModelFactory.ServiceBusReceivedMessage(
                            body: BinaryData.FromString($"{{\"msg\":{seq}}}"),
                            sequenceNumber: seq));
                    }
                    return messages;
                });

            var mockSender = new Mock<ServiceBusSender>();
            _serviceBusClient
                .Setup(c => c.CreateSender(QueueName))
                .Returns(mockSender.Object);

            var mockBatch = ServiceBusModelFactory.ServiceBusMessageBatch(
                256_000,
                new List<ServiceBusMessage>(),
                new CreateMessageBatchOptions(),
                _ => true);

            mockSender
                .Setup(s => s.CreateMessageBatchAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockBatch);

            mockSender
                .Setup(s => s.SendMessagesAsync(
                    It.IsAny<ServiceBusMessageBatch>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _receiver
                .Setup(r => r.CompleteMessageAsync(
                    It.IsAny<ServiceBusReceivedMessage>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _receiver
                .Setup(r => r.AbandonMessageAsync(
                    It.IsAny<ServiceBusReceivedMessage>(),
                    It.IsAny<IDictionary<string, object>>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var sut = new AzureServiceBusDeadLetterQueueService(_clientFactory.Object, _managedWorkStatusManager.Object, _logger.Object);

            // Act
            await sut.ResubmitMessagesAsync(QueueName, new[] { targetSeq });

            // Assert: the 99 non-target messages were abandoned...
            _receiver.Verify(
                r => r.AbandonMessageAsync(
                    It.Is<ServiceBusReceivedMessage>(m => m.SequenceNumber != targetSeq),
                    It.IsAny<IDictionary<string, object>>(),
                    It.IsAny<CancellationToken>()),
                Times.Exactly(99),
                "every non-target scanned message should be abandoned to release its lock");

            // ...and the target was never abandoned (it was resubmitted and completed).
            _receiver.Verify(
                r => r.AbandonMessageAsync(
                    It.Is<ServiceBusReceivedMessage>(m => m.SequenceNumber == targetSeq),
                    It.IsAny<IDictionary<string, object>>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task ResubmitMessagesAsync_DisposesReceiverAndSender()
        {
            // Arrange: an empty DLQ so the method does no real work, then exits.
            // The receiver and sender it created must still be disposed.
            _receiver
                .Setup(r => r.ReceiveMessagesAsync(
                    It.IsAny<int>(),
                    It.IsAny<TimeSpan?>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ServiceBusReceivedMessage>());

            var mockSender = new Mock<ServiceBusSender>();
            _serviceBusClient
                .Setup(c => c.CreateSender(QueueName))
                .Returns(mockSender.Object);

            var mockBatch = ServiceBusModelFactory.ServiceBusMessageBatch(
                256_000,
                new List<ServiceBusMessage>(),
                new CreateMessageBatchOptions(),
                _ => true);

            mockSender
                .Setup(s => s.CreateMessageBatchAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockBatch);

            var sut = new AzureServiceBusDeadLetterQueueService(_clientFactory.Object, _managedWorkStatusManager.Object, _logger.Object);

            // Act
            await sut.ResubmitMessagesAsync(QueueName, new long[] { 1 });

            // Assert: both the receiver and the sender were disposed.
            _receiver.Verify(r => r.DisposeAsync(), Times.Once);
            mockSender.Verify(s => s.DisposeAsync(), Times.Once);
        }

        [Fact]
        public async Task ResubmitMessagesAsync_ScanReachesTargetBeyondFirstBatch_AndDefersAbandonUntilScanComplete()
        {
            // The target sits in the 3rd batch. The scan must walk forward to reach it,
            // which requires non-targets to stay locked during the scan; abandoning them
            // mid-scan would re-deliver the head of the DLQ and stall the scan on batch 1.
            // So every receive must happen before any abandon, and the deep target must
            // still be found and completed.
            const long targetSeq = 250;
            var callOrder = new List<string>();
            var receiveCount = 0;

            _receiver
                .Setup(r => r.ReceiveMessagesAsync(
                    It.IsAny<int>(),
                    It.IsAny<TimeSpan?>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() =>
                {
                    callOrder.Add("receive");
                    receiveCount++;
                    if (receiveCount > 3) return new List<ServiceBusReceivedMessage>();

                    var start = (receiveCount - 1) * 100 + 1;
                    var messages = new List<ServiceBusReceivedMessage>();
                    for (var i = 0; i < 100; i++)
                    {
                        var seq = start + i;
                        messages.Add(ServiceBusModelFactory.ServiceBusReceivedMessage(
                            body: BinaryData.FromString($"{{\"msg\":{seq}}}"),
                            sequenceNumber: seq));
                    }
                    return messages;
                });

            _receiver
                .Setup(r => r.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            _receiver
                .Setup(r => r.AbandonMessageAsync(
                    It.IsAny<ServiceBusReceivedMessage>(),
                    It.IsAny<IDictionary<string, object>>(),
                    It.IsAny<CancellationToken>()))
                .Callback(() => callOrder.Add("abandon"))
                .Returns(Task.CompletedTask);

            var mockSender = new Mock<ServiceBusSender>();
            _serviceBusClient.Setup(c => c.CreateSender(QueueName)).Returns(mockSender.Object);
            var mockBatch = ServiceBusModelFactory.ServiceBusMessageBatch(
                256_000, new List<ServiceBusMessage>(), new CreateMessageBatchOptions(), _ => true);
            mockSender.Setup(s => s.CreateMessageBatchAsync(It.IsAny<CancellationToken>())).ReturnsAsync(mockBatch);
            mockSender.Setup(s => s.SendMessagesAsync(It.IsAny<ServiceBusMessageBatch>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var sut = new AzureServiceBusDeadLetterQueueService(_clientFactory.Object, _managedWorkStatusManager.Object, _logger.Object);

            await sut.ResubmitMessagesAsync(QueueName, new[] { targetSeq });

            // The deep target (batch 3) was found and completed...
            _receiver.Verify(
                r => r.CompleteMessageAsync(
                    It.Is<ServiceBusReceivedMessage>(m => m.SequenceNumber == targetSeq),
                    It.IsAny<CancellationToken>()),
                Times.Once,
                "the scan must advance past the first batch to find a deep target");

            // ...and every non-target abandon happened only after the scan finished receiving.
            Assert.Contains("abandon", callOrder);
            Assert.True(callOrder.LastIndexOf("receive") <
                callOrder.IndexOf("abandon"),
                "non-target locks must be released after the scan completes, not during it (abandoning mid-scan re-delivers the head and stalls forward progress)");
        }

        [Fact]
        public async Task ResubmitMessagesAsync_AbandonsNonTargetsAfterTargetsCompleted()
        {
            // Regression guard for the abandon-ordering fix: the non-target abandon must
            // run AFTER the targets are completed. Abandoning first puts best-effort lock
            // cleanup on the critical path, and a slow abandon phase could push a target's
            // lock past expiry so its CompleteMessageAsync fails after the copy was already
            // sent, producing a duplicate resubmit.
            const long targetSeq = 50;
            var callOrder = new List<string>();
            var callCount = 0;

            _receiver
                .Setup(r => r.ReceiveMessagesAsync(
                    It.IsAny<int>(),
                    It.IsAny<TimeSpan?>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() =>
                {
                    if (callCount++ > 0) return new List<ServiceBusReceivedMessage>();
                    var messages = new List<ServiceBusReceivedMessage>();
                    for (var seq = 1; seq <= 100; seq++)
                    {
                        messages.Add(ServiceBusModelFactory.ServiceBusReceivedMessage(
                            body: BinaryData.FromString($"{{\"msg\":{seq}}}"),
                            sequenceNumber: seq));
                    }
                    return messages;
                });

            _receiver
                .Setup(r => r.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()))
                .Callback(() => callOrder.Add("complete"))
                .Returns(Task.CompletedTask);
            _receiver
                .Setup(r => r.AbandonMessageAsync(
                    It.IsAny<ServiceBusReceivedMessage>(),
                    It.IsAny<IDictionary<string, object>>(),
                    It.IsAny<CancellationToken>()))
                .Callback(() => callOrder.Add("abandon"))
                .Returns(Task.CompletedTask);

            var mockSender = new Mock<ServiceBusSender>();
            _serviceBusClient.Setup(c => c.CreateSender(QueueName)).Returns(mockSender.Object);
            var mockBatch = ServiceBusModelFactory.ServiceBusMessageBatch(
                256_000, new List<ServiceBusMessage>(), new CreateMessageBatchOptions(), _ => true);
            mockSender.Setup(s => s.CreateMessageBatchAsync(It.IsAny<CancellationToken>())).ReturnsAsync(mockBatch);
            mockSender.Setup(s => s.SendMessagesAsync(It.IsAny<ServiceBusMessageBatch>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var sut = new AzureServiceBusDeadLetterQueueService(_clientFactory.Object, _managedWorkStatusManager.Object, _logger.Object);

            await sut.ResubmitMessagesAsync(QueueName, new[] { targetSeq });

            Assert.Contains("complete", callOrder);
            Assert.Contains("abandon", callOrder);
            Assert.True(callOrder.LastIndexOf("complete") <
                callOrder.IndexOf("abandon"),
                "all targets must be completed before any non-target is abandoned, keeping the abandon phase off the target settlement critical path");
        }

        [Fact]
        public async Task ResubmitMessagesAsync_AbandonFailureIsSwallowed_AndTargetStillCompleted()
        {
            // A failing non-target abandon (e.g. its lock already expired) is best-effort:
            // it must be swallowed and must not stop the target being resubmitted/completed.
            const long targetSeq = 50;
            var callCount = 0;

            _receiver
                .Setup(r => r.ReceiveMessagesAsync(
                    It.IsAny<int>(),
                    It.IsAny<TimeSpan?>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() =>
                {
                    if (callCount++ > 0) return new List<ServiceBusReceivedMessage>();
                    var messages = new List<ServiceBusReceivedMessage>();
                    for (var seq = 1; seq <= 100; seq++)
                    {
                        messages.Add(ServiceBusModelFactory.ServiceBusReceivedMessage(
                            body: BinaryData.FromString($"{{\"msg\":{seq}}}"),
                            sequenceNumber: seq));
                    }
                    return messages;
                });

            _receiver
                .Setup(r => r.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            // Every non-target abandon throws - the service must swallow and log it.
            _receiver
                .Setup(r => r.AbandonMessageAsync(
                    It.IsAny<ServiceBusReceivedMessage>(),
                    It.IsAny<IDictionary<string, object>>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ServiceBusException("lock lost", ServiceBusFailureReason.MessageLockLost));

            var mockSender = new Mock<ServiceBusSender>();
            _serviceBusClient.Setup(c => c.CreateSender(QueueName)).Returns(mockSender.Object);
            var mockBatch = ServiceBusModelFactory.ServiceBusMessageBatch(
                256_000, new List<ServiceBusMessage>(), new CreateMessageBatchOptions(), _ => true);
            mockSender.Setup(s => s.CreateMessageBatchAsync(It.IsAny<CancellationToken>())).ReturnsAsync(mockBatch);
            mockSender.Setup(s => s.SendMessagesAsync(It.IsAny<ServiceBusMessageBatch>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var sut = new AzureServiceBusDeadLetterQueueService(_clientFactory.Object, _managedWorkStatusManager.Object, _logger.Object);

            // Act: must not throw despite every abandon failing.
            var act = async () => await sut.ResubmitMessagesAsync(QueueName, new[] { targetSeq });
            _ = await Record.ExceptionAsync(async () => await act());

            // The target was still resubmitted and completed.
            _receiver.Verify(
                r => r.CompleteMessageAsync(
                    It.Is<ServiceBusReceivedMessage>(m => m.SequenceNumber == targetSeq),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task ResubmitMessagesAsync_ForwardsBodyVerbatim_WhenWorkRequestDeserialisesToNull()
        {
            // Arrange: a DLQ message whose body does not match WorkRequest, so
            // JsonConvert.DeserializeObject<WorkRequest> returns null. Before the fix,
            // this branch skipped the send but still completed (removed) the message
            // from the DLQ - a silent message-loss window.
            const long targetSeq = 42;
            var callCount = 0;

            _receiver
                .Setup(r => r.ReceiveMessagesAsync(
                    It.IsAny<int>(),
                    It.IsAny<TimeSpan?>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() =>
                {
                    callCount++;
                    if (callCount > 1) return new List<ServiceBusReceivedMessage>();
                    return new List<ServiceBusReceivedMessage>
                    {
                        ServiceBusModelFactory.ServiceBusReceivedMessage(
                            body: BinaryData.FromString("null"),
                            sequenceNumber: targetSeq)
                    };
                });

            var mockSender = new Mock<ServiceBusSender>();
            _serviceBusClient
                .Setup(c => c.CreateSender(QueueName))
                .Returns(mockSender.Object);

            var sentMessages = new List<ServiceBusMessage>();
            var mockBatch = ServiceBusModelFactory.ServiceBusMessageBatch(
                256_000,
                sentMessages,
                new CreateMessageBatchOptions(),
                _ => true);

            mockSender
                .Setup(s => s.CreateMessageBatchAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockBatch);

            mockSender
                .Setup(s => s.SendMessagesAsync(
                    It.IsAny<ServiceBusMessageBatch>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _receiver
                .Setup(r => r.CompleteMessageAsync(
                    It.IsAny<ServiceBusReceivedMessage>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var sut = new AzureServiceBusDeadLetterQueueService(_clientFactory.Object, _managedWorkStatusManager.Object, _logger.Object);

            // Act
            await sut.ResubmitMessagesAsync(QueueName, new[] { targetSeq });

            // Assert: the message was forwarded to the main queue (not silently dropped)...
            Assert.Single(sentMessages);
            Assert.Equal("null", sentMessages[0].Body.ToString());

            // ...and only then completed from the DLQ.
            _receiver.Verify(
                r => r.CompleteMessageAsync(
                    It.Is<ServiceBusReceivedMessage>(m => m.SequenceNumber == targetSeq),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }


        [Fact]
        public async Task ResubmitMessagesAsync_ForwardsBodyVerbatim_WhenBodyIsMalformedJson()
        {
            // Arrange: a DLQ message with a body that is not valid JSON, so
            // JsonConvert.DeserializeObject<WorkRequest> throws. Without the guard,
            // that exception would abort the whole resubmit and complete nothing.
            const long targetSeq = 77;
            const string malformed = "{not-valid-json";
            var callCount = 0;

            _receiver
                .Setup(r => r.ReceiveMessagesAsync(
                    It.IsAny<int>(),
                    It.IsAny<TimeSpan?>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() =>
                {
                    callCount++;
                    if (callCount > 1) return new List<ServiceBusReceivedMessage>();
                    return new List<ServiceBusReceivedMessage>
                    {
                        ServiceBusModelFactory.ServiceBusReceivedMessage(
                            body: BinaryData.FromString(malformed),
                            sequenceNumber: targetSeq)
                    };
                });

            var mockSender = new Mock<ServiceBusSender>();
            _serviceBusClient
                .Setup(c => c.CreateSender(QueueName))
                .Returns(mockSender.Object);

            var sentMessages = new List<ServiceBusMessage>();
            var mockBatch = ServiceBusModelFactory.ServiceBusMessageBatch(
                256_000,
                sentMessages,
                new CreateMessageBatchOptions(),
                _ => true);

            mockSender
                .Setup(s => s.CreateMessageBatchAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockBatch);
            mockSender
                .Setup(s => s.SendMessagesAsync(It.IsAny<ServiceBusMessageBatch>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            _receiver
                .Setup(r => r.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var sut = new AzureServiceBusDeadLetterQueueService(_clientFactory.Object, _managedWorkStatusManager.Object, _logger.Object);

            // Act: must not throw despite the malformed body.
            await sut.ResubmitMessagesAsync(QueueName, new[] { targetSeq });

            // Assert: forwarded verbatim, then completed.
            Assert.Single(sentMessages);
            Assert.Equal(malformed, sentMessages[0].Body.ToString());
            _receiver.Verify(
                r => r.CompleteMessageAsync(
                    It.Is<ServiceBusReceivedMessage>(m => m.SequenceNumber == targetSeq),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }


        [Fact]
        public async Task ResubmitMessagesAsync_MixedBatch_ForwardsAllAndCompletesAll()
        {
            // A single batch mixing valid WorkRequests with an unparseable ("null") body
            // and a malformed body. One bad message must not abort the batch or drop its
            // healthy neighbours: every message must be forwarded and then completed.
            const string validBodyA = "{\"FaultedCount\":5}";
            const string validBodyB = "{\"FaultedCount\":9}";
            const string malformed = "{not-valid-json";
            var targetSeqs = new long[] { 1, 2, 3, 4 };
            var callCount = 0;

            _receiver
                .Setup(r => r.ReceiveMessagesAsync(
                    It.IsAny<int>(),
                    It.IsAny<TimeSpan?>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() =>
                {
                    if (callCount++ > 0) return new List<ServiceBusReceivedMessage>();
                    return new List<ServiceBusReceivedMessage>
                    {
                        ServiceBusModelFactory.ServiceBusReceivedMessage(body: BinaryData.FromString(validBodyA), sequenceNumber: 1),
                        ServiceBusModelFactory.ServiceBusReceivedMessage(body: BinaryData.FromString("null"), sequenceNumber: 2),
                        ServiceBusModelFactory.ServiceBusReceivedMessage(body: BinaryData.FromString(malformed), sequenceNumber: 3),
                        ServiceBusModelFactory.ServiceBusReceivedMessage(body: BinaryData.FromString(validBodyB), sequenceNumber: 4),
                    };
                });

            var mockSender = new Mock<ServiceBusSender>();
            _serviceBusClient.Setup(c => c.CreateSender(QueueName)).Returns(mockSender.Object);

            var sentMessages = new List<ServiceBusMessage>();
            var mockBatch = ServiceBusModelFactory.ServiceBusMessageBatch(
                256_000, sentMessages, new CreateMessageBatchOptions(), _ => true);
            mockSender.Setup(s => s.CreateMessageBatchAsync(It.IsAny<CancellationToken>())).ReturnsAsync(mockBatch);
            mockSender.Setup(s => s.SendMessagesAsync(It.IsAny<ServiceBusMessageBatch>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            _receiver.Setup(r => r.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var sut = new AzureServiceBusDeadLetterQueueService(_clientFactory.Object, _managedWorkStatusManager.Object, _logger.Object);

            // Act: must not throw even though the batch mixes good and bad messages.
            await sut.ResubmitMessagesAsync(QueueName, targetSeqs);

            // Every message was forwarded, not just the parseable ones.
            Assert.Equal(4, sentMessages.Count);

            var sentBodies = sentMessages.Select(m => m.Body.ToString()).ToList();
            // The unparseable and malformed bodies are forwarded verbatim.
            Assert.Contains("null", sentBodies);
            Assert.Contains(malformed, sentBodies);

            // The two valid WorkRequests are forwarded with their fault count reset to 0.
            var reprocessed = sentBodies
                .Where(b => b != "null" && b != malformed)
                .Select(b => JsonConvert.DeserializeObject<WorkRequest>(b))
                .ToList();
            Assert.Equal(2, reprocessed.Count);
            Assert.All(reprocessed, w => Assert.True(w != null && w.FaultedCount == 0,
                "valid WorkRequests must have their fault count reset for a fresh set of attempts"));

            // Every message was completed (removed) from the DLQ.
            _receiver.Verify(
                r => r.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()),
                Times.Exactly(4),
                "every scanned target must be completed once it has been forwarded");
        }

        [Fact]
        public async Task ResubmitTopMessagesAsync_ReturnsZero_WhenMaxCountNotPositive()
        {
            var sut = new AzureServiceBusDeadLetterQueueService(_clientFactory.Object, _managedWorkStatusManager.Object, _logger.Object);

            var result = await sut.ResubmitTopMessagesAsync(QueueName, 0, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(0, result.Resubmitted);
            _receiver.Verify(
                r => r.ReceiveMessagesAsync(It.IsAny<int>(), It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task ResubmitTopMessagesAsync_DrainsRequestedCount_WhenDlqHasEnough()
        {
            // Three messages requested, three available in one batch: all resubmitted and completed.
            var callCount = 0;
            _receiver
                .Setup(r => r.ReceiveMessagesAsync(It.IsAny<int>(), It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() =>
                {
                    callCount++;
                    if (callCount > 1) return new List<ServiceBusReceivedMessage>();
                    return new List<ServiceBusReceivedMessage> { DrainMsg(1), DrainMsg(2), DrainMsg(3) };
                });

            var mockSender = new Mock<ServiceBusSender>();
            _serviceBusClient.Setup(c => c.CreateSender(QueueName)).Returns(mockSender.Object);
            mockSender
                .Setup(s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            _receiver
                .Setup(r => r.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var sut = new AzureServiceBusDeadLetterQueueService(_clientFactory.Object, _managedWorkStatusManager.Object, _logger.Object);

            var result = await sut.ResubmitTopMessagesAsync(QueueName, 3, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(3, result.Resubmitted);
            mockSender.Verify(s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()), Times.Exactly(3));
            _receiver.Verify(r => r.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()), Times.Exactly(3));
        }

        [Fact]
        public async Task ResubmitTopMessagesAsync_StopsAtAvailable_WhenFewerThanRequested()
        {
            // Ten requested, only three exist: drains the three that are there.
            var callCount = 0;
            _receiver
                .Setup(r => r.ReceiveMessagesAsync(It.IsAny<int>(), It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() =>
                {
                    callCount++;
                    if (callCount > 1) return new List<ServiceBusReceivedMessage>();
                    return new List<ServiceBusReceivedMessage> { DrainMsg(1), DrainMsg(2), DrainMsg(3) };
                });

            var mockSender = new Mock<ServiceBusSender>();
            _serviceBusClient.Setup(c => c.CreateSender(QueueName)).Returns(mockSender.Object);
            mockSender
                .Setup(s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            _receiver
                .Setup(r => r.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var sut = new AzureServiceBusDeadLetterQueueService(_clientFactory.Object, _managedWorkStatusManager.Object, _logger.Object);

            var result = await sut.ResubmitTopMessagesAsync(QueueName, 10, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(3, result.Resubmitted);
            _receiver.Verify(r => r.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()), Times.Exactly(3));
        }

        [Fact]
        public async Task ResubmitTopMessagesAsync_AbandonsOnSendFailure_AndDoesNotComplete()
        {
            // Send fails for every message: each is abandoned (stays in the DLQ), none completed.
            var callCount = 0;
            _receiver
                .Setup(r => r.ReceiveMessagesAsync(It.IsAny<int>(), It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() =>
                {
                    callCount++;
                    if (callCount > 1) return new List<ServiceBusReceivedMessage>();
                    return new List<ServiceBusReceivedMessage> { DrainMsg(1), DrainMsg(2) };
                });

            var mockSender = new Mock<ServiceBusSender>();
            _serviceBusClient.Setup(c => c.CreateSender(QueueName)).Returns(mockSender.Object);
            mockSender
                .Setup(s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("send failed"));
            _receiver
                .Setup(r => r.AbandonMessageAsync(
                    It.IsAny<ServiceBusReceivedMessage>(),
                    It.IsAny<IDictionary<string, object>>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var sut = new AzureServiceBusDeadLetterQueueService(_clientFactory.Object, _managedWorkStatusManager.Object, _logger.Object);

            var result = await sut.ResubmitTopMessagesAsync(QueueName, 2, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(0, result.Resubmitted);
            _receiver.Verify(
                r => r.AbandonMessageAsync(
                    It.IsAny<ServiceBusReceivedMessage>(),
                    It.IsAny<IDictionary<string, object>>(),
                    It.IsAny<CancellationToken>()),
                Times.Exactly(2));
            _receiver.Verify(
                r => r.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task ResubmitTopMessagesAsync_ContinuesDraining_WhenBatchShorterThanRequested()
        {
            // Service Bus can return fewer messages than requested even when more
            // exist. The drain must not stop on a short batch; it should keep going
            // until an empty batch signals the DLQ is drained. Here 5 are requested,
            // delivered as a short batch of 2 then a batch of 3.
            var callCount = 0;
            _receiver
                .Setup(r => r.ReceiveMessagesAsync(It.IsAny<int>(), It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() =>
                {
                    callCount++;
                    if (callCount == 1)
                    {
                        return new List<ServiceBusReceivedMessage> { DrainMsg(1), DrainMsg(2) };
                    }
                    if (callCount == 2)
                    {
                        return new List<ServiceBusReceivedMessage> { DrainMsg(3), DrainMsg(4), DrainMsg(5) };
                    }
                    return new List<ServiceBusReceivedMessage>();
                });

            var mockSender = new Mock<ServiceBusSender>();
            _serviceBusClient.Setup(c => c.CreateSender(QueueName)).Returns(mockSender.Object);
            mockSender
                .Setup(s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            _receiver
                .Setup(r => r.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var sut = new AzureServiceBusDeadLetterQueueService(_clientFactory.Object, _managedWorkStatusManager.Object, _logger.Object);

            var result = await sut.ResubmitTopMessagesAsync(QueueName, 5, cancellationToken: TestContext.Current.CancellationToken);

            // A premature break on the short first batch would drain only 2.
            Assert.Equal(5, result.Resubmitted);
        }

        [Fact]
        public async Task ResubmitTopMessagesAsync_PreservesCorrelationIdAndApplicationProperties()
        {
            var source = ServiceBusModelFactory.ServiceBusReceivedMessage(
                body: BinaryData.FromString("{\"WorkId\":\"1\"}"),
                sequenceNumber: 1,
                lockedUntil: DateTimeOffset.UtcNow.AddMinutes(5),
                correlationId: "corr-1",
                properties: new Dictionary<string, object> { ["tenant"] = "acme" });

            var callCount = 0;
            _receiver
                .Setup(r => r.ReceiveMessagesAsync(It.IsAny<int>(), It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() =>
                {
                    callCount++;
                    return callCount == 1
                        ? new List<ServiceBusReceivedMessage> { source }
                        : new List<ServiceBusReceivedMessage>();
                });

            var sentMessages = new List<ServiceBusMessage>();
            var mockSender = new Mock<ServiceBusSender>();
            _serviceBusClient.Setup(c => c.CreateSender(QueueName)).Returns(mockSender.Object);
            mockSender
                .Setup(s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()))
                .Callback<ServiceBusMessage, CancellationToken>((m, _) => sentMessages.Add(m))
                .Returns(Task.CompletedTask);
            _receiver
                .Setup(r => r.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var sut = new AzureServiceBusDeadLetterQueueService(_clientFactory.Object, _managedWorkStatusManager.Object, _logger.Object);

            await sut.ResubmitTopMessagesAsync(QueueName, 1, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Single(sentMessages);
            Assert.Equal("corr-1", sentMessages[0].CorrelationId);
            Assert.Contains("tenant", sentMessages[0].ApplicationProperties.Keys);
            Assert.Equal("acme", sentMessages[0].ApplicationProperties["tenant"] as string);
        }

        [Fact]
        public async Task ResubmitTopMessagesAsync_Terminates_WhenMessageAlwaysFailsToResubmit()
        {
            // A poison message: SendMessageAsync always throws, so it is abandoned and
            // (in production) becomes available again immediately. This mock redelivers
            // it on every receive. Without the progress guard the drain would spin
            // forever; the guard must stop once a batch brings only already-attempted
            // messages.
            const long poisonSeq = 999;
            var receiveCalls = 0;

            _receiver
                .Setup(r => r.ReceiveMessagesAsync(It.IsAny<int>(), It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() =>
                {
                    receiveCalls++;
                    // Safety net so a regressed guard fails the assertion instead of hanging.
                    if (receiveCalls > 20) return new List<ServiceBusReceivedMessage>();
                    return new List<ServiceBusReceivedMessage> { DrainMsg(poisonSeq) };
                });

            var mockSender = new Mock<ServiceBusSender>();
            _serviceBusClient.Setup(c => c.CreateSender(QueueName)).Returns(mockSender.Object);
            mockSender
                .Setup(s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("send failed"));
            _receiver
                .Setup(r => r.AbandonMessageAsync(
                    It.IsAny<ServiceBusReceivedMessage>(),
                    It.IsAny<IDictionary<string, object>>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var sut = new AzureServiceBusDeadLetterQueueService(_clientFactory.Object, _managedWorkStatusManager.Object, _logger.Object);

            var result = await sut.ResubmitTopMessagesAsync(QueueName, 1000, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(0, result.Resubmitted);
            Assert.True(receiveCalls <= 3, "the progress guard must terminate the loop, not spin on the redelivered message");
        }

        [Fact]
        public async Task ResubmitTopMessagesAsync_TerminatesAtMaxCountAttempts_WhenDistinctMessagesAlwaysFailToResubmit()
        {
            // Every send fails, but unlike the single-poison case the DLQ keeps yielding
            // DISTINCT new messages on each receive (the broker is free to hand out unseen
            // messages ahead of redelivered abandons - Service Bus does not guarantee order).
            // The newlyAttempted==0 progress guard never trips here, so termination must come
            // from the maxCount attempt ceiling; otherwise the drain walks the whole DLQ under
            // a systemic send failure (main queue disabled / at quota / throttled).
            const int maxCount = 5;
            var nextSeq = 0L;
            var receiveCalls = 0;

            _receiver
                .Setup(r => r.ReceiveMessagesAsync(It.IsAny<int>(), It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((int requested, TimeSpan _, CancellationToken __) =>
                {
                    receiveCalls++;
                    // Safety net so a regressed ceiling fails the assertions instead of hanging.
                    if (receiveCalls > 50) return new List<ServiceBusReceivedMessage>();
                    // Always return fresh, never-before-seen messages (at most 2 per call, so
                    // reaching the ceiling takes several receives and exercises the shrinking budget).
                    var count = Math.Min(requested, 2);
                    var batch = new List<ServiceBusReceivedMessage>();
                    for (var i = 0; i < count; i++) batch.Add(DrainMsg(++nextSeq));
                    return batch;
                });

            var mockSender = new Mock<ServiceBusSender>();
            _serviceBusClient.Setup(c => c.CreateSender(QueueName)).Returns(mockSender.Object);
            mockSender
                .Setup(s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("send failed"));
            _receiver
                .Setup(r => r.AbandonMessageAsync(
                    It.IsAny<ServiceBusReceivedMessage>(),
                    It.IsAny<IDictionary<string, object>>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var sut = new AzureServiceBusDeadLetterQueueService(_clientFactory.Object, _managedWorkStatusManager.Object, _logger.Object);

            var result = await sut.ResubmitTopMessagesAsync(QueueName, maxCount, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(0, result.Resubmitted);
            // No more than maxCount distinct messages may be attempted, and the loop must stop
            // within a handful of receives - NOT walk the effectively unbounded stream.
            Assert.True(nextSeq <= maxCount, "at most maxCount distinct messages may be attempted");
            Assert.True(receiveCalls <= maxCount, "the attempt ceiling must terminate the drain, not the safety net");
        }

        [Fact]
        public async Task ResubmitTopMessagesAsync_StopsBeforeSending_WhenLockIsAtRisk()
        {
            // The batch tail is past its lock. Sending it would post a copy we then
            // cannot Complete, which redelivery duplicates. The guard must stop before
            // sending the at-risk message.
            var healthy1 = DrainMsg(1);
            var healthy2 = DrainMsg(2);
            // Not expired, but inside the 15s default safety margin: this proves the
            // margin itself is enforced (a regression dropping it to zero would send this).
            var expired = DrainMsg(3, lockedUntil: DateTimeOffset.UtcNow.AddSeconds(5));

            var receiveCalls = 0;
            _receiver
                .Setup(r => r.ReceiveMessagesAsync(It.IsAny<int>(), It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() =>
                {
                    receiveCalls++;
                    return receiveCalls == 1
                        ? new List<ServiceBusReceivedMessage> { healthy1, healthy2, expired }
                        : new List<ServiceBusReceivedMessage>();
                });

            var sentMessages = new List<ServiceBusMessage>();
            var mockSender = new Mock<ServiceBusSender>();
            _serviceBusClient.Setup(c => c.CreateSender(QueueName)).Returns(mockSender.Object);
            mockSender
                .Setup(s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()))
                .Callback<ServiceBusMessage, CancellationToken>((m, _) => sentMessages.Add(m))
                .Returns(Task.CompletedTask);
            _receiver
                .Setup(r => r.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            _receiver
                .Setup(r => r.AbandonMessageAsync(
                    It.IsAny<ServiceBusReceivedMessage>(),
                    It.IsAny<IDictionary<string, object>>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var sut = new AzureServiceBusDeadLetterQueueService(_clientFactory.Object, _managedWorkStatusManager.Object, _logger.Object);

            var result = await sut.ResubmitTopMessagesAsync(QueueName, 3, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(2, result.Resubmitted);
            Assert.Equal(2, sentMessages.Count);
            _receiver.Verify(
                r => r.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()),
                Times.Exactly(2));
            // The at-risk message is neither sent nor abandoned (abandon needs the lock too)
            // it is left to expire back to the DLQ for a later pass.
            _receiver.Verify(
                r => r.AbandonMessageAsync(
                    It.IsAny<ServiceBusReceivedMessage>(),
                    It.IsAny<IDictionary<string, object>>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task ResubmitTopMessagesAsync_DoesNotResendWithinCall_WhenCompleteFailsAfterSend()
        {
            // Send succeeds but Complete throws (lock lost in the gap). The message
            // becomes available again immediately and is redelivered on the next
            // receive. It must NOT be sent a second time within the same call.
            var callCount = 0;
            _receiver
                .Setup(r => r.ReceiveMessagesAsync(It.IsAny<int>(), It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() =>
                {
                    callCount++;
                    // Redeliver the same message on the first two receives, then empty.
                    return callCount <= 2
                        ? new List<ServiceBusReceivedMessage> { DrainMsg(7) }
                        : new List<ServiceBusReceivedMessage>();
                });

            var sendCount = 0;
            var mockSender = new Mock<ServiceBusSender>();
            _serviceBusClient.Setup(c => c.CreateSender(QueueName)).Returns(mockSender.Object);
            mockSender
                .Setup(s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()))
                .Callback(() => sendCount++)
                .Returns(Task.CompletedTask);
            _receiver
                .Setup(r => r.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ServiceBusException("lock lost", ServiceBusFailureReason.MessageLockLost));
            _receiver
                .Setup(r => r.AbandonMessageAsync(
                    It.IsAny<ServiceBusReceivedMessage>(),
                    It.IsAny<IDictionary<string, object>>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var sut = new AzureServiceBusDeadLetterQueueService(_clientFactory.Object, _managedWorkStatusManager.Object, _logger.Object);

            var result = await sut.ResubmitTopMessagesAsync(QueueName, 5, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(0, result.Resubmitted);
            Assert.Equal(1, sendCount);
        }

        [Fact]
        public async Task ResubmitTopMessagesAsync_Propagates_WhenCancelledMidSend()
        {
            // Cancellation during send must surface as OperationCanceledException, not
            // be swallowed and reported as a normal (successful) completion.
            using var cts = new CancellationTokenSource();

            _receiver
                .Setup(r => r.ReceiveMessagesAsync(It.IsAny<int>(), It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => new List<ServiceBusReceivedMessage> { DrainMsg(1) });

            var mockSender = new Mock<ServiceBusSender>();
            _serviceBusClient.Setup(c => c.CreateSender(QueueName)).Returns(mockSender.Object);
            mockSender
                .Setup(s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()))
                .Returns<ServiceBusMessage, CancellationToken>((_, _) =>
                {
                    cts.Cancel();
                    throw new OperationCanceledException(cts.Token);
                });

            var sut = new AzureServiceBusDeadLetterQueueService(_clientFactory.Object, _managedWorkStatusManager.Object, _logger.Object);

            Func<Task> act = () => sut.ResubmitTopMessagesAsync(QueueName, 5, cts.Token);

            await Assert.ThrowsAsync<OperationCanceledException>(act);
        }

        [Fact]
        public async Task ResubmitTopMessagesAsync_DrainsShortLockQueue_WhenAdminMarginUnavailable()
        {
            // The admin GetQueueAsync is not mocked here, so the configured-margin
            // lookup fails and the margin is derived from the observed lock window.
            // Messages have a short (8s) lock. A fixed 15s fallback would exceed the
            // lock and mark every message at-risk (draining nothing); the observed
            // derivation (~4s) must let them drain.
            var callCount = 0;
            _receiver
                .Setup(r => r.ReceiveMessagesAsync(It.IsAny<int>(), It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() =>
                {
                    callCount++;
                    if (callCount > 1) return new List<ServiceBusReceivedMessage>();
                    return new List<ServiceBusReceivedMessage>
                    {
                        DrainMsg(1, lockedUntil: DateTimeOffset.UtcNow.AddSeconds(8)),
                        DrainMsg(2, lockedUntil: DateTimeOffset.UtcNow.AddSeconds(8)),
                    };
                });

            var mockSender = new Mock<ServiceBusSender>();
            _serviceBusClient.Setup(c => c.CreateSender(QueueName)).Returns(mockSender.Object);
            mockSender
                .Setup(s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            _receiver
                .Setup(r => r.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var sut = new AzureServiceBusDeadLetterQueueService(_clientFactory.Object, _managedWorkStatusManager.Object, _logger.Object);

            var result = await sut.ResubmitTopMessagesAsync(QueueName, 5, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(2, result.Resubmitted);
        }

        [Fact]
        public async Task ResubmitTopMessagesAsync_ReportsNotEmpty_WhenZeroResubmittedButPeekFindsMessages()
        {
            // The reported bug: a transient empty ReceiveMessagesAsync (head momentarily
            // locked) resubmits 0, but the DLQ is NOT empty. A lock-independent peek finds a
            // message, so the result must report QueueConfirmedEmpty=false.
            _receiver
                .Setup(r => r.ReceiveMessagesAsync(It.IsAny<int>(), It.IsAny<TimeSpan?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ServiceBusReceivedMessage>()); // nothing received this pass
            _receiver
                .Setup(r => r.PeekMessagesAsync(It.IsAny<int>(), It.IsAny<long?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ServiceBusReceivedMessage> { DrainMsg(1) }); // but the DLQ still has messages

            var mockSender = new Mock<ServiceBusSender>();
            _serviceBusClient.Setup(c => c.CreateSender(QueueName)).Returns(mockSender.Object);

            var sut = new AzureServiceBusDeadLetterQueueService(_clientFactory.Object, _managedWorkStatusManager.Object, _logger.Object);

            var result = await sut.ResubmitTopMessagesAsync(QueueName, 5, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(0, result.Resubmitted);
            Assert.False(result.QueueConfirmedEmpty);
        }

        [Fact]
        public async Task ResubmitTopMessagesAsync_ReportsEmpty_WhenZeroResubmittedAndPeekEmpty()
        {
            _receiver
                .Setup(r => r.ReceiveMessagesAsync(It.IsAny<int>(), It.IsAny<TimeSpan?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ServiceBusReceivedMessage>());
            // Constructor default: PeekMessagesAsync returns empty => confirmed empty.

            var mockSender = new Mock<ServiceBusSender>();
            _serviceBusClient.Setup(c => c.CreateSender(QueueName)).Returns(mockSender.Object);

            var sut = new AzureServiceBusDeadLetterQueueService(_clientFactory.Object, _managedWorkStatusManager.Object, _logger.Object);

            var result = await sut.ResubmitTopMessagesAsync(QueueName, 5, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(0, result.Resubmitted);
            Assert.True(result.QueueConfirmedEmpty);
        }

        [Fact]
        public async Task ResubmitTopMessagesAsync_DoesNotPeek_WhenSomethingWasResubmitted()
        {
            var callCount = 0;
            _receiver
                .Setup(r => r.ReceiveMessagesAsync(It.IsAny<int>(), It.IsAny<TimeSpan?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() =>
                {
                    if (callCount++ > 0) return new List<ServiceBusReceivedMessage>();
                    return new List<ServiceBusReceivedMessage> { DrainMsg(1) };
                });
            _receiver
                .Setup(r => r.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var mockSender = new Mock<ServiceBusSender>();
            _serviceBusClient.Setup(c => c.CreateSender(QueueName)).Returns(mockSender.Object);
            mockSender
                .Setup(s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var sut = new AzureServiceBusDeadLetterQueueService(_clientFactory.Object, _managedWorkStatusManager.Object, _logger.Object);

            var result = await sut.ResubmitTopMessagesAsync(QueueName, 5, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(1, result.Resubmitted);
            Assert.False(result.QueueConfirmedEmpty);
            _receiver.Verify(
                r => r.PeekMessagesAsync(It.IsAny<int>(), It.IsAny<long?>(), It.IsAny<CancellationToken>()),
                Times.Never,
                "no emptiness peek is needed when the pass made progress");
        }

        [Fact]
        public async Task ResubmitTopMessagesAsync_TreatsPeekFailureAsNotEmpty()
        {
            _receiver
                .Setup(r => r.ReceiveMessagesAsync(It.IsAny<int>(), It.IsAny<TimeSpan?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ServiceBusReceivedMessage>());
            _receiver
                .Setup(r => r.PeekMessagesAsync(It.IsAny<int>(), It.IsAny<long?>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ServiceBusException("peek failed", ServiceBusFailureReason.ServiceCommunicationProblem));

            var mockSender = new Mock<ServiceBusSender>();
            _serviceBusClient.Setup(c => c.CreateSender(QueueName)).Returns(mockSender.Object);

            var sut = new AzureServiceBusDeadLetterQueueService(_clientFactory.Object, _managedWorkStatusManager.Object, _logger.Object);

            var result = await sut.ResubmitTopMessagesAsync(QueueName, 5, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(0, result.Resubmitted);
            Assert.False(result.QueueConfirmedEmpty);
        }

        [Fact]
        public async Task ResubmitTopMessagesAsync_RethrowsPeekCancellation_WhenTokenCancelled()
        {
            using var cts = new CancellationTokenSource();
            await cts.CancelAsync();

            _receiver
                .Setup(r => r.ReceiveMessagesAsync(It.IsAny<int>(), It.IsAny<TimeSpan?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ServiceBusReceivedMessage>());
            // Throw ONLY when the cancelled token is the one forwarded to the peek. If the
            // implementation stopped forwarding the token, the constructor's default peek
            // stub (returns empty) would match instead and no exception would be thrown, so
            // this also guards against the token no longer being propagated.
            _receiver
                .Setup(r => r.PeekMessagesAsync(It.IsAny<int>(), It.IsAny<long?>(), It.Is<CancellationToken>(ct => ct.IsCancellationRequested)))
                .ThrowsAsync(new OperationCanceledException());

            var mockSender = new Mock<ServiceBusSender>();
            _serviceBusClient.Setup(c => c.CreateSender(QueueName)).Returns(mockSender.Object);

            var sut = new AzureServiceBusDeadLetterQueueService(_clientFactory.Object, _managedWorkStatusManager.Object, _logger.Object);

            Func<Task> act = () => sut.ResubmitTopMessagesAsync(QueueName, 5, cts.Token);

            await Assert.ThrowsAsync<OperationCanceledException>(act);
        }
    }
}
