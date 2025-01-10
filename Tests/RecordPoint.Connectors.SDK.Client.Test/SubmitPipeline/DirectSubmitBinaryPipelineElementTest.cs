using Azure;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Rest;
using Moq;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Diagnostics;
using RecordPoint.Connectors.SDK.Exceptions;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using System.Net;
using System.Text;
using Xunit;
using static RecordPoint.Connectors.SDK.Fields;

namespace RecordPoint.Connectors.SDK.Test.SubmitPipeline
{
    public class DirectSubmitBinaryPipelineElementTest
    {
        private readonly Mock<BlobClient> _mockBlob = new Mock<BlobClient>();
        private readonly Mock<ILog> _mockLog = new Mock<ILog>();
        private readonly Mock<ISubmission> _mockSubmission = new Mock<ISubmission>();
        private readonly Mock<IApiClient> _mockClient = new Mock<IApiClient>();
        private readonly Mock<IApiClientFactory> _mockClientFactory = new Mock<IApiClientFactory>();

        private readonly DirectSubmitBinaryPipelineElement _pipelineElement;

        public DirectSubmitBinaryPipelineElementTest()
        {
            SetupDefaultsForClient();

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

            var circuit = new AzureBlobRetryProviderWithCircuitBreaker(GetCircuitBreakerOptions(), true)
            {
                Log = _mockLog.Object
            };

            _pipelineElement = new DirectSubmitBinaryPipelineElement(_mockSubmission.Object)
            {
                ApiClientFactory = _mockClientFactory.Object,
                Log = _mockLog.Object,
                BlobFactory = (uri) => _mockBlob.Object,
                CircuitProvider = circuit,
                RetryProvider = circuit
            };
        }

        [Fact]
        public async Task HappyPath()
        {
            var submitContext = GetSubmitContext();
            await _pipelineElement.Submit(submitContext);

            _mockBlob.Verify(x => x.UploadAsync(It.IsAny<Stream>(), It.IsAny<BlobHttpHeaders>(), It.IsAny<IDictionary<string, string>>(), It.IsAny<BlobRequestConditions>(), It.IsAny<IProgress<long>>(), It.IsAny<AccessTier?>(), It.IsAny<StorageTransferOptions>(), It.IsAny<CancellationToken>()));
            _mockBlob.Verify(x => x.SetMetadataAsync(It.IsAny<Dictionary<string, string>>(), It.IsAny<BlobRequestConditions>(), It.IsAny<CancellationToken>()));
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

            _mockBlob.Verify(x => x.UploadAsync(It.IsAny<Stream>(), It.IsAny<BlobHttpHeaders>(), It.IsAny<IDictionary<string, string>>(), It.IsAny<BlobRequestConditions>(), It.IsAny<IProgress<long>>(), It.IsAny<AccessTier?>(), It.IsAny<StorageTransferOptions>(), It.IsAny<CancellationToken>()));
            _mockSubmission.Verify(x => x.Submit(It.IsAny<SubmitContext>()));

            Assert.Equal(SubmitResult.Status.OK, submitContext.SubmitResult.SubmitStatus);
            Assert.Equal("Submission returned Accepted : Binary accepted for submission.", submitContext.SubmitResult.Reason);
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
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<Stream>(),
                It.IsAny<CancellationToken>()
            ));
            _mockBlob.Verify(x => x.UploadAsync(It.IsAny<Stream>(), It.IsAny<BlobHttpHeaders>(), It.IsAny<IDictionary<string, string>>(), It.IsAny<BlobRequestConditions>(), It.IsAny<IProgress<long>>(), It.IsAny<AccessTier?>(), It.IsAny<StorageTransferOptions>(), It.IsAny<CancellationToken>()), Times.Never);
            _mockBlob.Verify(x => x.SetMetadataAsync(It.IsAny<Dictionary<string, string>>(), It.IsAny<BlobRequestConditions>(), It.IsAny<CancellationToken>()), Times.Never);
            _mockSubmission.Verify(x => x.Submit(It.IsAny<SubmitContext>()));

