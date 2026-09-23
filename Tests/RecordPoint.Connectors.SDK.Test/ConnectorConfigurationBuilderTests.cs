#nullable enable
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test
{
    /// <summary>
    /// Coverage tests for the ConnectorConfigurationBuilder host builder helpers.
    /// </summary>
    [Collection("EnvironmentVariable")]
    public class ConnectorConfigurationBuilderTests
    {
        [Fact]
        public void UseAppSettings_AddsCommandLineConfiguration()
        {
            using var host = Host.CreateDefaultBuilder()
                .UseAppSettings(["--MyKey=MyValue"], typeof(ConnectorConfigurationBuilderTests).Assembly)
                .Build();

            var config = host.Services.GetRequiredService<IConfiguration>();
            Assert.Equal("MyValue", config["MyKey"]);
        }

        [Fact]
        public void UseAppSettings_DevelopmentEnvironment_AddsUserSecrets()
        {
            var previous = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
            try
            {
                Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Development");
                using var host = Host.CreateDefaultBuilder()
                    .UseAppSettings(["--Foo=Bar"], typeof(ConnectorConfigurationBuilderTests).Assembly)
                    .Build();
                var config = host.Services.GetRequiredService<IConfiguration>();
                Assert.Equal("Bar", config["Foo"]);
            }
            finally
            {
                Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", previous);
            }
        }

        [Fact]
        public void CreateConfigurationBuilder_BuildsConfiguration()
        {
            var builder = ConnectorConfigurationBuilder.CreateConfigurationBuilder(
                ["--Alpha=One"],
                typeof(ConnectorConfigurationBuilderTests).Assembly);
            var config = builder.Build();
            Assert.Equal("One", config["Alpha"]);
        }

        [Fact]
        public void CreateConfigurationBuilder_DevelopmentEnvironment_AddsUserSecrets()
        {
            var previous = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            try
            {
                Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
                var builder = ConnectorConfigurationBuilder.CreateConfigurationBuilder(
                    ["--Beta=Two"],
                    typeof(ConnectorConfigurationBuilderTests).Assembly);
                var config = builder.Build();
                Assert.Equal("Two", config["Beta"]);
            }
            finally
            {
                Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", previous);
            }
        }

        [Fact]
        public void UseConfiguration_AddsSuppliedConfiguration()
        {
            var supplied = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?> { ["Injected"] = "Yes" })
                .Build();

            using var host = Host.CreateDefaultBuilder()
                .UseConfiguration(supplied)
                .Build();

            var config = host.Services.GetRequiredService<IConfiguration>();
            Assert.Equal("Yes", config["Injected"]);
        }
    }
}
