#nullable enable
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.WebHost.Controllers;
using RecordPoint.Connectors.SDK.Work;
using RecordPoint.Connectors.SDK.Work.Models;
using Xunit;

namespace RecordPoint.Connectors.SDK.WebHost.Test
{
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

        // ----- Get (all messages) -----

        [Fact]
        public async Task GetAll_WhenQueueNameIsEmpty_ReturnsBadRequest()
        {
            var sut = CreateSubject();

            var result = await sut.Get(string.Empty);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Queue Name is required", badRequest.Value);
        }

        [Fact]
        public async Task GetAll_WhenQueueNotAllowed_ReturnsBadRequest()
        {
            var sut = CreateSubject(o => o.AllowedQueueNames.Add(AllowedQueueName));

            var result = await sut.Get("not-allowed");

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Queue Name is not allowed. Supported queues: allowed-queue", badRequest.Value);
        }

        [Fact]
        public async Task GetAll_WhenValid_ReturnsOkWithMessages()
        {
            var messages = new List<DeadLetterModel> { new() { SequenceNumber = "1" } };
            _deadLetterQueueService.Setup(s => s.GetMessagesAsync(AllowedQueueName, 0)).ReturnsAsync(messages);
            var sut = CreateSubject();

            var result = await sut.Get(AllowedQueueName);

            var okObject = Assert.IsType<OkObjectResult>(result);
            Assert.Same(messages, okObject.Value);
        }

        [Fact]
        public async Task GetAll_WhenNoAllowList_AllowsAnyQueue()
        {
            _deadLetterQueueService.Setup(s => s.GetMessagesAsync("any-queue", 0)).ReturnsAsync([]);
            var sut = CreateSubject(clearAllowList: true);

            var result = await sut.Get("any-queue");

            Assert.IsType<OkObjectResult>(result);
        }

        // ----- Get (single message by sequence number) -----

