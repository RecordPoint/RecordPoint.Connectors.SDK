#nullable enable
using Microsoft.IdentityModel.Tokens;
using Xunit;

namespace RecordPoint.Connectors.SDK.WebHost.Test
{
    public class OptionsTests
    {
        [Fact]
        public void WebHostOptions_HasExpectedDefaults()
        {
            var options = new WebHostOptions();

            Assert.Equal("WebHost", WebHostOptions.SECTION_NAME);
            Assert.Equal("v1/swagger.json", options.SwaggerEndpointUrl);
            Assert.Equal("RecordPoint Connectors SDK WebHost API", options.SwaggerEndpointName);
        }

        [Fact]
        public void WebHostOptions_PropertiesAreSettable()
        {
            var options = new WebHostOptions
            {
                SwaggerEndpointUrl = "v2/swagger.json",
                SwaggerEndpointName = "Custom"
            };

            Assert.Equal("v2/swagger.json", options.SwaggerEndpointUrl);
            Assert.Equal("Custom", options.SwaggerEndpointName);
        }

        [Fact]
        public void WebHostAuthenticationOptions_HasExpectedDefaults()
        {
            var options = new WebHostAuthenticationOptions();

            Assert.Equal("WebHost:Authentication", WebHostAuthenticationOptions.SECTION_NAME);
            Assert.Equal("https://login.microsoftonline.com/", options.Instance);
            Assert.Empty(options.ClientId);
            Assert.Empty(options.Audience);
            Assert.Equal("common", options.TenantId);
            Assert.Empty(options.Domain);
            Assert.NotNull(options.TokenValidationParameters);
            Assert.Empty(options.TokenValidationParameters.ValidAudiences ?? Array.Empty<string>());
        }

        static readonly string[] _audiences = new[] { "aud1", "aud2" };

        [Fact]
        public void WebHostAuthenticationOptions_PropertiesAreSettable()
        {
            
            var options = new WebHostAuthenticationOptions
            {
                Instance = "https://example.com/",
                ClientId = "client",
                Audience = "audience",
                TenantId = "tenant",
                Domain = "domain",
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudiences = _audiences
                }
            };

            Assert.Equal("https://example.com/", options.Instance);
            Assert.Equal("client", options.ClientId);
            Assert.Equal("audience", options.Audience);
            Assert.Equal("tenant", options.TenantId);
            Assert.Equal("domain", options.Domain);
            Assert.Equal(_audiences, options.TokenValidationParameters.ValidAudiences);
        }
    }
}
