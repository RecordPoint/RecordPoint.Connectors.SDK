using Azure.Core;
using Azure.Identity;

namespace RecordPoint.Connectors.SDK.Configuration
{
    /// <summary>
    /// Extenions for the AzureAuthenticationOptions
    /// </summary>
    public static class AzureAuthenticationOptionsExtensions
    {
        private static TokenCredential? _cachedCredential = null;

        /// <summary>
        /// Returns a TokenCredential for a given configuration
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static TokenCredential GetTokenCredential(this AzureAuthenticationOptions options)
        {
            if (_cachedCredential != null) return _cachedCredential;

            if (options != null && options.UseVsCredentials)
            {
                _cachedCredential = new VisualStudioCredential();
            }
            else if (!string.IsNullOrEmpty(options?.TenantId) && !string.IsNullOrEmpty(options?.ClientId) && !string.IsNullOrEmpty(options?.ClientSecret))
            {
                _cachedCredential = new ClientSecretCredential(options.TenantId, options.ClientId, options.ClientSecret);
            }
            else
            {
                _cachedCredential = new DefaultAzureCredential();
            }

            return _cachedCredential;
        }
    }
}
