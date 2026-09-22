#nullable enable
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Configuration;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Health;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Observability.AppInsights;
using RecordPoint.Connectors.SDK.Observability.Console;
using RecordPoint.Connectors.SDK.Observability.Null;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Status;
using RecordPoint.Connectors.SDK.Time;
using RecordPoint.Connectors.SDK.Toggles;
using RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles;
using RecordPoint.Connectors.SDK.Toggles.Null;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.SUT
{
    /// <summary>
    /// Verifies the host builder extension methods register their expected services.
    /// </summary>
    public class HostBuilderExtensionsTests
    {
        private static IServiceCollection Capture(Action<IHostBuilder> apply)
        {
            IServiceCollection? captured = null;
            var builder = Host.CreateDefaultBuilder().UseEnvironment("Production");
            apply(builder);
            builder.ConfigureServices(services => captured = services);
            using var host = builder.Build();
            Assert.NotNull(captured);
            return captured!;
        }

        private static bool Has<TService>(IServiceCollection services)
            => services.Any(d => d.ServiceType == typeof(TService));

        private static bool HasImplementation<TService, TImpl>(IServiceCollection services)
            => services.Any(d => d.ServiceType == typeof(TService) && d.ImplementationType == typeof(TImpl));

        [Fact]
        public void UseR365AppSettingsConfiguration_RegistersConfigurationClient()
        {
            var services = Capture(b => b.UseR365AppSettingsConfiguration());
            Assert.True(HasImplementation<IR365ConfigurationClient, AppSettingsR365ConfigurationClient>(services));
        }

        [Fact]
        public void UseSystemContext_RegistersSystemContext()
        {
            var services = Capture(b => b.UseSystemContext("Company", "Connector", "Short", "Service"));
            Assert.True(HasImplementation<ISystemContext, SystemContext>(services));
        }

        [Fact]
        public void UseObservabilityTracking_RegistersScopeAndTracker()
        {
            var services = Capture(b => b.UseObservabilityTracking());
            Assert.True(HasImplementation<IObservabilityScope, ObservabilityScope>(services));
            Assert.True(HasImplementation<ITelemetryTracker, TelemetryTracker>(services));
        }

        [Fact]
        public void UseNullTelemetryTracking_RegistersNullTracker()
        {
            var services = Capture(b => b.UseNullTelemetryTracking());
            Assert.True(Has<ITelemetryTracker>(services));
        }

        [Fact]
        public void UseNullToggleProvider_RegistersNullToggleProvider()
        {
            var services = Capture(b => b.UseNullToggleProvider());
            Assert.True(HasImplementation<IToggleProvider, NullToggleProvider>(services));
        }

        [Fact]
        public void UseLocalFileToggleProvider_RegistersProviderAndFileReader()
        {
            var services = Capture(b => b.UseLocalFileToggleProvider());
            Assert.True(HasImplementation<IToggleProvider, LocalFileToggleProvider>(services));
            Assert.True(HasImplementation<IFileReader, FileReader>(services));
        }

        [Fact]
        public void UseSystemTime_RegistersDateTimeProvider()
        {
            var services = Capture(b => b.UseSystemTime());
            Assert.True(Has<IDateTimeProvider>(services));
        }

        [Fact]
        public void UseStatusManager_RegistersStatusServices()
        {
            var services = Capture(b => b.UseStatusManager());
            Assert.True(HasImplementation<IStatusManager, StatusManager>(services));
            Assert.True(Has<IStatusStrategy>(services));
        }

        [Fact]
        public void UseHealthChecker_RegistersHealthServices()
        {
            var services = Capture(b => b.UseHealthChecker());
            Assert.True(HasImplementation<IHealthCheckManager, HealthCheckManager>(services));
            Assert.True(Has<IHealthCheckStrategy>(services));
            Assert.True(Has<IHealthCheckLiveAction>(services));
            Assert.True(Has<IHealthCheckReadyAction>(services));
        }

        [Fact]
        public void UseConsoleLogging_RegistersConsoleSink()
        {
            var services = Capture(b => b.UseConsoleLogging());
            Assert.True(Has<ITelemetrySink>(services));
            Assert.True(HasImplementation<IObservabilityScope, ObservabilityScope>(services));
        }

        [Fact]
        public void UseAppInsightsTelemetryTracking_RegistersAppInsightsSink()
        {
            var services = Capture(b => b.UseAppInsightsTelemetryTracking());
            Assert.True(HasImplementation<ITelemetryClientFactory, TelemetryClientFactory>(services));
            Assert.True(Has<ITelemetrySink>(services));
        }
    }
}
