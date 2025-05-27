using Microsoft.Extensions.Options;
using System;

namespace RecordPoint.Connectors.SDK.Observability.Console;

/// <summary>
/// Console Logging Sink
/// </summary>
public class ConsoleLoggingSink(IOptions<ConsoleLoggingOptions> consoleLoggingOptions) : ITelemetrySink
{
    /// <summary>
    /// Logs an event to the console
    /// </summary>
    /// <param name="name"></param>
    /// <param name="dimensions"></param>
    /// <param name="measures"></param>
    public void TrackEvent(string name, Dimensions dimensions = null, Measures measures = null)
    {
        System.Console.ResetColor();

        System.Console.WriteLine($"{DateTimeOffset.Now:yyyy-MM-dd HH:mm:ss.fff zzz} EVNT: {name}");
        WriteDimensions(dimensions);
        WriteMeasures(measures);
    }

    /// <summary>
    /// Logs an exception to the console
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="dimensions"></param>
    /// <param name="measures"></param>
    public void TrackException(Exception exception, Dimensions dimensions = null, Measures measures = null)
    {
        System.Console.ForegroundColor = ConsoleColor.DarkRed;

        System.Console.WriteLine($"{DateTimeOffset.Now:yyyy-MM-dd HH:mm:ss.fff zzz} FAIL: [{exception.GetType()}]");
        System.Console.WriteLine(exception);
        WriteDimensions(dimensions);
        WriteMeasures(measures);

        System.Console.ResetColor();
    }

    /// <summary>
    /// Logs a trace to the console
    /// </summary>
    /// <param name="message"></param>
    /// <param name="severityLevel"></param>
    /// <param name="dimensions"></param>
    public void TrackTrace(string message, SeverityLevel severityLevel, Dimensions dimensions = null)
    {
        if ((int)consoleLoggingOptions.Value.LogLevel > (int)severityLevel)
            return;

        System.Console.ResetColor();

        System.Console.ForegroundColor = severityLevel switch
        {
            SeverityLevel.Verbose => ConsoleColor.Gray,
            SeverityLevel.Information => System.Console.ForegroundColor,
            SeverityLevel.Warning => ConsoleColor.Yellow,
            SeverityLevel.Error => ConsoleColor.Red,
            SeverityLevel.Critical => ConsoleColor.DarkRed,
            _ => System.Console.ForegroundColor
        };

        System.Console.WriteLine($"{DateTimeOffset.Now:yyyy-MM-dd HH:mm:ss.fff zzz} {GetLogLevelString(severityLevel)}: {message}");
        WriteDimensions(dimensions);

        System.Console.ResetColor();
    }

    private void WriteDimensions(Dimensions dimensions)
    {
        if (consoleLoggingOptions.Value.WriteDimensions && dimensions != null)
        {
            foreach (var dimension in dimensions)
            {
                System.Console.WriteLine($"  {dimension.Key}: {dimension.Value}");
            }
        }
    }

    private void WriteMeasures(Measures measures)
    {
        if (consoleLoggingOptions.Value.WriteMeasures && measures != null)
        {
            foreach (var measure in measures)
            {
                System.Console.WriteLine($"  {measure.Key}: {measure.Value}");
            }
        }
    }

    private static string GetLogLevelString(SeverityLevel severityLevel)
    {
        return severityLevel switch
        {
            SeverityLevel.Verbose => "DBUG",
            SeverityLevel.Information => "INFO",
            SeverityLevel.Warning => "WARN",
            SeverityLevel.Error => "FAIL",
            SeverityLevel.Critical => "CRIT",
            _ => throw new ArgumentOutOfRangeException(nameof(severityLevel))
        };
    }
}
