using Microsoft.Rest;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.SubmitPipeline
{
    public abstract class HttpSubmitPipelineElementBase : SubmitPipelineElementBase
    {
        protected HttpSubmitPipelineElementBase(ISubmission next) : base(next)
        {
        }

        public IApiClientFactory ApiClientFactory { get; set; }

        protected async Task<Dictionary<string, List<string>>> GetHttpRequestHeaders(SubmitContext submitContext)
        {
            var headers = new Dictionary<string, List<string>>();
            var authenticationResult = await ApiClientFactory
                .CreateAuthenticationHelper()
                .AcquireTokenAsync(submitContext.AuthenticationHelperSettings)
                .ConfigureAwait(false);
            headers.AddAuthorizationHeader(authenticationResult.AccessTokenType, authenticationResult.AccessToken);
            return headers;
        }

        protected async Task HandleSubmitResponse<T>(SubmitContext submitContext, HttpOperationResponse<T> result, string itemTypeName)
        {
            if (result == null)
            {
                throw new ArgumentNullException($"{submitContext.LogPrefix()}Invalid {itemTypeName} submission! Expecting a return type of {nameof(HttpOperationResponse<object>)} but got null!");
            }

            if (result.Response == null)
            {
                throw new HttpOperationException($"{submitContext.LogPrefix()}Invalid {itemTypeName} submission! Expecting a return type of {nameof(HttpResponseMessage)} but got null!");
            }

            switch (result.Response.StatusCode)
            {
                case System.Net.HttpStatusCode.Conflict:
                    LogVerbose(submitContext, nameof(HandleSubmitResponse), $"Submission returned {result.Response.StatusCode} : {itemTypeName} already submitted.");
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
                    {
                        // BadRequest (400) is returned in one of two scenarios:
                        // - The submit was invalid (e.g. missing required field)
                        // - the connector was disabled
                        var errorResponse = result.Body as ErrorResponseModel;
                        if (errorResponse?.Error?.MessageCode == "ConnectorNotEnabled")
                        {
                            // Connector was disabled
                            LogWarning(submitContext, nameof(HandleSubmitResponse), $"Submission returned {result.Response.StatusCode} : {itemTypeName} NOT submitted because the connector was disabled.");
                            submitContext.SubmitResult.SubmitStatus = SubmitResult.Status.ConnectorDisabled;
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
                                catch { }
                            }
                            throw new HttpOperationException(submitContext.LogPrefix() +
                                $"Submission returned {result.Response.StatusCode} : {itemTypeName} NOT submitted because the submit was invalid." +
                                $"Http Content: {httpContent}");
                        }
                        break;
                    }
                case System.Net.HttpStatusCode.Forbidden:
                    LogWarning(submitContext, nameof(HandleSubmitResponse), $"Submission returned {result.Response.StatusCode} : {itemTypeName} NOT submitted because the connector was not found.");
                    submitContext.SubmitResult.SubmitStatus = SubmitResult.Status.ConnectorNotFound;
                    break;
                default:
                    LogWarning(submitContext, nameof(HandleSubmitResponse),
                        $"Submission returned {result.Response.StatusCode} : {itemTypeName} NOT submitted.");

                    throw new HttpOperationException(submitContext.LogPrefix() +
                                    $"Submission returned {result.Response.StatusCode} : NOT submitted. " +
                                    $"Http Content: {await result.Response.Content.ReadAsStringAsync().ConfigureAwait(false)}");
            }
        }
    }
}
