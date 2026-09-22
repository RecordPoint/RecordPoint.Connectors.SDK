#nullable enable
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace RecordPoint.Connectors.SDK.WebHost.Test
{
    public class WebHostBuilderExtensionsTests
    {
        [Fact]
        public void UseWebHost_WithConfiguredUrls_ReturnsHostBuilder()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["WebHost:Urls:0"] = "https://localhost:12345"
                })
                .Build();

            var hostBuilder = Host.CreateDefaultBuilder();

            var result = hostBuilder.UseWebHost(configuration);

            Assert.Same(hostBuilder, result);
        }

        [Fact]
        public void UseWebHost_WithoutConfiguredUrls_UsesDefaultsAndReturnsHostBuilder()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>())
                .Build();

            var hostBuilder = Host.CreateDefaultBuilder();

            var result = hostBuilder.UseWebHost(configuration);

            Assert.Same(hostBuilder, result);
        }
    }
}
