using Microsoft.Extensions.Logging;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.ContentReport.Export;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Notifications;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;
using System.Text.Json;

namespace RecordPoint.Connectors.SDK.ContentReport
{
    // TODO: Respect long running process conventions
    // Save state and call continue if no work to do immediately
    public class GenerateReportOperation : ManagedQueueableWorkBase<ConnectorNotificationModel, ContentReportState>
    {

        public const string WORK_TYPE = "Generate Report";
        private const string CONNECTOR_NOT_FOUND = "The specified Connector Configuration does not exist.";

        public override string ServiceName => ContentReportStandardDimensions.SERVICE_NAME;

        public override string WorkType => WORK_TYPE;

        private readonly IConnectorConfigurationManager _connectorConfigurationManager;
        private readonly IContentManagerActionProvider _contentManagerActionProvider;
        private readonly IR365NotificationClient _r365NotificationClient;
        private readonly IExport _export;

        public GenerateReportOperation(
            IServiceProvider serviceProvider,
            IConnectorConfigurationManager connectorConfigurationManager,
            IContentManagerActionProvider contentManagerActionProvider,
            IR365NotificationClient r365NotificationClient,
            ISystemContext systemContext,
            ILogger<GenerateReportOperation> logger,
            IDateTimeProvider dateTimeProvider,
            IManagedWorkFactory managedWorkFactory,
            IScopeManager scopeManager,
            ITelemetryTracker telemetryTracker,
            IExport export)
            : base(serviceProvider, managedWorkFactory, systemContext, scopeManager, logger, telemetryTracker, dateTimeProvider)
        {
            _connectorConfigurationManager = connectorConfigurationManager;
            _contentManagerActionProvider = contentManagerActionProvider;
            _r365NotificationClient = r365NotificationClient;
            _contentManagerActionProvider = contentManagerActionProvider;
            _export = export;
        }

        protected async override Task InnerRunAsync(CancellationToken cancellationToken)
        {
            // Retrieve the notification
            var notification = Configuration;
            var startTime = DateTimeProvider.UtcNow;
            try
            {
                var connectorConfiguration = await _connectorConfigurationManager.GetConnectorConfigurationAsync(notification.ConnectorConfig.Id, cancellationToken);
                if (connectorConfiguration == null)
                {
                    // Tell Records 365 we failed
                    await _r365NotificationClient.AcknowledgeNotificationAsync(notification, SDK.Client.ProcessingResult.NotificationError, CONNECTOR_NOT_FOUND, cancellationToken).ConfigureAwait(false);

                    // End the Content Report work as the Connector Configuration doesn't exist
                    await CompleteAsync(CONNECTOR_NOT_FOUND, cancellationToken);
                    return;
                }

                var contentDiscoveryAction = _contentManagerActionProvider.CreateGenerationReportAction();

                ContentResult? scanOutcome = null;
                while (scanOutcome == null || scanOutcome.ResultType == ContentResultType.Incomplete)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    var scanTask = scanOutcome == null 
                        ? contentDiscoveryAction.BeginAsync(notification.ConnectorConfig, cancellationToken) 
                        : contentDiscoveryAction.ContinueAsync(notification.ConnectorConfig, scanOutcome.Cursor, cancellationToken);

                    scanOutcome = await scanTask.ConfigureAwait(false);

                    var scannedRecords = scanOutcome.Records.ToList();
                    if (scannedRecords.Any())
                    {
                        var exportRequest = new ExportRequest()
                        {
                            ExportRequestId = Guid.NewGuid().ToString(),
                            ExporDateTime = startTime,
                            ExportLocation = connectorConfiguration.ReportLocation,
                            Records = scannedRecords
                        };
                        _export.ExportToDestination(exportRequest, cancellationToken);
                    }
                    else
                    {
                        // Wait for some more work
                        await Task.Delay(1000, cancellationToken);
                    }
                }

                // Tell Records 365 we completed
                var completeReason = "Report Completed";
                await _r365NotificationClient.AcknowledgeNotificationAsync(notification, SDK.Client.ProcessingResult.OK, completeReason, cancellationToken).ConfigureAwait(false);

                await CompleteAsync(completeReason, cancellationToken);
            }
            catch (Exception ex)
            {
                // Tell Records 365 we failed
                await _r365NotificationClient.AcknowledgeNotificationAsync(notification, SDK.Client.ProcessingResult.NotificationError, ex.Message, cancellationToken).ConfigureAwait(false);

                // Lots of units of work might retry to handle transient errors
                // But report generation can be WAY expensive and its not idempotent
                // So we will fail it

                // Report that the job has failed
                await FailAsync(ex.Message, ex, cancellationToken);
            }
        }

        /// <summary>
        /// Get custom key dimensions
        /// </summary>
        /// <returns></returns>
        protected override Dimensions GetCustomKeyDimensions()
        {
            var keyDimensions = base.GetCustomKeyDimensions();

            keyDimensions["TenantID"] = Configuration.TenantId;
            keyDimensions["ConnectorConfigurationId"] = Configuration.ConnectorId;

            return keyDimensions;

        }

        protected override ConnectorNotificationModel DeserializeConfiguration(string configurationType, string configurationText)
        {
            if (configurationType != nameof(ConnectorNotificationModel))
                throw new ArgumentOutOfRangeException(nameof(configurationType));

            return JsonSerializer.Deserialize<ConnectorNotificationModel>(configurationText);
        }

        protected override ContentReportState DeserializeState(string stateType, string stateText) => ContentReportState.Deserialize(stateType, stateText);

        protected override (string, string) SerializeState(ContentReportState state) => (ContentReportState.LatestStateType, state.Serialize());
    }
}
