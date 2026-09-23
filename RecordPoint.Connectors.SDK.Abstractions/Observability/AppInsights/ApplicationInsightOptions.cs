namespace RecordPoint.Connectors.SDK.Observability.AppInsights;

/// <summary>
/// Configurable Options for Application Insights
/// </summary>
public class ApplicationInsightOptions
{
    /// <summary>
    /// The configuration section name
    /// </summary>
    public const string OPTION_NAME = "ApplicationInsights";

    /// <summary>
    /// The Connection String used to connect to the Application Insights instance
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;

    /// <summary>
    /// The minimum severity level to log traces to Application Insights
    /// </summary>
    public SeverityLevel LogLevel { get; set; } = SeverityLevel.Warning;
}
