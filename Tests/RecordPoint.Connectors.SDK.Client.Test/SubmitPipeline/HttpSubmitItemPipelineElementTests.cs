
using Microsoft.Rest;
using Moq;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using System.Net;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.SubmitPipeline
{
    public class HttpSubmitItemPipelineElementTests
    {
        private readonly Mock<ISubmission> _mockSubmission = new Mock<ISubmission>();
        private readonly Mock<IApiClient> _mockClient = new Mock<IApiClient>();
        private readonly Mock<IApiClientFactory> _mockClientFactory = new Mock<IApiClientFactory>();

        private HttpSubmitItemPipelineElement _itemPipelineElement;
        private static readonly string[] expected = new[] { "item-external-id-1" };

        public HttpSubmitItemPipelineElementTests()
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
        }

        [Fact]
        public async Task HttpSubmitItemPipelineElement_Validates_ItemSubmissionInputModel_With_BinariesSubmittedProp()
        {
            ItemSubmissionInputModel inputModelBuiltOnSubmit = null;

            _mockClient
                .Setup(x =>
                    x.POST.ApiItemsWithHttpMessagesAsync(
                        It.IsAny<string>(),
                        It.IsAny<ItemSubmissionInputModel>(),
                        It.IsAny<Dictionary<string, List<string>>>(),
                        CancellationToken.None))
                .Callback<string, ItemSubmissionInputModel, Dictionary<string, List<string>>, CancellationToken>(
                    (lan, model, headers, ct) => inputModelBuiltOnSubmit = model)
                .ReturnsAsync(() => new HttpOperationResponse<object> { Body = new { text = "test ok" }, Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK) });

            _mockClientFactory.Setup(x => x.CreateApiClient(It.IsAny<ApiClientFactorySettings>())).Returns(_mockClient.Object);

            _itemPipelineElement = new HttpSubmitItemPipelineElement(_mockSubmission.Object)
            {
                ApiClientFactory = _mockClientFactory.Object
            };

            var submitContext = new ItemSubmitContext
            {
                BinariesSubmitted = new List<DirectBinarySubmissionInputModel>
                {
                    new DirectBinarySubmissionInputModel
                    {
                        BinaryExternalId = "binary-external-id-1",
                        ItemExternalId = "item-external-id-1"
                    },
                    new DirectBinarySubmissionInputModel
                    {
                        BinaryExternalId = "binary-external-id-2",
                        ItemExternalId = "item-external-id-1"
                    },
                    new DirectBinarySubmissionInputModel
                    {
                        BinaryExternalId = "binary-external-id-3",
                        ItemExternalId = "item-external-id-1"
                    },
                }
            };

            await _itemPipelineElement.Submit(submitContext);

            _mockSubmission.Verify(x => x.Submit(It.IsAny<SubmitContext>()));

            Assert.Equal(3, inputModelBuiltOnSubmit.BinariesSubmitted.Count);
            Assert.Equal(expected, inputModelBuiltOnSubmit.BinariesSubmitted.Select(x => x.ItemExternalId).Distinct());
        }

        [Fact]
        public async Task Submit_Handles_Forbidden_As_ConnectorNotFound()
        {
            _mockClient
                .Setup(x =>
                    x.POST.ApiItemsWithHttpMessagesAsync(
                        It.IsAny<string>(),
                        It.IsAny<ItemSubmissionInputModel>(),
                        It.IsAny<Dictionary<string, List<string>>>(),
                        It.IsAny<CancellationToken>()))
                .ThrowsAsync(CreateHttpOperationException(HttpStatusCode.Forbidden));

            _mockClientFactory.Setup(x => x.CreateApiClient(It.IsAny<ApiClientFactorySettings>())).Returns(_mockClient.Object);

            _itemPipelineElement = new HttpSubmitItemPipelineElement(_mockSubmission.Object)
            {
                ApiClientFactory = _mockClientFactory.Object
            };

            var submitContext = new SubmitContext();

            await _itemPipelineElement.Submit(submitContext);

            Assert.Equal(SubmitResult.Status.ConnectorNotFound, submitContext.SubmitResult.SubmitStatus);
            Assert.Equal("Submission returned Forbidden : Item NOT submitted because the connector request was forbidden.", submitContext.SubmitResult.Reason);
            _mockSubmission.Verify(x => x.Submit(It.IsAny<SubmitContext>()), Times.Never);
        }

        [Fact]
        public async Task Submit_Rethrows_Unknown_HttpOperationException()
        {
            _mockClient
                .Setup(x =>
                    x.POST.ApiItemsWithHttpMessagesAsync(
                        It.IsAny<string>(),
                        It.IsAny<ItemSubmissionInputModel>(),
                        It.IsAny<Dictionary<string, List<string>>>(),
                        It.IsAny<CancellationToken>()))
                .ThrowsAsync(CreateHttpOperationException(HttpStatusCode.InternalServerError));

            _mockClientFactory.Setup(x => x.CreateApiClient(It.IsAny<ApiClientFactorySettings>())).Returns(_mockClient.Object);

            _itemPipelineElement = new HttpSubmitItemPipelineElement(_mockSubmission.Object)
            {
                ApiClientFactory = _mockClientFactory.Object
            };

            await Assert.ThrowsAsync<HttpOperationException>(() => _itemPipelineElement.Submit(new SubmitContext()));
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
