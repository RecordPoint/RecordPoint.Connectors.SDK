using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq
{
    /// <summary>
    /// The rabbit mq builder extensions.
    /// </summary>
    public static class RabbitMqBuilderExtensions
    {
        /// <summary>
        /// Use rabbit mq work queue.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <returns>An IHostBuilder</returns>
        public static IHostBuilder UseRabbitMqWorkQueue(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;
                services.Configure<RabbitMqOptions>(configuration.GetSection(RabbitMqOptions.SECTION_NAME));
                services.AddSingleton<IRabbitMqClientFactory, RabbitMqClientFactory>();
                services.AddSingleton<IWorkQueueClient, RabbitMqWorkClient>();
                services.AddHostedService<RabbitMqWorkServer>();
            });

            return hostBuilder;
        }
        /// <summary>
        /// Use rabbit mq dead letter queue service.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <returns>An IHostBuilder</returns>
        public static IHostBuilder UseRabbitMqDeadLetterQueueService(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((hostContext, services) =>
            {
                services.AddTransient<IDeadLetterQueueService, RabbitMqDeadLetterQueueService>();
            });

            return hostBuilder;
        }
    }
}
