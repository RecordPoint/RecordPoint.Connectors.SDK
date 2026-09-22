using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
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
        private readonly IConnection _rabbitMqConnection;
        private readonly IDateTimeProvider _dateTimeProvider;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rabbitMqClientFactory"></param>
        /// <param name="dateTimeProvider"></param>
        public RabbitMqDeadLetterQueueService(IRabbitMqClientFactory rabbitMqClientFactory,
            IDateTimeProvider dateTimeProvider)
        {
            _rabbitMqConnection = rabbitMqClientFactory.CreateRabbitMqConnection();
            _dateTimeProvider = dateTimeProvider;
        }

        /// <summary>
        /// Get all messages based on the queue
        /// </summary>
        public async Task<List<DeadLetterModel>> GetMessagesAsync(string queueName, int maxCount = 0)
        {
            var deadLetterList = new List<DeadLetterModel>();
            var dlqName = GetDlqName(queueName);
            var totalCount = 0;
            maxCount = maxCount == 0 ? Int32.MaxValue : maxCount;
            using var channel = await _rabbitMqConnection.CreateChannelAsync();
            do
            {
                var message = await channel.BasicGetAsync(dlqName, false);
                if (message != null)
                {
                    deadLetterList.Add(message.ToDeadLetterModel());
                    totalCount++;
                }
                else
                {
                    break;
                }
            }
            while (totalCount < maxCount);

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
            var dlQueueName = GetDlqName(queueName);
            using var channel = await _rabbitMqConnection.CreateChannelAsync();
            do
            {
                var message = await channel.BasicGetAsync(dlQueueName, false);
                if (message != null && message.DeliveryTag == Convert.ToUInt64(sequenceNumber))
                {
                    return message.ToDeadLetterModel();
                }
            }
            while (true);
        }


        /// <summary>
        /// Resubmit to queue based on the queueName and sequenceNumbers
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="sequenceNumbers"></param>
        /// <returns></returns>
        public async Task ResubmitMessagesAsync(string queueName, long[] sequenceNumbers)
        {
            using var channel = await _rabbitMqConnection.CreateChannelAsync();

            var receivedMessages = new List<BasicGetResult>();
            var dlQueueName = GetDlqName(queueName);
            do
            {
                var message = await channel.BasicGetAsync(dlQueueName, false);
                if (message != null && Array.Exists(sequenceNumbers, s => Convert.ToUInt64(s) == message.DeliveryTag))
                {
                    receivedMessages.Add(message);
                    continue;
                }

                break;
            }
            while (true);

            foreach (var deadLetterMessageBody in receivedMessages.Select(deadLetterMessage => deadLetterMessage.Body))
            {
                var serializedMessage = Encoding.UTF8.GetString(deadLetterMessageBody.ToArray());

                var workRequest = JsonConvert.DeserializeObject<WorkRequest>(serializedMessage);
                if (workRequest == null) continue;

                //We want to reset the fault count back to 0 
                workRequest.FaultedCount = 0;

                var props = new BasicProperties
                {
                    Persistent = true
                };

                if (workRequest.WaitTill.HasValue)
                {
                    var delayMilliSeconds = workRequest.WaitTill.Value.Subtract(_dateTimeProvider.UtcNow).TotalMilliseconds;
                    props.Headers = new Dictionary<string, object?>
                    {
                        { ExchangeDelayHeader, Convert.ToInt64(Math.Max(0, delayMilliSeconds)) }
                    };
                }

                await channel.BasicPublishAsync(ExchangeName, queueName, false, props, deadLetterMessageBody);
            }

            foreach (var deadLetterMessages in receivedMessages)
            {
                await channel.BasicAckAsync(deadLetterMessages.DeliveryTag, false);
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
            using var channel = await _rabbitMqConnection.CreateChannelAsync();
            do
            {
                var message = await channel.BasicGetAsync(dlqName, false);
                if (message != null && message.DeliveryTag == Convert.ToUInt64(sequenceNumber))
                {
                    await channel.BasicAckAsync(message.DeliveryTag, false);
                    break;
                }
            }
            while (true);
        }

        /// <summary>
        /// Delete all messages from the dead-letter queue.
        /// </summary>
        /// <param name="queueName"></param>
        public async Task DeleteAllMessagesAsync(string queueName)
        {
            var dlqName = GetDlqName(queueName);
            using var channel = await _rabbitMqConnection.CreateChannelAsync();

            do
            {
                var message = await channel.BasicGetAsync(dlqName, false);
                if (message != null)
                {
                    await channel.BasicAckAsync(message.DeliveryTag, false);
                }
                else
                {
                    break;
                }
            }
            while (true);
        }

        /// <summary>
        /// Drain up to <paramref name="maxCount"/> messages from the dead-letter
        /// queue and resubmit them. For RabbitMQ this is a thin wrapper around
        /// <see cref="GetMessagesAsync"/> + <see cref="ResubmitMessagesAsync"/>;
        /// the peek/receive mismatch that motivated the Service Bus version of
        /// this method does not apply here.
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="maxCount"></param>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Caveat: the returned count is the number of dead-letters found by
        /// <see cref="GetMessagesAsync"/>, not a settled-success count.
        /// <see cref="ResubmitMessagesAsync"/> may republish fewer if the two BasicGet
        /// passes desynchronise (delivery tags are channel-scoped, so concurrent DLQ
        /// activity between the passes can cause a mismatch). This differs from the
        /// Service Bus implementation, which returns the count actually resubmitted.
        /// RabbitMQ is not the production replay path; tightening this to a true
        /// settled count (single-pass fetch+ack+republish) is tracked separately.
        /// </remarks>
        public async Task<DeadLetterResubmitResult> ResubmitTopMessagesAsync(string queueName, int maxCount, CancellationToken cancellationToken = default)
        {
            // A non-positive maxCount is a no-op request. Report it as terminal (confirmed
            // empty) so a caller looping "until confirmed empty" with maxCount <= 0 stops
            // rather than spinning forever.
            if (maxCount <= 0) return new DeadLetterResubmitResult(0, QueueConfirmedEmpty: true);

            cancellationToken.ThrowIfCancellationRequested();

            var deadLetters = await GetMessagesAsync(queueName, maxCount);
            // Never claim the DLQ is confirmed empty from RabbitMQ. The only cheap signal
            // (QueueDeclarePassive) reports the READY count, which excludes unacked/in-flight
            // messages - and GetMessagesAsync itself briefly holds messages unacked (BasicGet
            // autoAck:false + dispose-requeue), as can any concurrent consumer. Reporting empty
            // from that could emit a false terminal "No dead letters found" while messages
            // still exist - the very failure this change fixes. Confirming true emptiness would
            // need ready AND unacked counts (a management-API call). RabbitMQ is not the
            // production replay path, so we keep it on a non-terminal contract: always report
            // not-confirmed-empty and let the caller decide when to stop.
            if (deadLetters.Count == 0) return new DeadLetterResubmitResult(0, QueueConfirmedEmpty: false);

            var sequenceNumbers = deadLetters
                .Select(dlm => long.Parse(dlm.SequenceNumber))
                .ToArray();

            cancellationToken.ThrowIfCancellationRequested();

            await ResubmitMessagesAsync(queueName, sequenceNumbers);
            return new DeadLetterResubmitResult(sequenceNumbers.Length, false);
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
