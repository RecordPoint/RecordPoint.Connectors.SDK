namespace RecordPoint.Connectors.SDK.Configuration
{
    /// <summary>
    /// Defines the Records 365 configuration client that provides access to the Records 365 configuration
    /// </summary>
    public interface IR365ConfigurationClient
    {

        /// <summary>
        /// Does a R365 configuration exists?
        /// </summary>
        bool R365ConfigurationExists();

        /// <summary>
        /// Get the Records 365 configuration
        /// </summary>
        /// <returns>Default records 365 configuration, null if it doesn't exist</returns>
        R365ConfigurationModel GetR365Configuration(string key = "");
    }
}
