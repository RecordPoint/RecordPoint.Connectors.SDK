using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.ContentManager;

namespace RecordPoint.Connectors.SDK.Notifications.Handlers
{
    /// <summary>
    /// Host builder extensions for the connectors service
    /// </summary>
    public static class ConnectorConfigBuilderExtensions
    {
        /// <summary>
        /// Use all connector notification handlers
        /// </summary>
        /// <param name="hostBuilder">Host builder to configure</param>
        /// <returns>Updated host builder</returns>
        public static IHostBuilder UseNotificationHandlers<TContentRegistrationRequestAction, TConnectorSecretAction>(this IHostBuilder hostBuilder)
            where TContentRegistrationRequestAction : class, IContentRegistrationRequestAction
            where TConnectorSecretAction : class, IConnectorSecretAction
        {
            hostBuilder
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .AddSingleton<INotificationStrategy, ConnectorConfigCreatedHandler>()
                        .AddSingleton<INotificationStrategy, ConnectorConfigUpdatedHandler>()
                        .AddSingleton<INotificationStrategy, ConnectorConfigDeletedHandler>()
                        .AddSingleton<INotificationStrategy, ItemDestroyedHandler>()
                        .AddSingleton<INotificationStrategy, PingHandler>()
                        .AddSingleton<INotificationStrategy, ContentRegistrationHandler>()
                        .AddSingleton<INotificationStrategy, ConnectorSecretHandler>()
                        .AddSingleton<IContentRegistrationRequestAction, TContentRegistrationRequestAction>()
                        .AddSingleton<IConnectorSecretAction, TConnectorSecretAction>();

                });
            return hostBuilder;
        }

        /// <summary>
        /// Use base connector notification handlers
        /// </summary>
        /// <param name="hostBuilder">Host builder to configure</param>
        /// <returns>Updated host builder</returns>
        public static IHostBuilder UseConnectorConfigHandlers(this IHostBuilder hostBuilder)
        {
            hostBuilder
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .AddSingleton<INotificationStrategy, ConnectorConfigCreatedHandler>()
                        .AddSingleton<INotificationStrategy, ConnectorConfigUpdatedHandler>()
                        .AddSingleton<INotificationStrategy, ConnectorConfigDeletedHandler>()
                        .AddSingleton<INotificationStrategy, ItemDestroyedHandler>()
                        .AddSingleton<INotificationStrategy, PingHandler>();
                });
            return hostBuilder;
        }

        /// <summary>
        /// Use the connector secret handler
        /// </summary>
        /// <param name="hostBuilder">Host builder to configure</param>
        /// <returns>Updated host builder</returns>
        public static IHostBuilder UseConnectorSecretHandler<TConnectorSecretAction>(this IHostBuilder hostBuilder)
            where TConnectorSecretAction : class, IConnectorSecretAction
        {
            hostBuilder
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .AddSingleton<INotificationStrategy, ConnectorSecretHandler>()
                        .AddSingleton<IConnectorSecretAction, TConnectorSecretAction>();
                });
            return hostBuilder;
        }

        /// <summary>
        /// Use the connector content registration handler
        /// </summary>
        /// <param name="hostBuilder">Host builder to configure</param>
        /// <returns>Updated host builder</returns>
        public static IHostBuilder UseContentRegistrationHandler<TContentRegistrationRequestAction>(this IHostBuilder hostBuilder)
            where TContentRegistrationRequestAction : class, IContentRegistrationRequestAction
        {
            hostBuilder
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .AddSingleton<INotificationStrategy, ContentRegistrationHandler>()
                        .AddSingleton<IContentRegistrationRequestAction, TContentRegistrationRequestAction>();
                });
            return hostBuilder;
        }
    }
}