using Microsoft.Rest;
using Polly;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using static RecordPoint.Connectors.SDK.Fields;

namespace RecordPoint.Connectors.SDK.SubmitPipeline
{
    /// <summary>
    /// Base class for submit pipeline elements that submit content to the Records365 vNext
    /// Connector API.
    /// </summary>
    public abstract class HttpSubmitPipelineElementBase : SubmitPipelineElementBase
    {
        private const string waitUntilTime = "wait-until-time";
        private const int defaultWaitTimeSeconds = 30;
        private const int MaxRetryAttempts = 4;

        /// <summary>
        /// Constructs a new HttpSubmitPipelineElementBase with an optional next submit
        /// pipeline element.
        /// </summary>
        /// <param name="next"></param>
        protected HttpSubmitPipelineElementBase(ISubmission next) : base(next)
        {
        }

        /// <summary>
        /// Constructs the APIClientFactory used to communicate with Records365
        /// </summary>
        public IApiClientFactory ApiClientFactory { get; set; }

        /// <summary>
        /// Handles the result of a call to a submission API.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="submitContext">The context of the submission.</param>
        /// <param name="result">The result of the submit API call.</param>
        /// <param name="itemTypeName">A string that identifies the item type (e.g., "Aggregation"). Used only for constructing log strings.</param>
        /// <returns>True if the submit pipeline should continue as a result of successful submission, false otherwise.</returns>
        protected async Task<bool> HandleSubmitResponse<T>(SubmitContext submitContext, HttpOperationResponse<T> result, string itemTypeName)
        {
            var shouldContinueSubmitPipeline = true;
            HandleSubmitResponseValidation(submitContext, result, itemTypeName);

            // Default the SubmitStatus to OK first in case any developer reuse the same SubmitContext
            // Then all the following SubmitContext will have the previous result
            submitContext.SubmitResult.SubmitStatus = SubmitResult.Status.OK;

            switch (result.Response.StatusCode)
            {
                case System.Net.HttpStatusCode.Conflict:
                    // shouldContinueSubmitPipeline should still be true on Conflict.
                    // There may have been previous submissions of that item that failed at a later stage and need to be re-tried.
                    LogVerbose(submitContext, nameof(HandleSubmitResponse), $"Submission returned {result.Response.StatusCode} : {itemTypeName} already submitted.");
                    break;
                case System.Net.HttpStatusCode.OK:
                    LogVerbose(submitContext, nameof(HandleSubmitResponse), $"Submission returned {result.Response.StatusCode} : {itemTypeName} submitted.");
                    break;
                case System.Net.HttpStatusCode.Created:
                    LogVerbose(submitContext, nameof(HandleSubmitResponse), $"Submission returned {result.Response.StatusCode} : {itemTypeName} submitted.");
                    break;
                case System.Net.HttpStatusCode.Accepted:
                    LogVerbose(submitContext, nameof(HandleSubmitResponse), $"Submission returned {result.Response.StatusCode} : {itemTypeName} accepted for submission.");
                    break;
                case System.Net.HttpStatusCode.NoContent:
                    LogVerbose(submitContext, nameof(HandleSubmitResponse), $"Submission returned {result.Response.StatusCode} : {itemTypeName} accepted for submission.");
                    break;
                case System.Net.HttpStatusCode.BadRequest:
                    shouldContinueSubmitPipeline = false;
                    await HandleBadRequestAsync<T>(submitContext, result, itemTypeName);
                    break;
                case System.Net.HttpStatusCode.Forbidden:
                    shouldContinueSubmitPipeline = false;
                    LogWarning(submitContext, nameof(HandleSubmitResponse), $"Submission returned {result.Response.StatusCode} : {itemTypeName} NOT submitted because the connector was not found.");
                    submitContext.SubmitResult.SubmitStatus = SubmitResult.Status.ConnectorNotFound;
                    break;
                case System.Net.HttpStatusCode.PreconditionFailed:
                    shouldContinueSubmitPipeline = false;
                    LogVerbose(submitContext, nameof(HandleSubmitResponse), $"Submission returned {result.Response.StatusCode} : {itemTypeName} NOT submitted because a precondition failed.");
                    submitContext.SubmitResult.SubmitStatus = SubmitResult.Status.Deferred;
                    break;
                case System.Net.HttpStatusCode.TooManyRequests:
                    shouldContinueSubmitPipeline = false;
                    HandleTooManyRequests(submitContext, result, itemTypeName);
                    break;
                default:
                    LogWarning(submitContext, nameof(HandleSubmitResponse), $"Submission returned {result.Response.StatusCode} : {itemTypeName} NOT submitted.");
                    throw new HttpOperationException(submitContext.LogPrefix() +
                                    $"Submission returned {result.Response.StatusCode} : NOT submitted. " +
                                    $"Http Content: {await result.Response.Content.ReadAsStringAsync().ConfigureAwait(false)}");
            }

            return shouldContinueSubmitPipeline;
        }

