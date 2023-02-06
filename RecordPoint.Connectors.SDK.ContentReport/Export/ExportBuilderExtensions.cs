using Microsoft.Extensions.DependencyInjection;

namespace RecordPoint.Connectors.SDK.ContentReport.Export
{

    /// <summary>
    /// Export manager builder extensions
    /// </summary>
    public static class ExportBuilderExtensions
    {

        public static IServiceCollection AddExports(this IServiceCollection services)
        {
            services.AddTransient<IExport, CsvExport>();
            return services;
        }

    }
}
