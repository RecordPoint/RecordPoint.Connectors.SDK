using FluentAssertions;
using Microsoft.Rest;
using Moq;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.SubmitPipeline
{
    public class HttpSubmitItemPipelineElementTests
    {
        private readonly Mock<ISubmission> _mockSubmission = new Mock<ISubmission>();
        private readonly Mock<IApiClient> _mockClient = new Mock<IApiClient>();
        private readonly Mock<IApiClientFactory> _mockClientFactory = new Mock<IApiClientFactory>();

        private HttpSubmitItemPipelineElement _itemPipelineElement;

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
                    x.ApiItemsPostWithHttpMessagesAsync(
                        It.IsAny<ItemSubmissionInputModel>(),
                        It.IsAny<string>(),
                        It.IsAny<Dictionary<string, List<string>>>(),
                        CancellationToken.None))
                .Callback<ItemSubmissionInputModel, string, Dictionary<string, List<string>>, CancellationToken>(
                    (model, lan, headers, ct) => inputModelBuiltOnSubmit = model)
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

            inputModelBuiltOnSubmit.BinariesSubmitted.Should().HaveCount(3);
            inputModelBuiltOnSubmit.BinariesSubmitted.Select(x => x.ItemExternalId).Distinct().Should().BeEquivalentTo("item-external-id-1");
        }
    }
}
