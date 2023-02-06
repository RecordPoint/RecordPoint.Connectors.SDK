using Microsoft.Extensions.Logging;
using RecordPoint.Connectors.SDK.Abstractions.ContentManager;
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
    /// Binary submission Operation
    /// </summary>
    public class SubmitBinaryOperation : QueueableWorkBase<BinaryMetaInfo>
    {
        public const string WORK_TYPE = "Binary Submission";
        public const int DEFAULT_DEFERRAL_SECONDS = 10;
        public const int BINARY_SUBMISSION_DELAY_SECONDS = 10;

        private readonly IContentManagerActionProvider _contentManagerActionProvider;
        private readonly IConnectorConfigurationManager _connectorManager;
        private readonly IR365Client _r365Client;
        private readonly IWorkQueueClient _workQueueClient;

        public SubmitBinaryOperation(
            IServiceProvider serviceProvider,
            IContentManagerActionProvider contentManagerActionProvider,
            IR365Client r365Client,
            IConnectorConfigurationManager connectorManager,
            ISystemContext systemContext,
            IScopeManager scopeManager,
            ILogger<SubmitBinaryOperation> logger,
            ITelemetryTracker telemetryTracker,
            IWorkQueueClient workQueueClient,
            IDateTimeProvider dateTimeProvider)
            : base(serviceProvider, systemContext, scopeManager, logger, telemetryTracker, dateTimeProvider)
        {
            _contentManagerActionProvider = contentManagerActionProvider;
            _r365Client = r365Client;
            _workQueueClient = workQueueClient;
            _connectorManager = connectorManager;
        }

        public override string ServiceName => ContentManagerObservabilityExtensions.SERVICE_NAME;

        public override string WorkType => WORK_TYPE;

        public BinaryMetaInfo BinaryMetaInfo => Parameter;

        public string ConnectorConfigId => WorkRequest.ConnectorConfigId;

        private ConnectorConfigModel _connectorConfiguration;
        private BinaryRetrievalResult _binaryRetrievalResult;

        protected override Dimensions GetCustomKeyDimensions()
        {
            var dimensions = new Dimensions
            {
                [nameof(BinaryMetaInfo.ExternalId)] = BinaryMetaInfo.ExternalId,
                [nameof(BinaryMetaInfo.ItemExternalId)] = BinaryMetaInfo.ItemExternalId
            };
            if (!string.IsNullOrEmpty(_binaryRetrievalResult?.Reason))
            {
                dimensions.Add(StandardDimensions.ACTION_RESULT_REASON, _binaryRetrievalResult.Reason);
            }
            if (_binaryRetrievalResult?.Exception != null)
            {
                dimensions.Add(StandardDimensions.EXCEPTION, _binaryRetrievalResult.Exception.ToString());
            }
            return dimensions;
        }

        protected override async Task InnerRunAsync(CancellationToken cancellationToken)
        {
            _connectorConfiguration = await _connectorManager.GetConnectorAsync(ConnectorConfigId, cancellationToken);
            if (_connectorConfiguration == null)
            {
                // If connector not found assume its been deleted
                await CompleteAsync("Connector not found", cancellationToken);
                return;
            }

            var binarySubmissionStatus = await _connectorManager.GetBinarySubmissionStatusAsync(ConnectorConfigId, cancellationToken);
            if (!binarySubmissionStatus.Enabled)
            {
                await CompleteAsync(binarySubmissionStatus.EnabledReason, cancellationToken);
                return;
            }

            if (await CheckSemaphoreLockAsync(_connectorConfiguration, cancellationToken))
            {
                return;
            }

            var binaryRetrievalAction = CreateBinaryRetrievalAction();
            _binaryRetrievalResult = await binaryRetrievalAction
                .ExecuteAsync(_connectorConfiguration, BinaryMetaInfo, cancellationToken)
                .ConfigureAwait(false);

            using var binaryStream = _binaryRetrievalResult.Stream;

            SubmitResult submitResult = null;

            switch (_binaryRetrievalResult.ResultType)
            {
                case BinaryRetrievalResultType.Complete:
                    await InvokeSubmissionCallbackAsync(SubmissionActionType.PreSubmit, cancellationToken)
                        .ConfigureAwait(false);

                    submitResult = await _r365Client
                        .SubmitBinary(_connectorConfiguration, BinaryMetaInfo, binaryStream, cancellationToken)
                        .ConfigureAwait(false);
                    break;

                case BinaryRetrievalResultType.ZeroBinary:
                    submitResult = new SubmitResult
                    {
                        SubmitStatus = SubmitResult.Status.Skipped
                    };
                    break;

                case BinaryRetrievalResultType.Deleted:
                    submitResult = new SubmitResult
                    {
                        SubmitStatus = SubmitResult.Status.Skipped
                    };
                    break;

                case BinaryRetrievalResultType.Failed:
                    throw new InvalidOperationException(_binaryRetrievalResult.Reason, _binaryRetrievalResult.Exception);

                case BinaryRetrievalResultType.BackOff:
                    await HandleBackOffResultAsync(_connectorConfiguration, _binaryRetrievalResult.SemaphoreLockType, _binaryRetrievalResult.NextDelay, cancellationToken);
                    break;

                default:
                    throw new InvalidOperationException($"Unexpected get binary result {_binaryRetrievalResult.ResultType}");
            }


            if (submitResult == null)
            {
                await FailAsync("No result returned by Records365", cancellationToken);
                return;
            }

            if (submitResult.SubmitStatus == SubmitResult.Status.OK)
            {
                await InvokeSubmissionCallbackAsync(SubmissionActionType.PostSubmit, cancellationToken)
                    .ConfigureAwait(false);
            }
            else if (submitResult.SubmitStatus == SubmitResult.Status.Deferred ||
                submitResult.SubmitStatus == SubmitResult.Status.TooManyRequests)
            {
                var finalWaitUntil = submitResult.WaitUntilTime ??
                                     DateTimeProvider.UtcNow + TimeSpan.FromSeconds(DEFAULT_DEFERRAL_SECONDS);
                await _workQueueClient.SubmitBinaryAsync(new ContentSynchronisationConfiguration()
                {
                    ConnectorConfigurationId = _connectorConfiguration.Id,
                    TenantId = _connectorConfiguration.TenantId,
                    TenantDomainName = _connectorConfiguration.TenantDomainName,
                }, BinaryMetaInfo, finalWaitUntil, cancellationToken);
            }

            await RecordOutcomeAsync(submitResult, cancellationToken);
        }

        private IBinaryRetrievalAction CreateBinaryRetrievalAction() => _contentManagerActionProvider.CreateBinaryRetrievalAction();
        private IBinarySubmissionCallbackAction CreateBinarySubmissionCallbackAction() => _contentManagerActionProvider.CreateBinarySubmissionCallbackAction();

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
                    return CompleteAsync("Binary skipped as expected", cancellationToken);

                case SubmitResult.Status.Deferred:
                    return CompleteAsync("Item was deferred by Records365", cancellationToken);

                case SubmitResult.Status.TooManyRequests:
                    return CompleteAsync("Backoff requested by Records365", cancellationToken);

                case SubmitResult.Status.ConnectorDisabled:
                    Abandoned("The Records365 connector was disabled");
                    break;

                case SubmitResult.Status.ConnectorNotFound:
                    Abandoned("The connector was not found in Records365");
                    break;

                default:
                    return FailAsync($"Unexpected result [{submitResult.SubmitStatus}] received from Records365", cancellationToken);
            }

            return Task.Delay(0, cancellationToken);
        }

        private async Task InvokeSubmissionCallbackAsync(SubmissionActionType submissionActionType, CancellationToken cancellationToken)
        {
            var binarySubmissionCallbackAction = CreateBinarySubmissionCallbackAction();

            //If no callback action has been registered, just bail out now
            if (binarySubmissionCallbackAction == null)
                return;

            await binarySubmissionCallbackAction
                .ExecuteAsync(_connectorConfiguration, BinaryMetaInfo, submissionActionType, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}