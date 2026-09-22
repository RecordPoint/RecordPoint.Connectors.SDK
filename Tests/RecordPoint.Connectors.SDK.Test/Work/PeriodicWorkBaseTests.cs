#nullable enable
using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Work
{
    /// <summary>
    /// Concrete PeriodicWorkBase used to exercise the base class logic.
    /// </summary>
    internal sealed class TestPeriodicWork : PeriodicWorkBase
    {
        public TestPeriodicWork(IObservabilityScope scope, ITelemetryTracker tracker, IDateTimeProvider clock)
            : base(scope, tracker, clock) { }

        public override int ServiceIntervalInSeconds => 3600;
        public override string ServiceName => "TestService";
        public override string WorkType => "TestWork";

        public Func<CancellationToken, Task>? OnStart { get; set; }
        public Func<CancellationToken, Task>? OnRun { get; set; }

        public readonly SemaphoreSlim IterationSignal = new(0);

        protected override Task InnerStartAsync(CancellationToken cancellationToken)
            => OnStart?.Invoke(cancellationToken) ?? Task.CompletedTask;

        protected override async Task InnerRunAsync(CancellationToken cancellationToken)
        {
            try
            {
                if (OnRun != null)
                    await OnRun(cancellationToken);
            }
            finally
            {
                IterationSignal.Release();
            }
        }

        // Expose protected members for direct unit testing.
        public void CallComplete(string reason) => Complete(reason);
        public void CallResetResult() => ResetResult();
        public void CallEnsureHasOutcome() => EnsureHasOutcome();
        public void CallEnsureIncomplete() => EnsureIncomplete();
        public void SetDetails(string details) => ResultReasonDetails = details;
    }

    public class PeriodicWorkBaseTests
    {
        private readonly ObservabilityScope _scope = new();
        private readonly Mock<ITelemetryTracker> _tracker = new();
        private readonly Mock<IDateTimeProvider> _clock = new();

        public PeriodicWorkBaseTests()
        {
            _clock.SetupGet(x => x.UtcNow).Returns(() => DateTime.UtcNow);
        }

        private TestPeriodicWork Create() => new(_scope, _tracker.Object, _clock.Object);

        private static async Task RunOneIterationAsync(TestPeriodicWork work)
        {
            await work.StartAsync(CancellationToken.None);
            await work.IterationSignal.WaitAsync(TimeSpan.FromSeconds(5));
            await work.StopAsync(CancellationToken.None);
        }

        [Fact]
        public async Task ExecuteAsync_HappyPath_TracksStartAndFinishAndShutdown()
        {
            var work = Create();
            work.OnRun = _ => { work.CallComplete("done"); return Task.CompletedTask; };

            await RunOneIterationAsync(work);

            Assert.True(work.HasResult);
            Assert.Equal("done", work.ResultReason);
            // Start + Finish events tracked.
            _tracker.Verify(x => x.TrackEvent(It.Is<string>(s => s.Contains("Start")), It.IsAny<Dimensions>(), It.IsAny<Measures>()), Times.AtLeastOnce);
            _tracker.Verify(x => x.TrackEvent(It.Is<string>(s => s.Contains("Complete")), It.IsAny<Dimensions>(), It.IsAny<Measures>()), Times.AtLeastOnce);
            // Shutdown trace.
            _tracker.Verify(x => x.TrackTrace("Periodic Work Stopped", SeverityLevel.Information, It.IsAny<Dimensions>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_NoOutcome_TracksException()
        {
            var work = Create();
            work.OnRun = _ => Task.CompletedTask; // never calls Complete

            await RunOneIterationAsync(work);

            _tracker.Verify(x => x.TrackException(It.IsAny<InvalidOperationException>(), It.IsAny<Dimensions>(), It.IsAny<Measures>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task ExecuteAsync_InnerRunThrows_TracksException()
        {
            var work = Create();
            work.OnRun = _ => throw new InvalidOperationException("run failed");

            await RunOneIterationAsync(work);

            _tracker.Verify(x => x.TrackException(It.IsAny<Exception>(), It.IsAny<Dimensions>(), It.IsAny<Measures>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task StartAsync_InnerStartThrows_TracksExceptionAndStillStarts()
        {
            var work = Create();
            work.OnStart = _ => throw new InvalidOperationException("start failed");
            work.OnRun = _ => { work.CallComplete("done"); return Task.CompletedTask; };

            await work.StartAsync(CancellationToken.None);
            await work.IterationSignal.WaitAsync(TimeSpan.FromSeconds(5), cancellationToken: TestContext.Current.CancellationToken);
            await work.StopAsync(CancellationToken.None);

            _tracker.Verify(x => x.TrackException(It.Is<Exception>(e => e.Message == "start failed"), It.IsAny<Dimensions>(), It.IsAny<Measures>()), Times.Once);
        }

        [Fact]
        public void Complete_Twice_Throws()
        {
            var work = Create();
            work.CallResetResult();
            work.CallComplete("first");
            Assert.Throws<InvalidOperationException>(() => work.CallComplete("second"));
        }

        [Fact]
        public void EnsureHasOutcome_ThrowsWhenNoResult()
        {
            var work = Create();
            work.CallResetResult();
            Assert.Throws<InvalidOperationException>(() => work.CallEnsureHasOutcome());
        }

        [Fact]
        public void ResetResult_ClearsState()
        {
            var work = Create();
            work.CallComplete("done");
            Assert.True(work.HasResult);
            work.CallResetResult();
            Assert.False(work.HasResult);
            Assert.Equal(string.Empty, work.ResultReason);
        }

        [Fact]
        public void GetKeyDimensions_IncludesWorkType()
        {
            var work = Create();
            var dimensions = work.GetKeyDimensions();
            Assert.Equal("TestWork", dimensions[StandardDimensions.WORK]);
        }
    }
}
