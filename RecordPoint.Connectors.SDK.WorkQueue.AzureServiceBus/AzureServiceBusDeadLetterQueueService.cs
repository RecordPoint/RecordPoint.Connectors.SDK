using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Newtonsoft.Json;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Extensions;
using RecordPoint.Connectors.SDK.Work.Models;
using RecordPoint.Connectors.SDK.Work;
using RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus;
using RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.Extensions;

namespace RecordPoint.Connectors.SDK.WebHost.Services
{
    /// <summary>
    /// Deadletter queue service class
    /// </summary>
    public class AzureServiceBusDeadLetterQueueService : IDeadLetterQueueService
    {
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ServiceBusAdministrationClient _serviceBusAdministrationClient;
        private readonly IManagedWorkStatusManager _managedWorkStatusManager;

        /// <summary>
        /// Maximum number of messages from the queue
        /// </summary>
        public const int MaxMessages = 1000;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceBusClientFactory"></param>
        /// <param name="managedWorkStatusManager"></param>
        public AzureServiceBusDeadLetterQueueService(IServiceBusClientFactory serviceBusClientFactory, IManagedWorkStatusManager managedWorkStatusManager)
        {
            _serviceBusClient = serviceBusClientFactory.CreateServiceBusClient();
            _serviceBusAdministrationClient = serviceBusClientFactory.CreateServiceBusAdministrationClient();
            _managedWorkStatusManager = managedWorkStatusManager;
        }
        /// <summary>
        /// Get all messages based on the queue
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        public async Task<List<DeadLetterModel>> GetAllMessagesAsync(string queueName)
        {
            var (serviceBusReceivedMessages, _) = await GetAllPeekedServiceBusMessagesAsync(queueName);

            var deadLetterList = new List<DeadLetterModel>();

            foreach (var receivedMessage in serviceBusReceivedMessages)
            {
                deadLetterList.Add(receivedMessage.ToDeadLetterModel());
            }
            return deadLetterList;
        }

        /// <summary>
        /// Get message based on the queue and sequenceNumber
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="sequenceNumber"></param>
        /// <returns></returns>
        public async Task<DeadLetterModel> GetMessageAsync(string queueName, long sequenceNumber)
        {
            var (receivedMessage, _) = await GetPeekedServiceBusMessageAsync(queueName, sequenceNumber);
            return receivedMessage.ToDeadLetterModel();
        }


        /// <summary>
        /// Resubmit to queue based on the queueName and sequenceNumbers
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="sequenceNumbers"></param>
        /// <returns></returns>
        public async Task ResubmitMessagesAsync(string queueName, long[] sequenceNumbers)
        {
            var serviceBusSender = _serviceBusClient.CreateSender(queueName);
            var messageBatch = await serviceBusSender.CreateMessageBatchAsync();

            var deadLetterMessagesList = new List<ServiceBusReceivedMessage>();

            var (receivedMessages, serviceBusReceiver) = await GetReceivedServiceBusMessagesAsync(queueName, sequenceNumbers.ToList());

            foreach (var receivedMessage in receivedMessages)
            {
                var workRequest = JsonConvert.DeserializeObject<WorkRequest>(receivedMessage.Body.ToString());

                if (workRequest != null)
                {
                    //We want to reset the fault count back to 0 
                    workRequest.FaultedCount = 0;
                    var workStatus = ManagedWorkStatusModel.Deserialize(workRequest.Body);

                    if (workStatus?.WorkType is ContentManagerOperation.WORK_TYPE or ContentRegistrationOperation.WORK_TYPE or ContentSynchronisationOperation.WORK_TYPE or ChannelDiscoveryOperation.WORK_TYPE)
                    {
                        await _managedWorkStatusManager.SetWorkRunningAsync(workStatus.Id, CancellationToken.None);
                    }

                    var serialisedWorkRequest = JsonConvert.SerializeObject(workRequest);
                    messageBatch.TryAddMessage(new ServiceBusMessage(serialisedWorkRequest));
                }

                //Remove the message after we put it on the bus
                deadLetterMessagesList.Add(receivedMessage);
            }

            await serviceBusSender.SendMessagesAsync(messageBatch);

            foreach (var deadLetterMessages in deadLetterMessagesList)
            {
                await serviceBusReceiver.CompleteMessageAsync(deadLetterMessages);
            }

        }

