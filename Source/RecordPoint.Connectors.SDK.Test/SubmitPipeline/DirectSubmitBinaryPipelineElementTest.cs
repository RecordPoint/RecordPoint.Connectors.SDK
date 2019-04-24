using Microsoft.Rest;
using Microsoft.WindowsAzure.Storage.Blob;
using Moq;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Diagnostics;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static RecordPoint.Connectors.SDK.Fields;

namespace RecordPoint.Connectors.SDK.Test.SubmitPipeline
{
    public class DirectSubmitBinaryPipelineElementTest
    {
        private Mock<ICloudBlob> _mockBlob = new Mock<ICloudBlob>();
        private Mock<ILog> _mockLog = new Mock<ILog>();
        private Mock<ISubmission> _mockSubmission = new Mock<ISubmission>();
        private Mock<IApiClient> _mockClient = new Mock<IApiClient>();
        private Mock<IApiClientFactory> _mockClientFactory = new Mock<IApiClientFactory>();

        private DirectSubmitBinaryPipelineElement _pipelineElement;

        public DirectSubmitBinaryPipelineElementTest()
        {
            _mockBlob.Setup(x => x.Metadata).Returns(new Dictionary<string, string>());
            _mockBlob.SetupGet(x => x.Properties).Returns(new BlobProperties());
            SetupDefaultsForClient();

            var mockAuthenticationHelper = new Mock<IAuthenticationHelper>();
            mockAuthenticationHelper.Setup(x => x.AcquireTokenAsync(It.IsAny<AuthenticationHelperSettings>(), It.IsAny<bool>())).ReturnsAsync(
                new AuthenticationResult()
                {
                    AccessToken = Guid.NewGuid().ToString(),
                    AccessTokenType = "Bearer"
                }
            );

            _mockClientFactory.Setup(x => x.CreateAuthenticationHelper()).Returns(mockAuthenticationHelper.Object);
            _mockClientFactory.Setup(x => x.CreateApiClient(It.IsAny<ApiClientFactorySettings>())).Returns(_mockClient.Object);

            _pipelineElement = new DirectSubmitBinaryPipelineElement(_mockSubmission.Object)
            {
                ApiClientFactory = _mockClientFactory.Object,
                Log = _mockLog.Object,
                BlobFactory = (uri) => _mockBlob.Object 
            };
        }

        [Fact]
        public async Task HappyPath()
        {
            var submitContext = GetSubmitContext();
            await _pipelineElement.Submit(submitContext);

            _mockBlob.Verify(x => x.UploadFromStreamAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()));
            _mockBlob.Verify(x => x.SetMetadataAsync(It.IsAny<CancellationToken>()));
            _mockSubmission.Verify(x => x.Submit(It.IsAny<SubmitContext>()));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task HappyPathWithoutFileName(string fileName)
        {
            var submitContext = GetSubmitContext();
            submitContext.FileName = fileName;

            await _pipelineElement.Submit(submitContext);

            _mockBlob.Verify(x => x.UploadFromStreamAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()));
            _mockSubmission.Verify(x => x.Submit(It.IsAny<SubmitContext>()));

            Assert.Equal(SubmitResult.Status.OK, submitContext.SubmitResult.SubmitStatus);
        }

