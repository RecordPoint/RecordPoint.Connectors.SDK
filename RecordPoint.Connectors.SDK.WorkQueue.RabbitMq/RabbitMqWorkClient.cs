using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;
using System.Text;
using System.Threading;

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
        private readonly Dictionary<string, IChannel> _rabbitMqSenders = new();
        private readonly IOptions<RabbitMqOptions> _rabbitMqOptions;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly SemaphoreSlim _rabbitMqSenderLock = new(1, 1);

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
            await SendMessageAsync(workRequest, cancellationToken);
        }

        private async Task SendMessageAsync(WorkRequest workRequest, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            workRequest.SubmitDateTime = _dateTimeProvider.UtcNow;
            var serializedMessage = JsonConvert.SerializeObject(workRequest);
            var messageBytes = Encoding.UTF8.GetBytes(serializedMessage);
            var sender = await GetRabbitMqSenderAsync(workRequest.WorkType, cancellationToken);

            var props = new BasicProperties
            {
                Persistent = true
            };

            if (workRequest.WaitTill.HasValue)
            {
                var delayMilliSeconds = workRequest.WaitTill.Value.Subtract(_dateTimeProvider.UtcNow).TotalMilliseconds;
                if (props.Headers == null || props.Headers.Count == 0)
                {
                    props.Headers = new Dictionary<string, object?>();
                }

                props.Headers[ExchangeDelayHeader] = Convert.ToInt64(Math.Max(0, delayMilliSeconds));
            }

            var dlQueueName = QueueNameHelper.GetDLQueueName(workRequest.WorkType, _rabbitMqOptions.Value.QueuePrefix);
            await sender.BasicPublishAsync(ExchangeName, dlQueueName, false, props, messageBytes, cancellationToken);
        }

        private async Task<IChannel> GetRabbitMqSenderAsync(string workType, CancellationToken cancellationToken)
        {
            var queueName = QueueNameHelper.GetQueueName(workType, _rabbitMqOptions.Value.QueuePrefix);
            if (_rabbitMqSenders.TryGetValue(queueName, out var existingSender))
            {
                return existingSender;
            }

            await _rabbitMqSenderLock.WaitAsync(cancellationToken);
            try
            {
                if (!_rabbitMqSenders.TryGetValue(queueName, out existingSender))
                {
                    existingSender = await _rabbitMqConnection.CreateChannelAsync(cancellationToken: cancellationToken);
                    _rabbitMqSenders.Add(queueName, existingSender);
                }
            }
            finally
            {
                _rabbitMqSenderLock.Release();
            }

            return existingSender;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async ValueTask DisposeAsync()
        {
            foreach (var sender in _rabbitMqSenders.Values)
            {
                if (sender.IsOpen)
                {
                    await sender.CloseAsync(cancellationToken: CancellationToken.None);
                }
                sender.Dispose();
            }

            if (_rabbitMqConnection.IsOpen)
            {
                await _rabbitMqConnection.CloseAsync(cancellationToken: CancellationToken.None);
            }

            _rabbitMqConnection.Dispose();
            _rabbitMqSenderLock.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
