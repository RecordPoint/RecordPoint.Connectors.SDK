using System.Collections.Generic;

namespace RecordPoint.Connectors.SDK.Toggles.Development
{
    /// <summary>
    /// Toggle provider that returns values set in the Toggles dictionary, otherwise just returns the default value.
    /// </summary>
    /// <remarks>
    /// Intended for use in development
    /// </remarks>
    public class DictionaryToggleProvider : IToggleProvider
    {
        /// <summary>
        /// Gets the toggles.
        /// </summary>
        public Dictionary<string, bool> Toggles { get; } = new Dictionary<string, bool>();

        /// <summary>
        /// Get toggle bool.
        /// </summary>
        /// <param name="toggle">The toggle.</param>
        /// <param name="default">If true, default.</param>
        /// <returns>A bool</returns>
        public bool GetToggleBool(string toggle, bool @default)
        {
            return Toggles.GetValueOrDefault(toggle, @default);
        }

        /// <summary>
        /// Get toggle bool.
        /// </summary>
        /// <param name="toggle">The toggle.</param>
        /// <param name="userKey">The user key.</param>
        /// <param name="default">If true, default.</param>
        /// <returns>A bool</returns>
        public bool GetToggleBool(string toggle, string userKey, bool @default)
        {
            return Toggles.GetValueOrDefault(toggle, @default);
        }
    }
}
