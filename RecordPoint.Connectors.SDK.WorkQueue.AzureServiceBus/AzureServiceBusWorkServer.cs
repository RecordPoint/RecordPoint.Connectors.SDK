﻿using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
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
using System.Reflection;

namespace RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus
{
    /// <summary>
    /// The azure service bus work server.
    /// </summary>
    public class AzureServiceBusWorkServer : BackgroundService
    {
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
        /// The service bus options.
        /// </summary>
        private readonly IOptions<AzureServiceBusOptions> _serviceBusOptions;
        /// <summary>
        /// The scope manager.
        /// </summary>
        private readonly IScopeManager _scopeManager;
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<AzureServiceBusWorkServer> _logger;
        /// <summary>
        /// The date time provider.
        /// </summary>
        private readonly IDateTimeProvider _dateTimeProvider;
        /// <summary>
        /// The toggle provider.
        /// </summary>
        private readonly IToggleProvider _toggleProvider;
        /// <summary>
        /// The configuration.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// The service bus client.
        /// </summary>
        private readonly ServiceBusClient _serviceBusClient;
        /// <summary>
        /// The service bus processors.
        /// </summary>
        private readonly Dictionary<string, ServiceBusProcessor> _serviceBusProcessors = new();
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
        /// Operation types to be used when creating Service Bus Processors
        /// </summary>
        private readonly IList<Type> _operationTypes;

        /// <summary>
        /// Default list of Operation types to be used if none are provided
        /// </summary>
        private static readonly IList<Type> DefaultOperationTypes = new List<Type>
        {
            typeof(ContentManagerOperation),
            typeof(ChannelDiscoveryOperation),
            typeof(ContentRegistrationOperation),
            typeof(ContentSynchronisationOperation),
            typeof(RecordDisposalOperation),
            typeof(SubmitAggregationOperation),
            typeof(SubmitBinaryOperation),
            typeof(SubmitRecordOperation),
            typeof(SubmitAuditEventOperation)
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureServiceBusWorkServer"/> class.
        /// </summary>
        /// <param name="workQueueClient">The work queue client.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="systemContext">The system context.</param>
        /// <param name="workManager">The work manager.</param>
        /// <param name="serviceBusClientFactory">The service bus client factory.</param>
        /// <param name="serviceBusOptions">The service bus options.</param>
        /// <param name="scopeManager">The scope manager.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="dateTimeProvider">The date time provider.</param>
        /// <param name="toggleProvider">The toggle provider.</param>
        /// <param name="operationTypes">An optional list of operation types to create service bus processors for</param>
        /// <param name="configuration">The configuration.</param>
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
            IToggleProvider toggleProvider,
            IList<Type> operationTypes,
            IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _systemContext = systemContext;
            _workManager = workManager;
            _serviceBusOptions = serviceBusOptions;
            _logger = logger;
            _dateTimeProvider = dateTimeProvider;
            _scopeManager = scopeManager;
            _toggleProvider = toggleProvider;
            _configuration = configuration;

            _serviceId = Guid.NewGuid().ToString();
            _serviceBusClient = serviceBusClientFactory.CreateServiceBusClient();
            _workQueueClient = workQueueClient;
            _operationTypes = ValidateOperationTypes(operationTypes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
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
                    case WorkResultType.Abandoned:
                    //When we abandon work, we need to complete the message on Service Bus so it is removed from the queue
                    case WorkResultType.Complete:
                        await args.CompleteMessageAsync(args.Message);
                        break;

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
            foreach (var type in _operationTypes)
            {
                var method = typeof(AzureServiceBusWorkServer).GetMethod(nameof(TryCreateServiceBusProcessor), BindingFlags.Instance | BindingFlags.NonPublic);
                var genericMethod = method?.MakeGenericMethod(type);
                genericMethod?.Invoke(this, null);
            }
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
            where TQueueableWork : class, IQueueableWork
        {
            var service = _serviceProvider.GetService<TQueueableWork>();
            if (service == null) return;

            var queueName = GetQueueName(service.WorkType);
            var processor = _serviceBusClient.CreateProcessor(queueName, new ServiceBusProcessorOptions
            {
                AutoCompleteMessages = false,
                MaxAutoLockRenewalDuration = GetMaxAutoLockRenewalDuration(service.WorkType),
                MaxConcurrentCalls = GetMaxDegreeOfParallelism(service.WorkType)
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
                throw new ArgumentException($"At least one Operation type passed to {nameof(AzureServiceBusWorkServer)} does not implement {nameof(IQueueableWork)}");
            }

            return types;
        }

        /// <summary>
        /// Gets the Max degree of parallelism for the given work type.
        /// </summary>
        /// <param name="workType"></param>
        /// <returns></returns>
        private int GetMaxDegreeOfParallelism(string workType)
        {
            var configurationSection = _configuration.GetSection(AzureServiceBusOptions.SECTION_NAME);
            var configurationKey = $"{nameof(AzureServiceBusOptions.MaxDegreeOfParallelism)}-{workType.Replace(" ", "")}";
            return configurationSection.GetValue(configurationKey, _serviceBusOptions.Value.MaxDegreeOfParallelism);
        }

        /// <summary>
        /// Gets the Max Auto Lock Renewal Duration for the given work type.
        /// </summary>
        /// <param name="workType"></param>
        /// <returns></returns>
        private TimeSpan GetMaxAutoLockRenewalDuration(string workType)
        {
            var configurationSection = _configuration.GetSection(AzureServiceBusOptions.SECTION_NAME);
            var configurationKey = $"{nameof(AzureServiceBusOptions.MaxAutoLockRenewalDurationSeconds)}-{workType.Replace(" ", "")}";
            var configuredValue = configurationSection.GetValue(configurationKey, _serviceBusOptions.Value.MaxAutoLockRenewalDurationSeconds);
            return TimeSpan.FromSeconds(configuredValue);
        }

    }
}