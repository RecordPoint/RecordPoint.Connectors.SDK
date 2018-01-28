using System;

namespace RecordPoint.Connectors.Client
{
    public class ApiClientFactorySettings
    {
        public string AuthenticationResource { get; set; }
        public string TenantDomainName { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ConnectorApiUrl { get; set; }
        public bool ServerCertificateValidation { get; set; } = false;
    }
}
