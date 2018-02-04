using System;
using System.Security;

namespace RecordPoint.Connectors.SDK.Client
{
    /// <summary>
    /// Settings used when creating a new ApiClient used for calling the Records365 vNext Connector API.
    /// </summary>
    public class ApiClientFactorySettings
    {
        public string AuthenticationResource { get; set; }
        public string TenantDomainName { get; set; }
        public string ClientId { get; set; }
        public SecureString ClientSecret { get; set; }
        public string ConnectorApiUrl { get; set; }
        public bool ServerCertificateValidation { get; set; } = false;
    }
}
