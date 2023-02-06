using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Toggles;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus
{
    public class AzureServiceBusWorkServer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ISystemContext _systemContext;
        private readonly IQueueableWorkManager _workManager;
        private readonly IOptions<AzureServiceBusOptions> _serviceBusOptions;
        private readonly IScopeManager _scopeManager;
        private readonly ILogger<AzureServiceBusWorkServer> _logger;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IToggleProvider _toggleProvider;


        private readonly ServiceBusClient _serviceBusClient;
        private readonly Dictionary<string, ServiceBusProcessor> _serviceBusProcessors = new();
        private readonly IWorkQueueClient _workQueueClient;
        private readonly string _serviceId;

        private CancellationTokenSource _processingToken = new();

        public AzureServiceBusWorkServer(
            IWorkQueueClient workQueueClient,
            IServiceProvider serviceProvider,
            ISystemContext systemContext,
            IQueueableWorkManager workManager,
            IServiceBusClientFactory serviceBusClientFactory,
            IOptions<AzureServiceBusOptions> serviceBusOptions,
            IScopeManager scopeManager,
            ILogger<AzureServiceBusWorkServer> logger,
            IDateTimeProvider dateTimeProvider,
            IToggleProvider toggleProvider)
        {
            _serviceProvider = serviceProvider;
            _systemContext = systemContext;
            _workManager = workManager;
            _serviceBusOptions = serviceBusOptions;
            _logger = logger;
            _dateTimeProvider = dateTimeProvider;
            _scopeManager = scopeManager;
            _toggleProvider = toggleProvider;

            _serviceId = Guid.NewGuid().ToString();
            _serviceBusClient = serviceBusClientFactory.CreateServiceBusClient();
            _workQueueClient = workQueueClient;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (var processor in _serviceBusProcessors.Values)
            {
                await processor.StopProcessingAsync(cancellationToken);
                await processor.DisposeAsync();
            }

            await _serviceBusClient.DisposeAsync();

            await base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var systemScope = _scopeManager.BeginSystemScope(_systemContext);
            using var serviceScope = _scopeManager.BeginServiceScope(AzureServiceBusDimensions.SERVICE_NAME, _serviceId);

            var startupDimensions = new Dimensions()
            {
                [StandardDimensions.EVENT_TYPE] = EventType.Startup.ToString()
            };
            _logger.LogEvent("Connecting to ASB", startupDimensions);

            try
            {
                if (_serviceBusOptions == null)
                {
                    _logger.LogEvent("AzureServiceBusSettings not found");
                    return;
                }
                else
                {
                    _logger.LogEvent("AzureServiceBusSettings found");
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
                            await StopProcessorsAsync(_processingToken.Token);
                            isProcessingStarted = false;
                        }
                        else if (!isProcessingStarted && !isKillSwitched)
                        {
                            //If processing is stopped, but the kill switch is not enabled, lets start the processors
                            isProcessingStarted = true;
                            await StartProcessorsAsync(_processingToken.Token);
                        }

                        await Task.Delay(_serviceBusOptions.Value.KillswitchCheckInterval, stoppingToken);
                    }
                    catch (OperationCanceledException)
                    {
                    }
                }

                //If the background host is stopping, lets gracefully shutdown the processors
                if (isProcessingStarted)
                {
                    await StopProcessorsAsync(CancellationToken.None);
                }

                //Delay to allow currently processing operations to complete
                await Task.Delay(_serviceBusOptions.Value.ServiceShutdownDelay, CancellationToken.None);
                _processingToken.Cancel();

                await CloseProcessorsAsync(CancellationToken.None);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception thrown");
            }

            var shutdownDimensions = new Dimensions()
            {
                [StandardDimensions.EVENT_TYPE] = EventType.Shutdown.ToString()
            };
            _logger.LogEvent("ASB stopped", shutdownDimensions);
        }

        private Task HandleErrors(ProcessErrorEventArgs args)
        {
            _logger.LogError(args.Exception, "Exception thrown from HandleErrorsAsync");
            return Task.CompletedTask;
        }

        private async Task HandleIncomingMessagesAsync(ProcessMessageEventArgs args)
        {
            var json = args.Message.Body.ToString();
            var workRequest = JsonConvert.DeserializeObject<WorkRequest>(json);
            if (workRequest == null) return;

            try
            {
                var result = await _workManager.HandleWorkRequestAsync(workRequest, _processingToken.Token).ConfigureAwait(false);

                workRequest.MustFinishDateTime = _dateTimeProvider.UtcNow + TimeSpan.FromMinutes(1);

                switch (result.ResultType)
                {
                    case WorkResultType.Complete:
                        await args.CompleteMessageAsync(args.Message);
                        break;

                    case WorkResultType.Abandoned:
                        //When we abandon work, we need to complete the message on Service Bus so it is removed from the queue
                        await args.CompleteMessageAsync(args.Message);
                        throw new WorkResultException(result.Reason);

                    case WorkResultType.DeadLetter:
                        //Move the message to the Dead Letter Queue
                        await args.DeadLetterMessageAsync(args.Message);
                        throw new WorkResultException(result.Reason);

                    case WorkResultType.Deferred:
                        await args.CompleteMessageAsync(args.Message);
                        await DeferMessageAsync(workRequest, args.CancellationToken);
                        break;

                    case WorkResultType.Failed:
                        //Allow Service Bus to handle retries based on the Maximum Delivery Count setting on the Queue
                        await args.AbandonMessageAsync(args.Message);
                        throw new WorkResultException(result.Reason);
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
            TryCreateServiceBusProcessor<ContentManagerOperation>();
            TryCreateServiceBusProcessor<ChannelDiscoveryOperation>();
            TryCreateServiceBusProcessor<ContentRegistrationOperation>();
            TryCreateServiceBusProcessor<ContentSynchronisationOperation>();
            TryCreateServiceBusProcessor<RecordDisposalOperation>();
            TryCreateServiceBusProcessor<SubmitAggregationOperation>();
            TryCreateServiceBusProcessor<SubmitBinaryOperation>();
            TryCreateServiceBusProcessor<SubmitRecordOperation>();
            TryCreateServiceBusProcessor<SubmitAuditEventOperation>();
        }

        private async Task StartProcessorsAsync(CancellationToken cancellationToken)
        {
            var tasks = new List<Task>();
            foreach (var queueProcessor in _serviceBusProcessors)
            {
                tasks.Add(queueProcessor.Value.StartProcessingAsync(cancellationToken));
            }
            await Task.WhenAll(tasks);
        }

        private async Task StopProcessorsAsync(CancellationToken cancellationToken)
        {
            var tasks = new List<Task>();
            foreach (var queueProcessor in _serviceBusProcessors.Where(a => a.Value.IsProcessing))
            {
                tasks.Add(queueProcessor.Value.StopProcessingAsync(cancellationToken));
            }
            await Task.WhenAll(tasks);
        }

        private async Task CloseProcessorsAsync(CancellationToken cancellationToken)
        {
            var tasks = new List<Task>();
            foreach (var queueProcessor in _serviceBusProcessors.Where(a => !a.Value.IsClosed))
            {
                tasks.Add(queueProcessor.Value.CloseAsync(cancellationToken));
            }
            await Task.WhenAll(tasks);
        }

        private void TryCreateServiceBusProcessor<TQueueableWork>()
            where TQueueableWork : IQueueableWork
        {
            var service = _serviceProvider.GetService<TQueueableWork>();
            if (service == null) return;

            var queueName = GetQueueName(service.WorkType);
            var processor = _serviceBusClient.CreateProcessor(queueName, new ServiceBusProcessorOptions
            {
                AutoCompleteMessages = false,
                MaxAutoLockRenewalDuration = TimeSpan.FromMinutes(5),
                MaxConcurrentCalls = _serviceBusOptions.Value.MaxDegreeOfParallelism
            });
            _serviceBusProcessors.Add(queueName, processor);

            // add handler to process messages
            processor.ProcessMessageAsync += HandleIncomingMessagesAsync;

            // add handler to process any errors
            processor.ProcessErrorAsync += HandleErrors;
        }

        private string GetQueueName(string workType) => string.IsNullOrEmpty(_serviceBusOptions.Value.QueuePrefix)
            ? $"{workType.Replace(" ", "-").ToLower()}"
            : $"{_serviceBusOptions.Value.QueuePrefix}-{workType.Replace(" ", "-").ToLower()}";
    }
}