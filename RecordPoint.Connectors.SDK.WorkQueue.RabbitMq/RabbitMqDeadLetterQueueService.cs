using Newtonsoft.Json;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Extensions;
using RecordPoint.Connectors.SDK.Work.Models;
using RecordPoint.Connectors.SDK.Work;
using RecordPoint.Connectors.SDK.WorkQueue.RabbitMq;
using RabbitMQ.Client;
using System.Data.Common;
using System.Text;
using RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.Extensions;
using System.Reflection;
using RecordPoint.Connectors.SDK.Providers;

namespace RecordPoint.Connectors.SDK.WorkQueue.Services
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
        /// <param name="serviceBusClientFactory"></param>
        /// <param name="managedWorkStatusManager"></param>
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
            using var channel = _rabbitMqConnection.CreateModel();
            do
            {
                var message = channel.BasicGet(queueName, false);
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
            var deadLetterModel = new DeadLetterModel();
            using var channel = _rabbitMqConnection.CreateModel();
            do
            {
                var message = channel.BasicGet(queueName, false);
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
            var dlQueueName = queueName + "-DL";
            do
            {
                var message = channel.BasicGet(dlQueueName, false);
                if (message != null && sequenceNumbers.Any(s => Convert.ToUInt64(s) == message.DeliveryTag))
                {
                    receivedMessages.Add(message);
                }
                else
                {
                    break;
                }
            }
            while (true);

            foreach (var receivedMessage in receivedMessages)
            {
                var serilizeMessage = Encoding.UTF8.GetString(receivedMessage.Body.ToArray());

                var workRequest = JsonConvert.DeserializeObject<WorkRequest>(serilizeMessage);

                if (workRequest != null)
                {
                    //We want to reset the fault count back to 0 
                    workRequest.FaultedCount = 0;
                    var workStatus = ManagedWorkStatusModel.Deserialize(workRequest.Body);

                    if (workStatus?.WorkType is ContentManagerOperation.WORK_TYPE or ContentRegistrationOperation.WORK_TYPE or ContentSynchronisationOperation.WORK_TYPE or ChannelDiscoveryOperation.WORK_TYPE)
                    {
                        await _managedWorkStatusManager.SetWorkRunningAsync(workStatus.Id, CancellationToken.None);
                    }
                   
                    IBasicProperties props = channel.CreateBasicProperties();

                    if (workRequest.WaitTill.HasValue)
                    {
                        var delayMilliSeconds = workRequest.WaitTill.Value.Subtract(_dateTimeProvider.UtcNow).TotalMilliseconds;
                        props.Headers = new Dictionary<string, object>
                        {
                            { ExchangeDelayHeader, delayMilliSeconds }
                        };
                    }

                    messageBatch.Add(ExchangeName, dlQueueName, false, props, receivedMessage.Body);
                }
            }

            messageBatch.Publish();

            foreach (var deadLetterMessages in receivedMessages)
            {
                channel.BasicAck(deadLetterMessages.DeliveryTag, false);
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
            using var channel = _rabbitMqConnection.CreateModel();
            do
            {
                var message = channel.BasicGet(queueName, false);
                if (message != null && message.DeliveryTag == Convert.ToUInt64(sequenceNumber))
                {
                    channel.BasicAck(message.DeliveryTag, false);
                    break;
                }
            }
            while (true);

            await Task.CompletedTask;
        }

        public async Task DeleteAllMessagesAsync(string queueName)
        {
            using var channel = _rabbitMqConnection.CreateModel();

            do
            {
                var message = channel.BasicGet(queueName, false);
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
    }
}
