using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Rest;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Exceptions;
using RecordPoint.Connectors.SDK.Helpers;
using RecordPoint.Connectors.SDK.Providers;
using System.Net;
using static RecordPoint.Connectors.SDK.Fields;

namespace RecordPoint.Connectors.SDK.SubmitPipeline
{
    /// <summary>
    /// Pipeline element stream binary directly to blob storage
    /// </summary>
    public class DirectSubmitBinaryPipelineElement : HttpSubmitBinaryPipelineElement
    {
        /// <summary>
        /// Function which returns a CloudBlob any binaries will be saved into.
        /// Override the default for testing purposes.
        /// Note that when resolving classes from LightInject, defaults are not respected and LightInject will be unable to 
        /// resolve this class without the DefaultBlobFactory function being registered as well. 
        /// </summary>
        public Func<string, BlobClient> BlobFactory { get; set; } = DefaultBlobFactory;

        /// <summary>
        /// Circuit provider for handling backpressure for Azure Blob Storage
        /// </summary>
        public ISdkAzureBlobCircuitProvider CircuitProvider { get; set; }

        /// <summary>
        /// Retry Provider for handling backpressure for Azure Blob Storage
        /// </summary>
        public ISdkAzureBlobRetryProvider RetryProvider { get; set; }

        private readonly bool _uploadBinaryOnly = false;

