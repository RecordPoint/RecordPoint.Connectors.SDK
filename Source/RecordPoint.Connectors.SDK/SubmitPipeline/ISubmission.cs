using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.SubmitPipeline
{
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
