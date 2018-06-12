using System.Security;

namespace RecordPoint.Connectors.SDK.Client
{
    public class AuthenticationHelperSettings
    {
        public string AuthenticationResource { get; set; }
        public string TenantDomainName { get; set; }
        public string ClientId { get; set; }
        public SecureString ClientSecret { get; set; }
    }
}
