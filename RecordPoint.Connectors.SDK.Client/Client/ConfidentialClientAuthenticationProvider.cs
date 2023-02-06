using Microsoft.Identity.Client;
using RecordPoint.Connectors.SDK.Helpers;
using System.Runtime.InteropServices;
using System.Security;

namespace RecordPoint.Connectors.SDK.Client
{
    /// <summary>
    /// Helper for acquiring access tokens from Azure AD.
    /// </summary>
    public class ConfidentialClientAuthenticationProvider : IAuthenticationProvider
    {
        private const string AuthEndpointPrefix = "https://login.microsoftonline.com/";

        private static IConfidentialClientApplication clientApp;

        public void Initialize(AuthenticationHelperSettings settings)
        {
            if (clientApp == null)
            {
                ValidationHelper.ArgumentNotNull(settings, nameof(settings));
                ValidationHelper.ArgumentNotNullOrWhiteSpace(settings.AuthenticationResource, nameof(settings.AuthenticationResource));
                ValidationHelper.ArgumentNotNullOrWhiteSpace(settings.ClientId, nameof(settings.ClientId));


                // sign in & get an authentication token...
                clientApp = ConfidentialClientApplicationBuilder.Create(settings.ClientId)
                        .WithClientSecret(SecureStringToString(settings.ClientSecret))
                        .WithCacheOptions(CacheOptions.EnableSharedCacheOptions)
                        .Build();
            }
        }

        /// <summary>
        /// Acquires an OAuth 2.0 access token from Azure AD for use with the Records365 Connector API.
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult> AcquireTokenAsync(AuthenticationHelperSettings settings)
        {
            if (clientApp == null)
            {
                throw new InvalidOperationException("Client Application has not been initialized");
            }
            var authority = GetAuthority(settings);

            var scopes = new[] { settings.AuthenticationResource + "/.default" };
            var aadAuthenticationResult = await clientApp.AcquireTokenForClient(scopes).WithAuthority(authority).ExecuteAsync().ConfigureAwait(false);

            return new AuthenticationResult
            {
                AccessTokenType = aadAuthenticationResult.TokenType,
                AccessToken = aadAuthenticationResult.AccessToken
            };
        }


        private static string SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        private static string GetAuthority(AuthenticationHelperSettings settings)
        {
            ValidationHelper.ArgumentNotNullOrWhiteSpace(settings.TenantDomainName, nameof(settings.TenantDomainName));
            return AuthEndpointPrefix + settings.TenantDomainName;
        }
    }
}
