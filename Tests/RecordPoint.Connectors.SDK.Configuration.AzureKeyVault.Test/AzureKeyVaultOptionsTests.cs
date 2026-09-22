#nullable enable
using RecordPoint.Connectors.SDK.Configuration.AzureKeyVault;
using Xunit;

namespace RecordPoint.Connectors.SDK.Configuration.AzureKeyVault.Test
{
    public class AzureKeyVaultOptionsTests
    {
        [Fact]
        public void SectionName_HasExpectedValue()
        {
            Assert.Equal("AzureKeyVault", AzureKeyVaultOptions.SECTION_NAME);
        }

        [Fact]
        public void ReloadInterval_DefaultsTo300()
        {
            var options = new AzureKeyVaultOptions();

            Assert.Equal(300, options.ReloadInterval);
        }

        [Fact]
        public void KeyVaultName_DefaultsToNull()
        {
            var options = new AzureKeyVaultOptions();

            Assert.Null(options.KeyVaultName);
        }

        [Fact]
        public void KeyVaultName_CanBeSetAndRetrieved()
        {
            var options = new AzureKeyVaultOptions
            {
                KeyVaultName = "my-vault"
            };

            Assert.Equal("my-vault", options.KeyVaultName);
        }

        [Fact]
        public void ReloadInterval_CanBeSetAndRetrieved()
        {
            var options = new AzureKeyVaultOptions
            {
                ReloadInterval = 42
            };

            Assert.Equal(42, options.ReloadInterval);
        }
    }
}
