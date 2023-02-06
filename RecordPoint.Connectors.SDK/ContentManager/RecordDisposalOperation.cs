using Microsoft.Extensions.Logging;
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
    /// Binary submission work item
    /// </summary>
    public class RecordDisposalOperation : QueueableWorkBase<Record>
    {
        public const string WORK_TYPE = "Record Disposal";
        public const int DEFAULT_DEFERRAL_SECONDS = 10;
        public const int BINARY_SUBMISSION_DELAY_SECONDS = 10;

        private readonly IContentManagerActionProvider _contentManagerActionProvider;
        private readonly IConnectorConfigurationManager _connectorManager;
        public RecordDisposalOperation(
            IServiceProvider serviceProvider,
            IContentManagerActionProvider contentManagerActionProvider,
            IConnectorConfigurationManager connectorManager,
            ISystemContext systemContext,
            IScopeManager scopeManager,
            ILogger<RecordDisposalOperation> logger,
            ITelemetryTracker telemetryTracker,
            IDateTimeProvider dateTimeProvider)
            : base(serviceProvider, systemContext, scopeManager, logger, telemetryTracker, dateTimeProvider)
        {
            _contentManagerActionProvider = contentManagerActionProvider;
            _connectorManager = connectorManager;
        }

        public override string ServiceName => ContentManagerObservabilityExtensions.SERVICE_NAME;

        public override string WorkType => WORK_TYPE;

        public Record Record => Parameter;

        public string ConnectorConfigId => WorkRequest.ConnectorConfigId;

        private ConnectorConfigModel _connectorConfiguration;
        private RecordDisposalResult _recordDisposalResult;

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

            if (await CheckSemaphoreLockAsync(_connectorConfiguration, cancellationToken))
            {
                return;
            }

            var recordDisposalAction = CreateRecordDisposalAction();
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
                    await HandleBackOffResultAsync(_connectorConfiguration, _recordDisposalResult.SemaphoreLockType, _recordDisposalResult.NextDelay, cancellationToken);
                    break;

                default:
                    throw new InvalidOperationException($"Unexpected record disposal result {_recordDisposalResult.ResultType}");
            }
        }

        /// <summary>
        /// Create a syncer for the current connector configuration
        /// </summary>
        /// <returns>Content syncer</returns>
        protected IRecordDisposalAction CreateRecordDisposalAction() => _contentManagerActionProvider.CreateRecordDisposalAction();

        #region Observability
        protected override Dimensions GetCustomKeyDimensions()
        {
            var dimensions = new Dimensions
            {
                [nameof(Record.ExternalId)] = Record.ExternalId,
            };
            return dimensions;
        }

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
            return dimensions;
        }
        #endregion
    }
}