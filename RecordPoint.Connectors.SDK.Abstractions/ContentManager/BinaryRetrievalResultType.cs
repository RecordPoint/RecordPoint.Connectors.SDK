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
        /// The content cannot be obtained because it has neen deleted
        /// </summary>
        Deleted,
        /// <summary>
        /// The BinaryStream has a length of zero and can't be written to blob
        /// </summary>
        ZeroBinary,
        /// <summary>
        /// Content Source has throttled requests
        /// </summary>
        BackOff
    }

}
