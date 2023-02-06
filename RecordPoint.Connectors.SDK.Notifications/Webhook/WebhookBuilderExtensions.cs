using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RecordPoint.Connectors.SDK.Notifications.Webhook
{
    public static class WebhookBuilderExtensions
    {
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

