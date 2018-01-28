using System.Threading.Tasks;
using System.Linq;

namespace RecordPoint.Connectors.SubmitPipeline
{
    public class PipelineSelectorPipelineElement : SubmitPipelineElementBase
    {
        private ISubmission _submitAggregation = null;

        public PipelineSelectorPipelineElement(ISubmission submitRecord, ISubmission submitAggregation)
            : base(submitRecord)
        {
            _submitAggregation = submitAggregation;
        }

        public override async Task Submit(SubmitContext submitContext)
        {
            int itemTypeId = 0;
            var rawItemType = submitContext.CoreMetaData?.Where(x => x.Name == Fields.ItemTypeId)?.FirstOrDefault()?.Value;
            bool itemTypeValid = int.TryParse(rawItemType, out itemTypeId);

            if(itemTypeValid)
            {
                if(itemTypeId == 1)
                {
                    // submit using the folder submission pipeline
                    if(_submitAggregation != null)
                    {
                        await _submitAggregation.Submit(submitContext).ConfigureAwait(false);
                    }
                    else
                    {
                        SkipNext(submitContext, "No aggregation pipeline was provided!");
                        return;
                    }
                }
                else
                {
                    // submit using the default submission pipeline
                    await InvokeNext(submitContext).ConfigureAwait(false);
                }
            }
            else
            {
                SkipNext(submitContext, $"Item type {rawItemType} is invalid");
                return;
            }
        }
    }
}
