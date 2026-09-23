#nullable enable
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Toggles;
using RecordPoint.Connectors.SDK.Work;
using RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.Test.Mock;
using Xunit;

namespace RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.Test
{
    public class AzureServiceBusWorkServerTests
    {
        private sealed class WorkServerHarness
        {
            public MockServiceBusClientFactory Factory { get; } = new();
            public Mock<IQueueableWorkManager> WorkManager { get; } = new();
            public Mock<IWorkQueueClient> WorkQueueClient { get; } = new();
            public Mock<IToggleProvider> ToggleProvider { get; } = new();
            public Mock<ISystemContext> SystemContext { get; } = new();
            public Mock<IObservabilityScope> ObservabilityScope { get; } = new();
            public Mock<ITelemetryTracker> TelemetryTracker { get; } = new();
            public AzureServiceBusOptions Options { get; } = new()
            {
                KillswitchCheckInterval = 60000,
                ServiceShutdownDelay = 0
            };
            public IConfiguration Configuration { get; set; } =
                new ConfigurationBuilder().AddInMemoryCollection().Build();

            public WorkServerHarness()
            {
                SystemContext.Setup(c => c.GetCompanyName()).Returns("RecordPoint");
                SystemContext.Setup(c => c.GetConnectorName()).Returns("TestConnector");
                SystemContext.Setup(c => c.GetServiceName()).Returns("TestService");
                ObservabilityScope.Setup(s => s.BeginScope(It.IsAny<Dimensions>(), It.IsAny<Measures>()))
                    .Returns(Moq.Mock.Of<IDisposable>());
                // Default killswitch false
                ToggleProvider.Setup(t => t.GetToggleBool(It.IsAny<string>(), It.IsAny<bool>())).Returns(false);
            }

            public AzureServiceBusWorkServer Build(IList<Type>? operationTypes = null, params IQueueableWork[] registered)
            {
                var services = new ServiceCollection();
                foreach (var work in registered)
                {
                    services.AddSingleton<IQueueableWork>(work);
                }
                var provider = services.BuildServiceProvider();

                return new AzureServiceBusWorkServer(
                    WorkQueueClient.Object,
                    provider,
                    SystemContext.Object,
                    WorkManager.Object,
                    Factory,
                    Microsoft.Extensions.Options.Options.Create(Options),
                    ObservabilityScope.Object,
                    TelemetryTracker.Object,
                    ToggleProvider.Object,
                    operationTypes ?? new List<Type>(),
                    Configuration);
            }

            public TestableWorkServer BuildTestable(IList<Type> operationTypes, params IQueueableWork[] registered)
            {
                var services = new ServiceCollection();
                foreach (var work in registered)
                {
                    services.AddSingleton<IQueueableWork>(work);
                }
                var provider = services.BuildServiceProvider();

                return new TestableWorkServer(
                    WorkQueueClient.Object,
                    provider,
                    SystemContext.Object,
                    WorkManager.Object,
                    Factory,
                    Microsoft.Extensions.Options.Options.Create(Options),
                    ObservabilityScope.Object,
                    TelemetryTracker.Object,
                    ToggleProvider.Object,
                    operationTypes,
                    Configuration);
            }

            /// <summary>
            /// Waits for a processor mock to be created in the ProcessorMocks dictionary.
            /// Retries with delays to account for the background ExecuteAsync task timing.
            /// </summary>
            public async Task<Mock<ServiceBusProcessor>> WaitForProcessorAsync(string queueName, int maxRetries = 50)
            {
                for (int i = 0; i < maxRetries; i++)
                {
                    if (Factory.ProcessorMocks.TryGetValue(queueName, out var processor))
                    {
                        return processor;
                    }
                    await Task.Delay(20);
                }
                throw new KeyNotFoundException($"Processor for queue '{queueName}' was not created after {maxRetries * 20}ms");
            }
        }

