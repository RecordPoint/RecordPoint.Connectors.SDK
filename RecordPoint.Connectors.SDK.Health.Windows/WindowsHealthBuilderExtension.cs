using Microsoft.Extensions.DependencyInjection;

namespace RecordPoint.Connectors.SDK.Health.Windows
{

    /// <summary>
    /// Windows Health builder extensions
    /// </summary>
    public static class WindowsHealthBuilderExtension
    {

        /// <summary>
        /// Add Windows health components
        /// </summary>
        /// <param name="services">Service collection to add to</param>
        /// <returns>Updated service collection</returns>
        public static IServiceCollection AddOnPremHealthComponents(this IServiceCollection services)
        {
            return services.AddSingleton<IHealthCheckStrategy, WindowsHealthChecker>();
        }

    }
}
