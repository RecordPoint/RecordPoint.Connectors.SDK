#nullable enable
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using RecordPoint.Connectors.SDK.Configuration;
using RecordPoint.Connectors.SDK.Diagnostics;
using RecordPoint.Connectors.SDK.Notifications;
using RecordPoint.Connectors.SDK.Observability.Null;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.R365;
using RecordPoint.Connectors.SDK.Time;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.R365
{
    /// <summary>
    /// Tests for the R365BuilderExtensions
    /// </summary>
    [Collection("EnvironmentVariable")]
    public class R365BuilderExtensionsTests
    {
        private static IHost BuildHost(bool synchronous = false)
        {
            return Host.CreateDefaultBuilder()
                .UseSystemTime()
                .UseNullTelemetryTracking()
                .ConfigureServices(services => services
                    .AddSingleton(new Mock<IR365ConfigurationClient>().Object)
                    .AddR365Integration(synchronous))
                .Build();
        }

        [Fact]
        public void AddR365Integration_ResolvesClientAndPipelines()
        {
            using var host = BuildHost();

            Assert.NotNull(host.Services.GetRequiredService<IR365Client>());
            Assert.NotNull(host.Services.GetRequiredService<INotificationApiManager>());
            Assert.NotNull(host.Services.GetRequiredService<ILog>());
            var pipelines = host.Services.GetRequiredService<IR365Pipelines>();
            Assert.NotNull(pipelines.RecordPipeline);
            Assert.NotNull(pipelines.BinaryPipeline);
            Assert.NotNull(pipelines.AggregationPipeline);
            Assert.NotNull(pipelines.AuditEventPipeline);
        }

        [Fact]
        public void AddR365Integration_RegistersCircuitProviders()
        {
            using var host = BuildHost();

            Assert.NotNull(host.Services.GetRequiredService<ISdkAzureBlobCircuitProvider>());
            Assert.NotNull(host.Services.GetRequiredService<ISdkAzureBlobRetryProvider>());
            Assert.NotNull(host.Services.GetRequiredService<ISettableCircuitProvider>());
        }

        [Fact]
        public void AddR365Integration_Synchronous_ResolvesPipelines()
        {
            using var host = BuildHost(synchronous: true);
            var pipelines = host.Services.GetRequiredService<IR365Pipelines>();
            Assert.NotNull(pipelines.BinaryPipeline);
        }

        [Fact]
        public void UseR365Integration_ResolvesClient()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["RecordSubmission:SubmitRecordAndBinariesSynchronously"] = "true"
                })
                .Build();

            using var host = Host.CreateDefaultBuilder()
                .UseSystemTime()
                .UseNullTelemetryTracking()
                .ConfigureAppConfiguration(b => b.AddConfiguration(config))
                .ConfigureServices(services => services.AddSingleton(new Mock<IR365ConfigurationClient>().Object))
                .UseR365Integration()
                .Build();

            Assert.NotNull(host.Services.GetRequiredService<IR365Client>());
            Assert.NotNull(host.Services.GetRequiredService<IR365Pipelines>());
        }

        [Fact]
        public void CreateAuditEventPipeline_BuildsPipelineElement()
        {
            using var host = BuildHost();
            var element = host.Services.CreateAuditEventPipeline();
            Assert.NotNull(element);
        }
    }
}
