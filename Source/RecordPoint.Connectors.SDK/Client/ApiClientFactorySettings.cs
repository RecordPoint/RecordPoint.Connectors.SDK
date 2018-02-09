namespace RecordPoint.Connectors.SDK.Client
{
    /// <summary>
    /// Settings used when creating a new ApiClient used for calling the Records365 vNext Connector API.
    /// </summary>
    public class ApiClientFactorySettings
    {
        public string ConnectorApiUrl { get; set; }
        public bool ServerCertificateValidation { get; set; } = false;
    }
}
