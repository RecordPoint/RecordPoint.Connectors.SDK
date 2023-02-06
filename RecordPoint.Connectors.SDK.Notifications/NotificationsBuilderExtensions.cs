using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Notifications.Handlers;
using RecordPoint.Connectors.SDK.Notifications.Webhook;

namespace RecordPoint.Connectors.SDK.Notifications
{

    /// <summary>
    /// Notifications host builder extensions
    /// </summary>
    public static class NotificationsBuilderExtensions
    {

        /// <summary>
        /// Configure the host to use polled notifications
        /// </summary>
        /// <param name="hostBuilder">Host builder to configure</param>
        /// <returns>Configured host builder</returns>
        public static IHostBuilder UsePolledNotifications(this IHostBuilder hostBuilder)
        {
            return hostBuilder
                .UseConnectorConfigHandlers()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IR365NotificationClient, R365NotificationClient>()
                            .AddSingleton<INotificationManager, PullNotificationManager>()
                            .AddTransient<PollNotificationsOperation>()
                            .AddHostedService<NotificationPollService>();
                });
        }

        /// <summary>
        /// Configure the host to use Webhook based notifications
        /// </summary>
        /// <param name="hostBuilder">Host builder to configure</param>
        /// <returns></returns>
        public static IHostBuilder UseWebhookNotifications(this IHostBuilder hostBuilder)
        {
            hostBuilder
                .UseConnectorConfigHandlers()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IR365NotificationClient, R365NotificationClient>()
                            .AddSingleton<INotificationManager, PushNotificationManager>()
                            .AddTransient<WebhookOperation>();
                });
            return hostBuilder;
        }

    }
}
