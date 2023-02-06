using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RecordPoint.Connectors.SDK.Context
{

    /// <summary>
    /// On cloud context builder extensions
    /// </summary>
    public static class SystemContextBuilderExtensions
    {

        /// <summary>
        /// Use the on cloud system context
        /// </summary>
        /// <param name="hostBuilder">Host builder to update</param>
        /// <param name="companyName">Optional connector company name</param>
        /// <param name="connectorName">Optional connector name</param>
        /// <param name="shortName">Connector short name</param>
        /// <returns>The updated host builder</returns>
        public static IHostBuilder UseSystemContext(this IHostBuilder hostBuilder, string companyName = null, string connectorName = null, string shortName = null)
        {
            hostBuilder.ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;
                services.Configure<SystemOptions>(configuration.GetSection(SystemOptions.SECTION_NAME));
                services.PostConfigure<SystemOptions>(options =>
                {
                    options.CompanyName = companyName ?? options.CompanyName;
                    options.ConnectorName = connectorName ?? options.ConnectorName;
                    options.ShortName = shortName ?? options.ShortName;
                });
                services.AddSingleton<ISystemContext, SystemContext>();
            });
            return hostBuilder;
        }

    }
}
