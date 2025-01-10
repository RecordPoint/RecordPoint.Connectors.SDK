namespace RecordPoint.Connectors.SDK.Configuration
{
    /// <summary>
    /// Records 365 data model
    /// </summary>
    public class R365ConfigurationModel
    {
        /// <summary>
        /// The base url of the Records365 Connector API.
        /// </summary>
        public string ConnectorApiUrl { get; set; } = string.Empty;

        /// <summary>
        /// Set to false to skip validation of the server SSL certificate at the
        /// Records365 vNext Connector API endpoint, true otherwise.
        /// </summary>
        public bool ServerCertificateValidation { get; set; } = true;

        /// <summary>
        /// Client Id
        /// </summary>
        public string ClientId { get; set; } = string.Empty;

        /// <summary>
        /// Client Secret
        /// </summary>
        public string ClientSecret { get; set; } = string.Empty;

        /// <summary>
        /// Audience
        /// </summary>
        public string Audience { get; set; } = string.Empty;
    }
}