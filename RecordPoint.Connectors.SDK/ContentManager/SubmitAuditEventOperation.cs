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
    /// The submit audit event operation.
    /// </summary>
    public class SubmitAuditEventOperation : QueueableWorkBase<AuditEvent>
    {
        /// <summary>
        /// WORK TYPE.
        /// </summary>
        public const string WORK_TYPE = "Audit Event Submission";

        /// <summary>
        /// The CONTENT LABEL.
        /// </summary>
        public const string CONTENT_LABEL = "Audit Event";

        /// <summary>
        /// The DEFAULT DEFERRAL SECONDS.
        /// </summary>
        public const int DEFAULT_DEFERRAL_SECONDS = 10;

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
        /// Initializes a new instance of the <see cref="SubmitAuditEventOperation"/> class.
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
        public SubmitAuditEventOperation(
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
        /// Gets the audit event.
        /// </summary>
        public AuditEvent AuditEvent => Parameter;

        /// <summary>
        /// Gets the connector config id.
        /// </summary>
        public string ConnectorConfigId => WorkRequest.ConnectorConfigId;

        /// <summary>
        /// The connector configuration.
        /// </summary>
        private ConnectorConfigModel _connectorConfiguration;

        /// <summary>
        /// Get custom key dimensions.
        /// </summary>
        /// <returns>A Dimensions</returns>
        protected override Dimensions GetCustomKeyDimensions()
        {
            var dimensions = new Dimensions
            {
                [nameof(AuditEvent.ExternalId)] = AuditEvent.ExternalId,
                [nameof(AuditEvent.EventExternalId)] = AuditEvent.EventExternalId
            };
            return dimensions;
        }

        /// <summary>
        /// Inner the run asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
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

            var submissionStatus = await GetFeatureStatus(cancellationToken);
            if (!submissionStatus.Enabled)
            {
                await CompleteAsync(submissionStatus.EnabledReason, cancellationToken);
                return;
            }

            await InvokeSubmissionCallbackAsync(SubmissionActionType.PreSubmit, cancellationToken)
                .ConfigureAwait(false);

            var submitResult = await SubmitAsync(cancellationToken).ConfigureAwait(false);
            if (submitResult == null)
            {
                await FailAsync("Unexpected no submit result", cancellationToken);
                return;
            }

            switch (submitResult.SubmitStatus)
            {
                case SubmitResult.Status.OK:
                    await InvokeSubmissionCallbackAsync(SubmissionActionType.PostSubmit, cancellationToken);
                    await CompleteAsync($"{CONTENT_LABEL} submitted", cancellationToken);
                    break;

                case SubmitResult.Status.Skipped:
                    await CompleteAsync($"{CONTENT_LABEL} skipped", cancellationToken);
                    break;

                case SubmitResult.Status.Deferred:
                    {
                        var waitUntil = GetDeferralDateTime(submitResult.WaitUntilTime);
                        await RequeueAsync(waitUntil, cancellationToken);
                        await CompleteAsync($"{CONTENT_LABEL} was deferred by Records365", cancellationToken);
                    }
                    break;

                case SubmitResult.Status.ConnectorDisabled:
                    Abandoned("Connector was disabled");
                    break;

                case SubmitResult.Status.ConnectorNotFound:
                    Abandoned("The connector was not found in Records365");
                    break;

                case SubmitResult.Status.TooManyRequests:
                    {
                        var waitUntil = GetBackoffDateTime(submitResult.WaitUntilTime);
                        await RequeueAsync(waitUntil, cancellationToken);
                        await CompleteAsync("Backoff requested by Records365", cancellationToken);
                    }
                    break;

                default:
                    await FailAsync($"Unexpected submit result {submitResult.SubmitStatus}", cancellationToken);
                    break;
            }
        }

        /// <summary>
        /// Submits and return a task of type submitresult asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><![CDATA[Task<SubmitResult>]]></returns>
        private Task<SubmitResult> SubmitAsync(CancellationToken cancellationToken)
        {
            return _r365Client.SubmitAuditEvent(_connectorConfiguration, Parameter, cancellationToken);
        }

        /// <summary>
        /// Get feature status.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><![CDATA[Task<ConnectorFeatureStatus>]]></returns>
        private Task<ConnectorFeatureStatus> GetFeatureStatus(CancellationToken cancellationToken)
        {
            return _connectorManager.GetSubmissionStatusAsync(ConnectorConfigId, cancellationToken);
        }


        /// <summary>
        /// Get deferral date time
        /// </summary>
        /// <param name="suggestedDateTime">Date time suggested by R365, if provided</param>
        /// <returns></returns>
        private DateTimeOffset GetDeferralDateTime(DateTimeOffset? suggestedDateTime)
        {
            return suggestedDateTime ?? DateTimeProvider.UtcNow + TimeSpan.FromSeconds(DEFAULT_DEFERRAL_SECONDS);
        }

        /// <summary>
        /// Get back off till date time
        /// </summary>
        /// <param name="suggestedDateTime">Date time suggested by R365, if provided</param>
        /// <returns></returns>
        private DateTimeOffset GetBackoffDateTime(DateTimeOffset? suggestedDateTime)
        {
            return suggestedDateTime ?? DateTimeProvider.UtcNow + TimeSpan.FromSeconds(DEFAULT_DEFERRAL_SECONDS);
        }

        private Task RequeueAsync(DateTimeOffset waitTill, CancellationToken cancellationToken)
        {
            return _workQueueClient.SubmitAuditEventAsync(new ContentSubmissionConfiguration
            {
                ConnectorConfigurationId = _connectorConfiguration.Id,
                TenantId = _connectorConfiguration.TenantId,
                TenantDomainName = _connectorConfiguration.TenantDomainName,
            }, Parameter, waitTill, cancellationToken);
        }

        private IAuditEventSubmissionCallbackAction CreateAuditEventSubmissionCallbackAction() => _contentManagerActionProvider.CreateAuditEventSubmissionCallbackAction();

        private async Task InvokeSubmissionCallbackAsync(SubmissionActionType submissionActionType, CancellationToken cancellationToken)
        {
            var auditEventSubmissionCallbackAction = CreateAuditEventSubmissionCallbackAction();

            //If no callback action has been registered, just bail out now
            if (auditEventSubmissionCallbackAction == null)
                return;

            var connectorConfiguration = await _connectorManager.GetConnectorAsync(ConnectorConfigId, cancellationToken);
            await auditEventSubmissionCallbackAction
                .ExecuteAsync(connectorConfiguration, Parameter, submissionActionType, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}