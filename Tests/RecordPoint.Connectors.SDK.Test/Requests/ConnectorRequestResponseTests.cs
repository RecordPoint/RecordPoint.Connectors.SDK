using RecordPoint.Connectors.SDK.Requests;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Requests
{
    public class ConnectorRequestResponseTests
    {
        [Fact]
        public void Ok_SetsOutcomeAndCarriesCorrelationId()
        {
            var response = ConnectorRequestResponse.Ok("the-correlation-id");

            Assert.Equal(RequestOutcomeType.Ok, response.Outcome);
            Assert.Equal("the-correlation-id", response.CorrelationId);
            Assert.Empty(response.Messages);
            Assert.Null(response.Data);
        }

        [Fact]
        public void Ok_CarriesMessageAndData()
        {
            var data = new { Entities = 3 };

            var response = ConnectorRequestResponse.Ok("c", "All good.", data);

            Assert.Equal("All good.", Assert.Single(response.Messages));
            Assert.Same(data, response.Data);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Ok_OmitsAnEmptyMessage(string message)
        {
            var response = ConnectorRequestResponse.Ok("c", message);

            Assert.Empty(response.Messages);
        }

        [Fact]
        public void Failed_SetsOutcomeAndMessage()
        {
            var response = ConnectorRequestResponse.Failed("the-correlation-id", "It did not work.");

            Assert.Equal(RequestOutcomeType.Failed, response.Outcome);
            Assert.Equal("the-correlation-id", response.CorrelationId);
            Assert.Equal("It did not work.", Assert.Single(response.Messages));
        }

        [Fact]
        public void RequestType_IsNotSetByTheFactories()
        {
            // The SDK stamps RequestType from the request envelope after the handler returns, so a
            // handler is never required to set it. If a factory started setting it, a handler that
            // built its own response would be filed under a different key than one that used a
            // factory.
            Assert.Equal(string.Empty, ConnectorRequestResponse.Ok("c").RequestType);
            Assert.Equal(string.Empty, ConnectorRequestResponse.Failed("c", "m").RequestType);
        }
    }
}