        [Fact]
        public async Task GetSingle_WhenQueueInvalid_ReturnsBadRequest()
        {
            var sut = CreateSubject(o => o.AllowedQueueNames.Add(AllowedQueueName));

            var result = await sut.Get("not-allowed", 5);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetSingle_WhenSequenceNumberInvalid_ReturnsBadRequest()
        {
            var sut = CreateSubject();

            var result = await sut.Get(AllowedQueueName, 0);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Sequence Number is required", badRequest.Value);
        }

        [Fact]
        public async Task GetSingle_WhenValid_ReturnsOk()
        {
            var model = new DeadLetterModel { SequenceNumber = "42" };
            _deadLetterQueueService.Setup(s => s.GetMessageAsync(AllowedQueueName, 42)).ReturnsAsync(model);
            var sut = CreateSubject();

            var result = await sut.Get(AllowedQueueName, 42);

            var okObject = Assert.IsType<OkObjectResult>(result);
            Assert.Same(model, okObject.Value);
        }

        // ----- Post (specific sequence numbers) -----

        [Fact]
        public async Task Post_WhenQueueInvalid_ReturnsBadRequest()
        {
            var sut = CreateSubject(o => o.AllowedQueueNames.Add(AllowedQueueName));

            var result = await sut.Post("not-allowed", new long[] { 1 });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Post_WhenNoSequenceNumbers_ReturnsBadRequest()
        {
            var sut = CreateSubject();

            var result = await sut.Post(AllowedQueueName, Array.Empty<long>());

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Sequence Number(s) is required", badRequest.Value);
        }

        [Fact]
        public async Task Post_WhenValid_ResubmitsAndReturnsOk()
        {
            var sut = CreateSubject();

            var result = await sut.Post(AllowedQueueName, new long[] { 1, 2 });

            Assert.IsType<OkResult>(result);
            _deadLetterQueueService.Verify(s => s.ResubmitMessagesAsync(AllowedQueueName, new long[] { 1, 2 }), Times.Once);
        }

        // ----- Post (PostAllMessages / maxCount) -----

        [Fact]
        public async Task PostAll_WhenQueueInvalid_ReturnsBadRequest()
        {
            var sut = CreateSubject(o => o.AllowedQueueNames.Add(AllowedQueueName));

            var result = await sut.Post("not-allowed", 10, TestContext.Current.CancellationToken);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PostAll_WhenBatchTooLarge_ReturnsBadRequest()
        {
            var sut = CreateSubject(o => o.MaxReplayBatchSize = 25);

            var result = await sut.Post(AllowedQueueName, 26, TestContext.Current.CancellationToken);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Batch size [26] is too big (>25) or invalid", badRequest.Value);
        }

        [Fact]
        public async Task PostAll_WhenMaxCountZero_ReturnsBadRequest()
        {
            var sut = CreateSubject();

            var result = await sut.Post(AllowedQueueName, 0, TestContext.Current.CancellationToken);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PostAll_WhenNoDeadLetters_ReturnsOkMessage()
        {
            // Drain reports 0 resubmitted AND confirmed-empty -> terminal "No dead letters found".
            _deadLetterQueueService
                .Setup(s => s.ResubmitTopMessagesAsync(AllowedQueueName, 10, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DeadLetterResubmitResult(0, QueueConfirmedEmpty: true));
            var sut = CreateSubject();

            var result = await sut.Post(AllowedQueueName, 10, TestContext.Current.CancellationToken);

            var okObject = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("No dead letters found", okObject.Value);
        }

        [Fact]
        public async Task PostAll_WhenZeroResubmittedButQueueNotConfirmedEmpty_ReturnsOkNotTheEmptyMessage()
        {
            var cancellationToken = TestContext.Current.CancellationToken;

            // No progress this pass (e.g. head momentarily locked) -> non-terminal Ok(), so a
            // looping caller keeps draining rather than false-stopping on an unverified empty.
            _deadLetterQueueService
                .Setup(s => s.ResubmitTopMessagesAsync(AllowedQueueName, 10, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DeadLetterResubmitResult(0, QueueConfirmedEmpty: false));
            var sut = CreateSubject();

            var result = await sut.Post(AllowedQueueName, 10, cancellationToken);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task PostAll_WhenDeadLettersExist_ResubmitsAndReturnsOk()
        {
            // PostAll now drains via ResubmitTopMessagesAsync (returns the count resubmitted).
            _deadLetterQueueService
                .Setup(s => s.ResubmitTopMessagesAsync(AllowedQueueName, 10, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DeadLetterResubmitResult(2, QueueConfirmedEmpty: false));
            var sut = CreateSubject();

            var result = await sut.Post(AllowedQueueName, 10, TestContext.Current.CancellationToken);

            Assert.IsType<OkResult>(result);
            _deadLetterQueueService.Verify(s => s.ResubmitTopMessagesAsync(AllowedQueueName, 10, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task PostAll_WhenResubmitThrows_TracksExceptionAndRethrows()
        {
            _deadLetterQueueService
                .Setup(s => s.ResubmitTopMessagesAsync(AllowedQueueName, 10, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("boom"));
            var sut = CreateSubject();

            var act = () => sut.Post(AllowedQueueName, 10, TestContext.Current.CancellationToken);

            await Assert.ThrowsAsync<InvalidOperationException>(() => act());
            _telemetryTracker.Verify(t => t.TrackException(It.IsAny<InvalidOperationException>(), null, null), Times.Once);
        }

        // ----- Delete -----

        [Fact]
        public async Task Delete_WhenDisabled_ReturnsNotFound()
        {
            var sut = CreateSubject(o => o.EnableDeleteOperations = false);

            var result = await sut.Delete(AllowedQueueName, 1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_WhenQueueInvalid_ReturnsBadRequest()
        {
            var sut = CreateSubject(o => o.AllowedQueueNames.Add(AllowedQueueName));

            var result = await sut.Delete("not-allowed", 1);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Delete_WhenSequenceNumberInvalid_ReturnsBadRequest()
        {
            var sut = CreateSubject();

            var result = await sut.Delete(AllowedQueueName, 0);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Sequence Number is required", badRequest.Value);
        }

        [Fact]
        public async Task Delete_WhenValid_ReturnsOk()
        {
            var sut = CreateSubject();

            var result = await sut.Delete(AllowedQueueName, 5);

            Assert.IsType<OkResult>(result);
            _deadLetterQueueService.Verify(s => s.DeleteMessageAsync(AllowedQueueName, 5), Times.Once);
        }

        // ----- DeleteAll -----

        [Fact]
        public async Task DeleteAll_WhenDisabled_ReturnsNotFound()
        {
            var sut = CreateSubject(o => o.EnableDeleteOperations = false);

            var result = await sut.DeleteAll(AllowedQueueName);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteAll_WhenQueueInvalid_ReturnsBadRequest()
        {
            var sut = CreateSubject(o => o.AllowedQueueNames.Add(AllowedQueueName));

            var result = await sut.DeleteAll("not-allowed");

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteAll_WhenValid_ReturnsOk()
        {
            var sut = CreateSubject();

            var result = await sut.DeleteAll(AllowedQueueName);

            Assert.IsType<OkResult>(result);
            _deadLetterQueueService.Verify(s => s.DeleteAllMessagesAsync(AllowedQueueName), Times.Once);
        }

        private DeadLetterController CreateSubject(
            Action<DeadLetterControllerOptions>? configureOptions = null,
            bool clearAllowList = false)
        {
            var options = new DeadLetterControllerOptions();
            if (!clearAllowList)
            {
                options.AllowedQueueNames.Add(AllowedQueueName);
            }
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
