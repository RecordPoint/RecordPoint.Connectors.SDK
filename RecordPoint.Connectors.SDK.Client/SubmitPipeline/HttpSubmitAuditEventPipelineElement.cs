using Microsoft.Rest;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Helpers;
using System.Globalization;

namespace RecordPoint.Connectors.SDK.SubmitPipeline
{
    /// <summary>
    /// A submit pipeline element that submits audit events via Records365 vNext Connector API.
    /// </summary>
    public class HttpSubmitAuditEventPipelineElement : HttpSubmitPipelineElementBase
    {
        /// <summary>
        /// Constructs a new HttpSubmitAuditEventPipelineElement with an optional next submit
        /// pipeline element.
        /// </summary>
        /// <param name="next"></param>
        public HttpSubmitAuditEventPipelineElement(ISubmission next) : base(next)
        {
        }

        /// <summary>
        /// Submits an audit event via the Records365 vNext Connector API.
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
                var result = await GetRetryPolicy(submitContext).ExecuteAsync(
                    async (ct) =>
                    {
                        var headers = await authHelper.GetHttpRequestHeaders(submitContext.AuthenticationHelperSettings).ConfigureAwait(false);
                        return await apiClient.PUT.ApiAuditEventsWithHttpMessagesAsync(
                            body: auditEventModel,
                            customHeaders: headers,
                            cancellationToken: ct
                        ).ConfigureAwait(false);
                    },
                    submitContext.CancellationToken
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
