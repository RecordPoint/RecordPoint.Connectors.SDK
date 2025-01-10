using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;
using System.Text;

namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq
{
    /// <summary>
    /// Implementation of an IWorkQueueClient that utilises RabbitMq as the underlying Queue
    /// </summary>
    public class RabbitMqWorkClient : IWorkQueueClient, IAsyncDisposable
    {
        private const string ExchangeName = "publish-consumer-exchange";
        private const string ExchangeDelayHeader = "x-delay";

        private readonly IConnection _rabbitMqConnection;
        private readonly Dictionary<string, IModel> _rabbitMqSenders = new();
        private readonly IOptions<RabbitMqOptions> _rabbitMqOptions;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly object _rabbitMqClientLock = new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rabbitMqClientFactory"></param>
        /// <param name="rabbitMqOptions"></param>
        /// <param name="dateTimeProvider"></param>
        public RabbitMqWorkClient(
            IRabbitMqClientFactory rabbitMqClientFactory,
            IOptions<RabbitMqOptions> rabbitMqOptions,
            IDateTimeProvider dateTimeProvider)
        {
            _rabbitMqOptions = rabbitMqOptions;
            _rabbitMqConnection = rabbitMqClientFactory.CreateRabbitMqConnection();
            _dateTimeProvider = dateTimeProvider;
        }

        /// <inheritdoc/>
        public async Task SubmitWorkAsync(WorkRequest workRequest, CancellationToken cancellationToken)
        {
            await SendMessageAsync(workRequest);
        }

        private async Task SendMessageAsync(WorkRequest workRequest)
        {
            workRequest.SubmitDateTime = _dateTimeProvider.UtcNow;
            var serializedMessage = JsonConvert.SerializeObject(workRequest);
            var messageBytes = Encoding.UTF8.GetBytes(serializedMessage);
            var sender = GetRabbitMqSender(workRequest.WorkType);

            IBasicProperties props = sender.CreateBasicProperties();
            props.Persistent = true;

            if (workRequest.WaitTill.HasValue)
            {
                var delayMilliSeconds = workRequest.WaitTill.Value.Subtract(_dateTimeProvider.UtcNow).TotalMilliseconds;
                if (props.Headers == null || props.Headers.Count == 0)
                {
                    props.Headers = new Dictionary<string, object>();
                }
                props.Headers.Add(ExchangeDelayHeader, delayMilliSeconds);
            }

            var dlQueueName = QueueNameHelper.GetDLQueueName(workRequest.WorkType, _rabbitMqOptions.Value.QueuePrefix);
            sender.BasicPublish(ExchangeName, dlQueueName, props, messageBytes);
            await Task.CompletedTask;
        }

        private IModel GetRabbitMqSender(string workType)
        {
            var queueName = QueueNameHelper.GetQueueName(workType, _rabbitMqOptions.Value.QueuePrefix);
            lock (_rabbitMqClientLock)
            {
                if (!_rabbitMqSenders.ContainsKey(queueName)) _rabbitMqSenders.Add(queueName, _rabbitMqConnection.CreateModel());
            }

            return _rabbitMqSenders[queueName];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ValueTask DisposeAsync()
        {
            foreach (var sender in _rabbitMqSenders.Values)
            {
                sender.Dispose();
            }
            _rabbitMqConnection.Dispose();
            return ValueTask.CompletedTask;
        }
    }
}
