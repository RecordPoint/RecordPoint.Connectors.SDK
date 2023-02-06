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
    public class SubmitAggregationOperation : SubmitOperationBase<Aggregation>
    {
        public const string WORK_TYPE = "Aggregation Submission";

        public const string CONTENT_LABEL = "Aggregation";

        private readonly IContentManagerActionProvider _contentManagerActionProvider;
        private readonly IConnectorConfigurationManager _connectorManager;

        public SubmitAggregationOperation(
            IServiceProvider serviceProvider,
            IContentManagerActionProvider contentManagerActionProvider,
            IR365Client r365Client,
            IWorkQueueClient workQueueClient,
            IConnectorConfigurationManager connectorManager,
            ISystemContext systemContext,
            IScopeManager scopeManager,
            ILogger<SubmitAggregationOperation> logger,
            ITelemetryTracker telemetryTracker,
            IDateTimeProvider dateTimeProvider)
            : base(serviceProvider, r365Client, workQueueClient, systemContext, scopeManager, logger, telemetryTracker, dateTimeProvider)
        {
            _contentManagerActionProvider = contentManagerActionProvider;
            _connectorManager = connectorManager;
        }

        public override string ServiceName => ContentManagerObservabilityExtensions.SERVICE_NAME;

        public override string WorkType => WORK_TYPE;

        public Aggregation Aggregation => Parameter;

        public override string ContentLabel => CONTENT_LABEL;

        protected override Task<SubmitResult> SubmitAsync(CancellationToken cancellationToken)
        {
            return R365Client.SubmitAggregation(ConnectorConfig, Aggregation, cancellationToken);
        }

        public ConnectorConfigModel ConnectorConfig { get; private set; }

        protected override async Task InnerRunAsync(CancellationToken cancellationToken)
        {
            ConnectorConfig = await _connectorManager.GetConnectorAsync(ConnectorConfigId, cancellationToken);
            await base.InnerRunAsync(cancellationToken);
        }

        protected override Task<ConnectorFeatureStatus> GetFeatureStatus(CancellationToken cancellationToken)
        {
            return _connectorManager.GetSubmissionStatusAsync(ConnectorConfigId, cancellationToken);
        }

        protected override Task RequeueAsync(DateTimeOffset waitTill, CancellationToken cancellationToken)
        {
            return WorkQueueClient.SubmitAggregationAsync(new ContentSynchronisationConfiguration()
            {
                ConnectorConfigurationId = ConnectorConfig.Id,
                TenantId = ConnectorConfig.TenantId,
                TenantDomainName = ConnectorConfig.TenantDomainName,
            }, Aggregation, waitTill, cancellationToken);
        }

        private IAggregationSubmissionCallbackAction CreateAggregationSubmissionCallbackAction() => _contentManagerActionProvider.CreateAggregationSubmissionCallbackAction();

        protected override async Task PreSubmitAsync(CancellationToken cancellationToken)
        {
            await InvokeSubmissionCallbackAsync(SubmissionActionType.PreSubmit, cancellationToken)
                .ConfigureAwait(false);
        }

        protected override async Task SubmitSuccessfulAsync(CancellationToken cancellationToken)
        {
            await InvokeSubmissionCallbackAsync(SubmissionActionType.PostSubmit, cancellationToken)
                .ConfigureAwait(false);
        }

        private async Task InvokeSubmissionCallbackAsync(SubmissionActionType submissionActionType, CancellationToken cancellationToken)
        {
            var aggregationSubmissionCallbackAction = CreateAggregationSubmissionCallbackAction();

            //If no callback action has been registered, just bail out now
            if (aggregationSubmissionCallbackAction == null)
                return;

            var connectorConfiguration = await _connectorManager.GetConnectorAsync(ConnectorConfigId, cancellationToken);
            await aggregationSubmissionCallbackAction
                .ExecuteAsync(connectorConfiguration, Parameter, submissionActionType, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}