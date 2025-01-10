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
    /// The submit binary operation.
    /// </summary>
    public class SubmitBinaryOperation : QueueableWorkBase<BinaryMetaInfo>
    {
        /// <summary>
        /// WORK TYPE.
        /// </summary>
        public const string WORK_TYPE = "Binary Submission";
        /// <summary>
        /// The DEFAULT DEFERRAL SECONDS.
        /// </summary>
        public const int DEFAULT_DEFERRAL_SECONDS = 10;
        /// <summary>
        /// The BINARY SUBMISSION DELAY SECONDS.
        /// </summary>
        public const int BINARY_SUBMISSION_DELAY_SECONDS = 10;

        /// <summary>
        /// The content manager action provider.
        /// </summary>
        private readonly IContentManagerActionProvider _contentManagerActionProvider;
        /// <summary>
        /// The connector manager.
        /// </summary>
        private readonly IConnectorConfigurationManager _connectorManager;
        /// <summary>
        /// The r365 client.
        /// </summary>
        private readonly IR365Client _r365Client;
        /// <summary>
        /// Work queue client.
        /// </summary>
        private readonly IWorkQueueClient _workQueueClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubmitBinaryOperation"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="contentManagerActionProvider">The content manager action provider.</param>
        /// <param name="r365Client">The r365 client.</param>
        /// <param name="connectorManager">The connector manager.</param>
        /// <param name="systemContext">The system context.</param>
        /// <param name="scopeManager">The scope manager.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="telemetryTracker">The telemetry tracker.</param>
        /// <param name="workQueueClient">The work queue client.</param>
        /// <param name="dateTimeProvider">The date time provider.</param>
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

        /// <summary>
        /// Gets the service name.
        /// </summary>
        public override string ServiceName => ContentManagerObservabilityExtensions.SERVICE_NAME;

        /// <summary>
        /// Gets the work type.
        /// </summary>
        public override string WorkType => WORK_TYPE;

        /// <summary>
        /// Gets the binary meta info.
        /// </summary>
        public BinaryMetaInfo BinaryMetaInfo => Parameter;

        /// <summary>
        /// Gets the connector config id.
        /// </summary>
        public string ConnectorConfigId => WorkRequest.ConnectorConfigId;

        /// <summary>
        /// The connector configuration.
        /// </summary>
        private ConnectorConfigModel _connectorConfiguration;
        /// <summary>
        /// The binary retrieval result.
        /// </summary>
        private BinaryRetrievalResult _binaryRetrievalResult;
        /// <summary>
        /// How long it took to for the operation to execute
        /// </summary>
        private TimeSpan? _actionExecutionTimespan;
        /// <summary>
        /// How long it took to submit the work to the queue
        /// </summary>
        private TimeSpan? _submitTimespan;

        /// <summary>
        /// Get custom key dimensions.
        /// </summary>
        /// <returns>A Dimensions</returns>
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
            if (_binaryRetrievalResult?.Dimensions != null)
            {
                foreach (var dimension in _binaryRetrievalResult.Dimensions)
                {
                    dimensions[dimension.Key] = dimension.Value;
                }
            }
            return dimensions;
        }

        /// <summary>
        /// Get custom result measures.
        /// </summary>
        /// <returns>A Measures</returns>
        protected override Measures GetCustomResultMeasures()
        {
            var measures = base.GetCustomResultMeasures();
            if (_actionExecutionTimespan.HasValue)
                measures[StandardMeasures.ACTION_EXECUTION_SECONDS] = _actionExecutionTimespan.Value.Milliseconds / 1000D;
            if (_submitTimespan.HasValue)
                measures[StandardMeasures.SUBMIT_SECONDS] = _submitTimespan.Value.Milliseconds / 1000D;

            if (_binaryRetrievalResult?.Measures != null)
            {
                foreach (var measure in _binaryRetrievalResult.Measures)
                {
                    measures[measure.Key] = measure.Value;
                }
            }

            return measures;
        }

        /// <summary>
        /// Inner the run asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns>A Task</returns>
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

            if (await CheckSemaphoreLockAsync(_connectorConfiguration, BinaryMetaInfo, cancellationToken))
            {
                return;
            }

            //Invoke retrieval action to obtain binary stream
            var startTime = DateTimeProvider.UtcNow;

            var binaryRetrievalAction = CreateBinaryRetrievalAction();
            _binaryRetrievalResult = await binaryRetrievalAction
                .ExecuteAsync(_connectorConfiguration, BinaryMetaInfo, cancellationToken)
                .ConfigureAwait(false);

            _actionExecutionTimespan = DateTimeProvider.UtcNow - startTime;


            SubmitResult submitResult = null;

            switch (_binaryRetrievalResult.ResultType)
            {
                case BinaryRetrievalResultType.Complete:
                    await InvokeSubmissionCallbackAsync(SubmissionActionType.PreSubmit, cancellationToken)
                        .ConfigureAwait(false);

                    var submissionStartTime = DateTimeProvider.UtcNow;
                    submitResult = await _r365Client
                        .SubmitBinary(_connectorConfiguration, BinaryMetaInfo, _binaryRetrievalResult.Stream, cancellationToken)
                        .ConfigureAwait(false);
                    _submitTimespan = DateTimeProvider.UtcNow - submissionStartTime;
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
                    await HandleBackOffResultAsync(_connectorConfiguration, BinaryMetaInfo, _binaryRetrievalResult.SemaphoreLockType, _binaryRetrievalResult.NextDelay, cancellationToken);
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

        /// <summary>
        /// Dispose binary stream
        /// </summary>
        protected override void InnerDispose()
        {
            _binaryRetrievalResult?.Stream?.Dispose();
        }

        /// <summary>
        /// Creates binary retrieval action.
        /// </summary>
        /// <returns>An IBinaryRetrievalAction</returns>
        private IBinaryRetrievalAction CreateBinaryRetrievalAction() => _contentManagerActionProvider.CreateBinaryRetrievalAction();
        /// <summary>
        /// Creates binary submission callback action.
        /// </summary>
        /// <returns>An IBinarySubmissionCallbackAction</returns>
        private IBinarySubmissionCallbackAction CreateBinarySubmissionCallbackAction() => _contentManagerActionProvider.CreateBinarySubmissionCallbackAction();

        /// <summary>
        /// Record the work outcome
        /// </summary>
        /// <param name="submitResult">Submission result</param>
        /// <param name="cancellationToken">Cancellation token</param>
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

        /// <summary>
        /// Invokes submission callback asynchronously.
        /// </summary>
        /// <param name="submissionActionType">The submission action type.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task</returns>
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