using Microsoft.Extensions.DependencyInjection;
using RecordPoint.Connectors.SDK.Notifications;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.ContentReport
{

    /// <summary>
    /// Content reports builder extensions
    /// </summary>
    public static class ContentReportBuilderExtension
    {

        /// <summary>
        /// Add the content manager
        /// </summary>
        /// <param name="services">Service collection to add to</param>
        /// <returns>Updated service collection</returns>
        public static IServiceCollection AddContentReports(this IServiceCollection services)
        {
            services
                .AddAddQueueableWorkOperation<GenerateReportOperation>(GenerateReportOperation.WORK_TYPE)
                .AddSingleton<INotificationStrategy, GenerateReportHandler>()
                .AddSingleton<IContentReportFilter, ContentReportFilter>();

            return services;
        }

    }
}
