#nullable enable
using Moq;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Observability;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Notifications.Test;

public class NotificationManagerTests
{
    private const string KnownType = "ConnectorConfigUpdated";

    private static Mock<IObservabilityScope> CreateObservabilityScope()
    {
        var scope = new Mock<IObservabilityScope>();
        scope.Setup(x => x.BeginScope(It.IsAny<Dimensions>(), It.IsAny<Measures>()))
            .Returns(Mock.Of<IDisposable>());
        return scope;
    }

    private static Mock<INotificationStrategy> CreateStrategy(string type, Func<Task<NotificationOutcome>> handler)
    {
        var strategy = new Mock<INotificationStrategy>();
        strategy.SetupGet(x => x.NotificationType).Returns(type);
        strategy.Setup(x => x.HandleNotificationAsync(It.IsAny<ConnectorNotificationModel>(), It.IsAny<CancellationToken>()))
            .Returns(handler);
        return strategy;
    }

    private static ConnectorNotificationModel Notification(string type, bool withConfig = true)
    {
        return new ConnectorNotificationModel
        {
            Id = Guid.NewGuid().ToString(),
            NotificationType = type,
            TenantId = Guid.NewGuid().ToString(),
            ConnectorConfig = withConfig ? new ConnectorConfigModel { Id = "cfg-1" } : null
        };
    }

    #region PullNotificationManager

