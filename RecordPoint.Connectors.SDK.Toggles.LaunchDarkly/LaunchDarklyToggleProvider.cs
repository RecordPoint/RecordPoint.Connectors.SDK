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

    }
}