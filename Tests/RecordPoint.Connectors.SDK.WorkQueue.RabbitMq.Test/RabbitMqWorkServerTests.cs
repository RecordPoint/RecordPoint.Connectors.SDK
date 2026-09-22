#nullable enable
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Toggles;
using RecordPoint.Connectors.SDK.Work;
using Xunit;

namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.Test
{
    /// <summary>
    /// Minimal IQueueableWork implementation used only so that the work server can
    /// resolve it from the service provider and match it against an operation type.
    /// </summary>
    public sealed class FakeQueueableWork : IQueueableWork
    {
        public string WorkType => "content-manager";
        public WorkRequest WorkRequest => throw new NotImplementedException();
        public string Id => throw new NotImplementedException();
        public DateTimeOffset StartDateTime => throw new NotImplementedException();
        public bool HasResult => throw new NotImplementedException();
        public WorkResultType ResultType => throw new NotImplementedException();
        public string ResultReason => throw new NotImplementedException();
        public string ResultReasonDetails => throw new NotImplementedException();
        public DateTimeOffset FinishDateTime => throw new NotImplementedException();
        public Exception Exception => throw new NotImplementedException();
        public TimeSpan WorkDuration => throw new NotImplementedException();
        public WorkResult GetWorkResult() => throw new NotImplementedException();
        public Task RunWorkRequestAsync(WorkRequest workRequest, CancellationToken cancellationToken) => throw new NotImplementedException();
        public void Dispose() { }
    }

    public class RabbitMqWorkServerTests : IDisposable
    {
        private const string WorkType = "content-manager";
        private const string QueueName = "test-content-manager";

        private readonly Mock<IWorkQueueClient> _workQueueClient = new();
        private readonly Mock<IServiceProvider> _serviceProvider = new();
        private readonly Mock<ISystemContext> _systemContext = new();
        private readonly Mock<IQueueableWorkManager> _workManager = new();
        private readonly Mock<IRabbitMqClientFactory> _clientFactory = new();
        private readonly Mock<IConnection> _connection = new();
        private readonly Mock<IChannel> _model = new();
        private readonly Mock<IObservabilityScope> _observabilityScope = new();
        private readonly Mock<ITelemetryTracker> _telemetryTracker = new();
        private readonly Mock<IToggleProvider> _toggleProvider = new();
        private readonly RabbitMqOptions _options = new()
        {
            QueuePrefix = "test",
            KillswitchCheckInterval = 30,
            ServiceShutdownDelay = 0
        };

        private AsyncEventingBasicConsumer? _capturedConsumer;
        private volatile bool _killswitch;
        private int _consumeCount;
        private int _cancelCount;
        private int _ackCount;
        private int _nackCount;
        private int _exceptionCount;

        private RabbitMqWorkServer? _server;

        public RabbitMqWorkServerTests()
        {
            _clientFactory.Setup(f => f.CreateRabbitMqConnection()).Returns(_connection.Object);
            _connection.Setup(c => c.CreateChannelAsync(It.IsAny<CreateChannelOptions?>(), It.IsAny<CancellationToken>())).ReturnsAsync(_model.Object);
            _model.SetupGet(m => m.IsOpen).Returns(true);
            _model.SetupGet(m => m.IsClosed).Returns(false);

            _observabilityScope
                .Setup(o => o.BeginScope(It.IsAny<Dimensions?>(), It.IsAny<Measures?>()))
                .Returns(Moq.Mock.Of<IDisposable>());

            _systemContext.Setup(s => s.GetCompanyName()).Returns("company");
            _systemContext.Setup(s => s.GetConnectorName()).Returns("connector");
            _systemContext.Setup(s => s.GetServiceName()).Returns("service");

            _toggleProvider.Setup(t => t.GetToggleBool(It.IsAny<string>(), It.IsAny<bool>())).Returns(() => _killswitch);

            _workQueueClient.Setup(c => c.SubmitWorkAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            _serviceProvider
                .Setup(sp => sp.GetService(typeof(IEnumerable<IQueueableWork>)))
                .Returns(new List<IQueueableWork> { new FakeQueueableWork() });

            _model.Setup(m => m.BasicConsumeAsync(
                    It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<string>(),
                    It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<IDictionary<string, object?>>(), It.IsAny<IAsyncBasicConsumer>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync("tag")
                .Callback<string, bool, string, bool, bool, IDictionary<string, object?>, IAsyncBasicConsumer, CancellationToken>(
                    (q, autoAck, ct, noLocal, exclusive, args, consumer, cancellationToken) =>
                    {
                        _capturedConsumer = (AsyncEventingBasicConsumer)consumer;
                        _capturedConsumer.HandleBasicConsumeOkAsync("tag", cancellationToken).GetAwaiter().GetResult();
                        Interlocked.Increment(ref _consumeCount);
                    });

            _model.Setup(m => m.BasicCancelAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Callback(() => Interlocked.Increment(ref _cancelCount));
            _model.Setup(m => m.BasicAckAsync(It.IsAny<ulong>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Callback(() => Interlocked.Increment(ref _ackCount));
            _model.Setup(m => m.BasicNackAsync(It.IsAny<ulong>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .Callback(() => Interlocked.Increment(ref _nackCount));
            _telemetryTracker.Setup(t => t.TrackException(It.IsAny<Exception>(), It.IsAny<Dimensions?>(), It.IsAny<Measures?>()))
                .Callback(() => Interlocked.Increment(ref _exceptionCount));
        }

        private RabbitMqWorkServer CreateServer(IList<Type>? operationTypes = null)
        {
            _server = new RabbitMqWorkServer(
                _workQueueClient.Object,
                _serviceProvider.Object,
                _systemContext.Object,
                _workManager.Object,
                _clientFactory.Object,
                Options.Create(_options),
                _observabilityScope.Object,
                _telemetryTracker.Object,
                _toggleProvider.Object,
                operationTypes ?? new List<Type> { typeof(FakeQueueableWork) });
            return _server;
        }

        private async Task<RabbitMqWorkServer> StartServerAsync(IList<Type>? operationTypes = null)
        {
            var server = CreateServer(operationTypes);
            await server.StartAsync(CancellationToken.None);
            await WaitUntil(() => _consumeCount > 0);
            return server;
        }

        private static async Task WaitUntil(Func<bool> condition, int timeoutMs = 3000)
        {
            var sw = Stopwatch.StartNew();
            while (!condition() && sw.ElapsedMilliseconds < timeoutMs)
            {
                await Task.Delay(10);
            }
        }

        private void DeliverMessage(WorkRequest? workRequest, ulong deliveryTag = 1)
        {
            var json = workRequest == null ? "null" : JsonConvert.SerializeObject(workRequest);
            var body = Encoding.Default.GetBytes(json);
            _capturedConsumer!.HandleBasicDeliverAsync("tag", deliveryTag, false, "exchange", "rk", new BasicProperties(), body, CancellationToken.None)
                .GetAwaiter().GetResult();
        }

        [Fact]
        public void Constructor_WithNonQueueableType_Throws()
        {
            Assert.Throws<ArgumentException>(() => CreateServer(new List<Type> { typeof(string) }));
        }

        [Fact]
        public void Constructor_WithValidType_DoesNotThrow()
        {
            var server = CreateServer(new List<Type> { typeof(FakeQueueableWork) });
            Assert.NotNull(server);
        }

        [Fact]
        public async Task Execute_StartsConsumer_AndDeclaresQueues()
        {
            await StartServerAsync();

            Assert.True(_consumeCount > 0);
            _model.Verify(m => m.ExchangeDeclareAsync("publish-consumer-exchange", "x-delayed-message", true, false, It.IsAny<IDictionary<string, object?>>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
            _model.Verify(m => m.QueueDeclareAsync(QueueName, true, false, false, It.IsAny<IDictionary<string, object?>>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
            _model.Verify(m => m.QueueBindAsync(QueueName, "publish-consumer-exchange", It.IsAny<string>(), It.IsAny<IDictionary<string, object?>>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Execute_WithMaxDegreeOfParallelism_SetsBasicQos()
        {
            _options.MaxDegreeOfParallelism = 5;

            await StartServerAsync();

            _model.Verify(m => m.BasicQosAsync(0, (ushort)5, false, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Execute_WithoutMaxDegreeOfParallelism_DoesNotSetBasicQos()
        {
            await StartServerAsync();

            _model.Verify(m => m.BasicQosAsync(It.IsAny<uint>(), It.IsAny<ushort>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Execute_WithNoMatchingOperations_CreatesNoConsumers()
        {
            _serviceProvider
                .Setup(sp => sp.GetService(typeof(IEnumerable<IQueueableWork>)))
                .Returns(new List<IQueueableWork>());

            // Empty operation list => default operation types are used, none of which resolve.
            var server = CreateServer(new List<Type>());
            await server.StartAsync(CancellationToken.None);
            await Task.Delay(100, cancellationToken: TestContext.Current.CancellationToken);

            _model.Verify(m => m.BasicConsumeAsync(
                It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<string>(),
                It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<IDictionary<string, object?>>(), It.IsAny<IAsyncBasicConsumer>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task HandleMessage_Complete_Acks()
        {
            _workManager.Setup(m => m.HandleWorkRequestAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(WorkResult.Complete());
            await StartServerAsync();

            DeliverMessage(new WorkRequest { WorkType = WorkType });
            await WaitUntil(() => _ackCount > 0);

            _model.Verify(m => m.BasicAckAsync(1, false, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task HandleMessage_Abandoned_Acks()
        {
            _workManager.Setup(m => m.HandleWorkRequestAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(WorkResult.Abandoned("abandon"));
            await StartServerAsync();

            DeliverMessage(new WorkRequest { WorkType = WorkType });
            await WaitUntil(() => _ackCount > 0);

            _model.Verify(m => m.BasicAckAsync(1, false, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task HandleMessage_Failed_Nacks()
        {
            _workManager.Setup(m => m.HandleWorkRequestAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(WorkResult.Failed("failed"));
            await StartServerAsync();

            DeliverMessage(new WorkRequest { WorkType = WorkType });
            await WaitUntil(() => _nackCount > 0);

            _model.Verify(m => m.BasicNackAsync(1, false, false, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task HandleMessage_DeadLetter_Nacks()
        {
            _workManager.Setup(m => m.HandleWorkRequestAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(WorkResult.DeadLetter("dead", new Exception("boom")));
            await StartServerAsync();

            DeliverMessage(new WorkRequest { WorkType = WorkType });
            await WaitUntil(() => _nackCount > 0);

            _model.Verify(m => m.BasicNackAsync(1, false, false, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task HandleMessage_Deferred_ResubmitsAndAcks()
        {
            _workManager.Setup(m => m.HandleWorkRequestAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(WorkResult.Defer("defer", DateTimeOffset.UtcNow.AddMinutes(5)));
            await StartServerAsync();

            DeliverMessage(new WorkRequest { WorkType = WorkType });
            await WaitUntil(() => _ackCount > 0);

            _workQueueClient.Verify(c => c.SubmitWorkAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()), Times.Once);
            _model.Verify(m => m.BasicAckAsync(1, false, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task HandleMessage_DeferredWithNullWaitTill_ResubmitsAndAcks()
        {
            _workManager.Setup(m => m.HandleWorkRequestAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(WorkResult.Defer("defer", null));
            await StartServerAsync();

            WorkRequest? submitted = null;
            _workQueueClient.Setup(c => c.SubmitWorkAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
                .Callback<WorkRequest, CancellationToken>((wr, _) => submitted = wr)
                .Returns(Task.CompletedTask);

            DeliverMessage(new WorkRequest { WorkType = WorkType });
            await WaitUntil(() => _ackCount > 0);

            Assert.NotNull(submitted);
            Assert.NotNull(submitted!.WaitTill);
            Assert.True(submitted.WaitTill > DateTimeOffset.UtcNow);
        }

        [Fact]
        public async Task HandleMessage_DeferredRequeueFails_Nacks()
        {
            _workManager.Setup(m => m.HandleWorkRequestAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(WorkResult.Defer("defer", DateTimeOffset.UtcNow.AddMinutes(5)));
            _workQueueClient.Setup(c => c.SubmitWorkAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("requeue failed"));
            await StartServerAsync();

            DeliverMessage(new WorkRequest { WorkType = WorkType });
            await WaitUntil(() => _nackCount > 0);

            _model.Verify(m => m.BasicNackAsync(1, false, false, It.IsAny<CancellationToken>()), Times.Once);
            _telemetryTracker.Verify(t => t.TrackException(It.IsAny<DeferredMessageRequeueFailedException>(), It.IsAny<Dimensions?>(), It.IsAny<Measures?>()), Times.Once);
        }

        [Fact]
        public async Task HandleMessage_UnknownException_NacksWithRequeue()
        {
            _workManager.Setup(m => m.HandleWorkRequestAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("kaboom"));
            await StartServerAsync();

            DeliverMessage(new WorkRequest { WorkType = WorkType });
            await WaitUntil(() => _nackCount > 0);

            _model.Verify(m => m.BasicNackAsync(1, false, true, It.IsAny<CancellationToken>()), Times.Once);
            _telemetryTracker.Verify(t => t.TrackException(It.IsAny<UnknownWorkRequestException>(), It.IsAny<Dimensions?>(), It.IsAny<Measures?>()), Times.Once);
        }

        [Fact]
        public async Task HandleMessage_UnknownExceptionAndNackThrows_TracksBothExceptions()
        {
            _workManager.Setup(m => m.HandleWorkRequestAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("kaboom"));
            _model.Setup(m => m.BasicNackAsync(It.IsAny<ulong>(), false, true, It.IsAny<CancellationToken>())).Throws(new Exception("nack failed"));
            await StartServerAsync();

            DeliverMessage(new WorkRequest { WorkType = WorkType });
            await WaitUntil(() => _exceptionCount >= 2);

            Assert.True(_exceptionCount >= 2);
        }

        [Fact]
        public async Task HandleMessage_NullWorkRequest_DoesNothing()
        {
            await StartServerAsync();

            DeliverMessage(null);
            await Task.Delay(150, TestContext.Current.CancellationToken);

            Assert.Equal(0, _ackCount);
            Assert.Equal(0, _nackCount);
            _workManager.Verify(m => m.HandleWorkRequestAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Killswitch_TogglesConsumersOnAndOff()
        {
            _killswitch = false;
            var server = CreateServer();
            await server.StartAsync(CancellationToken.None);
            await WaitUntil(() => _consumeCount > 0);

            _killswitch = true;
            await WaitUntil(() => _cancelCount > 0);

            Assert.True(_consumeCount > 0);
            Assert.True(_cancelCount > 0);

            await server.StopAsync(CancellationToken.None);
        }

        [Fact]
        public async Task Killswitch_EnabledFromStart_DoesNotStartConsumers()
        {
            _killswitch = true;
            var server = CreateServer();
            await server.StartAsync(CancellationToken.None);
            await Task.Delay(150, TestContext.Current.CancellationToken);

            Assert.Equal(0, _consumeCount);

            await server.StopAsync(CancellationToken.None);
        }

        [Fact]
        public async Task StopAsync_ClosesModelsAndConnection()
        {
            var server = await StartServerAsync();

            await server.StopAsync(CancellationToken.None);

            _model.Verify(m => m.CloseAsync(It.IsAny<ushort>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);
            _model.Verify(m => m.Dispose(), Times.AtLeastOnce);
        }

        [Fact]
        public async Task Execute_WithNullOptions_LogsAndReturns()
        {
            _server = new RabbitMqWorkServer(
                _workQueueClient.Object,
                _serviceProvider.Object,
                _systemContext.Object,
                _workManager.Object,
                _clientFactory.Object,
                null!,
                _observabilityScope.Object,
                _telemetryTracker.Object,
                _toggleProvider.Object,
                new List<Type> { typeof(FakeQueueableWork) });

            await _server.StartAsync(CancellationToken.None);
            await Task.Delay(100, TestContext.Current.CancellationToken);

            _telemetryTracker.Verify(t => t.TrackTrace("RabbitMqSettings not found", SeverityLevel.Critical, It.IsAny<Dimensions?>()), Times.Once);
            Assert.Equal(0, _consumeCount);
        }

        [Fact]
        public async Task Execute_WhenCreateProcessorsThrows_TracksException()
        {
            var failure = new InvalidOperationException("service failure");
            _serviceProvider
                .Setup(sp => sp.GetService(typeof(IEnumerable<IQueueableWork>)))
                .Throws(failure);

            var server = CreateServer();
            await server.StartAsync(CancellationToken.None);
            await WaitUntil(() => _exceptionCount > 0);

            _telemetryTracker.Verify(t => t.TrackException(failure, It.IsAny<Dimensions?>(), It.IsAny<Measures?>()), Times.Once);
        }

        [Fact]
        public void Dispose_DoesNotThrow()
        {
            var server = CreateServer();
            Assert.Null(Record.Exception(() => server.Dispose()));
        }

        public void Dispose()
        {
            try
            {
                _server?.StopAsync(CancellationToken.None).GetAwaiter().GetResult();
            }
            catch
            {
                // ignore shutdown errors during teardown
            }
            _server?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
