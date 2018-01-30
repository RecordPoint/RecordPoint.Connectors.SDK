using Microsoft.Rest;
using RecordPoint.Connectors.Client;
using RecordPoint.Connectors.Client.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.SubmitPipeline
{
    public class HttpSubmitAuditEventPipelineElement : HttpSubmitPipelineElementBase
    {
        public IApiClientFactory ApiClientFactory { get; set; }

        public HttpSubmitAuditEventPipelineElement(ISubmission next) : base(next)
        {
        }

        public override async Task Submit(SubmitContext submitContext)
        {
            // Submit via HTTP API Client that is generated with AutoRest
            using (var apiClient = await ApiClientFactory.CreateApiClientAsync(submitContext.ApiClientFactorySettings).ConfigureAwait(false))
            {
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

                // try / catch here so I debug http auth or other issues raised by the call to the Eiger API by sticking a breakpoint in the catch
                // in release builds there's no need for it as we should just let the exception bubble up the call stack until it is handled properly
                // in the ConnectorTaskRunner
                try
                {
                    var policy = ApiClientRetryPolicy.GetPolicy(4, 2000, submitContext.CancellationToken);

                    var result = await policy.ExecuteAsync(async () => await apiClient.ApiAuditEventsPutWithHttpMessagesAsync(
                        auditEventModel).ConfigureAwait(false)
                    ).ConfigureAwait(false);

                    await HandleSubmitResponse(submitContext, result, "AuditEvent").ConfigureAwait(false);
                }
                catch (HttpOperationException ex)
                    when (ex.Response?.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    // submitted item already exists!  Nothing to do but continue with the submission pipeline
                    LogVerbose(submitContext, nameof(Submit), $"Submission returned {ex.Response.StatusCode} : AuditEvent already submitted.");
                }

                await InvokeNext(submitContext).ConfigureAwait(false);
            }
        }
    }
}
