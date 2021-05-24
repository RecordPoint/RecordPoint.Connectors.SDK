using Microsoft.Rest;
using RecordPoint.Connectors.SDK.Helpers;
using System;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.SubmitPipeline
{
    /// <summary>
    /// A submit pipeline element that submits item binaries via Records365 vNext Connector API.
    /// </summary>
    [Obsolete("Please use DirectSubmitBinaryPipelineElement instead (if possible to do so) ", false)]
    
    public class HttpSubmitBinaryPipelineElement : HttpSubmitPipelineElementBase
    {
        /// <summary>
        /// Constructs a new HttpSubmitBinaryPipelineElement with an optional next submit
        /// pipeline element.
        /// </summary>
        /// <param name="next"></param>
        public HttpSubmitBinaryPipelineElement(ISubmission next)
            : base(next)
        {
        }

        private void ValidateFields(BinarySubmitContext submitContext)
        {
            if (submitContext.Stream.CanSeek)
            {
                // Validate that the stream is not empty only when Stream CanSeek, 
                // otherwise will throw NotSupportedException
                // If not CanSeek, we have to rely on out API to do Length check.
                if (submitContext.Stream.Length == 0)
                {
                    // Use the same validation style as we see in the autorest generated code
                    // (e.g., ItemSubmissionInputModel.Validate(), which is called on every submit)
                    throw new ValidationException(ValidationRules.MinLength, nameof(submitContext.Stream));
                }
            }

            if (string.IsNullOrEmpty(submitContext.ItemExternalId))
            {
                throw new ValidationException(ValidationRules.CannotBeNull, nameof(submitContext.ItemExternalId));
            }

            if (string.IsNullOrEmpty(submitContext.ExternalId))
            {
                throw new ValidationException(ValidationRules.CannotBeNull, nameof(submitContext.ExternalId));
            }

            if (submitContext.ConnectorConfigId == default(Guid))
            {
                throw new ValidationException(ValidationRules.CannotBeNull, nameof(submitContext.ConnectorConfigId));
            }
        }

        /// <summary>
        /// Submits an item binary via the Records365 vNext Connector API.
        /// </summary>
        /// <param name="submitContext"></param>
        /// <returns></returns>
        public override async Task Submit(SubmitContext submitContext)
        {
            var binarySubmitContext = submitContext as BinarySubmitContext;

            ValidateFields(binarySubmitContext);

            // Submit via HTTP API Client that is generated with AutoRest
            var apiClient = ApiClientFactory.CreateApiClient(submitContext.ApiClientFactorySettings);
            var result = await GetRetryPolicy(submitContext).ExecuteAsync(
                async (ct) =>
                {
                    // In case a stream is reused during submission retry, it might not be in 0 Position
                    // since the previous submission already read to the end. We should reset this value.
                    if (binarySubmitContext.Stream.CanSeek)
                    {
                        binarySubmitContext.Stream.Position = 0;
                    }

                    var authHelper = ApiClientFactory.CreateAuthenticationHelper();
                    var headers = await authHelper.GetHttpRequestHeaders(submitContext.AuthenticationHelperSettings).ConfigureAwait(false);
                    return await apiClient.ApiBinariesPostWithHttpMessagesAndStreamAsync(
                        binarySubmitContext.ConnectorConfigId.ToString(),
                        binarySubmitContext.ItemExternalId,
                        binarySubmitContext.ExternalId,
                        binarySubmitContext.FileName,
                        binarySubmitContext.CorrelationId.ToString(),
                        inputStream: binarySubmitContext.Stream,
                        customHeaders: headers, 
                        cancellationToken: ct
                    ).ConfigureAwait(false);
                }, 
                binarySubmitContext.CancellationToken
            ).ConfigureAwait(false);

            var shouldContinue = true;
            shouldContinue = await HandleSubmitResponse(submitContext, result, "Binary").ConfigureAwait(false);

            if (shouldContinue)
            {
                await InvokeNext(submitContext).ConfigureAwait(false);
            }
        }
    }
}
