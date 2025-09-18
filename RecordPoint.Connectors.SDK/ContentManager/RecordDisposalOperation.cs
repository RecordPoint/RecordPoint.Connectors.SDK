using Microsoft.Extensions.DependencyInjection;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// The record disposal operation.
    /// </summary>
    public class RecordDisposalOperation : QueueableWorkBase<Record>
    {
        /// <summary>
        /// WORK TYPE.
        /// </summary>
        public const string WORK_TYPE = "Record Disposal";
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
        /// Initializes a new instance of the <see cref="RecordDisposalOperation"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="contentManagerActionProvider">The content manager action provider.</param>
        /// <param name="connectorManager">The connector manager.</param>
        /// <param name="systemContext">The system context.</param>
        /// <param name="observabilityScope">The scope manager.</param>
        /// <param name="telemetryTracker">The telemetry tracker.</param>
        /// <param name="dateTimeProvider">The date time provider.</param>
        public RecordDisposalOperation(
            IServiceProvider serviceProvider,
            IContentManagerActionProvider contentManagerActionProvider,
            IConnectorConfigurationManager connectorManager,
            ISystemContext systemContext,
            IObservabilityScope observabilityScope,
            ITelemetryTracker telemetryTracker,
            IDateTimeProvider dateTimeProvider)
            : base(serviceProvider, systemContext, observabilityScope, telemetryTracker, dateTimeProvider)
        {
            _contentManagerActionProvider = contentManagerActionProvider;
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
        /// Gets the record.
        /// </summary>
        public Record Record => Parameter;

        /// <summary>
        /// Gets the connector config id.
        /// </summary>
        public string ConnectorConfigId => WorkRequest.ConnectorConfigId;

        /// <summary>
        /// The connector configuration.
        /// </summary>
        private ConnectorConfigModel _connectorConfiguration;
        /// <summary>
        /// The record disposal result.
        /// </summary>
        private RecordDisposalResult _recordDisposalResult;

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

            var connectorStatus = await _connectorManager.GetConnectorStatusAsync(ConnectorConfigId, cancellationToken);
            if (!connectorStatus.Enabled)
            {
                await CompleteAsync(connectorStatus.EnabledReason, cancellationToken);
                return;
            }

            if (await CheckSemaphoreLockAsync(_connectorConfiguration, Record, cancellationToken))
            {
                return;
            }

            using var scope = _serviceProvider.CreateScope();
            var recordDisposalAction = _contentManagerActionProvider.CreateRecordDisposalAction(scope);
            _recordDisposalResult = await recordDisposalAction
                .ExecuteAsync(_connectorConfiguration, Record, cancellationToken)
                .ConfigureAwait(false);

            switch (_recordDisposalResult.ResultType)
            {
                case RecordDisposalResultType.Complete:
                    await CompleteAsync("Record successfully disposed", cancellationToken);
                    break;

                case RecordDisposalResultType.Deleted:
                    await CompleteAsync("Record already deleted on content source", cancellationToken);
                    break;

                case RecordDisposalResultType.Failed:
                    throw new InvalidOperationException(_recordDisposalResult.Reason, _recordDisposalResult.Exception);

                case RecordDisposalResultType.BackOff:
                    await HandleBackOffResultAsync(_connectorConfiguration, Record, _recordDisposalResult.SemaphoreLockType, _recordDisposalResult.NextDelay, cancellationToken);
                    break;

                default:
                    throw new InvalidOperationException($"Unexpected record disposal result {_recordDisposalResult.ResultType}");
            }
        }

        #region Observability
        /// <summary>
        /// Get custom key dimensions.
        /// </summary>
        /// <returns>A Dimensions</returns>
        protected override Dimensions GetCustomKeyDimensions()
        {
            var dimensions = new Dimensions
            {
                [nameof(Record.ExternalId)] = Record.ExternalId,
            };
            return dimensions;
        }

        /// <summary>
        /// Get custom result dimensions.
        /// </summary>
        /// <returns>A Dimensions</returns>
        protected override Dimensions GetCustomResultDimensions()
        {
            var dimensions = base.GetCustomResultDimensions();
            dimensions[StandardDimensions.WORK_COMPLETE] = _recordDisposalResult?.ResultType.ToString() ?? "Unknown";
            if (!string.IsNullOrEmpty(_recordDisposalResult?.Reason))
            {
                dimensions.Add(StandardDimensions.ACTION_RESULT_REASON, _recordDisposalResult.Reason);
            }
            if (_recordDisposalResult?.Exception != null)
            {
                dimensions[StandardDimensions.EXCEPTION] = _recordDisposalResult.Exception.ToString();
            }

            if (_recordDisposalResult?.Dimensions != null)
            {
                foreach (var dimension in _recordDisposalResult.Dimensions)
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

            if (_recordDisposalResult?.Measures != null)
            {
                foreach (var measure in _recordDisposalResult.Measures)
                {
                    measures[measure.Key] = measure.Value;
                }
            }

            return measures;
        }
        #endregion

        #region Disposable
        /// <summary>
        /// Dispose invocation results
        /// </summary>
        protected override void InnerDispose()
        {
            _connectorConfiguration = null;
            _recordDisposalResult = null;
        }
        #endregion
    }
}