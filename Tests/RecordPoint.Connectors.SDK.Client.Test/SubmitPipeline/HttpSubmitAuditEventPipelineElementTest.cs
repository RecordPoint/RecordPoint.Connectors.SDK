
using Microsoft.Rest;
using Moq;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using System.Net;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.SubmitPipeline
{
    public class HttpSubmitAuditEventPipelineElementTest
    {
        private readonly Mock<ISubmission> _next = new();
        private readonly Mock<IApiClient> _mockClient = new();
        private readonly Mock<IApiClientFactory> _mockClientFactory = new();
        private readonly HttpSubmitAuditEventPipelineElement _element;

        public HttpSubmitAuditEventPipelineElementTest()
        {
            var mockAuth = new Mock<IAuthenticationProvider>();
            mockAuth.Setup(x => x.AcquireTokenAsync(It.IsAny<AuthenticationHelperSettings>()))
                .ReturnsAsync(new AuthenticationResult { AccessToken = "token", AccessTokenType = "Bearer" });

            _mockClientFactory.Setup(x => x.CreateApiClient(It.IsAny<ApiClientFactorySettings>())).Returns(_mockClient.Object);
            _mockClientFactory.Setup(x => x.CreateAuthenticationProvider(It.IsAny<AuthenticationHelperSettings>())).Returns(mockAuth.Object);

            _element = new HttpSubmitAuditEventPipelineElement(_next.Object)
            {
                ApiClientFactory = _mockClientFactory.Object
            };
        }

        private void SetupAuditEventResponse(HttpStatusCode statusCode)
        {
            _mockClient.Setup(x => x.PUT.ApiAuditEventsWithHttpMessagesAsync(
                It.IsAny<string>(),
                It.IsAny<ConnectorAuditEventModel>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpOperationResponse<ErrorResponseModel>
                {
                    Response = new HttpResponseMessage(statusCode)
                });
        }

        private static SubmitContext GetSubmitContext()
        {
            return new SubmitContext
            {
                ConnectorConfigId = Guid.NewGuid(),
                CoreMetaData = new List<SubmissionMetaDataModel>
                {
                    new() { Name = Fields.AuditEvent.EventExternalId, Type = nameof(String), Value = "event-1" },
                    new() { Name = Fields.AuditEvent.ExternalId, Type = nameof(String), Value = "item-1" },
                    new() { Name = Fields.AuditEvent.Created, Type = nameof(DateTime), Value = DateTime.UtcNow.ToString("O") },
                    new() { Name = Fields.AuditEvent.Description, Type = nameof(String), Value = "desc" },
                    new() { Name = Fields.AuditEvent.EventType, Type = nameof(String), Value = "type" },
                    new() { Name = Fields.AuditEvent.UserName, Type = nameof(String), Value = "user" },
                    new() { Name = Fields.AuditEvent.UserId, Type = nameof(String), Value = "user-id" }
                },
                SourceMetaData = new List<SubmissionMetaDataModel>
                {
                    new() { Name = "source", Type = nameof(String), Value = "value" }
                }
            };
        }

        [Fact]
        public async Task Submit_Continues_OnOk()
        {
            SetupAuditEventResponse(HttpStatusCode.OK);

            var context = GetSubmitContext();
            await _element.Submit(context);

            _next.Verify(x => x.Submit(context), Times.Once);
        }

        [Fact]
        public async Task Submit_Handles_Conflict_Thrown_AsAlreadySubmitted()
        {
            _mockClient.Setup(x => x.PUT.ApiAuditEventsWithHttpMessagesAsync(
                It.IsAny<string>(),
                It.IsAny<ConnectorAuditEventModel>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<CancellationToken>()))
                .ThrowsAsync(new HttpOperationException("conflict")
                {
                    Response = new HttpResponseMessageWrapper(new HttpResponseMessage(HttpStatusCode.Conflict), string.Empty)
                });

            var context = GetSubmitContext();
            await _element.Submit(context);

            _next.Verify(x => x.Submit(context), Times.Once);
        }

        [Fact]
        public async Task Submit_DoesNotContinue_OnBadRequest()
        {
            SetupAuditEventResponse(HttpStatusCode.BadRequest);

            var context = GetSubmitContext();

            var act = async () => await _element.Submit(context);

            // BadRequest with no ErrorResponseModel body throws to encourage dead-lettering.
            await Assert.ThrowsAsync<HttpOperationException>(act);
            _next.Verify(x => x.Submit(It.IsAny<SubmitContext>()), Times.Never);
        }
    }
}
