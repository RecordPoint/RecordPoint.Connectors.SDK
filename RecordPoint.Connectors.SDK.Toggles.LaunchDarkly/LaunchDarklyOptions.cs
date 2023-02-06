namespace RecordPoint.Connectors.SDK.Toggles.LaunchDarkly
{
    public class LaunchDarklyOptions
    {
        public const string SECTION_NAME = "LaunchDarkly";

        /// <summary>
        /// Sdk key used to access launch darkly
        /// </summary>
        public string SdkKey { get; set; } = string.Empty;

        /// <summary>
        /// Default user key used to fetch toggle settings.
        /// </summary>
        /// <remarks>
        /// Set to a personal value to change settings without impacting other users
        /// </remarks>
        public string DefaultUserKey { get; set; } = string.Empty;
    }
}