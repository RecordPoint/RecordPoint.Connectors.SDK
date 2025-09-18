using Microsoft.Extensions.DependencyInjection;
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
    /// Base class for single item submission workers
    /// </summary>
    public abstract class SubmitOperationBase<TContentItem> : QueueableWorkBase<TContentItem>
        where TContentItem : ContentItem
    {
        /// <summary>
        /// Default number of seconds to defer work if requested and no time is provided
        /// </summary>
        public const int DEFAULT_DEFERRAL_SECONDS = 10;

        /// <summary>
        /// Number of seconds to backoff if requested too and no time is provided
        /// </summary>
        public const int DEFAULT_BACKOFF_SECONDS = 60;

        #region Observability
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Dimensions GetCoreKeyDimensions()
        {
            var dimensions = base.GetCoreKeyDimensions();
            dimensions[nameof(ContentItem.ExternalId)] = ContentItem.ExternalId;
            return dimensions;
        }

        #endregion

        #region Dependencies

        /// <summary>
        /// Constructor used to inject dependencies
        /// </summary>
        protected SubmitOperationBase(
            IServiceProvider serviceProvider,
            IR365Client r365Client,
            IWorkQueueClient workQueueClient,
            ISystemContext systemContext,
            IObservabilityScope observabilityScope,
            ITelemetryTracker telemetryTracker,
            IDateTimeProvider dateTimeProvider)
            : base(serviceProvider, systemContext, observabilityScope, telemetryTracker, dateTimeProvider)
        {
            R365Client = r365Client;
            WorkQueueClient = workQueueClient;
        }

        /// <summary>
        /// R365 Client
        /// </summary>
        public IR365Client R365Client { get; }

        /// <summary>
        /// Work queue client needed to submit further work
        /// </summary>
        public IWorkQueueClient WorkQueueClient { get; }

        #endregion

        #region Parameters

        /// <summary>
        /// Connector the request is for
        /// </summary>
        public string ConnectorConfigId => WorkRequest.ConnectorConfigId;

        /// <summary>
        /// Content Item being submitted
        /// </summary>
        public ContentItem ContentItem => Parameter;

        /// <summary>
        /// A label used to describe what the content is. I.E. Record, Aggregation etc.
        /// Used to display diagnostics, observability messages
        /// </summary>
        public abstract string ContentLabel { get; }

        #endregion

        #region Run
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task InnerRunAsync(CancellationToken cancellationToken)
        {
            var featureStatus = await GetFeatureStatus(cancellationToken);
            if (!featureStatus.Enabled)
            {
                await CompleteAsync(featureStatus.EnabledReason, cancellationToken);
                return;
            }

            using var scope = _serviceProvider.CreateScope();

            await PreSubmitAsync(scope, cancellationToken)
                .ConfigureAwait(false);

            var submitResult = await SubmitAsync(cancellationToken).ConfigureAwait(false);
            if (submitResult == null)
            {
                await FailAsync("Unexpected no submit result", cancellationToken).ConfigureAwait(false);
                return;
            }

            switch (submitResult.SubmitStatus)
            {
                case SubmitResult.Status.OK:
                    await SubmitSuccessfulAsync(scope, cancellationToken).ConfigureAwait(false);
                    await CompleteAsync($"{ContentLabel} submitted", cancellationToken).ConfigureAwait(false);
                    break;

                case SubmitResult.Status.Skipped:
                    await CompleteAsync($"{ContentLabel} skipped", cancellationToken).ConfigureAwait(false);
                    break;

                case SubmitResult.Status.Deferred:
                    {
                        var waitUntil = GetDeferralDateTime(submitResult.WaitUntilTime);
                        await RequeueAsync(waitUntil, cancellationToken).ConfigureAwait(false);
                        await CompleteAsync($"{ContentLabel} was deferred by Records365", cancellationToken).ConfigureAwait(false);
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
                        await RequeueAsync(waitUntil, cancellationToken).ConfigureAwait(false);
                        await CompleteAsync("Backoff requested by Records365", cancellationToken).ConfigureAwait(false);
                    }
                    break;

                default:
                    await FailAsync($"Unexpected submit result {submitResult.SubmitStatus}", cancellationToken).ConfigureAwait(false);
                    break;
            }
        }

        /// <summary>
        /// Required override that does the submission to R365
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Submit result</returns>
        protected abstract Task<SubmitResult> SubmitAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Required override that gets the current feature status for this submission
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Feature status</returns>
        protected abstract Task<ConnectorFeatureStatus> GetFeatureStatus(CancellationToken cancellationToken);

        /// <summary>
        /// Required override that requeues the request if needed
        /// </summary>
        /// <param name="waitTill">Time to wait until</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Submit result</returns>
        protected abstract Task RequeueAsync(DateTimeOffset waitTill, CancellationToken cancellationToken);

        /// <summary>
        /// Required override that is called prior to submission to R365
        /// </summary>
        /// <param name="scope">Service scope</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Submit result</returns>
        protected abstract Task PreSubmitAsync(IServiceScope scope, CancellationToken cancellationToken);

        /// <summary>
        /// Required override that is called after a successfull submission to R365
        /// </summary>
        /// <param name="scope">Service scope</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Submit result</returns>
        protected abstract Task SubmitSuccessfulAsync(IServiceScope scope, CancellationToken cancellationToken);

        /// <summary>
        /// Get deferral date time
        /// </summary>
        /// <param name="suggestedDateTime">Date time suggested by R365, if provided</param>
        /// <returns></returns>
        protected virtual DateTimeOffset GetDeferralDateTime(DateTimeOffset? suggestedDateTime)
        {
            return suggestedDateTime ?? DateTimeProvider.UtcNow + TimeSpan.FromSeconds(DEFAULT_DEFERRAL_SECONDS);
        }

        /// <summary>
        /// Get back off till date time
        /// </summary>
        /// <param name="suggestedDateTime">Date time suggested by R365, if provided</param>
        /// <returns></returns>
        protected virtual DateTimeOffset GetBackoffDateTime(DateTimeOffset? suggestedDateTime)
        {
            return suggestedDateTime ?? DateTimeProvider.UtcNow + TimeSpan.FromSeconds(DEFAULT_DEFERRAL_SECONDS);
        }

        #endregion

        #region Dispose
        /// <summary>
        /// Dispose
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        protected override void InnerDispose()
        {
        }
        #endregion
    }
}