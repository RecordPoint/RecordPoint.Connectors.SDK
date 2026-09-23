using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Notifications.Webhook;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;
using System;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Notifications.Test;

public class WebhookOperationTests
{
    [Fact]
    public async Task RunAsync_QueuesNotification_WhenTypeConfiguredAsAsync()
    {
        var notificationManager = new Mock<INotificationManager>(MockBehavior.Strict);
        var workQueueClient = new Mock<IWorkQueueClient>();
        var observabilityScope = CreateObservabilityScope();
        var telemetryTracker = new Mock<ITelemetryTracker>();
        var dateTimeProvider = CreateDateTimeProvider();
        var notification = CreateNotification("ContentRegistration");
        WorkRequest submittedWorkRequest = null;

        workQueueClient
            .Setup(client => client.SubmitWorkAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
            .Callback<WorkRequest, CancellationToken>((request, _) => submittedWorkRequest = request)
            .Returns(Task.CompletedTask);

        AsyncNotifications.NotificationTypes = ["ContentRegistration"];

        try
        {
            var operation = new WebhookOperation(
                CreateServiceProvider(),
                notificationManager.Object,
                workQueueClient.Object,
                observabilityScope.Object,
                telemetryTracker.Object,
                dateTimeProvider.Object);

            await operation.RunAsync(notification, CancellationToken.None);

            workQueueClient.Verify(
                client => client.SubmitWorkAsync(
                    It.Is<WorkRequest>(request =>
                        request.WorkType == AsyncNotificationOperation.WORK_TYPE &&
                        request.ConnectorConfigId == notification.ConnectorConfig.Id &&
                        request.TenantId == notification.ConnectorConfig.TenantId &&
                        request.TenantDomainName == notification.ConnectorConfig.TenantDomainName),
                    It.IsAny<CancellationToken>()),
                Times.Once);
            Assert.NotNull(submittedWorkRequest);
            Assert.Contains("ContentRegistration", submittedWorkRequest.Body, StringComparison.Ordinal);
            notificationManager.Verify(
                manager => manager.HandleNotificationAsync(It.IsAny<ConnectorNotificationModel>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }
        finally
        {
            AsyncNotifications.NotificationTypes = [];
        }
    }

    [Fact]
    public async Task RunAsync_HandlesNotificationInline_WhenTypeIsNotConfiguredAsAsync()
    {
        var notificationManager = new Mock<INotificationManager>();
        var workQueueClient = new Mock<IWorkQueueClient>(MockBehavior.Strict);
        var observabilityScope = CreateObservabilityScope();
        var telemetryTracker = new Mock<ITelemetryTracker>();
        var dateTimeProvider = CreateDateTimeProvider();
        var notification = CreateNotification("ConnectorConfig");

        notificationManager
            .Setup(manager => manager.HandleNotificationAsync(notification, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        AsyncNotifications.NotificationTypes = ["ContentRegistration"];

        try
        {
            var operation = new WebhookOperation(
                CreateServiceProvider(),
                notificationManager.Object,
                workQueueClient.Object,
                observabilityScope.Object,
                telemetryTracker.Object,
                dateTimeProvider.Object);

            await operation.RunAsync(notification, CancellationToken.None);

            notificationManager.Verify(
                manager => manager.HandleNotificationAsync(notification, It.IsAny<CancellationToken>()),
                Times.Once);
            workQueueClient.Verify(
                client => client.SubmitWorkAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }
        finally
        {
            AsyncNotifications.NotificationTypes = [];
        }
    }

    private static ConnectorNotificationModel CreateNotification(string notificationType)
    {
        return new ConnectorNotificationModel
        {
            Id = Guid.NewGuid().ToString(),
            NotificationType = notificationType,
            TenantId = Guid.NewGuid().ToString(),
            ConnectorId = Guid.NewGuid().ToString(),
            ConnectorConfig = new ConnectorConfigModel
            {
                Id = Guid.NewGuid().ToString(),
                TenantId = Guid.NewGuid().ToString(),
                TenantDomainName = "tenant.example.com",
                ConnectorTypeId = Guid.NewGuid().ToString()
            }
        };
    }

    private static Mock<IObservabilityScope> CreateObservabilityScope()
    {
        var observabilityScope = new Mock<IObservabilityScope>();
        observabilityScope.SetupGet(scope => scope.Dimensions).Returns(ImmutableDictionary<string, string>.Empty);
        observabilityScope.SetupGet(scope => scope.Measures).Returns(ImmutableDictionary<string, double>.Empty);
        observabilityScope
            .Setup(scope => scope.BeginScope(It.IsAny<Dimensions>(), It.IsAny<Measures>()))
            .Returns(Mock.Of<IDisposable>());

        return observabilityScope;
    }

    private static Mock<IDateTimeProvider> CreateDateTimeProvider()
    {
        var dateTimeProvider = new Mock<IDateTimeProvider>();
        dateTimeProvider.SetupGet(provider => provider.UtcNow).Returns(DateTime.UtcNow);
        return dateTimeProvider;
    }

    private static IServiceProvider CreateServiceProvider() => new ServiceCollection().BuildServiceProvider();
}
