#nullable enable
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RecordPoint.Connectors.SDK.Health;
using RecordPoint.Connectors.SDK.WebHost.Api.Controllers;
using Xunit;

namespace RecordPoint.Connectors.SDK.WebHost.Test
{
    public class HealthControllerTests
    {
        private readonly Mock<IHealthCheckManager> _healthCheckManager = new();
        private readonly Mock<IHealthCheckLiveAction> _liveAction = new();
        private readonly Mock<IHealthCheckReadyAction> _readyAction = new();

        [Fact]
        public void Get_ReturnsHealthCheckResultFromManager()
        {
            var expected = new HealthCheckResult { LastUpdate = DateTimeOffset.UtcNow };
            _healthCheckManager.SetupGet(m => m.HealthCheckResult).Returns(expected);
            var sut = CreateSubject();

            var result = sut.Get();

            Assert.Same(expected, result);
        }

        [Fact]
        public async Task Livez_WhenLive_ReturnsOk()
        {
            _liveAction.Setup(a => a.CheckIsLiveAsync()).ReturnsAsync(true);
            var sut = CreateSubject();

            var result = await sut.Livez();

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Livez_WhenNotLive_Returns503()
        {
            _liveAction.Setup(a => a.CheckIsLiveAsync()).ReturnsAsync(false);
            var sut = CreateSubject();

            var result = await sut.Livez();

            var statusResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(503, statusResult.StatusCode);
        }

        [Fact]
        public async Task Readyz_WhenReady_ReturnsOk()
        {
            _readyAction.Setup(a => a.CheckIsReadyAsync()).ReturnsAsync(true);
            var sut = CreateSubject();

            var result = await sut.Readyz();

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Readyz_WhenNotReady_Returns503()
        {
            _readyAction.Setup(a => a.CheckIsReadyAsync()).ReturnsAsync(false);
            var sut = CreateSubject();

            var result = await sut.Readyz();

            var statusResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(503, statusResult.StatusCode);
        }

        private HealthController CreateSubject()
        {
            var services = new ServiceCollection();
            services.AddSingleton(_liveAction.Object);
            services.AddSingleton(_readyAction.Object);
            var serviceProvider = services.BuildServiceProvider();

            return new HealthController(serviceProvider, _healthCheckManager.Object);
        }
    }
}
