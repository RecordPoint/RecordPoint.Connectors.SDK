using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.SubmitPipeline
{
    /// <summary>
    /// An extended version of the <see cref="SubmitContext" /> class including list of binaries 
    /// information sent via the Binary submission pipeline
    /// </summary>
    public class ItemSubmitContext : SubmitContext
    {
        /// <summary>
        /// List of binaries sent to binary pipeline and linked to this item
        /// </summary>
        public IList<DirectBinarySubmissionInputModel> BinariesSubmitted { get; set; }
    }
}
