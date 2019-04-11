using System;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.SubmitPipeline
{
    /// <summary>
    /// Pipeline element stream binary directly to blob storage
    /// </summary>
    public class DirectSubmitBinaryPipelineElement : SubmitPipelineElementBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next"></param>
        public DirectSubmitBinaryPipelineElement(ISubmission next) : base(next)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="submitContext"></param>
        /// <returns></returns>
        public override async Task Submit(SubmitContext submitContext)
        {
            var context = submitContext as BinarySubmitContext;
            // TODO: - Make API call to verify item existence and SAS token
            //       - Use the SAS token to upload to blob, respect max size
            //       - Retry deferred result from the platform
            //       - Re-get SAS token if stream fails
            //       - Do not stream if connector content protection is disabled
            //       - Log anything other exceptions
            //       - Make API call once upload is finished to inform the platform
            throw new NotImplementedException();
        }
    }
}