        [Fact]
        public async Task FallsBackIfDirectSubmissionIsDisabled()
        {
            var submitContext = GetSubmitContext();

            SetupGetSASTokenResponseWithError(HttpStatusCode.MethodNotAllowed, "NotChecked");

            await _pipelineElement.Submit(submitContext);

            _mockClient.Verify(x => x.ApiBinariesPostWithHttpMessagesAndStreamAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<Stream>(),
                It.IsAny<CancellationToken>()
            ));
            _mockBlob.Verify(x => x.UploadFromStreamAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()), Times.Never);
            _mockBlob.Verify(x => x.SetMetadataAsync(It.IsAny<CancellationToken>()), Times.Never);
            _mockSubmission.Verify(x => x.Submit(It.IsAny<SubmitContext>()));

            Assert.Equal(SubmitResult.Status.OK, submitContext.SubmitResult.SubmitStatus);
        }

        [Fact]
        public async Task SkipsIfBinaryProtectionIsDisabled()
        {
            var submitContext = GetSubmitContext();

            SetupGetSASTokenResponseWithError(HttpStatusCode.BadRequest, MessageCode.ProtectionNotEnabled);

            await _pipelineElement.Submit(submitContext);

            VerifyNotSubmitted();

            Assert.Equal(SubmitResult.Status.Skipped, submitContext.SubmitResult.SubmitStatus);
        }

        [Fact]
        public async Task DefersIfItemIsNotSubmitted()
        {
            var submitContext = GetSubmitContext();

            SetupGetSASTokenResponseWithError(HttpStatusCode.PreconditionFailed, "NotChecked");

            await _pipelineElement.Submit(submitContext);

            VerifyNotSubmitted();

            Assert.Equal(SubmitResult.Status.Deferred, submitContext.SubmitResult.SubmitStatus);
        }

        [Fact]
        public async Task ThrowsIfNotificationResultIsNotOk()
        {
            var submitContext = GetSubmitContext();

            SetupNotifyBinariesForClient(HttpStatusCode.BadRequest, "NotChecked");

            await Assert.ThrowsAsync<HttpOperationException>(async () =>
            {
                await _pipelineElement.Submit(submitContext);
            });

            _mockSubmission.Verify(x => x.Submit(It.IsAny<SubmitContext>()), Times.Never);
        }

        [Fact]
        public async Task ThrowsIfStreamLengthIsZero()
        {
            var submitContext = GetSubmitContext(0);

            await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await _pipelineElement.Submit(submitContext);
            });

            VerifyNotSubmitted();
        }

        [Fact]
        public async Task SkipsIfStreamLengthIsGreaterThanMaximum()
        {
            var submitContext = GetSubmitContext(100);

            await _pipelineElement.Submit(submitContext);

            VerifyNotSubmitted();
        }

        private BinarySubmitContext GetSubmitContext(long length = 10)
        {
            return new BinarySubmitContext()
            {
                ApiClientFactorySettings = new ApiClientFactorySettings(),
                AuthenticationHelperSettings = new AuthenticationHelperSettings(),
                CancellationToken = CancellationToken.None,
                ConnectorConfigId = Guid.NewGuid(),
                ExternalId = Guid.NewGuid().ToString(),
                FileName = Guid.NewGuid().ToString(),
                ItemExternalId = Guid.NewGuid().ToString(),
                Stream = GetStream(length),
                TenantId = Guid.NewGuid()
            };
        }

        private void VerifyNotSubmitted()
        {
            _mockClient.Verify(x => x.ApiBinariesPostWithHttpMessagesAndStreamAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<Stream>(),
                It.IsAny<CancellationToken>()
            ), Times.Never);
            _mockBlob.Verify(x => x.UploadFromStreamAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()), Times.Never);
            _mockBlob.Verify(x => x.SetMetadataAsync(It.IsAny<CancellationToken>()), Times.Never);
            _mockSubmission.Verify(x => x.Submit(It.IsAny<SubmitContext>()), Times.Never);
        }

        /// <summary>
        /// As per https://stackoverflow.com/questions/1879395/how-do-i-generate-a-stream-from-a-string
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private Stream GetStream(long length = 10)
        {
            var builder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                builder.Append("s");
            }

            var content = builder.ToString();

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            return stream;
        }

        private void SetupDefaultsForClient()
        {
            SetupDefaultGetSASTokenResponse();
            SetupDefaultBinariesPostForClient();
            SetupDefaultNotifyBinariesForClient();
        }

        private void SetupDefaultBinariesPostForClient()
        {
            _mockClient.Setup(x => x.ApiBinariesPostWithHttpMessagesAndStreamAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<Stream>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(new HttpOperationResponse<ErrorResponseModel>()
            {
                Response = new HttpResponseMessage(HttpStatusCode.Accepted),
                Body = null
            });
        }

        private void SetupDefaultNotifyBinariesForClient()
        {
            _mockClient.Setup(x => x.ApiBinariesNotifyBinarySubmissionPostWithHttpMessagesAsync(
                It.IsAny <DirectBinarySubmissionInputModel>(),
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(new HttpOperationResponse<ErrorResponseModel>()
            {
                Response = new HttpResponseMessage(HttpStatusCode.OK),
                Body = null
            });
        }

        private void SetupNotifyBinariesForClient(HttpStatusCode statusCode, string messageCode)
        {
            _mockClient.Setup(x => x.ApiBinariesNotifyBinarySubmissionPostWithHttpMessagesAsync(
                It.IsAny<DirectBinarySubmissionInputModel>(),
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(new HttpOperationResponse<ErrorResponseModel>()
            {
                Response = new HttpResponseMessage(statusCode),
                Body = new ErrorResponseModel()
                {
                    Error = new ErrorModel()
                    {
                        MessageCode = messageCode
                    }
                }
            });
        }

        private void SetupDefaultGetSASTokenResponse()
        {
            _mockClient.Setup(x => x.ApiBinariesGetSASTokenPostWithHttpMessagesAsync(
                It.IsAny <DirectBinarySubmissionInputModel>(),
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(new HttpOperationResponse<object>()
            {
                Response = new HttpResponseMessage(HttpStatusCode.Accepted),
                Body = new DirectBinarySubmissionResponseModel()
                {
                    MaxFileSize = 11, 
                    Url = "https://fakeurl.fake"
                }
            });
        }

        private void SetupGetSASTokenResponseWithError(HttpStatusCode statusCode, string messageCode)
        {
            _mockClient.Setup(x => x.ApiBinariesGetSASTokenPostWithHttpMessagesAsync(
                It.IsAny<DirectBinarySubmissionInputModel>(),
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(new HttpOperationResponse<object>()
            {
                Response = new HttpResponseMessage(statusCode),
                Body = new ErrorResponseModel()
                {
                    Error = new ErrorModel()
                    {
                        MessageCode = messageCode
                    }
                }
            });
        }
    }
}
