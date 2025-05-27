namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Possible results of content retrieval operations
    /// </summary>
    public enum BinaryRetrievalResultType
    {
        /// <summary>
        /// We successfully got the content item requested
        /// </summary>
        Complete,
        /// <summary>
        /// We failed to get the content item due to an error
        /// </summary>
        Failed,
        /// <summary>
        /// The content cannot be obtained because it has been deleted
        /// </summary>
        [Obsolete("Use Abandoned instead")]
        Deleted,
        /// <summary>
        /// The BinaryStream has a length of zero and can't be written to blob
        /// </summary>
        [Obsolete("Use Abandoned instead")]
        ZeroBinary,
        /// <summary>
        /// Content Source has throttled requests
        /// </summary>
        BackOff,
        /// <summary>
        /// We decided the content item should not be submitted, should be abandoned
        /// </summary>
        Abandoned,
    }

}
