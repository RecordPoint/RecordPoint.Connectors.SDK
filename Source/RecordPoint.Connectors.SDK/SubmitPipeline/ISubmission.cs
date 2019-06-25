using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.SubmitPipeline
{
    /// <summary>
    /// Top level interface for classes that submit content or otherwise 
    /// influence the submission behaviour. 
    /// </summary>
    public interface ISubmission
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="submitContext"></param>
        /// <returns></returns>
        Task Submit(SubmitContext submitContext); 
    }
}
