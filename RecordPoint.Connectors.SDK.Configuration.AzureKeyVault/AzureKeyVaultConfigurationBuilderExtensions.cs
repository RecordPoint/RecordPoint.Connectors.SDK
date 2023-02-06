using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;

namespace RecordPoint.Connectors.SDK.Configuration.AzureKeyVault
{
    /// <summary>
    /// Azure key vault builder extensions
    /// </summary>
    public static class AzureKeyVaultConfigurationBuilderExtensions
    {
        /// <summary>
        /// Use Azure Key Vault
        /// The KeyVaultName setting must be populated with the name of the keyvault.
        /// When no other settings are provided, the connection will be attempted using the DefaultAzureCredential.
        /// If the ClientId and ClientSecret settings are populated, the connection will be attempted using the ClientSecretCredential.
        /// If the UseVSCredentials is True, the connection will be attempted using the VisualStudioCredential.
        /// </summary>
        /// <param name="configuration">Configuration builder to target</param>
        public static IConfigurationBuilder UseAzureKeyVaultConfigurationProvider(this IConfigurationBuilder configuration)
        {
            var builtConfig = configuration.Build();
            var keyVaultOptions = builtConfig.GetSection(AzureKeyVaultOptions.SECTION_NAME).Get<AzureKeyVaultOptions>();
            if (keyVaultOptions == null)
                return configuration;

            var uri = new Uri($"https://{keyVaultOptions.KeyVaultName}.vault.azure.net");

            var azureAuthenticationOptions = builtConfig.GetSection(AzureAuthenticationOptions.SECTION_NAME).Get<AzureAuthenticationOptions>();
            var credential = azureAuthenticationOptions.GetTokenCredential();

            var secretClient = new SecretClient(uri, credential);
            configuration.AddAzureKeyVault(secretClient, new AzureKeyVaultConfigurationOptions
            {
                ReloadInterval = keyVaultOptions.ReloadInterval == 0 
                    ? null 
                    : TimeSpan.FromSeconds(keyVaultOptions.ReloadInterval)
            });

            return configuration;
        }
    }
}
