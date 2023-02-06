namespace RecordPoint.Connectors.SDK.Observability.AppInsights
{
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
        /// The Instrumentation Key used to connect to the Application Insights instance
        /// </summary>
        public string InstrumentationKey { get; set; } = string.Empty;
    }
}
