using Microsoft.Extensions.Options;

namespace RecordPoint.Connectors.SDK.Configuration
{
    /// <summary>
    /// Appsettings R365 configuration client 
    /// </summary>
    public class AppSettingsR365ConfigurationClient : IR365ConfigurationClient
    {

        private readonly R365ConfigurationModel _r365ConfigurationModel;

        public AppSettingsR365ConfigurationClient(IOptions<R365ConfigurationModel> r365ConfigurationModel)
        {
            _r365ConfigurationModel = r365ConfigurationModel.Value;
        }

        /// <summary>
        /// Does a R365 configuration exists?
        /// </summary>
        public bool R365ConfigurationExists()
        {
            //Check validation
            return _r365ConfigurationModel != null;
        }

        /// <summary>
        /// Get the Records 365 configuration
        /// </summary>
        /// <returns>Default records 365 configuration, null if it doesn't exist</returns>
        public R365ConfigurationModel GetR365Configuration()
        {
            return _r365ConfigurationModel;
        }
    }
}
