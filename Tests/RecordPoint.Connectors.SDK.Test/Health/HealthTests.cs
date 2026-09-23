#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moq;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Health;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Health
{
    public class UptimeStrategyTests
    {
        [Fact]
        public void HealthCheckType_IsUptime()
        {
            var clock = new Mock<IDateTimeProvider>();
            clock.SetupGet(x => x.UtcNow).Returns(DateTime.UtcNow);
            var strategy = new UptimeStrategy(clock.Object);
            Assert.Equal(UptimeStrategy.HEALTH_CHECK_TYPE, strategy.HealthCheckType);
        }

        [Fact]
        public async Task HealthCheckAsync_ReturnsPositiveUptime()
        {
            var start = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var clock = new Mock<IDateTimeProvider>();
            var current = start;
            clock.SetupGet(x => x.UtcNow).Returns(() => current);
            var strategy = new UptimeStrategy(clock.Object);

            current = start.AddSeconds(42);
            var result = await strategy.HealthCheckAsync(CancellationToken.None);

            var measure = Assert.Single(result.Measures);
            Assert.Equal(UptimeStrategy.UPTIME_SECONDS_NAME, measure.Name);
            Assert.Equal(42, measure.Value);
            Assert.Equal(UptimeStrategy.HEALTH_CHECK_TYPE, measure.HealthCheckType);
        }
    }

    public class HealthCheckManagerTests
    {
        private static Mock<IDateTimeProvider> Clock()
        {
            var clock = new Mock<IDateTimeProvider>();
            clock.SetupGet(x => x.UtcNow).Returns(new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            return clock;
        }

        [Fact]
        public async Task RunHealthCheckAsync_AggregatesStrategyResults()
        {
            var strategy = new Mock<IHealthCheckStrategy>();
            strategy.SetupGet(x => x.HealthCheckType).Returns("TypeA");
            strategy.Setup(x => x.HealthCheckAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HealthCheckResult
                {
                    Dimensions = new List<HealthCheckDimension> { new() { Name = "d1", Value = "v1" } },
                    Measures = new List<HealthCheckMeasure> { new() { Name = "m1", Value = 1 } }
                });

            var manager = new HealthCheckManager(new[] { strategy.Object }, Clock().Object);
            await manager.RunHealthCheckAsync(CancellationToken.None);

            Assert.NotNull(manager.HealthCheckResult);
            Assert.Equal(new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), manager.HealthCheckResult.LastUpdate);
            var dim = Assert.Single(manager.HealthCheckResult.Dimensions);
            Assert.Equal("TypeA", dim.HealthCheckType); // stamped by manager
            var measure = Assert.Single(manager.HealthCheckResult.Measures);
            Assert.Equal("TypeA", measure.HealthCheckType);
        }

        [Fact]
        public async Task RunHealthCheckAsync_StrategyException_ReportsWarning()
        {
            var strategy = new Mock<IHealthCheckStrategy>();
            strategy.SetupGet(x => x.HealthCheckType).Returns("Faulty");
            strategy.Setup(x => x.HealthCheckAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("failure message"));

            var manager = new HealthCheckManager(new[] { strategy.Object }, Clock().Object);
            await manager.RunHealthCheckAsync(CancellationToken.None);

            var dim = Assert.Single(manager.HealthCheckResult.Dimensions);
            Assert.Equal(HealthCheckManager.HEALTH_CHECK_FAULT_NAME, dim.Name);
            Assert.Equal(HealthLevel.Warning, dim.HealthLevel);
            Assert.Equal("failure message", dim.Value);
        }

        [Fact]
        public async Task RunHealthCheckAsync_NoStrategies_ProducesEmptyResult()
        {
            var manager = new HealthCheckManager(Enumerable.Empty<IHealthCheckStrategy>(), Clock().Object);
            await manager.RunHealthCheckAsync(CancellationToken.None);
            Assert.Empty(manager.HealthCheckResult.Dimensions);
            Assert.Empty(manager.HealthCheckResult.Measures);
        }
    }

    public class DefaultHealthCheckLiveActionTests
    {
        [Fact]
        public async Task CheckIsLiveAsync_TrueWhenUptimePositive()
        {
            var manager = new Mock<IHealthCheckManager>();
            manager.SetupGet(x => x.HealthCheckResult).Returns(new HealthCheckResult
            {
                Measures = new List<HealthCheckMeasure>
                {
                    new() { HealthCheckType = UptimeStrategy.HEALTH_CHECK_TYPE, Name = UptimeStrategy.UPTIME_SECONDS_NAME, Value = 5 }
                }
            });
            var action = new DefaultHealthCheckLiveAction(manager.Object);
            Assert.True(await action.CheckIsLiveAsync());
        }

        [Fact]
        public async Task CheckIsLiveAsync_FalseWhenNoUptime()
        {
            var manager = new Mock<IHealthCheckManager>();
            manager.SetupGet(x => x.HealthCheckResult).Returns(new HealthCheckResult());
            var action = new DefaultHealthCheckLiveAction(manager.Object);
            Assert.False(await action.CheckIsLiveAsync());
        }
    }

    public class HealthCheckServiceTests
    {
        [Fact]
        public async Task ExecuteAsync_RunsHealthCheck()
        {
            var manager = new Mock<IHealthCheckManager>();
            var ranSignal = new SemaphoreSlim(0);
            manager.Setup(x => x.RunHealthCheckAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Callback(() => ranSignal.Release());

            var systemContext = new Mock<ISystemContext>();
            systemContext.Setup(x => x.GetCompanyName()).Returns("Company");
            systemContext.Setup(x => x.GetConnectorName()).Returns("Connector");
            systemContext.Setup(x => x.GetServiceName()).Returns("Service");

            // Large frequency so the service performs one health check then waits (no busy loop).
            var options = Options.Create(new HealthCheckOptions
            {
                HealthCheckStartDelaySeconds = 0,
                HealthCheckFrequencySeconds = 3600
            });

            var service = new HealthCheckService(manager.Object, systemContext.Object, new ObservabilityScope(), options);

            await service.StartAsync(CancellationToken.None);
            var ran = await ranSignal.WaitAsync(TimeSpan.FromSeconds(5), cancellationToken: TestContext.Current.CancellationToken);
            await service.StopAsync(CancellationToken.None);

            Assert.True(ran);
            manager.Verify(x => x.RunHealthCheckAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
        }
    }
}
