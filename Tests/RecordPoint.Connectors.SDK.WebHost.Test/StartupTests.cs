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
    public class StartupTests
    {
        private static Dictionary<string, string?> BaseConfig() => new Dictionary<string, string?>
        {
            ["CorsOrigins:0"] = "https://example.com",
            ["WebHost:SwaggerEndpointUrl"] = "v1/swagger.json",
            ["WebHost:SwaggerEndpointName"] = "Test API",
            ["WebHost:Authentication:Instance"] = "https://login.microsoftonline.com/",
            ["WebHost:Authentication:ClientId"] = "00000000-0000-0000-0000-000000000001",
            ["WebHost:Authentication:TenantId"] = "common",
            ["WebHost:Authentication:Audience"] = "api://test"
        };

        private static async Task<IHost> StartHostAsync(IDictionary<string, string?> config, CancellationToken cancellationToken)
        {
            var host = new HostBuilder()
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder.UseTestServer();
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

            await host.StartAsync(cancellationToken);
            return host;
        }

        [Fact]
        public async Task Startup_ConfiguresPipeline_RootEndpointReturns200()
        {
            var cancellationToken = TestContext.Current.CancellationToken;
            using var host = await StartHostAsync(BaseConfig(), cancellationToken);
            var client = host.GetTestClient();

            var response = await client.GetAsync("/", cancellationToken);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Startup_HealthEndpoint_IsReachable()
        {
            var cancellationToken = TestContext.Current.CancellationToken;
            using var host = await StartHostAsync(BaseConfig(), cancellationToken);
            var client = host.GetTestClient();

            var response = await client.GetAsync("/health", cancellationToken);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Startup_SwaggerJson_WithForwardedPrefix_RewritesServers()
        {
            var cancellationToken = TestContext.Current.CancellationToken;
            using var host = await StartHostAsync(BaseConfig(), cancellationToken);
            var client = host.GetTestClient();

            var request = new HttpRequestMessage(HttpMethod.Get, "/swagger/v1/swagger.json");
            request.Headers.Add("X-Forwarded-Prefix", "/proxy-prefix");
            var response = await client.SendAsync(request, cancellationToken);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var body = await response.Content.ReadAsStringAsync(cancellationToken);
            Assert.Contains("/proxy-prefix", body);
        }

        [Fact]
        public async Task Startup_SwaggerJson_WithoutForwardedPrefix_IsReachable()
        {
            var cancellationToken = TestContext.Current.CancellationToken;
            using var host = await StartHostAsync(BaseConfig(), cancellationToken);
            var client = host.GetTestClient();

            var response = await client.GetAsync("/swagger/v1/swagger.json", cancellationToken);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}

