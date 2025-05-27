namespace RecordPoint.Connectors.SDK.WebHost
{
    /// <summary>
    /// The web host authentication options.
    /// </summary>
    public class WebHostAuthenticationOptions
    {
        /// <summary>
        /// The SECTION NAME.
        /// </summary>
        public const string SECTION_NAME = "WebHost:Authentication";

        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        public string Instance { get; set; } = "https://login.microsoftonline.com/";
        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        public string ClientId { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the audience.
        /// </summary>
        public string Audience { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the tenant id.
        /// </summary>
        public string TenantId { get; set; } = "common";
        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        public string Domain { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the token validation parameters.
        /// </summary>
        public TokenValidationParameters TokenValidationParameters { get; set; } = new();
    }

    /// <summary>
    /// The token validation parameters.
    /// </summary>
    public class TokenValidationParameters
    {
        /// <summary>
        /// Gets or sets the valid audiences.
        /// </summary>
        public IEnumerable<string> ValidAudiences { get; set; } = [];
    }
}
