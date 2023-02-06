namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Possible results of content retrieval operations
    /// </summary>
    public enum BinaryRetrievalResultType
    {
        Complete,   // We successfully got the content item requested
        Failed,     // We failed to get the content item due to an error
        Deleted,    // The content cannot be obtained because it has neen deleted
        ZeroBinary, // The BinaryStream has a length of zero and can't be written to blob
        BackOff     // Content Source has throttled requests
    }

}
