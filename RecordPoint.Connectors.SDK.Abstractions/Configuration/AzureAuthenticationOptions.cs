namespace RecordPoint.Connectors.SDK.Configuration
{
    /// <summary>
    /// Azure Authentication Options
    /// </summary>
    public class AzureAuthenticationOptions
    {
        /// <summary>
        /// Section name for Azure Authentication Options
        /// </summary>
        public const string SECTION_NAME = "AzureAuthentication";

        /// <summary>
        /// Tenant id
        /// </summary>
        public string? TenantId { get; set; }

        /// <summary>
        /// Client id
        /// </summary>
        public string? ClientId { get; set; }

        /// <summary>
        /// Client Secret
        /// </summary>
        public string? ClientSecret { get; set; }

        /// <summary>
        /// Setting name to use VS Credentials when connecting to various Azure Resources
        /// </summary>
        public bool UseVsCredentials { get; set; } = false;
    }
}
