
using Microsoft.Rest;
using Moq;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using System.Net;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.SubmitPipeline
{
    public class HttpSubmitAggregationPipelineElementTests
    {
        private readonly Mock<ISubmission> _mockSubmission = new Mock<ISubmission>();
        private readonly Mock<IApiClient> _mockClient = new Mock<IApiClient>();
        private readonly Mock<IApiClientFactory> _mockClientFactory = new Mock<IApiClientFactory>();

        public HttpSubmitAggregationPipelineElementTests()
        {
            var mockAuthenticationHelper = new Mock<IAuthenticationProvider>();
            mockAuthenticationHelper.Setup(x => x.AcquireTokenAsync(It.IsAny<AuthenticationHelperSettings>())).ReturnsAsync(
                new AuthenticationResult()
                {
                    AccessToken = Guid.NewGuid().ToString(),
                    AccessTokenType = "Bearer"
                }
            );

            _mockClientFactory.Setup(x => x.CreateAuthenticationProvider(It.IsAny<AuthenticationHelperSettings>())).Returns(mockAuthenticationHelper.Object);
            _mockClientFactory.Setup(x => x.CreateApiClient(It.IsAny<ApiClientFactorySettings>())).Returns(_mockClient.Object);
        }

        [Fact]
        public async Task Submit_Handles_Forbidden_As_ConnectorNotFound()
        {
            _mockClient
                .Setup(x => x.POST.ApiAggregationsWithHttpMessagesAsync(
                    It.IsAny<string>(),
                    It.IsAny<AggregationSubmissionInputModel>(),
                    It.IsAny<Dictionary<string, List<string>>>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(CreateHttpOperationException(HttpStatusCode.Forbidden));

            var pipelineElement = new HttpSubmitAggregationPipelineElement(_mockSubmission.Object)
            {
                ApiClientFactory = _mockClientFactory.Object
            };

            var submitContext = new SubmitContext();

            await pipelineElement.Submit(submitContext);

            Assert.Equal(SubmitResult.Status.ConnectorNotFound, submitContext.SubmitResult.SubmitStatus);
            Assert.Equal("Submission returned Forbidden : Aggregation NOT submitted because the connector request was forbidden.", submitContext.SubmitResult.Reason);
            _mockSubmission.Verify(x => x.Submit(It.IsAny<SubmitContext>()), Times.Never);
        }

        [Fact]
        public async Task Submit_Rethrows_Unknown_HttpOperationException()
        {
            _mockClient
                .Setup(x => x.POST.ApiAggregationsWithHttpMessagesAsync(
                    It.IsAny<string>(),
                    It.IsAny<AggregationSubmissionInputModel>(),
                    It.IsAny<Dictionary<string, List<string>>>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(CreateHttpOperationException(HttpStatusCode.InternalServerError));

            var pipelineElement = new HttpSubmitAggregationPipelineElement(_mockSubmission.Object)
            {
                ApiClientFactory = _mockClientFactory.Object
            };

            await Assert.ThrowsAsync<HttpOperationException>(() => pipelineElement.Submit(new SubmitContext()));
        }

        private static HttpOperationException CreateHttpOperationException(HttpStatusCode statusCode)
        {
            return new HttpOperationException($"Operation returned an invalid status code '{statusCode}'")
            {
                Response = new HttpResponseMessageWrapper(new HttpResponseMessage(statusCode), string.Empty)
            };
        }
    }
}
