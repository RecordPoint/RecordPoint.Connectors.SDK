
using Microsoft.Rest;
using Moq;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using System.Net;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.SubmitPipeline
{
    public class HttpSubmitBinaryPipelineElementTest
    {
        private readonly Mock<ISubmission> _next = new();
        private readonly Mock<IApiClient> _mockClient = new();
        private readonly Mock<IApiClientFactory> _mockClientFactory = new();
        private readonly HttpSubmitBinaryPipelineElement _element;

        public HttpSubmitBinaryPipelineElementTest()
        {
            var mockAuth = new Mock<IAuthenticationProvider>();
            mockAuth.Setup(x => x.AcquireTokenAsync(It.IsAny<AuthenticationHelperSettings>()))
                .ReturnsAsync(new AuthenticationResult { AccessToken = "token", AccessTokenType = "Bearer" });

            _mockClientFactory.Setup(x => x.CreateApiClient(It.IsAny<ApiClientFactorySettings>())).Returns(_mockClient.Object);
            _mockClientFactory.Setup(x => x.CreateAuthenticationProvider(It.IsAny<AuthenticationHelperSettings>())).Returns(mockAuth.Object);

            _mockClient.Setup(x => x.ApiBinariesPostWithHttpMessagesAndStreamAsync(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpOperationResponse<ErrorResponseModel>
                {
                    Response = new HttpResponseMessage(HttpStatusCode.Accepted)
                });

            _element = new HttpSubmitBinaryPipelineElement(_next.Object)
            {
                ApiClientFactory = _mockClientFactory.Object
            };
        }

        private static MemoryStream MakeStream(long length = 10)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(new string('s', (int)length));
            writer.Flush();
            stream.Position = 5;
            return stream;
        }

        private static BinarySubmitContext GetContext(Stream stream = null)
        {
            return new BinarySubmitContext
            {
                ConnectorConfigId = Guid.NewGuid(),
                ItemExternalId = "item-1",
                ExternalId = "binary-1",
                FileName = "file.txt",
                Stream = stream ?? MakeStream()
            };
        }

        [Fact]
        public async Task Submit_HappyPath_ResetsStreamAndContinues()
        {
            var context = GetContext();

            await _element.Submit(context);

            Assert.Equal(0, context.Stream.Position);
            _next.Verify(x => x.Submit(context), Times.Once);
        }

        [Fact]
        public async Task Submit_Throws_WhenStreamEmpty()
        {
            var context = GetContext(new MemoryStream());

            var act = async () => await _element.Submit(context);

            await Assert.ThrowsAsync<ValidationException>(act);
        }

        [Fact]
        public async Task Submit_Throws_WhenItemExternalIdMissing()
        {
            var context = GetContext();
            context.ItemExternalId = "";

            var act = async () => await _element.Submit(context);

            await Assert.ThrowsAsync<ValidationException>(act);
        }

        [Fact]
        public async Task Submit_Throws_WhenExternalIdMissing()
        {
            var context = GetContext();
            context.ExternalId = "";

            var act = async () => await _element.Submit(context);

            await Assert.ThrowsAsync<ValidationException>(act);
        }

        [Fact]
        public async Task Submit_Throws_WhenConnectorConfigIdEmpty()
        {
            var context = GetContext();
            context.ConnectorConfigId = Guid.Empty;

            var act = async () => await _element.Submit(context);

            await Assert.ThrowsAsync<ValidationException>(act);
        }
    }
}
