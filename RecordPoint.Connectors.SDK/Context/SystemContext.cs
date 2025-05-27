using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.IO;

namespace RecordPoint.Connectors.SDK.Context
{

    /// <summary>
    /// The system context.
    /// </summary>
    public class SystemContext : ISystemContext
    {

        /// <summary>
        /// The host environment.
        /// </summary>
        private readonly IHostEnvironment _hostEnvironment;
        /// <summary>
        /// The system options.
        /// </summary>
        private readonly IOptions<SystemOptions> _systemOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemContext"/> class.
        /// </summary>
        /// <param name="hostEnvironment">The host environment.</param>
        /// <param name="systemOptions">The system options.</param>
        public SystemContext(IHostEnvironment hostEnvironment, IOptions<SystemOptions> systemOptions)
        {
            _systemOptions = systemOptions;
            _hostEnvironment = hostEnvironment;
        }

        /// <summary>
        /// Get company name.
        /// </summary>
        /// <returns>A string</returns>
        public string GetCompanyName()
        {
            return _systemOptions.Value.CompanyName;
        }

        /// <summary>
        /// Get connector name.
        /// </summary>
        /// <returns>A string</returns>
        public string GetConnectorName()
        {
            return _systemOptions.Value.ConnectorName;
        }

        /// <summary>
        /// Get short name.
        /// </summary>
        /// <returns>A string</returns>
        public string GetShortName()
        {
            return _systemOptions.Value.ShortName;
        }

        /// <summary>
        /// Get short name.
        /// </summary>
        /// <returns>A string</returns>
        public string GetServiceName()
        {
            return _systemOptions.Value.ServiceName;
        }

        /// <summary>
        /// Get content root path.
        /// </summary>
        /// <returns>A string</returns>
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

        /// <summary>
        /// Get data root path.
        /// </summary>
        /// <returns>A string</returns>
        public string GetDataRootPath()
        {
            return !string.IsNullOrEmpty(_systemOptions.Value.DataPathRoot) ? _systemOptions.Value.DataPathRoot : GetDefaultDataRootPath();
        }
    }

}
