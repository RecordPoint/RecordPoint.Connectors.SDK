using Azure;
using Microsoft.Extensions.DependencyInjection;
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
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.ContentManager;

/// <summary>
/// Synchronously submits records and all associated binaries to the R365 system.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="SynchronousSubmitRecordOperation"/> class.
/// </remarks>
/// <param name="serviceProvider">The service provider.</param>
/// <param name="contentManagerActionProvider">The content manager action provider.</param>
/// <param name="r365Client">The r365 client.</param>
/// <param name="connectorManager">The connector manager.</param>
/// <param name="systemContext">The system context.</param>
/// <param name="observabilityScope">The scope manager.</param>
/// <param name="telemetryTracker">The telemetry tracker.</param>
/// <param name="workQueueClient">The work queue client.</param>
/// <param name="dateTimeProvider">The date time provider.</param>
public class SynchronousSubmitRecordOperation(
    IServiceProvider serviceProvider,
    IContentManagerActionProvider contentManagerActionProvider,
    IR365Client r365Client,
    IConnectorConfigurationManager connectorManager,
    ISystemContext systemContext,
    IObservabilityScope observabilityScope,
    ITelemetryTracker telemetryTracker,
    IWorkQueueClient workQueueClient,
    IDateTimeProvider dateTimeProvider) : QueueableWorkBase<Record>(serviceProvider, systemContext, observabilityScope, telemetryTracker, dateTimeProvider)
{

    private const int DEFAULT_DEFERRAL_SECONDS = 10;
    private const int MAX_BINARY_SUBMISSION_RETRIES = 5;
    private const string CONNECTOR_DISABLED_REASON = "Connector disabled";
    private const string CONNECTOR_NOT_FOUND_REASON = "Connector not found";

    /// <summary>
    /// WORK TYPE.
    /// </summary>
    public const string WORK_TYPE = "Record Submission";
    private const string BINARY_SUBMISSION_DEFERRED_REASON = "Binary submission deferred";
    private const string TOO_MANY_REQUESTS_REASON = "Too many requests";

    /// <summary>
    /// Gets the service name.
    /// </summary>
    public override string ServiceName => ContentManagerObservabilityExtensions.SERVICE_NAME;

    /// <summary>
    /// Gets the work type.
    /// </summary>
    public override string WorkType => WORK_TYPE;

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
            [nameof(Record.ExternalId)] = Parameter.ExternalId,
        };
        return dimensions;
    }

    /// <summary>
    /// Inner the run asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <exception cref="InvalidOperationException"></exception>
    /// <returns>A Task</returns>
    protected override async Task InnerRunAsync(CancellationToken cancellationToken)
    {
        var featureStatus = await connectorManager.GetSubmissionStatusAsync(ConnectorConfigId, cancellationToken);
        if (!featureStatus.Enabled)
        {
            //If submissions are not enabled, just complete the submission request without submitting
            await CompleteAsync(featureStatus.EnabledReason, cancellationToken);
            return;
        }

        _connectorConfiguration = await connectorManager.GetConnectorAsync(ConnectorConfigId, cancellationToken);
        if (_connectorConfiguration == null)
        {
            //If connector not found assume its been deleted
            await CompleteAsync(CONNECTOR_NOT_FOUND_REASON, cancellationToken);
            return;
        }

        if (await CheckSemaphoreLockAsync(_connectorConfiguration, Parameter, cancellationToken))
        {
            return;
        }

        //Attempt to submit the binaries
        var (shouldDeferRecordSubmissionReason, waitUntilTime, shouldAbandonRecordSubmissionReason) = await ProcessBinariesAsync(cancellationToken);

        if (string.IsNullOrEmpty(shouldDeferRecordSubmissionReason) && string.IsNullOrEmpty(shouldAbandonRecordSubmissionReason))
        {
            //If we didn't submit all the binaries we should still submit the record, but requeue it for further processing later
            var shouldRequeue = Parameter.Binaries.Any(binary => binary.BinarySubmissionStatus == BinarySubmissionStatus.NotSubmitted && binary.SubmissionAttempts < MAX_BINARY_SUBMISSION_RETRIES);

            //Submit the record to the platform
            await SubmitRecordAsync(shouldRequeue, waitUntilTime, cancellationToken);
        }
        else if (!string.IsNullOrEmpty(shouldDeferRecordSubmissionReason))
        {
            await RequeueAsync(new DateTimeOffset(waitUntilTime.Value), cancellationToken);

            Deferred($"Deferring record submission - {shouldDeferRecordSubmissionReason}", waitUntilTime);
        }
        else if (!string.IsNullOrEmpty(shouldAbandonRecordSubmissionReason))
        {
            Abandoned(shouldAbandonRecordSubmissionReason);
        }
    }

    private async Task<(string shouldDeferRecordSubmissionReason, DateTime? WaitUntilTime, string ShouldAbandonRecordSubmissionReason)> ProcessBinariesAsync(CancellationToken cancellationToken)
    {
        var binarySubmissionStatus = await connectorManager.GetBinarySubmissionStatusAsync(ConnectorConfigId, cancellationToken);
        if (binarySubmissionStatus.Enabled)
        {
            //Get binaries that are unsubmitted and have not reached the max retry threshold
            var unsubmittedBinaries = Parameter.Binaries
                .Where(binary => binary.BinarySubmissionStatus == BinarySubmissionStatus.NotSubmitted && binary.SubmissionAttempts < MAX_BINARY_SUBMISSION_RETRIES);

            //Process unsubmitted binaries
            foreach (var binary in unsubmittedBinaries)
            {
                binary.SubmissionAttempts++;

                try
                {
                    var binaryRetrievalResult = await RetrieveBinaryAsync(binary, cancellationToken);
                    if (!string.IsNullOrEmpty(binaryRetrievalResult.ShouldDeferBinarySubmissionsReason))
                    {
                        //If we have received a backoff result from the content source for retrieving the binary
                        //We should not attempt to retrieve any more binaries.
                        //This won't stop us from submitting the record to the platform though
                        //so we don't want to return a record deferal here, only the back off time for the content source.
                        //We will requeue the record for further processing after this wait time.
                        return (null, binaryRetrievalResult.WaitUntilTime, null);
                    }
                    else
                    {
                        var submitResult = await SubmitBinaryAsync(binary, binaryRetrievalResult.Stream, cancellationToken);
                        if (!string.IsNullOrEmpty(submitResult.ShouldDeferRecordSubmissionReason))
                        {
                            return (submitResult.ShouldDeferRecordSubmissionReason, submitResult.WaitUntilTime, null);
                        }
                        else if (!string.IsNullOrEmpty(submitResult.ShouldAbandonRecordSubmissionReason))
                        {
                            return (null, null, submitResult.ShouldAbandonRecordSubmissionReason);
                        }
                    }
                }
                catch (Exception ex)
                {
                    telemetryTracker.TrackException(ex, GetKeyDimensions());
                }
            }
        }

        return (null, null, null);
    }

    private async Task<(string ShouldDeferBinarySubmissionsReason, DateTime? WaitUntilTime, Stream Stream)> RetrieveBinaryAsync(BinaryMetaInfo binary, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var binaryRetrievalAction = contentManagerActionProvider.CreateBinaryRetrievalAction(scope);

        //Invoke retrieval action to obtain binary stream
        var binaryRetrievalResult = await binaryRetrievalAction
            .ExecuteAsync(_connectorConfiguration, binary, cancellationToken)
            .ConfigureAwait(false);

        //Could not successfully retrieve the binary stream
        switch (binaryRetrievalResult.ResultType)
        {
            case BinaryRetrievalResultType.BackOff:
                return (TOO_MANY_REQUESTS_REASON,  DateTime.UtcNow.AddSeconds(binaryRetrievalResult.NextDelay ?? DEFAULT_DEFERRAL_SECONDS), null);
            case BinaryRetrievalResultType.Failed:
                return (null, null, null);
            case BinaryRetrievalResultType.Abandoned:
            case BinaryRetrievalResultType.ZeroBinary:
            case BinaryRetrievalResultType.Deleted:
                binary.BinarySubmissionStatus = BinarySubmissionStatus.Skipped;
                return (null, null, null);
            default:
                return (null, null, binaryRetrievalResult.Stream);
        }
    }

    private async Task<(string ShouldDeferRecordSubmissionReason, DateTime? WaitUntilTime, string ShouldAbandonRecordSubmissionReason)> SubmitBinaryAsync(BinaryMetaInfo binary, Stream stream, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        //Invoke Pre-Submit Action
        await InvokeBinarySubmissionCallbackAsync(scope, SubmissionActionType.PreSubmit, binary, cancellationToken)
            .ConfigureAwait(false);

        //Perform Binary Submission
        var submitResult = await r365Client
            .SubmitBinary(_connectorConfiguration, binary, stream, cancellationToken)
            .ConfigureAwait(false);

        switch (submitResult.SubmitStatus)
        {
            case SubmitResult.Status.Skipped:
                return (null, null, null);
            case SubmitResult.Status.Deferred:
                return (BINARY_SUBMISSION_DEFERRED_REASON, submitResult.WaitUntilTime, null);
            case SubmitResult.Status.TooManyRequests:
                return (TOO_MANY_REQUESTS_REASON, submitResult.WaitUntilTime, null);
            case SubmitResult.Status.ConnectorDisabled:
                return (null, null, CONNECTOR_DISABLED_REASON);
            case SubmitResult.Status.ConnectorNotFound:
                return (null, null, CONNECTOR_NOT_FOUND_REASON);
            default:
                //If the binary was successfully submitted, process the next binary
                binary.BinarySubmissionStatus = BinarySubmissionStatus.Submitted;

                //Invoke Post-Submit Action
                await InvokeBinarySubmissionCallbackAsync(scope, SubmissionActionType.PostSubmit, binary, cancellationToken)
                    .ConfigureAwait(false);

                return (null, null, null);
        }
    }

    private async Task SubmitRecordAsync(bool shouldRequeue, DateTime? requeueWaitTill, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        await InvokeRecordSubmissionCallbackAsync(scope, SubmissionActionType.PreSubmit, cancellationToken)
            .ConfigureAwait(false);

        var submitResult = await r365Client.SubmitRecord(_connectorConfiguration, Parameter, cancellationToken)
            .ConfigureAwait(false);

        if (submitResult == null)
        {
            await FailAsync("Unexpected no submit result", cancellationToken)
                .ConfigureAwait(false);
            return;
        }

        switch (submitResult.SubmitStatus)
        {
            case SubmitResult.Status.OK:
                await InvokeRecordSubmissionCallbackAsync(scope, SubmissionActionType.PostSubmit, cancellationToken)
                    .ConfigureAwait(false);

                if (shouldRequeue)
                {
                    await RequeueAsync(requeueWaitTill ?? DateTime.UtcNow.AddSeconds(DEFAULT_DEFERRAL_SECONDS), cancellationToken).ConfigureAwait(false);
                }
                await CompleteAsync("Record submitted", cancellationToken).ConfigureAwait(false);
                break;

            case SubmitResult.Status.Skipped:
                await CompleteAsync("Record skipped", submitResult.Reason, cancellationToken).ConfigureAwait(false);
                break;

            case SubmitResult.Status.Deferred:
            case SubmitResult.Status.TooManyRequests:
                {
                    var waitUntil = GetDeferralDateTime(submitResult.WaitUntilTime);
                    await RequeueAsync(waitUntil, cancellationToken).ConfigureAwait(false);
                    var reason = submitResult.SubmitStatus == SubmitResult.Status.Deferred
                        ? "Record was deferred by Records365"
                        : "Backoff requested by Records365";
                    await CompleteAsync(reason, cancellationToken).ConfigureAwait(false);
                }
                break;

            case SubmitResult.Status.ConnectorDisabled:
                Abandoned("Connector was disabled");
                break;

            case SubmitResult.Status.ConnectorNotFound:
                Abandoned("The connector was not found in Records365");
                break;

            default:
                await FailAsync($"Unexpected submit result {submitResult.SubmitStatus}", cancellationToken).ConfigureAwait(false);
                break;
        }
    }

    private async Task InvokeBinarySubmissionCallbackAsync(IServiceScope scope, SubmissionActionType submissionActionType, BinaryMetaInfo binary, CancellationToken cancellationToken)
    {
        var binarySubmissionCallbackAction = contentManagerActionProvider.CreateBinarySubmissionCallbackAction(scope);

        //If no callback action has been registered, just bail out now
        if (binarySubmissionCallbackAction == null)
            return;

        await binarySubmissionCallbackAction
            .ExecuteAsync(_connectorConfiguration, binary, submissionActionType, cancellationToken)
            .ConfigureAwait(false);
    }

    private async Task InvokeRecordSubmissionCallbackAsync(IServiceScope scope, SubmissionActionType submissionActionType, CancellationToken cancellationToken)
    {
        var recordSubmissionCallbackAction = contentManagerActionProvider.CreateRecordSubmissionCallbackAction(scope);

        //If no callback action has been registered, just bail out now
        if (recordSubmissionCallbackAction == null)
            return;

        await recordSubmissionCallbackAction
            .ExecuteAsync(_connectorConfiguration, Parameter, submissionActionType, cancellationToken)
            .ConfigureAwait(false);
    }

    private DateTimeOffset GetDeferralDateTime(DateTimeOffset? suggestedDateTime)
    {
        return suggestedDateTime ?? DateTimeProvider.UtcNow + TimeSpan.FromSeconds(DEFAULT_DEFERRAL_SECONDS);
    }

    private Task RequeueAsync(DateTimeOffset waitTill, CancellationToken cancellationToken)
    {
        return workQueueClient.SubmitRecordAsync(new ContentSubmissionConfiguration()
        {
            ConnectorConfigurationId = _connectorConfiguration.Id,
            TenantId = _connectorConfiguration.TenantId,
            TenantDomainName = _connectorConfiguration.TenantDomainName,
        }, Parameter, waitTill, cancellationToken);
    }

    /// <summary>
    /// Dispose
    /// </summary>
    protected override void InnerDispose()
    {
    }
}
