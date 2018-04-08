using Microsoft.Rest;
using RecordPoint.Connectors.SDK.Client;
using System;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.SubmitPipeline
{
    public class HttpSubmitBinaryPipelineElement : HttpSubmitPipelineElementBase
    {
        public HttpSubmitBinaryPipelineElement(ISubmission next)
            : base(next)
        {
        }

        private void ValidateFields(BinarySubmitContext submitContext)
        {
            // Validate that the stream is not empty
            if (submitContext.Stream.Length == 0)
            {
                // Use the same validation style as we see in the autorest generated code
                // (e.g., ItemSubmissionInputModel.Validate(), which is called on every submit)
                throw new ValidationException(ValidationRules.MinLength, nameof(submitContext.Stream));
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

        public async override Task Submit(SubmitContext submitContext)
        {
            var binarySubmitContext = submitContext as BinarySubmitContext;

            ValidateFields(binarySubmitContext);

            // Submit via HTTP API Client that is generated with AutoRest
            var apiClient = ApiClientFactory.CreateApiClient(submitContext.ApiClientFactorySettings);
            
            var policy = ApiClientRetryPolicy.GetPolicy(4, 2000, submitContext.CancellationToken);

            var result = await policy.ExecuteAsync(
                async () =>
                {
                    var authHelper = ApiClientFactory.CreateAuthenticationHelper();
                    var headers = await authHelper.GetHttpRequestHeaders(submitContext.AuthenticationHelperSettings).ConfigureAwait(false);
                    return await apiClient.ApiBinariesPostWithHttpMessagesAndStreamAsync(binarySubmitContext.ConnectorConfigId.ToString(),
                        binarySubmitContext.ItemExternalId,
                        binarySubmitContext.ExternalId,
                        binarySubmitContext.FileName,
                        inputStream: binarySubmitContext.Stream,
                        customHeaders: headers, 
                        cancellationToken: binarySubmitContext.CancellationToken)
                        .ConfigureAwait(false);
                }
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
