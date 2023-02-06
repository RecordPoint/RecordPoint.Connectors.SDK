namespace RecordPoint.Connectors.SDK.Client
{
    /// <summary>
    /// Settings used when creating a new ApiClient used for calling the Records365 vNext Connector API.
    /// </summary>
    public class ApiClientFactorySettings
    {
        /// <summary>
        /// The base URL of the Records36 vNext Connector API to call.
        /// </summary>
        public string ConnectorApiUrl { get; set; } = string.Empty;

        /// <summary>
        /// Set to false to skip validation of the server SSL certificate at the
        /// Records365 vNext Connector API endpoint, true otherwise.
        /// </summary>
        public bool ServerCertificateValidation { get; set; } = true;
    }
}
