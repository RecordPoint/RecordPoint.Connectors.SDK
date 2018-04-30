﻿using Microsoft.Rest;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.SubmitPipeline
{
    /// <summary>
    /// A submit pipeline element that submits audit events via Records365 vNext Connector API.
    /// </summary>
    public class HttpSubmitAuditEventPipelineElement : HttpSubmitPipelineElementBase
    {
        public HttpSubmitAuditEventPipelineElement(ISubmission next) : base(next)
        {
        }

        public override async Task Submit(SubmitContext submitContext)
        {
            // Submit via HTTP API Client that is generated with AutoRest
            var apiClient = ApiClientFactory.CreateApiClient(submitContext.ApiClientFactorySettings);
            Func<string, DateTime> parseDateTime = (string value) =>
            {
                DateTime result;
                return !string.IsNullOrEmpty(value) && DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out result) ? result : DateTime.UtcNow;
            };

            var auditEventModel = new ConnectorAuditEventModel()
            {
                EventExternalId = submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.AuditEvent.EventExternalId)?.Value ?? "",
                ItemExternalId = submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.AuditEvent.ExternalId)?.Value ?? "",
                CreatedDate = parseDateTime(submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.AuditEvent.Created)?.Value),
                Description = submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.AuditEvent.Description)?.Value ?? "",
                EventType = submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.AuditEvent.EventType)?.Value ?? "",
                UserName = submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.AuditEvent.UserName)?.Value ?? "",
                UserId = submitContext.CoreMetaData?.FirstOrDefault(metadata => metadata.Name == Fields.AuditEvent.UserId)?.Value ?? "",
                ConnectorId = submitContext.ConnectorConfigId.ToString(),
                SourceProperties = new List<SubmissionMetaDataModel>(),
            };

            if (submitContext.SourceMetaData != null)
            {
                auditEventModel.SourceProperties = submitContext.SourceMetaData;
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
                        return await apiClient.ApiAuditEventsPutWithHttpMessagesAsync(auditEventModel, customHeaders: headers, cancellationToken: submitContext.CancellationToken)
                                              .ConfigureAwait(false);
                    }
                ).ConfigureAwait(false);

                shouldContinue = await HandleSubmitResponse(submitContext, result, "AuditEvent").ConfigureAwait(false);
            }
            catch (HttpOperationException ex)
                when (ex.Response?.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                // submitted item already exists!  Nothing to do but continue with the submission pipeline
                LogVerbose(submitContext, nameof(Submit), $"Submission returned {ex.Response.StatusCode} : AuditEvent already submitted.");
            }

            if (shouldContinue)
            {
                await InvokeNext(submitContext).ConfigureAwait(false);
            }
        }
    }
}
