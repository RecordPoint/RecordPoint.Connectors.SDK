using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RecordPoint.Connectors.SDK.Work
{
    /// <summary>
    /// Job builder extensions
    /// </summary>
    public static class ManagedWorkBuilderExtensions
    {
        /// <summary>
        /// Add the standard database backed job component
        /// </summary>
        /// <param name="services">Services to extend</param>
        /// <returns>Updated services</returns>
        public static IServiceCollection AddWorkStateManagement<TManagedWorkStatusManager>(this IServiceCollection services)
            where TManagedWorkStatusManager : class, IManagedWorkStatusManager
        {
            services
                .AddScoped<IManagedWorkStatusManager, TManagedWorkStatusManager>()
                .AddScoped<IManagedWorkFactory, ManagedWorkFactory>();
            return services;
        }


        /// <summary>
        /// Use the Managed Work Status Manager
        /// </summary>
        /// <param name="hostBuilder">Host builder to target</param>
        public static IHostBuilder UseWorkStateManager<TManagedWorkStatusManager>(this IHostBuilder hostBuilder)
            where TManagedWorkStatusManager : class, IManagedWorkStatusManager
        {
            return hostBuilder.ConfigureServices(services =>
            {
                services.AddWorkStateManagement<TManagedWorkStatusManager>();
            });
        }
    }
}
