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
using System.Diagnostics;
using System.Reflection;
using System.Text;
using YamlDotNet.Core.Tokens;

namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq
{
    public class RabbitMqWorkServer : BackgroundService
    {
        private const string ExchangeName = "publish-consumer-exchange";
        private const string ExchangeType = "x-delayed-message";
        private const string DeadletterExchange = "dead-letter-exchange";
        private const string DeadletterExchangeType = "x-dead-letter-exchange";

        private readonly IServiceProvider _serviceProvider;
        private readonly ISystemContext _systemContext;
        private readonly IQueueableWorkManager _workManager;
        private readonly IOptions<RabbitMqOptions> _rabbitMqOptions;
        private readonly IScopeManager _scopeManager;
        private readonly ILogger<RabbitMqWorkServer> _logger;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IToggleProvider _toggleProvider;


        private readonly IConnection _rabbitMqConnection;
        private readonly Dictionary<string, RabbitMqProcessModel> _rabbitMqProcessors = new();
        private readonly IWorkQueueClient _workQueueClient;
        private readonly string _serviceId;
        private readonly CancellationTokenSource _processingToken = new();

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

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (var processor in _rabbitMqProcessors.Values)
            {
                processor.RabbitMqModel.Close();
                processor.RabbitMqModel.Dispose();
            }

            _rabbitMqConnection.Close();

            await base.StopAsync(cancellationToken);
        }

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
                else
                {
                    _logger.LogEvent("RabbitMqSettings found");
                }

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
                    case WorkResultType.Complete:
                        eventingBasicConsumer!.Model.BasicAck(args.DeliveryTag, false);
                        break;

                    case WorkResultType.Abandoned:
                        //When we abandon work, we need to complete the message on queue so it is removed from the queue
                        eventingBasicConsumer!.Model.BasicAck(args.DeliveryTag, false);
                        throw new WorkResultException(result.Reason);

                    case WorkResultType.DeadLetter:
                    case WorkResultType.Failed:
                        //Move the message to the Dead Letter Queue
                        eventingBasicConsumer!.Model.BasicNack(args.DeliveryTag, false, false);
                        throw new WorkResultException(result.Reason);

                    case WorkResultType.Deferred:
                        eventingBasicConsumer!.Model.BasicAck(args.DeliveryTag, false);
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
            foreach (var queueProcessor in _rabbitMqProcessors.Where(a => !a.Value.RabbitMqModel.IsClosed))
            {
                queueProcessor.Value.RabbitMqModel.Close();
            }
        }

        private void TryCreateRabbitMqConsumer<TQueueableWork>()
            where TQueueableWork : IQueueableWork
        {
            var service = _serviceProvider.GetService<TQueueableWork>();
            if (service == null) return;

            var model = _rabbitMqConnection.CreateModel();
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

            var exchangeArgumets = new Dictionary<string, object>
            {
                { "x-delayed-type", "direct" }
            };

            var queueName = QueueNameHelper.GetQueueName(workType, _rabbitMqOptions.Value.QueuePrefix);
            var dlQueueName = QueueNameHelper.GetDLQueueName(workType, _rabbitMqOptions.Value.QueuePrefix);

            model.ExchangeDeclare(ExchangeName, ExchangeType, durable: true, arguments: exchangeArgumets);
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