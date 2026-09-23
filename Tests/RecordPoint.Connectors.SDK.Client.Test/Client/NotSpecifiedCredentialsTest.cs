using RecordPoint.Connectors.SDK.Client;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Client
{
    public class NotSpecifiedCredentialsTest
    {
        [Fact]
        public async Task ProcessHttpRequestAsync_DoesNotAddAuthorizationHeader()
        {
            var credentials = new NotSpecifiedCredentials();
            using var request = new HttpRequestMessage(HttpMethod.Get, "https://example.com");

            await credentials.ProcessHttpRequestAsync(request, CancellationToken.None);

            Assert.False(request.Headers.Contains("Authorization"));
        }

        [Fact]
        public async Task ProcessHttpRequestAsync_Throws_WhenRequestNull()
        {
            var credentials = new NotSpecifiedCredentials();

            var act = async () => await credentials.ProcessHttpRequestAsync(null, CancellationToken.None);

            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }
    }
}
