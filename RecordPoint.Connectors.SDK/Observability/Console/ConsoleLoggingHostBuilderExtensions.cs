using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RecordPoint.Connectors.SDK.Observability.Console;

/// <summary>
/// Host builder extensions for registering a console logger
/// </summary>
public static class ConsoleLoggingHostBuilderExtensions
{
    /// <summary>
    /// Register a console logger with the host
    /// </summary>
    public static IHostBuilder UseConsoleLogging(this IHostBuilder hostBuilder)
    {
        return hostBuilder
            .UseObservabilityTracking()
            .ConfigureServices((hostContext, services) =>
            {
                var consoleLoggingOptions = hostContext.Configuration.GetSection(ConsoleLoggingOptions.OPTION_NAME);

                services
                    .Configure<ConsoleLoggingOptions>(consoleLoggingOptions)
                    .AddSingleton<ITelemetrySink, ConsoleLoggingSink>();
            });
    }
}
