namespace RecordPoint.Connectors.SDK.Work
{
    /// <summary>
    /// Work results
    /// </summary>
    public enum WorkResultType
    {
        Complete,  // Unit of work completed sucessfully
        Failed, // Unit of work has critically failed
        Abandoned, // Unit of work was abandoned and should be retried
        Deferred, // Unit of work should be deferred till a later time
        DeadLetter // Unit of work should move to dead letter immediately
    }
}
