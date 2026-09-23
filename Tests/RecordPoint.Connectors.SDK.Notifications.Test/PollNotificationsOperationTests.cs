#nullable enable
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Notifications.Test;

public class PollNotificationsOperationTests
{
    private static readonly string[] ExpectedHandledOrder = { "earlier", "later" };

    private readonly Mock<INotificationManager> _notificationManager = new();
    private readonly Mock<IR365NotificationClient> _client = new();
    private readonly Mock<ITelemetryTracker> _telemetry = new();

    private PollNotificationsOperation CreateOperation()
    {
        var scope = new Mock<IObservabilityScope>();
        scope.Setup(x => x.BeginScope(It.IsAny<Dimensions>(), It.IsAny<Measures>())).Returns(Mock.Of<IDisposable>());
        var dateTimeProvider = new Mock<IDateTimeProvider>();
        dateTimeProvider.SetupGet(x => x.UtcNow).Returns(DateTime.UtcNow);

        return new PollNotificationsOperation(
            new ServiceCollection().BuildServiceProvider(),
            _notificationManager.Object,
            _client.Object,
            scope.Object,
            _telemetry.Object,
            dateTimeProvider.Object);
    }

    [Fact]
    public async Task RunAsync_DoesNothing_WhenNotConfigured()
    {
        _client.Setup(x => x.IsConfigured()).Returns(false);
        var operation = CreateOperation();

        await operation.RunAsync(string.Empty, CancellationToken.None);

        Assert.Equal(WorkResultType.Complete, operation.ResultType);
        Assert.Equal(0, operation.NotificationCount);
        _client.Verify(x => x.GetAllPendingNotifications(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task RunAsync_Completes_WhenNoNotifications()
    {
        _client.Setup(x => x.IsConfigured()).Returns(true);
        _client.Setup(x => x.GetAllPendingNotifications(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<ConnectorNotificationModel>());
        var operation = CreateOperation();

        await operation.RunAsync(string.Empty, CancellationToken.None);

        Assert.Equal(WorkResultType.Complete, operation.ResultType);
        Assert.Equal(0, operation.NotificationCount);
        _notificationManager.Verify(x => x.HandleNotificationAsync(It.IsAny<ConnectorNotificationModel>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task RunAsync_HandlesAllNotifications_InTimestampOrder()
    {
        var now = DateTime.UtcNow;
        var notifications = new List<ConnectorNotificationModel>
        {
            new ConnectorNotificationModel { Id = "later", Timestamp = now.AddSeconds(1) },
            new ConnectorNotificationModel { Id = "earlier", Timestamp = now.AddSeconds(-1) }
        };
        _client.Setup(x => x.IsConfigured()).Returns(true);
        _client.Setup(x => x.GetAllPendingNotifications(It.IsAny<CancellationToken>())).ReturnsAsync(notifications);

        var handled = new List<string>();
        _notificationManager.Setup(x => x.HandleNotificationAsync(It.IsAny<ConnectorNotificationModel>(), It.IsAny<CancellationToken>()))
            .Callback<ConnectorNotificationModel, CancellationToken>((n, _) => handled.Add(n.Id))
            .Returns(Task.CompletedTask);

        var operation = CreateOperation();

        await operation.RunAsync(string.Empty, CancellationToken.None);

        Assert.Equal(WorkResultType.Complete, operation.ResultType);
        Assert.Equal(2, operation.NotificationCount);
        Assert.Equal(ExpectedHandledOrder, handled);
    }
}
