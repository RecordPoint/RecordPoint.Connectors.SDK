using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;
using RecordPoint.Connectors.SDK.Work.Models;

namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq
{
    /// <summary>
    /// Deadletter queue service class
    /// </summary>
    public class RabbitMqDeadLetterQueueService : IDeadLetterQueueService
    {
        private const string ExchangeName = "publish-consumer-exchange";
        private const string ExchangeDelayHeader = "x-delay";
        private readonly IManagedWorkStatusManager _managedWorkStatusManager;
        private readonly IConnection _rabbitMqConnection;
        private readonly IDateTimeProvider _dateTimeProvider;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rabbitMqClientFactory"></param>
        /// <param name="managedWorkStatusManager"></param>
        /// <param name="dateTimeProvider"></param>
        public RabbitMqDeadLetterQueueService(IRabbitMqClientFactory rabbitMqClientFactory,
            IManagedWorkStatusManager managedWorkStatusManager,
            IDateTimeProvider dateTimeProvider)
        {
            _rabbitMqConnection = rabbitMqClientFactory.CreateRabbitMqConnection();
            _managedWorkStatusManager = managedWorkStatusManager;
            _dateTimeProvider = dateTimeProvider;
        }

        /// <summary>
        /// Get all messages based on the queue
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        public async Task<List<DeadLetterModel>> GetAllMessagesAsync(string queueName)
        {
            var deadLetterList = new List<DeadLetterModel>();
            var dlqName = GetDlqName(queueName);
            using var channel = _rabbitMqConnection.CreateModel();
            do
            {
                var message = channel.BasicGet(dlqName, false);
                if (message != null)
                {
                    deadLetterList.Add(message.ToDeadLetterModel());
                }
                else
                {
                    break;
                }
            }
            while (true);

            return await Task.FromResult(deadLetterList);
        }

        /// <summary>
        /// Get message based on the queue and sequenceNumber
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="sequenceNumber"></param>
        /// <returns></returns>
        public async Task<DeadLetterModel> GetMessageAsync(string queueName, long sequenceNumber)
        {
            var dlQueueName = GetDlqName(queueName);
            DeadLetterModel deadLetterModel;
            using var channel = _rabbitMqConnection.CreateModel();
            do
            {
                var message = channel.BasicGet(dlQueueName, false);
                if (message != null && message.DeliveryTag == Convert.ToUInt64(sequenceNumber))
                {
                    deadLetterModel = message.ToDeadLetterModel();
                    break;
                }
            }
            while (true);

            return await Task.FromResult(deadLetterModel);
        }


        /// <summary>
        /// Resubmit to queue based on the queueName and sequenceNumbers
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="sequenceNumbers"></param>
        /// <returns></returns>
        public async Task ResubmitMessagesAsync(string queueName, long[] sequenceNumbers)
        {
            using var channel = _rabbitMqConnection.CreateModel();

            var receivedMessages = new List<BasicGetResult>();
            var messageBatch = channel.CreateBasicPublishBatch();
            var dlQueueName = GetDlqName(queueName);
            do
            {
                var message = channel.BasicGet(dlQueueName, false);
                if (message != null && Array.Exists(sequenceNumbers, s => Convert.ToUInt64(s) == message.DeliveryTag))
                {
                    receivedMessages.Add(message);
                    continue;
                }

                break;
            }
            while (true);

            foreach (var messageBody in receivedMessages.Select(m => m.Body))
            {
                var serializedMessage = Encoding.UTF8.GetString(messageBody.ToArray());

                var workRequest = JsonConvert.DeserializeObject<WorkRequest>(serializedMessage);
                if (workRequest == null) continue;

                //We want to reset the fault count back to 0 
                workRequest.FaultedCount = 0;
                var workStatus = ManagedWorkStatusModel.Deserialize(workRequest.Body);

                if (workStatus?.WorkType is ContentManagerOperation.WORK_TYPE or ContentRegistrationOperation.WORK_TYPE or ContentSynchronisationOperation.WORK_TYPE or ChannelDiscoveryOperation.WORK_TYPE)
                {
                    await _managedWorkStatusManager.SetWorkRunningAsync(workStatus.Id, CancellationToken.None);
                }

                IBasicProperties props = channel.CreateBasicProperties();
                props.Persistent = true;

                if (workRequest.WaitTill.HasValue)
                {
                    var delayMilliSeconds = workRequest.WaitTill.Value.Subtract(_dateTimeProvider.UtcNow).TotalMilliseconds;
                    props.Headers = new Dictionary<string, object>
                    {
                        { ExchangeDelayHeader, delayMilliSeconds }
                    };
                }

                messageBatch.Add(ExchangeName, dlQueueName, false, props, messageBody);
            }

            messageBatch.Publish();

            foreach (var deadLetterMessages in receivedMessages)
            {
                channel.BasicAck(deadLetterMessages.DeliveryTag, false);
            }

        }

        /// <summary>
        /// Delete a specific message from the dead-letter queue
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="sequenceNumber"></param>
        /// <returns></returns>
        public async Task DeleteMessageAsync(string queueName, long sequenceNumber)
        {
            var dlqName = GetDlqName(queueName);
            using var channel = _rabbitMqConnection.CreateModel();
            do
            {
                var message = channel.BasicGet(dlqName, false);
                if (message != null && message.DeliveryTag == Convert.ToUInt64(sequenceNumber))
                {
                    channel.BasicAck(message.DeliveryTag, false);
                    break;
                }
            }
            while (true);

            await Task.CompletedTask;
        }

        /// <summary>
        /// Delete all messages from the dead-letter queue.
        /// </summary>
        /// <param name="queueName"></param>
        public async Task DeleteAllMessagesAsync(string queueName)
        {
            var dlqName = GetDlqName(queueName);
            using var channel = _rabbitMqConnection.CreateModel();

            do
            {
                var message = channel.BasicGet(dlqName, false);
                if (message != null)
                {
                    channel.BasicAck(message.DeliveryTag, false);
                }
                else
                {
                    break;
                }
            }
            while (true);

            await Task.CompletedTask;
        }

        private static string GetDlqName(string queueName)
        {
            // Allow users to use the base queue name
            // (for consistency with the Service Bus implementation)
            return queueName.EndsWith(QueueNameHelper.DeadLetterSuffix) 
                ? queueName 
                : $"{queueName}-{QueueNameHelper.DeadLetterSuffix}";
        }
    }
}
