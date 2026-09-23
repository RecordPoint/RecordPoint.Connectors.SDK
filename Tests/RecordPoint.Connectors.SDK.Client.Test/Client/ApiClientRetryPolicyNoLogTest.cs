using Microsoft.Rest;
using RecordPoint.Connectors.SDK.Client;
using System.Net;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Client
{
    public class ApiClientRetryPolicyNoLogTest
    {
        [Fact]
        public async Task GetPolicy_NoLogOverload_RetriesRetriableException()
        {
            var attempts = 0;
            var policy = ApiClientRetryPolicy.GetPolicy(1, CancellationToken.None);

            var act = async () => await policy.ExecuteAsync(_ =>
            {
                attempts++;
                throw new HttpOperationException("429")
                {
                    Response = new HttpResponseMessageWrapper(new HttpResponseMessage((HttpStatusCode)429), "too many")
                };
            }, CancellationToken.None);

            await Assert.ThrowsAsync<HttpOperationException>(act);
            // Initial attempt plus a single retry (maxTryCount = 1).
            Assert.Equal(2, attempts);
        }

        [Fact]
        public async Task GetPolicy_NoLogOverload_DoesNotRetryNonRetriableException()
        {
            var attempts = 0;
            var policy = ApiClientRetryPolicy.GetPolicy(3, CancellationToken.None);

            var act = async () => await policy.ExecuteAsync(_ =>
            {
                attempts++;
                throw new HttpOperationException("500")
                {
                    Response = new HttpResponseMessageWrapper(new HttpResponseMessage(HttpStatusCode.InternalServerError), "server error")
                };
            }, CancellationToken.None);

            await Assert.ThrowsAsync<HttpOperationException>(act);
            Assert.Equal(1, attempts);
        }

        [Fact]
        public void IsRecords365ApiRetriableException_ReturnsFalse_ForNonRetriable()
        {
            var ex = new InvalidOperationException("nope");

            Assert.False(ex.IsRecords365ApiRetriableException(CancellationToken.None));
        }

        [Fact]
        public void IsRecords365ApiRetriableException_ReturnsFalse_ForTaskCanceled_WhenTokenCancelled()
        {
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            var ex = new TaskCanceledException();

            Assert.False(ex.IsRecords365ApiRetriableException(cts.Token));
        }

        [Fact]
        public void IsRecords365ApiRetriableException_ReturnsTrue_ForTaskCanceled_WhenTokenNotCancelled()
        {
            var ex = new TaskCanceledException();

            Assert.True(ex.IsRecords365ApiRetriableException(CancellationToken.None));
        }
    }
}
