using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RecordPoint.Connectors.SDK.Abstractions.Observability;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.Serialization;

namespace RecordPoint.Connectors.SDK.Observability
{

    /// <summary>
    /// Class that extends logger with observability/scope methods
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// Adds Date and Time information to Console Logs
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <returns></returns>
        public static IHostBuilder UseConsoleLogging(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((hostContext, services) =>
            {
                var loggingConfiguration = hostContext.Configuration.GetSection("Logging");
                var consoleLoggingOptions = loggingConfiguration.GetSection("Console").Get<ConsoleLoggingOptions>();
                consoleLoggingOptions ??= new ConsoleLoggingOptions();
                services.AddLogging(builder =>
                {
                    builder.AddConfiguration(loggingConfiguration);
                    builder.AddSimpleConsole(c => { c.TimestampFormat = $"{consoleLoggingOptions.DateTimeFormat} "; });
                });
            });
        }

        /// <summary>
        /// Log an event. This is the loggin correlate of of telemetry TrackEvent and should be invoked at the same time.
        /// </summary>
        /// <param name="logger">Target logger</param>
        /// <param name="name">Event name</param>
        /// <param name="dimensions">Observability dimensions to be logged</param>
        /// <param name="measures">Observability measures to be logged</param>
        public static void LogEvent(this ILogger logger, string name, Dimensions dimensions = null, Measures measures = null)
        {
            using var logScope = logger.BeginScope(dimensions, measures);
            logger.LogInformation(name);
        }

        /// <summary>
        /// Log an event exception. This is the logging correlate of telemetry TrackException and should be invoked at the same time.
        /// </summary>
        /// <param name="logger">Target logger</param>
        /// <param name="exception">Exception</param>
        /// <param name="name">Event name</param>
        /// <param name="dimensions">Observability dimensions to be logged</param>
        /// <param name="measures">Observability measures to be logged</param>
        public static void LogEventException(this ILogger logger, string name, Exception exception, Dimensions dimensions = null, Measures measures = null)
        {
            using var logScope = logger.BeginScope(dimensions, measures, exception);
            logger.LogError(exception, name);
        }

        /// <summary>
        /// Begin a logging scope containing observability properties
        /// </summary>
        /// <param name="logger">Logger to being scope for</param>
        /// <param name="dimensions">Observability dimensions</param>
        /// <param name="measures">Observability measures</param>
        /// <param name="exception"></param>
        /// <returns>Logging scope</returns>
        public static IDisposable BeginScope(this ILogger logger, Dimensions dimensions = null, Measures measures = null, Exception exception = null)
        {
            var dimensionPairs = (dimensions ?? new Dimensions()).Select(kp => new KeyValuePair<string, object>(kp.Key, kp.Value));
            var measurePairs = (measures ?? new Measures()).Select(kp => new KeyValuePair<string, object>(kp.Key, kp.Value));
            var exceptionPairs = (exception?.GetDimensions() ?? ImmutableDictionary<string, string>.Empty).Select(kp => new KeyValuePair<string, object>(kp.Key, kp.Value));
            // Reduce all input pairs into a dictionary. Take the last version of a value, which gives exceptions priority
            var logState = dimensionPairs
                .Concat(measurePairs)
                .Concat(exceptionPairs)
                .GroupBy(p => p.Key, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(kp => kp.Key, kp => kp.Last().Value);
            var logScope = logger.BeginScope(logState);
            return logScope;
        }

    }
}
