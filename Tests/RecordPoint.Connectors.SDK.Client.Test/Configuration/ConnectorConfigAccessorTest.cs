using Microsoft.Rest;
using Moq;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Configuration;
using System.Net;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Configuration
{
    public class ConnectorConfigAccessorTest
    {
        [Fact]
        public async Task GetConnectorConfig_ReturnsConfigBody()
        {
            var connectorConfigId = Guid.NewGuid();
            var expected = new ConnectorConfigModel { Id = connectorConfigId.ToString() };

            var mockClient = new Mock<IApiClient>();
            mockClient.Setup(x => x.GET.ApiConnectorConfigurationsGetMultiTenantedWithHttpMessagesAsync(
                It.IsAny<Guid?>(),
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpOperationResponse<ConnectorConfigModel>
                {
                    Response = new HttpResponseMessage(HttpStatusCode.OK),
                    Body = expected
                });

            var mockAuth = new Mock<IAuthenticationProvider>();
            mockAuth.Setup(x => x.AcquireTokenAsync(It.IsAny<AuthenticationHelperSettings>()))
                .ReturnsAsync(new AuthenticationResult { AccessToken = "token", AccessTokenType = "Bearer" });

            var mockFactory = new Mock<IApiClientFactory>();
            mockFactory.Setup(x => x.CreateApiClient(It.IsAny<ApiClientFactorySettings>())).Returns(mockClient.Object);
            mockFactory.Setup(x => x.CreateAuthenticationProvider(It.IsAny<AuthenticationHelperSettings>())).Returns(mockAuth.Object);

            var accessor = new ConnectorConfigAccessor
            {
                ApiClientFactory = mockFactory.Object,
                ApiClientFactorySettings = new ApiClientFactorySettings(),
                AuthenticationHelperSettings = new AuthenticationHelperSettings()
            };

            var result = await accessor.GetConnectorConfig(connectorConfigId, CancellationToken.None);

            Assert.Same(expected, result);
            mockClient.Verify(x => x.GET.ApiConnectorConfigurationsGetMultiTenantedWithHttpMessagesAsync(
                connectorConfigId,
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
