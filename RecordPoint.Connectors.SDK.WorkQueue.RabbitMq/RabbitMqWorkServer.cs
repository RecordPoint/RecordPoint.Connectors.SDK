using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
using RecordPoint.Connectors.SDK.Abstractions.Work;

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
        private readonly IObservabilityScope _observabilityScope;
        /// <summary>
        /// The telemetry tracker.
        /// </summary>
        private readonly ITelemetryTracker _telemetryTracker;
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
        /// Processing token.
        /// </summary>
        private readonly CancellationTokenSource _processingToken = new();

        /// <summary>
        /// Operation types to be used when creating Service Bus Processors
        /// </summary>
        private readonly IList<Type> _operationTypes;

        /// <summary>
        /// Default list of Operation types to be used if none are provided
        /// </summary>
        private static readonly IList<Type> DefaultOperationTypes = new List<Type>
        {
            typeof(ChannelDiscoveryOperation),
            typeof(ContentRegistrationOperation),
            typeof(ContentSynchronisationOperation),
            typeof(RecordDisposalOperation),
            typeof(SubmitAggregationOperation),
            typeof(SubmitBinaryOperation),
            typeof(SubmitRecordOperation),
            typeof(SubmitAuditEventOperation),
            typeof(SynchronousSubmitRecordOperation)
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitMqWorkServer"/> class.
        /// </summary>
        /// <param name="workQueueClient">The work queue client.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="systemContext">The system context.</param>
        /// <param name="workManager">The work manager.</param>
        /// <param name="rabbitMqClientFactory">The rabbit mq client factory.</param>
        /// <param name="rabbitMqOptions">The rabbit mq options.</param>
        /// <param name="observabilityScope">The scope manager.</param>
        /// <param name="telemetryTracker">The telemetry tracker.</param>
        /// <param name="toggleProvider">The toggle provider.</param>
        /// <param name="operationTypes">An optional list of operation types to create RabbitMq consumers for</param>
        public RabbitMqWorkServer(
            IWorkQueueClient workQueueClient,
            IServiceProvider serviceProvider,
            ISystemContext systemContext,
            IQueueableWorkManager workManager,
            IRabbitMqClientFactory rabbitMqClientFactory,
            IOptions<RabbitMqOptions> rabbitMqOptions,
            IObservabilityScope observabilityScope,
            ITelemetryTracker telemetryTracker,
            IToggleProvider toggleProvider,
            IList<Type>? operationTypes = null)
        {
            _serviceProvider = serviceProvider;
            _systemContext = systemContext;
            _workManager = workManager;
            _rabbitMqOptions = rabbitMqOptions;
            _telemetryTracker = telemetryTracker;
            _observabilityScope = observabilityScope;
            _toggleProvider = toggleProvider;

            _rabbitMqConnection = rabbitMqClientFactory.CreateRabbitMqConnection();
            _workQueueClient = workQueueClient;

            _operationTypes = ValidateOperationTypes(operationTypes ?? []);
        }

        /// <summary>
        /// Stops all RabbitMQ consumers and closes open RabbitMQ resources.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for shutdown operations.</param>
        /// <returns>A task that completes when shutdown finishes.</returns>
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (var model in _rabbitMqProcessors.Values.Select(processor => processor.RabbitMqModel))
            {
                if (model.IsOpen)
                {
                    await model.CloseAsync(cancellationToken: cancellationToken);
                }

                model.Dispose();
            }

            if (_rabbitMqConnection.IsOpen)
            {
                await _rabbitMqConnection.CloseAsync(cancellationToken: cancellationToken);
            }

            await base.StopAsync(cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var systemScope = _observabilityScope.BeginSystemScope(_systemContext);

            var startupDimensions = new Dimensions()
            {
                [StandardDimensions.EVENT_TYPE] = EventType.Startup.ToString()
            };
            _telemetryTracker.TrackTrace("Connecting to RabbitMq", SeverityLevel.Information, startupDimensions);

            try
            {
                if (_rabbitMqOptions == null)
                {
                    _telemetryTracker.TrackTrace("RabbitMqSettings not found", SeverityLevel.Critical);
                    return;
                }

                _telemetryTracker.TrackTrace("RabbitMqSettings found", SeverityLevel.Information);

                await CreateProcessorsAsync();

                var isProcessingStarted = false;
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var isKillSwitched = _toggleProvider.GetConnectorKillswitch(_systemContext);
                        if (isKillSwitched && isProcessingStarted)
                        {
                            //If the kill switch has just been enabled, lets stop the processors
                            await StopConsumersAsync();
                            isProcessingStarted = false;
                        }
                        else if (!isProcessingStarted && !isKillSwitched)
                        {
                            //If processing is stopped, but the kill switch is not enabled, lets start the processors
                            isProcessingStarted = true;
                            await StartConsumersAsync();
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
                    await StopConsumersAsync();
                }

                //Delay to allow currently processing operations to complete
                await Task.Delay(_rabbitMqOptions.Value.ServiceShutdownDelay, CancellationToken.None);
                await _processingToken.CancelAsync();

                await CloseModelsAsync();
            }
            catch (Exception ex)
            {
                _telemetryTracker.TrackException(ex);
            }

            var shutdownDimensions = new Dimensions()
            {
                [StandardDimensions.EVENT_TYPE] = EventType.Shutdown.ToString()
            };
            _telemetryTracker.TrackTrace("Consumer stopped", SeverityLevel.Information, shutdownDimensions);
        }

        /// <summary>
        /// Basic IDisposable implementation.
        /// </summary>
        public override void Dispose()
        {
            _processingToken.Dispose();
            base.Dispose();
        }

        private async Task HandleIncomingMessagesAsync(object sender, BasicDeliverEventArgs args)
        {
            var json = Encoding.Default.GetString(args.Body.Span);
            var workRequest = JsonConvert.DeserializeObject<WorkRequest>(json);
            var eventingBasicConsumer = (AsyncEventingBasicConsumer)sender;
            if (workRequest == null) return;

            var messageHandled = false;
            try
            {
                var result = await _workManager.HandleWorkRequestAsync(workRequest, _processingToken.Token).ConfigureAwait(false);

                switch (result.ResultType)
                {
                    //When we abandon work, we need to complete the message on queue so it is removed from the queue
                    case WorkResultType.Abandoned:
                    case WorkResultType.Complete:
                        await eventingBasicConsumer.Channel.BasicAckAsync(args.DeliveryTag, false, _processingToken.Token);
                        messageHandled = true;
                        break;

                    case WorkResultType.DeadLetter:
                    case WorkResultType.Failed:
                        //Move the message to the Dead Letter Queue
                        await eventingBasicConsumer.Channel.BasicNackAsync(args.DeliveryTag, false, false, _processingToken.Token);
                        messageHandled = true;
                        throw new WorkResultException(result.Reason, result.Exception);

                    case WorkResultType.Deferred:
                        try
                        {
                            await DeferMessageAsync(workRequest, result.WaitTill, _processingToken.Token);
                            // Only acknowledge the original message after successful re-queue
                            await eventingBasicConsumer.Channel.BasicAckAsync(args.DeliveryTag, false, _processingToken.Token);
                            messageHandled = true;
                        }
                        catch (Exception deferException)
                        {
                            // If re-queue fails, nack without requeue to send to dead letter
                            // The work was successfully processed (result was Deferred),
                            // but we failed to re-enqueue it for later processing
                            var deferErrorDimensions = new Dimensions
                            {
                                { "ConnectorId", workRequest.ConnectorConfigId },
                                { "TenantId", workRequest.TenantId },
                                { "TenantDomainName", workRequest.TenantDomainName },
                                { "Work", workRequest.WorkType },
                                { "WorkId", workRequest.WorkId },
                            };
                            _telemetryTracker.TrackException(new DeferredMessageRequeueFailedException("Failed to requeue deferred message", deferException), deferErrorDimensions);
                            await eventingBasicConsumer.Channel.BasicNackAsync(args.DeliveryTag, false, false, _processingToken.Token);
                            messageHandled = true;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                var workRequestException = new UnknownWorkRequestException(ex);
                var dimensions = new Dimensions
                {
                    { "ConnectorId", workRequest.ConnectorConfigId },
                    { "TenantId", workRequest.TenantId },
                    { "TenantDomainName", workRequest.TenantDomainName },
                    { "Work", workRequest.WorkType },
                    { "WorkId", workRequest.WorkId },
                };
                _telemetryTracker.TrackException(workRequestException, dimensions);

                // Nack and let RabbitMQ retry; will eventually reach dead letter after max retries
                if (!messageHandled)
                {
                    try
                    {
                        await eventingBasicConsumer.Channel.BasicNackAsync(args.DeliveryTag, false, true, _processingToken.Token);
                    }
                    catch (Exception nackEx)
                    {
                        _telemetryTracker.TrackException(nackEx);
                    }
                }
            }
        }

        private async Task DeferMessageAsync(WorkRequest request, DateTimeOffset? resultWaitTill, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var offsetWaitTill = (resultWaitTill == null || resultWaitTill < DateTimeOffset.UtcNow)
                ? DateTimeOffset.UtcNow.AddSeconds(20)
                : resultWaitTill.Value;

            request.WaitTill = offsetWaitTill;

            await _workQueueClient.SubmitWorkAsync(request, cancellationToken);
        }

        private async Task CreateProcessorsAsync()
        {
            var queueableWorkOperations = _serviceProvider.GetServices<IQueueableWork>();
            foreach (var type in _operationTypes)
            {
                var queueableWorkOperation = queueableWorkOperations.FirstOrDefault(queueableWorkOperation => queueableWorkOperation.GetType() == type);
                if (queueableWorkOperation == null)
                    continue;

                await TryCreateRabbitMqConsumerAsync(queueableWorkOperation.WorkType);
            }
        }

        private async Task StartConsumersAsync()
        {
            foreach (var queueProcessor in _rabbitMqProcessors)
            {
                queueProcessor.Value.ConsumerTag = await queueProcessor.Value.RabbitMqModel.BasicConsumeAsync(queueProcessor.Key, false, queueProcessor.Value.RabbitMqEventingBasicConsumer, _processingToken.Token);
            }
        }

        private async Task StopConsumersAsync()
        {
            var filteredProcessors = _rabbitMqProcessors.Where(queueProcessor => queueProcessor.Value.RabbitMqModel.IsOpen && !string.IsNullOrEmpty(queueProcessor.Value.ConsumerTag));
            foreach (var queueProcessorValue in filteredProcessors.Select(queueProcessor => queueProcessor.Value))
            {
                await queueProcessorValue.RabbitMqModel.BasicCancelAsync(queueProcessorValue.ConsumerTag!, false, _processingToken.Token);
                queueProcessorValue.ConsumerTag = null;
            }
        }

        private async Task CloseModelsAsync()
        {
            foreach (var processModel in _rabbitMqProcessors.Select(a => a.Value)
                .Where(a => !a.RabbitMqModel.IsClosed))
            {
                await processModel.RabbitMqModel.CloseAsync(cancellationToken: CancellationToken.None);
            }
        }

        private async Task TryCreateRabbitMqConsumerAsync(string workType)
        {
            var model = await _rabbitMqConnection.CreateChannelAsync(cancellationToken: _processingToken.Token);

            // Set prefetch count if required
            var dop = _rabbitMqOptions.Value.MaxDegreeOfParallelism;
            if (dop != null)
            {
                await model.BasicQosAsync(0, dop.Value, false, _processingToken.Token);
            }

            var processor = new RabbitMqProcessModel
            {
                RabbitMqModel = model,
                RabbitMqEventingBasicConsumer = await DeclareAndBindQueueToExchangeAsync(model, workType)
            };
            // add handler to process messages
            processor.RabbitMqEventingBasicConsumer.ReceivedAsync += HandleIncomingMessagesAsync;

            //Add Processor to internal Cache
            var queueName = QueueNameHelper.GetQueueName(workType, _rabbitMqOptions.Value.QueuePrefix);
            _rabbitMqProcessors.Add(queueName, processor);
        }

        private async Task<AsyncEventingBasicConsumer> DeclareAndBindQueueToExchangeAsync(IChannel model, string workType)
        {
            //Create Normal Queue
            var dlExchangeArguments = new Dictionary<string, object?>
            {
                { DeadletterExchangeType, DeadletterExchange}
            };

            var exchangeArguments = new Dictionary<string, object?>
            {
                { "x-delayed-type", "direct" }
            };

            var queueName = QueueNameHelper.GetQueueName(workType, _rabbitMqOptions.Value.QueuePrefix);
            var dlQueueName = QueueNameHelper.GetDLQueueName(workType, _rabbitMqOptions.Value.QueuePrefix);

            await model.ExchangeDeclareAsync(ExchangeName, ExchangeType, durable: true, arguments: exchangeArguments, cancellationToken: _processingToken.Token);
            await model.QueueDeclareAsync(queueName, true, false, false, dlExchangeArguments, cancellationToken: _processingToken.Token);
            await model.QueueBindAsync(queueName, ExchangeName, dlQueueName, cancellationToken: _processingToken.Token);
            var consumer = new AsyncEventingBasicConsumer(model);

            //Create Dead Letter Queue            
            await model.ExchangeDeclareAsync(DeadletterExchange, RabbitMQ.Client.ExchangeType.Direct, durable: true, autoDelete: false, cancellationToken: _processingToken.Token);
            await model.QueueDeclareAsync(dlQueueName, true, false, false, dlExchangeArguments, cancellationToken: _processingToken.Token);
            await model.QueueBindAsync(dlQueueName, DeadletterExchange, dlQueueName, cancellationToken: _processingToken.Token);

            return consumer;
        }

        /// <summary>
        /// Ensures a default list of types is used if the provided list is empty.
        /// Also validates that the List of types provided all implement IQueueableWork,
        /// throws if it does not.
        /// </summary>
        /// <param name="types">List of types to validate</param>
        /// <returns>List of types of IQueueableWork</returns>
        /// <exception cref="ArgumentException"></exception>
        private static IList<Type> ValidateOperationTypes(IList<Type> types)
        {
            if (!types.Any())
            {
                return DefaultOperationTypes;
            }

            if (types.Any(type => !typeof(IQueueableWork).IsAssignableFrom(type)))
            {
                throw new ArgumentException($"At least one Operation type passed to {nameof(RabbitMqWorkServer)} does not implement {nameof(IQueueableWork)}");
            }

            return types;
        }
    }
}