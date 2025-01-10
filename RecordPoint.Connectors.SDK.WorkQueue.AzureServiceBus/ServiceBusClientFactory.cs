using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Configuration;

namespace RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus
{
    /// <summary>
    /// A factory that creates a ServiceBusClient
    /// </summary>
    public class ServiceBusClientFactory : IServiceBusClientFactory
    {
        private readonly IConfiguration _configuration;
        private readonly IOptions<AzureServiceBusOptions> _serviceBusOptions;

        /// <summary>
        /// Constructor for the Factory
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="serviceBusOptions"></param>
        public ServiceBusClientFactory(
            IConfiguration configuration,
            IOptions<AzureServiceBusOptions> serviceBusOptions)
        {
            _configuration = configuration;
            _serviceBusOptions = serviceBusOptions;
        }

        /// <summary>
        /// Creates an instance of a ServiceBusClient
        /// </summary>
        /// <returns></returns>
        public ServiceBusClient CreateServiceBusClient()
        {
            if (!string.IsNullOrEmpty(_serviceBusOptions.Value.ServiceBusConnectionString))
            {
                return new ServiceBusClient(_serviceBusOptions.Value.ServiceBusConnectionString);
            }

            var azureAuthenticationOptions = _configuration.GetSection(AzureAuthenticationOptions.SECTION_NAME).Get<AzureAuthenticationOptions>();
            var credential = azureAuthenticationOptions.GetTokenCredential();

            var fullyQualifiedNamespace = $"{_serviceBusOptions.Value.ServiceBusName}.servicebus.windows.net";


            return new ServiceBusClient(fullyQualifiedNamespace, credential);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ServiceBusAdministrationClient CreateServiceBusAdministrationClient()
        {
            if (!string.IsNullOrEmpty(_serviceBusOptions.Value.ServiceBusConnectionString))
            {
                return new ServiceBusAdministrationClient(_serviceBusOptions.Value.ServiceBusConnectionString);
            }

            var azureAuthenticationOptions = _configuration.GetSection(AzureAuthenticationOptions.SECTION_NAME).Get<AzureAuthenticationOptions>();
            var credential = azureAuthenticationOptions.GetTokenCredential();

            var fullyQualifiedNamespace = $"{_serviceBusOptions.Value.ServiceBusName}.servicebus.windows.net";


            return new ServiceBusAdministrationClient(fullyQualifiedNamespace, credential);
        }
    }
}
