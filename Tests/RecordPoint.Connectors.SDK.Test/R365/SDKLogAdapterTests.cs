#nullable enable
using System;
using Moq;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.R365;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.R365
{
    public class SDKLogAdapterTests
    {
        private readonly Mock<ITelemetryTracker> _telemetryTracker = new();
        private readonly SDKLogAdapter _sut;

        public SDKLogAdapterTests()
        {
            _sut = new SDKLogAdapter(_telemetryTracker.Object);
        }

        [Fact]
        public void LogMessage_TracksInformationTrace()
        {
            _sut.LogMessage(typeof(SDKLogAdapterTests), "Method", "a message", 123);
            _telemetryTracker.Verify(x => x.TrackTrace(
                It.Is<string>(s => s.Contains("Message::") && s.Contains("a message") && s.Contains("123")),
                SeverityLevel.Information,
                It.IsAny<Dimensions>()), Times.Once);
        }

        [Fact]
        public void LogVerbose_TracksVerboseTrace_DefaultTicks()
        {
            _sut.LogVerbose(typeof(SDKLogAdapterTests), "Method", "verbose message");
            _telemetryTracker.Verify(x => x.TrackTrace(
                It.Is<string>(s => s.Contains("Verbose::") && s.Contains("-1")),
                SeverityLevel.Verbose,
                It.IsAny<Dimensions>()), Times.Once);
        }

        [Fact]
        public void LogWarning_TracksWarningTrace()
        {
            _sut.LogWarning(typeof(SDKLogAdapterTests), "Method", "warn message", 5);
            _telemetryTracker.Verify(x => x.TrackTrace(
                It.Is<string>(s => s.Contains("Warning::") && s.Contains("warn message")),
                SeverityLevel.Warning,
                It.IsAny<Dimensions>()), Times.Once);
        }
    }
}
