namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Types/Statuses representing Channel Discovery result.
    /// </summary>
    public enum ChannelDiscoveryResultType
    {
        /// <summary>
        /// Represents Channel Discovery work for a Connector Source is fully complete.
        /// </summary>
        Complete,
        /// <summary>
        /// Represents Channel Discovery work for a Connector Source has failed, although is subjected to limited retry attempts.
        /// </summary>
        Failed,
        /// <summary>
        /// Represents Channel Discovery work for a Connector Source has encountered a likely transient error, such as throttling,
        /// so it is appropriate to defer and retry when error condition(s) subside.
        /// </summary>
        BackOff,
        /// <summary>
        /// Represents Channel Discovery work for a Connector Source is not complete, and that further work needs to be done,
        /// potentially via batching and cursor.
        /// </summary>
        Incomplete,
        /// <summary>
        /// Represents Channel Discovery work for a Connector Source has been abandoned due to invalid or unsupported operation(s).
        /// </summary>
        Abandoned
    }
}
