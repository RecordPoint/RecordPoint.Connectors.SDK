#nullable enable
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Notifications;
using RecordPoint.Connectors.SDK.Notifications.Webhook;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.WebHost.Controllers;
using RecordPoint.Connectors.SDK.Work;
using System.Net;
using Xunit;

namespace RecordPoint.Connectors.SDK.WebHost.Test
{
    public class NotificationsControllerTests
    {
        private readonly Mock<IObservabilityScope> _observabilityScope = new();
        private readonly Mock<ISystemContext> _systemContext = new();
        private readonly Mock<INotificationManager> _notificationManager = new();
        private readonly Mock<IWorkQueueClient> _workQueueClient = new();
        private readonly Mock<ITelemetryTracker> _telemetryTracker = new();
        private readonly Mock<IDateTimeProvider> _dateTimeProvider = new();

        public NotificationsControllerTests()
        {
            _observabilityScope
                .Setup(scope => scope.BeginScope(It.IsAny<Dimensions>(), It.IsAny<Measures>()))
                .Returns(new Mock<IDisposable>().Object);
            _dateTimeProvider.SetupGet(p => p.UtcNow).Returns(DateTime.UtcNow);
        }

        [Fact]
        public void Ping_ReturnsOk()
        {
            var sut = CreateSubject();

            var result = sut.Ping();

            var statusResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, statusResult.StatusCode);
        }

        [Fact]
        public async Task Post_WhenSyncNotificationCompletes_ReturnsOk()
        {
            SetAsyncNotificationTypes();
            var notification = new ConnectorNotificationModel { NotificationType = NotificationType.Ping };
            _notificationManager
                .Setup(m => m.HandleNotificationAsync(notification, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            var sut = CreateSubject();

            var result = await sut.Post(notification);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Post_WhenAsyncNotificationCompletes_ReturnsAccepted()
        {
            try
            {
                // ItemDestroyed is always handled synchronously inside the operation,
                // but is registered as an async notification type so the controller
                // returns Accepted.
                SetAsyncNotificationTypes(NotificationType.ItemDestroyed);
                var notification = new ConnectorNotificationModel { NotificationType = NotificationType.ItemDestroyed };
                _notificationManager
                    .Setup(m => m.HandleNotificationAsync(notification, It.IsAny<CancellationToken>()))
                    .Returns(Task.CompletedTask);
                var sut = CreateSubject();

                var result = await sut.Post(notification);

                Assert.IsType<AcceptedResult>(result);
            }
            finally
            {
                SetAsyncNotificationTypes();
            }
        }

        [Fact]
        public async Task Post_WhenOperationFailsWithException_Rethrows()
        {
            SetAsyncNotificationTypes();
            var notification = new ConnectorNotificationModel { NotificationType = NotificationType.Ping };
            _notificationManager
                .Setup(m => m.HandleNotificationAsync(notification, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("handler failed"));
            var sut = CreateSubject();

            var act = () => sut.Post(notification);

            await Assert.ThrowsAsync<InvalidOperationException>(act);
        }

        private static void SetAsyncNotificationTypes(params string[] types)
        {
            AsyncNotifications.NotificationTypes = new List<string>(types);
        }

        private NotificationsController CreateSubject()
        {
            var services = new ServiceCollection();
            services.AddSingleton(_notificationManager.Object);
            services.AddSingleton(_workQueueClient.Object);
            services.AddSingleton(_observabilityScope.Object);
            services.AddSingleton(_telemetryTracker.Object);
            services.AddSingleton(_dateTimeProvider.Object);
            services.AddTransient<WebhookOperation>();
            var serviceProvider = services.BuildServiceProvider();

            return new NotificationsController(
                _observabilityScope.Object,
                serviceProvider,
                _systemContext.Object);
        }
    }
}
