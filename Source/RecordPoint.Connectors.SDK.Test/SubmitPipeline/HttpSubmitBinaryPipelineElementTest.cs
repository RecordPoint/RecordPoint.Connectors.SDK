using FluentAssertions;
using Microsoft.Rest;
using Moq;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.SubmitPipeline
{
    public class HttpSubmitBinaryPipelineElementTest
    {
        private const string waitUntilTime = "wait-until-time";

        private Mock<ISubmission> _mockSubmission;
        private Mock<IApiClient> _mockApiClient;
        private Mock<IApiClientFactory> _mockClientFactory;
        private Mock<IAuthenticationHelper> _mockAuthenticationHelper;

        private HttpSubmitPipelineElementBase _hspe;
        
        public HttpSubmitBinaryPipelineElementTest()
        {
            _mockSubmission = new Mock<ISubmission>();
            _mockApiClient = new Mock<IApiClient>();
            _mockAuthenticationHelper = new Mock<IAuthenticationHelper>();
            _mockClientFactory = new Mock<IApiClientFactory>();

            _mockAuthenticationHelper.Setup(x => x.AcquireTokenAsync(It.IsAny<AuthenticationHelperSettings>(), It.IsAny<bool>())).ReturnsAsync(
                new AuthenticationResult()
                {
                    AccessToken = Guid.NewGuid().ToString(),
                    AccessTokenType = "Bearer"
                }
            );

            _mockClientFactory.Setup(x => x.CreateAuthenticationHelper()).Returns(_mockAuthenticationHelper.Object);
            _mockClientFactory.Setup(x => x.CreateApiClient(It.IsAny<ApiClientFactorySettings>())).Returns(_mockApiClient.Object);

            _hspe = new HttpSubmitBinaryPipelineElement(_mockSubmission.Object)
            {
                ApiClientFactory = _mockClientFactory.Object
            };
        }

        [Fact]
        public async Task SetWaitingTimeFromHeaders()
        {
            var time = DateTime.UtcNow;
            var submitContext = GetSubmitContext();

            Setup429Response(time);

            await _hspe.Submit(submitContext);
            
            Assert.Equal(time, submitContext.SubmitResult.WaitUntilTime);
            Assert.True(submitContext.SubmitResult.SubmitStatus == SubmitResult.Status.TooManyRequests);
        }

        [Fact]
        public async Task SetWaitingTimeWithoutValue()
        {
            var time = DateTime.UtcNow.AddSeconds(30);
            var submitContext = GetSubmitContext();

            Setup429ResponseWithoutTime();

            await _hspe.Submit(submitContext);

            Assert.True(submitContext.SubmitResult.WaitUntilTime != null);
            Assert.True(time.Subtract(submitContext.SubmitResult.WaitUntilTime.Value) <= TimeSpan.FromSeconds(1));
            Assert.True(submitContext.SubmitResult.SubmitStatus == SubmitResult.Status.TooManyRequests);
        }

        [Fact]
        public async Task SetWaitingTimeWithInvalidValue()
        {
            var time = DateTime.UtcNow.AddSeconds(30);
            var submitContext = GetSubmitContext();

            Setup429ResponseWithInvalidTime();

            await _hspe.Submit(submitContext);

            Assert.True(submitContext.SubmitResult.WaitUntilTime != null);
            Assert.True(time.Subtract(submitContext.SubmitResult.WaitUntilTime.Value) <= TimeSpan.FromSeconds(1));
            Assert.True(submitContext.SubmitResult.SubmitStatus == SubmitResult.Status.TooManyRequests);
        }

        private void Setup429Response(DateTime time)
        {
            _mockApiClient.Setup(x => x.ApiBinariesPostWithHttpMessagesAndStreamAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<Stream>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(() => {
                var result = new HttpOperationResponse<ErrorResponseModel>()
                {
                    Response = new HttpResponseMessage((HttpStatusCode)429),
                    Body = null
                };
                result.Response.Headers.Add(waitUntilTime, time.ToUniversalTime().ToString("o"));
                return result;
            });
        }

        private void Setup429ResponseWithoutTime()
        {
            _mockApiClient.Setup(x => x.ApiBinariesPostWithHttpMessagesAndStreamAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<Stream>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(
                new HttpOperationResponse<ErrorResponseModel>()
                {
                    Response = new HttpResponseMessage((HttpStatusCode)429),
                    Body = null
                }
            );
        }

        private void Setup429ResponseWithInvalidTime()
        {
            _mockApiClient.Setup(x => x.ApiBinariesPostWithHttpMessagesAndStreamAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<Stream>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(() => {
                var result = new HttpOperationResponse<ErrorResponseModel>()
                {
                    Response = new HttpResponseMessage((HttpStatusCode)429),
                    Body = null
                };
                result.Response.Headers.Add(waitUntilTime, "Invalid Time");
                return result;
            });
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
    }
}
