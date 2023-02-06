namespace RecordPoint.Connectors.SDK.Abstractions.Observability
{
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
        /// The format of the Date/Time stamp printed to the Console
        /// </summary>
        public string DateTimeFormat { get; set; } = "[yyyy-MM-dd HH:mm:ss]";
    }
}
