namespace RecordPoint.Connectors.SDK.WebHost
{
    public class WebHostAuthenticationOptions
    {
        public const string SECTION_NAME = "WebHost:Authentication";

        public string Instance { get; set; } = "https://login.microsoftonline.com/";
        public string ClientId { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string TenantId { get; set; } = "common";
        public string Domain { get; set; } = string.Empty;
    }
}
