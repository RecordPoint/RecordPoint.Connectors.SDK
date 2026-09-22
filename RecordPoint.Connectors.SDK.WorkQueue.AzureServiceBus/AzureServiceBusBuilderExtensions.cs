using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.WebHost.Services;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus
{
    /// <summary>
    /// The azure service bus builder extensions.
    /// </summary>
    public static class AzureServiceBusBuilderExtensions
    {
        /// <summary>
        /// Use ASB work queue.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <param name="operationTypes">Optional list of operation types to register for use by AzureServiceBusWorkServer</param>
        /// <returns>An IHostBuilder</returns>
        public static IHostBuilder UseASBWorkQueue(this IHostBuilder hostBuilder, IList<Type>? operationTypes = null)
        {
            hostBuilder.ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;
                services.Configure<AzureServiceBusOptions>(configuration.GetSection(AzureServiceBusOptions.SECTION_NAME));
                services.AddSingleton<IServiceBusClientFactory, ServiceBusClientFactory>();
                services.AddSingleton<IWorkQueueClient, AzureServiceBusWorkClient>();
                services.AddSingleton(operationTypes ?? new List<Type>());
                services.AddHostedService<AzureServiceBusWorkServer>();
            });

            return hostBuilder;
        }

        /// <summary>
        /// Use ASB work queue.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <param name="operationTypes">Optional list of operation types to register for use by AzureServiceBusWorkServer</param>
        /// <returns>An IHostBuilder</returns>
        public static IHostBuilder UseASBWorkClient(this IHostBuilder hostBuilder, IList<Type>? operationTypes = null)
        {
            hostBuilder.ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;
                services.Configure<AzureServiceBusOptions>(configuration.GetSection(AzureServiceBusOptions.SECTION_NAME));
                services.AddSingleton<IServiceBusClientFactory, ServiceBusClientFactory>();
                services.AddSingleton<IWorkQueueClient, AzureServiceBusWorkClient>();
            });

            return hostBuilder;
        }

        /// <summary>
        /// Use ASB dead letter queue service.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <returns>An IHostBuilder</returns>
        public static IHostBuilder UseASBDeadLetterQueueService(this IHostBuilder hostBuilder)
            => hostBuilder.UseASBDeadLetterQueueService(_ => { });

        /// <summary>
        /// Use ASB dead letter queue service with controller options.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <param name="configureOptions">Optional controller policy configuration.</param>
        /// <returns>An IHostBuilder</returns>
        public static IHostBuilder UseASBDeadLetterQueueService(this IHostBuilder hostBuilder, Action<DeadLetterControllerOptions> configureOptions)
        {
            hostBuilder.ConfigureServices((hostContext, services) =>
            {
                var options = new DeadLetterControllerOptions();
                configureOptions(options);

                services.AddTransient<IDeadLetterQueueService, AzureServiceBusDeadLetterQueueService>();
                services.AddSingleton<IServiceBusClientFactory, ServiceBusClientFactory>();
                services.AddSingleton<IOptions<DeadLetterControllerOptions>>(Options.Create(options));
            });

            return hostBuilder;
        }
    }
}
