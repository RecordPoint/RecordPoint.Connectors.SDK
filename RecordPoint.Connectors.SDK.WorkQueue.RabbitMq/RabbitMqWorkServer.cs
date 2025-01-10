using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Toggles;
using RecordPoint.Connectors.SDK.Work;
using System.Text;

namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq
{
    /// <summary>
    /// The rabbit mq work server.
    /// </summary>
    public class RabbitMqWorkServer : BackgroundService
    {
        /// <summary>
        /// The exchange name.
        /// </summary>
        private const string ExchangeName = "publish-consumer-exchange";
        /// <summary>
        /// The exchange type.
        /// </summary>
        private const string ExchangeType = "x-delayed-message";
        /// <summary>
        /// The deadletter exchange.
        /// </summary>
        private const string DeadletterExchange = "dead-letter-exchange";
        /// <summary>
        /// The deadletter exchange type.
        /// </summary>
        private const string DeadletterExchangeType = "x-dead-letter-exchange";

        /// <summary>
        /// The service provider.
        /// </summary>
        private readonly IServiceProvider _serviceProvider;
        /// <summary>
        /// The system context.
        /// </summary>
        private readonly ISystemContext _systemContext;
        /// <summary>
        /// Work manager.
        /// </summary>
        private readonly IQueueableWorkManager _workManager;
        /// <summary>
        /// The rabbit mq options.
        /// </summary>
        private readonly IOptions<RabbitMqOptions> _rabbitMqOptions;
        /// <summary>
        /// The scope manager.
        /// </summary>
        private readonly IScopeManager _scopeManager;
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<RabbitMqWorkServer> _logger;
        /// <summary>
        /// The date time provider.
        /// </summary>
        private readonly IDateTimeProvider _dateTimeProvider;
        /// <summary>
        /// The toggle provider.
        /// </summary>
        private readonly IToggleProvider _toggleProvider;


        /// <summary>
        /// The rabbit mq connection.
        /// </summary>
        private readonly IConnection _rabbitMqConnection;
        /// <summary>
        /// The rabbit mq processors.
        /// </summary>
        private readonly Dictionary<string, RabbitMqProcessModel> _rabbitMqProcessors = new();
        /// <summary>
        /// Work queue client.
        /// </summary>
        private readonly IWorkQueueClient _workQueueClient;
        /// <summary>
        /// The service id.
        /// </summary>
        private readonly string _serviceId;
        /// <summary>
        /// Processing token.
        /// </summary>
        private readonly CancellationTokenSource _processingToken = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitMqWorkServer"/> class.
        /// </summary>
        /// <param name="workQueueClient">The work queue client.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="systemContext">The system context.</param>
        /// <param name="workManager">The work manager.</param>
        /// <param name="rabbitMqClientFactory">The rabbit mq client factory.</param>
        /// <param name="rabbitMqOptions">The rabbit mq options.</param>
        /// <param name="scopeManager">The scope manager.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="dateTimeProvider">The date time provider.</param>
        /// <param name="toggleProvider">The toggle provider.</param>
        public RabbitMqWorkServer(
            IWorkQueueClient workQueueClient,
            IServiceProvider serviceProvider,
            ISystemContext systemContext,
            IQueueableWorkManager workManager,
            IRabbitMqClientFactory rabbitMqClientFactory,
            IOptions<RabbitMqOptions> rabbitMqOptions,
            IScopeManager scopeManager,
            ILogger<RabbitMqWorkServer> logger,
            IDateTimeProvider dateTimeProvider,
            IToggleProvider toggleProvider)
        {
            _serviceProvider = serviceProvider;
            _systemContext = systemContext;
            _workManager = workManager;
            _rabbitMqOptions = rabbitMqOptions;
            _logger = logger;
            _dateTimeProvider = dateTimeProvider;
            _scopeManager = scopeManager;
            _toggleProvider = toggleProvider;

            _serviceId = Guid.NewGuid().ToString();
            _rabbitMqConnection = rabbitMqClientFactory.CreateRabbitMqConnection();
            _workQueueClient = workQueueClient;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (var model in _rabbitMqProcessors.Values.Select(processor => processor.RabbitMqModel))
            {
                model.Close();
                model.Dispose();
            }

            _rabbitMqConnection.Close();

            await base.StopAsync(cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var systemScope = _scopeManager.BeginSystemScope(_systemContext);
            using var serviceScope = _scopeManager.BeginServiceScope(RabbitMqDimensions.SERVICE_NAME, _serviceId);

            var startupDimensions = new Dimensions()
            {
                [StandardDimensions.EVENT_TYPE] = EventType.Startup.ToString()
            };
            _logger.LogEvent("Connecting to RabbitMq", startupDimensions);

            try
            {
                if (_rabbitMqOptions == null)
                {
                    _logger.LogEvent("RabbitMqSettings not found");
                    return;
                }

                _logger.LogEvent("RabbitMqSettings found");

                CreateProcessors();

                var isProcessingStarted = false;
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var isKillSwitched = _toggleProvider.GetConnectorKillswitch(_systemContext);
                        if (isKillSwitched && isProcessingStarted)
                        {
                            //If the kill switch has just been enabled, lets stop the processors
                            StopConsumers();
                            isProcessingStarted = false;
                        }
                        else if (!isProcessingStarted && !isKillSwitched)
                        {
                            //If processing is stopped, but the kill switch is not enabled, lets start the processors
                            isProcessingStarted = true;
                            StartConsumers();
                        }

                        await Task.Delay(_rabbitMqOptions.Value.KillswitchCheckInterval, stoppingToken);
                    }
                    catch (OperationCanceledException)
                    {
                        // Do nothing, should result in exiting the loop
                    }
                }

                //If the background host is stopping, lets gracefully shutdown the processors
                if (isProcessingStarted)
                {
                    StopConsumers();
                }

                //Delay to allow currently processing operations to complete
                await Task.Delay(_rabbitMqOptions.Value.ServiceShutdownDelay, CancellationToken.None);
                _processingToken.Cancel();

                CloseModels();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception thrown");
            }

            var shutdownDimensions = new Dimensions()
            {
                [StandardDimensions.EVENT_TYPE] = EventType.Shutdown.ToString()
            };
            _logger.LogEvent("Consumer stopped", shutdownDimensions);
        }

        /// <summary>
        /// Basic IDisposable implementation.
        /// </summary>
        public override void Dispose()
        {
            _processingToken.Dispose();
            base.Dispose();
        }

        private async void HandleIncomingMessagesAsync(object? sender, BasicDeliverEventArgs args)
        {
            var json = Encoding.Default.GetString(args.Body.Span);
            var workRequest = JsonConvert.DeserializeObject<WorkRequest>(json);
            var eventingBasicConsumer = (EventingBasicConsumer)sender!;
            if (workRequest == null) return;

            try
            {
                var result = await _workManager.HandleWorkRequestAsync(workRequest, _processingToken.Token).ConfigureAwait(false);

                workRequest.MustFinishDateTime = _dateTimeProvider.UtcNow + TimeSpan.FromMinutes(1);

                switch (result.ResultType)
                {
                    //When we abandon work, we need to complete the message on queue so it is removed from the queue
                    case WorkResultType.Abandoned:
                    case WorkResultType.Complete:
                        eventingBasicConsumer.Model.BasicAck(args.DeliveryTag, false);
                        break;

                    case WorkResultType.DeadLetter:
                    case WorkResultType.Failed:
                        //Move the message to the Dead Letter Queue
                        eventingBasicConsumer.Model.BasicNack(args.DeliveryTag, false, false);
                        throw new WorkResultException(result.Reason);

                    case WorkResultType.Deferred:
                        eventingBasicConsumer.Model.BasicAck(args.DeliveryTag, false);
                        await DeferMessageAsync(workRequest, CancellationToken.None);
                        break;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception thrown when trying to handle incoming message");
            }
        }

        private async Task DeferMessageAsync(WorkRequest request, CancellationToken cancellationToken)
        {
            var offsetWaitTill = request.WaitTill == null || request.WaitTill < DateTimeOffset.Now
                ? DateTimeOffset.UtcNow.AddSeconds(20)
                : (DateTimeOffset)request.WaitTill;

            request.WaitTill = offsetWaitTill;

            await _workQueueClient.SubmitWorkAsync(request, cancellationToken);
        }

        private void CreateProcessors()
        {
            TryCreateRabbitMqConsumer<ContentManagerOperation>();
            TryCreateRabbitMqConsumer<ChannelDiscoveryOperation>();
            TryCreateRabbitMqConsumer<ContentRegistrationOperation>();
            TryCreateRabbitMqConsumer<ContentSynchronisationOperation>();
            TryCreateRabbitMqConsumer<RecordDisposalOperation>();
            TryCreateRabbitMqConsumer<SubmitAggregationOperation>();
            TryCreateRabbitMqConsumer<SubmitBinaryOperation>();
            TryCreateRabbitMqConsumer<SubmitRecordOperation>();
            TryCreateRabbitMqConsumer<SubmitAuditEventOperation>();
        }

        private void StartConsumers()
        {
            foreach (var queueProcessor in _rabbitMqProcessors)
            {
                queueProcessor.Value.RabbitMqModel.BasicConsume(queueProcessor.Key, false, queueProcessor.Value.RabbitMqEventingBasicConsumer);
            }
        }

        private void StopConsumers()
        {
            foreach (var queueProcessor in _rabbitMqProcessors!.Where(a => a.Value.RabbitMqModel.IsOpen))
            {
                var consumerTags = queueProcessor.Value.RabbitMqEventingBasicConsumer.ConsumerTags;
                foreach (var consumerTag in consumerTags)
                {
                    queueProcessor.Value.RabbitMqModel.BasicCancel(consumerTag);
                }
            }
        }

        private void CloseModels()
        {
            foreach (var processModel in _rabbitMqProcessors.Select(a => a.Value)
                .Where(a => !a.RabbitMqModel.IsClosed))
            {
                processModel.RabbitMqModel.Close();
            }
        }

        private void TryCreateRabbitMqConsumer<TQueueableWork>()
            where TQueueableWork : class, IQueueableWork
        {
            var service = _serviceProvider.GetService<TQueueableWork>();
            if (service == null) return;

            var model = _rabbitMqConnection.CreateModel();

            // Set prefetch count if required
            var dop = _rabbitMqOptions.Value.MaxDegreeOfParallelism;
            if (dop != null)
            {
                model.BasicQos(0, dop.Value, false);
            }

            var processor = new RabbitMqProcessModel
            {
                RabbitMqModel = model,
                RabbitMqEventingBasicConsumer = DeclareAndBindQueueToExchange(model, service.WorkType)
            };
            // add handler to process messages
            processor.RabbitMqEventingBasicConsumer.Received += HandleIncomingMessagesAsync;

            //Add Processor to internal Cache
            var queueName = QueueNameHelper.GetQueueName(service.WorkType, _rabbitMqOptions.Value.QueuePrefix);
            _rabbitMqProcessors.Add(queueName, processor);
        }

        private EventingBasicConsumer DeclareAndBindQueueToExchange(IModel model, string workType)
        {
            //Create Normal Queue
            var dlExchangeArguments = new Dictionary<string, object>
            {
                { DeadletterExchangeType, DeadletterExchange}
            };

            var exchangeArguments = new Dictionary<string, object>
            {
                { "x-delayed-type", "direct" }
            };

            var queueName = QueueNameHelper.GetQueueName(workType, _rabbitMqOptions.Value.QueuePrefix);
            var dlQueueName = QueueNameHelper.GetDLQueueName(workType, _rabbitMqOptions.Value.QueuePrefix);

            model.ExchangeDeclare(ExchangeName, ExchangeType, durable: true, arguments: exchangeArguments);
            model.QueueDeclare(queueName, true, false, false, dlExchangeArguments);
            model.QueueBind(queueName, ExchangeName, dlQueueName);
            var consumer = new EventingBasicConsumer(model);

            //Create Dead Letter Queue            
            model.ExchangeDeclare(DeadletterExchange, RabbitMQ.Client.ExchangeType.Direct, durable: true, false);
            model.QueueDeclare(dlQueueName, true, false, false, dlExchangeArguments);
            model.QueueBind(dlQueueName, DeadletterExchange, dlQueueName);

            return consumer;
        }
    }
}