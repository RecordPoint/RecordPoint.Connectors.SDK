using Microsoft.Rest;
using Moq;
using Newtonsoft.Json;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Exceptions;
using RecordPoint.Connectors.SDK.Notifications;
using System.Net;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Notifications
{
    public class NotificationApiManagerTest
    {
        private readonly Mock<IApiClient> _mockClient = new();
        private readonly Mock<IApiClientFactory> _mockClientFactory = new();
        private readonly NotificationApiManager _sut;
        private static readonly string[] expected = new[] { "n1", "n2" };

        public NotificationApiManagerTest()
        {
            var mockAuth = new Mock<IAuthenticationProvider>();
            mockAuth.Setup(x => x.AcquireTokenAsync(It.IsAny<AuthenticationHelperSettings>()))
                .ReturnsAsync(new AuthenticationResult { AccessToken = "token", AccessTokenType = "Bearer" });

            _mockClientFactory.Setup(x => x.CreateApiClient(It.IsAny<ApiClientFactorySettings>())).Returns(_mockClient.Object);
            _mockClientFactory.Setup(x => x.CreateAuthenticationProvider(It.IsAny<AuthenticationHelperSettings>())).Returns(mockAuth.Object);

            _sut = new NotificationApiManager(_mockClientFactory.Object);
        }

        [Fact]
        public void DefaultConstructor_CreatesInstance()
        {
            var manager = new NotificationApiManager();
            Assert.NotNull(manager);
        }

        [Fact]
        public void Constructor_Throws_WhenApiClientFactoryNull()
        {
            var act = () => new NotificationApiManager(null);

            var ex = Assert.Throws<ArgumentNullException>(act);
            Assert.Equal("apiClientFactory", ex.ParamName);
        }

        [Fact]
        public async Task GetAllPendingConnectorNotifications_ReturnsBody()
        {
            var expected = new List<ConnectorNotificationModel>
            {
                new() { Id = "n1", ConnectorId = "c1" }
            };

            _mockClient.Setup(x => x.GET.ApiNotificationsWithHttpMessagesAsync(
                It.IsAny<string>(),
                It.IsAny<bool?>(),
                It.IsAny<int?>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpOperationResponse<IList<ConnectorNotificationModel>>
                {
                    Response = new HttpResponseMessage(HttpStatusCode.OK),
                    Body = expected
                });

            var result = await _sut.GetAllPendingConnectorNotifications(
                new ApiClientFactorySettings(),
                new AuthenticationHelperSettings(),
                "config-1",
                CancellationToken.None);

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task GetAllPendingConnectorTypeNotifications_DeserialisesBody()
        {
            var models = new List<ConnectorNotificationModel>
            {
                new() { Id = "n1", ConnectorId = "c1" },
                new() { Id = "n2", ConnectorId = "c2" }
            };
            // Body is object; simulate the AutoRest deserialised shape.
            var body = JsonConvert.DeserializeObject<object>(JsonConvert.SerializeObject(models));

            _mockClient.Setup(x => x.GET.ApiNotificationsConnectorTypesconnectorTypeIdNotificationsWithHttpMessagesAsync(
                It.IsAny<Guid>(),
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpOperationResponse<object>
                {
                    Response = new HttpResponseMessage(HttpStatusCode.OK),
                    Body = body
                });

            var result = await _sut.GetAllPendingConnectorTypeNotifications(
                new ApiClientFactorySettings(),
                new AuthenticationHelperSettings(),
                Guid.NewGuid(),
                CancellationToken.None);

            Assert.Equal(2, result.Count);
            Assert.Equal(expected, result.Select(x => x.Id));
        }

        [Fact]
        public async Task GetAllPendingConnectorTypeNotifications_ReturnsEmpty_WhenBodyNull()
        {
            _mockClient.Setup(x => x.GET.ApiNotificationsConnectorTypesconnectorTypeIdNotificationsWithHttpMessagesAsync(
                It.IsAny<Guid>(),
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpOperationResponse<object>
                {
                    Response = new HttpResponseMessage(HttpStatusCode.OK),
                    Body = null
                });

            var result = await _sut.GetAllPendingConnectorTypeNotifications(
                new ApiClientFactorySettings(),
                new AuthenticationHelperSettings(),
                Guid.NewGuid(),
                CancellationToken.None);

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllPendingConnectorTypeNotifications_ReturnsEmpty_WhenBodyNotDeserialisable()
        {
            _mockClient.Setup(x => x.GET.ApiNotificationsConnectorTypesconnectorTypeIdNotificationsWithHttpMessagesAsync(
                It.IsAny<Guid>(),
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpOperationResponse<object>
                {
                    Response = new HttpResponseMessage(HttpStatusCode.OK),
                    // A bare integer cannot be deserialised into IList<ConnectorNotificationModel>.
                    Body = 42
                });

            var result = await _sut.GetAllPendingConnectorTypeNotifications(
                new ApiClientFactorySettings(),
                new AuthenticationHelperSettings(),
                Guid.NewGuid(),
                CancellationToken.None);

            Assert.Empty(result);
        }

        [Fact]
        public async Task AcknowledgeNotification_Completes_WhenResponseOk()
        {
            _mockClient.Setup(x => x.POST.ApiNotificationsWithHttpMessagesAsync(
                It.IsAny<string>(),
                It.IsAny<ConnectorNotificationAcknowledgeModel>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpOperationResponse<ErrorResponseModel>
                {
                    Response = new HttpResponseMessage(HttpStatusCode.OK)
                });

            var act = async () => await _sut.AcknowledgeNotification(
                new ApiClientFactorySettings(),
                new AuthenticationHelperSettings(),
                new ConnectorNotificationAcknowledgeModel { NotificationId = "n1" },
                CancellationToken.None);

            var exception = await Record.ExceptionAsync(act);

            Assert.Null(exception);
        }

        [Fact]
        public async Task AcknowledgeNotification_Throws_WhenNotFound()
        {
            _mockClient.Setup(x => x.POST.ApiNotificationsWithHttpMessagesAsync(
                It.IsAny<string>(),
                It.IsAny<ConnectorNotificationAcknowledgeModel>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpOperationResponse<ErrorResponseModel>
                {
                    Response = new HttpResponseMessage(HttpStatusCode.NotFound)
                });

            var act = async () => await _sut.AcknowledgeNotification(
                new ApiClientFactorySettings(),
                new AuthenticationHelperSettings(),
                new ConnectorNotificationAcknowledgeModel { NotificationId = "n1" },
                CancellationToken.None);

            await Assert.ThrowsAsync<ResourceNotFoundException>(act);
        }

        [Fact]
        public async Task DisposalCallback_InvokesApi()
        {
            _mockClient.Setup(x => x.POST.ApiNotificationsDisposalCallbackWithHttpMessagesAsync(
                It.IsAny<string>(),
                It.IsAny<ItemNotificationDisposalCallbackModel>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpOperationResponse
                {
                    Response = new HttpResponseMessage(HttpStatusCode.OK)
                });

            await _sut.DisposalCallback(
                new ApiClientFactorySettings(),
                new AuthenticationHelperSettings(),
                new ItemNotificationDisposalCallbackModel(),
                CancellationToken.None);

            _mockClient.Verify(x => x.POST.ApiNotificationsDisposalCallbackWithHttpMessagesAsync(
                It.IsAny<string>(),
                It.IsAny<ItemNotificationDisposalCallbackModel>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
