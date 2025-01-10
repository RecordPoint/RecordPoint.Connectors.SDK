namespace RecordPoint.Connectors.SDK.Toggles
{
    /// <summary>
    /// Defines a feature toggle provider
    /// </summary>
    /// <remarks>
    /// A feature toggle provider is effectively a set of key-value pairs. This values
    /// are used describe whether feature are available or not.
    /// 
    /// The expected use case is to provide feature toggles using launch darkly
    /// </remarks>
    public interface IToggleProvider
    {
        /// <summary>
        /// Get a Boolean toggle value
        /// </summary>
        /// <param name="toggle">toggle to get</param>
        /// <param name="default">Default value</param>
        /// <returns>Toggle value</returns>
        bool GetToggleBool(string toggle, bool @default);

        /// <summary>
        /// Get a Boolean toggle value
        /// </summary>
        /// <param name="toggle">toggle to get</param>
        /// <param name="userKey">Targeted user key</param>
        /// <param name="default">Default value</param>
        /// <returns>Toggle value</returns>
        bool GetToggleBool(string toggle, string userKey, bool @default);
    }
}