    [Fact]
    public async Task Pull_UnknownType_AcknowledgesError_AndDoesNotInvokeStrategy()
    {
        var strategy = CreateStrategy(KnownType, () => Task.FromResult(NotificationOutcome.OK()));
        var client = new Mock<IR365NotificationClient>();
        var telemetry = new Mock<ITelemetryTracker>();

        var manager = new PullNotificationManager(
            client.Object, new[] { strategy.Object }, CreateObservabilityScope().Object, telemetry.Object);

        var notification = Notification("SomethingUnknown");
        await manager.HandleNotificationAsync(notification, CancellationToken.None);

        client.Verify(x => x.AcknowledgeNotificationAsync(
            notification, ProcessingResult.NotificationError, It.Is<string>(s => s.Contains("SomethingUnknown")), It.IsAny<CancellationToken>()), Times.Once);
        telemetry.Verify(x => x.TrackTrace(It.IsAny<string>(), SeverityLevel.Warning, It.IsAny<Dimensions>()), Times.Once);
        strategy.Verify(x => x.HandleNotificationAsync(It.IsAny<ConnectorNotificationModel>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Pull_OkOutcome_AcknowledgesOk()
    {
        var strategy = CreateStrategy(KnownType, () => Task.FromResult(NotificationOutcome.OK()));
        var client = new Mock<IR365NotificationClient>();
        var telemetry = new Mock<ITelemetryTracker>();

        var manager = new PullNotificationManager(
            client.Object, new[] { strategy.Object }, CreateObservabilityScope().Object, telemetry.Object);

        var notification = Notification(KnownType);
        await manager.HandleNotificationAsync(notification, CancellationToken.None);

        client.Verify(x => x.AcknowledgeNotificationAsync(
            notification, ProcessingResult.OK, It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        telemetry.Verify(x => x.TrackTrace("Notification Processed OK", SeverityLevel.Information, It.IsAny<Dimensions>()), Times.Once);
    }

    [Fact]
    public async Task Pull_FailedOutcome_AcknowledgesError()
    {
        var strategy = CreateStrategy(KnownType, () => Task.FromResult(NotificationOutcome.Failed("boom")));
        var client = new Mock<IR365NotificationClient>();
        var telemetry = new Mock<ITelemetryTracker>();

        var manager = new PullNotificationManager(
            client.Object, new[] { strategy.Object }, CreateObservabilityScope().Object, telemetry.Object);

        var notification = Notification(KnownType, withConfig: false);
        await manager.HandleNotificationAsync(notification, CancellationToken.None);

        client.Verify(x => x.AcknowledgeNotificationAsync(
            notification, ProcessingResult.NotificationError, "boom", It.IsAny<CancellationToken>()), Times.Once);
        telemetry.Verify(x => x.TrackTrace("Notification Processing Failed", SeverityLevel.Error, It.IsAny<Dimensions>()), Times.Once);
    }

    [Fact]
    public async Task Pull_StrategyThrows_AcknowledgesError_AndTracksException()
    {
        var strategy = CreateStrategy(KnownType, () => throw new InvalidOperationException("kaboom"));
        var client = new Mock<IR365NotificationClient>();
        var telemetry = new Mock<ITelemetryTracker>();

        var manager = new PullNotificationManager(
            client.Object, new[] { strategy.Object }, CreateObservabilityScope().Object, telemetry.Object);

        var notification = Notification(KnownType);
        await manager.HandleNotificationAsync(notification, CancellationToken.None);

        client.Verify(x => x.AcknowledgeNotificationAsync(
            notification, ProcessingResult.NotificationError, "kaboom", It.IsAny<CancellationToken>()), Times.Once);
        telemetry.Verify(x => x.TrackException(It.Is<Exception>(e => e.Message == "kaboom"), It.IsAny<Dimensions>(), It.IsAny<Measures>()), Times.Once);
    }

    #endregion

    #region PushNotificationManager

    [Fact]
    public async Task Push_UnknownType_TracksWarning_AndReturns()
    {
        var strategy = CreateStrategy(KnownType, () => Task.FromResult(NotificationOutcome.OK()));
        var telemetry = new Mock<ITelemetryTracker>();

        var manager = new PushNotificationManager(
            new[] { strategy.Object }, CreateObservabilityScope().Object, telemetry.Object);

        await manager.HandleNotificationAsync(Notification("Unknown"), CancellationToken.None);

        telemetry.Verify(x => x.TrackTrace(It.Is<string>(s => s.Contains("Unknown")), SeverityLevel.Warning, It.IsAny<Dimensions>()), Times.Once);
        strategy.Verify(x => x.HandleNotificationAsync(It.IsAny<ConnectorNotificationModel>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Push_OkOutcome_TracksInformation()
    {
        var strategy = CreateStrategy(KnownType, () => Task.FromResult(NotificationOutcome.OK()));
        var telemetry = new Mock<ITelemetryTracker>();

        var manager = new PushNotificationManager(
            new[] { strategy.Object }, CreateObservabilityScope().Object, telemetry.Object);

        await manager.HandleNotificationAsync(Notification(KnownType), CancellationToken.None);

        telemetry.Verify(x => x.TrackTrace("Notification Processed OK", SeverityLevel.Information, It.IsAny<Dimensions>()), Times.Once);
    }

    [Fact]
    public async Task Push_FailedOutcome_TracksError()
    {
        var strategy = CreateStrategy(KnownType, () => Task.FromResult(NotificationOutcome.Failed("bad")));
        var telemetry = new Mock<ITelemetryTracker>();

        var manager = new PushNotificationManager(
            new[] { strategy.Object }, CreateObservabilityScope().Object, telemetry.Object);

        await manager.HandleNotificationAsync(Notification(KnownType), CancellationToken.None);

        telemetry.Verify(x => x.TrackTrace("Notification Processing Failed", SeverityLevel.Error, It.IsAny<Dimensions>()), Times.Once);
    }

    [Fact]
    public async Task Push_StrategyThrows_TracksException()
    {
        var strategy = CreateStrategy(KnownType, () => throw new InvalidOperationException("explode"));
        var telemetry = new Mock<ITelemetryTracker>();

        var manager = new PushNotificationManager(
            new[] { strategy.Object }, CreateObservabilityScope().Object, telemetry.Object);

        await manager.HandleNotificationAsync(Notification(KnownType), CancellationToken.None);

        telemetry.Verify(x => x.TrackException(It.Is<Exception>(e => e.Message == "explode"), It.IsAny<Dimensions>(), It.IsAny<Measures>()), Times.Once);
    }

    #endregion
}
