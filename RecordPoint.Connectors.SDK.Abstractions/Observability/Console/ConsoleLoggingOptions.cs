namespace RecordPoint.Connectors.SDK.Observability.Console;

/// <summary>
/// Configurable Options for Console Logging
/// </summary>
public class ConsoleLoggingOptions
{
    /// <summary>
    /// The configuration section name
    /// </summary>
    public const string OPTION_NAME = "Console";

    /// <summary>
    /// The minimum severity level to log traces to the Console
    /// </summary>
    public SeverityLevel LogLevel { get; set; } = SeverityLevel.Information;

    /// <summary>
    /// Determines if the logger should write dimensions to the console
    /// </summary>
    public bool WriteDimensions { get; set; } = false;

    /// <summary>
    /// Determines if the logger should write measures to the console
    /// </summary>
    public bool WriteMeasures { get; set; } = false;
}
