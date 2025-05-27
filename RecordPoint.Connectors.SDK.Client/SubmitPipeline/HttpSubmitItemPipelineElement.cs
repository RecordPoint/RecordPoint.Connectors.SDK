using Microsoft.Rest;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Helpers;
using System.Globalization;

namespace RecordPoint.Connectors.SDK.SubmitPipeline
{
    /// <summary>
    /// A submit pipeline element that submits items via Records365 vNext Connector API.
    /// </summary>
    public class HttpSubmitItemPipelineElement
        : HttpSubmitPipelineElementBase
    {
        /// <summary>
        /// Constructs a new HttpSubmitItemPipelineElement with an optional next submit
        /// pipeline element.
        /// </summary>
        /// <param name="next"></param>
        public HttpSubmitItemPipelineElement(ISubmission next)
            : base(next)
        {
        }

        /// <summary>
        /// Submits an item via the Records365 vNext Connector API.
        /// </summary>
        /// <param name="submitContext"></param>
        /// <returns></returns>
        public override async Task Submit(SubmitContext submitContext)
        {
            // Submit via HTTP API Client that is generated with AutoRest
            var apiClient = ApiClientFactory.CreateApiClient(submitContext.ApiClientFactorySettings);
            var authHelper = ApiClientFactory.CreateAuthenticationProvider(submitContext.AuthenticationHelperSettings);


            static DateTime parseDateTime(string value)
            {
                return !string.IsNullOrEmpty(value) && DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out DateTime result) ? result : DateTime.UtcNow;
            }

            var itemModel = new ItemSubmissionInputModel()
            {
                ExternalId = submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.ExternalId)?.Value ?? "",
                Title = submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.Title)?.Value ?? "",
                Author = submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.Author)?.Value ?? "",
                MimeType = submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.MimeType)?.Value ?? "",
                SourceLastModifiedDate = parseDateTime(submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.SourceLastModifiedDate)?.Value),
                SourceLastModifiedBy = submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.SourceLastModifiedBy)?.Value ?? "",
                SourceCreatedDate = parseDateTime(submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.SourceCreatedDate)?.Value),
                SourceCreatedBy = submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.SourceCreatedBy)?.Value ?? "",
                ContentVersion = submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.ContentVersion)?.Value ?? "",
                Location = submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.Location)?.Value ?? "",
                MediaType = submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.MediaType)?.Value ?? "Electronic",
                ConnectorId = submitContext.ConnectorConfigId.ToString(),
                ParentExternalId = submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.ParentExternalId)?.Value ?? "",
                BarcodeType = submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.BarcodeType)?.Value ?? "",
                BarcodeValue = submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.BarcodeValue)?.Value ?? "",
                SecurityProfileIdentifier = submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.SecurityProfileIdentifier)?.Value ?? "",
                CorrelationId = submitContext.CorrelationId.ToString(),
                SourceProperties = new List<SubmissionMetaDataModel>(),
                Relationships = new List<RelationshipDataModel>()
            };

            if (submitContext is ItemSubmitContext context)
            {
                itemModel.BinariesSubmitted = context.BinariesSubmitted;
            }

            if (submitContext.SourceMetaData != null)
            {
                itemModel.SourceProperties = submitContext.SourceMetaData;
            }

            if (submitContext.Relationships != null)
            {
                itemModel.Relationships = submitContext.Relationships;
            }

            var shouldContinue = true;

            try
            {
                var result = await GetRetryPolicy(submitContext).ExecuteAsync(
                    async (ct) =>
                    {
                        var headers = await authHelper.GetHttpRequestHeaders(submitContext.AuthenticationHelperSettings).ConfigureAwait(false);
                        return await apiClient.POST.ApiItemsWithHttpMessagesAsync(
                            body: itemModel,
                            customHeaders: headers,
                            cancellationToken: ct
                        ).ConfigureAwait(false);
                    },
                    submitContext.CancellationToken
                ).ConfigureAwait(false);

                shouldContinue = await HandleSubmitResponse(submitContext, result, "Item").ConfigureAwait(false);

                if (result.Body is ItemAcceptanceModel resultBody)
                {
                    if (!string.IsNullOrEmpty(resultBody.AggregationStatus) &&
                        resultBody.AggregationStatus.ToLower() == "found")
                    {
                        submitContext.AggregationFoundDuringItemSubmission = true;
                    }
                    else
                    {
                        submitContext.AggregationFoundDuringItemSubmission = false;
                    }
                }
            }
            catch (HttpOperationException ex)
                when (ex.Response?.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                // Submitted item already exists! Nothing to do but continue with the submission pipeline
                LogVerbose(submitContext, nameof(Submit), $"Submission returned {ex.Response.StatusCode} : Item already submitted.");
            }

            if (shouldContinue)
            {
                await InvokeNext(submitContext).ConfigureAwait(false);
            }
        }
    }
}