        /// <summary>
        /// Constructor
        /// <param name="next"></param>
        /// <param name="uploadBinaryOnly">Indicates if the binary should be uploaded without notifying the platform of the submission</param>
        /// </summary>
        public DirectSubmitBinaryPipelineElement(ISubmission next, bool uploadBinaryOnly) : base(next)
        {
            _uploadBinaryOnly = uploadBinaryOnly;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="submitContext"></param>
        /// <returns></returns>
        public override async Task Submit(SubmitContext submitContext)
        {
            var binarySubmitContext = submitContext as BinarySubmitContext;
            ValidateFields(binarySubmitContext);

            if (!CircuitProvider.IsCircuitClosed(out _))
            {
                submitContext.SubmitResult.SubmitStatus = SubmitResult.Status.Deferred;
                submitContext.SubmitResult.Reason = "Azure blob storage back pressure circuit open";
                return;
            }

            //Create the DirectBinarySubmissionInputModel needed for the SaS token call
            var binarySubmissionInputModel = new DirectBinarySubmissionInputModel(
                binarySubmitContext.ConnectorConfigId.ToString(),
                binarySubmitContext.ItemExternalId,
                binarySubmitContext.ExternalId,
                sourceLastModifiedDate: binarySubmitContext.SourceLastModifiedDate,
                fileSize: binarySubmitContext.Stream.Length,
                fileName: binarySubmitContext.FileName,
                fileHash: binarySubmitContext.FileHash,
                mimeType: binarySubmitContext.MimeType ?? binarySubmitContext.SourceMetaData?.FirstOrDefault(metaInfo => metaInfo.Name == Fields.MimeType)?.Value,
                correlationId: binarySubmitContext.CorrelationId.ToString(),
                isOldVersion: binarySubmitContext.IsOldVersion ?? default,
                skipEnrichment: binarySubmitContext.SkipEnrichment ?? default,
                itemSourceLastModifiedDate: binarySubmitContext.ItemSourceLastModifiedDate
            );

            // Get token and URL via Autorest-generated API call
            var apiClient = ApiClientFactory.CreateApiClient(submitContext.ApiClientFactorySettings);
            var authHelper = ApiClientFactory.CreateAuthenticationProvider(submitContext.AuthenticationHelperSettings);
            var retryPolicy = GetRetryPolicy(binarySubmitContext);

            var result = await retryPolicy.ExecuteAsync(
                async (ct) =>
                {
                    var headers = await authHelper.GetHttpRequestHeaders(submitContext.AuthenticationHelperSettings).ConfigureAwait(false);
                    return await apiClient.POST.ApiBinariesGetSASTokenWithHttpMessagesAsync(
                        body: binarySubmissionInputModel,
                        customHeaders: headers,
                        cancellationToken: ct
                    ).ConfigureAwait(false);
                },
                submitContext.CancellationToken
            ).ConfigureAwait(false);

            if (result.Response.StatusCode == HttpStatusCode.MethodNotAllowed)
            {
                // Direct binary submission is disabled, fall back to old submission method
                await base.Submit(submitContext).ConfigureAwait(false);
                return;
            }

            if (await HandleSubmitResponse(submitContext, result, "Binary").ConfigureAwait(false))
            {
                if (!await InnerHandleSuccessfulSubmissionAsync(submitContext, result, binarySubmissionInputModel, apiClient, retryPolicy))
                {
                    return;
                }
            }
            else
            {
                return;
            }

            await InvokeNext(submitContext).ConfigureAwait(false);
        }

        /// <summary>
        /// Handles the result of a successful submission and determines if the pipeline should continue
        /// </summary>
        /// <param name="submitContext"></param>
        /// <param name="result"></param>
        /// <param name="binarySubmissionInputModel"></param>
        /// <param name="apiClient"></param>
        /// <param name="retryPolicy"></param>
        /// <returns></returns>
        /// <exception cref="HttpOperationException"></exception>
        private async Task<bool> InnerHandleSuccessfulSubmissionAsync(SubmitContext submitContext, HttpOperationResponse<object> result, DirectBinarySubmissionInputModel binarySubmissionInputModel, IApiClient apiClient, Polly.Retry.AsyncRetryPolicy retryPolicy)
        {
            var binarySubmitContext = submitContext as BinarySubmitContext;
            var response = result.Body as DirectBinarySubmissionResponseModel;

            if (binarySubmitContext.Stream.CanSeek && binarySubmitContext.Stream.Length > response.MaxFileSize)
            {
                // We want to skip submission if the binary is too large. The CanSeek is to prevent NotSupportedException if
                // we attempt to get the length of an unseekable stream.
                // If we cannot seek the stream, we assume it's under the maxFileSize and allow submission.
                submitContext.SubmitResult.SubmitStatus = SubmitResult.Status.Skipped;
                submitContext.SubmitResult.Reason = $"Binary file length {binarySubmitContext.Stream.Length} exceeds the maximum permitted";
                return false;
            }

            if (!binarySubmitContext.Stream.CanSeek)
            {
                //TODO: Log that submission is was allowed to proceed since size could not be determined.
            }

            // Retrieve reference to a blob. Use the DefaultBlobFactory if the BlobFactory on the pipeline element has not been set
            var blockBlob = BlobFactory != null ? BlobFactory(response.Url) : DefaultBlobFactory(response.Url);

            // If catch TooManyRequestsException, make it return a TooManyRequests Status
            try
            {
                await RetryProvider.ExecuteWithRetry(
                //Upload to blob
                async () =>
                {
                    await blockBlob.UploadAsync(binarySubmitContext.Stream, new BlobHttpHeaders()
                    {
                        ContentType = "application/octet-stream"
                    }, cancellationToken: binarySubmitContext.CancellationToken).ConfigureAwait(false);
                },
                GetType(),
                nameof(Submit)).ConfigureAwait(false);
            }
            catch (TooManyRequestsException ex)
            {
                submitContext.SubmitResult.SubmitStatus = SubmitResult.Status.TooManyRequests;
                submitContext.SubmitResult.WaitUntilTime = ex.WaitUntilTime;
                submitContext.SubmitResult.Reason = ex.Message;
                submitContext.SubmitResult.Exception = ex;
                return false;
            }

            if (!string.IsNullOrWhiteSpace(binarySubmitContext.FileName))
            {
                var metaData = new Dictionary<string, string>()
                {
                    { MetaDataKeys.ItemBinary_FileName, EscapeBlobMetaDataValue(binarySubmitContext.FileName) },
                    { MetaDataKeys.ItemBinary_CorrelationId, EscapeBlobMetaDataValue(binarySubmitContext.CorrelationId.ToString()) }
                };

                // If catch TooManyRequestsException, make it return a TooManyRequests Status
                try
                {
                    await RetryProvider.ExecuteWithRetry(
                    async () =>
                    {
                        await blockBlob.SetMetadataAsync(metaData, cancellationToken: binarySubmitContext.CancellationToken).ConfigureAwait(false);
                    },
                    GetType(),
                    nameof(Submit)).ConfigureAwait(false);
                }
                catch (TooManyRequestsException ex)
                {
                    submitContext.SubmitResult.SubmitStatus = SubmitResult.Status.TooManyRequests;
                    submitContext.SubmitResult.WaitUntilTime = ex.WaitUntilTime;
                    submitContext.SubmitResult.Reason = ex.Message;
                    submitContext.SubmitResult.Exception = ex;
                    return false;
                }

            }

            if (_uploadBinaryOnly) {
                //If the connector is configured to perform synchronous record sumissions
                //We do not need to notify the platform of the successful upload
                //As the binaries will be included as part of the record submission
                return true;
            }
            
            var authHelper = ApiClientFactory.CreateAuthenticationProvider(submitContext.AuthenticationHelperSettings);

            var notifyResult = await retryPolicy.ExecuteAsync(
                async (ct) =>
                {
                    //If the item corresponding to the submitted binary is not yet present, the platform will have to handle this.
                    var headers = await authHelper.GetHttpRequestHeaders(submitContext.AuthenticationHelperSettings).ConfigureAwait(false);
                    return await apiClient.POST.ApiBinariesNotifyBinarySubmissionWithHttpMessagesAsync(
                        body: binarySubmissionInputModel,
                        customHeaders: headers,
                        cancellationToken: ct
                    ).ConfigureAwait(false);
                },
                submitContext.CancellationToken
            ).ConfigureAwait(false);

            if (notifyResult.Response.StatusCode != HttpStatusCode.OK)
            {
                var notificationStatusCode = "<No Status Code>";
                if (result.Response != null)
                {
                    notificationStatusCode = result.Response.StatusCode.ToString();
                }
                // An issue with notification occurred, so we must throw
                throw new HttpOperationException(submitContext.LogPrefix() +
                        $"Submission returned {notificationStatusCode} : Notification of binary submission failed.");
            }

            return true;
        }

        //TODO: This is currently being duplicated here and in the HttpSubmitBinaryPipelineElementBase. We should probably just use the same in both.
        //Could move to the SubmitPipelineElementBase but feels messy having binary specific validation in there. An intermediary, perhaps?
        private static void ValidateFields(BinarySubmitContext submitContext)
        {
            if (submitContext.Stream == null)
            {
                // If we cannot seek on the stream, we cannot confirm that the stream length is less than the maximum allowed
                throw new ValidationException("Provided binary stream cannot seek, and so cannot confirm if stream is below maximum allowed binary length.", nameof(submitContext.Stream));
            }

            if (submitContext.Stream.CanSeek && submitContext.Stream.Length == 0)
            {
                // The stream must have a non-zero amount of data.
                // We only check this on seekable streams, as checking the length of an unseekable stream throws a NotSupportedException
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

            if (submitContext.ConnectorConfigId == Guid.Empty)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, nameof(submitContext.ConnectorConfigId));
            }
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/rest/api/storageservices/Setting-and-Retrieving-Properties-and-Metadata-for-Blob-Resources#Subheading1
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string EscapeBlobMetaDataValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ValidationException("The value was NULL");
            }

            // URL reserved characters are not permitted - escape them
            var result = Uri.EscapeDataString(value);

            if (result.Length > 8096)
            {
                throw new ValidationException($"Escaped value {result} is greater than 8096 characters.");
            }

            return result;
        }

        /// <summary>
        /// Default blob factory used for Production workloads
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static BlobClient DefaultBlobFactory(string url)
        {
            ValidationHelper.ArgumentNotNullOrWhiteSpace(url, nameof(url));
            var blobClientOptions = new BlobClientOptions();
            blobClientOptions.Retry.MaxRetries = 0;
            var blockBlob = new BlobClient(new Uri(url), options: blobClientOptions);
            return blockBlob;
        }
    }
}
