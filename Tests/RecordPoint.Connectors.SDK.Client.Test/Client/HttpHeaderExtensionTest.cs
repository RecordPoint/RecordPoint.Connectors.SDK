using FluentAssertions;
using RecordPoint.Connectors.SDK.Client;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Client
{
    public class HttpHeaderExtensionTest
    {
        [Fact]
        public void AddAuthorizationHeader_WhenAuthorizationHeaderDoesNotExist_AddsTheAuthorizationHeader()
        {
            // Arrange
            var headers = new Dictionary<string, List<string>>();
            var tokenType = "Bearer";
            var token = "MyToken1";
            // Act
            HttpHeaderExtension.AddAuthorizationHeader(headers, tokenType, token);
            // Assert
            headers.TryGetValue(HttpHeaderExtension.AuthorizationHeaderName, out List<string> headerValue).Should().BeTrue();
            headerValue.FirstOrDefault().Should().Be("Bearer MyToken1");
        }

        [Fact]
        public void AddAuthorizationHeader_WhenAuthorizationHeaderAlreadyExists_OverwritesTheNewAuthorizationHeaderValueOnOldValue()
        {
            // Arrange
            var tokenType = "Bearer";
            var newToken = "MyToken2";
            var headers = new Dictionary<string, List<string>>() { { HttpHeaderExtension.AuthorizationHeaderName, new List<string>() { "Bearer MyToken1" } } };
            // Act
            HttpHeaderExtension.AddAuthorizationHeader(headers, tokenType, newToken);
            // Assert
            headers.TryGetValue(HttpHeaderExtension.AuthorizationHeaderName, out List<string> headerValue).Should().BeTrue();
            headerValue.FirstOrDefault().Should().Be("Bearer MyToken2");
        }

        [Fact]
        public void AddAuthorizationHeader_WhenOtherHeaderExist_AddsTheTokenAndDoesNotChangeTheOtherHeaders()
        {
            // Arrange
            var tokenType = "Bearer";
            var token = "MyToken2";
            var headers = new Dictionary<string, List<string>>() {
                { "IrrelevantHeader1", new List<string>(){ "Val1" } },
                { "IrrelevantHeader2", new List<string>(){ "Val2" } }
            };
            // Act
            HttpHeaderExtension.AddAuthorizationHeader(headers, tokenType, token);
            // Assert
            headers.Count.Should().Be(3);
            headers.TryGetValue(HttpHeaderExtension.AuthorizationHeaderName, out List<string> authorizationHeaderValue).Should().BeTrue();
            authorizationHeaderValue.FirstOrDefault().Should().Be("Bearer MyToken2");
            headers["IrrelevantHeader1"].FirstOrDefault().Should().Be("Val1");
            headers["IrrelevantHeader2"].FirstOrDefault().Should().Be("Val2");
        }

        [Fact]
        public void AddAuthorizationHeader_WhenHeadersArgumentIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Dictionary<string, List<string>> headers = null;
            var tokenType = "Bearer";
            var token = "MyToken";
            // Act
            Action action = () => HttpHeaderExtension.AddAuthorizationHeader(headers, tokenType, token);
            // Assert
            action.Should().ThrowExactly<ArgumentNullException>().Which.Message.Contains("headers");
        }
    }
}
