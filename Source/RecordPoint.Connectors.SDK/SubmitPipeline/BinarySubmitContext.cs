using System.IO;

namespace RecordPoint.Connectors.SDK.SubmitPipeline
{
    /// <summary>
    /// A SubmitContext to be used specifically when submitting binaries 
    /// to the Records365 vNext Connector API. Use with the HttpSubmitBinaryPipelineElement.
    /// </summary>
    public class BinarySubmitContext : SubmitContext
    {
        /// <summary>
        /// The ExternalId of the Item that the submitted binary is to be associated with.
        /// </summary>
        public string ItemExternalId { get; set; }

        /// <summary>
        /// The ExternalId of the binary.
        /// </summary>
        public string ExternalId { get; set; }

        /// <summary>
        /// An optional FileName for the binary.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// A readable stream of binary data to be included in the submit.
        /// </summary>
        public Stream Stream { get; set; }

        /// <summary>
        /// Retrieve the title from the strongly typed FileName field instead of the Core Metadata for binaries
        /// </summary>
        /// <returns></returns>
        protected override string GetTitle()
        {
            return !string.IsNullOrEmpty(FileName) ? FileName : NoTitleFound;
        }
    }
}
