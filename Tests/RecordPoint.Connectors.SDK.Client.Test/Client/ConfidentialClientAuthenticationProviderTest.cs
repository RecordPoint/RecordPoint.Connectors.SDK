using RecordPoint.Connectors.SDK.Client;
using System.Security;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Client
{
    public class ConfidentialClientAuthenticationProviderTest
    {
        private static SecureString MakeSecureString(string value)
        {
            var secure = new SecureString();
            foreach (var ch in value)
            {
                secure.AppendChar(ch);
            }
            return secure;
        }

        private static AuthenticationHelperSettings ValidSettings() => new()
        {
            AuthenticationResource = "https://resource",
            ClientId = Guid.NewGuid().ToString(),
            ClientSecret = MakeSecureString("secret"),
            TenantDomainName = "contoso.onmicrosoft.com"
        };

        [Fact]
        public void Constructor_Throws_WhenSettingsNull()
        {
            var act = () => new ConfidentialClientAuthenticationProvider(null);

            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void Constructor_Throws_WhenAuthenticationResourceMissing()
        {
            var settings = ValidSettings();
            settings.AuthenticationResource = "";

            var act = () => new ConfidentialClientAuthenticationProvider(settings);

            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void Constructor_Throws_WhenClientIdMissing()
        {
            var settings = ValidSettings();
            settings.ClientId = "";

            var act = () => new ConfidentialClientAuthenticationProvider(settings);

            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void Constructor_Succeeds_WithValidSettings()
        {
            var provider = new ConfidentialClientAuthenticationProvider(ValidSettings());

            Assert.NotNull(provider);
        }

        [Fact]
        public async Task AcquireTokenAsync_Throws_WhenTenantDomainNameMissing()
        {
            var provider = new ConfidentialClientAuthenticationProvider(ValidSettings());

            var settings = ValidSettings();
            settings.TenantDomainName = "";

            var act = async () => await provider.AcquireTokenAsync(settings);

            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }
    }
}