        private void HandleSubmitResponseValidation<T>(SubmitContext submitContext, HttpOperationResponse<T> result, string itemTypeName)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }
            if (result.Response == null)
            {
                throw new HttpOperationException($"{submitContext.LogPrefix()}Invalid {itemTypeName} submission! Expecting a return type of {nameof(HttpResponseMessage)} but got null!");
            }
        }

        private async Task HandleBadRequestAsync<T>(SubmitContext submitContext, HttpOperationResponse<T> result, string itemTypeName)
        {
            // BadRequest (400) is returned in one of three scenarios:
            // - The submit was invalid (e.g. missing required field)
            // - the connector was disabled
            // - we're submitting a binary but binary protection is disabled for this connector
            var errorResponse = result.Body as ErrorResponseModel;
            if (errorResponse?.Error?.MessageCode == MessageCode.ConnectorNotEnabled)
            {
                // Connector was disabled
                LogWarning(submitContext, nameof(HandleSubmitResponse), $"Submission returned {result.Response.StatusCode} : {itemTypeName} NOT submitted because the connector was disabled.");
                submitContext.SubmitResult.SubmitStatus = SubmitResult.Status.ConnectorDisabled;
            }
            else if (errorResponse?.Error?.MessageCode == MessageCode.ProtectionNotEnabled)
            {
                // Protection is disabled
                LogWarning(submitContext, nameof(HandleSubmitResponse), $"Binary submission returned {result.Response.StatusCode} : {itemTypeName} NOT submitted because the connector does not have protection enabled.");
                submitContext.SubmitResult.SubmitStatus = SubmitResult.Status.Skipped;
            }
            else
            {
                // Bad request
                LogWarning(submitContext, nameof(HandleSubmitResponse),
                    $"Submission returned {result.Response.StatusCode} : {itemTypeName} NOT submitted because the submit was invalid. " +
                    $"Error Message: {errorResponse?.Error?.Message ?? "<null>"} " +
                    $"MessageCode: {errorResponse?.Error?.MessageCode ?? "<null>"} ");

                // On an invalid submission, throw an exception to encourage correct dead lettering.
                // The rest of the pipeline won't execute.
                var httpContent = "<null>";
                if (result.Response != null && result.Response.Content != null)
                {
                    try
                    {
                        httpContent = await result.Response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    }
                    catch
                    {
                        //Response content may not contain a value and will throw and exception which can be ignored
                    }
                }
                throw new HttpOperationException(submitContext.LogPrefix() +
                    $"Submission returned {result?.Response?.StatusCode} : {itemTypeName} NOT submitted because the submit was invalid." +
                    $"Http Content: {httpContent}");
            }
        }

        private void HandleTooManyRequests<T>(SubmitContext submitContext, HttpOperationResponse<T> result, string itemTypeName)
        {
            if (result.Response.Headers.TryGetValues(waitUntilTime, out var values))
            {
                var timeValue = values?.FirstOrDefault();
                if (!string.IsNullOrEmpty(timeValue) && DateTime.TryParse(timeValue, out var time))
                {
                    submitContext.SubmitResult.WaitUntilTime = time.ToUniversalTime();
                }
            }

            if (submitContext.SubmitResult.WaitUntilTime == null)
            {
                submitContext.SubmitResult.WaitUntilTime = DateTime.UtcNow.AddSeconds(defaultWaitTimeSeconds);
            }

            LogVerbose(submitContext, nameof(HandleSubmitResponse), $"Submission returned {result.Response.StatusCode} : {itemTypeName} NOT submitted because a part of the system is experiencing heavy load. " +
                $"Try again after {submitContext.SubmitResult.WaitUntilTime}.");
            submitContext.SubmitResult.SubmitStatus = SubmitResult.Status.TooManyRequests;
        }

        /// <summary>
        /// Gets a retry policy which can be used for communicating with Records365
        /// </summary>
        /// <returns></returns>
        protected Policy GetRetryPolicy(SubmitContext submitContext, string methodName = nameof(Submit))
        {
            return ApiClientRetryPolicy.GetPolicy(Log, GetType(), methodName, MaxRetryAttempts, submitContext.LogPrefix(), submitContext.CancellationToken);
        }
    }
}
