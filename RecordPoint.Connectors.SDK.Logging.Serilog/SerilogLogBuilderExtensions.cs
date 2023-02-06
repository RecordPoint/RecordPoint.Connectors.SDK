using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability.AppInsights;
using RecordPoint.Connectors.SDK.Observability.FileLogs;
using Serilog;
using Serilog.Events;

namespace RecordPoint.Connectors.SDK.Logging.Serilog
{

    /// <summary>
    /// Serilog log builder extensions
    /// </summary>
    public static class SerilogLogBuilderExtensions
    {
        public const string LOG_TEMPLATE = "[{Timestamp:HH:mm:ss} {Level:u3}]: {Message:lj}{NewLine}{Properties}{NewLine}{Exception}";

        /// <summary>
        /// Use the serilog based appInsights
        /// </summary>
        /// <param name="hostBuilder">Host builder to configure</param>
        /// <returns>Updated host builder</returns>
        public static IHostBuilder UseSerilogApplicationInsightsLogger(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog(ConfigureLogging);
            return hostBuilder;
        }

        /// <summary>
        /// Add console to the logger configuration
        /// </summary>
        private static void AddConsoleLogging(LoggerConfiguration loggerConfiguration)
        {
            loggerConfiguration
               .WriteTo.Console(outputTemplate: LOG_TEMPLATE);
        }

        private static void AddFileLogging(IServiceProvider serviceProvider, LoggerConfiguration loggerConfiguration, FileLogOptions fileLogOptions)
        {
            var onPremisesContext = serviceProvider.GetRequiredService<ISystemContext>();
            fileLogOptions.LogPath ??= Path.Combine(onPremisesContext.GetDataRootPath(), "Log", "log.txt");
            fileLogOptions.OutputTemplate ??= LOG_TEMPLATE;
            if (!Enum.TryParse(fileLogOptions.RollingInterval, true, out RollingInterval rollInt))
            {
                rollInt = RollingInterval.Day;
            };
            loggerConfiguration
               .WriteTo.File(
                path: fileLogOptions.LogPath, // Path to write the log to. Note that the name may be changed if rollingInterval is enabled; a datetime will be appended (e.g. "log-<date>.txt" if "log.txt" was the specified name).
                rollingInterval: rollInt, // How often to roll over to a new file, based on time.
                rollOnFileSizeLimit: fileLogOptions.RollOnFileSizeLimit, // Whether to roll over to a new file when the file size limit is reached (1gb by default). If false, will simply stop logging.
                fileSizeLimitBytes: fileLogOptions.FileSizeLimitBytes, // How large to allow a single log file to grow. Listed value here is 300mb.
                retainedFileCountLimit: fileLogOptions.RetainedFileCountLimit, // Number of files to retain at any one time.
                outputTemplate: fileLogOptions.OutputTemplate);
        }

        /// <summary>
        /// Config appInsights as main logging and file logging is backup
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="loggerConfiguration"></param>
        /// <param name="hostBuilderContext"></param>
        private static void ConfigureLogging(HostBuilderContext hostBuilderContext, IServiceProvider serviceProvider, LoggerConfiguration loggerConfiguration)
        {
            // Load general config. This will grab everything under "Serilog" in the configuration.
            loggerConfiguration
                .ReadFrom.Configuration(hostBuilderContext.Configuration)
                .Enrich.FromLogContext();

            // Check if AppInsights options are provided, and add logging if so.
            var appInsightOptions = hostBuilderContext.Configuration.GetSection(ApplicationInsightOptions.OPTION_NAME)
                .Get<ApplicationInsightOptions>();
            if (!string.IsNullOrEmpty(appInsightOptions?.InstrumentationKey))
            {
                AppInsightsLogging(loggerConfiguration, appInsightOptions);
            }

            // Check if file logging options are provided. Use if so, use defaults if not.
            var fileLogOptions = hostBuilderContext.Configuration.GetSection(FileLogOptions.OPTION_NAME)
                .Get<FileLogOptions>();
            AddFileLogging(serviceProvider, loggerConfiguration, fileLogOptions);

            // Always use default console logging.
            // TODO: Console logging options?
            AddConsoleLogging(loggerConfiguration);
        }

        private static void AppInsightsLogging(LoggerConfiguration loggerConfiguration, ApplicationInsightOptions applicationInsightOptions)
        {
            var telemetryConfiguration = TelemetryConfiguration
                .CreateDefault();
            loggerConfiguration
                .Enrich.FromLogContext()
                .WriteTo.ApplicationInsights(telemetryConfiguration, TelemetryConverter.Traces, LogEventLevel.Information);
        }

    }
}
