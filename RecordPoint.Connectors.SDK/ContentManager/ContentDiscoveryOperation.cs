using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.R365;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using RecordPoint.Connectors.SDK.Work;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Content Discovery work item
    /// </summary>
    public class ContentDiscoveryOperation : QueueableWorkBase<BinaryMetaInfo>
    {
        public const string WORK_TYPE = "Content Discovery";
        public const int DEFAULT_DEFERRAL_SECONDS = 10;
        public const int BINARY_SUBMISSION_DELAY_SECONDS = 10;
        private readonly IConnectorConfigurationManager _connectorManager;

        private readonly IR365Client _r365Client;
        private readonly IServiceProvider _serviceProvider;
        private readonly IWorkQueueClient _workQueueClient;

        public ContentDiscoveryOperation(
            IR365Client r365Client,
            IConnectorConfigurationManager connectorManager,
            IServiceProvider serviceProvider,
            ISystemContext systemContext,
            IScopeManager scopeManager,
            ILogger<ContentDiscoveryOperation> logger,
            ITelemetryTracker telemetryTracker,
            IWorkQueueClient workQueueClient,
            IDateTimeProvider dateTimeProvider)
            : base(systemContext, scopeManager, logger, telemetryTracker, dateTimeProvider)
        {
            _r365Client = r365Client;
            _serviceProvider = serviceProvider;
            _workQueueClient = workQueueClient;
            _connectorManager = connectorManager;
        }

        public override string ServiceName => ContentManagerObservabilityExtensions.SERVICE_NAME;

        public override string WorkType => WORK_TYPE;

        public BinaryMetaInfo BinaryMetaInfo => Parameter;

        public string ConnectorConfigId => WorkRequest.ConnectorConfigId;

        public ConnectorConfigModel ConnectorConfig { get; private set; }

        protected override Dimensions GetCustomKeyDimensions()
        {
            var dimensions = new Dimensions
            {
                [nameof(BinaryMetaInfo.ExternalId)] = BinaryMetaInfo.ExternalId,
                [nameof(BinaryMetaInfo.ItemExternalId)] = BinaryMetaInfo.ItemExternalId
            };
            return dimensions;
        }

        protected override async Task InnerRunAsync(CancellationToken cancellationToken)
        {
            if (!await _connectorManager.ConnectorConfigurationExistsAsync(ConnectorConfigId, cancellationToken))
            {
                // If connector not found assume its been deleted!
                await CompleteAsync($"Connector {ConnectorConfigId} has been deleted", cancellationToken);
                return;
            }

            ConnectorConfig = await _connectorManager.GetConnectorAsync(ConnectorConfigId, cancellationToken);

            var binarySubmissionStatus = await _connectorManager.GetBinarySubmissionStatusAsync(ConnectorConfigId, cancellationToken);
            if (!binarySubmissionStatus.Enabled)
            {
                await CompleteAsync(binarySubmissionStatus.EnabledReason, cancellationToken);
                return;
            }

            var loader = CreateContentLoader();
            var getBinaryOutcome = await loader.ExecuteAsync(BinaryMetaInfo, cancellationToken).ConfigureAwait(false);
            using var binaryStream = getBinaryOutcome.Stream;

            SubmitResult submitResult = null;

            switch (getBinaryOutcome.ResultType)
            {
                case RetrieveContentResultType.Complete:
                    submitResult = await _r365Client.SubmitBinary(ConnectorConfig, BinaryMetaInfo, binaryStream, cancellationToken)
                        .ConfigureAwait(false);
                    break;

                case RetrieveContentResultType.ZeroBinary:
                    submitResult = new SubmitResult()
                    {
                        SubmitStatus = SubmitResult.Status.Skipped
                    };
                    break;

                case RetrieveContentResultType.Deleted:
                    submitResult = new SubmitResult()
                    {
                        SubmitStatus = SubmitResult.Status.Skipped
                    };
                    break;

                case RetrieveContentResultType.Failed:
                    throw new InvalidOperationException(getBinaryOutcome.Reason, getBinaryOutcome.Exception);

                default:
                    throw new InvalidOperationException(
                        $"Unexpected get binary outcome {getBinaryOutcome.ResultType}");
            }


            if (submitResult == null)
            {
                await FailAsync("No result returned by Records365", cancellationToken);
                return;
            }

            if (submitResult.SubmitStatus == SubmitResult.Status.Deferred ||
                submitResult.SubmitStatus == SubmitResult.Status.TooManyRequests)
            {
                var finalWaitUntil = submitResult.WaitUntilTime ??
                                     DateTimeProvider.UtcNow + TimeSpan.FromSeconds(DEFAULT_DEFERRAL_SECONDS);
                await _workQueueClient.SubmitBinaryAsync(new ContentSyncronisationConfiguration()
                {
                    ConnectorConfigurationId = ConnectorConfig.Id,
                    TenantId = ConnectorConfig.TenantId,
                    TenantDomainName = ConnectorConfig.TenantDomainName,
                }, BinaryMetaInfo, finalWaitUntil, cancellationToken);
            }

            await RecordOutcomeAsync(submitResult, cancellationToken);
        }

        /// <summary>
        /// Create a syncer for the current connector configuration
        /// </summary>
        /// <returns>Content syncer</returns>
        protected IBinaryRetrievalAction CreateContentLoader()
        {
            var contentSourceDiscoveryProvider = _serviceProvider.GetRequiredService<IContentManagerProvider>();
            var loader = contentSourceDiscoveryProvider.CreateBinaryRetrievalAction(ConnectorConfig);
            return loader;
        }

        /// <summary>
        /// Record the work outcome
        /// </summary>
        /// <param name="submitResult">Submission result</param>
        private Task RecordOutcomeAsync(SubmitResult submitResult, CancellationToken cancellationToken)
        {
            switch (submitResult.SubmitStatus)
            {
                case SubmitResult.Status.OK:
                    return CompleteAsync("Binary successfully submitted", cancellationToken);

                case SubmitResult.Status.Skipped:
                    // TODO: Review this behaviour
                    // TODO: Add optional completion messages
                    return CompleteAsync("Binary skipped as expected", cancellationToken);

                case SubmitResult.Status.Deferred:
                    return CompleteAsync("Item was deferred by Records365", cancellationToken);

                case SubmitResult.Status.TooManyRequests:
                    return CompleteAsync("Backoff requested by Records365", cancellationToken);

                case SubmitResult.Status.ConnectorDisabled:
                    // TODO: Provide resubmission support when implemented
                    Abandoned("The Records365 connector was disabled");
                    break;

                case SubmitResult.Status.ConnectorNotFound:
                    // TODO: Review this behavior
                    Abandoned("The connector was not found in Records365");
                    break;

                default:
                    return FailAsync($"Unexpected result [{submitResult.SubmitStatus}] received from Records365", cancellationToken);
            }

            return Task.Delay(0, cancellationToken);
        }
    }
}