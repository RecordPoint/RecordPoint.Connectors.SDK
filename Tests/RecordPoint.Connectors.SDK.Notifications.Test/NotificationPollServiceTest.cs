using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Moq;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Test;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Notifications.Test
{

    /// <summary>
    /// SUT for NotificationPollService tests
    /// </summary>
    public class NotificationPollServiceSut : CommonSutBase
    {
        private Mock<INotificationManager> _mockNotificationManager;
        private Mock<IR365NotificationClient> _mockR365Client;
        private IOptions<NotificationsPollerOptions> _options;

        public Mock<INotificationManager> NotificationManager => _mockNotificationManager;

        public Mock<IR365NotificationClient> R365Client => _mockR365Client;

        public TimeSpan GetPollInternal() => TimeSpan.FromSeconds(_options.Value.PollIntervalSeconds);

        protected override IHostBuilder CreateSutBuilder()
        {
            _mockNotificationManager = new Mock<INotificationManager>();
            _mockR365Client = new Mock<IR365NotificationClient>();
            _options = Options.Create(new NotificationsPollerOptions(){ PollIntervalSeconds = 1 });

            var builder = base.CreateSutBuilder();
            builder.ConfigureServices(svcs => svcs
                .AddTransient<PollNotificationsOperation>()
                .AddSingleton(_mockNotificationManager.Object)
                .AddSingleton(_mockR365Client.Object)
                .AddSingleton<NotificationPollService>()
            );
            return builder;
        }

    }

    public class PollNotificationWorkItemTest: CommonTestBase<NotificationPollServiceSut>
    {

        [Fact]
        public async Task HandleNotifications_CorrectOrder()
        {
            await StartSutAsync();
            var pollService = Services.GetRequiredService<NotificationPollService>();
            var timeStamp = DateTime.UtcNow;

            // Setup notifications in wrong timestamp
            var listOfNotifications = new List<ConnectorNotificationModel>()
            {
                new ConnectorNotificationModel() {Timestamp = timeStamp},
                new ConnectorNotificationModel() {Timestamp = timeStamp.AddSeconds(-1)},
                new ConnectorNotificationModel() {Timestamp = timeStamp.AddSeconds(1)}
            };

            var sequence = new MockSequence();
            var notificationManager = SUT.NotificationManager;

            var executedNotifications = new List<ConnectorNotificationModel>();
            notificationManager.InSequence(sequence)
                .Setup(x => x.HandleNotificationAsync(It.IsAny<ConnectorNotificationModel>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Callback<ConnectorNotificationModel, CancellationToken>((x, y) =>
                {
                    executedNotifications.Add(x);
                });
            notificationManager.InSequence(sequence)
                .Setup(x => x.HandleNotificationAsync(It.IsAny<ConnectorNotificationModel>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Callback<ConnectorNotificationModel, CancellationToken>((x, y) =>
                {
                    executedNotifications.Add(x);
                });
            notificationManager.InSequence(sequence)
                .Setup(x => x.HandleNotificationAsync(It.IsAny<ConnectorNotificationModel>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Callback<ConnectorNotificationModel, CancellationToken>((x, y) =>
                {
                    executedNotifications.Add(x);
                });

            SUT.R365Client?.Setup(x => x.IsConfigured()).Returns(true);
            SUT.R365Client?.Setup(x=> x.GetAllPendingNotifications(It.IsAny<CancellationToken>())).ReturnsAsync(listOfNotifications);

            _ = pollService.StartAsync(CancellationToken.None);

            await Task.Delay(SUT.GetPollInternal()).ConfigureAwait(false);
            await pollService.StopAsync(CancellationToken.None);

            notificationManager?.Verify(x => x.HandleNotificationAsync(It.IsAny<ConnectorNotificationModel>(), It.IsAny<CancellationToken>()), Times.Exactly(3));

            // Expect notification handle in right time order
            Assert.Equal(executedNotifications[0], listOfNotifications[1]);
            Assert.Equal(executedNotifications[1], listOfNotifications[0]);
            Assert.Equal(executedNotifications[2], listOfNotifications[2]);
        }
    }
}
