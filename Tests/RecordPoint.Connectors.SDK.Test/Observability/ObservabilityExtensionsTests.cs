#nullable enable
using System;
using Moq;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Observability.Null;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Observability
{
    public class ISystemContextExtensionsTests
    {
        private static Mock<ISystemContext> MockContext(string connector = "Connector", string company = "Company")
        {
            var mock = new Mock<ISystemContext>();
            mock.Setup(x => x.GetConnectorName()).Returns(connector);
            mock.Setup(x => x.GetCompanyName()).Returns(company);
            return mock;
        }

        [Fact]
        public void GetDimensions_ReturnsSystemAndCompany()
        {
            var dimensions = MockContext().Object.GetDimensions();
            Assert.Equal("Connector", dimensions[StandardDimensions.SYSTEM]);
            Assert.Equal("Company", dimensions[StandardDimensions.COMPANY]);
        }

        [Fact]
        public void GetMetricDimensions_ReturnsSystem_WhenConnectorPresent()
        {
            var dimensions = MockContext().Object.GetMetricDimensions();
            Assert.Single(dimensions);
            Assert.Equal("Connector", dimensions[StandardDimensions.SYSTEM]);
        }

        [Fact]
        public void GetMetricDimensions_ReturnsEmpty_WhenConnectorEmpty()
        {
            var dimensions = MockContext(connector: "").Object.GetMetricDimensions();
            Assert.Empty(dimensions);
        }
    }

    public class ITelemetryTrackerExtensionsTests
    {
        [Fact]
        public void CreateRootServiceScope_BeginsScopeWithStandardDimensions()
        {
            var systemContext = new Mock<ISystemContext>();
            systemContext.Setup(x => x.GetCompanyName()).Returns("Company");
            systemContext.Setup(x => x.GetConnectorName()).Returns("Connector");
            systemContext.Setup(x => x.GetServiceName()).Returns("Service");

            var tracker = new Mock<ITelemetryTracker>();
            Dimensions? captured = null;
            tracker.Setup(x => x.BeginScope(It.IsAny<Dimensions>(), It.IsAny<Measures>()))
                .Callback<Dimensions, Measures>((d, _) => captured = d)
                .Returns(new NullObservabilityScope());

            tracker.Object.CreateRootServiceScope(systemContext.Object, new Dimensions { ["extra"] = "1" });

            Assert.NotNull(captured);
            Assert.Equal("Company", captured![StandardDimensions.COMPANY]);
            Assert.Equal("Connector", captured[StandardDimensions.SYSTEM]);
            Assert.Equal("Service", captured[StandardDimensions.SERVICE]);
            Assert.Equal("1", captured["extra"]);
        }

        [Fact]
        public void CreateRootServiceScope_DoesNotOverrideStandardDimensions()
        {
            var systemContext = new Mock<ISystemContext>();
            systemContext.Setup(x => x.GetCompanyName()).Returns("Company");
            systemContext.Setup(x => x.GetConnectorName()).Returns("Connector");
            systemContext.Setup(x => x.GetServiceName()).Returns("Service");

            var tracker = new Mock<ITelemetryTracker>();
            Dimensions? captured = null;
            tracker.Setup(x => x.BeginScope(It.IsAny<Dimensions>(), It.IsAny<Measures>()))
                .Callback<Dimensions, Measures>((d, _) => captured = d)
                .Returns(new NullObservabilityScope());

            tracker.Object.CreateRootServiceScope(systemContext.Object,
                new Dimensions { [StandardDimensions.COMPANY] = "Override" });

            Assert.Equal("Company", captured![StandardDimensions.COMPANY]);
        }
    }

    public class ContextTelemetryExtensionsTests
    {
        [Fact]
        public void BeginSystemScope_AddsCompanySystemService()
        {
            var systemContext = new Mock<ISystemContext>();
            systemContext.Setup(x => x.GetCompanyName()).Returns("Company");
            systemContext.Setup(x => x.GetConnectorName()).Returns("Connector");
            systemContext.Setup(x => x.GetServiceName()).Returns("Service");

            var scope = new ObservabilityScope();
            using (scope.BeginSystemScope(systemContext.Object))
            {
                Assert.Equal("Company", scope.Dimensions[StandardDimensions.COMPANY]);
                Assert.Equal("Connector", scope.Dimensions[StandardDimensions.SYSTEM]);
                Assert.Equal("Service", scope.Dimensions[StandardDimensions.SERVICE]);
            }
        }

        [Fact]
        public void BeginServiceScope_AddsServiceId()
        {
            var scope = new ObservabilityScope();
            using (scope.BeginServiceScope("service-123"))
            {
                Assert.Equal("service-123", scope.Dimensions[StandardDimensions.SERVICE_ID]);
            }
        }
    }
}
