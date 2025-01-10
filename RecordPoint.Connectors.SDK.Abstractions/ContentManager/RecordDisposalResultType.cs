
namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Possible results of record disposal action
    /// </summary>
    public enum RecordDisposalResultType
    {
        /// <summary>
        /// We successfully disposed of the record
        /// </summary>
        Complete,
        /// <summary>
        /// We failed to dispose of the record
        /// </summary>
        Failed,
        /// <summary>
        /// The record cannot be disposed because it has already been deleted
        /// </summary>
        Deleted,
        /// <summary>
        /// Content Source has throttled requests
        /// </summary>
        BackOff
    }
}
