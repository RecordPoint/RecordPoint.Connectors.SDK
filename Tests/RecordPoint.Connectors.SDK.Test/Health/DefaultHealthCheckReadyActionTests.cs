using FluentAssertions;
using Moq;
using RecordPoint.Connectors.SDK.Health;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Health
{
    public class DefaultHealthCheckReadyActionTests
    {
        private readonly Mock<IHealthCheckManager> _mockHealthManager = new();
        private readonly DefaultHealthCheckReadyAction _sut;

        public DefaultHealthCheckReadyActionTests()
        {
            _sut = new(_mockHealthManager.Object);
        }

        [Fact]
        public async Task CheckIsReadyAsync_NoHealthCheckResult_ShouldBeNotReady()
        {
            var isReady = await _sut.CheckIsReadyAsync();
            isReady.Should().BeFalse();
        }

        [Fact]
        public async Task CheckIsReadyAsync_UptimeHealthCheckMetric_ShouldBeReady()
        {
            var uptimeMeasure = new HealthCheckMeasure
            {
                HealthCheckType = UptimeStrategy.HEALTH_CHECK_TYPE,
                Name = UptimeStrategy.UPTIME_SECONDS_NAME,
                Value = 10
            };
            var healthCheckResult = new HealthCheckResult
            {  
                Measures = [uptimeMeasure]
            };

            _mockHealthManager.SetupGet(hm => hm.HealthCheckResult).Returns(healthCheckResult);

            var isReady = await _sut.CheckIsReadyAsync();

            isReady.Should().BeTrue();
        }
    }
}
