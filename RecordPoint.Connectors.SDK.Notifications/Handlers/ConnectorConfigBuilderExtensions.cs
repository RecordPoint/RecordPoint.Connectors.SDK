using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RecordPoint.Connectors.SDK.Notifications.Handlers
{
    /// <summary>
    /// Host builder extensions for the connectors service
    /// </summary>
    public static class ConnectorConfigBuilderExtensions
    {
        /// <summary>
        /// Use the connector management services
        /// </summary>
        /// <param name="hostBuilder">Host builder to configure</param>
        /// <returns>Updated host builder</returns>
        public static IHostBuilder UseConnectorConfigHandlers(this IHostBuilder hostBuilder)
        {
            hostBuilder
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = hostContext.Configuration;
                    services
                        .AddSingleton<INotificationStrategy, ConnectorConfigCreatedHandler>()
                        .AddSingleton<INotificationStrategy, ConnectorConfigUpdatedHandler>()
                        .AddSingleton<INotificationStrategy, ConnectorConfigDeletedHandler>()
                        .AddSingleton<INotificationStrategy, ItemDestroyedHandler>()
                        .AddSingleton<INotificationStrategy, PingHandler>();
                });
            return hostBuilder;
        }
    }
}