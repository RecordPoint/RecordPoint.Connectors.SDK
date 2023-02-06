using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Databases;

namespace RecordPoint.Connectors.SDK.Test.Mock.Databases
{
    /// <summary>
    /// Mock connector database builder extensions
    /// </summary>
    public static class MockConnectorDatabaseBuilderExtensions
    {
        /// <summary>
        /// Add the mock connector database
        /// </summary>
        /// <param name="hostBuilder">Service collection to add to</param>
        /// <returns>Updated service collection</returns>
        public static IHostBuilder UseMockConnectorDatabase(this IHostBuilder hostBuilder) =>
            hostBuilder
                .ConfigureServices(services =>
                    services
                        .AddSingleton<IConnectorDatabaseProvider, MockConnectorDatabaseProvider>()
                        .AddSingleton<IConnectorDatabaseClient, ConnectorDatabaseClient>()
                );
    }
}
