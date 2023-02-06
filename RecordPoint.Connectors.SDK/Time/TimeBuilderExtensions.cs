using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Providers;

namespace RecordPoint.Connectors.SDK.Time
{

    /// <summary>
    /// Time builder extensions
    /// </summary>
    public static class TimeBuilderExtensions
    {

        /// <summary>
        /// Use the system clock to get the time
        /// </summary>
        /// <param name="hostBuilder">Host builder to target</param>
        public static IHostBuilder UseSystemTime(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(svcs => svcs
                .AddSingleton<IDateTimeProvider>(svp => DateTimeProvider.Instance)
            );
        }

    }
}
