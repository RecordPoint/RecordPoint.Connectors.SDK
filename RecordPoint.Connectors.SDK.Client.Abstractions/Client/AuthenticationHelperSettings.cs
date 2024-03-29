﻿using System.Security;

namespace RecordPoint.Connectors.SDK.Client
{
    /// <summary>
    /// Provides the necessary information required to authenticate and obtain an access token for use
    /// with the Records365 Connector API.
    /// </summary>
    public class AuthenticationHelperSettings
    {
        /// <summary>
        /// Authentication resource.
        /// </summary>
        public string AuthenticationResource { get; set; } = string.Empty;

        /// <summary>
        /// Tenant domain name.
        /// </summary>
        public string TenantDomainName { get; set; } = string.Empty;

        /// <summary>
        /// Client Id.
        /// </summary>
        public string ClientId { get; set; } = string.Empty;

        /// <summary>
        /// Client secret.
        /// </summary>
        public SecureString ClientSecret { get; set; }
    }
}
