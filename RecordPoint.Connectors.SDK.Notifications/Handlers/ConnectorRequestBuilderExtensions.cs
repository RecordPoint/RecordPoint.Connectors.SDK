using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Requests;

namespace RecordPoint.Connectors.SDK.Notifications.Handlers
{
    /// <summary>
    /// Host builder extensions for answering connector requests.
    /// </summary>
    /// <remarks>Registered as an INotificationStrategy, so webhook and polled both pick it up.</remarks>
    public static class ConnectorRequestBuilderExtensions
    {
        /// <summary>Enables connector requests. Add handlers with AddConnectorRequestHandler.</summary>
        /// <param name="hostBuilder">Host builder to configure.</param>
        /// <returns>Updated host builder.</returns>
        /// <remarks>Safe to call twice: two entries for one notification type would break dispatch.</remarks>
        public static IHostBuilder UseConnectorRequestHandlers(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((hostContext, services) =>
            {
                // Shared with the ConnectorSecret path, so a handler gets usable values either way.
                services.TryAddSingleton<IConnectorSecretDecryptor, ConnectorSecretDecryptor>();
                services.TryAddSingleton<IConnectorRequestCallbackClient, ConnectorRequestCallbackClient>();
                services.TryAddEnumerable(
                    ServiceDescriptor.Singleton<INotificationStrategy, ConnectorRequestHandler>());
            });
        }

        /// <summary>Registers one request handler. Call once per request type answered.</summary>
        /// <typeparam name="THandler">The handler to register.</typeparam>
        /// <param name="hostBuilder">Host builder to configure.</param>
        /// <returns>Updated host builder.</returns>
        /// <remarks>Also enables connector requests. Two for one type is allowed; the last wins.</remarks>
        public static IHostBuilder AddConnectorRequestHandler<THandler>(this IHostBuilder hostBuilder)
            where THandler : class, IConnectorRequestHandler
        {
            hostBuilder.UseConnectorRequestHandlers();

            return hostBuilder.ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<IConnectorRequestHandler, THandler>();
            });
        }
    }
}
