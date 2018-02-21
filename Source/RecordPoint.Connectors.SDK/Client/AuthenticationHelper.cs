using Microsoft.IdentityModel.Clients.ActiveDirectory;
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

            var aadAuthenticationResult = await authenticationContext.AcquireTokenAsync(settings.AuthenticationResource, new ClientCredential(settings.ClientId, new SecureClientSecret(settings.ClientSecret))).ConfigureAwait(false);

            return new AuthenticationResult
            {
                AccessTokenType = aadAuthenticationResult.AccessTokenType,
                AccessToken = aadAuthenticationResult.AccessToken
            };
        }

        private string GetAuthority(AuthenticationHelperSettings settings)
        {
            ValidationHelper.ArgumentNotNullOrWhiteSpace(settings.TenantDomainName, nameof(settings.TenantDomainName));
            return AuthEndpointPrefix + settings.TenantDomainName;
        }
    }
}
