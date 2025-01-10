namespace RecordPoint.Connectors.SDK.WebHost
{
    public class WebHostAuthenticationOptions
    {
        public const string SECTION_NAME = "WebHost:Authentication";

        /// <summary>
        /// Config for an array of WebHostAuthenticationOptions
        /// </summary>
        public const string MULTI_CONFIG_SECTION_NAME = "WebHost:MultiAuthentication";

        public string Instance { get; set; } = "https://login.microsoftonline.com/";
        public string ClientId { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string TenantId { get; set; } = "common";
        public string Domain { get; set; } = string.Empty;
    }
}