            Assert.Equal(SubmitResult.Status.OK, submitContext.SubmitResult.SubmitStatus);
            Assert.Equal("Submission returned Accepted : Binary accepted for submission.", submitContext.SubmitResult.Reason);
        }

        [Fact]
        public async Task SkipsIfBinaryProtectionIsDisabled()
        {
            var submitContext = GetSubmitContext();

            SetupGetSASTokenResponseWithError(HttpStatusCode.BadRequest, MessageCode.ProtectionNotEnabled);

            await _pipelineElement.Submit(submitContext);

            VerifyNotSubmitted();

            Assert.Equal(SubmitResult.Status.Skipped, submitContext.SubmitResult.SubmitStatus);
            Assert.Equal("Binary submission returned BadRequest : Binary NOT submitted because the connector does not have protection enabled.", submitContext.SubmitResult.Reason);
        }

        [Fact]
        public async Task DefersIfItemIsNotSubmitted()
        {
            var submitContext = GetSubmitContext();

            SetupGetSASTokenResponseWithError(HttpStatusCode.PreconditionFailed, "NotChecked");

            await _pipelineElement.Submit(submitContext);

            VerifyNotSubmitted();

            Assert.Equal(SubmitResult.Status.Deferred, submitContext.SubmitResult.SubmitStatus);
            Assert.Equal("Submission returned PreconditionFailed : Binary NOT submitted because a precondition failed.", submitContext.SubmitResult.Reason);

        }

        [Fact]
        public async Task ReturnsTooManyRequestsIf429ErrorIsReceived()
        {
            var submitContext = GetSubmitContext();

            SetupGetSASTokenResponseWithError((System.Net.HttpStatusCode)429, "TooManyRequests");

            await _pipelineElement.Submit(submitContext);

            VerifyNotSubmitted();

            Assert.Equal(SubmitResult.Status.TooManyRequests, submitContext.SubmitResult.SubmitStatus);
            Assert.StartsWith("Submission returned TooManyRequests : Binary NOT submitted because a part of the system is experiencing heavy load", submitContext.SubmitResult.Reason);
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

        [Fact]
        public async Task CircuitBreaksIfBlobExceptionReceived()
        {
            _mockBlob.Setup(x => x.UploadAsync(It.IsAny<Stream>(), It.IsAny<BlobHttpHeaders>(), It.IsAny<IDictionary<string, string>>(), It.IsAny<BlobRequestConditions>(), It.IsAny<IProgress<long>>(), It.IsAny<AccessTier?>(), It.IsAny<StorageTransferOptions>(), It.IsAny<CancellationToken>())).Throws(Throw503Exception());

            var submitContext = GetSubmitContext();
            await _pipelineElement.Submit(submitContext);

            _mockBlob.Verify(x => x.UploadAsync(It.IsAny<Stream>(), It.IsAny<BlobHttpHeaders>(), It.IsAny<IDictionary<string, string>>(), It.IsAny<BlobRequestConditions>(), It.IsAny<IProgress<long>>(), It.IsAny<AccessTier?>(), It.IsAny<StorageTransferOptions>(), It.IsAny<CancellationToken>()));
            WaitUntilComplete(() =>
            {
                Assert.False(_pipelineElement.CircuitProvider.IsCircuitClosed(out var tmp));
                return true;
            }, 10, 1000, "Circuit remains closed when it's supposed to be open.");
            Assert.Equal(SubmitResult.Status.TooManyRequests, submitContext.SubmitResult.SubmitStatus);
            Assert.IsType<TooManyRequestsException>(submitContext.SubmitResult.Exception);
            Assert.Equal(submitContext.SubmitResult.Exception.Message, submitContext.SubmitResult.Reason);
        }

        private static BinarySubmitContext GetSubmitContext(long length = 10)
        {
            return new BinarySubmitContext()
            {
                ApiClientFactorySettings = new ApiClientFactorySettings(),
                AuthenticationHelperSettings = new AuthenticationHelperSettings(),
                CancellationToken = CancellationToken.None,
                ConnectorConfigId = Guid.NewGuid(),
                ExternalId = Guid.NewGuid().ToString(),
                FileName = Guid.NewGuid().ToString(),
                FileHash = Guid.NewGuid().ToString(),
                MimeType = "text/plain",
                SourceLastModifiedDate = DateTime.Now,
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
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<Stream>(),
                It.IsAny<CancellationToken>()
            ), Times.Never);
            _mockBlob.Verify(x => x.UploadAsync(It.IsAny<Stream>(), It.IsAny<BlobHttpHeaders>(), It.IsAny<IDictionary<string, string>>(), It.IsAny<BlobRequestConditions>(), It.IsAny<IProgress<long>>(), It.IsAny<AccessTier?>(), It.IsAny<StorageTransferOptions>(), It.IsAny<CancellationToken>()), Times.Never);
            _mockBlob.Verify(x => x.SetMetadataAsync(It.IsAny<Dictionary<string, string>>(), It.IsAny<BlobRequestConditions>(), It.IsAny<CancellationToken>()), Times.Never);
            _mockSubmission.Verify(x => x.Submit(It.IsAny<SubmitContext>()), Times.Never);
        }

        /// <summary>
        /// As per https://stackoverflow.com/questions/1879395/how-do-i-generate-a-stream-from-a-string
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static Stream GetStream(long length = 10)
        {
            var builder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                builder.Append('s');
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
                It.IsAny<DirectBinarySubmissionInputModel>(),
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
                It.IsAny<DirectBinarySubmissionInputModel>(),
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

        private static RequestFailedException Throw503Exception()
        {
            return new RequestFailedException(503, "503 test", new Exception("503 test Inner"));
        }

        private static CircuitBreakerOptions GetCircuitBreakerOptions()
        {
            return new CircuitBreakerOptions()
            {
                FailureThreshold = 0.5,
                MinimumAttempts = 2,
                SamplingDurationS = 100,
                DurationOfBreakS = 10
            };
        }

        private static void WaitUntilComplete(Func<bool> code, int retry, int sleepms, string errorMessage)
        {
            int count = 0;
            while (!code())
            {
                if (count > retry)
                {
                    var output = "The code executed [" + count + "] times and did not complete";

                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        output += $": {errorMessage}";
                    }

                    throw new ApplicationException(output);
                }
                count++;
                System.Threading.Thread.Sleep(sleepms);
            }
        }
    }
}
