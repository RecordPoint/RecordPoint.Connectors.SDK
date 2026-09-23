using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Rest;
using Moq;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Diagnostics;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using System.Net;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.SubmitPipeline
{
    public class DirectSubmitBinaryPipelineElementExtraTest
    {
        private readonly Mock<BlobClient> _mockBlob = new();
        private readonly Mock<ILog> _mockLog = new();
        private readonly Mock<ISubmission> _next = new();
        private readonly Mock<IApiClient> _mockClient = new();
        private readonly Mock<IApiClientFactory> _mockClientFactory = new();
        private readonly Mock<ISdkAzureBlobCircuitProvider> _circuit = new();
        private readonly Mock<ISdkAzureBlobRetryProvider> _retry = new();

        public DirectSubmitBinaryPipelineElementExtraTest()
        {
            var mockAuth = new Mock<IAuthenticationProvider>();
            mockAuth.Setup(x => x.AcquireTokenAsync(It.IsAny<AuthenticationHelperSettings>()))
                .ReturnsAsync(new AuthenticationResult { AccessToken = "token", AccessTokenType = "Bearer" });
            _mockClientFactory.Setup(x => x.CreateAuthenticationProvider(It.IsAny<AuthenticationHelperSettings>())).Returns(mockAuth.Object);
            _mockClientFactory.Setup(x => x.CreateApiClient(It.IsAny<ApiClientFactorySettings>())).Returns(_mockClient.Object);

            // Circuit closed by default.
            var wait = TimeSpan.Zero;
            _circuit.Setup(x => x.IsCircuitClosed(out wait)).Returns(true);

            // Retry provider simply executes the supplied delegate.
            _retry.Setup(x => x.ExecuteWithRetry(It.IsAny<Func<Task>>(), It.IsAny<Type>(), It.IsAny<string>()))
                .Returns<Func<Task>, Type, string>((code, _, _) => code());

            _mockClient.Setup(x => x.POST.ApiBinariesGetSASTokenWithHttpMessagesAsync(
                It.IsAny<string>(), It.IsAny<DirectBinarySubmissionInputModel>(),
                It.IsAny<Dictionary<string, List<string>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpOperationResponse<object>
                {
                    Response = new HttpResponseMessage(HttpStatusCode.Accepted),
                    Body = new DirectBinarySubmissionResponseModel { MaxFileSize = 1000, Url = "https://fake.blob" }
                });

            _mockClient.Setup(x => x.POST.ApiBinariesNotifyBinarySubmissionWithHttpMessagesAsync(
                It.IsAny<string>(), It.IsAny<DirectBinarySubmissionInputModel>(),
                It.IsAny<Dictionary<string, List<string>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpOperationResponse<ErrorResponseModel>
                {
                    Response = new HttpResponseMessage(HttpStatusCode.OK)
                });
        }

        private DirectSubmitBinaryPipelineElement CreateElement(bool uploadBinaryOnly)
        {
            return new DirectSubmitBinaryPipelineElement(_next.Object, uploadBinaryOnly)
            {
                ApiClientFactory = _mockClientFactory.Object,
                Log = _mockLog.Object,
                BlobFactory = _ => _mockBlob.Object,
                CircuitProvider = _circuit.Object,
                RetryProvider = _retry.Object
            };
        }

        private static BinarySubmitContext GetContext(long length = 10)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(new string('s', (int)length));
            writer.Flush();
            stream.Position = 0;

            return new BinarySubmitContext
            {
                ApiClientFactorySettings = new ApiClientFactorySettings(),
                AuthenticationHelperSettings = new AuthenticationHelperSettings(),
                ConnectorConfigId = Guid.NewGuid(),
                ItemExternalId = Guid.NewGuid().ToString(),
                ExternalId = Guid.NewGuid().ToString(),
                FileName = "file.txt",
                Stream = stream
            };
        }

        [Fact]
        public async Task Submit_Defers_WhenCircuitOpen()
        {
            var wait = TimeSpan.FromSeconds(30);
            _circuit.Setup(x => x.IsCircuitClosed(out wait)).Returns(false);

            var element = CreateElement(false);
            var context = GetContext();

            await element.Submit(context);

            Assert.Equal(SubmitResult.Status.Deferred, context.SubmitResult.SubmitStatus);
            _next.Verify(x => x.Submit(It.IsAny<SubmitContext>()), Times.Never);
        }

        [Fact]
        public async Task Submit_UploadBinaryOnly_DoesNotNotifyPlatform()
        {
            var element = CreateElement(uploadBinaryOnly: true);
            var context = GetContext();

            await element.Submit(context);

            _mockClient.Verify(x => x.POST.ApiBinariesNotifyBinarySubmissionWithHttpMessagesAsync(
                It.IsAny<string>(), It.IsAny<DirectBinarySubmissionInputModel>(),
                It.IsAny<Dictionary<string, List<string>>>(), It.IsAny<CancellationToken>()), Times.Never);
            _next.Verify(x => x.Submit(context), Times.Once);
        }

        [Fact]
        public async Task Submit_UploadsAndNotifies_WhenNotUploadOnly()
        {
            var element = CreateElement(uploadBinaryOnly: false);
            var context = GetContext();

            await element.Submit(context);

            _mockClient.Verify(x => x.POST.ApiBinariesNotifyBinarySubmissionWithHttpMessagesAsync(
                It.IsAny<string>(), It.IsAny<DirectBinarySubmissionInputModel>(),
                It.IsAny<Dictionary<string, List<string>>>(), It.IsAny<CancellationToken>()), Times.Once);
            _next.Verify(x => x.Submit(context), Times.Once);
        }

        [Fact]
        public void DefaultBlobFactory_Throws_WhenUrlNullOrWhitespace()
        {
            var act = () => DirectSubmitBinaryPipelineElement.DefaultBlobFactory("");

            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void DefaultBlobFactory_ReturnsBlobClient_ForValidUrl()
        {
            var blob = DirectSubmitBinaryPipelineElement.DefaultBlobFactory("https://account.blob.core.windows.net/container/blob");

            Assert.NotNull(blob);
        }
    }
}
