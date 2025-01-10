using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RecordPoint.Connectors.SDK.Notifications.Webhook
{
    /// <summary>
    /// The webhook builder extensions.
    /// </summary>
    public static class WebhookBuilderExtensions
    {
        /// <summary>
        /// Use webhook notifications.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <returns>An IHostBuilder</returns>
        public static IHostBuilder UseWebhookNotifications(this IHostBuilder hostBuilder)
        {
            hostBuilder
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = hostContext.Configuration;
                    services.AddSingleton<INotificationManager, PullNotificationManager>();
                    services.AddTransient<WebhookOperation>();
                });
            return hostBuilder;
        }
    }
}

