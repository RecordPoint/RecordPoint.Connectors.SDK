using RecordPoint.Services.Common.FeatureToggles;

namespace RecordPoint.Connectors.SDK.Toggles.LaunchDarkly
{
    /// <summary>
    /// Launch darkly feature toggle class for use with the connector sdk
    /// </summary>
    public class FeatureToggle : IFeatureToggle
    {
        private string _toggleKey;

        /// <summary>
        /// sets toggle key
        /// </summary>
        /// <param name="toggleKey"></param>
        public FeatureToggle(string toggleKey)
        {
            _toggleKey = toggleKey;
        }

        /// <summary>
        /// Overrides to string method for feature toggle class
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => _toggleKey;
    }
}
