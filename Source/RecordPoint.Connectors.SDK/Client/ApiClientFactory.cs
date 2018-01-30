﻿using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using System;
using System.Net;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Client
{
    public class ApiClientFactory : IApiClientFactory
    {
        private const string AuthEndpointPrefix = "https://login.microsoftonline.com/";
        private const string ConnectorApiPrefix = "/connector";

        static ApiClientFactory()
        {
            // default security is SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls, but our web service only accepts Tls11 or Tls12
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;            
        }

        public async Task<AuthenticationResult> AcquireTokenAsync(ApiClientFactorySettings settings, bool useTokenCache = true)
        {
            ValidationHelper.ArgumentNotNull(settings, nameof(settings));
            ValidationHelper.ArgumentNotNullOrWhiteSpace(settings.AuthenticationResource, nameof(settings.AuthenticationResource));
            ValidationHelper.ArgumentNotNullOrWhiteSpace(settings.ClientId, nameof(settings.ClientId));
            ValidationHelper.ArgumentNotNullOrEmpty(settings.ClientSecret, nameof(settings.ClientSecret));            
            
            var authority = GetAuthority(settings);

            // sign in & get an authentication token...
            var authenticationContext = useTokenCache ?
                // by default an internal token cache is used
                new AuthenticationContext(authority) :
                // pass a null token cache so that the token must be retrieved from the authority
                new AuthenticationContext(authority, null);
                        
            return await authenticationContext.AcquireTokenAsync(settings.AuthenticationResource, new ClientCredential(settings.ClientId, new SecureClientSecret(settings.ClientSecret))).ConfigureAwait(false);
        }

        private async Task<IApiClient> CreateApiClientAsync(ApiClientFactorySettings settings, bool useTokenCache = true)
        {
            ValidationHelper.ArgumentNotNull(settings, nameof(settings));
            ValidationHelper.ArgumentNotNullOrWhiteSpace(settings.ConnectorApiUrl, nameof(settings.ConnectorApiUrl));

            if (settings.ServerCertificateValidation == false)
            {
                ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            }

            var authResult = await AcquireTokenAsync(settings, useTokenCache).ConfigureAwait(false);

            // Support both the root site and /connector in the configuration
            var connectorApiUrl = new Uri(settings.ConnectorApiUrl);
            if(!settings.ConnectorApiUrl.Contains(ConnectorApiPrefix))
            {
                connectorApiUrl = new Uri(connectorApiUrl, ConnectorApiPrefix);
            }

            // get the api client generated by AutoRest from the swagger...
            var apiClient = new ApiClient(connectorApiUrl, new TokenCredentials(authResult.AccessToken, authResult.AccessTokenType));
            apiClient.Authorization = $"{authResult.AccessTokenType} {authResult.AccessToken}";
            
            return apiClient;
        }

        private string GetAuthority(ApiClientFactorySettings settings)
        {
            ValidationHelper.ArgumentNotNullOrWhiteSpace(settings.TenantDomainName, nameof(settings.TenantDomainName));
            return AuthEndpointPrefix + settings.TenantDomainName;
        }

        public async Task<IApiClient> CreateApiClientAsync(ApiClientFactorySettings settings)
        {
            return await CreateApiClientAsync(settings, true).ConfigureAwait(false);
        }
    }
}
