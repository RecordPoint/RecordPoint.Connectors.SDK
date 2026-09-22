
using Microsoft.Rest;
using Moq;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using static RecordPoint.Connectors.SDK.Fields;
using System.Net;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.SubmitPipeline
{
    /// <summary>
    /// Exercises the shared response-handling logic in HttpSubmitPipelineElementBase
    /// through the concrete HttpSubmitItemPipelineElement.
    /// </summary>
    public class HttpSubmitPipelineElementBaseTests
    {
        private readonly Mock<ISubmission> _next = new();
        private readonly Mock<IApiClient> _mockClient = new();
        private readonly Mock<IApiClientFactory> _mockClientFactory = new();
        private readonly HttpSubmitItemPipelineElement _element;

        public HttpSubmitPipelineElementBaseTests()
        {
            var mockAuth = new Mock<IAuthenticationProvider>();
            mockAuth.Setup(x => x.AcquireTokenAsync(It.IsAny<AuthenticationHelperSettings>()))
                .ReturnsAsync(new AuthenticationResult { AccessToken = "token", AccessTokenType = "Bearer" });

            _mockClientFactory.Setup(x => x.CreateApiClient(It.IsAny<ApiClientFactorySettings>())).Returns(_mockClient.Object);
            _mockClientFactory.Setup(x => x.CreateAuthenticationProvider(It.IsAny<AuthenticationHelperSettings>())).Returns(mockAuth.Object);

            _element = new HttpSubmitItemPipelineElement(_next.Object)
            {
                ApiClientFactory = _mockClientFactory.Object
            };
        }

        private void SetupItemResponse(HttpResponseMessage response, object body = null)
        {
            _mockClient.Setup(x => x.POST.ApiItemsWithHttpMessagesAsync(
                It.IsAny<string>(),
                It.IsAny<ItemSubmissionInputModel>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpOperationResponse<object>
                {
                    Response = response,
                    Body = body
                });
        }

        [Theory]
        [InlineData(HttpStatusCode.OK)]
        [InlineData(HttpStatusCode.Created)]
        [InlineData(HttpStatusCode.Accepted)]
        [InlineData(HttpStatusCode.NoContent)]
        [InlineData(HttpStatusCode.Conflict)]
        public async Task HandleSubmitResponse_SuccessStatuses_ContinuePipeline(HttpStatusCode statusCode)
        {
            SetupItemResponse(new HttpResponseMessage(statusCode));

            var context = new SubmitContext();
            await _element.Submit(context);

            Assert.Equal(SubmitResult.Status.OK, context.SubmitResult.SubmitStatus);
            _next.Verify(x => x.Submit(context), Times.Once);
        }

        [Fact]
        public async Task HandleSubmitResponse_PreconditionFailed_DefersAndStops()
        {
            SetupItemResponse(new HttpResponseMessage(HttpStatusCode.PreconditionFailed));

            var context = new SubmitContext();
            await _element.Submit(context);

            Assert.Equal(SubmitResult.Status.Deferred, context.SubmitResult.SubmitStatus);
            _next.Verify(x => x.Submit(It.IsAny<SubmitContext>()), Times.Never);
        }

        [Fact]
        public async Task HandleSubmitResponse_ConnectorDisabled_BadRequest_SetsConnectorDisabled()
        {
            SetupItemResponse(
                new HttpResponseMessage(HttpStatusCode.BadRequest),
                new ErrorResponseModel { Error = new ErrorModel { MessageCode = MessageCode.ConnectorNotEnabled } });

            var context = new SubmitContext();
            await _element.Submit(context);

            Assert.Equal(SubmitResult.Status.ConnectorDisabled, context.SubmitResult.SubmitStatus);
            _next.Verify(x => x.Submit(It.IsAny<SubmitContext>()), Times.Never);
        }

        [Fact]
        public async Task HandleSubmitResponse_ProtectionNotEnabled_BadRequest_SetsSkipped()
        {
            SetupItemResponse(
                new HttpResponseMessage(HttpStatusCode.BadRequest),
                new ErrorResponseModel { Error = new ErrorModel { MessageCode = MessageCode.ProtectionNotEnabled } });

            var context = new SubmitContext();
            await _element.Submit(context);

            Assert.Equal(SubmitResult.Status.Skipped, context.SubmitResult.SubmitStatus);
            _next.Verify(x => x.Submit(It.IsAny<SubmitContext>()), Times.Never);
        }

        [Fact]
        public async Task HandleSubmitResponse_GenericBadRequest_Throws()
        {
            SetupItemResponse(
                new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("invalid") },
                new ErrorResponseModel { Error = new ErrorModel { MessageCode = "SomethingElse", Message = "bad" } });

            var act = async () => await _element.Submit(new SubmitContext());

            await Assert.ThrowsAsync<HttpOperationException>(act);
            _next.Verify(x => x.Submit(It.IsAny<SubmitContext>()), Times.Never);
        }

        [Fact]
        public async Task HandleSubmitResponse_Forbidden_SetsConnectorNotFound_WithErrorDetail()
        {
            var errorJson = "{\"error\":{\"message\":\"top level\",\"innerError\":[{\"message\":\"inner detail\"}]}}";
            SetupItemResponse(new HttpResponseMessage(HttpStatusCode.Forbidden)
            {
                Content = new StringContent(errorJson)
            });

            var context = new SubmitContext();
            await _element.Submit(context);

            Assert.Equal(SubmitResult.Status.ConnectorNotFound, context.SubmitResult.SubmitStatus);
            Assert.Contains("top level", context.SubmitResult.Reason);
            Assert.Contains("inner detail", context.SubmitResult.Reason);
            _next.Verify(x => x.Submit(It.IsAny<SubmitContext>()), Times.Never);
        }

        [Fact]
        public async Task HandleSubmitResponse_TooManyRequests_UsesWaitUntilTimeHeader()
        {
            var waitUntil = DateTime.UtcNow.AddMinutes(5);
            var response = new HttpResponseMessage(HttpStatusCode.TooManyRequests);
            response.Headers.TryAddWithoutValidation("wait-until-time", waitUntil.ToString("O"));
            SetupItemResponse(response);

            var context = new SubmitContext();
            await _element.Submit(context);

            Assert.Equal(SubmitResult.Status.TooManyRequests, context.SubmitResult.SubmitStatus);
            Assert.NotNull(context.SubmitResult.WaitUntilTime);
            var diff = Math.Abs((context.SubmitResult.WaitUntilTime.Value - waitUntil).TotalSeconds);
            Assert.True(diff <= 1, $"WaitUntilTime {context.SubmitResult.WaitUntilTime} is not close to {waitUntil} (diff: {diff} seconds)");
            _next.Verify(x => x.Submit(It.IsAny<SubmitContext>()), Times.Never);
        }

        [Fact]
        public async Task HandleSubmitResponse_TooManyRequests_WithoutHeader_UsesDefaultWait()
        {
            SetupItemResponse(new HttpResponseMessage(HttpStatusCode.TooManyRequests));

            var context = new SubmitContext();
            await _element.Submit(context);

            Assert.Equal(SubmitResult.Status.TooManyRequests, context.SubmitResult.SubmitStatus);
            Assert.NotNull(context.SubmitResult.WaitUntilTime);
            Assert.True(context.SubmitResult.WaitUntilTime > DateTime.UtcNow);
        }

        [Fact]
        public async Task HandleSubmitResponse_UnexpectedStatus_Throws()
        {
            SetupItemResponse(new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("server error")
            });

            var act = async () => await _element.Submit(new SubmitContext());

            await Assert.ThrowsAsync<HttpOperationException>(act);
        }

        [Fact]
        public async Task HandleSubmitResponse_NullResponse_Throws()
        {
            SetupItemResponse(null);

            var act = async () => await _element.Submit(new SubmitContext());

            await Assert.ThrowsAsync<HttpOperationException>(act);
        }
    }
}
