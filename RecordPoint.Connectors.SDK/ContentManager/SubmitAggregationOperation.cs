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
    /// The submit aggregation operation.
    /// </summary>
    public class SubmitAggregationOperation : SubmitOperationBase<Aggregation>
    {
        /// <summary>
        /// WORK TYPE.
        /// </summary>
        public const string WORK_TYPE = "Aggregation Submission";

        /// <summary>
        /// The CONTENT LABEL.
        /// </summary>
        public const string CONTENT_LABEL = "Aggregation";

        /// <summary>
        /// The content manager action provider.
        /// </summary>
        private readonly IContentManagerActionProvider _contentManagerActionProvider;
        /// <summary>
        /// The connector manager.
        /// </summary>
        private readonly IConnectorConfigurationManager _connectorManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubmitAggregationOperation"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="contentManagerActionProvider">The content manager action provider.</param>
        /// <param name="r365Client">The r365 client.</param>
        /// <param name="workQueueClient">The work queue client.</param>
        /// <param name="connectorManager">The connector manager.</param>
        /// <param name="systemContext">The system context.</param>
        /// <param name="scopeManager">The scope manager.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="telemetryTracker">The telemetry tracker.</param>
        /// <param name="dateTimeProvider">The date time provider.</param>
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

        /// <summary>
        /// Gets the service name.
        /// </summary>
        public override string ServiceName => ContentManagerObservabilityExtensions.SERVICE_NAME;

        /// <summary>
        /// Gets the work type.
        /// </summary>
        public override string WorkType => WORK_TYPE;

        /// <summary>
        /// Gets the aggregation.
        /// </summary>
        public Aggregation Aggregation => Parameter;

        /// <summary>
        /// Gets the content label.
        /// </summary>
        public override string ContentLabel => CONTENT_LABEL;

        /// <summary>
        /// Submits and return a task of type submitresult asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><![CDATA[Task<SubmitResult>]]></returns>
        protected override Task<SubmitResult> SubmitAsync(CancellationToken cancellationToken)
        {
            return R365Client.SubmitAggregation(ConnectorConfig, Aggregation, cancellationToken);
        }

        /// <summary>
        /// Gets the connector config.
        /// </summary>
        public ConnectorConfigModel ConnectorConfig { get; private set; }

        /// <summary>
        /// Inner the run asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task</returns>
        protected override async Task InnerRunAsync(CancellationToken cancellationToken)
        {
            ConnectorConfig = await _connectorManager.GetConnectorAsync(ConnectorConfigId, cancellationToken);
            await base.InnerRunAsync(cancellationToken);
        }

        /// <summary>
        /// Get feature status.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><![CDATA[Task<ConnectorFeatureStatus>]]></returns>
        protected override Task<ConnectorFeatureStatus> GetFeatureStatus(CancellationToken cancellationToken)
        {
            return _connectorManager.GetSubmissionStatusAsync(ConnectorConfigId, cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="waitTill"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task PreSubmitAsync(CancellationToken cancellationToken)
        {
            await InvokeSubmissionCallbackAsync(SubmissionActionType.PreSubmit, cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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