using Microsoft.Rest;
using Newtonsoft.Json;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using System.Net;
using System.Text;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Client
{
    /// <summary>
    /// Tests for the hand-written binary-submission logic in ApiClientPartial.cs
    /// (ApiBinariesPostWithHttpMessagesAndStreamAsync and its private helpers
    /// SetupRequestAsync, GetUrl, AddRequestHeaders, HandleInitialResponseAsync and
    /// HandleBadRequestAsync). The AutoRest-generated code in ApiClient.cs is excluded
    /// from coverage by file (coverage.runsettings), so this partial is measured.
    /// </summary>
    public class ApiClientPartialTest
    {
        private const string BaseUrl = "https://connectorapi-endpoint.com/";

        /// <summary>
        /// Captures the outgoing request and returns a canned response so the
        /// hand-written submission logic can be exercised over a real HttpClient.
        /// </summary>
        private sealed class CapturingHandler : DelegatingHandler
        {
            private readonly HttpResponseMessage _response;
            public HttpRequestMessage CapturedRequest { get; private set; }

            public CapturingHandler(HttpResponseMessage response)
            {
                _response = response;
            }

            protected override async Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                CapturedRequest = request;
                // Force the request content (the input stream) to be read so the
                // StreamContent path is exercised end to end.
                if (request.Content != null)
                {
                    await request.Content.ReadAsByteArrayAsync(cancellationToken);
                }
                return _response;
            }
        }

        private static ApiClient CreateClient(CapturingHandler handler)
        {
            return new ApiClient(new NotSpecifiedCredentials(), handler)
            {
                BaseUri = new Uri(BaseUrl)
            };
        }

        private static HttpResponseMessage Response(HttpStatusCode statusCode, string content = "")
        {
            return new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };
        }

        private static MemoryStream NewStream(string content = "binary-payload")
            => new MemoryStream(Encoding.UTF8.GetBytes(content));

        [Fact]
        public async Task ApiBinariesPost_BuildsExpectedUrl_WithAllQueryParameters()
        {
            var handler = new CapturingHandler(Response(HttpStatusCode.OK));
            var client = CreateClient(handler);

            await client.ApiBinariesPostWithHttpMessagesAndStreamAsync(
                connectorId: "conn 1",
                itemExternalId: "item&1",
                binaryExternalId: "bin=1",
                fileName: "my file.txt",
                correlationId: "corr/1",
                inputStream: NewStream(),
                cancellationToken: TestContext.Current.CancellationToken);

            // AbsoluteUri (not ToString) keeps the canonical percent-encoding: ToString
            // decodes %20 back to a space for display, which would mask the escaping.
            var uri = handler.CapturedRequest.RequestUri.AbsoluteUri;
            Assert.StartsWith(BaseUrl + "api/Binaries?", uri);
            Assert.Contains("ConnectorId=conn%201", uri);
            Assert.Contains("ItemExternalId=item%261", uri);
            Assert.Contains("BinaryExternalId=bin%3D1", uri);
            Assert.Contains("FileName=my%20file.txt", uri);
            Assert.Contains("CorrelationId=corr%2F1", uri);
        }

        [Fact]
        public async Task ApiBinariesPost_BuildsUrlWithoutQueryString_WhenNoParametersSupplied()
        {
            var handler = new CapturingHandler(Response(HttpStatusCode.OK));
            var client = CreateClient(handler);

            await client.ApiBinariesPostWithHttpMessagesAndStreamAsync(inputStream: NewStream(), cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(BaseUrl + "api/Binaries", handler.CapturedRequest.RequestUri.ToString());
        }

        [Fact]
        public async Task ApiBinariesPost_OmitsNullParameters_FromQueryString()
        {
            var handler = new CapturingHandler(Response(HttpStatusCode.OK));
            var client = CreateClient(handler);

            await client.ApiBinariesPostWithHttpMessagesAndStreamAsync(
                connectorId: "abc",
                binaryExternalId: "xyz",
                inputStream: NewStream(),
                cancellationToken: TestContext.Current.CancellationToken);

            var uri = handler.CapturedRequest.RequestUri.ToString();
            Assert.Contains("ConnectorId=abc", uri);
            Assert.Contains("BinaryExternalId=xyz", uri);
            Assert.DoesNotContain("ItemExternalId", uri);
            Assert.DoesNotContain("FileName", uri);
            Assert.DoesNotContain("CorrelationId", uri);
        }

        [Fact]
        public async Task ApiBinariesPost_SetsPostMethodExpectContinueAndStreamContent()
        {
            var handler = new CapturingHandler(Response(HttpStatusCode.OK));
            var client = CreateClient(handler);

            await client.ApiBinariesPostWithHttpMessagesAndStreamAsync(
                connectorId: "abc",
                inputStream: NewStream("hello"),
                cancellationToken: TestContext.Current.CancellationToken);

            var request = handler.CapturedRequest;
            Assert.Equal(HttpMethod.Post, request.Method);
            Assert.True(request.Headers.ExpectContinue);
            Assert.NotNull(request.Content);
            Assert.Equal("hello", await request.Content.ReadAsStringAsync(cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task ApiBinariesPost_AddsAcceptLanguageAndCustomHeaders()
        {
            var handler = new CapturingHandler(Response(HttpStatusCode.OK));
            var client = CreateClient(handler);

            var customHeaders = new Dictionary<string, List<string>>
            {
                ["X-Custom-Header"] = new List<string> { "custom-value" }
            };

            await client.ApiBinariesPostWithHttpMessagesAndStreamAsync(
                connectorId: "abc",
                acceptLanguage: "en-US",
                customHeaders: customHeaders,
                inputStream: NewStream(),
                cancellationToken: TestContext.Current.CancellationToken);

            var request = handler.CapturedRequest;
            Assert.Contains("en-US", request.Headers.GetValues("Accept-Language"));
            Assert.Contains("custom-value", request.Headers.GetValues("X-Custom-Header"));
        }

        [Fact]
        public async Task ApiBinariesPost_ReturnsResponse_WithoutBody_OnSuccess()
        {
            var handler = new CapturingHandler(Response(HttpStatusCode.OK));
            var client = CreateClient(handler);

            var result = await client.ApiBinariesPostWithHttpMessagesAndStreamAsync(
                connectorId: "abc",
                inputStream: NewStream(),
                cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, result.Response.StatusCode);
            Assert.Null(result.Body);
        }

        [Fact]
        public async Task ApiBinariesPost_DeserializesErrorBody_OnBadRequest()
        {
            var errorJson = JsonConvert.SerializeObject(new ErrorResponseModel(
                new ErrorModel(message: "something went wrong")));
            var handler = new CapturingHandler(Response(HttpStatusCode.BadRequest, errorJson));
            var client = CreateClient(handler);

            var result = await client.ApiBinariesPostWithHttpMessagesAndStreamAsync(
                connectorId: "abc",
                inputStream: NewStream(),
                cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(HttpStatusCode.BadRequest, result.Response.StatusCode);
            Assert.NotNull(result.Body);
            Assert.Equal("something went wrong", result.Body.Error.Message);
        }

        [Fact]
        public async Task ApiBinariesPost_ThrowsSerializationException_OnUnparseableBadRequestBody()
        {
            var handler = new CapturingHandler(Response(HttpStatusCode.BadRequest, "this-is-not-json"));
            var client = CreateClient(handler);

            var act = async () => await client.ApiBinariesPostWithHttpMessagesAndStreamAsync(
                connectorId: "abc",
                inputStream: NewStream());

            await Assert.ThrowsAsync<SerializationException>(act);
        }

        [Fact]
        public async Task ApiBinariesPost_ReturnsResponse_OnPreconditionFailed()
        {
            var handler = new CapturingHandler(Response(HttpStatusCode.PreconditionFailed));
            var client = CreateClient(handler);

            var result = await client.ApiBinariesPostWithHttpMessagesAndStreamAsync(
                connectorId: "abc",
                inputStream: NewStream(),
                cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(HttpStatusCode.PreconditionFailed, result.Response.StatusCode);
            Assert.Null(result.Body);
        }

        [Fact]
        public async Task ApiBinariesPost_ThrowsHttpOperationException_OnUnexpectedStatusCode()
        {
            var handler = new CapturingHandler(Response(HttpStatusCode.InternalServerError, "boom"));
            var client = CreateClient(handler);

            var act = async () => await client.ApiBinariesPostWithHttpMessagesAndStreamAsync(
                connectorId: "abc",
                inputStream: NewStream());

            var ex = await Assert.ThrowsAsync<HttpOperationException>(act);
            Assert.Equal("boom", ex.Response.Content);
        }

        [Fact]
        public async Task ApiBinariesPost_Throws_WhenCancelledBeforeSend()
        {
            var handler = new CapturingHandler(Response(HttpStatusCode.OK));
            var client = CreateClient(handler);
            using var cts = new CancellationTokenSource();
            await cts.CancelAsync();

            var act = async () => await client.ApiBinariesPostWithHttpMessagesAndStreamAsync(
                connectorId: "abc",
                inputStream: NewStream(),
                cancellationToken: cts.Token);

            await Assert.ThrowsAsync<OperationCanceledException>(act);
        }
    }
}
