using Microsoft.Extensions.Options;
using Moq;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Configuration;
using RecordPoint.Connectors.SDK.Notifications;
using RecordPoint.Connectors.SDK.Observability;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Notifications
{
    [Collection("Sequential")]
    public class R365NotificationClientTest
    {
        private readonly Mock<IR365ConfigurationClient> _r365ConfigClientMock = new();
        private readonly IObservabilityScope _observabilityScope = new ObservabilityScope();
        private readonly Mock<ITelemetryTracker> _telemetryTrackerMock = new();
        private readonly Mock<IOptions<NotificationsPollerOptions>> _optionsMock = new();
        private readonly Mock<INotificationApiManager> _notificationPullManagerMock = new();

        private NotificationsPollerOptions _pollerOptions = new()
        {
            ConnectorTypes = new Guid[] { Guid.NewGuid(), Guid.NewGuid() },
            TenantDomainName = "tenant"
        };

        private R365NotificationClient CreateSut()
        {
            _optionsMock.Setup(x => x.Value).Returns(_pollerOptions);
            return new R365NotificationClient(
                _r365ConfigClientMock.Object,
                _observabilityScope,
                _telemetryTrackerMock.Object,
                _optionsMock.Object,
                _notificationPullManagerMock.Object
            );
        }

        [Fact]
        public void IsConfigured_ReturnsCorrectValue()
        {
            _r365ConfigClientMock.Setup(x => x.R365ConfigurationExists()).Returns(true);
            var sut = CreateSut();
            Assert.True(sut.IsConfigured());

            _r365ConfigClientMock.Setup(x => x.R365ConfigurationExists()).Returns(false);
            Assert.False(sut.IsConfigured());
        }

        [Fact]
        public async Task GetAllPendingNotifications_ReturnsEmpty_WhenConnectorTypesNullOrEmpty()
        {
            _pollerOptions.ConnectorTypes = null;
            var sut = CreateSut();
            var result = await sut.GetAllPendingNotifications(CancellationToken.None);
            Assert.Empty(result);

            _pollerOptions.ConnectorTypes = Array.Empty<Guid>();
            sut = CreateSut();
            result = await sut.GetAllPendingNotifications(CancellationToken.None);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllPendingNotifications_AggregatesNotifications()
        {
            var connectorType1 = Guid.NewGuid();
            var connectorType2 = Guid.NewGuid();
            _pollerOptions.ConnectorTypes = new[] { connectorType1, connectorType2 };

            var configModel = new R365ConfigurationModel();
            _r365ConfigClientMock.Setup(x => x.GetR365Configuration(It.IsAny<string>())).Returns(configModel);

            var notifications1 = new List<ConnectorNotificationModel> { new ConnectorNotificationModel() };
            var notifications2 = new List<ConnectorNotificationModel> { new ConnectorNotificationModel(), new ConnectorNotificationModel() };

            _notificationPullManagerMock.Setup(x =>
                x.GetAllPendingConnectorTypeNotifications(
                    It.IsAny<ApiClientFactorySettings>(),
                    It.IsAny<AuthenticationHelperSettings>(),
                    connectorType1,
                    It.IsAny<CancellationToken>())
            ).ReturnsAsync(notifications1);

            _notificationPullManagerMock.Setup(x =>
                x.GetAllPendingConnectorTypeNotifications(
                    It.IsAny<ApiClientFactorySettings>(),
                    It.IsAny<AuthenticationHelperSettings>(),
                    connectorType2,
                    It.IsAny<CancellationToken>())
            ).ReturnsAsync(notifications2);

            var sut = CreateSut();
            var result = await sut.GetAllPendingNotifications(CancellationToken.None);

            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetAllPendingNotifications_TracksException()
        {
            var connectorType = Guid.NewGuid();
            _pollerOptions.ConnectorTypes = new[] { connectorType };

            var configModel = new R365ConfigurationModel();
            _r365ConfigClientMock.Setup(x => x.GetR365Configuration(It.IsAny<string>())).Returns(configModel);

            _notificationPullManagerMock.Setup(x =>
                x.GetAllPendingConnectorTypeNotifications(
                    It.IsAny<ApiClientFactorySettings>(),
                    It.IsAny<AuthenticationHelperSettings>(),
                    connectorType,
                    It.IsAny<CancellationToken>())
            ).ThrowsAsync(new Exception("Test exception"));

            var sut = CreateSut();
            var result = await sut.GetAllPendingNotifications(CancellationToken.None);

            _telemetryTrackerMock.Verify(x => x.TrackException(It.Is<Exception>(ex => ex.Message == "Test exception"), It.IsAny<Dimensions>(), It.IsAny<Measures>()), Times.Once);
            
            Assert.Empty(result);
        }

        [Fact]
        public async Task AcknowledgeNotificationAsync_CallsPullManager()
        {
            var notification = new ConnectorNotificationModel
            {
               ConnectorConfig = new ConnectorConfigModel()
            };
            var configModel = new R365ConfigurationModel();
            _r365ConfigClientMock.Setup(x => x.GetR365Configuration(It.IsAny<string>())).Returns(configModel);

            _notificationPullManagerMock.Setup(x =>
                x.AcknowledgeNotification(
                    It.IsAny<ApiClientFactorySettings>(),
                    It.IsAny<AuthenticationHelperSettings>(),
                    It.IsAny<ConnectorNotificationAcknowledgeModel>(),
                    It.IsAny<CancellationToken>())
            ).Returns(Task.CompletedTask);

            var sut = CreateSut();
            await sut.AcknowledgeNotificationAsync(notification, ProcessingResult.OK, "message", CancellationToken.None);

            _notificationPullManagerMock.Verify(x =>
                x.AcknowledgeNotification(
                    It.IsAny<ApiClientFactorySettings>(),
                    It.IsAny<AuthenticationHelperSettings>(),
                    It.IsAny<ConnectorNotificationAcknowledgeModel>(),
                    It.IsAny<CancellationToken>()), Times.Once);
        }       
    }
}