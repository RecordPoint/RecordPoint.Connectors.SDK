#nullable enable
using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using RecordPoint.Connectors.SDK.Databases.Cosmos.Telemetry;
using RecordPoint.Connectors.SDK.Observability;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos.Test.Telemetry
{
    public class CosmosRuTelemetryLoggerTests
    {
        private const string Metric = "Cosmos.RequestCharge";
        private const string Dimension = "Container";

        // EF Core Cosmos event ids
        private const int ExecutedReadNextId = 30102;
        private const int ExecutedReadItemId = 30103;
        private const int ExecutedCreateItemId = 30104;
        private const int ExecutedReplaceItemId = 30105;
        private const int ExecutedDeleteItemId = 30106;

        private readonly Mock<ITelemetryTracker> _telemetry = new();
        private readonly CosmosRuTelemetryLogger _sut;

        public CosmosRuTelemetryLoggerTests()
        {
            _sut = new CosmosRuTelemetryLogger(_telemetry.Object);
        }

        private void Log(int eventId, object? state)
            => _sut.Log(LogLevel.Information, new EventId(eventId), state, null, (s, _) => s?.ToString() ?? string.Empty);

        private static List<KeyValuePair<string, object?>> State(params (string Key, object? Value)[] pairs)
        {
            var list = new List<KeyValuePair<string, object?>>();
            foreach (var p in pairs)
            {
                list.Add(new KeyValuePair<string, object?>(p.Key, p.Value));
            }
            return list;
        }

        [Fact]
        public void IsEnabled_TrueForInformationAndAbove_FalseBelow()
        {
            Assert.True(_sut.IsEnabled(LogLevel.Information));
            Assert.True(_sut.IsEnabled(LogLevel.Warning));
            Assert.False(_sut.IsEnabled(LogLevel.Debug));
        }

        [Fact]
        public void BeginScope_ReturnsNull()
        {
            Assert.Null(_sut.BeginScope("state"));
        }

        [Fact]
        public void Log_TracksCharge_ForReadNext_WithDoubleCharge()
        {
            Log(ExecutedReadNextId, State(("charge", 4.2d), ("container", "channels")));

            _telemetry.Verify(t => t.TrackMetric(Metric, 4.2, Dimension, "channels"), Times.Once);
        }

        [Fact]
        public void Log_TracksCharge_ForReadItem_WithStringCharge()
        {
            Log(ExecutedReadItemId, State(("charge", "1.5"), ("container", "connectors")));

            _telemetry.Verify(t => t.TrackMetric(Metric, 1.5, Dimension, "connectors"), Times.Once);
        }

        [Theory]
        [InlineData(ExecutedCreateItemId)]
        [InlineData(ExecutedReplaceItemId)]
        [InlineData(ExecutedDeleteItemId)]
        public void Log_TracksCharge_ForAllTrackedItemEvents(int eventId)
        {
            Log(eventId, State(("charge", "2.0"), ("container", "aggregations")));

            _telemetry.Verify(t => t.TrackMetric(Metric, 2.0, Dimension, "aggregations"), Times.Once);
        }

        [Fact]
        public void Log_Ignores_UntrackedEventId()
        {
            Log(30100, State(("charge", 9.9d), ("container", "channels")));

            _telemetry.Verify(t => t.TrackMetric(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void Log_Ignores_StateThatIsNotKeyValueList()
        {
            Log(ExecutedReadItemId, "just a string");

            _telemetry.Verify(t => t.TrackMetric(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void Log_DoesNotTrack_WhenChargeIsZero()
        {
            Log(ExecutedReadItemId, State(("charge", 0d), ("container", "channels")));

            _telemetry.Verify(t => t.TrackMetric(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void Log_DoesNotTrack_WhenContainerMissing()
        {
            Log(ExecutedReadItemId, State(("charge", 3.0d)));

            _telemetry.Verify(t => t.TrackMetric(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void Log_DoesNotTrack_WhenContainerEmpty()
        {
            Log(ExecutedReadItemId, State(("charge", 3.0d), ("container", "")));

            _telemetry.Verify(t => t.TrackMetric(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void Log_TreatsUnparseableStringCharge_AsZero()
        {
            Log(ExecutedReadItemId, State(("charge", "not-a-number"), ("container", "channels")));

            _telemetry.Verify(t => t.TrackMetric(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void Log_TreatsUnexpectedChargeType_AsZero()
        {
            Log(ExecutedReadItemId, State(("charge", new object()), ("container", "channels")));

            _telemetry.Verify(t => t.TrackMetric(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void Log_IgnoresUnrelatedKeys()
        {
            Log(ExecutedReadNextId, State(("elapsed", 100), ("charge", 5.0d), ("container", "channels"), ("other", "x")));

            _telemetry.Verify(t => t.TrackMetric(Metric, 5.0, Dimension, "channels"), Times.Once);
        }
    }

    public class CosmosRuTelemetryLoggerProviderTests
    {
        [Fact]
        public void CreateLogger_ReturnsRuLogger_ForCommandCategory()
        {
            var telemetry = new Mock<ITelemetryTracker>();
            using var provider = new CosmosRuTelemetryLoggerProvider(telemetry.Object);

            var logger = provider.CreateLogger("Microsoft.EntityFrameworkCore.Database.Command");

            Assert.IsType<CosmosRuTelemetryLogger>(logger);
        }

        [Fact]
        public void CreateLogger_ReturnsNullLogger_ForOtherCategory()
        {
            var telemetry = new Mock<ITelemetryTracker>();
            using var provider = new CosmosRuTelemetryLoggerProvider(telemetry.Object);

            var logger = provider.CreateLogger("Some.Other.Category");

            Assert.Same(NullLogger.Instance, logger);
        }

        [Fact]
        public void Dispose_DoesNotThrow()
        {
            var telemetry = new Mock<ITelemetryTracker>();
            var provider = new CosmosRuTelemetryLoggerProvider(telemetry.Object);

            var ex = Record.Exception(() => provider.Dispose());

            Assert.Null(ex);
        }

        [Fact]
        public void CommandCategoryLogger_EmitsMetric_EndToEnd()
        {
            var telemetry = new Mock<ITelemetryTracker>();
            using var provider = new CosmosRuTelemetryLoggerProvider(telemetry.Object);
            var logger = provider.CreateLogger("Microsoft.EntityFrameworkCore.Database.Command");

            var state = new List<KeyValuePair<string, object?>>
            {
                new("charge", 2.5d.ToString(CultureInfo.InvariantCulture)),
                new("container", "channels")
            };
            logger.Log(LogLevel.Information, new EventId(30103), (IReadOnlyList<KeyValuePair<string, object?>>)state, null, (s, _) => "msg");

            telemetry.Verify(t => t.TrackMetric("Cosmos.RequestCharge", 2.5, "Container", "channels"), Times.Once);
        }
    }
}
