#nullable enable
using System.Text;
using Moq;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;
using Xunit;

namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.Test
{
    public class RabbitMqDeadLetterQueueServiceTests
    {
        private const string QueueName = "content-manager";
        private const string DlqName = "content-manager-DL";

        private readonly Mock<IRabbitMqClientFactory> _clientFactory = new();
        private readonly Mock<IConnection> _connection = new();
        private readonly Mock<IChannel> _model = new();
        private readonly Mock<IDateTimeProvider> _dateTimeProvider = new();

        public RabbitMqDeadLetterQueueServiceTests()
        {
            _clientFactory.Setup(f => f.CreateRabbitMqConnection()).Returns(_connection.Object);
            _connection.Setup(c => c.CreateChannelAsync(It.IsAny<CreateChannelOptions?>(), It.IsAny<CancellationToken>())).ReturnsAsync(_model.Object);
            _dateTimeProvider.SetupGet(d => d.UtcNow).Returns(new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc));
        }

        private RabbitMqDeadLetterQueueService CreateSut() =>
            new(_clientFactory.Object, _dateTimeProvider.Object);

        private static BasicGetResult CreateResult(ulong deliveryTag, WorkRequest? workRequest = null)
        {
            workRequest ??= new WorkRequest { WorkType = "work", SubmitDateTime = DateTimeOffset.UtcNow };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(workRequest));
            var props = new BasicProperties();
            return new BasicGetResult(deliveryTag, false, "exchange", "routingKey", 1, props, body);
        }

        [Fact]
        public async Task GetMessagesAsync_ReturnsAllUntilNull()
        {
            _model.SetupSequence(m => m.BasicGetAsync(DlqName, false, It.IsAny<CancellationToken>()))
                .ReturnsAsync(CreateResult(1))
                .ReturnsAsync(CreateResult(2))
                .ReturnsAsync((BasicGetResult?)null);

            var result = await CreateSut().GetMessagesAsync(QueueName);

            Assert.Equal(2, result.Count);
            Assert.Equal("1", result[0].MessageId);
            Assert.Equal("2", result[1].MessageId);
        }

        [Fact]
        public async Task GetMessagesAsync_RespectsMaxCount()
        {
            _model.Setup(m => m.BasicGetAsync(DlqName, false, It.IsAny<CancellationToken>())).ReturnsAsync(() => CreateResult(1));

            var result = await CreateSut().GetMessagesAsync(QueueName, maxCount: 3);

            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetMessagesAsync_AcceptsDlqSuffixedName()
        {
            _model.SetupSequence(m => m.BasicGetAsync(DlqName, false, It.IsAny<CancellationToken>()))
                .ReturnsAsync(CreateResult(1))
                .ReturnsAsync((BasicGetResult?)null);

            var result = await CreateSut().GetMessagesAsync(DlqName);

            Assert.Single(result);
            _model.Verify(m => m.BasicGetAsync(DlqName, false, It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [Fact]
        public async Task GetMessageAsync_ReturnsMatchingMessage()
        {
            _model.SetupSequence(m => m.BasicGetAsync(DlqName, false, It.IsAny<CancellationToken>()))
                .ReturnsAsync(CreateResult(1))
                .ReturnsAsync(CreateResult(7));

            var result = await CreateSut().GetMessageAsync(QueueName, 7);

            Assert.Equal("7", result.MessageId);
        }

        [Fact]
        public async Task ResubmitMessagesAsync_WithoutWaitTill_PublishesAndAcks()
        {
            var workRequest = new WorkRequest { WorkType = "work", FaultedCount = 5 };
            _model.SetupSequence(m => m.BasicGetAsync(DlqName, false, It.IsAny<CancellationToken>()))
                .ReturnsAsync(CreateResult(1, workRequest))
                .ReturnsAsync((BasicGetResult?)null);

            await CreateSut().ResubmitMessagesAsync(QueueName, new long[] { 1 });

            _model.Verify(m => m.BasicPublishAsync("publish-consumer-exchange", QueueName, false, It.IsAny<BasicProperties>(), It.IsAny<ReadOnlyMemory<byte>>(), It.IsAny<CancellationToken>()), Times.Once);
            _model.Verify(m => m.BasicAckAsync(1, false, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ResubmitMessagesAsync_WithWaitTill_SetsDelayHeader()
        {
            var workRequest = new WorkRequest { WorkType = "work", WaitTill = new DateTime(2026, 1, 1, 0, 1, 0, DateTimeKind.Utc) };
            BasicProperties? capturedProperties = null;
            _model.Setup(m => m.BasicPublishAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<BasicProperties>(), It.IsAny<ReadOnlyMemory<byte>>(), It.IsAny<CancellationToken>()))
                .Callback<string, string, bool, BasicProperties, ReadOnlyMemory<byte>, CancellationToken>((_, _, _, props, _, _) => capturedProperties = props);
            _model.SetupSequence(m => m.BasicGetAsync(DlqName, false, It.IsAny<CancellationToken>()))
                .ReturnsAsync(CreateResult(2, workRequest))
                .ReturnsAsync((BasicGetResult?)null);

            await CreateSut().ResubmitMessagesAsync(QueueName, new long[] { 2 });

            Assert.NotNull(capturedProperties);
            Assert.NotNull(capturedProperties!.Headers);
            Assert.True(capturedProperties.Headers.ContainsKey("x-delay"));
            Assert.Equal(60000d, Convert.ToDouble(capturedProperties.Headers["x-delay"]));
            _model.Verify(m => m.BasicAckAsync(2, false, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ResubmitMessagesAsync_NonMatchingMessage_StopsAndPublishesNothing()
        {
            _model.Setup(m => m.BasicGetAsync(DlqName, false, It.IsAny<CancellationToken>())).ReturnsAsync(CreateResult(99));

            await CreateSut().ResubmitMessagesAsync(QueueName, new long[] { 1 });

            _model.Verify(m => m.BasicPublishAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<BasicProperties>(), It.IsAny<ReadOnlyMemory<byte>>(), It.IsAny<CancellationToken>()), Times.Never);
            _model.Verify(m => m.BasicAckAsync(It.IsAny<ulong>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task DeleteMessageAsync_AcksMatchingMessage()
        {
            _model.SetupSequence(m => m.BasicGetAsync(DlqName, false, It.IsAny<CancellationToken>()))
                .ReturnsAsync(CreateResult(1))
                .ReturnsAsync(CreateResult(4));

            await CreateSut().DeleteMessageAsync(QueueName, 4);

            _model.Verify(m => m.BasicAckAsync(4, false, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAllMessagesAsync_AcksAllUntilNull()
        {
            _model.SetupSequence(m => m.BasicGetAsync(DlqName, false, It.IsAny<CancellationToken>()))
                .ReturnsAsync(CreateResult(1))
                .ReturnsAsync(CreateResult(2))
                .ReturnsAsync((BasicGetResult?)null);

            await CreateSut().DeleteAllMessagesAsync(QueueName);

            _model.Verify(m => m.BasicAckAsync(1, false, It.IsAny<CancellationToken>()), Times.Once);
            _model.Verify(m => m.BasicAckAsync(2, false, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ResubmitTopMessagesAsync_DoesNotConfirmEmpty_WhenDlqHasNoReadyMessages()
        {
            // RabbitMQ cannot confirm true emptiness cheaply (ready-count excludes unacked),
            // so a drained pass must report not-confirmed-empty and must not consult the
            // ready-count to claim otherwise.
            _model.Setup(m => m.BasicGetAsync(DlqName, false, It.IsAny<CancellationToken>())).ReturnsAsync((BasicGetResult?)null);

            var result = await CreateSut().ResubmitTopMessagesAsync(QueueName, 10, TestContext.Current.CancellationToken);

            Assert.Equal(0, result.Resubmitted);
            Assert.False(result.QueueConfirmedEmpty, "RabbitMQ never claims confirmed-empty from a ready-count");
        }

        [Fact]
        public async Task ResubmitTopMessagesAsync_ReturnsConfirmedEmpty_WhenMaxCountNotPositive()
        {
            var result = await CreateSut().ResubmitTopMessagesAsync(QueueName, 0, TestContext.Current.CancellationToken);

            Assert.Equal(0, result.Resubmitted);
            Assert.True(result.QueueConfirmedEmpty, "a no-op request is terminal so a looping caller stops");
        }
    }
}
