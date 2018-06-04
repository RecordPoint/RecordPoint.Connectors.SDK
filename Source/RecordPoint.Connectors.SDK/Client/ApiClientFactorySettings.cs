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
        public string ConnectorApiUrl { get; set; }

        /// <summary>
        /// Set to true to perform validation of the server SSL certificate at the
        /// Records365 vNext Connector API endpoint, false otherwise.
        /// </summary>
        public bool ServerCertificateValidation { get; set; } = false;
    }
}
