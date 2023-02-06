
namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Possible results of record disposal action
    /// </summary>
    public enum RecordDisposalResultType
    {
        Complete,   // We successfully disposed of the record
        Failed,     // We failed to dispose of the record
        Deleted,    // The record cannot be disposed because it has already been deleted
        BackOff     // Content Source has throttled requests
    }
}
