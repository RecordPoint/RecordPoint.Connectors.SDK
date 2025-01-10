using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.ContentManager;
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
        /// Configure the base services for Polled Notifications
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <returns></returns>
        private static IHostBuilder UseBasePolledNotificationsServices(this IHostBuilder hostBuilder)
        {
            return hostBuilder
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IR365NotificationClient, R365NotificationClient>()
                        .AddSingleton<INotificationManager, PullNotificationManager>()
                        .AddTransient<PollNotificationsOperation>()
                        .AddHostedService<NotificationPollService>();
                });
        }

        /// <summary>
        /// Configure the host to use polled notifications
        /// </summary>
        /// <param name="hostBuilder">Host builder to configure</param>
        /// <returns>Configured host builder</returns>
        public static IHostBuilder UsePolledNotifications(this IHostBuilder hostBuilder)
        {
            return hostBuilder
                .UseConnectorConfigHandlers()
                .UseBasePolledNotificationsServices();
        }

        /// <summary>
        /// Configure the host to use polled notifications
        /// </summary>
        /// <param name="hostBuilder">Host builder to configure</param>
        /// <returns>Configured host builder</returns>
        public static IHostBuilder UsePolledNotifications<TContentRegistrationRequestAction>(this IHostBuilder hostBuilder)
            where TContentRegistrationRequestAction : class, IContentRegistrationRequestAction

        {
            return hostBuilder
                .UseConnectorConfigHandlers()
                .UseContentRegistrationHandler<TContentRegistrationRequestAction>()
                .UseBasePolledNotificationsServices();
        }

        /// <summary>
        /// Configure the host to use polled notifications
        /// </summary>
        /// <param name="hostBuilder">Host builder to configure</param>
        /// <returns>Configured host builder</returns>
        public static IHostBuilder UsePolledNotifications<TContentRegistrationRequestAction, TConnectorSecretAction>(this IHostBuilder hostBuilder)
            where TContentRegistrationRequestAction : class, IContentRegistrationRequestAction
            where TConnectorSecretAction : class, IConnectorSecretAction

        {
            return hostBuilder
                .UseNotificationHandlers<TContentRegistrationRequestAction, TConnectorSecretAction>()
                .UseBasePolledNotificationsServices();
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

        /// <summary>
        /// Configure the host to use Webhook based notifications
        /// </summary>
        /// <param name="hostBuilder">Host builder to configure</param>
        /// <returns></returns>
        public static IHostBuilder UseWebhookNotifications<TContentRegistrationRequestAction>(this IHostBuilder hostBuilder)
            where TContentRegistrationRequestAction : class, IContentRegistrationRequestAction
        {
            hostBuilder
                .UseConnectorConfigHandlers()
                .UseContentRegistrationHandler<TContentRegistrationRequestAction>()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IR365NotificationClient, R365NotificationClient>()
                            .AddSingleton<INotificationManager, PushNotificationManager>()
                            .AddTransient<WebhookOperation>();
                });
            return hostBuilder;
        }

        /// <summary>
        /// Configure the host to use Webhook based notifications
        /// </summary>
        /// <param name="hostBuilder">Host builder to configure</param>
        /// <returns></returns>
        public static IHostBuilder UseWebhookNotifications<TContentRegistrationRequestAction, TConnectorSecretAction>(this IHostBuilder hostBuilder)
            where TContentRegistrationRequestAction : class, IContentRegistrationRequestAction
            where TConnectorSecretAction : class, IConnectorSecretAction
        {
            hostBuilder
                .UseNotificationHandlers<TContentRegistrationRequestAction, TConnectorSecretAction>()
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
