using Microsoft.Rest;
using Moq;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Diagnostics;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Client
{
    public class ApiClientRetryPolicyTest
    {
        private Mock<ILog> _mockLog = new Mock<ILog>();

        [Theory]
        [MemberData(nameof(GenerateException))]
        public async Task RetryPolicy_Logs(Exception exception)
        {
            var externalId = Guid.NewGuid().ToString();
            SubmitContext submitContext = new BinarySubmitContext
            {
                ExternalId = externalId
            };

            var policy = ApiClientRetryPolicy.GetPolicy(
                _mockLog.Object, 
                typeof(HttpSubmitBinaryPipelineElement),
                nameof(HttpSubmitBinaryPipelineElement.Submit), 
                2,
                CancellationToken.None, 
                submitContext.LogPrefix()
            );

            Type expectedExceptionType = exception.GetType();

            await Assert.ThrowsAsync(expectedExceptionType, async () =>
            {
                await policy.ExecuteAsync(
                    async (ct) =>
                    {
                        throw exception;
                    },
                    CancellationToken.None
                );
            });

            _mockLog.Verify(x => x.LogMessage(
                It.Is<Type>(t => t == typeof(HttpSubmitBinaryPipelineElement)),
                It.Is<string>(m => m == "Submit_Retry"),
                It.Is<string>(l => l.Contains(externalId)),
                It.IsAny<long?>()
            ), Times.Exactly(2));
        }

        public static IEnumerable<object[]> GenerateException()
        {
            yield return new object[] { new TaskCanceledException() };
            yield return new object[] { new HttpOperationException() { Response = new HttpResponseMessageWrapper(new HttpResponseMessage((HttpStatusCode)429), "derp") } };
            yield return new object[] { new HttpOperationException() { Response = new HttpResponseMessageWrapper(new HttpResponseMessage(HttpStatusCode.RequestTimeout), "derp") } };
            yield return new object[] { new HttpOperationException() { Response = new HttpResponseMessageWrapper(new HttpResponseMessage(HttpStatusCode.GatewayTimeout), "derp") } };
            yield return new object[] { new HttpOperationException() { Response = new HttpResponseMessageWrapper(new HttpResponseMessage(HttpStatusCode.ServiceUnavailable), "derp") } };
            yield return new object[] { new WebException("derp", WebExceptionStatus.ConnectFailure) };
            yield return new object[] { new WebException("derp", WebExceptionStatus.ConnectionClosed) };
            yield return new object[] { new WebException("derp", WebExceptionStatus.KeepAliveFailure) };
            yield return new object[] { new WebException("derp", WebExceptionStatus.NameResolutionFailure) };
            yield return new object[] { new WebException("derp", WebExceptionStatus.Pending) };
            yield return new object[] { new WebException("derp", WebExceptionStatus.PipelineFailure) };
            yield return new object[] { new WebException("derp", WebExceptionStatus.ProxyNameResolutionFailure) };
            yield return new object[] { new WebException("derp", WebExceptionStatus.ReceiveFailure) };
            yield return new object[] { new WebException("derp", WebExceptionStatus.SendFailure) };
            yield return new object[] { new WebException("derp", WebExceptionStatus.Timeout) };
            yield return new object[] { new WebException("derp", WebExceptionStatus.UnknownError) };
            yield return new object[] { new HttpRequestException("derp", new WebException("derp", WebExceptionStatus.Timeout)) };
        }
    }
}
