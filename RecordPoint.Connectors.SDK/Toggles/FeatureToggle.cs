namespace RecordPoint.Connectors.SDK.Toggles
{
    /// <summary>
    /// Central definition for all Feature Toggle values currently used throughout the application.
    /// Implementing FeatureToggle definitions as classes gives us greater flexibility in future - for example
    /// we can add additional fields to FeatureToggles to control their behaviour.
    /// 
    /// Every toggle here must contain a comment description that indicates what the toggle is used for.
    /// The description should match what is provided in LaunchDarkly.
    /// 
    /// When removing a toggle, simply delete the factory method for it below, and fix the resulting compile errors accordingly.
    /// </summary>
    public class FeatureToggle
    {
        /// <summary>
        /// Optionally override the log level defined in appsettings
        /// Returns an int representation of a LogLevel Enum value or -1 if the value in appsettings should be restricted
        /// </summary>
        /// <returns></returns>
        public static FeatureToggle LogLevel() { return new FeatureToggle("LogLevel"); }

        /// <summary>
        /// Determines if the system should submit telemetry such as events, counts, timings, and gauges.
        /// Evaluates to true if we should submit telemetry, or false if we should not.
        /// </summary>
        /// <returns></returns>
        public static FeatureToggle TelemetrySubmission() { return new FeatureToggle("TelemetrySubmission"); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        private FeatureToggle(string key)
        {
            _key = key;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _key;
        }

        /// <summary>
        /// 
        /// </summary>
        private readonly string _key;
    }
}
