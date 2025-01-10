namespace RecordPoint.Connectors.SDK.Toggles.Null
{
    /// <summary>
    /// Toggle provider that adds no funtionality and just passes through the default values
    /// </summary>
    public class NullToggleProvider : IToggleProvider
    {
        /// <summary>
        /// Get toggle bool.
        /// </summary>
        /// <param name="toggle">The toggle.</param>
        /// <param name="default">If true, default.</param>
        /// <returns>A bool</returns>
        public bool GetToggleBool(string toggle, bool @default)
        {
            return @default;
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
            return @default;
        }
    }
}
