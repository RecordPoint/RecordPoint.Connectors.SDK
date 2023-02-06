using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.WebHost.Services;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus
{
    public static class AzureServiceBusBuilderExtensions
    {
        public static IHostBuilder UseASBWorkQueue(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;
                services.Configure<AzureServiceBusOptions>(configuration.GetSection(AzureServiceBusOptions.SECTION_NAME));
                services.AddSingleton<IServiceBusClientFactory, ServiceBusClientFactory>();
                services.AddSingleton<IWorkQueueClient, AzureServiceBusWorkClient>();
                services.AddHostedService<AzureServiceBusWorkServer>();
            });

            return hostBuilder;
        }

        public static IHostBuilder UseASBDeadLetterQueueService(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((hostContext, services) =>
            {                
                services.AddTransient<IDeadLetterQueueService, AzureServiceBusDeadLetterQueueService>();
                services.AddSingleton<IServiceBusClientFactory, ServiceBusClientFactory>();
            });

            return hostBuilder;
        }
    }
}
