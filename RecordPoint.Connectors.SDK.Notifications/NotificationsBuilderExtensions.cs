using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Notifications.Handlers;
using RecordPoint.Connectors.SDK.Notifications.Webhook;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.Notifications
{
    /// <summary>
    /// Notifications host builder extensions
    /// </summary>
    public static class NotificationsBuilderExtensions
    {              
        private const string UsePolledNotificationsEnv = "UsePolledNotifications";

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
                .UseBaseWebhookNotificationsServices();
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
                .UseBaseWebhookNotificationsServices();
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
                .UseBaseWebhookNotificationsServices();
            return hostBuilder;
        }

        /// <summary>
        /// Registers the notifications components for the Connector.
        /// An environment variable 'UsePolledNotifications' can be set with a value of 'true' to enable Poll based notifications
        /// Otherwise the Webhook based notifications will be registered.
        /// </summary>
        public static IHostBuilder UseNotifications(this IHostBuilder hostBuilder)
        {
            return IsPolledNotificationsEnabled()
                ? UsePolledNotifications(hostBuilder)
                : UseWebhookNotifications(hostBuilder);
        }

        /// <summary>
        /// Registers the notifications components for the Connector.
        /// An environment variable 'UsePolledNotifications' can be set with a value of 'true' to enable Poll based notifications
        /// Otherwise the Webhook based notifications will be registered.
        /// </summary>
        public static IHostBuilder UseNotifications<TContentRegistrationRequestAction>(this IHostBuilder hostBuilder) 
            where TContentRegistrationRequestAction : class, IContentRegistrationRequestAction
        {
            return IsPolledNotificationsEnabled()
                ? UsePolledNotifications<TContentRegistrationRequestAction>(hostBuilder)
                : UseWebhookNotifications<TContentRegistrationRequestAction>(hostBuilder);
        }

        /// <summary>
        /// Registers the notifications components for the Connector.
        /// An environment variable 'UsePolledNotifications' can be set with a value of 'true' to enable Poll based notifications
        /// Otherwise the Webhook based notifications will be registered.
        /// </summary>
        public static IHostBuilder UseNotifications<TContentRegistrationRequestAction, TConnectorSecretAction>(this IHostBuilder hostBuilder)
            where TContentRegistrationRequestAction : class, IContentRegistrationRequestAction
            where TConnectorSecretAction : class, IConnectorSecretAction
        {
            return IsPolledNotificationsEnabled()
                ? UsePolledNotifications<TContentRegistrationRequestAction, TConnectorSecretAction>(hostBuilder)
                : UseWebhookNotifications<TContentRegistrationRequestAction, TConnectorSecretAction>(hostBuilder);
        }

        /// <summary>
        /// Use asynchronous notification processing operation.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <returns>An IHostBuilder</returns>
        public static IHostBuilder UseAsyncNotificationOperation(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(services =>
            {
                services
                    .AddQueueableWorkOperation<AsyncNotificationOperation>();
            });
        }

        private static IHostBuilder UseBaseWebhookNotificationsServices(this IHostBuilder hostBuilder)
        {
            hostBuilder
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IR365NotificationClient, R365NotificationClient>()
                            .AddSingleton<INotificationApiManager, NotificationApiManager>()
                            .AddSingleton<INotificationManager, PushNotificationManager>()
                            .AddTransient<WebhookOperation>();
                });
            return hostBuilder;
        }

        private static IHostBuilder UseBasePolledNotificationsServices(this IHostBuilder hostBuilder)
        {
            return hostBuilder
                          .ConfigureServices((hostContext, services) =>
                          {
                              var configuration = hostContext.Configuration;
                              services.AddSingleton<IR365NotificationClient, R365NotificationClient>()
                                  .Configure<NotificationsPollerOptions>(configuration.GetSection(NotificationsPollerOptions.SECTION_NAME))
                                  .AddSingleton<INotificationManager, PullNotificationManager>()
                                  .AddTransient<PollNotificationsOperation>()
                                  .AddSingleton<INotificationApiManager, NotificationApiManager>()
                                  .AddHostedService<NotificationPollService>();
                          });
        }

        private static bool IsPolledNotificationsEnabled()
        {
            var usePolledNotifications = Environment.GetEnvironmentVariable(UsePolledNotificationsEnv);
            return string.Equals(usePolledNotifications, "true", StringComparison.OrdinalIgnoreCase);
        }
    }
}
