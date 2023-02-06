using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Observability;

namespace RecordPoint.Connectors.SDK.Databases.LocalDb
{
    /// <summary>
    /// LocalDb host builder extensions
    /// </summary>
    public static class LocalDbConnectorDatabaseBuilderExtensions
    {
        /// <summary>
        /// Use an Sql Server Local DB as the connector database
        /// </summary>
        /// <param name="hostBuilder">App host builder</param>
        /// <returns>Updated host builder</returns>
        public static IHostBuilder UseLocalDbConnectorDatabase(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(svcs => svcs
                .AddSingleton<LocalDbConnectorDatabaseProvider, LocalDbConnectorDatabaseProvider>()
                .AddSingleton(container =>
                {
                    // Wrap the local db provider with custom telemetry
                    // so that we know when connections are made
                    var realProvider = container.GetService<LocalDbConnectorDatabaseProvider>();
                    var tracker = container.GetService<ITelemetryTracker>();
                    return realProvider.WithAddedTelemetry(tracker);
                })
                .AddSingleton<IConnectorDatabaseClient, ConnectorDatabaseClient>())
                .AddDatabaseService();
        }
    }
}