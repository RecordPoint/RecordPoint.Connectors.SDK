using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Work;
using RecordPoint.Connectors.SDK.WorkQueue.Services;

namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq
{
    public static class RabbitMqBuilderExtensions
    {
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
