#nullable enable
using System;
using System.Collections.Generic;
using Moq;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Observability
{
    public class TelemetryTrackerTests
    {
        private readonly ObservabilityScope _scope = new();
        private readonly Mock<ITelemetrySink> _sink = new();
        private readonly Mock<ISystemContext> _systemContext = new();

        private TelemetryTracker CreateTracker(IEnumerable<ITelemetrySink>? sinks = null)
        {
            _systemContext.Setup(x => x.GetConnectorName()).Returns("Connector");
            _systemContext.Setup(x => x.GetCompanyName()).Returns("Company");
            _systemContext.Setup(x => x.GetServiceName()).Returns("Service");
            return new TelemetryTracker(_scope, sinks ?? new[] { _sink.Object }, _systemContext.Object);
        }

        [Fact]
        public void BeginScope_DelegatesToObservabilityScope()
        {
            var tracker = CreateTracker();
            using (tracker.BeginScope(new Dimensions { ["a"] = "1" }))
            {
                Assert.Equal("1", _scope.Dimensions["a"]);
            }
            Assert.Empty(_scope.Dimensions);
        }

        [Fact]
        public void TrackEvent_ForwardsToSink_WithGatheredDimensions()
        {
            var tracker = CreateTracker();
            Dimensions? captured = null;
            _sink.Setup(x => x.TrackEvent("evt", It.IsAny<Dimensions>(), It.IsAny<Measures>()))
                .Callback<string, Dimensions, Measures>((_, d, _) => captured = d);

            tracker.TrackEvent("evt", new Dimensions { ["custom"] = "val" });

            _sink.Verify(x => x.TrackEvent("evt", It.IsAny<Dimensions>(), It.IsAny<Measures>()), Times.Once);
            Assert.NotNull(captured);
            Assert.Equal("val", captured!["custom"]);
            // System context dimensions merged in.
            Assert.Equal("Connector", captured[StandardDimensions.SYSTEM]);
            Assert.Equal("Company", captured[StandardDimensions.COMPANY]);
        }

        [Fact]
        public void TrackException_NullExceptionIgnored()
        {
            var tracker = CreateTracker();
            tracker.TrackException(null!);
            _sink.Verify(x => x.TrackException(It.IsAny<Exception>(), It.IsAny<Dimensions>(), It.IsAny<Measures>()), Times.Never);
        }

        [Fact]
        public void TrackException_ForwardsToSink()
        {
            var tracker = CreateTracker();
            var ex = new InvalidOperationException("x");
            ex.GetDimensions(); // seed empty
            tracker.TrackException(ex, new Dimensions { ["d"] = "1" }, new Measures { ["m"] = 2 });
            _sink.Verify(x => x.TrackException(ex, It.IsAny<Dimensions>(), It.IsAny<Measures>()), Times.Once);
        }

        [Fact]
        public void TrackMetric_ForwardsToSink_WithMetricDimensions()
        {
            var tracker = CreateTracker();
            Dimensions? captured = null;
            _sink.Setup(x => x.TrackMetric("metric", 5.0, It.IsAny<Dimensions>()))
                .Callback<string, double, Dimensions>((_, _, d) => captured = d);

            tracker.TrackMetric("metric", 5.0, "extra", "value");

            _sink.Verify(x => x.TrackMetric("metric", 5.0, It.IsAny<Dimensions>()), Times.Once);
            Assert.NotNull(captured);
            Assert.Equal("Connector", captured![StandardDimensions.SYSTEM]);
            Assert.Equal("value", captured["extra"]);
        }

        [Fact]
        public void TrackMetric_WithoutExtraDimension()
        {
            var tracker = CreateTracker();
            tracker.TrackMetric("metric", 1.0);
            _sink.Verify(x => x.TrackMetric("metric", 1.0, It.IsAny<Dimensions>()), Times.Once);
        }

        [Fact]
        public void TrackTrace_ForwardsToSink()
        {
            var tracker = CreateTracker();
            tracker.TrackTrace("msg", SeverityLevel.Warning, new Dimensions { ["a"] = "1" });
            _sink.Verify(x => x.TrackTrace("msg", SeverityLevel.Warning, It.IsAny<Dimensions>()), Times.Once);
        }

        [Fact]
        public void TrackEvent_MergesScopeDimensions()
        {
            var tracker = CreateTracker();
            Dimensions? captured = null;
            _sink.Setup(x => x.TrackEvent("evt", It.IsAny<Dimensions>(), It.IsAny<Measures>()))
                .Callback<string, Dimensions, Measures>((_, d, _) => captured = d);

            using (_scope.BeginScope(new Dimensions { ["scoped"] = "yes" }, new Measures { ["sm"] = 9 }))
            {
                tracker.TrackEvent("evt");
            }
            Assert.NotNull(captured);
            Assert.Equal("yes", captured!["scoped"]);
        }
    }
}
