#nullable enable
using System;
using System.IO;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Observability.Console;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Observability
{
    [Collection(ConsoleOutputCollection.Name)]
    public class ConsoleLoggingSinkTests
    {
        private static ConsoleLoggingSink CreateSink(ConsoleLoggingOptions? options = null)
            => new(Options.Create(options ?? new ConsoleLoggingOptions()));

        /// <summary>
        /// Redirects <see cref="System.Console.Out"/> to an in-memory writer for the duration of
        /// <paramref name="act"/>, returning everything the sink wrote so it can be asserted on.
        /// The original writer is always restored.
        /// </summary>
        private static string CaptureConsole(Action act)
        {
            var original = System.Console.Out;
            using var writer = new StringWriter();
            System.Console.SetOut(writer);
            try
            {
                act();
            }
            finally
            {
                System.Console.SetOut(original);
            }

            return writer.ToString();
        }

        [Fact]
        public void TrackEvent_WritesNameDimensionsAndMeasures()
        {
            var sink = CreateSink(new ConsoleLoggingOptions { WriteDimensions = true, WriteMeasures = true });

            var output = CaptureConsole(() =>
                sink.TrackEvent("event", new Dimensions { ["a"] = "1" }, new Measures { ["m"] = 2 }));

            Assert.Contains("EVNT: event", output);
            Assert.Contains("a: 1", output);
            Assert.Contains("m: 2", output);
        }

        [Fact]
        public void TrackEvent_WritesName_WithNulls()
        {
            var sink = CreateSink();

            var output = CaptureConsole(() => sink.TrackEvent("event"));

            Assert.Contains("EVNT: event", output);
        }

        [Fact]
        public void TrackException_WritesFailLineExceptionAndDetails()
        {
            var sink = CreateSink(new ConsoleLoggingOptions { WriteDimensions = true, WriteMeasures = true });
            var exception = new InvalidOperationException("boom");

            var output = CaptureConsole(() =>
                sink.TrackException(exception, new Dimensions { ["a"] = "1" }, new Measures { ["m"] = 2 }));

            Assert.Contains("FAIL: [System.InvalidOperationException]", output);
            Assert.Contains("boom", output);
            Assert.Contains("a: 1", output);
            Assert.Contains("m: 2", output);
        }

        [Fact]
        public void TrackMetric_WritesWhenEnabled()
        {
            var sink = CreateSink(new ConsoleLoggingOptions { WriteMeasures = true });

            var output = CaptureConsole(() =>
                sink.TrackMetric("metric", 1.23, new Dimensions { ["a"] = "1" }));

            Assert.Contains("METR: metric = 1.23 [a=1]", output);
        }

        [Fact]
        public void TrackMetric_WritesNothingWhenDisabled()
        {
            var sink = CreateSink(new ConsoleLoggingOptions { WriteMeasures = false });

            var output = CaptureConsole(() =>
                sink.TrackMetric("metric", 1.23, new Dimensions { ["a"] = "1" }));

            Assert.DoesNotContain("METR", output);
            Assert.Equal(string.Empty, output);
        }

        [Fact]
        public void TrackMetric_HandlesNullDimensions()
        {
            var sink = CreateSink(new ConsoleLoggingOptions { WriteMeasures = true });

            var output = CaptureConsole(() => sink.TrackMetric("metric", 1.23, null));

            Assert.Contains("METR: metric = 1.23", output);
            Assert.DoesNotContain("[", output);
        }

        [Theory]
        [InlineData(SeverityLevel.Verbose, "DBUG")]
        [InlineData(SeverityLevel.Information, "INFO")]
        [InlineData(SeverityLevel.Warning, "WARN")]
        [InlineData(SeverityLevel.Error, "FAIL")]
        [InlineData(SeverityLevel.Critical, "CRIT")]
        public void TrackTrace_WritesLevelPrefixMessageAndDimensions(SeverityLevel level, string expectedPrefix)
        {
            var sink = CreateSink(new ConsoleLoggingOptions { LogLevel = SeverityLevel.Verbose, WriteDimensions = true });

            var output = CaptureConsole(() =>
                sink.TrackTrace("message", level, new Dimensions { ["a"] = "1" }));

            Assert.Contains($"{expectedPrefix}: message", output);
            Assert.Contains("a: 1", output);
        }

        [Fact]
        public void TrackTrace_WritesNothingBelowLogLevel()
        {
            var sink = CreateSink(new ConsoleLoggingOptions { LogLevel = SeverityLevel.Error });

            var output = CaptureConsole(() => sink.TrackTrace("message", SeverityLevel.Information));

            Assert.Equal(string.Empty, output);
        }
    }
}