        /// <summary>
        /// Exposes the protected ExecuteAsync so tests can drive the full lifecycle
        /// (including the graceful-shutdown path) with a token they control.
        /// </summary>
        private sealed class TestableWorkServer : AzureServiceBusWorkServer
        {
            public TestableWorkServer(
                IWorkQueueClient workQueueClient,
                IServiceProvider serviceProvider,
                ISystemContext systemContext,
                IQueueableWorkManager workManager,
                IServiceBusClientFactory serviceBusClientFactory,
                IOptions<AzureServiceBusOptions> serviceBusOptions,
                IObservabilityScope observabilityScope,
                ITelemetryTracker telemetryTracker,
                IToggleProvider toggleProvider,
                IList<Type> operationTypes,
                IConfiguration configuration)
                : base(workQueueClient, serviceProvider, systemContext, workManager, serviceBusClientFactory,
                    serviceBusOptions, observabilityScope, telemetryTracker, toggleProvider, operationTypes, configuration)
            {
            }

            public Task RunExecuteAsync(CancellationToken cancellationToken) => ExecuteAsync(cancellationToken);
        }

        private static Mock<ProcessMessageEventArgs> CreateMessageArgs(WorkRequest? request, out ServiceBusReceivedMessage message, bool callBase = false)
        {
            var json = request == null ? "null" : JsonConvert.SerializeObject(request);
            message = ServiceBusModelFactory.ServiceBusReceivedMessage(body: BinaryData.FromString(json));
            var receiver = new Mock<ServiceBusReceiver>();
            var localMessage = message;
            var argsMock = new Mock<ProcessMessageEventArgs>(localMessage, receiver.Object, CancellationToken.None)
            {
                CallBase = callBase
            };
            argsMock.Setup(a => a.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            argsMock.Setup(a => a.AbandonMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<IDictionary<string, object>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            argsMock.Setup(a => a.DeadLetterMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            return argsMock;
        }

        // ---- Constructor / operation-type validation ----

        [Fact]
        public void Constructor_Throws_WhenTypeDoesNotImplementIQueueableWork()
        {
            var harness = new WorkServerHarness();
            var ex = Assert.Throws<ArgumentException>(() =>
                harness.Build(new List<Type> { typeof(NotQueueableWork) }));
            Assert.Contains("does not implement", ex.Message);
        }

        [Fact]
        public void Constructor_UsesProvidedOperationTypes()
        {
            var harness = new WorkServerHarness();
            var server = harness.Build(new List<Type> { typeof(StubQueueableWork) }, new StubQueueableWork());
            Assert.NotNull(server);
        }

        [Fact]
        public void Constructor_UsesDefaults_WhenNoOperationTypesProvided()
        {
            var harness = new WorkServerHarness();
            var server = harness.Build(new List<Type>());
            Assert.NotNull(server);
        }

        // ---- Processor creation via ExecuteAsync ----

        [Fact]
        public async Task Execute_CreatesProcessor_AndStartsProcessing_WhenKillswitchOff()
        {
            var harness = new WorkServerHarness();
            Exception? tracked = null;
            harness.TelemetryTracker.Setup(t => t.TrackException(It.IsAny<Exception>(), It.IsAny<Dimensions>(), It.IsAny<Measures>()))
                .Callback<Exception?, Dimensions?, Measures?>((e, _, _) => tracked = e);
            var server = harness.Build(
                new List<Type> { typeof(StubQueueableWork) },
                new StubQueueableWork { WorkType = "Test Work" });

            await server.StartAsync(CancellationToken.None);

            var processor = await harness.WaitForProcessorAsync("test-work");
            Assert.NotNull(processor);
            processor.Verify(p => p.StartProcessingAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.NotNull(harness.Factory.GetMessageHandler("test-work"));
            Assert.NotNull(harness.Factory.GetErrorHandler("test-work"));

            await server.StopAsync(CancellationToken.None);
        }

        [Fact]
        public async Task Execute_AppliesQueuePrefix_ToProcessorName()
        {
            var harness = new WorkServerHarness();
            harness.Options.QueuePrefix = "pre";
            var server = harness.Build(
                new List<Type> { typeof(StubQueueableWork) },
                new StubQueueableWork { WorkType = "Test Work" });

            await server.StartAsync(CancellationToken.None);

            var processor = await harness.WaitForProcessorAsync("pre-test-work");
            Assert.NotNull(processor);

            await server.StopAsync(CancellationToken.None);
        }

        [Fact]
        public async Task Execute_SkipsOperationType_WhenNotRegistered()
        {
            var harness = new WorkServerHarness();
            // operation type requested but no matching IQueueableWork registered
            var server = harness.Build(new List<Type> { typeof(StubQueueableWork) });

            await server.StartAsync(CancellationToken.None);

            Assert.Empty(harness.Factory.ProcessorMocks);

            await server.StopAsync(CancellationToken.None);
        }

        [Fact]
        public async Task Execute_DoesNotStart_WhenKillswitchOn()
        {
            var harness = new WorkServerHarness();
            harness.ToggleProvider.Setup(t => t.GetToggleBool(It.IsAny<string>(), It.IsAny<bool>())).Returns(true);
            var server = harness.Build(
                new List<Type> { typeof(StubQueueableWork) },
                new StubQueueableWork { WorkType = "Test Work" });

            await server.StartAsync(CancellationToken.None);

            var processor = await harness.WaitForProcessorAsync("test-work");
            processor.Verify(p => p.StartProcessingAsync(It.IsAny<CancellationToken>()), Times.Never);

            await server.StopAsync(CancellationToken.None);
        }

        [Fact]
        public async Task Execute_StopsProcessors_WhenKillswitchTurnsOn()
        {
            var harness = new WorkServerHarness();
            harness.Options.KillswitchCheckInterval = 20;
            var killswitch = false;
            harness.ToggleProvider.Setup(t => t.GetToggleBool(It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(() => killswitch);

            var server = harness.Build(
                new List<Type> { typeof(StubQueueableWork) },
                new StubQueueableWork { WorkType = "Test Work" });

            await server.StartAsync(CancellationToken.None);
            var processor = await harness.WaitForProcessorAsync("test-work");
            processor.Setup(p => p.IsProcessing).Returns(true);

            // Flip the killswitch on; the loop should observe it and stop processing.
            killswitch = true;

            var stopped = false;
            for (var i = 0; i < 100 && !stopped; i++)
            {
                try
                {
                    processor.Verify(p => p.StopProcessingAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
                    stopped = true;
                }
                catch (MockException)
                {
                    await Task.Delay(20, TestContext.Current.CancellationToken);
                }
            }

            Assert.True(stopped, "Expected processors to be stopped when killswitch turned on");

            await server.StopAsync(CancellationToken.None);
        }

        // ---- StopAsync ----

        [Fact]
        public async Task StopAsync_StopsAndDisposesProcessors_AndDisposesClient()
        {
            var harness = new WorkServerHarness();
            var server = harness.Build(
                new List<Type> { typeof(StubQueueableWork) },
                new StubQueueableWork { WorkType = "Test Work" });

            await server.StartAsync(CancellationToken.None);
            var processor = await harness.WaitForProcessorAsync("test-work");

            await server.StopAsync(CancellationToken.None);

            processor.Verify(p => p.StopProcessingAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
            // ServiceBusProcessor.DisposeAsync is non-overridable and cannot be verified via Moq.
            harness.Factory.ClientMock.Verify(c => c.DisposeAsync(), Times.Once);
        }

        // ---- Message handling branches ----

        private static async Task<(WorkServerHarness harness, AzureServiceBusWorkServer server, Func<ProcessMessageEventArgs, Task> handler)> StartAndGetHandlerAsync()
        {
            var harness = new WorkServerHarness();
            var server = harness.Build(
                new List<Type> { typeof(StubQueueableWork) },
                new StubQueueableWork { WorkType = "Test Work" });
            await server.StartAsync(CancellationToken.None);
            await harness.WaitForProcessorAsync("test-work");
            var handler = harness.Factory.GetMessageHandler("test-work")!;
            return (harness, server, handler);
        }

        [Fact]
        public async Task HandleMessage_NullWorkRequest_DoesNothing()
        {
            var (harness, _, handler) = await StartAndGetHandlerAsync();
            var args = CreateMessageArgs(null, out _);

            await handler(args.Object);

            harness.WorkManager.Verify(m => m.HandleWorkRequestAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()), Times.Never);
            args.Verify(a => a.CompleteMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Theory]
        [InlineData(WorkResultType.Complete)]
        [InlineData(WorkResultType.Abandoned)]
        public async Task HandleMessage_CompletesMessage(WorkResultType resultType)
        {
            var (harness, _, handler) = await StartAndGetHandlerAsync();
            harness.WorkManager
                .Setup(m => m.HandleWorkRequestAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new WorkResult { ResultType = resultType });

            var args = CreateMessageArgs(new WorkRequest { WorkType = "Test Work" }, out var msg);
            await handler(args.Object);

            args.Verify(a => a.CompleteMessageAsync(msg, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task HandleMessage_DeadLetter_MovesToDeadLetter()
        {
            var (harness, _, handler) = await StartAndGetHandlerAsync();
            var exception = new InvalidOperationException("boom");
            harness.WorkManager
                .Setup(m => m.HandleWorkRequestAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(WorkResult.DeadLetter("bad", exception));

            var args = CreateMessageArgs(new WorkRequest { WorkType = "Test Work" }, out var msg);
            await handler(args.Object);

            args.Verify(a => a.DeadLetterMessageAsync(msg, "bad", It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task HandleMessage_Failed_AbandonsMessage()
        {
            var (harness, _, handler) = await StartAndGetHandlerAsync();
            harness.WorkManager
                .Setup(m => m.HandleWorkRequestAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(WorkResult.Failed("nope"));

            var args = CreateMessageArgs(new WorkRequest { WorkType = "Test Work" }, out var msg);
            await handler(args.Object);

            args.Verify(a => a.AbandonMessageAsync(msg, It.IsAny<IDictionary<string, object>>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task HandleMessage_Deferred_ResubmitsAndCompletes_WithFutureWaitTill()
        {
            var (harness, _, handler) = await StartAndGetHandlerAsync();
            var future = DateTimeOffset.UtcNow.AddHours(1);
            harness.WorkManager
                .Setup(m => m.HandleWorkRequestAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(WorkResult.Defer("later", future));

            WorkRequest? resubmitted = null;
            harness.WorkQueueClient
                .Setup(c => c.SubmitWorkAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
                .Callback<WorkRequest, CancellationToken>((r, _) => resubmitted = r)
                .Returns(Task.CompletedTask);

            var args = CreateMessageArgs(new WorkRequest { WorkType = "Test Work" }, out var msg);
            await handler(args.Object);

            Assert.NotNull(resubmitted);
            Assert.Equal(future, resubmitted!.WaitTill);
            args.Verify(a => a.CompleteMessageAsync(msg, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task HandleMessage_Deferred_UsesDefaultDelay_WhenWaitTillPast()
        {
            var (harness, _, handler) = await StartAndGetHandlerAsync();
            harness.WorkManager
                .Setup(m => m.HandleWorkRequestAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(WorkResult.Defer("later", DateTimeOffset.UtcNow.AddMinutes(-5)));

            WorkRequest? resubmitted = null;
            harness.WorkQueueClient
                .Setup(c => c.SubmitWorkAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
                .Callback<WorkRequest, CancellationToken>((r, _) => resubmitted = r)
                .Returns(Task.CompletedTask);

            var args = CreateMessageArgs(new WorkRequest { WorkType = "Test Work" }, out _);
            await handler(args.Object);

            Assert.NotNull(resubmitted);
            Assert.True(resubmitted!.WaitTill > DateTimeOffset.UtcNow);
        }

        [Fact]
        public async Task HandleMessage_Deferred_DeadLetters_WhenResubmitFails()
        {
            var (harness, _, handler) = await StartAndGetHandlerAsync();
            harness.WorkManager
                .Setup(m => m.HandleWorkRequestAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(WorkResult.Defer("later", DateTimeOffset.UtcNow.AddHours(1)));
            harness.WorkQueueClient
                .Setup(c => c.SubmitWorkAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("requeue failed"));

            var args = CreateMessageArgs(new WorkRequest { WorkType = "Test Work" }, out var msg);
            await handler(args.Object);

            args.Verify(a => a.DeadLetterMessageAsync(msg, "Failed to handle deferred message", It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
            harness.TelemetryTracker.Verify(t => t.TrackException(It.IsAny<Exception>(), It.IsAny<Dimensions>(), It.IsAny<Measures>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task HandleMessage_UnhandledException_AbandonsAndTracks()
        {
            var (harness, _, handler) = await StartAndGetHandlerAsync();
            harness.WorkManager
                .Setup(m => m.HandleWorkRequestAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("kaboom"));

            var args = CreateMessageArgs(new WorkRequest { WorkType = "Test Work" }, out var msg);
            await handler(args.Object);

            args.Verify(a => a.AbandonMessageAsync(msg, It.IsAny<IDictionary<string, object>>(), It.IsAny<CancellationToken>()), Times.Once);
            harness.TelemetryTracker.Verify(t => t.TrackException(It.IsAny<Exception>(), It.IsAny<Dimensions>(), It.IsAny<Measures>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task HandleMessage_UnhandledException_AndAbandonFails_TracksBoth()
        {
            var (harness, _, handler) = await StartAndGetHandlerAsync();
            harness.WorkManager
                .Setup(m => m.HandleWorkRequestAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("kaboom"));

            var args = CreateMessageArgs(new WorkRequest { WorkType = "Test Work" }, out _);
            args.Setup(a => a.AbandonMessageAsync(It.IsAny<ServiceBusReceivedMessage>(), It.IsAny<IDictionary<string, object>>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("abandon failed"));

            await handler(args.Object);

            harness.TelemetryTracker.Verify(t => t.TrackException(It.IsAny<Exception>(), It.IsAny<Dimensions>(), It.IsAny<Measures>()), Times.AtLeast(2));
        }

        // ---- Error handler ----

        [Fact]
        public async Task ErrorHandler_TracksException()
        {
            var harness = new WorkServerHarness();
            var server = harness.Build(
                new List<Type> { typeof(StubQueueableWork) },
                new StubQueueableWork { WorkType = "Test Work" });
            await server.StartAsync(CancellationToken.None);

            await harness.WaitForProcessorAsync("test-work");
            var errorHandler = harness.Factory.GetErrorHandler("test-work")!;
            var errorArgs = new ProcessErrorEventArgs(
                new Exception("processing error"),
                ServiceBusErrorSource.Receive,
                "namespace",
                "entity",
                CancellationToken.None);

            await errorHandler(errorArgs);

            harness.TelemetryTracker.Verify(t => t.TrackException(It.IsAny<Exception>(), It.IsAny<Dimensions>(), It.IsAny<Measures>()), Times.AtLeastOnce);

            await server.StopAsync(CancellationToken.None);
        }

        // ---- Config-driven per-work-type overrides ----

        [Fact]
        public async Task Execute_UsesPerWorkTypeConfigOverrides()
        {
            var harness = new WorkServerHarness();
            harness.Configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["AzureServiceBusSettings:MaxDegreeOfParallelism-TestWork"] = "7",
                    ["AzureServiceBusSettings:MaxAutoLockRenewalDurationSeconds-TestWork"] = "123"
                })
                .Build();

            ServiceBusProcessorOptions? capturedOptions = null;
            harness.Factory.ClientMock
                .Setup(c => c.CreateProcessor(It.IsAny<string>(), It.IsAny<ServiceBusProcessorOptions>()))
                .Returns<string, ServiceBusProcessorOptions>((queueName, options) =>
                {
                    capturedOptions = options;
                    var pm = new Mock<ServiceBusProcessor>(MockBehavior.Loose);
                    pm.Setup(p => p.StartProcessingAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
                    pm.Setup(p => p.StopProcessingAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
                    pm.Setup(p => p.CloseAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
                    pm.Setup(p => p.DisposeAsync()).Returns(ValueTask.CompletedTask);
                    harness.Factory.ProcessorMocks[queueName] = pm;
                    return pm.Object;
                });

            var server = harness.Build(
                new List<Type> { typeof(StubQueueableWork) },
                new StubQueueableWork { WorkType = "Test Work" });

            await server.StartAsync(CancellationToken.None);

            // Give the background task a bit more time to call CreateProcessor
            await Task.Delay(100, TestContext.Current.CancellationToken);
            for (var i = 0; i < 100; i++)
            {
                if (capturedOptions != null)
                    break;
                await Task.Delay(20, TestContext.Current.CancellationToken);
            }

            Assert.NotNull(capturedOptions);
            Assert.Equal(7, capturedOptions!.MaxConcurrentCalls);
            Assert.Equal(TimeSpan.FromSeconds(123), capturedOptions.MaxAutoLockRenewalDuration);

            await server.StopAsync(CancellationToken.None);
        }

        // ---- Graceful shutdown path (ExecuteAsync cancellation) ----

        [Fact]
        public async Task Execute_GracefullyClosesProcessors_OnCancellation()
        {
            var harness = new WorkServerHarness();
            var server = harness.BuildTestable(
                new List<Type> { typeof(StubQueueableWork) },
                new StubQueueableWork { WorkType = "Test Work" });

            using var cts = new CancellationTokenSource();
            var executeTask = server.RunExecuteAsync(cts.Token);

            // Wait until the processor has been created and started.
            var processor = await harness.WaitForProcessorAsync("test-work");

            await cts.CancelAsync();
            await executeTask;

            processor.Verify(p => p.CloseAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        // ---- Message lock lost handler ----

        [Fact]
        public async Task HandleMessage_MessageLockLost_TracksTrace_AndCancelsProcessing()
        {
            var (harness, _, handler) = await StartAndGetHandlerAsync();

            var processingStarted = new TaskCompletionSource();
            var release = new TaskCompletionSource<WorkResult>();
            CancellationToken processingToken = default;
            harness.WorkManager
                .Setup(m => m.HandleWorkRequestAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
                .Returns<WorkRequest, CancellationToken>((_, token) =>
                {
                    // Capture the token handed to the work manager so we can assert the
                    // lock-lost event cancels the in-flight processing.
                    processingToken = token;
                    processingStarted.TrySetResult();
                    return release.Task;
                });

            var args = CreateMessageArgs(new WorkRequest { WorkType = "Test Work" }, out var msg, callBase: true);
            var handlerTask = handler(args.Object);

            // Wait until processing is in-flight (lock-lost handler is now subscribed).
            await processingStarted.Task;

            // The processing token should still be live while work is in progress.
            Assert.False(processingToken.IsCancellationRequested);

            // Raise the MessageLockLostAsync event via the delegate stored on the args.
            var lockLostDelegate = ExtractFuncField<MessageLockLostEventArgs>(args.Object);
            Assert.NotNull(lockLostDelegate);
            await lockLostDelegate!(new MessageLockLostEventArgs(msg, new Exception("lock lost")));

            // The lock-lost handler must cancel the token passed to HandleWorkRequestAsync,
            // signalling the in-flight work to stop processing.
            Assert.True(processingToken.IsCancellationRequested);

            // Let processing finish.
            release.TrySetResult(WorkResult.Complete());
            await handlerTask;

            harness.TelemetryTracker.Verify(
                t => t.TrackTrace("Message lock lost before processing could complete", SeverityLevel.Warning, It.IsAny<Dimensions>()),
                Times.Once);
        }

        private static Func<TArgs, Task>? ExtractFuncField<TArgs>(object instance) where TArgs : class
            => ReflectionTestHelper.ExtractDelegateField<Func<TArgs, Task>>(instance);
    }
}
