using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;

namespace RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus
{
    public interface IServiceBusClientFactory
    {
        ServiceBusClient CreateServiceBusClient();
        ServiceBusAdministrationClient CreateServiceBusAdministrationClient();
    }
}