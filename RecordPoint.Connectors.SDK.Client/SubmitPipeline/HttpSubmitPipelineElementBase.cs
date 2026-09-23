using Microsoft.Rest;
using Newtonsoft.Json;
using Polly.Retry;
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
                    HandleSuccessfulRequest(submitContext, $"Submission returned {result.Response.StatusCode} : {itemTypeName} already submitted.");
                    break;
                case System.Net.HttpStatusCode.OK:
                    HandleSuccessfulRequest(submitContext, $"Submission returned {result.Response.StatusCode} : {itemTypeName} submitted.");
                    break;
                case System.Net.HttpStatusCode.Created:
                    HandleSuccessfulRequest(submitContext, $"Submission returned {result.Response.StatusCode} : {itemTypeName} submitted.");
                    break;
                case System.Net.HttpStatusCode.Accepted:
                    HandleSuccessfulRequest(submitContext, $"Submission returned {result.Response.StatusCode} : {itemTypeName} accepted for submission.");
                    break;
                case System.Net.HttpStatusCode.NoContent:
                    HandleSuccessfulRequest(submitContext, $"Submission returned {result.Response.StatusCode} : {itemTypeName} accepted for submission.");
                    break;
                case System.Net.HttpStatusCode.BadRequest:
                    shouldContinueSubmitPipeline = false;
                    await HandleBadRequestAsync<T>(submitContext, result, itemTypeName);
                    break;
                case System.Net.HttpStatusCode.Forbidden:
                    shouldContinueSubmitPipeline = false;
                    var forbiddenReason = await BuildForbiddenReasonAsync(result.Response, itemTypeName).ConfigureAwait(false);
                    LogWarning(submitContext, nameof(HandleSubmitResponse), forbiddenReason);
                    submitContext.SubmitResult.SubmitStatus = SubmitResult.Status.ConnectorNotFound;
                    submitContext.SubmitResult.Reason = forbiddenReason;
                    break;
                case System.Net.HttpStatusCode.PreconditionFailed:
                    shouldContinueSubmitPipeline = false;
                    var preconditionReason = $"Submission returned {result.Response.StatusCode} : {itemTypeName} NOT submitted because a precondition failed.";
                    LogVerbose(submitContext, nameof(HandleSubmitResponse), preconditionReason);
                    submitContext.SubmitResult.SubmitStatus = SubmitResult.Status.Deferred;
                    submitContext.SubmitResult.Reason = preconditionReason;
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

        private void HandleSuccessfulRequest(SubmitContext submitContext, string reason)
        {
            LogVerbose(submitContext, nameof(HandleSuccessfulRequest), reason);
            submitContext.SubmitResult.SubmitStatus = SubmitResult.Status.OK;
            submitContext.SubmitResult.Reason = reason;
        }

        private static async Task<string> BuildForbiddenReasonAsync(HttpResponseMessage response, string itemTypeName)
        {
            var responseContent = string.Empty;
            if (response?.Content != null)
            {
                try
                {
                    responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
                catch
                {
                    responseContent = string.Empty;
                }
            }

            return BuildForbiddenReason(response?.StatusCode.ToString() ?? "<No Status Code>", itemTypeName, responseContent);
        }

        private static string BuildForbiddenReason(string statusCode, string itemTypeName, string responseContent)
        {
            var forbiddenReason = $"Submission returned {statusCode} : {itemTypeName} NOT submitted because the connector request was forbidden.";
            var errorDetail = TryExtractForbiddenErrorDetail(responseContent);

            return string.IsNullOrWhiteSpace(errorDetail)
                ? forbiddenReason
                : $"{forbiddenReason} Error Detail: {errorDetail}";
        }

        private static string TryExtractForbiddenErrorDetail(string responseContent)
        {
            if (string.IsNullOrWhiteSpace(responseContent))
            {
                return string.Empty;
            }

            try
            {
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponseModel>(responseContent);
                var messages = new List<string>();

                if (!string.IsNullOrWhiteSpace(errorResponse?.Error?.Message))
                {
                    messages.Add(errorResponse.Error.Message);
                }

                if (errorResponse?.Error?.InnerError?.Any() == true)
                {
                    messages.AddRange(errorResponse.Error.InnerError
                        .Select(x => x?.Message)
                        .Where(x => !string.IsNullOrWhiteSpace(x)));
                }

                return string.Join(" | ", messages.Distinct());
            }
            catch
            {
                return responseContent;
            }
        }

        /// <summary>
        /// Attempts to classify a thrown <see cref="HttpOperationException"/> as a known submission outcome.
        /// </summary>
        /// <param name="submitContext">The submission context for the current operation.</param>
        /// <param name="ex">The exception thrown by the API client.</param>
        /// <param name="itemTypeName">The submitted item type name used in log and result messages.</param>
        /// <param name="shouldContinueSubmitPipeline">Whether the submission pipeline should continue after handling the exception.</param>
        /// <returns><c>true</c> if the exception was recognized and handled; otherwise, <c>false</c>.</returns>
        protected bool TryHandleKnownHttpOperationException(SubmitContext submitContext, HttpOperationException ex, string itemTypeName, out bool shouldContinueSubmitPipeline)
        {
            shouldContinueSubmitPipeline = true;

            if (ex.Response?.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                LogVerbose(submitContext, nameof(TryHandleKnownHttpOperationException), $"Submission returned {ex.Response.StatusCode} : {itemTypeName} already submitted.");
                return true;
            }

            if (ex.Response?.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                shouldContinueSubmitPipeline = false;
                var forbiddenReason = BuildForbiddenReason(ex.Response.StatusCode.ToString(), itemTypeName, ex.Response.Content);
                LogWarning(submitContext, nameof(TryHandleKnownHttpOperationException), forbiddenReason);
                submitContext.SubmitResult.SubmitStatus = SubmitResult.Status.ConnectorNotFound;
                submitContext.SubmitResult.Reason = forbiddenReason;
                return true;
            }

            return false;
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
                var connectorDisabledReason = $"Submission returned {result.Response.StatusCode} : {itemTypeName} NOT submitted because the connector was disabled.";
                LogWarning(submitContext, nameof(HandleSubmitResponse), connectorDisabledReason);
                submitContext.SubmitResult.SubmitStatus = SubmitResult.Status.ConnectorDisabled;
                submitContext.SubmitResult.Reason = connectorDisabledReason;
            }
            else if (errorResponse?.Error?.MessageCode == MessageCode.ProtectionNotEnabled)
            {
                // Protection is disabled
                var protectionDisabledReason = $"Binary submission returned {result.Response.StatusCode} : {itemTypeName} NOT submitted because the connector does not have protection enabled.";
                LogWarning(submitContext, nameof(HandleSubmitResponse), protectionDisabledReason);
                submitContext.SubmitResult.SubmitStatus = SubmitResult.Status.Skipped;
                submitContext.SubmitResult.Reason = protectionDisabledReason;
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

            var reason = $"Submission returned {result.Response.StatusCode} : {itemTypeName} NOT submitted because a part of the system is experiencing heavy load. " +
                $"Try again after {submitContext.SubmitResult.WaitUntilTime}.";
            LogVerbose(submitContext, nameof(HandleSubmitResponse), reason);
            submitContext.SubmitResult.SubmitStatus = SubmitResult.Status.TooManyRequests;
            submitContext.SubmitResult.Reason = reason;

        }

        /// <summary>
        /// Gets a retry policy which can be used for communicating with Records365
        /// </summary>
        /// <returns></returns>
        protected AsyncRetryPolicy GetRetryPolicy(SubmitContext submitContext, string methodName = nameof(Submit))
        {
            return ApiClientRetryPolicy.GetPolicy(Log, GetType(), methodName, MaxRetryAttempts, submitContext.LogPrefix(), submitContext.CancellationToken);
        }
    }
}
