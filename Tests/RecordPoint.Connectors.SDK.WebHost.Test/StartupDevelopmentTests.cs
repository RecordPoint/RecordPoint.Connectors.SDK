#nullable enable
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Health;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;
using System.Net;
using Xunit;

namespace RecordPoint.Connectors.SDK.WebHost.Test
{
    /// <summary>
    /// Covers the development-environment branch of <see cref="Startup.Configure"/> that registers
    /// the developer exception page. The primary Startup tests run in the default environment, so
    /// this test explicitly exercises the <c>env.IsDevelopment()</c> path.
    /// </summary>
    public class StartupDevelopmentTests
    {
        [Fact]
        public async Task Startup_InDevelopmentEnvironment_ConfiguresPipeline()
        {
            var config = new Dictionary<string, string?>
            {
                ["CorsOrigins:0"] = "https://example.com",
                ["WebHost:SwaggerEndpointUrl"] = "v1/swagger.json",
                ["WebHost:SwaggerEndpointName"] = "Test API"
            };

            using var host = new HostBuilder()
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder.UseTestServer();
                    webBuilder.UseEnvironment(Environments.Development);
                    webBuilder.ConfigureAppConfiguration((_, cfg) => cfg.AddInMemoryCollection(config));
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureTestServices(services =>
                    {
                        services.AddSingleton(Mock.Of<ISystemContext>());
                        services.AddSingleton(Mock.Of<IConnectorDatabaseClient>());
                        services.AddSingleton(Mock.Of<IObservabilityScope>());
                        services.AddSingleton(Mock.Of<IDateTimeProvider>());
                        services.AddSingleton(Mock.Of<IWorkQueueClient>());
                        var healthManager = new Mock<IHealthCheckManager>();
                        healthManager.SetupGet(m => m.HealthCheckResult).Returns(new HealthCheckResult());
                        services.AddSingleton(healthManager.Object);
                    });
                })
                .Build();

            var cancellationToken = TestContext.Current.CancellationToken;

            await host.StartAsync(cancellationToken);
            var client = host.GetTestClient();

            var response = await client.GetAsync("/", cancellationToken);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}

