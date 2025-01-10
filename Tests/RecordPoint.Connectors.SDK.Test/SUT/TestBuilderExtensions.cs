using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Test.SUT;
using RecordPoint.Connectors.SDK.Time;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.Tests.SUT
{

    /// <summary>
    /// Builder extensions that register useful services for testing
    /// </summary>
    public static class TestBuilderExtensions
    {

        /// <summary>
        /// Add test system context
        /// </summary>
        /// <param name="services">Services to add manager too</param>
        /// <returns></returns>
        public static IServiceCollection AddTestSystemContext(this IServiceCollection services)
        {
            return services.AddSingleton<ISystemContext, TestSystemContext>();
        }

        /// <summary>
        /// Add test worker
        /// </summary>
        /// <param name="services">Services to add manager too</param>
        /// <returns></returns>
        public static IServiceCollection AddTestQueueableWorkManager(this IServiceCollection services)
        {
            return services.AddSingleton<IQueueableWorkManager, TestWorkManager>();
        }

        /// <summary>
        /// Use on test positioning that adds a collection of standard testing services
        /// </summary>
        /// <param name="hostBuilder">App host builder</param>
        /// <returns></returns>
        public static IHostBuilder UseTestServices(this IHostBuilder hostBuilder)
        {
            hostBuilder
                .UseSystemTime()
                .ConfigureServices(serviceCollection =>
                {
                    serviceCollection
                        .AddTestSystemContext()
                        .AddTestQueueableWorkManager();
                });
            return hostBuilder;
        }

    }
}
