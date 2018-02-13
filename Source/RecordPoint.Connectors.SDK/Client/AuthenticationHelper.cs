using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

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

            return await authenticationContext.AcquireTokenAsync(settings.AuthenticationResource, new ClientCredential(settings.ClientId, new SecureClientSecret(settings.ClientSecret))).ConfigureAwait(false);
        }

        private string GetAuthority(AuthenticationHelperSettings settings)
        {
            ValidationHelper.ArgumentNotNullOrWhiteSpace(settings.TenantDomainName, nameof(settings.TenantDomainName));
            return AuthEndpointPrefix + settings.TenantDomainName;
        }
    }
}
