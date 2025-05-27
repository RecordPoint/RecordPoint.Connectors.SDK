using RecordPoint.Services.Common.FeatureToggles;

namespace RecordPoint.Connectors.SDK.Toggles.LaunchDarkly
{
    /// <summary>
    /// The launch darkly toggle provider.
    /// </summary>
    public sealed class LaunchDarklyToggleProvider : IToggleProvider
    {
        /// <summary>
        /// Launch darkly feature toggle provider.
        /// </summary>
        private readonly IFeatureToggleProvider _launchDarklyFeatureToggleProvider;

        /// <summary>
        /// Launch darkly feature toggle provider class for use with the connector sdk
        /// </summary>
        /// <param name="launchDarklyFeatureToggleProvider"></param>
        public LaunchDarklyToggleProvider(IFeatureToggleProvider launchDarklyFeatureToggleProvider)
        {
            _launchDarklyFeatureToggleProvider = launchDarklyFeatureToggleProvider;
        }

        /// <summary>
        /// Get toggle value for given toggle (non tenanted)
        /// </summary>
        /// <param name="toggle"></param>
        /// <param name="default"></param>
        /// <returns></returns>
        public bool GetToggleBool(string toggle, bool @default)
            => _launchDarklyFeatureToggleProvider.GetBoolValue(new FeatureToggle(toggle), defaultValue: @default);

        /// <summary>
        /// Get a tenanted toggle value for given toggle
        /// </summary>
        /// <param name="toggle"></param>
        /// <param name="userKey"></param>
        /// <param name="default"></param>
        /// <returns></returns>
        public bool GetToggleBool(string toggle, string userKey, bool @default)
            => _launchDarklyFeatureToggleProvider.GetBoolValue(new FeatureToggle(toggle), userKey, @default);

        /// <inheritdoc/>
        public int GetToggleNumber(string toggle, string userKey, int @default)
            => _launchDarklyFeatureToggleProvider.GetNumberValue(new FeatureToggle(toggle), userKey, @default);

        /// <inheritdoc/>
        public int GetToggleNumber(string toggle, int @default)
            => _launchDarklyFeatureToggleProvider.GetNumberValue(new FeatureToggle(toggle), @default);

        /// <inheritdoc/>
        public string? GetToggleString(string toggle, string userKey, string? @default = null)
            => _launchDarklyFeatureToggleProvider.GetStringValue(new FeatureToggle(toggle), userKey, @default);

        /// <inheritdoc/>
        public string? GetToggleString(string toggle, string? @default = null)
            => _launchDarklyFeatureToggleProvider.GetStringValue(new FeatureToggle(toggle), @default);
    }
}