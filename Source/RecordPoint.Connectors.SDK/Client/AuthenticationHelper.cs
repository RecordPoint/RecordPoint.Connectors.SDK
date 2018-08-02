using Microsoft.IdentityModel.Clients.ActiveDirectory;
using RecordPoint.Connectors.SDK.Helpers;
using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Client
{
    public class AuthenticationHelper : IAuthenticationHelper
    {
        private const string AuthEndpointPrefix = "https://login.microsoftonline.com/";

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

            var aadAuthenticationResult = await authenticationContext.AcquireTokenAsync(settings.AuthenticationResource, new ClientCredential(settings.ClientId,
#if !NETSTANDARD2_0
                new SecureClientSecret(settings.ClientSecret)
#else
                // SecureClientSecret isn't supported for netstandard2.0 yet
                // https://github.com/AzureAD/azure-activedirectory-library-for-dotnet/issues/1026
                SecureStringToString(settings.ClientSecret)
#endif
            )).ConfigureAwait(false);

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
