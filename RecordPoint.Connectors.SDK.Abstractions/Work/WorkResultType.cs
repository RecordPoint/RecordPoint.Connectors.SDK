namespace RecordPoint.Connectors.SDK.Work
{
    /// <summary>
    /// Work results
    /// </summary>
    public enum WorkResultType
    {
        /// <summary>
        /// Unit of work completed sucessfully
        /// </summary>
        Complete,
        /// <summary>
        /// Unit of work has critically failed
        /// </summary>
        Failed,
        /// <summary>
        /// Unit of work was abandoned and should be retried
        /// </summary>
        Abandoned,
        /// <summary>
        /// Unit of work should be deferred till a later time
        /// </summary>
        Deferred,
        /// <summary>
        /// Unit of work should move to dead letter immediately
        /// </summary>
        DeadLetter
    }
}
