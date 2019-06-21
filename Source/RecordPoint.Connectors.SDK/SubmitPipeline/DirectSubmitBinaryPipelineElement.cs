using Microsoft.Rest;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Diagnostics;
using RecordPoint.Connectors.SDK.Exceptions;
using RecordPoint.Connectors.SDK.Helpers;
using RecordPoint.Connectors.SDK.Providers;
using System;
using System.Net;
using System.Threading.Tasks;
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
        public Func<string, ICloudBlob> BlobFactory { get; set; } = DefaultBlobFactory;

        /// <summary>
        /// Circuit breaker for handling backpressure for Azure Blob Storage
        /// </summary>
        public AzureBlobRetryProviderWithCircuitBreaker CircuitBreaker { get; set; }
        
        /// <summary>
        /// Constructor
        /// <param name="next"></param>
        public DirectSubmitBinaryPipelineElement(ISubmission next) : base(next)
        {
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
            
            if (!CircuitBreaker.IsCircuitClosed(out var tmp))
            {
                submitContext.SubmitResult.SubmitStatus = SubmitResult.Status.Deferred;
                return;
            }

            //Create the DirectBinarySubmissionInputModel needed for the SaS token call
            var binarySubmissionInputModel = new DirectBinarySubmissionInputModel(
                binarySubmitContext.ConnectorConfigId.ToString(),
                binarySubmitContext.ItemExternalId,
                binarySubmitContext.ExternalId,
                fileSize: binarySubmitContext.Stream.Length,
                fileName: binarySubmitContext.FileName
            );

            // Get token and URL via Autorest-generated API call
            var apiClient = ApiClientFactory.CreateApiClient(submitContext.ApiClientFactorySettings);
            var retryPolicy = GetRetryPolicy(binarySubmitContext);

            var result = await retryPolicy.ExecuteAsync(
                async (ct) =>
                {
                    var authHelper = ApiClientFactory.CreateAuthenticationHelper();
                    var headers = await authHelper.GetHttpRequestHeaders(submitContext.AuthenticationHelperSettings).ConfigureAwait(false);
                    return await apiClient.ApiBinariesGetSASTokenPostWithHttpMessagesAsync(
                        binarySubmissionInputModel: binarySubmissionInputModel,
                        customHeaders: headers,
                        cancellationToken: ct
                    ).ConfigureAwait(false);
                },
                submitContext.CancellationToken
            ).ConfigureAwait(false);

            if(result.Response.StatusCode == HttpStatusCode.MethodNotAllowed)
            {
                // Direct binary submission is disabled, fall back to old submission method
                await base.Submit(submitContext).ConfigureAwait(false);
                return;
            }

            if (await HandleSubmitResponse(submitContext, result, "Binary").ConfigureAwait(false))
            {
                var response = result.Body as DirectBinarySubmissionResponseModel;

                if (binarySubmitContext.Stream.CanSeek && binarySubmitContext.Stream.Length > response.MaxFileSize)
                {
                    // We want to skip submission if the binary is too large. The CanSeek is to prevent NotSupportedException if
                    // we attempt to get the length of an unseekable stream.
                    // If we cannot seek the stream, we assume it's under the maxFileSize and allow submission.
                    submitContext.SubmitResult.SubmitStatus = SubmitResult.Status.Skipped;
                    return;
                }
                else if (!binarySubmitContext.Stream.CanSeek)
                {
                    //TODO: Log that submission is was allowed to proceed since size could not be determined.
                }

                // Retrieve reference to a blob. Use the DefaultBlobFactory if the BlobFactory on the pipeline element has not been set
                var blockBlob = BlobFactory != null ? BlobFactory(response.Url) : DefaultBlobFactory(response.Url);
                // Set Blob ContentType
                blockBlob.Properties.ContentType = "application/octet-stream";

                // If catch TooManyRequestsException, make it return a TooManyRequests Status
                try
                {
                    await CircuitBreaker.ExecuteWithRetry(
                    blockBlob.ServiceClient,
                    //Upload to blob
                    async () =>
                    {
                        await blockBlob.UploadFromStreamAsync(binarySubmitContext.Stream, binarySubmitContext.CancellationToken).ConfigureAwait(false);
                    },
                    GetType(),
                    nameof(Submit)).ConfigureAwait(false);
                }
                catch (TooManyRequestsException ex)
                {
                    submitContext.SubmitResult.SubmitStatus = SubmitResult.Status.TooManyRequests;
                    submitContext.SubmitResult.WaitUntilTime = ex.WaitUntilTime;
                    return;
                }
                
                if (!string.IsNullOrWhiteSpace(binarySubmitContext.FileName))
                {
                    blockBlob.Metadata[MetaDataKeys.ItemBinary_FileName] = EscapeBlobMetaDataValue(binarySubmitContext.FileName);

                    // If catch TooManyRequestsException, make it return a TooManyRequests Status
                    try
                    {
                        await CircuitBreaker.ExecuteWithRetry(
                        blockBlob.ServiceClient,
                        async () =>
                        {
                            await blockBlob.SetMetadataAsync(binarySubmitContext.CancellationToken).ConfigureAwait(false);
                        },
                        GetType(),
                        nameof(Submit)).ConfigureAwait(false);
                    }
                    catch(TooManyRequestsException ex)
                    {
                        submitContext.SubmitResult.SubmitStatus = SubmitResult.Status.TooManyRequests;
                        submitContext.SubmitResult.WaitUntilTime = ex.WaitUntilTime;
                        return;
                    }
                    
                }

                var notifyResult = await retryPolicy.ExecuteAsync(
                    async (ct) =>
                    {
                        //If the item corresponding to the submitted binary is not yet present, the platform will have to handle this.
                        var authHelper = ApiClientFactory.CreateAuthenticationHelper();
                        var headers = await authHelper.GetHttpRequestHeaders(submitContext.AuthenticationHelperSettings).ConfigureAwait(false);
                        return await apiClient.ApiBinariesNotifyBinarySubmissionPostWithHttpMessagesAsync(
                            binarySubmissionInputModel: binarySubmissionInputModel,
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
                    // An issue with notification occured, so we must throw
                    throw new HttpOperationException(submitContext.LogPrefix() +
                            $"Submission returned {notificationStatusCode} : Notification of binary submission failed.");
                }

            }
            else
            {
                return;
            }

            await InvokeNext(submitContext).ConfigureAwait(false);
        }

        //TODO: This is currently being duplicated here and in the HttpSubmitBinaryPipelineElementBase. We should probably just use the same in both.
        //Could move to the SubmitPipelineElementBase but feels messy having binary specific validation in there. An intermediary, perhaps?
        private void ValidateFields(BinarySubmitContext submitContext)
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

            if (submitContext.ConnectorConfigId == default(Guid))
            {
                throw new ValidationException(ValidationRules.CannotBeNull, nameof(submitContext.ConnectorConfigId));
            }
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/rest/api/storageservices/Setting-and-Retrieving-Properties-and-Metadata-for-Blob-Resources#Subheading1
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string EscapeBlobMetaDataValue(string value)
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
        public static ICloudBlob DefaultBlobFactory(string url)
        {
            ValidationHelper.ArgumentNotNullOrWhiteSpace(url, nameof(url));
            //Example of CloudBlockBlob with a SaS token: https://docs.microsoft.com/en-us/azure/storage/common/storage-dotnet-shared-access-signature-part-1
            var blockBlob = new CloudBlockBlob(new Uri(url));

            return blockBlob;
        }
    }
}
