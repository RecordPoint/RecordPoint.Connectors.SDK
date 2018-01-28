using System.Threading.Tasks;

namespace RecordPoint.Connectors.SubmitPipeline
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
