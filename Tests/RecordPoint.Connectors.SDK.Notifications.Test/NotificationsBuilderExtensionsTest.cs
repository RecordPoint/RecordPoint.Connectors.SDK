using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using RecordPoint.Connectors.SDK.Configuration;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Test;
using RecordPoint.Connectors.SDK.Work;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Notifications.Test;

[Collection("Sequential")]
public class NotificationsBuilderExtensionsTest : CommonSutBase
{
    private const string UsePolledNotificationsEnvVar = "UsePolledNotifications";

    protected override IHostBuilder CreateSutBuilder()
    {
        var strategyMock = new Mock<INotificationStrategy>();

        strategyMock
        .SetupGet(s => s.NotificationType)
        .Returns("polled");

        var strategy2 = new Mock<INotificationStrategy>();
        strategy2
            .SetupGet(s => s.NotificationType)
            .Returns("webhook");

        var builder = base.CreateSutBuilder();

        builder.ConfigureServices(svcs => svcs
            .AddSingleton(strategyMock.Object)
            .AddSingleton(strategy2.Object)
            .AddSingleton(new Mock<IObservabilityScope>().Object)
            .AddSingleton(new Mock<ITelemetryTracker>().Object)
            .AddSingleton(new Mock<IR365ConfigurationClient>().Object)
            .AddSingleton(new Mock<IConnectorConfigurationManager>().Object)
            .AddSingleton(new Mock<IWorkQueueClient>().Object)
        );

        NotificationsBuilderExtensions.UseNotifications(builder);

        return builder;
    }

    [Theory]
    [InlineData("true")]
    [InlineData("TRUE")]
    public async Task UseNotifications_UsesPolledNotifications_WhenEnvVarIsSetupCorrectly(string usePolledNotification)
    {
        // Arrange
        Environment.SetEnvironmentVariable(UsePolledNotificationsEnvVar, usePolledNotification);

        // Act
        await StartSUTAsync();

        // Assert
        var notificationManager = Services.GetRequiredService<INotificationManager>();
        Assert.NotNull(notificationManager);
        Assert.IsType<PullNotificationManager>(notificationManager);

        // Cleanup
        Environment.SetEnvironmentVariable(UsePolledNotificationsEnvVar, null);
    }

    [Theory]
    [InlineData("true1")]
    [InlineData("false")]
    [InlineData("")]
    [InlineData(null)]
    public async Task UseNotifications_UsesWebhookNotifications_WhenEnvVarIsNotPolledEnable(string usePolledNotification)
    {
        // Arrange
        Environment.SetEnvironmentVariable(UsePolledNotificationsEnvVar, usePolledNotification);

        // Act
        await StartSUTAsync();

        // Assert
        var notificationManager = Services.GetRequiredService<INotificationManager>();
        Assert.NotNull(notificationManager);
        Assert.IsType<PushNotificationManager>(notificationManager);

        // Cleanup
        Environment.SetEnvironmentVariable(UsePolledNotificationsEnvVar, null);
    }

    [CollectionDefinition("Sequential", DisableParallelization = true)]
    public class SequentialCollection
    {
        // no code needed
    }
}
