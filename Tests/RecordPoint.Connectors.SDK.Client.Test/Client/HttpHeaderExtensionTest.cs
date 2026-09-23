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
            Assert.True(headers.TryGetValue(HttpHeaderExtension.AuthorizationHeaderName, out List<string> headerValue));
            Assert.Equal("Bearer MyToken1", headerValue.FirstOrDefault());
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
            Assert.True(headers.TryGetValue(HttpHeaderExtension.AuthorizationHeaderName, out List<string> headerValue));
            Assert.Equal("Bearer MyToken2", headerValue.FirstOrDefault());
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
            Assert.Equal(3, headers.Count);
            Assert.True(headers.TryGetValue(HttpHeaderExtension.AuthorizationHeaderName, out List<string> authorizationHeaderValue));
            Assert.Equal("Bearer MyToken2", authorizationHeaderValue.FirstOrDefault());
            Assert.Equal("Val1", headers["IrrelevantHeader1"].FirstOrDefault());
            Assert.Equal("Val2", headers["IrrelevantHeader2"].FirstOrDefault());
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
            var ex = Assert.Throws<ArgumentNullException>(action);
            Assert.Contains("headers", ex.Message);
        }
    }
}
