using Moq;
using RecordPoint.Connectors.SDK.Diagnostics;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.SubmitPipeline
{
    public class SubmitPipelineElementBaseTests
    {
        /// <summary>
        /// Test element that exposes the protected members of SubmitPipelineElementBase.
        /// </summary>
        private sealed class TestElement : SubmitPipelineElementBase
        {
            public TestElement(ISubmission next) : base(next)
            {
            }

            public override Task Submit(SubmitContext submitContext) => InvokeNext(submitContext);

            public Task InvokeNextPublic(SubmitContext ctx) => InvokeNext(ctx);
            public void SkipNextPublic(SubmitContext ctx, string reason) => SkipNext(ctx, reason);
            public void LogVerbosePublic(SubmitContext ctx, string method, string message) => LogVerbose(ctx, method, message);
            public void LogMessagePublic(SubmitContext ctx, string method, string message) => LogMessage(ctx, method, message);
            public void LogWarningPublic(SubmitContext ctx, string method, string message) => LogWarning(ctx, method, message);
        }

        private readonly Mock<ISubmission> _next = new();
        private readonly Mock<ILog> _log = new();

        [Fact]
        public async Task InvokeNext_CallsNext_WhenPresent()
        {
            var element = new TestElement(_next.Object) { Log = _log.Object };
            var context = new SubmitContext();

            await element.InvokeNextPublic(context);

            _next.Verify(x => x.Submit(context), Times.Once);
        }

        [Fact]
        public async Task InvokeNext_DoesNothing_WhenNextNull()
        {
            var element = new TestElement(null) { Log = _log.Object };

            var exception = await Record.ExceptionAsync(async () => await element.InvokeNextPublic(new SubmitContext()));

            Assert.Null(exception);
        }

        [Fact]
        public async Task InvokeNext_Throws_WhenCancellationRequested()
        {
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            var element = new TestElement(_next.Object) { Log = _log.Object };
            var context = new SubmitContext { CancellationToken = cts.Token };

            await Assert.ThrowsAsync<OperationCanceledException>(async () => await element.InvokeNextPublic(context));
            _next.Verify(x => x.Submit(It.IsAny<SubmitContext>()), Times.Never);
        }

        [Fact]
        public async Task InvokeNext_PropagatesException_FromNext()
        {
            _next.Setup(x => x.Submit(It.IsAny<SubmitContext>())).ThrowsAsync(new InvalidOperationException("boom"));

            var element = new TestElement(_next.Object) { Log = _log.Object };

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await element.InvokeNextPublic(new SubmitContext()));
        }

        [Fact]
        public void SkipNext_SetsSkippedStatusAndReason()
        {
            var element = new TestElement(_next.Object) { Log = _log.Object };
            var context = new SubmitContext();

            element.SkipNextPublic(context, "because reasons");

            Assert.Equal(SubmitResult.Status.Skipped, context.SubmitResult.SubmitStatus);
            Assert.Equal("because reasons", context.SubmitResult.Reason);
        }

        [Fact]
        public void LogHelpers_InvokeLog_WhenLogSet()
        {
            var element = new TestElement(_next.Object) { Log = _log.Object };
            var context = new SubmitContext();

            element.LogVerbosePublic(context, "m", "verbose");
            element.LogMessagePublic(context, "m", "message");
            element.LogWarningPublic(context, "m", "warning");

            _log.Verify(x => x.LogVerbose(It.IsAny<Type>(), "m", It.IsAny<string>(), It.IsAny<long?>()), Times.Once);
            _log.Verify(x => x.LogMessage(It.IsAny<Type>(), "m", It.IsAny<string>(), It.IsAny<long?>()), Times.Once);
            _log.Verify(x => x.LogWarning(It.IsAny<Type>(), "m", It.IsAny<string>(), It.IsAny<long?>()), Times.Once);
        }

        [Fact]
        public void LogHelpers_DoNotThrow_WhenLogNull()
        {
            var element = new TestElement(_next.Object);
            var context = new SubmitContext();

            var exception = Record.Exception(() =>
            {
                element.LogVerbosePublic(context, "m", "verbose");
                element.LogMessagePublic(context, "m", "message");
                element.LogWarningPublic(context, "m", "warning");
            });

            Assert.Null(exception);
        }
    }
}
