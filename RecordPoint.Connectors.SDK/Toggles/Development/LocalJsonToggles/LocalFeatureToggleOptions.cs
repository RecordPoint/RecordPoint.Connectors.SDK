namespace RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles
{
    /// <summary>
    /// Options that allows for the configuration of the LocalFeatureToggleProvider
    /// </summary>
    public class LocalFeatureToggleOptions
    {
        /// <summary>
        /// Configuration section name
        /// </summary>
        public const string SECTION_NAME = "LocalFeatureToggleOptions";

        /// <summary>
        /// Path to the json file containing the feature toggles
        /// </summary>
        public string JsonFilePath { get; set; } = string.Empty;
    }
}