        /// <summary>
        /// Delete the message from the deadletter queue
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="sequenceNumber"></param>
        /// <returns></returns>
        public async Task DeleteMessageAsync(string queueName, long sequenceNumber)
        {
            var sequenceNumbers = new List<long> { sequenceNumber };
            var (receivedMessages, serviceBusReceiver) = await GetReceivedServiceBusMessagesAsync(queueName, sequenceNumbers);

            if (receivedMessages[0].SequenceNumber == sequenceNumber)
            {
                await serviceBusReceiver.CompleteMessageAsync(receivedMessages[0]);
            }
        }

        /// <summary>
        /// Delete the all messages from the deadletter queue
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        public async Task DeleteAllMessagesAsync(string queueName)
        {
            var receiverOptions = new ServiceBusReceiverOptions { SubQueue = SubQueue.DeadLetter };
            var serviceBusReceiver = _serviceBusClient.CreateReceiver(queueName, receiverOptions);

            QueueRuntimeProperties queue = await _serviceBusAdministrationClient.GetQueueRuntimePropertiesAsync(queueName);

            // we need to received the message as peekmessage doesn't allow to delete the message 
            for (var i = 0; i < queue.DeadLetterMessageCount; i++)
            {
                var deleteMessage = await serviceBusReceiver.ReceiveMessageAsync();
                if (deleteMessage != null)
                    await serviceBusReceiver.CompleteMessageAsync(deleteMessage);
                else
                    break;
            }
        }

        private async Task<(List<ServiceBusReceivedMessage>, ServiceBusReceiver)> GetReceivedServiceBusMessagesAsync(string queueName, IList<long> sequenceNumbers)
        {
            QueueRuntimeProperties queue = await _serviceBusAdministrationClient.GetQueueRuntimePropertiesAsync(queueName);
            var receiverOptions = new ServiceBusReceiverOptions { SubQueue = SubQueue.DeadLetter };

            var serviceBusReceiver = _serviceBusClient.CreateReceiver(queueName, receiverOptions);

            var messages = new List<ServiceBusReceivedMessage>();

            if (!sequenceNumbers.Any()) return (messages, serviceBusReceiver);

            for (var i = 0; i < queue.DeadLetterMessageCount; i++)
            {
                var message = await serviceBusReceiver.ReceiveMessageAsync();
                if (message == null)
                    break;

                if (sequenceNumbers.Any(x => x.Equals(message.SequenceNumber)))
                {
                    messages.Add(message);
                }
            }

            return (messages, serviceBusReceiver);
        }

        private async Task<(IReadOnlyList<ServiceBusReceivedMessage> serviceBusReceivedMessages, ServiceBusReceiver serviceBusReceiver)> GetAllPeekedServiceBusMessagesAsync(string queueName)
        {
            var receiverOptions = new ServiceBusReceiverOptions { SubQueue = SubQueue.DeadLetter };

            var serviceBusReceiver = _serviceBusClient.CreateReceiver(queueName, receiverOptions);
            var receivedMessages = new List<ServiceBusReceivedMessage>();

            var previousSequenceNumber = -1L;
            var sequenceNumber = 0L;

            do
            {
                var messageBatch = await serviceBusReceiver.PeekMessagesAsync(MaxMessages, sequenceNumber);

                if (messageBatch.Count > 0)
                {
                    // Below line of code will give element position from the end of a list
                    var sequenceNumberFromMessage = messageBatch[^1].SequenceNumber;

                    // Increasing the SequenceNumber by 1 to avoid getting the message with the same SequenceNumber twice
                    sequenceNumber = sequenceNumberFromMessage + 1;

                    if (sequenceNumber == previousSequenceNumber)
                        break;

                    receivedMessages.AddRange(messageBatch);

                    previousSequenceNumber = sequenceNumber;
                }
                else
                {
                    break;
                }
            } while (true);

            return (receivedMessages, serviceBusReceiver);
        }

        private async Task<(ServiceBusReceivedMessage serviceBusReceivedMessage, ServiceBusReceiver serviceBusReceiver)> GetPeekedServiceBusMessageAsync(string queueName, long sequenceNumber)
        {
            var receiverOptions = new ServiceBusReceiverOptions { SubQueue = SubQueue.DeadLetter, ReceiveMode = ServiceBusReceiveMode.PeekLock };

            var serviceBusReceiver = _serviceBusClient.CreateReceiver(queueName, receiverOptions);

            var receivedMessage = await serviceBusReceiver.PeekMessageAsync(sequenceNumber);

            return (receivedMessage, serviceBusReceiver);
        }

    }
}
