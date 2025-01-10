using System.Collections.Generic;

namespace RecordPoint.Connectors.SDK.Toggles.Development.LocalJsonToggles
{
    /// <summary>
    /// This is to serve as a model for feature toggles. So this will store boolean values for each feature toggle and user keys for each toggle.
    /// The feature toggles will only have a boolean value as this class has limited scope.
    /// </summary>
    public class FeatureToggleModel()
    {
        /// <summary>
        /// This is the value of the toggle.
        /// </summary>
        public bool Value { get; set; }

        /// <summary>
        /// This contains the UserKeys that have an override for this toggle.
        /// </summary>
        public Dictionary<string, bool> UserKeyOverrides { get; set; } = new();
    }
}
