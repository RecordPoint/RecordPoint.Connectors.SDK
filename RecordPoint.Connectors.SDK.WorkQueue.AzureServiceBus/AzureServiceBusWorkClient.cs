using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;
using System.Text;

namespace RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus
{
    /// <summary>
    /// Implementation of an IWorkQueueClient that utilises Azure Service Bus as the underlying Queue
    /// </summary>
    public class AzureServiceBusWorkClient : IWorkQueueClient, IAsyncDisposable
    {
        private readonly ServiceBusClient _serviceBusClient;
        private readonly Dictionary<string, ServiceBusSender> _serviceBusSenders = new();
        private readonly IOptions<AzureServiceBusOptions> _serviceBusOptions;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly object _serviceBusSendersLock = new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceBusClientFactory"></param>
        /// <param name="serviceBusOptions"></param>
        /// <param name="dateTimeProvider"></param>
        public AzureServiceBusWorkClient(
            IServiceBusClientFactory serviceBusClientFactory,
            IOptions<AzureServiceBusOptions> serviceBusOptions,
            IDateTimeProvider dateTimeProvider)
        {
            _serviceBusOptions = serviceBusOptions;
            _serviceBusClient = serviceBusClientFactory.CreateServiceBusClient();
            _dateTimeProvider = dateTimeProvider;
        }

        /// <inheritdoc/>
        public async Task SubmitWorkAsync(WorkRequest workRequest, CancellationToken cancellationToken)
        {
            await SendMessageAsync(workRequest, cancellationToken);
        }

        private async Task SendMessageAsync(WorkRequest workRequest, CancellationToken cancellationToken)
        {
            workRequest.SubmitDateTime = _dateTimeProvider.UtcNow;
            var serializedMessage = JsonConvert.SerializeObject(workRequest);
            var messageBytes = Encoding.UTF8.GetBytes(serializedMessage);
            var message = new ServiceBusMessage(messageBytes);
            if (workRequest.WaitTill.HasValue) message.ScheduledEnqueueTime = workRequest.WaitTill.Value;

            var sender = GetServiceBusSender(workRequest.WorkType);
            await sender.SendMessageAsync(message, cancellationToken);
        }

        private ServiceBusSender GetServiceBusSender(string workType)
        {
            var queueName = GetQueueName(workType);
            lock (_serviceBusSendersLock)
            {
                if (!_serviceBusSenders.ContainsKey(queueName)) _serviceBusSenders.Add(queueName, _serviceBusClient.CreateSender(queueName));
            }

            return _serviceBusSenders[queueName];
        }

        private string GetQueueName(string workType) => string.IsNullOrEmpty(_serviceBusOptions.Value.QueuePrefix)
            ? $"{workType.Replace(" ", "-").ToLower()}"
            : $"{_serviceBusOptions.Value.QueuePrefix}-{workType.Replace(" ", "-").ToLower()}";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async ValueTask DisposeAsync()
        {
            foreach (var sender in _serviceBusSenders.Values)
            {
                await sender.DisposeAsync();
            }
            await _serviceBusClient.DisposeAsync();
        }
    }
}
