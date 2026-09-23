#nullable enable
using System;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Observability.Null;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Observability
{
    public class NullTelemetryTrackerTests
    {
        [Fact]
        public void BeginScope_ReturnsNullScope()
        {
            var tracker = new NullTelemetryTracker();
            using var scope = tracker.BeginScope();
            Assert.IsType<NullObservabilityScope>(scope);
        }

        [Fact]
        public void TrackMethods_DoNotThrow()
        {
            var tracker = new NullTelemetryTracker();
            Assert.Null(Record.Exception(() =>
            {
                tracker.TrackEvent("evt", new Dimensions(), new Measures());
                tracker.TrackException(new Exception(), new Dimensions(), new Measures());
                tracker.TrackMetric("metric", 1.0, "dim", "val");
                tracker.TrackTrace("trace", SeverityLevel.Information, new Dimensions());
            }));
        }

        [Fact]
        public void NullObservabilityScope_DisposeIsIdempotent()
        {
            var scope = new NullObservabilityScope();
            Assert.Null(Record.Exception(() =>
            {
                scope.Dispose();
                scope.Dispose();
            }));
        }
    }
}
