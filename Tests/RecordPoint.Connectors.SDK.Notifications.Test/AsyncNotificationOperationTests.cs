#nullable enable
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Notifications.Webhook;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Notifications.Test;

public class AsyncNotificationOperationTests
{
    private static Mock<IObservabilityScope> CreateObservabilityScope()
    {
        var scope = new Mock<IObservabilityScope>();
        scope.Setup(x => x.BeginScope(It.IsAny<Dimensions>(), It.IsAny<Measures>()))
            .Returns(Mock.Of<IDisposable>());
        return scope;
    }

    private static Mock<IDateTimeProvider> CreateDateTimeProvider()
    {
        var provider = new Mock<IDateTimeProvider>();
        provider.SetupGet(x => x.UtcNow).Returns(DateTime.UtcNow);
        return provider;
    }

    private static ConnectorNotificationModel CreateNotification()
    {
        return new ConnectorNotificationModel
        {
            Id = Guid.NewGuid().ToString(),
            NotificationType = "ContentRegistration",
            TenantId = Guid.NewGuid().ToString(),
            ConnectorId = Guid.NewGuid().ToString(),
            ConnectorConfig = new ConnectorConfigModel
            {
                Id = Guid.NewGuid().ToString(),
                TenantId = Guid.NewGuid().ToString(),
                TenantDomainName = "tenant.example.com"
            }
        };
    }

    private static WorkRequest CreateWorkRequest(ConnectorNotificationModel notification)
    {
        return new WorkRequest
        {
            WorkId = Guid.NewGuid().ToString(),
            WorkType = AsyncNotificationOperation.WORK_TYPE,
            Body = JsonSerializer.Serialize(notification),
            ConnectorConfigId = notification.ConnectorConfig.Id,
            TenantId = notification.ConnectorConfig.TenantId,
            TenantDomainName = notification.ConnectorConfig.TenantDomainName
        };
    }

    private static AsyncNotificationOperation CreateOperation(INotificationManager notificationManager)
    {
        var systemContext = new Mock<ISystemContext>();
        systemContext.Setup(x => x.GetCompanyName()).Returns("company");
        systemContext.Setup(x => x.GetConnectorName()).Returns("connector");

        return new AsyncNotificationOperation(
            new ServiceCollection().BuildServiceProvider(),
            notificationManager,
            systemContext.Object,
            CreateObservabilityScope().Object,
            new Mock<ITelemetryTracker>().Object,
            CreateDateTimeProvider().Object);
    }

    [Fact]
    public void HasExpectedWorkTypeAndServiceName()
    {
        var operation = CreateOperation(Mock.Of<INotificationManager>());
        Assert.Equal("Async Notifications", operation.WorkType);
        Assert.Equal("Async Notifications", AsyncNotificationOperation.WORK_TYPE);
        Assert.Equal("Webhook Notifications", operation.ServiceName);
    }

    [Fact]
    public async Task RunWorkRequestAsync_HandlesNotification_AndCompletes()
    {
        var notification = CreateNotification();
        var notificationManager = new Mock<INotificationManager>();
        notificationManager.Setup(x => x.HandleNotificationAsync(It.IsAny<ConnectorNotificationModel>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var operation = CreateOperation(notificationManager.Object);

        await operation.RunWorkRequestAsync(CreateWorkRequest(notification), CancellationToken.None);

        notificationManager.Verify(x => x.HandleNotificationAsync(
            It.Is<ConnectorNotificationModel>(n => n.Id == notification.Id), It.IsAny<CancellationToken>()), Times.Once);
        Assert.True(operation.HasResult);
        Assert.Equal(WorkResultType.Complete, operation.ResultType);
    }

    [Fact]
    public async Task RunWorkRequestAsync_MarksFailed_WhenHandlingThrows()
    {
        var notification = CreateNotification();
        var notificationManager = new Mock<INotificationManager>();
        notificationManager.Setup(x => x.HandleNotificationAsync(It.IsAny<ConnectorNotificationModel>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("handler failed"));

        var operation = CreateOperation(notificationManager.Object);

        await operation.RunWorkRequestAsync(CreateWorkRequest(notification), CancellationToken.None);

        Assert.True(operation.HasResult);
        Assert.Equal(WorkResultType.Failed, operation.ResultType);
        Assert.IsType<InvalidOperationException>(operation.Exception);
    }

    [Fact]
    public async Task RunWorkRequestAsync_Throws_WhenWorkTypeMismatched()
    {
        var operation = CreateOperation(Mock.Of<INotificationManager>());
        var workRequest = new WorkRequest { WorkId = "1", WorkType = "SomethingElse", Body = "{}" };

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            operation.RunWorkRequestAsync(workRequest, CancellationToken.None));
    }

    [Fact]
    public void Dispose_DoesNotThrow_AndIsIdempotent()
    {
        var operation = CreateOperation(Mock.Of<INotificationManager>());

        var exception = Record.Exception(() =>
        {
            operation.Dispose();
            operation.Dispose();
        });

        Assert.Null(exception);
    }

    [Fact]
    public void AccessingHasDisposed_AfterDispose_ThrowsObjectDisposedException()
    {
        var operation = CreateOperation(Mock.Of<INotificationManager>());
        operation.Dispose();
        Assert.Throws<ObjectDisposedException>(() => operation.HasDisposed);
    }
}
