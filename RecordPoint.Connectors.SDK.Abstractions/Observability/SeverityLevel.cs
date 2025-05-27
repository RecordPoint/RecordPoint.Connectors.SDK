namespace RecordPoint.Connectors.SDK.Observability;

/// <summary>
/// Trace Logging Severity Levels
/// </summary>
public enum SeverityLevel
{
    //Note the integer values are used to sort the severity levels in the order of increasing severity.
    //We start at 1 instead of zero, as our levels don't exactly align with the dotnet LogLevels (we do not have trace, and we use "verbose" instead of debug)

    /// <summary>
    /// Verbose severity level.
    /// </summary>
    Verbose = 1,
    /// <summary>
    /// Information severity level.
    /// </summary>
    Information = 2,
    /// <summary>
    /// Warning severity level.
    /// </summary>
    Warning = 3,
    /// <summary>
    /// Error severity level.
    /// </summary>
    Error = 4,
    /// <summary>
    /// Critical severity level.
    /// </summary>
    Critical = 5,
    /// <summary>
    /// Do not log anything.
    /// </summary>
    None = 6
}
