using Microsoft.IdentityModel.Clients.ActiveDirectory;
using RecordPoint.Connectors.SDK.Helpers;
#if NETSTANDARD2_0
using System;
using System.Runtime.InteropServices;
using System.Security;
#endif
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Client
{
    /// <summary>
    /// Helper for acquiring access tokens from Azure AD.
    /// </summary>
    public class AuthenticationHelper : IAuthenticationHelper
    {
        private const string AuthEndpointPrefix = "https://login.microsoftonline.com/";

        /// <summary>
        /// Acquires an OAuth 2.0 access token from Azure AD for use with the Records365 Connector API.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="useTokenCache"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult> AcquireTokenAsync(AuthenticationHelperSettings settings, bool useTokenCache = true)
        {
            ValidationHelper.ArgumentNotNull(settings, nameof(settings));
            ValidationHelper.ArgumentNotNullOrWhiteSpace(settings.AuthenticationResource, nameof(settings.AuthenticationResource));
            ValidationHelper.ArgumentNotNullOrWhiteSpace(settings.ClientId, nameof(settings.ClientId));
            ValidationHelper.ArgumentNotNullOrEmpty(settings.ClientSecret, nameof(settings.ClientSecret));

            var authority = GetAuthority(settings);

            // sign in & get an authentication token...
            var authenticationContext = useTokenCache
                // by default an internal token cache is used
                ? new AuthenticationContext(authority)
                // pass a null token cache so that the token must be retrieved from the authority
                : new AuthenticationContext(authority, null);


#if NETSTANDARD2_0

            //NOTE: The following call may throw a PlatformNotSupported exception that occurs the first time a token is acquired.
            //However, the exception will not raise beyond this point, and the call will automatically retry and resolved a token on the second attempt.
            //This is due to the first (throwing) attempt actually acquiring a token, storing it in the AuthenticationContext token cache, and then throwing an exception regardless.
            //Later attempts simply use the cached token and thus avoid the issue. While undocumented, it is believed that this is the intended functionality.
            var aadAuthenticationResult = await authenticationContext.AcquireTokenAsync(settings.AuthenticationResource, new ClientCredential(settings.ClientId, SecureStringToString(settings.ClientSecret))).ConfigureAwait(false);
#else
            var aadAuthenticationResult = await authenticationContext.AcquireTokenAsync(settings.AuthenticationResource, new ClientCredential(settings.ClientId, new SecureClientSecret(settings.ClientSecret))).ConfigureAwait(false);
#endif
            return new AuthenticationResult
            {
                AccessTokenType = aadAuthenticationResult.AccessTokenType,
                AccessToken = aadAuthenticationResult.AccessToken
            };
        }

#if NETSTANDARD2_0
        private string SecureStringToString(SecureString value)
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
#endif 

        private string GetAuthority(AuthenticationHelperSettings settings)
        {
            ValidationHelper.ArgumentNotNullOrWhiteSpace(settings.TenantDomainName, nameof(settings.TenantDomainName));
            return AuthEndpointPrefix + settings.TenantDomainName;
        }
    }
}
