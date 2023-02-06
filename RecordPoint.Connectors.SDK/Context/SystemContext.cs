using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.IO;

namespace RecordPoint.Connectors.SDK.Context
{

    /// <summary>
    /// Context that extends the standard context with additional features for OnCloud connectors
    /// </summary>
    public class SystemContext : ISystemContext
    {

        private readonly IHostEnvironment _hostEnvironment;
        private readonly IOptions<SystemOptions> _systemOptions;

        public SystemContext(IHostEnvironment hostEnvironment, IOptions<SystemOptions> systemOptions)
        {
            _systemOptions = systemOptions;
            _hostEnvironment = hostEnvironment;
        }

        public string GetCompanyName()
        {
            return _systemOptions.Value.CompanyName;
        }

        public string GetConnectorName()
        {
            return _systemOptions.Value.ConnectorName;
        }

        public string GetShortName()
        {
            return _systemOptions.Value.ShortName;
        }

        public string GetContentRootPath()
        {
            return _hostEnvironment.ContentRootPath;
        }

        /// <summary>
        /// Get the data root path that will be used if one is not suppplied in the options
        /// </summary>
        /// <returns></returns>
        public string GetDefaultDataRootPath()
        {
            return Path.Combine(GetContentRootPath(), "Data");
        }

        public string GetDataRootPath()
        {
            return !string.IsNullOrEmpty(_systemOptions.Value.DataPathRoot) ? _systemOptions.Value.DataPathRoot : GetDefaultDataRootPath();
        }
    }

}
