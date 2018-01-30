using System;
using System.Security;

namespace RecordPoint.Connectors.SDK.Client
{
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
