#nullable enable
using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Options;
using Moq;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Observability.AppInsights;
using RecordPoint.Connectors.SDK.Toggles;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Observability
{
    public class TelemetryClientFactoryTests
    {
        [Fact]
        public void GetTelemetryClient_ThrowsWhenNotConfigured()
        {
            var factory = new TelemetryClientFactory(Options.Create(new ApplicationInsightOptions()));
            Assert.Throws<RequiredValueNullException>(() => factory.GetTelemetryClient());
        }

        [Fact]
        public void GetTelemetryClient_WithConnectionString_ReturnsCachedClient()
        {
            var factory = new TelemetryClientFactory(Options.Create(new ApplicationInsightOptions
            {
                ConnectionString = "InstrumentationKey=00000000-0000-0000-0000-000000000000"
            }));
            var client = factory.GetTelemetryClient();
            Assert.NotNull(client);
            Assert.Same(client, factory.GetTelemetryClient());
        }
    }

    public class ApplicationInsightsTelemetrySinkTests
    {
        private readonly Mock<ITelemetryClientFactory> _clientFactory = new();
        private readonly Mock<IToggleProvider> _toggleProvider = new();
        private readonly Mock<ISystemContext> _systemContext = new();

        public ApplicationInsightsTelemetrySinkTests()
        {
            _systemContext.Setup(x => x.GetConnectorName()).Returns("Connector");
            _toggleProvider.Setup(x => x.GetToggleBool(It.IsAny<string>(), It.IsAny<bool>())).Returns(true);
        }

        private ApplicationInsightsTelemetrySink CreateSink(ApplicationInsightOptions options)
            => new(_clientFactory.Object, Options.Create(options), _toggleProvider.Object, _systemContext.Object);

        private static ApplicationInsightOptions ConfiguredOptions(SeverityLevel logLevel = SeverityLevel.Verbose) => new()
        {
            ConnectionString = "InstrumentationKey=00000000-0000-0000-0000-000000000000",
            LogLevel = logLevel
        };

        /// <summary>
        /// Wires the factory mock to return a real <see cref="TelemetryClient"/>.
        /// TelemetryClient is sealed in ApplicationInsights 3.x; content-level assertions are
        /// not feasible without an OTel in-memory exporter, so tests verify factory interactions.
        /// </summary>
        private void SetupRealClient()
        {
            var config = new TelemetryConfiguration
            {
                ConnectionString = "InstrumentationKey=00000000-0000-0000-0000-000000000000"
            };
            _clientFactory.Setup(x => x.GetTelemetryClient()).Returns(new TelemetryClient(config));
        }

        [Fact]
        public void TrackEvent_NotConfigured_DoesNothing()
        {
            var sink = CreateSink(new ApplicationInsightOptions());
            sink.TrackEvent("evt", new Dimensions(), new Measures());
            _clientFactory.Verify(x => x.GetTelemetryClient(), Times.Never);
        }

        [Fact]
        public void TrackException_NotConfigured_DoesNothing()
        {
            var sink = CreateSink(new ApplicationInsightOptions());
            sink.TrackException(new Exception(), new Dimensions(), new Measures());
            _clientFactory.Verify(x => x.GetTelemetryClient(), Times.Never);
        }

        [Fact]
        public void TrackMetric_NotConfigured_DoesNothing()
        {
            var sink = CreateSink(new ApplicationInsightOptions());
            sink.TrackMetric("m", 1.0, new Dimensions());
            _clientFactory.Verify(x => x.GetTelemetryClient(), Times.Never);
        }

        [Fact]
        public void TrackTrace_BelowLogLevel_DoesNothing()
        {
            var sink = CreateSink(ConfiguredOptions(SeverityLevel.Error));
            sink.TrackTrace("msg", SeverityLevel.Information);
            _clientFactory.Verify(x => x.GetTelemetryClient(), Times.Never);
        }

        [Fact]
        public void TrackEvent_Enabled_CallsClient()
        {
            SetupRealClient();
            var sink = CreateSink(ConfiguredOptions());
            sink.TrackEvent("evt", new Dimensions { ["a"] = "1" }, new Measures { ["m"] = 1 });
            _clientFactory.Verify(x => x.GetTelemetryClient(), Times.AtLeastOnce);
        }

        [Fact]
        public void TrackEvent_ToggleDisabled_DoesNotCallClient()
        {
            SetupRealClient();
            _toggleProvider.Setup(x => x.GetToggleBool(It.IsAny<string>(), It.IsAny<bool>())).Returns(false);
            var sink = CreateSink(ConfiguredOptions());

            sink.TrackEvent("evt");

            // IsEnabled short-circuits once the feature toggle is off; the factory is never consulted for tracking.
            _clientFactory.Verify(x => x.GetTelemetryClient(), Times.Never);
        }

        [Fact]
        public void TrackException_Enabled_CallsClient()
        {
            SetupRealClient();
            var sink = CreateSink(ConfiguredOptions());
            sink.TrackException(new InvalidOperationException("x"), new Dimensions { ["a"] = "1" }, new Measures());
            _clientFactory.Verify(x => x.GetTelemetryClient(), Times.AtLeastOnce);
        }

        [Theory]
        [InlineData(SeverityLevel.Verbose)]
        [InlineData(SeverityLevel.Information)]
        [InlineData(SeverityLevel.Warning)]
        [InlineData(SeverityLevel.Error)]
        [InlineData(SeverityLevel.Critical)]
        public void TrackTrace_Enabled_CallsClient(SeverityLevel level)
        {
            SetupRealClient();
            var sink = CreateSink(ConfiguredOptions());
            sink.TrackTrace("msg", level, new Dimensions { ["a"] = "1" });
            _clientFactory.Verify(x => x.GetTelemetryClient(), Times.AtLeastOnce);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void TrackMetric_Enabled_CallsClient(int dimensionCount)
        {
            SetupRealClient();
            var sink = CreateSink(ConfiguredOptions());
            var dimensions = new Dimensions();
            for (var i = 0; i < dimensionCount; i++)
                dimensions[$"key{i}"] = $"value{i}";

            sink.TrackMetric("metric", 3.14, dimensions);

            _clientFactory.Verify(x => x.GetTelemetryClient(), Times.AtLeastOnce);
        }
    }
}
