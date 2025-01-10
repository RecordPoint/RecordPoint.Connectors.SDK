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
        protected override IHostBuilder CreateSutBuilder()
        {
            var myConfig = new Dictionary<string, string>
            {
                { "appsettings:Audience", "AudienceValue" },
                { "appsettings:ClientId", "ClientIdValue" },
                { "appsettings:ClientSecret", "ClientSecretValue" },
                { "appsettings:ConnectorApiUrl", "ConnectorApiUrlValue" }
            };

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfig)
                .Build();

            return base
                .CreateSutBuilder()
                .ConfigureServices(svcs =>
                {
                    svcs.Configure<R365ConfigurationModel>(config.GetSection("appsettings"));
                    svcs.AddSingleton<IR365ConfigurationClient, AppSettingsR365ConfigurationClient>();
                });
        }
    }



    /// <summary>
    /// R365 Configuration Tests
    /// </summary>
    public class AppSettingsR365ConfigurationClientTests : CommonTestBase<AppSettingsR365ConfigurationClientSut>
    {

        [Fact]
        public async Task HappyPath_Should_GetCorrectConfig()
        {
            await StartSutAsync();
            var configurationClient = Services.GetRequiredService<IR365ConfigurationClient>();
            Assert.True(configurationClient.R365ConfigurationExists());

            var configuration = configurationClient.GetR365Configuration();
            Assert.NotNull(configuration);

            //these should be null and eventually removed completely for on cloud/multi-tenant systems
            Assert.Equal("AudienceValue", configuration.Audience);
            Assert.Equal("ClientIdValue", configuration.ClientId);
            Assert.Equal("ClientSecretValue", configuration.ClientSecret);
            Assert.Equal("ConnectorApiUrlValue", configuration.ConnectorApiUrl);
        }
    }
}