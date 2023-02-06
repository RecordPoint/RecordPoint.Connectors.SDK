using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.ContentReport.Export;

namespace RecordPoint.Connectors.SDK.ContentReport
{
    /// <summary>
    /// Content Report Host Builder Extensions
    /// </summary>
    public static class ContentReportHostBuilderExtensions
    {
        /// <summary>
        /// Use Connector Content Reporting
        /// </summary>
        /// <param name="hostBuilder">App host builder</param>
        /// <returns>Updated host builder</returns>
        public static IHostBuilder UseContentReport(this IHostBuilder hostBuilder)
        {
            return hostBuilder
                .ConfigureServices((hostContext, serviceCollection) =>
                {
                    var options = hostContext.Configuration.GetSection(nameof(ExportOptions));
                    serviceCollection
                        .Configure<ExportOptions>(options)
                        // TODO: Get working again!
                        // .AddContentReports()
                        .AddExports();
                });
        }
    }
}