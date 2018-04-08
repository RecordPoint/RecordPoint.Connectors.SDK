﻿using Microsoft.Rest;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.SubmitPipeline
{
    /// <summary>
    /// A submit pipeline element that submits aggregations via Records365 vNext Connector API.
    /// </summary>
    public class HttpSubmitAggregationPipelineElement
        : HttpSubmitPipelineElementBase
    {
        public HttpSubmitAggregationPipelineElement(ISubmission next)
            : base(next)
        {
        }



        public async override Task Submit(SubmitContext submitContext)
        {
            // Submit via HTTP API Client that is generated with AutoRest
            var apiClient = ApiClientFactory.CreateApiClient(submitContext.ApiClientFactorySettings);

            Func<string, DateTime> parseDateTime = (string value) =>
            {
                DateTime result;
                return !string.IsNullOrEmpty(value) && DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out result) ? result : DateTime.UtcNow;
            };

            var aggregationModel = new AggregationSubmissionInputModel()
            {
                ItemTypeId = submitContext.ItemTypeId,
                ExternalId = submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.ExternalId)?.Value ?? "",
                Title = submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.Title)?.Value ?? "",
                Author = submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.Author)?.Value ?? "",
                SourceLastModifiedDate = parseDateTime(submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.SourceLastModifiedDate)?.Value),
                SourceCreatedDate = parseDateTime(submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.SourceCreatedDate)?.Value),
                SourceLastModifiedBy = submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.SourceLastModifiedBy)?.Value ?? "",
                SourceCreatedBy = submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.SourceCreatedBy)?.Value ?? "",
                ConnectorId = submitContext.ConnectorConfigId.ToString(),
                Location = submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.Location)?.Value ?? "",
                MediaType = submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.MediaType)?.Value ?? "Electronic",
                BarcodeType = submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.BarcodeType)?.Value ?? "",
                BarcodeValue = submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.BarcodeValue)?.Value ?? "",
                SourceProperties = new List<SubmissionMetaDataModel>(),
            };

            if (submitContext.SourceMetaData != null)
            {
                aggregationModel.SourceProperties = submitContext.SourceMetaData;
            }

            var shouldContinue = true;

            try
            {
                var policy = ApiClientRetryPolicy.GetPolicy(4, 2000, submitContext.CancellationToken);

                var result = await policy.ExecuteAsync(
                    async () =>
                    {
                        var authHelper = ApiClientFactory.CreateAuthenticationHelper();
                        var headers = await authHelper.GetHttpRequestHeaders(submitContext.AuthenticationHelperSettings).ConfigureAwait(false);
                        return await apiClient.ApiAggregationsPostWithHttpMessagesAsync(aggregationModel, customHeaders: headers, cancellationToken: submitContext.CancellationToken)
                                              .ConfigureAwait(false);
                    })
                    .ConfigureAwait(false);

                shouldContinue = await HandleSubmitResponse(submitContext, result, "Aggregation").ConfigureAwait(false);
            }
            catch (HttpOperationException ex)
                when (ex.Response?.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                // submitted item already exists!  Nothing to do but continue with the submission pipeline
                LogVerbose(submitContext, nameof(Submit), $"Submission returned {ex.Response.StatusCode} : Aggregation already submitted.");
            }

            if (shouldContinue)
            {
                await InvokeNext(submitContext).ConfigureAwait(false);
            }
        }
    }
}
