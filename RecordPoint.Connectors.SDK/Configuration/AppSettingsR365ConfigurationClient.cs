using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace RecordPoint.Connectors.SDK.Configuration
{
    /// <summary>
    /// Appsettings R365 configuration client 
    /// </summary>
    public class AppSettingsR365ConfigurationClient : IR365ConfigurationClient
    {

        private readonly R365ConfigurationModel _r365ConfigurationModel;
        private readonly R365MultiConfigurationModel _r365MultiConfigurationModel;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="r365ConfigurationModel"></param>
        /// <param name="r365MultiConfigurationModel"></param>
        public AppSettingsR365ConfigurationClient(IOptions<R365ConfigurationModel> r365ConfigurationModel,
            IOptions<R365MultiConfigurationModel> r365MultiConfigurationModel)
        {
            _r365ConfigurationModel = r365ConfigurationModel.Value;
            _r365MultiConfigurationModel = r365MultiConfigurationModel.Value;
        }

        /// <summary>
        /// Does a R365 configuration exist?
        /// </summary>
        public bool R365ConfigurationExists()
        {
            if (_r365ConfigurationModel is not null)
            {
                return true;
            }

            return _r365MultiConfigurationModel is { Count: > 0 };
        }
        
        /// <summary>
        /// Get the Records 365 configuration using options key for multi configuration lookup
        /// </summary>
        /// <returns>Default records 365 configuration, null if it doesn't exist</returns>
        public R365ConfigurationModel GetR365Configuration(string key = "")
        {
            if (_r365MultiConfigurationModel == null || _r365MultiConfigurationModel.Count == 0)
            {
                return _r365ConfigurationModel;
            }

            if (string.IsNullOrEmpty(key))
            {
                key = R365MultiConfigurationModel.DefaultConfigurationKey;
            }

            return _r365MultiConfigurationModel.GetValueOrDefault(key, _r365ConfigurationModel);
        }
    }
}
