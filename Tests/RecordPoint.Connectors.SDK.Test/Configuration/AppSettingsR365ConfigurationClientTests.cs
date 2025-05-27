using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Configuration;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Configuration
{
    /// <summary>
    /// SUT for R365 Configuration tests
    /// </summary>
    public class AppSettingsR365ConfigurationClientSut : CommonSutBase
    {
        public static bool _multiConfig = true;

        protected override IHostBuilder CreateSutBuilder()
        {
            var myConfig = new Dictionary<string, string>
            {
                { "appsettings:Audience", "AudienceValue" },
                { "appsettings:ClientId", "ClientIdValue" },
                { "appsettings:ClientSecret", "ClientSecretValue" },
                { "appsettings:ConnectorApiUrl", "ConnectorApiUrlValue" }
            };

            var myMultiConfig = new Dictionary<string, string>
            {
                { "multiConfig:Default:Audience", "AudienceValue1" },
                { "multiConfig:Default:ClientId", "ClientIdValue1" },
                { "multiConfig:Default:ClientSecret", "ClientSecretValue1" },
                { "multiConfig:Default:ConnectorApiUrl", "ConnectorApiUrlValue1" },
                { "multiConfig:Test:Audience", "AudienceValue2" },
                { "multiConfig:Test:ClientId", "ClientIdValue2" },
                { "multiConfig:Test:ClientSecret", "ClientSecretValue2" },
                { "multiConfig:Test:ConnectorApiUrl", "ConnectorApiUrlValue2" }
            };

            var configBuilder = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfig);
            if (_multiConfig)
            {
                configBuilder.AddInMemoryCollection(myMultiConfig);
            }
            var config = configBuilder.Build();

            return base
                .CreateSutBuilder()
                .ConfigureServices(svcs =>
                {
                    svcs.Configure<R365ConfigurationModel>(config.GetSection("appsettings"));
                    svcs.Configure<R365MultiConfigurationModel>(config.GetSection("multiConfig"));
                    svcs.AddSingleton<IR365ConfigurationClient, AppSettingsR365ConfigurationClient>();
                });
        }
    }



    /// <summary>
    /// R365 Configuration Tests
    /// </summary>
    public class AppSettingsR365ConfigurationClientTests : CommonTestBase<AppSettingsR365ConfigurationClientSut>
    {

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task HappyPath_Should_GetCorrectConfig(bool multiConfig)
        {
            AppSettingsR365ConfigurationClientSut._multiConfig = multiConfig;
            await StartSutAsync();
            var configurationClient = Services.GetRequiredService<IR365ConfigurationClient>();
            Assert.True(configurationClient.R365ConfigurationExists());

            // Default key should return the same config as not supplying a key
            var singleConfig = configurationClient.GetR365Configuration();
            var multiConfig1 = configurationClient.GetR365Configuration("Default");
            var multiConfig2 = configurationClient.GetR365Configuration("Test");

            if(multiConfig)
            {
                // If multi config is available, the config with default key will be used
                Assert.Equal("AudienceValue1", singleConfig.Audience);
                Assert.Equal("ClientIdValue1", singleConfig.ClientId);
                Assert.Equal("ClientSecretValue1", singleConfig.ClientSecret);
                Assert.Equal("ConnectorApiUrlValue1", singleConfig.ConnectorApiUrl);
            }
            else
            {
                // Otherwise the old style single config will be used
                Assert.Equal("AudienceValue", singleConfig.Audience);
                Assert.Equal("ClientIdValue", singleConfig.ClientId);
                Assert.Equal("ClientSecretValue", singleConfig.ClientSecret);
                Assert.Equal("ConnectorApiUrlValue", singleConfig.ConnectorApiUrl);
            }

            // if multi config isn't configured, all configs will be the same.
            Assert.Equal(multiConfig ? "AudienceValue1" : "AudienceValue", multiConfig1.Audience);
            Assert.Equal(multiConfig ? "ClientIdValue1" : "ClientIdValue", multiConfig1.ClientId);
            Assert.Equal(multiConfig ? "ClientSecretValue1" : "ClientSecretValue", multiConfig1.ClientSecret);
            Assert.Equal(multiConfig ? "ConnectorApiUrlValue1" : "ConnectorApiUrlValue", multiConfig1.ConnectorApiUrl);

            Assert.Equal(multiConfig ? "AudienceValue2" : "AudienceValue", multiConfig2.Audience);
            Assert.Equal(multiConfig ? "ClientIdValue2" : "ClientIdValue", multiConfig2.ClientId);
            Assert.Equal(multiConfig ? "ClientSecretValue2" : "ClientSecretValue", multiConfig2.ClientSecret);
            Assert.Equal(multiConfig ? "ConnectorApiUrlValue2" : "ConnectorApiUrlValue", multiConfig2.ConnectorApiUrl);
        }
    }
}