using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Moq;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.WebHost.Controllers;
using RecordPoint.Connectors.SDK.Work;
using RecordPoint.Connectors.SDK.Work.Models;
using RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Work
{
    [Collection("EnvironmentVariable")]
    public class DeadLetterControllerTests
    {
        private const string AllowedQueueName = "allowed-queue";

        private readonly Mock<IDeadLetterQueueService> _deadLetterQueueService = new();
        private readonly Mock<IObservabilityScope> _observabilityScope = new();
        private readonly Mock<ISystemContext> _systemContext = new();
        private readonly Mock<ITelemetryTracker> _telemetryTracker = new();

        public DeadLetterControllerTests()
        {
            _observabilityScope
                .Setup(scope => scope.BeginScope(It.IsAny<Dimensions>(), It.IsAny<Measures>()))
                .Returns(new Mock<IDisposable>().Object);
        }

        [Fact]
        public async Task Get_WhenQueueIsNotAllowed_ShouldReturnBadRequest()
        {
            var sut = CreateSubject(options =>
            {
                options.AllowedQueueNames.Add(AllowedQueueName);
            });

            var result = await sut.Get("different-queue");

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Queue Name is not allowed. Supported queues: allowed-queue", badRequest.Value);
        }

        [Fact]
        public async Task PostAllMessages_WhenBatchExceedsConfiguredLimit_ShouldReturnBadRequest()
        {
            var sut = CreateSubject(options =>
            {
                options.MaxReplayBatchSize = 25;
            });

            var result = await sut.Post(AllowedQueueName, 26, cancellationToken: TestContext.Current.CancellationToken);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Batch size [26] is too big (>25) or invalid", badRequest.Value);
        }

        [Fact]
        public async Task PostAllMessages_WhenNoDeadLettersFound_ShouldReturnOkWithMessage()
        {
            _deadLetterQueueService
                .Setup(service => service.ResubmitTopMessagesAsync(AllowedQueueName, 10, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DeadLetterResubmitResult(0, QueueConfirmedEmpty: true));

            var sut = CreateSubject();

            var result = await sut.Post(AllowedQueueName, 10, cancellationToken: TestContext.Current.CancellationToken);

            var okObject = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("No dead letters found", okObject.Value);

            _telemetryTracker.Verify(
                t => t.TrackEvent(
                    "DLQ.ResubmitBatch",
                    It.Is<Dimensions>(d => d["Reason"] == "NoMessages" && d["QueueName"] == AllowedQueueName),
                    It.Is<Measures>(m => m["Resubmitted"] == 0d && m["Requested"] == 10d)),
                Times.Once);
        }

        [Fact]
        public async Task PostAllMessages_WhenZeroResubmittedButQueueNotConfirmedEmpty_ShouldReturnOkAndTrackNoProgress()
        {
            // A pass that made no progress (e.g. the head was momentarily locked) must NOT
            // report the terminal "No dead letters found" - it returns a bare Ok() so a
            // looping caller keeps draining instead of false-stopping.
            _deadLetterQueueService
                .Setup(service => service.ResubmitTopMessagesAsync(AllowedQueueName, 10, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DeadLetterResubmitResult(0, QueueConfirmedEmpty: false));

            var sut = CreateSubject();

            var result = await sut.Post(AllowedQueueName, 10, cancellationToken: TestContext.Current.CancellationToken);

            Assert.IsType<OkResult>(result);

            _telemetryTracker.Verify(
                t => t.TrackEvent(
                    "DLQ.ResubmitBatch",
                    It.Is<Dimensions>(d => d["Reason"] == "NoProgress" && d["QueueName"] == AllowedQueueName),
                    It.Is<Measures>(m => m["Resubmitted"] == 0d && m["Requested"] == 10d)),
                Times.Once);
        }

        [Fact]
        public async Task PostAllMessages_WhenDeadLettersExist_ShouldResubmitAndReturnOk()
        {
            _deadLetterQueueService
                .Setup(service => service.ResubmitTopMessagesAsync(AllowedQueueName, 10, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DeadLetterResubmitResult(3, QueueConfirmedEmpty: false));

            var sut = CreateSubject();

            var result = await sut.Post(AllowedQueueName, 10, cancellationToken: TestContext.Current.CancellationToken);

            Assert.IsType<OkResult>(result);
            _deadLetterQueueService.Verify(
                service => service.ResubmitTopMessagesAsync(AllowedQueueName, 10, It.IsAny<CancellationToken>()),
                Times.Once);

            _telemetryTracker.Verify(
                t => t.TrackEvent(
                    "DLQ.ResubmitBatch",
                    It.Is<Dimensions>(d => d["Reason"] == "Completed" && d["QueueName"] == AllowedQueueName),
                    It.Is<Measures>(m => m["Resubmitted"] == 3d && m["Requested"] == 10d)),
                Times.Once);
        }

        [Fact]
        public async Task PostAllMessages_WhenResubmitThrows_ShouldTrackExceptionAndRethrow()
        {
            _deadLetterQueueService
                .Setup(service => service.ResubmitTopMessagesAsync(AllowedQueueName, 10, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("Service Bus error"));

            var sut = CreateSubject();

            var act = () => sut.Post(AllowedQueueName, 10);

            await Assert.ThrowsAsync<InvalidOperationException>(act);
            _telemetryTracker.Verify(
                t => t.TrackException(It.IsAny<InvalidOperationException>(), null, null),
                Times.Once);

            _telemetryTracker.Verify(
                t => t.TrackEvent(
                    "DLQ.ResubmitBatch",
                    It.Is<Dimensions>(d => d["Reason"] == "Exception"),
                    It.IsAny<Measures>()),
                Times.Once);
        }

        [Fact]
        public async Task PostAllMessages_WhenMaxCountIsZero_ShouldReturnBadRequest()
        {
            var sut = CreateSubject();

            var result = await sut.Post(AllowedQueueName, 0, cancellationToken: TestContext.Current.CancellationToken);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Delete_WhenDeleteOperationsAreDisabled_ShouldReturnNotFound()
        {
            var sut = CreateSubject(options =>
            {
                options.EnableDeleteOperations = false;
            });

            var result = await sut.Delete(AllowedQueueName, 1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void UseAsbDeadLetterQueueService_WhenConfigured_ShouldRegisterControllerOptions()
        {
            using var host = Host.CreateDefaultBuilder()
                .UseASBDeadLetterQueueService(options =>
                {
                    options.AllowedQueueNames.Add(AllowedQueueName);
                    options.EnableDeleteOperations = false;
                    options.MaxReplayBatchSize = 25;
                })
                .Build();

            var options = host.Services.GetRequiredService<IOptions<DeadLetterControllerOptions>>().Value;

            Assert.Contains(AllowedQueueName, options.AllowedQueueNames);
            Assert.False(options.EnableDeleteOperations);
            Assert.Equal(25, options.MaxReplayBatchSize);
        }

        private DeadLetterController CreateSubject(Action<DeadLetterControllerOptions> configureOptions = null)
        {
            var options = new DeadLetterControllerOptions();
            options.AllowedQueueNames.Add(AllowedQueueName);
            configureOptions?.Invoke(options);

            return new DeadLetterController(
                _deadLetterQueueService.Object,
                _observabilityScope.Object,
                _systemContext.Object,
                _telemetryTracker.Object,
                Options.Create(options));
        }
    }
}

