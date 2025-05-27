namespace RecordPoint.Connectors.SDK.Configuration
{
    /// <summary>
    /// A collection of configurations for submitting to R365.
    /// The key should be the ConnectorTypeConfigurationId or "Default".
    /// Use "Default" when the ConnectorTypeConfigurationId is null or empty for your Connector Config.
    /// This is optional and can be used to support multiple configurations.
    /// </summary>
    public class R365MultiConfigurationModel : Dictionary<string, R365ConfigurationModel>
    {
        /// <summary>
        /// The intended default configuration key
        /// </summary>
        public const string DefaultConfigurationKey = "Default";
    }
}