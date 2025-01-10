using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;

namespace RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus
{
    /// <summary>
    /// 
    /// </summary>
    public interface IServiceBusClientFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ServiceBusClient CreateServiceBusClient();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ServiceBusAdministrationClient CreateServiceBusAdministrationClient();
    }
}